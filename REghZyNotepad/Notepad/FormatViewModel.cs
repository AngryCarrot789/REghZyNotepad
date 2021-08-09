using System.Windows;
using System.Windows.Media;
using REghZyFramework.Utilities;

namespace REghZyNotepad.Notepad {
    public class FormatViewModel : BaseViewModel {
        private double _fontSize;
        private FontFamily _font;
        private FontStyle _style;
        private FontWeight _weight;
        private TextDecorationCollection _decoration;
        private string _decorationString;
        private TextWrapping _wrapping;
        private bool _isWrapped;

        public double FontSize {
            get => _fontSize;
            set {
                if (value > 0 || value <= 200) {
                    RaisePropertyChanged(ref this._fontSize, value);
                }
            }
        }

        public FontFamily Font {
            get => _font;
            set => RaisePropertyChanged(ref this._font, value);
        }

        public FontStyle Style {
            get => _style;
            set => RaisePropertyChanged(ref this._style, value);
        }

        public FontWeight Weight {
            get => _weight;
            set => RaisePropertyChanged(ref this._weight, value);
        }

        public TextDecorationCollection Decoration {
            get => _decoration;
            set => RaisePropertyChanged(ref this._decoration, value);
        }

        public string DecorationString {
            get => _decorationString;
            set {
                RaisePropertyChanged(ref this._decorationString, value);
                switch (value) {
                    case "None":
                        this.Decoration = null;
                        break;
                    case "Underline":
                        this.Decoration = TextDecorations.Underline;
                        break;
                    case "Strikethrough":
                        this.Decoration = TextDecorations.Strikethrough;
                        break;
                    case "OverLine":
                        this.Decoration = TextDecorations.OverLine;
                        break;
                    case "Baseline":
                        this.Decoration = TextDecorations.Baseline;
                        break;
                }
            }
        }

        public TextWrapping Wrapping {
            get => _wrapping;
            set => RaisePropertyChanged(ref this._wrapping, value);
        }

        public bool IsWrapped {
            get => _isWrapped;
            set {
                RaisePropertyChanged(ref this._isWrapped, value);
                this.Wrapping = value ? TextWrapping.Wrap : TextWrapping.NoWrap;
            }
        }

        public FormatViewModel(double fontSize = 14) {
            this.Font = new FontFamily("Consolas");
            this.FontSize = fontSize;
            this.Style = FontStyles.Normal;
            this.Weight = FontWeights.Normal;
            this.IsWrapped = false;
        }
    }
}