namespace REghZyNotepad.Notepad {
    public interface ITextSelectable {
        int LineIndex { get; set; }
        int ColumnIndex { get; set; }
        int CaretIndex { get; set; }
        int GetCharIndexWithinLine(int line);
        void SetCharIndexWithinLine(int line, int caretIndex);
    }
}
