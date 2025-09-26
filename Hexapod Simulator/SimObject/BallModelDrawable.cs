using GFunctions.Mathnet;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class BallModelDrawable : BallModel, IGLDrawable, IBall
    {
        public bool IsDrawn { get; set; }

        public BallModelDrawable(double radius, double density, Vector3 startingPos) : base(radius, density, startingPos)
        {
            this.IsDrawn = true;
        }

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, Position.ToArray(), 5);
        }
    }
}
