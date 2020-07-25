using GFunctions.Mathematics;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.SimObject
{
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
}
