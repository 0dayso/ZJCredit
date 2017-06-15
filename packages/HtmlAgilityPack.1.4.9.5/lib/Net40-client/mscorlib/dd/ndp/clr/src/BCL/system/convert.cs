// Decompiled with JetBrains decompiler
// Type: System.Convert
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>将一个基本数据类型转换为另一个基本数据类型。</summary>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public static class Convert
  {
    internal static readonly RuntimeType[] ConvertTypes = new RuntimeType[19]
    {
      (RuntimeType) typeof (Empty),
      (RuntimeType) typeof (object),
      (RuntimeType) typeof (System.DBNull),
      (RuntimeType) typeof (bool),
      (RuntimeType) typeof (char),
      (RuntimeType) typeof (sbyte),
      (RuntimeType) typeof (byte),
      (RuntimeType) typeof (short),
      (RuntimeType) typeof (ushort),
      (RuntimeType) typeof (int),
      (RuntimeType) typeof (uint),
      (RuntimeType) typeof (long),
      (RuntimeType) typeof (ulong),
      (RuntimeType) typeof (float),
      (RuntimeType) typeof (double),
      (RuntimeType) typeof (Decimal),
      (RuntimeType) typeof (DateTime),
      (RuntimeType) typeof (object),
      (RuntimeType) typeof (string)
    };
    private static readonly RuntimeType EnumType = (RuntimeType) typeof (Enum);
    internal static readonly char[] base64Table = new char[65]
    {
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z',
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm',
      'n',
      'o',
      'p',
      'q',
      'r',
      's',
      't',
      'u',
      'v',
      'w',
      'x',
      'y',
      'z',
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      '+',
      '/',
      '='
    };
    /// <summary>一个常数，用于表示没有数据的数据库列；即数据库为空。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly object DBNull = (object) System.DBNull.Value;
    private const int base64LineBreakPosition = 76;

    /// <summary>返回指定对象的 <see cref="T:System.TypeCode" />。</summary>
    /// <returns>
    /// <paramref name="value" /> 的 <see cref="T:System.TypeCode" />，或者如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.TypeCode.Empty" />。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TypeCode GetTypeCode(object value)
    {
      if (value == null)
        return TypeCode.Empty;
      IConvertible convertible = value as IConvertible;
      if (convertible != null)
        return convertible.GetTypeCode();
      return TypeCode.Object;
    }

    /// <summary>返回有关指定对象是否为 <see cref="T:System.DBNull" /> 类型的指示。</summary>
    /// <returns>如果 <paramref name="value" /> 为 <see cref="T:System.DBNull" /> 类型，则为 true；否则为 false。</returns>
    /// <param name="value">一个对象。 </param>
    /// <filterpriority>1</filterpriority>
    public static bool IsDBNull(object value)
    {
      if (value == System.DBNull.Value)
        return true;
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
        return false;
      return convertible.GetTypeCode() == TypeCode.DBNull;
    }

    /// <summary>返回指定类型的对象，其值等效于指定对象。</summary>
    /// <returns>一个对象，其基础类型为 <paramref name="typeCode" />，并且其值等效于 <paramref name="value" />。- 或 -如果 <paramref name="value" /> 为 null 并且 <paramref name="typeCode" /> 为 <see cref="F:System.TypeCode.Empty" />、<see cref="F:System.TypeCode.String" /> 或 <see cref="F:System.TypeCode.Object" />，则为空引用（在 Visual Basic 中为 Nothing）。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="typeCode">要返回的对象的类型。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。- 或 -<paramref name="value" /> 为 null，而且 <paramref name="typeCode" /> 指定一个值类型。- 或 -<paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <paramref name="typeCode" /> 类型可以识别的格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示超出 <paramref name="typeCode" /> 类型范围的数字。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeCode" /> 无效。 </exception>
    /// <filterpriority>1</filterpriority>
    public static object ChangeType(object value, TypeCode typeCode)
    {
      return Convert.ChangeType(value, typeCode, (IFormatProvider) Thread.CurrentThread.CurrentCulture);
    }

    /// <summary>返回指定类型的对象，其值等效于指定对象。参数提供区域性特定的格式设置信息。</summary>
    /// <returns>一个对象，其基础类型为 <paramref name="typeCode" />，并且其值等效于 <paramref name="value" />。- 或 -如果 <paramref name="value" /> 为 null 并且 <paramref name="typeCode" /> 为 <see cref="F:System.TypeCode.Empty" />、<see cref="F:System.TypeCode.String" /> 或 <see cref="F:System.TypeCode.Object" />，则为空引用（在 Visual Basic 中为 Nothing）。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="typeCode">要返回的对象的类型。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。- 或 -<paramref name="value" /> 为 null，而且 <paramref name="typeCode" /> 指定一个值类型。- 或 -<paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不是 <paramref name="provider" /> 可以识别的 <paramref name="typeCode" /> 类型的格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示超出 <paramref name="typeCode" /> 类型范围的数字。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeCode" /> 无效。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
    {
      if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
        return (object) null;
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
      switch (typeCode)
      {
        case TypeCode.Empty:
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
        case TypeCode.Object:
          return value;
        case TypeCode.DBNull:
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
        case TypeCode.Boolean:
          return (object) convertible.ToBoolean(provider);
        case TypeCode.Char:
          return (object) convertible.ToChar(provider);
        case TypeCode.SByte:
          return (object) convertible.ToSByte(provider);
        case TypeCode.Byte:
          return (object) convertible.ToByte(provider);
        case TypeCode.Int16:
          return (object) convertible.ToInt16(provider);
        case TypeCode.UInt16:
          return (object) convertible.ToUInt16(provider);
        case TypeCode.Int32:
          return (object) convertible.ToInt32(provider);
        case TypeCode.UInt32:
          return (object) convertible.ToUInt32(provider);
        case TypeCode.Int64:
          return (object) convertible.ToInt64(provider);
        case TypeCode.UInt64:
          return (object) convertible.ToUInt64(provider);
        case TypeCode.Single:
          return (object) convertible.ToSingle(provider);
        case TypeCode.Double:
          return (object) convertible.ToDouble(provider);
        case TypeCode.Decimal:
          return (object) convertible.ToDecimal(provider);
        case TypeCode.DateTime:
          return (object) convertible.ToDateTime(provider);
        case TypeCode.String:
          return (object) convertible.ToString(provider);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_UnknownTypeCode"));
      }
    }

    internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException("targetType");
      RuntimeType runtimeType = targetType as RuntimeType;
      if (runtimeType != (RuntimeType) null)
      {
        if (value.GetType() == targetType)
          return (object) value;
        if (runtimeType == Convert.ConvertTypes[3])
          return (object) value.ToBoolean(provider);
        if (runtimeType == Convert.ConvertTypes[4])
          return (object) value.ToChar(provider);
        if (runtimeType == Convert.ConvertTypes[5])
          return (object) value.ToSByte(provider);
        if (runtimeType == Convert.ConvertTypes[6])
          return (object) value.ToByte(provider);
        if (runtimeType == Convert.ConvertTypes[7])
          return (object) value.ToInt16(provider);
        if (runtimeType == Convert.ConvertTypes[8])
          return (object) value.ToUInt16(provider);
        if (runtimeType == Convert.ConvertTypes[9])
          return (object) value.ToInt32(provider);
        if (runtimeType == Convert.ConvertTypes[10])
          return (object) value.ToUInt32(provider);
        if (runtimeType == Convert.ConvertTypes[11])
          return (object) value.ToInt64(provider);
        if (runtimeType == Convert.ConvertTypes[12])
          return (object) value.ToUInt64(provider);
        if (runtimeType == Convert.ConvertTypes[13])
          return (object) value.ToSingle(provider);
        if (runtimeType == Convert.ConvertTypes[14])
          return (object) value.ToDouble(provider);
        if (runtimeType == Convert.ConvertTypes[15])
          return (object) value.ToDecimal(provider);
        if (runtimeType == Convert.ConvertTypes[16])
          return (object) value.ToDateTime(provider);
        if (runtimeType == Convert.ConvertTypes[18])
          return (object) value.ToString(provider);
        if (runtimeType == Convert.ConvertTypes[1])
          return (object) value;
        if (runtimeType == Convert.EnumType)
          return (object) (Enum) value;
        if (runtimeType == Convert.ConvertTypes[2])
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_DBNull"));
        if (runtimeType == Convert.ConvertTypes[0])
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_Empty"));
      }
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) value.GetType().FullName, (object) targetType.FullName));
    }

    /// <summary>返回一个指定类型的对象，该对象的值等效于指定的对象。</summary>
    /// <returns>一个对象，其类型为 <paramref name="conversionType" />，并且其值等效于 <paramref name="value" />。- 或 -如果 <paramref name="value" /> 为 null，并且 <paramref name="conversionType" /> 不是值类型，则为空引用（在 Visual Basic 中为 Nothing）。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="conversionType">要返回的对象的类型。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。- 或 -<paramref name="value" /> 为 null，而且 <paramref name="conversionType" /> 是值类型。- 或 -<paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="conversionType" /> 无法识别<paramref name="value" />的格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示超出 <paramref name="conversionType" /> 范围的数字。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="conversionType" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static object ChangeType(object value, Type conversionType)
    {
      return Convert.ChangeType(value, conversionType, (IFormatProvider) Thread.CurrentThread.CurrentCulture);
    }

    /// <summary>返回指定类型的对象，其值等效于指定对象。参数提供区域性特定的格式设置信息。</summary>
    /// <returns>一个对象，其类型为 <paramref name="conversionType" />，并且其值等效于 <paramref name="value" />。- 或 - 如果 <paramref name="value" /> 的 <see cref="T:System.Type" /> 与 <paramref name="conversionType" /> 相等，则为 <paramref name="value" />。- 或 -如果 <paramref name="value" /> 为 null，并且 <paramref name="conversionType" /> 不是值类型，则为空引用（在 Visual Basic 中为 Nothing）。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="conversionType">要返回的对象的类型。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。- 或 -<paramref name="value" /> 为 null，而且 <paramref name="conversionType" /> 是值类型。- 或 -<paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不是 <paramref name="provider" /> 可以识别的 <paramref name="conversionType" /> 的格式。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示超出 <paramref name="conversionType" /> 范围的数字。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="conversionType" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
    {
      if (conversionType == (Type) null)
        throw new ArgumentNullException("conversionType");
      if (value == null)
      {
        if (conversionType.IsValueType)
          throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCastNullToValueType"));
        return (object) null;
      }
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
      {
        if (value.GetType() == conversionType)
          return value;
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_IConvertible"));
      }
      RuntimeType runtimeType = conversionType as RuntimeType;
      if (runtimeType == Convert.ConvertTypes[3])
        return (object) convertible.ToBoolean(provider);
      if (runtimeType == Convert.ConvertTypes[4])
        return (object) convertible.ToChar(provider);
      if (runtimeType == Convert.ConvertTypes[5])
        return (object) convertible.ToSByte(provider);
      if (runtimeType == Convert.ConvertTypes[6])
        return (object) convertible.ToByte(provider);
      if (runtimeType == Convert.ConvertTypes[7])
        return (object) convertible.ToInt16(provider);
      if (runtimeType == Convert.ConvertTypes[8])
        return (object) convertible.ToUInt16(provider);
      if (runtimeType == Convert.ConvertTypes[9])
        return (object) convertible.ToInt32(provider);
      if (runtimeType == Convert.ConvertTypes[10])
        return (object) convertible.ToUInt32(provider);
      if (runtimeType == Convert.ConvertTypes[11])
        return (object) convertible.ToInt64(provider);
      if (runtimeType == Convert.ConvertTypes[12])
        return (object) convertible.ToUInt64(provider);
      if (runtimeType == Convert.ConvertTypes[13])
        return (object) convertible.ToSingle(provider);
      if (runtimeType == Convert.ConvertTypes[14])
        return (object) convertible.ToDouble(provider);
      if (runtimeType == Convert.ConvertTypes[15])
        return (object) convertible.ToDecimal(provider);
      if (runtimeType == Convert.ConvertTypes[16])
        return (object) convertible.ToDateTime(provider);
      if (runtimeType == Convert.ConvertTypes[18])
        return (object) convertible.ToString(provider);
      if (runtimeType == Convert.ConvertTypes[1])
        return value;
      return convertible.ToType(conversionType, provider);
    }

    /// <summary>将指定对象的值转换为等效的布尔值。</summary>
    /// <returns>true 或 false，它将反映通过对 <paramref name="value" /> 的基础类型调用 <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> 方法而返回的值。如果 <paramref name="value" /> 为 null，则此方法返回 false。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。 </param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 是一个字符串，它不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" />。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持 <paramref name="value" /> 转换为 <see cref="T:System.Boolean" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToBoolean((IFormatProvider) null);
      return false;
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定对象的值转换为等效的布尔值。</summary>
    /// <returns>true 或 false，它将反映通过对 <paramref name="value" /> 的基础类型调用 <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> 方法而返回的值。如果 <paramref name="value" /> 为 null，则此方法返回 false。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。 </param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 是一个字符串，它不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" />。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持 <paramref name="value" /> 转换为 <see cref="T:System.Boolean" />。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToBoolean(provider);
      return false;
    }

    /// <summary>返回指定的布尔值；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的布尔值。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(bool value)
    {
      return value;
    }

    /// <summary>将指定的 8 位有符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 8 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(sbyte value)
    {
      return (uint) value > 0U;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool ToBoolean(char value)
    {
      return ((IConvertible) value).ToBoolean((IFormatProvider) null);
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 8 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(byte value)
    {
      return (uint) value > 0U;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 16 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(short value)
    {
      return (uint) value > 0U;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 16 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(ushort value)
    {
      return (uint) value > 0U;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 32 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(int value)
    {
      return (uint) value > 0U;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 32 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(uint value)
    {
      return value > 0U;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 64 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(long value)
    {
      return (ulong) value > 0UL;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的 64 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static bool ToBoolean(ulong value)
    {
      return value > 0UL;
    }

    /// <summary>将逻辑值的指定字符串表示形式转换为其等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 等于 <see cref="F:System.Boolean.TrueString" />，则为 true；如果 <paramref name="value" /> 等于 <see cref="F:System.Boolean.FalseString" /> 或 null，则为 false。</returns>
    /// <param name="value">包含 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" /> 值的字符串。 </param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(string value)
    {
      if (value == null)
        return false;
      return bool.Parse(value);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将逻辑值的指定字符串表示形式转换为其等效的布尔值。</summary>
    /// <returns>true if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or false if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or null.</returns>
    /// <param name="value">包含 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" /> 值的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。忽略此参数。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不等于 <see cref="F:System.Boolean.TrueString" /> 或 <see cref="F:System.Boolean.FalseString" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(string value, IFormatProvider provider)
    {
      if (value == null)
        return false;
      return bool.Parse(value);
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(float value)
    {
      return (double) value != 0.0;
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(double value)
    {
      return value != 0.0;
    }

    /// <summary>将指定的十进制数字的值转换为等效的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 为非零值，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的数字。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(Decimal value)
    {
      return value != Decimal.Zero;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool ToBoolean(DateTime value)
    {
      return ((IConvertible) value).ToBoolean((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 Unicode 字符。</summary>
    /// <returns>与 value 等效的 Unicode 字符，如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.Char.MinValue" />。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 是空字符串。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持 <paramref name="value" /> 转换为 <see cref="T:System.Char" />。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Char.MinValue" /> 或大于 <see cref="F:System.Char.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToChar((IFormatProvider) null);
      return char.MinValue;
    }

    /// <summary>使用指定的区域性特定格式设置信息将指定对象的值转换为其等效的 Unicode 字符。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 Unicode 字符，如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.Char.MinValue" />。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 是空字符串。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持 <paramref name="value" /> 转换为 <see cref="T:System.Char" />。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Char.MinValue" /> 或大于 <see cref="F:System.Char.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToChar(provider);
      return char.MinValue;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static char ToChar(bool value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>返回指定的 Unicode 字符值；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 Unicode 字符。 </param>
    /// <filterpriority>1</filterpriority>
    public static char ToChar(char value)
    {
      return value;
    }

    /// <summary>将指定的 8 位有符号整数的值转换为它的等效 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Char.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(sbyte value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为其等效的 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(byte value)
    {
      return (char) value;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为它的等效 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Char.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(short value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为其等效的 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(ushort value)
    {
      return (char) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为它的等效 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Char.MinValue" /> 或大于 <see cref="F:System.Char.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(int value)
    {
      if (value < 0 || value > (int) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为其等效的 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Char.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(uint value)
    {
      if (value > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为它的等效 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Char.MinValue" /> 或大于 <see cref="F:System.Char.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(long value)
    {
      if (value < 0L || value > (long) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为其等效的 Unicode 字符。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Char.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static char ToChar(ulong value)
    {
      if (value > (ulong) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"));
      return (char) value;
    }

    /// <summary>将指定字符串的第一个字符转换为 Unicode 字符。</summary>
    /// <returns>与 <paramref name="value" /> 中第一个且仅有的字符等效的 Unicode 字符。</returns>
    /// <param name="value">长度为 1 的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的长度不是 1。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(string value)
    {
      return Convert.ToChar(value, (IFormatProvider) null);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定字符串的第一个字符转换为 Unicode 字符。</summary>
    /// <returns>与 <paramref name="value" /> 中第一个且仅有的字符等效的 Unicode 字符。</returns>
    /// <param name="value">长度为 1 或 null 的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。忽略此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的长度不是 1。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(string value, IFormatProvider provider)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (value.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
      return value[0];
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的单精度浮点数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static char ToChar(float value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的双精度浮点数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static char ToChar(double value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static char ToChar(Decimal value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static char ToChar(DateTime value)
    {
      return ((IConvertible) value).ToChar((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 8 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。 </exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.SByte.MinValue" /> 或大于 <see cref="F:System.SByte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToSByte((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 8 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。 </exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.SByte.MinValue" /> 或大于 <see cref="F:System.SByte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToSByte(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 8 位带符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(bool value)
    {
      return !value ? (sbyte) 0 : (sbyte) 1;
    }

    /// <summary>返回指定的 8 位有符号整数；不执行实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 8 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(sbyte value)
    {
      return value;
    }

    /// <summary>将指定的 Unicode 字符的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(char value)
    {
      if ((int) value > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(byte value)
    {
      if ((int) value > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 8-bit signed integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(short value)
    {
      if ((int) value < (int) sbyte.MinValue || (int) value > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(ushort value)
    {
      if ((int) value > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(int value)
    {
      if (value < (int) sbyte.MinValue || value > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(uint value)
    {
      if ((long) value > (long) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(long value)
    {
      if (value < (long) sbyte.MinValue || value > (long) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(ulong value)
    {
      if (value > (ulong) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 8 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 8 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(float value)
    {
      return Convert.ToSByte((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 8 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 8 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(double value)
    {
      return Convert.ToSByte(Convert.ToInt32(value));
    }

    /// <summary>将指定的十进制数的值转换为等效的 8 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 8 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.SByte.MaxValue" /> 或小于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(Decimal value)
    {
      return Decimal.ToSByte(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 8 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 8 位带符号整数，如果 value 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.SByte.MinValue" /> 或大于 <see cref="F:System.SByte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(string value)
    {
      if (value == null)
        return 0;
      return sbyte.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 8 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.SByte.MinValue" /> 或大于 <see cref="F:System.SByte.MaxValue" /> 的数字。 </exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(string value, IFormatProvider provider)
    {
      return sbyte.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static sbyte ToSByte(DateTime value)
    {
      return ((IConvertible) value).ToSByte((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Byte" /> 值的属性格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" />。- 或 -不支持从 <paramref name="value" /> 到 <see cref="T:System.Byte" /> 类型的转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToByte((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定对象的值转换为 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Byte" /> 值的属性格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" />。- 或 -不支持从 <paramref name="value" /> 到 <see cref="T:System.Byte" /> 类型的转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToByte(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 8 位无符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(bool value)
    {
      return !value ? (byte) 0 : (byte) 1;
    }

    /// <summary>返回指定的 8 位无符号整数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 8 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(byte value)
    {
      return value;
    }

    /// <summary>将指定 Unicode 字符的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示的数字大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(char value)
    {
      if ((int) value > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的 8 位有符号整数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位有符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Byte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(sbyte value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(short value)
    {
      if ((int) value < 0 || (int) value > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(ushort value)
    {
      if ((int) value > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(int value)
    {
      if (value < 0 || value > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(uint value)
    {
      if (value > (uint) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(long value)
    {
      if (value < 0L || value > (long) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Byte.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte ToByte(ulong value)
    {
      if (value > (ulong) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 8 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">一个单精度浮点数字。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Byte.MaxValue" /> 或小于 <see cref="F:System.Byte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(float value)
    {
      return Convert.ToByte((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 8 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Byte.MaxValue" /> 或小于 <see cref="F:System.Byte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(double value)
    {
      return Convert.ToByte(Convert.ToInt32(value));
    }

    /// <summary>将指定的十进制数的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 8 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的数字。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Byte.MaxValue" /> 或小于 <see cref="F:System.Byte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(Decimal value)
    {
      return Decimal.ToByte(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(string value)
    {
      if (value == null)
        return 0;
      return byte.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 8 位无符号整数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的 8 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" /> 的数字。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return byte.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static byte ToByte(DateTime value)
    {
      return ((IConvertible) value).ToByte((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 16 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Int16" /> 类型的相应格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int16.MinValue" /> 或大于 <see cref="F:System.Int16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToInt16((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 16 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Int16" /> 类型的相应格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" />。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int16.MinValue" /> 或大于 <see cref="F:System.Int16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToInt16(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 16 位带符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(bool value)
    {
      return !value ? (short) 0 : (short) 1;
    }

    /// <summary>将指定的 Unicode 字符的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数。 </returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(char value)
    {
      if ((int) value > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>将指定的 8 位带符号整数的值转换为等效的 16 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(sbyte value)
    {
      return (short) value;
    }

    /// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(byte value)
    {
      return (short) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(ushort value)
    {
      if ((int) value > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>等效于 <paramref name="value" /> 的 16 位有符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" /> 或小于 <see cref="F:System.Int16.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(int value)
    {
      if (value < (int) short.MinValue || value > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(uint value)
    {
      if ((long) value > (long) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>返回指定的 16 位有符号整数；不执行实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 16 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(short value)
    {
      return value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" /> 或小于 <see cref="F:System.Int16.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(long value)
    {
      if (value < (long) short.MinValue || value > (long) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static short ToInt16(ulong value)
    {
      if (value > (ulong) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 16 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 16 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" /> 或小于 <see cref="F:System.Int16.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(float value)
    {
      return Convert.ToInt16((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 16 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 16 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" /> 或小于 <see cref="F:System.Int16.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(double value)
    {
      return Convert.ToInt16(Convert.ToInt32(value));
    }

    /// <summary>将指定的十进制数的值转换为等效的 16 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 16 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int16.MaxValue" /> 或小于 <see cref="F:System.Int16.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(Decimal value)
    {
      return Decimal.ToInt16(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 16 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 16 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int16.MinValue" /> 或大于 <see cref="F:System.Int16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(string value)
    {
      if (value == null)
        return 0;
      return short.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 16 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int16.MinValue" /> 或大于 <see cref="F:System.Int16.MaxValue" /> 的数字。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return short.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static short ToInt16(DateTime value)
    {
      return ((IConvertible) value).ToInt16((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt16.MinValue" /> 或大于 <see cref="F:System.UInt16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt16((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt16.MinValue" /> 或大于 <see cref="F:System.UInt16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt16(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 16 位无符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(bool value)
    {
      return !value ? (ushort) 0 : (ushort) 1;
    }

    /// <summary>将指定 Unicode 字符的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>等效于 <paramref name="value" /> 的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(char value)
    {
      return (ushort) value;
    }

    /// <summary>将指定的 8 位有符号整数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(sbyte value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit unsigned integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(byte value)
    {
      return (ushort) value;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(short value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(int value)
    {
      if (value < 0 || value > (int) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>返回指定的 16 位无符号整数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 16 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(ushort value)
    {
      return value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(uint value)
    {
      if (value > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(long value)
    {
      if (value < 0L || value > (long) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(ulong value)
    {
      if (value > (ulong) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 16 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(float value)
    {
      return Convert.ToUInt16((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 16 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(double value)
    {
      return Convert.ToUInt16(Convert.ToInt32(value));
    }

    /// <summary>将指定的十进制数的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 16 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt16.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(Decimal value)
    {
      return Decimal.ToUInt16(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 16 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt16.MinValue" /> 或大于 <see cref="F:System.UInt16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(string value)
    {
      if (value == null)
        return 0;
      return ushort.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 16 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt16.MinValue" /> 或大于 <see cref="F:System.UInt16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return ushort.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static ushort ToUInt16(DateTime value)
    {
      return ((IConvertible) value).ToUInt16((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 32 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int32.MinValue" /> 或大于 <see cref="F:System.Int32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToInt32((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 32 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" />。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int32.MinValue" /> 或大于 <see cref="F:System.Int32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToInt32(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 32 位带符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(bool value)
    {
      return !value ? 0 : 1;
    }

    /// <summary>将指定的 Unicode 字符的值转换为等效的 32 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(char value)
    {
      return (int) value;
    }

    /// <summary>将指定的 8 位带符号整数的值转换为等效的 32 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(sbyte value)
    {
      return (int) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的 32 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(byte value)
    {
      return (int) value;
    }

    /// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 32-bit signed integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(short value)
    {
      return (int) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的 32 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(ushort value)
    {
      return (int) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的 32 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(uint value)
    {
      if (value > (uint) int.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return (int) value;
    }

    /// <summary>返回指定的 32 位有符号整数；不执行实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 32 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(int value)
    {
      return value;
    }

    /// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit signed integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int32.MaxValue" /> 或小于 <see cref="F:System.Int32.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(long value)
    {
      if (value < (long) int.MinValue || value > (long) int.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return (int) value;
    }

    /// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int ToInt32(ulong value)
    {
      if (value > (ulong) int.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return (int) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 32 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 32 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int32.MaxValue" /> 或小于 <see cref="F:System.Int32.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(float value)
    {
      return Convert.ToInt32((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 32 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 32 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int32.MaxValue" /> 或小于 <see cref="F:System.Int32.MinValue" />。 </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(double value)
    {
      if (value >= 0.0)
      {
        if (value < 2147483647.5)
        {
          int num1 = (int) value;
          double num2 = value - (double) num1;
          if (num2 > 0.5 || num2 == 0.5 && (num1 & 1) != 0)
            ++num1;
          return num1;
        }
      }
      else if (value >= -2147483648.5)
      {
        int num1 = (int) value;
        double num2 = value - (double) num1;
        if (num2 < -0.5 || num2 == -0.5 && (num1 & 1) != 0)
          --num1;
        return num1;
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
    }

    /// <summary>将指定的十进制数的值转换为等效的 32 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 32 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int32.MaxValue" /> 或小于 <see cref="F:System.Int32.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int ToInt32(Decimal value)
    {
      return Decimal.FCallToInt32(value);
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 32 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 32 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int32.MinValue" /> 或大于 <see cref="F:System.Int32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(string value)
    {
      if (value == null)
        return 0;
      return int.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 32 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int32.MinValue" /> 或大于 <see cref="F:System.Int32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return int.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static int ToInt32(DateTime value)
    {
      return ((IConvertible) value).ToInt32((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt32.MinValue" /> 或大于 <see cref="F:System.UInt32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt32((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt32.MinValue" /> 或大于 <see cref="F:System.UInt32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt32(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 32 位无符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(bool value)
    {
      return !value ? 0U : 1U;
    }

    /// <summary>将指定 Unicode 字符的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(char value)
    {
      return (uint) value;
    }

    /// <summary>将指定的 8 位有符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(sbyte value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(byte value)
    {
      return (uint) value;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(short value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(ushort value)
    {
      return (uint) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(int value)
    {
      if (value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>返回指定的 32 位无符号整数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 32 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(uint value)
    {
      return value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(long value)
    {
      if (value < 0L || value > (long) uint.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.UInt32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(ulong value)
    {
      if (value > (ulong) uint.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return (uint) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 32 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(float value)
    {
      return Convert.ToUInt32((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 32 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(double value)
    {
      if (value < -0.5 || value >= 4294967295.5)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      uint num1 = (uint) value;
      double num2 = value - (double) num1;
      if (num2 > 0.5 || num2 == 0.5 && ((int) num1 & 1) != 0)
        ++num1;
      return num1;
    }

    /// <summary>将指定的十进制数的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 32 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(Decimal value)
    {
      return Decimal.ToUInt32(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 32 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt32.MinValue" /> 或大于 <see cref="F:System.UInt32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(string value)
    {
      if (value == null)
        return 0;
      return uint.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 32 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt32.MinValue" /> 或大于 <see cref="F:System.UInt32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return uint.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static uint ToUInt32(DateTime value)
    {
      return ((IConvertible) value).ToUInt32((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 64 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToInt64((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 64 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToInt64(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 64 位带符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(bool value)
    {
      return value ? 1L : 0L;
    }

    /// <summary>将指定的 Unicode 字符的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(char value)
    {
      return (long) value;
    }

    /// <summary>将指定的 8 位带符号整数的值转换为等效的 64 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(sbyte value)
    {
      return (long) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(byte value)
    {
      return (long) value;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(short value)
    {
      return (long) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(ushort value)
    {
      return (long) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(int value)
    {
      return (long) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(uint value)
    {
      return (long) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int64.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static long ToInt64(ulong value)
    {
      if (value > 9223372036854775807UL)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
      return (long) value;
    }

    /// <summary>返回指定的 64 位有符号整数；不执行实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">64 位带符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(long value)
    {
      return value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 64 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 64 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int64.MaxValue" /> 或小于 <see cref="F:System.Int64.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(float value)
    {
      return Convert.ToInt64((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 64 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 64 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int64.MaxValue" /> 或小于 <see cref="F:System.Int64.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(double value)
    {
      return checked ((long) Math.Round(value));
    }

    /// <summary>将指定的十进制数的值转换为等效的 64 位带符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 64 位带符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Int64.MaxValue" /> 或小于 <see cref="F:System.Int64.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(Decimal value)
    {
      return Decimal.ToInt64(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 64 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 64 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(string value)
    {
      if (value == null)
        return 0;
      return long.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位带符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 64 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return long.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static long ToInt64(DateTime value)
    {
      return ((IConvertible) value).ToInt64((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt64.MinValue" /> 或大于 <see cref="F:System.UInt64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt64((IFormatProvider) null);
      return 0;
    }

    /// <summary>使用指定的区域性特定格式信息，将指定对象的值转换为 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt64.MinValue" /> 或大于 <see cref="F:System.UInt64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToUInt64(provider);
      return 0;
    }

    /// <summary>将指定的布尔值转换为等效的 64 位无符号整数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(bool value)
    {
      return !value ? 0UL : 1UL;
    }

    /// <summary>将指定 Unicode 字符的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(char value)
    {
      return (ulong) value;
    }

    /// <summary>将指定的 8 位有符号整数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(sbyte value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(byte value)
    {
      return (ulong) value;
    }

    /// <summary>将指定的 16 位有符号整数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(short value)
    {
      if ((int) value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(ushort value)
    {
      return (ulong) value;
    }

    /// <summary>将指定的 32 位有符号整数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(int value)
    {
      if (value < 0)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit unsigned integer.</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(uint value)
    {
      return (ulong) value;
    }

    /// <summary>将指定的 64 位有符号整数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(long value)
    {
      if (value < 0L)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return (ulong) value;
    }

    /// <summary>返回指定的 64 位无符号整数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的 64 位无符号整数。 </param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(ulong value)
    {
      return value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 64 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的单精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt64.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(float value)
    {
      return Convert.ToUInt64((double) value);
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 64 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的双精度浮点数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt64.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(double value)
    {
      return checked ((ulong) Math.Round(value));
    }

    /// <summary>将指定的十进制数的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" />，舍入为最接近的 64 位无符号整数。如果 <paramref name="value" /> 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
    /// <param name="value">要转换的十进制数。 </param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 小于零或大于 <see cref="F:System.UInt64.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(Decimal value)
    {
      return Decimal.ToUInt64(Decimal.Round(value, 0));
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 64 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt64.MinValue" /> 或大于 <see cref="F:System.UInt64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(string value)
    {
      if (value == null)
        return 0;
      return ulong.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 64 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是由一个可选符号后跟数字序列（0 到 9）组成的。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.UInt64.MinValue" /> 或大于 <see cref="F:System.UInt64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0;
      return ulong.Parse(value, NumberStyles.Integer, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static ulong ToUInt64(DateTime value)
    {
      return ((IConvertible) value).ToUInt64((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为单精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的单精度浮点数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Single.MinValue" /> 或大于 <see cref="F:System.Single.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToSingle((IFormatProvider) null);
      return 0.0f;
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定对象的值转换为单精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的单精度浮点数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 的格式不正确。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" />。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Single.MinValue" /> 或大于 <see cref="F:System.Single.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToSingle(provider);
      return 0.0f;
    }

    /// <summary>将指定的 8 位带符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(sbyte value)
    {
      return (float) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(byte value)
    {
      return (float) value;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static float ToSingle(char value)
    {
      return ((IConvertible) value).ToSingle((IFormatProvider) null);
    }

    /// <summary>将指定的 16 位带符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(short value)
    {
      return (float) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(ushort value)
    {
      return (float) value;
    }

    /// <summary>将指定的 32 位带符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(int value)
    {
      return (float) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(uint value)
    {
      return (float) value;
    }

    /// <summary>将指定的 64 位带符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(long value)
    {
      return (float) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static float ToSingle(ulong value)
    {
      return (float) value;
    }

    /// <summary>返回指定的单精度浮点数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的单精度浮点数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(float value)
    {
      return value;
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。<paramref name="value" /> 使用"舍入到最接近的数字"规则进行舍入。例如，当舍入为两位小数时，值 2.345 变成 2.34，而值 2.355 变成 2.36。</returns>
    /// <param name="value">要转换的双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(double value)
    {
      return (float) value;
    }

    /// <summary>将指定的十进制数的值转换为等效的单精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的单精度浮点数。<paramref name="value" /> 使用"舍入到最接近的数字"规则进行舍入。例如，当舍入为两位小数时，值 2.345 变成 2.34，而值 2.355 变成 2.36。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(Decimal value)
    {
      return (float) value;
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的单精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的单精度浮点数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是一个有效格式的数字。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Single.MinValue" /> 或大于 <see cref="F:System.Single.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(string value)
    {
      if (value == null)
        return 0.0f;
      return float.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的单精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的单精度浮点数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是一个有效格式的数字。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Single.MinValue" /> 或大于 <see cref="F:System.Single.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0.0f;
      return float.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }

    /// <summary>将指定的布尔值转换为等效的单精度浮点数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static float ToSingle(bool value)
    {
      return value ? 1f : 0.0f;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static float ToSingle(DateTime value)
    {
      return ((IConvertible) value).ToSingle((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为双精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的双精度浮点数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Double" /> 类型的相应格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Double.MinValue" /> 或大于 <see cref="F:System.Double.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToDouble((IFormatProvider) null);
      return 0.0;
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定对象的值转换为双精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的双精度浮点数，如果 <paramref name="value" /> 为 null，则为零。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Double" /> 类型的相应格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Double.MinValue" /> 或大于 <see cref="F:System.Double.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToDouble(provider);
      return 0.0;
    }

    /// <summary>将指定的 8 位带符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的 8 位带符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(sbyte value)
    {
      return (double) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的双精度浮点数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(byte value)
    {
      return (double) value;
    }

    /// <summary>将指定的 16 位带符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>等效于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(short value)
    {
      return (double) value;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static double ToDouble(char value)
    {
      return ((IConvertible) value).ToDouble((IFormatProvider) null);
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(ushort value)
    {
      return (double) value;
    }

    /// <summary>将指定的 32 位带符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(int value)
    {
      return (double) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(uint value)
    {
      return (double) value;
    }

    /// <summary>将指定的 64 位带符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(long value)
    {
      return (double) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static double ToDouble(ulong value)
    {
      return (double) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">单精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(float value)
    {
      return (double) value;
    }

    /// <summary>返回指定的双精度浮点数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的双精度浮点数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(double value)
    {
      return value;
    }

    /// <summary>将指定的十进制数的值转换为等效的双精度浮点数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的双精度浮点数。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(Decimal value)
    {
      return (double) value;
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的双精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的双精度浮点数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是一个有效格式的数字。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Double.MinValue" /> 或大于 <see cref="F:System.Double.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(string value)
    {
      if (value == null)
        return 0.0;
      return double.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的双精度浮点数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的双精度浮点数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是一个有效格式的数字。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Double.MinValue" /> 或大于 <see cref="F:System.Double.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(string value, IFormatProvider provider)
    {
      if (value == null)
        return 0.0;
      return double.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }

    /// <summary>将指定的布尔值转换为等效的双精度浮点数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double ToDouble(bool value)
    {
      return value ? 1.0 : 0.0;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static double ToDouble(DateTime value)
    {
      return ((IConvertible) value).ToDouble((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为等效的十进制数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的十进制数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Decimal" /> 类型的相应格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Decimal.MinValue" /> 或大于 <see cref="F:System.Decimal.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToDecimal((IFormatProvider) null);
      return Decimal.Zero;
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定对象的值转换为等效的十进制数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的十进制数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 未采用 <see cref="T:System.Decimal" /> 类型的相应格式。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Decimal.MinValue" /> 或大于 <see cref="F:System.Decimal.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToDecimal(provider);
      return Decimal.Zero;
    }

    /// <summary>将指定的 8 位带符号整数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(sbyte value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的 8 位无符号整数的值转换为等效的十进制数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的十进制数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(byte value)
    {
      return (Decimal) value;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static Decimal ToDecimal(char value)
    {
      return ((IConvertible) value).ToDecimal((IFormatProvider) null);
    }

    /// <summary>将指定的 16 位带符号整数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(short value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的 16 位无符号整数的值转换为等效的十进制数。</summary>
    /// <returns>与 <paramref name="value" /> 等效的十进制数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(ushort value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的 32 位带符号整数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(int value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的 32 位无符号整数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(uint value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的 64 位带符号整数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(long value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的 64 位无符号整数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(ulong value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的单精度浮点数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。 </returns>
    /// <param name="value">要转换的单精度浮点数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Decimal.MaxValue" /> 或小于 <see cref="F:System.Decimal.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(float value)
    {
      return (Decimal) value;
    }

    /// <summary>将指定的双精度浮点数的值转换为等效的十进制数。</summary>
    /// <returns>一个等于 <paramref name="value" /> 的十进制数。 </returns>
    /// <param name="value">要转换的双精度浮点数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 大于 <see cref="F:System.Decimal.MaxValue" /> 或小于 <see cref="F:System.Decimal.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(double value)
    {
      return (Decimal) value;
    }

    /// <summary>将数字的指定字符串表示形式转换为等效的十进制数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的十进制数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是一个有效格式的数字。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Decimal.MinValue" /> 或大于 <see cref="F:System.Decimal.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(string value)
    {
      if (value == null)
        return Decimal.Zero;
      return Decimal.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的十进制数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的十进制数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是一个有效格式的数字。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 表示小于 <see cref="F:System.Decimal.MinValue" /> 或大于 <see cref="F:System.Decimal.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(string value, IFormatProvider provider)
    {
      if (value == null)
        return Decimal.Zero;
      return Decimal.Parse(value, NumberStyles.Number, provider);
    }

    /// <summary>返回指定的十进制数；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">一个小数。 </param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(Decimal value)
    {
      return value;
    }

    /// <summary>将指定的布尔值转换为等效的十进制数。</summary>
    /// <returns>如果 <paramref name="value" /> 为 true，则为数字 1；否则，为 0。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal ToDecimal(bool value)
    {
      return (Decimal) (value ? 1 : 0);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static Decimal ToDecimal(DateTime value)
    {
      return ((IConvertible) value).ToDecimal((IFormatProvider) null);
    }

    /// <summary>返回指定的 <see cref="T:System.DateTime" /> 对象；不执行任何实际的转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">日期和时间值。 </param>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(DateTime value)
    {
      return value;
    }

    /// <summary>将指定对象的值转换为 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>
    /// <paramref name="value" /> 的值的日期和时间等效项，如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.DateTime.MinValue" /> 的日期和时间等效项。</returns>
    /// <param name="value">用于实现 <see cref="T:System.IConvertible" /> 接口的对象，或为 null。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是有效的日期和时间值。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(object value)
    {
      if (value != null)
        return ((IConvertible) value).ToDateTime((IFormatProvider) null);
      return DateTime.MinValue;
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定对象的值转换为 <see cref="T:System.DateTime" /> 对象。</summary>
    /// <returns>
    /// <paramref name="value" /> 的值的日期和时间等效项，如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.DateTime.MinValue" /> 的日期和时间等效项。</returns>
    /// <param name="value">一个实现 <see cref="T:System.IConvertible" /> 接口的对象。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" />不是有效的日期和时间值。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不实现 <see cref="T:System.IConvertible" /> 接口。- 或 -不支持该转换。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(object value, IFormatProvider provider)
    {
      if (value != null)
        return ((IConvertible) value).ToDateTime(provider);
      return DateTime.MinValue;
    }

    /// <summary>将日期和时间的指定字符串表示形式转换为等效的日期和时间值。</summary>
    /// <returns>
    /// <paramref name="value" /> 的值的日期和时间等效项，如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.DateTime.MinValue" /> 的日期和时间等效项。</returns>
    /// <param name="value">日期和时间的字符串表示形式。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是格式正确的日期和时间字符串。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(string value)
    {
      if (value == null)
        return new DateTime(0L);
      return DateTime.Parse(value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将数字的指定字符串表示形式转换为等效的日期和时间。</summary>
    /// <returns>
    /// <paramref name="value" /> 的值的日期和时间等效项，如果 <paramref name="value" /> 为 null，则为 <see cref="F:System.DateTime.MinValue" /> 的日期和时间等效项。</returns>
    /// <param name="value">包含要转换的日期和时间的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 不是格式正确的日期和时间字符串。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static DateTime ToDateTime(string value, IFormatProvider provider)
    {
      if (value == null)
        return new DateTime(0L);
      return DateTime.Parse(value, provider);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(sbyte value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(byte value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(short value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(ushort value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(int value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(uint value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(long value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    public static DateTime ToDateTime(ulong value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(bool value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(char value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的单精度浮点值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(float value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的双精度浮点值。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(double value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.InvalidCastException" />。</summary>
    /// <returns>不支持此转换。不返回任何值。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <exception cref="T:System.InvalidCastException">不支持此转换。</exception>
    /// <filterpriority>1</filterpriority>
    public static DateTime ToDateTime(Decimal value)
    {
      return ((IConvertible) value).ToDateTime((IFormatProvider) null);
    }

    /// <summary>将指定对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式；如果 <paramref name="value" /> 是一个值为 null 的对象，则为 <see cref="F:System.String.Empty" />。如果 <paramref name="value" /> 为 null，则此方法返回 null。</returns>
    /// <param name="value">一个对象，用于提供要转换的值，或 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(object value)
    {
      return Convert.ToString(value, (IFormatProvider) null);
    }

    /// <summary>使用指定的区域性特定格式设置信息将指定对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式；如果 <paramref name="value" /> 是一个值为 null 的对象，则为 <see cref="F:System.String.Empty" />。如果 <paramref name="value" /> 为 null，则此方法返回 null。</returns>
    /// <param name="value">一个对象，用于提供要转换的值，或 null。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(object value, IFormatProvider provider)
    {
      IConvertible convertible = value as IConvertible;
      if (convertible != null)
        return convertible.ToString(provider);
      IFormattable formattable = value as IFormattable;
      if (formattable != null)
        return formattable.ToString((string) null, provider);
      if (value != null)
        return value.ToString();
      return string.Empty;
    }

    /// <summary>将指定的布尔值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(bool value)
    {
      return value.ToString();
    }

    /// <summary>将指定的布尔值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的布尔值。</param>
    /// <param name="provider">一个对象的实例。忽略此参数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(bool value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 Unicode 字符的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(char value)
    {
      return char.ToString(value);
    }

    /// <summary>使用指定的区域性特定格式设置信息将指定的 Unicode 字符的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。忽略此参数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(char value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 8 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(sbyte value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 8 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(sbyte value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 8 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(byte value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 8 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(byte value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 16 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(short value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 16 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(short value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 16 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ushort value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 16 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ushort value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 32 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(int value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 32 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(int value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 32 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(uint value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 32 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(uint value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 64 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(long value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 64 位带符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(long value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 64 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ulong value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的 64 位无符号整数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static string ToString(ulong value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的单精度浮点数的值转换其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的单精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(float value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定的单精度浮点数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的单精度浮点数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(float value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的双精度浮点数的值转换其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(double value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>将指定的双精度浮点数的值转换其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的双精度浮点数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(double value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的十进制数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(Decimal value)
    {
      return value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>使用指定的区域性特定格式设置信息将指定的十进制数的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(Decimal value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>将指定的 <see cref="T:System.DateTime" /> 的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(DateTime value)
    {
      return value.ToString();
    }

    /// <summary>使用指定的区域性特定格式设置信息，将指定 <see cref="T:System.DateTime" /> 的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="value">要转换的日期和时间值。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(DateTime value, IFormatProvider provider)
    {
      return value.ToString(provider);
    }

    /// <summary>返回指定的字符串实例；不执行任何实际转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的字符串。 </param>
    /// <filterpriority>1</filterpriority>
    public static string ToString(string value)
    {
      return value;
    }

    /// <summary>返回指定的字符串实例；不执行任何实际转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 不经更改即返回。</returns>
    /// <param name="value">要返回的字符串。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。忽略此参数。</param>
    /// <filterpriority>1</filterpriority>
    public static string ToString(string value, IFormatProvider provider)
    {
      return value;
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 8 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 8 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个 10 为基的无符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.Byte.MinValue" /> 或大于 <see cref="F:System.Byte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int @int = ParseNumbers.StringToInt(value, fromBase, 4608);
      if (@int < 0 || @int > (int) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) @int;
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 8 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 8 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.SByte.MinValue" /> 或大于 <see cref="F:System.SByte.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int @int = ParseNumbers.StringToInt(value, fromBase, 5120);
      if (fromBase != 10 && @int <= (int) byte.MaxValue || @int >= (int) sbyte.MinValue && @int <= (int) sbyte.MaxValue)
        return (sbyte) @int;
      throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 16 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 16 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.Int16.MinValue" /> 或大于 <see cref="F:System.Int16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int @int = ParseNumbers.StringToInt(value, fromBase, 6144);
      if (fromBase != 10 && @int <= (int) ushort.MaxValue || @int >= (int) short.MinValue && @int <= (int) short.MaxValue)
        return (short) @int;
      throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 16 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.UInt16.MinValue" /> 或大于 <see cref="F:System.UInt16.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      int @int = ParseNumbers.StringToInt(value, fromBase, 4608);
      if (@int < 0 || @int > (int) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) @int;
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 32 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 32 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.Int32.MinValue" /> 或大于 <see cref="F:System.Int32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToInt32(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.StringToInt(value, fromBase, 4096);
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 32 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.UInt32.MinValue" /> 或大于 <see cref="F:System.UInt32.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return (uint) ParseNumbers.StringToInt(value, fromBase, 4608);
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 64 位带符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的有符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.Int64.MinValue" /> 或大于 <see cref="F:System.Int64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long ToInt64(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.StringToLong(value, fromBase, 4096);
    }

    /// <summary>将指定基数的数字的字符串表示形式转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 中数字等效的 64 位无符号整数，如果 <paramref name="value" /> 为 null，则为 0（零）。</returns>
    /// <param name="value">包含要转换的数字的字符串。</param>
    /// <param name="fromBase">
    /// <paramref name="value" /> 中数字的基数，它必须是 2、8、10 或 16。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fromBase" /> 不是 2、8、10 或 16。- 或 -<paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为 <see cref="F:System.String.Empty" />。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="value" /> 包含的一个字符不是 <paramref name="fromBase" /> 指定的基中的有效数字。如果 <paramref name="value" /> 中的第一个字符无效，异常消息则指示没有可转换的数字；否则，该消息将指示 <paramref name="value" /> 包含无效的尾随字符。</exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" />，它表示一个非 10 为基的无符号数，前面带一个负号。- 或 -<paramref name="value" /> 表示小于 <see cref="F:System.UInt64.MinValue" /> 或大于 <see cref="F:System.UInt64.MaxValue" /> 的数字。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(string value, int fromBase)
    {
      if (fromBase != 2 && fromBase != 8 && (fromBase != 10 && fromBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return (ulong) ParseNumbers.StringToLong(value, fromBase, 4608);
    }

    /// <summary>将 8 位无符号整数的值转换为其等效的指定基数的字符串表示形式。</summary>
    /// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <param name="toBase">返回值的基数，必须是 2、8、10 或 16。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="toBase" /> 不是 2、8、10 或 16。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(byte value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.IntToString((int) value, toBase, -1, ' ', 64);
    }

    /// <summary>将 16 位带符号整数的值转换为其指定基的等效字符串表示形式。</summary>
    /// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
    /// <param name="value">要转换的 16 位带符号整数。</param>
    /// <param name="toBase">返回值的基数，必须是 2、8、10 或 16。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="toBase" /> 不是 2、8、10 或 16。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(short value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.IntToString((int) value, toBase, -1, ' ', 128);
    }

    /// <summary>将 32 位带符号整数的值转换为其指定基的等效字符串表示形式。</summary>
    /// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <param name="toBase">返回值的基数，必须是 2、8、10 或 16。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="toBase" /> 不是 2、8、10 或 16。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(int value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
    }

    /// <summary>将 64 位带符号整数的值转换为其指定基的等效字符串表示形式。</summary>
    /// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <param name="toBase">返回值的基数，必须是 2、8、10 或 16。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="toBase" /> 不是 2、8、10 或 16。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string ToString(long value, int toBase)
    {
      if (toBase != 2 && toBase != 8 && (toBase != 10 && toBase != 16))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidBase"));
      return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
    }

    /// <summary>将 8 位无符号整数的数组转换为其用 Base64 数字编码的等效字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="inArray" /> 的内容的字符串表示形式，以 Base64 表示。</returns>
    /// <param name="inArray">一个 8 位无符号整数数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToBase64String(byte[] inArray)
    {
      if (inArray == null)
        throw new ArgumentNullException("inArray");
      return Convert.ToBase64String(inArray, 0, inArray.Length, Base64FormattingOptions.None);
    }

    /// <summary>将 8 位无符号整数的数组转换为其用 Base64 数字编码的等效字符串表示形式。参数指定是否在返回值中插入分行符。</summary>
    /// <returns>
    /// <paramref name="inArray" /> 中元素的字符串表示形式，以 Base64 表示。</returns>
    /// <param name="inArray">一个 8 位无符号整数数组。 </param>
    /// <param name="options">如果每 76 个字符插入一个分行符，则使用 <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" />，如果不插入分行符，则使用 <see cref="F:System.Base64FormattingOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是有效的 <see cref="T:System.Base64FormattingOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
    {
      if (inArray == null)
        throw new ArgumentNullException("inArray");
      return Convert.ToBase64String(inArray, 0, inArray.Length, options);
    }

    /// <summary>将 8 位无符号整数数组的子集转换为其用 Base64 数字编码的等效字符串表示形式。参数将子集指定为输入数组中的偏移量和数组中要转换的元素数。</summary>
    /// <returns>
    /// <paramref name="inArray" /> 中从位置 <paramref name="offset" /> 开始的 <paramref name="length" /> 个元素的字符串表示形式，以 Base64 表示。</returns>
    /// <param name="inArray">一个 8 位无符号整数数组。</param>
    /// <param name="offset">
    /// <paramref name="inArray" /> 中的偏移量。</param>
    /// <param name="length">要转换的 <paramref name="inArray" /> 的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="length" /> 为负。- 或 - <paramref name="offset" /> 加上 <paramref name="length" /> 大于 <paramref name="inArray" /> 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToBase64String(byte[] inArray, int offset, int length)
    {
      return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
    }

    /// <summary>将 8 位无符号整数数组的子集转换为其用 Base64 数字编码的等效字符串表示形式。参数指定作为输入数组中偏移量的子集、数组中要转换的元素数以及是否在返回值中插入分行符。</summary>
    /// <returns>
    /// <paramref name="inArray" /> 中从位置 <paramref name="offset" /> 开始的 <paramref name="length" /> 个元素的字符串表示形式，以 Base64 表示。</returns>
    /// <param name="inArray">一个 8 位无符号整数数组。</param>
    /// <param name="offset">
    /// <paramref name="inArray" /> 中的偏移量。</param>
    /// <param name="length">要转换的 <paramref name="inArray" /> 的元素数。 </param>
    /// <param name="options">如果每 76 个字符插入一个分行符，则使用 <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" />，如果不插入分行符，则使用 <see cref="F:System.Base64FormattingOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="length" /> 为负。- 或 - <paramref name="offset" /> 加上 <paramref name="length" /> 大于 <paramref name="inArray" /> 的长度。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是有效的 <see cref="T:System.Base64FormattingOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static unsafe string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
    {
      if (inArray == null)
        throw new ArgumentNullException("inArray");
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      int length1 = inArray.Length;
      if (offset > length1 - length)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      if (length1 == 0)
        return string.Empty;
      bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
      string str1;
      string str2 = str1 = string.FastAllocateString(Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks));
      char* outChars = (char*) str2;
      if ((IntPtr) outChars != IntPtr.Zero)
        outChars += RuntimeHelpers.OffsetToStringData;
      fixed (byte* inData = inArray)
      {
        Convert.ConvertToBase64Array(outChars, inData, offset, length, insertLineBreaks);
        return str1;
      }
    }

    /// <summary>将 8 位无符号整数数组的子集转换为用 Base64 数字编码的 Unicode 字符数组的等价子集。参数将子集指定为输入和输出数组中的偏移量和输入数组中要转换的元素数。</summary>
    /// <returns>包含 <paramref name="outArray" /> 中的字节数的 32 位有符号整数。</returns>
    /// <param name="inArray">8 位无符号整数的输入数组。</param>
    /// <param name="offsetIn">
    /// <paramref name="inArray" /> 内的一个位置。</param>
    /// <param name="length">要转换的 <paramref name="inArray" /> 的元素数。</param>
    /// <param name="outArray">Unicode 字符的输出数组。</param>
    /// <param name="offsetOut">
    /// <paramref name="outArray" /> 内的一个位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 或 <paramref name="outArray" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offsetIn" />、<paramref name="offsetOut" /> 或 <paramref name="length" /> 为负。- 或 - <paramref name="offsetIn" /> 加上 <paramref name="length" /> 大于 <paramref name="inArray" /> 的长度。- 或 - <paramref name="offsetOut" /> 加上要返回的元素数大于 <paramref name="outArray" /> 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
    {
      return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
    }

    /// <summary>将 8 位无符号整数数组的子集转换为用 Base64 数字编码的 Unicode 字符数组的等价子集。参数指定作为输入和输出数组中偏移量的子集、输入数组中要转换的元素数以及是否在输出数组中插入分行符。</summary>
    /// <returns>包含 <paramref name="outArray" /> 中的字节数的 32 位有符号整数。</returns>
    /// <param name="inArray">8 位无符号整数的输入数组。</param>
    /// <param name="offsetIn">
    /// <paramref name="inArray" /> 内的一个位置。</param>
    /// <param name="length">要转换的 <paramref name="inArray" /> 的元素数。</param>
    /// <param name="outArray">Unicode 字符的输出数组。</param>
    /// <param name="offsetOut">
    /// <paramref name="outArray" /> 内的一个位置。 </param>
    /// <param name="options">如果每 76 个字符插入一个分行符，则使用 <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" />，如果不插入分行符，则使用 <see cref="F:System.Base64FormattingOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 或 <paramref name="outArray" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offsetIn" />、<paramref name="offsetOut" /> 或 <paramref name="length" /> 为负。- 或 - <paramref name="offsetIn" /> 加上 <paramref name="length" /> 大于 <paramref name="inArray" /> 的长度。- 或 - <paramref name="offsetOut" /> 加上要返回的元素数大于 <paramref name="outArray" /> 的长度。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是有效的 <see cref="T:System.Base64FormattingOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static unsafe int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
    {
      if (inArray == null)
        throw new ArgumentNullException("inArray");
      if (outArray == null)
        throw new ArgumentNullException("outArray");
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (offsetIn < 0)
        throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (offsetOut < 0)
        throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      int length1 = inArray.Length;
      if (offsetIn > length1 - length)
        throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      if (length1 == 0)
        return 0;
      bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
      int length2 = outArray.Length;
      int validateOutputLength = Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks);
      if (offsetOut > length2 - validateOutputLength)
        throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
      int base64Array;
      fixed (char* outChars = &outArray[offsetOut])
        fixed (byte* inData = inArray)
          base64Array = Convert.ConvertToBase64Array(outChars, inData, offsetIn, length, insertLineBreaks);
      return base64Array;
    }

    [SecurityCritical]
    private static unsafe int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
    {
      int num1 = length % 3;
      int num2 = offset + (length - num1);
      int index1 = 0;
      int num3 = 0;
      fixed (char* chPtr1 = Convert.base64Table)
      {
        int index2 = offset;
        while (index2 < num2)
        {
          if (insertLineBreaks)
          {
            if (num3 == 76)
            {
              char* chPtr2 = outChars;
              int num4 = index1;
              int num5 = 1;
              int num6 = num4 + num5;
              IntPtr num7 = (IntPtr) num4 * 2;
              *(short*) ((IntPtr) chPtr2 + num7) = (short) 13;
              char* chPtr3 = outChars;
              int num8 = num6;
              int num9 = 1;
              index1 = num8 + num9;
              IntPtr num10 = (IntPtr) num8 * 2;
              *(short*) ((IntPtr) chPtr3 + num10) = (short) 10;
              num3 = 0;
            }
            num3 += 4;
          }
          outChars[index1] = chPtr1[((int) inData[index2] & 252) >> 2];
          outChars[index1 + 1] = chPtr1[((int) inData[index2] & 3) << 4 | ((int) inData[index2 + 1] & 240) >> 4];
          outChars[index1 + 2] = chPtr1[((int) inData[index2 + 1] & 15) << 2 | ((int) inData[index2 + 2] & 192) >> 6];
          outChars[index1 + 3] = chPtr1[(int) inData[index2 + 2] & 63];
          index1 += 4;
          index2 += 3;
        }
        int index3 = num2;
        if (insertLineBreaks && num1 != 0 && num3 == 76)
        {
          char* chPtr2 = outChars;
          int num4 = index1;
          int num5 = 1;
          int num6 = num4 + num5;
          IntPtr num7 = (IntPtr) num4 * 2;
          *(short*) ((IntPtr) chPtr2 + num7) = (short) 13;
          char* chPtr3 = outChars;
          int num8 = num6;
          int num9 = 1;
          index1 = num8 + num9;
          IntPtr num10 = (IntPtr) num8 * 2;
          *(short*) ((IntPtr) chPtr3 + num10) = (short) 10;
        }
        if (num1 != 1)
        {
          if (num1 == 2)
          {
            outChars[index1] = chPtr1[((int) inData[index3] & 252) >> 2];
            outChars[index1 + 1] = chPtr1[((int) inData[index3] & 3) << 4 | ((int) inData[index3 + 1] & 240) >> 4];
            outChars[index1 + 2] = chPtr1[((int) inData[index3 + 1] & 15) << 2];
            outChars[index1 + 3] = chPtr1[64];
            index1 += 4;
          }
        }
        else
        {
          outChars[index1] = chPtr1[((int) inData[index3] & 252) >> 2];
          outChars[index1 + 1] = chPtr1[((int) inData[index3] & 3) << 4];
          outChars[index1 + 2] = chPtr1[64];
          outChars[index1 + 3] = chPtr1[64];
          index1 += 4;
        }
      }
      return index1;
    }

    private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
    {
      long num1 = (long) inputLength / 3L * 4L + (inputLength % 3 != 0 ? 4L : 0L);
      if (num1 == 0L)
        return 0;
      if (insertLineBreaks)
      {
        long num2 = num1 / 76L;
        if (num1 % 76L == 0L)
          --num2;
        num1 += num2 * 2L;
      }
      if (num1 > (long) int.MaxValue)
        throw new OutOfMemoryException();
      return (int) num1;
    }

    /// <summary>将指定的字符串（它将二进制数据编码为 Base64 数字）转换为等效的 8 位无符号整数数组。</summary>
    /// <returns>与 <paramref name="s" /> 等效的 8 位无符号整数数组。</returns>
    /// <param name="s">要转换的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> 的长度（忽略空白字符）不是 0 或 4 的倍数。- 或 -<paramref name="s" /> 的格式无效。<paramref name="s" /> 包含一个非 base 64 字符、两个以上的填充字符或者在填充字符中包含非空白字符。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] FromBase64String(string s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      string str = s;
      char* inputPtr = (char*) str;
      if ((IntPtr) inputPtr != IntPtr.Zero)
        inputPtr += RuntimeHelpers.OffsetToStringData;
      return Convert.FromBase64CharPtr(inputPtr, s.Length);
    }

    /// <summary>将 Unicode 字符数组（它将二进制数据编码为 Base64 数字）的子集转换为等效的 8 位无符号整数数组。参数指定输入数组的子集以及要转换的元素数。</summary>
    /// <returns>等效于 <paramref name="inArray" /> 中位于 <paramref name="offset" /> 位置的 <paramref name="length" /> 元素的 8 位无符号整数数组。</returns>
    /// <param name="inArray">Unicode 字符数组。</param>
    /// <param name="offset">
    /// <paramref name="inArray" /> 内的一个位置。</param>
    /// <param name="length">要转换的 <paramref name="inArray" /> 中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inArray" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="length" /> 小于 0。- 或 - <paramref name="offset" /> 和 <paramref name="length" /> 指示不在 <paramref name="inArray" /> 内的位置。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="inArray" /> 的长度（忽略空白字符）不是 0 或 4 的倍数。- 或 -<paramref name="inArray" /> 的格式无效。<paramref name="inArray" /> 包含一个非 base 64 字符、两个以上的填充字符或者在填充字符中包含非空白字符。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] FromBase64CharArray(char[] inArray, int offset, int length)
    {
      if (inArray == null)
        throw new ArgumentNullException("inArray");
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (offset > inArray.Length - length)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      fixed (char* chPtr = inArray)
        return Convert.FromBase64CharPtr(chPtr + offset, length);
    }

    [SecurityCritical]
    private static unsafe byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
    {
      for (; inputLength > 0; --inputLength)
      {
        switch (inputPtr[inputLength - 1])
        {
          case ' ':
          case '\n':
          case '\r':
          case '\t':
            goto case ' ';
          default:
            goto label_4;
        }
      }
label_4:
      int resultLength = Convert.FromBase64_ComputeResultLength(inputPtr, inputLength);
      byte[] numArray1;
      byte[] numArray2 = numArray1 = new byte[resultLength];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (byte* startDestPtr = &^(numArray1 == null || numArray2.Length == 0 ? (byte&) IntPtr.Zero : @numArray2[0]))
        Convert.FromBase64_Decode(inputPtr, inputLength, startDestPtr, resultLength);
      return numArray1;
    }

    [SecurityCritical]
    private static unsafe int FromBase64_Decode(char* startInputPtr, int inputLength, byte* startDestPtr, int destLength)
    {
      char* chPtr1 = startInputPtr;
      byte* numPtr1 = startDestPtr;
      char* chPtr2 = chPtr1 + inputLength;
      byte* numPtr2 = numPtr1 + destLength;
      uint num1 = (uint) byte.MaxValue;
      while (chPtr1 < chPtr2)
      {
        uint num2 = (uint) *chPtr1;
        chPtr1 += 2;
        uint num3;
        if (num2 - 65U <= 25U)
          num3 = num2 - 65U;
        else if (num2 - 97U <= 25U)
          num3 = num2 - 71U;
        else if (num2 - 48U <= 9U)
        {
          num3 = num2 - 4294967292U;
        }
        else
        {
          if (num2 <= 32U)
          {
            switch (num2)
            {
              case 9:
              case 10:
              case 13:
              case 32:
                continue;
            }
          }
          else if ((int) num2 != 43)
          {
            if ((int) num2 != 47)
            {
              if ((int) num2 == 61)
              {
                if (chPtr1 == chPtr2)
                {
                  uint num4 = num1 << 6;
                  if (((int) num4 & int.MinValue) == 0)
                    throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
                  if ((int) (numPtr2 - numPtr1) < 2)
                    return -1;
                  byte* numPtr3 = numPtr1;
                  int num5 = 1;
                  byte* numPtr4 = numPtr3 + num5;
                  int num6 = (int) (byte) (num4 >> 16);
                  *numPtr3 = (byte) num6;
                  byte* numPtr5 = numPtr4;
                  int num7 = 1;
                  numPtr1 = numPtr5 + num7;
                  int num8 = (int) (byte) (num4 >> 8);
                  *numPtr5 = (byte) num8;
                  num1 = (uint) byte.MaxValue;
                  break;
                }
                while ((UIntPtr) chPtr1 < (UIntPtr) chPtr2 - new UIntPtr(2))
                {
                  switch (*chPtr1)
                  {
                    case ' ':
                    case '\n':
                    case '\r':
                    case '\t':
                      chPtr1 += 2;
                      continue;
                    default:
                      goto label_29;
                  }
                }
label_29:
                if ((IntPtr) chPtr1 != (IntPtr) chPtr2 - 2 || (int) *chPtr1 != 61)
                  throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
                uint num9 = num1 << 12;
                if (((int) num9 & int.MinValue) == 0)
                  throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
                if ((int) (numPtr2 - numPtr1) < 1)
                  return -1;
                *numPtr1++ = (byte) (num9 >> 16);
                num1 = (uint) byte.MaxValue;
                break;
              }
            }
            else
            {
              num3 = 63U;
              goto label_16;
            }
          }
          else
          {
            num3 = 62U;
            goto label_16;
          }
          throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
        }
label_16:
        num1 = num1 << 6 | num3;
        if (((int) num1 & int.MinValue) != 0)
        {
          if ((int) (numPtr2 - numPtr1) < 3)
            return -1;
          *numPtr1 = (byte) (num1 >> 16);
          numPtr1[1] = (byte) (num1 >> 8);
          numPtr1[2] = (byte) num1;
          numPtr1 += 3;
          num1 = (uint) byte.MaxValue;
        }
      }
      if ((int) num1 != (int) byte.MaxValue)
        throw new FormatException(Environment.GetResourceString("Format_BadBase64CharArrayLength"));
      return (int) (numPtr1 - startDestPtr);
    }

    [SecurityCritical]
    private static unsafe int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
    {
      char* chPtr = inputPtr + inputLength;
      int num1 = inputLength;
      int num2 = 0;
      while (inputPtr < chPtr)
      {
        uint num3 = (uint) *inputPtr;
        inputPtr += 2;
        if (num3 <= 32U)
          --num1;
        else if ((int) num3 == 61)
        {
          --num1;
          ++num2;
        }
      }
      if (num2 != 0)
      {
        if (num2 == 1)
        {
          num2 = 2;
        }
        else
        {
          if (num2 != 2)
            throw new FormatException(Environment.GetResourceString("Format_BadBase64Char"));
          num2 = 1;
        }
      }
      return num1 / 4 * 3 + num2;
    }
  }
}
