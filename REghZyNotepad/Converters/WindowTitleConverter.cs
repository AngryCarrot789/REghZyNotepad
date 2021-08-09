using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace REghZyNotepad.Converters {
    public class WindowTitleConverter : IValueConverter {
        public const string TITLE = "REghZy Notepad";

        public object Convert(object filePath, Type targetType, object parameter, CultureInfo culture) {
            if (filePath == null) {
                return TITLE;
            }
            else {
                return $"{TITLE} - {filePath}";
            }
        }

        public object ConvertBack(object fullTitle, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
