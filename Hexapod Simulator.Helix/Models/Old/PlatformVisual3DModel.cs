using HelixToolkit.Wpf;
using Hexapod_Simulator.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using PropertyChanged;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using Hexapod_Simulator.Helix.ViewModels;

namespace Hexapod_Simulator.Helix.Models.Old
{
    [AddINotifyPropertyChangedInterface]
    public class PlatformVisual3DModel : MeshElement3D, INotifyPropertyChanged
    {
        /// <summary>
        /// Event gets fired when any child property value gets changed
        /// </summary>    
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty ZProperty = DependencyProperty.Register("Z", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));

        public double Z
        {
            get { return platformModel.Position[2]; }
            set
            {
                platformModel.TranslateAbs(new double[] { 0, 0, value });
                UpdateModel();
            }
        }


        /// <summary>
        /// Allows ResetPosition to be bound to the UI
        /// </summary>
        public ICommand ResetPositionCommand { get; set; }


        private void ResetPosition()
        {
            Z = 0;
        }


        public IPlatform platformModel { get; set; }

        public PlatformVisual3DModel(IPlatform plat) : base()
        {
            platformModel = plat;

            ResetPositionCommand = new RelayCommand(ResetPosition);

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
