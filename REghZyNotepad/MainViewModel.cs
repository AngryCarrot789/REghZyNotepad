using System.Windows;
using REghZyMVVM.ViewModels.Base;
using REghZyNotepad.Core;

namespace REghZyNotepad {
    public class MainViewModel : BaseViewModel {
        private bool _showInfoBar;
        public bool ShowInfoBar {
            get => this._showInfoBar;
            set => RaisePropertyChanged(ref this._showInfoBar, value);
        }

        private bool _showOutlineBar;
        public bool ShowOutlineBar {
            get => _showOutlineBar;
            set => RaisePropertyChanged(ref this._showOutlineBar, value);
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
        public Command SwitchLineOutlineCommand { get; }

        public MainViewModel() {
            this.NewFileCommand = new Command(() => {
                if (ViewModelLocator.Instance.Application.Notepad.Editor.Document.HasTextChangedSinceSave) {
                    if (ServiceLocator.Dialog.ShowConfirmable("Save changes?", "You have unsaved changes. Do you want to save them?", false)) {
                        ViewModelLocator.Instance.Application.Notepad.SaveDocumentAuto();
                    }
                }

                ViewModelLocator.Instance.Application.Notepad.ClearDocument();
            });

            this.OpenFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.OpenDocumentWithDialog());
            this.SaveFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.SaveDocumentAuto());
            this.SaveFileAsCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.SaveDocumentAsAuto());
            this.ExitCommand = new Command(() => Application.Current.Shutdown());
            this.FindCommand = new Command(() => ServiceLocator.ViewProvider.OpenFindView(false));
            this.ReplaceCommand = new Command(() => ServiceLocator.ViewProvider.OpenFindView(true));
            this.ShowFormatCommand = new Command(() => ServiceLocator.ViewProvider.OpenFormatView());
            this.GotoLineCommand = new Command(() => ServiceLocator.ViewProvider.OpenGotoLineView());
            this.AboutCommand = new Command(() => ServiceLocator.ViewProvider.OpenAboutView());
            this.SwitchShowBottomBarCommand = new Command(() => { 
                this.ShowInfoBar = !this.ShowInfoBar; 
            });
            this.SwitchLineOutlineCommand = new Command(()=> { 
                this.ShowOutlineBar = !this.ShowOutlineBar; 
            });

            this.ShowInfoBar = true;
            this.ShowOutlineBar = true;
        }
    }
}