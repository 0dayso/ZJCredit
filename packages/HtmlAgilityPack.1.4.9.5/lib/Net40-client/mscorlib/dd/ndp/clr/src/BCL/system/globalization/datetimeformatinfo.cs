// Decompiled with JetBrains decompiler
// Type: System.Globalization.DateTimeFormatInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;
using System.Threading;

namespace System.Globalization
{
  /// <summary>提供有关日期和时间值格式的区域性特定信息。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DateTimeFormatInfo : ICloneable, IFormatProvider
  {
    internal static bool preferExistingTokens = DateTimeFormatInfo.InitPreferExistingTokens();
    private static char[] MonthSpaces = new char[2]{ ' ', ' ' };
    internal int firstDayOfWeek = -1;
    internal int calendarWeekRule = -1;
    [OptionalField(VersionAdded = 2)]
    internal DateTimeFormatFlags formatFlags = DateTimeFormatFlags.NotInitialized;
    private static volatile DateTimeFormatInfo invariantInfo;
    [NonSerialized]
    private CultureData m_cultureData;
    [OptionalField(VersionAdded = 2)]
    internal string m_name;
    [NonSerialized]
    private string m_langName;
    [NonSerialized]
    private CompareInfo m_compareInfo;
    [NonSerialized]
    private CultureInfo m_cultureInfo;
    internal string amDesignator;
    internal string pmDesignator;
    [OptionalField(VersionAdded = 1)]
    internal string dateSeparator;
    [OptionalField(VersionAdded = 1)]
    internal string generalShortTimePattern;
    [OptionalField(VersionAdded = 1)]
    internal string generalLongTimePattern;
    [OptionalField(VersionAdded = 1)]
    internal string timeSeparator;
    internal string monthDayPattern;
    [OptionalField(VersionAdded = 2)]
    internal string dateTimeOffsetPattern;
    internal const string rfc1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
    internal const string sortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
    internal const string universalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
    internal Calendar calendar;
    [OptionalField(VersionAdded = 1)]
    internal string fullDateTimePattern;
    internal string[] abbreviatedDayNames;
    [OptionalField(VersionAdded = 2)]
    internal string[] m_superShortDayNames;
    internal string[] dayNames;
    internal string[] abbreviatedMonthNames;
    internal string[] monthNames;
    [OptionalField(VersionAdded = 2)]
    internal string[] genitiveMonthNames;
    [OptionalField(VersionAdded = 2)]
    internal string[] m_genitiveAbbreviatedMonthNames;
    [OptionalField(VersionAdded = 2)]
    internal string[] leapYearMonthNames;
    internal string longDatePattern;
    internal string shortDatePattern;
    internal string yearMonthPattern;
    internal string longTimePattern;
    internal string shortTimePattern;
    [OptionalField(VersionAdded = 3)]
    private string[] allYearMonthPatterns;
    internal string[] allShortDatePatterns;
    internal string[] allLongDatePatterns;
    internal string[] allShortTimePatterns;
    internal string[] allLongTimePatterns;
    internal string[] m_eraNames;
    internal string[] m_abbrevEraNames;
    internal string[] m_abbrevEnglishEraNames;
    internal int[] optionalCalendars;
    private const int DEFAULT_ALL_DATETIMES_SIZE = 132;
    internal bool m_isReadOnly;
    [OptionalField(VersionAdded = 1)]
    private int CultureID;
    [OptionalField(VersionAdded = 1)]
    private bool m_useUserOverride;
    [OptionalField(VersionAdded = 1)]
    private bool bUseCalendarInfo;
    [OptionalField(VersionAdded = 1)]
    private int nDataItem;
    [OptionalField(VersionAdded = 2)]
    internal bool m_isDefaultCalendar;
    [OptionalField(VersionAdded = 2)]
    private static volatile Hashtable s_calendarNativeNames;
    [OptionalField(VersionAdded = 1)]
    internal string[] m_dateWords;
    [NonSerialized]
    private string m_fullTimeSpanPositivePattern;
    [NonSerialized]
    private string m_fullTimeSpanNegativePattern;
    internal const DateTimeStyles InvalidDateTimeStyles = ~(DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind);
    [NonSerialized]
    private TokenHashValue[] m_dtfiTokenHash;
    private const int TOKEN_HASH_SIZE = 199;
    private const int SECOND_PRIME = 197;
    private const string dateSeparatorOrTimeZoneOffset = "-";
    private const string invariantDateSeparator = "/";
    private const string invariantTimeSeparator = ":";
    internal const string IgnorablePeriod = ".";
    internal const string IgnorableComma = ",";
    internal const string CJKYearSuff = "年";
    internal const string CJKMonthSuff = "月";
    internal const string CJKDaySuff = "日";
    internal const string KoreanYearSuff = "년";
    internal const string KoreanMonthSuff = "월";
    internal const string KoreanDaySuff = "일";
    internal const string KoreanHourSuff = "시";
    internal const string KoreanMinuteSuff = "분";
    internal const string KoreanSecondSuff = "초";
    internal const string CJKHourSuff = "時";
    internal const string ChineseHourSuff = "时";
    internal const string CJKMinuteSuff = "分";
    internal const string CJKSecondSuff = "秒";
    internal const string LocalTimeMark = "T";
    internal const string KoreanLangName = "ko";
    internal const string JapaneseLangName = "ja";
    internal const string EnglishLangName = "en";
    private static volatile DateTimeFormatInfo s_jajpDTFI;
    private static volatile DateTimeFormatInfo s_zhtwDTFI;

    private string CultureName
    {
      get
      {
        if (this.m_name == null)
          this.m_name = this.m_cultureData.CultureName;
        return this.m_name;
      }
    }

    private CultureInfo Culture
    {
      get
      {
        if (this.m_cultureInfo == null)
          this.m_cultureInfo = CultureInfo.GetCultureInfo(this.CultureName);
        return this.m_cultureInfo;
      }
    }

    private string LanguageName
    {
      [SecurityCritical] get
      {
        if (this.m_langName == null)
          this.m_langName = this.m_cultureData.SISO639LANGNAME;
        return this.m_langName;
      }
    }

    /// <summary>获取不依赖于区域性的（固定）默认只读的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象。</summary>
    /// <returns>不依赖于区域性的（固定的）默认只读对象。</returns>
    [__DynamicallyInvokable]
    public static DateTimeFormatInfo InvariantInfo
    {
      [__DynamicallyInvokable] get
      {
        if (DateTimeFormatInfo.invariantInfo == null)
        {
          DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
          dateTimeFormatInfo.Calendar.SetReadOnlyState(true);
          int num = 1;
          dateTimeFormatInfo.m_isReadOnly = num != 0;
          DateTimeFormatInfo.invariantInfo = dateTimeFormatInfo;
        }
        return DateTimeFormatInfo.invariantInfo;
      }
    }

    /// <summary>获取基于当前区域性对值进行格式设置的只读的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象。</summary>
    /// <returns>一个基于当前线程的 <see cref="T:System.Globalization.CultureInfo" /> 对象的只读的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象。</returns>
    [__DynamicallyInvokable]
    public static DateTimeFormatInfo CurrentInfo
    {
      [__DynamicallyInvokable] get
      {
        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        if (!currentCulture.m_isInherited)
        {
          DateTimeFormatInfo dateTimeFormatInfo = currentCulture.dateTimeInfo;
          if (dateTimeFormatInfo != null)
            return dateTimeFormatInfo;
        }
        return (DateTimeFormatInfo) currentCulture.GetFormat(typeof (DateTimeFormatInfo));
      }
    }

    /// <summary>获取或设置表示处于“上午”（中午前）的各小时的字符串指示符。</summary>
    /// <returns>表示属于上午的各小时的字符串指示符。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 默认为“AM”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string AMDesignator
    {
      [__DynamicallyInvokable] get
      {
        return this.amDesignator;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.ClearTokenHashTable();
        this.amDesignator = value;
      }
    }

    /// <summary>获取或设置用于当前区域性的日历。</summary>
    /// <returns>用于当前区域性的日历。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 默认为 <see cref="T:System.Globalization.GregorianCalendar" /> 对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a <see cref="T:System.Globalization.Calendar" /> object that is not valid for the current culture. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public Calendar Calendar
    {
      [__DynamicallyInvokable] get
      {
        return this.calendar;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
        if (value != this.calendar)
        {
          CultureInfo.CheckDomainSafetyObject((object) value, (object) this);
          for (int index = 0; index < this.OptionalCalendars.Length; ++index)
          {
            if (this.OptionalCalendars[index] == value.ID)
            {
              if (this.calendar != null)
              {
                this.m_eraNames = (string[]) null;
                this.m_abbrevEraNames = (string[]) null;
                this.m_abbrevEnglishEraNames = (string[]) null;
                this.monthDayPattern = (string) null;
                this.dayNames = (string[]) null;
                this.abbreviatedDayNames = (string[]) null;
                this.m_superShortDayNames = (string[]) null;
                this.monthNames = (string[]) null;
                this.abbreviatedMonthNames = (string[]) null;
                this.genitiveMonthNames = (string[]) null;
                this.m_genitiveAbbreviatedMonthNames = (string[]) null;
                this.leapYearMonthNames = (string[]) null;
                this.formatFlags = DateTimeFormatFlags.NotInitialized;
                this.allShortDatePatterns = (string[]) null;
                this.allLongDatePatterns = (string[]) null;
                this.allYearMonthPatterns = (string[]) null;
                this.dateTimeOffsetPattern = (string) null;
                this.longDatePattern = (string) null;
                this.shortDatePattern = (string) null;
                this.yearMonthPattern = (string) null;
                this.fullDateTimePattern = (string) null;
                this.generalShortTimePattern = (string) null;
                this.generalLongTimePattern = (string) null;
                this.dateSeparator = (string) null;
                this.ClearTokenHashTable();
              }
              this.calendar = value;
              this.InitializeOverridableProperties(this.m_cultureData, this.calendar.ID);
              return;
            }
          }
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Argument_InvalidCalendar"));
        }
      }
    }

    private int[] OptionalCalendars
    {
      get
      {
        if (this.optionalCalendars == null)
          this.optionalCalendars = this.m_cultureData.CalendarIds;
        return this.optionalCalendars;
      }
    }

    internal string[] EraNames
    {
      get
      {
        if (this.m_eraNames == null)
          this.m_eraNames = this.m_cultureData.EraNames(this.Calendar.ID);
        return this.m_eraNames;
      }
    }

    internal string[] AbbreviatedEraNames
    {
      get
      {
        if (this.m_abbrevEraNames == null)
          this.m_abbrevEraNames = this.m_cultureData.AbbrevEraNames(this.Calendar.ID);
        return this.m_abbrevEraNames;
      }
    }

    internal string[] AbbreviatedEnglishEraNames
    {
      get
      {
        if (this.m_abbrevEnglishEraNames == null)
          this.m_abbrevEnglishEraNames = this.m_cultureData.AbbreviatedEnglishEraNames(this.Calendar.ID);
        return this.m_abbrevEnglishEraNames;
      }
    }

    /// <summary>获取或设置分隔日期中各组成部分（即年、月、日）的字符串。</summary>
    /// <returns>分隔日期中各组成部分（即年、月、日）的字符串。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 默认为“/”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    public string DateSeparator
    {
      get
      {
        return this.dateSeparator;
      }
      set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.ClearTokenHashTable();
        this.dateSeparator = value;
      }
    }

