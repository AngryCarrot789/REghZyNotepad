using System;

namespace DragonJetzNotepad.Core.Exceptions {

    /// <summary>
    /// An exception for invalid data or information
    /// </summary>
    [Serializable]
    public class InvalidDataException : Exception {
        public InvalidDataException() { }
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
