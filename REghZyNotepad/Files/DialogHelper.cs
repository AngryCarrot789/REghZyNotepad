using System.IO;
using Microsoft.Win32;

namespace REghZyNotepad.Files {
    public static class DialogHelper {
        public static string OpenFile() {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Select a file to open"
            };

            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }

            return null;
        }

        public static string SaveFile() {
            SaveFileDialog dialog = new SaveFileDialog() {
                FileName = "new.txt",
                Filter = "All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }

            return null;
        }
    }
}