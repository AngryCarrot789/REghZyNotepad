using System;
using System.Globalization;
using System.Windows.Data;
using DragonJetzNotepad.Core.Exceptions;

namespace DragonJetzNotepad.Converters {
    // values[0] = bool (has content changed)
    // values[1] = string (file path)
    public class WindowTitleConverter : IMultiValueConverter {
        public const string WINDOW_TITLE = "DragonJetz Notepad";
        public const string WINDOW_TITLE_UNTITLED = (WINDOW_TITLE + " - Untitled");

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values[1] == null) {
                if (values[0] is bool hasChanged) {
                    if (hasChanged) {
                        return WINDOW_TITLE_UNTITLED;
                    }
                    else {
                        return WINDOW_TITLE;
                    }
                }
                else {
                    throw new InvalidDataException("Element 0 of 'values' wasn't a boolean");
                }
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
