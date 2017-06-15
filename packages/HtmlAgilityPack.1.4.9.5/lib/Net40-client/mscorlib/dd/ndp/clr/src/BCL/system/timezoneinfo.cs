// Decompiled with JetBrains decompiler
// Type: System.TimeZoneInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System
{
  /// <summary>表示世界上的任何时区。</summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
  {
    private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();
    private static DateTime s_maxDateOnly = new DateTime(9999, 12, 31);
    private static DateTime s_minDateOnly = new DateTime(1, 1, 2);
    private string m_id;
    private string m_displayName;
    private string m_standardDisplayName;
    private string m_daylightDisplayName;
    private TimeSpan m_baseUtcOffset;
    private bool m_supportsDaylightSavingTime;
    private TimeZoneInfo.AdjustmentRule[] m_adjustmentRules;
    private const string c_timeZonesRegistryHive = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";
    private const string c_timeZonesRegistryHivePermissionList = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";
    private const string c_displayValue = "Display";
    private const string c_daylightValue = "Dlt";
    private const string c_standardValue = "Std";
    private const string c_muiDisplayValue = "MUI_Display";
    private const string c_muiDaylightValue = "MUI_Dlt";
    private const string c_muiStandardValue = "MUI_Std";
    private const string c_timeZoneInfoValue = "TZI";
    private const string c_firstEntryValue = "FirstEntry";
    private const string c_lastEntryValue = "LastEntry";
    private const string c_utcId = "UTC";
    private const string c_localId = "Local";
    private const int c_maxKeyLength = 255;
    private const int c_regByteLength = 44;
    private const long c_ticksPerMillisecond = 10000;
    private const long c_ticksPerSecond = 10000000;
    private const long c_ticksPerMinute = 600000000;
    private const long c_ticksPerHour = 36000000000;
    private const long c_ticksPerDay = 864000000000;
    private const long c_ticksPerDayRange = 863999990000;

    /// <summary>获取时区标识符。</summary>
    /// <returns>时区标识符。</returns>
    [__DynamicallyInvokable]
    public string Id
    {
      [__DynamicallyInvokable] get
      {
        return this.m_id;
      }
    }

    /// <summary>获取表示时区的一般显示名称。</summary>
    /// <returns>时区的一般显示名称。</returns>
    [__DynamicallyInvokable]
    public string DisplayName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_displayName != null)
          return this.m_displayName;
        return string.Empty;
      }
    }

    /// <summary>获取时区的标准时间的显示名称。</summary>
    /// <returns>时区的标准时间的显示名称。</returns>
    [__DynamicallyInvokable]
    public string StandardName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_standardDisplayName != null)
          return this.m_standardDisplayName;
        return string.Empty;
      }
    }

    /// <summary>获取当前时区的夏令时的显示名称。</summary>
    /// <returns>时区的夏令时的显示名称。</returns>
    [__DynamicallyInvokable]
    public string DaylightName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_daylightDisplayName != null)
          return this.m_daylightDisplayName;
        return string.Empty;
      }
    }

    /// <summary>获取当前时区的标准时间与协调世界时 (UTC) 之间的时差。</summary>
    /// <returns>一个对象，它指示当前时区的标准时间与协调世界时 (UTC) 之间的时差。</returns>
    [__DynamicallyInvokable]
    public TimeSpan BaseUtcOffset
    {
      [__DynamicallyInvokable] get
      {
        return this.m_baseUtcOffset;
      }
    }

    /// <summary>获取一个值，该值指示时区是否具有任何夏令制规则。</summary>
    /// <returns>如果时区支持夏令制，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool SupportsDaylightSavingTime
    {
      [__DynamicallyInvokable] get
      {
        return this.m_supportsDaylightSavingTime;
      }
    }

    /// <summary>获取表示本地时区的 <see cref="T:System.TimeZoneInfo" /> 对象。</summary>
    /// <returns>一个对象，表示本地时区。</returns>
    [__DynamicallyInvokable]
    public static TimeZoneInfo Local
    {
      [__DynamicallyInvokable] get
      {
        return TimeZoneInfo.s_cachedData.Local;
      }
    }

    /// <summary>获取表示协调世界时 (UTC) 区域的 <see cref="T:System.TimeZoneInfo" /> 对象。</summary>
    /// <returns>一个对象，表示协调世界时 (UTC) 区域。</returns>
    [__DynamicallyInvokable]
    public static TimeZoneInfo Utc
    {
      [__DynamicallyInvokable] get
      {
        return TimeZoneInfo.s_cachedData.Utc;
      }
    }

    [SecurityCritical]
    private TimeZoneInfo(Win32Native.TimeZoneInformation zone, bool dstDisabled)
    {
      this.m_id = !string.IsNullOrEmpty(zone.StandardName) ? zone.StandardName : "Local";
      this.m_baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
      if (!dstDisabled)
      {
        TimeZoneInfo.AdjustmentRule timeZoneInformation = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(new Win32Native.RegistryTimeZoneInformation(zone), DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
        if (timeZoneInformation != null)
        {
          this.m_adjustmentRules = new TimeZoneInfo.AdjustmentRule[1];
          this.m_adjustmentRules[0] = timeZoneInformation;
        }
      }
      TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out this.m_supportsDaylightSavingTime);
      this.m_displayName = zone.StandardName;
      this.m_standardDisplayName = zone.StandardName;
      this.m_daylightDisplayName = zone.DaylightName;
    }

    private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
    {
      bool adjustmentRulesSupportDst;
      TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out adjustmentRulesSupportDst);
      if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
        this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[]) adjustmentRules.Clone();
      this.m_id = id;
      this.m_baseUtcOffset = baseUtcOffset;
      this.m_displayName = displayName;
      this.m_standardDisplayName = standardDisplayName;
      this.m_daylightDisplayName = disableDaylightSavingTime ? (string) null : daylightDisplayName;
      this.m_supportsDaylightSavingTime = adjustmentRulesSupportDst && !disableDaylightSavingTime;
    }

    private TimeZoneInfo(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.m_id = (string) info.GetValue("Id", typeof (string));
      this.m_displayName = (string) info.GetValue("DisplayName", typeof (string));
      this.m_standardDisplayName = (string) info.GetValue("StandardName", typeof (string));
      this.m_daylightDisplayName = (string) info.GetValue("DaylightName", typeof (string));
      this.m_baseUtcOffset = (TimeSpan) info.GetValue("BaseUtcOffset", typeof (TimeSpan));
      this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[]) info.GetValue("AdjustmentRules", typeof (TimeZoneInfo.AdjustmentRule[]));
      this.m_supportsDaylightSavingTime = (bool) info.GetValue("SupportsDaylightSavingTime", typeof (bool));
    }

    /// <summary>检索应用至当前 <see cref="T:System.TimeZoneInfo" /> 对象的 <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> 对象的数组。</summary>
    /// <returns>此时区的对象的数组。</returns>
    /// <exception cref="T:System.OutOfMemoryException">系统没有足够的内存来进行调整规则的内存中副本。</exception>
    /// <filterpriority>2</filterpriority>
    public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
    {
      if (this.m_adjustmentRules == null)
        return new TimeZoneInfo.AdjustmentRule[0];
      return (TimeZoneInfo.AdjustmentRule[]) this.m_adjustmentRules.Clone();
    }

    /// <summary>返回不明确的日期和时间可能映射到的日期和时间的相关信息。</summary>
    /// <returns>对象的数组，它表示特定日期和时间可以映射到的可能的协调世界时 (UTC) 偏移量。</returns>
    /// <param name="dateTimeOffset">日期和时间。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dateTimeOffset" /> 不是不明确的时间。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
    {
      if (!this.SupportsDaylightSavingTime)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), "dateTimeOffset");
      DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
      bool flag = false;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
      if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
      {
        DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
        flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForTime, daylightTime);
      }
      if (!flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), "dateTimeOffset");
      TimeSpan[] timeSpanArray = new TimeSpan[2];
      TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
      if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
      {
        timeSpanArray[0] = timeSpan;
        timeSpanArray[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
      }
      else
      {
        timeSpanArray[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
        timeSpanArray[1] = timeSpan;
      }
      return timeSpanArray;
    }

    /// <summary>返回不明确的日期和时间可能映射到的日期和时间的相关信息。</summary>
    /// <returns>对象的数组，它表示特定日期和时间可以映射到的可能的协调世界时 (UTC) 偏移量。</returns>
    /// <param name="dateTime">日期和时间。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dateTime" /> 不是不明确的时间。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
    {
      if (!this.SupportsDaylightSavingTime)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), "dateTime");
      DateTime dateTime1;
      if (dateTime.Kind == DateTimeKind.Local)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
      }
      else if (dateTime.Kind == DateTimeKind.Utc)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, this, TimeZoneInfoOptions.None, cachedData);
      }
      else
        dateTime1 = dateTime;
      bool flag = false;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime1);
      if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
      {
        DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime);
        flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime1, adjustmentRuleForTime, daylightTime);
      }
      if (!flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), "dateTime");
      TimeSpan[] timeSpanArray = new TimeSpan[2];
      TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
      if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
      {
        timeSpanArray[0] = timeSpan;
        timeSpanArray[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
      }
      else
      {
        timeSpanArray[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
        timeSpanArray[1] = timeSpan;
      }
      return timeSpanArray;
    }

    /// <summary>计算此时区中的时间与协调世界时 (UTC) 之间针对特定日期和时间的偏移量或差值。</summary>
    /// <returns>一个对象，该对象指示协调世界时 (UTC) 与当前时区之间的时差。</returns>
    /// <param name="dateTimeOffset">要为其确定偏移量的日期和时间。</param>
    [__DynamicallyInvokable]
    public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
    {
      return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
    }

    /// <summary>计算此时区中的时间与协调世界时 (UTC) 之间针对特定日期和时间的偏移量或差值。</summary>
    /// <returns>一个对象，该对象指示两个时区之间的时差。</returns>
    /// <param name="dateTime">要为其确定偏移量的日期和时间。  </param>
    [__DynamicallyInvokable]
    public TimeSpan GetUtcOffset(DateTime dateTime)
    {
      return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
    }

    internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
    }

    internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
    }

    private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
    {
      if (dateTime.Kind == DateTimeKind.Local)
      {
        if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
          return TimeZoneInfo.GetUtcOffsetFromUtc(TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags), this);
      }
      else if (dateTime.Kind == DateTimeKind.Utc)
      {
        if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
          return this.m_baseUtcOffset;
        return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
      }
      return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
    }

    /// <summary>确定特定时区中的特定日期和时间是否不明确以及是否可以映射至两个或多个协调世界时 (UTC) 时间。</summary>
    /// <returns>如果 <paramref name="dateTimeOffset" /> 参数在当前时区中不明确，则为 true；否则为 false。</returns>
    /// <param name="dateTimeOffset">日期和时间。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
    {
      if (!this.m_supportsDaylightSavingTime)
        return false;
      return this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
    }

    /// <summary>确定特定时区中的特定日期和时间是否不明确以及是否可以映射至两个或多个协调世界时 (UTC) 时间。</summary>
    /// <returns>如果 <paramref name="dateTime" /> 参数不明确，则为 true；否则为 false。</returns>
    /// <param name="dateTime">日期和时间值。  </param>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.DateTime.Kind" /> 属性 <paramref name="dateTime" /> 值是 <see cref="F:System.DateTimeKind.Local" /> 和 <paramref name="dateTime" /> 是无效的时间。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsAmbiguousTime(DateTime dateTime)
    {
      return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
    }

    internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      if (!this.m_supportsDaylightSavingTime)
        return false;
      DateTime dateTime1;
      if (dateTime.Kind == DateTimeKind.Local)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
      }
      else if (dateTime.Kind == DateTimeKind.Utc)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, this, flags, cachedData);
      }
      else
        dateTime1 = dateTime;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime1);
      if (adjustmentRuleForTime == null || !adjustmentRuleForTime.HasDaylightSaving)
        return false;
      DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime);
      return TimeZoneInfo.GetIsAmbiguousTime(dateTime1, adjustmentRuleForTime, daylightTime);
    }

    /// <summary>指示指定的日期和时间是否处于当前 <see cref="T:System.TimeZoneInfo" /> 对象时区的夏令制范围内。</summary>
    /// <returns>如果 <paramref name="dateTimeOffset" /> 参数为夏令制，则为 true；否则为 false。</returns>
    /// <param name="dateTimeOffset">日期和时间值。</param>
    [__DynamicallyInvokable]
    public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
    {
      bool isDaylightSavings;
      TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out isDaylightSavings);
      return isDaylightSavings;
    }

    /// <summary>指示指定的日期和时间是否处于当前 <see cref="T:System.TimeZoneInfo" /> 对象时区的夏令制范围内。</summary>
    /// <returns>如果 <paramref name="dateTime" /> 参数为夏令制，则为 true；否则为 false。</returns>
    /// <param name="dateTime">日期和时间值。  </param>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.DateTime.Kind" /> 属性 <paramref name="dateTime" /> 值是 <see cref="F:System.DateTimeKind.Local" /> 和 <paramref name="dateTime" /> 是无效的时间。</exception>
    [__DynamicallyInvokable]
    public bool IsDaylightSavingTime(DateTime dateTime)
    {
      return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
    }

    internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
    }

    private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
    {
      if (!this.m_supportsDaylightSavingTime || this.m_adjustmentRules == null)
        return false;
      DateTime dateTime1;
      if (dateTime.Kind == DateTimeKind.Local)
      {
        dateTime1 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
      }
      else
      {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
          if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
            return false;
          bool isDaylightSavings;
          TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out isDaylightSavings);
          return isDaylightSavings;
        }
        dateTime1 = dateTime;
      }
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime1);
      if (adjustmentRuleForTime == null || !adjustmentRuleForTime.HasDaylightSaving)
        return false;
      DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime);
      return TimeZoneInfo.GetIsDaylightSavings(dateTime1, adjustmentRuleForTime, daylightTime, flags);
    }

    /// <summary>指示特定日期和时间是否无效。</summary>
    /// <returns>如果 <paramref name="dateTime" /> 无效，则为 true；否则为 false。</returns>
    /// <param name="dateTime">日期和时间值。  </param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsInvalidTime(DateTime dateTime)
    {
      bool flag = false;
      if (dateTime.Kind == DateTimeKind.Unspecified || dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local)
      {
        TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
        if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
        {
          DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
          flag = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
        }
        else
          flag = false;
      }
      return flag;
    }

    /// <summary>清除已缓存的时区数据。</summary>
    /// <filterpriority>2</filterpriority>
    public static void ClearCachedData()
    {
      TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
    }

    /// <summary>根据时区标识符将时间转换为另一时区中的时间。</summary>
    /// <returns>目标时区的日期和时间。</returns>
    /// <param name="dateTimeOffset">要转换的日期和时间。</param>
    /// <param name="destinationTimeZoneId">目标时区的标识符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationTimeZoneId" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidTimeZoneException">找到了时区标识符，但注册表数据已损坏。</exception>
    /// <exception cref="T:System.Security.SecurityException">进程不具有从包含时区信息的注册表项中读取所需的权限。</exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    /// <paramref name="destinationTimeZoneId" /> 在本地系统上找不到标识符。</exception>
    public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
    {
      return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
    }

    /// <summary>根据时区标识符将时间转换为另一时区中的时间。</summary>
    /// <returns>目标时区的日期和时间。</returns>
    /// <param name="dateTime">要转换的日期和时间。</param>
    /// <param name="destinationTimeZoneId">目标时区的标识符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationTimeZoneId" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidTimeZoneException">找到时区标识符，但注册表数据已损坏。</exception>
    /// <exception cref="T:System.Security.SecurityException">进程不具有从包含时区信息的注册表项中读取所需的权限。</exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    /// <paramref name="destinationTimeZoneId" /> 在本地系统上找不到标识符。</exception>
    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
    {
      return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
    }

    /// <summary>根据时区标识符将时间从一个时区转换到另一个时区。</summary>
    /// <returns>目标时区中与源时区中的 <paramref name="dateTime" /> 参数对应的日期和时间。</returns>
    /// <param name="dateTime">要转换的日期和时间。</param>
    /// <param name="sourceTimeZoneId">源时区的标识符。</param>
    /// <param name="destinationTimeZoneId">目标时区的标识符。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.DateTime.Kind" /> 属性 <paramref name="dateTime" /> 参数不对应于源时区。- 或 -<paramref name="dateTime" /> 是源时区中的无效时间。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceTimeZoneId" /> 为 null。- 或 -<paramref name="destinationTimeZoneId" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidTimeZoneException">时区标识符未找到，但注册表数据已损坏。</exception>
    /// <exception cref="T:System.Security.SecurityException">进程不具有从包含时区信息的注册表项中读取所需的权限。</exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">
    /// <paramref name="sourceTimeZoneId" /> 在本地系统上找不到标识符。- 或 -<paramref name="destinationTimeZoneId" /> 在本地系统上找不到标识符。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有从包含时区数据的注册表项中读取所需的权限。</exception>
    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
    {
      if (dateTime.Kind == DateTimeKind.Local && string.Compare(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase) == 0)
      {
        TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
        return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
      }
      if (dateTime.Kind != DateTimeKind.Utc || string.Compare(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase) != 0)
        return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
      TimeZoneInfo.CachedData cachedData1 = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData1.Utc, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData1);
    }

    /// <summary>将时间转换为特定时区的时间。</summary>
    /// <returns>目标时区的日期和时间。</returns>
    /// <param name="dateTimeOffset">要转换的日期和时间。  </param>
    /// <param name="destinationTimeZone">要将 <paramref name="dateTime" /> 转换到的时区。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationTimeZone" /> 参数的值为 null。</exception>
    [__DynamicallyInvokable]
    public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
    {
      if (destinationTimeZone == null)
        throw new ArgumentNullException("destinationTimeZone");
      DateTime utcDateTime = dateTimeOffset.UtcDateTime;
      TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
      long ticks = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
      if (ticks > DateTimeOffset.MaxValue.Ticks)
        return DateTimeOffset.MaxValue;
      if (ticks < DateTimeOffset.MinValue.Ticks)
        return DateTimeOffset.MinValue;
      return new DateTimeOffset(ticks, utcOffsetFromUtc);
    }

    /// <summary>将时间转换为特定时区的时间。</summary>
    /// <returns>目标时区的日期和时间。</returns>
    /// <param name="dateTime">要转换的日期和时间。  </param>
    /// <param name="destinationTimeZone">要将 <paramref name="dateTime" /> 转换到的时区。</param>
    /// <exception cref="T:System.ArgumentException">值 <paramref name="dateTime" /> 参数代表时间无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationTimeZone" /> 参数的值为 null。</exception>
    [__DynamicallyInvokable]
    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      if (destinationTimeZone == null)
        throw new ArgumentNullException("destinationTimeZone");
      if (dateTime.Ticks == 0L)
        TimeZoneInfo.ClearCachedData();
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      if (dateTime.Kind == DateTimeKind.Utc)
        return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
    }

    /// <summary>将时间从一个时区转换到另一个时区。</summary>
    /// <returns>目标时区中与源时区中的 <paramref name="dateTime" /> 参数对应的日期和时间。</returns>
    /// <param name="dateTime">要转换的日期和时间。</param>
    /// <param name="sourceTimeZone">
    /// <paramref name="dateTime" /> 的时区。</param>
    /// <param name="destinationTimeZone">要将 <paramref name="dateTime" /> 转换到的时区。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.DateTime.Kind" /> 属性 <paramref name="dateTime" /> 参数是 <see cref="F:System.DateTimeKind.Local" />, ，但 <paramref name="sourceTimeZone" /> 参数不等于 <see cref="F:System.DateTimeKind.Local" />。有关详细信息，请参阅“备注”部分。- 或 -<see cref="P:System.DateTime.Kind" /> 属性 <paramref name="dateTime" /> 参数是 <see cref="F:System.DateTimeKind.Utc" />, ，但 <paramref name="sourceTimeZone" /> 参数不等于 <see cref="P:System.TimeZoneInfo.Utc" />。- 或 -<paramref name="dateTime" /> 参数是一个无效的时间 （即，它表示时区调整规则由于不存在的时间）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceTimeZone" /> 参数为 null。- 或 -<paramref name="destinationTimeZone" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
    {
      return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
    }

    internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
    {
      return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
    }

    private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
    {
      if (sourceTimeZone == null)
        throw new ArgumentNullException("sourceTimeZone");
      if (destinationTimeZone == null)
        throw new ArgumentNullException("destinationTimeZone");
      DateTimeKind correspondingKind1 = cachedData.GetCorrespondingKind(sourceTimeZone);
      if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions) 0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind1)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConvertMismatch"), "sourceTimeZone");
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime);
      TimeSpan baseUtcOffset = sourceTimeZone.BaseUtcOffset;
      if (adjustmentRuleForTime != null)
      {
        baseUtcOffset += adjustmentRuleForTime.BaseUtcOffsetDelta;
        if (adjustmentRuleForTime.HasDaylightSaving)
        {
          DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
          if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions) 0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
            throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsInvalid"), "dateTime");
          bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
          baseUtcOffset += isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero;
        }
      }
      DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
      if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind1 != DateTimeKind.Unspecified && correspondingKind1 == correspondingKind2)
        return dateTime;
      long ticks = dateTime.Ticks - baseUtcOffset.Ticks;
      bool isAmbiguousDst = false;
      TimeZoneInfo destinationTimeZone1 = destinationTimeZone;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      bool& isAmbiguousLocalDst = @isAmbiguousDst;
      DateTime timeZone = TimeZoneInfo.ConvertUtcToTimeZone(ticks, destinationTimeZone1, isAmbiguousLocalDst);
      if (correspondingKind2 == DateTimeKind.Local)
        return new DateTime(timeZone.Ticks, DateTimeKind.Local, isAmbiguousDst);
      return new DateTime(timeZone.Ticks, correspondingKind2);
    }

    /// <summary>将协调世界时 (UTC) 转换为指定时区中的时间。</summary>
    /// <returns>目标时区的日期和时间。如果 <paramref name="destinationTimeZone" /> 为 <see cref="P:System.TimeZoneInfo.Utc" />，则其 <see cref="P:System.DateTime.Kind" /> 属性为 <see cref="F:System.DateTimeKind.Utc" />；否则其 <see cref="P:System.DateTime.Kind" /> 属性为 <see cref="F:System.DateTimeKind.Unspecified" />。</returns>
    /// <param name="dateTime">协调世界时 (UTC)。</param>
    /// <param name="destinationTimeZone">要将 <paramref name="dateTime" /> 转换到的时区。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.DateTime.Kind" /> 属性 <paramref name="dateTime" /> 是 <see cref="F:System.DateTimeKind.Local" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationTimeZone" /> 为 null。</exception>
    public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
    }

    /// <summary>将指定的日期和时间转换为协调世界时 (UTC)。</summary>
    /// <returns>与 <paramref name="dateTime" /> 参数对应的协调世界时 (UTC)。<see cref="T:System.DateTime" /> 值的 <see cref="P:System.DateTime.Kind" /> 属性始终设置为 <see cref="F:System.DateTimeKind.Utc" />。</returns>
    /// <param name="dateTime">要转换的日期和时间。</param>
    /// <exception cref="T:System.ArgumentException">TimeZoneInfo.Local.IsInvalidDateTime(<paramref name="dateTime" />) 返回 true。</exception>
    public static DateTime ConvertTimeToUtc(DateTime dateTime)
    {
      if (dateTime.Kind == DateTimeKind.Utc)
        return dateTime;
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
    }

    internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
    {
      if (dateTime.Kind == DateTimeKind.Utc)
        return dateTime;
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags, cachedData);
    }

    /// <summary>将指定时区中的时间转换为协调世界时 (UTC)。</summary>
    /// <returns>与 <paramref name="dateTime" /> 参数对应的协调世界时 (UTC)。<see cref="T:System.DateTime" /> 对象的 <see cref="P:System.DateTime.Kind" /> 属性始终设置为 <see cref="F:System.DateTimeKind.Utc" />。</returns>
    /// <param name="dateTime">要转换的日期和时间。</param>
    /// <param name="sourceTimeZone">
    /// <paramref name="dateTime" /> 的时区。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dateTime" />.Kind 是 <see cref="F:System.DateTimeKind.Utc" /> 和 <paramref name="sourceTimeZone" /> 不等于 <see cref="P:System.TimeZoneInfo.Utc" />。- 或 -<paramref name="dateTime" />.Kind 是 <see cref="F:System.DateTimeKind.Local" /> 和 <paramref name="sourceTimeZone" /> 不等于 <see cref="P:System.TimeZoneInfo.Local" />。- 或 -<paramref name="sourceTimeZone" />.IsInvalidDateTime(<paramref name="dateTime" />) 返回 true。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceTimeZone" /> 为 null。</exception>
    public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
    }

    /// <summary>确定当前的 <see cref="T:System.TimeZoneInfo" /> 对象和另一个 <see cref="T:System.TimeZoneInfo" /> 对象是否相等。</summary>
    /// <returns>如果两个 <see cref="T:System.TimeZoneInfo" /> 对象相等，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前对象进行比较的第二个对象。 </param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(TimeZoneInfo other)
    {
      if (other != null && string.Compare(this.m_id, other.m_id, StringComparison.OrdinalIgnoreCase) == 0)
        return this.HasSameRules(other);
      return false;
    }

    /// <summary>确定当前的 <see cref="T:System.TimeZoneInfo" /> 对象和另一个对象是否相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 是等于当前实例的 <see cref="T:System.TimeZoneInfo" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前对象进行比较的第二个对象。 </param>
    public override bool Equals(object obj)
    {
      TimeZoneInfo other = obj as TimeZoneInfo;
      if (other == null)
        return false;
      return this.Equals(other);
    }

    /// <summary>反序列化一个字符串，以重新创建原始的已序列化的 <see cref="T:System.TimeZoneInfo" /> 对象。</summary>
    /// <returns>原始序列化对象。</returns>
    /// <param name="source">已序列化的 <see cref="T:System.TimeZoneInfo" /> 对象的字符串表示形式。  </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="source" /> 参数为 <see cref="F:System.String.Empty" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 参数是一个空字符串。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">源参数不能反序列化回 <see cref="T:System.TimeZoneInfo" /> 对象。</exception>
    public static TimeZoneInfo FromSerializedString(string source)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (source.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSerializedString", (object) source), "source");
      return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
    }

    /// <summary>用作哈希算法的哈希函数和数据结构（如哈希表）。</summary>
    /// <returns>一个 32 位有符号整数，用作此 <see cref="T:System.TimeZoneInfo" /> 对象的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_id.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
    }

    /// <summary>返回时区信息在本地系统上可用的所有时区的已排序集合。</summary>
    /// <returns>
    /// <see cref="T:System.TimeZoneInfo" /> 对象的只读集合。</returns>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的内存来存储所有的时区信息。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有权限读取包含时区信息的注册表项。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
    {
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      lock (cachedData)
      {
        if (cachedData.m_readOnlySystemTimeZones == null)
        {
          PermissionSet temp_10 = new PermissionSet(PermissionState.None);
          RegistryPermission temp_13 = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");
          temp_10.AddPermission((IPermission) temp_13);
          temp_10.Assert();
          using (RegistryKey resource_0 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
          {
            if (resource_0 != null)
            {
              foreach (string item_0 in resource_0.GetSubKeyNames())
              {
                TimeZoneInfo local_7;
                Exception local_8;
                int temp_53 = (int) TimeZoneInfo.TryGetTimeZone(item_0, false, out local_7, out local_8, cachedData);
              }
            }
            cachedData.m_allSystemTimeZonesRead = true;
          }
          List<TimeZoneInfo> local_3 = cachedData.m_systemTimeZones == null ? new List<TimeZoneInfo>() : new List<TimeZoneInfo>((IEnumerable<TimeZoneInfo>) cachedData.m_systemTimeZones.Values);
          local_3.Sort((IComparer<TimeZoneInfo>) new TimeZoneInfo.TimeZoneInfoComparer());
          cachedData.m_readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>((IList<TimeZoneInfo>) local_3);
        }
      }
      return cachedData.m_readOnlySystemTimeZones;
    }

    /// <summary>指示当前对象和另一个 <see cref="T:System.TimeZoneInfo" /> 对象是否具有相同的调整规则。</summary>
    /// <returns>如果两个时区具有相同的调整规则和相同的基本偏移量，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前的 <see cref="T:System.TimeZoneInfo" /> 对象进行比较的第二个对象。  </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="other" /> 参数为 null。</exception>
    public bool HasSameRules(TimeZoneInfo other)
    {
      if (other == null)
        throw new ArgumentNullException("other");
      if (this.m_baseUtcOffset != other.m_baseUtcOffset || this.m_supportsDaylightSavingTime != other.m_supportsDaylightSavingTime)
        return false;
      TimeZoneInfo.AdjustmentRule[] adjustmentRuleArray1 = this.m_adjustmentRules;
      TimeZoneInfo.AdjustmentRule[] adjustmentRuleArray2 = other.m_adjustmentRules;
      bool flag = adjustmentRuleArray1 == null && adjustmentRuleArray2 == null || adjustmentRuleArray1 != null && adjustmentRuleArray2 != null;
      if (!flag)
        return false;
      if (adjustmentRuleArray1 != null)
      {
        if (adjustmentRuleArray1.Length != adjustmentRuleArray2.Length)
          return false;
        for (int index = 0; index < adjustmentRuleArray1.Length; ++index)
        {
          if (!adjustmentRuleArray1[index].Equals(adjustmentRuleArray2[index]))
            return false;
        }
      }
      return flag;
    }

    /// <summary>将当前的 <see cref="T:System.TimeZoneInfo" /> 对象转换为序列化字符串。</summary>
    /// <returns>表示当前 <see cref="T:System.TimeZoneInfo" /> 对象的字符串。</returns>
    public string ToSerializedString()
    {
      return TimeZoneInfo.StringSerializer.GetSerializedString(this);
    }

    /// <summary>返回当前 <see cref="T:System.TimeZoneInfo" /> 对象的显示名称。</summary>
    /// <returns>当前 <see cref="T:System.TimeZoneInfo" /> 对象的 <see cref="P:System.TimeZoneInfo.DisplayName" /> 属性值。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.DisplayName;
    }

    /// <summary>创建带指定标识符的自定义时区、与协调世界时 (UTC) 的偏移量、显示名称以及标准时间显示名称。</summary>
    /// <returns>新时区。</returns>
    /// <param name="id">时区的标识符。</param>
    /// <param name="baseUtcOffset">一个对象，它表示此时区和协调世界时 (UTC) 之间的时差。</param>
    /// <param name="displayName">新时区的显示名称。  </param>
    /// <param name="standardDisplayName">新时区的标准时间的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="id" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="id" /> 参数为空字符串 ("")。- 或 -<paramref name="baseUtcOffset" /> 参数不表示整分钟数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="baseUtcOffset" /> 参数是否大于 14 小时或小于-14 小时。</exception>
    /// <filterpriority>2</filterpriority>
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
    {
      string id1 = id;
      TimeSpan baseUtcOffset1 = baseUtcOffset;
      string displayName1 = displayName;
      string str = standardDisplayName;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      return new TimeZoneInfo(id1, baseUtcOffset1, displayName1, str, str, (TimeZoneInfo.AdjustmentRule[]) local, num != 0);
    }

    /// <summary>创建带指定标识符的自定义时区、与协调世界时 (UTC) 的偏移量、显示名称、标准时间名称、夏时制名称和夏时制规则。</summary>
    /// <returns>一个表示新时区的 <see cref="T:System.TimeZoneInfo" /> 对象。</returns>
    /// <param name="id">时区的标识符。</param>
    /// <param name="baseUtcOffset">一个对象，它表示此时区和协调世界时 (UTC) 之间的时差。</param>
    /// <param name="displayName">新时区的显示名称。  </param>
    /// <param name="standardDisplayName">新时区的标准时间名称。</param>
    /// <param name="daylightDisplayName">新时区的夏令制名称。  </param>
    /// <param name="adjustmentRules">一个数组，它将基本 UTC 偏移量增加了特定的期间。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="id" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="id" /> 参数为空字符串 ("")。- 或 -<paramref name="baseUtcOffset" /> 参数不表示整分钟数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="baseUtcOffset" /> 参数是否大于 14 小时或小于-14 小时。</exception>
    /// <exception cref="T:System.InvalidTimeZoneException">调整规则中指定 <paramref name="adjustmentRules" /> 参数重叠。- 或 -调整规则中指定 <paramref name="adjustmentRules" /> 参数不是按时间顺序。- 或 -中的一个或多个元素 <paramref name="adjustmentRules" /> 是 null。- 或 -日期可以有多个调整规则应用于它。- 或 -总和 <paramref name="baseUtcOffset" /> 参数和 <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> 值中的一个或多个对象的 <paramref name="adjustmentRules" /> 数组大于 14 小时或小于-14 小时。</exception>
    /// <filterpriority>2</filterpriority>
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
    {
      return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
    }

    /// <summary>创建带指定标识符的自定义时区、与协调世界时 (UTC) 的偏移量、显示名称、标准时间名称、夏令制名称、夏令制规则以及指示返回的对象是否反映夏令制信息的值。</summary>
    /// <returns>新时区。如果 <paramref name="disableDaylightSavingTime" /> 参数为 true，则返回的对象没有夏令制数据。</returns>
    /// <param name="id">时区的标识符。</param>
    /// <param name="baseUtcOffset">一个 <see cref="T:System.TimeSpan" /> 对象，它表示此时区和协调世界时 (UTC) 之间的时差。</param>
    /// <param name="displayName">新时区的显示名称。  </param>
    /// <param name="standardDisplayName">新时区的标准时间名称。</param>
    /// <param name="daylightDisplayName">新时区的夏令制名称。  </param>
    /// <param name="adjustmentRules">一个 <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> 对象的数组，这些对象增加特定期间的基本 UTC 偏移量。</param>
    /// <param name="disableDaylightSavingTime">如果为 true ，则丢弃包含新对象的 <paramref name="adjustmentRules" /> 中与夏令制相关的任何信息；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="id" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="id" /> 参数为空字符串 ("")。- 或 -<paramref name="baseUtcOffset" /> 参数不表示整分钟数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="baseUtcOffset" /> 参数是否大于 14 小时或小于-14 小时。</exception>
    /// <exception cref="T:System.InvalidTimeZoneException">调整规则中指定 <paramref name="adjustmentRules" /> 参数重叠。- 或 -调整规则中指定 <paramref name="adjustmentRules" /> 参数不是按时间顺序。- 或 -中的一个或多个元素 <paramref name="adjustmentRules" /> 是 null。- 或 -日期可以有多个调整规则应用于它。- 或 -总和 <paramref name="baseUtcOffset" /> 参数和 <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> 值中的一个或多个对象的 <paramref name="adjustmentRules" /> 数组大于 14 小时或小于-14 小时。</exception>
    /// <filterpriority>2</filterpriority>
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
    {
      return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        bool adjustmentRulesSupportDst;
        TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out adjustmentRulesSupportDst);
        if (adjustmentRulesSupportDst != this.m_supportsDaylightSavingTime)
          throw new SerializationException(Environment.GetResourceString("Serialization_CorruptField", (object) "SupportsDaylightSavingTime"));
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
      }
      catch (InvalidTimeZoneException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
      }
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("Id", (object) this.m_id);
      info.AddValue("DisplayName", (object) this.m_displayName);
      info.AddValue("StandardName", (object) this.m_standardDisplayName);
      info.AddValue("DaylightName", (object) this.m_daylightDisplayName);
      info.AddValue("BaseUtcOffset", (object) this.m_baseUtcOffset);
      info.AddValue("AdjustmentRules", (object) this.m_adjustmentRules);
      info.AddValue("SupportsDaylightSavingTime", this.m_supportsDaylightSavingTime);
    }

    private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime)
    {
      if (this.m_adjustmentRules == null || this.m_adjustmentRules.Length == 0)
        return (TimeZoneInfo.AdjustmentRule) null;
      DateTime date = dateTime.Date;
      for (int index = 0; index < this.m_adjustmentRules.Length; ++index)
      {
        if (this.m_adjustmentRules[index].DateStart <= date && this.m_adjustmentRules[index].DateEnd >= date)
          return this.m_adjustmentRules[index];
      }
      return (TimeZoneInfo.AdjustmentRule) null;
    }

    [SecurityCritical]
    private static bool CheckDaylightSavingTimeNotSupported(Win32Native.TimeZoneInformation timeZone)
    {
      if ((int) timeZone.DaylightDate.Year == (int) timeZone.StandardDate.Year && (int) timeZone.DaylightDate.Month == (int) timeZone.StandardDate.Month && ((int) timeZone.DaylightDate.DayOfWeek == (int) timeZone.StandardDate.DayOfWeek && (int) timeZone.DaylightDate.Day == (int) timeZone.StandardDate.Day) && ((int) timeZone.DaylightDate.Hour == (int) timeZone.StandardDate.Hour && (int) timeZone.DaylightDate.Minute == (int) timeZone.StandardDate.Minute && (int) timeZone.DaylightDate.Second == (int) timeZone.StandardDate.Second))
        return (int) timeZone.DaylightDate.Milliseconds == (int) timeZone.StandardDate.Milliseconds;
      return false;
    }

    private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
    {
      TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(ticks <= DateTime.MaxValue.Ticks ? (ticks >= DateTime.MinValue.Ticks ? new DateTime(ticks) : DateTime.MinValue) : DateTime.MaxValue, destinationTimeZone, out isAmbiguousLocalDst);
      ticks += utcOffsetFromUtc.Ticks;
      return ticks <= DateTime.MaxValue.Ticks ? (ticks >= DateTime.MinValue.Ticks ? new DateTime(ticks) : DateTime.MinValue) : DateTime.MaxValue;
    }

    [SecurityCritical]
    private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
    {
      if ((uint) timeZoneInformation.StandardDate.Month <= 0U)
      {
        if (timeZoneInformation.Bias == defaultBaseUtcOffset)
          return (TimeZoneInfo.AdjustmentRule) null;
        return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
      }
      TimeZoneInfo.TransitionTime transitionTime1;
      if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime1, true))
        return (TimeZoneInfo.AdjustmentRule) null;
      TimeZoneInfo.TransitionTime transitionTime2;
      if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime2, false))
        return (TimeZoneInfo.AdjustmentRule) null;
      if (transitionTime1.Equals(transitionTime2))
        return (TimeZoneInfo.AdjustmentRule) null;
      return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), transitionTime1, transitionTime2, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
    }

    [SecuritySafeCritical]
    private static string FindIdFromTimeZoneInformation(Win32Native.TimeZoneInformation timeZone, out bool dstDisabled)
    {
      dstDisabled = false;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");
        permissionSet.AddPermission((IPermission) registryPermission);
        permissionSet.Assert();
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (registryKey == null)
            return (string) null;
          foreach (string subKeyName in registryKey.GetSubKeyNames())
          {
            if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, subKeyName, out dstDisabled))
              return subKeyName;
          }
        }
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
      return (string) null;
    }

    private static DaylightTime GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule)
    {
      TimeSpan daylightDelta = rule.DaylightDelta;
      return new DaylightTime(TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart), TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd), daylightDelta);
    }

    private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTime daylightTime, TimeZoneInfoOptions flags)
    {
      if (rule == null)
        return false;
      DateTime startTime;
      DateTime endTime;
      if (time.Kind == DateTimeKind.Local)
      {
        DateTime dateTime1;
        DateTime dateTime2;
        if (!rule.IsStartDateMarkerForBeginningOfYear())
        {
          dateTime2 = daylightTime.Start + daylightTime.Delta;
        }
        else
        {
          dateTime1 = daylightTime.Start;
          dateTime2 = new DateTime(dateTime1.Year, 1, 1, 0, 0, 0);
        }
        startTime = dateTime2;
        DateTime dateTime3;
        if (!rule.IsEndDateMarkerForEndOfYear())
        {
          dateTime3 = daylightTime.End;
        }
        else
        {
          dateTime1 = daylightTime.End;
          dateTime1 = new DateTime(dateTime1.Year + 1, 1, 1, 0, 0, 0);
          dateTime3 = dateTime1.AddTicks(-1L);
        }
        endTime = dateTime3;
      }
      else
      {
        bool flag = rule.DaylightDelta > TimeSpan.Zero;
        DateTime dateTime1;
        DateTime dateTime2;
        if (!rule.IsStartDateMarkerForBeginningOfYear())
        {
          dateTime2 = daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero);
        }
        else
        {
          dateTime1 = daylightTime.Start;
          dateTime2 = new DateTime(dateTime1.Year, 1, 1, 0, 0, 0);
        }
        startTime = dateTime2;
        DateTime dateTime3;
        if (!rule.IsEndDateMarkerForEndOfYear())
        {
          dateTime3 = daylightTime.End + (flag ? -rule.DaylightDelta : TimeSpan.Zero);
        }
        else
        {
          dateTime1 = daylightTime.End;
          dateTime1 = new DateTime(dateTime1.Year + 1, 1, 1, 0, 0, 0);
          dateTime3 = dateTime1.AddTicks(-1L);
        }
        endTime = dateTime3;
      }
      bool flag1 = TimeZoneInfo.CheckIsDst(startTime, time, endTime, false);
      if (flag1 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
        flag1 = time.IsAmbiguousDaylightSavingTime();
      return flag1;
    }

    private static bool GetIsDaylightSavingsFromUtc(DateTime time, int Year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
    {
      isAmbiguousLocalDst = false;
      if (rule == null)
        return false;
      TimeSpan timeSpan = utc + rule.BaseUtcOffsetDelta;
      DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(Year, rule);
      bool ignoreYearAdjustment = false;
      DateTime dateTime1;
      DateTime startTime;
      if (rule.IsStartDateMarkerForBeginningOfYear())
      {
        int year1 = daylightTime.Start.Year;
        dateTime1 = DateTime.MinValue;
        int year2 = dateTime1.Year;
        if (year1 > year2)
        {
          TimeZoneInfo timeZoneInfo = zone;
          dateTime1 = daylightTime.Start;
          DateTime dateTime2 = new DateTime(dateTime1.Year - 1, 12, 31);
          TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = timeZoneInfo.GetAdjustmentRuleForTime(dateTime2);
          if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
          {
            dateTime1 = daylightTime.Start;
            startTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year - 1, adjustmentRuleForTime).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
            ignoreYearAdjustment = true;
            goto label_8;
          }
          else
          {
            dateTime1 = daylightTime.Start;
            startTime = new DateTime(dateTime1.Year, 1, 1, 0, 0, 0) - timeSpan;
            goto label_8;
          }
        }
      }
      startTime = daylightTime.Start - timeSpan;
label_8:
      DateTime endTime;
      if (rule.IsEndDateMarkerForEndOfYear())
      {
        dateTime1 = daylightTime.End;
        int year1 = dateTime1.Year;
        dateTime1 = DateTime.MaxValue;
        int year2 = dateTime1.Year;
        if (year1 < year2)
        {
          TimeZoneInfo timeZoneInfo = zone;
          dateTime1 = daylightTime.End;
          DateTime dateTime2 = new DateTime(dateTime1.Year + 1, 1, 1);
          TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = timeZoneInfo.GetAdjustmentRuleForTime(dateTime2);
          if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsStartDateMarkerForBeginningOfYear())
          {
            if (adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
            {
              dateTime1 = daylightTime.End;
              endTime = new DateTime(dateTime1.Year + 1, 12, 31) - utc - adjustmentRuleForTime.BaseUtcOffsetDelta - adjustmentRuleForTime.DaylightDelta;
            }
            else
            {
              dateTime1 = daylightTime.End;
              endTime = TimeZoneInfo.GetDaylightTime(dateTime1.Year + 1, adjustmentRuleForTime).End - utc - adjustmentRuleForTime.BaseUtcOffsetDelta - adjustmentRuleForTime.DaylightDelta;
            }
            ignoreYearAdjustment = true;
            goto label_17;
          }
          else
          {
            dateTime1 = daylightTime.End;
            dateTime1 = new DateTime(dateTime1.Year + 1, 1, 1, 0, 0, 0);
            endTime = dateTime1.AddTicks(-1L) - timeSpan - rule.DaylightDelta;
            goto label_17;
          }
        }
      }
      endTime = daylightTime.End - timeSpan - rule.DaylightDelta;
label_17:
      DateTime dateTime3;
      DateTime dateTime4;
      if (daylightTime.Delta.Ticks > 0L)
      {
        dateTime3 = endTime - daylightTime.Delta;
        dateTime4 = endTime;
      }
      else
      {
        dateTime3 = startTime;
        dateTime4 = startTime - daylightTime.Delta;
      }
      bool flag = TimeZoneInfo.CheckIsDst(startTime, time, endTime, ignoreYearAdjustment);
      if (flag)
      {
        isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
        if (!isAmbiguousLocalDst)
        {
          if (dateTime3.Year != dateTime4.Year)
          {
            try
            {
              dateTime3.AddYears(1);
              dateTime4.AddYears(1);
              isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
            if (!isAmbiguousLocalDst)
            {
              try
              {
                dateTime3.AddYears(-1);
                dateTime4.AddYears(-1);
                isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
              }
              catch (ArgumentOutOfRangeException ex)
              {
              }
            }
          }
        }
      }
      return flag;
    }

    private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment)
    {
      if (!ignoreYearAdjustment)
      {
        int year1 = startTime.Year;
        int year2 = endTime.Year;
        if (year1 != year2)
          endTime = endTime.AddYears(year1 - year2);
        int year3 = time.Year;
        if (year1 != year3)
          time = time.AddYears(year1 - year3);
      }
      return !(startTime > endTime) ? time >= startTime && time < endTime : time < endTime || time >= startTime;
    }

    private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTime daylightTime)
    {
      bool flag1 = false;
      if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
        return flag1;
      DateTime dateTime1;
      DateTime dateTime2;
      if (rule.DaylightDelta > TimeSpan.Zero)
      {
        if (rule.IsEndDateMarkerForEndOfYear())
          return false;
        dateTime1 = daylightTime.End;
        dateTime2 = daylightTime.End - rule.DaylightDelta;
      }
      else
      {
        if (rule.IsStartDateMarkerForBeginningOfYear())
          return false;
        dateTime1 = daylightTime.Start;
        dateTime2 = daylightTime.Start + rule.DaylightDelta;
      }
      bool flag2 = time >= dateTime2 && time < dateTime1;
      if (!flag2)
      {
        if (dateTime1.Year != dateTime2.Year)
        {
          try
          {
            DateTime dateTime3 = dateTime1.AddYears(1);
            DateTime dateTime4 = dateTime2.AddYears(1);
            flag2 = time >= dateTime4 && time < dateTime3;
          }
          catch (ArgumentOutOfRangeException ex)
          {
          }
          if (!flag2)
          {
            try
            {
              DateTime dateTime3 = dateTime1.AddYears(-1);
              DateTime dateTime4 = dateTime2.AddYears(-1);
              flag2 = time >= dateTime4 && time < dateTime3;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
        }
      }
      return flag2;
    }

    private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTime daylightTime)
    {
      bool flag1 = false;
      if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
        return flag1;
      DateTime dateTime1;
      DateTime dateTime2;
      if (rule.DaylightDelta < TimeSpan.Zero)
      {
        if (rule.IsEndDateMarkerForEndOfYear())
          return false;
        dateTime1 = daylightTime.End;
        dateTime2 = daylightTime.End - rule.DaylightDelta;
      }
      else
      {
        if (rule.IsStartDateMarkerForBeginningOfYear())
          return false;
        dateTime1 = daylightTime.Start;
        dateTime2 = daylightTime.Start + rule.DaylightDelta;
      }
      bool flag2 = time >= dateTime1 && time < dateTime2;
      if (!flag2)
      {
        if (dateTime1.Year != dateTime2.Year)
        {
          try
          {
            DateTime dateTime3 = dateTime1.AddYears(1);
            DateTime dateTime4 = dateTime2.AddYears(1);
            flag2 = time >= dateTime3 && time < dateTime4;
          }
          catch (ArgumentOutOfRangeException ex)
          {
          }
          if (!flag2)
          {
            try
            {
              DateTime dateTime3 = dateTime1.AddYears(-1);
              DateTime dateTime4 = dateTime2.AddYears(-1);
              flag2 = time >= dateTime3 && time < dateTime4;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
        }
      }
      return flag2;
    }

    [SecuritySafeCritical]
    private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
    {
      Win32Native.DynamicTimeZoneInformation lpDynamicTimeZoneInformation = new Win32Native.DynamicTimeZoneInformation();
      if ((long) UnsafeNativeMethods.GetDynamicTimeZoneInformation(out lpDynamicTimeZoneInformation) == -1L)
        return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
      Win32Native.TimeZoneInformation timeZoneInformation1 = new Win32Native.TimeZoneInformation(lpDynamicTimeZoneInformation);
      bool dstDisabled = lpDynamicTimeZoneInformation.DynamicDaylightTimeDisabled;
      TimeZoneInfo timeZoneInfo1;
      Exception e1;
      if (!string.IsNullOrEmpty(lpDynamicTimeZoneInformation.TimeZoneKeyName) && TimeZoneInfo.TryGetTimeZone(lpDynamicTimeZoneInformation.TimeZoneKeyName, dstDisabled, out timeZoneInfo1, out e1, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
        return timeZoneInfo1;
      string timeZoneInformation2 = TimeZoneInfo.FindIdFromTimeZoneInformation(timeZoneInformation1, out dstDisabled);
      TimeZoneInfo timeZoneInfo2;
      Exception e2;
      if (timeZoneInformation2 != null && TimeZoneInfo.TryGetTimeZone(timeZoneInformation2, dstDisabled, out timeZoneInfo2, out e2, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
        return timeZoneInfo2;
      return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(timeZoneInformation1, dstDisabled);
    }

    [SecurityCritical]
    private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(Win32Native.TimeZoneInformation timeZoneInformation, bool dstDisabled)
    {
      try
      {
        return new TimeZoneInfo(timeZoneInformation, dstDisabled);
      }
      catch (ArgumentException ex)
      {
      }
      catch (InvalidTimeZoneException ex)
      {
      }
      if (!dstDisabled)
      {
        try
        {
          return new TimeZoneInfo(timeZoneInformation, true);
        }
        catch (ArgumentException ex)
        {
        }
        catch (InvalidTimeZoneException ex)
        {
        }
      }
      return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
    }

    /// <summary>根据其标识符从注册表中检索 <see cref="T:System.TimeZoneInfo" /> 对象。</summary>
    /// <returns>一个对象，其标识符为 <paramref name="id" /> 参数的值。</returns>
    /// <param name="id">时区标识符，它对应于 <see cref="P:System.TimeZoneInfo.Id" /> 属性。     </param>
    /// <exception cref="T:System.OutOfMemoryException">系统没有足够的内存来存放有关时区信息。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="id" /> 参数为 null。</exception>
    /// <exception cref="T:System.TimeZoneNotFoundException">指定的时区标识符 <paramref name="id" /> 找不到。这意味着，其名称与匹配的注册表项 <paramref name="id" /> 不存在，或此键存在，但不包含任何时区的数据。</exception>
    /// <exception cref="T:System.Security.SecurityException">进程不具有从包含时区信息的注册表项中读取所需的权限。</exception>
    /// <exception cref="T:System.InvalidTimeZoneException">找到时区标识符，但注册表数据已损坏。</exception>
    [__DynamicallyInvokable]
    public static TimeZoneInfo FindSystemTimeZoneById(string id)
    {
      if (string.Compare(id, "UTC", StringComparison.OrdinalIgnoreCase) == 0)
        return TimeZoneInfo.Utc;
      if (id == null)
        throw new ArgumentNullException("id");
      if (id.Length == 0 || id.Length > (int) byte.MaxValue || id.Contains("\0"))
        throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", (object) id));
      TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
      TimeZoneInfo timeZoneInfo;
      Exception e;
      TimeZoneInfo.TimeZoneInfoResult timeZone;
      lock (cachedData)
        timeZone = TimeZoneInfo.TryGetTimeZone(id, false, out timeZoneInfo, out e, cachedData);
      if (timeZone == TimeZoneInfo.TimeZoneInfoResult.Success)
        return timeZoneInfo;
      if (timeZone == TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException)
        throw new InvalidTimeZoneException(Environment.GetResourceString("InvalidTimeZone_InvalidRegistryData", (object) id), e);
      if (timeZone == TimeZoneInfo.TimeZoneInfoResult.SecurityException)
        throw new SecurityException(Environment.GetResourceString("Security_CannotReadRegistryData", (object) id), e);
      throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", (object) id), e);
    }

    private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
    {
      TimeSpan baseUtcOffset = zone.BaseUtcOffset;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time);
      if (adjustmentRuleForTime != null)
      {
        baseUtcOffset += adjustmentRuleForTime.BaseUtcOffsetDelta;
        if (adjustmentRuleForTime.HasDaylightSaving)
        {
          DaylightTime daylightTime = TimeZoneInfo.GetDaylightTime(time.Year, adjustmentRuleForTime);
          bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
          baseUtcOffset += isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero;
        }
      }
      return baseUtcOffset;
    }

    private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
    {
      bool isDaylightSavings;
      return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings);
    }

    private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
    {
      bool isAmbiguousLocalDst;
      return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out isAmbiguousLocalDst);
    }

    internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
    {
      isAmbiguousLocalDst = false;
      int year = time.Year;
      TimeZoneInfo.OffsetAndRule yearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
      TimeSpan timeSpan = yearLocalFromUtc.offset;
      if (yearLocalFromUtc.rule != null)
      {
        timeSpan += yearLocalFromUtc.rule.BaseUtcOffsetDelta;
        if (yearLocalFromUtc.rule.HasDaylightSaving)
        {
          bool daylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, yearLocalFromUtc.offset, yearLocalFromUtc.rule, out isAmbiguousLocalDst, TimeZoneInfo.Local);
          timeSpan += daylightSavingsFromUtc ? yearLocalFromUtc.rule.DaylightDelta : TimeSpan.Zero;
        }
      }
      return timeSpan;
    }

    internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
    {
      isDaylightSavings = false;
      isAmbiguousLocalDst = false;
      TimeSpan baseUtcOffset = zone.BaseUtcOffset;
      TimeZoneInfo.AdjustmentRule adjustmentRuleForTime;
      int Year;
      if (time > TimeZoneInfo.s_maxDateOnly)
      {
        adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MaxValue);
        Year = 9999;
      }
      else if (time < TimeZoneInfo.s_minDateOnly)
      {
        adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MinValue);
        Year = 1;
      }
      else
      {
        DateTime dateTime = time + baseUtcOffset;
        Year = dateTime.Year;
        adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(dateTime);
      }
      if (adjustmentRuleForTime != null)
      {
        baseUtcOffset += adjustmentRuleForTime.BaseUtcOffsetDelta;
        if (adjustmentRuleForTime.HasDaylightSaving)
        {
          isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, Year, zone.m_baseUtcOffset, adjustmentRuleForTime, out isAmbiguousLocalDst, zone);
          baseUtcOffset += isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero;
        }
      }
      return baseUtcOffset;
    }

    [SecurityCritical]
    private static bool TransitionTimeFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
    {
      if ((uint) timeZoneInformation.StandardDate.Month <= 0U)
      {
        transitionTime = new TimeZoneInfo.TransitionTime();
        return false;
      }
      transitionTime = !readStartDate ? ((int) timeZoneInformation.StandardDate.Year != 0 ? TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.StandardDate.Hour, (int) timeZoneInformation.StandardDate.Minute, (int) timeZoneInformation.StandardDate.Second, (int) timeZoneInformation.StandardDate.Milliseconds), (int) timeZoneInformation.StandardDate.Month, (int) timeZoneInformation.StandardDate.Day) : TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.StandardDate.Hour, (int) timeZoneInformation.StandardDate.Minute, (int) timeZoneInformation.StandardDate.Second, (int) timeZoneInformation.StandardDate.Milliseconds), (int) timeZoneInformation.StandardDate.Month, (int) timeZoneInformation.StandardDate.Day, (DayOfWeek) timeZoneInformation.StandardDate.DayOfWeek)) : ((int) timeZoneInformation.DaylightDate.Year != 0 ? TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.DaylightDate.Hour, (int) timeZoneInformation.DaylightDate.Minute, (int) timeZoneInformation.DaylightDate.Second, (int) timeZoneInformation.DaylightDate.Milliseconds), (int) timeZoneInformation.DaylightDate.Month, (int) timeZoneInformation.DaylightDate.Day) : TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int) timeZoneInformation.DaylightDate.Hour, (int) timeZoneInformation.DaylightDate.Minute, (int) timeZoneInformation.DaylightDate.Second, (int) timeZoneInformation.DaylightDate.Milliseconds), (int) timeZoneInformation.DaylightDate.Month, (int) timeZoneInformation.DaylightDate.Day, (DayOfWeek) timeZoneInformation.DaylightDate.DayOfWeek));
      return true;
    }

    private static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
    {
      DateTime timeOfDay = transitionTime.TimeOfDay;
      DateTime dateTime;
      if (transitionTime.IsFixedDateRule)
      {
        int num = DateTime.DaysInMonth(year, transitionTime.Month);
        dateTime = new DateTime(year, transitionTime.Month, num < transitionTime.Day ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
      }
      else if (transitionTime.Week <= 4)
      {
        dateTime = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
        int num1 = (int) dateTime.DayOfWeek;
        int num2 = (int) (transitionTime.DayOfWeek - num1);
        if (num2 < 0)
          num2 += 7;
        int num3 = num2 + 7 * (transitionTime.Week - 1);
        if (num3 > 0)
          dateTime = dateTime.AddDays((double) num3);
      }
      else
      {
        int day = DateTime.DaysInMonth(year, transitionTime.Month);
        dateTime = new DateTime(year, transitionTime.Month, day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
        int num = dateTime.DayOfWeek - transitionTime.DayOfWeek;
        if (num < 0)
          num += 7;
        if (num > 0)
          dateTime = dateTime.AddDays((double) -num);
      }
      return dateTime;
    }

    [SecurityCritical]
    private static bool TryCreateAdjustmentRules(string id, Win32Native.RegistryTimeZoneInformation defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
    {
      e = (Exception) null;
      try
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\{1}\\Dynamic DST", (object) "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", (object) id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (registryKey == null)
          {
            Win32Native.RegistryTimeZoneInformation timeZoneInformation1 = defaultTimeZoneInformation;
            DateTime dateTime = DateTime.MinValue;
            DateTime date1 = dateTime.Date;
            dateTime = DateTime.MaxValue;
            DateTime date2 = dateTime.Date;
            int defaultBaseUtcOffset1 = defaultBaseUtcOffset;
            TimeZoneInfo.AdjustmentRule timeZoneInformation2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation1, date1, date2, defaultBaseUtcOffset1);
            if (timeZoneInformation2 == null)
            {
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
            }
            else
            {
              rules = new TimeZoneInfo.AdjustmentRule[1];
              rules[0] = timeZoneInformation2;
            }
            return true;
          }
          int year1 = (int) registryKey.GetValue("FirstEntry", (object) -1, RegistryValueOptions.None);
          int year2 = (int) registryKey.GetValue("LastEntry", (object) -1, RegistryValueOptions.None);
          if (year1 == -1 || year2 == -1 || year1 > year2)
          {
            rules = (TimeZoneInfo.AdjustmentRule[]) null;
            return false;
          }
          byte[] bytes1 = registryKey.GetValue(year1.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) null, RegistryValueOptions.None) as byte[];
          if (bytes1 == null || bytes1.Length != 44)
          {
            rules = (TimeZoneInfo.AdjustmentRule[]) null;
            return false;
          }
          Win32Native.RegistryTimeZoneInformation timeZoneInformation3 = new Win32Native.RegistryTimeZoneInformation(bytes1);
          if (year1 == year2)
          {
            Win32Native.RegistryTimeZoneInformation timeZoneInformation1 = timeZoneInformation3;
            DateTime dateTime = DateTime.MinValue;
            DateTime date1 = dateTime.Date;
            dateTime = DateTime.MaxValue;
            DateTime date2 = dateTime.Date;
            int defaultBaseUtcOffset1 = defaultBaseUtcOffset;
            TimeZoneInfo.AdjustmentRule timeZoneInformation2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation1, date1, date2, defaultBaseUtcOffset1);
            if (timeZoneInformation2 == null)
            {
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
            }
            else
            {
              rules = new TimeZoneInfo.AdjustmentRule[1];
              rules[0] = timeZoneInformation2;
            }
            return true;
          }
          List<TimeZoneInfo.AdjustmentRule> adjustmentRuleList = new List<TimeZoneInfo.AdjustmentRule>(1);
          TimeZoneInfo.AdjustmentRule timeZoneInformation4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation3, DateTime.MinValue.Date, new DateTime(year1, 12, 31), defaultBaseUtcOffset);
          if (timeZoneInformation4 != null)
            adjustmentRuleList.Add(timeZoneInformation4);
          for (int year3 = year1 + 1; year3 < year2; ++year3)
          {
            byte[] bytes2 = registryKey.GetValue(year3.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) null, RegistryValueOptions.None) as byte[];
            if (bytes2 == null || bytes2.Length != 44)
            {
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
              return false;
            }
            timeZoneInformation3 = new Win32Native.RegistryTimeZoneInformation(bytes2);
            TimeZoneInfo.AdjustmentRule timeZoneInformation1 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation3, new DateTime(year3, 1, 1), new DateTime(year3, 12, 31), defaultBaseUtcOffset);
            if (timeZoneInformation1 != null)
              adjustmentRuleList.Add(timeZoneInformation1);
          }
          byte[] bytes3 = registryKey.GetValue(year2.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) null, RegistryValueOptions.None) as byte[];
          timeZoneInformation3 = new Win32Native.RegistryTimeZoneInformation(bytes3);
          if (bytes3 == null || bytes3.Length != 44)
          {
            rules = (TimeZoneInfo.AdjustmentRule[]) null;
            return false;
          }
          TimeZoneInfo.AdjustmentRule timeZoneInformation5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(timeZoneInformation3, new DateTime(year2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
          if (timeZoneInformation5 != null)
            adjustmentRuleList.Add(timeZoneInformation5);
          rules = adjustmentRuleList.ToArray();
          if (rules != null)
          {
            if (rules.Length == 0)
              rules = (TimeZoneInfo.AdjustmentRule[]) null;
          }
        }
      }
      catch (InvalidCastException ex)
      {
        rules = (TimeZoneInfo.AdjustmentRule[]) null;
        e = (Exception) ex;
        return false;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        rules = (TimeZoneInfo.AdjustmentRule[]) null;
        e = (Exception) ex;
        return false;
      }
      catch (ArgumentException ex)
      {
        rules = (TimeZoneInfo.AdjustmentRule[]) null;
        e = (Exception) ex;
        return false;
      }
      return true;
    }

    [SecurityCritical]
    private static bool TryCompareStandardDate(Win32Native.TimeZoneInformation timeZone, Win32Native.RegistryTimeZoneInformation registryTimeZoneInfo)
    {
      if (timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias && ((int) timeZone.StandardDate.Year == (int) registryTimeZoneInfo.StandardDate.Year && (int) timeZone.StandardDate.Month == (int) registryTimeZoneInfo.StandardDate.Month) && ((int) timeZone.StandardDate.DayOfWeek == (int) registryTimeZoneInfo.StandardDate.DayOfWeek && (int) timeZone.StandardDate.Day == (int) registryTimeZoneInfo.StandardDate.Day && ((int) timeZone.StandardDate.Hour == (int) registryTimeZoneInfo.StandardDate.Hour && (int) timeZone.StandardDate.Minute == (int) registryTimeZoneInfo.StandardDate.Minute)) && (int) timeZone.StandardDate.Second == (int) registryTimeZoneInfo.StandardDate.Second)
        return (int) timeZone.StandardDate.Milliseconds == (int) registryTimeZoneInfo.StandardDate.Milliseconds;
      return false;
    }

    [SecuritySafeCritical]
    private static bool TryCompareTimeZoneInformationToRegistry(Win32Native.TimeZoneInformation timeZone, string id, out bool dstDisabled)
    {
      dstDisabled = false;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");
        permissionSet.AddPermission((IPermission) registryPermission);
        permissionSet.Assert();
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\{1}", (object) "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", (object) id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (registryKey == null)
            return false;
          byte[] bytes = (byte[]) registryKey.GetValue("TZI", (object) null, RegistryValueOptions.None);
          if (bytes == null || bytes.Length != 44)
            return false;
          Win32Native.RegistryTimeZoneInformation registryTimeZoneInfo = new Win32Native.RegistryTimeZoneInformation(bytes);
          if (!TimeZoneInfo.TryCompareStandardDate(timeZone, registryTimeZoneInfo))
            return false;
          bool flag = dstDisabled || TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone) || timeZone.DaylightBias == registryTimeZoneInfo.DaylightBias && (int) timeZone.DaylightDate.Year == (int) registryTimeZoneInfo.DaylightDate.Year && ((int) timeZone.DaylightDate.Month == (int) registryTimeZoneInfo.DaylightDate.Month && (int) timeZone.DaylightDate.DayOfWeek == (int) registryTimeZoneInfo.DaylightDate.DayOfWeek) && ((int) timeZone.DaylightDate.Day == (int) registryTimeZoneInfo.DaylightDate.Day && (int) timeZone.DaylightDate.Hour == (int) registryTimeZoneInfo.DaylightDate.Hour && ((int) timeZone.DaylightDate.Minute == (int) registryTimeZoneInfo.DaylightDate.Minute && (int) timeZone.DaylightDate.Second == (int) registryTimeZoneInfo.DaylightDate.Second)) && (int) timeZone.DaylightDate.Milliseconds == (int) registryTimeZoneInfo.DaylightDate.Milliseconds;
          if (flag)
            flag = string.Compare(registryKey.GetValue("Std", (object) string.Empty, RegistryValueOptions.None) as string, timeZone.StandardName, StringComparison.Ordinal) == 0;
          return flag;
        }
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
    }

    [SecuritySafeCritical]
    [FileIOPermission(SecurityAction.Assert, AllLocalFiles = FileIOPermissionAccess.PathDiscovery)]
    private static string TryGetLocalizedNameByMuiNativeResource(string resource)
    {
      if (string.IsNullOrEmpty(resource))
        return string.Empty;
      string[] strArray = resource.Split(new char[1]{ ',' }, StringSplitOptions.None);
      if (strArray.Length != 2)
        return string.Empty;
      string folderPath = Environment.UnsafeGetFolderPath(Environment.SpecialFolder.System);
      string path2 = strArray[0].TrimStart('@');
      string filePath;
      try
      {
        filePath = Path.Combine(folderPath, path2);
      }
      catch (ArgumentException ex)
      {
        return string.Empty;
      }
      int result;
      if (!int.TryParse(strArray[1], NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return string.Empty;
      result = -result;
      try
      {
        StringBuilder stringBuilder = StringBuilderCache.Acquire(260);
        stringBuilder.Length = 260;
        int fileMuiPathLength = 260;
        int languageLength = 0;
        long enumerator = 0;
        if (UnsafeNativeMethods.GetFileMUIPath(16, filePath, (StringBuilder) null, ref languageLength, stringBuilder, ref fileMuiPathLength, ref enumerator))
          return TimeZoneInfo.TryGetLocalizedNameByNativeResource(StringBuilderCache.GetStringAndRelease(stringBuilder), result);
        StringBuilderCache.Release(stringBuilder);
        return string.Empty;
      }
      catch (EntryPointNotFoundException ex)
      {
        return string.Empty;
      }
    }

    [SecurityCritical]
    private static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
    {
      using (SafeLibraryHandle safeLibraryHandle = UnsafeNativeMethods.LoadLibraryEx(filePath, IntPtr.Zero, 2))
      {
        if (!safeLibraryHandle.IsInvalid)
        {
          StringBuilder sb = StringBuilderCache.Acquire(500);
          sb.Length = 500;
          SafeLibraryHandle handle = safeLibraryHandle;
          int id = resource;
          StringBuilder buffer = sb;
          int length = buffer.Length;
          if (UnsafeNativeMethods.LoadString(handle, id, buffer, length) != 0)
            return StringBuilderCache.GetStringAndRelease(sb);
        }
      }
      return string.Empty;
    }

    private static bool TryGetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
    {
      displayName = string.Empty;
      standardName = string.Empty;
      daylightName = string.Empty;
      string resource1 = key.GetValue("MUI_Display", (object) string.Empty, RegistryValueOptions.None) as string;
      string resource2 = key.GetValue("MUI_Std", (object) string.Empty, RegistryValueOptions.None) as string;
      string resource3 = key.GetValue("MUI_Dlt", (object) string.Empty, RegistryValueOptions.None) as string;
      if (!string.IsNullOrEmpty(resource1))
        displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(resource1);
      if (!string.IsNullOrEmpty(resource2))
        standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(resource2);
      if (!string.IsNullOrEmpty(resource3))
        daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(resource3);
      if (string.IsNullOrEmpty(displayName))
        displayName = key.GetValue("Display", (object) string.Empty, RegistryValueOptions.None) as string;
      if (string.IsNullOrEmpty(standardName))
        standardName = key.GetValue("Std", (object) string.Empty, RegistryValueOptions.None) as string;
      if (string.IsNullOrEmpty(daylightName))
        daylightName = key.GetValue("Dlt", (object) string.Empty, RegistryValueOptions.None) as string;
      return true;
    }

    [SecuritySafeCritical]
    private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneByRegistryKey(string id, out TimeZoneInfo value, out Exception e)
    {
      e = (Exception) null;
      try
      {
        PermissionSet permissionSet = new PermissionSet(PermissionState.None);
        RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");
        permissionSet.AddPermission((IPermission) registryPermission);
        permissionSet.Assert();
        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\{1}", (object) "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", (object) id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
        {
          if (key == null)
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
          }
          byte[] bytes = key.GetValue("TZI", (object) null, RegistryValueOptions.None) as byte[];
          if (bytes == null || bytes.Length != 44)
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          Win32Native.RegistryTimeZoneInformation defaultTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(bytes);
          TimeZoneInfo.AdjustmentRule[] rules;
          if (!TimeZoneInfo.TryCreateAdjustmentRules(id, defaultTimeZoneInformation, out rules, out e, defaultTimeZoneInformation.Bias))
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          string displayName;
          string standardName;
          string daylightName;
          if (!TimeZoneInfo.TryGetLocalizedNamesByRegistryKey(key, out displayName, out standardName, out daylightName))
          {
            value = (TimeZoneInfo) null;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          try
          {
            value = new TimeZoneInfo(id, new TimeSpan(0, -defaultTimeZoneInformation.Bias, 0), displayName, standardName, daylightName, rules, false);
            return TimeZoneInfo.TimeZoneInfoResult.Success;
          }
          catch (ArgumentException ex)
          {
            value = (TimeZoneInfo) null;
            e = (Exception) ex;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
          catch (InvalidTimeZoneException ex)
          {
            value = (TimeZoneInfo) null;
            e = (Exception) ex;
            return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
          }
        }
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
    }

    private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
    {
      TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult1 = TimeZoneInfo.TimeZoneInfoResult.Success;
      e = (Exception) null;
      TimeZoneInfo timeZoneInfo = (TimeZoneInfo) null;
      if (cachedData.m_systemTimeZones != null && cachedData.m_systemTimeZones.TryGetValue(id, out timeZoneInfo))
      {
        value = !dstDisabled || !timeZoneInfo.m_supportsDaylightSavingTime ? new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false) : TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
        return timeZoneInfoResult1;
      }
      TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult2;
      if (!cachedData.m_allSystemTimeZonesRead)
      {
        timeZoneInfoResult2 = TimeZoneInfo.TryGetTimeZoneByRegistryKey(id, out timeZoneInfo, out e);
        if (timeZoneInfoResult2 == TimeZoneInfo.TimeZoneInfoResult.Success)
        {
          if (cachedData.m_systemTimeZones == null)
            cachedData.m_systemTimeZones = new Dictionary<string, TimeZoneInfo>();
          cachedData.m_systemTimeZones.Add(id, timeZoneInfo);
          value = !dstDisabled || !timeZoneInfo.m_supportsDaylightSavingTime ? new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false) : TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
        }
        else
          value = (TimeZoneInfo) null;
      }
      else
      {
        timeZoneInfoResult2 = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
        value = (TimeZoneInfo) null;
      }
      return timeZoneInfoResult2;
    }

    internal static bool UtcOffsetOutOfRange(TimeSpan offset)
    {
      if (offset.TotalHours >= -14.0)
        return offset.TotalHours > 14.0;
      return true;
    }

    private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      if (id.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidId", (object) id), "id");
      if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
        throw new ArgumentOutOfRangeException("baseUtcOffset", Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
      if (baseUtcOffset.Ticks % 600000000L != 0L)
        throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), "baseUtcOffset");
      adjustmentRulesSupportDst = false;
      if (adjustmentRules == null || adjustmentRules.Length == 0)
        return;
      adjustmentRulesSupportDst = true;
      TimeZoneInfo.AdjustmentRule adjustmentRule1 = (TimeZoneInfo.AdjustmentRule) null;
      for (int index = 0; index < adjustmentRules.Length; ++index)
      {
        TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule1;
        adjustmentRule1 = adjustmentRules[index];
        if (adjustmentRule1 == null)
          throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesNoNulls"));
        if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset + adjustmentRule1.DaylightDelta))
          throw new InvalidTimeZoneException(Environment.GetResourceString("ArgumentOutOfRange_UtcOffsetAndDaylightDelta"));
        if (adjustmentRule2 != null && adjustmentRule1.DateStart <= adjustmentRule2.DateEnd)
          throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesOutOfOrder"));
      }
    }

    private enum TimeZoneInfoResult
    {
      Success,
      TimeZoneNotFoundException,
      InvalidTimeZoneException,
      SecurityException,
    }

    private class CachedData
    {
      private volatile TimeZoneInfo m_localTimeZone;
      private volatile TimeZoneInfo m_utcTimeZone;
      public Dictionary<string, TimeZoneInfo> m_systemTimeZones;
      public ReadOnlyCollection<TimeZoneInfo> m_readOnlySystemTimeZones;
      public bool m_allSystemTimeZonesRead;
      private volatile TimeZoneInfo.OffsetAndRule m_oneYearLocalFromUtc;

      public TimeZoneInfo Local
      {
        get
        {
          return this.m_localTimeZone ?? this.CreateLocal();
        }
      }

      public TimeZoneInfo Utc
      {
        get
        {
          return this.m_utcTimeZone ?? this.CreateUtc();
        }
      }

      private TimeZoneInfo CreateLocal()
      {
        lock (this)
        {
          TimeZoneInfo local_2 = this.m_localTimeZone;
          if (local_2 == null)
          {
            TimeZoneInfo local_2_1 = TimeZoneInfo.GetLocalTimeZone(this);
            local_2 = new TimeZoneInfo(local_2_1.m_id, local_2_1.m_baseUtcOffset, local_2_1.m_displayName, local_2_1.m_standardDisplayName, local_2_1.m_daylightDisplayName, local_2_1.m_adjustmentRules, false);
            this.m_localTimeZone = local_2;
          }
          return local_2;
        }
      }

      private TimeZoneInfo CreateUtc()
      {
        lock (this)
        {
          TimeZoneInfo local_2 = this.m_utcTimeZone;
          if (local_2 == null)
          {
            local_2 = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");
            this.m_utcTimeZone = local_2;
          }
          return local_2;
        }
      }

      public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
      {
        return timeZone != this.m_utcTimeZone ? (timeZone != this.m_localTimeZone ? DateTimeKind.Unspecified : DateTimeKind.Local) : DateTimeKind.Utc;
      }

      [SecuritySafeCritical]
      private static TimeZoneInfo GetCurrentOneYearLocal()
      {
        Win32Native.TimeZoneInformation lpTimeZoneInformation = new Win32Native.TimeZoneInformation();
        return (long) UnsafeNativeMethods.GetTimeZoneInformation(out lpTimeZoneInformation) != -1L ? TimeZoneInfo.GetLocalTimeZoneFromWin32Data(lpTimeZoneInformation, false) : TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
      }

      public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
      {
        TimeZoneInfo.OffsetAndRule offsetAndRule = this.m_oneYearLocalFromUtc;
        if (offsetAndRule == null || offsetAndRule.year != year)
        {
          TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
          TimeZoneInfo.AdjustmentRule rule = currentOneYearLocal.m_adjustmentRules == null ? (TimeZoneInfo.AdjustmentRule) null : currentOneYearLocal.m_adjustmentRules[0];
          offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, rule);
          this.m_oneYearLocalFromUtc = offsetAndRule;
        }
        return offsetAndRule;
      }
    }

    private class OffsetAndRule
    {
      public int year;
      public TimeSpan offset;
      public TimeZoneInfo.AdjustmentRule rule;

      public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
      {
        this.year = year;
        this.offset = offset;
        this.rule = rule;
      }
    }

    /// <summary>提供有关时区调整（如与夏时制之间的转换）的信息。</summary>
    /// <filterpriority>2</filterpriority>
    [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
    {
      private DateTime m_dateStart;
      private DateTime m_dateEnd;
      private TimeSpan m_daylightDelta;
      private TimeZoneInfo.TransitionTime m_daylightTransitionStart;
      private TimeZoneInfo.TransitionTime m_daylightTransitionEnd;
      private TimeSpan m_baseUtcOffsetDelta;

      /// <summary>获取调整规则的生效日期。</summary>
      /// <returns>一个 <see cref="T:System.DateTime" /> 值，指示调整规则何时生效。</returns>
      public DateTime DateStart
      {
        get
        {
          return this.m_dateStart;
        }
      }

      /// <summary>获取调整规则的失效日期。</summary>
      /// <returns>一个 <see cref="T:System.DateTime" /> 值，指示调整规则的结束日期。</returns>
      public DateTime DateEnd
      {
        get
        {
          return this.m_dateEnd;
        }
      }

      /// <summary>获取形成时区的夏时制所需的时间量。该时间量添加到时区与协调世界时 (UTC) 的偏移量中。</summary>
      /// <returns>一个 <see cref="T:System.TimeSpan" /> 对象，指示作为调整规则的执行结果而添加到标准时间更改中的时间量。</returns>
      public TimeSpan DaylightDelta
      {
        get
        {
          return this.m_daylightDelta;
        }
      }

      /// <summary>获取有关每年何时从标准时间转换为夏时制的信息。</summary>
      /// <returns>一个 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象，定义每年何时从时区的标准时间转换为夏时制。</returns>
      public TimeZoneInfo.TransitionTime DaylightTransitionStart
      {
        get
        {
          return this.m_daylightTransitionStart;
        }
      }

      /// <summary>获取有关每年何时从夏时制转换回标准时间的信息。</summary>
      /// <returns>一个 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象，定义每年何时从夏时制转换回时区的标准时间。</returns>
      public TimeZoneInfo.TransitionTime DaylightTransitionEnd
      {
        get
        {
          return this.m_daylightTransitionEnd;
        }
      }

      internal TimeSpan BaseUtcOffsetDelta
      {
        get
        {
          return this.m_baseUtcOffsetDelta;
        }
      }

      internal bool HasDaylightSaving
      {
        get
        {
          if (!(this.DaylightDelta != TimeSpan.Zero) && !(this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue))
            return this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0);
          return true;
        }
      }

      private AdjustmentRule()
      {
      }

      private AdjustmentRule(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.m_dateStart = (DateTime) info.GetValue("DateStart", typeof (DateTime));
        this.m_dateEnd = (DateTime) info.GetValue("DateEnd", typeof (DateTime));
        this.m_daylightDelta = (TimeSpan) info.GetValue("DaylightDelta", typeof (TimeSpan));
        this.m_daylightTransitionStart = (TimeZoneInfo.TransitionTime) info.GetValue("DaylightTransitionStart", typeof (TimeZoneInfo.TransitionTime));
        this.m_daylightTransitionEnd = (TimeZoneInfo.TransitionTime) info.GetValue("DaylightTransitionEnd", typeof (TimeZoneInfo.TransitionTime));
        object valueNoThrow = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof (TimeSpan));
        if (valueNoThrow == null)
          return;
        this.m_baseUtcOffsetDelta = (TimeSpan) valueNoThrow;
      }

      /// <summary>确定当前 <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> 对象是否等于另一个 <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> 对象。</summary>
      /// <returns>如果两个 <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> 对象具有相等的值，则为 true；否则为 false。</returns>
      /// <param name="other">要与当前对象进行比较的对象。</param>
      /// <filterpriority>2</filterpriority>
      public bool Equals(TimeZoneInfo.AdjustmentRule other)
      {
        if ((other == null || !(this.m_dateStart == other.m_dateStart) || (!(this.m_dateEnd == other.m_dateEnd) || !(this.m_daylightDelta == other.m_daylightDelta)) ? 0 : (this.m_baseUtcOffsetDelta == other.m_baseUtcOffsetDelta ? 1 : 0)) != 0 && this.m_daylightTransitionEnd.Equals(other.m_daylightTransitionEnd))
          return this.m_daylightTransitionStart.Equals(other.m_daylightTransitionStart);
        return false;
      }

      /// <summary>用作哈希算法的哈希函数和数据结构（如哈希表）。</summary>
      /// <returns>一个 32 位有符号整数，用作当前 <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> 对象的哈希代码。</returns>
      /// <filterpriority>2</filterpriority>
      public override int GetHashCode()
      {
        return this.m_dateStart.GetHashCode();
      }

      /// <summary>为特定时区创建新的调整规则。</summary>
      /// <returns>一个对象，表示新的调整规则。</returns>
      /// <param name="dateStart">调整规则的生效日期。如果 <paramref name="dateStart" /> 参数的值为 DateTime.MinValue.Date，则这是对某时区生效的第一个调整规则。</param>
      /// <param name="dateEnd">调整规则生效的最后日期。如果 <paramref name="dateEnd" /> 参数的值为 DateTime.MaxValue.Date，则调整规则没有结束日期。</param>
      /// <param name="daylightDelta">调整产生的时间更改。将此值添加到时区的 <see cref="P:System.TimeZoneInfo.BaseUtcOffset" /> 属性，以获取与协调世界时 (UTC) 之间正确的白昼偏移量。此值的范围是从 -14 到 14。</param>
      /// <param name="daylightTransitionStart">一个对象，定义夏时制的起始时间。</param>
      /// <param name="daylightTransitionEnd">一个对象，定义夏时制的结束时间。</param>
      /// <exception cref="T:System.ArgumentException">
      /// <paramref name="dateStart" /> 或 <paramref name="dateEnd" /> 参数的 <see cref="P:System.DateTime.Kind" /> 属性不等于 <see cref="F:System.DateTimeKind.Unspecified" />。- 或 -<paramref name="daylightTransitionStart" /> 参数等于 <paramref name="daylightTransitionEnd" /> 参数。- 或 -<paramref name="dateStart" /> 或 <paramref name="dateEnd" /> 参数包含一个时间值。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="dateEnd" /> 早于 <paramref name="dateStart" />。- 或 -<paramref name="daylightDelta" /> 小于 -14 或大于 14。- 或 -<paramref name="daylightDelta" /> 参数的 <see cref="P:System.TimeSpan.Milliseconds" /> 属性不等于 0。- 或 -<paramref name="daylightDelta" /> 参数的 <see cref="P:System.TimeSpan.Ticks" /> 属性不等于整秒数。</exception>
      public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
      {
        TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
        return new TimeZoneInfo.AdjustmentRule() { m_dateStart = dateStart, m_dateEnd = dateEnd, m_daylightDelta = daylightDelta, m_daylightTransitionStart = daylightTransitionStart, m_daylightTransitionEnd = daylightTransitionEnd, m_baseUtcOffsetDelta = TimeSpan.Zero };
      }

      internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta)
      {
        TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
        TimeSpan timeSpan = baseUtcOffsetDelta;
        adjustmentRule.m_baseUtcOffsetDelta = timeSpan;
        return adjustmentRule;
      }

      internal bool IsStartDateMarkerForBeginningOfYear()
      {
        if (this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1)
        {
          DateTime timeOfDay = this.DaylightTransitionStart.TimeOfDay;
          if (timeOfDay.Hour == 0)
          {
            timeOfDay = this.DaylightTransitionStart.TimeOfDay;
            if (timeOfDay.Minute == 0)
            {
              timeOfDay = this.DaylightTransitionStart.TimeOfDay;
              if (timeOfDay.Second == 0)
                return this.m_dateStart.Year == this.m_dateEnd.Year;
            }
          }
        }
        return false;
      }

      internal bool IsEndDateMarkerForEndOfYear()
      {
        if (this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1)
        {
          DateTime timeOfDay = this.DaylightTransitionEnd.TimeOfDay;
          if (timeOfDay.Hour == 0)
          {
            timeOfDay = this.DaylightTransitionEnd.TimeOfDay;
            if (timeOfDay.Minute == 0)
            {
              timeOfDay = this.DaylightTransitionEnd.TimeOfDay;
              if (timeOfDay.Second == 0)
                return this.m_dateStart.Year == this.m_dateEnd.Year;
            }
          }
        }
        return false;
      }

      private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
      {
        if (dateStart.Kind != DateTimeKind.Unspecified)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "dateStart");
        if (dateEnd.Kind != DateTimeKind.Unspecified)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "dateEnd");
        if (daylightTransitionStart.Equals(daylightTransitionEnd))
          throw new ArgumentException(Environment.GetResourceString("Argument_TransitionTimesAreIdentical"), "daylightTransitionEnd");
        if (dateStart > dateEnd)
          throw new ArgumentException(Environment.GetResourceString("Argument_OutOfOrderDateTimes"), "dateStart");
        if (TimeZoneInfo.UtcOffsetOutOfRange(daylightDelta))
          throw new ArgumentOutOfRangeException("daylightDelta", (object) daylightDelta, Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
        if (daylightDelta.Ticks % 600000000L != 0L)
          throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), "daylightDelta");
        if (dateStart.TimeOfDay != TimeSpan.Zero)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), "dateStart");
        if (dateEnd.TimeOfDay != TimeSpan.Zero)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), "dateEnd");
      }

      void IDeserializationCallback.OnDeserialization(object sender)
      {
        try
        {
          TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this.m_dateStart, this.m_dateEnd, this.m_daylightDelta, this.m_daylightTransitionStart, this.m_daylightTransitionEnd);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("DateStart", this.m_dateStart);
        info.AddValue("DateEnd", this.m_dateEnd);
        info.AddValue("DaylightDelta", (object) this.m_daylightDelta);
        info.AddValue("DaylightTransitionStart", (object) this.m_daylightTransitionStart);
        info.AddValue("DaylightTransitionEnd", (object) this.m_daylightTransitionEnd);
        info.AddValue("BaseUtcOffsetDelta", (object) this.m_baseUtcOffsetDelta);
      }
    }

    /// <summary>提供有关特定时区中特定时间更改（例如从夏时制更改为标准时间，或者从标准时间更改为夏时制）的信息。</summary>
    /// <filterpriority>2</filterpriority>
    [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
    {
      private DateTime m_timeOfDay;
      private byte m_month;
      private byte m_week;
      private byte m_day;
      private DayOfWeek m_dayOfWeek;
      private bool m_isFixedDateRule;

      /// <summary>获取发生时间更改的小时、分钟和秒。</summary>
      /// <returns>一天内发生时间更改的时间。</returns>
      public DateTime TimeOfDay
      {
        get
        {
          return this.m_timeOfDay;
        }
      }

      /// <summary>获取发生时间更改的月份。</summary>
      /// <returns>发生时间更改的月份。</returns>
      public int Month
      {
        get
        {
          return (int) this.m_month;
        }
      }

      /// <summary>获取发生时间更改的月份的第几个星期。</summary>
      /// <returns>发生时间更改的月份的第几个星期。</returns>
      public int Week
      {
        get
        {
          return (int) this.m_week;
        }
      }

      /// <summary>获取发生时间更改的那一天。</summary>
      /// <returns>发生时间更改的那一天。</returns>
      public int Day
      {
        get
        {
          return (int) this.m_day;
        }
      }

      /// <summary>获取发生时间更改的星期几。</summary>
      /// <returns>发生时间更改的星期几。</returns>
      public DayOfWeek DayOfWeek
      {
        get
        {
          return this.m_dayOfWeek;
        }
      }

      /// <summary>获取一个值，该值指示是在固定日期和时间（如 11 月 1 日）还是在浮动日期和时间（如 10 月的最后一个星期日）发生时间更改。</summary>
      /// <returns>如果时间更改规则为固定日期，则为 true；如果时间更改规则为浮动日期，则为 false。</returns>
      public bool IsFixedDateRule
      {
        get
        {
          return this.m_isFixedDateRule;
        }
      }

      private TransitionTime(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.m_timeOfDay = (DateTime) info.GetValue("TimeOfDay", typeof (DateTime));
        this.m_month = (byte) info.GetValue("Month", typeof (byte));
        this.m_week = (byte) info.GetValue("Week", typeof (byte));
        this.m_day = (byte) info.GetValue("Day", typeof (byte));
        this.m_dayOfWeek = (DayOfWeek) info.GetValue("DayOfWeek", typeof (DayOfWeek));
        this.m_isFixedDateRule = (bool) info.GetValue("IsFixedDateRule", typeof (bool));
      }

      /// <summary>确定指定的两个 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象是否相等。</summary>
      /// <returns>如果 <paramref name="t1" /> 和 <paramref name="t2" /> 具有相同的值，则为 true；否则为 false。</returns>
      /// <param name="t1">要比较的第一个对象。</param>
      /// <param name="t2">要比较的第二个对象。</param>
      public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
      {
        return t1.Equals(t2);
      }

      /// <summary>确定指定的两个 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象是否不相等。</summary>
      /// <returns>如果 <paramref name="t1" /> 和 <paramref name="t2" /> 具有任何不同的成员值，则为 true；否则为 false。</returns>
      /// <param name="t1">要比较的第一个对象。</param>
      /// <param name="t2">要比较的第二个对象。</param>
      public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
      {
        return !t1.Equals(t2);
      }

      /// <summary>确定一个对象与当前的 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象是否具有相同的值。</summary>
      /// <returns>如果两个对象相等，则为 true；否则为 false。</returns>
      /// <param name="obj">将与当前的 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象进行比较的对象。</param>
      /// <filterpriority>2</filterpriority>
      public override bool Equals(object obj)
      {
        if (obj is TimeZoneInfo.TransitionTime)
          return this.Equals((TimeZoneInfo.TransitionTime) obj);
        return false;
      }

      /// <summary>确定当前的 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象与另一个 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象是否具有相同的值。</summary>
      /// <returns>如果这两个对象具有相同的属性值，则为 true；否则为 false。</returns>
      /// <param name="other">要与当前实例进行比较的对象。</param>
      /// <filterpriority>2</filterpriority>
      public bool Equals(TimeZoneInfo.TransitionTime other)
      {
        bool flag = this.m_isFixedDateRule == other.m_isFixedDateRule && this.m_timeOfDay == other.m_timeOfDay && (int) this.m_month == (int) other.m_month;
        if (flag)
          flag = !other.m_isFixedDateRule ? (int) this.m_week == (int) other.m_week && this.m_dayOfWeek == other.m_dayOfWeek : (int) this.m_day == (int) other.m_day;
        return flag;
      }

      /// <summary>用作哈希算法的哈希函数和数据结构（如哈希表）。</summary>
      /// <returns>一个 32 位有符号整数，用作此 <see cref="T:System.TimeZoneInfo.TransitionTime" /> 对象的哈希代码。</returns>
      /// <filterpriority>2</filterpriority>
      public override int GetHashCode()
      {
        return (int) this.m_month ^ (int) this.m_week << 8;
      }

      /// <summary>定义一个使用固定日期规则的实际更改（即，发生在特定月份的特定日期的时间更改）。</summary>
      /// <returns>有关时间更改的数据。</returns>
      /// <param name="timeOfDay">发生时间更改的时间。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" /> 属性。有关详细信息，请参见“备注”。</param>
      /// <param name="month">发生时间更改的月份。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.Month" /> 属性。</param>
      /// <param name="day">发生时间更改的月份的日期。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.Day" /> 属性。</param>
      /// <exception cref="T:System.ArgumentException">
      /// <paramref name="timeOfDay" /> 参数具有非默认的日期部分。- 或 -<paramref name="timeOfDay" /> 参数的 <see cref="P:System.DateTime.Kind" /> 属性不是 <see cref="F:System.DateTimeKind.Unspecified" />。- 或 -<paramref name="timeOfDay" /> 参数不表示整毫秒数。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="month" /> 参数小于 1 或大于 12。- 或 -<paramref name="day" /> 参数小于 1 或大于 31。</exception>
      public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
      {
        return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
      }

      /// <summary>定义一个使用浮动日期规则的日期更改（即，发生在特定月份的特定星期的特定星期几的时间更改）。</summary>
      /// <returns>有关时间更改的数据。</returns>
      /// <param name="timeOfDay">发生时间更改的时间。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" /> 属性。有关详细信息，请参见“备注”。</param>
      /// <param name="month">发生时间更改的月份。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.Month" /> 属性。</param>
      /// <param name="week">发生时间更改的月份的第几个星期。它的值的范围是从 1 到 5，其中 5 表示该月的最后一周。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.Week" /> 属性。</param>
      /// <param name="dayOfWeek">发生时间更改的星期几。此参数对应于 <see cref="P:System.TimeZoneInfo.TransitionTime.DayOfWeek" /> 属性。</param>
      /// <exception cref="T:System.ArgumentException">
      /// <paramref name="timeOfDay" /> 参数具有非默认的日期部分。- 或 -<paramref name="timeOfDay" /> 参数不表示整毫秒数。- 或 -<paramref name="timeOfDay" /> 参数的 <see cref="P:System.DateTime.Kind" /> 属性不是 <see cref="F:System.DateTimeKind.Unspecified" />。</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      /// <paramref name="month" /> 小于 1 或大于 12。- 或 -<paramref name="week" /> 小于 1 或大于 5。- 或 -<paramref name="dayOfWeek" /> 参数不是 <see cref="T:System.DayOfWeek" /> 枚举的成员。</exception>
      public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
      {
        return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
      }

      private static TimeZoneInfo.TransitionTime CreateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
      {
        TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
        return new TimeZoneInfo.TransitionTime() { m_isFixedDateRule = isFixedDateRule, m_timeOfDay = timeOfDay, m_dayOfWeek = dayOfWeek, m_day = (byte) day, m_week = (byte) week, m_month = (byte) month };
      }

      private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
      {
        if (timeOfDay.Kind != DateTimeKind.Unspecified)
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "timeOfDay");
        if (month < 1 || month > 12)
          throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_MonthParam"));
        if (day < 1 || day > 31)
          throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_DayParam"));
        if (week < 1 || week > 5)
          throw new ArgumentOutOfRangeException("week", Environment.GetResourceString("ArgumentOutOfRange_Week"));
        if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
          throw new ArgumentOutOfRangeException("dayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_DayOfWeek"));
        if (timeOfDay.Year != 1 || timeOfDay.Month != 1 || (timeOfDay.Day != 1 || timeOfDay.Ticks % 10000L != 0L))
          throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTicks"), "timeOfDay");
      }

      void IDeserializationCallback.OnDeserialization(object sender)
      {
        try
        {
          TimeZoneInfo.TransitionTime.ValidateTransitionTime(this.m_timeOfDay, (int) this.m_month, (int) this.m_week, (int) this.m_day, this.m_dayOfWeek);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("TimeOfDay", this.m_timeOfDay);
        info.AddValue("Month", this.m_month);
        info.AddValue("Week", this.m_week);
        info.AddValue("Day", this.m_day);
        info.AddValue("DayOfWeek", (object) this.m_dayOfWeek);
        info.AddValue("IsFixedDateRule", this.m_isFixedDateRule);
      }
    }

    private sealed class StringSerializer
    {
      private string m_serializedText;
      private int m_currentTokenStartIndex;
      private TimeZoneInfo.StringSerializer.State m_state;
      private const int initialCapacityForString = 64;
      private const char esc = '\\';
      private const char sep = ';';
      private const char lhs = '[';
      private const char rhs = ']';
      private const string escString = "\\";
      private const string sepString = ";";
      private const string lhsString = "[";
      private const string rhsString = "]";
      private const string escapedEsc = "\\\\";
      private const string escapedSep = "\\;";
      private const string escapedLhs = "\\[";
      private const string escapedRhs = "\\]";
      private const string dateTimeFormat = "MM:dd:yyyy";
      private const string timeOfDayFormat = "HH:mm:ss.FFF";

      private StringSerializer(string str)
      {
        this.m_serializedText = str;
        this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
      }

      public static string GetSerializedString(TimeZoneInfo zone)
      {
        StringBuilder stringBuilder1 = StringBuilderCache.Acquire(16);
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.BaseUtcOffset.TotalMinutes.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName));
        stringBuilder1.Append(';');
        stringBuilder1.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName));
        stringBuilder1.Append(';');
        TimeZoneInfo.AdjustmentRule[] adjustmentRules = zone.GetAdjustmentRules();
        if (adjustmentRules != null && adjustmentRules.Length != 0)
        {
          for (int index = 0; index < adjustmentRules.Length; ++index)
          {
            TimeZoneInfo.AdjustmentRule adjustmentRule = adjustmentRules[index];
            stringBuilder1.Append('[');
            StringBuilder stringBuilder2 = stringBuilder1;
            DateTime dateTime = adjustmentRule.DateStart;
            string str1 = TimeZoneInfo.StringSerializer.SerializeSubstitute(dateTime.ToString("MM:dd:yyyy", (IFormatProvider) DateTimeFormatInfo.InvariantInfo));
            stringBuilder2.Append(str1);
            stringBuilder1.Append(';');
            StringBuilder stringBuilder3 = stringBuilder1;
            dateTime = adjustmentRule.DateEnd;
            string str2 = TimeZoneInfo.StringSerializer.SerializeSubstitute(dateTime.ToString("MM:dd:yyyy", (IFormatProvider) DateTimeFormatInfo.InvariantInfo));
            stringBuilder3.Append(str2);
            stringBuilder1.Append(';');
            StringBuilder stringBuilder4 = stringBuilder1;
            double totalMinutes = adjustmentRule.DaylightDelta.TotalMinutes;
            string str3 = TimeZoneInfo.StringSerializer.SerializeSubstitute(totalMinutes.ToString((IFormatProvider) CultureInfo.InvariantCulture));
            stringBuilder4.Append(str3);
            stringBuilder1.Append(';');
            TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder1);
            stringBuilder1.Append(';');
            TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder1);
            stringBuilder1.Append(';');
            if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
            {
              StringBuilder stringBuilder5 = stringBuilder1;
              totalMinutes = adjustmentRule.BaseUtcOffsetDelta.TotalMinutes;
              string str4 = TimeZoneInfo.StringSerializer.SerializeSubstitute(totalMinutes.ToString((IFormatProvider) CultureInfo.InvariantCulture));
              stringBuilder5.Append(str4);
              stringBuilder1.Append(';');
            }
            stringBuilder1.Append(']');
          }
        }
        stringBuilder1.Append(';');
        return StringBuilderCache.GetStringAndRelease(stringBuilder1);
      }

      public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
      {
        TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
        int num1 = 0;
        string nextStringValue1 = stringSerializer.GetNextStringValue(num1 != 0);
        int num2 = 0;
        TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue(num2 != 0);
        int num3 = 0;
        string nextStringValue2 = stringSerializer.GetNextStringValue(num3 != 0);
        int num4 = 0;
        string nextStringValue3 = stringSerializer.GetNextStringValue(num4 != 0);
        int num5 = 0;
        string nextStringValue4 = stringSerializer.GetNextStringValue(num5 != 0);
        int num6 = 0;
        TimeZoneInfo.AdjustmentRule[] adjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue(num6 != 0);
        try
        {
          return TimeZoneInfo.CreateCustomTimeZone(nextStringValue1, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, adjustmentRuleArrayValue);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
        catch (InvalidTimeZoneException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      private static string SerializeSubstitute(string text)
      {
        text = text.Replace("\\", "\\\\");
        text = text.Replace("[", "\\[");
        text = text.Replace("]", "\\]");
        return text.Replace(";", "\\;");
      }

      private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
      {
        serializedText.Append('[');
        int num = time.IsFixedDateRule ? 1 : 0;
        serializedText.Append(num.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        serializedText.Append(';');
        if (time.IsFixedDateRule)
        {
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", (IFormatProvider) DateTimeFormatInfo.InvariantInfo)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Day.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
        }
        else
        {
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", (IFormatProvider) DateTimeFormatInfo.InvariantInfo)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Week.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
          serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(((int) time.DayOfWeek).ToString((IFormatProvider) CultureInfo.InvariantCulture)));
          serializedText.Append(';');
        }
        serializedText.Append(']');
      }

      private static void VerifyIsEscapableCharacter(char c)
      {
        if ((int) c != 92 && (int) c != 59 && ((int) c != 91 && (int) c != 93))
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", (object) c));
      }

      private void SkipVersionNextDataFields(int depth)
      {
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
        for (int index = this.m_currentTokenStartIndex; index < this.m_serializedText.Length; ++index)
        {
          if (state == TimeZoneInfo.StringSerializer.State.Escaped)
          {
            TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[index]);
            state = TimeZoneInfo.StringSerializer.State.NotEscaped;
          }
          else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
          {
            switch (this.m_serializedText[index])
            {
              case char.MinValue:
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
              case '[':
                ++depth;
                continue;
              case '\\':
                state = TimeZoneInfo.StringSerializer.State.Escaped;
                continue;
              case ']':
                --depth;
                if (depth == 0)
                {
                  this.m_currentTokenStartIndex = index + 1;
                  if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
                  {
                    this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
                    return;
                  }
                  this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
                  return;
                }
                continue;
              default:
                continue;
            }
          }
        }
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
      }

      private string GetNextStringValue(bool canEndWithoutSeparator)
      {
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
        {
          if (canEndWithoutSeparator)
            return (string) null;
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
        StringBuilder sb = StringBuilderCache.Acquire(64);
        for (int index = this.m_currentTokenStartIndex; index < this.m_serializedText.Length; ++index)
        {
          if (state == TimeZoneInfo.StringSerializer.State.Escaped)
          {
            TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[index]);
            sb.Append(this.m_serializedText[index]);
            state = TimeZoneInfo.StringSerializer.State.NotEscaped;
          }
          else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
          {
            switch (this.m_serializedText[index])
            {
              case char.MinValue:
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
              case ';':
                this.m_currentTokenStartIndex = index + 1;
                this.m_state = this.m_currentTokenStartIndex < this.m_serializedText.Length ? TimeZoneInfo.StringSerializer.State.StartOfToken : TimeZoneInfo.StringSerializer.State.EndOfLine;
                return StringBuilderCache.GetStringAndRelease(sb);
              case '[':
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
              case '\\':
                state = TimeZoneInfo.StringSerializer.State.Escaped;
                continue;
              case ']':
                if (!canEndWithoutSeparator)
                  throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
                this.m_currentTokenStartIndex = index;
                this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
                return sb.ToString();
              default:
                sb.Append(this.m_serializedText[index]);
                continue;
            }
          }
        }
        if (state == TimeZoneInfo.StringSerializer.State.Escaped)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", (object) string.Empty));
        if (!canEndWithoutSeparator)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        this.m_currentTokenStartIndex = this.m_serializedText.Length;
        this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
        return StringBuilderCache.GetStringAndRelease(sb);
      }

      private DateTime GetNextDateTimeValue(bool canEndWithoutSeparator, string format)
      {
        DateTime result;
        if (!DateTime.TryParseExact(this.GetNextStringValue(canEndWithoutSeparator), format, (IFormatProvider) DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        return result;
      }

      private TimeSpan GetNextTimeSpanValue(bool canEndWithoutSeparator)
      {
        int nextInt32Value = this.GetNextInt32Value(canEndWithoutSeparator);
        try
        {
          return new TimeSpan(0, nextInt32Value, 0);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
      }

      private int GetNextInt32Value(bool canEndWithoutSeparator)
      {
        int result;
        if (!int.TryParse(this.GetNextStringValue(canEndWithoutSeparator), NumberStyles.AllowLeadingSign, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        return result;
      }

      private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue(bool canEndWithoutSeparator)
      {
        List<TimeZoneInfo.AdjustmentRule> adjustmentRuleList = new List<TimeZoneInfo.AdjustmentRule>(1);
        int num = 0;
        for (TimeZoneInfo.AdjustmentRule adjustmentRuleValue = this.GetNextAdjustmentRuleValue(true); adjustmentRuleValue != null; adjustmentRuleValue = this.GetNextAdjustmentRuleValue(true))
        {
          adjustmentRuleList.Add(adjustmentRuleValue);
          ++num;
        }
        if (!canEndWithoutSeparator)
        {
          if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
            throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
          if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
            throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
        if (num == 0)
          return (TimeZoneInfo.AdjustmentRule[]) null;
        return adjustmentRuleList.ToArray();
      }

      private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue(bool canEndWithoutSeparator)
      {
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
        {
          if (canEndWithoutSeparator)
            return (TimeZoneInfo.AdjustmentRule) null;
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if ((int) this.m_serializedText[this.m_currentTokenStartIndex] == 59)
          return (TimeZoneInfo.AdjustmentRule) null;
        if ((int) this.m_serializedText[this.m_currentTokenStartIndex] != 91)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        this.m_currentTokenStartIndex = this.m_currentTokenStartIndex + 1;
        DateTime nextDateTimeValue1 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
        DateTime nextDateTimeValue2 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
        TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue(false);
        TimeZoneInfo.TransitionTime transitionTimeValue1 = this.GetNextTransitionTimeValue(false);
        TimeZoneInfo.TransitionTime transitionTimeValue2 = this.GetNextTransitionTimeValue(false);
        TimeSpan baseUtcOffsetDelta = TimeSpan.Zero;
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if ((int) this.m_serializedText[this.m_currentTokenStartIndex] >= 48 && (int) this.m_serializedText[this.m_currentTokenStartIndex] <= 57 || ((int) this.m_serializedText[this.m_currentTokenStartIndex] == 45 || (int) this.m_serializedText[this.m_currentTokenStartIndex] == 43))
          baseUtcOffsetDelta = this.GetNextTimeSpanValue(false);
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if ((int) this.m_serializedText[this.m_currentTokenStartIndex] != 93)
          this.SkipVersionNextDataFields(1);
        else
          this.m_currentTokenStartIndex = this.m_currentTokenStartIndex + 1;
        TimeZoneInfo.AdjustmentRule adjustmentRule;
        try
        {
          adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue1, nextDateTimeValue2, nextTimeSpanValue, transitionTimeValue1, transitionTimeValue2, baseUtcOffsetDelta);
        }
        catch (ArgumentException ex)
        {
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
        }
        this.m_state = this.m_currentTokenStartIndex < this.m_serializedText.Length ? TimeZoneInfo.StringSerializer.State.StartOfToken : TimeZoneInfo.StringSerializer.State.EndOfLine;
        return adjustmentRule;
      }

      private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue(bool canEndWithoutSeparator)
      {
        if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex < this.m_serializedText.Length && (int) this.m_serializedText[this.m_currentTokenStartIndex] == 93)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        if ((int) this.m_serializedText[this.m_currentTokenStartIndex] != 91)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        this.m_currentTokenStartIndex = this.m_currentTokenStartIndex + 1;
        int nextInt32Value1 = this.GetNextInt32Value(false);
        switch (nextInt32Value1)
        {
          case 0:
          case 1:
            DateTime timeOfDay = this.GetNextDateTimeValue(false, "HH:mm:ss.FFF");
            timeOfDay = new DateTime(1, 1, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
            int nextInt32Value2 = this.GetNextInt32Value(false);
            TimeZoneInfo.TransitionTime transitionTime;
            if (nextInt32Value1 == 1)
            {
              int nextInt32Value3 = this.GetNextInt32Value(false);
              try
              {
                transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(timeOfDay, nextInt32Value2, nextInt32Value3);
              }
              catch (ArgumentException ex)
              {
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
              }
            }
            else
            {
              int nextInt32Value3 = this.GetNextInt32Value(false);
              int nextInt32Value4 = this.GetNextInt32Value(false);
              try
              {
                transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(timeOfDay, nextInt32Value2, nextInt32Value3, (DayOfWeek) nextInt32Value4);
              }
              catch (ArgumentException ex)
              {
                throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), (Exception) ex);
              }
            }
            if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
              throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
            if ((int) this.m_serializedText[this.m_currentTokenStartIndex] != 93)
              this.SkipVersionNextDataFields(1);
            else
              this.m_currentTokenStartIndex = this.m_currentTokenStartIndex + 1;
            bool flag = false;
            if (this.m_currentTokenStartIndex < this.m_serializedText.Length && (int) this.m_serializedText[this.m_currentTokenStartIndex] == 59)
            {
              this.m_currentTokenStartIndex = this.m_currentTokenStartIndex + 1;
              flag = true;
            }
            if (!flag && !canEndWithoutSeparator)
              throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
            this.m_state = this.m_currentTokenStartIndex < this.m_serializedText.Length ? TimeZoneInfo.StringSerializer.State.StartOfToken : TimeZoneInfo.StringSerializer.State.EndOfLine;
            return transitionTime;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
        }
      }

      private enum State
      {
        Escaped,
        NotEscaped,
        StartOfToken,
        EndOfLine,
      }
    }

    private class TimeZoneInfoComparer : IComparer<TimeZoneInfo>
    {
      int IComparer<TimeZoneInfo>.Compare(TimeZoneInfo x, TimeZoneInfo y)
      {
        int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
        if (num != 0)
          return num;
        return string.Compare(x.DisplayName, y.DisplayName, StringComparison.Ordinal);
      }
    }
  }
}
