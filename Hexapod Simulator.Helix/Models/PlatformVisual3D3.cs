using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.Helix.Models
{
    public class PlatformVisual3D3 : ModelVisual3D
    {
        /// <summary>
        /// Identifies the <see cref="Radius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(PlatformVisual3D3), new UIPropertyMetadata(5.0, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="JointAngle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty JointAngleProperty = DependencyProperty.Register("JointAngle", typeof(double), typeof(PlatformVisual3D3), new UIPropertyMetadata(30.0, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="JointColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty JointColorProperty = DependencyProperty.Register(
            "JointColor",
            typeof(Color),
            typeof(PlatformVisual3D3),
            new UIPropertyMetadata(Colors.Blue));

        /// <summary>
        /// Identifies the <see cref="LinkColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkColorProperty = DependencyProperty.Register(
            "LinkColor",
            typeof(Color),
            typeof(PlatformVisual3D3),
            new UIPropertyMetadata(Colors.AliceBlue));


        /// <summary>
        /// The radius of the platform
        /// </summary>
        public double Radius 
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        /// <summary>
        /// The offset angle of each corner node of the platform in degrees
        /// </summary>
        public double JointAngle
        {
            get { return (double)GetValue(JointAngleProperty); }
            set { SetValue(JointAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the platform's joints.
        /// </summary>
        /// <value>The plaform's joint color.</value>
        public Color JointColor
        {
            get { return (Color)this.GetValue(JointColorProperty); }
            set { this.SetValue(JointColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the platform's links.
        /// </summary>
        /// <value>The plaform's link color.</value>
        public Color LinkColor
        {
            get { return (Color)this.GetValue(LinkColorProperty); }
            set { this.SetValue(LinkColorProperty, value); }
        }


        /// <summary>
        /// XYZ pos [mm] of each joint, without trans/rotation 
        /// </summary>
        private double[][] LocalJointCoords;

        /// <summary>
        /// default constructor
        /// </summary>
        public PlatformVisual3D3() : base()
        {
            OnGeometryChanged();
        }
       
        /// <summary>
        /// Called by a dependency property when the geometry gets changed.
        /// </summary>
        /// <param name="d">The object</param>
        /// <param name="e">The args.</param>
        protected static void GeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PlatformVisual3D3)d).OnGeometryChanged();
        }


        /// <summary>
        /// Called when the geometry has changed.
        /// </summary>
        protected virtual void OnGeometryChanged()
        {
            //calculate the local joint coordinates
            LocalJointCoords = Platform.CalcLocalCoords(JointAngle, Radius);

            this.Children.Clear();

            //iterate through and add each link
            for (int i = 0; i < 6; i++)
            {
                var p0 = LocalJointCoords[i];
                var p1 = LocalJointCoords[0];

                if (i != 5)
                    p1 = LocalJointCoords[i + 1];

                //-------------- Create each joint node ---------------
                var joint = new SphereVisual3D();
                joint.BeginEdit();
                joint.Center = new Point3D(p0[0], p0[1], p0[2]);
                joint.Radius = 1;
                joint.Fill = new SolidColorBrush(this.JointColor);
                joint.EndEdit();
                this.Children.Add(joint);

                //-------------- Create each connection link ---------------
                var link = new PipeVisual3D();
                link.BeginEdit();
                link.Point1 = new Point3D(p0[0], p0[1], p0[2]);
                link.Point2 = new Point3D(p1[0], p1[1], p1[2]);
                link.Diameter = 0.5;
                link.ThetaDiv = 10;
                link.Fill = new SolidColorBrush(this.LinkColor);
                link.EndEdit();
                this.Children.Add(link);
            }
        }

    }
}
