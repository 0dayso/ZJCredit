// Decompiled with JetBrains decompiler
// Type: System.Globalization.UmAlQuraCalendar
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>表示沙特阿拉伯回历 (Um Al Qura)。</summary>
  [Serializable]
  public class UmAlQuraCalendar : Calendar
  {
    private static readonly UmAlQuraCalendar.DateMapping[] HijriYearInfo = UmAlQuraCalendar.InitDateMapping();
    internal static DateTime minDate = new DateTime(1900, 4, 30);
    internal static DateTime maxDate = new DateTime(new DateTime(2077, 11, 16, 23, 59, 59, 999).Ticks + 9999L);
    internal const int MinCalendarYear = 1318;
    internal const int MaxCalendarYear = 1500;
    /// <summary>表示当前纪元。此字段为常数。</summary>
    public const int UmAlQuraEra = 1;
    internal const int DateCycle = 30;
    internal const int DatePartYear = 0;
    internal const int DatePartDayOfYear = 1;
    internal const int DatePartMonth = 2;
    internal const int DatePartDay = 3;
    private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;

    /// <summary>获取此日历支持的最早日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的最早日期和时间，它相当于公历的公元 1900 年 4 月 30 日开始的那一刻。</returns>
    public override DateTime MinSupportedDateTime
    {
      get
      {
        return UmAlQuraCalendar.minDate;
      }
    }

    /// <summary>获取此日历支持的最晚日期和时间。</summary>
    /// <returns>
    /// <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的最晚日期和时间，此日期时间为公历公元 2077 年 11 月 16 日的结束时刻。</returns>
    public override DateTime MaxSupportedDateTime
    {
      get
      {
        return UmAlQuraCalendar.maxDate;
      }
    }

    /// <summary>获取一个值，该值指示当前日历是阳历、阴历还是二者的组合。</summary>
    /// <returns>始终返回 <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" />。</returns>
    public override CalendarAlgorithmType AlgorithmType
    {
      get
      {
        return CalendarAlgorithmType.LunarCalendar;
      }
    }

    internal override int BaseCalendarID
    {
      get
      {
        return 6;
      }
    }

    internal override int ID
    {
      get
      {
        return 23;
      }
    }

    /// <summary>获取指定 <see cref="P:System.Globalization.UmAlQuraCalendar.MinSupportedDateTime" /> 属性的指定年份中的天数。</summary>
    /// <returns>由 <see cref="P:System.Globalization.UmAlQuraCalendar.MinSupportedDateTime" /> 指定的在年之前的一年的天数。</returns>
    protected override int DaysInYearBeforeMinSupportedYear
    {
      get
      {
        return 355;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 支持的纪元的列表。</summary>
    /// <returns>一个数组，它由值为 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" /> 的单个元素组成。</returns>
    public override int[] Eras
    {
      get
      {
        return new int[1]{ 1 };
      }
    }

    /// <summary>获取或设置可以用两位数年份表示的 100 年范围内的最后一年。</summary>
    /// <returns>可以用两位数年份表示的 100 年范围内的最后一年。</returns>
    /// <exception cref="T:System.InvalidOperationException">此日历为只读。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置操作中，Um Al Qura 年份值小于 1318 而不是 99，或者大于 1450。</exception>
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
        if (value != 99 && (value < 1318 || value > 1500))
          throw new ArgumentOutOfRangeException("value", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1318, (object) 1500));
        this.VerifyWritable();
        this.twoDigitYearMax = value;
      }
    }

    private static UmAlQuraCalendar.DateMapping[] InitDateMapping()
    {
      short[] numArray = new short[736]{ (short) 746, (short) 1900, (short) 4, (short) 30, (short) 1769, (short) 1901, (short) 4, (short) 19, (short) 3794, (short) 1902, (short) 4, (short) 9, (short) 3748, (short) 1903, (short) 3, (short) 30, (short) 3402, (short) 1904, (short) 3, (short) 18, (short) 2710, (short) 1905, (short) 3, (short) 7, (short) 1334, (short) 1906, (short) 2, (short) 24, (short) 2741, (short) 1907, (short) 2, (short) 13, (short) 3498, (short) 1908, (short) 2, (short) 3, (short) 2980, (short) 1909, (short) 1, (short) 23, (short) 2889, (short) 1910, (short) 1, (short) 12, (short) 2707, (short) 1911, (short) 1, (short) 1, (short) 1323, (short) 1911, (short) 12, (short) 21, (short) 2647, (short) 1912, (short) 12, (short) 9, (short) 1206, (short) 1913, (short) 11, (short) 29, (short) 2741, (short) 1914, (short) 11, (short) 18, (short) 1450, (short) 1915, (short) 11, (short) 8, (short) 3413, (short) 1916, (short) 10, (short) 27, (short) 3370, (short) 1917, (short) 10, (short) 17, (short) 2646, (short) 1918, (short) 10, (short) 6, (short) 1198, (short) 1919, (short) 9, (short) 25, (short) 2397, (short) 1920, (short) 9, (short) 13, (short) 748, (short) 1921, (short) 9, (short) 3, (short) 1749, (short) 1922, (short) 8, (short) 23, (short) 1706, (short) 1923, (short) 8, (short) 13, (short) 1365, (short) 1924, (short) 8, (short) 1, (short) 1195, (short) 1925, (short) 7, (short) 21, (short) 2395, (short) 1926, (short) 7, (short) 10, (short) 698, (short) 1927, (short) 6, (short) 30, (short) 1397, (short) 1928, (short) 6, (short) 18, (short) 2994, (short) 1929, (short) 6, (short) 8, (short) 1892, (short) 1930, (short) 5, (short) 29, (short) 1865, (short) 1931, (short) 5, (short) 18, (short) 1621, (short) 1932, (short) 5, (short) 6, (short) 683, (short) 1933, (short) 4, (short) 25, (short) 1371, (short) 1934, (short) 4, (short) 14, (short) 2778, (short) 1935, (short) 4, (short) 4, (short) 1748, (short) 1936, (short) 3, (short) 24, (short) 3785, (short) 1937, (short) 3, (short) 13, (short) 3474, (short) 1938, (short) 3, (short) 3, (short) 3365, (short) 1939, (short) 2, (short) 20, (short) 2637, (short) 1940, (short) 2, (short) 9, (short) 685, (short) 1941, (short) 1, (short) 28, (short) 1389, (short) 1942, (short) 1, (short) 17, (short) 2922, (short) 1943, (short) 1, (short) 7, (short) 2898, (short) 1943, (short) 12, (short) 28, (short) 2725, (short) 1944, (short) 12, (short) 16, (short) 2635, (short) 1945, (short) 12, (short) 5, (short) 1175, (short) 1946, (short) 11, (short) 24, (short) 2359, (short) 1947, (short) 11, (short) 13, (short) 694, (short) 1948, (short) 11, (short) 2, (short) 1397, (short) 1949, (short) 10, (short) 22, (short) 3434, (short) 1950, (short) 10, (short) 12, (short) 3410, (short) 1951, (short) 10, (short) 2, (short) 2710, (short) 1952, (short) 9, (short) 20, (short) 2349, (short) 1953, (short) 9, (short) 9, (short) 605, (short) 1954, (short) 8, (short) 29, (short) 1245, (short) 1955, (short) 8, (short) 18, (short) 2778, (short) 1956, (short) 8, (short) 7, (short) 1492, (short) 1957, (short) 7, (short) 28, (short) 3497, (short) 1958, (short) 7, (short) 17, (short) 3410, (short) 1959, (short) 7, (short) 7, (short) 2730, (short) 1960, (short) 6, (short) 25, (short) 1238, (short) 1961, (short) 6, (short) 14, (short) 2486, (short) 1962, (short) 6, (short) 3, (short) 884, (short) 1963, (short) 5, (short) 24, (short) 1897, (short) 1964, (short) 5, (short) 12, (short) 1874, (short) 1965, (short) 5, (short) 2, (short) 1701, (short) 1966, (short) 4, (short) 21, (short) 1355, (short) 1967, (short) 4, (short) 10, (short) 2731, (short) 1968, (short) 3, (short) 29, (short) 1370, (short) 1969, (short) 3, (short) 19, (short) 2773, (short) 1970, (short) 3, (short) 8, (short) 3538, (short) 1971, (short) 2, (short) 26, (short) 3492, (short) 1972, (short) 2, (short) 16, (short) 3401, (short) 1973, (short) 2, (short) 4, (short) 2709, (short) 1974, (short) 1, (short) 24, (short) 1325, (short) 1975, (short) 1, (short) 13, (short) 2653, (short) 1976, (short) 1, (short) 2, (short) 1370, (short) 1976, (short) 12, (short) 22, (short) 2773, (short) 1977, (short) 12, (short) 11, (short) 1706, (short) 1978, (short) 12, (short) 1, (short) 1685, (short) 1979, (short) 11, (short) 20, (short) 1323, (short) 1980, (short) 11, (short) 8, (short) 2647, (short) 1981, (short) 10, (short) 28, (short) 1198, (short) 1982, (short) 10, (short) 18, (short) 2422, (short) 1983, (short) 10, (short) 7, (short) 1388, (short) 1984, (short) 9, (short) 26, (short) 2901, (short) 1985, (short) 9, (short) 15, (short) 2730, (short) 1986, (short) 9, (short) 5, (short) 2645, (short) 1987, (short) 8, (short) 25, (short) 1197, (short) 1988, (short) 8, (short) 13, (short) 2397, (short) 1989, (short) 8, (short) 2, (short) 730, (short) 1990, (short) 7, (short) 23, (short) 1497, (short) 1991, (short) 7, (short) 12, (short) 3506, (short) 1992, (short) 7, (short) 1, (short) 2980, (short) 1993, (short) 6, (short) 21, (short) 2890, (short) 1994, (short) 6, (short) 10, (short) 2645, (short) 1995, (short) 5, (short) 30, (short) 693, (short) 1996, (short) 5, (short) 18, (short) 1397, (short) 1997, (short) 5, (short) 7, (short) 2922, (short) 1998, (short) 4, (short) 27, (short) 3026, (short) 1999, (short) 4, (short) 17, (short) 3012, (short) 2000, (short) 4, (short) 6, (short) 2953, (short) 2001, (short) 3, (short) 26, (short) 2709, (short) 2002, (short) 3, (short) 15, (short) 1325, (short) 2003, (short) 3, (short) 4, (short) 1453, (short) 2004, (short) 2, (short) 21, (short) 2922, (short) 2005, (short) 2, (short) 10, (short) 1748, (short) 2006, (short) 1, (short) 31, (short) 3529, (short) 2007, (short) 1, (short) 20, (short) 3474, (short) 2008, (short) 1, (short) 10, (short) 2726, (short) 2008, (short) 12, (short) 29, (short) 2390, (short) 2009, (short) 12, (short) 18, (short) 686, (short) 2010, (short) 12, (short) 7, (short) 1389, (short) 2011, (short) 11, (short) 26, (short) 874, (short) 2012, (short) 11, (short) 15, (short) 2901, (short) 2013, (short) 11, (short) 4, (short) 2730, (short) 2014, (short) 10, (short) 25, (short) 2381, (short) 2015, (short) 10, (short) 14, (short) 1181, (short) 2016, (short) 10, (short) 2, (short) 2397, (short) 2017, (short) 9, (short) 21, (short) 698, (short) 2018, (short) 9, (short) 11, (short) 1461, (short) 2019, (short) 8, (short) 31, (short) 1450, (short) 2020, (short) 8, (short) 20, (short) 3413, (short) 2021, (short) 8, (short) 9, (short) 2714, (short) 2022, (short) 7, (short) 30, (short) 2350, (short) 2023, (short) 7, (short) 19, (short) 622, (short) 2024, (short) 7, (short) 7, (short) 1373, (short) 2025, (short) 6, (short) 26, (short) 2778, (short) 2026, (short) 6, (short) 16, (short) 1748, (short) 2027, (short) 6, (short) 6, (short) 1701, (short) 2028, (short) 5, (short) 25, (short) 1355, (short) 2029, (short) 5, (short) 14, (short) 2711, (short) 2030, (short) 5, (short) 3, (short) 1358, (short) 2031, (short) 4, (short) 23, (short) 2734, (short) 2032, (short) 4, (short) 11, (short) 1452, (short) 2033, (short) 4, (short) 1, (short) 2985, (short) 2034, (short) 3, (short) 21, (short) 3474, (short) 2035, (short) 3, (short) 11, (short) 2853, (short) 2036, (short) 2, (short) 28, (short) 1611, (short) 2037, (short) 2, (short) 16, (short) 3243, (short) 2038, (short) 2, (short) 5, (short) 1370, (short) 2039, (short) 1, (short) 26, (short) 2901, (short) 2040, (short) 1, (short) 15, (short) 1746, (short) 2041, (short) 1, (short) 4, (short) 3749, (short) 2041, (short) 12, (short) 24, (short) 3658, (short) 2042, (short) 12, (short) 14, (short) 2709, (short) 2043, (short) 12, (short) 3, (short) 1325, (short) 2044, (short) 11, (short) 21, (short) 2733, (short) 2045, (short) 11, (short) 10, (short) 876, (short) 2046, (short) 10, (short) 31, (short) 1881, (short) 2047, (short) 10, (short) 20, (short) 1746, (short) 2048, (short) 10, (short) 9, (short) 1685, (short) 2049, (short) 9, (short) 28, (short) 1325, (short) 2050, (short) 9, (short) 17, (short) 2651, (short) 2051, (short) 9, (short) 6, (short) 1210, (short) 2052, (short) 8, (short) 26, (short) 2490, (short) 2053, (short) 8, (short) 15, (short) 948, (short) 2054, (short) 8, (short) 5, (short) 2921, (short) 2055, (short) 7, (short) 25, (short) 2898, (short) 2056, (short) 7, (short) 14, (short) 2726, (short) 2057, (short) 7, (short) 3, (short) 1206, (short) 2058, (short) 6, (short) 22, (short) 2413, (short) 2059, (short) 6, (short) 11, (short) 748, (short) 2060, (short) 5, (short) 31, (short) 1753, (short) 2061, (short) 5, (short) 20, (short) 3762, (short) 2062, (short) 5, (short) 10, (short) 3412, (short) 2063, (short) 4, (short) 30, (short) 3370, (short) 2064, (short) 4, (short) 18, (short) 2646, (short) 2065, (short) 4, (short) 7, (short) 1198, (short) 2066, (short) 3, (short) 27, (short) 2413, (short) 2067, (short) 3, (short) 16, (short) 3434, (short) 2068, (short) 3, (short) 5, (short) 2900, (short) 2069, (short) 2, (short) 23, (short) 2857, (short) 2070, (short) 2, (short) 12, (short) 2707, (short) 2071, (short) 2, (short) 1, (short) 1323, (short) 2072, (short) 1, (short) 21, (short) 2647, (short) 2073, (short) 1, (short) 9, (short) 1334, (short) 2073, (short) 12, (short) 30, (short) 2741, (short) 2074, (short) 12, (short) 19, (short) 1706, (short) 2075, (short) 12, (short) 9, (short) 3731, (short) 2076, (short) 11, (short) 27, (short) 0, (short) 2077, (short) 11, (short) 17 };
      UmAlQuraCalendar.DateMapping[] dateMappingArray = new UmAlQuraCalendar.DateMapping[numArray.Length / 4];
      for (int index = 0; index < dateMappingArray.Length; ++index)
        dateMappingArray[index] = new UmAlQuraCalendar.DateMapping((int) numArray[index * 4], (int) numArray[index * 4 + 1], (int) numArray[index * 4 + 2], (int) numArray[index * 4 + 3]);
      return dateMappingArray;
    }

    private static void ConvertHijriToGregorian(int HijriYear, int HijriMonth, int HijriDay, ref int yg, ref int mg, ref int dg)
    {
      int num1 = HijriDay - 1;
      int index1 = HijriYear - 1318;
      DateTime dateTime1 = UmAlQuraCalendar.HijriYearInfo[index1].GregorianDate;
      int num2 = UmAlQuraCalendar.HijriYearInfo[index1].HijriMonthsLengthFlags;
      for (int index2 = 1; index2 < HijriMonth; ++index2)
      {
        num1 += 29 + (num2 & 1);
        num2 >>= 1;
      }
      DateTime dateTime2 = dateTime1.AddDays((double) num1);
      yg = dateTime2.Year;
      mg = dateTime2.Month;
      dg = dateTime2.Day;
    }

    private static long GetAbsoluteDateUmAlQura(int year, int month, int day)
    {
      int yg = 0;
      int mg = 0;
      int dg = 0;
      UmAlQuraCalendar.ConvertHijriToGregorian(year, month, day, ref yg, ref mg, ref dg);
      return GregorianCalendar.GetAbsoluteDate(yg, mg, dg);
    }

    internal static void CheckTicksRange(long ticks)
    {
      if (ticks < UmAlQuraCalendar.minDate.Ticks || ticks > UmAlQuraCalendar.maxDate.Ticks)
        throw new ArgumentOutOfRangeException("time", string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), (object) UmAlQuraCalendar.minDate, (object) UmAlQuraCalendar.maxDate));
    }

    internal static void CheckEraRange(int era)
    {
      if (era != 0 && era != 1)
        throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    internal static void CheckYearRange(int year, int era)
    {
      UmAlQuraCalendar.CheckEraRange(era);
      if (year < 1318 || year > 1500)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1318, (object) 1500));
    }

    internal static void CheckYearMonthRange(int year, int month, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      if (month < 1 || month > 12)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
    }

    private static void ConvertGregorianToHijri(DateTime time, ref int HijriYear, ref int HijriMonth, ref int HijriDay)
    {
      int index = (int) ((time.Ticks - UmAlQuraCalendar.minDate.Ticks) / 864000000000L) / 355;
      do
        ;
      while (time.CompareTo(UmAlQuraCalendar.HijriYearInfo[++index].GregorianDate) > 0);
      if (time.CompareTo(UmAlQuraCalendar.HijriYearInfo[index].GregorianDate) != 0)
        --index;
      TimeSpan timeSpan = time.Subtract(UmAlQuraCalendar.HijriYearInfo[index].GregorianDate);
      int num1 = index + 1318;
      int num2 = 1;
      int num3 = 1;
      double totalDays = timeSpan.TotalDays;
      int num4 = UmAlQuraCalendar.HijriYearInfo[index].HijriMonthsLengthFlags;
      int num5 = 29 + (num4 & 1);
      while (totalDays >= (double) num5)
      {
        totalDays -= (double) num5;
        num4 >>= 1;
        num5 = 29 + (num4 & 1);
        ++num2;
      }
      int num6 = num3 + (int) totalDays;
      HijriDay = num6;
      HijriMonth = num2;
      HijriYear = num1;
    }

    internal virtual int GetDatePart(DateTime time, int part)
    {
      int HijriYear = 0;
      int HijriMonth = 0;
      int HijriDay = 0;
      UmAlQuraCalendar.CheckTicksRange(time.Ticks);
      UmAlQuraCalendar.ConvertGregorianToHijri(time, ref HijriYear, ref HijriMonth, ref HijriDay);
      if (part == 0)
        return HijriYear;
      if (part == 2)
        return HijriMonth;
      if (part == 3)
        return HijriDay;
      if (part == 1)
        return (int) (UmAlQuraCalendar.GetAbsoluteDateUmAlQura(HijriYear, HijriMonth, HijriDay) - UmAlQuraCalendar.GetAbsoluteDateUmAlQura(HijriYear, 1, 1) + 1L);
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
    }

    /// <summary>计算与指定初始日期相距指定月数的日期。</summary>
    /// <returns>在将 <paramref name="months" /> 参数指定的月数加到 <paramref name="time" /> 参数指定的日期后所得的日期。</returns>
    /// <param name="time">要加上月数的日期。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <param name="months">要添加的正月数或负月数。</param>
    /// <exception cref="T:System.ArgumentException">得到的日期不在 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围内。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="months" /> 小于 -120,000 或大于 120,000。- 或 -<paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override DateTime AddMonths(DateTime time, int months)
    {
      if (months < -120000 || months > 120000)
        throw new ArgumentOutOfRangeException("months", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) -120000, (object) 120000));
      int datePart1 = this.GetDatePart(time, 0);
      int datePart2 = this.GetDatePart(time, 2);
      int day = this.GetDatePart(time, 3);
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
      if (day > 29)
      {
        int daysInMonth = this.GetDaysInMonth(year, month);
        if (day > daysInMonth)
          day = daysInMonth;
      }
      UmAlQuraCalendar.CheckYearRange(year, 1);
      DateTime dateTime = new DateTime(UmAlQuraCalendar.GetAbsoluteDateUmAlQura(year, month, day) * 864000000000L + time.Ticks % 864000000000L);
      Calendar.CheckAddResult(dateTime.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
      return dateTime;
    }

    /// <summary>计算与指定初始日期相距指定年数的日期。</summary>
    /// <returns>在将 <paramref name="years" /> 参数指定的年数加到 <paramref name="time" /> 参数指定的日期后所得的日期。</returns>
    /// <param name="time">要加上年数的日期。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <param name="years">要添加的正年数或负年数。</param>
    /// <exception cref="T:System.ArgumentException">得到的日期不在 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围内。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="years" /> 小于 -10,000 或大于 10,000。- 或 -<paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override DateTime AddYears(DateTime time, int years)
    {
      return this.AddMonths(time, years * 12);
    }

    /// <summary>计算指定日期出现在月中的哪一天。</summary>
    /// <returns>一个从 1 到 30 的整数，表示由 <paramref name="time" /> 参数指定的月中日期。</returns>
    /// <param name="time">要读取的数据类型。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override int GetDayOfMonth(DateTime time)
    {
      return this.GetDatePart(time, 3);
    }

    /// <summary>计算指定日期出现在星期几。</summary>
    /// <returns>一个 <see cref="T:System.DayOfWeek" /> 值，表示 <paramref name="time" /> 参数指定的日期是星期几。</returns>
    /// <param name="time">要读取的数据类型。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
      return (DayOfWeek) ((int) (time.Ticks / 864000000000L + 1L) % 7);
    }

    /// <summary>计算指定日期出现在年中的哪一天。</summary>
    /// <returns>一个从 1 到 355 的整数，表示 <paramref name="time" /> 参数指定的日期是年中的第几天。</returns>
    /// <param name="time">要读取的数据类型。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override int GetDayOfYear(DateTime time)
    {
      return this.GetDatePart(time, 1);
    }

    /// <summary>计算指定纪元年份的指定月份中的天数。</summary>
    /// <returns>指定纪元年份中指定月份的天数。返回值是 29（在平年中）或 30（在闰年中）。</returns>
    /// <param name="year">年份。</param>
    /// <param name="month">1 到 12 之间的一个整数，用于表示月。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" /> 或 <paramref name="era" /> 超出了 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围。</exception>
    public override int GetDaysInMonth(int year, int month, int era)
    {
      UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
      return (UmAlQuraCalendar.HijriYearInfo[year - 1318].HijriMonthsLengthFlags & 1 << month - 1) == 0 ? 29 : 30;
    }

    internal static int RealGetDaysInYear(int year)
    {
      int num1 = 0;
      int num2 = UmAlQuraCalendar.HijriYearInfo[year - 1318].HijriMonthsLengthFlags;
      for (int index = 1; index <= 12; ++index)
      {
        num1 += 29 + (num2 & 1);
        num2 >>= 1;
      }
      return num1;
    }

    /// <summary>计算指定纪元的指定年份中的天数。</summary>
    /// <returns>指定纪元年份中的天数。天数在平年中为 354，在闰年中为 355。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围。</exception>
    public override int GetDaysInYear(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return UmAlQuraCalendar.RealGetDaysInYear(year);
    }

    /// <summary>计算指定日期出现在哪个纪元。</summary>
    /// <returns>总是返回 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" /> 值。</returns>
    /// <param name="time">要读取的数据类型。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override int GetEra(DateTime time)
    {
      UmAlQuraCalendar.CheckTicksRange(time.Ticks);
      return 1;
    }

    /// <summary>计算指定日期出现在哪个月份中。</summary>
    /// <returns>一个从 1 到 12 的整数，表示 <paramref name="time" /> 参数指定的日期中的月份。</returns>
    /// <param name="time">要读取的数据类型。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override int GetMonth(DateTime time)
    {
      return this.GetDatePart(time, 2);
    }

    /// <summary>计算指定纪元的指定年份中的月数。</summary>
    /// <returns>始终为 12。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 UmAlQuaraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了此日历支持的范围。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="era" /> 超出了此日历支持的范围。</exception>
    public override int GetMonthsInYear(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return 12;
    }

    /// <summary>计算由指定 <see cref="T:System.DateTime" /> 表示的日期所在的年份。</summary>
    /// <returns>一个整数，表示由 <paramref name="time" /> 参数指定的年份。</returns>
    /// <param name="time">要读取的数据类型。<see cref="T:System.Globalization.UmAlQuraCalendar" /> 类仅支持从 04/30/1900 00.00.00（公历日期）到 11/16/2077 23:59:59（公历日期）的日期。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="time" /> 超出了此日历支持的范围。</exception>
    public override int GetYear(DateTime time)
    {
      return this.GetDatePart(time, 0);
    }

    /// <summary>确定指定的日期是否为闰日。</summary>
    /// <returns>如果指定的日期是闰日，则为 true；否则为 false。返回值始终为 false 因为 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类不支持闰日。</returns>
    /// <param name="year">年份。</param>
    /// <param name="month">1 到 12 之间的一个整数，用于表示月。</param>
    /// <param name="day">1 到 30 之间的一个整数，用于表示日。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" />、<paramref name="day" /> 或 <paramref name="era" /> 超出了 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围。</exception>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
      if (day >= 1 && day <= 29)
      {
        UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
        return false;
      }
      int daysInMonth = this.GetDaysInMonth(year, month, era);
      if (day < 1 || day > daysInMonth)
        throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      return false;
    }

    /// <summary>计算指定纪元年份的闰月。</summary>
    /// <returns>总为 0，因为 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类不支持闰月。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 小于 1318 或大于 1450。- 或 -<paramref name="era" /> 不是 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</exception>
    public override int GetLeapMonth(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return 0;
    }

    /// <summary>确定指定纪元年份中的指定月份是否为闰月。</summary>
    /// <returns>总为 false，因为 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类不支持闰月。</returns>
    /// <param name="year">年份。</param>
    /// <param name="month">1 到 12 之间的一个整数，用于表示月。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" /> 或 <paramref name="era" /> 超出了 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围。</exception>
    public override bool IsLeapMonth(int year, int month, int era)
    {
      UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
      return false;
    }

    /// <summary>确定指定纪元中的指定年份是否为闰年。</summary>
    /// <returns>如果指定的年是闰年，则为 true；否则为 false。</returns>
    /// <param name="year">年份。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 或 <paramref name="era" /> 超出了 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围。</exception>
    public override bool IsLeapYear(int year, int era)
    {
      UmAlQuraCalendar.CheckYearRange(year, era);
      return UmAlQuraCalendar.RealGetDaysInYear(year) == 355;
    }

    /// <summary>返回设置为指定的日期、时间和纪元的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>设置为当前纪元中的指定日期和时间的 <see cref="T:System.DateTime" />。</returns>
    /// <param name="year">年份。</param>
    /// <param name="month">1 到 12 之间的一个整数，用于表示月。</param>
    /// <param name="day">1 到 29 之间的一个整数，用于表示日。</param>
    /// <param name="hour">0 到 23 之间的一个整数，用于表示小时。</param>
    /// <param name="minute">0 到 59 之间的一个整数，用于表示分钟。</param>
    /// <param name="second">0 到 59 之间的一个整数，用于表示秒。</param>
    /// <param name="millisecond">0 到 999 之间的一个整数，用于表示毫秒。</param>
    /// <param name="era">纪元。指定 UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra] 或 <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" />、<paramref name="month" />、<paramref name="day" /> 或 <paramref name="era" /> 超出了 <see cref="T:System.Globalization.UmAlQuraCalendar" /> 类支持的范围。- 或 -<paramref name="hour" /> 小于零或大于 23。- 或 -<paramref name="minute" /> 小于零或大于 59。- 或 -<paramref name="second" /> 小于零或大于 59。- 或 -<paramref name="millisecond" /> 小于零或大于 999。</exception>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
      if (day >= 1 && day <= 29)
      {
        UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
      }
      else
      {
        int daysInMonth = this.GetDaysInMonth(year, month, era);
        if (day < 1 || day > daysInMonth)
          throw new ArgumentOutOfRangeException("day", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), (object) daysInMonth, (object) month));
      }
      long absoluteDateUmAlQura = UmAlQuraCalendar.GetAbsoluteDateUmAlQura(year, month, day);
      if (absoluteDateUmAlQura >= 0L)
        return new DateTime(absoluteDateUmAlQura * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
      throw new ArgumentOutOfRangeException((string) null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
    }

    /// <summary>使用 <see cref="P:System.Globalization.UmAlQuraCalendar.TwoDigitYearMax" /> 属性将指定的年份转换为四位数年份，以确定相应的纪元。</summary>
    /// <returns>如果 <paramref name="year" /> 参数是两位数年份，则返回值是对应的四位数年份。如果 <paramref name="year" /> 参数是四位数年份，则返回值是未更改的 <paramref name="year" /> 参数。</returns>
    /// <param name="year">一个从 0 到 99 的两位数年份，或一个从 1318 到 1450 的四位数 Um Al Qura 日历年份。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="year" /> 超出了此日历支持的范围。</exception>
    public override int ToFourDigitYear(int year)
    {
      if (year < 0)
        throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (year < 100)
        return base.ToFourDigitYear(year);
      if (year < 1318 || year > 1500)
        throw new ArgumentOutOfRangeException("year", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 1318, (object) 1500));
      return year;
    }

    internal struct DateMapping
    {
      internal int HijriMonthsLengthFlags;
      internal DateTime GregorianDate;

      internal DateMapping(int MonthsLengthFlags, int GYear, int GMonth, int GDay)
      {
        this.HijriMonthsLengthFlags = MonthsLengthFlags;
        this.GregorianDate = new DateTime(GYear, GMonth, GDay);
      }
    }
  }
}
