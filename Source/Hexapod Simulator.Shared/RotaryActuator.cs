using GFunctions.Mathematics;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// An actuator to lift the hexapod consisting of an arm fixed to a rotary joint
    /// </summary>
    public class RotaryActuator : LinearActuator, IActuator
    {
        private readonly double _armLength = 1; //arm length [mm], base class constructor will fail if default = 0
        private readonly double _armAngle = 0; //arm angle with respect to vertical [deg]

        /// <summary>
        /// The actuator type
        /// </summary>
        public override ActuatorTypes ActuatorType => ActuatorTypes.Rotary;

        public RotaryActuator(double armRadius, double armAngle, double linkLength, Vector3 position, Vector3 linkEndPosition) : base(180, linkLength, position, linkEndPosition)
        {
            _armLength = armRadius;
            _armAngle = armAngle;

            _maxTravel = 180; //done this way so everything doesn't calculate
            _minTravel = -180; //done this way so everything doesn't calculate

            Solver = new IterativeSolver(ComputeError)
            {
                InitialStepSize = 0.01,
                MaxSteps = 100,
                SuccessErrorThreshold = 0.001,
            };

            CalculateMotorAngle();
        }

        /// <summary>
        /// Calculates the end coordinates of the arm - needs to be different for rotary
        /// </summary>
        protected override void CalculateArmEndCoords()
        {
            Vector3 localCoord = new(_armLength, 0, 0);
            Vector3 translation = _position;
            RotationPRY rotation = new(TravelPosition, 0, _armAngle);

            ArmEndPosition = KinematicMath.CalcGlobalCoord(localCoord, translation, new(0, 0, 0), rotation);
        }
        
        /// <summary>
        /// Calculates the offset angle of the rotary actuator arm
        /// </summary>
        /// <param name="index">The hexapod joint index [0-5]</param>
        /// <param name="armAngle">The offset angle of the arm [deg]</param>
        /// <param name="jointOffsetAngle">The offset angle of the hexapod joint</param>
        /// <returns>The absolute angle of the arm [-180 to 180]</returns>
        public static double CalcMotorOffsetAngle(int index, double armAngle, double jointOffsetAngle)
        {
            double angle = 0;
            angle += jointOffsetAngle;

            if (index % 2 == 0)
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
}
