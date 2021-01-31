using System;

namespace Hexapod_Simulator.Shared
{
    public class Hexapod
    {
        public IActuator[] Actuators { get; private set; }
        public IPlatform Base { get; protected set; }
        public IPlatform Top { get; protected set; }

        public event EventHandler RedrawRequired;
        public event EventHandler ServosCalculated;

        public Hexapod(double defaultHeight, double BaseRad, double TopRad, double BaseAngle, double TopAngle)
        {
            this.Base = InitializeBase(BaseRad, BaseAngle);
            this.Top = InitializeTop(defaultHeight, TopRad, TopAngle);
            
            this.Base.RedrawRequired += this.RaiseSubRedraws;
            this.Top.RedrawRequired += this.RaiseSubRedraws;

            this.Base.PositionChanged += this.PlatformCoordsChanged;
            this.Top.PositionChanged += this.PlatformCoordsChanged;

            InitializeActuators(ActuatorTypes.Linear);
        }
        public void InitializeActuators(ActuatorTypes AType)
        {
            Actuators = new IActuator[6];

            switch (AType)
            {
                case ActuatorTypes.Linear:
                    InitializeLinearActuators(20, 40);
                    break;

                case ActuatorTypes.Rotary:
                    InitializeRotaryActuators(10, 40);
                    break;

                default:
                    break;
            }
        }
        public void InitializeRotaryActuators(double armRadius, double linkLength)
        {
            for (int i = 0; i < 6; i++)
            {
                Actuators[i] = InitializeRotaryActuator(Base.GlobalJointCoords[i], Top.GlobalJointCoords[i], i, Base.JointAngle, armRadius, linkLength);
            }
        }
        public void InitializeLinearActuators(double maxTravel, double linkLength)
        {
            for (int i = 0; i < 6; i++)
            {
                Actuators[i] = InitializeLinearActuator(Base.GlobalJointCoords[i], Top.GlobalJointCoords[i], maxTravel, linkLength);
            }
        }


        protected virtual IPlatform InitializeBase(double BaseRad, double BaseAngle)
        {
            return new Platform("Base", BaseRad, BaseAngle);
        }
        protected virtual IPlatform InitializeTop(double defaultHeight, double TopRad, double TopAngle)
        {
            return new Platform("Top", TopRad, TopAngle, new double[] { 0, 0, defaultHeight });
        }
        protected virtual IActuator InitializeLinearActuator(double[] position, double[] linkEndPosition, double maxTravel, double linkLength)
        {
            return new LinearActuator(maxTravel, linkLength, position, linkEndPosition);
        }
        protected virtual IActuator InitializeRotaryActuator(double[] position, double[] linkEndPosition, int index, double jointAngle, double armRadius, double linkLength)
        {
            double motorAngle = RotaryActuator.calcMotorOffsetAngle(index, 0, Platform.CalcJointOffsetAngle(index, jointAngle));
            return new RotaryActuator(armRadius, motorAngle, linkLength, position, linkEndPosition);
        }



        private void PlatformCoordsChanged(object sender, EventArgs e)
        {
            IPlatform plat = (IPlatform)sender;

            if (plat == this.Top)
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

            ServosCalculated?.Invoke(this, new EventArgs());
        }
        private void RaiseSubRedraws(object sender, EventArgs e)
        {
            RedrawRequired?.Invoke(this, new EventArgs());
        }
    }

}
