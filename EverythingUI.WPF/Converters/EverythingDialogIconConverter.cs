using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EverythingUI.WPF.Converters;

[ValueConversion(typeof(Controls.EverythingDialogIcon), typeof(ImageSource))]
public sealed class EverythingDialogIconConverter : IValueConverter
{
    private const uint SHGSI_ICON = 0x00000100;
    private const uint SHGSI_LARGEICON = 0x00000000;

    private const uint SIID_HELP = 23;
    private const uint SIID_WARNING = 78;
    private const uint SIID_INFO = 79;
    private const uint SIID_ERROR = 80;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct SHSTOCKICONINFO
    {
        public uint cbSize;
        public IntPtr hIcon;
        public int iSysImageIndex;
        public int iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SHGetStockIconInfo(uint siid, uint uFlags, ref SHSTOCKICONINFO psii);

    [DllImport("user32.dll")]
    private static extern bool DestroyIcon(IntPtr hIcon);

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Controls.EverythingDialogIcon icon || icon == Controls.EverythingDialogIcon.None)
            return null;

        uint iconId = icon switch
        {
            Controls.EverythingDialogIcon.Information => SIID_INFO,
            Controls.EverythingDialogIcon.Warning => SIID_WARNING,
            Controls.EverythingDialogIcon.Error => SIID_ERROR,
            Controls.EverythingDialogIcon.Question => SIID_HELP,
            _ => 0
        };

        if (iconId == 0) return null;

        var info = new SHSTOCKICONINFO { cbSize = (uint)Marshal.SizeOf(typeof(SHSTOCKICONINFO)) };
        if (SHGetStockIconInfo(iconId, SHGSI_ICON | SHGSI_LARGEICON, ref info) != 0 || info.hIcon == IntPtr.Zero)
            return null;

        try
        {
            return Imaging.CreateBitmapSourceFromHIcon(info.hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        finally
        {
            DestroyIcon(info.hIcon);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
