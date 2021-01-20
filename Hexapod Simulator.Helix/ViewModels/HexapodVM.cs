using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class HexapodVM : BaseViewModel
    {
        public PlatformVM BasePlatform { get; private set; }
        public PlatformVM TopPlatform { get; private set; }

        public Hexapod HexapodModel { get; private set; }

        public HexapodVM() 
        {
            HexapodModel = new Hexapod(10, 10, 5, 30, 30);

            BasePlatform = new PlatformVM((Platform)HexapodModel.Base);
            TopPlatform = new PlatformVM((Platform)HexapodModel.Top);
        }
        public HexapodVM(Hexapod hexapodModel)
        {
            HexapodModel = hexapodModel;

            BasePlatform = new PlatformVM((Platform)HexapodModel.Base);
            TopPlatform = new PlatformVM((Platform)HexapodModel.Top);
        }

    }
}
