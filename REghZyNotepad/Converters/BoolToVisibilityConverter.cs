using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZyMVVM.Utils;
using REghZyNotepad.Core.Exceptions;

namespace REghZyNotepad.Converters {
    public class BoolToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool visible;
            if (value is bool b) {
                visible = b;
            }
            else {
                throw new InvalidDataException("Value was not a boolean");
            }

            bool invert = false;
            bool hide = false;
            if (parameter is string param) {
                int splitIndex = param.IndexOf('|');
                if (splitIndex == -1) {
                    invert = param == "Invert";
                    if (!invert) {
                        hide = param == "Hide";
                    }
                }
                else {
                    invert = param.JSubstring(0, splitIndex) == "Invert";
                    hide = param.JSubstring(splitIndex + 1) == "Hide";
                }
            }
            if (visible) {
                if (invert) {
                    return hide ? Visibility.Hidden : Visibility.Collapsed;
                }
                else {
                    return Visibility.Visible;
                }
            }
            else {
                if (invert) {
                    return Visibility.Visible;
                }
                else {
                    return hide ? Visibility.Hidden : Visibility.Collapsed;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
