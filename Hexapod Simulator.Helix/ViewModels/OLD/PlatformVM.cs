using Hexapod_Simulator.Helix.Models.Old;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.ViewModels.Old
{
    public class PlatformVM : BaseViewModel
    {
        public static readonly DependencyProperty ZProperty = DependencyProperty.Register("Z", typeof(double), typeof(PlatformVisual3D), new UIPropertyMetadata(0.0));


        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get { return PlatformModel.platformModel.Position[2]; } 
            set { 
                PlatformModel.platformModel.TranslateAbs(new double[] { 0, 0, value });
                PlatformModel.UpdateModel();
            } }
        public double Pitch { get; set; } = 0;
        public double Roll { get; set; } = 0;
        public double Yaw { get; set; } = 0;

        public Transform3D Position { get 
            {
                var trans = new TranslateTransform3D(X, Y, Z);
                var rot = new RotateTransform3D(new QuaternionRotation3D(PlatformVM.EulerToQuaternion(Yaw, Pitch, Roll)), new Point3D(X, Y, Z));

                var output = new Transform3DGroup();
                output.Children.Add(trans);
                output.Children.Add(rot);
                return output;
                
            } }

        public PlatformVisual3D2 PlatformModel { get; private set; }


        /// <summary>
        /// Allows ResetPosition to be bound to the UI
        /// </summary>
        public ICommand ResetPositionCommand { get; set; } 

        public PlatformVM(PlatformVisual3D2 platformModel)
        {
            ResetPositionCommand = new RelayCommand(ResetPosition);
            PlatformModel = platformModel;
        }

        private void ResetPosition()
        {
            X = Y = Z = Pitch = Roll = Yaw = 0;
        }
        private static Quaternion EulerToQuaternion(double yaw, double pitch, double roll)
        {
            // Abbreviations for the various angular functions
            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            Quaternion q = new Quaternion();
            q.W = cr * cp * cy + sr * sp * sy;
            q.X = sr * cp * cy - cr * sp * sy;
            q.Y = cr * sp * cy + sr * cp * sy;
            q.Z = cr * cp * sy - sr * sp * cy;

            return q;
        }
    }
}
