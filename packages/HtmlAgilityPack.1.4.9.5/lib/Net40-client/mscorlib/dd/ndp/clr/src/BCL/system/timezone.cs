// Decompiled with JetBrains decompiler
// Type: System.TimeZone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
  /// <summary>表示时区。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public abstract class TimeZone
  {
    private static volatile TimeZone currentTimeZone;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (TimeZone.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, obj, (object) null);
        }
        return TimeZone.s_InternalSyncObject;
      }
    }

    /// <summary>获取当前计算机的时区。</summary>
    /// <returns>一个 <see cref="T:System.TimeZone" /> 对象，表示当前的本地时区。</returns>
    /// <filterpriority>1</filterpriority>
    public static TimeZone CurrentTimeZone
    {
      get
      {
        TimeZone timeZone = TimeZone.currentTimeZone;
        if (timeZone == null)
        {
          lock (TimeZone.InternalSyncObject)
          {
            if (TimeZone.currentTimeZone == null)
              TimeZone.currentTimeZone = (TimeZone) new CurrentSystemTimeZone();
            timeZone = TimeZone.currentTimeZone;
          }
        }
        return timeZone;
      }
    }

    /// <summary>获取标准时区名称。</summary>
    /// <returns>标准时区名称。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将该属性设置为 null。</exception>
    /// <filterpriority>2</filterpriority>
    public abstract string StandardName { get; }

    /// <summary>获取夏时制时区名称。</summary>
    /// <returns>夏时制时区名称。</returns>
    /// <filterpriority>2</filterpriority>
    public abstract string DaylightName { get; }

    internal static void ResetTimeZone()
    {
      if (TimeZone.currentTimeZone == null)
        return;
      lock (TimeZone.InternalSyncObject)
        TimeZone.currentTimeZone = (TimeZone) null;
    }

    /// <summary>返回指定本地时间的协调世界时 (UTC) 偏移量。</summary>
    /// <returns>与 <paramref name="time" /> 相比的协调世界时 (UTC) 偏移量。</returns>
    /// <param name="time">日期和时间值。</param>
    /// <filterpriority>2</filterpriority>
    public abstract TimeSpan GetUtcOffset(DateTime time);

    /// <summary>返回对应于指定时间的协调世界时 (UTC)。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 对象，其值为对应于 <paramref name="time" /> 的协调世界时 (UTC)。</returns>
    /// <param name="time">日期和时间。</param>
    /// <filterpriority>2</filterpriority>
    public virtual DateTime ToUniversalTime(DateTime time)
    {
      if (time.Kind == DateTimeKind.Utc)
        return time;
      long ticks = time.Ticks - this.GetUtcOffset(time).Ticks;
      if (ticks > 3155378975999999999L)
        return new DateTime(3155378975999999999L, DateTimeKind.Utc);
      if (ticks < 0L)
        return new DateTime(0L, DateTimeKind.Utc);
      return new DateTime(ticks, DateTimeKind.Utc);
    }

    /// <summary>返回对应于指定日期和时间值的本地时间。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 对象，其值为对应于 <paramref name="time" /> 的本地时间。</returns>
    /// <param name="time">协调世界时 (UTC) 时间。</param>
    /// <filterpriority>2</filterpriority>
    public virtual DateTime ToLocalTime(DateTime time)
    {
      if (time.Kind == DateTimeKind.Local)
        return time;
      bool isAmbiguousLocalDst = false;
      long fromUniversalTime = ((CurrentSystemTimeZone) TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref isAmbiguousLocalDst);
      return new DateTime(time.Ticks + fromUniversalTime, DateTimeKind.Local, isAmbiguousLocalDst);
    }

    /// <summary>返回特定年份的夏时制期间。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.DaylightTime" /> 对象，包含 <paramref name="year" /> 中夏时制的起始和结束日期。</returns>
    /// <param name="year">要应用夏时制期间的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于 1 或大于 9999。</exception>
    /// <filterpriority>2</filterpriority>
    public abstract DaylightTime GetDaylightChanges(int year);

    /// <summary>返回一个值，用以指示指定日期和时间是否处于夏时制期间。</summary>
    /// <returns>如果 <paramref name="time" /> 处于夏时制期间，则为 true；否则为 false。</returns>
    /// <param name="time">日期和时间。</param>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsDaylightSavingTime(DateTime time)
    {
      return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
    }

    /// <summary>返回一个值，用以指示指定日期和时间是否处于指定的夏时制期间。</summary>
    /// <returns>如果 <paramref name="time" /> 处于 <paramref name="daylightTimes" />，则为 true；否则为 false。</returns>
    /// <param name="time">日期和时间。</param>
    /// <param name="daylightTimes">夏时制期间。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="daylightTimes" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
    {
      return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
    }

    internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
    {
      if (daylightTimes == null || time.Kind == DateTimeKind.Utc)
        return TimeSpan.Zero;
      DateTime dateTime1 = daylightTimes.Start + daylightTimes.Delta;
      DateTime end = daylightTimes.End;
      DateTime dateTime2;
      DateTime dateTime3;
      if (daylightTimes.Delta.Ticks > 0L)
      {
        dateTime2 = end - daylightTimes.Delta;
        dateTime3 = end;
      }
      else
      {
        dateTime2 = dateTime1;
        dateTime3 = dateTime1 - daylightTimes.Delta;
      }
      bool flag = false;
      if (dateTime1 > end)
      {
        if (time >= dateTime1 || time < end)
          flag = true;
      }
      else if (time >= dateTime1 && time < end)
        flag = true;
      if (flag && time >= dateTime2 && time < dateTime3)
        flag = time.IsAmbiguousDaylightSavingTime();
      if (flag)
        return daylightTimes.Delta;
      return TimeSpan.Zero;
    }
  }
}
