using GFunctions.Mathnet;

namespace Hexapod_Simulator.Shared
{
    public interface IActuator
    {
        /// <summary>
        /// The max allowable actuator position
        /// </summary>
        double MaxTravel { get; set; }

        /// <summary>
        /// The min allowable actuator position
        /// </summary>
        double MinTravel { get; set; }

        /// <summary>
        /// Current position of the actuator [mm] 
        /// </summary>
        double TravelPosition { get; }

        /// <summary>
        /// Where the base of the actuator is attached to the hexapod base [x,y,z]
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// Actuator arm end position, where it attaches to link start [x,y,z]
        /// </summary>
        Vector3 ArmEndPosition { get; }

        /// <summary>
        /// where the link attaches to the hexapod top [x,y,z]
        /// </summary>
        Vector3 LinkEndPosition { get; set; }

        /// <summary>
        /// The length of the link joining the actuator tip to the hexapod top
        /// </summary>
        double LinkLength { get; set; }

        /// <summary>
        /// Whether the actuator can find a valid position within travel
        /// </summary>
        bool SolutionValid { get; }

        /// <summary>
        /// The actuator type
        /// </summary>
        ActuatorTypes ActuatorType { get; }

        /// <summary>
        /// Fires if the object needs to be updated in the view
        /// </summary>
        event EventHandler? RedrawRequired;
    }
    
    /// <summary>
    /// Different types of actuators
    /// </summary>
    public enum ActuatorTypes
    {
        Linear,
        Rotary
    }
}