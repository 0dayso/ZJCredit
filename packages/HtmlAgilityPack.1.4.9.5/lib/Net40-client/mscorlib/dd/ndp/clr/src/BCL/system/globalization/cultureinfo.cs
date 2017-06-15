// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Globalization
{
  /// <summary>提供有关特定区域性的信息（对于非托管代码开发，则称为“区域设置”）。这些信息包括区域性的名称、书写系统、使用的日历以及对日期和排序字符串的格式化设置。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CultureInfo : ICloneable, IFormatProvider
  {
    private static readonly bool init = CultureInfo.Init();
    [OptionalField(VersionAdded = 1)]
    internal int cultureID = (int) sbyte.MaxValue;
    internal bool m_isReadOnly;
    internal CompareInfo compareInfo;
    internal TextInfo textInfo;
    [NonSerialized]
    internal RegionInfo regionInfo;
    internal NumberFormatInfo numInfo;
    internal DateTimeFormatInfo dateTimeInfo;
    internal Calendar calendar;
    [OptionalField(VersionAdded = 1)]
    internal int m_dataItem;
    [NonSerialized]
    internal CultureData m_cultureData;
    [NonSerialized]
    internal bool m_isInherited;
    [NonSerialized]
    private bool m_isSafeCrossDomain;
    [NonSerialized]
    private int m_createdDomainID;
    [NonSerialized]
    private CultureInfo m_consoleFallbackCulture;
    internal string m_name;
    [NonSerialized]
    private string m_nonSortName;
    [NonSerialized]
    private string m_sortName;
    private static volatile CultureInfo s_userDefaultCulture;
    private static volatile CultureInfo s_InvariantCultureInfo;
    private static volatile CultureInfo s_userDefaultUICulture;
    private static volatile CultureInfo s_InstalledUICultureInfo;
    private static volatile CultureInfo s_DefaultThreadCurrentUICulture;
    private static volatile CultureInfo s_DefaultThreadCurrentCulture;
    private static volatile Hashtable s_LcidCachedCultures;
    private static volatile Hashtable s_NameCachedCultures;
    [SecurityCritical]
    private static volatile WindowsRuntimeResourceManagerBase s_WindowsRuntimeResourceManager;
    [ThreadStatic]
    private static bool ts_IsDoingAppXCultureInfoLookup;
    [NonSerialized]
    private CultureInfo m_parent;
    internal const int LOCALE_NEUTRAL = 0;
    private const int LOCALE_USER_DEFAULT = 1024;
    private const int LOCALE_SYSTEM_DEFAULT = 2048;
    internal const int LOCALE_CUSTOM_DEFAULT = 3072;
    internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;
    internal const int LOCALE_INVARIANT = 127;
    private const int LOCALE_TRADITIONAL_SPANISH = 1034;
    private bool m_useUserOverride;
    private const int LOCALE_SORTID_MASK = 983040;
    private static volatile bool s_isTaiwanSku;
    private static volatile bool s_haveIsTaiwanSku;

    internal bool IsSafeCrossDomain
    {
      get
      {
        return this.m_isSafeCrossDomain;
      }
    }

    internal int CreatedDomainID
    {
      get
      {
        return this.m_createdDomainID;
      }
    }

    /// <summary>获取表示当前线程使用的区域性的 <see cref="T:System.Globalization.CultureInfo" /> 对象。</summary>
    /// <returns>表示当前线程使用的区域性的对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    [__DynamicallyInvokable]
    public static CultureInfo CurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return Thread.CurrentThread.CurrentCulture;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
          return;
        Thread.CurrentThread.CurrentCulture = value;
      }
    }

    internal static CultureInfo UserDefaultCulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_userDefaultCulture;
        if (cultureInfo == null)
        {
          CultureInfo.s_userDefaultCulture = CultureInfo.InvariantCulture;
          cultureInfo = CultureInfo.InitUserDefaultCulture();
          CultureInfo.s_userDefaultCulture = cultureInfo;
        }
        return cultureInfo;
      }
    }

    internal static CultureInfo UserDefaultUICulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_userDefaultUICulture;
        if (cultureInfo == null)
        {
          CultureInfo.s_userDefaultUICulture = CultureInfo.InvariantCulture;
          cultureInfo = CultureInfo.InitUserDefaultUICulture();
          CultureInfo.s_userDefaultUICulture = cultureInfo;
        }
        return cultureInfo;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Globalization.CultureInfo" /> 对象，该对象表示资源管理器在运行时查找区域性特定资源时所用的当前用户接口区域性。</summary>
    /// <returns>资源管理器用于在运行时查找查找区域性特定资源的区域性。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">该属性设置为不能用于定位资源文件的区域性名称。资源文件名可以包含字母、 数字、 连字符或下划线。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static CultureInfo CurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        return Thread.CurrentThread.CurrentUICulture;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
          return;
        Thread.CurrentThread.CurrentUICulture = value;
      }
    }

    /// <summary>获取表示操作系统中安装的区域性的 <see cref="T:System.Globalization.CultureInfo" />。</summary>
    /// <returns>表示操作系统中安装的区域性的 <see cref="T:System.Globalization.CultureInfo" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static CultureInfo InstalledUICulture
    {
      get
      {
        CultureInfo cultureInfo = CultureInfo.s_InstalledUICultureInfo;
        if (cultureInfo == null)
        {
          cultureInfo = CultureInfo.GetCultureByName(CultureInfo.GetSystemDefaultUILanguage(), true) ?? CultureInfo.InvariantCulture;
          cultureInfo.m_isReadOnly = true;
          CultureInfo.s_InstalledUICultureInfo = cultureInfo;
        }
        return cultureInfo;
      }
    }

    /// <summary>获取或设置当前应用程序域中线程的默认区域性。</summary>
    /// <returns>如果当前系统区域性为应用程序域中的默认线程区域性，则为当前应用程序中线程的默认区域性或 null。</returns>
    [__DynamicallyInvokable]
    public static CultureInfo DefaultThreadCurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_DefaultThreadCurrentCulture;
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        CultureInfo.s_DefaultThreadCurrentCulture = value;
      }
    }

    /// <summary>获取或设置当前应用程序域中线程的默认 UI 区域性。</summary>
    /// <returns>如果当前系统 UI 区域性为当前应用程序域中的默认线程 UI 区域性，则当前应用程序域中线程的默认 UI 区域性或 null。</returns>
    /// <exception cref="T:System.ArgumentException">在设置操作中， <see cref="P:System.Globalization.CultureInfo.Name" /> 属性值无效。</exception>
    [__DynamicallyInvokable]
    public static CultureInfo DefaultThreadCurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_DefaultThreadCurrentUICulture;
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        if (value != null)
          CultureInfo.VerifyCultureName(value, true);
        CultureInfo.s_DefaultThreadCurrentUICulture = value;
      }
    }

    /// <summary>获取不依赖于区域性（固定）的 <see cref="T:System.Globalization.CultureInfo" /> 对象。</summary>
    /// <returns>不依赖于区域性（固定）的对象。</returns>
    [__DynamicallyInvokable]
    public static CultureInfo InvariantCulture
    {
      [__DynamicallyInvokable] get
      {
        return CultureInfo.s_InvariantCultureInfo;
      }
    }

    /// <summary>获取表示当前 <see cref="T:System.Globalization.CultureInfo" /> 的父区域性的 <see cref="T:System.Globalization.CultureInfo" />。</summary>
    /// <returns>表示当前 <see cref="T:System.Globalization.CultureInfo" /> 的父区域性的 <see cref="T:System.Globalization.CultureInfo" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual CultureInfo Parent
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.m_parent == null)
        {
          try
          {
            string sparent = this.m_cultureData.SPARENT;
            this.m_parent = !string.IsNullOrEmpty(sparent) ? new CultureInfo(sparent, this.m_cultureData.UseUserOverride) : CultureInfo.InvariantCulture;
          }
          catch (ArgumentException ex)
          {
            this.m_parent = CultureInfo.InvariantCulture;
          }
        }
        return this.m_parent;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.CultureInfo" /> 的区域性标识符。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CultureInfo" /> 的区域性标识符。</returns>
    public virtual int LCID
    {
      get
      {
        return this.m_cultureData.ILANGUAGE;
      }
    }

    /// <summary>获取活动的输入区域设置标识符。</summary>
    /// <returns>一个指定输入区域设置标识符的 32 位的有符号数字。</returns>
    [ComVisible(false)]
    public virtual int KeyboardLayoutId
    {
      get
      {
        return this.m_cultureData.IINPUTLANGUAGEHANDLE;
      }
    }

    /// <summary>获取格式为 languagecode2-country/regioncode2 的区域性名称。</summary>
    /// <returns>格式为 languagecode2-country/regioncode2 的区域性名称。languagecode2 是派生自 ISO 639-1 的小写双字母代码。country/regioncode2 派生自 ISO 3166，一般包含两个大写字母，或一个 BCP-47 语言标记。</returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_nonSortName == null)
        {
          this.m_nonSortName = this.m_cultureData.SNAME;
          if (this.m_nonSortName == null)
            this.m_nonSortName = string.Empty;
        }
        return this.m_nonSortName;
      }
    }

    internal string SortName
    {
      get
      {
        if (this.m_sortName == null)
          this.m_sortName = this.m_cultureData.SCOMPAREINFO;
        return this.m_sortName;
      }
    }

    /// <summary>已否决。获取某种语言的 RFC 4646 标准标识。</summary>
    /// <returns>一个表示某种语言的 RFC 4646 标准标识的字符串。</returns>
    [ComVisible(false)]
    public string IetfLanguageTag
    {
      get
      {
        string name = this.Name;
        if (name == "zh-CHT")
          return "zh-Hant";
        if (name == "zh-CHS")
          return "zh-Hans";
        return this.Name;
      }
    }

    /// <summary>获取完整的本地化区域性名称。</summary>
    /// <returns>格式为 languagefull [country/regionfull] 的完整本地化区域性名称，其中 languagefull 是语言的全名，country/regionfull 是国家/地区的全名。</returns>
    [__DynamicallyInvokable]
    public virtual string DisplayName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SLOCALIZEDDISPLAYNAME;
      }
    }

    /// <summary>获取为区域性设置的显示名称，它由语言、国家/地区以及可选脚本组成。</summary>
    /// <returns>区域性名称。由语言的全名、国家/地区的全名以及可选脚本组成。有关其格式的讨论，请参见对 <see cref="T:System.Globalization.CultureInfo" /> 类的说明。</returns>
    [__DynamicallyInvokable]
    public virtual string NativeName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SNATIVEDISPLAYNAME;
      }
    }

    /// <summary>获取格式为 languagefull [country/regionfull] 的英语区域性名称。</summary>
    /// <returns>格式为 languagefull [country/regionfull] 的英语区域性名称，其中 languagefull 是语言的全名，country/regionfull 是国家/地区的全名。</returns>
    [__DynamicallyInvokable]
    public virtual string EnglishName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SENGDISPLAYNAME;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.CultureInfo" /> 的语言的由两个字母构成的 ISO 639-1 代码。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CultureInfo" /> 的语言的由两个字母构成的 ISO 639-1 代码。</returns>
    [__DynamicallyInvokable]
    public virtual string TwoLetterISOLanguageName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SISO639LANGNAME;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.CultureInfo" /> 的语言的由三个字母构成的 ISO 639-2 代码。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CultureInfo" /> 的语言的由三个字母构成的 ISO 639-2 代码。</returns>
    public virtual string ThreeLetterISOLanguageName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SISO639LANGNAME2;
      }
    }

    /// <summary>获取 Windows API 中定义的由三个字母构成的语言代码。</summary>
    /// <returns>Windows API 中定义的由三个字母构成的语言代码。</returns>
    public virtual string ThreeLetterWindowsLanguageName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SABBREVLANGNAME;
      }
    }

    /// <summary>获取为区域性定义如何比较字符串的 <see cref="T:System.Globalization.CompareInfo" />。</summary>
    /// <returns>为区域性定义如何比较字符串的 <see cref="T:System.Globalization.CompareInfo" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual CompareInfo CompareInfo
    {
      [__DynamicallyInvokable] get
      {
        if (this.compareInfo == null)
        {
          CompareInfo compareInfo = this.UseUserOverride ? CultureInfo.GetCultureInfo(this.m_name).CompareInfo : new CompareInfo(this);
          if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
            return compareInfo;
          this.compareInfo = compareInfo;
        }
        return this.compareInfo;
      }
    }

    private RegionInfo Region
    {
      get
      {
        if (this.regionInfo == null)
          this.regionInfo = new RegionInfo(this.m_cultureData);
        return this.regionInfo;
      }
    }

    /// <summary>获取定义与区域性关联的书写体系的 <see cref="T:System.Globalization.TextInfo" />。</summary>
    /// <returns>定义与区域性关联的书写体系的 <see cref="T:System.Globalization.TextInfo" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual TextInfo TextInfo
    {
      [__DynamicallyInvokable] get
      {
        if (this.textInfo == null)
        {
          TextInfo textInfo = new TextInfo(this.m_cultureData);
          textInfo.SetReadOnlyState(this.m_isReadOnly);
          if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
            return textInfo;
          this.textInfo = textInfo;
        }
        return this.textInfo;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Globalization.CultureInfo" /> 是否表示非特定区域性。</summary>
    /// <returns>如果当前 <see cref="T:System.Globalization.CultureInfo" /> 表示非特定区域性，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsNeutralCulture
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IsNeutralCulture;
      }
    }

    /// <summary>获取属于当前 <see cref="T:System.Globalization.CultureInfo" /> 对象的区域性类型。</summary>
    /// <returns>一个或多个 <see cref="T:System.Globalization.CultureTypes" /> 值的按位组合。没有默认值。</returns>
    [ComVisible(false)]
    public CultureTypes CultureTypes
    {
      get
      {
        CultureTypes cultureTypes = (CultureTypes) 0;
        return (CultureTypes) ((!this.m_cultureData.IsNeutralCulture ? (int) (cultureTypes | CultureTypes.SpecificCultures) : (int) (cultureTypes | CultureTypes.NeutralCultures)) | (this.m_cultureData.IsWin32Installed ? 4 : 0) | (this.m_cultureData.IsFramework ? 64 : 0) | (this.m_cultureData.IsSupplementalCustomCulture ? 8 : 0) | (this.m_cultureData.IsReplacementCulture ? 24 : 0));
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Globalization.NumberFormatInfo" />，它定义适合区域性的、显示数字、货币和百分比的格式。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.NumberFormatInfo" />，它定义适合区域性的、显示数字、货币和百分比的格式。</returns>
    /// <exception cref="T:System.ArgumentNullException">此属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> 属性或任何 <see cref="T:System.Globalization.NumberFormatInfo" /> 设置的属性，与 <see cref="T:System.Globalization.CultureInfo" /> 是只读的。</exception>
    [__DynamicallyInvokable]
    public virtual NumberFormatInfo NumberFormat
    {
      [__DynamicallyInvokable] get
      {
        if (this.numInfo == null)
          this.numInfo = new NumberFormatInfo(this.m_cultureData)
          {
            isReadOnly = this.m_isReadOnly
          };
        return this.numInfo;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        this.numInfo = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Globalization.DateTimeFormatInfo" />，它定义适合区域性的、显示日期和时间的格式。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.DateTimeFormatInfo" />，它定义适合区域性的、显示日期和时间的格式。</returns>
    /// <exception cref="T:System.ArgumentNullException">此属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> 属性或任何 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 设置的属性，与 <see cref="T:System.Globalization.CultureInfo" /> 是只读的。</exception>
    [__DynamicallyInvokable]
    public virtual DateTimeFormatInfo DateTimeFormat
    {
      [__DynamicallyInvokable] get
      {
        if (this.dateTimeInfo == null)
        {
          DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
          dateTimeFormatInfo.m_isReadOnly = this.m_isReadOnly;
          Thread.MemoryBarrier();
          this.dateTimeInfo = dateTimeFormatInfo;
        }
        return this.dateTimeInfo;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        this.dateTimeInfo = value;
      }
    }

    /// <summary>获取区域性使用的默认日历。</summary>
    /// <returns>表示区域性使用的默认日历的 <see cref="T:System.Globalization.Calendar" />。</returns>
    [__DynamicallyInvokable]
    public virtual Calendar Calendar
    {
      [__DynamicallyInvokable] get
      {
        if (this.calendar == null)
        {
          Calendar defaultCalendar = this.m_cultureData.DefaultCalendar;
          Thread.MemoryBarrier();
          defaultCalendar.SetReadOnlyState(this.m_isReadOnly);
          this.calendar = defaultCalendar;
        }
        return this.calendar;
      }
    }

    /// <summary>获取该区域性可使用的日历的列表。</summary>
    /// <returns>类型为 <see cref="T:System.Globalization.Calendar" /> 的数组，表示当前 <see cref="T:System.Globalization.CultureInfo" /> 代表的区域性所使用的日历。</returns>
    [__DynamicallyInvokable]
    public virtual Calendar[] OptionalCalendars
    {
      [__DynamicallyInvokable] get
      {
        int[] calendarIds = this.m_cultureData.CalendarIds;
        Calendar[] calendarArray = new Calendar[calendarIds.Length];
        for (int index = 0; index < calendarArray.Length; ++index)
          calendarArray[index] = CultureInfo.GetCalendarInstance(calendarIds[index]);
        return calendarArray;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Globalization.CultureInfo" /> 对象是否使用用户选定的区域性设置。</summary>
    /// <returns>如果当前 <see cref="T:System.Globalization.CultureInfo" /> 使用用户选定的区域性设置，则为 true；否则为 false。</returns>
    public bool UseUserOverride
    {
      get
      {
        return this.m_cultureData.UseUserOverride;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Globalization.CultureInfo" /> 是否为只读。</summary>
    /// <returns>如果当前 true 为只读，则为 <see cref="T:System.Globalization.CultureInfo" />；否则为 false。默认值为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    internal bool HasInvariantCultureName
    {
      get
      {
        return this.Name == CultureInfo.InvariantCulture.Name;
      }
    }

    internal static bool IsTaiwanSku
    {
      get
      {
        if (!CultureInfo.s_haveIsTaiwanSku)
        {
          CultureInfo.s_isTaiwanSku = CultureInfo.GetSystemDefaultUILanguage() == "zh-TW";
          CultureInfo.s_haveIsTaiwanSku = true;
        }
        return CultureInfo.s_isTaiwanSku;
      }
    }

    /// <summary>根据由名称指定的区域性初始化 <see cref="T:System.Globalization.CultureInfo" /> 类的新实例。</summary>
    /// <param name="name">预定义的 <see cref="T:System.Globalization.CultureInfo" /> 名称、现有 <see cref="T:System.Globalization.CultureInfo" /> 的 <see cref="P:System.Globalization.CultureInfo.Name" /> 或仅 Windows 区域性名称。<paramref name="name" /> 不区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="name" /> 不是有效的区域性名称。有关详细信息，请参阅向调用方部分的说明。</exception>
    [__DynamicallyInvokable]
    public CultureInfo(string name)
      : this(name, true)
    {
    }

    /// <summary>基于名称指定的区域性并基于布尔值（指定是否使用系统中用户选定的区域性设置）来初始化 <see cref="T:System.Globalization.CultureInfo" /> 类的新实例。</summary>
    /// <param name="name">预定义的 <see cref="T:System.Globalization.CultureInfo" /> 名称、现有 <see cref="T:System.Globalization.CultureInfo" /> 的 <see cref="P:System.Globalization.CultureInfo.Name" /> 或仅 Windows 区域性名称。<paramref name="name" /> 不区分大小写。</param>
    /// <param name="useUserOverride">一个布尔值，它指示是使用用户选定的区域性设置 (true)，还是使用默认区域性设置 (false)。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="name" /> 不是有效的区域性名称。请参阅对调用方部分以了解更多信息的说明。</exception>
    public CultureInfo(string name, bool useUserOverride)
    {
      if (name == null)
        throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_String"));
      this.m_cultureData = CultureData.GetCultureData(name, useUserOverride);
      if (this.m_cultureData == null)
        throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
      this.m_name = this.m_cultureData.CultureName;
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
    }

    /// <summary>根据区域性标识符指定的区域性初始化 <see cref="T:System.Globalization.CultureInfo" /> 类的新实例。</summary>
    /// <param name="culture">预定义的 <see cref="T:System.Globalization.CultureInfo" /> 标识符、现有 <see cref="T:System.Globalization.CultureInfo" /> 对象的 <see cref="P:System.Globalization.CultureInfo.LCID" /> 属性或仅 Windows 区域性标识符。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="culture" /> 小于零。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="culture" /> 不是有效的区域性标识符。请参阅对调用方部分以了解更多信息的说明。</exception>
    public CultureInfo(int culture)
      : this(culture, true)
    {
    }

    /// <summary>基于区域性标识符指定的区域性并基于布尔值（指定是否使用系统中用户选定的区域性设置）来初始化 <see cref="T:System.Globalization.CultureInfo" /> 类的新实例。</summary>
    /// <param name="culture">预定义的 <see cref="T:System.Globalization.CultureInfo" /> 标识符、现有 <see cref="T:System.Globalization.CultureInfo" /> 对象的 <see cref="P:System.Globalization.CultureInfo.LCID" /> 属性或仅 Windows 区域性标识符。</param>
    /// <param name="useUserOverride">一个布尔值，它指示是使用用户选定的区域性设置 (true)，还是使用默认区域性设置 (false)。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="culture" /> 小于零。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="culture" /> 不是有效的区域性标识符。请参阅对调用方部分以了解更多信息的说明。</exception>
    public CultureInfo(int culture, bool useUserOverride)
    {
      if (culture < 0)
        throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.InitializeFromCultureId(culture, useUserOverride);
    }

    internal CultureInfo(string cultureName, string textAndCompareCultureName)
    {
      if (cultureName == null)
        throw new ArgumentNullException("cultureName", Environment.GetResourceString("ArgumentNull_String"));
      this.m_cultureData = CultureData.GetCultureData(cultureName, false);
      if (this.m_cultureData == null)
        throw new CultureNotFoundException("cultureName", cultureName, Environment.GetResourceString("Argument_CultureNotSupported"));
      this.m_name = this.m_cultureData.CultureName;
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo(textAndCompareCultureName);
      this.compareInfo = cultureInfo.CompareInfo;
      this.textInfo = cultureInfo.TextInfo;
    }

    private static bool Init()
    {
      if (CultureInfo.s_InvariantCultureInfo == null)
        CultureInfo.s_InvariantCultureInfo = new CultureInfo("", false)
        {
          m_isReadOnly = true
        };
      CultureInfo.s_userDefaultCulture = CultureInfo.s_userDefaultUICulture = CultureInfo.s_InvariantCultureInfo;
      CultureInfo.s_userDefaultCulture = CultureInfo.InitUserDefaultCulture();
      CultureInfo.s_userDefaultUICulture = CultureInfo.InitUserDefaultUICulture();
      return true;
    }

    [SecuritySafeCritical]
    private static CultureInfo InitUserDefaultCulture()
    {
      string defaultLocaleName = CultureInfo.GetDefaultLocaleName(1024);
      if (defaultLocaleName == null)
      {
        defaultLocaleName = CultureInfo.GetDefaultLocaleName(2048);
        if (defaultLocaleName == null)
          return CultureInfo.InvariantCulture;
      }
      CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultLocaleName, true);
      int num = 1;
      cultureByName.m_isReadOnly = num != 0;
      return cultureByName;
    }

    private static CultureInfo InitUserDefaultUICulture()
    {
      string defaultUiLanguage = CultureInfo.GetUserDefaultUILanguage();
      if (defaultUiLanguage == CultureInfo.UserDefaultCulture.Name)
        return CultureInfo.UserDefaultCulture;
      CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultUiLanguage, true);
      if (cultureByName == null)
        return CultureInfo.InvariantCulture;
      cultureByName.m_isReadOnly = true;
      return cultureByName;
    }

    [SecuritySafeCritical]
    internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
    {
      if (CultureInfo.ts_IsDoingAppXCultureInfoLookup)
        return (CultureInfo) null;
      if (AppDomain.IsAppXNGen)
        return (CultureInfo) null;
      try
      {
        CultureInfo.ts_IsDoingAppXCultureInfoLookup = true;
        if (CultureInfo.s_WindowsRuntimeResourceManager == null)
          CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
        return CultureInfo.s_WindowsRuntimeResourceManager.GlobalResourceContextBestFitCultureInfo;
      }
      finally
      {
        CultureInfo.ts_IsDoingAppXCultureInfoLookup = false;
      }
    }

    [SecuritySafeCritical]
    internal static bool SetCultureInfoForUserPreferredLanguageInAppX(CultureInfo ci)
    {
      if (AppDomain.IsAppXNGen)
        return false;
      if (CultureInfo.s_WindowsRuntimeResourceManager == null)
        CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
      return CultureInfo.s_WindowsRuntimeResourceManager.SetGlobalResourceContextDefaultCulture(ci);
    }

    private void InitializeFromCultureId(int culture, bool useUserOverride)
    {
      if (culture <= 1024)
      {
        if (culture != 0 && culture != 1024)
          goto label_4;
      }
      else if (culture != 2048 && culture != 3072 && culture != 4096)
        goto label_4;
      throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
label_4:
      this.m_cultureData = CultureData.GetCultureData(culture, useUserOverride);
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
      this.m_name = this.m_cultureData.CultureName;
    }

    internal static void CheckDomainSafetyObject(object obj, object container)
    {
      if (obj.GetType().Assembly != typeof (CultureInfo).Assembly)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidOperation_SubclassedObject"), (object) obj.GetType(), (object) container.GetType()));
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_name == null || CultureInfo.IsAlternateSortLcid(this.cultureID))
      {
        this.InitializeFromCultureId(this.cultureID, this.m_useUserOverride);
      }
      else
      {
        this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
        if (this.m_cultureData == null)
          throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
      }
      this.m_isInherited = this.GetType() != typeof (CultureInfo);
      if (!(this.GetType().Assembly == typeof (CultureInfo).Assembly))
        return;
      if (this.textInfo != null)
        CultureInfo.CheckDomainSafetyObject((object) this.textInfo, (object) this);
      if (this.compareInfo == null)
        return;
      CultureInfo.CheckDomainSafetyObject((object) this.compareInfo, (object) this);
    }

    private static bool IsAlternateSortLcid(int lcid)
    {
      if (lcid == 1034)
        return true;
      return (uint) (lcid & 983040) > 0U;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.m_name = this.m_cultureData.CultureName;
      this.m_useUserOverride = this.m_cultureData.UseUserOverride;
      this.cultureID = this.m_cultureData.ILANGUAGE;
    }

    internal void StartCrossDomainTracking()
    {
      if (this.m_createdDomainID != 0)
        return;
      if (this.CanSendCrossDomain())
        this.m_isSafeCrossDomain = true;
      Thread.MemoryBarrier();
      this.m_createdDomainID = Thread.GetDomainID();
    }

    internal bool CanSendCrossDomain()
    {
      bool flag = false;
      if (this.GetType() == typeof (CultureInfo))
        flag = true;
      return flag;
    }

    private static CultureInfo GetCultureByName(string name, bool userOverride)
    {
      try
      {
        return userOverride ? new CultureInfo(name) : CultureInfo.GetCultureInfo(name);
      }
      catch (ArgumentException ex)
      {
      }
      return (CultureInfo) null;
    }

    /// <summary>创建表示与指定名称关联的特定区域性的 <see cref="T:System.Globalization.CultureInfo" />。</summary>
    /// <returns>一个表示以下内容的 <see cref="T:System.Globalization.CultureInfo" /> 对象：固定的区域性，如果 <paramref name="name" /> 是空字符串 ("")。- 或 - 与 <paramref name="name" /> 关联的特定区域性，如果 <paramref name="name" /> 是非特定区域性。- 或 - 由 <paramref name="name" /> 指定的区域性，如果 <paramref name="name" /> 已经是特定区域性。</returns>
    /// <param name="name">预定义的 <see cref="T:System.Globalization.CultureInfo" /> 名称或现有 <see cref="T:System.Globalization.CultureInfo" /> 的对象。<paramref name="name" /> 不区分大小写。</param>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="name" /> 不是有效的区域性名称。- 或 - 指定的区域性 <paramref name="name" /> 不具有与其相关联的特定文化。</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="name" /> 为 null。</exception>
    public static CultureInfo CreateSpecificCulture(string name)
    {
      CultureInfo cultureInfo;
      try
      {
        cultureInfo = new CultureInfo(name);
      }
      catch (ArgumentException ex1)
      {
        cultureInfo = (CultureInfo) null;
        for (int length = 0; length < name.Length; ++length)
        {
          if (45 == (int) name[length])
          {
            try
            {
              cultureInfo = new CultureInfo(name.Substring(0, length));
              break;
            }
            catch (ArgumentException ex2)
            {
              throw;
            }
          }
        }
        if (cultureInfo == null)
          throw;
      }
      if (!cultureInfo.IsNeutralCulture)
        return cultureInfo;
      return new CultureInfo(cultureInfo.m_cultureData.SSPECIFICCULTURE);
    }

    internal static bool VerifyCultureName(string cultureName, bool throwException)
    {
      for (int index = 0; index < cultureName.Length; ++index)
      {
        char c = cultureName[index];
        if (!char.IsLetterOrDigit(c) && (int) c != 45 && (int) c != 95)
        {
          if (throwException)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", (object) cultureName));
          return false;
        }
      }
      return true;
    }

    internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
    {
      if (!culture.m_isInherited)
        return true;
      return CultureInfo.VerifyCultureName(culture.Name, throwException);
    }

    /// <summary>获取由指定 <see cref="T:System.Globalization.CultureTypes" /> 参数筛选的支持的区域性列表。</summary>
    /// <returns>一个数组，该数组包含由 <paramref name="types" /> 参数指定的区域性。区域性数组未排序。</returns>
    /// <param name="types">按位组合的枚举值，用于筛选要检索的区域性。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="types" /> 指定了无效的组合 <see cref="T:System.Globalization.CultureTypes" /> 值。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static CultureInfo[] GetCultures(CultureTypes types)
    {
      if ((types & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture)
        types |= CultureTypes.ReplacementCultures;
      return CultureData.GetCultures(types);
    }

    /// <summary>确定指定的对象是否与当前 <see cref="T:System.Globalization.CultureInfo" /> 具有相同的区域性。</summary>
    /// <returns>如果 <paramref name="value" /> 与当前 <see cref="T:System.Globalization.CultureInfo" /> 具有相同的区域性，则为 true；否则为 false。</returns>
    /// <param name="value">将与当前 <see cref="T:System.Globalization.CultureInfo" /> 进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (this == value)
        return true;
      CultureInfo cultureInfo = value as CultureInfo;
      if (cultureInfo != null && this.Name.Equals(cultureInfo.Name))
        return this.CompareInfo.Equals((object) cultureInfo.CompareInfo);
      return false;
    }

    /// <summary>用作当前 <see cref="T:System.Globalization.CultureInfo" /> 的哈希函数，适合用在哈希算法和数据结构（如哈希表）中。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CultureInfo" /> 的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode() + this.CompareInfo.GetHashCode();
    }

    /// <summary>返回一个字符串，该字符串包含当前 <see cref="T:System.Globalization.CultureInfo" /> 的名称，其格式为 languagecode2-country/regioncode2。</summary>
    /// <returns>包含当前 <see cref="T:System.Globalization.CultureInfo" /> 名称的字符串。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.m_name;
    }

    /// <summary>获取一个定义如何格式化指定类型的对象。</summary>
    /// <returns>
    /// <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> 属性的值，如果 <paramref name="formatType" /> 是 <see cref="T:System.Globalization.NumberFormatInfo" /> 类的 <see cref="T:System.Type" /> 对象，则该属性为包含当前 <see cref="T:System.Globalization.CultureInfo" /> 的默认数字格式信息的 <see cref="T:System.Globalization.NumberFormatInfo" />。- 或 - <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> 属性的值，如果 <paramref name="formatType" /> 是 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 类的 <see cref="T:System.Type" /> 对象，则该属性为包含当前 <see cref="T:System.Globalization.CultureInfo" /> 的默认日期和时间格式信息的 <see cref="T:System.Globalization.DateTimeFormatInfo" />。- 或 - 如果 <paramref name="formatType" /> 是其他任何对象，则为 null。</returns>
    /// <param name="formatType">要为其获取格式化对象的 <see cref="T:System.Type" />。此方法仅支持 <see cref="T:System.Globalization.NumberFormatInfo" /> 和 <see cref="T:System.Globalization.DateTimeFormatInfo" /> 两种类型。</param>
    [__DynamicallyInvokable]
    public virtual object GetFormat(Type formatType)
    {
      if (formatType == typeof (NumberFormatInfo))
        return (object) this.NumberFormat;
      if (formatType == typeof (DateTimeFormatInfo))
        return (object) this.DateTimeFormat;
      return (object) null;
    }

    /// <summary>刷新缓存的区域性相关信息。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void ClearCachedData()
    {
      CultureInfo.s_userDefaultUICulture = (CultureInfo) null;
      CultureInfo.s_userDefaultCulture = (CultureInfo) null;
      RegionInfo.s_currentRegionInfo = (RegionInfo) null;
      TimeZone.ResetTimeZone();
      TimeZoneInfo.ClearCachedData();
      CultureInfo.s_LcidCachedCultures = (Hashtable) null;
      CultureInfo.s_NameCachedCultures = (Hashtable) null;
      CultureData.ClearCachedData();
    }

    internal static Calendar GetCalendarInstance(int calType)
    {
      if (calType == 1)
        return (Calendar) new GregorianCalendar();
      return CultureInfo.GetCalendarInstanceRare(calType);
    }

    internal static Calendar GetCalendarInstanceRare(int calType)
    {
      switch (calType)
      {
        case 2:
        case 9:
        case 10:
        case 11:
        case 12:
          return (Calendar) new GregorianCalendar((GregorianCalendarTypes) calType);
        case 3:
          return (Calendar) new JapaneseCalendar();
        case 4:
          return (Calendar) new TaiwanCalendar();
        case 5:
          return (Calendar) new KoreanCalendar();
        case 6:
          return (Calendar) new HijriCalendar();
        case 7:
          return (Calendar) new ThaiBuddhistCalendar();
        case 8:
          return (Calendar) new HebrewCalendar();
        case 14:
          return (Calendar) new JapaneseLunisolarCalendar();
        case 15:
          return (Calendar) new ChineseLunisolarCalendar();
        case 20:
          return (Calendar) new KoreanLunisolarCalendar();
        case 21:
          return (Calendar) new TaiwanLunisolarCalendar();
        case 22:
          return (Calendar) new PersianCalendar();
        case 23:
          return (Calendar) new UmAlQuraCalendar();
        default:
          return (Calendar) new GregorianCalendar();
      }
    }

    /// <summary>如果默认的图形用户界面区域性不合适，则获取适合控制台应用程序的备用用户界面区域性。</summary>
    /// <returns>用于在控制台上读取和显示文本的备用区域性。</returns>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public CultureInfo GetConsoleFallbackUICulture()
    {
      CultureInfo cultureInfo = this.m_consoleFallbackCulture;
      if (cultureInfo == null)
      {
        cultureInfo = CultureInfo.CreateSpecificCulture(this.m_cultureData.SCONSOLEFALLBACKNAME);
        cultureInfo.m_isReadOnly = true;
        this.m_consoleFallbackCulture = cultureInfo;
      }
      return cultureInfo;
    }

    /// <summary>创建当前 <see cref="T:System.Globalization.CultureInfo" /> 的副本。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CultureInfo" /> 的副本。</returns>
    [__DynamicallyInvokable]
    public virtual object Clone()
    {
      CultureInfo cultureInfo = (CultureInfo) this.MemberwiseClone();
      cultureInfo.m_isReadOnly = false;
      if (!this.m_isInherited)
      {
        if (this.dateTimeInfo != null)
          cultureInfo.dateTimeInfo = (DateTimeFormatInfo) this.dateTimeInfo.Clone();
        if (this.numInfo != null)
          cultureInfo.numInfo = (NumberFormatInfo) this.numInfo.Clone();
      }
      else
      {
        cultureInfo.DateTimeFormat = (DateTimeFormatInfo) this.DateTimeFormat.Clone();
        cultureInfo.NumberFormat = (NumberFormatInfo) this.NumberFormat.Clone();
      }
      if (this.textInfo != null)
        cultureInfo.textInfo = (TextInfo) this.textInfo.Clone();
      if (this.calendar != null)
        cultureInfo.calendar = (Calendar) this.calendar.Clone();
      return (object) cultureInfo;
    }

    /// <summary>返回指定的 <see cref="T:System.Globalization.CultureInfo" /> 对象周围的只读包装。</summary>
    /// <returns>
    /// <paramref name="ci" /> 周围的只读 <see cref="T:System.Globalization.CultureInfo" /> 包装。</returns>
    /// <param name="ci">要包装的 <see cref="T:System.Globalization.CultureInfo" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ci" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static CultureInfo ReadOnly(CultureInfo ci)
    {
      if (ci == null)
        throw new ArgumentNullException("ci");
      if (ci.IsReadOnly)
        return ci;
      CultureInfo cultureInfo = (CultureInfo) ci.MemberwiseClone();
      if (!ci.IsNeutralCulture)
      {
        if (!ci.m_isInherited)
        {
          if (ci.dateTimeInfo != null)
            cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(ci.dateTimeInfo);
          if (ci.numInfo != null)
            cultureInfo.numInfo = NumberFormatInfo.ReadOnly(ci.numInfo);
        }
        else
        {
          cultureInfo.DateTimeFormat = DateTimeFormatInfo.ReadOnly(ci.DateTimeFormat);
          cultureInfo.NumberFormat = NumberFormatInfo.ReadOnly(ci.NumberFormat);
        }
      }
      if (ci.textInfo != null)
        cultureInfo.textInfo = TextInfo.ReadOnly(ci.textInfo);
      if (ci.calendar != null)
        cultureInfo.calendar = Calendar.ReadOnly(ci.calendar);
      cultureInfo.m_isReadOnly = true;
      return cultureInfo;
    }

    private void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    internal static CultureInfo GetCultureInfoHelper(int lcid, string name, string altName)
    {
      Hashtable hashtable1 = CultureInfo.s_NameCachedCultures;
      if (name != null)
        name = CultureData.AnsiToLower(name);
      if (altName != null)
        altName = CultureData.AnsiToLower(altName);
      if (hashtable1 == null)
        hashtable1 = Hashtable.Synchronized(new Hashtable());
      else if (lcid == -1)
      {
        CultureInfo cultureInfo = (CultureInfo) hashtable1[(object) (name + "�" + altName)];
        if (cultureInfo != null)
          return cultureInfo;
      }
      else if (lcid == 0)
      {
        CultureInfo cultureInfo = (CultureInfo) hashtable1[(object) name];
        if (cultureInfo != null)
          return cultureInfo;
      }
      Hashtable hashtable2 = CultureInfo.s_LcidCachedCultures;
      if (hashtable2 == null)
        hashtable2 = Hashtable.Synchronized(new Hashtable());
      else if (lcid > 0)
      {
        CultureInfo cultureInfo = (CultureInfo) hashtable2[(object) lcid];
        if (cultureInfo != null)
          return cultureInfo;
      }
      CultureInfo cultureInfo1;
      try
      {
        cultureInfo1 = lcid == -1 ? new CultureInfo(name, altName) : (lcid == 0 ? new CultureInfo(name, false) : new CultureInfo(lcid, false));
      }
      catch (ArgumentException ex)
      {
        return (CultureInfo) null;
      }
      cultureInfo1.m_isReadOnly = true;
      if (lcid == -1)
      {
        hashtable1[(object) (name + "�" + altName)] = (object) cultureInfo1;
        cultureInfo1.TextInfo.SetReadOnlyState(true);
      }
      else
      {
        string lower = CultureData.AnsiToLower(cultureInfo1.m_name);
        hashtable1[(object) lower] = (object) cultureInfo1;
        if ((cultureInfo1.LCID != 4 || !(lower == "zh-hans")) && (cultureInfo1.LCID != 31748 || !(lower == "zh-hant")))
          hashtable2[(object) cultureInfo1.LCID] = (object) cultureInfo1;
      }
      if (-1 != lcid)
        CultureInfo.s_LcidCachedCultures = hashtable2;
      CultureInfo.s_NameCachedCultures = hashtable1;
      return cultureInfo1;
    }

    /// <summary>使用特定的区域性标识符检索某个区域性的缓存的只读实例。</summary>
    /// <returns>只读 <see cref="T:System.Globalization.CultureInfo" /> 对象。</returns>
    /// <param name="culture">区域设置标识符 (LCID)。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="culture" /> 小于零。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="culture" /> 指定不支持的区域性。请参阅对调用方部分以了解更多信息的说明。</exception>
    public static CultureInfo GetCultureInfo(int culture)
    {
      if (culture <= 0)
        throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(culture, (string) null, (string) null);
      if (cultureInfoHelper == null)
        throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
      return cultureInfoHelper;
    }

    /// <summary>使用特定的区域性名称检索某个区域性的缓存的只读实例。</summary>
    /// <returns>只读 <see cref="T:System.Globalization.CultureInfo" /> 对象。</returns>
    /// <param name="name">区域性的名称。<paramref name="name" /> 不区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="name" /> 指定不支持的区域性。请参阅对调用方部分以了解更多信息的说明。</exception>
    public static CultureInfo GetCultureInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(0, name, (string) null);
      if (cultureInfoHelper != null)
        return cultureInfoHelper;
      throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
    }

    /// <summary>检索某个区域性的缓存的只读实例。参数指定了一个使用 <see cref="T:System.Globalization.TextInfo" /> 和 <see cref="T:System.Globalization.CompareInfo" /> 对象进行初始化的区域性，而这些对象则是由另一个区域性指定的。</summary>
    /// <returns>只读 <see cref="T:System.Globalization.CultureInfo" /> 对象。</returns>
    /// <param name="name">区域性的名称。<paramref name="name" /> 不区分大小写。</param>
    /// <param name="altName">区域性的名称提供了 <see cref="T:System.Globalization.TextInfo" /> 和 <see cref="T:System.Globalization.CompareInfo" /> 对象，这些对象用于对 <paramref name="name" /> 进行初始化。<paramref name="altName" /> 不区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="altName" /> 为 null。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="name" /> 或 <paramref name="altName" /> 指定不支持的区域性。请参阅对调用方部分以了解更多信息的说明。</exception>
    public static CultureInfo GetCultureInfo(string name, string altName)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (altName == null)
        throw new ArgumentNullException("altName");
      CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(-1, name, altName);
      if (cultureInfoHelper != null)
        return cultureInfoHelper;
      throw new CultureNotFoundException("name or altName", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_OneOfCulturesNotSupported"), (object) name, (object) altName));
    }

    /// <summary>已否决。检索只读的 <see cref="T:System.Globalization.CultureInfo" /> 对象，其语言特征由指定的 RFC 4646 语言标记标识。</summary>
    /// <returns>只读 <see cref="T:System.Globalization.CultureInfo" /> 对象。</returns>
    /// <param name="name">按 RFC 4646 标准指定的语言的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Globalization.CultureNotFoundException">
    /// <paramref name="name" /> 不对应于支持的区域性。</exception>
    public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
    {
      if (name == "zh-CHT" || name == "zh-CHS")
        throw new CultureNotFoundException("name", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), (object) name));
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
      if (cultureInfo.LCID > (int) ushort.MaxValue || cultureInfo.LCID == 1034)
        throw new CultureNotFoundException("name", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), (object) name));
      return cultureInfo;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string nativeGetLocaleInfoEx(string localeName, uint field);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeGetLocaleInfoExInt(string localeName, uint field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool nativeSetThreadLocale(string localeName);

    [SecurityCritical]
    private static string GetDefaultLocaleName(int localeType)
    {
      string s = (string) null;
      if (CultureInfo.InternalGetDefaultLocaleName(localeType, JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      return string.Empty;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetDefaultLocaleName(int localetype, StringHandleOnStack localeString);

    [SecuritySafeCritical]
    private static string GetUserDefaultUILanguage()
    {
      string s = (string) null;
      if (CultureInfo.InternalGetUserDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      return string.Empty;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetUserDefaultUILanguage(StringHandleOnStack userDefaultUiLanguage);

    [SecuritySafeCritical]
    private static string GetSystemDefaultUILanguage()
    {
      string s = (string) null;
      if (CultureInfo.InternalGetSystemDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref s)))
        return s;
      return string.Empty;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetSystemDefaultUILanguage(StringHandleOnStack systemDefaultUiLanguage);
  }
}
