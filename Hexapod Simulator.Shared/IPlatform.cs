using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// Generic definition for the top or bottom platform of a hexapod
    /// </summary>
    public interface IPlatform
    {
        /// <summary>
        /// XYZ position [mm] of each joint, without translation/rotation 
        /// </summary>
        Vector3[] LocalJointCoords { get; }

        /// <summary>
        /// XYZ position [mm] of each joint
        /// </summary>
        Vector3[] GlobalJointCoords { get; }

        /// <summary>
        /// Determines whether the platform will translate instantly, or gradually during a simulation
        /// </summary>
        TranslationModes TranslationMode { get; set; }

        /// <summary>
        /// The radius of the platform joints [mm]
        /// </summary>
        double Radius { get; set; }

        /// <summary>
        /// The angle of base nodes from an even 120 [deg]
        /// </summary>
        double JointAngle { get; set; }

        /// <summary>
        /// Default center position of the platform with no translation
        /// </summary>
        Vector3 DefaultPos { get; }

        /// <summary>
        /// The platform target translation (X, Y, Z) [mm]
        /// </summary>
        Vector3 TranslationTarget { get; }

        /// <summary>
        /// The platform target rotation (Pitch, Roll, Yaw) [deg]
        /// </summary>
        RotationPRY RotationTarget { get; }

        /// <summary>
        /// The actual platform translation (may be different than target if mode isn't instant)
        /// </summary>
        Vector3 Translation { get; }

        /// <summary>
        /// The actual platform rotation (Pitch, Roll, Yaw) [deg], may be different than target if mode isn't instant
        /// </summary>
        RotationPRY Rotation { get; }

        /// <summary>
        /// Rotation including translation if <see cref="FixedRotationCenter"/> is false
        /// </summary>
        Vector3 RotationCenter { get; }

        /// <summary>
        /// Absolute center position of the platform (default offset + translation), (X, Y, Z) [mm]
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// The absolute rotation center position (default offset + rotation center), (X, Y, Z) [mm]
        /// </summary>
        Vector3 AbsRotationCenter { get; }

        /// <summary>
        /// The unit vector of platform normal direction (x,y,z)
        /// </summary>
        Vector3 NormalVector { get; }

        /// <summary>
        /// Whether the rotation point is fixed in space, or moves when the platform translates
        /// </summary>
        bool FixedRotationCenter { get; }

        string Name { get; set; }


        /// <summary>
        /// A redraw of the platform is needed in the view
        /// </summary>
        event EventHandler? RedrawRequired;

        /// <summary>
        /// Rotation or translation of the platform has changed
        /// </summary>
        event EventHandler? PositionChanged;

        /// <summary>
        /// Translates the platform to the given absolute position
        /// </summary>
        /// <param name="position">X,Y,Z position [mm]</param>
        void TranslateAbs(Vector3 position);

        /// <summary>
        /// Translates the platform to the given relative position
        /// </summary>
        /// <param name="position">X,Y,Z position [mm]</param>
        void TranslateRel(Vector3 position);

        /// <summary>
        /// Translates the platform to the given absolute angle
        /// </summary>
        /// <param name="rotation">Pitch, Roll, Yaw rotation [deg]</param>
        void RotateAbs(RotationPRY rotation);

        /// <summary>
        /// Updates the platform local configuration, allowing the whole config to be updated without many re-calculations
        /// </summary>
        /// <param name="radius">Radius of the platform joints [mm]</param>
        /// <param name="jointAngle">The angle of base nodes from an even 120 [deg]</param>
        /// <param name="defaultPos">Default center position of the platform with no translation</param>
        void UpdateConfig(double radius, double jointAngle, Vector3 defaultPos);

        /// <summary>
        /// Allows whole rotation center to be updated without many re-calculations
        /// </summary>
        /// <param name="position">Position of the rotation center (x,y,z) [mm]</param>
        /// <param name="fixedPosition">Whether the rotation center translates with the platform</param>
        void UpdateRotationCenter(Vector3 position, bool fixedPosition);

        /// <summary>
        /// Hard resets the translation and rotation
        /// </summary>
        void ResetPosition();

        //---------------------------- Servo Translation Stuff ---------------------------------

        /// <summary>
        /// Calculates a timestep for real time simulations
        /// </summary>
        /// <param name="timeStep">The simulation timestep</param>
        void CalculateTimeStep(double TimeStep);

        //---------------- Geometry Calculation functions -----------------    

        /// <summary>
        /// Calculates XYZ global coordinates for a locally specified point anywhere on the platform
        /// </summary>
        /// <param name="localcoord">Local [X,Y,Z] coordinates, relative to the platform center</param>
        /// <returns>Global coordinates, including translation and rotation</returns>
        Vector3 CalcGlobalCoord(Vector3 localcoord);
    }
}
