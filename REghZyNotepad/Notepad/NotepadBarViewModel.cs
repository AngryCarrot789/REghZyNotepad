using REghZyFramework.Utilities;

namespace REghZyNotepad.Notepad {
    public class NotepadBarViewModel : BaseViewModel {
        private NotepadEditorViewModel _notepadEditor;
        public NotepadEditorViewModel NotepadEditor {
            get => _notepadEditor;
            set => RaisePropertyChanged(ref this._notepadEditor, value);
        }

        private int _line;
        public int Line {
            get => _line;
            set => RaisePropertyChanged(ref this._line, value);
        }

        private int _column;
        public int Column {
            get => _column;
            set => RaisePropertyChanged(ref this._column, value);
        }

        public NotepadBarViewModel(NotepadEditorViewModel notepadEditor) {
            this.NotepadEditor = notepadEditor;
        }
    }
}