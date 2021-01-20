using Hexapod_Simulator.Shared;
using System;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class PlatformVM : BaseViewModel
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
        public Transform3D Transform { get { return CreateTransform(PlatformModel.Position, PlatformModel.Rotation, PlatformModel.AbsRotationCenter); } }


        public Platform PlatformModel { get; private set; }


        /// <summary>
        /// Allows ResetPosition to be bound to the UI
        /// </summary>
        public ICommand ResetPositionCommand { get; set; } 

        public PlatformVM(Platform platformModel)
        {
            PlatformModel = platformModel;
            PlatformModel.RedrawRequired += onRedrawRequired;
            PlatformModel.LocalCoordsChanged += onLocalCoordsChanged;

            ResetPositionCommand = new RelayCommand(ResetPosition);
        }
        public PlatformVM()
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


        /// <summary>
        /// Creates a 3D transform from a translation and rotation
        /// </summary>
        /// <param name="translation">[X,Y,Z] translation of the object</param>
        /// <param name="rotation">[P,R,Y] rotation of the object (degrees)</param>
        /// <param name="rotationCenter">[X,Y,Z] position of the object's rotation center</param>
        /// <returns></returns>
        private static Transform3D CreateTransform(double[] translation, double[] rotation, double[] rotationCenter)
        {
            var trans = new TranslateTransform3D(translation[0], translation[1], translation[2]);
            var rot = new RotateTransform3D(new QuaternionRotation3D(PlatformVM.EulerToQuaternion(rotation[0], rotation[1], rotation[2])), new Point3D(rotationCenter[0], rotationCenter[1], rotationCenter[2]));

            var output = new Transform3DGroup();
            output.Children.Add(trans);
            output.Children.Add(rot);
            return output;
        }

        /// <summary>
        /// Converts euler rotation angles to a quaternion
        /// </summary>
        /// <param name="pitch">Pitch rotation [deg]</param>
        /// <param name="roll">Roll rotation [deg]</param>
        /// <param name="yaw">Yaw rotation [deg]</param>
        /// <returns>The corresponding quaternion</returns>
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
    }
}
