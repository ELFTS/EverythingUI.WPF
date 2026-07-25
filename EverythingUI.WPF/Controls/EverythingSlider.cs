using System.Windows;
using System.Windows.Controls;

namespace EverythingUI.WPF.Controls;

public class EverythingSlider : Slider
{
    static EverythingSlider() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingSlider),
            new FrameworkPropertyMetadata(typeof(EverythingSlider)));
}
