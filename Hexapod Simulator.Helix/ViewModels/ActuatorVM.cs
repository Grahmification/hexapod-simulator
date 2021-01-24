using Hexapod_Simulator.Shared;
using System;

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
        public double[] ArmEndPosition { get { return ActuatorModel.ArmEndPosition; } }
        
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
        public bool SolutionValid { get { return ActuatorModel.SolutionValid; } }

        /// <summary>
        /// The current travel position of the actuator
        /// </summary>
        public double TravelPosition { get { return ActuatorModel.TravelPosition; } }

        /// <summary>
        /// The actuator number in the hexapod
        /// </summary>
        public int ActuatorNumber { get; private set; } = 0;

        /// <summary>
        /// What type of actuator the model is
        /// </summary>
        public ActuatorTypes ActuatorType { get { return ActuatorModel.ActuatorType; } }


        /// <summary>
        /// The model class for the VM - can be either a linear or rotary actuator
        /// </summary>
        private IActuator ActuatorModel { get; set; }


        public ActuatorVM(IActuator actuatorModel, int actuatorNumber)
        {
            ActuatorNumber = actuatorNumber;
            initializeModel(actuatorModel);
        }
        public ActuatorVM()
        {
            initializeModel(new LinearActuator(10, 5, new double[] { 0, 0, 0 }, new double[] { 5, 5, 5 }));          
        }


        /// <summary>
        /// Properly initializes the model within this class
        /// </summary>
        /// <param name="actuatorModel">The actuator model to set for the VM</param>
        private void initializeModel(IActuator actuatorModel)
        {
            ActuatorModel = actuatorModel;
            ActuatorModel.RedrawRequired += onRedrawRequired;
        }

        /// <summary>
        /// Used to notify any calculated parameters if they change
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void onRedrawRequired(object sender, EventArgs e)
        {
            OnPropertyChanged("ArmEndPosition");
            OnPropertyChanged("LinkEndPosition");
            OnPropertyChanged("Position");
            OnPropertyChanged("SolutionValid");
            OnPropertyChanged("TravelPosition");
        }

    }
}
