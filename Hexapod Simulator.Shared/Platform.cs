using GFunctions.Mathematics;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// The top or bottom platform of a hexapod
    /// </summary>
    public class Platform : IPlatform
    {
        private double _radius = 1; //radius of joints [mm]
        private double _jointAngle = 0; //angle of base nodes from 120 [deg]
        private double[] _defaultPos = [0, 0, 0]; //Default starting position (X, Y, Z) [mm]
        private double[] _translation = [0, 0, 0]; //Platform translation (X, Y, Z) [mm]
        private double[] _rotation = [0, 0, 0]; //Platform rotation (Pitch, Roll, Yaw) [deg]
        private double[] _rotationCenter = [0, 0, 0]; //Platform relative rotation center (x,y,z) [mm]     
        private bool _fixedRotationCenter = false; //if false, rotation center moves to follow translation

        //----------- Public get + set properties ----------------------
        public string Name { get; set; }

        /// <summary>
        /// The radius of the platform joints [mm]
        /// </summary>
        public double Radius 
        { 
            get { return _radius; } 
            set { _radius = value; CalcLocalCoords(); } 
        }

        /// <summary>
        /// The angle of base nodes from an even 120 [deg]
        /// </summary>
        public double JointAngle 
        { 
            get { return _jointAngle; } 
            set { _jointAngle = value; CalcLocalCoords(); } 
        }
        
        /// <summary>
        /// Whether the rotation point is fixed in space, or moves when the platform translates
        /// </summary>
        public bool FixedRotationCenter
        {
            get { return _fixedRotationCenter; }
            set { _fixedRotationCenter = value; CalcGlobalCoords(); }
        }

        /// <summary>
        /// Determines whether the platform will translate instantly, or gradually during a simulation
        /// </summary>
        public TranslationModes TranslationMode { get; set; } = TranslationModes.Instant;

        //----------- Public get properties ----------------------

        /// <summary>
        /// XYZ position [mm] of each joint, without translation/rotation 
        /// </summary>
        public double[][] LocalJointCoords { get; private set; } = new double[6][];

        /// <summary>
        /// XYZ position [mm] of each joint
        /// </summary>
        public double[][] GlobalJointCoords { get; private set; } = new double[6][];

        /// <summary>
        /// The platform target translation (X, Y, Z) [mm]
        /// </summary>
        public double[] TranslationTarget { get; private set; } = [0, 0, 0];

        /// <summary>
        /// The platform target rotation (Pitch, Roll, Yaw) [deg]
        /// </summary>
        public double[] RotationTarget { get; private set; } = [0, 0, 0];

        /// <summary>
        /// Default center position of the platform with no translation
        /// </summary>
        public double[] DefaultPos
        {
            get { return _defaultPos; }
            private set { _defaultPos = value; CalcGlobalCoords(); }
        }

        /// <summary>
        /// The actual platform translation (may be different than target if mode isn't instant)
        /// </summary>
        public double[] Translation
        {
            get { return _translation; }
            private set { _translation = value; CalcGlobalCoords(); }
        }

        /// <summary>
        /// The actual platform rotation (Pitch, Roll, Yaw) [deg], may be different than target if mode isn't instant
        /// </summary>
        public double[] Rotation
        {
            get { return _rotation; }
            private set { _rotation = value; CalcGlobalCoords(); }
        }

        /// <summary>
        /// Rotation including translation if <see cref="FixedRotationCenter"/> is false
        /// </summary>
        public double[] RotationCenter
        {
            get
            {
                if (_fixedRotationCenter)
                {
                    return _rotationCenter;
                }
                else
                {
                    var output = new double[] { 0, 0, 0 };
                    for (int i = 0; i < Translation.Length; i++)
                    {
                        output[i] = _rotationCenter[i] + Translation[i];
                    }
                    return output;
                }
            }
            private set
            {
                _rotationCenter = value;
                CalcGlobalCoords();
            }
        }

        /// <summary>
        /// Absolute center position of the platform (default offset + translation), (X, Y, Z) [mm]
        /// </summary>
        public double[] Position
        {
            get
            {
                var output = new double[] { 0, 0, 0 };

                for (int i = 0; i < Translation.Length; i++)
                {
                    output[i] = Translation[i] + DefaultPos[i];
                }

                return output;
            }
        }

        /// <summary>
        /// The absolute rotation center position (default offset + rotation center), (X, Y, Z) [mm]
        /// </summary>
        public double[] AbsRotationCenter
        {
            get
            {
                var output = new double[] { 0, 0, 0 };

                for (int i = 0; i < Translation.Length; i++)
                {
                    output[i] = RotationCenter[i] + DefaultPos[i];
                }

                return output;
            }
        }

        /// <summary>
        /// The unit vector of platform normal direction (x,y,z)
        /// </summary>
        public double[] NormalVector { get; private set; } = [0, 0, 1];

        //----------- Events ----------------------

        /// <summary>
        /// A redraw of the platform is needed in the view
        /// </summary>
        public event EventHandler? RedrawRequired;

        /// <summary>
        /// Rotation or translation of the platform has changed
        /// </summary>
        public event EventHandler? PositionChanged;

        /// <summary>
        /// the local coordinates of the platform have changed
        /// </summary>
        public event EventHandler? LocalCoordsChanged;

        //----------- Public Methods ----------------------

        public Platform(string name, double radius, double jointAngle, double[]? defaultPos = null)
        {
            //------------- Initialize Everything ------------------------
            Name = name;

            UpdateConfig(radius, jointAngle, defaultPos); //will cause local/global coords to calculate

            //------------ Setup servo controllers -------------------------
            InitializeServoControllers();
        }
        
        /// <summary>
        /// Translates the platform to the given absolute position
        /// </summary>
        /// <param name="position">X,Y,Z position [mm]</param>
        public void TranslateAbs(double[] position)
        {
            TranslationTarget = position;

            if (TranslationMode == TranslationModes.Instant)
                Translation = TranslationTarget;
        }

        /// <summary>
        /// Translates the platform to the given relative position
        /// </summary>
        /// <param name="position">X,Y,Z position [mm]</param>
        public void TranslateRel(double[] position)
        {
            double[] newPos = [0, 0, 0];

            for (int i = 0; i < Translation.Length; i++)
            {
                newPos[i] = Translation[i] + position[i];
            }

            TranslationTarget = newPos;

            if (TranslationMode == TranslationModes.Instant)
                Translation = TranslationTarget;
        }

        /// <summary>
        /// Translates the platform to the given absolute angle
        /// </summary>
        /// <param name="rotation">Pitch, Roll, Yaw rotation [deg]</param>
        public void RotateAbs(double[] rotation)
        {
            RotationTarget = rotation;

            if (TranslationMode == TranslationModes.Instant)
                Rotation = RotationTarget;
        }

        /// <summary>
        /// Updates the platform local configuration, allowing the whole config to be updated without many re-calculations
        /// </summary>
        /// <param name="radius">Radius of the platform joints [mm]</param>
        /// <param name="jointAngle">The angle of base nodes from an even 120 [deg]</param>
        /// <param name="defaultPos">Default center position of the platform with no translation</param>
        public void UpdateConfig(double radius, double jointAngle, double[]? defaultPos)
        {
            _radius = radius;
            _jointAngle = jointAngle;

            if (defaultPos != null)
            {
                _defaultPos = defaultPos;
            }

            CalcLocalCoords();
        }

        /// <summary>
        /// Allows whole rotation center to be updated without many re-calculations
        /// </summary>
        /// <param name="position">Position of the rotation center (x,y,z) [mm]</param>
        /// <param name="fixedPosition">Whether the rotation center translates with the platform</param>
        public void UpdateRotationCenter(double[] position, bool fixedPosition)
        {
            _fixedRotationCenter = fixedPosition;
            RotationCenter = position; //will also re-calculate coords
        }

        /// <summary>
        /// Hard resets the translation and rotation
        /// </summary>
        public void ResetPosition()
        {
            TranslationTarget = [0, 0, 0];
            RotationTarget = [0, 0, 0];

            Translation = [0, 0, 0];
            Rotation = [0, 0, 0];

            // Reset the servo controllers in case the integral is wound up.
            InitializeServoControllers();
        }

        //---------------------------- Servo Translation Stuff ---------------------------------
        private List<PIDController> _positionControllers = [];
        private List<PIDController> _rotationControllers = [];

        /// <summary>
        /// Calculates a timestep for real time simulations
        /// </summary>
        /// <param name="timeStep">The simulation timestep</param>
        public void CalculateTimeStep(double timeStep)
        {
            //--------------------- Position ------------------------------
            double[] translation = [0, 0, 0];

            for (int i = 0; i < TranslationTarget.Length; i++)
            {
                translation[i] = _positionControllers[i].CalculateOutput(TranslationTarget[i] - Translation[i], timeStep);
            }
            _translation = translation;

            //--------------------- Rotation ------------------------------
            double[] rotation = [0, 0, 0];

            for (int i = 0; i < RotationTarget.Length; i++)
            {
                rotation[i] = _rotationControllers[i].CalculateOutput(RotationTarget[i] - Rotation[i], timeStep);
            }
            _rotation = rotation;

            //----------- Cleanup ------------------------------------------

            CalcGlobalCoords();
        }
        private void InitializeServoControllers()
        {
            _positionControllers = [];
            _rotationControllers = [];

            // Need controllers for X,Y,Z and P,R,Y
            for (int i = 0; i < 3; i++)
            {
                _positionControllers.Add(new PIDController(-1, 0.5, 3, 0));
                _rotationControllers.Add(new PIDController(-1, 0.5, 3, 0));
            }
        }

        //---------------- Geometry Calculation functions -----------------
        private void CalcLocalCoords()
        {
            LocalJointCoords = CalcLocalCoords(JointAngle, Radius);
            
            //notify parents of the change
            LocalCoordsChanged?.Invoke(this, new EventArgs());
    
            CalcGlobalCoords(); //will also have changed if local coords have changed
        } // Also forces global coords to be updated

        /// <summary>
        /// Calculates XYZ global coordinates for a locally specified point anywhere on the platform
        /// </summary>
        /// <param name="localcoord">Local [X,Y,Z] coordinates, relative to the platform center</param>
        /// <returns>Global coordinates, including translation and rotation</returns>
        public double[] CalcGlobalCoord(double[] localcoord)
        {
            return KinematicMath.CalcGlobalCoord2(localcoord, Translation, DefaultPos, Rotation, RotationCenter);
        }
        private void CalcGlobalCoords()
        {
            if (LocalJointCoords != null && Translation != null && Rotation != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    GlobalJointCoords[i] = CalcGlobalCoord(LocalJointCoords[i]);
                }
                CalcNormalVector();

                PositionChanged?.Invoke(this, new EventArgs());
                RedrawRequired?.Invoke(this, new EventArgs());
            }
        }
        private void CalcNormalVector()
        {
            double[] normal = [0, 0, 1]; //local vector without rotation
            NormalVector = KinematicMath.RotateVector(normal, Rotation);
        }

        /// <summary>
        /// Calculate the global angle for each joint node
        /// </summary>
        /// <param name="index">The joint node index (0 to 5)</param>
        /// <param name="offsetAngle">The angle offset from nominal 120 degrees</param>
        /// <returns>The global angle of the node from 0 to 360 degrees</returns>
        public static double CalcJointOffsetAngle(int index, double offsetAngle)
        {
            double angle = 0;

            if (index % 2 == 0)
            { //index is even
                angle -= offsetAngle;
            }
            else //index is odd
            {
                angle += offsetAngle;
            }

            angle += 120 * ((index - index % 2) / 2); // Each time index jumps by 2, need to add 120 degree spacing

            return angle;
        }

        /// <summary>
        /// Calculate local coordinates of each joint [X,Y,Z]
        /// </summary>
        /// <param name="offsetAngle">The angle offset from nominal 120 degrees</param>
        /// <param name="radius">The platform radius</param>
        /// <returns>X,Y,Z coordinates of each joint, sorted by joint index</returns>
        public static double[][] CalcLocalCoords(double offsetAngle, double radius)
        {
            var output = new double[6][];

            for (int i = 0; i < 6; i++)
                output[i] = CalcLocalCoord(i, offsetAngle, radius);

            return output;
        }

        /// <summary>
        /// Calculate local coordinates of an individual joint [X,Y,Z]
        /// </summary>
        /// <param name="index">The joint node index (0 to 5)</param>
        /// <param name="offsetAngle">The angle offset from nominal 120 degrees</param>
        /// <param name="radius">The platform radius</param>
        /// <returns>X,Y,Z coordinates of the joint</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private static double[] CalcLocalCoord(int index, double offsetAngle, double radius)
        {
            if (index < 0 || index > 5)
                throw new IndexOutOfRangeException("Index out of range when grabbing local hexapod coords.");

            double angle = CalcJointOffsetAngle(index, offsetAngle);
            double X = Math.Cos(angle * Math.PI / 180.0) * radius;
            double Y = Math.Sin(angle * Math.PI / 180.0) * radius;

            double[] output =  [X, Y, 0];
            return output;
        }
    }
}
