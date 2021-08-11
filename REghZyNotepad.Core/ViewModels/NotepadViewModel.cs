using System;
using System.IO;
using REghZyNotepad.Core.ViewModels.Base;

namespace REghZyNotepad.Core.ViewModels {
    /// <summary>
    /// A ViewModel for an entire Notepad view
    /// </summary>
    public class NotepadViewModel : BaseViewModel {
        private TextEditorViewModel _editor;

        /// <summary>
        /// The text editor view model, that holds the document and formats
        /// </summary>
        public TextEditorViewModel Editor {
            get => this._editor;
            set {
                RaisePropertyChanged(ref this._editor, value);
            }
        }

        public NotepadViewModel() {
            ClearDocument();
        }

        /// <summary>
        /// Saves this document. If this document has no file path or doesn't exist, it will call <see cref="SaveDocumentAsAuto"/>
        /// </summary>
        public void SaveDocumentAuto() {
            string path = this.Editor.Document.FilePath;
            if (path == null || !File.Exists(path)) {
                SaveDocumentAsAuto();
            }
            else {
                this.Editor.SaveDocument();
                UpdateTitle();
            }
        }

        /// <summary>
        /// Opens a dialog to let the user select a location to save this document
        /// </summary>
        public void SaveDocumentAsAuto() {
            string path = ServiceLocator.SaveOpen.SaveFile();
            if (path == null) {
                return;
            }

            if (!Path.HasExtension(path)) {
                path += ".txt";
            }

            this.Editor.SaveDocumentTo(path);
            UpdateTitle();
        }

        /// <summary>
        /// Clears this document (including text, file path, formatting, etc)
        /// </summary>
        public void ClearDocument() {
            this.Editor = new TextEditorViewModel();
        }

        /// <summary>
        /// Opens a file with a dialog, and clears the previous document (including formatting)
        /// </summary>
        public void OpenDocumentWithDialog() {
            OpenDocumentWithDialog(true);
        }

        /// <summary>
        /// Opens a dialog for selecting a file to open
        /// </summary>
        /// <param name="keepFormat">Whether to keep this notepad's formatting the same, or reset it</param>
        public void OpenDocumentWithDialog(bool keepFormat) {
            string path = ServiceLocator.SaveOpen.OpenFile();
            if (path == null) {
                return;
            }

            // a user might try and lighting fast delete a file just as they open it :)
            if (!File.Exists(path)) {
                ServiceLocator.Dialog.Show($"The file ({path}) doesn't exist!", "Failed to open file");
                return;
            }
            try {
                if (keepFormat) {
                    this.Editor.OpenDocument(path);
                }
                else {
                    ClearDocument();
                    this.Editor.OpenDocument(path);
                }
            }
            catch (Exception e) {
                ServiceLocator.Dialog.Show($"Cannot open file: {e.Message}", "Failed to open file");
            }
        }

        public void OpenDocument(string path, bool keepFormat = true) {
            // a user might try and lighting fast delete a file just as they open it :)
            if (!File.Exists(path)) {
                ServiceLocator.Dialog.Show($"The file ({path}) doesn't exist!", "Failed to open file");
                return;
            }
            try {
                if (keepFormat) {
                    this.Editor.OpenDocument(path);
                }
                else {
                    ClearDocument();
                    this.Editor.OpenDocument(path);
                }
            }
            catch (Exception e) {
                ServiceLocator.Dialog.Show($"Cannot open file: {e.Message}", "Failed to open file");
            }
        }

        /// <summary>
        /// Triggers the title of the view to update by """changing""" the File Path that it should be bound to
        /// </summary>
        public void UpdateTitle() {
            this.Editor.Document.FilePath = this.Editor.Document.FilePath;
        }
    }
}