    /// <summary>获取或设置一周的第一天。</summary>
    /// <returns>表示一周的第一天的枚举值。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 默认为 <see cref="F:System.DayOfWeek.Sunday" />。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public DayOfWeek FirstDayOfWeek
    {
      [__DynamicallyInvokable] get
      {
        return (DayOfWeek) this.firstDayOfWeek;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value >= DayOfWeek.Sunday && value <= DayOfWeek.Saturday)
          this.firstDayOfWeek = (int) value;
        else
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) DayOfWeek.Sunday, (object) DayOfWeek.Saturday));
      }
    }

    /// <summary>获取或设置一个值，该值指定使用哪个规则确定该年的第一个日历周。</summary>
    /// <returns>确定该年的第一个日历周的值。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 的默认值是 <see cref="F:System.Globalization.CalendarWeekRule.FirstDay" />。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
    /// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
    [__DynamicallyInvokable]
    public CalendarWeekRule CalendarWeekRule
    {
      [__DynamicallyInvokable] get
      {
        return (CalendarWeekRule) this.calendarWeekRule;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value >= CalendarWeekRule.FirstDay && value <= CalendarWeekRule.FirstFourDayWeek)
          this.calendarWeekRule = (int) value;
        else
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) CalendarWeekRule.FirstDay, (object) CalendarWeekRule.FirstFourDayWeek));
      }
    }

    /// <summary>为长日期和长时间值获取或设置自定义格式字符串。</summary>
    /// <returns>长日期和时间值的自定义格式字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string FullDateTimePattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.fullDateTimePattern == null)
          this.fullDateTimePattern = this.LongDatePattern + " " + this.LongTimePattern;
        return this.fullDateTimePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.fullDateTimePattern = value;
      }
    }

    /// <summary>获取或设置长日期值的自定义格式字符串。</summary>
    /// <returns>长日期值的自定义格式字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string LongDatePattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.longDatePattern == null)
          this.longDatePattern = this.UnclonedLongDatePatterns[0];
        return this.longDatePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.longDatePattern = value;
        this.ClearTokenHashTable();
        this.fullDateTimePattern = (string) null;
      }
    }

    /// <summary>为长时间值获取或设置自定义格式字符串。</summary>
    /// <returns>长时间值的格式模式。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string LongTimePattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.longTimePattern == null)
          this.longTimePattern = this.UnclonedLongTimePatterns[0];
        return this.longTimePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.longTimePattern = value;
        this.ClearTokenHashTable();
        this.fullDateTimePattern = (string) null;
        this.generalLongTimePattern = (string) null;
        this.dateTimeOffsetPattern = (string) null;
      }
    }

    /// <summary>获取或设置月份和日期值的自定义格式字符串。</summary>
    /// <returns>月份和日期值的自定义格式字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string MonthDayPattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.monthDayPattern == null)
          this.monthDayPattern = this.m_cultureData.MonthDay(this.Calendar.ID);
        return this.monthDayPattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.monthDayPattern = value;
      }
    }

    /// <summary>获取或设置表示处于“下午”（中午后）的各小时的字符串指示符。</summary>
    /// <returns>表示处于“下午”（中午后）的各小时的字符串指示符。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 默认为“PM”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string PMDesignator
    {
      [__DynamicallyInvokable] get
      {
        return this.pmDesignator;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.ClearTokenHashTable();
        this.pmDesignator = value;
      }
    }

    /// <summary>获取自定义的格式字符串，该字符串用于基于 Internet 工程任务组 (IETF) 征求意见文档 (RFC) 1123 规范的时间值。</summary>
    /// <returns>基于 IETF RFC 1123 规范的时间值的自定义格式字符串。</returns>
    [__DynamicallyInvokable]
    public string RFC1123Pattern
    {
      [__DynamicallyInvokable] get
      {
        return "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
      }
    }

    /// <summary>获取或设置短日期值的自定义格式字符串。</summary>
    /// <returns>短日期值的自定义格式字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string ShortDatePattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.shortDatePattern == null)
          this.shortDatePattern = this.UnclonedShortDatePatterns[0];
        return this.shortDatePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.shortDatePattern = value;
        this.ClearTokenHashTable();
        this.generalLongTimePattern = (string) null;
        this.generalShortTimePattern = (string) null;
        this.dateTimeOffsetPattern = (string) null;
      }
    }

    /// <summary>获取或设置短时间值的自定义格式字符串。</summary>
    /// <returns>短时间值的自定义格式字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string ShortTimePattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.shortTimePattern == null)
          this.shortTimePattern = this.UnclonedShortTimePatterns[0];
        return this.shortTimePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.shortTimePattern = value;
        this.ClearTokenHashTable();
        this.generalShortTimePattern = (string) null;
      }
    }

    /// <summary>获取可排序数据和时间值的自定义格式字符串。</summary>
    /// <returns>可排序的日期和时间值的自定义格式字符串。</returns>
    [__DynamicallyInvokable]
    public string SortableDateTimePattern
    {
      [__DynamicallyInvokable] get
      {
        return "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
      }
    }

    internal string GeneralShortTimePattern
    {
      get
      {
        if (this.generalShortTimePattern == null)
          this.generalShortTimePattern = this.ShortDatePattern + " " + this.ShortTimePattern;
        return this.generalShortTimePattern;
      }
    }

    internal string GeneralLongTimePattern
    {
      get
      {
        if (this.generalLongTimePattern == null)
          this.generalLongTimePattern = this.ShortDatePattern + " " + this.LongTimePattern;
        return this.generalLongTimePattern;
      }
    }

    internal string DateTimeOffsetPattern
    {
      get
      {
        if (this.dateTimeOffsetPattern == null)
        {
          this.dateTimeOffsetPattern = this.ShortDatePattern + " " + this.LongTimePattern;
          bool flag1 = false;
          bool flag2 = false;
          char ch1 = '\'';
          for (int index = 0; !flag1 && index < this.LongTimePattern.Length; ++index)
          {
            char ch2 = this.LongTimePattern[index];
            if ((uint) ch2 <= 37U)
            {
              if ((int) ch2 != 34)
              {
                if ((int) ch2 == 37)
                  goto label_13;
                else
                  continue;
              }
            }
            else if ((int) ch2 != 39)
            {
              if ((int) ch2 != 92)
              {
                if ((int) ch2 == 122)
                {
                  flag1 = !flag2;
                  continue;
                }
                continue;
              }
              goto label_13;
            }
            if (flag2 && (int) ch1 == (int) this.LongTimePattern[index])
            {
              flag2 = false;
              continue;
            }
            if (!flag2)
            {
              ch1 = this.LongTimePattern[index];
              flag2 = true;
              continue;
            }
            continue;
label_13:
            ++index;
          }
          if (!flag1)
            this.dateTimeOffsetPattern = this.dateTimeOffsetPattern + " zzz";
        }
        return this.dateTimeOffsetPattern;
      }
    }

    /// <summary>获取或设置分隔时间中各组成部分（即小时、分钟和秒钟）的字符串。</summary>
    /// <returns>分隔时间中各组成部分的字符串。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 默认为“.”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    public string TimeSeparator
    {
      get
      {
        return this.timeSeparator;
      }
      set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.ClearTokenHashTable();
        this.timeSeparator = value;
      }
    }

    /// <summary>获取通用的可排序数据和时间字符串的自定义格式字符串。</summary>
    /// <returns>通用的可排序的日期和时间字符串的自定义格式字符串。</returns>
    [__DynamicallyInvokable]
    public string UniversalSortableDateTimePattern
    {
      [__DynamicallyInvokable] get
      {
        return "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
      }
    }

    /// <summary>获取或设置年份和月份值的自定义格式字符串。</summary>
    /// <returns>年份和月份值的自定义格式字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string YearMonthPattern
    {
      [__DynamicallyInvokable] get
      {
        if (this.yearMonthPattern == null)
          this.yearMonthPattern = this.UnclonedYearMonthPatterns[0];
        return this.yearMonthPattern;
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.yearMonthPattern = value;
        this.ClearTokenHashTable();
      }
    }

    /// <summary>获取或设置 <see cref="T:System.String" /> 类型的一维数组，它包含周中各天的特定于区域性的缩写名称。</summary>
    /// <returns>
    /// <see cref="T:System.String" /> 类型的一维数组，它包含周中各天的特定于区域性的缩写名称。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 的数组包含“Sun”、“Mon”、“Tue”、“Wed”、“Thu”、“Fri”和“Sat”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string[] AbbreviatedDayNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetAbbreviatedDayOfWeekNames().Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 7)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 7), "value");
        string[] values = value;
        int length = values.Length;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.ClearTokenHashTable();
        this.abbreviatedDayNames = value;
      }
    }

    /// <summary>获取或设置与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的唯一最短日期缩写名称的字符串数组。</summary>
    /// <returns>日期名称的字符串数组。</returns>
    /// <exception cref="T:System.ArgumentException">In a set operation, the array does not have exactly seven elements.</exception>
    /// <exception cref="T:System.ArgumentNullException">In a set operation, the value array or one of the elements of the value array is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] ShortestDayNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetSuperShortDayNames().Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 7)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 7), "value");
        string[] values = value;
        int length = values.Length;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.m_superShortDayNames = value;
      }
    }

    /// <summary>获取或设置一维字符串数组，它包含该周中各天的特定于区域性的完整名称。</summary>
    /// <returns>一个一维字符串数组，它包含周中各天的特定于区域性的完整名称。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 数组包含“Sunday”、“Monday”、“Tuesday”、“Wednesday”、“Thursday”、“Friday”和“Saturday”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string[] DayNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetDayOfWeekNames().Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 7)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 7), "value");
        string[] values = value;
        int length = values.Length;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.ClearTokenHashTable();
        this.dayNames = value;
      }
    }

    /// <summary>获取或设置一维字符串数组，它包含各月的特定于区域性的缩写名称。</summary>
    /// <returns>一个具有 13 个元素的一维字符串数组，它包含各月的特定于区域性的缩写名称。对于 12 个月的日历，数组的第 13 个元素是一个空字符串。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 的数组包含“Jan”、“Feb”、“Mar”、“Apr”、“May”、“Jun”、“Jul”、“Aug”、“Sep”、“Oct”、“Nov”、“Dec”和“”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string[] AbbreviatedMonthNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetAbbreviatedMonthNames().Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 13)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 13), "value");
        string[] values = value;
        int length = values.Length - 1;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.ClearTokenHashTable();
        this.abbreviatedMonthNames = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.String" /> 类型的一维数组，它包含月份的特定于区域性的完整名称。</summary>
    /// <returns>一个类型 <see cref="T:System.String" /> 的一维数组，它包含月份的特定于区域性的完整名称。在 12 个月的日历中，数组的第 13 个元素是一个空字符串。<see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> 数组包含“January”、“February”、“March”、“April”、“May”、“June”、“July”、“August”、“September”、“October”、“November”、“December”和“”。</returns>
    /// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
    /// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
    /// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
    [__DynamicallyInvokable]
    public string[] MonthNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetMonthNames().Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 13)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 13), "value");
        string[] values = value;
        int length = values.Length - 1;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.monthNames = value;
        this.ClearTokenHashTable();
      }
    }

    internal bool HasSpacesInMonthNames
    {
      get
      {
        return (uint) (this.FormatFlags & DateTimeFormatFlags.UseSpacesInMonthNames) > 0U;
      }
    }

    internal bool HasSpacesInDayNames
    {
      get
      {
        return (uint) (this.FormatFlags & DateTimeFormatFlags.UseSpacesInDayNames) > 0U;
      }
    }

    private string[] AllYearMonthPatterns
    {
      get
      {
        return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedYearMonthPatterns, this.YearMonthPattern);
      }
    }

    private string[] AllShortDatePatterns
    {
      get
      {
        return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortDatePatterns, this.ShortDatePattern);
      }
    }

    private string[] AllShortTimePatterns
    {
      get
      {
        return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortTimePatterns, this.ShortTimePattern);
      }
    }

    private string[] AllLongDatePatterns
    {
      get
      {
        return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongDatePatterns, this.LongDatePattern);
      }
    }

    private string[] AllLongTimePatterns
    {
      get
      {
        return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongTimePatterns, this.LongTimePattern);
      }
    }

    private string[] UnclonedYearMonthPatterns
    {
      get
      {
        if (this.allYearMonthPatterns == null)
          this.allYearMonthPatterns = this.m_cultureData.YearMonths(this.Calendar.ID);
        return this.allYearMonthPatterns;
      }
    }

    private string[] UnclonedShortDatePatterns
    {
      get
      {
        if (this.allShortDatePatterns == null)
          this.allShortDatePatterns = this.m_cultureData.ShortDates(this.Calendar.ID);
        return this.allShortDatePatterns;
      }
    }

    private string[] UnclonedLongDatePatterns
    {
      get
      {
        if (this.allLongDatePatterns == null)
          this.allLongDatePatterns = this.m_cultureData.LongDates(this.Calendar.ID);
        return this.allLongDatePatterns;
      }
    }

    private string[] UnclonedShortTimePatterns
    {
      get
      {
        if (this.allShortTimePatterns == null)
          this.allShortTimePatterns = this.m_cultureData.ShortTimes;
        return this.allShortTimePatterns;
      }
    }

    private string[] UnclonedLongTimePatterns
    {
      get
      {
        if (this.allLongTimePatterns == null)
          this.allLongTimePatterns = this.m_cultureData.LongTimes;
        return this.allLongTimePatterns;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象是否为只读。</summary>
    /// <returns>如果 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象为只读，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    /// <summary>获取与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的本地日历名称。</summary>
    /// <returns>在与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的区域性中，如果使用的日历的本机名称可用，则为该名称；如果该本地日历名称不可用，则为空字符串 (“”)。</returns>
    [ComVisible(false)]
    public string NativeCalendarName
    {
      get
      {
        return this.m_cultureData.CalendarName(this.Calendar.ID);
      }
    }

    /// <summary>获取或设置与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的月份缩写名称的字符串数组。</summary>
    /// <returns>月份缩写名称的数组。</returns>
    /// <exception cref="T:System.ArgumentException">In a set operation, the array is multidimensional or has a length that is not exactly 13.</exception>
    /// <exception cref="T:System.ArgumentNullException">In a set operation, the array or one of the elements of the array is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] AbbreviatedMonthGenitiveNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetGenitiveMonthNames(true).Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 13)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 13), "value");
        string[] values = value;
        int length = values.Length - 1;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.ClearTokenHashTable();
        this.m_genitiveAbbreviatedMonthNames = value;
      }
    }

    /// <summary>获取或设置与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的月份名称的字符串数组。</summary>
    /// <returns>月份名称的字符串数组。</returns>
    /// <exception cref="T:System.ArgumentException">In a set operation, the array is multidimensional or has a length that is not exactly 13.</exception>
    /// <exception cref="T:System.ArgumentNullException">In a set operation, the array or one of its elements is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] MonthGenitiveNames
    {
      [__DynamicallyInvokable] get
      {
        return (string[]) this.internalGetGenitiveMonthNames(false).Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
        if (value.Length != 13)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", (object) 13), "value");
        string[] values = value;
        int length = values.Length - 1;
        DateTimeFormatInfo.CheckNullValue(values, length);
        this.genitiveMonthNames = value;
        this.ClearTokenHashTable();
      }
    }

    internal string FullTimeSpanPositivePattern
    {
      get
      {
        if (this.m_fullTimeSpanPositivePattern == null)
          this.m_fullTimeSpanPositivePattern = "d':'h':'mm':'ss'" + new NumberFormatInfo(!this.m_cultureData.UseUserOverride ? this.m_cultureData : CultureData.GetCultureData(this.m_cultureData.CultureName, false)).NumberDecimalSeparator + "'FFFFFFF";
        return this.m_fullTimeSpanPositivePattern;
      }
    }

    internal string FullTimeSpanNegativePattern
    {
      get
      {
        if (this.m_fullTimeSpanNegativePattern == null)
          this.m_fullTimeSpanNegativePattern = "'-'" + this.FullTimeSpanPositivePattern;
        return this.m_fullTimeSpanNegativePattern;
      }
    }

    internal CompareInfo CompareInfo
    {
      get
      {
        if (this.m_compareInfo == null)
          this.m_compareInfo = CompareInfo.GetCompareInfo(this.m_cultureData.SCOMPAREINFO);
        return this.m_compareInfo;
      }
    }

    internal DateTimeFormatFlags FormatFlags
    {
      get
      {
        if (this.formatFlags == DateTimeFormatFlags.NotInitialized)
        {
          this.formatFlags = DateTimeFormatFlags.None;
          this.formatFlags = this.formatFlags | (DateTimeFormatFlags) DateTimeFormatInfoScanner.GetFormatFlagGenitiveMonth(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true));
          this.formatFlags = this.formatFlags | (DateTimeFormatFlags) DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInMonthNames(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true));
          this.formatFlags = this.formatFlags | (DateTimeFormatFlags) DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInDayNames(this.DayNames, this.AbbreviatedDayNames);
          this.formatFlags = this.formatFlags | (DateTimeFormatFlags) DateTimeFormatInfoScanner.GetFormatFlagUseHebrewCalendar(this.Calendar.ID);
        }
        return this.formatFlags;
      }
    }

    internal bool HasForceTwoDigitYears
    {
      get
      {
        switch (this.calendar.ID)
        {
          case 3:
          case 4:
            return true;
          default:
            return false;
        }
      }
    }

    internal bool HasYearMonthAdjustment
    {
      get
      {
        return (uint) (this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) > 0U;
      }
    }

    /// <summary>初始化不依赖于区域性的（固定的）<see cref="T:System.Globalization.DateTimeFormatInfo" /> 类的新可写实例。</summary>
    [__DynamicallyInvokable]
    public DateTimeFormatInfo()
      : this(CultureInfo.InvariantCulture.m_cultureData, GregorianCalendar.GetDefaultInstance())
    {
    }

    internal DateTimeFormatInfo(CultureData cultureData, Calendar cal)
    {
      this.m_cultureData = cultureData;
      this.Calendar = cal;
    }

    [SecuritySafeCritical]
    private static bool InitPreferExistingTokens()
    {
      return DateTime.LegacyParseMode();
    }

    private string[] internalGetAbbreviatedDayOfWeekNames()
    {
      if (this.abbreviatedDayNames == null)
        this.abbreviatedDayNames = this.m_cultureData.AbbreviatedDayNames(this.Calendar.ID);
      return this.abbreviatedDayNames;
    }

    private string[] internalGetSuperShortDayNames()
    {
      if (this.m_superShortDayNames == null)
        this.m_superShortDayNames = this.m_cultureData.SuperShortDayNames(this.Calendar.ID);
      return this.m_superShortDayNames;
    }

    private string[] internalGetDayOfWeekNames()
    {
      if (this.dayNames == null)
        this.dayNames = this.m_cultureData.DayNames(this.Calendar.ID);
      return this.dayNames;
    }

    private string[] internalGetAbbreviatedMonthNames()
    {
      if (this.abbreviatedMonthNames == null)
        this.abbreviatedMonthNames = this.m_cultureData.AbbreviatedMonthNames(this.Calendar.ID);
      return this.abbreviatedMonthNames;
    }

    private string[] internalGetMonthNames()
    {
      if (this.monthNames == null)
        this.monthNames = this.m_cultureData.MonthNames(this.Calendar.ID);
      return this.monthNames;
    }

    [SecuritySafeCritical]
    private void InitializeOverridableProperties(CultureData cultureData, int calendarID)
    {
      if (this.firstDayOfWeek == -1)
        this.firstDayOfWeek = cultureData.IFIRSTDAYOFWEEK;
      if (this.calendarWeekRule == -1)
        this.calendarWeekRule = cultureData.IFIRSTWEEKOFYEAR;
      if (this.amDesignator == null)
        this.amDesignator = cultureData.SAM1159;
      if (this.pmDesignator == null)
        this.pmDesignator = cultureData.SPM2359;
      if (this.timeSeparator == null)
        this.timeSeparator = cultureData.TimeSeparator;
      if (this.dateSeparator == null)
        this.dateSeparator = cultureData.DateSeparator(calendarID);
      this.allLongTimePatterns = this.m_cultureData.LongTimes;
      this.allShortTimePatterns = this.m_cultureData.ShortTimes;
      this.allLongDatePatterns = cultureData.LongDates(calendarID);
      this.allShortDatePatterns = cultureData.ShortDates(calendarID);
      this.allYearMonthPatterns = cultureData.YearMonths(calendarID);
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_name != null)
      {
        this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
        if (this.m_cultureData == null)
          throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
      }
      else
        this.m_cultureData = CultureData.GetCultureData(this.CultureID, this.m_useUserOverride);
      if (this.calendar == null)
      {
        this.calendar = (Calendar) GregorianCalendar.GetDefaultInstance().Clone();
        this.calendar.SetReadOnlyState(this.m_isReadOnly);
      }
      else
        CultureInfo.CheckDomainSafetyObject((object) this.calendar, (object) this);
      this.InitializeOverridableProperties(this.m_cultureData, this.calendar.ID);
      bool flag = this.m_isReadOnly;
      this.m_isReadOnly = false;
      if (this.longDatePattern != null)
        this.LongDatePattern = this.longDatePattern;
      if (this.shortDatePattern != null)
        this.ShortDatePattern = this.shortDatePattern;
      if (this.yearMonthPattern != null)
        this.YearMonthPattern = this.yearMonthPattern;
      if (this.longTimePattern != null)
        this.LongTimePattern = this.longTimePattern;
      if (this.shortTimePattern != null)
        this.ShortTimePattern = this.shortTimePattern;
      this.m_isReadOnly = flag;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.CultureID = this.m_cultureData.ILANGUAGE;
      this.m_useUserOverride = this.m_cultureData.UseUserOverride;
      this.m_name = this.CultureName;
      if (DateTimeFormatInfo.s_calendarNativeNames == null)
        DateTimeFormatInfo.s_calendarNativeNames = new Hashtable();
      string longTimePattern = this.LongTimePattern;
      string longDatePattern = this.LongDatePattern;
      string shortTimePattern = this.ShortTimePattern;
      string shortDatePattern = this.ShortDatePattern;
      string yearMonthPattern = this.YearMonthPattern;
      string[] longTimePatterns = this.AllLongTimePatterns;
      string[] longDatePatterns = this.AllLongDatePatterns;
      string[] shortTimePatterns = this.AllShortTimePatterns;
      string[] shortDatePatterns = this.AllShortDatePatterns;
      string[] yearMonthPatterns = this.AllYearMonthPatterns;
    }

    /// <summary>返回与指定的 <see cref="T:System.IFormatProvider" /> 关联的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象。</summary>
    /// <returns>一个与 <see cref="T:System.IFormatProvider" /> 关联的 <see cref="T:System.Globalization.DateTimeFormatInfo" />。</returns>
    /// <param name="provider">获取 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象的 <see cref="T:System.IFormatProvider" />。- 或 - 要获取 <see cref="P:System.Globalization.DateTimeFormatInfo.CurrentInfo" /> 的 null。</param>
    [__DynamicallyInvokable]
    public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
    {
      CultureInfo cultureInfo = provider as CultureInfo;
      if (cultureInfo != null && !cultureInfo.m_isInherited)
        return cultureInfo.DateTimeFormat;
      DateTimeFormatInfo dateTimeFormatInfo1 = provider as DateTimeFormatInfo;
      if (dateTimeFormatInfo1 != null)
        return dateTimeFormatInfo1;
      if (provider != null)
      {
        DateTimeFormatInfo dateTimeFormatInfo2 = provider.GetFormat(typeof (DateTimeFormatInfo)) as DateTimeFormatInfo;
        if (dateTimeFormatInfo2 != null)
          return dateTimeFormatInfo2;
      }
      return DateTimeFormatInfo.CurrentInfo;
    }

    /// <summary>返回指定类型的对象，它提供日期和时间格式化服务。</summary>
    /// <returns>如果 <paramref name="formatType" /> 与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 的类型相同，则为当前对象；否则为 null。</returns>
    /// <param name="formatType">所需格式化服务的类型。</param>
    [__DynamicallyInvokable]
    public object GetFormat(Type formatType)
    {
      if (!(formatType == typeof (DateTimeFormatInfo)))
        return (object) null;
      return (object) this;
    }

    /// <summary>创建 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 的浅表副本。</summary>
    /// <returns>从原始 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 复制的新 <see cref="T:System.Globalization.DateTimeFormatInfo" />。</returns>
    [__DynamicallyInvokable]
    public object Clone()
    {
      DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo) this.MemberwiseClone();
      Calendar calendar = (Calendar) this.Calendar.Clone();
      dateTimeFormatInfo.calendar = calendar;
      int num = 0;
      dateTimeFormatInfo.m_isReadOnly = num != 0;
      return (object) dateTimeFormatInfo;
    }

    /// <summary>返回表示指定纪元的整数。</summary>
    /// <returns>如果 <paramref name="eraName" /> 有效，则为表示纪元的整数；否则为 -1。</returns>
    /// <param name="eraName">包含纪元名称的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eraName" /> is null. </exception>
    [__DynamicallyInvokable]
    public int GetEra(string eraName)
    {
      if (eraName == null)
        throw new ArgumentNullException("eraName", Environment.GetResourceString("ArgumentNull_String"));
      if (eraName.Length == 0)
        return -1;
      for (int index = 0; index < this.EraNames.Length; ++index)
      {
        if (this.m_eraNames[index].Length > 0 && string.Compare(eraName, this.m_eraNames[index], this.Culture, CompareOptions.IgnoreCase) == 0)
          return index + 1;
      }
      for (int index = 0; index < this.AbbreviatedEraNames.Length; ++index)
      {
        if (string.Compare(eraName, this.m_abbrevEraNames[index], this.Culture, CompareOptions.IgnoreCase) == 0)
          return index + 1;
      }
      for (int index = 0; index < this.AbbreviatedEnglishEraNames.Length; ++index)
      {
        if (string.Compare(eraName, this.m_abbrevEnglishEraNames[index], StringComparison.InvariantCultureIgnoreCase) == 0)
          return index + 1;
      }
      return -1;
    }

    /// <summary>返回包含指定纪元名称的字符串。</summary>
    /// <returns>包含纪元名称的字符串。</returns>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
    [__DynamicallyInvokable]
    public string GetEraName(int era)
    {
      if (era == 0)
        era = this.Calendar.CurrentEraValue;
      if (--era < this.EraNames.Length && era >= 0)
        return this.m_eraNames[era];
      throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    /// <summary>返回包含指定纪元的缩写名称的字符串（如果缩写名称存在）。</summary>
    /// <returns>包含指定纪元的缩写名称的字符串（如果缩写名称存在）。- 或 -包含纪元的完整名称的字符串（如果缩写名称不存在）。</returns>
    /// <param name="era">表示纪元的整数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
    [__DynamicallyInvokable]
    public string GetAbbreviatedEraName(int era)
    {
      if (this.AbbreviatedEraNames.Length == 0)
        return this.GetEraName(era);
      if (era == 0)
        era = this.Calendar.CurrentEraValue;
      if (--era < this.m_abbrevEraNames.Length && era >= 0)
        return this.m_abbrevEraNames[era];
      throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
    }

    private static void CheckNullValue(string[] values, int length)
    {
      for (int index = 0; index < length; ++index)
      {
        if (values[index] == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_ArrayValue"));
      }
    }

    internal string internalGetMonthName(int month, MonthNameStyles style, bool abbreviated)
    {
      string[] strArray = style == MonthNameStyles.Genitive ? this.internalGetGenitiveMonthNames(abbreviated) : (style == MonthNameStyles.LeapYear ? this.internalGetLeapYearMonthNames() : (abbreviated ? this.internalGetAbbreviatedMonthNames() : this.internalGetMonthNames()));
      if (month < 1 || month > strArray.Length)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) strArray.Length));
      return strArray[month - 1];
    }

    private string[] internalGetGenitiveMonthNames(bool abbreviated)
    {
      if (abbreviated)
      {
        if (this.m_genitiveAbbreviatedMonthNames == null)
          this.m_genitiveAbbreviatedMonthNames = this.m_cultureData.AbbreviatedGenitiveMonthNames(this.Calendar.ID);
        return this.m_genitiveAbbreviatedMonthNames;
      }
      if (this.genitiveMonthNames == null)
        this.genitiveMonthNames = this.m_cultureData.GenitiveMonthNames(this.Calendar.ID);
      return this.genitiveMonthNames;
    }

    internal string[] internalGetLeapYearMonthNames()
    {
      if (this.leapYearMonthNames == null)
        this.leapYearMonthNames = this.m_cultureData.LeapYearMonthNames(this.Calendar.ID);
      return this.leapYearMonthNames;
    }

    /// <summary>基于与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的区域性，返回周中指定日期的区域性特定的缩写名称。</summary>
    /// <returns>由 <paramref name="dayofweek" /> 表示的周中日期的区域性特定的缩写名称。</returns>
    /// <param name="dayofweek">一个 <see cref="T:System.DayOfWeek" /> 值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
    [__DynamicallyInvokable]
    public string GetAbbreviatedDayName(DayOfWeek dayofweek)
    {
      if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
        throw new ArgumentOutOfRangeException("dayofweek", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) DayOfWeek.Sunday, (object) DayOfWeek.Saturday));
      return this.internalGetAbbreviatedDayOfWeekNames()[(int) dayofweek];
    }

    /// <summary>获取与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的周中指定日期的最短日期名缩写。</summary>
    /// <returns>对应于 <paramref name="dayOfWeek" /> 参数的周的缩写名称。</returns>
    /// <param name="dayOfWeek">
    /// <see cref="T:System.DayOfWeek" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="dayOfWeek" /> is not a value in the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
    [ComVisible(false)]
    public string GetShortestDayName(DayOfWeek dayOfWeek)
    {
      if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
        throw new ArgumentOutOfRangeException("dayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) DayOfWeek.Sunday, (object) DayOfWeek.Saturday));
      return this.internalGetSuperShortDayNames()[(int) dayOfWeek];
    }

    private static string[] GetCombinedPatterns(string[] patterns1, string[] patterns2, string connectString)
    {
      string[] strArray = new string[patterns1.Length * patterns2.Length];
      int num = 0;
      for (int index1 = 0; index1 < patterns1.Length; ++index1)
      {
        for (int index2 = 0; index2 < patterns2.Length; ++index2)
          strArray[num++] = patterns1[index1] + connectString + patterns2[index2];
      }
      return strArray;
    }

    /// <summary>返回可用于对日期和时间值进行格式设置的所有标准模式。</summary>
    /// <returns>一个数组，包含可以用于对日期和时间值进行格式设置的标准模式。</returns>
    public string[] GetAllDateTimePatterns()
    {
      List<string> stringList = new List<string>(132);
      for (int index = 0; index < DateTimeFormat.allStandardFormats.Length; ++index)
      {
        foreach (string allDateTimePattern in this.GetAllDateTimePatterns(DateTimeFormat.allStandardFormats[index]))
          stringList.Add(allDateTimePattern);
      }
      return stringList.ToArray();
    }

    /// <summary>返回可在其中使用指定标准格式字符串对日期和时间值进行格式设置的所有模式。</summary>
    /// <returns>一个数组，它包含可在其中使用指定格式字符串对日期和时间值进行格式设置的标准模式。</returns>
    /// <param name="format">标准格式字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="format" /> is not a valid standard format string. </exception>
    public string[] GetAllDateTimePatterns(char format)
    {
      if ((uint) format <= 85U)
      {
        switch (format)
        {
          case 'D':
            return this.AllLongDatePatterns;
          case 'F':
          case 'U':
            return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllLongTimePatterns, " ");
          case 'G':
            return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllLongTimePatterns, " ");
          case 'M':
            break;
          case 'O':
            goto label_10;
          case 'R':
            goto label_11;
          case 'T':
            return this.AllLongTimePatterns;
          default:
            goto label_17;
        }
      }
      else
      {
        switch (format)
        {
          case 'Y':
          case 'y':
            return this.AllYearMonthPatterns;
          case 'd':
            return this.AllShortDatePatterns;
          case 'f':
            return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllShortTimePatterns, " ");
          case 'g':
            return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllShortTimePatterns, " ");
          case 'm':
            break;
          case 'o':
            goto label_10;
          case 'r':
            goto label_11;
          case 's':
            return new string[1]{ "yyyy'-'MM'-'dd'T'HH':'mm':'ss" };
          case 't':
            return this.AllShortTimePatterns;
          case 'u':
            return new string[1]{ this.UniversalSortableDateTimePattern };
          default:
            goto label_17;
        }
      }
      return new string[1]{ this.MonthDayPattern };
