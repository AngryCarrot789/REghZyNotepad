using System;
using System.Globalization;
using System.Windows.Data;
using DragonJetzNotepad.Core.Exceptions;

namespace DragonJetzNotepad.Converters {
    public class BoolToOpacityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double falseVal = 0.0d;
            double trueVal = 1.0d;
            if (parameter is string str) {
                int split = str.IndexOf('|');
                if (split != -1) {
                    falseVal = double.Parse(str.Substring(0, split));
                    trueVal = double.Parse(str.Substring(split + 1));
                }
            }

            if (value is bool val) {
                return val ? trueVal : falseVal;
            }

            throw new InvalidDataException("Given value was not a boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            double falseVal = 0.0d;
            double trueVal = 1.0d;
            if (parameter is string str) {
                int split = str.IndexOf('|');
                if (split != -1) {
                    falseVal = double.Parse(str.Substring(0, split));
                    trueVal = double.Parse(str.Substring(split + 1));
                }
            }

            if (value is double opacity) {
                return opacity == trueVal ? true : false;
            }

            throw new InvalidDataException("Given value was not a double");
        }
    }
}
