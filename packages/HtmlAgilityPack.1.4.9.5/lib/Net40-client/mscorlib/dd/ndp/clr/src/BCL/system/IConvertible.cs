// Decompiled with JetBrains decompiler
// Type: System.IConvertible
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>定义特定的方法，这些方法将实现引用或值类型的值转换为具有等效值的公共语言运行时类型。</summary>
  /// <filterpriority>2</filterpriority>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IConvertible
  {
    /// <summary>返回此实例的 <see cref="T:System.TypeCode" />。</summary>
    /// <returns>枚举常数，它是实现该接口的类或值类型的 <see cref="T:System.TypeCode" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    TypeCode GetTypeCode();

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 Boolean 值。</summary>
    /// <returns>与此实例的值等效的 Boolean 值。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool ToBoolean(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 Unicode 字符。</summary>
    /// <returns>与此实例的值等效的 Unicode 字符。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    char ToChar(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 8 位有符号整数。</summary>
    /// <returns>与此实例的值等效的 8 位有符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    sbyte ToSByte(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将该实例的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>与该实例的值等效的 8 位无符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    byte ToByte(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 16 位有符号整数。</summary>
    /// <returns>与此实例的值等效的 16 位有符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    short ToInt16(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将该实例的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与该实例的值等效的 16 位无符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    ushort ToUInt16(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 32 位有符号整数。</summary>
    /// <returns>与此实例的值等效的 32 位有符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    int ToInt32(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将该实例的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与该实例的值等效的 32 位无符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    uint ToUInt32(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与此实例的值等效的 64 位有符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    long ToInt64(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将该实例的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与该实例的值等效的 64 位无符号整数。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    ulong ToUInt64(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的单精度浮点数字。</summary>
    /// <returns>与此实例的值等效的单精度浮点数字。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    float ToSingle(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的双精度浮点数字。</summary>
    /// <returns>与此实例的值等效的双精度浮点数字。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    double ToDouble(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 <see cref="T:System.Decimal" /> 数字。</summary>
    /// <returns>与此实例的值等效的 <see cref="T:System.Decimal" /> 数字。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    Decimal ToDecimal(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 <see cref="T:System.DateTime" />。</summary>
    /// <returns>与此实例的值等效的 <see cref="T:System.DateTime" /> 实例。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    DateTime ToDateTime(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为等效的 <see cref="T:System.String" />。</summary>
    /// <returns>与此实例的值等效的 <see cref="T:System.String" /> 实例。</returns>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    string ToString(IFormatProvider provider);

    /// <summary>使用指定的区域性特定格式设置信息将此实例的值转换为具有等效值的指定 <see cref="T:System.Type" /> 的 <see cref="T:System.Object" />。</summary>
    /// <returns>其值与此实例值等效的 <paramref name="conversionType" /> 类型的 <see cref="T:System.Object" /> 实例。</returns>
    /// <param name="conversionType">要将此实例的值转换为的 <see cref="T:System.Type" />。</param>
    /// <param name="provider">
    /// <see cref="T:System.IFormatProvider" /> 接口实现，提供区域性特定的格式设置信息。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object ToType(Type conversionType, IFormatProvider provider);
  }
}
