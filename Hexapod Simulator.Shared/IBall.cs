namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// Generic definition of a ball that can be used in the 3D space simulation
    /// </summary>
    public interface IBall
    {
        /// <summary>
        /// Raised if a redraw of the object is needed in the view
        /// </summary>
        event EventHandler? RedrawRequired;

        /// <summary>
        /// PRY rotation about centerpoint [deg]
        /// </summary>
        double[] Angle { get; }

        /// <summary>
        /// XYZ position of centerpoint [m]
        /// </summary>
        double[] Position { get; }

        /// <summary>
        /// XYZ velocity of centerpoint [m/s]
        /// </summary>
        double[] Velocity { get; }

        /// <summary>
        /// XYZ accel of centerpoint [m/s/s]
        /// </summary>
        double[] Acceleration { get; }

        /// <summary>
        /// Ball radius [m]
        /// </summary>
        double Radius { get; set; }

        /// <summary>
        /// Ball density [kg/m^3]
        /// </summary>
        double Density { get; set; }

        /// <summary>
        /// Ball volume [m^3]
        /// </summary>
        double Volume { get; }

        /// <summary>
        /// Ball mass [kg]
        /// </summary>
        double Mass { get; }

        /// <summary>
        /// Calculates the ball's position and velocity given the angle of the surface it's on
        /// </summary>
        /// <param name="timeIncrement">The simulation time step</param>
        /// <param name="normalForceVector">Normal vector of the surface the ball is on</param>
        void CalculateTimeStep(double timeIncrement, double[] normalForceVector);

        /// <summary>
        /// Updates the ball's global coordinates
        /// </summary>
        /// <param name="globalCoords">The global position coordinates [X,Y,Z]</param>
        void UpdateGlobalCoords(double[] globalCoords);
    }
}
