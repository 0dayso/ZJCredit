// Decompiled with JetBrains decompiler
// Type: System.Globalization.JulianCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>表示儒略历。</summary>
  [ComVisible(true)]
  [Serializable]
  public class JulianCalendar : Calendar
  {
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public static readonly int JulianEra = 1;
    private static readonly int[] DaysToMonth365 = new int[13]{ 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
    private static readonly int[] DaysToMonth366 = new int[13]{ 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };
    internal int MaxYear = 9999;
    private const int DatePartYear = 0;
    private const int DatePartDayOfYear = 1;
    private const int DatePartMonth = 2;
    private const int DatePartDay = 3;
    private const int JulianDaysPerYear = 365;
    private const int JulianDaysPer4Years = 1461;

    /// <summary>获取 <see cref="T:System.Globalization.JulianCalendar" /> 类支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.JulianCalendar" /> 类支持的最早日期和时间，它相当于公历的公元 0001 年的 1 月 1 日开始的那一刻。</returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.JulianCalendar" /> 类支持的最大日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.JulianCalendar" /> 类支持的最晚日期和时间，它相当于公历的公元 9999 年 12 月 31 日结束的那一刻。</returns>
    [ComVisible(false)]
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return DateTime.MaxValue;
      }
    }

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />。</returns>
    [ComVisible(false)]
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.SolarCalendar;
      }
    }

    internal override int ID
    {
      get
      {
        return 13;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.JulianCalendar" /> 中的纪元的列表。</summary>
    /// <returns>表示 <see cref="T:System.Globalization.JulianCalendar" /> 中的纪元的整数数组。</returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ JulianCalendar.JulianEra };
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">为设置操作指定的值小于 99。- 或 -为 Set 操作指定的值大于 MaxSupportedDateTime.Year。</exception>
    /// <exception cref="T:System.InvalidOperationException">在设置操作中，当前实例是只读的。</exception>
    public override int TwoDigitYearMax
    {
      get
      {
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.MaxYear)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) this.MaxYear));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Globalization.JulianCalendar" /> 类的新实例。</summary>
    public JulianCalendar()
    {
      this.twoDigitYearMax = 2029;
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != JulianCalendar.JulianEra)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal void CheckYearEraRange(int year, int era)
    {
      JulianCalendar.CheckEraRange(era);
      if (year <= 0 || year > this.MaxYear)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) this.MaxYear));
    }

    internal static void CheckMonthRange(int month)
    {
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    internal static void CheckDayRange(int year, int month, int day)
    {
      if (year == 1 && month == 1 && day < 3)
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
      int[] numArray = year % 4 == 0 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      int num = numArray[month] - numArray[month - 1];
      if (day < 1 || day > num)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) num));
    }

    internal static int GetDatePart(long ticks, int part)
    {
      int num1 = (int) ((ticks + 1728000000000L) / 864000000000L);
      int num2 = num1 / 1461;
      int num3 = num1 - num2 * 1461;
      int num4 = num3 / 365;
      if (num4 == 4)
        num4 = 3;
      if (part == 0)
        return num2 * 4 + num4 + 1;
      int num5 = num3 - num4 * 365;
      if (part == 1)
        return num5 + 1;
      int[] numArray = num4 == 3 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      int index = num5 >> 6;
      while (num5 >= numArray[index])
        ++index;
      if (part == 2)
        return index;
      return num5 - numArray[index - 1] + 1;
    }

    internal static long DateToTicks(int year, int month, int day)
    {
      int[] numArray = year % 4 == 0 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      int num = year - 1;
      return (long) (num * 365 + num / 4 + numArray[month - 1] + day - 1 - 2) * 864000000000L;
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定月数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定的月数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加月数。</param>
    /// <param name="months">要添加的月数。</param>
    /// <exception cref="T:System.ArgumentException">结果 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于 -120000。- 或 -<paramref name="months" /> 大于 120000。</exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException("months", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = JulianCalendar.GetDatePart(time.Ticks, 0);
      int datePart2 = JulianCalendar.GetDatePart(time.Ticks, 2);
      int day = JulianCalendar.GetDatePart(time.Ticks, 3);
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
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? JulianCalendar.DaysToMonth365 : JulianCalendar.DaysToMonth366;
      int num2 = numArray[month] - numArray[month - 1];
      if (day > num2)
        day = num2;
      long ticks = JulianCalendar.DateToTicks(year, month, day) + time.Ticks % 864000000000L;
      DateTime supportedDateTime1 = this.MinSupportedDateTime;
      DateTime supportedDateTime2 = this.MaxSupportedDateTime;
      Calendar.CheckAddResult(ticks, supportedDateTime1, supportedDateTime2);
      return new DateTime(ticks);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定年数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定年数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加年数。</param>
    /// <param name="years">要添加的年数。</param>
    /// <exception cref="T:System.ArgumentException">结果 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</summary>
    /// <returns>从 1 到 31 的整数，表示 <paramref name="time" /> 中的日期是该月的几号。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetDayOfMonth(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 3);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是星期几。</summary>
    /// <returns>一个 <see cref="T:System.DayOfWeek" /> 值，它表示 <paramref name="time" /> 中的日期是星期几。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</summary>
    /// <returns>从 1 到 366 的整数，表示 <paramref name="time" /> 中的日期是该年中的第几天。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetDayOfYear(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 1);
    }

    /// <summary>返回指定纪元中指定年份的指定月份的天数。</summary>
    /// <returns>指定纪元中指定年份的指定月份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      this.CheckYearEraRange(year, era);
      JulianCalendar.CheckMonthRange(month);
      int[] numArray = year % 4 == 0 ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365;
      return numArray[month] - numArray[month - 1];
    }

    /// <summary>返回指定纪元中指定年份的天数。</summary>
    /// <returns>指定纪元中指定年份的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      return !this.IsLeapYear(year, era) ? 365 : 366;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的纪元。</summary>
    /// <returns>表示 <paramref name="time" /> 中的纪元的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetEra(DateTime time)
    {
      return JulianCalendar.JulianEra;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的月份。</summary>
    /// <returns>1 到 12 之间的一个整数，它表示 <paramref name="time" /> 中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetMonth(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 2);
    }

    /// <summary>返回指定纪元中指定年份中的月数。</summary>
    /// <returns>指定纪元中指定年份的月数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      this.CheckYearEraRange(year, era);
      return 12;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的年份。</summary>
    /// <returns>表示 <paramref name="time" /> 中的年份的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetYear(DateTime time)
    {
      return JulianCalendar.GetDatePart(time.Ticks, 0);
    }

    /// <summary>确定指定纪元中的指定日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 31 之间的一个整数，它表示天。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      JulianCalendar.CheckMonthRange(month);
      if (this.IsLeapYear(year, era))
      {
        JulianCalendar.CheckDayRange(year, month, day);
        if (month == 2)
          return day == 29;
        return false;
      }
      JulianCalendar.CheckDayRange(year, month, day);
      return false;
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>一个正整数，表示指定纪元年份中的闰月。另外，如果日历不支持闰月，或者 <paramref name="year" /> 和 <paramref name="era" /> 未指定闰年，此方法将返回零。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      this.CheckYearEraRange(year, era);
      return 0;
    }

    /// <summary>确定指定纪元中指定年份的指定月份是否为闰月。</summary>
    /// <returns>除非被派生类重写，否则此方法始终返回 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      this.CheckYearEraRange(year, era);
      JulianCalendar.CheckMonthRange(month);
      return false;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      this.CheckYearEraRange(year, era);
      return year % 4 == 0;
    }

    /// <summary>返回一个 <see cref="T:System.DateTime" />，它设置为指定纪元中的指定日期和时间。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 31 之间的一个整数，它表示天。</param>
    /// <param name="hour">0 与 23 之间的一个整数，它表示小时。</param>
    /// <param name="minute">0 与 59 之间的一个整数，它表示分钟。</param>
    /// <param name="second">0 与 59 之间的一个整数，它表示秒。</param>
    /// <param name="millisecond">0 与 999 之间的一个整数，它表示毫秒。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于零或大于 59。- 或 -<paramref name="second" /> 小于零或大于 59。- 或 -<paramref name="millisecond" /> 小于零或大于 999。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      this.CheckYearEraRange(year, era);
      JulianCalendar.CheckMonthRange(month);
      JulianCalendar.CheckDayRange(year, month, day);
      if (millisecond < 0 || millisecond >= 1000)
        throw new ArgumentOutOfRangeException("millisecond", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 999));
      if (hour >= 0 && hour < 24 && (minute >= 0 && minute < 60) && (second >= 0 && second < 60))
        return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
    }

    /// <summary>使用 <see cref="P:System.Globalization.JulianCalendar.TwoDigitYearMax" /> 属性将指定的年份转换为四位数年份，以确定相应的纪元。</summary>
    /// <returns>包含 <paramref name="year" /> 的四位数表示形式的整数。</returns>
    /// <param name="year">一个两位数或四位数的整数，表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year > this.MaxYear)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) 1, (object) this.MaxYear));
      return base.ToFourDigitYear(year);
    }
  }
}
