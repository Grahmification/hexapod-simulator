using System.Windows.Input;
using Hexapod_Simulator.Shared;
using PropertyChanged;

namespace Hexapod_Simulator.Helix.ViewModels
{
    /// <summary>
    /// Viewmodel for a hexapod class
    /// </summary>
    public class HexapodVM : BaseViewModel
    {
        /// <summary>
        /// The hexapod's base platform
        /// </summary>
        public PlatformVM BasePlatform { get; private set; }
        
        /// <summary>
        /// The hexapod's top platform
        /// </summary>
        public PlatformVM TopPlatform { get; private set; }
        
        /// <summary>
        /// The actuators in the hexapod
        /// </summary>
        public ActuatorVM [] Actuators { get; private set; }

        /// <summary>
        /// Holds a temporary value of the actuator link length until the updateActuators command is executed
        /// </summary>
        public double ActuatorLinkLengthTemp { get; set; } = 10;

        /// <summary>
        /// Holds a temporary value of the actuator max travel until the updateActuators command is executed
        /// </summary>
        public double ActuatorMaxTravelTemp { get; set; } = 20;


        /// <summary>
        /// RelayCommand for <see cref="UpdateActuators"/>
        /// </summary>
        public ICommand UpdateActuatorsCommand { get; set; }

        /// <summary>
        /// The model class for the VM
        /// </summary>
        private Hexapod HexapodModel { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HexapodVM()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            HexapodModel = new Hexapod(10, 10, 5, 30, 30);
            
            InitializeVMs();
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HexapodVM(Hexapod hexapodModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            HexapodModel = hexapodModel;
            InitializeVMs();
        }

        /// <summary>
        /// Initializes all sub VMs for the class
        /// </summary>
        private void InitializeVMs()
        {
            //-------------- Setup Platforms --------------------
            
            BasePlatform = new PlatformVM((Platform)HexapodModel.Base);
            TopPlatform = new PlatformVM((Platform)HexapodModel.Top);

            BasePlatform.PlatformModel.RedrawRequired += OnPlatFormGeometryChanged;
            TopPlatform.PlatformModel.RedrawRequired += OnPlatFormGeometryChanged;

            UpdateActuatorsCommand = new RelayCommand(UpdateActuators);

            //-------------- Setup Actuators --------------------

            Actuators = new ActuatorVM[6];

            //initialize the actuators in each vm
            for (int i = 0; i < 6; i++)
                Actuators[i] = new ActuatorVM(HexapodModel.Actuators[i], i+1);

            //update the actuators to the default configuration
            UpdateActuators();
        }

        [SuppressPropertyChangedWarnings]
        private void OnPlatFormGeometryChanged(object? sender, EventArgs e)
        {
            for(int i = 0; i < 6; i++)
            {
                Actuators[i].Position = BasePlatform.PlatformModel.GlobalJointCoords[i];
                Actuators[i].LinkEndPosition = TopPlatform.PlatformModel.GlobalJointCoords[i];               
            }
        }



        /// <summary>
        /// Updates the actuators on the hexapod
        /// </summary>
        private void UpdateActuators()
        {
            HexapodModel.InitializeLinearActuators(ActuatorMaxTravelTemp, ActuatorLinkLengthTemp);

            for (int i = 0; i < 6; i++)
                Actuators[i].UpdateModel(HexapodModel.Actuators[i]);
        }
    }
}
