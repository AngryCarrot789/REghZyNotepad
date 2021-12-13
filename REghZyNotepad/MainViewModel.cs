using System.Windows;
using System.Windows.Input;
using DragonJetzMVVM.ViewModels.Base;
using DragonJetzNotepad.Core;

namespace DragonJetzNotepad {
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

        public ICommand NewFileCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveFileAsCommand { get; }
        public ICommand ClearDocumentCommand { get; }
        public ICommand ExitCommand { get; }

        public ICommand FindCommand { get; }
        public ICommand FindNextCommand { get; }
        public ICommand FindPreviousCommand { get; }
        public ICommand ReplaceCommand { get; }
        public ICommand ReplaceAllCommand { get; }
        public ICommand GotoLineCommand { get; }

        public ICommand ShowFormatCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand SwitchShowBottomBarCommand { get; }
        public ICommand SwitchLineOutlineCommand { get; }

        public MainViewModel() {
            this.OpenFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.OpenDocumentWithDialog());
            this.SaveFileCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.SaveDocumentAuto());
            this.SaveFileAsCommand = new Command(() => ViewModelLocator.Instance.Application.Notepad.SaveDocumentAsAuto());
            this.ClearDocumentCommand = new Command(() => {
                if (ViewModelLocator.Instance.Application.Notepad.Editor.Document.HasTextChangedSinceSave) {
                    if (ServiceLocator.Dialog.ShowConfirmable("Save changes?", "You have unsaved changes. Do you want to save them?", false)) {
                        ViewModelLocator.Instance.Application.Notepad.SaveDocumentAuto();
                    }
                }

                ViewModelLocator.Instance.Application.Notepad.ClearDocument();
            });
            this.NewFileCommand = this.ClearDocumentCommand; //new Command(() => ViewModelLocator.Instance.Application.CreateAndAddNewNotepad());
            this.ExitCommand = new Command(() => Application.Current.Shutdown());
            this.FindCommand = new Command(() => ServiceLocator.ViewProvider.OpenFindView(false));
            this.ReplaceCommand = new Command(() => ServiceLocator.ViewProvider.OpenFindView(true));
            this.ShowFormatCommand = new Command(() => ServiceLocator.ViewProvider.OpenFormatView());
            this.GotoLineCommand = new Command(() => ServiceLocator.ViewProvider.OpenGotoLineView());
            this.AboutCommand = new Command(() => ServiceLocator.ViewProvider.OpenAboutView());
            this.SwitchShowBottomBarCommand = new Command(() => this.ShowInfoBar = !this.ShowInfoBar);
            this.SwitchLineOutlineCommand = new Command(() => this.ShowOutlineBar = !this.ShowOutlineBar);
            this.ShowInfoBar = true;
            this.ShowOutlineBar = true;
        }
    }
}