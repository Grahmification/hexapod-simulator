using GFunctions.Mathematics;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// An actuator to lift the hexapod consisting of a linearly sliding rod
    /// </summary>
    public class LinearActuator : IActuator
    {
        protected double _maxTravel = 1; //maximum travel [mm]
        protected double _minTravel = 0; //minimum travel [mm]
        private double _linkLength = 0; //link length between actuator tip and top [mm]

        protected Vector3 _position = new(0, 0, 0); //XYZ position actuator tip start point [mm]
        private Vector3 _linkEndPosition = new(0, 0, 0); //XYZ position of link attached to actuator [mm] 

        /// <summary>
        /// Used for solving kinematics iteratively to get actuator position
        /// </summary>
        protected IterativeSolver Solver;

        /// <summary>
        /// The max allowable actuator position
        /// </summary>
        public double MaxTravel
        {
            get { return _maxTravel; }
            set{ _maxTravel = value; CalculateMotorAngle(); }
        }

        /// <summary>
        /// The min allowable actuator position
        /// </summary>
        public double MinTravel
        {
            get { return _minTravel; }
            set { _minTravel = value; CalculateMotorAngle(); }
        }

        /// <summary>
        /// The length of the link joining the actuator tip to the hexapod top
        /// </summary>
        public double LinkLength
        {
            get { return _linkLength; }
            set { _linkLength = value; CalculateMotorAngle(); }
        }

        /// <summary>
        /// Where the base of the actuator is attached to the hexapod base [x,y,z]
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; CalculateMotorAngle(); }
        }

        /// <summary>
        /// where the link attaches to the hexapod top [x,y,z]
        /// </summary>
        public Vector3 LinkEndPosition
        {
            get { return _linkEndPosition; }
            set
            {
                _linkEndPosition = value;
                CalculateMotorAngle();
            }
        }

        /// <summary>
        /// Actuator arm end position, where it attaches to link start [x,y,z]
        /// </summary>
        public Vector3 ArmEndPosition { get; protected set; } = new();

        /// <summary>
        /// Current position of the actuator [mm] 
        /// </summary>
        public double TravelPosition { get; private set; } = 0;

        /// <summary>
        /// Whether the actuator can find a valid position within travel
        /// </summary>
        public bool SolutionValid => Solver.SolutionValid;

        /// <summary>
        /// The actuator type
        /// </summary>
        public virtual ActuatorTypes ActuatorType => ActuatorTypes.Linear;

        /// <summary>
        /// Fires if the object needs to be updated in the view
        /// </summary>

        /// <summary>
        /// Fires if the object needs to be updated in the view
        /// </summary>
        public event EventHandler? RedrawRequired; // Not called by anything currently, but could be

        public LinearActuator(double maxTravel, double linkLength, Vector3 position, Vector3 linkEndPosition)
        {
            _maxTravel = maxTravel;
            _position = position;
            _linkEndPosition = linkEndPosition;
            _linkLength = linkLength;

            Solver = new IterativeSolver(ComputeError)
            {
                InitialStepSize = 0.01,
                MaxSteps = 100,
                SuccessErrorThreshold = 0.01
            };

            CalculateMotorAngle();
        }

        protected void CalculateMotorAngle()
        {
            Solver.MaxSolutionValue = _maxTravel;
            Solver.MinSolutionValue = _minTravel;
            TravelPosition = Solver.Solve(TravelPosition);

            if (!Solver.SolutionValid)
                CalculateArmEndCoords(); // Need to re-calculate to get geometry back to starting position

            RedrawRequired?.Invoke(this, new EventArgs());
        } //iterative solution
        protected double ComputeError(double iterationValue)
        {
            TravelPosition = iterationValue;
            CalculateArmEndCoords();
            return (KinematicMath.VectorLength(ArmEndPosition.ToArray(), LinkEndPosition.ToArray()) - LinkLength);
        }
        protected virtual void CalculateArmEndCoords()
        {
            Vector3 localCoord = new(0, 0, TravelPosition); // Travel is in the Z direction
            ArmEndPosition = localCoord + _position;
        }
    }
}
