using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class PlatformDrawable : Platform, IPlatform, IGLDrawable
    {
        public bool IsDrawn { get; set; }

        public PlatformDrawable(string name, double radius, double jointAngle, double[] defaultPos = null) : base(name, radius, jointAngle, defaultPos)
        {
            this.IsDrawn = true;
        }

        public void Draw()
        {
            if (GlobalJointCoords != null)
            {
                if (GlobalJointCoords.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        GLObjects.Cube(System.Drawing.Color.Red, this.GlobalJointCoords[i], 1);

                        if (i != 5)
                        {
                            GLObjects.Line(Color.Green, this.GlobalJointCoords[i], this.GlobalJointCoords[i + 1]);
                        }
                        else
                        {
                            GLObjects.Line(Color.Green, this.GlobalJointCoords[i], this.GlobalJointCoords[0]);
                        }

                    }

                    GLObjects.Cube(System.Drawing.Color.Yellow, this.AbsRotationCenter, 1);
                }
            }
        }
    }
}
