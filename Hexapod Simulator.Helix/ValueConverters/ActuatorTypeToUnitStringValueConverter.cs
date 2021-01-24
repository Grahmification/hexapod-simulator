using Hexapod_Simulator.Shared;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Hexapod_Simulator.Helix.ValueConverters
{
    /// <summary>
    /// Converts an actuator type to the display units string to be shown in the view
    /// </summary>
    public class ActuatorTypeToUnitStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (ActuatorTypes)value;

            switch (tmp)
            {
                case ActuatorTypes.Linear:
                    return "mm";
                case ActuatorTypes.Rotary:
                    return "deg";
                default:
                    return "-";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
