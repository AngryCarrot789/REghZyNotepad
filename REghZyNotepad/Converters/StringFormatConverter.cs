using System;
using System.Globalization;
using System.Windows.Data;

namespace DragonJetzNotepad.Converters {
    [ValueConversion(typeof(string), typeof(string))]
    public class StringFormatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string format;
            if (parameter == null) {
                return value == null ? "" : value.ToString();
            }
            else {
                format = parameter.ToString();
            }

            return format.Replace("%", value == null ? "" : value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
