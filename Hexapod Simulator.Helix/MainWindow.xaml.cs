using Hexapod_Simulator.Helix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hexapod_Simulator.Shared;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
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

            //------------ Try out PlatformVisual3D -----------------------
            /*
            var platform = new Platform("test", 10, 30);
            var platformVM = new PlatformVM(platform);

            platform.UpdateConfig(10, 30, new double[] { 0, 0, 10 });

            //ManualPositionControl.DataContext = platformVM;

            //Do binding here for now. Update this to the xaml file once a sophisticated enough VM is ready for a single one to control the whole view
            Binding TransformBinding = new Binding("Transform");
            //TransformBinding.Source = platformVM;
            //BindingOperations.SetBinding(Platform1, ModelVisual3D.TransformProperty, TransformBinding);

            Binding RadiusBinding = new Binding("Radius");
            RadiusBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, PlatformVisual3D.RadiusProperty, RadiusBinding);

            Binding JointAngleBinding = new Binding("JointAngle");
            JointAngleBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, PlatformVisual3D.JointAngleProperty, JointAngleBinding);

            //Test out binding on the hexapod

            //BindingOperations.SetBinding(Hexa1.TopPlatform, ModelVisual3D.TransformProperty, TransformBinding); //this works fine

            //Test out the full hexapod VM
            var hexa = new HexapodVM();

            TransformBinding.Source = hexa.TopPlatform;
            ManualPositionControl.DataContext = hexa.TopPlatform;

            BindingOperations.SetBinding(Hexa1.TopPlatform, ModelVisual3D.TransformProperty, TransformBinding); //this works fine

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
            }
            */
        }


        private void Initialize1()
        {
            //------------ Try out PlatformVisual3D -----------------------

            var platform = new Platform("test", 10, 30);
            var platformVM = new PlatformVM(platform);

            platform.UpdateConfig(10, 30, new double[] { 0, 0, 10 });

            ManualPositionControl.DataContext = platformVM;

            //Do binding here for now. Update this to the xaml file once a sophisticated enough VM is ready for a single one to control the whole view
            Binding TransformBinding = new Binding("Transform");
            TransformBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, ModelVisual3D.TransformProperty, TransformBinding);

            Binding RadiusBinding = new Binding("Radius");
            RadiusBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, PlatformVisual3D.RadiusProperty, RadiusBinding);

            Binding JointAngleBinding = new Binding("JointAngle");
            JointAngleBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, PlatformVisual3D.JointAngleProperty, JointAngleBinding);

            //Test out binding on the hexapod

            BindingOperations.SetBinding(Hexa1.TopPlatform, ModelVisual3D.TransformProperty, TransformBinding); //this works fine
        }

        private void Initialize2()
        {
            var hexaModel = new Hexapod(30, 30, 10, 30, 5);
            var hexa = new HexapodVM(hexaModel);

            ManualPositionControl.DataContext = hexa.TopPlatform;
            ActuatorPositionView.DataContext = hexa;


            Binding TransformBinding = new Binding("Transform");
            TransformBinding.Source = hexa.TopPlatform;        
            BindingOperations.SetBinding(Hexa1.TopPlatform, ModelVisual3D.TransformProperty, TransformBinding);

            Binding BaseRadiusBinding = new Binding("Radius");
            BaseRadiusBinding.Source = hexa.BasePlatform;
            BindingOperations.SetBinding(Hexa1.BasePlatform, PlatformVisual3D.RadiusProperty, BaseRadiusBinding);

            Binding TopRadiusBinding = new Binding("Radius");
            TopRadiusBinding.Source = hexa.TopPlatform;
            BindingOperations.SetBinding(Hexa1.TopPlatform, PlatformVisual3D.RadiusProperty, TopRadiusBinding);

            Binding TopJointAngleBinding = new Binding("JointAngle");
            TopJointAngleBinding.Source = hexa.TopPlatform;
            BindingOperations.SetBinding(Hexa1.TopPlatform, PlatformVisual3D.JointAngleProperty, TopJointAngleBinding);


            ManualPositionControl2.DataContext = hexa.BasePlatform;
            Binding TransformBinding2 = new Binding("Transform");
            TransformBinding2.Source = hexa.BasePlatform;
            BindingOperations.SetBinding(Hexa1.BasePlatform, ModelVisual3D.TransformProperty, TransformBinding2);

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
                //SolutionValidBinding.Converter = new ValueConverters.ActuatorSolnStateTo3DVisualColorValueConverter();
                BindingOperations.SetBinding(Hexa1.Actuators[i], LinearActuatorVisual3D.SolutionValidProperty, SolutionValidBinding);
            }
        }
    }
}
