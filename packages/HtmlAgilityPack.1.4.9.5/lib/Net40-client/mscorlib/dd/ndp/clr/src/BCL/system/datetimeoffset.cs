// Decompiled with JetBrains decompiler
// Type: System.DateTimeOffset
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>表示一个时间点，通常以相对于协调世界时 (UTC) 的日期和时间来表示。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  public struct DateTimeOffset : IComparable, IFormattable, ISerializable, IDeserializationCallback, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>
  {
    /// <summary>表示可能的最早 <see cref="T:System.DateTimeOffset" /> 值。此字段为只读。</summary>
    [__DynamicallyInvokable]
    public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);
    /// <summary>表示 <see cref="T:System.DateTimeOffset" /> 的最大可能值。此字段为只读。</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <see cref="F:System.DateTime.MaxValue" /> 位于当前或指定区域性的默认日历的范围之外。</exception>
    [__DynamicallyInvokable]
    public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);
    internal const long MaxOffset = 504000000000;
    internal const long MinOffset = -504000000000;
    private const long UnixEpochTicks = 621355968000000000;
    private const long UnixEpochSeconds = 62135596800;
    private const long UnixEpochMilliseconds = 62135596800000;
    private DateTime m_dateTime;
    private short m_offsetMinutes;

    /// <summary>获取一个 <see cref="T:System.DateTimeOffset" /> 对象，该对象设置为当前计算机上的当前日期和时间，偏移量设置为本地时间与协调世界时 (UTC) 之间的偏移量。</summary>
    /// <returns>一个 <see cref="T:System.DateTimeOffset" /> 对象，其日期和时间为当前的本地时间，其偏移量为本地时区与协调世界时 (UTC) 之间的偏移量。</returns>
    [__DynamicallyInvokable]
    public static DateTimeOffset Now
    {
      [__DynamicallyInvokable] get
      {
        return new DateTimeOffset(DateTime.Now);
      }
    }

    /// <summary>获取一个 <see cref="T:System.DateTimeOffset" /> 对象，其日期和时间设置为当前的协调世界时 (UTC) 日期和时间，其偏移量为 <see cref="F:System.TimeSpan.Zero" />。</summary>
    /// <returns>一个对象，其日期和时间为当前的协调世界时 (UTC)，其偏移量为 <see cref="F:System.TimeSpan.Zero" />。</returns>
    [__DynamicallyInvokable]
    public static DateTimeOffset UtcNow
    {
      [__DynamicallyInvokable] get
      {
        return new DateTimeOffset(DateTime.UtcNow);
      }
    }

    /// <summary>获取 <see cref="T:System.DateTime" /> 值，该值表示当前 <see cref="T:System.DateTimeOffset" /> 对象的日期和时间。</summary>
    /// <returns>当前的 <see cref="T:System.DateTimeOffset" /> 对象的日期和时间。</returns>
    [__DynamicallyInvokable]
    public DateTime DateTime
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime;
      }
    }

    /// <summary>获取一个 <see cref="T:System.DateTime" /> 值，该值表示当前 <see cref="T:System.DateTimeOffset" /> 对象的协调世界时 (UTC) 日期和时间。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的协调世界时 (UTC) 日期和时间。</returns>
    [__DynamicallyInvokable]
    public DateTime UtcDateTime
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.SpecifyKind(this.m_dateTime, DateTimeKind.Utc);
      }
    }

    /// <summary>获取 <see cref="T:System.DateTime" /> 值，该值表示当前 <see cref="T:System.DateTimeOffset" /> 对象的本地日期和时间。</summary>
    /// <returns>当前的 <see cref="T:System.DateTimeOffset" /> 对象的本地日期和时间。</returns>
    [__DynamicallyInvokable]
    public DateTime LocalDateTime
    {
      [__DynamicallyInvokable] get
      {
        return this.UtcDateTime.ToLocalTime();
      }
    }

    private DateTime ClockDateTime
    {
      get
      {
        return new DateTime((this.m_dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
      }
    }

    /// <summary>获取 <see cref="T:System.DateTime" /> 值，该值表示当前 <see cref="T:System.DateTimeOffset" /> 对象的日期组成部分。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 值，该值表示当前 <see cref="T:System.DateTimeOffset" /> 对象的日期组成部分。</returns>
    [__DynamicallyInvokable]
    public DateTime Date
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Date;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的月中的某一天。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的日组成部分，以 1 到 31 之间的一个值来表示。</returns>
    [__DynamicallyInvokable]
    public int Day
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Day;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的周中的某一天。</summary>
    /// <returns>用于指示当前 <see cref="T:System.DateTimeOffset" /> 对象的星期几的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public DayOfWeek DayOfWeek
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.DayOfWeek;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的年中的某一天。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的年中的某一天，以 1 到 366 之间的一个值来表示。</returns>
    [__DynamicallyInvokable]
    public int DayOfYear
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.DayOfYear;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的时间的小时组成部分。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的小时组成部分。此属性使用 24 小时制；值介于 0 到 23 之间。</returns>
    [__DynamicallyInvokable]
    public int Hour
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Hour;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的时间的毫秒组成部分。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的毫秒组成部分，以 0 到 999 之间的一个整数来表示。</returns>
    [__DynamicallyInvokable]
    public int Millisecond
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Millisecond;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的时间的分钟组成部分。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的分钟组成部分，以 0 到 59 之间的一个整数来表示。</returns>
    [__DynamicallyInvokable]
    public int Minute
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Minute;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期的月份组成部分。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的月份组成部分，以 1 到 12 之间的一个整数来表示。</returns>
    [__DynamicallyInvokable]
    public int Month
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Month;
      }
    }

    /// <summary>获取与协调世界时 (UTC) 之间的时间偏移量。</summary>
    /// <returns>当前的 <see cref="T:System.DateTimeOffset" /> 对象的时间值与协调世界时 (UTC) 之差。</returns>
    [__DynamicallyInvokable]
    public TimeSpan Offset
    {
      [__DynamicallyInvokable] get
      {
        return new TimeSpan(0, (int) this.m_offsetMinutes, 0);
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的时钟时间的秒组成部分。</summary>
    /// <returns>
    /// <see cref="T:System.DateTimeOffset" /> 对象的秒组成部分，以 0 到 59 之间的一个整数值来表示。</returns>
    [__DynamicallyInvokable]
    public int Second
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Second;
      }
    }

    /// <summary>获取计时周期数，此计时周期数表示时钟时间中当前 <see cref="T:System.DateTimeOffset" /> 对象的日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.DateTimeOffset" /> 对象的时钟时间中的计时周期数。</returns>
    [__DynamicallyInvokable]
    public long Ticks
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Ticks;
      }
    }

    /// <summary>获取表示当前 <see cref="T:System.DateTimeOffset" /> 对象的协调世界时 (UTC) 日期和时间的计时周期数。</summary>
    /// <returns>
    /// <see cref="T:System.DateTimeOffset" /> 对象的协调世界时 (UTC) 中的计时周期数。</returns>
    [__DynamicallyInvokable]
    public long UtcTicks
    {
      [__DynamicallyInvokable] get
      {
        return this.UtcDateTime.Ticks;
      }
    }

    /// <summary>获取当前 <see cref="T:System.DateTimeOffset" /> 对象的日时。</summary>
    /// <returns>表示当前日期自午夜以来的时间间隔。</returns>
    [__DynamicallyInvokable]
    public TimeSpan TimeOfDay
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.TimeOfDay;
      }
    }

    /// <summary>获取由当前 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期的年份组成部分。</summary>
    /// <returns>当前 <see cref="T:System.DateTimeOffset" /> 对象的年份组成部分，以 0 到 9999 之间的一个整数值来表示。</returns>
    [__DynamicallyInvokable]
    public int Year
    {
      [__DynamicallyInvokable] get
      {
        return this.ClockDateTime.Year;
      }
    }

    /// <summary>使用指定的计时周期数和偏移量初始化 <see cref="T:System.DateTimeOffset" /> 结构的新实例。</summary>
    /// <param name="ticks">一个日期和时间，以 0001 年 1 月 1 日午夜 12:00:00 以来所经历的以 100 纳秒为间隔的间隔数来表示。</param>
    /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
    /// <exception cref="T:System.ArgumentException">未采用整分钟数指定 <paramref name="offset" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <see cref="P:System.DateTimeOffset.UtcDateTime" /> 属性早于 <see cref="F:System.DateTimeOffset.MinValue" /> 或晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。- 或 -<paramref name="ticks" /> 小于 DateTimeOffset.MinValue.Ticks 或大于 DateTimeOffset.MaxValue.Ticks。- 或 -<paramref name="Offset" /> 小于 -14 小时或大于 14 小时。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(long ticks, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(ticks), offset);
    }

    /// <summary>使用指定的 <see cref="T:System.DateTime" /> 值初始化 <see cref="T:System.DateTimeOffset" /> 结构的新实例。</summary>
    /// <param name="dateTime">日期和时间。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">通过应用偏移量所产生的协调世界时 (UTC) 的日期和时间早于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -通过应用偏移量所产生的 UTC 日期和时间晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(DateTime dateTime)
    {
      TimeSpan offset = dateTime.Kind == DateTimeKind.Utc ? new TimeSpan(0L) : TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
    }

    /// <summary>使用指定的 <see cref="T:System.DateTime" /> 值和偏移量初始化 <see cref="T:System.DateTimeOffset" /> 结构的新实例。</summary>
    /// <param name="dateTime">日期和时间。</param>
    /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dateTime.Kind" /> 等于 <see cref="F:System.DateTimeKind.Utc" />，<paramref name="offset" /> 不等于零。- 或 -<paramref name="dateTime.Kind" /> 等于 <see cref="F:System.DateTimeKind.Local" />，<paramref name="offset" /> 不等于系统的本地时区的偏移量。- 或 -未采用整分钟数指定 <paramref name="offset" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 小于 -14 小时或大于 14 小时。- 或 -<see cref="P:System.DateTimeOffset.UtcDateTime" /> 小于 <see cref="F:System.DateTimeOffset.MinValue" /> 或大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(DateTime dateTime, TimeSpan offset)
    {
      if (dateTime.Kind == DateTimeKind.Local)
      {
        if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
          throw new ArgumentException(Environment.GetResourceString("Argument_OffsetLocalMismatch"), "offset");
      }
      else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetUtcMismatch"), "offset");
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
    }

    /// <summary>使用指定的年、月、日、小时、分钟、秒和偏移量初始化 <see cref="T:System.DateTimeOffset" /> 结构的新实例。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 并不表示整分钟数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于 1 或大于 9999。- 或 -<paramref name="month" /> 小于 1 或大于 12。- 或 -<paramref name="day" /> 小于 1 或大于 <paramref name="month" /> 中的天数。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于 0 或大于 59。- 或 -<paramref name="second" /> 小于 0 或大于 59。- 或 -<paramref name="offset" /> 小于 -14 小时或大于 14 小时。- 或 -<see cref="P:System.DateTimeOffset.UtcDateTime" /> 属性早于 <see cref="F:System.DateTimeOffset.MinValue" /> 或晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
    }

    /// <summary>使用指定的年、月、日、小时、分钟、秒、毫秒和偏移量初始化 <see cref="T:System.DateTimeOffset" /> 结构的新实例。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="millisecond">毫秒（0 到 999）。</param>
    /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 并不表示整分钟数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于 1 或大于 9999。- 或 -<paramref name="month" /> 小于 1 或大于 12。- 或 -<paramref name="day" /> 小于 1 或大于 <paramref name="month" /> 中的天数。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于 0 或大于 59。- 或 -<paramref name="second" /> 小于 0 或大于 59。- 或 -<paramref name="millisecond" /> 小于 0 或大于 999。- 或 -<paramref name="offset" /> 小于 -14 或大于 14。- 或 -<see cref="P:System.DateTimeOffset.UtcDateTime" /> 属性早于 <see cref="F:System.DateTimeOffset.MinValue" /> 或晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
    }

    /// <summary>用指定日历的指定年、月、日、小时、分钟、秒、毫秒和偏移量初始化 <see cref="T:System.DateTimeOffset" /> 结构的新实例。</summary>
    /// <param name="year">年。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="millisecond">毫秒（0 到 999）。</param>
    /// <param name="calendar">用于解释 <paramref name="year" />、<paramref name="month" /> 和 <paramref name="day" /> 的日历。</param>
    /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 并不表示整分钟数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="calendar" /> 不能为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于 <paramref name="calendar" /> 参数的 MinSupportedDateTime.Year 或大于 MaxSupportedDateTime.Year。- 或 -<paramref name="month" /> 小于或大于 <paramref name="calendar" /> 中 <paramref name="year" /> 的月份数。- 或 -<paramref name="day" /> 小于 1 或大于 <paramref name="month" /> 中的天数。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于 0 或大于 59。- 或 -<paramref name="second" /> 小于 0 或大于 59。- 或 -<paramref name="millisecond" /> 小于 0 或大于 999。- 或 -<paramref name="offset" /> 小于 -14 小时或大于 14 小时。- 或 -<paramref name="year" />、<paramref name="month" /> 和 <paramref name="day" /> 参数不能表示为日期和时间值。- 或 -<see cref="P:System.DateTimeOffset.UtcDateTime" /> 属性早于 <see cref="F:System.DateTimeOffset.MinValue" /> 或晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
    {
      this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
      this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
    }

    private DateTimeOffset(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.m_dateTime = (DateTime) info.GetValue("DateTime", typeof (DateTime));
      this.m_offsetMinutes = (short) info.GetValue("OffsetMinutes", typeof (short));
    }

    /// <summary>定义从 <see cref="T:System.DateTime" /> 对象到 <see cref="T:System.DateTimeOffset" /> 对象的隐式转换。</summary>
    /// <returns>被转换的对象。</returns>
    /// <param name="dateTime">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">通过应用偏移量所产生的协调世界时 (UTC) 的日期和时间早于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -通过应用偏移量所产生的 UTC 日期和时间晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public static implicit operator DateTimeOffset(DateTime dateTime)
    {
      return new DateTimeOffset(dateTime);
    }

    /// <summary>将指定的时间间隔与具有指定的日期和时间的 <see cref="T:System.DateTimeOffset" /> 对象相加，产生一个具有新的日期和时间的 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>一个对象，其值为 <paramref name="dateTimeTz" /> 与 <paramref name="timeSpan" /> 的值之和。</returns>
    /// <param name="dateTimeOffset">要向其加上时间间隔的对象。</param>
    /// <param name="timeSpan">待添加的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
    {
      return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
    }

    /// <summary>从指定的日期和时间减去指定的时间间隔，并生成新的日期和时间。</summary>
    /// <returns>一个对象，它等于 <paramref name="dateTimeOffset" /> 减 <paramref name="timeSpan" /> 的值。</returns>
    /// <param name="dateTimeOffset">要从其减去的日期和时间对象。</param>
    /// <param name="timeSpan">待减去的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" /> 或大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
    {
      return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
    }

    /// <summary>从一个 <see cref="T:System.DateTimeOffset" /> 对象中减去另一个对象并生成时间间隔。</summary>
    /// <returns>一个表示 <paramref name="left" /> 与 <paramref name="right" /> 之差的对象。</returns>
    /// <param name="left">被减数。</param>
    /// <param name="right">减数。</param>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime - right.UtcDateTime;
    }

    /// <summary>确定两个指定的 <see cref="T:System.DateTimeOffset" /> 对象是否表示同一时间点。</summary>
    /// <returns>如果两个 <see cref="T:System.DateTimeOffset" /> 对象具有相同的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime == right.UtcDateTime;
    }

    /// <summary>确定两个指定的 <see cref="T:System.DateTimeOffset" /> 对象是否表示不同的时间点。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 不具有相同的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime != right.UtcDateTime;
    }

    /// <summary>确定一个指定的 <see cref="T:System.DateTimeOffset" /> 对象是否小于另一个指定的 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>如果 <paramref name="left" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值早于 <paramref name="right" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator <(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime < right.UtcDateTime;
    }

    /// <summary>确定一个指定的 <see cref="T:System.DateTimeOffset" /> 对象是否小于另一个指定的 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>如果 <paramref name="left" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值早于 <paramref name="right" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime <= right.UtcDateTime;
    }

    /// <summary>确定一个指定的 <see cref="T:System.DateTimeOffset" /> 对象是否大于（或晚于）另一个指定的 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>如果 <paramref name="left" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值晚于 <paramref name="right" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator >(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime > right.UtcDateTime;
    }

    /// <summary>确定一个指定的 <see cref="T:System.DateTimeOffset" /> 对象是大于还是等于另一个指定的 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>如果 <paramref name="left" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值等于或晚于 <paramref name="right" /> 的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
    {
      return left.UtcDateTime >= right.UtcDateTime;
    }

    /// <summary>将当前 <see cref="T:System.DateTimeOffset" /> 对象的值转换为偏移量值所指定的日期和时间。</summary>
    /// <returns>一个对象，它等于原始的 <see cref="T:System.DateTimeOffset" /> 对象（也就是说，它们的 <see cref="M:System.DateTimeOffset.ToUniversalTime" /> 方法返回的时间点相同），但其 <see cref="P:System.DateTimeOffset.Offset" /> 属性设置为 <paramref name="offset" />。</returns>
    /// <param name="offset">
    /// <see cref="T:System.DateTimeOffset" /> 值所转换成的偏移量。</param>
    /// <exception cref="T:System.ArgumentException">所生成的 <see cref="T:System.DateTimeOffset" /> 对象的值 <see cref="P:System.DateTimeOffset.DateTime" /> 值早于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 对象的 <see cref="P:System.DateTimeOffset.DateTime" /> 值晚于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 小于 -14 小时。- 或 -<paramref name="offset" /> 大于 14 小时。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset ToOffset(TimeSpan offset)
    {
      return new DateTimeOffset((this.m_dateTime + offset).Ticks, offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将此实例的值加上指定的时间间隔的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="timeSpan" /> 所表示的时间间隔之和。</returns>
    /// <param name="timeSpan">一个 <see cref="T:System.TimeSpan" /> 对象，表示正时间间隔或负时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset Add(TimeSpan timeSpan)
    {
      return new DateTimeOffset(this.ClockDateTime.Add(timeSpan), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的天数整数和小数部分添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="days" /> 所表示的天数之和。</returns>
    /// <param name="days">由整数和小数部分组成的天数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddDays(double days)
    {
      return new DateTimeOffset(this.ClockDateTime.AddDays(days), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的整数和小数的小时数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="hours" /> 所表示的小时数之和。</returns>
    /// <param name="hours">由整数和小数部分组成的小时数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddHours(double hours)
    {
      return new DateTimeOffset(this.ClockDateTime.AddHours(hours), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的毫秒数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="milliseconds" /> 所表示的整毫秒数之和。</returns>
    /// <param name="milliseconds">由整数和小数部分组成的毫秒数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddMilliseconds(double milliseconds)
    {
      return new DateTimeOffset(this.ClockDateTime.AddMilliseconds(milliseconds), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的整数和小数的分钟数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="minutes" /> 所表示的分钟数之和。</returns>
    /// <param name="minutes">由整数和小数部分组成的分钟数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddMinutes(double minutes)
    {
      return new DateTimeOffset(this.ClockDateTime.AddMinutes(minutes), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的月数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="months" /> 所表示的月份数之和。</returns>
    /// <param name="months">整月份数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddMonths(int months)
    {
      return new DateTimeOffset(this.ClockDateTime.AddMonths(months), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的整数和小数秒数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="seconds" /> 所表示的秒数之和。</returns>
    /// <param name="seconds">由整数和小数部分组成的秒数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddSeconds(double seconds)
    {
      return new DateTimeOffset(this.ClockDateTime.AddSeconds(seconds), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的刻度数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="ticks" /> 所表示的计时周期数之和。</returns>
    /// <param name="ticks">以 100 纳秒为单位的计时周期数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddTicks(long ticks)
    {
      return new DateTimeOffset(this.ClockDateTime.AddTicks(ticks), this.Offset);
    }

    /// <summary>返回一个新<see cref="T:System.DateTimeOffset" />将指定的年数添加到此实例的值的对象。</summary>
    /// <returns>一个对象，其值为当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间与 <paramref name="years" /> 所表示的年数之和。</returns>
    /// <param name="years">年份数。此数值可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset AddYears(int years)
    {
      return new DateTimeOffset(this.ClockDateTime.AddYears(years), this.Offset);
    }

    /// <summary>对两个 <see cref="T:System.DateTimeOffset" /> 对象进行比较，并指明第一个对象是早于、等于还是晚于第二个对象。</summary>
    /// <returns>一个有符号的整数，它表示 <paramref name="first" /> 参数的值是早于、晚于还是等于 <paramref name="second" /> 参数的值，如下表所示。返回值含义小于零<paramref name="first" /> 早于 <paramref name="second" />。零<paramref name="first" /> 等于 <paramref name="second" />。大于零<paramref name="first" /> 晚于 <paramref name="second" />。</returns>
    /// <param name="first">要比较的第一个对象。</param>
    /// <param name="second">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static int Compare(DateTimeOffset first, DateTimeOffset second)
    {
      return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
    }

    [__DynamicallyInvokable]
    int IComparable.CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      if (!(obj is DateTimeOffset))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTimeOffset"));
      DateTime utcDateTime1 = ((DateTimeOffset) obj).UtcDateTime;
      DateTime utcDateTime2 = this.UtcDateTime;
      if (utcDateTime2 > utcDateTime1)
        return 1;
      return utcDateTime2 < utcDateTime1 ? -1 : 0;
    }

    /// <summary>将当前的 <see cref="T:System.DateTimeOffset" /> 对象与指定的 <see cref="T:System.DateTimeOffset" /> 对象进行比较，并指明当前对象是早于、等于还是晚于另一个 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>一个有符号的整数，它指明了当前的 <see cref="T:System.DateTimeOffset" /> 对象与 <paramref name="other" /> 之间的关系，如下表所示。返回值描述小于零当前的 <see cref="T:System.DateTimeOffset" /> 对象早于 <paramref name="other" />。零当前的 <see cref="T:System.DateTimeOffset" /> 对象与 <paramref name="other" /> 相同。大于零。当前的 <see cref="T:System.DateTimeOffset" /> 对象晚于 <paramref name="other" />。</returns>
    /// <param name="other">将与当前的 <see cref="T:System.DateTimeOffset" /> 对象进行比较的对象。</param>
    [__DynamicallyInvokable]
    public int CompareTo(DateTimeOffset other)
    {
      DateTime utcDateTime1 = other.UtcDateTime;
      DateTime utcDateTime2 = this.UtcDateTime;
      if (utcDateTime2 > utcDateTime1)
        return 1;
      return utcDateTime2 < utcDateTime1 ? -1 : 0;
    }

    /// <summary>确定 <see cref="T:System.DateTimeOffset" /> 对象是否与指定的对象表示同一时间点。</summary>
    /// <returns>如果 <paramref name="obj" /> 参数是 <see cref="T:System.DateTimeOffset" /> 对象，并且与当前的 <see cref="T:System.DateTimeOffset" /> 对象表示同一时间点，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前 <see cref="T:System.DateTimeOffset" /> 对象进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is DateTimeOffset)
        return this.UtcDateTime.Equals(((DateTimeOffset) obj).UtcDateTime);
      return false;
    }

    /// <summary>确定当前的 <see cref="T:System.DateTimeOffset" /> 对象是否与指定的 <see cref="T:System.DateTimeOffset" /> 对象表示同一时间点。</summary>
    /// <returns>如果两个 <see cref="T:System.DateTimeOffset" /> 对象具有相同的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前的 <see cref="T:System.DateTimeOffset" /> 对象进行比较的对象。   </param>
    [__DynamicallyInvokable]
    public bool Equals(DateTimeOffset other)
    {
      return this.UtcDateTime.Equals(other.UtcDateTime);
    }

    /// <summary>确定当前的 <see cref="T:System.DateTimeOffset" /> 对象与指定的 <see cref="T:System.DateTimeOffset" /> 对象是否表示同一时间并且是否具有相同的偏移量。</summary>
    /// <returns>如果当前的 <see cref="T:System.DateTimeOffset" /> 对象与 <paramref name="other" /> 具有相同的日期和时间值以及相同的 <see cref="P:System.DateTimeOffset.Offset" /> 值，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前 <see cref="T:System.DateTimeOffset" /> 对象进行比较的对象。 </param>
    [__DynamicallyInvokable]
    public bool EqualsExact(DateTimeOffset other)
    {
      if (this.ClockDateTime == other.ClockDateTime && this.Offset == other.Offset)
        return this.ClockDateTime.Kind == other.ClockDateTime.Kind;
      return false;
    }

    /// <summary>确定两个指定的 <see cref="T:System.DateTimeOffset" /> 对象是否表示同一时间点。</summary>
    /// <returns>如果这两个 <see cref="T:System.DateTimeOffset" /> 对象具有相同的 <see cref="P:System.DateTimeOffset.UtcDateTime" /> 值，则为 true；否则为 false。</returns>
    /// <param name="first">要比较的第一个对象。</param>
    /// <param name="second">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool Equals(DateTimeOffset first, DateTimeOffset second)
    {
      return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
    }

    /// <summary>将指定的 Windows 文件时间转换为等效的本地时间。</summary>
    /// <returns>一个对象，表示偏移量被设置为本地时间偏移量的 <paramref name="fileTime" /> 的日期和时间。</returns>
    /// <param name="fileTime">以计时周期表示的 Windows 文件时间。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="filetime" /> 小于零。- 或 -<paramref name="filetime" /> 大于 DateTimeOffset.MaxValue.Ticks。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset FromFileTime(long fileTime)
    {
      return new DateTimeOffset(DateTime.FromFileTime(fileTime));
    }

    /// <summary>将 Unix 时间表示为自 1970 年 1 已过去的秒数转换-01-到 01T00:00:00Z<see cref="T:System.DateTimeOffset" />值。</summary>
    /// <returns>日期和时间值，该值表示为 Unix 时间的时间在同一时刻。 </returns>
    /// <param name="seconds">一个 Unix 时间，它表示为从 1970-01-01T00:00:00Z（1970 年 1 月 1 日，UTC 时间上午 12:00）开始已经过的秒数。对于在此日期之前的 Unix 时间，其值为负数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="seconds" /> 小于 -62,135,596,800。- 或 -<paramref name="seconds" /> 大于 253,402,300,799。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
    {
      if (seconds < -62135596800L || seconds > 253402300799L)
        throw new ArgumentOutOfRangeException("seconds", string.Format(Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -62135596800L, (object) 253402300799L));
      return new DateTimeOffset(seconds * 10000000L + 621355968000000000L, TimeSpan.Zero);
    }

    /// <summary>将表示为自 1970 年 1 经过的毫秒数 Unix 时间转换为-01-到 01T00:00:00Z<see cref="T:System.DateTimeOffset" />值。</summary>
    /// <returns>日期和时间值，该值表示为 Unix 时间的时间在同一时刻。 </returns>
    /// <param name="milliseconds">一个 Unix 时间，它表示为从 1970-01-01T00:00:00Z（1970 年 1 月 1 日，UTC 时间上午 12:00）开始已经过的毫秒数。对于在此日期之前的 Unix 时间，其值为负数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="milliseconds" /> 小于 -62,135,596,800,000。- 或 -<paramref name="milliseconds" /> 大于 253,402,300,799,999。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
    {
      if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
        throw new ArgumentOutOfRangeException("milliseconds", string.Format(Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -62135596800000L, (object) 253402300799999L));
      return new DateTimeOffset(milliseconds * 10000L + 621355968000000000L, TimeSpan.Zero);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        this.m_offsetMinutes = DateTimeOffset.ValidateOffset(this.Offset);
        this.m_dateTime = DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
      }
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("DateTime", this.m_dateTime);
      info.AddValue("OffsetMinutes", this.m_offsetMinutes);
    }

    /// <summary>返回当前 <see cref="T:System.DateTimeOffset" /> 对象的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.UtcDateTime.GetHashCode();
    }

    /// <summary>将日期、时间和偏移量的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。</summary>
    /// <returns>一个对象，它等效于 <paramref name="input" /> 中包含的日期和时间。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <exception cref="T:System.ArgumentException">偏移量大于 14 小时或小于 -14 小时。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 中不包含有效的日期和时间的字符串表示形式。- 或 -<paramref name="input" /> 包含不带日期或时间的偏移量值的字符串表示形式。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset Parse(string input)
    {
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out offset).Ticks, offset);
    }

    /// <summary>使用指定的区域性特定格式信息，将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。</summary>
    /// <returns>一个对象，它等效于 <paramref name="input" /> 中包含的日期和时间，由 <paramref name="formatProvider" /> 指定。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定的格式信息。</param>
    /// <exception cref="T:System.ArgumentException">偏移量大于 14 小时或小于 -14 小时。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 中不包含有效的日期和时间的字符串表示形式。- 或 -<paramref name="input" /> 包含不带日期或时间的偏移量值的字符串表示形式。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset Parse(string input, IFormatProvider formatProvider)
    {
      return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
    }

    /// <summary>使用指定的区域性特定格式信息和格式设置样式将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。</summary>
    /// <returns>一个对象，它等效于 <paramref name="input" /> 中包含的日期和时间，由 <paramref name="formatProvider" /> 和 <paramref name="styles" /> 指定。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定的格式信息。</param>
    /// <param name="styles">枚举值的一个按位组合，指示 <paramref name="input" /> 所允许的格式。要指定的一个典型值为 <see cref="F:System.Globalization.DateTimeStyles.None" />。</param>
    /// <exception cref="T:System.ArgumentException">偏移量大于 14 小时或小于 -14 小时。- 或 -<paramref name="styles" /> 不是有效的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -<paramref name="styles" /> 包括不受支持的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -<paramref name="styles" /> 包括不能在一起使用的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 中不包含有效的日期和时间的字符串表示形式。- 或 -<paramref name="input" /> 包含不带日期或时间的偏移量值的字符串表示形式。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset Parse(string input, IFormatProvider formatProvider, DateTimeStyles styles)
    {
      styles = DateTimeOffset.ValidateStyles(styles, "styles");
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>一个对象，它等效于 <paramref name="input" /> 中包含的日期和时间，由 <paramref name="format" /> 和 <paramref name="formatProvider" /> 指定。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的格式说明符。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.ArgumentException">偏移量大于 14 小时或小于 -14 小时。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。- 或 -<paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 为空字符串 ("")。- 或 -<paramref name="input" /> 中不包含有效的日期和时间的字符串表示形式。- 或 -<paramref name="format" /> 是空字符串。- 或 -<paramref name="input" /> 中的小时组成部分和 AM/PM 指示符不一致。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider)
    {
      return DateTimeOffset.ParseExact(input, format, formatProvider, DateTimeStyles.None);
    }

    /// <summary>使用指定的格式、区域性特定的格式信息和样式将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>一个对象，它等效于 <paramref name="input" /> 参数中包含的日期和时间，由 <paramref name="format" />、<paramref name="formatProvider" /> 和 <paramref name="styles" /> 参数指定。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的格式说明符。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定格式设置信息。</param>
    /// <param name="styles">枚举值的一个按位组合，指示 <paramref name="input" /> 所允许的格式。</param>
    /// <exception cref="T:System.ArgumentException">偏移量大于 14 小时或小于 -14 小时。- 或 -<paramref name="styles" /> 参数包括不受支持的值。- 或 -<paramref name="styles" /> 参数包含不能在一起使用的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。- 或 -<paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 为空字符串 ("")。- 或 -<paramref name="input" /> 中不包含有效的日期和时间的字符串表示形式。- 或 -<paramref name="format" /> 是空字符串。- 或 -<paramref name="input" /> 中的小时组成部分和 AM/PM 指示符不一致。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles)
    {
      styles = DateTimeOffset.ValidateStyles(styles, "styles");
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
    }

    /// <summary>使用指定的格式、区域性特定格式信息和样式将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。字符串表示形式的格式必须与一种指定的格式完全匹配。</summary>
    /// <returns>一个对象，它等效于 <paramref name="input" /> 参数中包含的日期和时间，由 <paramref name="formats" />、<paramref name="formatProvider" /> 和 <paramref name="styles" /> 参数指定。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="formats">一个由格式说明符组成的数组，格式说明符用于定义 <paramref name="input" /> 的所需格式。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定格式设置信息。</param>
    /// <param name="styles">枚举值的一个按位组合，指示 <paramref name="input" /> 所允许的格式。</param>
    /// <exception cref="T:System.ArgumentException">偏移量大于 14 小时或小于 -14 小时。- 或 -<paramref name="styles" /> 包括不受支持的值。- 或 -<paramref name="styles" /> 参数包含不能在一起使用的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="input" /> 为空字符串 ("")。- 或 -<paramref name="input" /> 中不包含有效的日期和时间的字符串表示形式。- 或 -<paramref name="formats" /> 的元素不包含有效的格式说明符。- 或 -<paramref name="input" /> 中的小时组成部分和 AM/PM 指示符不一致。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles)
    {
      styles = DateTimeOffset.ValidateStyles(styles, "styles");
      TimeSpan offset;
      return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
    }

    /// <summary>从当前的 <see cref="T:System.DateTimeOffset" /> 对象中减去表示特定日期和时间的 <see cref="T:System.DateTimeOffset" /> 值。</summary>
    /// <returns>一个指定两个 <see cref="T:System.DateTimeOffset" /> 对象之间的间隔的对象。</returns>
    /// <param name="value">一个对象，表示要减去的值。</param>
    [__DynamicallyInvokable]
    public TimeSpan Subtract(DateTimeOffset value)
    {
      return this.UtcDateTime.Subtract(value.UtcDateTime);
    }

    /// <summary>从当前的 <see cref="T:System.DateTimeOffset" /> 对象中减去指定的时间间隔。</summary>
    /// <returns>一个对象，它等于当前的 <see cref="T:System.DateTimeOffset" /> 对象所表示的日期和时间减去 <paramref name="value" /> 所表示的时间间隔。</returns>
    /// <param name="value">待减去的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的 <see cref="T:System.DateTimeOffset" /> 值小于 <see cref="F:System.DateTimeOffset.MinValue" />。- 或 -所生成的 <see cref="T:System.DateTimeOffset" /> 值大于 <see cref="F:System.DateTimeOffset.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTimeOffset Subtract(TimeSpan value)
    {
      return new DateTimeOffset(this.ClockDateTime.Subtract(value), this.Offset);
    }

    /// <summary>将当前 <see cref="T:System.DateTimeOffset" /> 对象的值转换为 Windows 文件时间。</summary>
    /// <returns>用 Windows 文件时间来表示的当前 <see cref="T:System.DateTimeOffset" /> 对象的值。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的文件时间将表示协调世界时 (UTC) 公元 1601 年 1 月 1 日午夜之前的日期和时间协调世界时 (UTC)。</exception>
    [__DynamicallyInvokable]
    public long ToFileTime()
    {
      return this.UtcDateTime.ToFileTime();
    }

    /// <summary>返回自 1970 年 1 已过去的秒数-01-01T00:00:00Z。</summary>
    /// <returns>自 1970 年 1 已过去的秒数-01-01T00:00:00Z。</returns>
    [__DynamicallyInvokable]
    public long ToUnixTimeSeconds()
    {
      return this.UtcDateTime.Ticks / 10000000L - 62135596800L;
    }

    /// <summary>返回自 1970 年 1 经过的毫秒数-01-01T00:00:00.000Z。</summary>
    /// <returns>自 1970 年 1 经过的毫秒数-01-01T00:00:00.000Z。</returns>
    [__DynamicallyInvokable]
    public long ToUnixTimeMilliseconds()
    {
      return this.UtcDateTime.Ticks / 10000L - 62135596800000L;
    }

    /// <summary>将当前的 <see cref="T:System.DateTimeOffset" /> 对象转换为表示本地时间的 <see cref="T:System.DateTimeOffset" /> 对象。</summary>
    /// <returns>一个对象，表示当前的 <see cref="T:System.DateTimeOffset" /> 对象的日期和时间，已转换为本地时间。</returns>
    [__DynamicallyInvokable]
    public DateTimeOffset ToLocalTime()
    {
      return this.ToLocalTime(false);
    }

    internal DateTimeOffset ToLocalTime(bool throwOnOverflow)
    {
      return new DateTimeOffset(this.UtcDateTime.ToLocalTime(throwOnOverflow));
    }

    /// <summary>将当前 <see cref="T:System.DateTimeOffset" /> 对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>一个 <see cref="T:System.DateTimeOffset" /> 对象的字符串表示形式，并在字符串末尾追加了偏移量。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前区域性所使用的日历支持的日期范围之外的日期和时间。</exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return DateTimeFormat.Format(this.ClockDateTime, (string) null, DateTimeFormatInfo.CurrentInfo, this.Offset);
    }

    /// <summary>使用指定的格式将当前 <see cref="T:System.DateTimeOffset" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="format" />.</returns>
    /// <param name="format">一个格式字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 的长度是 1，并且它不是为 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 定义的标准格式说明符之一。- 或 -<paramref name="format" /> 中不包含有效的自定义格式模式。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前区域性所使用的日历支持的日期范围之外的日期和时间。</exception>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.CurrentInfo, this.Offset);
    }

    /// <summary>使用指定的区域性特定格式设置信息将当前 <see cref="T:System.DateTimeOffset" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>由 <paramref name="formatProvider" /> 指定的当前 <see cref="T:System.DateTimeOffset" /> 对象的值的字符串表示形式。</returns>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">日期和时间处于由 <paramref name="formatProvider" /> 使用的日历支持的日期范围之外。</exception>
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider formatProvider)
    {
      return DateTimeFormat.Format(this.ClockDateTime, (string) null, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
    }

    /// <summary>使用指定的格式和区域性特定格式信息将当前 <see cref="T:System.DateTimeOffset" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>由 <paramref name="format" /> 和 <paramref name="provider" /> 指定的当前 <see cref="T:System.DateTimeOffset" /> 对象的值的字符串表示形式。</returns>
    /// <param name="format">一个格式字符串。</param>
    /// <param name="formatProvider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 的长度是 1，并且它不是为 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 定义的标准格式说明符之一。- 或 -<paramref name="format" /> 中不包含有效的自定义格式模式。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">日期和时间处于由 <paramref name="formatProvider" /> 使用的日历支持的日期范围之外。</exception>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider formatProvider)
    {
      return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
    }

    /// <summary>将当前的 <see cref="T:System.DateTimeOffset" /> 对象转换为一个表示协调世界时 (UTC) 的 <see cref="T:System.DateTimeOffset" /> 值。</summary>
    /// <returns>一个对象，它表示转换为协调世界时 (UTC) 的当前 <see cref="T:System.DateTimeOffset" /> 对象的日期和时间。</returns>
    [__DynamicallyInvokable]
    public DateTimeOffset ToUniversalTime()
    {
      return new DateTimeOffset(this.UtcDateTime);
    }

    /// <summary>尝试将日期和时间的指定字符串表示形式转换为它的等效 <see cref="T:System.DateTimeOffset" />，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 <paramref name="input" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="input" /> 的日期和时间等效的 <see cref="T:System.DateTimeOffset" />；如果转换失败，则包含 <see cref="F:System.DateTimeOffset.MinValue" />。如果 <paramref name="input" /> 参数为 null，或者不包含日期和时间的有效字符串表示形式，则转换失败。此参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, out DateTimeOffset result)
    {
      DateTime result1;
      TimeSpan offset;
      int num = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result1, out offset) ? 1 : 0;
      result = new DateTimeOffset(result1.Ticks, offset);
      return num != 0;
    }

    /// <summary>尝试将日期和时间的指定字符串表示形式转换为它的等效 <see cref="T:System.DateTimeOffset" />，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 <paramref name="input" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定的格式设置信息。</param>
    /// <param name="styles">枚举值的一个按位组合，指示 <paramref name="input" /> 所允许的格式。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="input" /> 的日期和时间等效的 <see cref="T:System.DateTimeOffset" /> 值，如果转换失败，则为 <see cref="F:System.DateTimeOffset.MinValue" />。如果 <paramref name="input" /> 参数为 null，或者不包含日期和时间的有效字符串表示形式，则转换失败。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 包括未定义的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -不支持 <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />。- 或 -<paramref name="styles" /> 包括相互排斥的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。</exception>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
    {
      styles = DateTimeOffset.ValidateStyles(styles, "styles");
      DateTime result1;
      TimeSpan offset;
      int num = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out result1, out offset) ? 1 : 0;
      result = new DateTimeOffset(result1.Ticks, offset);
      return num != 0;
    }

    /// <summary>使用指定的格式、区域性特定的格式信息和样式将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>如果 <paramref name="input" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="format">用于定义所需的 <paramref name="input" /> 格式的格式说明符。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定格式设置信息。</param>
    /// <param name="styles">枚举值的一个按位组合，指示输入所允许的格式。要指定的一个典型值为 None。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="input" /> 的日期和时间等效的 <see cref="T:System.DateTimeOffset" />；如果转换失败，则包含 <see cref="F:System.DateTimeOffset.MinValue" />。如果 <paramref name="input" /> 参数为 null，或者该参数不包含 <paramref name="format" /> 和 <paramref name="provider" /> 所定义的所需格式的日期和时间的有效字符串表示形式，则转换失败。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 包括未定义的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -不支持 <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />。- 或 -<paramref name="styles" /> 包括相互排斥的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。</exception>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
    {
      styles = DateTimeOffset.ValidateStyles(styles, "styles");
      DateTime result1;
      TimeSpan offset;
      int num = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out result1, out offset) ? 1 : 0;
      result = new DateTimeOffset(result1.Ticks, offset);
      return num != 0;
    }

    /// <summary>使用指定的格式数组、区域性特定格式信息和样式，将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTimeOffset" />。字符串表示形式的格式必须与一种指定的格式完全匹配。</summary>
    /// <returns>如果 <paramref name="input" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="input">包含要转换的日期和时间的字符串。</param>
    /// <param name="formats">一个用于定义 <paramref name="input" /> 的所需格式的数组。</param>
    /// <param name="formatProvider">一个对象，提供有关 <paramref name="input" /> 的区域性特定格式设置信息。</param>
    /// <param name="styles">枚举值的一个按位组合，指示输入所允许的格式。要指定的一个典型值为 None。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="input" /> 的日期和时间等效的 <see cref="T:System.DateTimeOffset" />；如果转换失败，则包含 <see cref="F:System.DateTimeOffset.MinValue" />。如果 <paramref name="input" /> 不包含日期和时间的有效字符串表示形式，或者不包含 <paramref name="format" /> 所定义的所需格式的日期和时间，或者 <paramref name="formats" /> 为 null，则转换失败。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 包括未定义的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -不支持 <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />。- 或 -<paramref name="styles" /> 包括相互排斥的 <see cref="T:System.Globalization.DateTimeStyles" /> 值。</exception>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
    {
      styles = DateTimeOffset.ValidateStyles(styles, "styles");
      DateTime result1;
      TimeSpan offset;
      int num = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out result1, out offset) ? 1 : 0;
      result = new DateTimeOffset(result1.Ticks, offset);
      return num != 0;
    }

    private static short ValidateOffset(TimeSpan offset)
    {
      long ticks = offset.Ticks;
      if (ticks % 600000000L != 0L)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetPrecision"), "offset");
      if (ticks < -504000000000L || ticks > 504000000000L)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Argument_OffsetOutOfRange"));
      return (short) (offset.Ticks / 600000000L);
    }

    private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
    {
      long ticks = dateTime.Ticks - offset.Ticks;
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Argument_UTCOutOfRange"));
      return new DateTime(ticks, DateTimeKind.Unspecified);
    }

    private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
    {
      if ((style & ~(DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeStyles"), parameterName);
      if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeStyles"), parameterName);
      if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetInvalidDateTimeStyles"), parameterName);
      style &= ~DateTimeStyles.RoundtripKind;
      style &= ~DateTimeStyles.AssumeLocal;
      return style;
    }
  }
}
