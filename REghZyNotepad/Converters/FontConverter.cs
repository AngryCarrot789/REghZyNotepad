using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using REghZyNotepad.Core.Exceptions;

namespace REghZyNotepad.Converters {
    public class FontConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is FontFamily font) {
                return font.ToString();
            }
            else if (value is string fontName) {
                if (string.IsNullOrEmpty(fontName)) {
                    return null;
                }

                return new FontFamily(fontName);
            }
            else {
                throw new InvalidDataException($"(Convert) The provided value ({value}) wasn't a string (font name) or a FontFamily (font)");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is FontFamily font) {
                return font.ToString();
            }
            else if (value is string fontName) {
                if (string.IsNullOrEmpty(fontName)) {
                    return null;
                }

                return new FontFamily(fontName);
            }
            else {
                throw new InvalidDataException($"(Convert Back) The provided value ({value}) wasn't a string (font name) or a FontFamily (font)");
            }
        }
    }
}
