using GFunctions.Mathematics;
using MathNet.Numerics.LinearAlgebra.Double;


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

        public BallModel(double radius, double density, double[] startingPos)
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
        public void CalculateTimeStep(double timeIncrement, double[] normalForceVector)
        {
            CalcKineticSolution(new DenseVector(normalForceVector)); //calculates acceleration

            for (int i = 0; i < 2; i++)
            {
                Velocity[i] = Velocity[i] + Calculus.Integrate(Acceleration[i], timeIncrement);
                Position[i] = Position[i] + Calculus.Integrate(Velocity[i], timeIncrement);
            }

            RedrawRequired?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Updates the ball's global coordinates
        /// </summary>
        /// <param name="globalCoords">The global position coordinates [X,Y,Z]</param>
        public void UpdateGlobalCoords(double[] globalCoords)
        {
            return;
        } // Does nothing for this ball

        private void CalcKineticSolution(DenseVector normalForceVector)
        {
            double mass = Mass;
            DenseVector gravityForceVector = new([0, 0, -9.81 * mass * 100]);

            double stepSize = 0.01; //initial change to test
            double maxSteps = 100;
            double errorTolerance = 0.01;

            double startingValue = _normalForce; //starting guess
            double prevError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, startingValue, mass), [.. normalForceVector]);
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

                newError = CalcAccelVectorError(CalcAccelVector(gravityForceVector, normalForceVector, _normalForce, mass), [.. normalForceVector]);

                ratio = (prevError - newError) / stepSize; // Ratio between change in k and error
                stepSize = newError / (ratio * 2.0);
                prevError = newError;
            }

            _normalForce = startingValue; // Solution has failed. Reset to starting. 

        } //iterative solution
        private static double[] CalcAccelVector(DenseVector gravityForceVector, DenseVector normalForceVector, double normalForce, double mass)
        {
            // accel = (Gravity Force + NormalForce) / mass

            DenseVector output = (gravityForceVector + (normalForceVector * normalForce)) / mass;
            return [.. output];
        }
        private static double CalcAccelVectorError(double[] accelVector, double[] normalForceVector)
        {
            // Vectors should be perpendicular (dot product = 0), error is any value produced

            double output = 0;

            for (int i = 0; i < accelVector.Length; i++)
            {
                output += accelVector[i] * normalForceVector[i];
            }

            return output;
        }
    }
}
