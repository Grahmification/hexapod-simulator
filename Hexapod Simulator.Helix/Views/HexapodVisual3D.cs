using HelixToolkit.Wpf;
using Hexapod_Simulator.Shared;
using System;
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
        /// The base platform object
        /// </summary>
        private PlatformVisual3D basePlatform = new PlatformVisual3D();

        /// <summary>
        /// The base platform object
        /// </summary>
        private PlatformVisual3D topPlatform = new PlatformVisual3D();

        /// <summary>
        /// default constructor
        /// </summary>
        public HexapodVisual3D() : base()
        {
            TopPlatform = new PlatformVisual3D();
            BasePlatform = new PlatformVisual3D();
            

            BasePlatform.Radius = 12;
            TopPlatform.JointColor = Colors.Pink;

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

            var trans = new TranslateTransform3D(0, 0, 15);
            topPlatform.JointColor = Colors.Green;
            topPlatform.Radius = 4;
            topPlatform.Transform = trans;

            basePlatform.JointColor = Colors.Green;
            basePlatform.Radius = 10;

            if(!(TopPlatform is null))
                this.Children.Add(TopPlatform);

            if(!(BasePlatform is null))
                this.Children.Add(BasePlatform);
        
        }


    }
}
