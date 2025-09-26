using GFunctions.Mathematics;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// Test of a ball that will dynamically update in a 3D simulation with kinematics
    /// </summary>
    public class BallModel : Ball, IBall
    {
        /// <summary>
        /// The last calculated normal force magnitude
        /// </summary>
        private double _normalForce = 0;

        /// <summary>
        /// Raised if a redraw of the object is needed in the view
        /// </summary>
        public event EventHandler? RedrawRequired;

        public BallModel(double radius, double density, Vector3 startingPos)
        {
            Radius = radius;
            Density = density;
            Position = startingPos;

            _normalForce = Mass * 9.81; // Give an initial estimate assuming ball is on flat surface

            RedrawRequired?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Calculates the ball's position and velocity given the angle of the surface it's on
        /// </summary>
        /// <param name="timeIncrement">The simulation time step</param>
        /// <param name="normalForceVector">Normal vector of the surface the ball is on</param>
        public void CalculateTimeStep(double timeIncrement, Vector3 normalForceVector)
        {
            CalcKineticSolution(normalForceVector); //calculates acceleration

            // Integration function definition
            double integration(double v) => Calculus.Integrate(v, timeIncrement);

            // Integrate acceleration and velocity
            Velocity += Acceleration.Operate(integration);
            Position += Velocity.Operate(integration);

            RedrawRequired?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Updates the ball's global coordinates
        /// </summary>
        /// <param name="globalCoords">The global position coordinates [X,Y,Z]</param>
        public void UpdateGlobalCoords(Vector3 globalCoords)
        {
            return;
        } // Does nothing for this ball

        private void CalcKineticSolution(Vector3 normalForceVector)
        {
            double mass = Mass;
            Vector3 gravityForceVector = new(0, 0, -9.81 * mass * 100);

            double stepSize = 0.01; //initial change to test
            double maxSteps = 100;
            double errorTolerance = 0.01;

            double startingValue = _normalForce; //starting guess
            double prevError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, startingValue, mass), normalForceVector);
            double newError = 0;
            double ratio = 0;

            for (int i = 0; i < maxSteps; i++)
            {
                // Valid solution
                if (Math.Abs(prevError) <= errorTolerance)
                {
                    Acceleration = CalcAccelVector(gravityForceVector, normalForceVector, _normalForce, mass);
                    return;
                }

                _normalForce += stepSize; // Change by the stepsize

                newError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, _normalForce, mass), normalForceVector);

                ratio = (prevError - newError) / stepSize; // Ratio between change in k and error
                stepSize = newError / (ratio * 2.0);
                prevError = newError;
            }

            _normalForce = startingValue; // Solution has failed. Reset to starting. 

        } //iterative solution
        private static Vector3 CalcAccelVector(Vector3 gravityForceVector, Vector3 normalForceVector, double normalForce, double mass)
        {
            // accel = (Gravity Force + NormalForce) / mass
            return (gravityForceVector + (normalForceVector * normalForce)) / mass;
        }
        
        private static double CalcAccelVectorError(Vector3 accelVector, Vector3 normalForceVector)
        {
            // Vectors should be perpendicular (dot product = 0), error is any value produced
            return accelVector.Dot(normalForceVector);
        }
    }
}
