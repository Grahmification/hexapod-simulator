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
        public Transform3D Transform => new TranslateTransform3D(BallModel.Position[0], BallModel.Position[1], BallModel.Position[2]);

        /// <summary>
        /// The radius of the ball in [mm]
        /// </summary>
        public double Radius => BallModel.Radius*1000;

        /// <summary>
        /// the model class for this VM
        /// </summary>
        public IBall BallModel { get; private set; }

        /// <summary>
        /// default constructor
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BallVM()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeModel(new BallModelLocal(0.005, 9800, new double[] { 0, 0, 0 }));
        }

        /// <summary>
        /// Allows setting a predefined ball model
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BallVM(IBall ballModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
            BallModel.RedrawRequired += OnRedrawRequired;
        }

        /// <summary>
        /// Fires when the ball model indicates redrawing is needed
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">Event args</param>
        private void OnRedrawRequired(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Transform));
        }
    }
}
