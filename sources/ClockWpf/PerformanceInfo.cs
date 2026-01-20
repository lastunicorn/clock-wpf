// ClockNet
// Copyright (C) 2010 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Diagnostics;
using System.Text;

namespace DustInTheWind.ClockWpf;

public class PerformanceInfo
{
    private readonly Stopwatch stopwatch = new();

    public long MeasurementCount { get; private set; }

    public TimeSpan TotalTime { get; private set; }

    public TimeSpan LastTime { get; set; }

    public TimeSpan AverageTime => TotalTime / MeasurementCount;

    public event EventHandler Changed;

    public void Start()
    {
        stopwatch.Restart();
    }

    public void Stop()
    {
        stopwatch.Stop();

        MeasurementCount++;
        LastTime = stopwatch.Elapsed;
        TotalTime += LastTime;

        OnChanged();
    }

    protected virtual void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine("average: " + TimeSpan.FromTicks(AverageTime.Ticks).TotalMilliseconds + " ms");
        sb.AppendLine("instant: " + TimeSpan.FromTicks(LastTime.Ticks).TotalMilliseconds + " ms");
        sb.AppendLine("count: " + MeasurementCount);

        return sb.ToString();
    }
}
