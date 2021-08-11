using REghZyNotepad.Core.ViewModels;
using REghZyNotepad.Core.ViewModels.Base;

namespace REghZyNotepad.Core {
    public class ApplicationViewModel : BaseViewModel {
        private NotepadViewModel _notepad;
        public NotepadViewModel Notepad {
            get => this._notepad;
            set => RaisePropertyChanged(ref this._notepad, value);
        }

        public ApplicationViewModel() {
            this.Notepad = new NotepadViewModel();
        }
    }
}
