using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EverythingUI.Demo.Views;

public partial class CircularProgressBarPage : UserControl
{
    private DispatcherTimer? _timer;
    private bool _isRunning = false;

    public CircularProgressBarPage()
    {
        InitializeComponent();
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };
        _timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (AnimatedCircularProgressBar.Value < 100)
        {
            AnimatedCircularProgressBar.Value += 1;
        }
        else
        {
            _timer?.Stop();
            _isRunning = false;
        }
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_isRunning && AnimatedCircularProgressBar.Value < 100)
        {
            _timer?.Start();
            _isRunning = true;
        }
    }

    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        _timer?.Stop();
        _isRunning = false;
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        _timer?.Stop();
        _isRunning = false;
        AnimatedCircularProgressBar.Value = 0;
    }
}
