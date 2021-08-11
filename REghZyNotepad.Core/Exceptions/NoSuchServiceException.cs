using System;

namespace REghZyNotepad.Core.Exceptions {
    public class NoSuchServiceException : Exception {
        public NoSuchServiceException(Type type) : base($"The service of type '{type.Name}' does not exist") {

        }
    }
}
