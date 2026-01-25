using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DustInTheWind.ClockWpf;

public class PerformaceView : Control
{
    private AnalogClock currentAnalogClock;
    private PerformanceInfo currentPerformanceInfo;
    private PropertyInfo performanceInfoProperty;

    static PerformaceView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PerformaceView), new FrameworkPropertyMetadata(typeof(PerformaceView)));
    }

    public PerformaceView()
    {
        ResetCommand = new RelayCommand(ExecuteReset, CanExecuteReset);
    }

    #region ResetCommand

    public ICommand ResetCommand { get; }

    private void ExecuteReset()
    {
        currentPerformanceInfo?.Reset();
    }

    private bool CanExecuteReset()
    {
        return currentPerformanceInfo != null;
    }

    #endregion

    #region AverageTime DependencyProperty

    public static readonly DependencyPropertyKey AverageTimePropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(AverageTime),
        typeof(TimeSpan),
        typeof(PerformaceView),
        new FrameworkPropertyMetadata(TimeSpan.Zero, FrameworkPropertyMetadataOptions.AffectsRender));

    public TimeSpan AverageTime
    {
        get => (TimeSpan)GetValue(AverageTimePropertyKey.DependencyProperty);
        private set => SetValue(AverageTimePropertyKey, value);
    }

    #endregion

    #region LastTime DependencyProperty

    public static readonly DependencyPropertyKey LastTimePropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(LastTime),
        typeof(TimeSpan),
        typeof(PerformaceView),
        new FrameworkPropertyMetadata(TimeSpan.Zero, FrameworkPropertyMetadataOptions.AffectsRender));

    public TimeSpan LastTime
    {
        get => (TimeSpan)GetValue(LastTimePropertyKey.DependencyProperty);
        private set => SetValue(LastTimePropertyKey, value);
    }

    #endregion

    #region MeasurementCount DependencyProperty

    public static readonly DependencyPropertyKey MeasurementCountPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(MeasurementCount),
        typeof(long),
        typeof(PerformaceView),
        new FrameworkPropertyMetadata((long)0, FrameworkPropertyMetadataOptions.AffectsRender));

    public long MeasurementCount
    {
        get => (long)GetValue(MeasurementCountPropertyKey.DependencyProperty);
        private set => SetValue(MeasurementCountPropertyKey, value);
    }

    #endregion

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property == DataContextProperty)
        {
            UnsubscribeFromCurrentPerformanceInfo();
            UnsubscribeFromCurrentAnalogClock();

            currentAnalogClock = null;
            currentPerformanceInfo = null;
            performanceInfoProperty = null;

            if (DataContext is AnalogClock analogClock)
            {
                currentAnalogClock = analogClock;
                performanceInfoProperty = analogClock.GetType().GetProperty("PerformanceInfo");

                if (performanceInfoProperty != null)
                {
                    SubscribeToAnalogClockPropertyChanges();
                    UpdatePerformanceInfo();
                }
            }
        }
    }

    private void SubscribeToAnalogClockPropertyChanges()
    {
        if (currentAnalogClock != null)
        {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromName(
                "PerformanceInfo",
                typeof(AnalogClock),
                typeof(AnalogClock));

            if (descriptor != null)
                descriptor.AddValueChanged(currentAnalogClock, OnAnalogClockPerformanceInfoChanged);
        }
    }

    private void UnsubscribeFromCurrentAnalogClock()
    {
        if (currentAnalogClock != null)
        {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromName(
                "PerformanceInfo",
                typeof(AnalogClock),
                typeof(AnalogClock));

            if (descriptor != null)
                descriptor.RemoveValueChanged(currentAnalogClock, OnAnalogClockPerformanceInfoChanged);
        }
    }

    private void OnAnalogClockPerformanceInfoChanged(object sender, EventArgs e)
    {
        UpdatePerformanceInfo();
    }

    private void UpdatePerformanceInfo()
    {
        UnsubscribeFromCurrentPerformanceInfo();

        if (performanceInfoProperty != null && currentAnalogClock != null)
        {
            currentPerformanceInfo = performanceInfoProperty.GetValue(currentAnalogClock) as PerformanceInfo;

            if (currentPerformanceInfo != null)
                currentPerformanceInfo.Changed += OnPerformanceInfoChanged;
        }
    }

    private void UnsubscribeFromCurrentPerformanceInfo()
    {
        if (currentPerformanceInfo != null)
            currentPerformanceInfo.Changed -= OnPerformanceInfoChanged;
    }

    private void OnPerformanceInfoChanged(object sender, EventArgs e)
    {
        AverageTime = currentPerformanceInfo.AverageTime;
        LastTime = currentPerformanceInfo.LastTime;
        MeasurementCount = currentPerformanceInfo.MeasurementCount;
    }
}