label_10:
      return new string[1]{ "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK" };
label_11:
      return new string[1]{ "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'" };
label_17:
      throw new ArgumentException(Environment.GetResourceString("Format_BadFormatSpecifier"), "format");
    }

    /// <summary>基于与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的区域性，返回周中指定日期的区域性特定的完整名称。</summary>
    /// <returns>由 <paramref name="dayofweek" /> 表示的周中日期的区域性特定的完整名称。</returns>
    /// <param name="dayofweek">一个 <see cref="T:System.DayOfWeek" /> 值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
    [__DynamicallyInvokable]
    public string GetDayName(DayOfWeek dayofweek)
    {
      if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
        throw new ArgumentOutOfRangeException("dayofweek", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) DayOfWeek.Sunday, (object) DayOfWeek.Saturday));
      return this.internalGetDayOfWeekNames()[(int) dayofweek];
    }

    /// <summary>基于与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的区域性，返回指定月份的区域性特定的缩写名称。</summary>
    /// <returns>由 <paramref name="month" /> 表示的月份的区域性特定的缩写名称。</returns>
    /// <param name="month">一个从 1 到 13 的整数，表示要检索的月份的名称。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="month" /> is less than 1 or greater than 13. </exception>
    [__DynamicallyInvokable]
    public string GetAbbreviatedMonthName(int month)
    {
      if (month < 1 || month > 13)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 13));
      return this.internalGetAbbreviatedMonthNames()[month - 1];
    }

    /// <summary>基于与当前 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象关联的区域性，返回指定月份的区域性特定的完整名称。</summary>
    /// <returns>由 <paramref name="month" /> 表示的月份的区域性特定的完整名称。</returns>
    /// <param name="month">一个从 1 到 13 的整数，表示要检索的月份的名称。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="month" /> is less than 1 or greater than 13. </exception>
    [__DynamicallyInvokable]
    public string GetMonthName(int month)
    {
      if (month < 1 || month > 13)
        throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 1, (object) 13));
      return this.internalGetMonthNames()[month - 1];
    }

    private static string[] GetMergedPatterns(string[] patterns, string defaultPattern)
    {
      if (defaultPattern == patterns[0])
        return (string[]) patterns.Clone();
      int index = 0;
      while (index < patterns.Length && !(defaultPattern == patterns[index]))
        ++index;
      string[] strArray;
      if (index < patterns.Length)
      {
        strArray = (string[]) patterns.Clone();
        strArray[index] = strArray[0];
      }
      else
      {
        strArray = new string[patterns.Length + 1];
        Array.Copy((Array) patterns, 0, (Array) strArray, 1, patterns.Length);
      }
      strArray[0] = defaultPattern;
      return strArray;
    }

    /// <summary>返回只读的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 包装。</summary>
    /// <returns>一个只读的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 包装。</returns>
    /// <param name="dtfi">要包装的 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 对象。。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="dtfi" /> is null. </exception>
    [__DynamicallyInvokable]
    public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
    {
      if (dtfi == null)
        throw new ArgumentNullException("dtfi", Environment.GetResourceString("ArgumentNull_Obj"));
      if (dtfi.IsReadOnly)
        return dtfi;
      DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo) dtfi.MemberwiseClone();
      Calendar calendar = Calendar.ReadOnly(dtfi.Calendar);
      dateTimeFormatInfo.calendar = calendar;
      int num = 1;
      dateTimeFormatInfo.m_isReadOnly = num != 0;
      return dateTimeFormatInfo;
    }

    /// <summary>设置对应于指定的标准格式字符串的自定义日期和时间格式字符串。</summary>
    /// <param name="patterns">自定义格式字符串的数组。</param>
    /// <param name="format">在 <paramref name="patterns" /> 参数中指定的与自定义格式字符串关联的标准格式字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="patterns" /> is null or a zero-length array.-or-<paramref name="format" /> is not a valid standard format string or is a standard format string whose patterns cannot be set.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="patterns" /> has an array element whose value is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
    [ComVisible(false)]
    public void SetAllDateTimePatterns(string[] patterns, char format)
    {
      if (this.IsReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
      if (patterns == null)
        throw new ArgumentNullException("patterns", Environment.GetResourceString("ArgumentNull_Array"));
      if (patterns.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayZeroError"), "patterns");
      for (int index = 0; index < patterns.Length; ++index)
      {
        if (patterns[index] == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayValue"));
      }
      if ((uint) format <= 89U)
      {
        if ((int) format != 68)
        {
          if ((int) format != 84)
          {
            if ((int) format != 89)
              goto label_23;
          }
          else
          {
            this.allLongTimePatterns = patterns;
            this.longTimePattern = this.allLongTimePatterns[0];
            goto label_24;
          }
        }
        else
        {
          this.allLongDatePatterns = patterns;
          this.longDatePattern = this.allLongDatePatterns[0];
          goto label_24;
        }
      }
      else if ((int) format != 100)
      {
        if ((int) format != 116)
        {
          if ((int) format != 121)
            goto label_23;
        }
        else
        {
          this.allShortTimePatterns = patterns;
          this.shortTimePattern = this.allShortTimePatterns[0];
          goto label_24;
        }
      }
      else
      {
        this.allShortDatePatterns = patterns;
        this.shortDatePattern = this.allShortDatePatterns[0];
        goto label_24;
      }
      this.allYearMonthPatterns = patterns;
      this.yearMonthPattern = this.allYearMonthPatterns[0];
      goto label_24;
label_23:
      throw new ArgumentException(Environment.GetResourceString("Format_BadFormatSpecifier"), "format");
label_24:
      this.ClearTokenHashTable();
    }

    internal static void ValidateStyles(DateTimeStyles style, string parameterName)
    {
      if ((style & ~(DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeStyles"), parameterName);
      if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeStyles"), parameterName);
      if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (style & (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal)) != DateTimeStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeRoundtripStyles"), parameterName);
    }

    internal bool YearMonthAdjustment(ref int year, ref int month, bool parsedMonthName)
    {
      if ((this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
      {
        if (year < 1000)
          year += 5000;
        if (year < this.Calendar.GetYear(this.Calendar.MinSupportedDateTime) || year > this.Calendar.GetYear(this.Calendar.MaxSupportedDateTime))
          return false;
        if (parsedMonthName && !this.Calendar.IsLeapYear(year))
        {
          if (month >= 8)
            --month;
          else if (month == 7)
            return false;
        }
      }
      return true;
    }

    internal static DateTimeFormatInfo GetJapaneseCalendarDTFI()
    {
      DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.s_jajpDTFI;
      if (dateTimeFormatInfo == null)
      {
        dateTimeFormatInfo = new CultureInfo("ja-JP", false).DateTimeFormat;
        dateTimeFormatInfo.Calendar = JapaneseCalendar.GetDefaultInstance();
        DateTimeFormatInfo.s_jajpDTFI = dateTimeFormatInfo;
      }
      return dateTimeFormatInfo;
    }

    internal static DateTimeFormatInfo GetTaiwanCalendarDTFI()
    {
      DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.s_zhtwDTFI;
      if (dateTimeFormatInfo == null)
      {
        dateTimeFormatInfo = new CultureInfo("zh-TW", false).DateTimeFormat;
        dateTimeFormatInfo.Calendar = TaiwanCalendar.GetDefaultInstance();
        DateTimeFormatInfo.s_zhtwDTFI = dateTimeFormatInfo;
      }
      return dateTimeFormatInfo;
    }

    private void ClearTokenHashTable()
    {
      this.m_dtfiTokenHash = (TokenHashValue[]) null;
      this.formatFlags = DateTimeFormatFlags.NotInitialized;
    }

    [SecurityCritical]
    internal TokenHashValue[] CreateTokenHashTable()
    {
      TokenHashValue[] tokenHashValueArray = this.m_dtfiTokenHash;
      if (tokenHashValueArray == null)
      {
        tokenHashValueArray = new TokenHashValue[199];
        int num1 = this.LanguageName.Equals("ko") ? 1 : 0;
        string str1 = this.TimeSeparator.Trim();
        if ("," != str1)
          this.InsertHash(tokenHashValueArray, ",", TokenType.IgnorableSymbol, 0);
        if ("." != str1)
          this.InsertHash(tokenHashValueArray, ".", TokenType.IgnorableSymbol, 0);
        if ("시" != str1 && "時" != str1 && "时" != str1)
          this.InsertHash(tokenHashValueArray, this.TimeSeparator, TokenType.SEP_Time, 0);
        this.InsertHash(tokenHashValueArray, this.AMDesignator, TokenType.Am | TokenType.SEP_Am, 0);
        this.InsertHash(tokenHashValueArray, this.PMDesignator, TokenType.SEP_Pm | TokenType.Pm, 1);
        if (this.LanguageName.Equals("sq"))
        {
          this.InsertHash(tokenHashValueArray, "." + this.AMDesignator, TokenType.Am | TokenType.SEP_Am, 0);
          this.InsertHash(tokenHashValueArray, "." + this.PMDesignator, TokenType.SEP_Pm | TokenType.Pm, 1);
        }
        this.InsertHash(tokenHashValueArray, "年", TokenType.SEP_YearSuff, 0);
        this.InsertHash(tokenHashValueArray, "년", TokenType.SEP_YearSuff, 0);
        this.InsertHash(tokenHashValueArray, "月", TokenType.SEP_MonthSuff, 0);
        this.InsertHash(tokenHashValueArray, "월", TokenType.SEP_MonthSuff, 0);
        this.InsertHash(tokenHashValueArray, "日", TokenType.SEP_DaySuff, 0);
        this.InsertHash(tokenHashValueArray, "일", TokenType.SEP_DaySuff, 0);
        this.InsertHash(tokenHashValueArray, "時", TokenType.SEP_HourSuff, 0);
        this.InsertHash(tokenHashValueArray, "时", TokenType.SEP_HourSuff, 0);
        this.InsertHash(tokenHashValueArray, "分", TokenType.SEP_MinuteSuff, 0);
        this.InsertHash(tokenHashValueArray, "秒", TokenType.SEP_SecondSuff, 0);
        if (num1 != 0)
        {
          this.InsertHash(tokenHashValueArray, "시", TokenType.SEP_HourSuff, 0);
          this.InsertHash(tokenHashValueArray, "분", TokenType.SEP_MinuteSuff, 0);
          this.InsertHash(tokenHashValueArray, "초", TokenType.SEP_SecondSuff, 0);
        }
        if (this.LanguageName.Equals("ky"))
          this.InsertHash(tokenHashValueArray, "-", TokenType.IgnorableSymbol, 0);
        else
          this.InsertHash(tokenHashValueArray, "-", TokenType.SEP_DateOrOffset, 0);
        string[] dateWordsOfDtfi;
        this.m_dateWords = dateWordsOfDtfi = new DateTimeFormatInfoScanner().GetDateWordsOfDTFI(this);
        int num2 = (int) this.FormatFlags;
        bool flag = false;
        if (dateWordsOfDtfi != null)
        {
          for (int index = 0; index < dateWordsOfDtfi.Length; ++index)
          {
            switch (dateWordsOfDtfi[index][0])
            {
              case '\xE000':
                string monthPostfix = dateWordsOfDtfi[index].Substring(1);
                this.AddMonthNames(tokenHashValueArray, monthPostfix);
                break;
              case '\xE001':
                string str2 = dateWordsOfDtfi[index].Substring(1);
                this.InsertHash(tokenHashValueArray, str2, TokenType.IgnorableSymbol, 0);
                if (this.DateSeparator.Trim((char[]) null).Equals(str2))
                {
                  flag = true;
                  break;
                }
                break;
              default:
                this.InsertHash(tokenHashValueArray, dateWordsOfDtfi[index], TokenType.DateWordToken, 0);
                if (this.LanguageName.Equals("eu"))
                {
                  this.InsertHash(tokenHashValueArray, "." + dateWordsOfDtfi[index], TokenType.DateWordToken, 0);
                  break;
                }
                break;
            }
          }
        }
        if (!flag)
          this.InsertHash(tokenHashValueArray, this.DateSeparator, TokenType.SEP_Date, 0);
        this.AddMonthNames(tokenHashValueArray, (string) null);
        for (int index = 1; index <= 13; ++index)
          this.InsertHash(tokenHashValueArray, this.GetAbbreviatedMonthName(index), TokenType.MonthToken, index);
        if ((this.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
        {
          for (int index = 1; index <= 13; ++index)
          {
            string monthName = this.internalGetMonthName(index, MonthNameStyles.Genitive, false);
            this.InsertHash(tokenHashValueArray, monthName, TokenType.MonthToken, index);
          }
        }
        if ((this.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
        {
          for (int index = 1; index <= 13; ++index)
          {
            string monthName = this.internalGetMonthName(index, MonthNameStyles.LeapYear, false);
            this.InsertHash(tokenHashValueArray, monthName, TokenType.MonthToken, index);
          }
        }
        for (int tokenValue = 0; tokenValue < 7; ++tokenValue)
        {
          string dayName = this.GetDayName((DayOfWeek) tokenValue);
          this.InsertHash(tokenHashValueArray, dayName, TokenType.DayOfWeekToken, tokenValue);
          string abbreviatedDayName = this.GetAbbreviatedDayName((DayOfWeek) tokenValue);
          this.InsertHash(tokenHashValueArray, abbreviatedDayName, TokenType.DayOfWeekToken, tokenValue);
        }
        int[] eras = this.calendar.Eras;
        for (int index = 1; index <= eras.Length; ++index)
        {
          this.InsertHash(tokenHashValueArray, this.GetEraName(index), TokenType.EraToken, index);
          this.InsertHash(tokenHashValueArray, this.GetAbbreviatedEraName(index), TokenType.EraToken, index);
        }
        if (this.LanguageName.Equals("ja"))
        {
          for (int tokenValue = 0; tokenValue < 7; ++tokenValue)
          {
            string str2 = "(" + this.GetAbbreviatedDayName((DayOfWeek) tokenValue) + ")";
            this.InsertHash(tokenHashValueArray, str2, TokenType.DayOfWeekToken, tokenValue);
          }
          if (this.Calendar.GetType() != typeof (JapaneseCalendar))
          {
            DateTimeFormatInfo japaneseCalendarDtfi = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
            for (int index = 1; index <= japaneseCalendarDtfi.Calendar.Eras.Length; ++index)
            {
              this.InsertHash(tokenHashValueArray, japaneseCalendarDtfi.GetEraName(index), TokenType.JapaneseEraToken, index);
              this.InsertHash(tokenHashValueArray, japaneseCalendarDtfi.GetAbbreviatedEraName(index), TokenType.JapaneseEraToken, index);
              this.InsertHash(tokenHashValueArray, japaneseCalendarDtfi.AbbreviatedEnglishEraNames[index - 1], TokenType.JapaneseEraToken, index);
            }
          }
        }
        else if (this.CultureName.Equals("zh-TW"))
        {
          DateTimeFormatInfo taiwanCalendarDtfi = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
          for (int index = 1; index <= taiwanCalendarDtfi.Calendar.Eras.Length; ++index)
          {
            if (taiwanCalendarDtfi.GetEraName(index).Length > 0)
              this.InsertHash(tokenHashValueArray, taiwanCalendarDtfi.GetEraName(index), TokenType.TEraToken, index);
          }
        }
        this.InsertHash(tokenHashValueArray, DateTimeFormatInfo.InvariantInfo.AMDesignator, TokenType.Am | TokenType.SEP_Am, 0);
        this.InsertHash(tokenHashValueArray, DateTimeFormatInfo.InvariantInfo.PMDesignator, TokenType.SEP_Pm | TokenType.Pm, 1);
        for (int index = 1; index <= 12; ++index)
        {
          string monthName = DateTimeFormatInfo.InvariantInfo.GetMonthName(index);
          this.InsertHash(tokenHashValueArray, monthName, TokenType.MonthToken, index);
          string abbreviatedMonthName = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(index);
          this.InsertHash(tokenHashValueArray, abbreviatedMonthName, TokenType.MonthToken, index);
        }
        for (int tokenValue = 0; tokenValue < 7; ++tokenValue)
        {
          string dayName = DateTimeFormatInfo.InvariantInfo.GetDayName((DayOfWeek) tokenValue);
          this.InsertHash(tokenHashValueArray, dayName, TokenType.DayOfWeekToken, tokenValue);
          string abbreviatedDayName = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek) tokenValue);
          this.InsertHash(tokenHashValueArray, abbreviatedDayName, TokenType.DayOfWeekToken, tokenValue);
        }
        for (int index = 0; index < this.AbbreviatedEnglishEraNames.Length; ++index)
          this.InsertHash(tokenHashValueArray, this.AbbreviatedEnglishEraNames[index], TokenType.EraToken, index + 1);
        this.InsertHash(tokenHashValueArray, "T", TokenType.SEP_LocalTimeMark, 0);
        this.InsertHash(tokenHashValueArray, "GMT", TokenType.TimeZoneToken, 0);
        this.InsertHash(tokenHashValueArray, "Z", TokenType.TimeZoneToken, 0);
        this.InsertHash(tokenHashValueArray, "/", TokenType.SEP_Date, 0);
        this.InsertHash(tokenHashValueArray, ":", TokenType.SEP_Time, 0);
        this.m_dtfiTokenHash = tokenHashValueArray;
      }
      return tokenHashValueArray;
    }

    private void AddMonthNames(TokenHashValue[] temp, string monthPostfix)
    {
      for (int index = 1; index <= 13; ++index)
      {
        string monthName = this.GetMonthName(index);
        if (monthName.Length > 0)
        {
          if (monthPostfix != null)
            this.InsertHash(temp, monthName + monthPostfix, TokenType.MonthToken, index);
          else
            this.InsertHash(temp, monthName, TokenType.MonthToken, index);
        }
        string abbreviatedMonthName = this.GetAbbreviatedMonthName(index);
        this.InsertHash(temp, abbreviatedMonthName, TokenType.MonthToken, index);
      }
    }

    private static bool TryParseHebrewNumber(ref __DTString str, out bool badFormat, out int number)
    {
      number = -1;
      badFormat = false;
      int index = str.Index;
      if (!HebrewNumber.IsDigit(str.Value[index]))
        return false;
      HebrewNumberParsingContext context = new HebrewNumberParsingContext(0);
      HebrewNumberParsingState byChar;
      do
      {
        byChar = HebrewNumber.ParseByChar(str.Value[index++], ref context);
        if (byChar == HebrewNumberParsingState.InvalidHebrewNumber || byChar == HebrewNumberParsingState.NotHebrewDigit)
          return false;
      }
      while (index < str.Value.Length && byChar != HebrewNumberParsingState.FoundEndOfHebrewNumber);
      if (byChar != HebrewNumberParsingState.FoundEndOfHebrewNumber)
        return false;
      str.Advance(index - str.Index);
      number = context.result;
      return true;
    }

    private static bool IsHebrewChar(char ch)
    {
      if ((int) ch >= 1424)
        return (int) ch <= 1535;
      return false;
    }

    [SecurityCritical]
    internal bool Tokenize(TokenType TokenMask, out TokenType tokenType, out int tokenValue, ref __DTString str)
    {
      tokenType = TokenType.UnknownToken;
      tokenValue = 0;
      char ch = str.m_current;
      bool flag = char.IsLetter(ch);
      if (flag)
      {
        ch = char.ToLower(ch, this.Culture);
        bool badFormat;
        if (DateTimeFormatInfo.IsHebrewChar(ch) && TokenMask == TokenType.RegularTokenMask && DateTimeFormatInfo.TryParseHebrewNumber(ref str, out badFormat, out tokenValue))
        {
          if (badFormat)
          {
            tokenType = TokenType.UnknownToken;
            return false;
          }
          tokenType = TokenType.HebrewNumber;
          return true;
        }
      }
      int index1 = (int) ch % 199;
      int num1 = 1 + (int) ch % 197;
      int num2 = str.len - str.Index;
      int num3 = 0;
      TokenHashValue[] tokenHashValueArray = this.m_dtfiTokenHash ?? this.CreateTokenHashTable();
      do
      {
        TokenHashValue tokenHashValue = tokenHashValueArray[index1];
        if (tokenHashValue != null)
        {
          if ((tokenHashValue.tokenType & TokenMask) > (TokenType) 0 && tokenHashValue.tokenString.Length <= num2)
          {
            if (string.Compare(str.Value, str.Index, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length, this.Culture, CompareOptions.IgnoreCase) == 0)
            {
              int index2;
              if (flag && (index2 = str.Index + tokenHashValue.tokenString.Length) < str.len && char.IsLetter(str.Value[index2]))
                return false;
              tokenType = tokenHashValue.tokenType & TokenMask;
              tokenValue = tokenHashValue.tokenValue;
              str.Advance(tokenHashValue.tokenString.Length);
              return true;
            }
            if (tokenHashValue.tokenType == TokenType.MonthToken && this.HasSpacesInMonthNames)
            {
              int matchLength = 0;
              if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref matchLength))
              {
                tokenType = tokenHashValue.tokenType & TokenMask;
                tokenValue = tokenHashValue.tokenValue;
                str.Advance(matchLength);
                return true;
              }
            }
            else if (tokenHashValue.tokenType == TokenType.DayOfWeekToken && this.HasSpacesInDayNames)
            {
              int matchLength = 0;
              if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref matchLength))
              {
                tokenType = tokenHashValue.tokenType & TokenMask;
                tokenValue = tokenHashValue.tokenValue;
                str.Advance(matchLength);
                return true;
              }
            }
          }
          ++num3;
          index1 += num1;
          if (index1 >= 199)
            index1 -= 199;
        }
        else
          break;
      }
      while (num3 < 199);
      return false;
    }

    private void InsertAtCurrentHashNode(TokenHashValue[] hashTable, string str, char ch, TokenType tokenType, int tokenValue, int pos, int hashcode, int hashProbe)
    {
      TokenHashValue tokenHashValue1 = hashTable[hashcode];
      hashTable[hashcode] = new TokenHashValue(str, tokenType, tokenValue);
      while (++pos < 199)
      {
        hashcode += hashProbe;
        if (hashcode >= 199)
          hashcode -= 199;
        TokenHashValue tokenHashValue2 = hashTable[hashcode];
        if (tokenHashValue2 == null || (int) char.ToLower(tokenHashValue2.tokenString[0], this.Culture) == (int) ch)
        {
          hashTable[hashcode] = tokenHashValue1;
          if (tokenHashValue2 == null)
            break;
          tokenHashValue1 = tokenHashValue2;
        }
      }
    }

    private void InsertHash(TokenHashValue[] hashTable, string str, TokenType tokenType, int tokenValue)
    {
      if (str == null || str.Length == 0)
        return;
      int pos = 0;
      if (!char.IsWhiteSpace(str[0]))
      {
        string str1 = str;
        int index = str1.Length - 1;
        if (!char.IsWhiteSpace(str1[index]))
          goto label_6;
      }
      str = str.Trim((char[]) null);
      if (str.Length == 0)
        return;
label_6:
      char lower = char.ToLower(str[0], this.Culture);
      int hashcode = (int) lower % 199;
      int hashProbe = 1 + (int) lower % 197;
      do
      {
        TokenHashValue tokenHashValue = hashTable[hashcode];
        if (tokenHashValue == null)
        {
          hashTable[hashcode] = new TokenHashValue(str, tokenType, tokenValue);
          break;
        }
        if (str.Length >= tokenHashValue.tokenString.Length && string.Compare(str, 0, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length, this.Culture, CompareOptions.IgnoreCase) == 0)
        {
          if (str.Length > tokenHashValue.tokenString.Length)
          {
            this.InsertAtCurrentHashNode(hashTable, str, lower, tokenType, tokenValue, pos, hashcode, hashProbe);
            break;
          }
          int num1 = (int) tokenType;
          int num2 = (int) tokenHashValue.tokenType;
          if (DateTimeFormatInfo.preferExistingTokens || BinaryCompatibility.TargetsAtLeast_Desktop_V4_5_1)
          {
            if ((num2 & (int) byte.MaxValue) == 0 && (num1 & (int) byte.MaxValue) != 0 || (num2 & 65280) == 0 && (num1 & 65280) != 0)
            {
              tokenHashValue.tokenType |= tokenType;
              if (tokenValue != 0)
                tokenHashValue.tokenValue = tokenValue;
            }
          }
          else if (((num1 | num2) & (int) byte.MaxValue) == num1 || ((num1 | num2) & 65280) == num1)
          {
            tokenHashValue.tokenType |= tokenType;
            if (tokenValue != 0)
              tokenHashValue.tokenValue = tokenValue;
          }
        }
        ++pos;
        hashcode += hashProbe;
        if (hashcode >= 199)
          hashcode -= 199;
      }
      while (pos < 199);
    }
  }
}
