using System;
using System.Globalization;
using System.Windows.Data;
using REghZyNotepad.Core.Exceptions;

namespace REghZyNotepad.Converters {
    public class NumberAddConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter == null) {
                parameter = 1;
            }

            if (value is int i) {
                return i + int.Parse(parameter.ToString());
            }
            else if (value is long l) {
                return l + long.Parse(parameter.ToString());
            }
            else if (value is double d) {
                return d + double.Parse(parameter.ToString());
            }
            else if (value is float f) {
                return f + float.Parse(parameter.ToString());
            }
            else {
                throw new InvalidDataException($"Value is not a number ({value})");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter == null) {
                parameter = 1;
            }

            if (value is int i) {
                return i - int.Parse(parameter.ToString());
            }
            else if (value is long l) {
                return l - long.Parse(parameter.ToString());
            }
            else if (value is double d) {
                return d - double.Parse(parameter.ToString());
            }
            else if (value is float f) {
                return f - float.Parse(parameter.ToString());
            }
            else {
                throw new InvalidDataException($"Value is not a number ({value})");
            }
        }
    }
}
