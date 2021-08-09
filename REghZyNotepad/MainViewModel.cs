using System;
using System.IO;
using System.Windows;
using REghZyFramework.Utilities;
using REghZyNotepad.Files;
using REghZyNotepad.Notepad;

namespace REghZyNotepad {
    public class MainViewModel : BaseViewModel {
        public NotepadBarViewModel NotepadBar { get; }

        private NotepadEditorViewModel _notepadEditor;
        public NotepadEditorViewModel NotepadEditor {
            get => this._notepadEditor;
            set => RaisePropertyChanged(ref this._notepadEditor, value);
        }

        public ITextSelectable TextSelector { get; }

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

        public MainViewModel(ITextSelectable textSelector) {
            this.TextSelector = textSelector;
            this.NotepadEditor = new NotepadEditorViewModel();
            this.NotepadBar = new NotepadBarViewModel(this.NotepadEditor);
            this.NewFileCommand = new Command(ClearDocument);
            this.OpenFileCommand = new Command(OpenDocumentWithDialog);
            this.SaveFileCommand = new Command(SaveDocumentAuto);
            this.SaveFileAsCommand = new Command(SaveDocumentAsAuto);
            this.ExitCommand = new Command(() => { Application.Current.Shutdown(); });
        }

        public void SaveDocumentAuto() {
            string path = this.NotepadEditor.Document.FilePath;
            if (path == null) {
                SaveDocumentAsAuto();
            }
            else if (!File.Exists(path)) {
                SaveDocumentAsAuto();
            }
            else {
                this.NotepadEditor.SaveDocument();
            }

            UpdateTitle();
        }

        public void SaveDocumentAsAuto() {
            string path = DialogHelper.SaveFile();
            if (path == null) {
                return;
            }

            this.NotepadEditor.SaveDocumentTo(path);
        }

        public void ClearDocument() {
            this.NotepadEditor = new NotepadEditorViewModel();
        }

        public void OpenDocumentWithDialog() {
            OpenDocumentWithDialog(true);
        }

        public void OpenDocumentWithDialog(bool keepFormat) {
            string path = DialogHelper.OpenFile();
            if (path == null) {
                return;
            }

            // assuming that opening a file, the path will always exist
            if (keepFormat) {
                this.NotepadEditor.OpenDocument(path);
            }
            else {
                ClearDocument();
                this.NotepadEditor.OpenDocument(path);
            }
        }

        public void UpdateBar() {
            this.NotepadBar.Column = this.TextSelector.ColumnIndex;
            this.NotepadBar.Line = this.TextSelector.LineIndex;
        }

        public void UpdateTitle() {
            this.NotepadEditor.Document.FilePath = this.NotepadEditor.Document.FilePath;
        }
    }
}