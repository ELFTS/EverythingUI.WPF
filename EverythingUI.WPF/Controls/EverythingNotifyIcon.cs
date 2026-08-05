using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EverythingUI.WPF.Controls;

/// <summary>
/// 系统托盘图标控件。纯 P/Invoke 实现，不依赖 WinForms。
/// </summary>
public class EverythingNotifyIcon : FrameworkElement, IDisposable
{
    #region P/Invoke

    private const uint NIM_ADD = 0x00;
    private const uint NIM_MODIFY = 0x01;
    private const uint NIM_DELETE = 0x02;
    private const uint NIM_SETVERSION = 0x04;

    private const uint NIF_MESSAGE = 0x01;
    private const uint NIF_ICON = 0x02;
    private const uint NIF_TIP = 0x04;
    private const uint NIF_INFO = 0x10;
    private const uint NIF_SHOWTIP = 0x80;

    private const uint WM_USER = 0x0400;
    private const uint WM_LBUTTONUP = 0x0202;
    private const uint WM_RBUTTONUP = 0x0205;
    private const uint WM_LBUTTONDBLCLK = 0x0203;
    private const uint WM_RBUTTONDBLCLK = 0x0206;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NOTIFYICONDATAW
    {
        public int cbSize;
        public IntPtr hWnd;
        public uint uID;
        public uint uFlags;
        public uint uCallbackMessage;
        public IntPtr hIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szTip;
        public uint dwState;
        public uint dwStateMask;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szInfo;
        public uint uTimeout;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szInfoTitle;
        public uint dwInfoFlags;
        public Guid guidItem;
        public IntPtr hBalloonIcon;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ICONINFO
    {
        public bool fIcon;
        public uint xHotspot;
        public uint yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool Shell_NotifyIconW(uint dwMessage, ref NOTIFYICONDATAW lpData);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint RegisterWindowMessageW(string lpString);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint nPlanes, uint nBitCount, byte[]? lpBits);

    [DllImport("user32.dll")]
    private static extern IntPtr CreateIconIndirect(ref ICONINFO piconinfo);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyIcon(IntPtr hIcon);

    #endregion

    private HwndSource? _hwndSource;
    private IntPtr _hwndMessage;
    private uint _taskbarCreatedMessageId;
    private bool _iconAdded;
    private IntPtr _currentHicon;
    private bool _disposed;

    public EverythingNotifyIcon()
    {
        _taskbarCreatedMessageId = RegisterWindowMessageW("TaskbarCreated");
        Unloaded += (_, _) => RemoveIcon();
    }

    #region 依赖属性

    public static readonly DependencyProperty IconSourceProperty =
        DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(EverythingNotifyIcon),
            new FrameworkPropertyMetadata(null, OnIconSourceChanged));

