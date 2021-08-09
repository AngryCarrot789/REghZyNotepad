using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace REghZyNotepad.Converters {
    // values[0] = bool (has content changed)
    // values[1] = string (file path)
    public class WindowTitleConverter : IMultiValueConverter {
        public const string WINDOW_TITLE = "REghZy Notepad";

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values[1] == null) {
                if (values[0] is bool hasChanged && !hasChanged) {
                    return WINDOW_TITLE;
                }

                throw new InvalidDataException("Element 0 of 'values' wasn't a boolean, or was false, but the 2nd value (file path) was null");
            }

            if (values[1] is string filePath) {
                if (values[0] is bool hasChanged) {
                    if (hasChanged) {
                        return $"{WINDOW_TITLE} - {filePath} *";
                    }
                    else {
                        return $"{WINDOW_TITLE} - {filePath}";
                    }
                }
                else {
                    throw new InvalidDataException($"Element 0 of 'values' was not a boolean (It was {values[1]})");
                }
            }
            else {
                throw new InvalidDataException($"Element 1 of 'values' was not a string (It was {values[0]})");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
