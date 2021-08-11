using System.Windows;
using Microsoft.Win32;
using REghZyNotepad.Core.Views;

namespace REghZyNotepad.Dialogs {
    public class WindowsDialogService : ISaveOpenService, IDialogMessage {
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

        public string OpenFile() {
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

        public string SaveFile() {
            SaveFileDialog dialog = new SaveFileDialog() {
                Title = "Select a file to save",
                FileName = "Untitled.txt",
                Filter = FILTERS,
                FilterIndex = 0
            };

            if (dialog.ShowDialog() == true) {
                return dialog.FileName;
            }

            return null;
        }

        public void Show(string caption, string message) {
            MessageBox.Show(message, caption);
        }

        public bool ShowConfirmable(string caption, string message, bool defaultResult = false) {
            return MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Information, defaultResult ? MessageBoxResult.Yes : MessageBoxResult.No) == MessageBoxResult.Yes;
        }
    }
}
