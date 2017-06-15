// Decompiled with JetBrains decompiler
// Type: System.Globalization.KoreanCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>表示朝鲜历。</summary>
  [ComVisible(true)]
  [Serializable]
  public class KoreanCalendar : Calendar
  {
    internal static EraInfo[] koreanEraInfo = new EraInfo[1]{ new EraInfo(1, 1, 1, 1, -2333, 2334, 12332) };
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public const int KoreanEra = 1;
    internal GregorianCalendarHelper helper;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 4362;

    /// <summary>获取 <see cref="T:System.Globalization.KoreanCalendar" /> 类支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.KoreanCalendar" /> 类支持的最早日期和时间，它相当于公历的公元 0001 年的 1 月 1 日开始的那一刻。</returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.KoreanCalendar" /> 类支持的最大日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.KoreanCalendar" /> 类支持的最晚日期和时间，它相当于公历的公元 9999 年 12 月 31 日结束的那一刻。</returns>
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
        return 5;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.KoreanCalendar" /> 中的纪元的列表。</summary>
    /// <returns>表示 <see cref="T:System.Globalization.KoreanCalendar" /> 中的纪元的整数数组。</returns>
    public override int[] Eras
    {
      get
      {
        return this.helper.Eras;
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
          this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 4362);
        return this.twoDigitYearMax;
      }
      set
      {
        this.VerifyWritable();
        if (value < 99 || value > this.helper.MaxYear)
          throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 99, (object) this.helper.MaxYear));
        this.twoDigitYearMax = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Globalization.KoreanCalendar" /> 类的新实例。</summary>
    /// <exception cref="T:System.TypeInitializationException">由于缺少区域性信息，所以无法初始化 <see cref="T:System.Globalization.KoreanCalendar" /> 对象。</exception>
    public KoreanCalendar()
    {
      try
      {
        CultureInfo cultureInfo = new CultureInfo("ko-KR");
      }
      catch (ArgumentException ex)
      {
        throw new TypeInitializationException(this.GetType().FullName, (Exception) ex);
      }
      this.helper = new GregorianCalendarHelper((Calendar) this, KoreanCalendar.koreanEraInfo);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定月数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定的月数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加月数。</param>
    /// <param name="months">要添加的月数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于 -120000。- 或 -<paramref name="months" /> 大于 120000。</exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      return this.helper.AddMonths(time, months);
    }

    /// <summary>返回与指定 <see cref="T:System.DateTime" /> 相距指定年数的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>将指定年数添加到指定的 <see cref="T:System.DateTime" /> 中时得到的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="time">
    /// <see cref="T:System.DateTime" />，将向其添加年数。</param>
    /// <param name="years">要添加的年数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="years" /> 或 <paramref name="time" /> 已超出范围。</exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.helper.AddYears(time, years);
    }

    /// <summary>返回指定纪元中指定年份的指定月份的天数。</summary>
    /// <returns>指定纪元中指定年份的指定月份中的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="month">1 到 12 之间的一个整数，它表示月份。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="month" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      return this.helper.GetDaysInMonth(year, month, era);
    }

    /// <summary>返回指定纪元中指定年份的天数。</summary>
    /// <returns>指定纪元中指定年份的天数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      return this.helper.GetDaysInYear(year, era);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</summary>
    /// <returns>从 1 到 31 的整数，表示指定 <see cref="T:System.DateTime" /> 中的日期是该月的几号。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.helper.GetDayOfMonth(time);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是星期几。</summary>
    /// <returns>一个 <see cref="T:System.DayOfWeek" /> 值，它表示指定的 <see cref="T:System.DateTime" /> 中的日期是星期几。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return this.helper.GetDayOfWeek(time);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</summary>
    /// <returns>从 1 到 366 的整数，表示指定 <see cref="T:System.DateTime" /> 中的日期是该年中的第几天。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetDayOfYear(DateTime time)
    {
      return this.helper.GetDayOfYear(time);
    }

    /// <summary>返回指定纪元中指定年份中的月数。</summary>
    /// <returns>指定纪元中指定年份的月数。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      return this.helper.GetMonthsInYear(year, era);
    }

    /// <summary>返回年中包括指定 <see cref="T:System.DateTime" /> 中的日期的星期。</summary>
    /// <returns>一个从 1 开始的整数，表示一年中包括 <paramref name="time" /> 参数中的日期的那个星期。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    /// <param name="rule">
    /// <see cref="T:System.Globalization.CalendarWeekRule" /> 值之一，用于定义一个日历周。</param>
    /// <param name="firstDayOfWeek">
    /// <see cref="T:System.DayOfWeek" /> 值之一，表示一个星期的第一天。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 或 <paramref name="firstDayOfWeek" /> 超出了日历支持的范围。- 或 -<paramref name="rule" /> 不是有效的 <see cref="T:System.Globalization.CalendarWeekRule" /> 值。</exception>
    [ComVisible(false)]
    public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
      return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的纪元。</summary>
    /// <returns>表示指定的 <see cref="T:System.DateTime" /> 中纪元的整数。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetEra(DateTime time)
    {
      return this.helper.GetEra(time);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的月份。</summary>
    /// <returns>1 到 12 之间的一个整数，用于表示指定的 <see cref="T:System.DateTime" /> 中的月份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetMonth(DateTime time)
    {
      return this.helper.GetMonth(time);
    }

    /// <summary>返回指定 <see cref="T:System.DateTime" /> 中的年份。</summary>
    /// <returns>一个整数，它表示指定的 <see cref="T:System.DateTime" /> 中的年份。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetYear(DateTime time)
    {
      return this.helper.GetYear(time);
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
      return this.helper.IsLeapDay(year, month, day, era);
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">表示年份的整数。</param>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。- 或 -<paramref name="era" /> 超出了日历支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      return this.helper.IsLeapYear(year, era);
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>返回值始终为 0，因为 <see cref="T:System.Globalization.KoreanCalendar" /> 类不支持闰月这一概念。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。</param>
    [ComVisible(false)]
    public override int GetLeapMonth(int year, int era)
    {
      return this.helper.GetLeapMonth(year, era);
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
      return this.helper.IsLeapMonth(year, month, era);
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
      return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
    }

    /// <summary>使用 <see cref="P:System.Globalization.KoreanCalendar.TwoDigitYearMax" /> 属性将指定的年份转换为四位数年份，以确定相应的纪元。</summary>
    /// <returns>包含 <paramref name="year" /> 的四位数表示形式的整数。</returns>
    /// <param name="year">一个两位数或四位数的整数，表示要转换的年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了日历支持的范围。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
    }
  }
}
