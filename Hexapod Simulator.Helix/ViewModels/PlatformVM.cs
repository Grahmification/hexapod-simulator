using Hexapod_Simulator.Helix.Models;
using Hexapod_Simulator.Shared;
using System.Windows;
using System.Windows.Input;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class PlatformVM : BaseViewModel
    {
        public static readonly DependencyProperty XTranslationProperty = DependencyProperty.Register("XTranslation", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));
        public static readonly DependencyProperty YTranslationProperty = DependencyProperty.Register("YTranslation", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));
        public static readonly DependencyProperty ZTranslationProperty = DependencyProperty.Register("ZTranslation", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));
        public static readonly DependencyProperty PitchProperty = DependencyProperty.Register("Pitch", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));

        public double XTranslation
        {
            get { return PlatformModel.Translation[0]; }
            set { PlatformModel.TranslateAbs(new double[] { value, 0, 0 }); }
        }
        public double YTranslation
        {
            get { return PlatformModel.Translation[1]; }
            set { PlatformModel.TranslateAbs(new double[] { 0, value ,0}); }
        }
        public double ZTranslation 
        { 
            get { return PlatformModel.Translation[2]; } 
            set { PlatformModel.TranslateAbs(new double[] { 0, 0, value }); } 
        }
        public double Pitch
        {
            get { return PlatformModel.Rotation[0]; }
            set { PlatformModel.RotateAbs(new double[] { value, 0, 0 }); }
        }
        public double Roll { get; set; } = 0;
        public double Yaw { get; set; } = 0;

        public double[] Translation { get { return PlatformModel.Translation; }}


        public PlatformVisual3D PlatformVisual { get; private set; }
        public Platform PlatformModel { get; private set; }


        /// <summary>
        /// Allows ResetPosition to be bound to the UI
        /// </summary>
        public ICommand ResetPositionCommand { get; set; } 

        public PlatformVM(Platform platformModel)
        {
            PlatformModel = platformModel;
            PlatformVisual = new PlatformVisual3D(platformModel);

            ResetPositionCommand = new RelayCommand(ResetPosition);            
        }
        private void ResetPosition()
        {
            XTranslation = 0;
            YTranslation = 0;
            ZTranslation = 0;
            Pitch = 0;
            Roll = 0;
            Yaw = 0;
        }
    }
}
