// Decompiled with JetBrains decompiler
// Type: System.Version
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
  /// <summary>表示程序集、操作系统或公共语言运行时的版本号。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>
  {
    private static readonly char[] SeparatorsArray = new char[1]{ '.' };
    private int _Build = -1;
    private int _Revision = -1;
    private int _Major;
    private int _Minor;
    private const int ZERO_CHAR_VALUE = 48;

    /// <summary>获取当前 <see cref="T:System.Version" /> 对象版本号的主要版本号部分的值。</summary>
    /// <returns>主版本号。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Major
    {
      [__DynamicallyInvokable] get
      {
        return this._Major;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Version" /> 对象版本号的次要版本号部分的值。</summary>
    /// <returns>次版本号。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Minor
    {
      [__DynamicallyInvokable] get
      {
        return this._Minor;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Version" /> 对象版本号的内部版本号部分的值。</summary>
    /// <returns>内部版本号或 -1（如果未定义内部版本号）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Build
    {
      [__DynamicallyInvokable] get
      {
        return this._Build;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Version" /> 对象版本号的修订号部分的值。</summary>
    /// <returns>修订号或为 -1（如果未定义修订号）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Revision
    {
      [__DynamicallyInvokable] get
      {
        return this._Revision;
      }
    }

    /// <summary>获取修订号的高 16 位。</summary>
    /// <returns>16 位带符号整数。</returns>
    [__DynamicallyInvokable]
    public short MajorRevision
    {
      [__DynamicallyInvokable] get
      {
        return (short) (this._Revision >> 16);
      }
    }

    /// <summary>获取修订号的低 16 位。</summary>
    /// <returns>16 位带符号整数。</returns>
    [__DynamicallyInvokable]
    public short MinorRevision
    {
      [__DynamicallyInvokable] get
      {
        return (short) (this._Revision & (int) ushort.MaxValue);
      }
    }

    /// <summary>使用指定的主版本号、次版本号、内部版本号和修订号初始化 <see cref="T:System.Version" /> 类的新实例。</summary>
    /// <param name="major">主版本号。</param>
    /// <param name="minor">次版本号。</param>
    /// <param name="build">内部版本号。</param>
    /// <param name="revision">修订号。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="major" />, <paramref name="minor" />, <paramref name="build" />, or <paramref name="revision" /> is less than zero. </exception>
    [__DynamicallyInvokable]
    public Version(int major, int minor, int build, int revision)
    {
      if (major < 0)
        throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (minor < 0)
        throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (build < 0)
        throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (revision < 0)
        throw new ArgumentOutOfRangeException("revision", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      this._Major = major;
      this._Minor = minor;
      this._Build = build;
      this._Revision = revision;
    }

    /// <summary>使用指定的主要版本号、次要版本号和内部版本号值初始化 <see cref="T:System.Version" /> 类的新实例。</summary>
    /// <param name="major">主版本号。</param>
    /// <param name="minor">次版本号。</param>
    /// <param name="build">内部版本号。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="major" />, <paramref name="minor" />, or <paramref name="build" /> is less than zero. </exception>
    [__DynamicallyInvokable]
    public Version(int major, int minor, int build)
    {
      if (major < 0)
        throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (minor < 0)
        throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (build < 0)
        throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      this._Major = major;
      this._Minor = minor;
      this._Build = build;
    }

    /// <summary>使用指定的主要版本号值和次要版本号值初始化 <see cref="T:System.Version" /> 类的新实例。</summary>
    /// <param name="major">主版本号。</param>
    /// <param name="minor">次版本号。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="major" /> or <paramref name="minor" /> is less than zero. </exception>
    [__DynamicallyInvokable]
    public Version(int major, int minor)
    {
      if (major < 0)
        throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (minor < 0)
        throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
      this._Major = major;
      this._Minor = minor;
    }

    /// <summary>使用指定的字符串初始化 <see cref="T:System.Version" /> 类的新实例。</summary>
    /// <param name="version">一个包含主要版本号、次要版本号、内部版本号和修订号的字符串，其中的各个号之间以句点字符（“.”）分隔。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="version" /> has fewer than two components or more than four components. </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="version" /> is null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">A major, minor, build, or revision component is less than zero. </exception>
    /// <exception cref="T:System.FormatException">At least one component of <paramref name="version" /> does not parse to an integer. </exception>
    /// <exception cref="T:System.OverflowException">At least one component of <paramref name="version" /> represents a number greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    [__DynamicallyInvokable]
    public Version(string version)
    {
      Version version1 = Version.Parse(version);
      this._Major = version1.Major;
      this._Minor = version1.Minor;
      this._Build = version1.Build;
      this._Revision = version1.Revision;
    }

    /// <summary>初始化 <see cref="T:System.Version" /> 类的新实例。</summary>
    public Version()
    {
      this._Major = 0;
      this._Minor = 0;
    }

    /// <summary>确定两个指定 <see cref="T:System.Version" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="v1" /> 等于 <paramref name="v2" />，则为 true；否则为 false。</returns>
    /// <param name="v1">第一个 <see cref="T:System.Version" /> 对象。</param>
    /// <param name="v2">第二个 <see cref="T:System.Version" /> 对象。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(Version v1, Version v2)
    {
      if (v1 == null)
        return v2 == null;
      return v1.Equals(v2);
    }

    /// <summary>确定两个指定的 <see cref="T:System.Version" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="v1" /> 不等于 <paramref name="v2" />，则为 true；否则为 false。</returns>
    /// <param name="v1">第一个 <see cref="T:System.Version" /> 对象。</param>
    /// <param name="v2">第二个 <see cref="T:System.Version" /> 对象。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(Version v1, Version v2)
    {
      return !(v1 == v2);
    }

    /// <summary>确定指定的第一个 <see cref="T:System.Version" /> 对象是否小于指定的第二个 <see cref="T:System.Version" /> 对象。</summary>
    /// <returns>如果 <paramref name="v1" /> 小于 <paramref name="v2" />，则为 true；否则为 false。</returns>
    /// <param name="v1">第一个 <see cref="T:System.Version" /> 对象。</param>
    /// <param name="v2">第二个 <see cref="T:System.Version" /> 对象。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="v1" /> is null. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator <(Version v1, Version v2)
    {
      if (v1 == null)
        throw new ArgumentNullException("v1");
      return v1.CompareTo(v2) < 0;
    }

    /// <summary>确定指定的第一个 <see cref="T:System.Version" /> 对象是否小于或等于第二个 <see cref="T:System.Version" /> 对象。</summary>
    /// <returns>如果 <paramref name="v1" /> 小于等于 <paramref name="v2" />，则为 true；否则为 false。</returns>
    /// <param name="v1">第一个 <see cref="T:System.Version" /> 对象。</param>
    /// <param name="v2">第二个 <see cref="T:System.Version" /> 对象。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="v1" /> is null. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator <=(Version v1, Version v2)
    {
      if (v1 == null)
        throw new ArgumentNullException("v1");
      return v1.CompareTo(v2) <= 0;
    }

    /// <summary>确定指定的第一个 <see cref="T:System.Version" /> 对象是否大于指定的第二个 <see cref="T:System.Version" /> 对象。</summary>
    /// <returns>如果 <paramref name="v1" /> 大于 <paramref name="v2" />，则为 true；否则为 false。</returns>
    /// <param name="v1">第一个 <see cref="T:System.Version" /> 对象。</param>
    /// <param name="v2">第二个 <see cref="T:System.Version" /> 对象。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator >(Version v1, Version v2)
    {
      return v2 < v1;
    }

    /// <summary>确定指定的第一个 <see cref="T:System.Version" /> 对象是否大于等于指定的第二个 <see cref="T:System.Version" /> 对象。</summary>
    /// <returns>如果 <paramref name="v1" /> 大于等于 <paramref name="v2" />，则为 true；否则为 false。</returns>
    /// <param name="v1">第一个 <see cref="T:System.Version" /> 对象。</param>
    /// <param name="v2">第二个 <see cref="T:System.Version" /> 对象。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator >=(Version v1, Version v2)
    {
      return v2 <= v1;
    }

    /// <summary>返回一个新的 <see cref="T:System.Version" /> 对象，该对象的值与当前的 <see cref="T:System.Version" /> 对象相同。</summary>
    /// <returns>一个新的 <see cref="T:System.Object" />，其值为当前 <see cref="T:System.Version" /> 对象的副本。</returns>
    /// <filterpriority>2</filterpriority>
    public object Clone()
    {
      return (object) new Version() { _Major = this._Major, _Minor = this._Minor, _Build = this._Build, _Revision = this._Revision };
    }

    /// <summary>将当前 <see cref="T:System.Version" /> 对象与指定的对象进行比较，并返回二者相对值的一个指示。</summary>
    /// <returns>一个有符号整数，它指示两个对象的相对值，如下表所示。返回值含义小于零当前 <see cref="T:System.Version" /> 对象是 <paramref name="version" /> 之前的一个版本。零当前 <see cref="T:System.Version" /> 对象是与 <paramref name="version" /> 相同的版本。大于零当前 <see cref="T:System.Version" /> 对象是 <paramref name="version" /> 之后的一个版本。- 或 - <paramref name="version" /> 为 null。</returns>
    /// <param name="version">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="version" /> is not of type <see cref="T:System.Version" />. </exception>
    /// <filterpriority>1</filterpriority>
    public int CompareTo(object version)
    {
      if (version == null)
        return 1;
      Version version1 = version as Version;
      if (version1 == (Version) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeVersion"));
      if (this._Major != version1._Major)
        return this._Major > version1._Major ? 1 : -1;
      if (this._Minor != version1._Minor)
        return this._Minor > version1._Minor ? 1 : -1;
      if (this._Build != version1._Build)
        return this._Build > version1._Build ? 1 : -1;
      if (this._Revision == version1._Revision)
        return 0;
      return this._Revision > version1._Revision ? 1 : -1;
    }

    /// <summary>将当前 <see cref="T:System.Version" /> 对象与指定的 <see cref="T:System.Version" /> 对象进行比较，并返回二者相对值的一个指示。</summary>
    /// <returns>一个有符号整数，它指示两个对象的相对值，如下表所示。返回值含义小于零当前 <see cref="T:System.Version" /> 对象是 <paramref name="value" /> 之前的一个版本。零当前 <see cref="T:System.Version" /> 对象是与 <paramref name="value" /> 相同的版本。大于零当前 <see cref="T:System.Version" /> 对象是 <paramref name="value" /> 之后的一个版本。- 或 -<paramref name="value" /> 为 null。</returns>
    /// <param name="value">要与当前的 <see cref="T:System.Version" /> 对象进行比较的 <see cref="T:System.Version" /> 对象，或者为 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(Version value)
    {
      if (value == (Version) null)
        return 1;
      if (this._Major != value._Major)
        return this._Major > value._Major ? 1 : -1;
      if (this._Minor != value._Minor)
        return this._Minor > value._Minor ? 1 : -1;
      if (this._Build != value._Build)
        return this._Build > value._Build ? 1 : -1;
      if (this._Revision == value._Revision)
        return 0;
      return this._Revision > value._Revision ? 1 : -1;
    }

    /// <summary>返回一个值，该值指示当前 <see cref="T:System.Version" /> 对象是否等于指定的对象。。</summary>
    /// <returns>如果当前的 <see cref="T:System.Version" /> 对象和 <paramref name="obj" /> 都为 <see cref="T:System.Version" /> 对象，并且当前 <see cref="T:System.Version" /> 对象的每个部分都与 <paramref name="obj" /> 的相应部分匹配，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前 <see cref="T:System.Version" /> 对象进行比较的对象，或者为 null。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      Version version = obj as Version;
      return !(version == (Version) null) && this._Major == version._Major && (this._Minor == version._Minor && this._Build == version._Build) && this._Revision == version._Revision;
    }

    /// <summary>返回一个值，该值指示当前 <see cref="T:System.Version" /> 对象与指定 <see cref="T:System.Version" /> 对象是否表示同一个值。</summary>
    /// <returns>如果当前 <see cref="T:System.Version" /> 对象的每个部分都与 <paramref name="obj" /> 参数的相应部分匹配，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.Version" /> 对象进行比较的 <see cref="T:System.Version" /> 对象，或者为 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(Version obj)
    {
      return !(obj == (Version) null) && this._Major == obj._Major && (this._Minor == obj._Minor && this._Build == obj._Build) && this._Revision == obj._Revision;
    }

    /// <summary>返回当前 <see cref="T:System.Version" /> 对象的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return 0 | (this._Major & 15) << 28 | (this._Minor & (int) byte.MaxValue) << 20 | (this._Build & (int) byte.MaxValue) << 12 | this._Revision & 4095;
    }

    /// <summary>将当前 <see cref="T:System.Version" /> 对象的值转换为其等效的 <see cref="T:System.String" /> 表示形式。</summary>
    /// <returns>当前 <see cref="T:System.String" /> 对象的主要版本号、次要版本号、内部版本号和修订号部分的值的 <see cref="T:System.Version" /> 表示形式（遵循下面所示格式）。各部分之间由句点字符（“.”）分隔。方括号（“[”和“]”）指示在返回值中不会出现的部分（如果未定义该部分）：主要版本号.次要版本号[.内部版本号[.修订号]]例如，如果使用构造函数 Version(1,1) 创建 <see cref="T:System.Version" /> 对象，则返回的字符串为“1.1”。如果使用构造函数 Version(1,3,4,2) 创建 <see cref="T:System.Version" /> 对象，则返回的字符串为“1.3.4.2”。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      if (this._Build == -1)
        return this.ToString(2);
      if (this._Revision == -1)
        return this.ToString(3);
      return this.ToString(4);
    }

    /// <summary>将当前 <see cref="T:System.Version" /> 对象的值转换为其等效的 <see cref="T:System.String" /> 表示形式。指定的计数指示要返回的部分数。</summary>
    /// <returns>当前 <see cref="T:System.String" /> 对象的主要版本号、次要版本号、内部版本号和修订号部分的值的 <see cref="T:System.Version" /> 表示形式，各部分之间用句点字符（“.”）分隔。<paramref name="fieldCount" /> 参数确定返回多少个部分。fieldCount返回值0 空字符串 ("")。1 主要版本号2 主要版本号.次要版本号3 主要版本号.次要版本号.内部版本号4 主要版本号.次要版本号.内部版本号.修订号例如，如果使用构造函数 Version(1,3,5) 创建 <see cref="T:System.Version" /> 对象，则 ToString(2) 返回“1.3”，并且 ToString(4) 引发异常。</returns>
    /// <param name="fieldCount">要返回的部分数。<paramref name="fieldCount" /> 的范围是从 0 到 4。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fieldCount" /> is less than 0, or more than 4.-or- <paramref name="fieldCount" /> is more than the number of components defined in the current <see cref="T:System.Version" /> object. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string ToString(int fieldCount)
    {
      switch (fieldCount)
      {
        case 0:
          return string.Empty;
        case 1:
          return this._Major.ToString();
        case 2:
          StringBuilder sb1 = StringBuilderCache.Acquire(16);
          Version.AppendPositiveNumber(this._Major, sb1);
          sb1.Append('.');
          Version.AppendPositiveNumber(this._Minor, sb1);
          return StringBuilderCache.GetStringAndRelease(sb1);
        default:
          if (this._Build == -1)
            throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", (object) "0", (object) "2"), "fieldCount");
          if (fieldCount == 3)
          {
            StringBuilder sb2 = StringBuilderCache.Acquire(16);
            Version.AppendPositiveNumber(this._Major, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Minor, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Build, sb2);
            return StringBuilderCache.GetStringAndRelease(sb2);
          }
          if (this._Revision == -1)
            throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", (object) "0", (object) "3"), "fieldCount");
          if (fieldCount == 4)
          {
            StringBuilder sb2 = StringBuilderCache.Acquire(16);
            Version.AppendPositiveNumber(this._Major, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Minor, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Build, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Revision, sb2);
            return StringBuilderCache.GetStringAndRelease(sb2);
          }
          throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", (object) "0", (object) "4"), "fieldCount");
      }
    }

    private static void AppendPositiveNumber(int num, StringBuilder sb)
    {
      int length = sb.Length;
      do
      {
        int num1 = num % 10;
        num /= 10;
        sb.Insert(length, (char) (48 + num1));
      }
      while (num > 0);
    }

    /// <summary>将版本号的字符串表示形式转换为等效的 <see cref="T:System.Version" /> 对象。</summary>
    /// <returns>一个等效于 <paramref name="input" /> 参数中指定的版本号的对象。</returns>
    /// <param name="input">包含要转换的版本号的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="input" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="input" /> has fewer than two or more than four version components.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">At least one component in <paramref name="input" /> is less than zero.</exception>
    /// <exception cref="T:System.FormatException">At least one component in <paramref name="input" /> is not an integer.</exception>
    /// <exception cref="T:System.OverflowException">At least one component in <paramref name="input" /> represents a number that is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    [__DynamicallyInvokable]
    public static Version Parse(string input)
    {
      if (input == null)
        throw new ArgumentNullException("input");
      Version.VersionResult result = new Version.VersionResult();
      result.Init("input", true);
      if (!Version.TryParseVersion(input, ref result))
        throw result.GetVersionParseException();
      return result.m_parsedVersion;
    }

    /// <summary>尝试将版本号的字符串表示形式转换为等效的 <see cref="T:System.Version" /> 对象，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 <paramref name="input" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="input">包含要转换的版本号的字符串。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="input" /> 中所含编号等效的 <see cref="T:System.Version" />；如果转换失败，则包含主版本号和次版本号都为 0 的 <see cref="T:System.Version" /> 对象。如果 <paramref name="input" /> 为 null 或 <see cref="F:System.String.Empty" />，则当该方法返回时，<paramref name="result" /> 为 null。</param>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, out Version result)
    {
      Version.VersionResult result1 = new Version.VersionResult();
      result1.Init("input", false);
      int num = Version.TryParseVersion(input, ref result1) ? 1 : 0;
      result = result1.m_parsedVersion;
      return num != 0;
    }

    private static bool TryParseVersion(string version, ref Version.VersionResult result)
    {
      if (version == null)
      {
        result.SetFailure(Version.ParseFailureKind.ArgumentNullException);
        return false;
      }
      string[] strArray = version.Split(Version.SeparatorsArray);
      int length = strArray.Length;
      if (length < 2 || length > 4)
      {
        result.SetFailure(Version.ParseFailureKind.ArgumentException);
        return false;
      }
      int parsedComponent1;
      int parsedComponent2;
      if (!Version.TryParseComponent(strArray[0], "version", ref result, out parsedComponent1) || !Version.TryParseComponent(strArray[1], "version", ref result, out parsedComponent2))
        return false;
      int num = length - 2;
      if (num > 0)
      {
        int parsedComponent3;
        if (!Version.TryParseComponent(strArray[2], "build", ref result, out parsedComponent3))
          return false;
        if (num - 1 > 0)
        {
          int parsedComponent4;
          if (!Version.TryParseComponent(strArray[3], "revision", ref result, out parsedComponent4))
            return false;
          result.m_parsedVersion = new Version(parsedComponent1, parsedComponent2, parsedComponent3, parsedComponent4);
        }
        else
          result.m_parsedVersion = new Version(parsedComponent1, parsedComponent2, parsedComponent3);
      }
      else
        result.m_parsedVersion = new Version(parsedComponent1, parsedComponent2);
      return true;
    }

    private static bool TryParseComponent(string component, string componentName, ref Version.VersionResult result, out int parsedComponent)
    {
      if (!int.TryParse(component, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out parsedComponent))
      {
        result.SetFailure(Version.ParseFailureKind.FormatException, component);
        return false;
      }
      if (parsedComponent >= 0)
        return true;
      result.SetFailure(Version.ParseFailureKind.ArgumentOutOfRangeException, componentName);
      return false;
    }

    internal enum ParseFailureKind
    {
      ArgumentNullException,
      ArgumentException,
      ArgumentOutOfRangeException,
      FormatException,
    }

    internal struct VersionResult
    {
      internal Version m_parsedVersion;
      internal Version.ParseFailureKind m_failure;
      internal string m_exceptionArgument;
      internal string m_argumentName;
      internal bool m_canThrow;

      internal void Init(string argumentName, bool canThrow)
      {
        this.m_canThrow = canThrow;
        this.m_argumentName = argumentName;
      }

      internal void SetFailure(Version.ParseFailureKind failure)
      {
        this.SetFailure(failure, string.Empty);
      }

      internal void SetFailure(Version.ParseFailureKind failure, string argument)
      {
        this.m_failure = failure;
        this.m_exceptionArgument = argument;
        if (this.m_canThrow)
          throw this.GetVersionParseException();
      }

      internal Exception GetVersionParseException()
      {
        switch (this.m_failure)
        {
          case Version.ParseFailureKind.ArgumentNullException:
            return (Exception) new ArgumentNullException(this.m_argumentName);
          case Version.ParseFailureKind.ArgumentException:
            return (Exception) new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
          case Version.ParseFailureKind.ArgumentOutOfRangeException:
            return (Exception) new ArgumentOutOfRangeException(this.m_exceptionArgument, Environment.GetResourceString("ArgumentOutOfRange_Version"));
          case Version.ParseFailureKind.FormatException:
            try
            {
              int.Parse(this.m_exceptionArgument, (IFormatProvider) CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
              return (Exception) ex;
            }
            catch (OverflowException ex)
            {
              return (Exception) ex;
            }
            return (Exception) new FormatException(Environment.GetResourceString("Format_InvalidString"));
          default:
            return (Exception) new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
        }
      }
    }
  }
}
