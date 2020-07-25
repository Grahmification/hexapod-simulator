using System;

namespace Hexapod_Simulator.SimObject
{
    public class Ball
    {
        private double[] _position = new double[] { 0, 0, 0 }; //XYZ position of centerpoint [m]
        private double[] _angle = new double[] { 0, 0, 0 }; //PRY rotation about centerpoint [deg]

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
