using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra.Double;



namespace Hexapod_Simulator
{
    public class Hexapod : IGLDrawable
    {

        public IActuator[] Actuators { get; private set; }  
        public Platform Base { get; private set; }
        public Platform Top { get; private set; }
        public bool IsDrawn { get; set; }

        public event EventHandler RedrawRequired;
        public event EventHandler ServosCalculated;

        public Hexapod(double defaultHeight, double BaseRad, double TopRad, double BaseAngle, double TopAngle)
        {
            this.IsDrawn = true;
            
            this.Base = new Platform("Base", BaseRad, BaseAngle);
            this.Top = new Platform("Top", TopRad, TopAngle, new double[]{0,0, defaultHeight});

            this.Base.RedrawRequired += this.RaiseSubRedraws;
            this.Top.RedrawRequired += this.RaiseSubRedraws;

            this.Base.PositionChanged += this.PlatformCoordsChanged;
            this.Top.PositionChanged += this.PlatformCoordsChanged;

            InitializeActuators(ActuatorTypes.Linear); 
        } 
        public void Draw()
        {
            Base.Draw();
            Top.Draw();

            for (int i = 0; i < 6; i++)
            {
                //GLObjects.Line(Color.Blue, Base.GlobalJointCoords[i], Top.GlobalJointCoords[i]);
            }
          
            foreach (IActuator Motor in Actuators)
            {
                Motor.Draw();
            }
        }

        public void InitializeActuators(ActuatorTypes AType)
        {
            switch (AType)
            {
                case ActuatorTypes.Linear:
                    Actuators = new LinearActuator[6];

                    for (int i = 0; i < 6; i++)
                    {
                        Actuators[i] = new LinearActuator(20, 40, Base.GlobalJointCoords[i], Top.GlobalJointCoords[i]);
                    }
                    break;

                case ActuatorTypes.Rotary:
                    Actuators = new RotaryActuator[6];

                    for (int i = 0; i < 6; i++)
                    {
                        double motorAngle = RotaryActuator.calcMotorOffsetAngle(i, 0, Platform.CalcJointOffsetAngle(i, Base.JointAngle));
                        Actuators[i] = new RotaryActuator(10, motorAngle, 40, Base.GlobalJointCoords[i], Top.GlobalJointCoords[i]);
                    }
                    break; 

                default:
                    break;
            }
        }

        private void PlatformCoordsChanged(object sender, EventArgs e)
        {
            Platform plat = (Platform)sender;

            if(plat == this.Top)
            {
                for (int i = 0; i < Actuators.Length; i++)
                {
                    Actuators[i].LinkEndPosition = Top.GlobalJointCoords[i];
                }
            }
            if (plat == this.Base)
            {
                for (int i = 0; i < Actuators.Length; i++)
                {
                    Actuators[i].Position = Base.GlobalJointCoords[i];
                }
            }

            if (this.ServosCalculated != null)
                ServosCalculated(this, new EventArgs());
        }
        private void RaiseSubRedraws(object sender, EventArgs e)
        {
            if(this.RedrawRequired != null)
                RedrawRequired(this, new EventArgs());
        }

      
    }
    public class Platform : IGLDrawable
    {
        private double radius = 1; //radius of joints [mm]
        private double jointAngle = 0; //angle of base nodes from 120 [deg]
        private double[] defaultPos = new double[] { 0, 0, 0 }; //Default starting position (X, Y, Z) [mm]
        private double[] translation = new double[] { 0, 0, 0 }; //Platform translation (X, Y, Z) [mm]
        private double[] rotation = new double[] { 0, 0, 0 }; //Platform rotation (Pitch, Roll, Yaw) [deg]
        private double[] rotationCenter = new double[] { 0, 0, 0 }; //Platform relative rotation center (x,y,z) [mm]     
        private double[] normalVector = new double[] { 0, 0, 1 }; //unit vector of platform normal coords (x,y,z)

        private bool fixedRotationCenter = false; //if false, rotation center moves to follow translation

        public double[][] LocalJointCoords { get; private set; } //XYZ pos [mm] of each joint, without trans/rotation 
        public double[][] GlobalJointCoords { get; private set; } //XYZ pos [mm] of each joint

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
        } 
        public double[] Rotation 
        {
            get { return this.rotation; }
            private set
            {
                this.rotation = value;
                CalcGlobalCoords();
            }         
        }
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
        public double[] NormalVector
        {
            get { return this.normalVector;  }
            private set { this.normalVector = value;  }
        }

