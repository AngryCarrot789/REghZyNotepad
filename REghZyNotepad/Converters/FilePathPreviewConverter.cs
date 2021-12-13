using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DragonJetzNotepad.Core.Exceptions;

namespace DragonJetzNotepad.Converters {
    public class FilePathPreviewConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return "Save or Open a file to display the path";
            }
            else if (value is string path) {
                return value;
            }
            else {
                throw new InvalidDataException($"Provided value was not a string ({value.GetType().Name})");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            // if (value == null) {
            //     throw new NullReferenceException("Value was null");
            // }
            // else if (value is string text) {
            //     if (text != "Save or Open a file to display the path") {
            //         return "Save or Open a file to display the path";
            //     }
            // }
            // else {
            //     throw new InvalidDataException($"Provided value was not a string ({value.GetType().Name})");
            // }

            return DependencyProperty.UnsetValue;
        }
    }
}
