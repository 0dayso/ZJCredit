// Decompiled with JetBrains decompiler
// Type: System.Byte
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>表示一个 8 位无符号整数。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Byte : IComparable, IFormattable, IConvertible, IComparable<byte>, IEquatable<byte>
  {
    private byte m_value;
    /// <summary>表示 <see cref="T:System.Byte" /> 的最大可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const byte MaxValue = 255;
    /// <summary>表示 <see cref="T:System.Byte" /> 的最小可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const byte MinValue = 0;

    /// <summary>将此实例与指定对象进行比较并返回一个对二者的相对值的指示。</summary>
    /// <returns>一个有符号的整数，它指示此实例和 <paramref name="value" /> 的相对顺序。Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
    /// <param name="value">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> is not a <see cref="T:System.Byte" />. </exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is byte))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeByte"));
      return (int) this - (int) (byte) value;
    }

    /// <summary>将此实例与指定的 8 位无符号整数进行比较并返回对其相对值的指示。</summary>
    /// <returns>一个有符号的整数，它指示此实例和 <paramref name="value" /> 的相对顺序。Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
    /// <param name="value">要比较的 8 位无符号整数。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(byte value)
    {
      return (int) this - (int) value;
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 true 是 <paramref name="obj" /> 的实例并且等于此实例的值，则为 <see cref="T:System.Byte" />；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象，或为 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is byte))
        return false;
      return (int) this == (int) (byte) obj;
    }

    /// <summary>返回一个值，该值指示此实例和指定的 <see cref="T:System.Byte" /> 对象是否表示相同的值。</summary>
    /// <returns>如果 true 与此实例相等，则为 <paramref name="obj" />；否则为 false。</returns>
    /// <param name="obj">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(byte obj)
    {
      return (int) this == (int) obj;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Byte" /> 的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this;
    }

    /// <summary>将数字的字符串表示形式转换为它的等效 <see cref="T:System.Byte" /> 表示形式。</summary>
    /// <returns>一个字节值，它与 <paramref name="s" /> 中包含的数相等。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用 <see cref="F:System.Globalization.NumberStyles.Integer" /> 样式来进行解释。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not of the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte Parse(string s)
    {
      return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定样式的数字的字符串表示形式转换为它的等效 <see cref="T:System.Byte" />。</summary>
    /// <returns>一个字节值，它与 <paramref name="s" /> 中包含的数相等。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用由 <paramref name="style" /> 指定的样式来进行解释。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Integer" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not of the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return byte.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定的区域性特定格式的数字字符串表示形式转换为它的等效 <see cref="T:System.Byte" />。</summary>
    /// <returns>一个字节值，它与 <paramref name="s" /> 中包含的数相等。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用 <see cref="F:System.Globalization.NumberStyles.Integer" /> 样式来进行解释。</param>
    /// <param name="provider">一个对象，它提供有关 <paramref name="s" /> 的区域性特定分析信息。如果 <paramref name="provider" /> 为 null，则使用当前区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not of the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte Parse(string s, IFormatProvider provider)
    {
      return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将指定样式和区域性特定格式的数字的字符串表示形式转换为它的等效 <see cref="T:System.Byte" />。</summary>
    /// <returns>一个字节值，它与 <paramref name="s" /> 中包含的数相等。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用由 <paramref name="style" /> 指定的样式来进行解释。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Integer" />。</param>
    /// <param name="provider">一个对象，用于提供有关 <paramref name="s" /> 格式的区域性特定信息。如果 <paramref name="provider" /> 为 null，则使用当前区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not of the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static byte Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      int int32;
      try
      {
        int32 = Number.ParseInt32(s, style, info);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"), (Exception) ex);
      }
      if (int32 < 0 || int32 > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) int32;
    }

    /// <summary>尝试将数字的字符串表示形式转换为它的等效 <see cref="T:System.Byte" />，并返回一个指示转换是否成功的值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用 <see cref="F:System.Globalization.NumberStyles.Integer" /> 样式来进行解释。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <see cref="T:System.Byte" /> 中所包含的数字等效的 <paramref name="s" /> 值；如果转换失败，则包含零。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out byte result)
    {
      return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>将指定样式和区域性特定格式的数字的字符串表示形式转换为它的等效 <see cref="T:System.Byte" />。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的数字的字符串。该字符串使用由 <paramref name="style" /> 指定的样式来进行解释。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Integer" />。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。如果 <paramref name="provider" /> 为 null，则使用当前区域性。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中所含数字等效的 8 位无符号整数值；如果转换失败，则包含零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、格式不正确，或者表示的数字小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" />，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
    {
      NumberFormatInfo.ValidateParseStyleInteger(style);
      return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out byte result)
    {
      result = (byte) 0;
      int result1;
      if (!Number.TryParseInt32(s, style, info, out result1) || result1 < 0 || result1 > (int) byte.MaxValue)
        return false;
      result = (byte) result1;
      return true;
    }

    /// <summary>将当前 <see cref="T:System.Byte" /> 对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>此对象的值的字符串表示形式，由一系列从 0 到 9 之间的数字组成，不包含前导零。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的格式将当前 <see cref="T:System.Byte" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>按照 <see cref="T:System.Byte" /> 参数指定的方式进行格式设置的当前 <paramref name="format" /> 对象的字符串表示形式。</returns>
    /// <param name="format">一个数值格式字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> includes an unsupported specifier.Supported format specifiers are listed in the Remarks section.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatInt32((int) this, format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的区域性特定格式设置信息将当前 <see cref="T:System.Byte" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>此对象值的字符串表示形式，采用 <paramref name="provider" /> 参数所指定的格式。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatInt32((int) this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>使用指定的格式和区域性特定格式信息将当前 <see cref="T:System.Byte" /> 对象的值转换为它的等效字符串表示形式。</summary>
    /// <returns>按照 <see cref="T:System.Byte" /> 和 <paramref name="format" /> 参数指定的方式进行格式设置的当前 <paramref name="provider" /> 对象的字符串表示形式。</returns>
    /// <param name="format">标准或自定义的数值格式字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> includes an unsupported specifier.Supported format specifiers are listed in the Remarks section.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatInt32((int) this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>返回值类型 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.Byte" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.Byte" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Byte;
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
      return this;
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
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Byte", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
