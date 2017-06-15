// Decompiled with JetBrains decompiler
// Type: System.Globalization.RegionInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>包含国家/地区的相关信息。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class RegionInfo
  {
    private static readonly int[] IdFromEverettRegionInfoDataItem = new int[110]{ 14337, 1052, 1067, 11274, 3079, 3081, 1068, 2060, 1026, 15361, 2110, 16394, 1046, 1059, 10249, 3084, 9225, 2055, 13322, 2052, 9226, 5130, 1029, 1031, 1030, 7178, 5121, 12298, 1061, 3073, 1027, 1035, 1080, 1036, 2057, 1079, 1032, 4106, 3076, 18442, 1050, 1038, 1057, 6153, 1037, 1081, 2049, 1065, 1039, 1040, 8201, 11265, 1041, 1089, 1088, 1042, 13313, 1087, 12289, 5127, 1063, 4103, 1062, 4097, 6145, 6156, 1071, 1104, 5124, 1125, 2058, 1086, 19466, 1043, 1044, 5129, 8193, 6154, 10250, 13321, 1056, 1045, 20490, 2070, 15370, 16385, 1048, 1049, 1025, 1053, 4100, 1060, 1051, 2074, 17418, 1114, 1054, 7169, 1055, 11273, 1028, 1058, 1033, 14346, 1091, 8202, 1066, 9217, 1078, 12297 };
    internal string m_name;
    [NonSerialized]
    internal CultureData m_cultureData;
    internal static volatile RegionInfo s_currentRegionInfo;
    [OptionalField(VersionAdded = 2)]
    private int m_cultureId;
    [OptionalField(VersionAdded = 2)]
    internal int m_dataItem;

    /// <summary>获取表示当前线程所使用的国家/地区的 <see cref="T:System.Globalization.RegionInfo" />。</summary>
    /// <returns>表示当前线程所使用的国家/地区的 <see cref="T:System.Globalization.RegionInfo" />。</returns>
    [__DynamicallyInvokable]
    public static RegionInfo CurrentRegion
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        RegionInfo regionInfo1 = RegionInfo.s_currentRegionInfo;
        if (regionInfo1 == null)
        {
          regionInfo1 = new RegionInfo(CultureInfo.CurrentCulture.m_cultureData);
          RegionInfo regionInfo2 = regionInfo1;
          string sregionname = regionInfo2.m_cultureData.SREGIONNAME;
          regionInfo2.m_name = sregionname;
          RegionInfo.s_currentRegionInfo = regionInfo1;
        }
        return regionInfo1;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.RegionInfo" /> 对象的名称或 ISO 3166 双字母国家/地区代码。</summary>
    /// <returns>
    /// <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> 构造函数的 <paramref name="name" /> 参数指定的值。返回值为大写。- 或 -在 ISO 3166 中定义的且由 <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> 构造函数的 <paramref name="culture" /> 参数指定的国家/地区的双字母代码。返回值为大写。</returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.m_name;
      }
    }

    /// <summary>获取以英文表示的国家/地区的全名。</summary>
    /// <returns>以英文表示的国家/地区的全名。</returns>
    [__DynamicallyInvokable]
    public virtual string EnglishName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SENGCOUNTRY;
      }
    }

    /// <summary>获取以 .NET Framework 本地化版本语言表示的国家/地区的全名。</summary>
    /// <returns>以 .NET Framework 本地化版本语言表示的国家/地区的全名。</returns>
    [__DynamicallyInvokable]
    public virtual string DisplayName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SLOCALIZEDCOUNTRY;
      }
    }

    /// <summary>获取一个国家/地区的名称，它使用该国家/地区的本地语言格式表示。</summary>
    /// <returns>该国家/地区的本地名称，它使用与 ISO 3166 国家/地区代码关联的语言格式表示。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual string NativeName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SNATIVECOUNTRY;
      }
    }

    /// <summary>获取在 ISO 3166 中定义的由两个字母组成的国家/地区代码。</summary>
    /// <returns>在 ISO 3166 中定义的由两个字母组成的国家/地区代码。</returns>
    [__DynamicallyInvokable]
    public virtual string TwoLetterISORegionName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SISO3166CTRYNAME;
      }
    }

    /// <summary>获取在 ISO 3166 中定义的由三个字母组成的国家/地区代码。</summary>
    /// <returns>在 ISO 3166 中定义的由三个字母组成的国家/地区代码。</returns>
    public virtual string ThreeLetterISORegionName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SISO3166CTRYNAME2;
      }
    }

    /// <summary>获取 Windows 分配给此 <see cref="T:System.Globalization.RegionInfo" /> 所表示国家/地区的由三个字母组成的代码。</summary>
    /// <returns>Windows 分配给此 <see cref="T:System.Globalization.RegionInfo" /> 所表示国家/地区的由三个字母组成的代码。</returns>
    public virtual string ThreeLetterWindowsRegionName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SABBREVCTRYNAME;
      }
    }

    /// <summary>获取一个值，该值指示该国家/地区是否使用公制进行度量。</summary>
    /// <returns>如果该国家/地区使用公制进行度量，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsMetric
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IMEASURE == 0;
      }
    }

    /// <summary>获取地理区域、国家/地区、城市或地点的唯一标识号。</summary>
    /// <returns>一个唯一标识地理位置的 32 位有符号数字。</returns>
    [ComVisible(false)]
    public virtual int GeoId
    {
      get
      {
        return this.m_cultureData.IGEOID;
      }
    }

    /// <summary>获取该国家/地区中使用的货币的英文名称。</summary>
    /// <returns>该国家/地区中使用的货币的英文名称。</returns>
    [ComVisible(false)]
    public virtual string CurrencyEnglishName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SENGLISHCURRENCY;
      }
    }

    /// <summary>获取该国家/地区中使用的货币名称，它使用该国家/地区的本地语言格式表示。</summary>
    /// <returns>该国家/地区中使用的货币的本地名称，它使用与 ISO 3166 国家/地区代码关联的语言格式表示。</returns>
    [ComVisible(false)]
    public virtual string CurrencyNativeName
    {
      [SecuritySafeCritical] get
      {
        return this.m_cultureData.SNATIVECURRENCY;
      }
    }

    /// <summary>获取与国家/地区关联的货币符号。</summary>
    /// <returns>与国家/地区关联的货币符号。</returns>
    [__DynamicallyInvokable]
    public virtual string CurrencySymbol
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SCURRENCY;
      }
    }

    /// <summary>获取与国家/地区关联的由三个字符组成的 ISO 4217 货币符号。</summary>
    /// <returns>与国家/地区关联的由三个字符组成的 ISO 4217 货币符号。</returns>
    [__DynamicallyInvokable]
    public virtual string ISOCurrencySymbol
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return this.m_cultureData.SINTLSYMBOL;
      }
    }

    /// <summary>基于按名称指定的国家/地区或特定区域性初始化 <see cref="T:System.Globalization.RegionInfo" /> 类的新实例。</summary>
    /// <param name="name">包含 ISO 3166 中为国家/地区定义的由两个字母组成的代码的字符串。- 或 -包含特定区域性、自定义区域性或仅适用于 Windows 的区域性的区域性名称的字符串。如果区域性名称未采用 RFC 4646 格式，应用程序应指定整个区域性名称，而不是仅指定国家/地区。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> is not a valid country/region name or specific culture name.</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public RegionInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
      this.m_cultureData = CultureData.GetCultureDataForRegion(name, true);
      if (this.m_cultureData == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), (object) name), "name");
      if (this.m_cultureData.IsNeutralCulture)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNeutralRegionName", (object) name), "name");
      this.SetName(name);
    }

    /// <summary>基于与指定的区域性标识符关联的国家/地区初始化 <see cref="T:System.Globalization.RegionInfo" /> 类的新实例。</summary>
    /// <param name="culture">区域性标识符。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="culture" /> specifies either an invariant, custom, or neutral culture.</exception>
    [SecuritySafeCritical]
    public RegionInfo(int culture)
    {
      if (culture == (int) sbyte.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
      if (culture == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", (object) culture), "culture");
      if (culture == 3072)
        throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", (object) culture), "culture");
      this.m_cultureData = CultureData.GetCultureData(culture, true);
      this.m_name = this.m_cultureData.SREGIONNAME;
      if (this.m_cultureData.IsNeutralCulture)
        throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", (object) culture), "culture");
      this.m_cultureId = culture;
    }

    [SecuritySafeCritical]
    internal RegionInfo(CultureData cultureData)
    {
      this.m_cultureData = cultureData;
      this.m_name = this.m_cultureData.SREGIONNAME;
    }

    [SecurityCritical]
    private void SetName(string name)
    {
      this.m_name = name.Equals(this.m_cultureData.SREGIONNAME, StringComparison.OrdinalIgnoreCase) ? this.m_cultureData.SREGIONNAME : this.m_cultureData.CultureName;
    }

    [SecurityCritical]
    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_name == null)
        this.m_cultureId = RegionInfo.IdFromEverettRegionInfoDataItem[this.m_dataItem];
      this.m_cultureData = this.m_cultureId != 0 ? CultureData.GetCultureData(this.m_cultureId, true) : CultureData.GetCultureDataForRegion(this.m_name, true);
      if (this.m_cultureData == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), (object) this.m_name), "m_name");
      if (this.m_cultureId == 0)
        this.SetName(this.m_name);
      else
        this.m_name = this.m_cultureData.SREGIONNAME;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
    }

    /// <summary>确定指定对象与当前 <see cref="T:System.Globalization.RegionInfo" /> 对象是否为同一实例。</summary>
    /// <returns>如果 <paramref name="value" /> 参数是一个 <see cref="T:System.Globalization.RegionInfo" /> 对象并且其 <see cref="P:System.Globalization.RegionInfo.Name" /> 属性与当前 <see cref="T:System.Globalization.RegionInfo" /> 对象的 <see cref="P:System.Globalization.RegionInfo.Name" /> 属性相同，则为 true；否则为 false。</returns>
    /// <param name="value">将与当前 <see cref="T:System.Globalization.RegionInfo" /> 进行比较的对象。 </param>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      RegionInfo regionInfo = value as RegionInfo;
      if (regionInfo != null)
        return this.Name.Equals(regionInfo.Name);
      return false;
    }

    /// <summary>用作当前 <see cref="T:System.Globalization.RegionInfo" /> 的哈希函数，适合用在哈希算法和数据结构（如哈希表）中。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.RegionInfo" /> 的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    /// <summary>返回一个字符串，它包含为当前 <see cref="T:System.Globalization.RegionInfo" /> 指定的区域性名称或 ISO 3166 双字母国家/地区代码。</summary>
    /// <returns>一个字符串，它包含为当前 <see cref="T:System.Globalization.RegionInfo" /> 定义的区域性名称或 ISO 3166 双字母国家/地区代码。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.Name;
    }
  }
}
