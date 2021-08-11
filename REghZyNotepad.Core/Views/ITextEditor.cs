namespace REghZyNotepad.Core.Views {
    /// <summary>
    /// A TextEditor API for getting or setting the line, cursor, caret, etc
    /// </summary>
    public interface ITextEditor {
        int LineIndex { get; set; }
        int ColumnIndex { get; set; }
        int CaretIndex { get; set; }
        int GetCharIndexWithinLine(int line);
        void SetCharIndexWithinLine(int line, int caretIndex);
    }
}
