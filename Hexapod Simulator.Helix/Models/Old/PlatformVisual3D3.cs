using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using Hexapod_Simulator.Shared;
using System.Windows.Media;

namespace Hexapod_Simulator.Helix.Models.Old
{
    public class PlatformVisual3D3 : Platform
    {
        public PlatformDrawable platformVisual { get; private set; }

        public PlatformVisual3D3(string name, double radius, double jointAngle, double[] defaultPos = null) : base(name, radius, jointAngle, defaultPos)
        {
            platformVisual = new PlatformDrawable(this);
        }
       
    }


    public class PlatformDrawable : MeshElement3D
    {
        public PlatformVisual3D3 platformModel { get; private set; }


        public PlatformDrawable(PlatformVisual3D3 plat) : base()
        {
            platformModel = plat;
            UpdateModel();
            
        }
        protected override MeshGeometry3D Tessellate()
        {
            var builder = new MeshBuilder(true, true);


            var RC = new double[] { 0, 0, 0 };


            for (int i = 0; i < 6; i++)
            {
                var p0 = new double[] { 0, 0, 0 };
                var p1 = new double[] { 5, 5, 5 };


                if (!(platformModel is null))
                {
                    p0 = platformModel.GlobalJointCoords[i];
                    p1 = platformModel.GlobalJointCoords[0];

                    if (i != 5)
                        p1 = platformModel.GlobalJointCoords[i + 1];

                    RC = platformModel.AbsRotationCenter;
                }


                builder.AddSphere(new Point3D(p0[0], p0[1], p0[2]), 1);
                this.Fill = Brushes.White;
                builder.AddPipe(new Point3D(p0[0], p0[1], p0[2]), new Point3D(p1[0], p1[1], p1[2]), 0.1, 0.5, 10);
            }

            builder.AddBox(new Point3D(RC[0], RC[1], RC[2]), 2, 2, 2);

            return builder.ToMesh();
        }

    }
}
