using GFunctions.Mathematics;
using GFunctions.Mathnet;
using System;

namespace Hexapod_Simulator.Shared
{
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

        public event EventHandler RedrawRequired; //not called by anything currently, but could be

        public LinearActuator(double maxTravel, double linkLength, double[] position, double[] linkEndPosition)
        {
            this._maxTravel = maxTravel;
            this._position = position;
            this._linkEndPosition = linkEndPosition;
            this._linkLength = linkLength;

            Solver = new IterativeSolver(0.01, 100, 0.01, computeError, this._maxTravel, this._minTravel);

            calcMotorAngle();
        }

        protected void calcMotorAngle()
        {
            this._travelPosition = Solver.Solve(this._travelPosition);

            if (Solver.SolutionValid == false)
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
}
