// Decompiled with JetBrains decompiler
// Type: System.DBNull
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>表示不存在的值。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class DBNull : ISerializable, IConvertible
  {
    /// <summary>表示 <see cref="T:System.DBNull" /> 类的唯一实例。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly DBNull Value = new DBNull();

    private DBNull()
    {
    }

    private DBNull(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DBNullSerial"));
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口并返回序列化 <see cref="T:System.DBNull" /> 对象所需的数据。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含序列化 <see cref="T:System.DBNull" /> 对象所需的信息。</param>
    /// <param name="context">一个 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，它包含与 <see cref="T:System.DBNull" /> 对象关联的已序列化流的源和目标。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      UnitySerializationHolder.GetUnitySerializationInfo(info, 2, (string) null, (RuntimeAssembly) null);
    }

    /// <summary>返回空字符串（<see cref="F:System.String.Empty" />）。</summary>
    /// <returns>空字符串（<see cref="F:System.String.Empty" />）。</returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return string.Empty;
    }

    /// <summary>使用指定的 <see cref="T:System.IFormatProvider" /> 返回空字符串。</summary>
    /// <returns>空字符串（<see cref="F:System.String.Empty" />）。</returns>
    /// <param name="provider">用于格式化返回值的 <see cref="T:System.IFormatProvider" />。- 或 -从操作系统的当前区域设置中获取格式信息的 null。</param>
    /// <filterpriority>2</filterpriority>
    public string ToString(IFormatProvider provider)
    {
      return string.Empty;
    }

    /// <summary>获取 <see cref="T:System.DBNull" /> 的 <see cref="T:System.TypeCode" /> 值。</summary>
    /// <returns>
    /// <see cref="T:System.DBNull" />（为 <see cref="F:System.TypeCode.DBNull" />）的 <see cref="T:System.TypeCode" /> 值。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.DBNull;
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
    }

    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
