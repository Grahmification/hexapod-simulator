using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.Helix.ViewModels
{
    /// <summary>
    /// Viewmodel for an actuator class
    /// </summary>
    public class ActuatorVM : BaseViewModel
    {
        /// <summary>
        /// Coordinates where the base of the actuator is located on the base [x,y,z]
        /// </summary>
        public double[] Position { 
            get { return ActuatorModel.Position; }  
            set { ActuatorModel.Position = value; }
        }

        /// <summary>
        /// Coordinates of actuator arm end position, where it attaches to link start [x,y,z]
        /// </summary>
        public double[] ArmEndPosition => ActuatorModel.ArmEndPosition;
        
        /// <summary>
        /// Coordinates where the link attaches to the moving portion of the device [x,y,z]
        /// </summary>
        public double[] LinkEndPosition { 
            get { return ActuatorModel.LinkEndPosition; } 
            set { ActuatorModel.LinkEndPosition = value; }
        }

        /// <summary>
        /// Indicates whether the actuator is in a valid position
        /// </summary>
        public bool SolutionValid => ActuatorModel.SolutionValid;

        /// <summary>
        /// The current travel position of the actuator
        /// </summary>
        public double TravelPosition => ActuatorModel.TravelPosition;

        /// <summary>
        /// The actuator number in the hexapod
        /// </summary>
        public int ActuatorNumber { get; private set; } = 0;

        /// <summary>
        /// What type of actuator the model is
        /// </summary>
        public ActuatorTypes ActuatorType => ActuatorModel.ActuatorType;

        /// <summary>
        /// The model class for the VM - can be either a linear or rotary actuator
        /// </summary>
        private IActuator ActuatorModel { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ActuatorVM(IActuator actuatorModel, int actuatorNumber)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ActuatorNumber = actuatorNumber;
            UpdateModel(actuatorModel);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ActuatorVM()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            UpdateModel(new LinearActuator(10, 5, [0, 0, 0], [ 5, 5, 5]));
        }

        /// <summary>
        /// Properly initializes the model within this class
        /// </summary>
        /// <param name="actuatorModel">The actuator model to set for the VM</param>
        public void UpdateModel(IActuator actuatorModel)
        {
            ActuatorModel = actuatorModel;
            ActuatorModel.RedrawRequired += OnRedrawRequired;
            OnRedrawRequired(this, new EventArgs());
        }

        /// <summary>
        /// Used to notify any calculated parameters if they change
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void OnRedrawRequired(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ArmEndPosition));
            OnPropertyChanged(nameof(LinkEndPosition));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(SolutionValid));
            OnPropertyChanged(nameof(TravelPosition));
        }
    }
}
