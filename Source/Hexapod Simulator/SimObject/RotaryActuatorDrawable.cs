using GFunctions.Mathnet;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class RotaryActuatorDrawable : RotaryActuator, IActuator, IGLDrawable
    {
        public bool IsDrawn { get; set; }
        public RotaryActuatorDrawable(double armRadius, double armAngle, double linkLength, Vector3 position, Vector3 linkEndPosition) : base(armRadius, armAngle, linkLength, position, linkEndPosition)
        {
            IsDrawn = true;
        }
        public void Draw()
        {
            Color clr = Color.Yellow;

            if (!SolutionValid)
            {
                clr = Color.Red;
            }

            GLObjects.Line(clr, Position.ToArray(), ArmEndPosition.ToArray());
            GLObjects.Line(clr, LinkEndPosition.ToArray(), ArmEndPosition.ToArray());
        }
    }
}
