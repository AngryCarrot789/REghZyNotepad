using System.Windows.Input;
using REghZyMVVM.ViewModels.Base;
using REghZyNotepad.Core;

namespace REghZyNotepad.Views.Dialogs {
    public class GotoLineViewModel : BaseViewModel {
        private int _targetLine;
        public int TargetLine {
            get => this._targetLine;
            set {
                if (value < 0) {
                    value = 0;
                }
                else {
                    int totalLines = ViewModelLocator.Instance.GetCurrentTextEditor().Document.GetTotalLines();
                    if (value > totalLines) {
                        value = totalLines;
                    }
                }

                RaisePropertyChanged(ref this._targetLine, value);
            }
        }

        public ICommand GoToCommand { get; }

        public GotoLineViewModel() {
            this.GoToCommand = new Command(GotoLine);
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
