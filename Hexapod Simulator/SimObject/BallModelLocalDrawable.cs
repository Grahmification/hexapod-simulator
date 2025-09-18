using Hexapod_Simulator.Shared;
using GFunctions.OpenTK;

namespace Hexapod_Simulator.SimObject
{
    public class BallModelLocalDrawable : BallModelLocal, IGLDrawable, IBall
    {
        public bool IsDrawn { get; set; }

        public BallModelLocalDrawable(double radius, double density, double[] startingPos) : base(radius, density, startingPos)
        {
            this.IsDrawn = true;
        } //only calculates local coords

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, Position, 5);
        }
    }
}
