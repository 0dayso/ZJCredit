// Decompiled with JetBrains decompiler
// Type: System.Globalization.GregorianCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>表示公历。</summary>
  [ComVisible(true)]
  [Serializable]
  public class GregorianCalendar : Calendar
  {
    internal static readonly int[] DaysToMonth365 = new int[13]{ 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
    internal static readonly int[] DaysToMonth366 = new int[13]{ 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public const int ADEra = 1;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    internal const int MaxYear = 9999;
    internal GregorianCalendarTypes m_type;
    private static volatile Calendar s_defaultInstance;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 2029;

    /// <summary>获取 <see cref="T:System.Globalization.GregorianCalendar" /> 类型支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.GregorianCalendar" /> 类型支持的最早日期和时间，此日期时间为公历的公元 0001 年的 1 月 1 日开始的那一刻，等效于 <see cref="F:System.DateTime.MinValue" /></returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.GregorianCalendar" /> 类型支持的最晚日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.GregorianCalendar" /> 类型支持的最晚日期和时间，此日期时间为公历的公元 9999 年 12 月 31 日结束的那一刻，等效于 <see cref="F:System.DateTime.MaxValue" /></returns>
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

    /// <summary>获取或设置 <see cref="T:System.Globalization.GregorianCalendarTypes" /> 值，该值指示当前 <see cref="T:System.Globalization.GregorianCalendar" /> 的语言版本。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.GregorianCalendarTypes" /> 值，该值指示当前 <see cref="T:System.Globalization.GregorianCalendar" /> 的语言版本。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中指定的值不是 <see cref="T:System.Globalization.GregorianCalendarTypes" /> 枚举的成员。</exception>
    /// <exception cref="T:System.InvalidOperationException">在设置操作中，当前实例是只读的。</exception>
    public virtual GregorianCalendarTypes CalendarType
    {
      get
      {
        return this.m_type;
      }
      set
      {
        this.VerifyWritable();
        switch (value)
        {
          case GregorianCalendarTypes.Localized:
          case GregorianCalendarTypes.USEnglish:
          case GregorianCalendarTypes.MiddleEastFrench:
          case GregorianCalendarTypes.Arabic:
          case GregorianCalendarTypes.TransliteratedEnglish:
          case GregorianCalendarTypes.TransliteratedFrench:
            this.m_type = value;
            break;
          default:
            throw new ArgumentOutOfRangeException("m_type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
    }

    internal override int ID
    {
      get
      {
        return (int) this.m_type;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.GregorianCalendar" /> 中的纪元的列表。</summary>
    /// <returns>表示 <see cref="T:System.Globalization.GregorianCalendar" /> 中的纪元的整数数组。</returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ 1 };
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">为设置操作指定的值小于 99。- 或 -为 Set 操作指定的值大于 MaxSupportedDateTime.Year。</exception>
    /// <exception cref="T:System.InvalidOperationException">在设置操作中，当前实例是只读的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override int TwoDigitYearMax
    {
      get
      {
        if (this.twoDigitYearMax == -1)
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2029);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > 9999)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) 9999));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>使用默认的 <see cref="T:System.Globalization.GregorianCalendarTypes" /> 值初始化 <see cref="T:System.Globalization.GregorianCalendar" /> 类的新实例。</summary>
    public GregorianCalendar()
      : this(GregorianCalendarTypes.Localized)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.GregorianCalendarTypes" /> 值初始化 <see cref="T:System.Globalization.GregorianCalendar" /> 类的新实例。</summary>
    /// <param name="type">
    /// <see cref="T:System.Globalization.GregorianCalendarTypes" /> 值，指示要创建的日历的语言版本。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="type" /> 不是 <see cref="T:System.Globalization.GregorianCalendarTypes" /> 枚举的成员。</exception>
    public GregorianCalendar(GregorianCalendarTypes type)
    {
      if (type < GregorianCalendarTypes.Localized || type > GregorianCalendarTypes.TransliteratedFrench)
        throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) GregorianCalendarTypes.Localized, (object) GregorianCalendarTypes.TransliteratedFrench));
      this.m_type = type;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_type < GregorianCalendarTypes.Localized || this.m_type > GregorianCalendarTypes.TransliteratedFrench)
        throw new SerializationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_MemberOutOfRange"), (object) "type", (object) "GregorianCalendar"));
    }

    internal static Calendar GetDefaultInstance()
    {
      if (GregorianCalendar.s_defaultInstance == null)
        GregorianCalendar.s_defaultInstance = (Calendar) new GregorianCalendar();
      return GregorianCalendar.s_defaultInstance;
    }

    internal virtual int GetDatePart(long ticks, int part)
    {
      int num1 = (int) (ticks / 864000000000L);
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
      int[] numArray = (num8 != 3 ? 0 : (num6 != 24 ? 1 : (num4 == 3 ? 1 : 0))) != 0 ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365;
      int index = num9 >> 6;
      while (num9 >= numArray[index])
        ++index;
      if (part == 2)
        return index;
      return num9 - numArray[index - 1] + 1;
    }

    internal static long GetAbsoluteDate(int year, int month, int day)
    {
      if (year >= 1 && year <= 9999 && (month >= 1 && month <= 12))
      {
        int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendar.DaysToMonth365 : GregorianCalendar.DaysToMonth366;
        if (day >= 1 && day <= numArray[month] - numArray[month - 1])
        {
          int num = year - 1;
          return (long) (num * 365 + num / 4 - num / 100 + num / 400 + numArray[month - 1] + day - 1);
        }
      }
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    internal virtual long DateToTicks(int year, int month, int day)
    {
      return GregorianCalendar.GetAbsoluteDate(year, month, day) * 864000000000L;
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
      int datePart1 = this.GetDatePart(time.Ticks, 0);
      int datePart2 = this.GetDatePart(time.Ticks, 2);
      int day = this.GetDatePart(time.Ticks, 3);
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
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendar.DaysToMonth365 : GregorianCalendar.DaysToMonth366;
      int num2 = numArray[month] - numArray[month - 1];
      if (day > num2)
        day = num2;
      long ticks = this.DateToTicks(year, month, day) + time.Ticks % 864000000000L;
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
      return this.GetDatePart(time.Ticks, 3);
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
      return this.GetDatePart(time.Ticks, 1);
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
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 9999));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
      int[] numArray = year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? GregorianCalendar.DaysToMonth365 : GregorianCalendar.DaysToMonth366;
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
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      return year % 4 != 0 || year % 100 == 0 && year % 400 != 0 ? 365 : 366;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的纪元。</summary>
    /// <returns>表示 <paramref name="time" /> 中的纪元的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetEra(DateTime time)
    {
      return 1;
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的月份。</summary>
    /// <returns>1 到 12 之间的一个整数，它表示 <paramref name="time" /> 中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 2);
    }

    /// <summary>返回指定纪元中指定年份中的月数。</summary>
    /// <returns>指定纪元中指定年份的月数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year >= 1 && year <= 9999)
        return 12;
      throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的年份。</summary>
    /// <returns>表示 <paramref name="time" /> 中的年份的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time.Ticks, 0);
    }

    /// <summary>确定指定纪元中的指定日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="day">1 到 31 之间的一个整数，它表示天。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 12));
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 9999));
      if (day < 1 || day > this.GetDaysInMonth(year, month))
        throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) this.GetDaysInMonth(year, month)));
      return this.IsLeapYear(year) && month == 2 && day == 29;
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>总为 0，因为公历无法识别闰月。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> 或 GregorianCalendar.Eras[Calendar.CurrentEra]。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于公历 1 年或大于公历 9999 年。- 或 -<paramref name="era" /> 不是 <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> 或 GregorianCalendar.Eras[Calendar.CurrentEra]。</exception>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      return 0;
    }

    /// <summary>确定指定纪元中指定年份的指定月份是否为闰月。</summary>
    /// <returns>除非被派生类重写，否则此方法始终返回 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 12));
      return false;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1 || year > 9999)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      if (year % 4 != 0)
        return false;
      if (year % 100 == 0)
        return year % 400 == 0;
      return true;
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
    /// <paramref name="era" /> 超出了日历支持的范围。- 或 -<paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="day" /> 超出了日历支持的范围。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于零或大于 59。- 或 -<paramref name="second" /> 小于零或大于 59。- 或 -<paramref name="millisecond" /> 小于零或大于 999。</exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      if (era == 0 || era == 1)
        return new DateTime(year, month, day, hour, minute, second, millisecond);
      throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal override bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
    {
      if (era == 0 || era == 1)
        return DateTime.TryCreate(year, month, day, hour, minute, second, millisecond, out result);
      result = DateTime.MinValue;
      return false;
    }

    /// <summary>使用 <see cref="P:System.Globalization.GregorianCalendar.TwoDigitYearMax" /> 属性将指定的年份转换为四位数年份，以确定相应的纪元。</summary>
    /// <returns>包含 <paramref name="year" /> 的四位数表示形式的整数。</returns>
    /// <param name="year">一个两位数或四位数的整数，表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year > 9999)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1, (object) 9999));
      return base.ToFourDigitYear(year);
    }
  }
}
