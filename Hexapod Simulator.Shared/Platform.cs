using System;
using System.Collections.Generic;
using GFunctions.Mathematics;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    public class Platform : IPlatform
    {
        private double radius = 1; //radius of joints [mm]
        private double jointAngle = 0; //angle of base nodes from 120 [deg]
        private double[] defaultPos = new double[] { 0, 0, 0 }; //Default starting position (X, Y, Z) [mm]
        private double[] translation = new double[] { 0, 0, 0 }; //Platform translation (X, Y, Z) [mm]
        private double[] rotation = new double[] { 0, 0, 0 }; //Platform rotation (Pitch, Roll, Yaw) [deg]
        private double[] rotationCenter = new double[] { 0, 0, 0 }; //Platform relative rotation center (x,y,z) [mm]     
        private bool fixedRotationCenter = false; //if false, rotation center moves to follow translation

        //----------- Public get + set properties ----------------------
        public string Name { get; set; }
        public double Radius
        {
            get { return this.radius; }
            set
            {
                this.radius = value;
                CalcLocalCoords();
            }
        }
        public double JointAngle
        {
            get { return this.jointAngle; }
            set
            {
                this.jointAngle = value;
                CalcLocalCoords();
            }
        }
        public bool FixedRotationCenter
        {
            get { return this.fixedRotationCenter; }
            set { this.fixedRotationCenter = value; CalcGlobalCoords(); }
        }
        public TranslationModes TranslationMode { get; set; } = TranslationModes.Instant;

        //----------- Public get properties ----------------------
        public double[][] LocalJointCoords { get; private set; } //XYZ pos [mm] of each joint, without trans/rotation 
        public double[][] GlobalJointCoords { get; private set; } //XYZ pos [mm] of each joint
        public double[] TranslationTarget { get; private set; } = new double[] { 0, 0, 0 }; //the platform target translation (X, Y, Z) [mm]
        public double[] RotationTarget { get; private set; } = new double[] { 0, 0, 0 }; //the platform target rotation (Pitch, Roll, Yaw) [deg]
        public double[] DefaultPos
        {
            get { return this.defaultPos; }
            private set
            {
                this.defaultPos = value;
                CalcGlobalCoords();
            }
        }
        public double[] Translation
        {
            get { return this.translation; }
            private set
            {
                this.translation = value;
                CalcGlobalCoords();
            }
        } //actual platform translation (may be different than target if mode isn't instant)
        public double[] Rotation
        {
            get { return this.rotation; }
            private set
            {
                this.rotation = value;
                CalcGlobalCoords();
            }
        } //actual platform rotation (may be different than target if mode isn't instant)
        public double[] RotationCenter
        {
            get
            {
                if (fixedRotationCenter)
                {
                    return this.rotationCenter;
                }
                else
                {
                    var output = new double[] { 0, 0, 0 };
                    for (int i = 0; i < this.Translation.Length; i++)
                    {
                        output[i] = this.rotationCenter[i] + this.Translation[i];
                    }
                    return output;
                }
            }
            private set
            {
                this.rotationCenter = value;
                CalcGlobalCoords();
            }
        } //includes translation if FixedRotationCenter = false
        public double[] Position
        {
            get
            {
                var output = new double[] { 0, 0, 0 };

                for (int i = 0; i < this.Translation.Length; i++)
                {
                    output[i] = this.Translation[i] + this.DefaultPos[i];
                }

                return output;
            }
        } //absolute position (default offset + translation)
        public double[] AbsRotationCenter
        {
            get
            {
                var output = new double[] { 0, 0, 0 };

                for (int i = 0; i < this.Translation.Length; i++)
                {
                    output[i] = this.RotationCenter[i] + this.DefaultPos[i];
                }

                return output;
            }
        } //absolute rotation center (default offset + rotation center)
        public double[] NormalVector { get; private set; } //unit vector of platform normal coords (x,y,z)


        public event EventHandler RedrawRequired;
        public event EventHandler PositionChanged; //rotation or translation has changed
        public event EventHandler LocalCoordsChanged; //the local coordinates of the model have changed


        public Platform(string name, double radius, double jointAngle, double[] defaultPos = null)
        {
            //------------- Initialize Everything ------------------------

            this.Name = name;
            this.LocalJointCoords = new double[6][];
            this.GlobalJointCoords = new double[6][];

            this.UpdateConfig(radius, jointAngle, defaultPos); //will cause local/global coords to calculate

            //------------ Setup servo controllers -------------------------
            InitializeServoControllers();
           
        }
        public void TranslateAbs(double[] Pos)
        {
            TranslationTarget = Pos;

            if (TranslationMode == TranslationModes.Instant)
                Translation = TranslationTarget;           
        }
        public void TranslateRel(double[] Pos)
        {
            double[] newPos = new double[] { 0, 0, 0 };

            for (int i = 0; i < this.Translation.Length; i++)
            {
                newPos[i] = this.Translation[i] + Pos[i];
            }

            TranslationTarget = newPos;

            if (TranslationMode == TranslationModes.Instant)
                Translation = TranslationTarget;
        }
        public void RotateAbs(double[] Rot)
        {
            RotationTarget = Rot;

            if (TranslationMode == TranslationModes.Instant)
                Rotation = RotationTarget;
        }
        public void UpdateConfig(double radius, double jointAngle, double[] defaultPos)
        {
            this.radius = radius;
            this.jointAngle = jointAngle;

            if (defaultPos != null)
            {
                this.defaultPos = defaultPos;
            }

            CalcLocalCoords();
        } //allows whole config to be updated without many re-calculations
        public void UpdateRotationCenter(double[] Position, bool FixedPosition)
        {
            this.fixedRotationCenter = FixedPosition;
            this.RotationCenter = Position; //will also re-calculate coords

        } //allows whole rotation center to be updated without many re-calculations
        public void ResetPosition()
        {
            TranslationTarget = new double[] { 0, 0, 0 };
            RotationTarget = new double[] { 0, 0, 0 };

            Translation = new double[] { 0, 0, 0 };
            Rotation = new double[] { 0, 0, 0 };

            //Reset the servo controllers in case the integral is wound up. 
            InitializeServoControllers();
        } //hard resets the translation and rotation

        //---------------------------- Servo Translation Stuff ---------------------------------
        private List<PIDController> posControllers = new List<PIDController>();
        private List<PIDController> rotControllers = new List<PIDController>();

        public void CalculateTimeStep(double TimeStep)
        {
            var output = new double[] { 0, 0, 0 };

            //--------------------- Position ------------------------------

            for (int i = 0; i < TranslationTarget.Length; i++)
            {
                output[i] = posControllers[i].CalculateOutput(TranslationTarget[i] - this.Translation[i], TimeStep);
            }
            this.translation = output;

            //--------------------- Rotation ------------------------------
            var output2 = new double[] { 0, 0, 0 };

            for (int i = 0; i < RotationTarget.Length; i++)
            {
                output2[i] = rotControllers[i].CalculateOutput(RotationTarget[i] - this.Rotation[i], TimeStep);
            }
            this.rotation = output2;

            //----------- Cleanup ------------------------------------------

            CalcGlobalCoords();
        }
        private void InitializeServoControllers()
        {
            posControllers = new List<PIDController>();
            rotControllers = new List<PIDController>();

            for (int i = 0; i < 3; i++)
            {
                PIDController posController = new PIDController(-1, 0.5, 3, 0);
                posControllers.Add(posController);

                PIDController rotController = new PIDController(-1, 0.5, 3, 0);
                rotControllers.Add(rotController);
            }
        }

        //---------------- Geometry Calculation functions -----------------    
        private void CalcLocalCoords()
        {
            LocalJointCoords = CalcLocalCoords(JointAngle, Radius);
            
            //notify parents of the change
            LocalCoordsChanged?.Invoke(this, new EventArgs());
    
            CalcGlobalCoords(); //will also have changed if local coords have changed                         
        } //also forces global coords to be updated
        public double[] CalcGlobalCoord(double[] localcoord)
        {
            return KinematicMath.CalcGlobalCoord2(localcoord, this.Translation, this.DefaultPos, this.Rotation, this.RotationCenter);
        } //calculates XYZ global coords to a locally specified point anywhere on the platform
        private void CalcGlobalCoords()
        {
            if (this.LocalJointCoords != null && this.Translation != null && this.Rotation != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    this.GlobalJointCoords[i] = CalcGlobalCoord(this.LocalJointCoords[i]);
                }
                CalcNormalVector();


                PositionChanged?.Invoke(this, new EventArgs());
                RedrawRequired?.Invoke(this, new EventArgs());
            }
        }
        private void CalcNormalVector()
        {
            double[] normal = new double[] { 0, 0, 1 }; //local vector without rotation
            this.NormalVector = GFunctions.Mathnet.KinematicMath.RotateVector(normal, this.Rotation);
        }

        public static double CalcJointOffsetAngle(int Index, double OffsetAngle)
        {
            double angle = 0;

            if (Index % 2 == 0)
            { //index is even
                angle -= OffsetAngle;
            }
            else //index is odd
            {
                angle += OffsetAngle;
            }

            angle += 120 * ((Index - Index % 2) / 2); //each time index jumps by 2, need to add 120 degree spacing

            return angle;
        }
        public static double[][] CalcLocalCoords(double offsetAngle, double radius)
        {
            var output = new double[6][];

            for (int i = 0; i < 6; i++)
                output[i] = CalcLocalCoord(i, offsetAngle, radius);

            return output;
        }
        private static double[] CalcLocalCoord(int Index, double OffsetAngle, double Radius)
        {
            if (Index < 0 || Index > 5)
                throw new IndexOutOfRangeException("Index out of range when grabbing local hexapod coords.");

            var output = new double[] { 0, 0, 0 };
            double angle = Platform.CalcJointOffsetAngle(Index, OffsetAngle);

            double X = Math.Cos(angle * Math.PI / 180.0) * Radius;
            double Y = Math.Sin(angle * Math.PI / 180.0) * Radius;

            output = new double[] { X, Y, 0 };
            return output;
        }

    }
}
