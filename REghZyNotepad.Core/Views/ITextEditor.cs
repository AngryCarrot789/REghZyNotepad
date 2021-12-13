using DragonJetzMVVM.Service;

namespace DragonJetzNotepad.Core.Views {
    /// <summary>
    /// A TextEditor API for getting or setting the line, cursor, caret, etc
    /// </summary>
    public interface ITextEditor : IService {
        int LineIndex { get; set; }
        int ColumnIndex { get; set; }
        int CaretIndex { get; set; }
        int GetCharIndexWithinLine(int line);
        void SetCharIndexWithinLine(int line, int caretIndex);
    }
}
