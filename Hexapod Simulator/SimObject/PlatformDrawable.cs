using GFunctions.Mathnet;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class PlatformDrawable : Platform, IPlatform, IGLDrawable
    {
        public bool IsDrawn { get; set; }

        public PlatformDrawable(string name, double radius, double jointAngle, Vector3? defaultPos = null) : base(name, radius, jointAngle, defaultPos)
        {
            IsDrawn = true;
        }

        public void Draw()
        {
            if (GlobalJointCoords != null)
            {
                if (GlobalJointCoords.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        GLObjects.Cube(Color.Red, GlobalJointCoords[i].ToArray(), 1);

                        if (i != 5)
                        {
                            GLObjects.Line(Color.Green, GlobalJointCoords[i].ToArray(), GlobalJointCoords[i + 1].ToArray());
                        }
                        else
                        {
                            GLObjects.Line(Color.Green, GlobalJointCoords[i].ToArray(), GlobalJointCoords[0].ToArray());
                        }

                    }

                    GLObjects.Cube(Color.Yellow, AbsRotationCenter.ToArray(), 1);
                }
            }
        }
    }
}
