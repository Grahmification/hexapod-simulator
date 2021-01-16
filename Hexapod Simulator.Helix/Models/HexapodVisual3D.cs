using HelixToolkit.Wpf;
using Hexapod_Simulator.Shared;
using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.Models
{
    public class HexapodVisual3D : MeshElement3D
    {
        public Brush Color { get; set; } = Brushes.White;  
        public Hexapod HexapodModel { get; private set; }

        public HexapodVisual3D(Hexapod hexapodModel) : base()
        {
            HexapodModel = hexapodModel;
            UpdateModel();

            HexapodModel.RedrawRequired += onRedrawRequired;        }
        protected override MeshGeometry3D Tessellate()
        {
            var builder = new MeshBuilder(true, true);
            
            for (int i = 0; i < 6; i++)
            {
                var p0 = new double[] { 0, 0, 0 };
                var p1 = new double[] { 1, 1, 1 };

                //null check needed or else this method will fail during class initialization
                if (!(HexapodModel is null))
                {
                    p0 = HexapodModel.Base.GlobalJointCoords[i];
                    p1 = HexapodModel.Top.GlobalJointCoords[i];
                }
            
                this.Fill = Color;
                builder.AddPipe(new Point3D(p0[0], p0[1], p0[2]), new Point3D(p1[0], p1[1], p1[2]), 0.1, 0.5, 10);
            }

            return builder.ToMesh();
        }

        //needed to tell the model to regenerate
        private void onRedrawRequired(object sender, EventArgs e)
        {
            UpdateModel();
        }


    }
}
