// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.FormatterConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>表示 <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> 接口的基实现，该接口使用 <see cref="T:System.Convert" /> 类和 <see cref="T:System.IConvertible" /> 接口。</summary>
  [ComVisible(true)]
  public class FormatterConverter : IFormatterConverter
  {
    /// <summary>将值转换为给定的 <see cref="T:System.Type" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <param name="type">
    /// <paramref name="value" /> 将转换成的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public object Convert(object value, Type type)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ChangeType(value, type, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为给定的 <see cref="T:System.TypeCode" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <param name="typeCode">
    /// <paramref name="value" /> 将转换成的 <see cref="T:System.TypeCode" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public object Convert(object value, TypeCode typeCode)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ChangeType(value, typeCode, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 <see cref="T:System.Boolean" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public bool ToBoolean(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToBoolean(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 Unicode 字符。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public char ToChar(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToChar(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 <see cref="T:System.SByte" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    [CLSCompliant(false)]
    public sbyte ToSByte(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToSByte(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 8 位无符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public byte ToByte(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToByte(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 16 位带符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public short ToInt16(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToInt16(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 16 位无符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    [CLSCompliant(false)]
    public ushort ToUInt16(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToUInt16(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 32 位带符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public int ToInt32(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 32 位无符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    [CLSCompliant(false)]
    public uint ToUInt32(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToUInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 64 位带符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public long ToInt64(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 64 位无符号整数。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    [CLSCompliant(false)]
    public ulong ToUInt64(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToUInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为单精度浮点数字。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public float ToSingle(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToSingle(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为双精度浮点数字。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public double ToDouble(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToDouble(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 <see cref="T:System.Decimal" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public Decimal ToDecimal(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToDecimal(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将值转换为 <see cref="T:System.DateTime" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public DateTime ToDateTime(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将指定对象转换为 <see cref="T:System.String" />。</summary>
    /// <returns>转换的 <paramref name="value" />；或者，如果 <paramref name="type" /> 参数为 null，则为 null。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 参数为 null。</exception>
    public string ToString(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
