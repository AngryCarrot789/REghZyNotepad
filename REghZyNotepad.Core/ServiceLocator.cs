using REghZyNotepad.Core.Views;

namespace REghZyNotepad.Core {
    /// <summary>
    /// A wrapper for <see cref="IoC"/> services
    /// </summary>
    public class ServiceLocator {
        public static ServiceLocator Instance { get; }

        /// <summary>
        /// Gets or sets the message dialog service
        /// </summary>
        public static IDialogMessage Dialog {
            get => IoC.GetService<IDialogMessage>();
            set => IoC.SetService<IDialogMessage>(value);
        }

        /// <summary>
        /// Gets or sets the file save/open service
        /// </summary>
        public static ISaveOpenService SaveOpen {
            get => IoC.GetService<ISaveOpenService>();
            set => IoC.SetService<ISaveOpenService>(value);
        }

        /// <summary>
        /// Gets or sets the text editor API
        /// </summary>
        public static ITextEditor TextEditor {
            get => IoC.GetService<ITextEditor>();
            set => IoC.SetService<ITextEditor>(value);
        }

        /// <summary>
        /// Gets the view provider
        /// </summary>
        public static IViewProvider ViewProvider {
            get => IoC.GetService<IViewProvider>();
            set => IoC.SetService<IViewProvider>(value);
        }

        static ServiceLocator() {
            Instance = new ServiceLocator();
        }
    }
}
