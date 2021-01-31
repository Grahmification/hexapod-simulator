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
            Initialize2();
        }

        private void Initialize2()
        {
            var hexaModel = new Hexapod(15, 12, 8, 30, 5);
            var hexa = new HexapodVM(hexaModel);

            this.DataContext = hexa;
        
            //Look into observableCollections ---- there is probably a better way to handle this binding process
            for (int i = 0; i < 6; i++)
            {
                Binding PositionBinding = new Binding("Position");
                PositionBinding.Source = hexa.Actuators[i];
                BindingOperations.SetBinding(Hexa1.Actuators[i], LinearActuatorVisual3D.PositionProperty, PositionBinding);

                Binding ArmEndPositionBinding = new Binding("ArmEndPosition");
                ArmEndPositionBinding.Source = hexa.Actuators[i];
                BindingOperations.SetBinding(Hexa1.Actuators[i], LinearActuatorVisual3D.ArmEndPositionProperty, ArmEndPositionBinding);

                Binding LinkEndPositionBinding = new Binding("LinkEndPosition");
                LinkEndPositionBinding.Source = hexa.Actuators[i];
                BindingOperations.SetBinding(Hexa1.Actuators[i], LinearActuatorVisual3D.LinkEndPositionProperty, LinkEndPositionBinding);

                Binding SolutionValidBinding = new Binding("SolutionValid");
                SolutionValidBinding.Source = hexa.Actuators[i];
                SolutionValidBinding.Converter = new ValueConverters.ActuatorSolnStateTo3DVisualColorValueConverter();
                BindingOperations.SetBinding(Hexa1.Actuators[i], LinearActuatorVisual3D.ArmColorProperty, SolutionValidBinding);
                BindingOperations.SetBinding(Hexa1.Actuators[i], LinearActuatorVisual3D.LinkColorProperty, SolutionValidBinding);
            }
        }

        private void InitializePlatformBindings()
        {
            Binding TransformBinding = new Binding("Transform");
            //TransformBinding.Source = hexa.TopPlatform;
            //BindingOperations.SetBinding(Hexa1.TopPlatform, ModelVisual3D.TransformProperty, TransformBinding);

            Binding BaseRadiusBinding = new Binding("Radius");
            //BaseRadiusBinding.Source = hexa.BasePlatform;
            //BindingOperations.SetBinding(Hexa1.BasePlatform, PlatformVisual3D.RadiusProperty, BaseRadiusBinding);

            Binding TopRadiusBinding = new Binding("Radius");
            //TopRadiusBinding.Source = hexa.TopPlatform;
            //BindingOperations.SetBinding(Hexa1.TopPlatform, PlatformVisual3D.RadiusProperty, TopRadiusBinding);

            Binding TopJointAngleBinding = new Binding("JointAngle");
            //TopJointAngleBinding.Source = hexa.TopPlatform;
            //BindingOperations.SetBinding(Hexa1.TopPlatform, PlatformVisual3D.JointAngleProperty, TopJointAngleBinding);

            Binding TransformBinding2 = new Binding("Transform");
            //TransformBinding2.Source = hexa.BasePlatform;
            //BindingOperations.SetBinding(Hexa1.BasePlatform, ModelVisual3D.TransformProperty, TransformBinding2);
        }
    }
}
