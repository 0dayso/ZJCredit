// Decompiled with JetBrains decompiler
// Type: System.Globalization.HebrewCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>表示犹太历。</summary>
  [ComVisible(true)]
  [Serializable]
  public class HebrewCalendar : Calendar
  {
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public static readonly int HebrewEra = 1;
    private static readonly int[] HebrewTable = new int[1316]{ 7, 3, 17, 3, 0, 4, 11, 2, 21, 6, 1, 3, 13, 2, 25, 4, 5, 3, 16, 2, 27, 6, 9, 1, 20, 2, 0, 6, 11, 3, 23, 4, 4, 2, 14, 3, 27, 4, 8, 2, 18, 3, 28, 6, 11, 1, 22, 5, 2, 3, 12, 3, 25, 4, 6, 2, 16, 3, 26, 6, 8, 2, 20, 1, 0, 6, 11, 2, 24, 4, 4, 3, 15, 2, 25, 6, 8, 1, 19, 2, 29, 6, 9, 3, 22, 4, 3, 2, 13, 3, 25, 4, 6, 3, 17, 2, 27, 6, 7, 3, 19, 2, 31, 4, 11, 3, 23, 4, 5, 2, 15, 3, 25, 6, 6, 2, 19, 1, 29, 6, 10, 2, 22, 4, 3, 3, 14, 2, 24, 6, 6, 1, 17, 3, 28, 5, 8, 3, 20, 1, 32, 5, 12, 3, 22, 6, 4, 1, 16, 2, 26, 6, 6, 3, 17, 2, 0, 4, 10, 3, 22, 4, 3, 2, 14, 3, 24, 6, 5, 2, 17, 1, 28, 6, 9, 2, 19, 3, 31, 4, 13, 2, 23, 6, 3, 3, 15, 1, 27, 5, 7, 3, 17, 3, 29, 4, 11, 2, 21, 6, 3, 1, 14, 2, 25, 6, 5, 3, 16, 2, 28, 4, 9, 3, 20, 2, 0, 6, 12, 1, 23, 6, 4, 2, 14, 3, 26, 4, 8, 2, 18, 3, 0, 4, 10, 3, 21, 5, 1, 3, 13, 1, 24, 5, 5, 3, 15, 3, 27, 4, 8, 2, 19, 3, 29, 6, 10, 2, 22, 4, 3, 3, 14, 2, 26, 4, 6, 3, 18, 2, 28, 6, 10, 1, 20, 6, 2, 2, 12, 3, 24, 4, 5, 2, 16, 3, 28, 4, 8, 3, 19, 2, 0, 6, 12, 1, 23, 5, 3, 3, 14, 3, 26, 4, 7, 2, 17, 3, 28, 6, 9, 2, 21, 4, 1, 3, 13, 2, 25, 4, 5, 3, 16, 2, 27, 6, 9, 1, 19, 3, 0, 5, 11, 3, 23, 4, 4, 2, 14, 3, 25, 6, 7, 1, 18, 2, 28, 6, 9, 3, 21, 4, 2, 2, 12, 3, 25, 4, 6, 2, 16, 3, 26, 6, 8, 2, 20, 1, 0, 6, 11, 2, 22, 6, 4, 1, 15, 2, 25, 6, 6, 3, 18, 1, 29, 5, 9, 3, 22, 4, 2, 3, 13, 2, 23, 6, 4, 3, 15, 2, 27, 4, 7, 3, 19, 2, 31, 4, 11, 3, 21, 6, 3, 2, 15, 1, 25, 6, 6, 2, 17, 3, 29, 4, 10, 2, 20, 6, 3, 1, 13, 3, 24, 5, 4, 3, 16, 1, 27, 5, 7, 3, 17, 3, 0, 4, 11, 2, 21, 6, 1, 3, 13, 2, 25, 4, 5, 3, 16, 2, 29, 4, 9, 3, 19, 6, 30, 2, 13, 1, 23, 6, 4, 2, 14, 3, 27, 4, 8, 2, 18, 3, 0, 4, 11, 3, 22, 5, 2, 3, 14, 1, 26, 5, 6, 3, 16, 3, 28, 4, 10, 2, 20, 6, 30, 3, 11, 2, 24, 4, 4, 3, 15, 2, 25, 6, 8, 1, 19, 2, 29, 6, 9, 3, 22, 4, 3, 2, 13, 3, 25, 4, 7, 2, 17, 3, 27, 6, 9, 1, 21, 5, 1, 3, 11, 3, 23, 4, 5, 2, 15, 3, 25, 6, 6, 2, 19, 1, 29, 6, 10, 2, 22, 4, 3, 3, 14, 2, 24, 6, 6, 1, 18, 2, 28, 6, 8, 3, 20, 4, 2, 2, 12, 3, 24, 4, 4, 3, 16, 2, 26, 6, 6, 3, 17, 2, 0, 4, 10, 3, 22, 4, 3, 2, 14, 3, 24, 6, 5, 2, 17, 1, 28, 6, 9, 2, 21, 4, 1, 3, 13, 2, 23, 6, 5, 1, 15, 3, 27, 5, 7, 3, 19, 1, 0, 5, 10, 3, 22, 4, 2, 3, 13, 2, 24, 6, 4, 3, 15, 2, 27, 4, 8, 3, 20, 4, 1, 2, 11, 3, 22, 6, 3, 2, 15, 1, 25, 6, 7, 2, 17, 3, 29, 4, 10, 2, 21, 6, 1, 3, 13, 1, 24, 5, 5, 3, 15, 3, 27, 4, 8, 2, 19, 6, 1, 1, 12, 2, 22, 6, 3, 3, 14, 2, 26, 4, 6, 3, 18, 2, 28, 6, 10, 1, 20, 6, 2, 2, 12, 3, 24, 4, 5, 2, 16, 3, 28, 4, 9, 2, 19, 6, 30, 3, 12, 1, 23, 5, 3, 3, 14, 3, 26, 4, 7, 2, 17, 3, 28, 6, 9, 2, 21, 4, 1, 3, 13, 2, 25, 4, 5, 3, 16, 2, 27, 6, 9, 1, 19, 6, 30, 2, 11, 3, 23, 4, 4, 2, 14, 3, 27, 4, 7, 3, 18, 2, 28, 6, 11, 1, 22, 5, 2, 3, 12, 3, 25, 4, 6, 2, 16, 3, 26, 6, 8, 2, 20, 4, 30, 3, 11, 2, 24, 4, 4, 3, 15, 2, 25, 6, 8, 1, 18, 3, 29, 5, 9, 3, 22, 4, 3, 2, 13, 3, 23, 6, 6, 1, 17, 2, 27, 6, 7, 3, 20, 4, 1, 2, 11, 3, 23, 4, 5, 2, 15, 3, 25, 6, 6, 2, 19, 1, 29, 6, 10, 2, 20, 6, 3, 1, 14, 2, 24, 6, 4, 3, 17, 1, 28, 5, 8, 3, 20, 4, 1, 3, 12, 2, 22, 6, 2, 3, 14, 2, 26, 4, 6, 3, 17, 2, 0, 4, 10, 3, 20, 6, 1, 2, 14, 1, 24, 6, 5, 2, 15, 3, 28, 4, 9, 2, 19, 6, 1, 1, 12, 3, 23, 5, 3, 3, 15, 1, 27, 5, 7, 3, 17, 3, 29, 4, 11, 2, 21, 6, 1, 3, 12, 2, 25, 4, 5, 3, 16, 2, 28, 4, 9, 3, 19, 6, 30, 2, 12, 1, 23, 6, 4, 2, 14, 3, 26, 4, 8, 2, 18, 3, 0, 4, 10, 3, 22, 5, 2, 3, 14, 1, 25, 5, 6, 3, 16, 3, 28, 4, 9, 2, 20, 6, 30, 3, 11, 2, 23, 4, 4, 3, 15, 2, 27, 4, 7, 3, 19, 2, 29, 6, 11, 1, 21, 6, 3, 2, 13, 3, 25, 4, 6, 2, 17, 3, 27, 6, 9, 1, 20, 5, 30, 3, 10, 3, 22, 4, 3, 2, 14, 3, 24, 6, 5, 2, 17, 1, 28, 6, 9, 2, 21, 4, 1, 3, 13, 2, 23, 6, 5, 1, 16, 2, 27, 6, 7, 3, 19, 4, 30, 2, 11, 3, 23, 4, 3, 3, 14, 2, 25, 6, 5, 3, 16, 2, 28, 4, 9, 3, 21, 4, 2, 2, 12, 3, 23, 6, 4, 2, 16, 1, 26, 6, 8, 2, 20, 4, 30, 3, 11, 2, 22, 6, 4, 1, 14, 3, 25, 5, 6, 3, 18, 1, 29, 5, 9, 3, 22, 4, 2, 3, 13, 2, 23, 6, 4, 3, 15, 2, 27, 4, 7, 3, 20, 4, 1, 2, 11, 3, 21, 6, 3, 2, 15, 1, 25, 6, 6, 2, 17, 3, 29, 4, 10, 2, 20, 6, 3, 1, 13, 3, 24, 5, 4, 3, 17, 1, 28, 5, 8, 3, 18, 6, 1, 1, 12, 2, 22, 6, 2, 3, 14, 2, 26, 4, 6, 3, 17, 2, 28, 6, 10, 1, 20, 6, 1, 2, 12, 3, 24, 4, 5, 2, 15, 3, 28, 4, 9, 2, 19, 6, 33, 3, 12, 1, 23, 5, 3, 3, 13, 3, 25, 4, 6, 2, 16, 3, 26, 6, 8, 2, 20, 4, 30, 3, 11, 2, 24, 4, 4, 3, 15, 2, 25, 6, 8, 1, 18, 6, 33, 2, 9, 3, 22, 4, 3, 2, 13, 3, 25, 4, 6, 3, 17, 2, 27, 6, 9, 1, 21, 5, 1, 3, 11, 3, 23, 4, 5, 2, 15, 3, 25, 6, 6, 2, 19, 4, 33, 3, 10, 2, 22, 4, 3, 3, 14, 2, 24, 6, 6, 1 };
    private static readonly int[,] LunarMonthLen = new int[7, 14]{ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 30, 29, 29, 29, 30, 29, 30, 29, 30, 29, 30, 29, 0 }, { 0, 30, 29, 30, 29, 30, 29, 30, 29, 30, 29, 30, 29, 0 }, { 0, 30, 30, 30, 29, 30, 29, 30, 29, 30, 29, 30, 29, 0 }, { 0, 30, 29, 29, 29, 30, 30, 29, 30, 29, 30, 29, 30, 29 }, { 0, 30, 29, 30, 29, 30, 30, 29, 30, 29, 30, 29, 30, 29 }, { 0, 30, 30, 30, 29, 30, 30, 29, 30, 29, 30, 29, 30, 29 } };
    internal static readonly DateTime calendarMinValue = new DateTime(1583, 1, 1);
    internal static readonly DateTime calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int DatePartDayOfWeek = 4;
    private const int HebrewYearOf1AD = 3760;
    private const int FirstGregorianTableYear = 1583;
    private const int LastGregorianTableYear = 2239;
    private const int TABLESIZE = 656;
    private const int MinHebrewYear = 5343;
    private const int MaxHebrewYear = 5999;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 5790;

    /// <summary>获取 <see cref="T:System.Globalization.HebrewCalendar" /> 类型支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.HebrewCalendar" /> 类型支持的最早日期和时间，此日期时间相当于公历的公元 1583 年 1 月 1 日开始的那一刻。</returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return HebrewCalendar.calendarMinValue;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.HebrewCalendar" /> 类型支持的最晚日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.HebrewCalendar" /> 类型支持的最晚日期和时间，此日期时间为公历的公元 2239 年 9 月 29 日的最后时刻。</returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return HebrewCalendar.calendarMaxValue;
      }
    }

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />。</returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunisolarCalendar;
      }
    }

    internal override int ID
    {
      get
      {
        return 8;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.HebrewCalendar" /> 中的纪元的列表。</summary>
    /// <returns>表示 <see cref="T:System.Globalization.HebrewCalendar" /> 类型中的纪元的整数数组。返回值始终是包含一个等于 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 的元素的数组。</returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ HebrewCalendar.HebrewEra };
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前的 <see cref="T:System.Globalization.HebrewCalendar" /> 对象为只读。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在 set 操作中，犹太历年数值小于 5343 而不是 99，或者大于 5999。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value != 99)
          HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, "value");
        this.twoDigitYearMax = value;
      }
    }

    private static void CheckHebrewYearValue(int y, int era, string varName)
    {
      HebrewCalendar.CheckEraRange(era);
      if (y > 5999 || y < 5343)
        throw new ArgumentOutOfRangeException(varName, string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 5343, (object) 5999));
    }

    private void CheckHebrewMonthValue(int year, int month, int era)
    {
      int monthsInYear = this.GetMonthsInYear(year, era);
      if (month < 1 || month > monthsInYear)
        throw new ArgumentOutOfRangeException("month", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) monthsInYear));
    }

    private void CheckHebrewDayValue(int year, int month, int day, int era)
    {
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) daysInMonth));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != HebrewCalendar.HebrewEra)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    private static void CheckTicksRange(long ticks)
    {
      if (ticks < HebrewCalendar.calendarMinValue.Ticks || ticks > HebrewCalendar.calendarMaxValue.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) HebrewCalendar.calendarMinValue, (object) HebrewCalendar.calendarMaxValue));
    }

    internal static int GetResult(HebrewCalendar.__DateBuffer result, int part)
    {
      switch (part)
      {
        case 0:
          return result.year;
        case 2:
          return result.month;
        case 3:
          return result.day;
        default:
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
      }
    }

    internal static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.__DateBuffer lunarDate)
    {
      int num1 = gregorianYear - 1583;
      if (num1 < 0 || num1 > 656)
        throw new ArgumentOutOfRangeException("gregorianYear");
      int index = num1 * 2;
      lunarDate.day = HebrewCalendar.HebrewTable[index];
      int num2 = HebrewCalendar.HebrewTable[index + 1];
      switch (lunarDate.day)
      {
        case 0:
          lunarDate.month = 5;
          lunarDate.day = 1;
          break;
        case 30:
          lunarDate.month = 3;
          break;
        case 31:
          lunarDate.month = 5;
          lunarDate.day = 2;
          break;
        case 32:
          lunarDate.month = 5;
          lunarDate.day = 3;
          break;
        case 33:
          lunarDate.month = 3;
          lunarDate.day = 29;
          break;
        default:
          lunarDate.month = 4;
          break;
      }
      return num2;
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
      HebrewCalendar.CheckTicksRange(ticks);
      DateTime dateTime = new DateTime(ticks);
      int year = dateTime.Year;
      int month = dateTime.Month;
      int day = dateTime.Day;
      HebrewCalendar.__DateBuffer lunarDate = new HebrewCalendar.__DateBuffer();
      lunarDate.year = year + 3760;
      int index1 = HebrewCalendar.GetLunarMonthDay(year, lunarDate);
      HebrewCalendar.__DateBuffer result = new HebrewCalendar.__DateBuffer();
      result.year = lunarDate.year;
      result.month = lunarDate.month;
      result.day = lunarDate.day;
      long absoluteDate = GregorianCalendar.GetAbsoluteDate(year, month, day);
      if (month == 1 && day == 1)
        return HebrewCalendar.GetResult(result, part);
      long num1 = absoluteDate - GregorianCalendar.GetAbsoluteDate(year, 1, 1);
      if (num1 + (long) lunarDate.day <= (long) HebrewCalendar.LunarMonthLen[index1, lunarDate.month])
      {
        result.day += (int) num1;
        return HebrewCalendar.GetResult(result, part);
      }
      ++result.month;
      result.day = 1;
      long num2 = num1 - (long) (HebrewCalendar.LunarMonthLen[index1, lunarDate.month] - lunarDate.day);
      if (num2 > 1L)
      {
        while (num2 > (long) HebrewCalendar.LunarMonthLen[index1, result.month])
        {
          long num3 = num2;
          int[,] numArray = HebrewCalendar.LunarMonthLen;
          int index2 = index1;
          HebrewCalendar.__DateBuffer dateBuffer = result;
          int num4 = dateBuffer.month;
          int num5 = num4 + 1;
          dateBuffer.month = num5;
          int index3 = num4;
          long num6 = (long) numArray[index2, index3];
          num2 = num3 - num6;
          if (result.month > 13 || HebrewCalendar.LunarMonthLen[index1, result.month] == 0)
          {
            ++result.year;
            index1 = HebrewCalendar.HebrewTable[(year + 1 - 1583) * 2 + 1];
            result.month = 1;
          }
        }
        result.day += (int) (num2 - 1L);
      }
      return HebrewCalendar.GetResult(result, part);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定月数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定的月数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加 <paramref name="months" />。</param>
    /// <param name="months">要添加的月数。</param>
    /// <exception cref="T:System.ArgumentException">结果 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于 -120,000 或大于 120,000。</exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      try
      {
        int datePart1 = this.GetDatePart(time.Ticks, 0);
        int datePart2 = this.GetDatePart(time.Ticks, 2);
        int day = this.GetDatePart(time.Ticks, 3);
        int month;
        if (months >= 0)
        {
          month = datePart2 + months;
          int monthsInYear;
          while (month > (monthsInYear = this.GetMonthsInYear(datePart1, 0)))
          {
            ++datePart1;
            month -= monthsInYear;
          }
        }
        else if ((month = datePart2 + months) <= 0)
        {
          months = -months;
          months -= datePart2;
          --datePart1;
          int monthsInYear;
          while (months > (monthsInYear = this.GetMonthsInYear(datePart1, 0)))
          {
            --datePart1;
            months -= monthsInYear;
          }
          month = this.GetMonthsInYear(datePart1, 0) - months;
        }
        int daysInMonth = this.GetDaysInMonth(datePart1, month);
        if (day > daysInMonth)
          day = daysInMonth;
        return new DateTime(this.ToDateTime(datePart1, month, day, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L);
      }
      catch (ArgumentException ex)
      {
        throw new ArgumentOutOfRangeException("months", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_AddValue"), Array.Empty<object>()));
      }
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定年数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定年数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加 <paramref name="years" />。</param>
    /// <param name="years">要添加的年数。</param>
    /// <exception cref="T:System.ArgumentException">结果 <see cref="T:System.DateTime" /> 超出了支持的范围。</exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      int datePart = this.GetDatePart(time.Ticks, 0);
      int month = this.GetDatePart(time.Ticks, 2);
      int day = this.GetDatePart(time.Ticks, 3);
      int num = datePart + years;
      HebrewCalendar.CheckHebrewYearValue(num, 0, "years");
      int monthsInYear = this.GetMonthsInYear(num, 0);
      if (month > monthsInYear)
        month = monthsInYear;
      int daysInMonth = this.GetDaysInMonth(num, month);
      if (day > daysInMonth)
        day = daysInMonth;
      long ticks = this.ToDateTime(num, month, day, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
      DateTime supportedDateTime1 = this.MinSupportedDateTime;
      DateTime supportedDateTime2 = this.MaxSupportedDateTime;
      Calendar.CheckAddResult(ticks, supportedDateTime1, supportedDateTime2);
      return new DateTime(ticks);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</summary>
    /// <returns>从 1 到 30 的整数，表示指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
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

    internal static int GetHebrewYearType(int year, int era)
    {
      HebrewCalendar.CheckHebrewYearValue(year, era, "year");
      return HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1];
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</summary>
    /// <returns>从 1 到 385 的整数，表示指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 早于公历 1583 年 9 月 17 日，或晚于 <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetDayOfYear(DateTime time)
    {
      int year = this.GetYear(time);
      DateTime dateTime = year != 5343 ? this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0) : new DateTime(1582, 9, 27);
      return (int) ((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
    }

    /// <summary>返回指定纪元中指定年份的指定月份的天数。</summary>
    /// <returns>指定纪元中指定年份的指定月份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 Calendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" /> 或 <paramref name="era" /> 超出了当前 <see cref="T:System.Globalization.HebrewCalendar" /> 对象支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      HebrewCalendar.CheckEraRange(era);
      int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
      this.CheckHebrewMonthValue(year, month, era);
      int num = HebrewCalendar.LunarMonthLen[hebrewYearType, month];
      if (num != 0)
        return num;
      throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    /// <summary>返回指定纪元中指定年份的天数。</summary>
    /// <returns>指定纪元中指定年份的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了当前 <see cref="T:System.Globalization.HebrewCalendar" /> 对象支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      HebrewCalendar.CheckEraRange(era);
      int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
      if (hebrewYearType < 4)
        return 352 + hebrewYearType;
      return 382 + (hebrewYearType - 3);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的纪元。</summary>
    /// <returns>表示指定的 <see cref="T:System.DateTime" /> 中纪元的整数。返回值始终为 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetEra(DateTime time)
    {
      return HebrewCalendar.HebrewEra;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的月份。</summary>
    /// <returns>1 到 13 之间的一个整数，它表示指定的 <see cref="T:System.DateTime" /> 中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 小于 <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />。</exception>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>返回指定纪元中指定年份中的月数。</summary>
    /// <returns>指定纪元中指定年份的月数。返回值是 12（在平年中）或 13（在闰年中）。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了当前 <see cref="T:System.Globalization.HebrewCalendar" /> 对象支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      return !this.IsLeapYear(year, era) ? 12 : 13;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 值中的年份。</summary>
    /// <returns>一个整数，表示指定的 <see cref="T:System.DateTime" /> 值中的年份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了当前 <see cref="T:System.Globalization.HebrewCalendar" /> 对象支持的范围。</exception>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>确定指定纪元中的指定日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 30 之间的一个整数，它表示天。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" />、<paramref name="day" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      if (this.IsLeapMonth(year, month, era))
      {
        this.CheckHebrewDayValue(year, month, day, era);
        return true;
      }
      if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
        return true;
      this.CheckHebrewDayValue(year, month, day, era);
      return false;
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>一个正整数，表示指定纪元年份中的闰月。如果 <paramref name="year" /> 和 <paramref name="era" /> 参数指定闰年，则返回值为 7；如果不是闰年，则为 0。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 不是 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。- 或 -<paramref name="year" /> 小于犹太历年份 5343 或大于犹太历年份 5999。</exception>
    public override int GetLeapMonth(int year, int era)
    {
      return this.IsLeapYear(year, era) ? 7 : 0;
    }

    /// <summary>确定指定纪元中指定年份的指定月份是否为闰月。</summary>
    /// <returns>如果指定的月份是闰月，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      int num = this.IsLeapYear(year, era) ? 1 : 0;
      this.CheckHebrewMonthValue(year, month, era);
      return num != 0 && month == 7;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      HebrewCalendar.CheckHebrewYearValue(year, era, "year");
      return (7L * (long) year + 1L) % 19L < 7L;
    }

    private static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
    {
      if (month1 == month2)
        return day1 - day2;
      bool flag = month1 > month2;
      if (flag)
      {
        int num1 = month1;
        int num2 = day1;
        month1 = month2;
        day1 = day2;
        month2 = num1;
        day2 = num2;
      }
      int num3 = HebrewCalendar.LunarMonthLen[lunarYearType, month1] - day1;
      ++month1;
      while (month1 < month2)
        num3 += HebrewCalendar.LunarMonthLen[lunarYearType, month1++];
      int num4 = num3 + day2;
      if (!flag)
        return -num4;
      return num4;
    }

    private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
    {
      int num = hebrewYear - 3760;
      HebrewCalendar.__DateBuffer lunarDate = new HebrewCalendar.__DateBuffer();
      int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, lunarDate);
      if (hebrewMonth == lunarDate.month && hebrewDay == lunarDate.day)
        return new DateTime(num, 1, 1, hour, minute, second, millisecond);
      int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, lunarDate.month, lunarDate.day);
      return new DateTime(new DateTime(num, 1, 1).Ticks + (long) dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
    }

    /// <summary>返回一个 <see cref="T:System.DateTime" />，它设置为指定纪元中的指定日期和时间。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 13 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 30 之间的一个整数，它表示天。</param>
    /// <param name="hour">0 与 23 之间的一个整数，它表示小时。</param>
    /// <param name="minute">0 与 59 之间的一个整数，它表示分钟。</param>
    /// <param name="second">0 与 59 之间的一个整数，它表示秒。</param>
    /// <param name="millisecond">0 与 999 之间的一个整数，它表示毫秒。</param>
    /// <param name="era">表示纪元的整数。指定 <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> 或 HebrewCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" />、<paramref name="day" /> 或 <paramref name="era" /> 超出了当前 <see cref="T:System.Globalization.HebrewCalendar" /> 对象支持的范围。- 或 -<paramref name="hour" /> 小于 0 或大于 23。- 或 -<paramref name="minute" /> 小于 0 或大于 59。- 或 -<paramref name="second" /> 小于 0 或大于 59。- 或 -<paramref name="millisecond" /> 小于 0 或大于 999。</exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      HebrewCalendar.CheckHebrewYearValue(year, era, "year");
      this.CheckHebrewMonthValue(year, month, era);
      this.CheckHebrewDayValue(year, month, day, era);
      DateTime gregorian = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
      HebrewCalendar.CheckTicksRange(gregorian.Ticks);
      return gregorian;
    }

    /// <summary>使用 <see cref="P:System.Globalization.HebrewCalendar.TwoDigitYearMax" /> 属性将指定年份转换为四位数的年份，以确定相应的纪元。</summary>
    /// <returns>如果 <paramref name="year" /> 参数是两位数年份，则返回值是对应的四位数年份。如果 <paramref name="year" /> 参数是四位数年份，则返回值是未更改的 <paramref name="year" /> 参数。</returns>
    /// <param name="year">一个从 0 到 99 的两位数年份，或者从 5343 到 5999 的四位数犹太历年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于 0。- 或 -<paramref name="year" /> 小于 <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year > 5999 || year < 5343)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 5343, (object) 5999));
      return year;
    }

    internal class __DateBuffer
    {
      internal int year;
      internal int month;
      internal int day;
    }
  }
}
