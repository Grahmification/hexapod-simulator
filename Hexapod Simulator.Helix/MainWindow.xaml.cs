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
using Hexapod_Simulator.Helix.Models;

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

            /*
            var hexaVM = new HexapodVM();

            //link the viewmodel to the control panel
            ManualPositionControl.DataContext = hexaVM;

            //set the viewmodel to the 3d view
            MainViewPort.DataContext = hexaVM;

            //create a position binding and link it to the position property on the viewmodel
            Binding myBinding = new Binding("Position");
            myBinding.Source = hexaVM;

            Platform plat = new Platform("Top", 20, 10);
            PlatformVisual3D platDraw = new PlatformVisual3D(plat);
            BindingOperations.SetBinding(platDraw, PlatformVisual3D.TransformProperty, myBinding);
            MainViewPort.Children.Add(platDraw);
            */

            /*
            var hexaVM2 = new HexapodVM2();
            ManualPositionControl.DataContext = hexaVM2.BasePlatForm;
            MainViewPort.DataContext = hexaVM2;

            MainViewPort.Children.Add(hexaVM2.BasePlatForm);

            Binding ZBinding = new Binding("Z");
            BindingOperations.SetBinding(hexaVM2.BasePlatForm, PlatformVisual3DModel.ZProperty, ZBinding);
            */

            /*
            var platform = new PlatformVisual3D2(new Platform("Base", 10, 30));
            var platFormVM = new PlatformVM(platform);
            ManualPositionControl.DataContext = platFormVM;
            MainViewPort.Children.Add(platFormVM.PlatformModel);

            Binding ZBinding = new Binding("Z");
            BindingOperations.SetBinding(platform, PlatformVM.ZProperty, ZBinding);
            */

            /*
            var platform = new PlatformVisual3D3("Base", 10, 30);
            var platFormVM = new PlatformVM3(platform);
            ManualPositionControl.DataContext = platFormVM;
            MainViewPort.Children.Add(platFormVM.PlatformModel.platformVisual);

            Binding ZBinding = new Binding("Z");
            BindingOperations.SetBinding(platform.platformVisual, PlatformVM3.ZProperty, ZBinding);
            */
            /*
            var platform = new Platform("Base", 10, 30);
            var platFormVM = new PlatformVM(platform);
            ManualPositionControl.DataContext = platFormVM;
            MainViewPort.Children.Add(platFormVM.PlatformVisual);

            Binding ZBinding = new Binding("Z");
            BindingOperations.SetBinding(platFormVM.PlatformVisual, PlatformVM.ZProperty, ZBinding);
            */

            //var hexa = new Hexapod(10, 10, 5, 30, 30);
            //var hexaVM = new HexapodVM(hexa);
            //ManualPositionControl.DataContext = hexaVM.TopPlatform;
            //MainViewPort.Children.Add(hexaVM.HexapodVisual);
            //MainViewPort.Children.Add(hexaVM.BasePlatform.PlatformVisual);
            //MainViewPort.Children.Add(hexaVM.TopPlatform.PlatformVisual);

            //Binding ZBinding = new Binding("ZTranslation");
            //BindingOperations.SetBinding(hexaVM.TopPlatform.PlatformVisual, PlatformVM.ZTranslationProperty, ZBinding);

            //Binding YBinding = new Binding("YTranslation");
            //BindingOperations.SetBinding(hexaVM.TopPlatform.PlatformVisual, PlatformVM.YTranslationProperty, YBinding);

            //Binding XBinding = new Binding("XTranslation");
            //BindingOperations.SetBinding(hexaVM.TopPlatform.PlatformVisual, PlatformVM.XTranslationProperty, XBinding);

            //------------ Try out PlatformVisual3D2 -----------------------

            var platform = new Platform("test", 10, 30);
            var platformVM = new PlatformVM2(platform);

            ManualPositionControl.DataContext = platformVM;

            var platformVisual = new PlatformVisual3D2();

            Binding TransformBinding = new Binding("Transform");
            TransformBinding.Source = platformVM;
            BindingOperations.SetBinding(platformVisual, ModelVisual3D.TransformProperty, TransformBinding);

            Binding LocalCoordsBinding = new Binding("LocalJointCoords");
            LocalCoordsBinding.Source = platformVM;
            BindingOperations.SetBinding(platformVisual, PlatformVisual3D2.LocalJointCoordsProperty, LocalCoordsBinding);

            //MainViewPort.Children.Add(platformVisual);

            //------------- Try out PlatformVisual3D3 -----------------------

            var platform2 = new Platform("test2", 7, 30);
            BindingOperations.SetBinding(Platform1, ModelVisual3D.TransformProperty, TransformBinding);

            Binding RadiusBinding = new Binding("Radius");
            RadiusBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, PlatformVisual3D3.RadiusProperty, RadiusBinding);

            Binding JointAngleBinding = new Binding("JointAngle");
            JointAngleBinding.Source = platformVM;
            BindingOperations.SetBinding(Platform1, PlatformVisual3D3.JointAngleProperty, JointAngleBinding);
        }
    }
}
