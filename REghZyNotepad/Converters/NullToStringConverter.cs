using System;
using System.Globalization;
using System.Windows.Data;

namespace REghZyNotepad.Converters {
    public class NullToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? parameter : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
