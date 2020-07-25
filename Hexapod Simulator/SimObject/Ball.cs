using System;
using GFunctions.OpenTK;

namespace Hexapod_Simulator.SimObject
{
    public class Ball
    {
        public double[] Angle { get; protected set; } = new double[] { 0, 0, 0 }; //PRY rotation about centerpoint [deg]
        public double[] Position { get; protected set; } = new double[] { 0, 0, 0 }; //XYZ position of centerpoint [m]
        public double[] Velocity { get; protected set; } = new double[] { 0, 0, 0 }; //XYZ velocity of centerpoint [m/s]
        public double[] Acceleration { get; protected set; } = new double[] { 0, 0, 0 }; //XYZ accel of centerpoint [m/s/s]

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


}
