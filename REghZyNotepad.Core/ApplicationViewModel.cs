using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DragonJetzMVVM.ViewModels.Base;
using DragonJetzNotepad.Core.ViewModels;

namespace DragonJetzNotepad.Core {
    public class ApplicationViewModel : BaseViewModel {
        public ObservableCollection<NotepadViewModel> Notepads { get; }

        private NotepadViewModel _notepad;
        public NotepadViewModel Notepad {
            get => this._notepad;
            set {
                IsNotepadAvailable = value != null;
                RaisePropertyChanged(ref this._notepad, value);
            }
        }

        private bool _isNotepadAvailable;
        public bool IsNotepadAvailable {
            get => this._isNotepadAvailable;
            set => RaisePropertyChanged(ref this._isNotepadAvailable, value);
        }

        public CommandParam<NotepadViewModel> CloseNotepadCommand { get; }

        public ApplicationViewModel() {
            this.Notepads = new ObservableCollection<NotepadViewModel>();
            NotepadViewModel notepad = CreateNotepad();
            AddNotepad(notepad);
            SelectNotepad(notepad);

            this.CloseNotepadCommand = new CommandParam<NotepadViewModel>(CloseNotepad);
        }

        public void CreateAndAddNewNotepad() {
            AddNotepad(CreateNotepad());
        }

        public NotepadViewModel CreateNotepad() {
            return new NotepadViewModel();
        }

        public void AddNotepad(NotepadViewModel notepad) {
            this.Notepads.Add(notepad);
        }

        public bool RemoveNotepad(NotepadViewModel notepad) {
            return this.Notepads.Remove(notepad);
        }

        public void RemoveNotepadAt(int index) {
            this.Notepads.RemoveAt(index);
        }

        public int IndexOfNotepad(NotepadViewModel notepad) {
            return this.Notepads.IndexOf(notepad);
        }

        public void SelectNotepad(NotepadViewModel notepad) {
            if (this.Notepads.Contains(notepad)) {
                this.Notepad = notepad;
            }
            else {
                throw new Exception("That notepad was not within the notepad collection");
            }
        }

        public void FastSelectNotepad(NotepadViewModel notepad) {
            this.Notepad = notepad;
        }

        public void CloseNotepad(NotepadViewModel notepad) {
            int index = this.IndexOfNotepad(notepad);
            if (index == -1) {
                throw new Exception("That notepad was not contained within the notepad collection");
            }

            RemoveNotepadAt(index);
            if (index == 0) {
                if (this.Notepads.Count > 0) {
                    FastSelectNotepad(this.Notepads[0]);
                }
                else {
                    FastSelectNotepad(null);
                }
            }
            else {
                if (index >= this.Notepads.Count) {
                    index = this.Notepads.Count - 1;
                }

                FastSelectNotepad(this.Notepads[index]);
            }
        }
    }
}
