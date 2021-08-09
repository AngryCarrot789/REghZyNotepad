using System;
using System.Windows;
using System.Windows.Media;
using REghZyThemes.Themes;

namespace REghZyThemes
{
    public static class ThemesController
    {
        public static ThemeType CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[0]; }
            set { Application.Current.Resources.MergedDictionaries[0] = value; }
        }

        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }
        public static void SetTheme(ThemeType theme)
        {
            string themeName = theme.GetName();
            CurrentTheme = theme;
            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"Themes/{themeName}.xaml", UriKind.Relative));
            }
            catch
            {
                // Log?
            }
        }

        public static object GetResource(object key)
        {
            return ThemeDictionary[key];
        }

        public static SolidColorBrush GetBrush(string name)
        {
            if (GetResource(name) is SolidColorBrush brush)
                return brush;
            else
                return new SolidColorBrush(Colors.White);
        }
    }
}
