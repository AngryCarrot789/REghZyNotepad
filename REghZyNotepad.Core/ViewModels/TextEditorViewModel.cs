using System;
using System.IO;
using System.Text;
using REghZyMVVM.ViewModels.Base;
using REghZyNotepad.Core.Views;

namespace REghZyNotepad.Core.ViewModels {
    /// <summary>
    /// A ViewModel for the text editor area (including formats, contents, line number, etc)
    /// </summary>
    public class TextEditorViewModel : BaseViewModel {
        /// <summary>
        /// The document information, such as file path, file contents, etc
        /// </summary>
        public DocumentViewModel Document { get; }

        /// <summary>
        /// The formatting for this text editor
        /// </summary>
        public FormatViewModel Format { get; }

        private int _line;

        /// <summary>
        /// The zero-based line index that the caret it on
        /// </summary>
        public int Line {
            get => _line;
            set => RaisePropertyChanged(ref this._line, value);
        }

        private int _column;

        /// <summary>
        /// The zero-based column index that the caret it on
        /// </summary>
        public int Column {
            get => _column;
            set => RaisePropertyChanged(ref this._column, value);
        }

        public TextEditorViewModel() {
            this.Document = new DocumentViewModel();
            this.Format = new FormatViewModel();
        }

        /// <summary>
        /// Reads all the text from the given file and puts it into this notepad's document (and sets the file path)
        /// </summary>
        /// <param name="path">The path to the file to read from</param>
        /// <exception cref="NullReferenceException">If the given path is null</exception>
        /// <exception cref="FileNotFoundException">If the given path doesn't lead to a file</exception>
        public void OpenDocument(string path) {
            if (path == null) {
                throw new NullReferenceException("Path to open is null");
            }

            if (!File.Exists(path)) {
                throw new FileNotFoundException($"'{path}' was not found");
            }

            long fileSize = new FileInfo(path).Length;
            if (fileSize > int.MaxValue) {
                fileSize = int.MaxValue;
            }

            // 40mb
            if (fileSize > 41943040) {
                throw new Exceptions.InvalidDataException($"File is bigger than 41943040 bytes ({fileSize})");
            }

            StringBuilder contentBuilder = new StringBuilder((int) fileSize);
            foreach (string line in File.ReadLines(path)) {
                contentBuilder.AppendLine(line);
            }

            this.Document.FilePath = path;
            this.Document.Contents = contentBuilder.ToString();
            this.Document.HasTextChangedSinceSave = false;
        }

        /// <summary>
        /// Saves this notepad document to the given path, and sets the document's file path to it
        /// </summary>
        /// <param name="path">The path to save (path doesn't have to exist, but any file will be overritten)</param>
        /// <exception cref="NullReferenceException">If the given path is null</exception>
        public void SaveDocumentTo(string path) {
            if (path == null) {
                throw new NullReferenceException("The path to save to cannot be null");
            }

            this.Document.FilePath = path;
            SaveDocument();
        }

        /// <summary>
        /// Saves this notepad document to the internal path (by overriting the entire file)
        /// </summary>
        /// <exception cref="NullReferenceException">If this document's file path is null</exception>
        public void SaveDocument() {
            string path = this.Document.FilePath;
            if (path == null) {
                throw new NullReferenceException("This document didn't have a path");
            }

            StreamWriter writer = new StreamWriter(path, false, Encoding.Unicode, (int) (this.Document.Length + 1));
            writer.Write(this.Document.Contents);
            writer.Flush();
            writer.Close();
            writer.Dispose();
            this.Document.HasTextChangedSinceSave = false;
        }

        /// <summary>
        /// Updates the <see cref="Line"/> and <see cref="Column"/> for the Text Editor
        /// </summary>
        public void UpdateCaret() {
            ITextEditor editor = ServiceLocator.TextEditor;
            this.Line = editor.LineIndex;
            this.Column = editor.ColumnIndex;
        }
    }
}