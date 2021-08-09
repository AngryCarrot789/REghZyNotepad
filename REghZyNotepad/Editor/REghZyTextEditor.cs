using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using REghZyNotepad.Notepad;

namespace REghZyNotepad.Editor {
    public class REghZyTextEditor : TextBox {
        public const bool CAN_SELECT_ENTIRE_LINE_CTRL_SHIFT_A = true;
        public const bool CAN_CUT_ENTIRE_LINE_CTRL_X = true;
        public const bool CAN_ADD_ENTIRE_LINES = true;
        public const bool CAN_COPY_ENTIRE_LINE_CTRL_C = true;
        public const bool SCROLL_VERTICAL_WITH_CTRL_ARROWKEYS = true;
        public const bool SCROLL_HORIZONTAL_WITH_CTRL_ARROWKEYS = false;

        private NotepadEditorViewModel Model {
            get => (NotepadEditorViewModel)this.DataContext;
        }

        public REghZyTextEditor() { }

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            //Model.Notepad.LinesCounter.LinesCount = this.LineCount;
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                if (Keyboard.IsKeyDown(Key.LeftShift)) {
                    switch (e.Key) {
                        case Key.A:
                            if (!CAN_SELECT_ENTIRE_LINE_CTRL_SHIFT_A)
                                break;

                            SelectEntireCurrentLine();
                            break;

                        case Key.X:
                            if (!CAN_CUT_ENTIRE_LINE_CTRL_X)
                                break;
                            SelectEntireCurrentLine();
                            SelectedText = "";
                            break;

                        case Key.Enter:
                            if (!CAN_ADD_ENTIRE_LINES)
                                break;
                            AddNewLineBelowCurrentLine();
                            break;
                    }
                }
                else {
                    switch (e.Key) {
                        case Key.Enter:
                            if (!CAN_ADD_ENTIRE_LINES)
                                break;
                            AddNewLineAndMoveCaret();
                            break;
                        case Key.X:
                            if (!CAN_CUT_ENTIRE_LINE_CTRL_X)
                                break;
                            if (SelectedText == "") {
                                SelectEntireCurrentLine();
                                // not altering e.Handled lets the
                                // textbox base cut the line automatically.
                            }
                            break;
                        case Key.C:
                            if (!CAN_COPY_ENTIRE_LINE_CTRL_C)
                                break;
                            if (SelectedText == "") {
                                CopyEntireCurrentLine();
                                e.Handled = true;
                            }
                            break;

                        case Key.Up:
                            if (!SCROLL_VERTICAL_WITH_CTRL_ARROWKEYS)
                                break;
                            LineUp();
                            e.Handled = true;
                            break;
                        case Key.Down:
                            if (!SCROLL_VERTICAL_WITH_CTRL_ARROWKEYS)
                                break;
                            LineDown();
                            e.Handled = true;
                            break;
                        case Key.Left:
                            if (!SCROLL_HORIZONTAL_WITH_CTRL_ARROWKEYS)
                                break;
                            LineLeft();
                            e.Handled = true;
                            break;
                        case Key.Right:
                            if (!SCROLL_HORIZONTAL_WITH_CTRL_ARROWKEYS)
                                break;
                            LineRight();
                            e.Handled = true;
                            break;
                        // This prevents a bug with undo-ing causing the FindResults
                        // to sort of be offset. This could be quite laggy though
                        // with lots of text and text still in the find bit...
                        case Key.Z:
                        case Key.Y:
                            // Model.Find.StartFind();
                            break;
                    }
                }
            }
            // Cant do because holding alt apparently blocks the keydown events...
            //if (Keyboard.Modifiers == ModifierKeys.Alt)
            //{
            //    switch (e.Key)
            //    {
            //        case Key.Left: LineLeft(); e.Handled = true; break;
            //        case Key.Right: LineRight(); e.Handled = true; break;
            //    }
            //}

