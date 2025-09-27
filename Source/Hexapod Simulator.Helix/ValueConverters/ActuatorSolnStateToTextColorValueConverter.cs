using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Hexapod_Simulator.Helix.ValueConverters
{
    /// <summary>
    /// Converts the actuator solution state to the background color for textboxes
    /// </summary>
    public class ActuatorSolnStateToTextColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (bool)value;

            if (tmp)
                //light gray button color
                return SystemColors.ControlLightBrush;
            else
                return Brushes.IndianRed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