        public bool FixedRotationCenter
        {
            get { return this.fixedRotationCenter; }
            set { this.fixedRotationCenter = value; CalcGlobalCoords(); }
        }

        public string Name { get; set; }
        public bool IsDrawn { get; set; }


        public event EventHandler RedrawRequired;
        public event EventHandler PositionChanged; //rotation or translation has changed

        public Platform(string name, double radius, double jointAngle, double[] defaultPos = null)
        {
            //------------- Initialize Everything ------------------------

            this.IsDrawn = true;
            this.Name = name;
            this.LocalJointCoords = new double[6][];
            this.GlobalJointCoords = new double[6][];

            this.UpdateConfig(radius, jointAngle, defaultPos); //will cause local/global coords to calculate

            //------------ Setup servo controllers -------------------------

            for (int i = 0; i < 3; i++)
            {
                PIDController posController = new PIDController(-1, 0.5, 3, 0);
                posControllers.Add(posController);

                PIDController rotController = new PIDController(-1, 0.5, 3, 0);
                rotControllers.Add(rotController);
            }        
        }

            
        public void TranslateAbs(double[] Pos)
        {
            this.Translation = Pos;
;        }
        public void TranslateRel(double[] Pos)
        {
            double[] newPos = new double[] { 0, 0, 0 };

            for (int i = 0; i < this.Translation.Length; i++)
            {
                newPos[i] = this.Translation[i] + Pos[i];
            }

            this.Translation = newPos;
        }
        public void RotateAbs(double[] Rot)
        {
            this.Rotation = Rot;
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

        public void Draw()
        {
            if (GlobalJointCoords != null)
            {
                if(GlobalJointCoords.Length == 6)
                {
                    for(int i = 0; i < 6; i++)
                    {
                        GLObjects.Cube(System.Drawing.Color.Red, this.GlobalJointCoords[i], 1);

                        if(i != 5)
                        {
                            GLObjects.Line(Color.Green, this.GlobalJointCoords[i], this.GlobalJointCoords[i + 1]);
                        }
                        else
                        {
                            GLObjects.Line(Color.Green, this.GlobalJointCoords[i], this.GlobalJointCoords[0]);
                        }
                        
                    }

                    GLObjects.Cube(System.Drawing.Color.Yellow, this.AbsRotationCenter, 1);                  
                }
            }
        }

        //---------------------------- Servo Stuff ---------------------------------

        private double[] translationTarget = new double[] { 0, 0, 0 };
        private double[] rotationTarget = new double[] { 0, 0, 0 };
        private List<PIDController> posControllers = new List<PIDController>();
        private List<PIDController> rotControllers = new List<PIDController>();
        
        public void RotateAbsServo(double[] Rot)
        {
            rotationTarget = Rot;
        }
        public void TranslateAbsServo(double[] Pos)
        {
            translationTarget = Pos;
        }
        public void CalculateTimeStep(double TimeStep)
        {
            var output = new double[] { 0, 0, 0 };
            
            //--------------------- Position ------------------------------
            
            for (int i = 0; i < translationTarget.Length; i++)
            {
                output[i] = posControllers[i].CalcOutput(translationTarget[i] - this.Translation[i], TimeStep);
            }
            this.translation = output;
             
            //--------------------- Rotation ------------------------------
            var output2 = new double[] { 0, 0, 0 };

            for (int i = 0; i < rotationTarget.Length; i++)
            {
                output2[i] = rotControllers[i].CalcOutput(rotationTarget[i] - this.Rotation[i], TimeStep);
            }     
            this.rotation = output2;

            //----------- Cleanup ------------------------------------------

            CalcGlobalCoords(); 
        }

        //---------------- Geometry Calculation functions -----------------    
        private double[] CalcLocalCoord(int Index, double OffsetAngle, double Radius)
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
        private void CalcLocalCoords()
        {
            for (int i = 0; i < 6; i++)
            {
                this.LocalJointCoords[i] = CalcLocalCoord(i, this.JointAngle, this.Radius);
            }

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

                if (PositionChanged != null)
                    PositionChanged(this, new EventArgs());

                if(RedrawRequired != null)
                    RedrawRequired(this, new EventArgs());    
            }         
        }

