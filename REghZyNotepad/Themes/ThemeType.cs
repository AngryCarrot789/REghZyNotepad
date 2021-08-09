namespace REghZyThemes.Themes {
    public enum ThemeType
    {
        Light,
        Dark,
        ColourfulLight,
        ColourfulDark
    }

    public static class ThemeTypeExtension
    {
        public static string GetName(this ThemeType type)
        {
            switch (type)
            {
                case ThemeType.Light: return "LightTheme";
                case ThemeType.Dark: return "DarkTheme";
                case ThemeType.ColourfulLight: return "ColourfulLightTheme";
                case ThemeType.ColourfulDark: return "ColourfulDarkTheme";
                default: return null;
            }
        }
    }
}
