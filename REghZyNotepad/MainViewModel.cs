using System.Windows;
using REghZyNotepad.Core;
using REghZyNotepad.Core.ViewModels.Base;

namespace REghZyNotepad {
    public class MainViewModel : BaseViewModel {
        private bool _showInfoBar;
        public bool ShowInfoBar {
            get => this._showInfoBar;
            set => RaisePropertyChanged(ref this._showInfoBar, value);
        }

        public Command NewFileCommand { get; }
        public Command OpenFileCommand { get; }
        public Command SaveFileCommand { get; }
        public Command SaveFileAsCommand { get; }
        public Command ExitCommand { get; }

        public Command FindCommand { get; }
        public Command FindNextCommand { get; }
        public Command FindPreviousCommand { get; }
        public Command ReplaceCommand { get; }
        public Command ReplaceAllCommand { get; }
        public Command GotoLineCommand { get; }

        public Command ShowFormatCommand { get; }
        public Command AboutCommand { get; }
        public Command SwitchShowBottomBarCommand { get; }

        public MainViewModel() {
            this.NewFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.ClearDocument());
            this.OpenFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.OpenDocumentWithDialog());
            this.SaveFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.SaveDocumentAuto());
            this.SaveFileAsCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.SaveDocumentAsAuto());
            this.ExitCommand = new Command(() => Application.Current.Shutdown());
            this.FindCommand = new Command(() => ServiceLocator.ViewProvider.OpenFindView(false));
            this.ReplaceCommand = new Command(() => ServiceLocator.ViewProvider.OpenFindView(true));
            this.ShowFormatCommand = new Command(() => ServiceLocator.ViewProvider.OpenFormatView());
            this.GotoLineCommand = new Command(() => ServiceLocator.ViewProvider.OpenGotoLineView());
            this.AboutCommand = new Command(() => ServiceLocator.ViewProvider.OpenAboutView());
            this.SwitchShowBottomBarCommand = new Command(() => { this.ShowInfoBar = !this.ShowInfoBar; });
            this.ShowInfoBar = true;
        }
    }
}