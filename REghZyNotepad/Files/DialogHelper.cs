using Microsoft.Win32;

namespace REghZyNotepad.Files {
    public static class DialogHelper {
        private const string FILTERS =
            "All files|*.*|" +
            "Plain Text (.txt)|*.txt|" +
            "Text..? (.text)|*.text|" +
            "C# File (.cs)|*.cs|" +
            "C File (.c)|*.c|" +
            "C++ File (.cpp)|*.cpp|" +
            "C/C++ Header File (.h)|*.h|" +
            "XAML File (.xaml)|*.xaml|" +
            "XML File (.xml)|*.xml|" +
            "HTM File (.htm)|*.htm|" +
            "HTML File (.html)|*.html|" +
            "CSS File (.css)|*.css|" +
            "JS File (.js)|*.js";

        public static string OpenFile() {
            OpenFileDialog dialog = new OpenFileDialog {
                Title = "Select a file to open",
                Filter = FILTERS,
                FilterIndex = 0
            };

            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }

            return null;
        }

        public static string SaveFile() {
            SaveFileDialog dialog = new SaveFileDialog() {
                Title = "Select a file to save",
                FileName = "new.txt",
                Filter = FILTERS,
                FilterIndex = 0
            };

            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }

            return null;
        }
    }
}