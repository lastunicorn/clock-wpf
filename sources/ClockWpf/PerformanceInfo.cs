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

    private long sessionCount;
    private long totalTicks;
    private long lastSessionTicks;

    public event EventHandler Changed;

    public void Start()
    {
        stopwatch.Restart();
    }

    public void Stop()
    {
        stopwatch.Stop();

        sessionCount++;
        lastSessionTicks = stopwatch.ElapsedTicks;
        totalTicks += lastSessionTicks;

        OnChanged();
    }

    protected virtual void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        long averageTicks = totalTicks / sessionCount;

        StringBuilder sb = new();

        sb.AppendLine("average: " + TimeSpan.FromTicks(averageTicks).TotalMilliseconds + " ms");
        sb.AppendLine("instant: " + TimeSpan.FromTicks(lastSessionTicks).TotalMilliseconds + " ms");
        sb.AppendLine("count: " + sessionCount);

        return sb.ToString();
    }
}
