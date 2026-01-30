using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Performance;

public class PerformaceView : Control
{
    private PerformanceInfo currentPerformanceInfo;

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

    static PerformaceView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PerformaceView), new FrameworkPropertyMetadata(typeof(PerformaceView)));
    }

    public PerformaceView()
    {
        ResetCommand = new RelayCommand(ExecuteReset, CanExecuteReset);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property == DataContextProperty)
        {
            if (currentPerformanceInfo != null)
            {
                currentPerformanceInfo.Changed -= OnPerformanceInfoChanged;
                currentPerformanceInfo = null;
            }

            if (DataContext is PerformanceInfo performanceInfo)
            {
                currentPerformanceInfo = performanceInfo;

                if (currentPerformanceInfo != null)
                    currentPerformanceInfo.Changed += OnPerformanceInfoChanged;
            }
        }
    }

    private void OnPerformanceInfoChanged(object sender, EventArgs e)
    {
        AverageTime = currentPerformanceInfo.AverageTime;
        LastTime = currentPerformanceInfo.LastTime;
        MeasurementCount = currentPerformanceInfo.MeasurementCount;
    }
}