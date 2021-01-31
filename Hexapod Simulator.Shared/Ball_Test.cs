using System;
using GFunctions.Mathematics;
using MathNet.Numerics.LinearAlgebra.Double;


namespace Hexapod_Simulator.Shared
{
    public class Ball_Test : Ball, IBall
    {
        public event EventHandler RedrawRequired;

        private double _normalForce = 0; //last calculated normal force magnitude

        public Ball_Test(double radius, double density, double[] startingPos)
        {
            this.Radius = radius;
            this.Density = density;
            this.Position = startingPos;

            this._normalForce = this.Mass * 9.81; //give an initial estimate assuming ball is on flat surface

            RedrawRequired?.Invoke(this, new EventArgs());
        }

        public void CalculateTimeStep(double TimeIncrement, double[] normalForceVector)
        {
            CalcKineticSolution(new DenseVector(normalForceVector)); //calculates acceleration

            for (int i = 0; i < 2; i++)
            {
                Velocity[i] = Velocity[i] + Calculus.Integrate(Acceleration[i], TimeIncrement);
                Position[i] = Position[i] + Calculus.Integrate(Velocity[i], TimeIncrement);
            }

            RedrawRequired?.Invoke(this, new EventArgs());
        }
        public void UpdateGlobalCoords(double[] globalCoords)
        {
            return;
        } //do nothing for this ball

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

                    this.Acceleration = CalcAccelVector(gravityForceVector, normalForceVector, this._normalForce, mass);
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
}
