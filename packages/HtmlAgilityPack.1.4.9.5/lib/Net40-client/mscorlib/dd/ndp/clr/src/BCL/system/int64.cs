// Decompiled with JetBrains decompiler
// Type: System.Int64
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>表示 64 位有符号整数。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Int64 : IComparable, IFormattable, IConvertible, IComparable<long>, IEquatable<long>
  {
    internal long m_value;
    /// <summary>表示 Int64 的最大可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long MaxValue = 9223372036854775807;
    /// <summary>表示 Int64 的最小可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const long MinValue = -9223372036854775808;

    /// <summary>将此实例与指定对象进行比较并返回一个对二者的相对值的指示。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。返回值 描述 小于零 此实例小于 <paramref name="value" />。零 此实例等于 <paramref name="value" />。大于零 此实例大于 <paramref name="value" />。- 或 - <paramref name="value" /> 为 null。</returns>
    /// <param name="value">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是 <see cref="T:System.Int64" />。</exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is long))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt64"));
      long num = (long) value;
      if (this < num)
        return -1;
      return this > num ? 1 : 0;
    }

    /// <summary>将此实例与指定的 64 位有符号整数进行比较并返回对其相对值的指示。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。返回值 描述 小于零 此实例小于 <paramref name="value" />。零 此实例等于 <paramref name="value" />。大于零 此实例大于 <paramref name="value" />。</returns>
    /// <param name="value">要比较的整数。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(long value)
    {
      if (this < value)
        return -1;
      return this > value ? 1 : 0;
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 true 是 <paramref name="obj" /> 的实例并且等于此实例的值，则为 <see cref="T:System.Int64" />；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is long))
        return false;
      return this == (long) obj;
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的 <see cref="T:System.Int64" /> 值。</summary>
    /// <returns>如果 true 的值与此实例相同，则为 <paramref name="obj" />；否则为 false。</returns>
    /// <param name="obj">要与此实例进行比较的 <see cref="T:System.Int64" /> 值。</param>
    /// <filterpriority>2</filterpriority>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(long obj)
    {
      return this == obj;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this ^ (int) (this >> 32);
    }

    /// <summary>将此实例的数值转换为其等效的字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由减号（如果值为负）和范围从 0 到 9 且没有前导零的数字序列组成。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatInt64(this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的区域性特定格式信息，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="provider" /> 指定。</returns>
    /// <param name="provider">一个 <see cref="T:System.IFormatProvider" />，它提供区域性特定的格式设置信息。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatInt64(this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>使用指定的格式，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 指定。</returns>
    /// <param name="format">一个数值格式字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 是无效或不受支持。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatInt64(this, format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 和 <paramref name="provider" /> 指定。</returns>
    /// <param name="format">一个数值格式字符串。</param>
    /// <param name="provider">一个对象，它提供有关此实例的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 是无效或不受支持。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatInt64(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将数字的字符串表示形式转换为它的等效 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="s" /> 中包含的数字等效的 64 位有符号整数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不在正确的格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> 表示一个数字小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Parse(string s)
    {
      return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定样式的数字的字符串表示形式转换为它的等效 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数字等效的 64 位带符号整数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="style">
    /// <see cref="T:System.Globalization.NumberStyles" /> 值的按位组合，指示 <paramref name="s" /> 的允许格式。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Integer" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> 不是 <see cref="T:System.Globalization.NumberStyles" /> 值。- 或 -<paramref name="style" /> 不是组合 <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> 和 <see cref="F:System.Globalization.NumberStyles.HexNumber" /> 值。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不是符合格式 <paramref name="style" />。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> 表示一个数字小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" />。- 或 -<paramref name="style" /> 支持小数位数，但 <paramref name="s" /> 包括非零的小数位。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return Number.ParseInt64(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定的区域性特定格式的数字的字符串表示形式转换为它的等效 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数字等效的 64 位带符号整数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不在正确的格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> 表示一个数字小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Parse(string s, IFormatProvider provider)
    {
      return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将指定样式和区域性特定格式的数字的字符串表示形式转换为它的等效 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数字等效的 64 位带符号整数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Integer" />。</param>
    /// <param name="provider">一个 <see cref="T:System.IFormatProvider" />，它提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> 不是 <see cref="T:System.Globalization.NumberStyles" /> 值。- 或 -<paramref name="style" /> 不是组合 <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> 和 <see cref="F:System.Globalization.NumberStyles.HexNumber" /> 值。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 不是符合格式 <paramref name="style" />。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> 表示一个数字小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" />。- 或 -<paramref name="style" /> 支持小数位数，但 <paramref name="s" /> 包括非零的小数位。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将数字的字符串表示形式转换为它的等效 64 位有符号整数。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中所包含的数字等效的 64 位无符号整数值；如果转换失败，则包含零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、格式不正确，或者表示的数字小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" />，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out long result)
    {
      return Number.TryParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>将指定样式和区域性特定格式的数字的字符串表示形式转换为它的等效 64 位有符号整数。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用由 <paramref name="style" /> 指定的样式来进行解释。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Integer" />。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中所包含的数字等效的 64 位无符号整数值；如果转换失败，则包含零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、格式不符合 <paramref name="style" />，或者表示的数字小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" />，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> 不是 <see cref="T:System.Globalization.NumberStyles" /> 值。- 或 -<paramref name="style" /> 不是组合 <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> 和 <see cref="F:System.Globalization.NumberStyles.HexNumber" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out long result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    /// <summary>返回值类型 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.Int64" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.Int64" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Int64;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(this);
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
      return this;
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this);
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Int64", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
