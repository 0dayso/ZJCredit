// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IFormatterConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>提供 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 的实例与格式化程序所提供的、最适用于分析 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 中的数据的类之间的连接。</summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface IFormatterConverter
  {
    /// <summary>将值转换为给定的 <see cref="T:System.Type" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <param name="type">
    /// <paramref name="value" /> 将转换成的 <see cref="T:System.Type" />。</param>
    object Convert(object value, Type type);

    /// <summary>将值转换为给定的 <see cref="T:System.TypeCode" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    /// <param name="typeCode">
    /// <paramref name="value" /> 将转换成的 <see cref="T:System.TypeCode" />。</param>
    object Convert(object value, TypeCode typeCode);

    /// <summary>将值转换为 <see cref="T:System.Boolean" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    bool ToBoolean(object value);

    /// <summary>将值转换为 Unicode 字符。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    char ToChar(object value);

    /// <summary>将值转换为 <see cref="T:System.SByte" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    sbyte ToSByte(object value);

    /// <summary>将值转换为 8 位无符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    byte ToByte(object value);

    /// <summary>将值转换为 16 位带符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    short ToInt16(object value);

    /// <summary>将值转换为 16 位无符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    ushort ToUInt16(object value);

    /// <summary>将值转换为 32 位带符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    int ToInt32(object value);

    /// <summary>将值转换为 32 位无符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    uint ToUInt32(object value);

    /// <summary>将值转换为 64 位带符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    long ToInt64(object value);

    /// <summary>将值转换为 64 位无符号整数。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    ulong ToUInt64(object value);

    /// <summary>将值转换为单精度浮点数字。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    float ToSingle(object value);

    /// <summary>将值转换为双精度浮点数字。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    double ToDouble(object value);

    /// <summary>将值转换为 <see cref="T:System.Decimal" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    Decimal ToDecimal(object value);

    /// <summary>将值转换为 <see cref="T:System.DateTime" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    DateTime ToDateTime(object value);

    /// <summary>将值转换为 <see cref="T:System.String" />。</summary>
    /// <returns>转换后的 <paramref name="value" />。</returns>
    /// <param name="value">要转换的对象。</param>
    string ToString(object value);
  }
}
