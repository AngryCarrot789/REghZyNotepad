using System.Runtime.CompilerServices;
using REghZyNotepad.Core.ViewModels;

namespace REghZyNotepad.Core {
    /// <summary>
    /// A wrapper for the <see cref="IoC"/> class
    /// </summary>
    public class ViewModelLocator {
        public static ViewModelLocator Instance { get; }

        /// <summary>
        /// Gets the application view model
        /// </summary>
        public ApplicationViewModel Application {
            get => IoC.GetViewModel<ApplicationViewModel>();
            // shouldn't set this more than once because the view isn't designed to handle it changing
            private set => IoC.SetViewModel<ApplicationViewModel>(value);
        }

        static ViewModelLocator() {
            Instance = new ViewModelLocator();
        }

        public static void Init() {
            Instance.Application = new ApplicationViewModel();
        }

        public NotepadViewModel GetCurrentNotepad() {
            return this.Application.Notepad;
        }

        public TextEditorViewModel GetCurrentTextEditor() {
            return GetCurrentNotepad().Editor;
        }
    }
}
