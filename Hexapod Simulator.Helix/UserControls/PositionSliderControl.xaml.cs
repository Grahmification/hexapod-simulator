using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hexapod_Simulator.Helix.UserControls
{
    /// <summary>
    /// Interaction logic for PositionSliderControl.xaml
    /// </summary>
    public partial class PositionSliderControl : UserControl
    {
        public static readonly DependencyProperty PositionProperty
        = DependencyProperty.Register(
          "Position",
          typeof(double),
          typeof(PositionSliderControl),
          new PropertyMetadata(0.0, PositionChanged)
      );


        public double Position
        {
            get { return (double)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }


        public PositionSliderControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called by a dependency property when the position gets changed.
        /// </summary>
        /// <param name="d">The object</param>
        /// <param name="e">The args.</param>
        protected static void PositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PositionSliderControl)d).OnPositionChanged();
        }


        /// <summary>
        /// Called when the geometry has changed.
        /// </summary>
        protected virtual void OnPositionChanged()
        {
            Slider.Value = Position;
            PositioLabel.Content = Position.ToString();
        }

    }
}
