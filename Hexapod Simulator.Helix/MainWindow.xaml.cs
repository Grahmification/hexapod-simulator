using Hexapod_Simulator.Helix.ViewModels;
using System.Windows;
using System.Windows.Data;
using Hexapod_Simulator.Shared;
using Hexapod_Simulator.Helix.Views;

namespace Hexapod_Simulator.Helix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainVM();
            this.DataContext = vm;
            InitializeActuatorBindings(vm.Hexapod, Hexa1);
        }

        private void InitializeActuatorBindings(HexapodVM HexaVM, HexapodVisual3D HexaVisual)
        {
            //Look into observableCollections ---- there is probably a better way to handle this binding process
            for (int i = 0; i < 6; i++)
            {
                Binding PositionBinding = new Binding("Position");
                PositionBinding.Source = HexaVM.Actuators[i];
                BindingOperations.SetBinding(HexaVisual.Actuators[i], LinearActuatorVisual3D.PositionProperty, PositionBinding);

                Binding ArmEndPositionBinding = new Binding("ArmEndPosition");
                ArmEndPositionBinding.Source = HexaVM.Actuators[i];
                BindingOperations.SetBinding(HexaVisual.Actuators[i], LinearActuatorVisual3D.ArmEndPositionProperty, ArmEndPositionBinding);

                Binding LinkEndPositionBinding = new Binding("LinkEndPosition");
                LinkEndPositionBinding.Source = HexaVM.Actuators[i];
                BindingOperations.SetBinding(HexaVisual.Actuators[i], LinearActuatorVisual3D.LinkEndPositionProperty, LinkEndPositionBinding);

                Binding SolutionValidBinding = new Binding("SolutionValid");
                SolutionValidBinding.Source = HexaVM.Actuators[i];
                SolutionValidBinding.Converter = new ValueConverters.ActuatorSolnStateTo3DVisualColorValueConverter();
                BindingOperations.SetBinding(HexaVisual.Actuators[i], LinearActuatorVisual3D.ArmColorProperty, SolutionValidBinding);
                BindingOperations.SetBinding(HexaVisual.Actuators[i], LinearActuatorVisual3D.LinkColorProperty, SolutionValidBinding);
            }
        }
    }
}
