using System.Collections.Generic;

namespace Hexapod_Simulator.Shared
{
    public class Trajectory
    {        
        public double StartPos { get; private set; }
        public double EndPos { get; private set; }

        public double MaxAccel { get; set; } //acceleration
        public double MaxVeloc { get; set; } //max tragectory velocity

        public int InterpolationFrequency { get; set; } //number of positions/second to generate between start/end

        public Trajectory(double maxAccel, double maxVeloc, int interpolationFreq)
        {           
            this.MaxAccel = maxAccel;
            this.MaxVeloc = maxVeloc;
            this.InterpolationFrequency = interpolationFreq; 
        }    

        public double[] CalculatePositions(double startPos, double endPos, double moveTime)
        {
            this.StartPos = startPos;
            this.EndPos = endPos;

            //-------------------- Calculate number of points --------------------------

            int numPts = (int)(moveTime * this.InterpolationFrequency); //add 1 so last position gets calculated

            //----------------- Interpolate to get each intermediate position ------------

            List<double> output = new List<double>();

            for(int i = 0; i < numPts; i++)
            {
                output.Add(this.StartPos + (1.0 * i / (numPts-1)) * (this.EndPos - this.StartPos)); //linearly interpolate
            }
            return output.ToArray();
        }

    } //currently not finished, just interpolates between start and end
}
