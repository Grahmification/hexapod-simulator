using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;

namespace Hexapod_Simulator.Helix.Models
{
    public class PlatformVisual3D2 : MeshElement3D
    {
        public static readonly DependencyProperty LocalJointCoordsProperty = DependencyProperty.Register("LocalJointCoords", typeof(double[][]), typeof(PlatformVisual3D2), new UIPropertyMetadata(new double[6][], LocalCoordsChanged));
        
        public double[][] LocalJointCoords
        {
            get { return (double[][])GetValue(LocalJointCoordsProperty); }
            set { this.SetValue(LocalJointCoordsProperty, value); }
        } //XYZ pos [mm] of each joint
        public Brush Color { get; set; } = Brushes.Blue;

        public PlatformVisual3D2() : base()
        {
            this.LocalJointCoords = new double[6][];
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

                if(!(LocalJointCoords is null || LocalJointCoords[i] is null))
                {
                    p0 = LocalJointCoords[i];
                    p1 = LocalJointCoords[0];

                    if (i != 5)
                        p1 = LocalJointCoords[i + 1];
                }

                

                this.Fill = Color;

                builder.AddSphere(new Point3D(p0[0], p0[1], p0[2]), 1);
                builder.AddPipe(new Point3D(p0[0], p0[1], p0[2]), new Point3D(p1[0], p1[1], p1[2]), 0.1, 0.5, 10);
            }
        
            builder.AddBox(new Point3D(RC[0], RC[1], RC[2]), 2, 2, 2);

            return builder.ToMesh();
        }

        /// <summary>
        /// Needs to be called when the local coordinates are updated or the model won't regenerate.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected static void LocalCoordsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PlatformVisual3D2)d).UpdateModel();
        }

    }
}
