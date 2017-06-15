// Decompiled with JetBrains decompiler
// Type: System.Globalization.ChineseLunisolarCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>将时间分成多个部分来表示，如分成年、月和日。年按农历计算，而日和月按阴阳历计算。</summary>
  [Serializable]
  public class ChineseLunisolarCalendar : EastAsianLunisolarCalendar
  {
    internal static DateTime minDate = new DateTime(1901, 2, 19);
    internal static DateTime maxDate = new DateTime(new DateTime(2101, 1, 28, 23, 59, 59, 999).Ticks + 9999L);
    private static readonly int[,] yinfo = new int[200, 4]{ { 0, 2, 19, 19168 }, { 0, 2, 8, 42352 }, { 5, 1, 29, 21096 }, { 0, 2, 16, 53856 }, { 0, 2, 4, 55632 }, { 4, 1, 25, 27304 }, { 0, 2, 13, 22176 }, { 0, 2, 2, 39632 }, { 2, 1, 22, 19176 }, { 0, 2, 10, 19168 }, { 6, 1, 30, 42200 }, { 0, 2, 18, 42192 }, { 0, 2, 6, 53840 }, { 5, 1, 26, 54568 }, { 0, 2, 14, 46400 }, { 0, 2, 3, 54944 }, { 2, 1, 23, 38608 }, { 0, 2, 11, 38320 }, { 7, 2, 1, 18872 }, { 0, 2, 20, 18800 }, { 0, 2, 8, 42160 }, { 5, 1, 28, 45656 }, { 0, 2, 16, 27216 }, { 0, 2, 5, 27968 }, { 4, 1, 24, 44456 }, { 0, 2, 13, 11104 }, { 0, 2, 2, 38256 }, { 2, 1, 23, 18808 }, { 0, 2, 10, 18800 }, { 6, 1, 30, 25776 }, { 0, 2, 17, 54432 }, { 0, 2, 6, 59984 }, { 5, 1, 26, 27976 }, { 0, 2, 14, 23248 }, { 0, 2, 4, 11104 }, { 3, 1, 24, 37744 }, { 0, 2, 11, 37600 }, { 7, 1, 31, 51560 }, { 0, 2, 19, 51536 }, { 0, 2, 8, 54432 }, { 6, 1, 27, 55888 }, { 0, 2, 15, 46416 }, { 0, 2, 5, 22176 }, { 4, 1, 25, 43736 }, { 0, 2, 13, 9680 }, { 0, 2, 2, 37584 }, { 2, 1, 22, 51544 }, { 0, 2, 10, 43344 }, { 7, 1, 29, 46248 }, { 0, 2, 17, 27808 }, { 0, 2, 6, 46416 }, { 5, 1, 27, 21928 }, { 0, 2, 14, 19872 }, { 0, 2, 3, 42416 }, { 3, 1, 24, 21176 }, { 0, 2, 12, 21168 }, { 8, 1, 31, 43344 }, { 0, 2, 18, 59728 }, { 0, 2, 8, 27296 }, { 6, 1, 28, 44368 }, { 0, 2, 15, 43856 }, { 0, 2, 5, 19296 }, { 4, 1, 25, 42352 }, { 0, 2, 13, 42352 }, { 0, 2, 2, 21088 }, { 3, 1, 21, 59696 }, { 0, 2, 9, 55632 }, { 7, 1, 30, 23208 }, { 0, 2, 17, 22176 }, { 0, 2, 6, 38608 }, { 5, 1, 27, 19176 }, { 0, 2, 15, 19152 }, { 0, 2, 3, 42192 }, { 4, 1, 23, 53864 }, { 0, 2, 11, 53840 }, { 8, 1, 31, 54568 }, { 0, 2, 18, 46400 }, { 0, 2, 7, 46752 }, { 6, 1, 28, 38608 }, { 0, 2, 16, 38320 }, { 0, 2, 5, 18864 }, { 4, 1, 25, 42168 }, { 0, 2, 13, 42160 }, { 10, 2, 2, 45656 }, { 0, 2, 20, 27216 }, { 0, 2, 9, 27968 }, { 6, 1, 29, 44448 }, { 0, 2, 17, 43872 }, { 0, 2, 6, 38256 }, { 5, 1, 27, 18808 }, { 0, 2, 15, 18800 }, { 0, 2, 4, 25776 }, { 3, 1, 23, 27216 }, { 0, 2, 10, 59984 }, { 8, 1, 31, 27432 }, { 0, 2, 19, 23232 }, { 0, 2, 7, 43872 }, { 5, 1, 28, 37736 }, { 0, 2, 16, 37600 }, { 0, 2, 5, 51552 }, { 4, 1, 24, 54440 }, { 0, 2, 12, 54432 }, { 0, 2, 1, 55888 }, { 2, 1, 22, 23208 }, { 0, 2, 9, 22176 }, { 7, 1, 29, 43736 }, { 0, 2, 18, 9680 }, { 0, 2, 7, 37584 }, { 5, 1, 26, 51544 }, { 0, 2, 14, 43344 }, { 0, 2, 3, 46240 }, { 4, 1, 23, 46416 }, { 0, 2, 10, 44368 }, { 9, 1, 31, 21928 }, { 0, 2, 19, 19360 }, { 0, 2, 8, 42416 }, { 6, 1, 28, 21176 }, { 0, 2, 16, 21168 }, { 0, 2, 5, 43312 }, { 4, 1, 25, 29864 }, { 0, 2, 12, 27296 }, { 0, 2, 1, 44368 }, { 2, 1, 22, 19880 }, { 0, 2, 10, 19296 }, { 6, 1, 29, 42352 }, { 0, 2, 17, 42208 }, { 0, 2, 6, 53856 }, { 5, 1, 26, 59696 }, { 0, 2, 13, 54576 }, { 0, 2, 3, 23200 }, { 3, 1, 23, 27472 }, { 0, 2, 11, 38608 }, { 11, 1, 31, 19176 }, { 0, 2, 19, 19152 }, { 0, 2, 8, 42192 }, { 6, 1, 28, 53848 }, { 0, 2, 15, 53840 }, { 0, 2, 4, 54560 }, { 5, 1, 24, 55968 }, { 0, 2, 12, 46496 }, { 0, 2, 1, 22224 }, { 2, 1, 22, 19160 }, { 0, 2, 10, 18864 }, { 7, 1, 30, 42168 }, { 0, 2, 17, 42160 }, { 0, 2, 6, 43600 }, { 5, 1, 26, 46376 }, { 0, 2, 14, 27936 }, { 0, 2, 2, 44448 }, { 3, 1, 23, 21936 }, { 0, 2, 11, 37744 }, { 8, 2, 1, 18808 }, { 0, 2, 19, 18800 }, { 0, 2, 8, 25776 }, { 6, 1, 28, 27216 }, { 0, 2, 15, 59984 }, { 0, 2, 4, 27424 }, { 4, 1, 24, 43872 }, { 0, 2, 12, 43744 }, { 0, 2, 2, 37600 }, { 3, 1, 21, 51568 }, { 0, 2, 9, 51552 }, { 7, 1, 29, 54440 }, { 0, 2, 17, 54432 }, { 0, 2, 5, 55888 }, { 5, 1, 26, 23208 }, { 0, 2, 14, 22176 }, { 0, 2, 3, 42704 }, { 4, 1, 23, 21224 }, { 0, 2, 11, 21200 }, { 8, 1, 31, 43352 }, { 0, 2, 19, 43344 }, { 0, 2, 7, 46240 }, { 6, 1, 27, 46416 }, { 0, 2, 15, 44368 }, { 0, 2, 5, 21920 }, { 4, 1, 24, 42448 }, { 0, 2, 12, 42416 }, { 0, 2, 2, 21168 }, { 3, 1, 22, 43320 }, { 0, 2, 9, 26928 }, { 7, 1, 29, 29336 }, { 0, 2, 17, 27296 }, { 0, 2, 6, 44368 }, { 5, 1, 26, 19880 }, { 0, 2, 14, 19296 }, { 0, 2, 3, 42352 }, { 4, 1, 24, 21104 }, { 0, 2, 10, 53856 }, { 8, 1, 30, 59696 }, { 0, 2, 18, 54560 }, { 0, 2, 7, 55968 }, { 6, 1, 27, 27472 }, { 0, 2, 15, 22224 }, { 0, 2, 5, 19168 }, { 4, 1, 25, 42216 }, { 0, 2, 12, 42192 }, { 0, 2, 1, 53584 }, { 2, 1, 21, 55592 }, { 0, 2, 9, 54560 } };
    /// <summary>指定对应于当前 <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> 对象的纪元。</summary>
    public const int ChineseEra = 1;
    internal const int MIN_LUNISOLAR_YEAR = 1901;
    internal const int MAX_LUNISOLAR_YEAR = 2100;
    internal const int MIN_GREGORIAN_YEAR = 1901;
    internal const int MIN_GREGORIAN_MONTH = 2;
    internal const int MIN_GREGORIAN_DAY = 19;
    internal const int MAX_GREGORIAN_YEAR = 2101;
    internal const int MAX_GREGORIAN_MONTH = 1;
    internal const int MAX_GREGORIAN_DAY = 28;

    /// <summary>获取 <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> 类支持的最小日期和时间。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 类型，表示公历 1901 年 2 月 19 日，等同于构造函数 DateTime(1901, 2, 19)。</returns>
    [ComVisible(false)]
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return ChineseLunisolarCalendar.minDate;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> 类支持的最大日期和时间。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 类型，表示公历 2101 年 1 月 28 日结束的那一刻，约等于构造函数 DateTime(2101, 1, 28, 23, 59, 59, 999)。</returns>
    [ComVisible(false)]
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return ChineseLunisolarCalendar.maxDate;
      }
    }

    /// <summary>获取 <see cref="P:System.Globalization.ChineseLunisolarCalendar.MinSupportedDateTime" />属性指定的年份之前的年份天数。</summary>
    /// <returns>由 <see cref="P:System.Globalization.ChineseLunisolarCalendar.MinSupportedDateTime" /> 指定的在年之前的一年的天数。</returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 384;
      }
    }

    internal override int MinCalendarYear
    {
      get
      {
        return 1901;
      }
    }

    internal override int MaxCalendarYear
    {
      get
      {
        return 2100;
      }
    }

    internal override DateTime MinDate
    {
      get
      {
        return ChineseLunisolarCalendar.minDate;
      }
    }

    internal override DateTime MaxDate
    {
      get
      {
        return ChineseLunisolarCalendar.maxDate;
      }
    }

    internal override EraInfo[] CalEraInfo
    {
      get
      {
        return (EraInfo[]) null;
      }
    }

    internal override int ID
    {
      get
      {
        return 15;
      }
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 1;
      }
    }

    /// <summary>获取与当前 <see cref="T:System.Globalization.ChineseLunisolarCalendar" /> 对象支持的日期和时间范围对应的纪元。</summary>
    /// <returns>32 位有符号整数数组，用于指定相关的纪元。<see cref="T:System.Globalization.ChineseLunisolarCalendar" /> 对象的返回值始终是包含一个等于 <see cref="F:System.Globalization.ChineseLunisolarCalendar.ChineseEra" /> 值的元素的数组。</returns>
    [ComVisible(false)]
    public override int[] Eras
    {
      get
      {
        return new int[1]{ 1 };
      }
    }

    internal override int GetYearInfo(int LunarYear, int Index)
    {
      if (LunarYear < 1901 || LunarYear > 2100)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1901, (object) 2100));
      return ChineseLunisolarCalendar.yinfo[LunarYear - 1901, Index];
    }

    internal override int GetYear(int year, DateTime time)
    {
      return year;
    }

    internal override int GetGregorianYear(int year, int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
      if (year < 1901 || year > 2100)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1901, (object) 2100));
      return year;
    }

    /// <summary>检索对应于指定 <see cref="T:System.DateTime" /> 类型的纪元。</summary>
    /// <returns>一个整数，表示 <paramref name="time" /> 参数中的纪元。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" /> 类型。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 小于 <see cref="P:System.Globalization.ChineseLunisolarCalendar.MinSupportedDateTime" /> 或大于 <see cref="P:System.Globalization.ChineseLunisolarCalendar.MaxSupportedDateTime" />。</exception>
    [ComVisible(false)]
    public override int GetEra(DateTime time)
    {
      this.CheckTicksRange(time.Ticks);
      return 1;
    }
  }
}
