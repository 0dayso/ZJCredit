// Decompiled with JetBrains decompiler
// Type: System.Globalization.CompareInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>实现用于区分区域性的字符串的一组方法。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CompareInfo : IDeserializationCallback
  {
    private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
    private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);
    private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
    [OptionalField(VersionAdded = 2)]
    private string m_name;
    [NonSerialized]
    private string m_sortName;
    [NonSerialized]
    private IntPtr m_dataHandle;
    [NonSerialized]
    private IntPtr m_handleOrigin;
    [OptionalField(VersionAdded = 1)]
    private int win32LCID;
    private int culture;
    private const int LINGUISTIC_IGNORECASE = 16;
    private const int NORM_IGNORECASE = 1;
    private const int NORM_IGNOREKANATYPE = 65536;
    private const int LINGUISTIC_IGNOREDIACRITIC = 32;
    private const int NORM_IGNORENONSPACE = 2;
    private const int NORM_IGNORESYMBOLS = 4;
    private const int NORM_IGNOREWIDTH = 131072;
    private const int SORT_STRINGSORT = 4096;
    private const int COMPARE_OPTIONS_ORDINAL = 1073741824;
    internal const int NORM_LINGUISTIC_CASING = 134217728;
    private const int RESERVED_FIND_ASCII_STRING = 536870912;
    private const int SORT_VERSION_WHIDBEY = 4096;
    private const int SORT_VERSION_V4 = 393473;
    [OptionalField(VersionAdded = 3)]
    private SortVersion m_SortVersion;

    /// <summary>获取用于通过 <see cref="T:System.Globalization.CompareInfo" /> 对象执行排序操作的区域性的名称。</summary>
    /// <returns>区域性的名称。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
          return this.m_name;
        return this.m_sortName;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.CompareInfo" /> 的格式正确的区域性标识符。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CompareInfo" /> 的格式正确的区域性标识符。</returns>
    public int LCID
    {
      get
      {
        return CultureInfo.GetCultureInfo(this.Name).LCID;
      }
    }

    internal static bool IsLegacy20SortingBehaviorRequested
    {
      get
      {
        return (int) CompareInfo.InternalSortVersion == 4096;
      }
    }

    private static uint InternalSortVersion
    {
      [SecuritySafeCritical] get
      {
        return CompareInfo.InternalGetSortVersion();
      }
    }

    /// <summary>获取用于比较和排序字符串的 Unicode 版本的相关信息。</summary>
    /// <returns>包含用于比较和排序字符串的 Unicode 版本的相关信息的对象。</returns>
    public SortVersion Version
    {
      [SecuritySafeCritical] get
      {
        if (this.m_SortVersion == (SortVersion) null)
        {
          Win32Native.NlsVersionInfoEx lpNlsVersionInformation = new Win32Native.NlsVersionInfoEx();
          lpNlsVersionInformation.dwNLSVersionInfoSize = Marshal.SizeOf(typeof (Win32Native.NlsVersionInfoEx));
          CompareInfo.InternalGetNlsVersionEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, ref lpNlsVersionInformation);
          this.m_SortVersion = new SortVersion(lpNlsVersionInformation.dwNLSVersion, lpNlsVersionInformation.dwEffectiveId != 0 ? lpNlsVersionInformation.dwEffectiveId : this.LCID, lpNlsVersionInformation.guidCustomVersion);
        }
        return this.m_SortVersion;
      }
    }

    internal CompareInfo(CultureInfo culture)
    {
      this.m_name = culture.m_name;
      this.m_sortName = culture.SortName;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_sortName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    /// <summary>初始化一个新的 <see cref="T:System.Globalization.CompareInfo" /> 对象，该对象与指定区域性关联，并使用指定 <see cref="T:System.Reflection.Assembly" /> 中的字符串比较方法。</summary>
    /// <returns>一个新 <see cref="T:System.Globalization.CompareInfo" /> 对象，它与具有指定标识符的区域性关联，并使用当前 <see cref="T:System.Reflection.Assembly" /> 中的字符串比较方法。</returns>
    /// <param name="culture">表示区域性标识符的整数。</param>
    /// <param name="assembly">一个 <see cref="T:System.Reflection.Assembly" />，它包含将使用的字符串比较方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assembly" /> 属于无效类型。</exception>
    public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException("assembly");
      if (assembly != typeof (object).Module.Assembly)
        throw new ArgumentException(Environment.GetResourceString("Argument_OnlyMscorlib"));
      return CompareInfo.GetCompareInfo(culture);
    }

    /// <summary>初始化一个新的 <see cref="T:System.Globalization.CompareInfo" /> 对象，该对象与指定区域性关联，并使用指定 <see cref="T:System.Reflection.Assembly" /> 中的字符串比较方法。</summary>
    /// <returns>一个新 <see cref="T:System.Globalization.CompareInfo" /> 对象，它与具有指定标识符的区域性关联，并使用当前 <see cref="T:System.Reflection.Assembly" /> 中的字符串比较方法。</returns>
    /// <param name="name">表示区域性名称的字符串。</param>
    /// <param name="assembly">一个 <see cref="T:System.Reflection.Assembly" />，它包含将使用的字符串比较方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="assembly" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是无效的区域性名称。- 或 - <paramref name="assembly" /> 属于无效类型。</exception>
    public static CompareInfo GetCompareInfo(string name, Assembly assembly)
    {
      if (name == null || assembly == (Assembly) null)
        throw new ArgumentNullException(name == null ? "name" : "assembly");
      if (assembly != typeof (object).Module.Assembly)
        throw new ArgumentException(Environment.GetResourceString("Argument_OnlyMscorlib"));
      return CompareInfo.GetCompareInfo(name);
    }

    /// <summary>初始化与具有指定标识符的区域性关联的新 <see cref="T:System.Globalization.CompareInfo" /> 对象。</summary>
    /// <returns>一个新 <see cref="T:System.Globalization.CompareInfo" /> 对象，它与具有指定标识符的区域性关联，并使用当前 <see cref="T:System.Reflection.Assembly" /> 中的字符串比较方法。</returns>
    /// <param name="culture">表示区域性标识符的整数。</param>
    public static CompareInfo GetCompareInfo(int culture)
    {
      if (CultureData.IsCustomCultureId(culture))
        throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", (object) "culture"));
      return CultureInfo.GetCultureInfo(culture).CompareInfo;
    }

    /// <summary>初始化与具有指定名称的区域性关联的新 <see cref="T:System.Globalization.CompareInfo" /> 对象。</summary>
    /// <returns>一个新 <see cref="T:System.Globalization.CompareInfo" /> 对象，它与具有指定标识符的区域性关联，并使用当前 <see cref="T:System.Reflection.Assembly" /> 中的字符串比较方法。</returns>
    /// <param name="name">表示区域性名称的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是无效的区域性名称。</exception>
    [__DynamicallyInvokable]
    public static CompareInfo GetCompareInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      return CultureInfo.GetCultureInfo(name).CompareInfo;
    }

    /// <summary>指示指定的 Unicode 字符是否可排序。</summary>
    /// <returns>如果 <paramref name="ch" /> 参数可排序，则为 true；否则为 false。</returns>
    /// <param name="ch">一个 Unicode 字符。</param>
    [ComVisible(false)]
    public static bool IsSortable(char ch)
    {
      return CompareInfo.IsSortable(ch.ToString());
    }

    /// <summary>指示指定的 Unicode 字符串是否可排序。</summary>
    /// <returns>如果 <paramref name="str" /> 参数不是空字符串 ("") 且 <paramref name="str" /> 中的所有 Unicode 字符都是可排序的，则为 true；否则为 false。</returns>
    /// <param name="text">由 0 或更多 Unicode 字符组成的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static bool IsSortable(string text)
    {
      if (text == null)
        throw new ArgumentNullException("text");
      if (text.Length == 0)
        return false;
      CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
      IntPtr handle = compareInfo.m_dataHandle;
      IntPtr handleOrigin = compareInfo.m_handleOrigin;
      string localeName = compareInfo.m_sortName;
      string source = text;
      int length = source.Length;
      return CompareInfo.InternalIsSortable(handle, handleOrigin, localeName, source, length);
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_name = (string) null;
    }

    private void OnDeserialized()
    {
      CultureInfo cultureInfo;
      if (this.m_name == null)
      {
        cultureInfo = CultureInfo.GetCultureInfo(this.culture);
        this.m_name = cultureInfo.m_name;
      }
      else
        cultureInfo = CultureInfo.GetCultureInfo(this.m_name);
      this.m_sortName = cultureInfo.SortName;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_sortName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.OnDeserialized();
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      this.OnDeserialized();
    }

    internal static int GetNativeCompareFlags(CompareOptions options)
    {
      int num = 134217728;
      if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
        num |= 1;
      if ((options & CompareOptions.IgnoreKanaType) != CompareOptions.None)
        num |= 65536;
      if ((options & CompareOptions.IgnoreNonSpace) != CompareOptions.None)
        num |= 2;
      if ((options & CompareOptions.IgnoreSymbols) != CompareOptions.None)
        num |= 4;
      if ((options & CompareOptions.IgnoreWidth) != CompareOptions.None)
        num |= 131072;
      if ((options & CompareOptions.StringSort) != CompareOptions.None)
        num |= 4096;
      if (options == CompareOptions.Ordinal)
        num = 1073741824;
      return num;
    }

    /// <summary>比较两个字符串。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值条件零这两个字符串相等。小于零 <paramref name="string1" /> 小于 <paramref name="string2" />。大于零 <paramref name="string1" /> 大于 <paramref name="string2" />。</returns>
    /// <param name="string1">要比较的第一个字符串。</param>
    /// <param name="string2">要比较的第二个字符串。</param>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, string string2)
    {
      return this.Compare(string1, string2, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值比较两个字符串。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值条件零这两个字符串相等。小于零 <paramref name="string1" /> 小于 <paramref name="string2" />。大于零 <paramref name="string1" /> 大于 <paramref name="string2" />。 </returns>
    /// <param name="string1">要比较的第一个字符串。</param>
    /// <param name="string2">要比较的第二个字符串。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="string1" /> 和 <paramref name="string2" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />、<see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" /> 和 <see cref="F:System.Globalization.CompareOptions.StringSort" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, string string2, CompareOptions options)
    {
      if (options == CompareOptions.OrdinalIgnoreCase)
        return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
      if ((options & CompareOptions.Ordinal) != CompareOptions.None)
      {
        if (options != CompareOptions.Ordinal)
          throw new ArgumentException(Environment.GetResourceString("Argument_CompareOptionOrdinal"), "options");
        return string.CompareOrdinal(string1, string2);
      }
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      if (string1 == null)
        return string2 == null ? 0 : -1;
      if (string2 == null)
        return 1;
      return CompareInfo.InternalCompareString(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, string1, 0, string1.Length, string2, 0, string2.Length, CompareInfo.GetNativeCompareFlags(options));
    }

    /// <summary>将一个字符串的一部分与另一个字符串的一部分相比较。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值条件零这两个字符串相等。小于零<paramref name="string1" /> 的指定部分小于 <paramref name="string2" /> 的指定部分。大于零<paramref name="string1" /> 的指定部分大于 <paramref name="string2" /> 的指定部分。 </returns>
    /// <param name="string1">要比较的第一个字符串。</param>
    /// <param name="offset1">
    /// <paramref name="string1" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="length1">
    /// <paramref name="string1" /> 中要比较的连续字符数。</param>
    /// <param name="string2">要比较的第二个字符串。</param>
    /// <param name="offset2">
    /// <paramref name="string2" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="length2">
    /// <paramref name="string2" /> 中要比较的连续字符数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset1" />、<paramref name="length1" />、<paramref name="offset2" /> 或 <paramref name="length2" /> 小于零。- 或 - <paramref name="offset1" /> 大于或等于 <paramref name="string1" /> 中的字符数。- 或 - <paramref name="offset2" /> 大于或等于 <paramref name="string2" /> 中的字符数。- 或 - <paramref name="length1" /> 大于从 <paramref name="offset1" /> 到 <paramref name="string1" /> 末尾的字符数。- 或 - <paramref name="length2" /> 大于从 <paramref name="offset2" /> 到 <paramref name="string2" /> 末尾的字符数。</exception>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
    {
      return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值将一个字符串的结尾部分与另一个字符串的结尾部分相比较。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值条件零这两个字符串相等。小于零<paramref name="string1" /> 的指定部分小于 <paramref name="string2" /> 的指定部分。大于零<paramref name="string1" /> 的指定部分大于 <paramref name="string2" /> 的指定部分。 </returns>
    /// <param name="string1">要比较的第一个字符串。</param>
    /// <param name="offset1">
    /// <paramref name="string1" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="string2">要比较的第二个字符串。</param>
    /// <param name="offset2">
    /// <paramref name="string2" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="string1" /> 和 <paramref name="string2" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />、<see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" /> 和 <see cref="F:System.Globalization.CompareOptions.StringSort" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset1" /> 或 <paramref name="offset2" /> 小于零。- 或 - <paramref name="offset1" /> 大于或等于 <paramref name="string1" /> 中的字符数。- 或 - <paramref name="offset2" /> 大于或等于 <paramref name="string2" /> 中的字符数。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
    {
      return this.Compare(string1, offset1, string1 == null ? 0 : string1.Length - offset1, string2, offset2, string2 == null ? 0 : string2.Length - offset2, options);
    }

    /// <summary>将一个字符串的结尾部分与另一个字符串的结尾部分相比较。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值条件零这两个字符串相等。小于零<paramref name="string1" /> 的指定部分小于 <paramref name="string2" /> 的指定部分。大于零<paramref name="string1" /> 的指定部分大于 <paramref name="string2" /> 的指定部分。 </returns>
    /// <param name="string1">要比较的第一个字符串。</param>
    /// <param name="offset1">
    /// <paramref name="string1" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="string2">要比较的第二个字符串。</param>
    /// <param name="offset2">
    /// <paramref name="string2" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset1" /> 或 <paramref name="offset2" /> 小于零。- 或 - <paramref name="offset1" /> 大于或等于 <paramref name="string1" /> 中的字符数。- 或 - <paramref name="offset2" /> 大于或等于 <paramref name="string2" /> 中的字符数。</exception>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, string string2, int offset2)
    {
      return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值将一个字符串的一部分与另一个字符串的一部分相比较。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值条件零这两个字符串相等。小于零<paramref name="string1" /> 的指定部分小于 <paramref name="string2" /> 的指定部分。大于零<paramref name="string1" /> 的指定部分大于 <paramref name="string2" /> 的指定部分。 </returns>
    /// <param name="string1">要比较的第一个字符串。</param>
    /// <param name="offset1">
    /// <paramref name="string1" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="length1">
    /// <paramref name="string1" /> 中要比较的连续字符数。</param>
    /// <param name="string2">要比较的第二个字符串。</param>
    /// <param name="offset2">
    /// <paramref name="string2" /> 中的字符从零开始的索引，将从此位置开始比较。</param>
    /// <param name="length2">
    /// <paramref name="string2" /> 中要比较的连续字符数。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="string1" /> 和 <paramref name="string2" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />、<see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" /> 和 <see cref="F:System.Globalization.CompareOptions.StringSort" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset1" />、<paramref name="length1" />、<paramref name="offset2" /> 或 <paramref name="length2" /> 小于零。- 或 - <paramref name="offset1" /> 大于或等于 <paramref name="string1" /> 中的字符数。- 或 - <paramref name="offset2" /> 大于或等于 <paramref name="string2" /> 中的字符数。- 或 - <paramref name="length1" /> 大于从 <paramref name="offset1" /> 到 <paramref name="string1" /> 末尾的字符数。- 或 - <paramref name="length2" /> 大于从 <paramref name="offset2" /> 到 <paramref name="string2" /> 末尾的字符数。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
    {
      if (options == CompareOptions.OrdinalIgnoreCase)
      {
        int num = string.Compare(string1, offset1, string2, offset2, length1 < length2 ? length1 : length2, StringComparison.OrdinalIgnoreCase);
        if (length1 == length2 || num != 0)
          return num;
        return length1 <= length2 ? -1 : 1;
      }
      if (length1 < 0 || length2 < 0)
        throw new ArgumentOutOfRangeException(length1 < 0 ? "length1" : "length2", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (offset1 < 0 || offset2 < 0)
        throw new ArgumentOutOfRangeException(offset1 < 0 ? "offset1" : "offset2", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (offset1 > (string1 == null ? 0 : string1.Length) - length1)
        throw new ArgumentOutOfRangeException("string1", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      if (offset2 > (string2 == null ? 0 : string2.Length) - length2)
        throw new ArgumentOutOfRangeException("string2", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      if ((options & CompareOptions.Ordinal) != CompareOptions.None)
      {
        if (options != CompareOptions.Ordinal)
          throw new ArgumentException(Environment.GetResourceString("Argument_CompareOptionOrdinal"), "options");
      }
      else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      if (string1 == null)
        return string2 == null ? 0 : -1;
      if (string2 == null)
        return 1;
      if (options == CompareOptions.Ordinal)
        return CompareInfo.CompareOrdinal(string1, offset1, length1, string2, offset2, length2);
      return CompareInfo.InternalCompareString(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, string1, offset1, length1, string2, offset2, length2, CompareInfo.GetNativeCompareFlags(options));
    }

    [SecurityCritical]
    private static int CompareOrdinal(string string1, int offset1, int length1, string string2, int offset2, int length2)
    {
      int num = string.nativeCompareOrdinalEx(string1, offset1, string2, offset2, length1 < length2 ? length1 : length2);
      if (length1 == length2 || num != 0)
        return num;
      return length1 <= length2 ? -1 : 1;
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值确定指定的源字符串是否以指定的前缀开头。</summary>
    /// <returns>如果 <paramref name="prefix" /> 的长度小于或等于 <paramref name="source" /> 的长度，并且 <paramref name="source" /> 以 <paramref name="prefix" /> 开始，则为 true；否则为 false。</returns>
    /// <param name="source">要在其中搜索的字符串。</param>
    /// <param name="prefix">要与 <paramref name="source" /> 的开头进行比较的字符串。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="prefix" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="prefix" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
    {
      if (source == null || prefix == null)
        throw new ArgumentNullException(source == null ? "source" : "prefix", Environment.GetResourceString("ArgumentNull_String"));
      if (prefix.Length == 0)
        return true;
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
      if (options == CompareOptions.Ordinal)
        return source.StartsWith(prefix, StringComparison.Ordinal);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      IntPtr handle = this.m_dataHandle;
      IntPtr handleOrigin = this.m_handleOrigin;
      string localeName = this.m_sortName;
      int flags = CompareInfo.GetNativeCompareFlags(options) | 1048576 | (!source.IsAscii() || !prefix.IsAscii() ? 0 : 536870912);
      string source1 = source;
      int length1 = source1.Length;
      int startIndex = 0;
      string target = prefix;
      int length2 = target.Length;
      return CompareInfo.InternalFindNLSStringEx(handle, handleOrigin, localeName, flags, source1, length1, startIndex, target, length2) > -1;
    }

    /// <summary>确定指定的源字符串是否以指定的前缀开头。</summary>
    /// <returns>如果 <paramref name="prefix" /> 的长度小于或等于 <paramref name="source" /> 的长度，并且 <paramref name="source" /> 以 <paramref name="prefix" /> 开始，则为 true；否则为 false。</returns>
    /// <param name="source">要在其中搜索的字符串。</param>
    /// <param name="prefix">要与 <paramref name="source" /> 的开头进行比较的字符串。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="prefix" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsPrefix(string source, string prefix)
    {
      return this.IsPrefix(source, prefix, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值确定指定的源字符串是否以指定的后缀结尾。</summary>
    /// <returns>如果 <paramref name="suffix" /> 的长度小于或等于 <paramref name="source" /> 的长度，并且 <paramref name="source" /> 以 <paramref name="suffix" /> 结尾，则为 true；否则为 false。</returns>
    /// <param name="source">要在其中搜索的字符串。</param>
    /// <param name="suffix">要与 <paramref name="source" /> 的结尾进行比较的字符串。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="suffix" />。<paramref name="options" /> 可以为其自身使用的枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="suffix" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
    {
      if (source == null || suffix == null)
        throw new ArgumentNullException(source == null ? "source" : "suffix", Environment.GetResourceString("ArgumentNull_String"));
      if (suffix.Length == 0)
        return true;
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
      if (options == CompareOptions.Ordinal)
        return source.EndsWith(suffix, StringComparison.Ordinal);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      IntPtr handle = this.m_dataHandle;
      IntPtr handleOrigin = this.m_handleOrigin;
      string localeName = this.m_sortName;
      int flags = CompareInfo.GetNativeCompareFlags(options) | 2097152 | (!source.IsAscii() || !suffix.IsAscii() ? 0 : 536870912);
      string source1 = source;
      int length1 = source1.Length;
      int startIndex = source.Length - 1;
      string target = suffix;
      int length2 = target.Length;
      return CompareInfo.InternalFindNLSStringEx(handle, handleOrigin, localeName, flags, source1, length1, startIndex, target, length2) >= 0;
    }

    /// <summary>确定指定的源字符串是否以指定的后缀结尾。</summary>
    /// <returns>如果 <paramref name="suffix" /> 的长度小于或等于 <paramref name="source" /> 的长度，并且 <paramref name="source" /> 以 <paramref name="suffix" /> 结尾，则为 true；否则为 false。</returns>
    /// <param name="source">要在其中搜索的字符串。</param>
    /// <param name="suffix">要与 <paramref name="source" /> 的结尾进行比较的字符串。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="suffix" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsSuffix(string source, string suffix)
    {
      return this.IsSuffix(source, suffix, CompareOptions.None);
    }

    /// <summary>搜索指定的字符并返回整个源字符串内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到，则为 <paramref name="value" /> 在 <paramref name="source" /> 内的第一个匹配项从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 0（零）。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
    }

    /// <summary>搜索指定的子字符串并返回整个源字符串内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到，则为 <paramref name="value" /> 在 <paramref name="source" /> 内的第一个匹配项从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 0（零）。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的字符，并返回整个源字符串内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在 <paramref name="source" /> 中找到 <paramref name="value" /> 的第一个匹配项的从零开始的索引，使用指定的比较选项；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 0（零）。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="options">一个值，用于定义应如何比较这些字符串。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, 0, source.Length, options);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的子字符串，并返回整个源字符串内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在 <paramref name="source" /> 中找到 <paramref name="value" /> 的第一个匹配项的从零开始的索引，使用指定的比较选项；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 0（零）。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, 0, source.Length, options);
    }

    /// <summary>搜索指定的字符，并返回源字符串内从指定的索引位置到字符串结尾这一部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在部分 <paramref name="source" />（从 <paramref name="startIndex" /> 到 <paramref name="source" /> 的结尾这一部分）中找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。</exception>
    public virtual int IndexOf(string source, char value, int startIndex)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
    }

    /// <summary>搜索指定的子字符串，并返回源字符串内从指定的索引位置到字符串结尾这一部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在部分 <paramref name="source" />（从 <paramref name="startIndex" /> 到 <paramref name="source" /> 的结尾这一部分）中找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。</exception>
    public virtual int IndexOf(string source, string value, int startIndex)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的字符，并返回源字符串中从指定的索引位置到字符串结尾这一部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中从 <paramref name="startIndex" /> 一直到 <paramref name="source" /> 的结尾这部分找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的子字符串，并返回源字符串内从指定的索引位置到字符串结尾这一部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中从 <paramref name="startIndex" /> 一直到 <paramref name="source" /> 的结尾这部分找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
    }

    /// <summary>搜索指定的字符，并返回源字符串内从指定的索引位置开始、包含指定的元素数的部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在 <paramref name="source" /> 的从 <paramref name="startIndex" /> 开始、包含 <paramref name="count" /> 所指定的元素数的部分中，找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, int startIndex, int count)
    {
      return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>搜索指定的子字符串，并返回源字符串内从指定的索引位置开始、包含指定的元素数的部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在 <paramref name="source" /> 的从 <paramref name="startIndex" /> 开始、包含 <paramref name="count" /> 所指定的元素数的部分中，找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, int startIndex, int count)
    {
      return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的字符，并返回源字符串内从指定的索引位置开始、包含所指定元素数的部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中从 <paramref name="startIndex" /> 开始、包含 <paramref name="count" /> 指定的元素数的部分找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (startIndex < 0 || startIndex > source.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > source.Length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 4194304 | (!source.IsAscii() || (int) value > (int) sbyte.MaxValue ? 0 : 536870912), source, count, startIndex, new string(value, 1), 1);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的子字符串，并返回源字符串内从指定的索引位置开始、包含所指定元素数的部分中第一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中从 <paramref name="startIndex" /> 开始、包含 <paramref name="count" /> 指定的元素数的部分找到 <paramref name="value" /> 的第一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex > source.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (source.Length == 0)
        return value.Length == 0 ? 0 : -1;
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > source.Length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.IndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      IntPtr handle = this.m_dataHandle;
      IntPtr handleOrigin = this.m_handleOrigin;
      string localeName = this.m_sortName;
      int flags = CompareInfo.GetNativeCompareFlags(options) | 4194304 | (!source.IsAscii() || !value.IsAscii() ? 0 : 536870912);
      string source1 = source;
      int sourceCount = count;
      int startIndex1 = startIndex;
      string target = value;
      int length = target.Length;
      return CompareInfo.InternalFindNLSStringEx(handle, handleOrigin, localeName, flags, source1, sourceCount, startIndex1, target, length);
    }

    /// <summary>搜索指定的字符，并返回整个源字符串内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到，则为 <paramref name="value" /> 在 <paramref name="source" /> 内的最后一个匹配项从零开始的索引；否则为 -1。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
    }

    /// <summary>搜索指定的子字符串，并返回整个源字符串内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到，则为 <paramref name="value" /> 在 <paramref name="source" /> 内的最后一个匹配项从零开始的索引；否则为 -1。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的字符，并返回整个源字符串内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在<paramref name="source" /> 中找到 <paramref name="value" /> 的最后一个匹配项，则为从零开始的索引；否则为 -1。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的子字符串，并返回整个源字符串内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在<paramref name="source" /> 中找到 <paramref name="value" /> 的最后一个匹配项，则为从零开始的索引；否则为 -1。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
    }

    /// <summary>搜索指定的字符，并返回源字符串内从字符串开头到指定的索引位置这一部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在部分 <paramref name="source" />（从 <paramref name="source" /> 的开头到 <paramref name="startIndex" /> 这一部分）中找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。</exception>
    public virtual int LastIndexOf(string source, char value, int startIndex)
    {
      string source1 = source;
      int num1 = (int) value;
      int startIndex1 = startIndex;
      int num2 = 1;
      int count = startIndex1 + num2;
      int num3 = 0;
      return this.LastIndexOf(source1, (char) num1, startIndex1, count, (CompareOptions) num3);
    }

    /// <summary>搜索指定的子字符串，并返回源字符串内从字符串开头到指定的索引位置这一部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果在部分 <paramref name="source" />（从 <paramref name="source" /> 的开头到 <paramref name="startIndex" /> 这一部分）中找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。</exception>
    public virtual int LastIndexOf(string source, string value, int startIndex)
    {
      string source1 = source;
      string str = value;
      int startIndex1 = startIndex;
      int num1 = 1;
      int count = startIndex1 + num1;
      int num2 = 0;
      return this.LastIndexOf(source1, str, startIndex1, count, (CompareOptions) num2);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的字符，并返回源字符串内从字符串开头到指定的索引位置这一部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中从 <paramref name="source" /> 一直到 <paramref name="startIndex" /> 的开始这部分找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
    {
      string source1 = source;
      int num1 = (int) value;
      int startIndex1 = startIndex;
      int num2 = 1;
      int count = startIndex1 + num2;
      int num3 = (int) options;
      return this.LastIndexOf(source1, (char) num1, startIndex1, count, (CompareOptions) num3);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的子字符串，并返回源字符串内从字符串开头到指定的索引位置这一部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中从 <paramref name="source" /> 一直到 <paramref name="startIndex" /> 的开始这部分找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
    {
      string source1 = source;
      string str = value;
      int startIndex1 = startIndex;
      int num1 = 1;
      int count = startIndex1 + num1;
      int num2 = (int) options;
      return this.LastIndexOf(source1, str, startIndex1, count, (CompareOptions) num2);
    }

    /// <summary>搜索指定的字符，并返回源字符串内包含指定的元素数、以指定的索引位置结尾的部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>在包含 <paramref name="count" /> 所指定的元素数并以 <paramref name="startIndex" /> 结尾的这部分 <paramref name="source" /> 中，如果找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, int startIndex, int count)
    {
      return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>搜索指定的子字符串，并返回源字符串内包含指定的元素数、以指定的索引位置结尾的部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>在包含 <paramref name="count" /> 所指定的元素数并以 <paramref name="startIndex" /> 结尾的这部分 <paramref name="source" /> 中，如果找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, int startIndex, int count)
    {
      return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的字符，并返回源字符串内包含所指定元素数、以指定的索引位置结尾的部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中结束于 <paramref name="startIndex" /> 、包含 <paramref name="count" /> 指定的元素数的部分找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
        return -1;
      if (startIndex < 0 || startIndex > source.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (startIndex == source.Length)
      {
        --startIndex;
        if (count > 0)
          --count;
      }
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.LastIndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 8388608 | (!source.IsAscii() || (int) value > (int) sbyte.MaxValue ? 0 : 536870912), source, count, startIndex, new string(value, 1), 1);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值，搜索指定的子字符串，并返回源字符串内包含所指定元素数、以指定的索引位置结尾的部分中最后一个匹配项的从零开始的索引。</summary>
    /// <returns>使用指定的比较选项，如果在 <paramref name="source" /> 中结束于 <paramref name="startIndex" /> 、包含 <paramref name="count" /> 指定的元素数的部分找到 <paramref name="value" /> 的最后一个匹配项，则为该项的从零开始的索引；否则为 -1。如果 <paramref name="value" /> 为可忽略字符，则将返回 <paramref name="startIndex" />。</returns>
    /// <param name="source">要搜索的字符串。</param>
    /// <param name="value">要在 <paramref name="source" /> 中定位的字符串。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="options">一个值，用于定义应如何比较 <paramref name="source" /> 和 <paramref name="value" />。<paramref name="options" /> 可以为枚举值 <see cref="F:System.Globalization.CompareOptions.Ordinal" />，或为以下一个或多个值的按位组合：<see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、<see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、<see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、<see cref="F:System.Globalization.CompareOptions.IgnoreWidth" /> 和 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。- 或 - <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="source" /> 的有效索引范围。- 或 - <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="source" /> 中的有效部分。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (value == null)
        throw new ArgumentNullException("value");
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
        return value.Length != 0 ? -1 : 0;
      if (startIndex < 0 || startIndex > source.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (startIndex == source.Length)
      {
        --startIndex;
        if (count > 0)
          --count;
        if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
          return startIndex;
      }
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.LastIndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
      IntPtr handle = this.m_dataHandle;
      IntPtr handleOrigin = this.m_handleOrigin;
      string localeName = this.m_sortName;
      int flags = CompareInfo.GetNativeCompareFlags(options) | 8388608 | (!source.IsAscii() || !value.IsAscii() ? 0 : 536870912);
      string source1 = source;
      int sourceCount = count;
      int startIndex1 = startIndex;
      string target = value;
      int length = target.Length;
      return CompareInfo.InternalFindNLSStringEx(handle, handleOrigin, localeName, flags, source1, sourceCount, startIndex1, target, length);
    }

    /// <summary>使用指定的 <see cref="T:System.Globalization.CompareOptions" /> 值获取指定字符串的 <see cref="T:System.Globalization.SortKey" /> 对象。</summary>
    /// <returns>包含指定字符串的排序关键字的 <see cref="T:System.Globalization.SortKey" /> 对象。</returns>
    /// <param name="source">获取其 <see cref="T:System.Globalization.SortKey" /> 对象的字符串。</param>
    /// <param name="options">以下一个或多个定义该排序关键字如何计算的枚举值的按位组合: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />、 <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />、 <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />、 <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />、 <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />和 <see cref="F:System.Globalization.CompareOptions.StringSort" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 包含无效的 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual SortKey GetSortKey(string source, CompareOptions options)
    {
      return this.CreateSortKey(source, options);
    }

    /// <summary>获取指定字符串的排序关键字。</summary>
    /// <returns>包含指定字符串的排序关键字的 <see cref="T:System.Globalization.SortKey" /> 对象。</returns>
    /// <param name="source">获取其 <see cref="T:System.Globalization.SortKey" /> 对象的字符串。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual SortKey GetSortKey(string source)
    {
      return this.CreateSortKey(source, CompareOptions.None);
    }

    [SecuritySafeCritical]
    private SortKey CreateSortKey(string source, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      byte[] keyData = (byte[]) null;
      if (string.IsNullOrEmpty(source))
      {
        keyData = EmptyArray<byte>.Value;
        source = "\0";
      }
      int nativeCompareFlags = CompareInfo.GetNativeCompareFlags(options);
      IntPtr handle1 = this.m_dataHandle;
      IntPtr handleOrigin1 = this.m_handleOrigin;
      string localeName1 = this.m_sortName;
      int flags1 = nativeCompareFlags;
      string source1 = source;
      int length1 = source1.Length;
      // ISSUE: variable of the null type
      __Null local = null;
      int targetCount = 0;
      int sortKey = CompareInfo.InternalGetSortKey(handle1, handleOrigin1, localeName1, flags1, source1, length1, (byte[]) local, targetCount);
      if (sortKey == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "source");
      if (keyData == null)
      {
        keyData = new byte[sortKey];
        IntPtr handle2 = this.m_dataHandle;
        IntPtr handleOrigin2 = this.m_handleOrigin;
        string localeName2 = this.m_sortName;
        int flags2 = nativeCompareFlags;
        string source2 = source;
        int length2 = source2.Length;
        byte[] target = keyData;
        int length3 = target.Length;
        CompareInfo.InternalGetSortKey(handle2, handleOrigin2, localeName2, flags2, source2, length2, target, length3);
      }
      else
        source = string.Empty;
      return new SortKey(this.Name, source, options, keyData);
    }

    /// <summary>确定指定的对象是否等于当前的 <see cref="T:System.Globalization.CompareInfo" /> 对象。</summary>
    /// <returns>如果指定的对象等于当前的 <see cref="T:System.Globalization.CompareInfo" />，则为 true；否则为 false。</returns>
    /// <param name="value">将与当前 <see cref="T:System.Globalization.CompareInfo" /> 进行比较的对象。 </param>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      CompareInfo compareInfo = value as CompareInfo;
      if (compareInfo != null)
        return this.Name == compareInfo.Name;
      return false;
    }

    /// <summary>用作当前 <see cref="T:System.Globalization.CompareInfo" /> 的哈希函数，适合在哈希算法和数据结构（如哈希表）中使用。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.CompareInfo" /> 的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    /// <summary>获取基于指定的比较选项的字符串的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。 </returns>
    /// <param name="source">其哈希代码是要返回的字符串。</param>
    /// <param name="options">一个值，确定如何比较字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual int GetHashCode(string source, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (options == CompareOptions.Ordinal)
        return source.GetHashCode();
      if (options == CompareOptions.OrdinalIgnoreCase)
        return TextInfo.GetHashCodeOrdinalIgnoreCase(source);
      return this.GetHashCodeOfString(source, options, false, 0L);
    }

    internal int GetHashCodeOfString(string source, CompareOptions options)
    {
      return this.GetHashCodeOfString(source, options, false, 0L);
    }

    [SecuritySafeCritical]
    internal int GetHashCodeOfString(string source, CompareOptions options, bool forceRandomizedHashing, long additionalEntropy)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
      if (source.Length == 0)
        return 0;
      IntPtr handle = this.m_dataHandle;
      IntPtr handleOrigin = this.m_handleOrigin;
      string localeName = this.m_sortName;
      string source1 = source;
      int length = source1.Length;
      int nativeCompareFlags = CompareInfo.GetNativeCompareFlags(options);
      int num = forceRandomizedHashing ? 1 : 0;
      long additionalEntropy1 = additionalEntropy;
      return CompareInfo.InternalGetGlobalizedHashCode(handle, handleOrigin, localeName, source1, length, nativeCompareFlags, num != 0, additionalEntropy1);
    }

    /// <summary>返回表示当前 <see cref="T:System.Globalization.CompareInfo" /> 对象的字符串。</summary>
    /// <returns>表示当前 <see cref="T:System.Globalization.CompareInfo" /> 对象的字符串。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return "CompareInfo - " + this.Name;
    }

    [SecuritySafeCritical]
    internal static IntPtr InternalInitSortHandle(string localeName, out IntPtr handleOrigin)
    {
      return CompareInfo.NativeInternalInitSortHandle(localeName, out handleOrigin);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetNlsVersionEx(IntPtr handle, IntPtr handleOrigin, string localeName, ref Win32Native.NlsVersionInfoEx lpNlsVersionInformation);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern uint InternalGetSortVersion();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr NativeInternalInitSortHandle(string localeName, out IntPtr handleOrigin);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalGetGlobalizedHashCode(IntPtr handle, IntPtr handleOrigin, string localeName, string source, int length, int dwFlags, bool forceRandomizedHashing, long additionalEntropy);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalIsSortable(IntPtr handle, IntPtr handleOrigin, string localeName, string source, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalCompareString(IntPtr handle, IntPtr handleOrigin, string localeName, string string1, int offset1, int length1, string string2, int offset2, int length2, int flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalFindNLSStringEx(IntPtr handle, IntPtr handleOrigin, string localeName, int flags, string source, int sourceCount, int startIndex, string target, int targetCount);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalGetSortKey(IntPtr handle, IntPtr handleOrigin, string localeName, int flags, string source, int sourceCount, byte[] target, int targetCount);
  }
}
