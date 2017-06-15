// Decompiled with JetBrains decompiler
// Type: System.Boolean
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
  /// <summary>表示布尔值（true 或 false）。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
  {
    /// <summary>将布尔值 true 表示为字符串。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly string TrueString = "True";
    /// <summary>将布尔值 false 表示为字符串。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly string FalseString = "False";
    private bool m_value;
    internal const int True = 1;
    internal const int False = 0;
    internal const string TrueLiteral = "True";
    internal const string FalseLiteral = "False";

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Boolean" /> 的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return !this ? 0 : 1;
    }

    /// <summary>将此实例的值转换为其等效字符串表示形式（“True”或“False”）。</summary>
    /// <returns>如果此实例的值为 true，则为 <see cref="F:System.Boolean.TrueString" />；如果此实例的值为 false，则为 <see cref="F:System.Boolean.FalseString" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return !this ? "False" : "True";
    }

    /// <summary>将此实例的值转换为其等效字符串表示形式（“True”或“False”）。</summary>
    /// <returns>如果此实例的值为 true，则为 <see cref="F:System.Boolean.TrueString" />；如果此实例的值为 false，则为 <see cref="F:System.Boolean.FalseString" />。</returns>
    /// <param name="provider">（保留）<see cref="T:System.IFormatProvider" /> 对象。 </param>
    /// <filterpriority>2</filterpriority>
    public string ToString(IFormatProvider provider)
    {
      return !this ? "False" : "True";
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Boolean" /> and has the same value as this instance; otherwise, false.</returns>
    /// <param name="obj">要与此实例进行比较的对象。 </param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is bool))
        return false;
      return this == (bool) obj;
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的 <see cref="T:System.Boolean" /> 对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值与此实例相同，则为 true；否则为 false。</returns>
    /// <param name="obj">要与此实例进行比较的 <see cref="T:System.Boolean" /> 值。</param>
    /// <filterpriority>2</filterpriority>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(bool obj)
    {
      return this == obj;
    }

    /// <summary>将此实例与指定对象进行比较，并返回一个指示二者关系的整数。</summary>
    /// <returns>一个有符号的整数，它指示此实例和 <paramref name="obj" /> 的相对顺序。返回值条件小于零此实例为 false 而 <paramref name="obj" /> 为 true。零此实例与 <paramref name="obj" /> 相等（或者都为 true，或者都为 false）。大于零此实例为 true 而 <paramref name="obj" /> 为 false。- 或 - <paramref name="obj" /> 为 null。 </returns>
    /// <param name="obj">要与此实例比较的对象，或 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 不是 <see cref="T:System.Boolean" />。 </exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      if (!(obj is bool))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeBoolean"));
      if (this == (bool) obj)
        return 0;
      return !this ? -1 : 1;
    }

    /// <summary>将此实例与指定的 <see cref="T:System.Boolean" /> 对象进行比较，返回一个指示二者关系的整数。</summary>
    /// <returns>一个有符号整数，它指示此实例和 <paramref name="value" /> 的相对值。返回值条件小于零此实例为 false 而 <paramref name="value" /> 为 true。零此实例与 <paramref name="value" /> 相等（或者都为 true，或者都为 false）。大于零此实例为 true 而 <paramref name="value" /> 为 false。</returns>
    /// <param name="value">要与此实例进行比较的 <see cref="T:System.Boolean" /> 对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(bool value)
    {
      if (this == value)
        return 0;
      return !this ? -1 : 1;
    }

    /// <summary>将逻辑值的指定字符串表示形式转换为其等效的 <see cref="T:System.Boolean" /> 值；如果该字符串不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" /> 的值，则会引发异常。</summary>
    /// <returns>如果 <paramref name="value" /> 等于 <see cref="F:System.Boolean.TrueString" /> 字段的值，则为 true；如果 <paramref name="value" /> 等于 <see cref="F:System.Boolean.FalseString" /> 字段的值，则为 false。</returns>
    /// <param name="value">包含要转换的值的字符串。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" /> 字段的值。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Parse(string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      bool result = false;
      if (!bool.TryParse(value, out result))
        throw new FormatException(Environment.GetResourceString("Format_BadBoolean"));
      return result;
    }

    /// <summary>尝试将逻辑值的指定字符串表示形式转换为其等效的 <see cref="T:System.Boolean" /> 值。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 <paramref name="value" /> 成功转换，则为 true；否则为 false。</returns>
    /// <param name="value">包含要转换的值的字符串。</param>
    /// <param name="result">如果转换成功，当 <paramref name="value" /> 等于 <see cref="F:System.Boolean.TrueString" /> 时，此方法返回时将包含 true；当 <paramref name="value" /> 等于 <see cref="F:System.Boolean.FalseString" /> 时，将包含 false。如果转换失败，则包含 false。如果 <paramref name="value" /> 为 null 或者不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" /> 字段的值，该转换将失败。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string value, out bool result)
    {
      result = false;
      if (value == null)
        return false;
      if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
      {
        result = true;
        return true;
      }
      if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
      {
        result = false;
        return true;
      }
      value = bool.TrimWhiteSpaceAndNull(value);
      if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
      {
        result = true;
        return true;
      }
      if (!"False".Equals(value, StringComparison.OrdinalIgnoreCase))
        return false;
      result = false;
      return true;
    }

    private static string TrimWhiteSpaceAndNull(string value)
    {
      int startIndex = 0;
      int index = value.Length - 1;
      char ch = char.MinValue;
      while (startIndex < value.Length && (char.IsWhiteSpace(value[startIndex]) || (int) value[startIndex] == (int) ch))
        ++startIndex;
      while (index >= startIndex && (char.IsWhiteSpace(value[index]) || (int) value[index] == (int) ch))
        --index;
      return value.Substring(startIndex, index - startIndex + 1);
    }

    /// <summary>返回值类型 <see cref="T:System.Boolean" /> 的 <see cref="T:System.TypeCode" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.Boolean" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Boolean;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Boolean", (object) "Char"));
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
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Boolean", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
