using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace EverythingUI.WPF.Controls;

public class EverythingOverlayDialog : ContentControl
{
    static EverythingOverlayDialog() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(typeof(EverythingOverlayDialog)));

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsOpenChanged));

    public static readonly DependencyProperty BlurRadiusProperty =
        DependencyProperty.Register(nameof(BlurRadius), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(18.0));

    public static readonly DependencyProperty DialogWidthProperty =
        DependencyProperty.Register(nameof(DialogWidth), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogMaxWidthProperty =
        DependencyProperty.Register(nameof(DialogMaxWidth), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(520.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogMaxHeightProperty =
        DependencyProperty.Register(nameof(DialogMaxHeight), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(720.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogPaddingProperty =
        DependencyProperty.Register(nameof(DialogPadding), typeof(Thickness), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(new Thickness(24), FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogCornerRadiusProperty =
        DependencyProperty.Register(nameof(DialogCornerRadius), typeof(CornerRadius), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(new CornerRadius(16)));

    public static readonly DependencyProperty SystemSoundProperty =
        DependencyProperty.Register(nameof(SystemSound), typeof(EverythingDialogSound), typeof(EverythingOverlayDialog),
            new PropertyMetadata(EverythingDialogSound.None));

    public static readonly DependencyProperty SystemIconProperty =
        DependencyProperty.Register(nameof(SystemIcon), typeof(EverythingDialogIcon), typeof(EverythingOverlayDialog),
            new PropertyMetadata(EverythingDialogIcon.None));

    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(48.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public bool IsOpen { get => (bool)GetValue(IsOpenProperty); set => SetValue(IsOpenProperty, value); }
    public double BlurRadius { get => (double)GetValue(BlurRadiusProperty); set => SetValue(BlurRadiusProperty, value); }
    public double DialogWidth { get => (double)GetValue(DialogWidthProperty); set => SetValue(DialogWidthProperty, value); }
    public double DialogMaxWidth { get => (double)GetValue(DialogMaxWidthProperty); set => SetValue(DialogMaxWidthProperty, value); }
    public double DialogMaxHeight { get => (double)GetValue(DialogMaxHeightProperty); set => SetValue(DialogMaxHeightProperty, value); }
    public Thickness DialogPadding { get => (Thickness)GetValue(DialogPaddingProperty); set => SetValue(DialogPaddingProperty, value); }
    public CornerRadius DialogCornerRadius { get => (CornerRadius)GetValue(DialogCornerRadiusProperty); set => SetValue(DialogCornerRadiusProperty, value); }
    public EverythingDialogSound SystemSound { get => (EverythingDialogSound)GetValue(SystemSoundProperty); set => SetValue(SystemSoundProperty, value); }
    public EverythingDialogIcon SystemIcon { get => (EverythingDialogIcon)GetValue(SystemIconProperty); set => SetValue(SystemIconProperty, value); }
    public double IconSize { get => (double)GetValue(IconSizeProperty); set => SetValue(IconSizeProperty, value); }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingOverlayDialog dialog && e.NewValue is true)
            dialog.PlaySystemSound();
    }

    private void PlaySystemSound()
    {
        switch (SystemSound)
        {
            case EverythingDialogSound.Asterisk: SystemSounds.Asterisk.Play(); break;
            case EverythingDialogSound.Beep: SystemSounds.Beep.Play(); break;
            case EverythingDialogSound.Exclamation: SystemSounds.Exclamation.Play(); break;
            case EverythingDialogSound.Hand: SystemSounds.Hand.Play(); break;
            case EverythingDialogSound.Question: SystemSounds.Question.Play(); break;
        }
    }
}
