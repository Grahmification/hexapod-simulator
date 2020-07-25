using System;
using System.Drawing;
using GFunctions.OpenTK;

namespace Hexapod_Simulator.SimObject
{
    public class BallDrawable : Ball, IGLDrawable
    {
        public bool IsDrawn { get; set; }
        public event EventHandler RedrawRequired;

        public void Draw()
        {
            GLObjects.Cube(Color.LightGreen, Position, 5);
        }
    }
}
