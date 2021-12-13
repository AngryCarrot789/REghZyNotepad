using DragonJetzMVVM.Service;
using DragonJetzNotepad.Core.ViewModels;

namespace DragonJetzNotepad.Core.Views {
    /// <summary>
    /// An API for providing the ability to show specific views or update them
    /// </summary>
    public interface IViewProvider : IService {
        void OpenFormatView();
        void OpenGotoLineView();
        void OpenAboutView();
        void OpenFindView(bool replace);

        void UpdateCurrentEditor(TextEditorViewModel editor);
    }
}
