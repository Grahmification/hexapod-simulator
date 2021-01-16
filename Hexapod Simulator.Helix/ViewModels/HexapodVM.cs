using Hexapod_Simulator.Helix.Models;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class HexapodVM : BaseViewModel
    {
        public PlatformVM BasePlatform { get; private set; }
        public PlatformVM TopPlatform { get; private set; }

        public Hexapod HexapodModel { get; private set; }

        //this object should never be in the viewmodel - not following MVVM
        public HexapodVisual3D HexapodVisual { get; private set; }

        public HexapodVM() { }
        public HexapodVM(Hexapod hexapodModel)
        {
            HexapodModel = hexapodModel;
            HexapodVisual = new HexapodVisual3D(hexapodModel);

            BasePlatform = new PlatformVM((Platform)HexapodModel.Base);
            TopPlatform = new PlatformVM((Platform)HexapodModel.Top);
     
        } 
    }
}
