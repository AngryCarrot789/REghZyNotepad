using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using REghZyFramework.Utilities;
using REghZyNotepad.Notepad;

namespace REghZyNotepad.Views {
    /// <summary>
    /// Interaction logic for FormatWindow.xaml
    /// </summary>
    public partial class FormatWindow : Window, BaseView<NotepadEditorViewModel> {
        public NotepadEditorViewModel Model {
            get => (NotepadEditorViewModel) this.DataContext;
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
