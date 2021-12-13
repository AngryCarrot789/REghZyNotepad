using DragonJetzMVVM.ViewModels.Base;

namespace DragonJetzNotepad.Core.ViewModels {
    public class FormatViewModel : BaseViewModel {
        public static string DEFAULT_FONT_FAMILY = "Consolas";
        public static double DEFAULT_FONT_SIZE = 14;

        private double _fontSize;
        private string _font;
        private string _style;
        private string _weight;
        private string _decoration;
        private bool _isWrapped;

        public double FontSize {
            get => _fontSize;
            set {
                if (value > 0 && value <= 200) {
                    RaisePropertyChanged(ref this._fontSize, value);
                }
            }
        }

        public string Font {
            get => _font;
            set => RaisePropertyChanged(ref this._font, value);
        }

        public string Style {
            get => _style;
            set => RaisePropertyChanged(ref this._style, value);
        }

        public string Weight {
            get => _weight;
            set => RaisePropertyChanged(ref this._weight, value);
        }

        public string Decoration {
            get => _decoration;
            set => RaisePropertyChanged(ref this._decoration, value);
        }

        // public string DecorationString {
        //     get => _decorationString;
        //     set {
        //         RaisePropertyChanged(ref this._decorationString, value);
        //         switch (value) {
        //             case "None":
        //                 this.Decoration = null;
        //                 break;
        //             case "Underline":
        //                 this.Decoration = TextDecorations.Underline;
        //                 break;
        //             case "Strikethrough":
        //                 this.Decoration = TextDecorations.Strikethrough;
        //                 break;
        //             case "OverLine":
        //                 this.Decoration = TextDecorations.OverLine;
        //                 break;
        //             case "Baseline":
        //                 this.Decoration = TextDecorations.Baseline;
        //                 break;
        //         }
        //     }
        // }

        public bool IsWrapped {
            get => _isWrapped;
            set => RaisePropertyChanged(ref this._isWrapped, value);
        }

        public FormatViewModel() {
            this.Font = DEFAULT_FONT_FAMILY;
            this.FontSize = DEFAULT_FONT_SIZE;
            this.Style = "Normal";
            this.Weight = "Normal";
            this.IsWrapped = false;
        }
    }
}