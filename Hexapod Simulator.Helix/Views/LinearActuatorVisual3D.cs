using HelixToolkit.Wpf;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.Views
{
    public class LinearActuatorVisual3D : ModelVisual3D 
    {
        /// <summary>
        /// Identifies the <see cref="Position"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position",
            typeof(double[]),
            typeof(LinearActuatorVisual3D),
            new UIPropertyMetadata(new double[] { 0, 0, 0 }, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="ArmEndPosition"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ArmEndPositionProperty = DependencyProperty.Register(
            "ArmEndPosition",
            typeof(double[]),
            typeof(LinearActuatorVisual3D),
            new UIPropertyMetadata(new double[] { 0, 0, 1 }, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="LinkEndPosition"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkEndPositionProperty = DependencyProperty.Register(
            "LinkEndPosition",
            typeof(double[]),
            typeof(LinearActuatorVisual3D),
            new UIPropertyMetadata(new double[] { 0, 0, 5 }, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="SolutionValid"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SolutionValidProperty = DependencyProperty.Register(
            "SolutionValid",
            typeof(bool),
            typeof(LinearActuatorVisual3D),
            new UIPropertyMetadata(true, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="Color"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Color),
            typeof(PlatformVisual3D),
            new UIPropertyMetadata(Colors.Yellow, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="SolutionInvalidColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SolutionInvalidColorProperty = DependencyProperty.Register(
            "SolutionInvalidColorColor",
            typeof(Color),
            typeof(PlatformVisual3D),
            new UIPropertyMetadata(Colors.Red, GeometryChanged));


        /// <summary>
        /// Coordinates where the base of the actuator is located on the base [x,y,z]
        /// </summary>
        public double[] Position
        {
            get { return (double[])GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// Coordinates of actuator arm end position, where it attaches to link start [x,y,z]
        /// </summary>
        public double[] ArmEndPosition
        {
            get { return (double[])GetValue(ArmEndPositionProperty); }
            set { SetValue(ArmEndPositionProperty, value); }
        }

        /// <summary>
        /// Coordinates where the link attaches to the moving portion of the device [x,y,z]
        /// </summary>
        public double[] LinkEndPosition
        {
            get { return (double[])GetValue(LinkEndPositionProperty); }
            set { SetValue(LinkEndPositionProperty, value); }
        }

        /// <summary>
        /// Indicates if the axis position is allowed or not. 
        /// </summary>
        public bool SolutionValid
        {
            get { return (bool)GetValue(SolutionValidProperty); }
            set { SetValue(SolutionValidProperty, value); }
        }


        /// <summary>
        /// Actuator color when the solution is valid. 
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get { return (Color)this.GetValue(ColorProperty); }
            set { this.SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// Actuator color when the solution is invalid. 
        /// </summary>
        /// <value>The color.</value>
        public Color SolutionInvalidColor
        {
            get { return (Color)this.GetValue(SolutionInvalidColorProperty); }
            set { this.SetValue(SolutionInvalidColorProperty, value); }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LinearActuatorVisual3D() : base()
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
            ((LinearActuatorVisual3D)d).OnGeometryChanged();
        }


        /// <summary>
        /// Called when the geometry has changed.
        /// </summary>
        protected virtual void OnGeometryChanged()
        {
            this.Children.Clear();

            //----------------- Add the actuator arm ---------------------------
            var arm = new PipeVisual3D();
            arm.BeginEdit();
            arm.Point1 = new Point3D(ArmEndPosition[0], ArmEndPosition[1], ArmEndPosition[2]);
            arm.Point2 = new Point3D(Position[0], Position[1], Position[2]);
            arm.Diameter = 1;
            SetColor(arm);
            arm.EndEdit();
            this.Children.Add(arm);

            //-------------- Add the link -------------------
            var link = new PipeVisual3D();
            link.BeginEdit();
            link.Point1 = new Point3D(ArmEndPosition[0], ArmEndPosition[1], ArmEndPosition[2]);
            link.Point2 = new Point3D(LinkEndPosition[0], LinkEndPosition[1], LinkEndPosition[2]);
            link.Diameter = 0.5;
            SetColor(link);
            link.EndEdit();
            this.Children.Add(link);

       
        }

        /// <summary>
        /// Sets the visual color based on whether the solution is valid
        /// </summary>
        /// <param name="visual">The visual to set the color</param>
        private void SetColor(MeshElement3D visual)
        {
            if(SolutionValid)
                visual.Fill = new SolidColorBrush(this.Color);
            else
                visual.Fill = new SolidColorBrush(this.SolutionInvalidColor);
            
        }


    }
}
