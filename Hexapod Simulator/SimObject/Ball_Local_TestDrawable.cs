using Hexapod_Simulator.Shared;
using GFunctions.OpenTK;

namespace Hexapod_Simulator.SimObject
{
    public class Ball_Local_TestDrawable : Ball_Local_Test, IGLDrawable, IBall
    {
        public bool IsDrawn { get; set; }

        public Ball_Local_TestDrawable(double radius, double density, double[] startingPos) : base(radius, density, startingPos)
        {
            this.IsDrawn = true;
        } //only calculates local coords

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, Position, 5);
        }
    }
}
