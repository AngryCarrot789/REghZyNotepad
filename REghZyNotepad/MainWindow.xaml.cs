using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using REghZyMVVM.Views;
using REghZyNotepad.Core;
using REghZyNotepad.Core.Utilities;
using REghZyNotepad.Core.ViewModels;
using REghZyNotepad.Core.Views;
using REghZyNotepad.Themes;
using REghZyNotepad.Views;
using REghZyNotepad.Views.Dialogs;

namespace REghZyNotepad {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, BaseView<MainViewModel>, ITextEditor, IViewProvider {
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
            ServiceLocator.TextEditor = this;
            ServiceLocator.ViewProvider = this;

            // WPF's builtin settings thing sometimes doesnt load when the main window opens so i made my own :)))))
            int configVersion = 2;
            RCSConfig.Main = new RCSConfig("reghzy-notepad");
            if (!RCSConfig.Main.TryGetInteger("config-version", out int value) || value != configVersion) {
                RCSConfig.Main.SetBoolean("save-location", true);
                RCSConfig.Main.SetBoolean("load-location", true);
                RCSConfig.Main.SetBoolean("save-size", true);
                RCSConfig.Main.SetBoolean("load-size", true);
                RCSConfig.Main.SetBoolean("save-theme", true);
                RCSConfig.Main.SetBoolean("load-theme", true);
                RCSConfig.Main.SetBoolean("save-format", true);
                RCSConfig.Main.SetBoolean("load-format", true);
                RCSConfig.Main.SetString("default-font-family", "Consolas");
                RCSConfig.Main.SetDouble("default-font-size", 15);
                // RCSConfig.Main.SetBoolean("allow-mwheel-horiz-scroll", true);
                // RCSConfig.Main.SetBoolean("allow-select-entire-line", true);
                RCSConfig.Main.SetInteger("config-version", configVersion);
                RCSConfig.Main.SaveConfig();
            }
            else {
                if (RCSConfig.Main.TryGetBoolean("load-location", out bool saveLocation) && saveLocation) {
                    if (RCSConfig.Main.TryGetInteger("last-x", out int x)) { this.Left = x; }
                    if (RCSConfig.Main.TryGetInteger("last-y", out int y)) { this.Top = y; }
                }
                if (RCSConfig.Main.TryGetBoolean("load-size", out bool saveSize) && saveSize) {
                    if (RCSConfig.Main.TryGetInteger("last-w", out int w)) { this.Width = w; }
                    if (RCSConfig.Main.TryGetInteger("last-h", out int h)) { this.Height = h; }
                }
                if (RCSConfig.Main.TryGetBoolean("load-theme", out bool saveTheme) && saveTheme) {
                    if (RCSConfig.Main.TryGetEnum("theme", out ThemeType theme)) { ThemesController.SetTheme(theme); }
                }
                if (RCSConfig.Main.TryGetBoolean("load-format", out bool saveFormat) && saveFormat) {
                    if (RCSConfig.Main.TryGetString("default-font-family", out string defaultFamily)) {
                        FormatViewModel.DEFAULT_FONT_FAMILY = defaultFamily;
                    }
                    if (RCSConfig.Main.TryGetDouble("default-font-size", out double defaultSize)) {
                        FormatViewModel.DEFAULT_FONT_SIZE = defaultSize;
                    }
                }
                // if (RCSConfig.Main.TryGetBoolean("allow-mwheel-horiz-scroll", out bool mwheelScroll)) {
                //     HorizontalScrolling.SCROLL_HORIZONTAL_WITH_SHIFT_MOUSEWHEEL = mwheelScroll;
                // }
                // if (RCSConfig.Main.TryGetBoolean("allow-select-entire-line", out bool selectLine)) {
                //     REghZyTextEditor.CAN_SELECT_ENTIRE_LINE_CTRL_SHIFT_A = selectLine;
                // }
            }

            ViewModelLocator.Instance.Application.Notepad.ClearDocument();
            this.FormatWindow = new FormatWindow();
            this.AboutWindow = new AboutWindow();
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
            new GotoLineDialog().ShowDialog();
        }

        public void OpenAboutView() {
            this.AboutWindow.Show();
        }

        public void OpenFindView(bool replace) {

        }

        public void UpdateCurrentEditor(TextEditorViewModel editor) {
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
            ViewModelLocator.Instance.Application.Notepad.Editor.UpdateCaret();
            DrawLineBorder();
        }

        private void TextEditor_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                double currentFont = ViewModelLocator.Instance.Application.Notepad.Editor.Format.FontSize;
                int fontChange = e.Delta / 100;
                if (currentFont > 1)
                    currentFont += fontChange;
                if (currentFont == 1 && fontChange >= 1)
                    currentFont += fontChange;

                ViewModelLocator.Instance.Application.Notepad.Editor.Format.FontSize = currentFont;
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
            if (ViewModelLocator.Instance.Application.Notepad.Editor.Document.HasTextChangedSinceSave) {
                if (ServiceLocator.Dialog.ShowConfirmable("Save changes?", "You have unsaved changes. Do you want to save them?", false)) {
                    ViewModelLocator.Instance.Application.Notepad.SaveDocumentAuto();
                }
            }

            if (RCSConfig.Main.TryGetBoolean("save-location", out bool saveLocation) && saveLocation) {
                RCSConfig.Main.SetInteger("last-x", (int)this.Left);
                RCSConfig.Main.SetInteger("last-y", (int)this.Top);
            }
            if (RCSConfig.Main.TryGetBoolean("save-size", out bool saveSize) && saveSize) {
                RCSConfig.Main.SetInteger("last-w", (int)this.Width);
                RCSConfig.Main.SetInteger("last-h", (int)this.Height);
            }
            if (RCSConfig.Main.TryGetBoolean("save-theme", out bool saveTheme) && saveTheme) {
                RCSConfig.Main.SetEnum("theme", ThemesController.CurrentTheme);
            }
            FormatViewModel format = ViewModelLocator.Instance.GetCurrentNotepad().Editor.Format;
            if (RCSConfig.Main.TryGetBoolean("save-format", out bool saveFormat) && saveFormat) {
                RCSConfig.Main.SetString("default-font-family", format.Font);
                RCSConfig.Main.SetDouble("default-font-size", format.FontSize);
            }

            this.AboutWindow.Close();
            this.FormatWindow.Close();
            base.OnClosing(e);
            Application.Current.Shutdown();
            RCSConfig.Main.SaveConfig();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e) {
            ThemeType theme;
            switch (int.Parse(((MenuItem)sender).Uid)) {
                case 0:
                    theme = ThemeType.Light;
                    break;
                case 1:
                    theme = ThemeType.Dark;
                    break;
                case 2:
                    theme = ThemeType.Red;
                    break;
                default:
                    return;
            }

            ThemesController.SetTheme(theme);
            if (RCSConfig.Main.TryGetBoolean("save-theme", out bool saveTheme) && saveTheme) {
                RCSConfig.Main.SetEnum("theme", theme);
            }
        }

        private void NotepadDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] arr && arr.Length > 0) {
                    ViewModelLocator.Instance.GetCurrentNotepad().OpenDocument(arr[0]);
                }
            }
        }

        private void NotepadDragEnter(object sender, DragEventArgs e) {
            e.Handled = true;
        }
    }
}