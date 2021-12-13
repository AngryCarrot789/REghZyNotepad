using DragonJetzMVVM.Service;

namespace DragonJetzNotepad.Core.Views {
    /// <summary>
    /// An API for showing messages onscreen
    /// </summary>
    public interface IDialogMessage : IService {
        /// <summary>
        /// Shows a general message with the given title
        /// </summary>
        /// <param name="caption">Title of the dialog</param>
        /// <param name="message">Message of the dialog</param>
        void Show(string caption, string message);

        /// <summary>
        /// Shows a general message with the given title, with the ability to return an answer
        /// </summary>
        /// <param name="caption">Title of the dialog</param>
        /// <param name="message">Message of the dialog</param>
        /// <returns>Whether the dialog was accepted or not</returns>
        bool ShowConfirmable(string caption, string message, bool defaultResult = false);
    }
}
