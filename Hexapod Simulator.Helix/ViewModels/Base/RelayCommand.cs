using System.Windows.Input;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to run
        /// </summary>
        private Action mAction;
             
        /// <summary>
        /// The event that's fired when CanExecute value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender,e) =>{ };

        public RelayCommand(Action action)
        {
            mAction = action;
        }


        /// <summary>
        /// A relay command can always execute (will cause button to be greyed out if false, etc.)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Runs the action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction(); 
        }
    }
}
