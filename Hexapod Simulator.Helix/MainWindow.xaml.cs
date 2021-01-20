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

            Binding TransformBinding2 = new Binding("Transform");
            TransformBinding2.Source = platformVM;
            BindingOperations.SetBinding(Hexa1.TopPlatform, ModelVisual3D.TransformProperty, TransformBinding2);



        }
    }
}
