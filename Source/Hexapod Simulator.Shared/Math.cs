namespace Hexapod_Simulator.Shared
{
    /// <summary>
    /// Represents a motion trajectory. Currently unfinished, just interpolates between start and end
    /// </summary>
    public class Trajectory(double maxAccel, double maxVeloc, int interpolationFreq)
    {
        /// <summary>
        /// Starting position
        /// </summary>
        public double StartPos { get; private set; } = 0;

        /// <summary>
        /// Ending position
        /// </summary>
        public double EndPos { get; private set; } = 0;

        /// <summary>
        /// Acceleration for the trajectory
        /// </summary>
        public double MaxAccel { get; set; } = maxAccel;

        /// <summary>
        /// Maximum velocity for the trajectory
        /// </summary>
        public double MaxVeloc { get; set; } = maxVeloc;

        /// <summary>
        /// The number of positions/second to generate between start/end
        /// </summary>
        public int InterpolationFrequency { get; set; } = interpolationFreq;

        /// <summary>
        /// Calculate the trajectory intermediate positions
        /// </summary>
        /// <param name="startPos">Starting position</param>
        /// <param name="endPos">Ending position</param>
        /// <param name="moveTime">Total trajectory points</param>
        /// <returns>The trajectory positions from start to end</returns>
        public double[] CalculatePositions(double startPos, double endPos, double moveTime)
        {
            StartPos = startPos;
            EndPos = endPos;

            //-------------------- Calculate number of points --------------------------

            int numPts = (int)(moveTime * InterpolationFrequency); //add 1 so last position gets calculated

            //----------------- Interpolate to get each intermediate position ------------

            List<double> output = [];

            for(int i = 0; i < numPts; i++)
            {
                output.Add(StartPos + (1.0 * i / (numPts-1)) * (EndPos - StartPos)); //linearly interpolate
            }
            return [.. output];
        }
    }
}
