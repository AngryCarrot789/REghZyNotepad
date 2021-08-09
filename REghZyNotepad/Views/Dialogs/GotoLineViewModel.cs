using REghZyFramework.Utilities;
using REghZyNotepad.Notepad;

namespace REghZyNotepad.Views.Dialogs {
    public class GotoLineViewModel : BaseViewModel {
        private int _targetLine;

        public NotepadEditorViewModel NotepadEditor { get; }

        public GotoLineViewModel(NotepadEditorViewModel notepadEditor) {
            this.NotepadEditor = notepadEditor;
        }

        public int TargetLine {
            get => this._targetLine;
            set {
                if (value < 0 || value > this.NotepadEditor.Document.GetTotalLines()) {
                    return;
                }

                RaisePropertyChanged(ref this._targetLine, value);
            }
        }

        public void GotoLine() {
            if (this.TargetLine == 0) {
                this.NotepadEditor.TextSelector.LineIndex = 0;
            }

            this.NotepadEditor.TextSelector.LineIndex = this.TargetLine - 1;
        }
    }
}
