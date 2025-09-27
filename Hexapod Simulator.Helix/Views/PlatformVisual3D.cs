﻿using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using Hexapod_Simulator.Shared;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.Helix.Views
{
    public class PlatformVisual3D : ModelVisual3D
    {
        /// <summary>
        /// Identifies the <see cref="Radius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(5.0, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="JointAngle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty JointAngleProperty = DependencyProperty.Register("JointAngle", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(30.0, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="RotationCenterTransform"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationCenterTransformProperty = DependencyProperty.Register("RotationCenterTransform", typeof(Transform3D), typeof(PlatformVisual3D), new UIPropertyMetadata(new TranslateTransform3D(0,0,0), GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="JointColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty JointColorProperty = DependencyProperty.Register(
            "JointColor",
            typeof(Color),
            typeof(PlatformVisual3D),
            new UIPropertyMetadata(Colors.Blue, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="LinkColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkColorProperty = DependencyProperty.Register(
            "LinkColor",
            typeof(Color),
            typeof(PlatformVisual3D),
            new UIPropertyMetadata(Colors.AliceBlue, GeometryChanged));


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
        /// The rotation center transformation
        /// </summary>
        public Transform3D RotationCenterTransform
        {
            get { return (Transform3D)GetValue(RotationCenterTransformProperty); }
            set { SetValue(RotationCenterTransformProperty, value); }
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
        private Vector3[]? LocalJointCoords;

        /// <summary>
        /// default constructor
        /// </summary>
        public PlatformVisual3D() : base()
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
            ((PlatformVisual3D)d).OnGeometryChanged();
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
                joint.Center = new Point3D(p0.X, p0.Y, p0.Z);
                joint.Radius = 1;
                joint.Fill = new SolidColorBrush(this.JointColor);
                joint.EndEdit();
                this.Children.Add(joint);
                
                //-------------- Create each connection link ---------------
                var link = new PipeVisual3D();
                link.BeginEdit();
                link.Point1 = new Point3D(p0.X, p0.Y, p0.Z);
                link.Point2 = new Point3D(p1.X, p1.Y, p1.Z);
                link.Diameter = 0.5;
                link.Fill = new SolidColorBrush(this.LinkColor);
                link.EndEdit();
                this.Children.Add(link);

                //-------------- Create Coordinate System ---------------
                var coords = new CoordinateSystemVisual3D();
                coords.ArrowLengths = 3;
                
                //Need to create an absolute transform without the transformation of the platform added.
                //The inverse of the platform transform must be added last for this to work correctly. 
                var absXform = new Transform3DGroup();
                absXform.Children.Add(RotationCenterTransform);
                absXform.Children.Add((Transform3D)Transform.Inverse);            
                coords.Transform = absXform;

                this.Children.Add(coords);
            }
        }

    }
}
