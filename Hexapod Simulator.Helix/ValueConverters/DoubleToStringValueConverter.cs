using System;
using System.Globalization;
using System.Windows.Data;

namespace Hexapod_Simulator.Helix.ValueConverters
{
    public class DoubleToStringValueConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (double)value;
            return tmp.ToString();           
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 0;
            return double.TryParse((string)value, out ret) ? ret : 0;
        }
    }
}
