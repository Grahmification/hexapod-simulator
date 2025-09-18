namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// Represents a hexapod that can be translated and rotated in 3D space
    /// </summary>
    public class Hexapod
    {
        //----------- Public properties ----------------------

        /// <summary>
        /// The six actuators that lift the hexapod top platform
        /// </summary>
        public IActuator[] Actuators { get; private set; } = new IActuator[6];
        
        /// <summary>
        /// The lower stationary platform of the hexapod
        /// </summary>
        public IPlatform Base { get; protected set; }

        /// <summary>
        /// The upper moving platform of the hexapod
        /// </summary>
        public IPlatform Top { get; protected set; }

        //----------- Events ---------------------------------

        /// <summary>
        /// Raised if a redraw of the object is needed in the view
        /// </summary>
        public event EventHandler? RedrawRequired;

        /// <summary>
        /// Raised when all the hexapod actuators have their positions calculated successfully
        /// </summary>
        public event EventHandler? ServosCalculated;

        //----------- Public Methods -------------------------

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="defaultHeight">Height of the hexapod</param>
        /// <param name="baseRadius">Base platform radius</param>
        /// <param name="topRadius">Top platform radius</param>
        /// <param name="baseAngle">Base platform joint offset angle</param>
        /// <param name="topAngle">Top platform joint offset angle</param>
        public Hexapod(double defaultHeight, double baseRadius, double topRadius, double baseAngle, double topAngle)
        {
            Base = InitializeBase(baseRadius, baseAngle);
            Top = InitializeTop(defaultHeight, topRadius, topAngle);
            
            Base.RedrawRequired += (s, e) => { RedrawRequired?.Invoke(this, e); };
            Top.RedrawRequired += (s, e) => { RedrawRequired?.Invoke(this, e); };

            Base.PositionChanged += PlatformCoordsChanged;
            Top.PositionChanged += PlatformCoordsChanged;

            InitializeLinearActuators(20, 40);
        }
        
        /// <summary>
        /// Setup the hexapod with rotary arm actuators
        /// </summary>
        /// <param name="armRadius">The radius of each actuator arm</param>
        /// <param name="linkLength">The length of the link joining the actuator arm to the top platform joint</param>
        public void InitializeRotaryActuators(double armRadius, double linkLength)
        {
            for (int i = 0; i < 6; i++)
            {
                Actuators[i] = InitializeRotaryActuator(Base.GlobalJointCoords[i], Top.GlobalJointCoords[i], i, Base.JointAngle, armRadius, linkLength);
            }
        }

        /// <summary>
        /// Setup the hexapod with linear actuators
        /// </summary>
        /// <param name="maxTravel">The max sliding travel of the actuator</param>
        /// <param name="linkLength">The length of the link joining the actuator arm to the top platform joint</param>
        public void InitializeLinearActuators(double maxTravel, double linkLength)
        {
            for (int i = 0; i < 6; i++)
            {
                Actuators[i] = InitializeLinearActuator(Base.GlobalJointCoords[i], Top.GlobalJointCoords[i], maxTravel, linkLength);
            }
        }

        //----------- Protected Methods -----------------------

        protected virtual IPlatform InitializeBase(double baseRadius, double baseAngle)
        {
            return new Platform("Base", baseRadius, baseAngle);
        }
        protected virtual IPlatform InitializeTop(double defaultHeight, double topRadius, double topAngle)
        {
            return new Platform("Top", topRadius, topAngle, [0, 0, defaultHeight]);
        }
        protected virtual IActuator InitializeLinearActuator(double[] position, double[] linkEndPosition, double maxTravel, double linkLength)
        {
            return new LinearActuator(maxTravel, linkLength, position, linkEndPosition);
        }
        protected virtual IActuator InitializeRotaryActuator(double[] position, double[] linkEndPosition, int index, double jointAngle, double armRadius, double linkLength)
        {
            double motorAngle = RotaryActuator.CalcMotorOffsetAngle(index, 0, Platform.CalcJointOffsetAngle(index, jointAngle));
            return new RotaryActuator(armRadius, motorAngle, linkLength, position, linkEndPosition);
        }

        //----------- Event Methods ---------------------------

        /// <summary>
        /// Fires when one of the platforms positions changes
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Args</param>
        private void PlatformCoordsChanged(object? sender, EventArgs e)
        {
            if (sender is not null)
            {
                IPlatform plat = (IPlatform)sender;

                if (plat == Top)
                {
                    for (int i = 0; i < Actuators.Length; i++)
                    {
                        Actuators[i].LinkEndPosition = Top.GlobalJointCoords[i];
                    }
                }
                if (plat == Base)
                {
                    for (int i = 0; i < Actuators.Length; i++)
                    {
                        Actuators[i].Position = Base.GlobalJointCoords[i];
                    }
                }

                ServosCalculated?.Invoke(this, new EventArgs());
            }
        }
    }

}
