using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DragonJetzMVVM.Views;
using DragonJetzNotepad.Core.ViewModels;
using DragonJetzNotepad.Core.Views;

namespace DragonJetzNotepad.Views.Dialogs {
    /// <summary>
    /// Interaction logic for GotoLineDialog.xaml
    /// </summary>
    public partial class GotoLineDialog : Window, BaseView<GotoLineViewModel> {
        public GotoLineViewModel Model {
            get => (GotoLineViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public GotoLineDialog() {
            InitializeComponent();
            this.Model = new GotoLineViewModel();
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
                this.Model.GotoLine();
                return;
            }
            else {
                base.OnKeyDown(e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
            this.Model.GotoLine();
        }
    }
}