        private void CalcNormalVector()
        {
            DenseVector normal = new DenseVector(new double[] { 0, 0, 1 }); //local vector without rotation;
            DenseMatrix rotation = KinematicMath.RotationMatrixFromPRY(this.Rotation);

            DenseVector globalNormal = rotation * normal; //apply rotation

            this.NormalVector = globalNormal.ToArray(); 
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

    }
   

    public class LinearActuator : IActuator
    {
        protected double _maxTravel = 1; //maximum travel [mm]
        protected double _minTravel = 0; //minimum travel [mm]
        private double _linkLength = 0; //link length between actuator tip and top [mm]
         
        protected double[] _position = new double[] { 0, 0, 0 }; //XYZ position actuator tip start point [mm]
        protected double[] _armEndPosition = new double[] { 0, 0, 0 }; //XYZ position actuator tip after movement [mm] 
        private double[] _linkEndPosition = new double[] { 0, 0, 0 }; //XYZ position of link attached to actuator [mm] 

        protected double _travelPosition = 0; //travel position of actuator [mm]      

        protected IterativeSolver Solver; //used for solving kinematics iteratively

        public double MaxTravel
        {
            get { return this._maxTravel; }
            set
            {
                this._maxTravel = value;
                calcMotorAngle();
            }
        }
        public double MinTravel
        {
            get { return this._minTravel; }
            set
            {
                this._minTravel = value;
                calcMotorAngle();
            }
        }
        public double LinkLength
        {
            get { return this._linkLength; }
            set
            {
                this._linkLength = value;
                calcMotorAngle();
            }
        }
     
        public double[] Position
        {
            get { return this._position; }
            set
            {
                this._position = value;
                calcMotorAngle();
            }
        }
        public double[] LinkEndPosition
        {
            get { return this._linkEndPosition; }
            set
            {
                this._linkEndPosition = value;
                calcMotorAngle();
            }
        }

        public double[] ArmEndPosition
        {
            get { return this._armEndPosition; }
        }
        public double TravelPosition
        {
            get { return this._travelPosition; }
        }
        
        public bool SolutionValid { get { return this.Solver.SolutionValid; } }

        public virtual ActuatorTypes ActuatorType { get { return ActuatorTypes.Linear; } }

        public LinearActuator(double maxTravel, double linkLength, double[] position, double[] linkEndPosition)
        {
            this._maxTravel = maxTravel;
            this._position = position;
            this._linkEndPosition = linkEndPosition;
            this._linkLength = linkLength;

            Solver = new IterativeSolver(0.01, 100, 0.01, computeError, this._maxTravel, this._minTravel);
        
            calcMotorAngle();
        }
        public void Draw()
        {
            Color clr = Color.Yellow;

            if (this.SolutionValid == false)
            {
                clr = Color.Red;
            }

            GLObjects.Line(clr, this.Position, this.ArmEndPosition);
            GLObjects.Line(clr, this.LinkEndPosition, this.ArmEndPosition);
        }


        protected void calcMotorAngle()
        {
            this._travelPosition = Solver.Solve(this._travelPosition);

            if(Solver.SolutionValid == false)
                calcArmEndCoords();//need to re-calculate to get geometry back to starting position
        } //iterative solution
        protected double computeError(double iterationValue)
        {
            this._travelPosition = iterationValue; 
            calcArmEndCoords();
            return (KinematicMath.VectorLength(this.ArmEndPosition, this.LinkEndPosition) - this.LinkLength);
        }
        protected virtual void calcArmEndCoords()
        {
            var localCoord = new double[] { 0, 0, this._travelPosition }; //travel is in the Z direction

            double[] coords = new double[] { 0, 0, 0 };

            for (int i = 0; i < 3; i++)
            {
                coords[i] = localCoord[i] + this._position[i];
            }

            this._armEndPosition = coords;
        }

    }
    public class RotaryActuator : LinearActuator, IActuator
    {
        private double _armLength = 1; //arm length [mm], base class constructor will fail if default = 0
        private double _armAngle = 0; //arm angle with respect to vertical [deg]

        public override ActuatorTypes ActuatorType { get { return ActuatorTypes.Rotary; } }

