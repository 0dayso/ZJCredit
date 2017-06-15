// Decompiled with JetBrains decompiler
// Type: System.DateTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>表示时间上的一刻，通常以日期和当天的时间表示。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  public struct DateTime : IComparable, IFormattable, IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
  {
    private static readonly int[] DaysToMonth365 = new int[13]
    {
      0,
      31,
      59,
      90,
      120,
      151,
      181,
      212,
      243,
      273,
      304,
      334,
      365
    };
    private static readonly int[] DaysToMonth366 = new int[13]
    {
      0,
      31,
      60,
      91,
      121,
      152,
      182,
      213,
      244,
      274,
      305,
      335,
      366
    };
    /// <summary>表示 <see cref="T:System.DateTime" /> 的最小可能值。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);
    /// <summary>表示 <see cref="T:System.DateTime" /> 的最大可能值。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);
    private const long TicksPerMillisecond = 10000;
    private const long TicksPerSecond = 10000000;
    private const long TicksPerMinute = 600000000;
    private const long TicksPerHour = 36000000000;
    private const long TicksPerDay = 864000000000;
    private const int MillisPerSecond = 1000;
    private const int MillisPerMinute = 60000;
    private const int MillisPerHour = 3600000;
    private const int MillisPerDay = 86400000;
    private const int DaysPerYear = 365;
    private const int DaysPer4Years = 1461;
    private const int DaysPer100Years = 36524;
    private const int DaysPer400Years = 146097;
    private const int DaysTo1601 = 584388;
    private const int DaysTo1899 = 693593;
    internal const int DaysTo1970 = 719162;
    private const int DaysTo10000 = 3652059;
    internal const long MinTicks = 0;
    internal const long MaxTicks = 3155378975999999999;
    private const long MaxMillis = 315537897600000;
    private const long FileTimeOffset = 504911232000000000;
    private const long DoubleDateOffset = 599264352000000000;
    private const long OADateMinAsTicks = 31241376000000000;
    private const double OADateMinAsDouble = -657435.0;
    private const double OADateMaxAsDouble = 2958466.0;
    private const int DatePartYear = 0;
    private const int DatePartDayOfYear = 1;
    private const int DatePartMonth = 2;
    private const int DatePartDay = 3;
    private const ulong TicksMask = 4611686018427387903;
    private const ulong FlagsMask = 13835058055282163712;
    private const ulong LocalMask = 9223372036854775808;
    private const long TicksCeiling = 4611686018427387904;
    private const ulong KindUnspecified = 0;
    private const ulong KindUtc = 4611686018427387904;
    private const ulong KindLocal = 9223372036854775808;
    private const ulong KindLocalAmbiguousDst = 13835058055282163712;
    private const int KindShift = 62;
    private const string TicksField = "ticks";
    private const string DateDataField = "dateData";
    private ulong dateData;

    internal long InternalTicks
    {
      get
      {
        return (long) this.dateData & 4611686018427387903L;
      }
    }

    private ulong InternalKind
    {
      get
      {
        return this.dateData & 13835058055282163712UL;
      }
    }

    /// <summary>获取此实例的日期部分。</summary>
    /// <returns>一个新对象，其日期与此实例相同，时间值设置为午夜 12:00:00 (00:00:00)。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public DateTime Date
    {
      [__DynamicallyInvokable] get
      {
        long internalTicks = this.InternalTicks;
        long num1 = 864000000000;
        long num2 = internalTicks % num1;
        return new DateTime((ulong) (internalTicks - num2) | this.InternalKind);
      }
    }

    /// <summary>获取此实例所表示的日期为该月中的第几天。</summary>
    /// <returns>日组成部分，表示为 1 和 31 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Day
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(3);
      }
    }

    /// <summary>获取此实例所表示的日期是星期几。</summary>
    /// <returns>一个枚举常量，指示此 <see cref="T:System.DateTime" /> 值是星期几。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public DayOfWeek DayOfWeek
    {
      [__DynamicallyInvokable] get
      {
        return (DayOfWeek) ((this.InternalTicks / 864000000000L + 1L) % 7L);
      }
    }

    /// <summary>获取此实例所表示的日期是该年中的第几天。</summary>
    /// <returns>该年中的第几天，表示为 1 和 366 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int DayOfYear
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(1);
      }
    }

    /// <summary>获取此实例所表示日期的小时部分。</summary>
    /// <returns>小时组成部分，表示为 0 和 23 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Hour
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 36000000000L % 24L);
      }
    }

    /// <summary>获取一个值，该值指示由此实例表示的时间是基于本地时间、协调世界时 (UTC)，还是两者皆否。</summary>
    /// <returns>用于指示当前时间表示的含义的枚举值之一。默认值为 <see cref="F:System.DateTimeKind.Unspecified" />。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public DateTimeKind Kind
    {
      [__DynamicallyInvokable] get
      {
        switch (this.InternalKind)
        {
          case 0:
            return DateTimeKind.Unspecified;
          case 4611686018427387904:
            return DateTimeKind.Utc;
          default:
            return DateTimeKind.Local;
        }
      }
    }

    /// <summary>获取此实例所表示日期的毫秒部分。</summary>
    /// <returns>毫秒组成部分，表示为 0 和 999 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Millisecond
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 10000L % 1000L);
      }
    }

    /// <summary>获取此实例所表示日期的分钟部分。</summary>
    /// <returns>分钟组成部分，表示为 0 和 59 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Minute
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 600000000L % 60L);
      }
    }

    /// <summary>获取此实例所表示日期的月份部分。</summary>
    /// <returns>月组成部分，表示为 1 和 12 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Month
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(2);
      }
    }

    /// <summary>获取一个 <see cref="T:System.DateTime" /> 对象，该对象设置为此计算机上的当前日期和时间，表示为本地时间。</summary>
    /// <returns>其值为当前日期和时间的对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime Now
    {
      [__DynamicallyInvokable] get
      {
        DateTime utcNow = DateTime.UtcNow;
        bool isAmbiguousLocalDst = false;
        long ticks1 = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out isAmbiguousLocalDst).Ticks;
        long ticks2 = utcNow.Ticks + ticks1;
        if (ticks2 > 3155378975999999999L)
          return new DateTime(3155378975999999999L, DateTimeKind.Local);
        if (ticks2 < 0L)
          return new DateTime(0L, DateTimeKind.Local);
        return new DateTime(ticks2, DateTimeKind.Local, isAmbiguousLocalDst);
      }
    }

    /// <summary>获取一个 <see cref="T:System.DateTime" /> 对象，该对象设置为此计算机上的当前日期和时间，表示为协调通用时间 (UTC)。</summary>
    /// <returns>其值为当前 UTC 日期和时间的对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime UtcNow
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return new DateTime((ulong) (DateTime.GetSystemTimeAsFileTime() + 504911232000000000L | 4611686018427387904L));
      }
    }

    /// <summary>获取此实例所表示日期的秒部分。</summary>
    /// <returns>秒组成部分，表示为 0 和 59 之间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Second
    {
      [__DynamicallyInvokable] get
      {
        return (int) (this.InternalTicks / 10000000L % 60L);
      }
    }

    /// <summary>获取表示此实例的日期和时间的计时周期数。</summary>
    /// <returns>表示此实例的日期和时间的计时周期数。该值介于 DateTime.MinValue.Ticks 和 DateTime.MaxValue.Ticks 之间。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public long Ticks
    {
      [__DynamicallyInvokable] get
      {
        return this.InternalTicks;
      }
    }

    /// <summary>获取此实例的当天的时间。</summary>
    /// <returns>一个时间间隔，它表示当天自午夜以来已经过时间的部分。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan TimeOfDay
    {
      [__DynamicallyInvokable] get
      {
        return new TimeSpan(this.InternalTicks % 864000000000L);
      }
    }

    /// <summary>获取当前日期。</summary>
    /// <returns>一个对象，设置为当天日期，其时间组成部分设置为 00:00:00。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime Today
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.Now.Date;
      }
    }

    /// <summary>获取此实例所表示日期的年份部分。</summary>
    /// <returns>年份（介于 1 和 9999 之间）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Year
    {
      [__DynamicallyInvokable] get
      {
        return this.GetDatePart(0);
      }
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定的刻度数。</summary>
    /// <param name="ticks">一个日期和时间，以公历 0001 年 1 月 1 日 00:00:00.000 以来所经历的以 100 纳秒为间隔的间隔数来表示。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="ticks" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public DateTime(long ticks)
    {
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
      this.dateData = (ulong) ticks;
    }

    private DateTime(ulong dateData)
    {
      this.dateData = dateData;
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定的计时周期数以及协调世界时 (UTC) 或本地时间。</summary>
    /// <param name="ticks">一个日期和时间，以公历 0001 年 1 月 1 日 00:00:00.000 以来所经历的以 100 纳秒为间隔的间隔数来表示。</param>
    /// <param name="kind">枚举值之一，该值指示 <paramref name="ticks" /> 是指定了本地时间、协调世界时 (UTC)，还是两者皆未指定。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="ticks" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="kind" /> 不是之一 <see cref="T:System.DateTimeKind" /> 值。</exception>
    [__DynamicallyInvokable]
    public DateTime(long ticks, DateTimeKind kind)
    {
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
      if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
      this.dateData = (ulong) (ticks | (long) kind << 62);
    }

    internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
    {
      if (ticks < 0L || ticks > 3155378975999999999L)
        throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
      this.dateData = (ulong) (ticks | (isAmbiguousDst ? -4611686018427387904L : long.MinValue));
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定的年、月和日。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 是小于 1 或大于 9999。- 或 - <paramref name="month" /> 是小于 1 或大于 12。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。</exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day)
    {
      this.dateData = (ulong) DateTime.DateToTicks(year, month, day);
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定日历的指定年、月和日。</summary>
    /// <param name="year">年（1 到 <paramref name="calendar" /> 中的年数）。</param>
    /// <param name="month">月（1 到 <paramref name="calendar" /> 中的月数）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="calendar">用于解释 <paramref name="year" />、<paramref name="month" /> 和 <paramref name="day" /> 的日历。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="calendar" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 不支持的范围在 <paramref name="calendar" />。- 或 - <paramref name="month" /> 小于 1 或大于中的月数 <paramref name="calendar" />。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。</exception>
    public DateTime(int year, int month, int day, Calendar calendar)
    {
      this = new DateTime(year, month, day, 0, 0, 0, calendar);
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定的年、月、日、小时、分钟和秒。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 是小于 1 或大于 9999。- 或 - <paramref name="month" /> 是小于 1 或大于 12。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 为小于 0 或大于 23。- 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。</exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second)
    {
      this.dateData = (ulong) (DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定年、月、日、小时、分钟、秒和协调世界时 (UTC) 或本地时间。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="kind">枚举值之一，该值指示 <paramref name="year" />、<paramref name="month" />、<paramref name="day" />、<paramref name="hour" />、<paramref name="minute" /> 和 <paramref name="second" /> 指定了本地时间、协调世界时 (UTC)，还是两者皆未指定。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 是小于 1 或大于 9999。- 或 - <paramref name="month" /> 是小于 1 或大于 12。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 为小于 0 或大于 23。- 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="kind" /> 不是之一 <see cref="T:System.DateTimeKind" /> 值。</exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
    {
      if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
      this.dateData = (ulong) (DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) | (long) kind << 62);
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定日历的年、月、日、小时、分钟和秒。</summary>
    /// <param name="year">年（1 到 <paramref name="calendar" /> 中的年数）。</param>
    /// <param name="month">月（1 到 <paramref name="calendar" /> 中的月数）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="calendar">用于解释 <paramref name="year" />、<paramref name="month" /> 和 <paramref name="day" /> 的日历。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="calendar" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 不支持的范围在 <paramref name="calendar" />。- 或 - <paramref name="month" /> 小于 1 或大于中的月数 <paramref name="calendar" />。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 小于 0 或大于 23 - 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。</exception>
    public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
    {
      if (calendar == null)
        throw new ArgumentNullException("calendar");
      this.dateData = (ulong) calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定的年、月、日、小时、分钟、秒和毫秒。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="millisecond">毫秒（0 到 999）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 是小于 1 或大于 9999。- 或 - <paramref name="month" /> 是小于 1 或大于 12。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 为小于 0 或大于 23。- 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。- 或 - <paramref name="millisecond" /> 为小于 0 或大于 999。</exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
      this.dateData = (ulong) num;
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定年、月、日、小时、分钟、秒、毫秒和协调世界时 (UTC) 或本地时间。</summary>
    /// <param name="year">年（1 到 9999）。</param>
    /// <param name="month">月（1 到 12）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="millisecond">毫秒（0 到 999）。</param>
    /// <param name="kind">枚举值之一，该值指示 <paramref name="year" />、<paramref name="month" />、<paramref name="day" />, <paramref name="hour" />、<paramref name="minute" />、<paramref name="second" /> 和 <paramref name="millisecond" /> 指定了本地时间、协调世界时 (UTC)，还是两者皆未指定。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 是小于 1 或大于 9999。- 或 - <paramref name="month" /> 是小于 1 或大于 12。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 为小于 0 或大于 23。- 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。- 或 - <paramref name="millisecond" /> 为小于 0 或大于 999。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="kind" /> 不是之一 <see cref="T:System.DateTimeKind" /> 值。</exception>
    [__DynamicallyInvokable]
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
    {
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
      long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
      this.dateData = (ulong) (num | (long) kind << 62);
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定日历的指定年、月、日、小时、分钟、秒和毫秒。</summary>
    /// <param name="year">年（1 到 <paramref name="calendar" /> 中的年数）。</param>
    /// <param name="month">月（1 到 <paramref name="calendar" /> 中的月数）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="millisecond">毫秒（0 到 999）。</param>
    /// <param name="calendar">用于解释 <paramref name="year" />、<paramref name="month" /> 和 <paramref name="day" /> 的日历。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="calendar" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 不支持的范围在 <paramref name="calendar" />。- 或 - <paramref name="month" /> 小于 1 或大于中的月数 <paramref name="calendar" />。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 为小于 0 或大于 23。- 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。- 或 - <paramref name="millisecond" /> 为小于 0 或大于 999。</exception>
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
    {
      if (calendar == null)
        throw new ArgumentNullException("calendar");
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks + (long) millisecond * 10000L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
      this.dateData = (ulong) num;
    }

    /// <summary>将 <see cref="T:System.DateTime" /> 结构的新实例初始化为指定日历的指定年、月、日、小时、分钟、秒、毫秒和协调世界时 (UTC) 或本地时间。</summary>
    /// <param name="year">年（1 到 <paramref name="calendar" /> 中的年数）。</param>
    /// <param name="month">月（1 到 <paramref name="calendar" /> 中的月数）。</param>
    /// <param name="day">日（1 到 <paramref name="month" /> 中的天数）。</param>
    /// <param name="hour">小时（0 到 23）。</param>
    /// <param name="minute">分（0 到 59）。</param>
    /// <param name="second">秒（0 到 59）。</param>
    /// <param name="millisecond">毫秒（0 到 999）。</param>
    /// <param name="calendar">用于解释 <paramref name="year" />、<paramref name="month" /> 和 <paramref name="day" /> 的日历。</param>
    /// <param name="kind">枚举值之一，该值指示 <paramref name="year" />、<paramref name="month" />、<paramref name="day" />, <paramref name="hour" />、<paramref name="minute" />、<paramref name="second" /> 和 <paramref name="millisecond" /> 指定了本地时间、协调世界时 (UTC)，还是两者皆未指定。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="calendar" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 不支持的范围在 <paramref name="calendar" />。- 或 - <paramref name="month" /> 小于 1 或大于中的月数 <paramref name="calendar" />。- 或 - <paramref name="day" /> 小于 1 或大于的天数 <paramref name="month" />。- 或 - <paramref name="hour" /> 为小于 0 或大于 23。- 或 - <paramref name="minute" /> 为小于 0 或大于 59。- 或 - <paramref name="second" /> 为小于 0 或大于 59。- 或 - <paramref name="millisecond" /> 为小于 0 或大于 999。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="kind" /> 不是之一 <see cref="T:System.DateTimeKind" /> 值。</exception>
    public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
    {
      if (calendar == null)
        throw new ArgumentNullException("calendar");
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) 999));
      if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
      long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks + (long) millisecond * 10000L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
      this.dateData = (ulong) (num | (long) kind << 62);
    }

    private DateTime(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      bool flag1 = false;
      bool flag2 = false;
      long num1 = 0;
      ulong num2 = 0;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == "ticks"))
        {
          if (name == "dateData")
          {
            num2 = Convert.ToUInt64(enumerator.Value, (IFormatProvider) CultureInfo.InvariantCulture);
            flag2 = true;
          }
        }
        else
        {
          num1 = Convert.ToInt64(enumerator.Value, (IFormatProvider) CultureInfo.InvariantCulture);
          flag1 = true;
        }
      }
      if (flag2)
      {
        this.dateData = num2;
      }
      else
      {
        if (!flag1)
          throw new SerializationException(Environment.GetResourceString("Serialization_MissingDateTimeData"));
        this.dateData = (ulong) num1;
      }
      long internalTicks = this.InternalTicks;
      if (internalTicks < 0L || internalTicks > 3155378975999999999L)
        throw new SerializationException(Environment.GetResourceString("Serialization_DateTimeTicksOutOfRange"));
    }

    /// <summary>将指定的时间间隔加到指定的日期和时间以生成新的日期和时间。</summary>
    /// <returns>一个对象，它是 <paramref name="d" /> 和 <paramref name="t" /> 值的和。</returns>
    /// <param name="d">要添加的日期和时间值。</param>
    /// <param name="t">待添加的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime operator +(DateTime d, TimeSpan t)
    {
      long internalTicks = d.InternalTicks;
      long num = t._ticks;
      if (num > 3155378975999999999L - internalTicks || num < -internalTicks)
        throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks + num) | d.InternalKind);
    }

    /// <summary>从指定的日期和时间减去指定的时间间隔，返回新的日期和时间。</summary>
    /// <returns>一个对象，其值为 <paramref name="d" /> 的值减去 <paramref name="t" /> 的值。</returns>
    /// <param name="d">要从其中减去的日期和时间值。</param>
    /// <param name="t">待减去的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime operator -(DateTime d, TimeSpan t)
    {
      long internalTicks = d.InternalTicks;
      long num = t._ticks;
      if (internalTicks - 0L < num || internalTicks - 3155378975999999999L > num)
        throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks - num) | d.InternalKind);
    }

    /// <summary>将指定的日期和时间与另一个指定的日期和时间相减，返回一个时间间隔。</summary>
    /// <returns>
    /// <paramref name="d1" /> 和 <paramref name="d2" /> 之间的时间间隔；即 <paramref name="d1" /> 减去 <paramref name="d2" />。</returns>
    /// <param name="d1">要从中减去的日期和时间值（被减数）。</param>
    /// <param name="d2">要减去的日期和时间值（减数）。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static TimeSpan operator -(DateTime d1, DateTime d2)
    {
      return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
    }

    /// <summary>确定 <see cref="T:System.DateTime" /> 的两个指定的实例是否相等。</summary>
    /// <returns>如果 true 和 <paramref name="d1" /> 表示同一日期和时间，则为 <paramref name="d2" />；否则为 false。</returns>
    /// <param name="d1">要比较的第一个对象。</param>
    /// <param name="d2">要比较的第二个对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(DateTime d1, DateTime d2)
    {
      return d1.InternalTicks == d2.InternalTicks;
    }

    /// <summary>确定 <see cref="T:System.DateTime" /> 的两个指定的实例是否不等。</summary>
    /// <returns>如果 true 和 <paramref name="d1" /> 不表示同一日期和时间，则为 <paramref name="d2" />；否则为 false。</returns>
    /// <param name="d1">要比较的第一个对象。</param>
    /// <param name="d2">要比较的第二个对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(DateTime d1, DateTime d2)
    {
      return d1.InternalTicks != d2.InternalTicks;
    }

    /// <summary>确定指定的 <see cref="T:System.DateTime" /> 是否早于另一个指定的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>如果 true 早于 <paramref name="t1" />，则为 <paramref name="t2" />；否则为 false。</returns>
    /// <param name="t1">要比较的第一个对象。</param>
    /// <param name="t2">要比较的第二个对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator <(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks < t2.InternalTicks;
    }

    /// <summary>确定一个指定的 <see cref="T:System.DateTime" /> 表示的日期和时间等于还是早于另一个指定的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>如果 true 等于或晚于 <paramref name="t1" />，则为 <paramref name="t2" />；否则为 false。</returns>
    /// <param name="t1">要比较的第一个对象。</param>
    /// <param name="t2">要比较的第二个对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator <=(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks <= t2.InternalTicks;
    }

    /// <summary>确定指定的 <see cref="T:System.DateTime" /> 是否晚于另一个指定的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>如果 true 晚于 <paramref name="t1" />，则为 <paramref name="t2" />；否则为 false。</returns>
    /// <param name="t1">要比较的第一个对象。</param>
    /// <param name="t2">要比较的第二个对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator >(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks > t2.InternalTicks;
    }

    /// <summary>确定一个指定的 <see cref="T:System.DateTime" /> 表示的日期和时间等于还是晚于另一个指定的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>如果 true 等于或晚于 <paramref name="t1" />，则为 <paramref name="t2" />；否则为 false。</returns>
    /// <param name="t1">要比较的第一个对象。</param>
    /// <param name="t2">要比较的第二个对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator >=(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks >= t2.InternalTicks;
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定 <see cref="T:System.TimeSpan" /> 的值添加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的时间间隔之和。</returns>
    /// <param name="value">正或负时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime Add(TimeSpan value)
    {
      return this.AddTicks(value._ticks);
    }

    private DateTime Add(double value, int scale)
    {
      long num = (long) (value * (double) scale + (value >= 0.0 ? 0.5 : -0.5));
      if (num <= -315537897600000L || num >= 315537897600000L)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
      return this.AddTicks(num * 10000L);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的天数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的天数之和。</returns>
    /// <param name="value">由整数和小数部分组成的天数。<paramref name="value" /> 参数可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddDays(double value)
    {
      return this.Add(value, 86400000);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的小时数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的小时数之和。</returns>
    /// <param name="value">由整数和小数部分组成的小时数。<paramref name="value" /> 参数可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddHours(double value)
    {
      return this.Add(value, 3600000);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的毫秒数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的毫秒数之和。</returns>
    /// <param name="value">由整数和小数部分组成的毫秒数。<paramref name="value" /> 参数可以是负数也可以是正数。请注意，该值被舍入到最近的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddMilliseconds(double value)
    {
      return this.Add(value, 1);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的分钟数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的分钟数之和。</returns>
    /// <param name="value">由整数和小数部分组成的分钟数。<paramref name="value" /> 参数可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddMinutes(double value)
    {
      return this.Add(value, 60000);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的月数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="months" /> 所表示的时间之和。</returns>
    /// <param name="months">月份数。<paramref name="months" /> 参数可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。- 或 - <paramref name="months" /> 小于-120,000 或大于 120000。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddMonths(int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadMonths"));
      int datePart1 = this.GetDatePart(0);
      int datePart2 = this.GetDatePart(2);
      int day = this.GetDatePart(3);
      int num1 = datePart2 - 1 + months;
      int month;
      int year;
      if (num1 >= 0)
      {
        month = num1 % 12 + 1;
        year = datePart1 + num1 / 12;
      }
      else
      {
        month = 12 + (num1 + 1) % 12;
        year = datePart1 + (num1 - 11) / 12;
      }
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      int num2 = DateTime.DaysInMonth(year, month);
      if (day > num2)
        day = num2;
      return new DateTime((ulong) (DateTime.DateToTicks(year, month, day) + this.InternalTicks % 864000000000L) | this.InternalKind);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的秒数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的秒数之和。</returns>
    /// <param name="value">由整数和小数部分组成的秒数。<paramref name="value" /> 参数可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddSeconds(double value)
    {
      return this.Add(value, 1000);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的刻度数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的时间之和。</returns>
    /// <param name="value">以 100 纳秒为单位的计时周期数。<paramref name="value" /> 参数可以是正数也可以是负数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">生成 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddTicks(long value)
    {
      long internalTicks = this.InternalTicks;
      if (value > 3155378975999999999L - internalTicks || value < -internalTicks)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks + value) | this.InternalKind);
    }

    /// <summary>返回一个新的 <see cref="T:System.DateTime" />，它将指定的年份数加到此实例的值上。</summary>
    /// <returns>一个对象，其值是此实例所表示的日期和时间与 <paramref name="value" /> 所表示的年份数之和。</returns>
    /// <param name="value">年份数。<paramref name="value" /> 参数可以是负数也可以是正数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 生成或 <see cref="T:System.DateTime" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime AddYears(int value)
    {
      if (value < -10000 || value > 10000)
        throw new ArgumentOutOfRangeException("years", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadYears"));
      return this.AddMonths(value * 12);
    }

    /// <summary>对两个 <see cref="T:System.DateTime" /> 的实例进行比较，并返回一个指示第一个实例是早于、等于还是晚于第二个实例的整数。</summary>
    /// <returns>有符号数字，指示 <paramref name="t1" /> 和 <paramref name="t2" /> 的相对值。值类型 条件 小于零 <paramref name="t1" /> 早于 <paramref name="t2" />。零 <paramref name="t1" /> 与 <paramref name="t2" /> 相同。大于零 <paramref name="t1" /> 晚于 <paramref name="t2" />。</returns>
    /// <param name="t1">要比较的第一个对象。</param>
    /// <param name="t2">要比较的第二个对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Compare(DateTime t1, DateTime t2)
    {
      long internalTicks1 = t1.InternalTicks;
      long internalTicks2 = t2.InternalTicks;
      if (internalTicks1 > internalTicks2)
        return 1;
      return internalTicks1 < internalTicks2 ? -1 : 0;
    }

    /// <summary>将此实例的值与包含指定的 <see cref="T:System.DateTime" /> 值的指定对象相比较，并返回一个整数，该整数指示此实例是早于、等于还是晚于指定的 <see cref="T:System.DateTime" /> 值。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。值 描述 小于零 此实例早于 <paramref name="value" />。零 此实例与 <paramref name="value" /> 相同。大于零 此实例晚于 <paramref name="value" />，或 <paramref name="value" /> 为 null。</returns>
    /// <param name="value">要比较的装箱对象，或 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是 <see cref="T:System.DateTime" />。</exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is DateTime))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTime"));
      long internalTicks1 = ((DateTime) value).InternalTicks;
      long internalTicks2 = this.InternalTicks;
      if (internalTicks2 > internalTicks1)
        return 1;
      return internalTicks2 < internalTicks1 ? -1 : 0;
    }

    /// <summary>将此实例的值与指定的 <see cref="T:System.DateTime" /> 值相比较，并返回一个整数，该整数指示此实例是早于、等于还是晚于指定的 <see cref="T:System.DateTime" /> 值。</summary>
    /// <returns>有符号数字，指示此实例和 <paramref name="value" /> 参数的相对值。值 描述 小于零 此实例早于 <paramref name="value" />。零 此实例与 <paramref name="value" /> 相同。大于零 此实例晚于 <paramref name="value" />。</returns>
    /// <param name="value">要与当前类型进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(DateTime value)
    {
      long internalTicks1 = value.InternalTicks;
      long internalTicks2 = this.InternalTicks;
      if (internalTicks2 > internalTicks1)
        return 1;
      return internalTicks2 < internalTicks1 ? -1 : 0;
    }

    private static long DateToTicks(int year, int month, int day)
    {
      if (year >= 1 && year <= 9999 && (month >= 1 && month <= 12))
      {
        int[] numArray = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
        if (day >= 1 && day <= numArray[month] - numArray[month - 1])
        {
          int num = year - 1;
          return (long) (num * 365 + num / 4 - num / 100 + num / 400 + numArray[month - 1] + day - 1) * 864000000000L;
        }
      }
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    private static long TimeToTicks(int hour, int minute, int second)
    {
      if (hour >= 0 && hour < 24 && (minute >= 0 && minute < 60) && (second >= 0 && second < 60))
        return TimeSpan.TimeToTicks(hour, minute, second);
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
    }

    /// <summary>返回指定年和月中的天数。</summary>
    /// <returns>指定 <paramref name="month" /> 中 <paramref name="year" /> 中的天数。例如，如果 <paramref name="month" /> 等于 2（表示二月），则返回值为 28 或 29，具体取决于 <paramref name="year" /> 是否为闰年。</returns>
    /// <param name="year">年。</param>
    /// <param name="month">月（介于 1 到 12 之间的一个数字）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="month" /> 是小于 1 或大于 12。- 或 -<paramref name="year" /> 是小于 1 或大于 9999。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int DaysInMonth(int year, int month)
    {
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
      int[] numArray = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      return numArray[month] - numArray[month - 1];
    }

    internal static long DoubleDateToTicks(double value)
    {
      if (value >= 2958466.0 || value <= -657435.0)
        throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
      long num1 = (long) (value * 86400000.0 + (value >= 0.0 ? 0.5 : -0.5));
      if (num1 < 0L)
      {
        long num2 = num1;
        long num3 = 86400000;
        long num4 = num2 % num3 * 2L;
        num1 = num2 - num4;
      }
      long num5 = num1 + 59926435200000L;
      if (num5 < 0L || num5 >= 315537897600000L)
        throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateScale"));
      return num5 * 10000L;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool LegacyParseMode();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool EnableAmPmParseAdjustment();

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 true 是 <paramref name="value" /> 的实例并且等于此实例的值，则为 <see cref="T:System.DateTime" />；否则为 false。</returns>
    /// <param name="value">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (value is DateTime)
        return this.InternalTicks == ((DateTime) value).InternalTicks;
      return false;
    }

    /// <summary>返回一个值，该值指示此实例的值是否等于指定 <see cref="T:System.DateTime" /> 实例的值。</summary>
    /// <returns>如果 true 参数与此实例的值相等，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(DateTime value)
    {
      return this.InternalTicks == value.InternalTicks;
    }

    /// <summary>返回一个值，该值指示的两个 <see cref="T:System.DateTime" /> 实例是否具有同一个日期和时间值。</summary>
    /// <returns>如果两个值相等，则为，true；否则为 false。</returns>
    /// <param name="t1">要比较的第一个对象。</param>
    /// <param name="t2">要比较的第二个对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Equals(DateTime t1, DateTime t2)
    {
      return t1.InternalTicks == t2.InternalTicks;
    }

    /// <summary>反序列化一个 64 位二进制值，并重新创建序列化的 <see cref="T:System.DateTime" /> 初始对象。</summary>
    /// <returns>一个对象，它等效于由 <see cref="T:System.DateTime" /> 方法序列化的 <see cref="M:System.DateTime.ToBinary" /> 对象。</returns>
    /// <param name="dateData">64 位带符号整数，它对 2 位字段的 <see cref="P:System.DateTime.Kind" /> 属性以及 62 位字段的 <see cref="P:System.DateTime.Ticks" /> 属性进行了编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dateData" /> 是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime FromBinary(long dateData)
    {
      if ((dateData & long.MinValue) == 0L)
        return DateTime.FromBinaryRaw(dateData);
      long ticks1 = dateData & 4611686018427387903L;
      if (ticks1 > 4611685154427387904L)
        ticks1 -= 4611686018427387904L;
      bool isAmbiguousDst = false;
      long ticks2;
      if (ticks1 < 0L)
        ticks2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
      else if (ticks1 > 3155378975999999999L)
      {
        ticks2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
      }
      else
      {
        DateTime time = new DateTime(ticks1, DateTimeKind.Utc);
        bool flag = false;
        TimeZoneInfo local = TimeZoneInfo.Local;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        bool& isDaylightSavings = @flag;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        bool& isAmbiguousLocalDst = @isAmbiguousDst;
        ticks2 = TimeZoneInfo.GetUtcOffsetFromUtc(time, local, isDaylightSavings, isAmbiguousLocalDst).Ticks;
      }
      long ticks3 = ticks1 + ticks2;
      if (ticks3 < 0L)
        ticks3 += 864000000000L;
      if (ticks3 < 0L || ticks3 > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), "dateData");
      return new DateTime(ticks3, DateTimeKind.Local, isAmbiguousDst);
    }

    internal static DateTime FromBinaryRaw(long dateData)
    {
      long num = dateData & 4611686018427387903L;
      if (num < 0L || num > 3155378975999999999L)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), "dateData");
      return new DateTime((ulong) dateData);
    }

    /// <summary>将指定的 Windows 文件时间转换为等效的本地时间。</summary>
    /// <returns>一个表示本地时间的对象，等效于由 <paramref name="fileTime" /> 参数表示的日期和时间。</returns>
    /// <param name="fileTime">以计时周期表示的 Windows 文件时间。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="fileTime" /> 表示时间大于或小于 0 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime FromFileTime(long fileTime)
    {
      return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
    }

    /// <summary>将指定的 Windows 文件时间转换为等效的 UTC 时间。</summary>
    /// <returns>一个表示 UTC 时间的对象，等效于由 <paramref name="fileTime" /> 参数表示的日期和时间。</returns>
    /// <param name="fileTime">以计时周期表示的 Windows 文件时间。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="fileTime" /> 表示时间大于或小于 0 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime FromFileTimeUtc(long fileTime)
    {
      if (fileTime < 0L || fileTime > 2650467743999999999L)
        throw new ArgumentOutOfRangeException("fileTime", Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
      return new DateTime(fileTime + 504911232000000000L, DateTimeKind.Utc);
    }

    /// <summary>返回与指定的 OLE 自动化日期等效的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>一个对象，它表示与 <paramref name="d" /> 相同的日期和时间。</returns>
    /// <param name="d">OLE 自动化日期值。</param>
    /// <exception cref="T:System.ArgumentException">日期不是有效的 OLE 自动化日期值。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime FromOADate(double d)
    {
      return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("ticks", this.InternalTicks);
      info.AddValue("dateData", this.dateData);
    }

    /// <summary>指示此 <see cref="T:System.DateTime" /> 实例是否在当前时区的夏时制范围内。</summary>
    /// <returns>如果 true 属性的值为 <see cref="P:System.DateTime.Kind" /> 或 <see cref="F:System.DateTimeKind.Local" />，并且 <see cref="F:System.DateTimeKind.Unspecified" /> 的此实例的值在当前时区的夏时制范围内，则为 <see cref="T:System.DateTime" />；如果 false 为 <see cref="P:System.DateTime.Kind" />，则为 <see cref="F:System.DateTimeKind.Utc" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsDaylightSavingTime()
    {
      if (this.Kind == DateTimeKind.Utc)
        return false;
      return TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
    }

    /// <summary>创建新的 <see cref="T:System.DateTime" /> 对象，该对象具有与指定的 <see cref="T:System.DateTime" /> 相同的刻度数，但是根据指定的 <see cref="T:System.DateTimeKind" /> 值的指示，指定为本地时间或协调世界时 (UTC)，或者两者皆否。</summary>
    /// <returns>一个新对象，它与由 <paramref name="value" /> 参数和由 <see cref="T:System.DateTimeKind" /> 参数指定的 <paramref name="kind" /> 值代表的对象具有相同刻度数。</returns>
    /// <param name="value">日期和时间。</param>
    /// <param name="kind">枚举值之一，该值指示新对象是表示本地时间、UTC，还是两者皆否。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
    {
      return new DateTime(value.InternalTicks, kind);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象序列化为一个 64 位二进制值，该值随后可用于重新创建 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>64 位有符号整数，它对 <see cref="P:System.DateTime.Kind" /> 和 <see cref="P:System.DateTime.Ticks" /> 属性进行了编码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public long ToBinary()
    {
      if (this.Kind != DateTimeKind.Local)
        return (long) this.dateData;
      long num = this.Ticks - TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
      if (num < 0L)
        num = 4611686018427387904L + num;
      return num | long.MinValue;
    }

    internal long ToBinaryRaw()
    {
      return (long) this.dateData;
    }

    private int GetDatePart(int part)
    {
      int num1 = (int) (this.InternalTicks / 864000000000L);
      int num2 = num1 / 146097;
      int num3 = num1 - num2 * 146097;
      int num4 = num3 / 36524;
      if (num4 == 4)
        num4 = 3;
      int num5 = num3 - num4 * 36524;
      int num6 = num5 / 1461;
      int num7 = num5 - num6 * 1461;
      int num8 = num7 / 365;
      if (num8 == 4)
        num8 = 3;
      if (part == 0)
        return num2 * 400 + num4 * 100 + num6 * 4 + num8 + 1;
      int num9 = num7 - num8 * 365;
      if (part == 1)
        return num9 + 1;
      int[] numArray = (num8 != 3 ? 0 : (num6 != 24 ? 1 : (num4 == 3 ? 1 : 0))) != 0 ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      int index = num9 >> 6;
      while (num9 >= numArray[index])
        ++index;
      if (part == 2)
        return index;
      return num9 - numArray[index - 1] + 1;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      long internalTicks = this.InternalTicks;
      return (int) internalTicks ^ (int) (internalTicks >> 32);
    }

    internal bool IsAmbiguousDaylightSavingTime()
    {
      return (long) this.InternalKind == -4611686018427387904L;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern long GetSystemTimeAsFileTime();

    /// <summary>返回指定的年份是否为闰年的指示。</summary>
    /// <returns>如果 true 是闰年，则为 <paramref name="year" />；否则为 false。</returns>
    /// <param name="year">四位数年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 是小于 1 或大于 9999。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLeapYear(int year)
    {
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Year"));
      if (year % 4 != 0)
        return false;
      if (year % 100 == 0)
        return year % 400 == 0;
      return true;
    }

    /// <summary>将日期和时间的字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>一个对象，它等效于 <paramref name="s" /> 中包含的日期和时间。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不包含的有效字符串表示形式的日期和时间。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime Parse(string s)
    {
      return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将日期和时间的字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>一个对象，它等效于 <paramref name="s" /> 中包含的日期和时间，由 <paramref name="provider" /> 指定。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不包含的有效字符串表示形式的日期和时间。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime Parse(string s, IFormatProvider provider)
    {
      return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
    }

    /// <summary>使用指定的区域性特定格式设置信息和格式类型，将日期和时间的字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>一个对象，它等效于 <paramref name="s" /> 中包含的日期和时间，由 <paramref name="provider" /> 和 <paramref name="styles" /> 指定。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <param name="styles">枚举值的按位组合，用于指示 <paramref name="s" /> 成功执行分析操作所需的样式元素以及定义如何根据当前时区或当前日期解释已分析日期的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.DateTimeStyles.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不包含的有效字符串表示形式的日期和时间。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 包含无效的组件组合 <see cref="T:System.Globalization.DateTimeStyles" /> 值。例如，同时 <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> 和 <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
    {
      DateTimeFormatInfo.ValidateStyles(styles, "styles");
      return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。字符串表示形式的格式必须与指定的格式完全匹配。</summary>
    /// <returns>一个对象，它等效于 <paramref name="s" /> 中包含的日期和时间，由 <paramref name="format" /> 和 <paramref name="provider" /> 指定。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="format">用于定义所需的 <paramref name="s" /> 格式的格式说明符。有关详细信息，请参阅“备注”部分。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 或 <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 或 <paramref name="format" /> 为空字符串。- 或 - <paramref name="s" /> 不包含日期和时间中指定的模式与对应 <paramref name="format" />。- 或 -小时部分和 AM/PM 指示符在 <paramref name="s" /> 不一致。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ParseExact(string s, string format, IFormatProvider provider)
    {
      return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
    }

    /// <summary>使用指定的格式、区域性特定的格式信息和样式将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。字符串表示形式的格式必须与指定的格式完全匹配，否则会引发异常。</summary>
    /// <returns>一个对象，它等效于 <paramref name="s" /> 中包含的日期和时间，由 <paramref name="format" />、<paramref name="provider" /> 和 <paramref name="style" /> 指定。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="format">用于定义所需的 <paramref name="s" /> 格式的格式说明符。有关详细信息，请参阅“备注”部分。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <param name="style">枚举值的按位组合，提供有关以下内容的附加信息：<paramref name="s" />、可能出现在 <paramref name="s" /> 中的样式元素或从 <paramref name="s" /> 到 <see cref="T:System.DateTime" /> 值的转换。要指定的一个典型值为 <see cref="F:System.Globalization.DateTimeStyles.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 或 <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 或 <paramref name="format" /> 为空字符串。- 或 - <paramref name="s" /> 不包含日期和时间中指定的模式与对应 <paramref name="format" />。- 或 -小时部分和 AM/PM 指示符在 <paramref name="s" /> 不一致。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> 包含无效的组件组合 <see cref="T:System.Globalization.DateTimeStyles" /> 值。例如，同时 <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> 和 <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
    {
      DateTimeFormatInfo.ValidateStyles(style, "style");
      return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
    }

    /// <summary>使用指定的格式数组、区域性特定格式信息和样式，将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。字符串表示形式的格式必须至少与指定的格式之一完全匹配，否则会引发异常。</summary>
    /// <returns>一个对象，它等效于 <paramref name="s" /> 中包含的日期和时间，由 <paramref name="formats" />、<paramref name="provider" /> 和 <paramref name="style" /> 指定。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="formats">
    /// <paramref name="s" /> 的允许格式的数组。有关详细信息，请参阅“备注”部分。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式信息。</param>
    /// <param name="style">枚举值的一个按位组合，指示 <paramref name="s" /> 所允许的格式。要指定的一个典型值为 <see cref="F:System.Globalization.DateTimeStyles.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 或 <paramref name="formats" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 是一个空字符串。- 或 - 元素的 <paramref name="formats" /> 为空字符串。- 或 - <paramref name="s" /> 不包含日期和时间对应的任何元素 <paramref name="formats" />。- 或 -小时部分和 AM/PM 指示符在 <paramref name="s" /> 不一致。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> 包含无效的组件组合 <see cref="T:System.Globalization.DateTimeStyles" /> 值。例如，同时 <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> 和 <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
    {
      DateTimeFormatInfo.ValidateStyles(style, "style");
      return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
    }

    /// <summary>从此实例中减去指定的日期和时间。</summary>
    /// <returns>一个时间间隔，它等于此实例所表示的日期和时间减去 <paramref name="value" /> 所表示的日期和时间。</returns>
    /// <param name="value">要减去的日期和时间值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">结果是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan Subtract(DateTime value)
    {
      return new TimeSpan(this.InternalTicks - value.InternalTicks);
    }

    /// <summary>从此实例中减去指定持续时间。</summary>
    /// <returns>一个对象，它等于此实例所表示的日期和时间减去 <paramref name="value" /> 所表示的时间间隔。</returns>
    /// <param name="value">待减去的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">结果是小于 <see cref="F:System.DateTime.MinValue" /> 或大于 <see cref="F:System.DateTime.MaxValue" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime Subtract(TimeSpan value)
    {
      long internalTicks = this.InternalTicks;
      long num = value._ticks;
      if (internalTicks - 0L < num || internalTicks - 3155378975999999999L > num)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
      return new DateTime((ulong) (internalTicks - num) | this.InternalKind);
    }

    private static double TicksToOADate(long value)
    {
      if (value == 0L)
        return 0.0;
      if (value < 864000000000L)
        value += 599264352000000000L;
      if (value < 31241376000000000L)
        throw new OverflowException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
      long num1 = (value - 599264352000000000L) / 10000L;
      if (num1 < 0L)
      {
        long num2 = num1 % 86400000L;
        if (num2 != 0L)
          num1 -= (86400000L + num2) * 2L;
      }
      return (double) num1 / 86400000.0;
    }

    /// <summary>将此实例的值转换为等效的 OLE 自动化日期。</summary>
    /// <returns>一个双精度浮点数，它包含与此实例的值等效的 OLE 自动化日期。</returns>
    /// <exception cref="T:System.OverflowException">不能作为 OLE 自动化日期中表示此实例的值。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public double ToOADate()
    {
      return DateTime.TicksToOADate(this.InternalTicks);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为 Windows 文件时间。</summary>
    /// <returns>表示为 Windows 文件时间的当前 <see cref="T:System.DateTime" /> 对象的值。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的文件时间将表示的日期和时间之前公元 1601 年 1 月 1 日午夜 12:00UTC。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public long ToFileTime()
    {
      return this.ToUniversalTime().ToFileTimeUtc();
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为 Windows 文件时间。</summary>
    /// <returns>表示为 Windows 文件时间的当前 <see cref="T:System.DateTime" /> 对象的值。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所生成的文件时间将表示的日期和时间之前公元 1601 年 1 月 1 日午夜 12:00UTC。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public long ToFileTimeUtc()
    {
      long num1 = (((long) this.InternalKind & long.MinValue) != 0L ? this.ToUniversalTime().InternalTicks : this.InternalTicks) - 504911232000000000L;
      long num2 = 0;
      if (num1 >= num2)
        return num1;
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为本地时间。</summary>
    /// <returns>一个对象，其 <see cref="P:System.DateTime.Kind" /> 属性为 <see cref="F:System.DateTimeKind.Local" />，并且其值为等效于当前 <see cref="T:System.DateTime" /> 对象的值的本地时间；如果经转换的值过大以至于不能由 <see cref="F:System.DateTime.MaxValue" /> 对象表示，则为 <see cref="T:System.DateTime" />，或者，如果经转换的值过小以至于不能表示为 <see cref="F:System.DateTime.MinValue" /> 对象，则为 <see cref="T:System.DateTime" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime ToLocalTime()
    {
      return this.ToLocalTime(false);
    }

    internal DateTime ToLocalTime(bool throwOnOverflow)
    {
      if (this.Kind == DateTimeKind.Local)
        return this;
      bool isDaylightSavings = false;
      bool isAmbiguousLocalDst = false;
      long ticks = this.Ticks + TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out isDaylightSavings, out isAmbiguousLocalDst).Ticks;
      if (ticks > 3155378975999999999L)
      {
        if (throwOnOverflow)
          throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
        return new DateTime(3155378975999999999L, DateTimeKind.Local);
      }
      if (ticks >= 0L)
        return new DateTime(ticks, DateTimeKind.Local, isAmbiguousLocalDst);
      if (throwOnOverflow)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
      return new DateTime(0L, DateTimeKind.Local);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为其等效的长日期字符串表示形式。</summary>
    /// <returns>一个字符串，它包含当前 <see cref="T:System.DateTime" /> 对象的长日期字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public string ToLongDateString()
    {
      return DateTimeFormat.Format(this, "D", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为其等效的长时间字符串表示形式。</summary>
    /// <returns>一个字符串，它包含当前 <see cref="T:System.DateTime" /> 对象的长时间字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public string ToLongTimeString()
    {
      return DateTimeFormat.Format(this, "T", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为其等效的短日期字符串表示形式。</summary>
    /// <returns>一个字符串，它包含当前 <see cref="T:System.DateTime" /> 对象的短日期字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public string ToShortDateString()
    {
      return DateTimeFormat.Format(this, "d", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为其等效的短时间字符串表示形式。</summary>
    /// <returns>一个字符串，它包含当前 <see cref="T:System.DateTime" /> 对象的短时间字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public string ToShortTimeString()
    {
      return DateTimeFormat.Format(this, "t", DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.DateTime" /> 对象的值的字符串表示形式。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">日期和时间是支持使用当前区域性的日历的日期范围之外。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return DateTimeFormat.Format(this, (string) null, DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的格式将当前 <see cref="T:System.DateTime" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>由 <see cref="T:System.DateTime" /> 指定的当前 <paramref name="format" /> 对象的值的字符串表示形式。</returns>
    /// <param name="format">标准或自定义日期和时间格式字符串（请参见“备注”）。</param>
    /// <exception cref="T:System.FormatException">长度 <paramref name="format" /> 为 1，且不是为定义的格式说明符字符之一 <see cref="T:System.Globalization.DateTimeFormatInfo" />。- 或 - <paramref name="format" /> 不包含有效的自定义格式模式。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">日期和时间是支持使用当前区域性的日历的日期范围之外。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return DateTimeFormat.Format(this, format, DateTimeFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的区域性特定格式信息将当前 <see cref="T:System.DateTime" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>由 <see cref="T:System.DateTime" /> 指定的当前 <paramref name="provider" /> 对象的值的字符串表示形式。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">支持使用的日历的日期范围之外的日期和时间，则 <paramref name="provider" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return DateTimeFormat.Format(this, (string) null, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>使用指定的格式和区域性特定格式信息将当前 <see cref="T:System.DateTime" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>由 <see cref="T:System.DateTime" /> 和 <paramref name="format" /> 指定的当前 <paramref name="provider" /> 对象的值的字符串表示形式。</returns>
    /// <param name="format">标准或自定义日期和时间格式字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">长度 <paramref name="format" /> 为 1，且不是为定义的格式说明符字符之一 <see cref="T:System.Globalization.DateTimeFormatInfo" />。- 或 - <paramref name="format" /> 不包含有效的自定义格式模式。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">支持使用的日历的日期范围之外的日期和时间，则 <paramref name="provider" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return DateTimeFormat.Format(this, format, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>将当前 <see cref="T:System.DateTime" /> 对象的值转换为协调世界时 (UTC)。</summary>
    /// <returns>一个对象，其 <see cref="P:System.DateTime.Kind" /> 属性为 <see cref="F:System.DateTimeKind.Utc" />，并且其值为等效于当前 <see cref="T:System.DateTime" /> 对象的值的 UTC；如果经转换的值过大以至于不能由 <see cref="F:System.DateTime.MaxValue" /> 对象表示，则为 <see cref="T:System.DateTime" />，或者，如果经转换的值过小以至于不能表示为 <see cref="F:System.DateTime.MinValue" /> 对象，则为 <see cref="T:System.DateTime" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public DateTime ToUniversalTime()
    {
      return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
    }

    /// <summary>将日期和时间的指定字符串表示形式转换为其 <see cref="T:System.DateTime" /> 等效项，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 参数成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <see cref="T:System.DateTime" /> 中包含的日期和时间等效的 <paramref name="s" /> 值；如果转换失败，则为 <see cref="F:System.DateTime.MinValue" />。如果 <paramref name="s" /> 参数为 null，是空字符串 ("") 或者不包含日期和时间的有效字符串表示形式，则转换失败。此参数未经初始化即被传递。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out DateTime result)
    {
      return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
    }

    /// <summary>使用指定的区域性特定格式信息和格式设置样式，将日期和时间的指定字符串表示形式转换为其 <see cref="T:System.DateTime" /> 等效项，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 参数成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <param name="styles">枚举值的按位组合，该组合定义如何根据当前时区或当前日期解释已分析日期。要指定的一个典型值为 <see cref="F:System.Globalization.DateTimeStyles.None" />。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <see cref="T:System.DateTime" /> 中包含的日期和时间等效的 <paramref name="s" /> 值；如果转换失败，则为 <see cref="F:System.DateTime.MinValue" />。如果 <paramref name="s" /> 参数为 null，是空字符串 ("") 或者不包含日期和时间的有效字符串表示形式，则转换失败。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 不是有效 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -<paramref name="styles" /> 包含无效的组件组合 <see cref="T:System.Globalization.DateTimeStyles" /> 值 （例如，同时 <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> 和 <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="provider" /> 是一个中立区域并不能在分析操作中使用。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
    {
      DateTimeFormatInfo.ValidateStyles(styles, "styles");
      return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
    }

    /// <summary>使用指定的格式、区域性特定的格式信息和样式将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。字符串表示形式的格式必须与指定的格式完全匹配。该方法返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="format">所需的 <paramref name="s" /> 格式。有关详细信息，请参阅备注部分。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <param name="style">一个或多个枚举值的按位组合，指示 <paramref name="s" /> 允许使用的格式。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <see cref="T:System.DateTime" /> 中包含的日期和时间等效的 <paramref name="s" /> 值；如果转换失败，则为 <see cref="F:System.DateTime.MinValue" />。如果 <paramref name="s" /> 或 <paramref name="format" /> 参数为 null，或者为空字符串，或者未包含对应于 <paramref name="format" /> 中指定的模式的日期和时间，则转换失败。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 不是有效 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -<paramref name="styles" /> 包含无效的组件组合 <see cref="T:System.Globalization.DateTimeStyles" /> 值 （例如，同时 <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> 和 <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
    {
      DateTimeFormatInfo.ValidateStyles(style, "style");
      return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
    }

    /// <summary>使用指定的格式数组、区域性特定格式信息和样式，将日期和时间的指定字符串表示形式转换为其等效的 <see cref="T:System.DateTime" />。字符串表示形式的格式必须至少与指定的格式之一完全匹配。该方法返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 参数成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的日期和时间的字符串。</param>
    /// <param name="formats">
    /// <paramref name="s" /> 的允许格式的数组。有关详细信息，请参阅备注部分。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式信息。</param>
    /// <param name="style">枚举值的一个按位组合，指示 <paramref name="s" /> 所允许的格式。要指定的一个典型值为 <see cref="F:System.Globalization.DateTimeStyles.None" />。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <see cref="T:System.DateTime" /> 中包含的日期和时间等效的 <paramref name="s" /> 值；如果转换失败，则为 <see cref="F:System.DateTime.MinValue" />。如果 <paramref name="s" /> 或 <paramref name="formats" /> 为 null，<paramref name="s" /> 或 <paramref name="formats" /> 的某个元素为空字符串， 或者 <paramref name="s" /> 的格式与 <paramref name="formats" /> 中的格式模式所指定的格式都不完全匹配，则转换失败。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="styles" /> 不是有效 <see cref="T:System.Globalization.DateTimeStyles" /> 值。- 或 -<paramref name="styles" /> 包含无效的组件组合 <see cref="T:System.Globalization.DateTimeStyles" /> 值 （例如，同时 <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> 和 <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
    {
      DateTimeFormatInfo.ValidateStyles(style, "style");
      return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
    }

    /// <summary>将此实例的值转换为标准日期和时间格式说明符支持的所有字符串表示形式。</summary>
    /// <returns>字符串数组，其中每个元素都表示此实例的以标准日期和时间格式说明符之一进行格式设置的一个值。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats()
    {
      return this.GetDateTimeFormats((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>将此实例的值转换为标准日期和时间格式说明符和指定的区域性特定格式信息支持的所有字符串表示形式。</summary>
    /// <returns>字符串数组，其中每个元素都表示此实例的以标准日期和时间格式说明符之一进行格式设置的一个值。</returns>
    /// <param name="provider">一个对象，它提供有关此实例的区域性特定格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats(IFormatProvider provider)
    {
      return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>将此实例的值转换为指定的标准日期和时间格式说明符支持的所有字符串表示形式。</summary>
    /// <returns>符串数组，其中每个元素都表示此实例的以 <paramref name="format" /> 标准日期和时间格式说明符之一进行格式设置的一个值。</returns>
    /// <param name="format">标准日期和时间格式字符串（请参见注解）。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的标准日期和时间格式说明符。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats(char format)
    {
      return this.GetDateTimeFormats(format, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>将此实例的值转换为指定的标准日期和时间格式说明符和区域性特定格式信息支持的所有字符串表示形式。</summary>
    /// <returns>字符串数组，其中每个元素都表示此实例的以标准日期和时间格式说明符之一进行格式设置的一个值。</returns>
    /// <param name="format">日期和时间格式字符串（请参见注解）。</param>
    /// <param name="provider">一个对象，它提供有关此实例的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的标准日期和时间格式说明符。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string[] GetDateTimeFormats(char format, IFormatProvider provider)
    {
      return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
    }

    /// <summary>返回值类型 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.DateTime" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.DateTime;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Boolean"));
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Char"));
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "SByte"));
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Byte"));
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Int16"));
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "UInt16"));
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Int32"));
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "UInt32"));
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Int64"));
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "UInt64"));
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Single"));
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Double"));
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "DateTime", (object) "Decimal"));
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
    {
      result = DateTime.MinValue;
      if (year < 1 || year > 9999 || (month < 1 || month > 12))
        return false;
      int[] numArray = DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365;
      if (day < 1 || day > numArray[month] - numArray[month - 1] || (hour < 0 || hour >= 24) || (minute < 0 || minute >= 60 || (second < 0 || second >= 60)) || (millisecond < 0 || millisecond >= 1000))
        return false;
      long ticks = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
      if (ticks < 0L || ticks > 3155378975999999999L)
        return false;
      result = new DateTime(ticks, DateTimeKind.Unspecified);
      return true;
    }
  }
}
