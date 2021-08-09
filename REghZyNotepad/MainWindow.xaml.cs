using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using REghZyFramework.Utilities;
using REghZyNotepad.Notepad;

namespace REghZyNotepad {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, BaseView<MainViewModel>, ITextSelectable {
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
            this.Model = new MainViewModel(this);
        }

        public int GetCharIndexWithinLine(int line) {
            return this.TextEditor.CaretIndex - this.TextEditor.GetCharacterIndexFromLineIndex(line);
        }

        public void SetCharIndexWithinLine(int line, int caretIndex) {
            this.TextEditor.CaretIndex = this.TextEditor.GetCharacterIndexFromLineIndex(line) + caretIndex;
        }

        private void TextEditor_SelectionChanged(object sender, RoutedEventArgs e) {
            this.Model.UpdateBar();
        }
    }
}