            // This prevents a bug with undo-ing causing the FindResults
            // to sort of be offset. This could be quite laggy though
            // with lots of text and text still in the find bit...
            // this in unneeded now because it's re-done after
            // the find box is focused
            //Model.Find.StartFind();
        }

        public void InsertText(string text, int charIndex, bool setCaretAfterText = false) {
            SelectionStart = charIndex;
            SelectedText = text;
            SelectionLength = 0;
            if (setCaretAfterText)
                CaretIndex += text.Length;
        }

        public void InsetNewLineAtCaret() {
            InsertText("\n", CaretIndex, true);
        }

        public void AddNewLineAboveCurrentLine() {
            int startOfNewLineIndex = Text.LastIndexOf(Environment.NewLine, CaretIndex) + 1;
            SelectionStart = startOfNewLineIndex;
            SelectedText = Environment.NewLine;
            SelectionLength = 0;
            CaretIndex += 2;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddNewLineBelowCurrentLine() {
            int startOfNewLineIndex = Text.LastIndexOf(Environment.NewLine, CaretIndex) + 1;
            SelectionStart = startOfNewLineIndex;
            int prevCaretIndex = CaretIndex;
            SelectedText = Environment.NewLine;
            SelectionLength = 0;
            CaretIndex = prevCaretIndex;
        }

        /// <summary>
        /// Set the caret index to the start of the new line 
        /// and then inser a NewLine character
        /// </summary>
        public void AddNewLineAndMoveCaret() {
            int startOfNewLineIndex = Text.LastIndexOf(Environment.NewLine, CaretIndex) + 1;
            int lineIndex = base.GetLineIndexFromCharacterIndex(CaretIndex);
            int lineLength = base.GetLineLength(lineIndex);
            CaretIndex = startOfNewLineIndex + lineLength + 1;
            SelectionStart = CaretIndex;
            SelectedText = Environment.NewLine;
            SelectionLength = 0;
            if (CaretIndex > 1)
                CaretIndex -= 1;
        }

        public void CopyEntireCurrentLine() {
            SelectEntireCurrentLine();
            string newLineText = SelectedText;
            CaretIndex = Text.LastIndexOf(Environment.NewLine, CaretIndex) + 1;
            Clipboard.SetText(newLineText);
        }

        public void SelectEntireCurrentLine() {
            int start = Text.LastIndexOf(Environment.NewLine, CaretIndex) + 1;
            int lineIdx = GetLineIndexFromCharacterIndex(CaretIndex);
            int lineLength = GetLineLength(lineIdx);
            SelectionStart = start;
            SelectionLength = lineLength;
        }

        public int GetLinesCount() {
            return Text.Split('\n').Length;
        }

        // public void HighlightSearchResult(FindResult result, bool focusTextEditor = true) {
        //     if (result.StartIndex >= 0 &&
        //         result.StartIndex < Text.Length &&
        //         result.WordLength > 0 &&
        //         result.StartIndex + result.WordLength <= Text.Length) {
        //         try {
        //             if (focusTextEditor) {
        //                 Focus();
        //             }
        // 
        //             Select(result.StartIndex, result.WordLength);
        //             ScrollToLine(GetLineIndexFromCharacterIndex(SelectionStart));
        // 
        //             //int actualLine = GetLineIndexFromCharacterIndex(result.StartIndex);
        //             //int finalLine = actualLine + ((int) Math.Round((ActualHeight / FontSize) / 2, 0) ) ;
        //             //ScrollToLine(actualLine);
        //             //Select(result.StartIndex, result.WordLength);
        //         }
        //         catch { }
        //     }
        // }
        // 
        // public void ReplaceText(FindResult result, string toReplaceWith) {
        //     if (!toReplaceWith.IsEmpty()) {
        //         SelectionStart = result.StartIndex;
        //         SelectionLength = result.WordLength;
        //         SelectedText = toReplaceWith;
        //         //StringBuilder sb = new StringBuilder(heapText);
        //         //sb.Remove(result.StartIndex, result.WordLength);
        //         //sb.Insert(result.StartIndex, replaceWith);
        //     }
        // }

        public Rect GetCaretLocation() {
            try {
                if (CaretIndex >= 0) {
                    Rect rect = GetRectFromCharacterIndex(CaretIndex);
                    if (double.IsInfinity(rect.X) || double.IsInfinity(rect.Y))
                        return new Rect(0, 0, 0, 0);
                    return rect;
                }
                else
                    return new Rect(0, 0, 0, 0);
            }
            catch { return new Rect(0, 0, 0, 0); }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            //if (!this.IsFocused)
            //    this.Focus();
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            //if (!this.IsFocused)
            //{
            //    this.Focus();
            //    base.OnMouseWheel(e);
            //    this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            //}
            //else
            //{
            base.OnMouseWheel(e);
            //}
        }

        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //    e.Handled = true;
        //    //base.OnLostFocus(e);
        //}

        public int GetCaretIndexWithinLine(int line) {
            return CaretIndex - GetCharacterIndexFromLineIndex(line);
        }

        public int CountCharacters(string text, char character) {
            if (text == null) {
                return 0;
            }

            int nextIndex = text.IndexOf(character);
            if (nextIndex == -1) {
                return 0;
            }

            int charCount = 1;
            while (true) {
                nextIndex = text.IndexOf(character, nextIndex + 1);
                if (nextIndex == -1) {
                    return charCount;
                }

                charCount++;
            }
        }

        public int CustomGetLineCount() {
            return CountCharacters(Text, '\n');
        }

        public int CustomGetLineCountUptoCaretIndex(int caret) {
            int count = 0;
            for (int i = 0; i < caret; i++) {
                if (Text[i] == '\n') {
                    count++;
                }
            }
            return count;
        }

        public int CustomGetLineStartIndexFromCaret(int caretIndex) {
            for (int i = caretIndex; i < 0; i--) {
                if (Text[i] == '\n') {
                    return i + 1;
                }
            }
            return 0;
        }
        public int CustomGetLineEndIndexFromCaret(int caretIndex) {
            for (int i = caretIndex; i > Text.Length - 1; i++) {
                if (Text[i] == '\n') {
                    return i - 1;
                }
            }
            return 0;
        }

        public int CustomGetLineIndexFromCaret(int caretIndex) {
            string tempText = Text.Substring(0, caretIndex);
            int newLineCharacters = 0;
            for (int i = 0; i < tempText.Length; i++) {
                if (tempText[i] == '\n') {
                    newLineCharacters++;
                }
            }
            return newLineCharacters;
        }


        public int CustomGetCurrentLineLength() {
            return CustomGetLineStartIndexFromCaret(CaretIndex) - CustomGetLineEndIndexFromCaret(CaretIndex);
        }

        public int CustomGetCaretPositionWithinLineFromLineStartIndex() {
            return CustomGetCurrentLineLength() - CaretIndex;
        }
    }
}