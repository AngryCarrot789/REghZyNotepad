using System;
using System.Windows.Input;

namespace REghZyNotepad.Core.ViewModels.Base {
    /// <summary>
    /// An always executable command
    /// </summary>
    public class Command : ICommand {
        private readonly Action _action;

        /// <summary>
        /// Creates a command that can always execute
        /// </summary>
        /// <param name="action">The method to be executed</param>
        public Command(Action action) {
            _action = action;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Ignored</param>
        public void Execute(object parameter) {
            _action?.Invoke();
        }

        /// <summary>
        /// Returns true because this is an always executable command
        /// </summary>
        /// <param name="parameter">Ignored</param>
        /// <returns>True</returns>
        public bool CanExecute(object parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}