using GFunctions.Mathnet;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class LinearActuatorDrawable : LinearActuator, IActuator, IGLDrawable
    {
        public bool IsDrawn { get; set; }

        public LinearActuatorDrawable(double maxTravel, double linkLength, Vector3 position, Vector3 linkEndPosition) :base (maxTravel, linkLength, position, linkEndPosition)
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
