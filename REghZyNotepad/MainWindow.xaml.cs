using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using REghZyFramework.Utilities;
using REghZyNotepad.Notepad;
using REghZyNotepad.Views;
using REghZyNotepad.Views.Dialogs;
using REghZyThemes;
using REghZyThemes.Themes;

namespace REghZyNotepad {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, BaseView<MainViewModel>, ITextSelectable, IViewProvider {
        public FormatWindow FormatWindow { get; }
        public AboutWindow AboutWindow { get; }

        public MainViewModel Model {
            get => (MainViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public int LineIndex {
            get => this.TextEditor.GetLineIndexFromCharacterIndex(this.TextEditor.CaretIndex);
            set => this.TextEditor.CaretIndex = this.TextEditor.GetCharacterIndexFromLineIndex(value);
        }

        public int ColumnIndex {
            get => GetCharIndexWithinLine(this.LineIndex);
            set => SetCharIndexWithinLine(this.LineIndex, value);
        }

        public int CaretIndex {
            get => this.TextEditor.CaretIndex;
            set => this.TextEditor.CaretIndex = value;
        }

        public MainWindow() {
            InitializeComponent();
            this.FormatWindow = new FormatWindow();
            this.AboutWindow = new AboutWindow();
            this.Model = new MainViewModel(this, this);
        }

        public int GetCharIndexWithinLine(int line) {
            return this.TextEditor.CaretIndex - this.TextEditor.GetCharacterIndexFromLineIndex(line);
        }

        public void SetCharIndexWithinLine(int line, int caretIndex) {
            this.TextEditor.CaretIndex = this.TextEditor.GetCharacterIndexFromLineIndex(line) + caretIndex;
        }

        public void OpenFormatView() {
            this.FormatWindow.Show();
        }

        public void OpenGotoLineView() {
            GotoLineDialog dialog = new GotoLineDialog(this.Model.NotepadEditor);
            dialog.ShowDialog();
        }

        public void OpenAboutView() {
            this.AboutWindow.Show();
        }

        public void OpenFindView(bool replace) {

        }

        public void UpdateCurrentEditor(NotepadEditorViewModel editor) {
            this.FormatWindow.Model = editor;
        }

        public void DrawLineBorder() {
            Rect caret = TextEditor.GetCaretLocation();
            double offsetY = 1;
            double scrollBarWidth = 18;
            Thickness outlineMargin =
                new Thickness(
                    0,
                    caret.Y - offsetY,
                    scrollBarWidth,
                    TextEditor.ActualHeight - caret.Bottom - offsetY);
            if (outlineMargin.Top >= -1 && outlineMargin.Bottom >= 0) {
                aditionalSelection.Visibility = Visibility.Visible;
                aditionalSelection.Margin = outlineMargin;
            }
            // dont remove this else statement otherwise the rendering
            // goes absolutely mental for the entire app (black areas..)
            else {
                aditionalSelection.Visibility = Visibility.Collapsed;
            }
        }

        private void TextEditor_SelectionChanged(object sender, RoutedEventArgs e) {
            this.Model.UpdateBar();
            DrawLineBorder();
        }

        private void TextEditor_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                double currentFont = this.Model.NotepadEditor.Format.FontSize;
                int fontChange = e.Delta / 100;
                if (currentFont > 1)
                    currentFont += fontChange;
                if (currentFont == 1 && fontChange >= 1)
                    currentFont += fontChange;

                this.Model.NotepadEditor.Format.FontSize = currentFont;
                e.Handled = true;
            }

            DrawLineBorder();
        }

        private void TextEditor_SizeChanged(object sender, SizeChangedEventArgs e) {
            DrawLineBorder();
        }

        private void TextEditor_ScrollChanged(object sender, ScrollChangedEventArgs e) {
            DrawLineBorder();
        }

        protected override void OnClosing(CancelEventArgs e) {
            this.AboutWindow.Close();
            this.FormatWindow.Close();
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e) {
            switch (int.Parse(((MenuItem)sender).Uid)) {
                case 0:
                    ThemesController.SetTheme(ThemeType.Light);
                    break;
                case 1:
                    ThemesController.SetTheme(ThemeType.ColourfulLight);
                    break;
                case 2:
                    ThemesController.SetTheme(ThemeType.Dark);
                    break;
                case 3:
                    ThemesController.SetTheme(ThemeType.ColourfulDark);
                    break;
            }
        }
    }
}
