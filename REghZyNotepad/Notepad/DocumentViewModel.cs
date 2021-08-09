using System;
using System.IO;
using REghZyFramework.Utilities;

namespace REghZyNotepad.Notepad {
    public class DocumentViewModel : BaseViewModel {
        private string _contents;
        private string _filePath;
        private long _fileSize;

        public string Contents {
            get => _contents;
            set {
                RaisePropertyChanged(ref this._contents, value);
                this.FileSize = value.Length;
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
            set {
                if (value == null) {
                    throw new NullReferenceException("New file name cannot be null");
                }
                if (value.IndexOf('.') == -1) {
                    throw new InvalidDataException("New file name does not have an extension");
                }

                this.FilePath = Path.Combine(Path.GetPathRoot(this.FilePath), value);
            }
        }

        public long FileSize {
            get => _fileSize;
            set {
                if (value < 0) {
                    throw new InvalidDataException("File size cannot be below 0");
                }
                if (value > 41943040) {
                    throw new InvalidDataException("File size cannot exceed 41943040 bytes");
                }

                RaisePropertyChanged(ref this._fileSize, value);
            }
        }
    }
}