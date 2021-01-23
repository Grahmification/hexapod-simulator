using Hexapod_Simulator.Shared;
using System;
using System.Runtime.Remoting.Channels;

namespace Hexapod_Simulator.Helix.ViewModels
{
    public class HexapodVM : BaseViewModel
    {
        public PlatformVM BasePlatform { get; private set; }
        public PlatformVM TopPlatform { get; private set; }
        public ActuatorVM [] Actuators { get; private set; }


        private Hexapod HexapodModel { get; set; }

        public HexapodVM() 
        {
            HexapodModel = new Hexapod(10, 10, 5, 30, 30);
            
            InitializeVMs();           
        }
        public HexapodVM(Hexapod hexapodModel)
        {
            HexapodModel = hexapodModel;     
            InitializeVMs();
        }

        /// <summary>
        /// Initializes all sub VMs for the class
        /// </summary>
        private void InitializeVMs()
        {
            //-------------- Setup Platforms --------------------
            
            BasePlatform = new PlatformVM((Platform)HexapodModel.Base);
            TopPlatform = new PlatformVM((Platform)HexapodModel.Top);

            BasePlatform.PlatformModel.RedrawRequired += onPlatFormGeometryChanged;
            TopPlatform.PlatformModel.RedrawRequired += onPlatFormGeometryChanged;

            //-------------- Setup Actuators --------------------

            Actuators = new ActuatorVM[6];

            for (int i = 0; i < 6; i++)
            {
                Actuators[i] = new ActuatorVM(HexapodModel.Actuators[i]);
                
            }
        }

        private void onPlatFormGeometryChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < 6; i++)
            {
                Actuators[i].Position = BasePlatform.PlatformModel.GlobalJointCoords[i];
                Actuators[i].LinkEndPosition = TopPlatform.PlatformModel.GlobalJointCoords[i];               
            }
        }


    }
}
