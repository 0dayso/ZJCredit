// Decompiled with JetBrains decompiler
// Type: System.Globalization.JapaneseLunisolarCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>将时间分成多个部分来表示，如分成年、月和日。年按日本历计算，而日和月则按阴阳历计算。</summary>
  [Serializable]
  public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
  {
    internal static DateTime minDate = new DateTime(1960, 1, 28);
    internal static DateTime maxDate = new DateTime(new DateTime(2050, 1, 22, 23, 59, 59, 999).Ticks + 9999L);
    private static readonly int[,] yinfo = new int[90, 4]{ { 6, 1, 28, 44368 }, { 0, 2, 15, 43856 }, { 0, 2, 5, 19808 }, { 4, 1, 25, 42352 }, { 0, 2, 13, 42352 }, { 0, 2, 2, 21104 }, { 3, 1, 22, 26928 }, { 0, 2, 9, 55632 }, { 7, 1, 30, 27304 }, { 0, 2, 17, 22176 }, { 0, 2, 6, 39632 }, { 5, 1, 27, 19176 }, { 0, 2, 15, 19168 }, { 0, 2, 3, 42208 }, { 4, 1, 23, 53864 }, { 0, 2, 11, 53840 }, { 8, 1, 31, 54600 }, { 0, 2, 18, 46400 }, { 0, 2, 7, 54944 }, { 6, 1, 28, 38608 }, { 0, 2, 16, 38320 }, { 0, 2, 5, 18864 }, { 4, 1, 25, 42200 }, { 0, 2, 13, 42160 }, { 10, 2, 2, 45656 }, { 0, 2, 20, 27216 }, { 0, 2, 9, 27968 }, { 6, 1, 29, 46504 }, { 0, 2, 18, 11104 }, { 0, 2, 6, 38320 }, { 5, 1, 27, 18872 }, { 0, 2, 15, 18800 }, { 0, 2, 4, 25776 }, { 3, 1, 23, 27216 }, { 0, 2, 10, 59984 }, { 8, 1, 31, 27976 }, { 0, 2, 19, 23248 }, { 0, 2, 8, 11104 }, { 5, 1, 28, 37744 }, { 0, 2, 16, 37600 }, { 0, 2, 5, 51552 }, { 4, 1, 24, 58536 }, { 0, 2, 12, 54432 }, { 0, 2, 1, 55888 }, { 2, 1, 22, 23208 }, { 0, 2, 9, 22208 }, { 7, 1, 29, 43736 }, { 0, 2, 18, 9680 }, { 0, 2, 7, 37584 }, { 5, 1, 26, 51544 }, { 0, 2, 14, 43344 }, { 0, 2, 3, 46240 }, { 3, 1, 23, 47696 }, { 0, 2, 10, 46416 }, { 9, 1, 31, 21928 }, { 0, 2, 19, 19360 }, { 0, 2, 8, 42416 }, { 5, 1, 28, 21176 }, { 0, 2, 16, 21168 }, { 0, 2, 5, 43344 }, { 4, 1, 25, 46248 }, { 0, 2, 12, 27296 }, { 0, 2, 1, 44368 }, { 2, 1, 22, 21928 }, { 0, 2, 10, 19296 }, { 6, 1, 29, 42352 }, { 0, 2, 17, 42352 }, { 0, 2, 7, 21104 }, { 5, 1, 27, 26928 }, { 0, 2, 13, 55600 }, { 0, 2, 3, 23200 }, { 3, 1, 23, 43856 }, { 0, 2, 11, 38608 }, { 11, 1, 31, 19176 }, { 0, 2, 19, 19168 }, { 0, 2, 8, 42192 }, { 6, 1, 28, 53864 }, { 0, 2, 15, 53840 }, { 0, 2, 4, 54560 }, { 5, 1, 24, 55968 }, { 0, 2, 12, 46752 }, { 0, 2, 1, 38608 }, { 2, 1, 22, 19160 }, { 0, 2, 10, 18864 }, { 7, 1, 30, 42168 }, { 0, 2, 17, 42160 }, { 0, 2, 6, 45648 }, { 5, 1, 26, 46376 }, { 0, 2, 14, 27968 }, { 0, 2, 2, 44448 } };
    /// <summary>指定当前纪元。</summary>
    public const int JapaneseEra = 1;
    internal GregorianCalendarHelper helper;
    internal const int MIN_LUNISOLAR_YEAR = 1960;
    internal const int MAX_LUNISOLAR_YEAR = 2049;
    internal const int MIN_GREGORIAN_YEAR = 1960;
    internal const int MIN_GREGORIAN_MONTH = 1;
    internal const int MIN_GREGORIAN_DAY = 28;
    internal const int MAX_GREGORIAN_YEAR = 2050;
    internal const int MAX_GREGORIAN_MONTH = 1;
    internal const int MAX_GREGORIAN_DAY = 22;

    /// <summary>获取 <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> 类支持的最小日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> 类支持的最早日期和时间，它相当于公历的公元 1960 年的 1 月 28 日开始的那一刻。</returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return JapaneseLunisolarCalendar.minDate;
      }
    }

    /// <summary>获取 <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> 类支持的最大日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> 类支持的最晚日期和时间，它相当于公历的公元 2050 年 1 月 22 日结束的那一刻。</returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return JapaneseLunisolarCalendar.maxDate;
      }
    }

    /// <summary>获取 <see cref="P:System.Globalization.JapaneseLunisolarCalendar.MinSupportedDateTime" /> 属性指定的年份之前的年份的天数。</summary>
    /// <returns>由 <see cref="P:System.Globalization.JapaneseLunisolarCalendar.MinSupportedDateTime" /> 指定的在年之前的一年的天数。</returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 354;
      }
    }

    internal override int MinCalendarYear
    {
      get
      {
        return 1960;
      }
    }

    internal override int MaxCalendarYear
    {
      get
      {
        return 2049;
      }
    }

    internal override DateTime MinDate
    {
      get
      {
        return JapaneseLunisolarCalendar.minDate;
      }
    }

    internal override DateTime MaxDate
    {
      get
      {
        return JapaneseLunisolarCalendar.maxDate;
      }
    }

    internal override EraInfo[] CalEraInfo
    {
      get
      {
        return JapaneseCalendar.GetEraInfo();
      }
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 3;
      }
    }

    internal override int ID
    {
      get
      {
        return 14;
      }
    }

    /// <summary>获取与 <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> 对象相关的纪元。</summary>
    /// <returns>32 位有符号整数数组，用于指定相关的纪元。</returns>
    public override int[] Eras
    {
      get
      {
        return this.helper.Eras;
      }
    }

    /// <summary>初始化 <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> 类的新实例。</summary>
    public JapaneseLunisolarCalendar()
    {
      this.helper = new GregorianCalendarHelper((Calendar) this, JapaneseLunisolarCalendar.TrimEras(JapaneseCalendar.GetEraInfo()));
    }

    internal override int GetYearInfo(int LunarYear, int Index)
    {
      if (LunarYear < 1960 || LunarYear > 2049)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1960, (object) 2049));
      return JapaneseLunisolarCalendar.yinfo[LunarYear - 1960, Index];
    }

    internal override int GetYear(int year, DateTime time)
    {
      return this.helper.GetYear(year, time);
    }

    internal override int GetGregorianYear(int year, int era)
    {
      return this.helper.GetGregorianYear(year, era);
    }

    private static EraInfo[] TrimEras(EraInfo[] baseEras)
    {
      EraInfo[] array = new EraInfo[baseEras.Length];
      int newSize = 0;
      for (int index = 0; index < baseEras.Length; ++index)
      {
        if (baseEras[index].yearOffset + baseEras[index].minEraYear < 2049)
        {
          if (baseEras[index].yearOffset + baseEras[index].maxEraYear >= 1960)
          {
            array[newSize] = baseEras[index];
            ++newSize;
          }
          else
            break;
        }
      }
      if (newSize == 0)
        return baseEras;
      Array.Resize<EraInfo>(ref array, newSize);
      return array;
    }

    /// <summary>检索对应于指定 <see cref="T:System.DateTime" /> 的纪元。</summary>
    /// <returns>一个整数，表示 <paramref name="time" /> 参数中指定的纪元。</returns>
    /// <param name="time">要读取的 <see cref="T:System.DateTime" />。</param>
    public override int GetEra(DateTime time)
    {
      return this.helper.GetEra(time);
    }
  }
}
