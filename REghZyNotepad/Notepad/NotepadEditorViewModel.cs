using System;
using System.IO;
using System.Text;
using REghZyFramework.Utilities;

namespace REghZyNotepad.Notepad {
    public class NotepadEditorViewModel : BaseViewModel {
        public DocumentViewModel Document { get; }
        public FormatViewModel Format { get; }

        public NotepadEditorViewModel() {
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
                throw new InvalidDataException($"File is bigger than 41943040 bytes ({fileSize})");
            }

            StringBuilder contentBuilder = new StringBuilder((int) fileSize);
            foreach (string line in File.ReadLines(path)) {
                contentBuilder.AppendLine(line);
            }

            this.Document.FilePath = path;
            this.Document.Contents = contentBuilder.ToString();
            this.Document.HasTextChangedSinceSave = false;
        }

        public void SaveDocumentTo(string path) {
            this.Document.FilePath = path;
            SaveDocument();
        }

        public void SaveDocument() {
            string path = this.Document.FilePath;
            if (path == null) {
                throw new NullReferenceException("This document didn't have a path");
            }

            StreamWriter writer = new StreamWriter(path, false, Encoding.Unicode, (int) (this.Document.FileSize + 1));
            writer.Write(this.Document.Contents);
            writer.Flush();
            writer.Close();
            writer.Dispose();
            this.Document.HasTextChangedSinceSave = false;
        }
    }
}