        public RotaryActuator(double armRadius, double armAngle, double linkLength, double[] position, double[] linkEndPosition) : base(180, linkLength, position, linkEndPosition)
        {
            this._armLength = armRadius;
            this._armAngle = armAngle;

            this._maxTravel = 180; //done this way so everything doesn't calculate
            this._minTravel = -180; //done this way so everything doesn't calculate

            this.Solver = new IterativeSolver(0.01, 100, 0.001, computeError, this._maxTravel, this._minTravel);

            calcMotorAngle();
        }

        protected override void calcArmEndCoords()
        {
            var localCoord = new double[] { this._armLength, 0, 0 };
            var trans = this._position;
            var trans2 = new double[] { 0, 0, 0 };
            var rot = new double[] { this._travelPosition, 0, this._armAngle };


            double[] coords = KinematicMath.CalcGlobalCoord(localCoord, trans, trans2, rot);
            this._armEndPosition = coords;
        } //needs to be different for rotary

        public static double calcMotorOffsetAngle(int Index, double armAngle, double jointOffsetAngle)
        {
            double angle = 0;
            angle += jointOffsetAngle;


            if (Index % 2 == 0)
            { //index is even
                angle += 90;
                angle += armAngle;
            }
            else //index is odd
            {
                angle -= 90;
                angle -= armAngle;
            }

            return angle;
        }
    }

    public interface IActuator
    {
        double MaxTravel { get; set; } //min allowable actuator position
        double MinTravel { get; set; }
        double TravelPosition { get; }

        double[] Position { get; set; } //where the base of the actuator is located on the base
        double[] ArmEndPosition { get; } //actuator arm end position, where it attaches to link start
        double[] LinkEndPosition { get; set; } //where the link attaches to the top

        double LinkLength { get; set; }

        bool SolutionValid { get;  }
        ActuatorTypes ActuatorType { get; }

        void Draw();
    }
    public enum ActuatorTypes
    {
        Linear,
        Rotary
    }


    


    public class Ball
    {
        private double[] _position = new double[] { 0, 0, 0 }; //XYZ position of centerpoint [m]
        private double[] _angle = new double[] { 0, 0, 0 }; //PRY rotation about centerpoint [deg]
        
        public double Radius { get; set; } //radius [m]
        public double Density { get; set; } //density [kg/m^3]
        public double Volume
        {
            get
            {
                return (4 / 3.0) * Math.PI * Math.Pow(this.Radius, 3);
            }
        } //volume [m^3]
        public double Mass
        {
            get { return (this.Density * this.Volume); }
        } //mass [kg]

    }
    public class Ball_Test : IGLDrawable
    {         
        private double[] _position = new double[] { 0, 0, 0 }; //XYZ position of centerpoint [m]
        private double[] _velocity = new double[] { 0, 0, 0 }; //XYZ position of centerpoint [m]
        private double[] _acceleration = new double[] { 0, 0, 0 }; //XYZ position of centerpoint [m]
        private double _normalForce = 0; //last calculated normal force magnitude


        public double Radius { get; set; } //radius [m]
        public double Density { get; set; } //density [kg/m^3]
        public double Volume
        {
            get
            {
                return (4 / 3.0) * Math.PI * Math.Pow(this.Radius, 3);
            }
        } //volume [m^3]
        public double Mass
        {
            get { return (this.Density * this.Volume); }
        } //mass [kg]
        public double[] Position
        {
            get { return this._position;  }
        }


        public bool IsDrawn { get; set; }
    
        public event EventHandler RedrawRequired;

