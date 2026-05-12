using System;
using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

/// <summary>
/// 预设颜色名称枚举 - 对应GradientColors.xaml中的渐变配色
/// </summary>
public enum ColorName
{
    /// <summary>
    /// 白色
    /// </summary>
    White,

    /// <summary>
    /// 黑色
    /// </summary>
    Black,

    /// <summary>
    /// 灰色
    /// </summary>
    Gray,

    /// <summary>
    /// 红色 - 用于危险/删除操作
    /// </summary>
    Red,

    /// <summary>
    /// 橘色 - 用于警告/提示
    /// </summary>
    Orange,

    /// <summary>
    /// 黄色 - 用于高亮/注意
    /// </summary>
    Yellow,

    /// <summary>
    /// 绿色 - 用于成功/确认
    /// </summary>
    Green,

    /// <summary>
    /// 青色 - 用于信息/清新
    /// </summary>
    Cyan,

    /// <summary>
    /// 蓝色 - 默认主色,用于主要操作
    /// </summary>
    Blue,

    /// <summary>
    /// 紫色 - 用于创意/特殊
    /// </summary>
    Purple,

    /// <summary>
    /// 粉色 - 用于柔和/女性化
    /// </summary>
    Pink,

    /// <summary>
    /// 靛蓝 - 深邃蓝色
    /// </summary>
    Indigo,

    /// <summary>
    /// 天蓝 - 明亮清新
    /// </summary>
    Sky,

    /// <summary>
    /// 翠绿 - 生机勃勃
    /// </summary>
    Emerald,

    /// <summary>
    /// 玫瑰 - 浪漫柔和
    /// </summary>
    Rose,

    /// <summary>
    /// 琥珀 - 温暖复古
    /// </summary>
    Amber,

    /// <summary>
    /// 蓝紫 - 梦幻渐变
    /// </summary>
    Violet,

    /// <summary>
    /// 珊瑚 - 活力橙红
    /// </summary>
    Coral,

    /// <summary>
    /// 薄荷 - 清凉舒爽
    /// </summary>
    Mint
}

/// <summary>
/// 颜色工具类 - 提供颜色名称到实际颜色的转换
/// </summary>
public static class ColorHelper
{
    /// <summary>
    /// 根据颜色名称获取起始渐变颜色
    /// </summary>
    public static Color GetGradientStartColor(ColorName colorName)
    {
        return colorName switch
        {
            ColorName.White => (Color)ColorConverter.ConvertFromString("#FFFFFF"),
            ColorName.Black => (Color)ColorConverter.ConvertFromString("#808080"),
            ColorName.Gray => (Color)ColorConverter.ConvertFromString("#E5E5E5"),
            ColorName.Red => (Color)ColorConverter.ConvertFromString("#FF5833"),
            ColorName.Orange => (Color)ColorConverter.ConvertFromString("#FFC300"),
            ColorName.Yellow => (Color)ColorConverter.ConvertFromString("#FFEB3B"),
            ColorName.Green => (Color)ColorConverter.ConvertFromString("#A0D605"),
            ColorName.Cyan => (Color)ColorConverter.ConvertFromString("#00BAAD"),
            ColorName.Blue => (Color)ColorConverter.ConvertFromString("#00ACF0"),
            ColorName.Purple => (Color)ColorConverter.ConvertFromString("#AC33C1"),
            ColorName.Pink => (Color)ColorConverter.ConvertFromString("#F7D7EC"),
            ColorName.Indigo => (Color)ColorConverter.ConvertFromString("#5B7FFF"),
            ColorName.Sky => (Color)ColorConverter.ConvertFromString("#38BDF8"),
            ColorName.Emerald => (Color)ColorConverter.ConvertFromString("#34D399"),
            ColorName.Rose => (Color)ColorConverter.ConvertFromString("#FB7185"),
            ColorName.Amber => (Color)ColorConverter.ConvertFromString("#FBBF24"),
            ColorName.Violet => (Color)ColorConverter.ConvertFromString("#8B5CF6"),
            ColorName.Coral => (Color)ColorConverter.ConvertFromString("#FF7F7F"),
            ColorName.Mint => (Color)ColorConverter.ConvertFromString("#6EE7B7"),
            _ => (Color)ColorConverter.ConvertFromString("#00ACF0")
        };
    }

    /// <summary>
    /// 根据颜色名称获取中间渐变颜色
    /// </summary>
    public static Color GetGradientEndColor(ColorName colorName)
    {
        return colorName switch
        {
            ColorName.White => (Color)ColorConverter.ConvertFromString("#E6E6E6"),
            ColorName.Black => (Color)ColorConverter.ConvertFromString("#383838"),
            ColorName.Gray => (Color)ColorConverter.ConvertFromString("#A6A6A6"),
            ColorName.Red => (Color)ColorConverter.ConvertFromString("#D43030"),
            ColorName.Orange => (Color)ColorConverter.ConvertFromString("#FF8D1A"),
            ColorName.Yellow => (Color)ColorConverter.ConvertFromString("#FFC400"),
            ColorName.Green => (Color)ColorConverter.ConvertFromString("#19A654"),
            ColorName.Cyan => (Color)ColorConverter.ConvertFromString("#00998F"),
            ColorName.Blue => (Color)ColorConverter.ConvertFromString("#0078D4"),
            ColorName.Purple => (Color)ColorConverter.ConvertFromString("#8D2C9E"),
            ColorName.Pink => (Color)ColorConverter.ConvertFromString("#FF9CDB"),
            ColorName.Indigo => (Color)ColorConverter.ConvertFromString("#4A6BE5"),
            ColorName.Sky => (Color)ColorConverter.ConvertFromString("#0EA5E9"),
            ColorName.Emerald => (Color)ColorConverter.ConvertFromString("#10B981"),
            ColorName.Rose => (Color)ColorConverter.ConvertFromString("#E11D48"),
            ColorName.Amber => (Color)ColorConverter.ConvertFromString("#D97706"),
            ColorName.Violet => (Color)ColorConverter.ConvertFromString("#7C3AED"),
            ColorName.Coral => (Color)ColorConverter.ConvertFromString("#FF5252"),
            ColorName.Mint => (Color)ColorConverter.ConvertFromString("#34D399"),
            _ => (Color)ColorConverter.ConvertFromString("#0078D4")
        };
    }

    /// <summary>
    /// 根据颜色名称获取轨道颜色
    /// </summary>
    public static Color GetTrackColor(ColorName colorName)
    {
        return colorName switch
        {
            ColorName.Blue => (Color)ColorConverter.ConvertFromString("#E3F2FD"),
            ColorName.Red => (Color)ColorConverter.ConvertFromString("#FFEBEE"),
            ColorName.Green => (Color)ColorConverter.ConvertFromString("#E8F5E9"),
            ColorName.Purple => (Color)ColorConverter.ConvertFromString("#F3E5F5"),
            ColorName.Orange => (Color)ColorConverter.ConvertFromString("#FFF3E0"),
            ColorName.Yellow => (Color)ColorConverter.ConvertFromString("#FFFDE7"),
            ColorName.Cyan => (Color)ColorConverter.ConvertFromString("#E0F7FA"),
            ColorName.Pink => (Color)ColorConverter.ConvertFromString("#FCE4EC"),
            _ => (Color)ColorConverter.ConvertFromString("#E6E6E6")
        };
    }
}
