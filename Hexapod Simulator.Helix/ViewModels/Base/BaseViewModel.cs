using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace Hexapod_Simulator.Helix.ViewModels
{
    /// <summary>
    /// A base VM that fires property changed events as needed
    /// </summary>

    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event gets fired when any child property value gets changed
        /// </summary>    
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
