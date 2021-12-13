using System;
using System.IO;
using DragonJetzMVVM.ViewModels.Base;

namespace DragonJetzNotepad.Core.ViewModels {
    public class DocumentViewModel : BaseViewModel {
        private string _contents;
        private string _filePath;
        private long _length;
        private long _wordCount;

        private bool _hasMadeChanges;
        /// <summary>
        /// Whether the text has changed since the document was last saved (will be false if you save, then true if you type anything)
        /// <para>
        /// This is mainly used to prompt saving when exiting, and updating the window title
        /// </para>
        /// </summary>
        public bool HasTextChangedSinceSave {
            get => this._hasMadeChanges;
            set {
                RaisePropertyChanged(ref this._hasMadeChanges, value);
                this.FilePath = this.FilePath;
            }
        }

        /// <summary>
        /// The raw document text/contents
        /// </summary>
        public string Contents {
            get => _contents;
            set {
                RaisePropertyChanged(ref this._contents, value);
                this.Length = value.Length;
                if (!this._hasMadeChanges) {
                    this.HasTextChangedSinceSave = true;
                }
            }
        }

        /// <summary>
        /// The path to the opened file, or null if it hasn't got one
        /// </summary>
        public string FilePath {
            get => _filePath;
            set {
                RaisePropertyChanged(ref this._filePath, value);
            }
        }

        /// <summary>
        /// How many characters there are in this document
        /// </summary>
        public long Length {
            get => _length;
            set {
                if (value > 41943040) {
                    throw new Exceptions.InvalidDataException("File size cannot exceed 41943040 bytes (40 MB)");
                }

                this.WordCount = CountWords();
                RaisePropertyChanged(ref this._length, value);
            }
        }

        public long WordCount {
            get => _wordCount;
            set => RaisePropertyChanged(ref this._wordCount, value);
        }

        /// <summary>
        /// Gets the name of the file path, or null if there isn't one
        /// </summary>
        public string GetFileName() {
            return Path.GetFileName(this.FilePath);
        }

        /// <summary>
        /// Gets the total number of lines in the content. This is equal to Content.Count('\n') + 1
        /// </summary>
        /// <returns></returns>
        public int GetTotalLines() {
            string content = this.Contents;
            if (content == null) {
                return -1;
            }

            int nextIndex = content.IndexOf('\n');
            if (nextIndex == -1) {
                return 0;
            }

            int charCount = 1;
            while (true) {
                nextIndex = content.IndexOf('\n', nextIndex + 1);
                if (nextIndex == -1) {
                    return charCount + 1;
                }

                charCount++;
            }
        }

        public int CountWords() {
            string text = this.Contents.Trim();
            if (string.IsNullOrEmpty(text)) {
                return 0;
            }

            int count = 0;
            bool lastWasWordChar = false;
            foreach (char c in text) {
                if (char.IsLetterOrDigit(c) || c == '_' || c == '\'' || c == '-') {
                    lastWasWordChar = true;
                    continue;
                }
                if (lastWasWordChar) {
                    lastWasWordChar = false;
                    count++;
                }
            }
            if (!lastWasWordChar) {
                count--;
            }

            return count + 1;
        }
    }
}