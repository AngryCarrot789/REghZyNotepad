using REghZyMVVM.Service;

namespace REghZyNotepad.Core.Views {
    /// <summary>
    /// A dialog service for saving and opening files
    /// </summary>
    public interface ISaveOpenService : IService {
        /// <summary>
        /// Returns a string path of the file that is to be saved, 
        /// or <see langword="null"/> if a path wasn't selected
        /// </summary>
        /// <returns>The path, or <see langword="null"/></returns>
        string SaveFile();

        /// <summary>
        /// Returns a string path of the file that is to be opened, 
        /// or <see langword="null"/> if a path wasn't selected
        /// </summary>
        /// <returns>The path, or <see langword="null"/></returns>
        string OpenFile();
    }
}
