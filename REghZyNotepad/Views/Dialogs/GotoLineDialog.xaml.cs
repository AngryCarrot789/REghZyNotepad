using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using REghZyFramework.Utilities;
using REghZyNotepad.Notepad;

namespace REghZyNotepad.Views.Dialogs {
    /// <summary>
    /// Interaction logic for GotoLineDialog.xaml
    /// </summary>
    public partial class GotoLineDialog : Window, BaseView<GotoLineViewModel> {
        public GotoLineViewModel Model {
            get => (GotoLineViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public GotoLineDialog(NotepadEditorViewModel editor) {
            InitializeComponent();
            this.Model = new GotoLineViewModel(editor);
            Task.Run(async() => {
                await Task.Delay(50);
                Application.Current.Dispatcher.Invoke(() => {
                    this.LineBox.Focus();
                    this.LineBox.SelectAll();
                });
            });
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Key == Key.Escape || e.Key == Key.Enter) {
                this.Close();
                return;
            }

            base.OnKeyDown(e);
        }

        protected override void OnClosing(CancelEventArgs e) {
            this.Model.GotoLine();
            base.OnClosing(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