    public ImageSource? IconSource
    {
        get => (ImageSource?)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    private static void OnIconSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingNotifyIcon icon) icon.UpdateIcon();
    }

    public static readonly DependencyProperty ToolTipTextProperty =
        DependencyProperty.Register(nameof(ToolTipText), typeof(string), typeof(EverythingNotifyIcon),
            new FrameworkPropertyMetadata(string.Empty, OnToolTipTextChanged));

    public string ToolTipText
    {
        get => (string)GetValue(ToolTipTextProperty);
        set => SetValue(ToolTipTextProperty, value);
    }

    private static void OnToolTipTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingNotifyIcon icon) icon.UpdateToolTip();
    }

    public static readonly DependencyProperty VisibleProperty =
        DependencyProperty.Register(nameof(Visible), typeof(bool), typeof(EverythingNotifyIcon),
            new FrameworkPropertyMetadata(false, OnVisibleChanged));

    public bool Visible
    {
        get => (bool)GetValue(VisibleProperty);
        set => SetValue(VisibleProperty, value);
    }

    private static void OnVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingNotifyIcon icon)
        {
            if ((bool)e.NewValue) icon.AddIcon();
            else icon.RemoveIcon();
        }
    }

    public static readonly DependencyProperty ContextMenuControlProperty =
        DependencyProperty.Register(nameof(ContextMenuControl), typeof(EverythingContextMenu), typeof(EverythingNotifyIcon),
            new FrameworkPropertyMetadata(null));

    public EverythingContextMenu? ContextMenuControl
    {
        get => (EverythingContextMenu?)GetValue(ContextMenuControlProperty);
        set => SetValue(ContextMenuControlProperty, value);
    }

    #endregion

    #region 事件

    /// <summary>鼠标点击托盘图标时触发（含左键/右键/双击）。</summary>
    public event EventHandler<NotifyIconMouseEventArgs>? MouseClick;

    /// <summary>鼠标双击托盘图标时触发。</summary>
    public event EventHandler<NotifyIconMouseEventArgs>? MouseDoubleClick;

    private void RaiseMouseClick(MouseButton button)
        => MouseClick?.Invoke(this, new NotifyIconMouseEventArgs(button));

    private void RaiseMouseDoubleClick(MouseButton button)
        => MouseDoubleClick?.Invoke(this, new NotifyIconMouseEventArgs(button));

    #endregion

    #region 气泡通知

    /// <summary>显示气泡通知。</summary>
    public void ShowBalloonTip(string title, string message, int timeoutMs = 5000)
    {
        if (!_iconAdded || _hwndMessage == IntPtr.Zero) return;

        var data = CreateNotifyIconData(NIF_INFO);
        data.szInfoTitle = title ?? string.Empty;
        data.szInfo = message ?? string.Empty;
        data.uTimeout = (uint)Math.Clamp(timeoutMs, 0, 30000);
        Shell_NotifyIconW(NIM_MODIFY, ref data);
    }

    #endregion

    #region 内部实现

    private void AddIcon()
    {
        if (_iconAdded) return;
        EnsureMessageWindow();
        if (_hwndMessage == IntPtr.Zero) return;

        UpdateHicon();
        var data = CreateNotifyIconData(NIF_MESSAGE | NIF_ICON | NIF_TIP | NIF_SHOWTIP);
        Shell_NotifyIconW(NIM_ADD, ref data);

        // 设置版本为 Vista+，支持新样式气泡
        data.uTimeout = 4; // NOTIFYICON_VERSION_4
        Shell_NotifyIconW(NIM_SETVERSION, ref data);

        _iconAdded = true;
    }

    private void RemoveIcon()
    {
        if (!_iconAdded) return;

        var data = new NOTIFYICONDATAW
        {
            cbSize = Marshal.SizeOf<NOTIFYICONDATAW>(),
            hWnd = _hwndMessage,
            uID = 1
        };
        Shell_NotifyIconW(NIM_DELETE, ref data);
        _iconAdded = false;

        DestroyCurrentHicon();
    }

    private void UpdateIcon()
    {
        if (!_iconAdded) return;
        UpdateHicon();
        var data = CreateNotifyIconData(NIF_ICON | NIF_TIP | NIF_SHOWTIP);
        Shell_NotifyIconW(NIM_MODIFY, ref data);
    }

    private void UpdateToolTip()
    {
        if (!_iconAdded) return;
        var data = CreateNotifyIconData(NIF_TIP | NIF_SHOWTIP);
        Shell_NotifyIconW(NIM_MODIFY, ref data);
    }

    private void UpdateHicon()
    {
        DestroyCurrentHicon();

        if (IconSource is BitmapSource bs)
            _currentHicon = CreateHiconFromBitmapSource(bs);
    }

    private void DestroyCurrentHicon()
    {
        if (_currentHicon != IntPtr.Zero)
        {
            DestroyIcon(_currentHicon);
            _currentHicon = IntPtr.Zero;
        }
    }

    private NOTIFYICONDATAW CreateNotifyIconData(uint flags)
    {
        return new NOTIFYICONDATAW
        {
            cbSize = Marshal.SizeOf<NOTIFYICONDATAW>(),
            hWnd = _hwndMessage,
            uID = 1,
            uFlags = flags,
            uCallbackMessage = WM_USER + 1,
            hIcon = _currentHicon,
            szTip = ToolTipText ?? string.Empty,
            dwState = 0,
            dwStateMask = 0
        };
    }

    private void EnsureMessageWindow()
    {
        if (_hwndSource != null) return;

        var parameters = new HwndSourceParameters("EverythingNotifyIconMessageWindow")
        {
            Width = 0,
            Height = 0,
            PositionX = 0,
            PositionY = 0,
            WindowStyle = 0,
            ExtendedWindowStyle = 0
        };

        _hwndSource = new HwndSource(parameters);
        _hwndSource.AddHook(WndProc);
        _hwndMessage = _hwndSource.Handle;
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        // Explorer 重启后重新添加图标
        if ((uint)msg == _taskbarCreatedMessageId && Visible)
        {
            _iconAdded = false;
            AddIcon();
            return IntPtr.Zero;
        }

        if ((uint)msg != WM_USER + 1) return IntPtr.Zero;

        var mouseMsg = (uint)(lParam.ToInt64() & 0xFFFF);
        handled = true;

        switch (mouseMsg)
        {
            case WM_LBUTTONUP:
                RaiseMouseClick(MouseButton.Left);
                break;
            case WM_RBUTTONUP:
                RaiseMouseClick(MouseButton.Right);
                ShowContextMenu();
                break;
            case WM_LBUTTONDBLCLK:
                RaiseMouseDoubleClick(MouseButton.Left);
                break;
            case WM_RBUTTONDBLCLK:
                RaiseMouseDoubleClick(MouseButton.Right);
                break;
        }

        return IntPtr.Zero;
    }

    private void ShowContextMenu()
    {
        if (ContextMenuControl == null) return;

        // 设置前台窗口，确保点击外部时菜单能自动关闭
        if (_hwndMessage != IntPtr.Zero)
            SetForegroundWindow(_hwndMessage);

        var target = Application.Current?.MainWindow;
        if (target != null)
            ContextMenuControl.Show(target);
    }

    private static IntPtr CreateHiconFromBitmapSource(BitmapSource source)
    {
        // 确保为 Pbgra32 格式
        if (source.Format != PixelFormats.Pbgra32)
            source = new FormatConvertedBitmap(source, PixelFormats.Pbgra32, null, 0);

        int width = source.PixelWidth;
        int height = source.PixelHeight;
        if (width <= 0 || height <= 0) return IntPtr.Zero;

        int stride = width * 4;
        var pixels = new byte[stride * height];
        source.CopyPixels(pixels, stride, 0);

        // 创建掩码位图（全零 = 完全不透明）
        IntPtr hbmMask = CreateBitmap(width, height, 1, 1, null);
        // 创建颜色位图（32bpp BGRA）
        IntPtr hbmColor = CreateBitmap(width, height, 1, 32, pixels);

        var iconInfo = new ICONINFO
        {
            fIcon = true,
            xHotspot = 0,
            yHotspot = 0,
            hbmMask = hbmMask,
            hbmColor = hbmColor
        };

        IntPtr hicon = CreateIconIndirect(ref iconInfo);

        DeleteObject(hbmMask);
        DeleteObject(hbmColor);

        return hicon;
    }

    #endregion

    #region IDisposable

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        RemoveIcon();

        if (_hwndSource != null)
        {
            _hwndSource.RemoveHook(WndProc);
            _hwndSource.Dispose();
            _hwndSource = null;
        }
    }

    #endregion
}

/// <summary>托盘图标鼠标事件参数。</summary>
public class NotifyIconMouseEventArgs : EventArgs
{
    public MouseButton Button { get; }
    public NotifyIconMouseEventArgs(MouseButton button) => Button = button;
}
