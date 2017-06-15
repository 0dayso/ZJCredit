// Decompiled with JetBrains decompiler
// Type: System.Globalization.HijriCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
  /// <summary>表示回历。</summary>
  [ComVisible(true)]
  [Serializable]
  public class HijriCalendar : Calendar
  {
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public static readonly int HijriEra = 1;
    internal static readonly int[] HijriMonthDays = new int[13]{ 0, 30, 59, 89, 118, 148, 177, 207, 236, 266, 295, 325, 355 };
    internal static readonly DateTime calendarMinValue = new DateTime(622, 7, 18);
    internal static readonly DateTime calendarMaxValue = DateTime.MaxValue;
    private int m_HijriAdvance = int.MinValue;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MinAdvancedHijri = -2;
    internal const int MaxAdvancedHijri = 2;
    private const string InternationalRegKey = "Control Panel\\International";
    private const string HijriAdvanceRegKeyEntry = "AddHijriDate";
    internal const int MaxCalendarYear = 9666;
    internal const int MaxCalendarMonth = 4;
    internal const int MaxCalendarDay = 3;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;

    /// <summary>获取此日历支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.HijriCalendar" /> 类型支持的最早日期和时间，此日期时间相当于公历的公元 622 年 7 月 18 月开始的那一刻。</returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return HijriCalendar.calendarMinValue;
      }
    }

    /// <summary>获取此日历支持的最晚日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.HijriCalendar" /> 类型支持的最晚日期和时间，此日期时间相当于公历的公元 9999 年 12 月 31 日结束的那一刻。</returns>
    [ComVisible(false)]
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return HijriCalendar.calendarMaxValue;
      }
    }

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" />。</returns>
    [ComVisible(false)]
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunarCalendar;
      }
    }

    internal override int ID
    {
      get
      {
        return 6;
      }
    }

    /// <summary>获取指定 <see cref="P:System.Globalization.HijriCalendar.MinSupportedDateTime" /> 属性的指定年份中的天数。</summary>
    /// <returns>由 <see cref="P:System.Globalization.HijriCalendar.MinSupportedDateTime" /> 指定的在年之前的一年的天数。</returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 354;
      }
    }

    /// <summary>获取或设置要在日历中添加或减去的天数，以适应 Ramadan 的开头和结尾中的差异以及国家/地区间的日期差别。</summary>
    /// <returns>-2 到 2 之间的一个整数，表示要在日历中添加或减去的天数。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">此属性当前设置为无效值。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public int HijriAdjustment
    {
      [SecuritySafeCritical] get
      {
        if (this.m_HijriAdvance == int.MinValue)
          this.m_HijriAdvance = HijriCalendar.GetAdvanceHijriDate();
        return this.m_HijriAdvance;
      }
      set
      {
        if (value < -2 || value > 2)
          throw new ArgumentOutOfRangeException("HijriAdjustment", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) -2, (object) 2));
        this.VerifyWritable();
        this.m_HijriAdvance = value;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.HijriCalendar" /> 中的纪元的列表。</summary>
    /// <returns>表示 <see cref="T:System.Globalization.HijriCalendar" /> 中的纪元的整数数组。</returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ HijriCalendar.HijriEra };
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.InvalidOperationException">此日历为只读。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">设置操作中的值小于 100 或大于 9666。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > 9666)
          throw new ArgumentOutOfRangeException("value", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) 9666));
        this.twoDigitYearMax = value;
      }
    }

    private long GetAbsoluteDateHijri(int y, int m, int d)
    {
      return this.DaysUpToHijriYear(y) + (long) HijriCalendar.HijriMonthDays[m - 1] + (long) d - 1L - (long) this.HijriAdjustment;
    }

    private long DaysUpToHijriYear(int HijriYear)
    {
      int num1 = (HijriYear - 1) / 30 * 30;
      int year = HijriYear - num1 - 1;
      long num2 = (long) num1 * 10631L / 30L + 227013L;
      for (; year > 0; --year)
        num2 += (long) (354 + (this.IsLeapYear(year, 0) ? 1 : 0));
      return num2;
    }

    [SecurityCritical]
    private static int GetAdvanceHijriDate()
    {
      int num1 = 0;
      RegistryKey registryKey;
      try
      {
        registryKey = Registry.CurrentUser.InternalOpenSubKey("Control Panel\\International", false);
      }
      catch (ObjectDisposedException ex)
      {
        return 0;
      }
      catch (ArgumentException ex)
      {
        return 0;
      }
      if (registryKey != null)
      {
        try
        {
          object obj = registryKey.InternalGetValue("AddHijriDate", (object) null, false, false);
          if (obj == null)
            return 0;
          string @string = obj.ToString();
          if (string.Compare(@string, 0, "AddHijriDate", 0, "AddHijriDate".Length, StringComparison.OrdinalIgnoreCase) == 0)
          {
            if (@string.Length == "AddHijriDate".Length)
            {
              num1 = -1;
            }
            else
            {
              string str = @string.Substring("AddHijriDate".Length);
              try
              {
                int num2 = int.Parse(str.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
                if (num2 >= -2)
                {
                  if (num2 <= 2)
                    num1 = num2;
                }
              }
              catch (ArgumentException ex)
              {
              }
              catch (FormatException ex)
              {
              }
              catch (OverflowException ex)
              {
              }
            }
          }
        }
        finally
        {
          registryKey.Close();
        }
      }
      return num1;
    }

    internal static void CheckTicksRange(long ticks)
    {
      if (ticks < HijriCalendar.calendarMinValue.Ticks || ticks > HijriCalendar.calendarMaxValue.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) HijriCalendar.calendarMinValue, (object) HijriCalendar.calendarMaxValue));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != HijriCalendar.HijriEra)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal static void CheckYearRange(int year, int era)
    {
      HijriCalendar.CheckEraRange(era);
      if (year < 1 || year > 9666)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9666));
    }

    internal static void CheckYearMonthRange(int year, int month, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      if (year == 9666 && month > 4)
        throw new ArgumentOutOfRangeException("month", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 4));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
      HijriCalendar.CheckTicksRange(ticks);
      long num1 = ticks / 864000000000L + 1L + (long) this.HijriAdjustment;
      int num2 = (int) ((num1 - 227013L) * 30L / 10631L) + 1;
      long hijriYear = this.DaysUpToHijriYear(num2);
      long num3 = (long) this.GetDaysInYear(num2, 0);
      if (num1 < hijriYear)
      {
        hijriYear -= num3;
        --num2;
      }
      else if (num1 == hijriYear)
      {
        --num2;
        hijriYear -= (long) this.GetDaysInYear(num2, 0);
      }
      else if (num1 > hijriYear + num3)
      {
        hijriYear += num3;
        ++num2;
      }
      if (part == 0)
        return num2;
      int num4 = 1;
      long num5 = num1 - hijriYear;
      if (part == 1)
        return (int) num5;
      while (num4 <= 12 && num5 > (long) HijriCalendar.HijriMonthDays[num4 - 1])
        ++num4;
      int num6 = num4 - 1;
      if (part == 2)
        return num6;
      int num7 = (int) (num5 - (long) HijriCalendar.HijriMonthDays[num6 - 1]);
      if (part == 3)
        return num7;
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定月数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定的月数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">将向其添加月数的 <see cref="T:System.DateTime" />。</param>
    /// <param name="months">要添加的月数。</param>
    /// <exception cref="T:System.ArgumentException">产生的 <see cref="T:System.DateTime" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于 -120000。- 或 -<paramref name="months" /> 大于 120000。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException("months", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = this.GetDatePart(time.Ticks, 0);
      int datePart2 = this.GetDatePart(time.Ticks, 2);
      int d = this.GetDatePart(time.Ticks, 3);
      int num1 = datePart2 - 1 + months;
      int num2;
      int num3;
      if (num1 >= 0)
      {
        num2 = num1 % 12 + 1;
        num3 = datePart1 + num1 / 12;
      }
      else
      {
        num2 = 12 + (num1 + 1) % 12;
        num3 = datePart1 + (num1 - 11) / 12;
      }
      int daysInMonth = this.GetDaysInMonth(num3, num2);
      if (d > daysInMonth)
        d = daysInMonth;
      long ticks = this.GetAbsoluteDateHijri(num3, num2, d) * 864000000000L + time.Ticks % 864000000000L;
      DateTime supportedDateTime1 = this.MinSupportedDateTime;
      DateTime supportedDateTime2 = this.MaxSupportedDateTime;
      Calendar.CheckAddResult(ticks, supportedDateTime1, supportedDateTime2);
      return new DateTime(ticks);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定年数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定年数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">将向其添加年数的 <see cref="T:System.DateTime" />。</param>
    /// <param name="years">要添加的年数。</param>
    /// <exception cref="T:System.ArgumentException">结果 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</summary>
    /// <returns>从 1 到 30 的整数，表示指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 3);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是星期几。</summary>
    /// <returns>一个 <see cref="T:System.DayOfWeek" /> 值，它表示指定的 <see cref="T:System.DateTime" /> 中的日期是星期几。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</summary>
    /// <returns>从 1 到 355 的整数，表示指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 1);
    }

    /// <summary>返回指定纪元年份中指定月份的天数。</summary>
    /// <returns>指定纪元中指定年份的指定月份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了此日历支持的范围。- 或 -<paramref name="year" /> 超出了此日历支持的范围。- 或 -<paramref name="month" /> 超出了此日历支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      HijriCalendar.CheckYearMonthRange(year, month, era);
      if (month == 12)
        return !this.IsLeapYear(year, 0) ? 29 : 30;
      return month % 2 != 1 ? 29 : 30;
    }

    /// <summary>返回指定纪元年份中的天数。</summary>
    /// <returns>指定纪元年份中的天数。天数在平年中为 354，在闰年中为 355。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return !this.IsLeapYear(year, 0) ? 354 : 355;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的纪元。</summary>
    /// <returns>表示指定的 <see cref="T:System.DateTime" /> 中纪元的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetEra(DateTime time)
    {
      HijriCalendar.CheckTicksRange(time.Ticks);
      return HijriCalendar.HijriEra;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的月份。</summary>
    /// <returns>1 到 12 之间的一个整数，用于表示指定的 <see cref="T:System.DateTime" /> 中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>返回指定纪元年份中的月数。</summary>
    /// <returns>指定纪元年份中的月数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了此日历支持的范围。- 或 -<paramref name="year" /> 超出了此日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return 12;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的年份。</summary>
    /// <returns>一个整数，它表示指定的 <see cref="T:System.DateTime" /> 中的年份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>确定指定的日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 30 之间的一个整数，它表示天。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了此日历支持的范围。- 或 -<paramref name="year" /> 超出了此日历支持的范围。- 或 -<paramref name="month" /> 超出了此日历支持的范围。- 或 -<paramref name="day" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      if (this.IsLeapYear(year, era) && month == 12)
        return day == 30;
      return false;
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>总为 0，因为 <see cref="T:System.Globalization.HijriCalendar" /> 类型不支持闰月的概念。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 <see cref="F:System.Globalization.Calendar.CurrentEra" /> 或 <see cref="F:System.Globalization.HijriCalendar.HijriEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于回历年 1 或大于 9666 年。- 或 -<paramref name="era" /> 不是 <see cref="F:System.Globalization.Calendar.CurrentEra" /> 或 <see cref="F:System.Globalization.HijriCalendar.HijriEra" />。</exception>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return 0;
    }

    /// <summary>确定指定纪元年份中的指定月份是否为闰月。</summary>
    /// <returns>此方法通常返回 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了此日历支持的范围。- 或 -<paramref name="year" /> 超出了此日历支持的范围。- 或 -<paramref name="month" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      HijriCalendar.CheckYearMonthRange(year, month, era);
      return false;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了此日历支持的范围。- 或 -<paramref name="year" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      HijriCalendar.CheckYearRange(year, era);
      return (year * 11 + 14) % 30 < 11;
    }

    /// <summary>返回设置为指定的日期、时间和纪元的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 30 之间的一个整数，它表示天。</param>
    /// <param name="hour">0 与 23 之间的一个整数，它表示小时。</param>
    /// <param name="minute">0 与 59 之间的一个整数，它表示分钟。</param>
    /// <param name="second">0 与 59 之间的一个整数，它表示秒。</param>
    /// <param name="millisecond">0 与 999 之间的一个整数，它表示毫秒。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了此日历支持的范围。- 或 -<paramref name="year" /> 超出了此日历支持的范围。- 或 -<paramref name="month" /> 超出了此日历支持的范围。- 或 -<paramref name="day" /> 超出了此日历支持的范围。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于零或大于 59。- 或 -<paramref name="second" /> 小于零或大于 59。- 或 -<paramref name="millisecond" /> 小于零或大于 999。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      long absoluteDateHijri = this.GetAbsoluteDateHijri(year, month, day);
      if (absoluteDateHijri >= 0L)
        return new DateTime(absoluteDateHijri * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    /// <summary>使用 <see cref="P:System.Globalization.HijriCalendar.TwoDigitYearMax" /> 属性将指定的年份转换为四位数年份，以确定相应的纪元。</summary>
    /// <returns>包含 <paramref name="year" /> 的四位数表示形式的整数。</returns>
    /// <param name="year">一个两位数或四位数的整数，表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了此日历支持的范围。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year > 9666)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9666));
      return year;
    }
  }
}
