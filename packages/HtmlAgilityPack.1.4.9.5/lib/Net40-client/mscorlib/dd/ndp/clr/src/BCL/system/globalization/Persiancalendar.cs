// Decompiled with JetBrains decompiler
// Type: System.Globalization.PersianCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>表示波斯历。</summary>
  [Serializable]
  public class PersianCalendar : Calendar
  {
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public static readonly int PersianEra = 1;
    internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;
    internal static int[] DaysToMonth = new int[13]{ 0, 31, 62, 93, 124, 155, 186, 216, 246, 276, 306, 336, 366 };
    internal static DateTime minDate = new DateTime(622, 3, 22);
    internal static DateTime maxDate = DateTime.MaxValue;
    private const int ApproximateHalfYear = 180;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MonthsPerYear = 12;
    internal const int MaxCalendarYear = 9378;
    internal const int MaxCalendarMonth = 10;
    internal const int MaxCalendarDay = 13;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;

    /// <summary>获取 <see cref="T:System.Globalization.PersianCalendar" /> 类支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.PersianCalendar" /> 类支持的最早日期和时间。有关详细信息，请参阅备注部分。</returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return PersianCalendar.minDate;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.PersianCalendar" /> 类支持的最晚日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.PersianCalendar" /> 类支持的最晚日期和时间。有关详细信息，请参阅备注部分。</returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return PersianCalendar.maxDate;
      }
    }

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />。</returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.SolarCalendar;
      }
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 1;
      }
    }

    internal override int ID
    {
      get
      {
        return 22;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.PersianCalendar" /> 对象中的纪元列表。</summary>
    /// <returns>表示 <see cref="T:System.Globalization.PersianCalendar" /> 对象中的纪元的整数数组。此数组由值为 <see cref="F:System.Globalization.PersianCalendar.PersianEra" /> 的单个元素组成。</returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ PersianCalendar.PersianEra };
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.InvalidOperationException">该日历是只读的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中的值是小于 100 或大于 9378。</exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > 9378)
          throw new ArgumentOutOfRangeException("value", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) 9378));
        this.twoDigitYearMax = value;
      }
    }

    private long GetAbsoluteDatePersian(int year, int month, int day)
    {
      if (year < 1 || year > 9378 || (month < 1 || month > 12))
        throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
      int num1 = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
      int num2 = (int) (365.242189 * (double) (year - 1));
      return CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long) num2 + 180L) + (long) num1;
    }

    internal static void CheckTicksRange(long ticks)
    {
      if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) PersianCalendar.minDate, (object) PersianCalendar.maxDate));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != PersianCalendar.PersianEra)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal static void CheckYearRange(int year, int era)
    {
      PersianCalendar.CheckEraRange(era);
      if (year < 1 || year > 9378)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9378));
    }

    internal static void CheckYearMonthRange(int year, int month, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      if (year == 9378 && month > 10)
        throw new ArgumentOutOfRangeException("month", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 10));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    private static int MonthFromOrdinalDay(int ordinalDay)
    {
      int index = 0;
      while (ordinalDay > PersianCalendar.DaysToMonth[index])
        ++index;
      return index;
    }

    private static int DaysInPreviousMonths(int month)
    {
      --month;
      return PersianCalendar.DaysToMonth[month];
    }

    internal int GetDatePart(long ticks, int part)
    {
      PersianCalendar.CheckTicksRange(ticks);
      long numberOfDays = ticks / 864000000000L + 1L;
      int year = (int) Math.Floor((double) (CalendricalCalculationsHelper.PersianNewYearOnOrBefore(numberOfDays) - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
      if (part == 0)
        return year;
      int ordinalDay = (int) (numberOfDays - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 1)));
      if (part == 1)
        return ordinalDay;
      int month = PersianCalendar.MonthFromOrdinalDay(ordinalDay);
      if (part == 2)
        return month;
      int num = ordinalDay - PersianCalendar.DaysInPreviousMonths(month);
      if (part == 3)
        return num;
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
    }

    /// <summary>返回一个基于指定 <see cref="T:System.DateTime" /> 对象偏移指定月数的 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 对象，表示将由 <paramref name="months" /> 参数指定的月数加上由 <paramref name="time" /> 参数指定的日期后所得的日期。</returns>
    /// <param name="time">将向其添加月数的 <see cref="T:System.DateTime" />。</param>
    /// <param name="months">要添加的正月数或负月数。</param>
    /// <exception cref="T:System.ArgumentException">生成 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于-120,000 或大于 120000。</exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException("months", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = this.GetDatePart(time.Ticks, 0);
      int datePart2 = this.GetDatePart(time.Ticks, 2);
      int day = this.GetDatePart(time.Ticks, 3);
      int num = datePart2 - 1 + months;
      int month;
      int year;
      if (num >= 0)
      {
        month = num % 12 + 1;
        year = datePart1 + num / 12;
      }
      else
      {
        month = 12 + (num + 1) % 12;
        year = datePart1 + (num - 11) / 12;
      }
      int daysInMonth = this.GetDaysInMonth(year, month);
      if (day > daysInMonth)
        day = daysInMonth;
      long ticks = this.GetAbsoluteDatePersian(year, month, day) * 864000000000L + time.Ticks % 864000000000L;
      DateTime supportedDateTime1 = this.MinSupportedDateTime;
      DateTime supportedDateTime2 = this.MaxSupportedDateTime;
      Calendar.CheckAddResult(ticks, supportedDateTime1, supportedDateTime2);
      return new DateTime(ticks);
    }

    /// <summary>返回一个基于指定 <see cref="T:System.DateTime" /> 对象偏移指定年数的 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>通过将指定的年数添加到指定的 <see cref="T:System.DateTime" /> 对象所得到的 <see cref="T:System.DateTime" /> 对象。</returns>
    /// <param name="time">将向其添加年数的 <see cref="T:System.DateTime" />。</param>
    /// <param name="years">要添加的正年数或负年数。</param>
    /// <exception cref="T:System.ArgumentException">生成 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="years" /> 是小于或大于 10000-10000。</exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 对象中的日期是该月的几号。</summary>
    /// <returns>从 1 到 31 之间的一个整数，表示指定 <see cref="T:System.DateTime" /> 对象中的日期是该月的几号。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 参数表示的日期小于 <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 3);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 对象中的日期是星期几。</summary>
    /// <returns>一个 <see cref="T:System.DayOfWeek" /> 值，它表示指定的 <see cref="T:System.DateTime" /> 对象中的日期是星期几。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 对象中的日期是该年中的第几天。</summary>
    /// <returns>从 1 到 366 之间的一个整数，表示指定 <see cref="T:System.DateTime" /> 对象中的日期是该年中的第几天。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 参数表示的日期小于 <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 1);
    }

    /// <summary>返回指定纪元年份中指定月份的天数。</summary>
    /// <returns>指定纪元年份中指定月份的天数。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="month">一个表示月份的整数，如果 <paramref name="year" /> 不是 9378，则值的范围是从 1 到 12；如果 <paramref name="year" /> 是 9378，则值的范围是从 1 到 10。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />, <paramref name="month" />, ，或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      PersianCalendar.CheckYearMonthRange(year, month, era);
      if (month == 10 && year == 9378)
        return 13;
      int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
      if (month == 12 && !this.IsLeapYear(year))
        --num;
      return num;
    }

    /// <summary>返回指定纪元中指定年份的天数。</summary>
    /// <returns>指定纪元年份中的天数。天数在平年中为 365，在闰年中为 366。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      if (year == 9378)
        return PersianCalendar.DaysToMonth[9] + 13;
      return !this.IsLeapYear(year, 0) ? 365 : 366;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 对象中的纪元。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.PersianCalendar.PersianEra" />。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 参数表示的日期小于 <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetEra(DateTime time)
    {
      PersianCalendar.CheckTicksRange(time.Ticks);
      return PersianCalendar.PersianEra;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 对象中的月份。</summary>
    /// <returns>1 到 12 之间的一个整数，表示指定 <see cref="T:System.DateTime" /> 对象中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 参数表示的日期小于 <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>返回指定纪元中指定年份中的月数。</summary>
    /// <returns>如果 <paramref name="year" /> 参数为 9378，则返回 10；否则始终返回 12。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      return year == 9378 ? 10 : 12;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 对象中的年份。</summary>
    /// <returns>一个介于 1 到 9378 之间的整数，表示指定 <see cref="T:System.DateTime" /> 中的年份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 参数表示的日期小于 <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>确定指定的日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="month">一个表示月份的整数，如果 <paramref name="year" /> 不是 9378，则值的范围是从 1 到 12；如果 <paramref name="year" /> 是 9378，则值的范围是从 1 到 10。</param>
    /// <param name="day">1 到 31 之间的一个整数，用于表示日。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />, <paramref name="month" />, ，<paramref name="day" />, ，或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      if (this.IsLeapYear(year, era) && month == 12)
        return day == 30;
      return false;
    }

    /// <summary>返回指定纪元年份的闰月。</summary>
    /// <returns>返回值始终为 0。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示要转换的年份。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetLeapMonth(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      return 0;
    }

    /// <summary>确定指定纪元年份中的指定月份是否为闰月。</summary>
    /// <returns>始终返回 false，因为 <see cref="T:System.Globalization.PersianCalendar" /> 类不支持闰月这一概念。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="month">一个表示月份的整数，如果 <paramref name="year" /> 不是 9378，则值的范围是从 1 到 12；如果 <paramref name="year" /> 是 9378，则值的范围是从 1 到 10。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />, <paramref name="month" />, ，或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      PersianCalendar.CheckYearMonthRange(year, month, era);
      return false;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      PersianCalendar.CheckYearRange(year, era);
      if (year == 9378)
        return false;
      return this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
    }

    /// <summary>返回一个 <see cref="T:System.DateTime" /> 对象，该对象设置为指定日期、时间和纪元。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" /> 对象。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示年。</param>
    /// <param name="month">1 到 12 之间的一个整数，用于表示月。</param>
    /// <param name="day">1 到 31 之间的一个整数，用于表示日。</param>
    /// <param name="hour">0 到 23 之间的一个整数，用于表示小时。</param>
    /// <param name="minute">0 到 59 之间的一个整数，用于表示分钟。</param>
    /// <param name="second">0 到 59 之间的一个整数，用于表示秒。</param>
    /// <param name="millisecond">0 到 999 之间的一个整数，用于表示毫秒。</param>
    /// <param name="era">整数 0 或 1，用于表示纪元。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />, <paramref name="month" />, ，<paramref name="day" />, ，<paramref name="hour" />, ，<paramref name="minute" />, ，<paramref name="second" />, ，<paramref name="millisecond" />, ，或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
      if (absoluteDatePersian >= 0L)
        return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    /// <summary>将指定的年份转换为四位数的年份表示形式。</summary>
    /// <returns>包含 <paramref name="year" /> 的四位数表示形式的整数。</returns>
    /// <param name="year">1 到 9378 之间的一个整数，用于表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 为小于 0 或大于 9378。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year > 9378)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9378));
      return year;
    }
  }
}
