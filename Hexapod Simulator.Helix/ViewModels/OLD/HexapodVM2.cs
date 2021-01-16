using Hexapod_Simulator.Helix.Models.Old;
using Hexapod_Simulator.Shared;
using System;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Hexapod_Simulator.Helix.ViewModels.Old
{
    public class HexapodVM2
    {
        public PlatformVisual3DModel BasePlatForm = new PlatformVisual3DModel(new Platform("Base", 10, 20));



        public HexapodVM2()
        {
            
        }

        private void ResetPosition()
        {
        
        }
    }
}
