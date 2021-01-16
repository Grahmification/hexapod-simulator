using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using Hexapod_Simulator.Shared;
using System.Windows.Media;
using System.Windows.Shapes;
using System;

namespace Hexapod_Simulator.Helix.Models
{
    public class PlatformVisual3D : MeshElement3D
    {
        public Brush Color { get; set; } = Brushes.Blue;

        public Platform platformModel { get; private set; }
        public PlatformVisual3D(Platform plat) : base()
        {
            platformModel = plat;
            platformModel.RedrawRequired += onRedrawRequired;
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

                //null check needed or else this method will fail during class initialization
                if (!(platformModel is null))
                {
                    p0 = platformModel.GlobalJointCoords[i];
                    p1 = platformModel.GlobalJointCoords[0];

                    if (i != 5)
                        p1 = platformModel.GlobalJointCoords[i + 1];

                    RC = platformModel.AbsRotationCenter;
                }

                this.Fill = Color;

                builder.AddSphere(new Point3D(p0[0], p0[1], p0[2]), 1);
                builder.AddPipe(new Point3D(p0[0], p0[1], p0[2]), new Point3D(p1[0], p1[1], p1[2]), 0.1, 0.5, 10);
            }

           
            builder.AddBox(new Point3D(RC[0], RC[1], RC[2]), 2, 2, 2);

            return builder.ToMesh();
        }

        //needed to tell the model to regenerate
        private void onRedrawRequired(object sender, EventArgs e)
        {
            UpdateModel();
        }
    }
}
