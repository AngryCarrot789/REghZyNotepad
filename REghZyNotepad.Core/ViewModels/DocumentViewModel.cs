using System;
using System.IO;
using REghZyNotepad.Core.ViewModels.Base;

namespace REghZyNotepad.Core.ViewModels {
    public class DocumentViewModel : BaseViewModel {
        private string _contents;
        private string _filePath;
        private long _length;
        private long _wordCount;

        private bool _hasMadeChanges;
        public bool HasTextChangedSinceSave {
            get => this._hasMadeChanges;
            set {
                RaisePropertyChanged(ref this._hasMadeChanges, value);
                this.FilePath = this.FilePath;
            }
        }

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

        public string FilePath {
            get => _filePath;
            set => RaisePropertyChanged(ref this._filePath, value);
        }

        /// <summary>
        /// Sets the file name by editing the <see cref="FilePath"/> property
        /// </summary>
        /// <exception cref="NullReferenceException">If the new file name is null or empty</exception>
        /// <exception cref="InvalidDataException">If the new file name doesn't contain an extension</exception>
        public string FileName {
            get => Path.GetFileName(this.FilePath);
            // set {
            //     if (value == null) {
            //         throw new NullReferenceException("New file name cannot be null");
            //     }
            //     if (value.IndexOf('.') == -1) {
            //         throw new Exceptions.InvalidDataException("New file name does not have an extension");
            //     }
            // 
            //     this.FilePath = Path.Combine(Path.GetFullPath(this.FilePath), value);
            // }
        }

        public long Length {
            get => _length;
            set {
                if (value > 41943040) {
                    throw new Exceptions.InvalidDataException("File size cannot exceed 41943040 bytes");
                }

                this.WordCount = CountWords();
                RaisePropertyChanged(ref this._length, value);
            }
        }

        public long WordCount {
            get => _wordCount;
            set {
                RaisePropertyChanged(ref this._wordCount, value);
            }
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
            if (string.IsNullOrEmpty(text))
                return 0;

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
            if (!lastWasWordChar)
                count--;

            return count + 1;
        }
    }
}