using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.Views
{
    public class HexapodVisual3D : ModelVisual3D
    {
        /// <summary>
        /// Identifies the <see cref="BasePlatform"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BasePlatformProperty = DependencyProperty.Register(
            "BasePlatform",
            typeof(PlatformVisual3D),
            typeof(HexapodVisual3D),
            new UIPropertyMetadata(null, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="TopPlatform"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TopPlatformProperty = DependencyProperty.Register(
            "TopPlatform",
            typeof(PlatformVisual3D),
            typeof(HexapodVisual3D),
            new UIPropertyMetadata(null, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="Actuators"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActuatorsProperty = DependencyProperty.Register(
            "Actuators",
            typeof(LinearActuatorVisual3D[]),
            typeof(HexapodVisual3D),
            new UIPropertyMetadata(null, GeometryChanged));

        /// <summary>
        /// The lower platform of the hexapod
        /// </summary>
        public PlatformVisual3D BasePlatform
        {
            get { return (PlatformVisual3D)GetValue(BasePlatformProperty); }
            set { SetValue(BasePlatformProperty, value); }
        }

        /// <summary>
        /// The upper platform of the hexapod
        /// </summary>
        public PlatformVisual3D TopPlatform
        {
            get { return (PlatformVisual3D)GetValue(TopPlatformProperty); }
            set { SetValue(TopPlatformProperty, value); }
        }

        /// <summary>
        /// The upper platform of the hexapod
        /// </summary>
        public LinearActuatorVisual3D[] Actuators
        {
            get { return (LinearActuatorVisual3D[])GetValue(ActuatorsProperty); }
            set { SetValue(ActuatorsProperty, value); }
        }


        /// <summary>
        /// default constructor
        /// </summary>
        public HexapodVisual3D() : base()
        {
            TopPlatform = new PlatformVisual3D();
            BasePlatform = new PlatformVisual3D();

            BasePlatform.Radius = 10;
            
            TopPlatform.Transform = new TranslateTransform3D(0, 0, 10);
            TopPlatform.JointColor = Colors.Pink;
            TopPlatform.Radius = 5;

            Actuators = new LinearActuatorVisual3D[6];

            for(int i = 0; i < 6; i++)
                Actuators[i] = new LinearActuatorVisual3D();
                
            OnGeometryChanged();
        }

        /// <summary>
        /// Called by a dependency property when the geometry gets changed.
        /// </summary>
        /// <param name="d">The object</param>
        /// <param name="e">The args.</param>
        protected static void GeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HexapodVisual3D)d).OnGeometryChanged();
        }


        /// <summary>
        /// Called when the geometry has changed.
        /// </summary>
        protected virtual void OnGeometryChanged()
        {
            this.Children.Clear();

            if(!(TopPlatform is null))
                this.Children.Add(TopPlatform);

            if(!(BasePlatform is null))
                this.Children.Add(BasePlatform);

            if (!(Actuators is null))
                foreach (LinearActuatorVisual3D actuator in Actuators)
                    if (!(actuator is null))
                        this.Children.Add(actuator);
        }


    }
}
