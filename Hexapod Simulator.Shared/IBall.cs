using System;
using System.Collections.Generic;
using System.Text;

namespace Hexapod_Simulator.Shared
{
    public interface IBall
    {
        double[] Angle { get; } //PRY rotation about centerpoint [deg]
        double[] Position { get; } //XYZ position of centerpoint [m]
        double[] Velocity { get; } //XYZ velocity of centerpoint [m/s]
        double[] Acceleration { get; }//XYZ accel of centerpoint [m/s/s]
        double Radius { get; set; } //radius [m]
        double Density { get; set; } //density [kg/m^3]

        double Volume { get; }
        double Mass { get; }

        void CalculateTimeStep(double TimeIncrement, double[] normalForceVector);
        void UpdateGlobalCoords(double[] globalCoords);
    }
}
