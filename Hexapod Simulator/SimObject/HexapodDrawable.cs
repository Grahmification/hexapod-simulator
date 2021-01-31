using System;
using GFunctions.OpenTK;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.SimObject
{
    public class HexapodDrawable : Hexapod, IGLDrawable
    {
        public bool IsDrawn { get; set; }

        public HexapodDrawable(double defaultHeight, double BaseRad, double TopRad, double BaseAngle, double TopAngle) : base(defaultHeight, BaseRad,TopRad, BaseAngle, TopAngle)
        {
            this.IsDrawn = true;        
        } 
        public void Draw()
        {
            var tmpBase = (PlatformDrawable)Base;
            tmpBase.Draw();

            var tmpTop = (PlatformDrawable)Top;
            tmpTop.Draw();
      
            foreach (IGLDrawable Motor in Actuators)
                Motor.Draw();
        }

        protected override IPlatform InitializeBase(double BaseRad, double BaseAngle)
        {
            return new PlatformDrawable("Base", BaseRad, BaseAngle);
        }
        protected override IPlatform InitializeTop(double defaultHeight, double TopRad, double TopAngle)
        {
            return new PlatformDrawable("Top", TopRad, TopAngle, new double[] { 0, 0, defaultHeight });
        }
        protected override IActuator InitializeLinearActuator(double[] position, double[] linkEndPosition, double maxTravel, double linkLength)
        {
            return new LinearActuatorDrawable(maxTravel, linkLength, position, linkEndPosition);
        }
        protected override IActuator InitializeRotaryActuator(double[] position, double[] linkEndPosition, int index, double jointAngle, double armRadius, double linkLength)
        {
            double motorAngle = RotaryActuator.calcMotorOffsetAngle(index, 0, Platform.CalcJointOffsetAngle(index, jointAngle));
            return new RotaryActuatorDrawable(armRadius, motorAngle, linkLength, position, linkEndPosition);
        }
    }
















}
