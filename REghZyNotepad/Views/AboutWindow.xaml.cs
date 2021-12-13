using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DragonJetzNotepad.Views {
    public partial class AboutWindow : Window {
        public AboutWindow() {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Key == Key.Escape || e.Key == Key.Enter) {
                this.Close();
                return;
            }

            base.OnKeyDown(e);
        }

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            this.Hide();
        }
    }
}