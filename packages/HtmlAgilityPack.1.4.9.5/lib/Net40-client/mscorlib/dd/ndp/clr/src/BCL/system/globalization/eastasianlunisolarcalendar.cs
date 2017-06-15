// Decompiled with JetBrains decompiler
// Type: System.Globalization.EastAsianLunisolarCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>表示一种日历，它将时间分为月、日、年和纪元，并且其日期基于太阳和月亮的循环。</summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class EastAsianLunisolarCalendar : Calendar
  {
    internal static readonly int[] DaysToMonth365 = new int[12]{ 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
    internal static readonly int[] DaysToMonth366 = new int[12]{ 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };
    internal const int LeapMonth = 0;
    internal const int Jan1Month = 1;
    internal const int Jan1Date = 2;
    internal const int nDaysPerMonth = 3;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MaxCalendarMonth = 13;
    internal const int MaxCalendarDay = 30;
    private const int DEFAULT_GREGORIAN_TWO_DIGIT_YEAR_MAX = 2029;

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />。</returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunisolarCalendar;
      }
    }

    internal abstract int MinCalendarYear { get; }

    internal abstract int MaxCalendarYear { get; }

    internal abstract EraInfo[] CalEraInfo { get; }

    internal abstract DateTime MinDate { get; }

    internal abstract DateTime MaxDate { get; }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前的 <see cref="T:System.Globalization.EastAsianLunisolarCalendar" /> 为只读。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">set 操作中的值小于 99 或大于当前日历中支持的最大年份。</exception>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.BaseCalendarID, this.GetYear(new DateTime(2029, 1, 1)));
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.MaxCalendarYear)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 99, (object) this.MaxCalendarYear));
        this.twoDigitYearMax = value;
      }
    }

    internal EastAsianLunisolarCalendar()
    {
    }

    /// <summary>计算与指定日期对应的甲子（60 年）循环中的年。</summary>
    /// <returns>甲子循环中的一个从 1 到 60 的数字，它与 <paramref name="date" /> 参数对应。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public virtual int GetSexagenaryYear(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return (year - 4) % 60 + 1;
    }

    /// <summary>计算甲子（60 年）循环中指定年份的天干。</summary>
    /// <returns>一个从 1 到 10 的数字。</returns>
    /// <param name="sexagenaryYear">一个从 1 到 60 的整数，表示甲子循环中的一年。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sexagenaryYear" /> 小于 1 或大于 60。</exception>
    public int GetCelestialStem(int sexagenaryYear)
    {
      if (sexagenaryYear < 1 || sexagenaryYear > 60)
        throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 60));
      return (sexagenaryYear - 1) % 10 + 1;
    }

    /// <summary>计算甲子（60 年）循环中指定年份的地支。</summary>
    /// <returns>一个从 1 到 12 的整数。</returns>
    /// <param name="sexagenaryYear">一个从 1 到 60 的整数，表示甲子循环中的一年。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sexagenaryYear" /> 小于 1 或大于 60。</exception>
    public int GetTerrestrialBranch(int sexagenaryYear)
    {
      if (sexagenaryYear < 1 || sexagenaryYear > 60)
        throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 60));
      return (sexagenaryYear - 1) % 12 + 1;
    }

    internal abstract int GetYearInfo(int LunarYear, int Index);

    internal abstract int GetYear(int year, DateTime time);

    internal abstract int GetGregorianYear(int year, int era);

    internal int MinEraCalendarYear(int era)
    {
      EraInfo[] calEraInfo = this.CalEraInfo;
      if (calEraInfo == null)
        return this.MinCalendarYear;
      if (era == 0)
        era = this.CurrentEraValue;
      if (era == this.GetEra(this.MinDate))
        return this.GetYear(this.MinCalendarYear, this.MinDate);
      for (int index = 0; index < calEraInfo.Length; ++index)
      {
        if (era == calEraInfo[index].era)
          return calEraInfo[index].minEraYear;
      }
      throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal int MaxEraCalendarYear(int era)
    {
      EraInfo[] calEraInfo = this.CalEraInfo;
      if (calEraInfo == null)
        return this.MaxCalendarYear;
      if (era == 0)
        era = this.CurrentEraValue;
      if (era == this.GetEra(this.MaxDate))
        return this.GetYear(this.MaxCalendarYear, this.MaxDate);
      for (int index = 0; index < calEraInfo.Length; ++index)
      {
        if (era == calEraInfo[index].era)
          return calEraInfo[index].maxEraYear;
      }
      throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal void CheckTicksRange(long ticks)
    {
      if (ticks < this.MinSupportedDateTime.Ticks || ticks > this.MaxSupportedDateTime.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) this.MinSupportedDateTime, (object) this.MaxSupportedDateTime));
    }

    internal void CheckEraRange(int era)
    {
      if (era == 0)
        era = this.CurrentEraValue;
      if (era < this.GetEra(this.MinDate) || era > this.GetEra(this.MaxDate))
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal int CheckYearRange(int year, int era)
    {
      this.CheckEraRange(era);
      year = this.GetGregorianYear(year, era);
      if (year < this.MinCalendarYear || year > this.MaxCalendarYear)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) this.MinEraCalendarYear(era), (object) this.MaxEraCalendarYear(era)));
      return year;
    }

    internal int CheckYearMonthRange(int year, int month, int era)
    {
      year = this.CheckYearRange(year, era);
      if (month == 13 && this.GetYearInfo(year, 0) == 0)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
      if (month < 1 || month > 13)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
      return year;
    }

    internal int InternalGetDaysInMonth(int year, int month)
    {
      int num = 32768 >> month - 1;
      return (this.GetYearInfo(year, 3) & num) != 0 ? 30 : 29;
    }

    /// <summary>计算指定纪元年份的指定月份中的天数。</summary>
    /// <returns>指定纪元年份中指定月份的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">一个从 1 到 12（在平年中）或从 1 到 13（在闰年中）的整数，用于表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      return this.InternalGetDaysInMonth(year, month);
    }

    private static int GregorianIsLeapYear(int y)
    {
      return y % 4 == 0 && (y % 100 != 0 || y % 400 == 0) ? 1 : 0;
    }

    /// <summary>返回设置为指定的日期、时间和纪元的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>一个设置为指定的日期、时间和纪元的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，用于表示月。</param>
    /// <param name="day">1 到 31 之间的一个整数，用于表示日。</param>
    /// <param name="hour">0 到 23 之间的一个整数，用于表示小时。</param>
    /// <param name="minute">0 到 59 之间的一个整数，用于表示分钟。</param>
    /// <param name="second">0 到 59 之间的一个整数，用于表示秒。</param>
    /// <param name="millisecond">0 到 999 之间的一个整数，用于表示毫秒。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" />、<paramref name="day" />、<paramref name="hour" />、<paramref name="minute" />、<paramref name="second" />、<paramref name="millisecond" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      int daysInMonth = this.InternalGetDaysInMonth(year, month);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Day", (object) daysInMonth, (object) month));
      int nSolarYear = 0;
      int nSolarMonth = 0;
      int nSolarDay = 0;
      if (this.LunarToGregorian(year, month, day, ref nSolarYear, ref nSolarMonth, ref nSolarDay))
        return new DateTime(nSolarYear, nSolarMonth, nSolarDay, hour, minute, second, millisecond);
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    internal void GregorianToLunar(int nSYear, int nSMonth, int nSDate, ref int nLYear, ref int nLMonth, ref int nLDate)
    {
      int num1 = (EastAsianLunisolarCalendar.GregorianIsLeapYear(nSYear) == 1 ? EastAsianLunisolarCalendar.DaysToMonth366[nSMonth - 1] : EastAsianLunisolarCalendar.DaysToMonth365[nSMonth - 1]) + nSDate;
      nLYear = nSYear;
      int yearInfo1;
      int yearInfo2;
      if (nLYear == this.MaxCalendarYear + 1)
      {
        --nLYear;
        num1 += EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1 ? 366 : 365;
        yearInfo1 = this.GetYearInfo(nLYear, 1);
        yearInfo2 = this.GetYearInfo(nLYear, 2);
      }
      else
      {
        yearInfo1 = this.GetYearInfo(nLYear, 1);
        yearInfo2 = this.GetYearInfo(nLYear, 2);
        if (nSMonth < yearInfo1 || nSMonth == yearInfo1 && nSDate < yearInfo2)
        {
          --nLYear;
          num1 += EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1 ? 366 : 365;
          yearInfo1 = this.GetYearInfo(nLYear, 1);
          yearInfo2 = this.GetYearInfo(nLYear, 2);
        }
      }
      int num2 = num1 - EastAsianLunisolarCalendar.DaysToMonth365[yearInfo1 - 1] - (yearInfo2 - 1);
      int num3 = 32768;
      int yearInfo3 = this.GetYearInfo(nLYear, 3);
      int num4 = (yearInfo3 & num3) != 0 ? 30 : 29;
      nLMonth = 1;
      for (; num2 > num4; num4 = (yearInfo3 & num3) != 0 ? 30 : 29)
      {
        num2 -= num4;
        ++nLMonth;
        num3 >>= 1;
      }
      nLDate = num2;
    }

    internal bool LunarToGregorian(int nLYear, int nLMonth, int nLDate, ref int nSolarYear, ref int nSolarMonth, ref int nSolarDay)
    {
      if (nLDate < 1 || nLDate > 30)
        return false;
      int num1 = nLDate - 1;
      for (int month = 1; month < nLMonth; ++month)
        num1 += this.InternalGetDaysInMonth(nLYear, month);
      int yearInfo1 = this.GetYearInfo(nLYear, 1);
      int yearInfo2 = this.GetYearInfo(nLYear, 2);
      int num2 = EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear);
      int[] numArray = num2 == 1 ? EastAsianLunisolarCalendar.DaysToMonth366 : EastAsianLunisolarCalendar.DaysToMonth365;
      nSolarDay = yearInfo2;
      if (yearInfo1 > 1)
        nSolarDay += numArray[yearInfo1 - 1];
      nSolarDay += num1;
      if (nSolarDay > num2 + 365)
      {
        nSolarYear = nLYear + 1;
        nSolarDay -= num2 + 365;
      }
      else
        nSolarYear = nLYear;
      nSolarMonth = 1;
      while (nSolarMonth < 12 && numArray[nSolarMonth] < nSolarDay)
        ++nSolarMonth;
      nSolarDay -= numArray[nSolarMonth - 1];
      return true;
    }

    internal DateTime LunarToTime(DateTime time, int year, int month, int day)
    {
      int nSolarYear = 0;
      int nSolarMonth = 0;
      int nSolarDay = 0;
      this.LunarToGregorian(year, month, day, ref nSolarYear, ref nSolarMonth, ref nSolarDay);
      return GregorianCalendar.GetDefaultInstance().ToDateTime(nSolarYear, nSolarMonth, nSolarDay, time.Hour, time.Minute, time.Second, time.Millisecond);
    }

    internal void TimeToLunar(DateTime time, ref int year, ref int month, ref int day)
    {
      Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
      DateTime time1 = time;
      int year1 = defaultInstance.GetYear(time1);
      DateTime time2 = time;
      int month1 = defaultInstance.GetMonth(time2);
      DateTime time3 = time;
      int dayOfMonth = defaultInstance.GetDayOfMonth(time3);
      this.GregorianToLunar(year1, month1, dayOfMonth, ref year, ref month, ref day);
    }

    /// <summary>计算与指定日期相距指定月数的日期。</summary>
    /// <returns>一个新的 <see cref="T:System.DateTime" />，通过在 <paramref name="time" /> 参数中添加指定的月数得到。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加 <paramref name="months" />。</param>
    /// <param name="months">要添加的月数。</param>
    /// <exception cref="T:System.ArgumentException">结果超出了 <see cref="T:System.DateTime" /> 支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于 -120,000 或大于 120,000。- 或 -<paramref name="time" /> 小于 <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />。</exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) -120000, (object) 120000));
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month1 = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month1, ref day);
      int num1 = month1 + months;
      int month2;
      if (num1 > 0)
      {
        for (int index = this.InternalIsLeapYear(year) ? 13 : 12; num1 - index > 0; index = this.InternalIsLeapYear(year) ? 13 : 12)
        {
          num1 -= index;
          ++year;
        }
        month2 = num1;
      }
      else
      {
        while (num1 <= 0)
        {
          int num2 = this.InternalIsLeapYear(year - 1) ? 13 : 12;
          num1 += num2;
          --year;
        }
        month2 = num1;
      }
      int daysInMonth = this.InternalGetDaysInMonth(year, month2);
      if (day > daysInMonth)
        day = daysInMonth;
      DateTime time1 = this.LunarToTime(time, year, month2, day);
      Calendar.CheckAddResult(time1.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return time1;
    }

    /// <summary>计算与指定日期相距指定年数的日期。</summary>
    /// <returns>一个新的 <see cref="T:System.DateTime" />，通过在 <paramref name="time" /> 参数中添加指定的年数得到。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加 <paramref name="years" />。</param>
    /// <param name="years">要添加的年数。</param>
    /// <exception cref="T:System.ArgumentException">结果超出了 <see cref="T:System.DateTime" /> 支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 小于 <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />。</exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      this.CheckTicksRange(time.Ticks);
      int year1 = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year1, ref month, ref day);
      int year2 = year1 + years;
      if (month == 13 && !this.InternalIsLeapYear(year2))
      {
        month = 12;
        day = this.InternalGetDaysInMonth(year2, month);
      }
      int daysInMonth = this.InternalGetDaysInMonth(year2, month);
      if (day > daysInMonth)
        day = daysInMonth;
      DateTime time1 = this.LunarToTime(time, year2, month, day);
      Calendar.CheckAddResult(time1.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return time1;
    }

    /// <summary>计算指定日期中的年中日期。</summary>
    /// <returns>一个从 1 到 354（在平年中）或从 1 到 384（在闰年中）的整数，表示 <paramref name="time" /> 参数中指定的年中日期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetDayOfYear(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month1 = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month1, ref day);
      for (int month2 = 1; month2 < month1; ++month2)
        day += this.InternalGetDaysInMonth(year, month2);
      return day;
    }

    /// <summary>计算指定日期中的月中日期。</summary>
    /// <returns>一个从 1 到 31 的整数，表示 <paramref name="time" /> 参数中指定的月中日期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetDayOfMonth(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return day;
    }

    /// <summary>计算指定纪元年份中的天数。</summary>
    /// <returns>指定纪元年份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      int num1 = 0;
      int num2 = this.InternalIsLeapYear(year) ? 13 : 12;
      while (num2 != 0)
        num1 += this.InternalGetDaysInMonth(year, num2--);
      return num1;
    }

    /// <summary>返回指定日期中的月份。</summary>
    /// <returns>一个 1 到 13 之间的整数，表示 <paramref name="time" /> 参数中指定的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetMonth(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return month;
    }

    /// <summary>返回指定日期中的年份。</summary>
    /// <returns>一个整数，它表示指定的 <see cref="T:System.DateTime" /> 中的年份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetYear(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      int year = 0;
      int month = 0;
      int day = 0;
      this.TimeToLunar(time, ref year, ref month, ref day);
      return this.GetYear(year, time);
    }

    /// <summary>计算指定日期中的周中日期。</summary>
    /// <returns>
    /// <see cref="T:System.DayOfWeek" /> 值之一，表示 <paramref name="time" /> 参数中指定的周中日期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 小于 <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />。</exception>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>计算指定纪元年份中的月数。</summary>
    /// <returns>指定纪元中指定年份的月数。返回值是 12 个月（在平年中）或 13 个月（在闰年中）。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      return !this.InternalIsLeapYear(year) ? 12 : 13;
    }

    /// <summary>确定指定纪元中的指定日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，用于表示月。</param>
    /// <param name="day">1 到 31 之间的一个整数，用于表示日。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" />、<paramref name="day" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      int daysInMonth = this.InternalGetDaysInMonth(year, month);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Day", (object) daysInMonth, (object) month));
      int yearInfo = this.GetYearInfo(year, 0);
      if (yearInfo != 0)
        return month == yearInfo + 1;
      return false;
    }

    /// <summary>确定指定纪元年份中的指定月份是否为闰月。</summary>
    /// <returns>如果 <paramref name="month" /> 参数是闰月，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，用于表示月。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      year = this.CheckYearMonthRange(year, month, era);
      int yearInfo = this.GetYearInfo(year, 0);
      if (yearInfo != 0)
        return month == yearInfo + 1;
      return false;
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>一个从 1 到 13 的正整数，表示指定纪元年份的闰月。 - 或 -如果此日历不支持闰月，或者 <paramref name="year" /> 和 <paramref name="era" /> 参数未指定闰年，则为零。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    public override int GetLeapMonth(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      int yearInfo = this.GetYearInfo(year, 0);
      if (yearInfo > 0)
        return yearInfo + 1;
      return 0;
    }

    internal bool InternalIsLeapYear(int year)
    {
      return (uint) this.GetYearInfo(year, 0) > 0U;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      year = this.CheckYearRange(year, era);
      return this.InternalIsLeapYear(year);
    }

    /// <summary>将指定的年份转换为一个四位数的年份。</summary>
    /// <returns>包含 <paramref name="year" /> 参数的四位数表示形式的整数。</returns>
    /// <param name="year">一个两位数或四位数的整数，表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了此日历支持的范围。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      year = base.ToFourDigitYear(year);
      this.CheckYearRange(year, 0);
      return year;
    }
  }
}
