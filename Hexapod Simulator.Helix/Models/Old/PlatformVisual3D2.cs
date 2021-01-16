using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using Hexapod_Simulator.Shared;
using System.Windows.Media;

namespace Hexapod_Simulator.Helix.Models.Old
{
    public class PlatformVisual3D2 : MeshElement3D
    {
        public IPlatform platformModel { get; set; }             
        public PlatformVisual3D2(IPlatform plat) : base()
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
        private MeshGeometry3D Tessellate2()
        {
            var builder = new MeshBuilder(true, true);

            if(!(platformModel is null))
            {
                if (platformModel.GlobalJointCoords != null)
                {
                    if (platformModel.GlobalJointCoords.Length == 6)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            var x0 = platformModel.GlobalJointCoords[i][0];
                            var y0 = platformModel.GlobalJointCoords[i][1];
                            var z0 = platformModel.GlobalJointCoords[i][2];

                            builder.AddSphere(new Point3D(x0, y0, z0), 1);

                            var x1 = platformModel.GlobalJointCoords[0][0];
                            var y1 = platformModel.GlobalJointCoords[0][1];
                            var z1 = platformModel.GlobalJointCoords[0][2];


                            if (i != 5)
                            {
                                x1 = platformModel.GlobalJointCoords[i + 1][0];
                                y1 = platformModel.GlobalJointCoords[i + 1][1];
                                z1 = platformModel.GlobalJointCoords[i + 1][2];                     
                            }

                            builder.AddPipe(new Point3D(x0, y0, z0), new Point3D(x1, y1, z1), 0.1, 0.5, 10);
                        }


                        builder.AddBox(new Point3D(platformModel.AbsRotationCenter[0], platformModel.AbsRotationCenter[1], platformModel.AbsRotationCenter[2]), 2, 2, 2);

                    }
                }
            }

            return builder.ToMesh();
        }
    }
}
