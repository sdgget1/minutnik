using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace Minutnik;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private TimeSpan _remaining;
    private bool _isRunning;
    private bool _isWorkPhase = true;
    private int _cycles;

    public MainWindow()
    {
        InitializeComponent();

        _remaining = TimeSpan.FromMinutes(25);
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;

        UpdateDisplay();
    }

    private void OnStartPauseClick(object? sender, RoutedEventArgs e)
    {
        if (_isRunning)
        {
            _timer.Stop();
            _isRunning = false;
            StartPauseButton.Content = "Wznów";
            return;
        }

        if (!_isRunning && _remaining == GetPhaseDuration(_isWorkPhase))
        {
            _remaining = GetPhaseDuration(_isWorkPhase);
        }

        _timer.Start();
        _isRunning = true;
        StartPauseButton.Content = "Pauza";
        UpdateDisplay();
    }

    private void OnResetClick(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _isRunning = false;
        _isWorkPhase = true;
        _remaining = GetPhaseDuration(isWork: true);
        StartPauseButton.Content = "Start";
        UpdateDisplay();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (_remaining > TimeSpan.FromSeconds(1))
        {
            _remaining -= TimeSpan.FromSeconds(1);
            UpdateDisplay();
            return;
        }

        if (_isWorkPhase)
        {
            _cycles++;
            _isWorkPhase = false;
            _remaining = GetPhaseDuration(isWork: false);
            UpdateDisplay();
        }
        else
        {
            _timer.Stop();
            _isRunning = false;
            _isWorkPhase = true;
            _remaining = GetPhaseDuration(isWork: true);
            StartPauseButton.Content = "Start";
            UpdateDisplay();
        }
    }

    private TimeSpan GetPhaseDuration(bool isWork)
    {
        var input = isWork ? WorkMinutesInput.Text : BreakMinutesInput.Text;
        var fallback = isWork ? 25.0 : 5.0;

        if (double.TryParse(input?.Trim(), out var minutes) && minutes > 0)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        return TimeSpan.FromMinutes(fallback);
    }

    private void UpdateDisplay()
    {
        PhaseText.Text = _isWorkPhase ? "Praca" : "Przerwa";
        var totalMinutes = (int)Math.Floor(_remaining.TotalMinutes);
        TimerText.Text = $"{totalMinutes:D2}:{_remaining.Seconds:D2}";
        CyclesCountText.Text = $"Ukończone cykle: {_cycles}";
    }
}
