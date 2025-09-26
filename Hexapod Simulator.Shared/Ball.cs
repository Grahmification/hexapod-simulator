using GFunctions.Mathnet;

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
        public RotationPRY Angle { get; protected set; } = new();

        /// <summary>
        /// XYZ position of centerpoint [m]
        /// </summary>
        public Vector3 Position { get; protected set; } = new();

        /// <summary>
        /// XYZ velocity of centerpoint [m/s]
        /// </summary>
        public Vector3 Velocity { get; protected set; } = new();

        /// <summary>
        /// XYZ accel of centerpoint [m/s/s]
        /// </summary>
        public Vector3 Acceleration { get; protected set; } = new();

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
