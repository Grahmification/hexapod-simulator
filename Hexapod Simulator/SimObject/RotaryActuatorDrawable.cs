using System;
using System.Drawing;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class RotaryActuatorDrawable : RotaryActuator, IActuator, IGLDrawable
    {
        public bool IsDrawn { get; set; }
        public RotaryActuatorDrawable(double armRadius, double armAngle, double linkLength, double[] position, double[] linkEndPosition) : base(armRadius, armAngle, linkLength, position, linkEndPosition)
        {
            this.IsDrawn = true;
        }
        public void Draw()
        {
            Color clr = Color.Yellow;

            if (this.SolutionValid == false)
            {
                clr = Color.Red;
            }

            GLObjects.Line(clr, this.Position, this.ArmEndPosition);
            GLObjects.Line(clr, this.LinkEndPosition, this.ArmEndPosition);
        }
    }
}
