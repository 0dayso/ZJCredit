// Decompiled with JetBrains decompiler
// Type: System.TimeSpan
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>表示一个时间间隔。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable
  {
    /// <summary>表示零 <see cref="T:System.TimeSpan" /> 值。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly TimeSpan Zero = new TimeSpan(0L);
    /// <summary>表示最大的 <see cref="T:System.TimeSpan" /> 值。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);
    /// <summary>表示最小的 <see cref="T:System.TimeSpan" /> 值。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);
    /// <summary>表示 1 毫秒的刻度数。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long TicksPerMillisecond = 10000;
    private const double MillisecondsPerTick = 0.0001;
    /// <summary>表示 1 秒的刻度数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long TicksPerSecond = 10000000;
    private const double SecondsPerTick = 1E-07;
    /// <summary>表示 1 分钟的刻度数。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long TicksPerMinute = 600000000;
    private const double MinutesPerTick = 1.66666666666667E-09;
    /// <summary>表示 1 小时的刻度数。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long TicksPerHour = 36000000000;
    private const double HoursPerTick = 2.77777777777778E-11;
    /// <summary>表示一天中的刻度数。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long TicksPerDay = 864000000000;
    private const double DaysPerTick = 1.15740740740741E-12;
    private const int MillisPerSecond = 1000;
    private const int MillisPerMinute = 60000;
    private const int MillisPerHour = 3600000;
    private const int MillisPerDay = 86400000;
    internal const long MaxSeconds = 922337203685;
    internal const long MinSeconds = -922337203685;
    internal const long MaxMilliSeconds = 922337203685477;
    internal const long MinMilliSeconds = -922337203685477;
    internal const long TicksPerTenthSecond = 1000000;
    internal long _ticks;
    private static volatile bool _legacyConfigChecked;
    private static volatile bool _legacyMode;

    /// <summary>获取表示当前 <see cref="T:System.TimeSpan" /> 结构的值的刻度数。</summary>
    /// <returns>此实例包含的刻度数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public long Ticks
    {
      [__DynamicallyInvokable] get
      {
        return this._ticks;
      }
    }

    /// <summary>获取当前 <see cref="T:System.TimeSpan" /> 结构所表示的时间间隔的天数部分。</summary>
    /// <returns>此实例的天数部分。返回值可以是正数也可以是负数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Days
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 864000000000L);
      }
    }

    /// <summary>获取当前 <see cref="T:System.TimeSpan" /> 结构所表示的时间间隔的小时数部分。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 结构的小时数部分。返回值的范围为 -23 到 23。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Hours
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 36000000000L % 24L);
      }
    }

    /// <summary>获取当前 <see cref="T:System.TimeSpan" /> 结构所表示的时间间隔的毫秒数部分。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 结构的毫秒数部分。返回值的范围为 -999 到 999。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Milliseconds
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 10000L % 1000L);
      }
    }

    /// <summary>获取当前 <see cref="T:System.TimeSpan" /> 结构所表示的时间间隔的分钟数部分。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 结构的分钟数部分。返回值的范围为 -59 到 59。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Minutes
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 600000000L % 60L);
      }
    }

    /// <summary>获取当前 <see cref="T:System.TimeSpan" /> 结构所表示的时间间隔的秒数部分。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 结构的秒数部分。返回值的范围为 -59 到 59。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Seconds
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this._ticks / 10000000L % 60L);
      }
    }

    /// <summary>获取以整天数和天的小数部分表示的当前 <see cref="T:System.TimeSpan" /> 结构的值。</summary>
    /// <returns>此实例表示的总天数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public double TotalDays
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 1.15740740740741E-12;
      }
    }

    /// <summary>获取以整小时数和小时的小数部分表示的当前 <see cref="T:System.TimeSpan" /> 结构的值。</summary>
    /// <returns>此实例表示的总小时数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public double TotalHours
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 2.77777777777778E-11;
      }
    }

    /// <summary>获取以整毫秒数和毫秒的小数部分表示的当前 <see cref="T:System.TimeSpan" /> 结构的值。</summary>
    /// <returns>此实例表示的总毫秒数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public double TotalMilliseconds
    {
      [__DynamicallyInvokable] get
      {
        double num = (double) this._ticks * 0.0001;
        if (num > 922337203685477.0)
          return 922337203685477.0;
        if (num < -922337203685477.0)
          return -922337203685477.0;
        return num;
      }
    }

    /// <summary>获取以整分钟数和分钟的小数部分表示的当前 <see cref="T:System.TimeSpan" /> 结构的值。</summary>
    /// <returns>此实例表示的总分钟数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public double TotalMinutes
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 1.66666666666667E-09;
      }
    }

    /// <summary>获取以整秒数和秒的小数部分表示的当前 <see cref="T:System.TimeSpan" /> 结构的值。</summary>
    /// <returns>此实例表示的总秒数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public double TotalSeconds
    {
      [__DynamicallyInvokable] get
      {
        return (double) this._ticks * 1E-07;
      }
    }

    private static bool LegacyMode
    {
      get
      {
        if (!TimeSpan._legacyConfigChecked)
        {
          TimeSpan._legacyMode = TimeSpan.GetLegacyFormatMode();
          TimeSpan._legacyConfigChecked = true;
        }
        return TimeSpan._legacyMode;
      }
    }

    /// <summary>将 <see cref="T:System.TimeSpan" /> 结构的新实例初始化为指定的刻度数。</summary>
    /// <param name="ticks">以 100 毫微秒为单位表示的时间段。</param>
    [__DynamicallyInvokable]
    public TimeSpan(long ticks)
    {
      this._ticks = ticks;
    }

    /// <summary>将 <see cref="T:System.TimeSpan" /> 结构的新实例初始化为指定的小时数、分钟数和秒数。</summary>
    /// <param name="hours">小时数。</param>
    /// <param name="minutes">分钟数。</param>
    /// <param name="seconds">秒数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该参数指定 <see cref="T:System.TimeSpan" /> 值小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public TimeSpan(int hours, int minutes, int seconds)
    {
      this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
    }

    /// <summary>将 <see cref="T:System.TimeSpan" /> 结构的新实例初始化为指定的天数、小时数、分钟数和秒数。</summary>
    /// <param name="days">天数。</param>
    /// <param name="hours">小时数。</param>
    /// <param name="minutes">分钟数。</param>
    /// <param name="seconds">秒数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该参数指定 <see cref="T:System.TimeSpan" /> 值小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public TimeSpan(int days, int hours, int minutes, int seconds)
    {
      this = new TimeSpan(days, hours, minutes, seconds, 0);
    }

    /// <summary>将 <see cref="T:System.TimeSpan" /> 结构的新实例初始化为指定的天数、小时数、分钟数、秒数和毫秒数。</summary>
    /// <param name="days">天数。</param>
    /// <param name="hours">小时数。</param>
    /// <param name="minutes">分钟数。</param>
    /// <param name="seconds">秒数。</param>
    /// <param name="milliseconds">毫秒数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该参数指定 <see cref="T:System.TimeSpan" /> 值小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
    {
      long num = ((long) days * 3600L * 24L + (long) hours * 3600L + (long) minutes * 60L + (long) seconds) * 1000L + (long) milliseconds;
      if (num > 922337203685477L || num < -922337203685477L)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      this._ticks = num * 10000L;
    }

    /// <summary>返回一个 <see cref="T:System.TimeSpan" />，它的值为这个指定实例的相反值。</summary>
    /// <returns>与此实例的数值相同，但符号相反的对象。</returns>
    /// <param name="t">要求反的时间间隔。</param>
    /// <exception cref="T:System.OverflowException">不能通过表示此实例的相反的值 <see cref="T:System.TimeSpan" />； 这就是为此实例的值是 <see cref="F:System.TimeSpan.MinValue" />。</exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(TimeSpan t)
    {
      if (t._ticks == TimeSpan.MinValue._ticks)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return new TimeSpan(-t._ticks);
    }

    /// <summary>从另一个指定的 <see cref="T:System.TimeSpan" /> 中减去指定的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>一个对象，其值是 <paramref name="t1" /> 的值减去 <paramref name="t2" /> 的值后所得的结果。</returns>
    /// <param name="t1">被减数。</param>
    /// <param name="t2">减数。</param>
    /// <exception cref="T:System.OverflowException">返回的值是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
    {
      return t1.Subtract(t2);
    }

    /// <summary>返回指定的 <see cref="T:System.TimeSpan" /> 的实例。</summary>
    /// <returns>
    /// <paramref name="t" /> 所指定的时间间隔。</returns>
    /// <param name="t">要返回的时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan operator +(TimeSpan t)
    {
      return t;
    }

    /// <summary>添加两个指定的 <see cref="T:System.TimeSpan" /> 实例。</summary>
    /// <returns>一个对象，其值为 <paramref name="t1" /> 与 <paramref name="t2" /> 的值之和。</returns>
    /// <param name="t1">要添加的第一个时间间隔。</param>
    /// <param name="t2">要添加的第二个时间间隔。</param>
    /// <exception cref="T:System.OverflowException">生成 <see cref="T:System.TimeSpan" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
    {
      return t1.Add(t2);
    }

    /// <summary>指示两个 <see cref="T:System.TimeSpan" /> 实例是否相等。</summary>
    /// <returns>如果 <paramref name="t1" /> 和 <paramref name="t2" /> 的值相等，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks == t2._ticks;
    }

    /// <summary>指示两个 <see cref="T:System.TimeSpan" /> 实例是否不相等。</summary>
    /// <returns>如果 <paramref name="t1" /> 和 <paramref name="t2" /> 的值不相等，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks != t2._ticks;
    }

    /// <summary>指示指定的 <see cref="T:System.TimeSpan" /> 是否小于另一个指定的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>如果 <paramref name="t1" /> 的值小于 <paramref name="t2" /> 的值，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator <(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks < t2._ticks;
    }

    /// <summary>指示指定的 <see cref="T:System.TimeSpan" /> 是否小于或等于另一个指定的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>如果 <paramref name="t1" /> 的值小于或等于 <paramref name="t2" /> 的值，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator <=(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks <= t2._ticks;
    }

    /// <summary>指示指定的 <see cref="T:System.TimeSpan" /> 是否大于另一个指定的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>如果 <paramref name="t1" /> 的值大于 <paramref name="t2" /> 的值，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator >(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks > t2._ticks;
    }

    /// <summary>指示指定的 <see cref="T:System.TimeSpan" /> 是否大于或等于另一个指定的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>如果 <paramref name="t1" /> 的值大于或等于 <paramref name="t2" /> 的值，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator >=(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks >= t2._ticks;
    }

    /// <summary>返回一个新的 <see cref="T:System.TimeSpan" /> 对象，其值为指定的 <see cref="T:System.TimeSpan" /> 对象与此实例的值之和。</summary>
    /// <returns>一个新对象，表示此实例的值加 <paramref name="ts" /> 的值。</returns>
    /// <param name="ts">待添加的时间间隔。</param>
    /// <exception cref="T:System.OverflowException">生成 <see cref="T:System.TimeSpan" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan Add(TimeSpan ts)
    {
      long ticks = this._ticks + ts._ticks;
      if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != ticks >> 63)
        throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return new TimeSpan(ticks);
    }

    /// <summary>比较两个 <see cref="T:System.TimeSpan" /> 值，并返回一个整数，该整数指示第一个值是短于、等于还是长于第二个值。</summary>
    /// <returns>以下值之一。值 描述 -1 <paramref name="t1" /> 小于 <paramref name="t2" />。0 <paramref name="t1" /> 等于 <paramref name="t2" />。1 <paramref name="t1" /> 长度超过 <paramref name="t2" />。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Compare(TimeSpan t1, TimeSpan t2)
    {
      if (t1._ticks > t2._ticks)
        return 1;
      return t1._ticks < t2._ticks ? -1 : 0;
    }

    /// <summary>将此实例与指定对象进行比较，并返回一个整数，该整数指示此实例是短于、等于还是长于指定对象。</summary>
    /// <returns>以下值之一。值 描述 -1 此实例小于 <paramref name="value" />。0 此实例等于 <paramref name="value" />。1 此实例的长度超过 <paramref name="value" />。- 或 - <paramref name="value" /> 为 null。</returns>
    /// <param name="value">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是 <see cref="T:System.TimeSpan" />。</exception>
    /// <filterpriority>1</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is TimeSpan))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeTimeSpan"));
      long num = ((TimeSpan) value)._ticks;
      if (this._ticks > num)
        return 1;
      return this._ticks < num ? -1 : 0;
    }

    /// <summary>将此实例与指定的 <see cref="T:System.TimeSpan" /> 对象进行比较，并返回一个整数，该整数指示此实例是短于、等于还是长于 <see cref="T:System.TimeSpan" /> 对象。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。值 描述 负整数 此实例小于 <paramref name="value" />。零 此实例等于 <paramref name="value" />。正整数 此实例的长度超过 <paramref name="value" />。</returns>
    /// <param name="value">要与此实例进行比较的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(TimeSpan value)
    {
      long num = value._ticks;
      if (this._ticks > num)
        return 1;
      return this._ticks < num ? -1 : 0;
    }

    /// <summary>返回表示指定天数的 <see cref="T:System.TimeSpan" />，其中对天数的指定精确到最接近的毫秒。</summary>
    /// <returns>表示 <paramref name="value" /> 的对象。</returns>
    /// <param name="value">天数，精确到最接近的毫秒。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.PositiveInfinity" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.NegativeInfinity" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan FromDays(double value)
    {
      return TimeSpan.Interval(value, 86400000);
    }

    /// <summary>返回新的 <see cref="T:System.TimeSpan" /> 对象，其值是当前 <see cref="T:System.TimeSpan" /> 对象的绝对值。</summary>
    /// <returns>一个新对象，其值是当前 <see cref="T:System.TimeSpan" /> 对象的绝对值。</returns>
    /// <exception cref="T:System.OverflowException">此实例的值是 <see cref="F:System.TimeSpan.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan Duration()
    {
      if (this.Ticks == TimeSpan.MinValue.Ticks)
        throw new OverflowException(Environment.GetResourceString("Overflow_Duration"));
      return new TimeSpan(this._ticks >= 0L ? this._ticks : -this._ticks);
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 <paramref name="value" /> 是表示与当前 <see cref="T:System.TimeSpan" /> 结构具有相同时间间隔的 <see cref="T:System.TimeSpan" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="value">与此实例进行比较的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (value is TimeSpan)
        return this._ticks == ((TimeSpan) value)._ticks;
      return false;
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的 <see cref="T:System.TimeSpan" /> 相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 表示的时间间隔与此实例相同，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(TimeSpan obj)
    {
      return this._ticks == obj._ticks;
    }

    /// <summary>返回一个值，该值指示 <see cref="T:System.TimeSpan" /> 的两个指定实例是否相等。</summary>
    /// <returns>如果 <paramref name="t1" /> 和 <paramref name="t2" /> 的值相等，则为 true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个时间间隔。</param>
    /// <param name="t2">要比较的第二个时间间隔。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Equals(TimeSpan t1, TimeSpan t2)
    {
      return t1._ticks == t2._ticks;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this._ticks ^ (int) (this._ticks >> 32);
    }

    /// <summary>返回表示指定小时数的 <see cref="T:System.TimeSpan" />，其中对小时数的指定精确到最接近的毫秒。</summary>
    /// <returns>表示 <paramref name="value" /> 的对象。</returns>
    /// <param name="value">精确到最接近的毫秒的小时数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.PositiveInfinity" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.NegativeInfinity" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan FromHours(double value)
    {
      return TimeSpan.Interval(value, 3600000);
    }

    private static TimeSpan Interval(double value, int scale)
    {
      if (double.IsNaN(value))
        throw new ArgumentException(Environment.GetResourceString("Arg_CannotBeNaN"));
      double num = value * (double) scale + (value >= 0.0 ? 0.5 : -0.5);
      if (num > 922337203685477.0 || num < -922337203685477.0)
        throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return new TimeSpan((long) num * 10000L);
    }

    /// <summary>返回表示指定毫秒数的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>表示 <paramref name="value" /> 的对象。</returns>
    /// <param name="value">毫秒数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.PositiveInfinity" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.NegativeInfinity" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan FromMilliseconds(double value)
    {
      return TimeSpan.Interval(value, 1);
    }

    /// <summary>返回表示指定分钟数的 <see cref="T:System.TimeSpan" />，其中对分钟数的指定精确到最接近的毫秒。</summary>
    /// <returns>表示 <paramref name="value" /> 的对象。</returns>
    /// <param name="value">分钟数，精确到最接近的毫秒。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.PositiveInfinity" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.NegativeInfinity" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan FromMinutes(double value)
    {
      return TimeSpan.Interval(value, 60000);
    }

    /// <summary>返回一个新的 <see cref="T:System.TimeSpan" /> 对象，它的值为这个实例的相反值。</summary>
    /// <returns>一个新对象，它与此实例的数值相同但符号相反。</returns>
    /// <exception cref="T:System.OverflowException">不能通过表示此实例的相反的值 <see cref="T:System.TimeSpan" />； 这就是为此实例的值是 <see cref="F:System.TimeSpan.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan Negate()
    {
      if (this.Ticks == TimeSpan.MinValue.Ticks)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return new TimeSpan(-this._ticks);
    }

    /// <summary>返回表示指定秒数的 <see cref="T:System.TimeSpan" />，其中对秒数的指定精确到最接近的毫秒。</summary>
    /// <returns>表示 <paramref name="value" /> 的对象。</returns>
    /// <param name="value">秒数，精确到最接近的毫秒。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.PositiveInfinity" />。- 或 -<paramref name="value" /> 为 <see cref="F:System.Double.NegativeInfinity" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan FromSeconds(double value)
    {
      return TimeSpan.Interval(value, 1000);
    }

    /// <summary>返回一个新的 <see cref="T:System.TimeSpan" /> 对象，其值为指定的 <see cref="T:System.TimeSpan" /> 对象与此实例的值之差。</summary>
    /// <returns>一个新的时间间隔，其值为此实例的值减去 <paramref name="ts" /> 的值所得的结果。</returns>
    /// <param name="ts">要减去的时间间隔。</param>
    /// <exception cref="T:System.OverflowException">返回的值是小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan Subtract(TimeSpan ts)
    {
      long ticks = this._ticks - ts._ticks;
      if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != ticks >> 63)
        throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return new TimeSpan(ticks);
    }

    /// <summary>返回表示指定时间的 <see cref="T:System.TimeSpan" />，其中对时间的指定以刻度为单位。</summary>
    /// <returns>表示 <paramref name="value" /> 的对象。</returns>
    /// <param name="value">表示时间的刻度数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan FromTicks(long value)
    {
      return new TimeSpan(value);
    }

    internal static long TimeToTicks(int hour, int minute, int second)
    {
      long num = (long) hour * 3600L + (long) minute * 60L + (long) second;
      if (num > 922337203685L || num < -922337203685L)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("Overflow_TimeSpanTooLong"));
      return num * 10000000L;
    }

    /// <summary>将时间间隔的字符串表示形式转换为等效的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>与 <paramref name="s" /> 对应的时间间隔。</returns>
    /// <param name="s">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 具有无效格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> 表示一个数字小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 - 天、 小时、 分钟或秒组件中至少一个是其有效范围之外。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan Parse(string s)
    {
      return TimeSpanParse.Parse(s, (IFormatProvider) null);
    }

    /// <summary>使用指定的区域性特定格式信息，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>与 <paramref name="input" /> 对应的时间间隔，由 <paramref name="formatProvider" /> 指定。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 具有无效格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="input" /> 表示一个数字小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 - 至少一个天、 小时、 分钟或中的秒组件 <paramref name="input" /> 其有效范围之外。</exception>
    [__DynamicallyInvokable]
    public static TimeSpan Parse(string input, IFormatProvider formatProvider)
    {
      return TimeSpanParse.Parse(input, formatProvider);
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>与 <paramref name="input" /> 对应的时间间隔，由 <paramref name="format" /> 和 <paramref name="formatProvider" /> 指定。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的标准或自定义格式字符串。</param>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 具有无效格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="input" /> 表示一个数字小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 - 至少一个天、 小时、 分钟或中的秒组件 <paramref name="input" /> 其有效范围之外。</exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
    {
      return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
    }

    /// <summary>使用指定的格式字符串数组和区域性特定格式信息，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />。字符串表示形式的格式必须与一种指定的格式完全匹配。</summary>
    /// <returns>与 <paramref name="input" /> 对应的时间间隔，由 <paramref name="formats" /> 和 <paramref name="formatProvider" /> 指定。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="formats">用于定义所需的 <paramref name="input" /> 格式的标准或自定义格式字符串的数组。</param>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 具有无效格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="input" /> 表示一个数字小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 - 至少一个天、 小时、 分钟或中的秒组件 <paramref name="input" /> 其有效范围之外。</exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
    {
      return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
    }

    /// <summary>使用指定的格式、区域性特定格式信息和样式，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>与 <paramref name="input" /> 对应的时间间隔，由 <paramref name="format" />、<paramref name="formatProvider" /> 和 <paramref name="styles" /> 指定。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的标准或自定义格式字符串。</param>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    /// <param name="styles">枚举值的按位组合，用于定义可出现在 <paramref name="input" /> 中的样式元素。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 是一个无效 <see cref="T:System.Globalization.TimeSpanStyles" /> 值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 具有无效格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="input" /> 表示一个数字小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 - 至少一个天、 小时、 分钟或中的秒组件 <paramref name="input" /> 其有效范围之外。</exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
    {
      TimeSpanParse.ValidateStyles(styles, "styles");
      return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
    }

    /// <summary>使用指定的格式、区域性特定格式信息和样式，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />。字符串表示形式的格式必须与一种指定的格式完全匹配。</summary>
    /// <returns>与 <paramref name="input" /> 对应的时间间隔，由 <paramref name="formats" />、<paramref name="formatProvider" /> 和 <paramref name="styles" /> 指定。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="formats">用于定义所需的 <paramref name="input" /> 格式的标准或自定义格式字符串的数组。</param>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    /// <param name="styles">枚举值的按位组合，用于定义可出现在 input 中的样式元素。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 是一个无效 <see cref="T:System.Globalization.TimeSpanStyles" /> 值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 具有无效格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="input" /> 表示一个数字小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />。- 或 - 至少一个天、 小时、 分钟或中的秒组件 <paramref name="input" /> 其有效范围之外。</exception>
    [__DynamicallyInvokable]
    public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
    {
      TimeSpanParse.ValidateStyles(styles, "styles");
      return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
    }

    /// <summary>将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />，格式无效，表示的时间间隔小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />，或者天、小时、分钟或秒分量中至少有一个超出其有效范围，则此运算返回 false。</returns>
    /// <param name="s">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="result">此方法返回时，包含表示由 <paramref name="s" /> 指定的时间间隔的对象；或者如果转换失败，则包含 <see cref="F:System.TimeSpan.Zero" />。此参数未经初始化即被传递。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out TimeSpan result)
    {
      return TimeSpanParse.TryParse(s, (IFormatProvider) null, out result);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="input" />；否则为 false。如果 <paramref name="input" /> 参数为 null 或 <see cref="F:System.String.Empty" />，格式无效，表示的时间间隔小于 <see cref="F:System.TimeSpan.MinValue" /> 或大于 <see cref="F:System.TimeSpan.MaxValue" />，或者天、小时、分钟或秒分量中至少有一个超出其有效范围，则此运算返回 false。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="result">此方法返回时，包含表示由 <paramref name="input" /> 指定的时间间隔的对象；或者如果转换失败，则包含 <see cref="F:System.TimeSpan.Zero" />。此参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
    {
      return TimeSpanParse.TryParse(input, formatProvider, out result);
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />，并返回一个指示转换是否成功的值。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="input" />；否则为 false。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的标准或自定义格式字符串。</param>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="result">此方法返回时，包含表示由 <paramref name="input" /> 指定的时间间隔的对象；或者如果转换失败，则包含 <see cref="F:System.TimeSpan.Zero" />。此参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out TimeSpan result)
    {
      return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将时间间隔的指定字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />，并返回一个指示转换是否成功的值。字符串表示形式的格式必须与一种指定的格式完全匹配。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="input" />；否则为 false。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="formats">用于定义可接受的 <paramref name="input" /> 格式的标准或自定义格式字符串的数组。</param>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    /// <param name="result">此方法返回时，包含表示由 <paramref name="input" /> 指定的时间间隔的对象；或者如果转换失败，则包含 <see cref="F:System.TimeSpan.Zero" />。此参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
    {
      return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
    }

    /// <summary>使用指定的格式、区域性特定格式信息和样式，将时间间隔的字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />，并返回一个指示转换是否成功的值。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="input" />；否则为 false。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的标准或自定义格式字符串。</param>
    /// <param name="formatProvider">一个对象，提供区域性特定的格式设置信息。</param>
    /// <param name="styles">用于指示 <paramref name="input" /> 的样式的一个或多个枚举值。</param>
    /// <param name="result">此方法返回时，包含表示由 <paramref name="input" /> 指定的时间间隔的对象；或者如果转换失败，则包含 <see cref="F:System.TimeSpan.Zero" />。此参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
    {
      TimeSpanParse.ValidateStyles(styles, "styles");
      return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
    }

    /// <summary>使用指定的格式、区域性特定格式信息和样式，将时间间隔的指定字符串表示形式转换为其等效的 <see cref="T:System.TimeSpan" />，并返回一个指示转换是否成功的值。字符串表示形式的格式必须与一种指定的格式完全匹配。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="input" />；否则为 false。</returns>
    /// <param name="input">一个字符串，用于指定进行转换的时间间隔。</param>
    /// <param name="formats">用于定义可接受的 <paramref name="input" /> 格式的标准或自定义格式字符串的数组。</param>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="styles">用于指示 <paramref name="input" /> 的样式的一个或多个枚举值。</param>
    /// <param name="result">此方法返回时，包含表示由 <paramref name="input" /> 指定的时间间隔的对象；或者如果转换失败，则包含 <see cref="F:System.TimeSpan.Zero" />。此参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
    {
      TimeSpanParse.ValidateStyles(styles, "styles");
      return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
    }

    /// <summary>将当前 <see cref="T:System.TimeSpan" /> 对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 值的字符串表示形式。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return TimeSpanFormat.Format(this, (string) null, (IFormatProvider) null);
    }

    /// <summary>使用指定的格式将当前 <see cref="T:System.TimeSpan" /> 对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 值的字符串表示形式，该值使用 <paramref name="format" /> 参数指定的格式。</returns>
    /// <param name="format">标准或自定义的 <see cref="T:System.TimeSpan" /> 格式字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 参数无法识别或不受支持。</exception>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return TimeSpanFormat.Format(this, format, (IFormatProvider) null);
    }

    /// <summary>使用指定的格式和区域性特定的格式设置信息，将当前 <see cref="T:System.TimeSpan" /> 对象的值转换为其等效字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.TimeSpan" /> 值的字符串表示形式，由 <paramref name="format" /> 和 <paramref name="formatProvider" /> 指定。</returns>
    /// <param name="format">标准或自定义的 <see cref="T:System.TimeSpan" /> 格式字符串。</param>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 参数无法识别或不受支持。</exception>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider formatProvider)
    {
      if (TimeSpan.LegacyMode)
        return TimeSpanFormat.Format(this, (string) null, (IFormatProvider) null);
      return TimeSpanFormat.Format(this, format, formatProvider);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool LegacyFormatMode();

    [SecuritySafeCritical]
    private static bool GetLegacyFormatMode()
    {
      if (TimeSpan.LegacyFormatMode())
        return true;
      return CompatibilitySwitches.IsNetFx40TimeSpanLegacyFormatMode;
    }
  }
}
