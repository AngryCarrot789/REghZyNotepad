using System;

namespace REghZyNotepad.Core.Exceptions {
    public class NoSuchViewModelException : Exception {
        public NoSuchViewModelException(Type type) : base($"The ViewModel type '{type.Name}' could not be found") {

        }
    }
}
