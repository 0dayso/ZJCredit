// Decompiled with JetBrains decompiler
// Type: System.Double
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>表示一个双精度浮点数。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Double : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
  {
    internal static double NegativeZero = BitConverter.Int64BitsToDouble(long.MinValue);
    internal double m_value;
    /// <summary>表示 <see cref="T:System.Double" /> 的最小可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double MinValue = -1.79769313486232E+308;
    /// <summary>表示 <see cref="T:System.Double" /> 的最大可能值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double MaxValue = 1.79769313486232E+308;
    /// <summary>表示大于零的最小正 <see cref="T:System.Double" /> 值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double Epsilon = 4.94065645841247E-324;
    /// <summary>表示负无穷。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double NegativeInfinity = double.NegativeInfinity;
    /// <summary>表示正无穷。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double PositiveInfinity = double.PositiveInfinity;
    /// <summary>表示不是数字 (NaN) 的值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double NaN = double.NaN;

    /// <summary>返回一个值，该值指示两个指定的 <see cref="T:System.Double" /> 值是否相等。</summary>
    /// <returns>如果 true 和 <paramref name="left" /> 相等，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个值。</param>
    /// <param name="right">要比较的第二个值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator ==(double left, double right)
    {
      return left == right;
    }

    /// <summary>返回一个值，该值指示两个指定的 <see cref="T:System.Double" /> 值是否不相等。</summary>
    /// <returns>如果 true 和 <paramref name="left" /> 不相等，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个值。</param>
    /// <param name="right">要比较的第二个值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator !=(double left, double right)
    {
      return left != right;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Double" /> 值是否小于另一个指定的 <see cref="T:System.Double" /> 值。</summary>
    /// <returns>如果 <paramref name="left" /> 小于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个值。</param>
    /// <param name="right">要比较的第二个值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator <(double left, double right)
    {
      return left < right;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Double" /> 值是否大于另一个指定的 <see cref="T:System.Double" /> 值。</summary>
    /// <returns>如果 true 大于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个值。</param>
    /// <param name="right">要比较的第二个值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator >(double left, double right)
    {
      return left > right;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Double" /> 值是否小于或等于另一个指定的 <see cref="T:System.Double" /> 值。</summary>
    /// <returns>如果 <paramref name="left" /> 小于等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个值。</param>
    /// <param name="right">要比较的第二个值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator <=(double left, double right)
    {
      return left <= right;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Double" /> 值是否大于或等于另一个指定的 <see cref="T:System.Double" /> 值。</summary>
    /// <returns>如果 <paramref name="left" /> 大于等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个值。</param>
    /// <param name="right">要比较的第二个值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool operator >=(double left, double right)
    {
      return left >= right;
    }

    /// <summary>返回一个值，该值指示指定数字是计算为负无穷大还是正无穷大。 </summary>
    /// <returns>如果 <paramref name="d" /> 的计算结果为 <see cref="F:System.Double.PositiveInfinity" /> 或 <see cref="F:System.Double.NegativeInfinity" />，则为 true；否则为 false。</returns>
    /// <param name="d">一个双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsInfinity(double d)
    {
      return (*(long*) &d & long.MaxValue) == 9218868437227405312L;
    }

    /// <summary>返回一个值，通过该值指示指定数字是否计算为正无穷大。</summary>
    /// <returns>如果 <paramref name="d" /> 的计算结果为 <see cref="F:System.Double.PositiveInfinity" />，则为 true；否则为 false。</returns>
    /// <param name="d">一个双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool IsPositiveInfinity(double d)
    {
      return d == double.PositiveInfinity;
    }

    /// <summary>返回一个值，通过该值指示指定数字是否计算为负无穷大。</summary>
    /// <returns>如果 <paramref name="d" /> 的计算结果为 <see cref="F:System.Double.NegativeInfinity" />，则为 true；否则为 false。</returns>
    /// <param name="d">一个双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool IsNegativeInfinity(double d)
    {
      return d == double.NegativeInfinity;
    }

    [SecuritySafeCritical]
    internal static unsafe bool IsNegative(double d)
    {
      return (*(long*) &d & long.MinValue) == long.MinValue;
    }

    /// <summary>返回一个值，该值指示指定的值是否不为数字 (<see cref="F:System.Double.NaN" />)。</summary>
    /// <returns>如果 <paramref name="d" /> 的计算结果为 <see cref="F:System.Double.NaN" />，则为 true；否则为 false。</returns>
    /// <param name="d">一个双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool IsNaN(double d)
    {
      return (ulong) (*(long*) &d & long.MaxValue) > 9218868437227405312UL;
    }

    /// <summary>将此实例与指定对象进行比较，并返回一个整数，该整数指示此实例的值是小于、等于还是大于指定对象的值。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。Value Description A negative integer This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- This instance and <paramref name="value" /> are both Double.NaN, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />A positive integer This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />).-or- <paramref name="value" /> is null. </returns>
    /// <param name="value">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> is not a <see cref="T:System.Double" />. </exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is double))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDouble"));
      double d = (double) value;
      if (this < d)
        return -1;
      if (this > d)
        return 1;
      if (this == d)
        return 0;
      if (!double.IsNaN(this))
        return 1;
      return !double.IsNaN(d) ? -1 : 0;
    }

    /// <summary>将此实例与指定的双精度浮点数进行比较，并返回一个整数，该整数指示此实例的值是小于、等于还是大于指定双精度浮点数的值。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Double.NaN" />), <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />). </returns>
    /// <param name="value">要比较的双精度浮点数。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(double value)
    {
      if (this < value)
        return -1;
      if (this > value)
        return 1;
      if (this == value)
        return 0;
      if (!double.IsNaN(this))
        return 1;
      return !double.IsNaN(value) ? -1 : 0;
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 true 是 <paramref name="obj" /> 的实例并且等于此实例的值，则为 <see cref="T:System.Double" />；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is double))
        return false;
      double d = (double) obj;
      if (d == this)
        return true;
      if (double.IsNaN(d))
        return double.IsNaN(this);
      return false;
    }

    /// <summary>返回一个值，该值指示此实例和指定的 <see cref="T:System.Double" /> 对象是否表示相同的值。</summary>
    /// <returns>如果 true 与此实例相等，则为 <paramref name="obj" />；否则为 false。</returns>
    /// <param name="obj">要与此示例比较的 <see cref="T:System.Double" /> 对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(double obj)
    {
      if (obj == this)
        return true;
      if (double.IsNaN(obj))
        return double.IsNaN(this);
      return false;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      double num1 = this;
      if (num1 == 0.0)
        return 0;
      long num2 = *(long*) &num1;
      return (int) num2 ^ (int) (num2 >> 32);
    }

    /// <summary>将此实例的数值转换为其等效的字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatDouble(this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的格式，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 指定。</returns>
    /// <param name="format">一个数值格式字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> is invalid. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的区域性特定格式信息，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="provider" /> 指定。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatDouble(this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 和 <paramref name="provider" /> 指定。</returns>
    /// <param name="format">一个数值格式字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将数字的字符串表示形式转换为它的等效双精度浮点数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数值或符号等效的双精度浮点数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> does not represent a number in a valid format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Parse(string s)
    {
      return double.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定样式的数字的字符串表示形式转换为它的等效双精度浮点数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数值或符号等效的双精度浮点数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。一个要指定的典型值为 <see cref="F:System.Globalization.NumberStyles.Float" /> 和 <see cref="F:System.Globalization.NumberStyles.AllowThousands" /> 的组合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> does not represent a number in a valid format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return double.Parse(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定的区域性特定格式的数字的字符串表示形式转换为它的等效双精度浮点数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数值或符号等效的双精度浮点数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> does not represent a number in a valid format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Parse(string s, IFormatProvider provider)
    {
      return double.Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将指定样式和区域性特定格式的数字的字符串表示形式转换为它的等效双精度浮点数。</summary>
    /// <returns>与 <paramref name="s" /> 中指定的数值或符号等效的双精度浮点数。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="style">枚举值的按位组合，用于指示可出现在 <paramref name="s" /> 中的样式元素。一个用来指定的典型值为 <see cref="F:System.Globalization.NumberStyles.Float" /> 与 <see cref="F:System.Globalization.NumberStyles.AllowThousands" /> 的组合。</param>
    /// <param name="provider">一个对象，提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> does not represent a numeric value. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return double.Parse(s, style, NumberFormatInfo.GetInstance(provider));
    }

    private static double Parse(string s, NumberStyles style, NumberFormatInfo info)
    {
      return Number.ParseDouble(s, style, info);
    }

    /// <summary>将数字的字符串表示形式转换为它的等效双精度浮点数。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 参数等效的双精度浮点数；如果转换失败，则包含零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、不是有效格式的数字，或者表示的数字小于 <see cref="F:System.Double.MinValue" /> 或大于 <see cref="F:System.Double.MaxValue" />，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out double result)
    {
      return double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>将指定样式和区域性特定格式的数字的字符串表示形式转换为它的等效双精度浮点数。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">包含要转换的数字的字符串。</param>
    /// <param name="style">
    /// <see cref="T:System.Globalization.NumberStyles" /> 值的按位组合，指示 <paramref name="s" /> 的允许格式。一个用来指定的典型值为 <see cref="F:System.Globalization.NumberStyles.Float" /> 与 <see cref="F:System.Globalization.NumberStyles.AllowThousands" /> 的组合。</param>
    /// <param name="provider">一个 <see cref="T:System.IFormatProvider" />，它提供有关 <paramref name="s" /> 的区域性特定格式设置信息。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中所包含的数值或符号等效的双精度浮点数；如果转换失败，则包含零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、格式不符合 <paramref name="style" />、表示的数字小于<see cref="F:System.SByte.MinValue" /> 或大于 <see cref="F:System.SByte.MaxValue" />，或者 <paramref name="style" /> 不是 <see cref="T:System.Globalization.NumberStyles" /> 枚举的常数的有效组合，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out double result)
    {
      if (s == null)
      {
        result = 0.0;
        return false;
      }
      if (!Number.TryParseDouble(s, style, info, out result))
      {
        string str = s.Trim();
        if (str.Equals(info.PositiveInfinitySymbol))
          result = double.PositiveInfinity;
        else if (str.Equals(info.NegativeInfinitySymbol))
        {
          result = double.NegativeInfinity;
        }
        else
        {
          if (!str.Equals(info.NaNSymbol))
            return false;
          result = double.NaN;
        }
      }
      return true;
    }

    /// <summary>返回值类型 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.Double" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.Double" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Double;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Double", (object) "Char"));
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
      return this;
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Double", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
