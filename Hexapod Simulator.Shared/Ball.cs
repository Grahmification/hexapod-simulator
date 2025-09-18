namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// A ball that can be used in the 3D space simulation
    /// </summary>
    public class Ball
    {
        /// <summary>
        /// PRY rotation about centerpoint [deg]
        /// </summary>
        public double[] Angle { get; protected set; } = [0, 0, 0];

        /// <summary>
        /// XYZ position of centerpoint [m]
        /// </summary>
        public double[] Position { get; protected set; } = [0, 0, 0];

        /// <summary>
        /// XYZ velocity of centerpoint [m/s]
        /// </summary>
        public double[] Velocity { get; protected set; } = [0, 0, 0];

        /// <summary>
        /// XYZ accel of centerpoint [m/s/s]
        /// </summary>
        public double[] Acceleration { get; protected set; } = [0, 0, 0];

        /// <summary>
        /// Ball radius [m]
        /// </summary>
        public double Radius { get; set; } = 1;

        /// <summary>
        /// Ball density [kg/m^3]
        /// </summary>
        public double Density { get; set; } = 1;

        /// <summary>
        /// Ball volume [m^3]
        /// </summary>
        public double Volume => (4 / 3.0) * Math.PI * Math.Pow(Radius, 3);

        /// <summary>
        /// Ball mass [kg]
        /// </summary>
        public double Mass => Density * Volume;
    }
}
