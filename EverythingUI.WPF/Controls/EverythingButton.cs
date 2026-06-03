using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingButton : Button
{
    public new static readonly RoutedEvent ClickEvent =
        EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(MouseButtonEventHandler), typeof(EverythingButton));

    public new event MouseButtonEventHandler Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    static EverythingButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingButton),
            new FrameworkPropertyMetadata(typeof(EverythingButton)));
    }

    protected override void OnClick()
    {
        base.OnClick();
        RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
        {
            RoutedEvent = ClickEvent,
            Source = this
        });
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingButton),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingButton),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IconPlacementProperty =
        DependencyProperty.Register(nameof(IconPlacement), typeof(Dock), typeof(EverythingButton),
            new PropertyMetadata(Dock.Left));

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingButton),
            new PropertyMetadata(string.Empty));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public Dock IconPlacement
    {
        get => (Dock)GetValue(IconPlacementProperty);
        set => SetValue(IconPlacementProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
