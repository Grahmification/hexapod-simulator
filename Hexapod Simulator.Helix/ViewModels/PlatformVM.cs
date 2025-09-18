using System.Windows.Input;
using System.Windows.Media.Media3D;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.Helix.ViewModels
{
    /// <summary>
    /// Viewmodel for a <see cref="Platform"/>
    /// </summary>
    public class PlatformVM : BaseViewModel
    {
        public double XTranslation
        {
            get { return PlatformModel.TranslationTarget[0]; }
            set { PlatformModel.TranslateAbs([value, YTranslation, ZTranslation]); }
        }
        public double YTranslation
        {
            get { return PlatformModel.TranslationTarget[1]; }
            set { PlatformModel.TranslateAbs([XTranslation, value, ZTranslation]); }
        }
        public double ZTranslation 
        {
            get { return PlatformModel.TranslationTarget[2]; }
            set { PlatformModel.TranslateAbs([XTranslation, YTranslation, value]); }
        }
        public double PitchRotation
        {
            get { return PlatformModel.RotationTarget[0]; }
            set { PlatformModel.RotateAbs([value, RollRotation, YawRotation]); }
        }
        public double RollRotation
        {
            get { return PlatformModel.RotationTarget[1]; }
            set { PlatformModel.RotateAbs([PitchRotation, value, YawRotation]); }
        }
        public double YawRotation
        {
            get { return PlatformModel.RotationTarget[2]; }
            set { PlatformModel.RotateAbs([PitchRotation, RollRotation, value]); }
        }


        public double[] Position => PlatformModel.Position;
        public double[] Rotation => PlatformModel.Rotation;
        public double[] AbsRotationCenter => PlatformModel.AbsRotationCenter;
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
        public string Name => PlatformModel.Name;

        /// <summary>
        /// The absolute transformation of the platform
        /// </summary>
        public Transform3D Transform => CreateTransform(PlatformModel.Position, PlatformModel.Rotation, PlatformModel.AbsRotationCenter);

        /// <summary>
        /// The absolute transformation of the platform rotation center
        /// </summary>
        public Transform3D RotationCenterTransform => CreateTransform(PlatformModel.AbsRotationCenter, PlatformModel.Rotation, PlatformModel.AbsRotationCenter);


        /// <summary>
        /// Holds a temporary value of the <see cref="Radius"/> until the updateconfig command is executed
        /// </summary>
        public double RadiusTemp { get; set; } = 10;

        /// <summary>
        /// Holds a temporary value of the <see cref="JointAngle"/> until the updateconfig command is executed
        /// </summary>
        public double JointAngleTemp { get; set; } = 30;

        /// <summary>
        /// Holds a temporary value of the <see cref="Platform.DefaultPos"/> until the updateconfig command is executed
        /// </summary>
        public double[] DefaultPositionTemp { get; set; } = [0, 0, 0];

        /// <summary>
        /// Holds a temporary value of the <see cref="Platform.RotationCenter"/> until the updateconfig command is executed
        /// </summary>
        public double[] RotationCenterTemp { get; set; } = [0, 0, 0];

        /// <summary>
        /// Holds a temporary value of the <see cref="Platform.FixedRotationCenter"/> until the updateconfig command is executed
        /// </summary>
        public bool FixedRotationCenterTemp { get; set; } = false;

        public Platform PlatformModel { get; private set; }


        /// <summary>
        /// RelayCommand for <see cref="ResetPosition"/>
        /// </summary>
        public ICommand? ResetPositionCommand { get; set; }

        /// <summary>
        /// RelayCommand for <see cref="UpdateConfig"/>
        /// </summary>
        public ICommand? UpdateConfigCommand { get; set; }

        /// <summary>
        /// RelayCommand for <see cref="UpdateRotationCenter"/>
        /// </summary>
        public ICommand? UpdateRotationCenterCommand { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PlatformVM(Platform platformModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeModel(platformModel);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PlatformVM()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeModel(new Platform("Temp", 10, 30));
        }
        public void SetSimulationState(bool SimulationRunning)
        {
            if (SimulationRunning)
                PlatformModel.TranslationMode = TranslationModes.Servo;
            else
                PlatformModel.TranslationMode = TranslationModes.Instant;
        }
        
        /// <summary>
        /// Sets up the model within the class
        /// </summary>
        /// <param name="platformModel"></param>
        private void InitializeModel(Platform platformModel)
        {
            PlatformModel = platformModel;
            PlatformModel.RedrawRequired += OnRedrawRequired;
            PlatformModel.LocalCoordsChanged += OnLocalCoordsChanged;

            ResetPositionCommand = new RelayCommand(ResetPosition);
            UpdateConfigCommand = new RelayCommand(UpdateConfig);
            UpdateRotationCenterCommand = new RelayCommand(UpdateRotationCenter);

            //set all these so they are updated to the initial value of the model
            RadiusTemp = PlatformModel.Radius;
            JointAngleTemp = PlatformModel.JointAngle;
            DefaultPositionTemp = PlatformModel.DefaultPos;
            RotationCenterTemp = PlatformModel.RotationCenter;
            FixedRotationCenterTemp = PlatformModel.FixedRotationCenter;

            //this is the only time the name will change
            OnPropertyChanged(nameof(Name));
        }

        /// <summary>
        /// Resets the platform translation/rotation
        /// </summary>
        private void ResetPosition()
        {
            //These commands are redundant, but update them anyway to update any controls bound to the values
            XTranslation = YTranslation = ZTranslation = 0;
            PitchRotation = RollRotation = YawRotation = 0;

            PlatformModel.ResetPosition();
        }

        /// <summary>
        /// Updates the platform configuation geometry to the temporary values
        /// </summary>
        private void UpdateConfig()
        {
            PlatformModel.UpdateConfig(RadiusTemp, JointAngleTemp, DefaultPositionTemp);
        }

        /// <summary>
        /// Updates the platform rotation center to the temporary values
        /// </summary>
        private void UpdateRotationCenter()
        {
            PlatformModel.UpdateRotationCenter(RotationCenterTemp, FixedRotationCenterTemp);
        }

        private void OnRedrawRequired(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Transform));
            OnPropertyChanged(nameof(RotationCenterTransform));
            OnPropertyChanged(nameof(AbsRotationCenter));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(Rotation));
        }
        private void OnLocalCoordsChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Radius));
            OnPropertyChanged(nameof(JointAngle));
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
            var rot = new RotateTransform3D(new QuaternionRotation3D(EulerToQuaternion(rotation[0], rotation[1], rotation[2])), new Point3D(rotationCenter[0], rotationCenter[1], rotationCenter[2]));

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

            Quaternion q = new()
            {
                W = cr * cp * cy + sr * sp * sy,
                X = sr * cp * cy - cr * sp * sy,
                Y = cr * sp * cy + sr * cp * sy,
                Z = cr * cp * sy - sr * sp * cy
            };

            return q;
        }
    }
}
