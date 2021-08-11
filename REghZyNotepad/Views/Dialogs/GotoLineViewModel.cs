using REghZyNotepad.Core;
using REghZyNotepad.Core.ViewModels;
using REghZyNotepad.Core.ViewModels.Base;
using System.Windows.Input;

namespace REghZyNotepad.Views.Dialogs {
    public class GotoLineViewModel : BaseViewModel {
        private int _targetLine;

        public ICommand GoToCommand { get; }

        public GotoLineViewModel() {
            this.GoToCommand = new Command(GotoLine);
        }

        public int TargetLine {
            get => this._targetLine;
            set {
                if (value < 0 || value > ViewModelLocator.Instance.GetCurrentTextEditor().Document.GetTotalLines()) {
                    return;
                }

                RaisePropertyChanged(ref this._targetLine, value);
            }
        }

        public void GotoLine() {
            if (this.TargetLine == 0) {
                ServiceLocator.TextEditor.LineIndex = 0;
            }
            else {
                ServiceLocator.TextEditor.LineIndex = this.TargetLine - 1;
            }
        }
    }
}
