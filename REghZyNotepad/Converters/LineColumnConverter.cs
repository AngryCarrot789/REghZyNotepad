using System;
using System.Globalization;
using System.Windows.Data;
using REghZyNotepad.Notepad;

namespace REghZyNotepad.Converters {
    public class LineColumnConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ITextSelectable textSelector = (ITextSelectable) value;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}