        public Ball_Test(double radius, double density, double[] startingPos)
        {
            this.Radius = radius;
            this.Density = density;
            this.IsDrawn = true;
            this._position = startingPos; 

            this._normalForce = this.Mass * 9.81; //give an initial estimate assuming ball is on flat surface
        }



        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, _position, 5);
        }
        public void CalculateTimeStep(double TimeIncrement, DenseVector normalForceVector)
        {
            CalcKineticSolution(normalForceVector); //calculates acceleration
                          
            for (int i = 0; i < 2; i++)
            {
                _velocity[i] = _velocity[i] + Calculus.Integrate(_acceleration[i], TimeIncrement);
                _position[i] = _position[i] + Calculus.Integrate(_velocity[i], TimeIncrement);
            }
        }
        private void CalcKineticSolution(DenseVector normalForceVector)
        {
            double mass = this.Mass;
            DenseVector gravityForceVector = new DenseVector(new double[] { 0, 0, -9.81 * mass * 100 });
            
            
            //this.SolutionValid = false;

            double stepSize = 0.01; //initial change to test
            double maxSteps = 100;
            double errorTolerance = 0.01;

            double startingValue = this._normalForce; //starting guess
            double prevError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, startingValue, mass), normalForceVector.ToArray()); 
            double newError = 0;
            double ratio = 0;

            for (int i = 0; i < maxSteps; i++)
            {
                if (Math.Abs(prevError) <= errorTolerance)
                {
                    //this.SolutionValid = true;

                    this._acceleration = CalcAccelVector(gravityForceVector, normalForceVector, this._normalForce, mass); 
                    return;
                }

                this._normalForce = this._normalForce + stepSize; //change the angle by the stepsize

                newError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, this._normalForce, mass), normalForceVector.ToArray()); 

                ratio = (prevError - newError) / stepSize; //ratio between change in k and error

                stepSize = newError / (ratio * 2.0);

                prevError = newError;
            }

            this._normalForce = startingValue; //solution has failed. Reset to starting. 
 
        } //iterative solution

        private double[] CalcAccelVector(DenseVector gravityForceVector, DenseVector normalForceVector, double normalForce, double mass)
        {
            //a = (Gravity Force + NormalForce)/mass
            
            DenseVector output = (gravityForceVector + (normalForceVector * normalForce)) / mass;
            return output.ToArray(); 
        }
        private double CalcAccelVectorError(double[] accelVector, double[] normalForceVector)
        {
            //vectors should be perpendicular (dot product = 0), error is any value produced
            
            double output = 0;

            for (int i = 0; i < accelVector.Length; i++)
            {
                output += accelVector[i] * normalForceVector[i]; 
            }

            return output; 
        }
    }
    public class Ball_Local_Test : IGLDrawable
    {
        private double[] _globalPosition = new double[] { 0, 0, 0 }; 
        
        private double[] _position = new double[] { 0, 0, 0 }; //XYZ position of centerpoint relative to platform [m]
        private double[] _velocity = new double[] { 0, 0, 0 }; //XYZ velocity of centerpoint [m/s]
        private double[] _acceleration = new double[] { 0, 0, 0 }; //XYZ accel of centerpoint [m/s/s]
        private double _normalForce = 0; //last calculated normal force magnitude


        public double Radius { get; set; } //radius [m]
        public double Density { get; set; } //density [kg/m^3]
        public double Volume
        {
            get
            {
                return (4 / 3.0) * Math.PI * Math.Pow(this.Radius, 3);
            }
        } //volume [m^3]
        public double Mass
        {
            get { return (this.Density * this.Volume); }
        } //mass [kg]
        public double[] Position
        {
            get { return this._position; }
        }


        public bool IsDrawn { get; set; }

        public event EventHandler RedrawRequired;

        public Ball_Local_Test(double radius, double density, double[] startingPos)
        {
            this.Radius = radius;
            this.Density = density;
            this.IsDrawn = true;
            this._position = startingPos;

            this._normalForce = this.Mass * 9.81; //give an initial estimate assuming ball is on flat surface
        } //only calculates local coords

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, _globalPosition, 5);
        }
        public void CalculateTimeStep(double TimeIncrement, DenseVector normalForceVector)
        {
            CalcKineticSolution(normalForceVector); //calculates acceleration

            for (int i = 0; i < 2; i++)
            {
                _velocity[i] = _velocity[i] + Calculus.Integrate(_acceleration[i], TimeIncrement);
                _position[i] = _position[i] + Calculus.Integrate(_velocity[i], TimeIncrement);
            }
        }
        public void UpdateGlobalCoords(double[] globalCoords)
        {
            this._globalPosition = globalCoords; 
        }


        private void CalcKineticSolution(DenseVector normalForceVector)
        {
            double mass = this.Mass;
            DenseVector gravityForceVector = new DenseVector(new double[] { 0, 0, -9.81 * mass * 100 });

            //this.SolutionValid = false;

            double stepSize = 0.01; //initial change to test
            double maxSteps = 100;
            double errorTolerance = 0.01;

            double startingValue = this._normalForce; //starting guess
            double prevError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, startingValue, mass), normalForceVector.ToArray());
            double newError = 0;
            double ratio = 0;

            for (int i = 0; i < maxSteps; i++)
            {
                if (Math.Abs(prevError) <= errorTolerance)
                {
                    //this.SolutionValid = true;

                    this._acceleration = CalcAccelVector(gravityForceVector, normalForceVector, this._normalForce, mass);
                    return;
                }

                this._normalForce = this._normalForce + stepSize; //change the angle by the stepsize

                newError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, this._normalForce, mass), normalForceVector.ToArray());

                ratio = (prevError - newError) / stepSize; //ratio between change in k and error

                stepSize = newError / (ratio * 2.0);

                prevError = newError;
            }

            this._normalForce = startingValue; //solution has failed. Reset to starting. 

        } //iterative solution

        private double[] CalcAccelVector(DenseVector gravityForceVector, DenseVector normalForceVector, double normalForce, double mass)
        {
            //a = (Gravity Force + NormalForce)/mass

            DenseVector output = (gravityForceVector + (normalForceVector * normalForce)) / mass;
            return output.ToArray();
        }
        private double CalcAccelVectorError(double[] accelVector, double[] normalForceVector)
        {
            //vectors should be perpendicular (dot product = 0), error is any value produced

            double output = 0;

            for (int i = 0; i < accelVector.Length; i++)
            {
                output += accelVector[i] * normalForceVector[i];
            }

            return output;
        }
    }



    public class ServoMotor_OLD
    {
        private double _armRadius = 0; //arm radius [mm]
        private double _linkLength = 0; //link length between arm and top [mm]
        private double _armAngle = 0; //arm angle with respect to vertical [deg]  

        private double[] _position = new double[] { 0, 0, 0 }; //XYZ position of pivot [mm]
        private double[] _linkEndPosition = new double[] { 0, 0, 0 }; //XYZ position of link end to top [mm] 

        private double[] _armEndPosition = new double[] { 0, 0, 0 }; //XYZ position of arm end [mm]
        private double _angle = 0; //motor angle [deg]       

        private IterativeSolver Solver; //used for solving kinematics iteratively

        public double ArmRadius
        {
            get { return this._armRadius; }
            set
            {
                this._armRadius = value;
                calcMotorAngle();
            }
        }
        public double LinkLength
        {
            get { return this._linkLength; }
            set
            {
                this._linkLength = value;
                calcMotorAngle();
            }
        }
        public double ArmAngle
        {
            get { return this._armAngle; }
            set
            {
                this._armAngle = value;
                calcMotorAngle();
            }
        }

        public double[] Position
        {
            get { return this._position; }
            set
            {
                this._position = value;
                calcMotorAngle();
            }
        }
        public double[] LinkEndPosition
        {
            get { return this._linkEndPosition; }
            set
            {
                this._linkEndPosition = value;
                calcMotorAngle();
            }
        }


        public double[] ArmEndPosition
        {
            get { return this._armEndPosition; }
        }
        public double Angle
        {
            get { return this._angle; }
        }
        public bool SolutionValid { get { return this.Solver.SolutionValid; } }



        public ServoMotor_OLD(double armRadius, double armAngle, double linkLength, double[] position, double[] linkEndPosition)
        {
            this._armRadius = armRadius;
            this._armAngle = armAngle;
            this._position = position;
            this._linkEndPosition = linkEndPosition;
            this._linkLength = linkLength;

            Solver = new IterativeSolver(0.01, 100, 0.001, computeError, 180, -180);

            calcMotorAngle();
        }
        public void Draw()
        {
            Color clr = Color.Yellow;

            if (this.Solver.SolutionValid == false)
            {
                clr = Color.Red;
            }

            GLObjects.Line(clr, this.Position, this.ArmEndPosition);
            GLObjects.Line(clr, this.LinkEndPosition, this.ArmEndPosition);
        }

        private void calcMotorAngle()
        {
            this._angle = Solver.Solve(this._angle);

            if (Solver.SolutionValid == false)
                calcArmEndCoords();//need to re-calculate to get geometry back to starting position
        } //iterative solution
        private double computeError(double iterationValue)
        {
            this._angle = iterationValue;

            calcArmEndCoords();
            return (KinematicMath.VectorLength(this.ArmEndPosition, this.LinkEndPosition) - this.LinkLength);
        }
        private void calcArmEndCoords()
        {
            var localCoord = new double[] { this._armRadius, 0, 0 };
            var trans = this._position;
            var trans2 = new double[] { 0, 0, 0 };
            var rot = new double[] { this._angle, 0, this._armAngle };


            double[] coords = KinematicMath.CalcGlobalCoord(localCoord, trans, trans2, rot);
            this._armEndPosition = coords;
        }


        public static double calcMotorOffsetAngle(int Index, double armAngle, double jointOffsetAngle)
        {
            double angle = 0;
            angle += jointOffsetAngle;


            if (Index % 2 == 0)
            { //index is even
                angle += 90;
                angle += armAngle;
            }
            else //index is odd
            {
                angle -= 90;
                angle -= armAngle;
            }

            return angle;
        }

    }
    public class RotaryActuator_OLD : IActuator
    {
        private double _maxTravel = 180; //maximum travel [deg]
        private double _minTravel = -180; //minimum travel [deg]
        private double _linkLength = 0; //link length between actuator tip and top [mm]
        private double _armLength = 0; //arm length [mm]
        private double _armAngle = 0; //arm angle with respect to vertical [deg] 

        private double[] _position = new double[] { 0, 0, 0 }; //XYZ position actuator tip start point [mm]
        private double[] _armEndPosition = new double[] { 0, 0, 0 }; //XYZ position actuator tip after movement [mm] 
        private double[] _linkEndPosition = new double[] { 0, 0, 0 }; //XYZ position of link attached to actuator [mm] 

        private double _travelPosition = 0; //travel position of actuator [deg]      

        private IterativeSolver Solver; //used for solving kinematics iteratively

        public double MaxTravel
        {
            get { return this._maxTravel; }
            set
            {
                this._maxTravel = value;
                calcMotorAngle();
            }
        }
        public double MinTravel
        {
            get { return this._minTravel; }
            set
            {
                this._minTravel = value;
                calcMotorAngle();
            }
        }
        public double LinkLength
        {
            get { return this._linkLength; }
            set
            {
                this._linkLength = value;
                calcMotorAngle();
            }
        }

        public double[] Position
        {
            get { return this._position; }
            set
            {
                this._position = value;
                calcMotorAngle();
            }
        }
        public double[] LinkEndPosition
        {
            get { return this._linkEndPosition; }
            set
            {
                this._linkEndPosition = value;
                calcMotorAngle();
            }
        }

        public double[] ArmEndPosition
        {
            get { return this._armEndPosition; }
        }
        public double TravelPosition
        {
            get { return this._travelPosition; }
        }

        public bool SolutionValid { get { return this.Solver.SolutionValid; } }

        public ActuatorTypes ActuatorType { get { return ActuatorTypes.Rotary; } }

        public RotaryActuator_OLD(double armRadius, double armAngle, double linkLength, double[] position, double[] linkEndPosition)
        {
            this._armLength = armRadius;
            this._armAngle = armAngle;
            this._position = position;
            this._linkEndPosition = linkEndPosition;
            this._linkLength = linkLength;

            Solver = new IterativeSolver(0.01, 100, 0.001, computeError, this._maxTravel, this._minTravel);

            calcMotorAngle();
        }
        public void Draw()
        {
            Color clr = Color.Yellow;

            if (this.SolutionValid == false)
            {
                clr = Color.Red;
            }

            GLObjects.Line(clr, this.Position, this.ArmEndPosition);
            GLObjects.Line(clr, this.LinkEndPosition, this.ArmEndPosition);
        }


        private void calcMotorAngle()
        {
            this._travelPosition = Solver.Solve(this._travelPosition);

            if (Solver.SolutionValid == false)
                calcArmEndCoords();//need to re-calculate to get geometry back to starting position
        } //iterative solution
        private double computeError(double iterationValue)
        {
            this._travelPosition = iterationValue;
            calcArmEndCoords();
            return (KinematicMath.VectorLength(this.ArmEndPosition, this.LinkEndPosition) - this.LinkLength);
        }

        private void calcArmEndCoords()
        {
            var localCoord = new double[] { this._armLength, 0, 0 };
            var trans = this._position;
            var trans2 = new double[] { 0, 0, 0 };
            var rot = new double[] { this._travelPosition, 0, this._armAngle };


            double[] coords = KinematicMath.CalcGlobalCoord(localCoord, trans, trans2, rot);
            this._armEndPosition = coords;
        }



    }

}
