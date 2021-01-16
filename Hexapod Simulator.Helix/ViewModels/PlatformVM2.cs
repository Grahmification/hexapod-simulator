using Hexapod_Simulator.Helix.Models;
using Hexapod_Simulator.Shared;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class PlatformVM2 : BaseViewModel
    {
        public double XTranslation
        {
            get { return PlatformModel.Translation[0]; }
            set { PlatformModel.TranslateAbs(new double[] { value, YTranslation, ZTranslation }); }
        }
        public double YTranslation
        {
            get { return PlatformModel.Translation[1]; }
            set { PlatformModel.TranslateAbs(new double[] { XTranslation, value , ZTranslation }); }
        }
        public double ZTranslation 
        { 
            get { return PlatformModel.Translation[2]; } 
            set { PlatformModel.TranslateAbs(new double[] { XTranslation, YTranslation, value }); } 
        }
        public double PitchRotation
        {
            get { return PlatformModel.Rotation[0]; }
            set { PlatformModel.RotateAbs(new double[] { value, RollRotation, YawRotation }); }
        }
        public double RollRotation
        {
            get { return PlatformModel.Rotation[1]; }
            set { PlatformModel.RotateAbs(new double[] { PitchRotation, value, YawRotation }); }
        }
        public double YawRotation
        {
            get { return PlatformModel.Rotation[2]; }
            set { PlatformModel.RotateAbs(new double[] { PitchRotation, RollRotation, value }); }
        }

        public double Radius
        {
            get { return PlatformModel.Radius; }
            set { PlatformModel.Radius = value; }
        }
        public double JointAngle
        {
            get { return PlatformModel.JointAngle; }
            set { PlatformModel.JointAngle = value; }
        }


        public double[][] LocalJointCoords { get { return PlatformModel.LocalJointCoords;  } }
        public Transform3D Transform
        {
            get
            {
                var trans = new TranslateTransform3D(XTranslation, YTranslation, ZTranslation);
                var rot = new RotateTransform3D(new QuaternionRotation3D(PlatformVM2.EulerToQuaternion(PlatformModel.Rotation[0], PlatformModel.Rotation[1], PlatformModel.Rotation[2])), new Point3D(PlatformModel.Position[0], PlatformModel.Position[1], PlatformModel.Position[2]));

                var output = new Transform3DGroup();
                output.Children.Add(trans);
                output.Children.Add(rot);
                return output;
            }
        }


        public Platform PlatformModel { get; private set; }


        /// <summary>
        /// Allows ResetPosition to be bound to the UI
        /// </summary>
        public ICommand ResetPositionCommand { get; set; } 

        public PlatformVM2(Platform platformModel)
        {
            PlatformModel = platformModel;
            PlatformModel.RedrawRequired += onRedrawRequired;
            PlatformModel.LocalCoordsChanged += onLocalCoordsChanged;

            ResetPositionCommand = new RelayCommand(ResetPosition);
        }
        public PlatformVM2()
        {
            PlatformModel = new Platform("Temp", 10, 30);
            ResetPositionCommand = new RelayCommand(ResetPosition);
        }

        private void ResetPosition()
        {
            XTranslation = YTranslation = ZTranslation = 0;
            PitchRotation = RollRotation = YawRotation = 0;

            //PlatformModel.TranslateAbs(new double[] { 0, 0, 0 });
        }
        private static Quaternion EulerToQuaternion(double pitch, double roll, double yaw)
        {
            //convert from degrees to radians
            pitch *= Math.PI / 180.0;
            roll *= Math.PI / 180.0;
            yaw *= Math.PI / 180.0;

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
        private void onRedrawRequired(object sender, EventArgs e)
        {
            OnPropertyChanged("Transform");
            OnPropertyChanged("LocalJointCoords");
        }
        private void onLocalCoordsChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Radius");
            OnPropertyChanged("JointAngle");
        }
    }
}
