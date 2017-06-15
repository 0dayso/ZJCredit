// Decompiled with JetBrains decompiler
// Type: System.Globalization.Calendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>将时间分成段来表示，如分成星期、月和年。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Calendar : ICloneable
  {
    internal int m_currentEraValue = -1;
    internal int twoDigitYearMax = -1;
    internal const long TicksPerMillisecond = 10000;
    internal const long TicksPerSecond = 10000000;
    internal const long TicksPerMinute = 600000000;
    internal const long TicksPerHour = 36000000000;
    internal const long TicksPerDay = 864000000000;
    internal const int MillisPerSecond = 1000;
    internal const int MillisPerMinute = 60000;
    internal const int MillisPerHour = 3600000;
    internal const int MillisPerDay = 86400000;
    internal const int DaysPerYear = 365;
    internal const int DaysPer4Years = 1461;
    internal const int DaysPer100Years = 36524;
    internal const int DaysPer400Years = 146097;
    internal const int DaysTo10000 = 3652059;
    internal const long MaxMillis = 315537897600000;
    internal const int CAL_GREGORIAN = 1;
    internal const int CAL_GREGORIAN_US = 2;
    internal const int CAL_JAPAN = 3;
    internal const int CAL_TAIWAN = 4;
    internal const int CAL_KOREA = 5;
    internal const int CAL_HIJRI = 6;
    internal const int CAL_THAI = 7;
    internal const int CAL_HEBREW = 8;
    internal const int CAL_GREGORIAN_ME_FRENCH = 9;
    internal const int CAL_GREGORIAN_ARABIC = 10;
    internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;
    internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;
    internal const int CAL_JULIAN = 13;
    internal const int CAL_JAPANESELUNISOLAR = 14;
    internal const int CAL_CHINESELUNISOLAR = 15;
    internal const int CAL_SAKA = 16;
    internal const int CAL_LUNAR_ETO_CHN = 17;
    internal const int CAL_LUNAR_ETO_KOR = 18;
    internal const int CAL_LUNAR_ETO_ROKUYOU = 19;
    internal const int CAL_KOREANLUNISOLAR = 20;
    internal const int CAL_TAIWANLUNISOLAR = 21;
    internal const int CAL_PERSIAN = 22;
    internal const int CAL_UMALQURA = 23;
    [OptionalField(VersionAdded = 2)]
    private bool m_isReadOnly;
    /// <summary>表示当前日历的当前纪元。</summary>
    [__DynamicallyInvokable]
    public const int CurrentEra = 0;

    /// <summary>获取此 <see cref="T:System.Globalization.Calendar" /> 对象支持的最早日期和时间。</summary>
    /// <returns>此日历支持的最早日期和时间。默认值为 <see cref="F:System.DateTime.MinValue" />。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual DateTime MinSupportedDateTime
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>获取此 <see cref="T:System.Globalization.Calendar" /> 对象支持的最晚日期和时间。</summary>
    /// <returns>此日历支持的最晚日期和时间。默认值为 <see cref="F:System.DateTime.MaxValue" />。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual DateTime MaxSupportedDateTime
    {
      [__DynamicallyInvokable] get
      {
        return DateTime.MaxValue;
      }
    }

    internal virtual int ID
    {
      get
      {
        return -1;
      }
    }

    internal virtual int BaseCalendarID
    {
      get
      {
        return this.ID;
      }
    }

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.CalendarAlgorithmType" /> 值之一。</returns>
    [ComVisible(false)]
    public virtual CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.Unknown;
      }
    }

    /// <summary>获取一个值，该值指示此 <see cref="T:System.Globalization.Calendar" /> 对象是否为只读。</summary>
    /// <returns>如果此 <see cref="T:System.Globalization.Calendar" /> 对象为只读，则为 true；否则为 false。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    internal virtual int CurrentEraValue
    {
      get
      {
        if (this.m_currentEraValue == -1)
          this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
        return this.m_currentEraValue;
      }
    }

    /// <summary>当在派生类中重写时，获取当前日历中的纪元列表。</summary>
    /// <returns>表示当前日历中的纪元的整数数组。</returns>
    [__DynamicallyInvokable]
    public abstract int[] Eras { [__DynamicallyInvokable] get; }

    /// <summary>获取指定 <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> 属性的指定年份中的天数。</summary>
    /// <returns>由 <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> 指定的在年之前的一年的天数。</returns>
    protected virtual int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 365;
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前的 <see cref="T:System.Globalization.Calendar" /> 对象为只读。</exception>
    [__DynamicallyInvokable]
    public virtual int TwoDigitYearMax
    {
      [__DynamicallyInvokable] get
      {
        return this.twoDigitYearMax;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Globalization.Calendar" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Calendar()
    {
    }

    /// <summary>创建作为当前 <see cref="T:System.Globalization.Calendar" /> 对象副本的新对象。</summary>
    /// <returns>一个新的 <see cref="T:System.Object" /> 实例（是当前 <see cref="T:System.Globalization.Calendar" /> 对象的成员副本）。</returns>
    [ComVisible(false)]
    public virtual object Clone()
    {
      object obj;
      ((Calendar) (obj = this.MemberwiseClone())).SetReadOnlyState(false);
      return obj;
    }

    /// <summary>返回指定 <see cref="T:System.Globalization.Calendar" /> 对象的只读版本。</summary>
    /// <returns>由 <paramref name="calendar" /> 参数指定的 <see cref="T:System.Globalization.Calendar" /> 对象（如果 <paramref name="calendar" /> 是只读的）。- 或 -一个由 <paramref name="calendar" /> 指定的 <see cref="T:System.Globalization.Calendar" /> 对象的只读成员副本（如果 <paramref name="calendar" /> 不是只读的）。</returns>
    /// <param name="calendar">一个 <see cref="T:System.Globalization.Calendar" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="calendar" /> 为 null。</exception>
    [ComVisible(false)]
    public static Calendar ReadOnly(Calendar calendar)
    {
      if (calendar == null)
        throw new ArgumentNullException("calendar");
      if (calendar.IsReadOnly)
        return calendar;
      Calendar calendar1 = (Calendar) calendar.MemberwiseClone();
      int num = 1;
      calendar1.SetReadOnlyState(num != 0);
      return calendar1;
    }

    internal void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    internal void SetReadOnlyState(bool readOnly)
    {
      this.m_isReadOnly = readOnly;
    }

    internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
    {
      if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Argument_ResultCalendarRange"), (object) minValue, (object) maxValue));
    }

    internal DateTime Add(DateTime time, double value, int scale)
    {
      double num1 = value * (double) scale + (value >= 0.0 ? 0.5 : -0.5);
      if (num1 <= -315537897600000.0 || num1 >= 315537897600000.0)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
      long num2 = (long) num1;
      long ticks = time.Ticks + num2 * 10000L;
      DateTime supportedDateTime1 = this.MinSupportedDateTime;
      DateTime supportedDateTime2 = this.MaxSupportedDateTime;
      Calendar.CheckAddResult(ticks, supportedDateTime1, supportedDateTime2);
      return new DateTime(ticks);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定毫秒数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定毫秒数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">要添加毫秒的 <see cref="T:System.DateTime" />。</param>
    /// <param name="milliseconds">要添加的毫秒数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="milliseconds" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
    {
      return this.Add(time, milliseconds, 1);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定天数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定天数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加天数。</param>
    /// <param name="days">要添加的天数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="days" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddDays(DateTime time, int days)
    {
      return this.Add(time, (double) days, 86400000);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定小时数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定小时数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加小时数。</param>
    /// <param name="hours">要添加的小时数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="hours" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddHours(DateTime time, int hours)
    {
      return this.Add(time, (double) hours, 3600000);
    }

    /// <summary>返回与指定的 <see cref="T:System.DateTime" /> 相距指定分钟数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定分钟数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加分钟数。</param>
    /// <param name="minutes">要添加的分钟数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="minutes" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddMinutes(DateTime time, int minutes)
    {
      return this.Add(time, (double) minutes, 60000);
    }

    /// <summary>当在派生类中重写时，将返回与指定的 <see cref="T:System.DateTime" /> 相距指定月数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定的月数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加月数。</param>
    /// <param name="months">要添加的月数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract DateTime AddMonths(DateTime time, int months);

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定秒数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定的秒数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加秒数。</param>
    /// <param name="seconds">要添加的秒数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="seconds" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddSeconds(DateTime time, int seconds)
    {
      return this.Add(time, (double) seconds, 1000);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定周数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定周数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加星期数。</param>
    /// <param name="weeks">要添加的星期数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="weeks" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime AddWeeks(DateTime time, int weeks)
    {
      return this.AddDays(time, weeks * 7);
    }

    /// <summary>当在派生类中重写时，将返回与指定的 <see cref="T:System.DateTime" /> 相距指定年数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定年数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加年数。</param>
    /// <param name="years">要添加的年数。</param>
    /// <exception cref="T:System.ArgumentException">得到的 <see cref="T:System.DateTime" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="years" /> 超出了 <see cref="T:System.DateTime" /> 返回值支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract DateTime AddYears(DateTime time, int years);

    /// <summary>当在派生类中重写时，将返回指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</summary>
    /// <returns>一个正整数，表示 <paramref name="time" /> 参数中的月中日期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public abstract int GetDayOfMonth(DateTime time);

    /// <summary>当在派生类中重写时，将返回指定 <see cref="T:System.DateTime" /> 中的日期是星期几。</summary>
    /// <returns>一个 <see cref="T:System.DayOfWeek" /> 值，表示 <paramref name="time" /> 参数中的周中日期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public abstract DayOfWeek GetDayOfWeek(DateTime time);

    /// <summary>当在派生类中重写时，将返回指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</summary>
    /// <returns>一个正整数，表示 <paramref name="time" /> 参数中的年中日期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public abstract int GetDayOfYear(DateTime time);

    /// <summary>返回当前纪元的指定月份和年份中的天数。</summary>
    /// <returns>当前纪元中指定年份的指定月份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual int GetDaysInMonth(int year, int month)
    {
      return this.GetDaysInMonth(year, month, 0);
    }

    /// <summary>当在派生类中重写时，返回指定月份、纪元年份中的天数。</summary>
    /// <returns>指定纪元中指定年份的指定月份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract int GetDaysInMonth(int year, int month, int era);

    /// <summary>返回当前纪元的指定年份中的天数。</summary>
    /// <returns>当前纪元中指定年份的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual int GetDaysInYear(int year)
    {
      return this.GetDaysInYear(year, 0);
    }

    /// <summary>当在派生类中重写时，返回指定纪元年份中的天数。</summary>
    /// <returns>指定纪元中指定年份的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract int GetDaysInYear(int year, int era);

    /// <summary>当在派生类中重写时，将返回指定的 <see cref="T:System.DateTime" /> 中的纪元。</summary>
    /// <returns>表示 <paramref name="time" /> 中的纪元的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public abstract int GetEra(DateTime time);

    /// <summary>返回指定的 <see cref="T:System.DateTime" /> 中的小时值。</summary>
    /// <returns>0 与 23 之间的一个整数，它表示 <paramref name="time" /> 中的小时。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public virtual int GetHour(DateTime time)
    {
      return (int) (time.Ticks / 36000000000L % 24L);
    }

    /// <summary>返回指定的 <see cref="T:System.DateTime" /> 中的毫秒值。</summary>
    /// <returns>一个介于 0 到 999 之间的双精度浮点数字，表示 <paramref name="time" /> 参数中的毫秒数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public virtual double GetMilliseconds(DateTime time)
    {
      return (double) (time.Ticks / 10000L % 1000L);
    }

    /// <summary>返回指定的 <see cref="T:System.DateTime" /> 中的分钟值。</summary>
    /// <returns>0 到 59 之间的一个整数，它表示 <paramref name="time" /> 中的分钟值。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public virtual int GetMinute(DateTime time)
    {
      return (int) (time.Ticks / 600000000L % 60L);
    }

    /// <summary>当在派生类中重写时，将返回指定的 <see cref="T:System.DateTime" /> 中的月份。</summary>
    /// <returns>一个正整数，表示 <paramref name="time" /> 中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public abstract int GetMonth(DateTime time);

    /// <summary>返回当前纪元中指定年份的月数。</summary>
    /// <returns>当前纪元中指定年份的月数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual int GetMonthsInYear(int year)
    {
      return this.GetMonthsInYear(year, 0);
    }

    /// <summary>当在派生类中重写时，将返回指定纪元中指定年份的月数。</summary>
    /// <returns>指定纪元中指定年份的月数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract int GetMonthsInYear(int year, int era);

    /// <summary>返回指定的 <see cref="T:System.DateTime" /> 中的秒值。</summary>
    /// <returns>0 到 59 之间的一个整数，它表示 <paramref name="time" /> 中的秒数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public virtual int GetSecond(DateTime time)
    {
      return (int) (time.Ticks / 10000000L % 60L);
    }

    internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
    {
      int num1 = this.GetDayOfYear(time) - 1;
      int num2 = (int) (this.GetDayOfWeek(time) - num1 % 7 - firstDayOfWeek + 14) % 7;
      return (num1 + num2) / 7 + 1;
    }

    private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
    {
      int num1 = this.GetDayOfYear(time) - 1;
      int num2 = (int) (this.GetDayOfWeek(time) - num1 % 7);
      int num3 = (firstDayOfWeek - num2 + 14) % 7;
      if (num3 != 0 && num3 >= fullDays)
        num3 -= 7;
      int num4 = num1 - num3;
      if (num4 >= 0)
        return num4 / 7 + 1;
      if (time <= this.MinSupportedDateTime.AddDays((double) num1))
        return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
      return this.GetWeekOfYearFullDays(time.AddDays((double) -(num1 + 1)), firstDayOfWeek, fullDays);
    }

    private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
    {
      int num1 = (int) (this.GetDayOfWeek(this.MinSupportedDateTime) - (this.GetDayOfYear(this.MinSupportedDateTime) - 1) % 7);
      int num2 = (firstDayOfWeek + 7 - num1) % 7;
      if (num2 == 0 || num2 >= minimumDaysInFirstWeek)
        return 1;
      int num3 = this.DaysInYearBeforeMinSupportedYear - 1;
      int num4 = num1 - 1 - num3 % 7;
      int num5 = (firstDayOfWeek - num4 + 14) % 7;
      int num6 = num3 - num5;
      if (num5 >= minimumDaysInFirstWeek)
        num6 += 7;
      return num6 / 7 + 1;
    }

    /// <summary>返回一年中包括指定 <see cref="T:System.DateTime" /> 值中的日期的那个星期。</summary>
    /// <returns>一个正整数，表示一年中包括 <paramref name="time" /> 参数中的日期的那个星期。</returns>
    /// <param name="time">日期和时间值。</param>
    /// <param name="rule">定义日历周的枚举值。</param>
    /// <param name="firstDayOfWeek">表示一周的第一天的枚举值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 早于 <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> 或晚于 <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />。- 或 -<paramref name="firstDayOfWeek" /> 不是有效的 <see cref="T:System.DayOfWeek" /> 值。- 或 -<paramref name="rule" /> 不是有效的 <see cref="T:System.Globalization.CalendarWeekRule" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
        throw new ArgumentOutOfRangeException("firstDayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) DayOfWeek.Sunday, (object) DayOfWeek.Saturday));
      switch (rule)
      {
        case CalendarWeekRule.FirstDay:
          return this.GetFirstDayWeekOfYear(time, (int) firstDayOfWeek);
        case CalendarWeekRule.FirstFullWeek:
          return this.GetWeekOfYearFullDays(time, (int) firstDayOfWeek, 7);
        case CalendarWeekRule.FirstFourDayWeek:
          return this.GetWeekOfYearFullDays(time, (int) firstDayOfWeek, 4);
        default:
          throw new ArgumentOutOfRangeException("rule", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) CalendarWeekRule.FirstDay, (object) CalendarWeekRule.FirstFourDayWeek));
      }
    }

    /// <summary>当在派生类中重写时，将返回指定的 <see cref="T:System.DateTime" /> 中的年份。</summary>
    /// <returns>表示 <paramref name="time" /> 中的年份的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    [__DynamicallyInvokable]
    public abstract int GetYear(DateTime time);

    /// <summary>确定当前纪元中的指定日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <param name="day">一个表示天的正整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsLeapDay(int year, int month, int day)
    {
      return this.IsLeapDay(year, month, day, 0);
    }

    /// <summary>当在派生类中重写时，将确定指定纪元中的指定日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <param name="day">一个表示天的正整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract bool IsLeapDay(int year, int month, int day, int era);

    /// <summary>确定当前纪元中指定年份的指定月份是否为闰月。</summary>
    /// <returns>如果指定的月份是闰月，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsLeapMonth(int year, int month)
    {
      return this.IsLeapMonth(year, month, 0);
    }

    /// <summary>当在派生类中重写时，将确定指定纪元中指定年份的指定月份是否为闰月。</summary>
    /// <returns>如果指定的月份是闰月，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract bool IsLeapMonth(int year, int month, int era);

    /// <summary>计算指定年份的闰月。</summary>
    /// <returns>一个表示指定年份的闰月的正整数。- 或 -如果此日历不支持闰月或 <paramref name="year" /> 参数不表示闰年，则为零。</returns>
    /// <param name="year">年份。</param>
    [ComVisible(false)]
    public virtual int GetLeapMonth(int year)
    {
      return this.GetLeapMonth(year, 0);
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>一个正整数，表示指定纪元年份中的闰月。- 或 -如果此日历不支持闰月，或者 <paramref name="year" /> 和 <paramref name="era" /> 参数未指定闰年，则为零。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。</param>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int GetLeapMonth(int year, int era)
    {
      if (!this.IsLeapYear(year, era))
        return 0;
      int monthsInYear = this.GetMonthsInYear(year, era);
      for (int month = 1; month <= monthsInYear; ++month)
      {
        if (this.IsLeapMonth(year, month, era))
          return month;
      }
      return 0;
    }

    /// <summary>确定当前纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsLeapYear(int year)
    {
      return this.IsLeapYear(year, 0);
    }

    /// <summary>当在派生类中重写时，将确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract bool IsLeapYear(int year, int era);

    /// <summary>返回设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <param name="day">一个表示天的正整数。</param>
    /// <param name="hour">0 与 23 之间的一个整数，它表示小时。</param>
    /// <param name="minute">0 与 59 之间的一个整数，它表示分钟。</param>
    /// <param name="second">0 与 59 之间的一个整数，它表示秒。</param>
    /// <param name="millisecond">0 与 999 之间的一个整数，它表示毫秒。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于零或大于 59。- 或 -<paramref name="second" /> 小于零或大于 59。- 或 -<paramref name="millisecond" /> 小于零或大于 999。</exception>
    [__DynamicallyInvokable]
    public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
    }

    /// <summary>当在派生类中重写时，将返回设置为指定纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个表示月份的正整数。</param>
    /// <param name="day">一个表示天的正整数。</param>
    /// <param name="hour">0 与 23 之间的一个整数，它表示小时。</param>
    /// <param name="minute">0 与 59 之间的一个整数，它表示分钟。</param>
    /// <param name="second">0 与 59 之间的一个整数，它表示秒。</param>
    /// <param name="millisecond">0 与 999 之间的一个整数，它表示毫秒。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于零或大于 59。- 或 -<paramref name="second" /> 小于零或大于 59。- 或 -<paramref name="millisecond" /> 小于零或大于 999。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

    internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
    {
      result = DateTime.MinValue;
      try
      {
        result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
        return true;
      }
      catch (ArgumentException ex)
      {
        return false;
      }
    }

    internal virtual bool IsValidYear(int year, int era)
    {
      if (year >= this.GetYear(this.MinSupportedDateTime))
        return year <= this.GetYear(this.MaxSupportedDateTime);
      return false;
    }

    internal virtual bool IsValidMonth(int year, int month, int era)
    {
      if (this.IsValidYear(year, era) && month >= 1)
        return month <= this.GetMonthsInYear(year, era);
      return false;
    }

    internal virtual bool IsValidDay(int year, int month, int day, int era)
    {
      if (this.IsValidMonth(year, month, era) && day >= 1)
        return day <= this.GetDaysInMonth(year, month, era);
      return false;
    }

    /// <summary>使用 <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> 属性将指定的年份转换为四位数年份，以确定相应的纪元。</summary>
    /// <returns>包含 <paramref name="year" /> 的四位数表示形式的整数。</returns>
    /// <param name="year">一个两位数或四位数的整数，表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    [__DynamicallyInvokable]
    public virtual int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return (this.TwoDigitYearMax / 100 - (year > this.TwoDigitYearMax % 100 ? 1 : 0)) * 100 + year;
      return year;
    }

    internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
    {
      if (hour < 0 || hour >= 24 || (minute < 0 || minute >= 60) || (second < 0 || second >= 60))
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException("millisecond", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 999));
      return TimeSpan.TimeToTicks(hour, minute, second) + (long) millisecond * 10000L;
    }

    [SecuritySafeCritical]
    internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
    {
      int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
      if (num < 0)
        num = defaultYearValue;
      return num;
    }
  }
}
