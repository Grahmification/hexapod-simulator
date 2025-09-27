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
        public Transform3D Transform => new TranslateTransform3D(BallModel.Position.X, BallModel.Position.Y, BallModel.Position.Z);

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
        public BallVM()
        {
            BallModel = new BallModelLocal(0.005, 9800, new(0, 0, 0));
            BallModel.RedrawRequired += OnRedrawRequired;
        }

        /// <summary>
        /// Allows setting a predefined ball model
        /// </summary>
        public BallVM(IBall ballModel)
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
