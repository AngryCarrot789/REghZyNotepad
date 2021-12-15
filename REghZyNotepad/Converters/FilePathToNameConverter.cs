using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace REghZyNotepad.Converters {
    public class FilePathToNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return DependencyProperty.UnsetValue;
            }
            else if (value is string path) {
                return Path.GetFileName(path);
            }

            throw new InvalidDataException("Value was not a string!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
