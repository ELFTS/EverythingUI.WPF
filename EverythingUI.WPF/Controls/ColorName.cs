using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public enum ColorName
{
    White, Black, Gray, Red, Orange, Yellow, Green, Cyan, Blue,
    Purple, Pink, Indigo, Sky, Emerald, Rose, Amber, Violet, Coral, Mint
}

public static class ColorHelper
{
    public const ColorName DefaultColorName = ColorName.Blue;
    public static Color DefaultGradientStartColor => GetGradientStartColor(DefaultColorName);
    public static Color DefaultGradientEndColor => GetGradientEndColor(DefaultColorName);
    public static Color DefaultTrackColor => GetTrackColor(DefaultColorName);

    public static Color GetGradientStartColor(ColorName colorName) => colorName switch
    {
        ColorName.White => C("#FFFFFF"), ColorName.Black => C("#808080"), ColorName.Gray => C("#E5E5E5"),
        ColorName.Red => C("#FF5833"), ColorName.Orange => C("#FFC300"), ColorName.Yellow => C("#FFEB3B"),
        ColorName.Green => C("#A0D605"), ColorName.Cyan => C("#00BAAD"), ColorName.Blue => C("#00ACF0"),
        ColorName.Purple => C("#AC33C1"), ColorName.Pink => C("#F7D7EC"), ColorName.Indigo => C("#5B7FFF"),
        ColorName.Sky => C("#38BDF8"), ColorName.Emerald => C("#34D399"), ColorName.Rose => C("#FB7185"),
        ColorName.Amber => C("#FBBF24"), ColorName.Violet => C("#8B5CF6"), ColorName.Coral => C("#FF7F7F"),
        ColorName.Mint => C("#6EE7B7"), _ => DefaultGradientStartColor
    };

    public static Color GetGradientEndColor(ColorName colorName) => colorName switch
    {
        ColorName.White => C("#E6E6E6"), ColorName.Black => C("#383838"), ColorName.Gray => C("#A6A6A6"),
        ColorName.Red => C("#D43030"), ColorName.Orange => C("#FF8D1A"), ColorName.Yellow => C("#FFC400"),
        ColorName.Green => C("#19A654"), ColorName.Cyan => C("#00998F"), ColorName.Blue => C("#0078D4"),
        ColorName.Purple => C("#8D2C9E"), ColorName.Pink => C("#FF9CDB"), ColorName.Indigo => C("#4A6BE5"),
        ColorName.Sky => C("#0EA5E9"), ColorName.Emerald => C("#10B981"), ColorName.Rose => C("#E11D48"),
        ColorName.Amber => C("#D97706"), ColorName.Violet => C("#7C3AED"), ColorName.Coral => C("#FF5252"),
        ColorName.Mint => C("#34D399"), _ => DefaultGradientEndColor
    };

    public static Color GetTrackColor(ColorName colorName) => colorName switch
    {
        ColorName.Blue => C("#E3F2FD"), ColorName.Red => C("#FFEBEE"), ColorName.Green => C("#E8F5E9"),
        ColorName.Purple => C("#F3E5F5"), ColorName.Orange => C("#FFF3E0"), ColorName.Yellow => C("#FFFDE7"),
        ColorName.Cyan => C("#E0F7FA"), ColorName.Pink => C("#FCE4EC"), _ => C("#E6E6E6")
    };

    public static (Color start, Color end) GetGradientColors(this ColorName colorName)
        => (GetGradientStartColor(colorName), GetGradientEndColor(colorName));

    private static Color C(string hex) => (Color)ColorConverter.ConvertFromString(hex);
}
