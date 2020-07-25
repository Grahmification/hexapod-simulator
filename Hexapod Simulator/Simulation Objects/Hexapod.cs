using System;
using GFunctions.Graphics;

namespace Hexapod_Simulator.SimObject
{
    public class Hexapod : IGLDrawable
    {

        public IActuator[] Actuators { get; private set; }  
        public Platform Base { get; private set; }
        public Platform Top { get; private set; }
        public bool IsDrawn { get; set; }

        public event EventHandler RedrawRequired;
        public event EventHandler ServosCalculated;

        public Hexapod(double defaultHeight, double BaseRad, double TopRad, double BaseAngle, double TopAngle)
        {
            this.IsDrawn = true;
            
            this.Base = new Platform("Base", BaseRad, BaseAngle);
            this.Top = new Platform("Top", TopRad, TopAngle, new double[]{0,0, defaultHeight});

            this.Base.RedrawRequired += this.RaiseSubRedraws;
            this.Top.RedrawRequired += this.RaiseSubRedraws;

            this.Base.PositionChanged += this.PlatformCoordsChanged;
            this.Top.PositionChanged += this.PlatformCoordsChanged;

            InitializeActuators(ActuatorTypes.Linear); 
        } 
        public void Draw()
        {
            Base.Draw();
            Top.Draw();

            for (int i = 0; i < 6; i++)
            {
                //GLObjects.Line(Color.Blue, Base.GlobalJointCoords[i], Top.GlobalJointCoords[i]);
            }
          
            foreach (IActuator Motor in Actuators)
            {
                Motor.Draw();
            }
        }

        public void InitializeActuators(ActuatorTypes AType)
        {
            switch (AType)
            {
                case ActuatorTypes.Linear:
                    Actuators = new LinearActuator[6];

                    for (int i = 0; i < 6; i++)
                    {
                        Actuators[i] = new LinearActuator(20, 40, Base.GlobalJointCoords[i], Top.GlobalJointCoords[i]);
                    }
                    break;

                case ActuatorTypes.Rotary:
                    Actuators = new RotaryActuator[6];

                    for (int i = 0; i < 6; i++)
                    {
                        double motorAngle = RotaryActuator.calcMotorOffsetAngle(i, 0, Platform.CalcJointOffsetAngle(i, Base.JointAngle));
                        Actuators[i] = new RotaryActuator(10, motorAngle, 40, Base.GlobalJointCoords[i], Top.GlobalJointCoords[i]);
                    }
                    break; 

                default:
                    break;
            }
        }

        private void PlatformCoordsChanged(object sender, EventArgs e)
        {
            Platform plat = (Platform)sender;

            if(plat == this.Top)
            {
                for (int i = 0; i < Actuators.Length; i++)
                {
                    Actuators[i].LinkEndPosition = Top.GlobalJointCoords[i];
                }
            }
            if (plat == this.Base)
            {
                for (int i = 0; i < Actuators.Length; i++)
                {
                    Actuators[i].Position = Base.GlobalJointCoords[i];
                }
            }

            if (this.ServosCalculated != null)
                ServosCalculated(this, new EventArgs());
        }
        private void RaiseSubRedraws(object sender, EventArgs e)
        {
            if(this.RedrawRequired != null)
                RedrawRequired(this, new EventArgs());
        }

      
    }
    
   

    
    




    


    
    
    

}
