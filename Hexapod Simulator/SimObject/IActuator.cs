namespace Hexapod_Simulator.SimObject
{
    public interface IActuator
    {
        double MaxTravel { get; set; } //min allowable actuator position
        double MinTravel { get; set; }
        double TravelPosition { get; }

        double[] Position { get; set; } //where the base of the actuator is located on the base
        double[] ArmEndPosition { get; } //actuator arm end position, where it attaches to link start
        double[] LinkEndPosition { get; set; } //where the link attaches to the top

        double LinkLength { get; set; }

        bool SolutionValid { get; }
        ActuatorTypes ActuatorType { get; }

        void Draw();
    }
    public enum ActuatorTypes
    {
        Linear,
        Rotary
    }
}
