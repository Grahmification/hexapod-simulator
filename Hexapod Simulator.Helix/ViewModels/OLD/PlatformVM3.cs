using Hexapod_Simulator.Helix.Models.Old;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.ViewModels.Old
{
    public class PlatformVM3 : BaseViewModel
    {
        public static readonly DependencyProperty ZProperty = DependencyProperty.Register("Z", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));


        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get { return PlatformModel.Position[2]; } 
            set { 
                PlatformModel.TranslateAbs(new double[] { 0, 0, value });
                PlatformModel.platformVisual.UpdateModel();
            } }
        public double Pitch { get; set; } = 0;
        public double Roll { get; set; } = 0;
        public double Yaw { get; set; } = 0;



        public PlatformVisual3D3 PlatformModel { get; private set; }


        /// <summary>
        /// Allows ResetPosition to be bound to the UI
        /// </summary>
        public ICommand ResetPositionCommand { get; set; } 

        public PlatformVM3(PlatformVisual3D3 platformModel)
        {
            ResetPositionCommand = new RelayCommand(ResetPosition);
            PlatformModel = platformModel;
        }

        private void ResetPosition()
        {
            X = Y = Z = Pitch = Roll = Yaw = 0;
        }
    }
}
