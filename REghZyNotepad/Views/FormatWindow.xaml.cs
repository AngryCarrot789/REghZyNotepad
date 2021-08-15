using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using REghZyMVVM.Views;
using REghZyNotepad.Core.ViewModels;

namespace REghZyNotepad.Views {
    /// <summary>
    /// Interaction logic for FormatWindow.xaml
    /// </summary>
    public partial class FormatWindow : Window, BaseView<TextEditorViewModel> {
        public TextEditorViewModel Model {
            get => (TextEditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public FormatWindow() {
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
