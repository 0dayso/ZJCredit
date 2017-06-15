// Decompiled with JetBrains decompiler
// Type: System.Char
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
  /// <summary>将字符表示为 UTF-16 代码单位。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
  {
    private static readonly byte[] categoryForLatin1 = new byte[256]
    {
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 11,
      (byte) 24,
      (byte) 24,
      (byte) 24,
      (byte) 26,
      (byte) 24,
      (byte) 24,
      (byte) 24,
      (byte) 20,
      (byte) 21,
      (byte) 24,
      (byte) 25,
      (byte) 24,
      (byte) 19,
      (byte) 24,
      (byte) 24,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 24,
      (byte) 24,
      (byte) 25,
      (byte) 25,
      (byte) 25,
      (byte) 24,
      (byte) 24,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 20,
      (byte) 24,
      (byte) 21,
      (byte) 27,
      (byte) 18,
      (byte) 27,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 20,
      (byte) 25,
      (byte) 21,
      (byte) 25,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 11,
      (byte) 24,
      (byte) 26,
      (byte) 26,
      (byte) 26,
      (byte) 26,
      (byte) 28,
      (byte) 28,
      (byte) 27,
      (byte) 28,
      (byte) 1,
      (byte) 22,
      (byte) 25,
      (byte) 19,
      (byte) 28,
      (byte) 27,
      (byte) 28,
      (byte) 25,
      (byte) 10,
      (byte) 10,
      (byte) 27,
      (byte) 1,
      (byte) 28,
      (byte) 24,
      (byte) 27,
      (byte) 10,
      (byte) 1,
      (byte) 23,
      (byte) 10,
      (byte) 10,
      (byte) 10,
      (byte) 24,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 25,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 25,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1
    };
    internal char m_value;
    /// <summary>表示 <see cref="T:System.Char" /> 的最大可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const char MaxValue = '\xFFFF';
    /// <summary>表示 <see cref="T:System.Char" /> 的最小可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const char MinValue = '\0';
    internal const int UNICODE_PLANE00_END = 65535;
    internal const int UNICODE_PLANE01_START = 65536;
    internal const int UNICODE_PLANE16_END = 1114111;
    internal const int HIGH_SURROGATE_START = 55296;
    internal const int LOW_SURROGATE_END = 57343;

    private static bool IsLatin1(char ch)
    {
      return (int) ch <= (int) byte.MaxValue;
    }

    private static bool IsAscii(char ch)
    {
      return (int) ch <= (int) sbyte.MaxValue;
    }

    private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
    {
      return (UnicodeCategory) char.categoryForLatin1[(int) ch];
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this | (int) this << 16;
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 true 是 <paramref name="obj" /> 的实例并且等于此实例的值，则为 <see cref="T:System.Char" />；否则为 false。</returns>
    /// <param name="obj">要与此示例比较的对象，或 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is char))
        return false;
      return (int) this == (int) (char) obj;
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的 <see cref="T:System.Char" /> 对象相等。</summary>
    /// <returns>如果 true 参数与此实例的值相等，则为 <paramref name="obj" />；否则为 false。</returns>
    /// <param name="obj">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(char obj)
    {
      return (int) this == (int) obj;
    }

    /// <summary>将此实例与指定的对象进行比较，并指示此实例在排序顺序中是位于指定的 <see cref="T:System.Object" /> 之前、之后还是与其出现在同一位置。</summary>
    /// <returns>一个有符号数字，指示此实例在排序顺序中相对于 <paramref name="value" /> 参数的位置。返回值 描述 小于零 此实例位于 <paramref name="value" /> 之前。零 此实例在排序顺序中的位置与 <paramref name="value" /> 相同。大于零 此实例位于 <paramref name="value" /> 之后。- 或 - <paramref name="value" /> 为 null。</returns>
    /// <param name="value">要与此实例比较的对象，或 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是 <see cref="T:System.Char" /> 对象。</exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is char))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeChar"));
      return (int) this - (int) (char) value;
    }

    /// <summary>将此实例与指定的 <see cref="T:System.Char" /> 对象进行比较，并指示此实例在排序顺序中是位于指定的 <see cref="T:System.Char" /> 对象之前、之后还是与其出现在同一位置。</summary>
    /// <returns>一个有符号数字，指示此实例在排序顺序中相对于 <paramref name="value" /> 参数的位置。返回值 描述 小于零 此实例位于 <paramref name="value" /> 之前。零 此实例在排序顺序中的位置与 <paramref name="value" /> 相同。大于零 此实例位于 <paramref name="value" /> 之后。</returns>
    /// <param name="value">要比较的 <see cref="T:System.Char" /> 对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(char value)
    {
      return (int) this - (int) value;
    }

    /// <summary>将此实例的值转换为其等效的字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return char.ToString(this);
    }

    /// <summary>使用指定的区域性特定格式信息将此实例的值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="provider" /> 指定。</returns>
    /// <param name="provider">（保留）一个对象，用于提供区域性特定的格式设置信息。</param>
    /// <filterpriority>1</filterpriority>
    public string ToString(IFormatProvider provider)
    {
      return char.ToString(this);
    }

    /// <summary>将指定的 Unicode 字符转换为它的等效字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="c" /> 值的字符串表示形式。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(char c)
    {
      return new string(c, 1);
    }

    /// <summary>将指定字符串的值转换为它的等效 Unicode 字符。</summary>
    /// <returns>一个等效于 <paramref name="s" /> 中唯一字符的 Unicode 字符。</returns>
    /// <param name="s">包含单个字符的字符串，或 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 的长度不是 1。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char Parse(string s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (s.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
      return s[0];
    }

    /// <summary>将指定字符串的值转换为它的等效 Unicode 字符。一个指示转换是成功还是失败的返回代码。</summary>
    /// <returns>如果 true 参数成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含单个字符的字符串，或 null。</param>
    /// <param name="result">此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中的唯一字符等效的 Unicode 字符；如果转换失败，则包含未定义的值。如果 <paramref name="s" /> 参数为 null 或 <paramref name="s" /> 的长度不为 1，则转换失败。此参数未经初始化即被传递。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out char result)
    {
      result = char.MinValue;
      if (s == null || s.Length != 1)
        return false;
      result = s[0];
      return true;
    }

    /// <summary>指示指定的 Unicode 字符是否属于十进制数字类别。</summary>
    /// <returns>如果 true 是十进制数，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDigit(char c)
    {
      if (!char.IsLatin1(c))
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
      if ((int) c >= 48)
        return (int) c <= 57;
      return false;
    }

    internal static bool CheckLetter(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
          return true;
        default:
          return false;
      }
    }

    /// <summary>指示指定的 Unicode 字符是否属于 Unicode 字母类别。</summary>
    /// <returns>如果 true 是一个字母，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLetter(char c)
    {
      if (!char.IsLatin1(c))
        return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
      if (!char.IsAscii(c))
        return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
      c |= ' ';
      if ((int) c >= 97)
        return (int) c <= 122;
      return false;
    }

    private static bool IsWhiteSpaceLatin1(char c)
    {
      return (int) c == 32 || (int) c >= 9 && (int) c <= 13 || ((int) c == 160 || (int) c == 133);
    }

    /// <summary>指示指定的 Unicode 字符是否属于空格类别。</summary>
    /// <returns>如果 true 是空格，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsWhiteSpace(char c)
    {
      if (char.IsLatin1(c))
        return char.IsWhiteSpaceLatin1(c);
      return CharUnicodeInfo.IsWhiteSpace(c);
    }

    /// <summary>指示指定的 Unicode 字符是否属于大写字母类别。</summary>
    /// <returns>如果 true 是一个大写字母，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsUpper(char c)
    {
      if (!char.IsLatin1(c))
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
      if (!char.IsAscii(c))
        return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
      if ((int) c >= 65)
        return (int) c <= 90;
      return false;
    }

    /// <summary>指示指定的 Unicode 字符是否属于小写字母类别。</summary>
    /// <returns>如果 true 是一个小写字母，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLower(char c)
    {
      if (!char.IsLatin1(c))
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
      if (!char.IsAscii(c))
        return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
      if ((int) c >= 97)
        return (int) c <= 122;
      return false;
    }

    internal static bool CheckPunctuation(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.ConnectorPunctuation:
        case UnicodeCategory.DashPunctuation:
        case UnicodeCategory.OpenPunctuation:
        case UnicodeCategory.ClosePunctuation:
        case UnicodeCategory.InitialQuotePunctuation:
        case UnicodeCategory.FinalQuotePunctuation:
        case UnicodeCategory.OtherPunctuation:
          return true;
        default:
          return false;
      }
    }

    /// <summary>指示指定的 Unicode 字符是否属于标点符号类别。</summary>
    /// <returns>如果 true 是一个标点符号，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsPunctuation(char c)
    {
      if (char.IsLatin1(c))
        return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
      return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    internal static bool CheckLetterOrDigit(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
        case UnicodeCategory.DecimalDigitNumber:
          return true;
        default:
          return false;
      }
    }

    /// <summary>指示指定的 Unicode 字符是否属于字母或十进制数字类别。</summary>
    /// <returns>如果 true 是字母或十进制数，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLetterOrDigit(char c)
    {
      if (char.IsLatin1(c))
        return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
      return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    /// <summary>使用指定的区域性特定格式设置信息将指定 Unicode 字符的值转换为它的大写等效项。</summary>
    /// <returns>
    /// <paramref name="c" /> 的大写等效项（根据 <paramref name="culture" /> 进行修改），或者，如果 <paramref name="c" /> 已经是大写字母、没有大写等效项或不是字母，则为 <paramref name="c" /> 的未更改值。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <param name="culture">一个对象，用于提供区域性特定的大小写规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToUpper(char c, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      return culture.TextInfo.ToUpper(c);
    }

    /// <summary>将 Unicode 字符的值转换为它的大写等效项。</summary>
    /// <returns>
    /// <paramref name="c" /> 的大写等效项，或者，如果 <paramref name="c" /> 已经是大写字母或不是字母，则为 <paramref name="c" /> 的未更改值。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToUpper(char c)
    {
      return char.ToUpper(c, CultureInfo.CurrentCulture);
    }

    /// <summary>使用固定区域性的大小写规则，将 Unicode 字符的值转换为其大写等效项。</summary>
    /// <returns>
    /// <paramref name="c" /> 参数的小写等效项，或者，如果 <paramref name="c" /> 已经是大写字母或不是字母，则为 <paramref name="c" /> 的未更改值。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToUpperInvariant(char c)
    {
      return char.ToUpper(c, CultureInfo.InvariantCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息将指定 Unicode 字符的值转换为它的小写等效项。</summary>
    /// <returns>
    /// <paramref name="c" /> 的小写等效项（根据 <paramref name="culture" /> 进行修改），或者，如果 <paramref name="c" /> 已经是小写字母或不是字母，则为 <paramref name="c" /> 的未更改值。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <param name="culture">一个对象，用于提供区域性特定的大小写规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToLower(char c, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      return culture.TextInfo.ToLower(c);
    }

    /// <summary>将 Unicode 字符的值转换为它的小写等效项。</summary>
    /// <returns>
    /// <paramref name="c" /> 的小写等效项，或者，如果 <paramref name="c" /> 已经是小写字母或不是字母，则为 <paramref name="c" /> 的未更改值。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToLower(char c)
    {
      return char.ToLower(c, CultureInfo.CurrentCulture);
    }

    /// <summary>使用固定区域性的大小写规则，将 Unicode 字符的值转换为其小写等效项。</summary>
    /// <returns>
    /// <paramref name="c" /> 参数的小写等效项，或者，如果 <paramref name="c" /> 已经是小写字母或不是字母，则为 <paramref name="c" /> 的未更改值。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToLowerInvariant(char c)
    {
      return char.ToLower(c, CultureInfo.InvariantCulture);
    }

    /// <summary>返回值类型 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.Char" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.Char" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Char;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Char", (object) "Boolean"));
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Char", (object) "Single"));
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Char", (object) "Double"));
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Char", (object) "Decimal"));
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Char", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    /// <summary>指示指定的 Unicode 字符是否属于控制字符类别。</summary>
    /// <returns>如果 true 是控制字符，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsControl(char c)
    {
      if (char.IsLatin1(c))
        return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
      return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于控制字符类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个控制字符，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsControl(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.Control;
      return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于十进制数字类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个十进制数，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDigit(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
      if ((int) ch >= 48)
        return (int) ch <= 57;
      return false;
    }

    /// <summary>指示指定字符串中位于指定位置处的指定字符串是否属于 Unicode 字母类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个字母，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLetter(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch1 = s[index];
      if (!char.IsLatin1(ch1))
        return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
      if (!char.IsAscii(ch1))
        return char.CheckLetter(char.GetLatin1UnicodeCategory(ch1));
      char ch2 = (char) ((uint) ch1 | 32U);
      if ((int) ch2 >= 97)
        return (int) ch2 <= 122;
      return false;
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于字母或十进制数字类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个字母或十进制数，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLetterOrDigit(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(ch));
      return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于小写字母类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个小写字母，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLower(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
      if (!char.IsAscii(ch))
        return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.LowercaseLetter;
      if ((int) ch >= 97)
        return (int) ch <= 122;
      return false;
    }

    internal static bool CheckNumber(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.DecimalDigitNumber:
        case UnicodeCategory.LetterNumber:
        case UnicodeCategory.OtherNumber:
          return true;
        default:
          return false;
      }
    }

    /// <summary>指示指定的 Unicode 字符是否属于数字类别。</summary>
    /// <returns>如果 true 是一个数字，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsNumber(char c)
    {
      if (!char.IsLatin1(c))
        return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
      if (!char.IsAscii(c))
        return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
      if ((int) c >= 48)
        return (int) c <= 57;
      return false;
    }

    /// <summary>指示指定字符串中位于指定位置的字符是否属于数字类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个数字，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsNumber(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
      if (!char.IsAscii(ch))
        return char.CheckNumber(char.GetLatin1UnicodeCategory(ch));
      if ((int) ch >= 48)
        return (int) ch <= 57;
      return false;
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于标点符号类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个标点符号，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsPunctuation(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.CheckPunctuation(char.GetLatin1UnicodeCategory(ch));
      return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    internal static bool CheckSeparator(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.LineSeparator:
        case UnicodeCategory.ParagraphSeparator:
          return true;
        default:
          return false;
      }
    }

    private static bool IsSeparatorLatin1(char c)
    {
      if ((int) c != 32)
        return (int) c == 160;
      return true;
    }

    /// <summary>指示指定的 Unicode 字符是否属于分隔符类别。</summary>
    /// <returns>如果 true 是分隔符，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSeparator(char c)
    {
      if (char.IsLatin1(c))
        return char.IsSeparatorLatin1(c);
      return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于分隔符类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个分隔符，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSeparator(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.IsSeparatorLatin1(ch);
      return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    /// <summary>指示指定的字符是否具有指定的代理项代码单位。</summary>
    /// <returns>如果 true 为高代理项或低代理项，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSurrogate(char c)
    {
      if ((int) c >= 55296)
        return (int) c <= 57343;
      return false;
    }

    /// <summary>指示指定字符串中位于指定位置的字符是否具有代理项代码单位。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个高代理项或低代理项，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSurrogate(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      return char.IsSurrogate(s[index]);
    }

    internal static bool CheckSymbol(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.MathSymbol:
        case UnicodeCategory.CurrencySymbol:
        case UnicodeCategory.ModifierSymbol:
        case UnicodeCategory.OtherSymbol:
          return true;
        default:
          return false;
      }
    }

    /// <summary>指示指定的 Unicode 字符是否属于符号字符类别。</summary>
    /// <returns>如果 true 是符号字符，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSymbol(char c)
    {
      if (char.IsLatin1(c))
        return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
      return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于符号字符类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个符号字符，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSymbol(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      if (char.IsLatin1(s[index]))
        return char.CheckSymbol(char.GetLatin1UnicodeCategory(s[index]));
      return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于大写字母类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是一个大写字母，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsUpper(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
      if (!char.IsAscii(ch))
        return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.UppercaseLetter;
      if ((int) ch >= 65)
        return (int) ch <= 90;
      return false;
    }

    /// <summary>指示指定字符串中位于指定位置处的字符是否属于空格类别。</summary>
    /// <returns>如果 true 中位于 <paramref name="index" /> 的字符是空格，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsWhiteSpace(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      if (char.IsLatin1(s[index]))
        return char.IsWhiteSpaceLatin1(s[index]);
      return CharUnicodeInfo.IsWhiteSpace(s, index);
    }

    /// <summary>将指定的 Unicode 字符分类到由一个 <see cref="T:System.Globalization.UnicodeCategory" /> 值标识的组中。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.UnicodeCategory" /> 值，它标识包含 <paramref name="c" /> 的组。</returns>
    /// <param name="c">要分类的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    public static UnicodeCategory GetUnicodeCategory(char c)
    {
      if (char.IsLatin1(c))
        return char.GetLatin1UnicodeCategory(c);
      return CharUnicodeInfo.InternalGetUnicodeCategory((int) c);
    }

    /// <summary>将指定字符串中位于指定位置的字符分类到由一个 <see cref="T:System.Globalization.UnicodeCategory" /> 值标识的组中。</summary>
    /// <returns>一个 <see cref="T:System.Globalization.UnicodeCategory" /> 枚举常数，标识包含 <paramref name="index" /> 中位于 <paramref name="s" /> 处的字符的组。</returns>
    /// <param name="s">
    /// <see cref="T:System.String" />。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中的字符位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    public static UnicodeCategory GetUnicodeCategory(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      if (char.IsLatin1(s[index]))
        return char.GetLatin1UnicodeCategory(s[index]);
      return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
    }

    /// <summary>将指定的数字 Unicode 字符转换为双精度浮点数。</summary>
    /// <returns>如果该字符表示数字，则数值为 <paramref name="c" />；否则为 -1.0。</returns>
    /// <param name="c">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double GetNumericValue(char c)
    {
      return CharUnicodeInfo.GetNumericValue(c);
    }

    /// <summary>将指定字符串中位于指定位置的数字 Unicode 字符转换为双精度浮点数。</summary>
    /// <returns>如果 <paramref name="index" /> 中位于 <paramref name="s" /> 处的字符表示数字，则为该字符的数值；否则为 -1。</returns>
    /// <param name="s">
    /// <see cref="T:System.String" />。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中的字符位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于 <paramref name="s" /> 中的最后一个位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double GetNumericValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException("index");
      return CharUnicodeInfo.GetNumericValue(s, index);
    }

    /// <summary>指示指定的 <see cref="T:System.Char" /> 对象是否是一个高代理项。</summary>
    /// <returns>如果 true 参数的数值范围是从 U+D800 到 U+DBFF，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsHighSurrogate(char c)
    {
      if ((int) c >= 55296)
        return (int) c <= 56319;
      return false;
    }

    /// <summary>指示字符串中指定位置处的 <see cref="T:System.Char" /> 对象是否为高代理项。</summary>
    /// <returns>如果 true 中指定字符的数值范围是从 U+D800 到 U+DBFF，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <paramref name="s" /> 中的位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsHighSurrogate(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index");
      return char.IsHighSurrogate(s[index]);
    }

    /// <summary>指示指定的 <see cref="T:System.Char" /> 对象是否是一个低代理项。</summary>
    /// <returns>如果 true 参数的数值范围是从 U+DC00 到 U+DFFF，则为 <paramref name="c" />；否则为 false。</returns>
    /// <param name="c">要计算的字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLowSurrogate(char c)
    {
      if ((int) c >= 56320)
        return (int) c <= 57343;
      return false;
    }

    /// <summary>指示字符串中指定位置处的 <see cref="T:System.Char" /> 对象是否为低代理项。</summary>
    /// <returns>如果 true 中指定字符的数值范围是从 U+DC00 到 U+DFFF，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <paramref name="s" /> 中的位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsLowSurrogate(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index");
      return char.IsLowSurrogate(s[index]);
    }

    /// <summary>指示字符串中指定位置处的两个相邻 <see cref="T:System.Char" /> 对象是否形成一个代理项对。</summary>
    /// <returns>如果 true 参数包括 <paramref name="s" /> 和 <paramref name="index" /> + 1 位置处的相邻字符，并且 <paramref name="index" /> 位置处字符的数值范围从 U+D800 到 U+DBFF，<paramref name="index" />+1 位置处字符的数值范围从 U+DC00 到 U+DFFF，则为 <paramref name="index" />；否则为 false。</returns>
    /// <param name="s">一个字符串。</param>
    /// <param name="index">
    /// <paramref name="s" /> 中要计算的字符对的开始位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <paramref name="s" /> 中的位置。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSurrogatePair(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index");
      if (index + 1 < s.Length)
        return char.IsSurrogatePair(s[index], s[index + 1]);
      return false;
    }

    /// <summary>指示两个指定的 <see cref="T:System.Char" /> 对象是否形成一个代理项对。</summary>
    /// <returns>如果 true 参数的数值范围是从 U+D800 到 U+DBFF，且 <paramref name="highSurrogate" /> 参数的数值是从 U+DC00 到 U+DFFF，则为 <paramref name="lowSurrogate" />；否则为 false。</returns>
    /// <param name="highSurrogate">要作为代理项对的高代理项进行计算的字符。</param>
    /// <param name="lowSurrogate">要作为代理项对的低代理项进行计算的字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
    {
      if ((int) highSurrogate >= 55296 && (int) highSurrogate <= 56319 && (int) lowSurrogate >= 56320)
        return (int) lowSurrogate <= 57343;
      return false;
    }

    /// <summary>将指定的 Unicode 码位转换为 UTF-16 编码字符串。</summary>
    /// <returns>由一个 <see cref="T:System.Char" /> 对象或一个 <see cref="T:System.Char" /> 对象的代理项对组成的字符串，等效于 <paramref name="utf32" /> 参数所指定的码位。</returns>
    /// <param name="utf32">21 位 Unicode 码位。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="utf32" /> 不是从 U+0 到 U+10FFFF 的有效的 21 位 Unicode 码位，不包括从 U+D800 到 U+DFFF 的代理项对。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ConvertFromUtf32(int utf32)
    {
      if (utf32 < 0 || utf32 > 1114111 || utf32 >= 55296 && utf32 <= 57343)
        throw new ArgumentOutOfRangeException("utf32", Environment.GetResourceString("ArgumentOutOfRange_InvalidUTF32"));
      if (utf32 < 65536)
        return char.ToString((char) utf32);
      utf32 -= 65536;
      return new string(new char[2]
      {
        (char) (utf32 / 1024 + 55296),
        (char) (utf32 % 1024 + 56320)
      });
    }

    /// <summary>将 UTF-16 编码的代理项对的值转换为 Unicode 码位。</summary>
    /// <returns>
    /// <paramref name="highSurrogate" /> 和 <paramref name="lowSurrogate" /> 参数表示的 21 位 Unicode 码位。</returns>
    /// <param name="highSurrogate">高代理项代码单位（即代码单位从 U+D800 到 U+DBFF）。</param>
    /// <param name="lowSurrogate">低代理项代码单位（即代码单位从 U+DC00 到 U+DFFF）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="highSurrogate" /> 不在 U+D800 到 U+DBFF 的范围内，或 <paramref name="lowSurrogate" /> 不在 U+DC00 到 U+DFFF 的范围内。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
    {
      if (!char.IsHighSurrogate(highSurrogate))
        throw new ArgumentOutOfRangeException("highSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate"));
      if (!char.IsLowSurrogate(lowSurrogate))
        throw new ArgumentOutOfRangeException("lowSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate"));
      return ((int) highSurrogate - 55296) * 1024 + ((int) lowSurrogate - 56320) + 65536;
    }

    /// <summary>将字符串中指定位置的 UTF-16 编码字符或代理项对的值转换为 Unicode 码位。</summary>
    /// <returns>字符或代理项对表示的 21 位 Unicode 码位，该字符或代理项对在 <paramref name="s" /> 参数中的位置由 <paramref name="index" /> 参数指定。</returns>
    /// <param name="s">包含字符或代理项对的字符串。</param>
    /// <param name="index">字符或代理项对在 <paramref name="s" /> 中的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <paramref name="s" /> 中的位置。</exception>
    /// <exception cref="T:System.ArgumentException">指定的索引位置包含一个代理项对，并且该代理项对中的第一个字符不是有效的高代理项，或该对中的第二个字符不是有效的低代理项。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ConvertToUtf32(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int num1 = (int) s[index] - 55296;
      if (num1 < 0 || num1 > 2047)
        return (int) s[index];
      if (num1 <= 1023)
      {
        if (index < s.Length - 1)
        {
          int num2 = (int) s[index + 1] - 56320;
          if (num2 >= 0 && num2 <= 1023)
            return num1 * 1024 + num2 + 65536;
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", (object) index), "s");
        }
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", (object) index), "s");
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLowSurrogate", (object) index), "s");
    }
  }
}
