using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZyNotepad.Core.Exceptions;

namespace REghZyNotepad.Converters {
    public class TextWrappingConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool isWrapping) {
                return isWrapping ? TextWrapping.Wrap : TextWrapping.NoWrap;
            }
            else if (value is TextWrapping wrapping) {
                return wrapping == TextWrapping.Wrap;
            }

            throw new InvalidDataException($"(Converting) provided value was not a valid value ({value.GetType().Name})");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is TextWrapping wrapping) {
                return wrapping == TextWrapping.Wrap;
            }
            else if (value is bool isWrapping) {
                return isWrapping ? TextWrapping.Wrap : TextWrapping.NoWrap;
            }

            throw new InvalidDataException($"(Converting Back) provided value was not a valid value ({value.GetType().Name})");
        }
    }
}
