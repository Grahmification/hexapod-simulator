using Hexapod_Simulator.Shared;
using GFunctions.OpenTK;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.SimObject
{
    public class BallModelLocalDrawable : BallModelLocal, IGLDrawable, IBall
    {
        public bool IsDrawn { get; set; }

        public BallModelLocalDrawable(double radius, double density, Vector3 startingPos) : base(radius, density, startingPos)
        {
            this.IsDrawn = true;
        } //only calculates local coords

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, Position.ToArray(), 5);
        }
    }
}
