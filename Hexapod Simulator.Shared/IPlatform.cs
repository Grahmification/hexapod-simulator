using System;

namespace Hexapod_Simulator.Shared
{
    public interface IPlatform
    {
        double[][] LocalJointCoords { get;  } //XYZ pos [mm] of each joint, without trans/rotation 
        double[][] GlobalJointCoords { get; } //XYZ pos [mm] of each joint

        double Radius { get; set; }
    
        double JointAngle { get; set; }

        double[] DefaultPos { get; }

        double[] Translation { get; }
 
        double[] Rotation { get; }

        double[] RotationCenter { get; }

        double[] Position { get; }
      
        double[] AbsRotationCenter { get; }
     
        double[] NormalVector { get; }

        bool FixedRotationCenter { get; }

        string Name { get; set;  }

        event EventHandler RedrawRequired;
        event EventHandler PositionChanged; //rotation or translation has changed

        void TranslateAbs(double[] Pos);
        void TranslateRel(double[] Pos);
        void RotateAbs(double[] Rot);
        void UpdateConfig(double radius, double jointAngle, double[] defaultPos);
        void UpdateRotationCenter(double[] Position, bool FixedPosition);

        //---------------------------- Servo Stuff ---------------------------------
        void RotateAbsServo(double[] Rot);
        void TranslateAbsServo(double[] Pos);
        void CalculateTimeStep(double TimeStep);

        //---------------- Geometry Calculation functions -----------------    

        double[] CalcGlobalCoord(double[] localcoord);
   
    }
}
