using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace REghZyNotepad.Views {
    /// <summary>
    /// Interaction logic for FormatWindow.xaml
    /// </summary>
    public partial class FormatWindow : Window {
        public FormatWindow() {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                this.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
        }
    }
}
