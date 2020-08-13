using System;
using System.Drawing;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class Ball_TestDrawable : Ball_Test, IGLDrawable, IBall
    {
        public bool IsDrawn { get; set; }

        public event EventHandler RedrawRequired;

        public Ball_TestDrawable(double radius, double density, double[] startingPos) : base(radius, density, startingPos)
        {
            this.IsDrawn = true;
        }

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, Position, 5);
        }
    }
}
