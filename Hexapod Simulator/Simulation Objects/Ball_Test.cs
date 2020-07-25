using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Drawing;
using GFunctions.Mathematics;
using GFunctions.OpenTK;


namespace Hexapod_Simulator.SimObject
{
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
            get { return this._position; }
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
}
