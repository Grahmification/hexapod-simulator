using System.Windows.Media.Media3D;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.Helix.ViewModels
{
    /// <summary>
    /// Viewmodel for a <see cref="Ball"></see> class/>
    /// </summary>
    public class BallVM : BaseViewModel
    {
        /// <summary>
        /// The absolute transformation of the ball
        /// </summary>
        public Transform3D Transform { get { return new TranslateTransform3D(BallModel.Position[0], BallModel.Position[1], BallModel.Position[2]); } }

        /// <summary>
        /// The radius of the ball in [mm]
        /// </summary>
        public double Radius { get { return BallModel.Radius*1000; } }

        /// <summary>
        /// the model class for this VM
        /// </summary>
        public IBall BallModel { get; private set; } 

        /// <summary>
        /// default constructor
        /// </summary>
        public BallVM()
        {
            InitializeModel(new Ball_Local_Test(0.005, 9800, new double[] { 0, 0, 0 }));
        }

        /// <summary>
        /// Allows setting a predefined ball model
        /// </summary>
        public BallVM(IBall ballModel)
        {
            InitializeModel(ballModel);
        }

        /// <summary>
        /// Properly sets up the model within the class
        /// </summary>
        /// <param name="ballModel">The ball model</param>
        private void InitializeModel(IBall ballModel)
        {
            BallModel = ballModel;
            BallModel.RedrawRequired += onRedrawRequired;
        }

        /// <summary>
        /// Fires when the ball model indicates redrawing is needed
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">Event args</param>
        private void onRedrawRequired(object sender, EventArgs e)
        {
            OnPropertyChanged("Transform");
        }
    }
}
