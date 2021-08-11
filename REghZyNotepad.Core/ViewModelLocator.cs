﻿using REghZyNotepad.Core.ViewModels;

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
            get => IoC.GetVM<ApplicationViewModel>();
            // shouldn't set this more than once because the view isn't designed to handle it changing... not yet atleast ;)
            set => IoC.SetViewModel<ApplicationViewModel>(value);
        }

        static ViewModelLocator() {
            Instance = new ViewModelLocator();
        }

        public NotepadViewModel GetCurrentNotepad() {
            return this.Application.Notepad;
        }

        public TextEditorViewModel GetCurrentTextEditor() {
            return GetCurrentNotepad().Editor;
        }
    }
}