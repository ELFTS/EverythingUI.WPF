using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingSlider : Slider
{
    static EverythingSlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingSlider), 
            new FrameworkPropertyMetadata(typeof(EverythingSlider)));
    }

    public static readonly DependencyProperty ThumbColorProperty = 
        DependencyProperty.Register(nameof(ThumbColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(Color.FromRgb(0, 172, 240)));

    public static readonly DependencyProperty TrackFillColorProperty = 
        DependencyProperty.Register(nameof(TrackFillColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(Color.FromRgb(0, 172, 240)));

    public static readonly DependencyProperty TrackBackgroundColorProperty = 
        DependencyProperty.Register(nameof(TrackBackgroundColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(Color.FromRgb(200, 200, 200)));

    public Color ThumbColor
    {
        get => (Color)GetValue(ThumbColorProperty);
        set => SetValue(ThumbColorProperty, value);
    }

    public Color TrackFillColor
    {
        get => (Color)GetValue(TrackFillColorProperty);
        set => SetValue(TrackFillColorProperty, value);
    }

    public Color TrackBackgroundColor
    {
        get => (Color)GetValue(TrackBackgroundColorProperty);
        set => SetValue(TrackBackgroundColorProperty, value);
    }
}
