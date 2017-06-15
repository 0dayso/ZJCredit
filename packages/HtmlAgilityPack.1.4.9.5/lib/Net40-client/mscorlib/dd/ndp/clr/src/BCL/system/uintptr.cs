// Decompiled with JetBrains decompiler
// Type: System.UIntPtr
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>用于表示指针或句柄的平台特定类型。</summary>
  /// <filterpriority>1</filterpriority>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct UIntPtr : ISerializable
  {
    [SecurityCritical]
    private unsafe void* m_value;
    /// <summary>一个只读字段，代表已初始化为零的指针或句柄。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly UIntPtr Zero;

    /// <summary>获得此实例的大小。</summary>
    /// <returns>此平台上的指针或句柄的大小，按字节计。此属性的值在 32 位平台上为 4，在 64 位平台上为 8。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Size
    {
      [NonVersionable, __DynamicallyInvokable] get
      {
        return 4;
      }
    }

    /// <summary>使用指定的 32 位指针或句柄初始化 <see cref="T:System.UIntPtr" /> 的新实例。</summary>
    /// <param name="value">包含于 32 位无符号整数中的指针或句柄。</param>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe UIntPtr(uint value)
    {
      this.m_value = (void*) value;
    }

    /// <summary>使用指定的 64 位指针或句柄初始化 <see cref="T:System.UIntPtr" /> 的新实例。</summary>
    /// <param name="value">包含于 64 位无符号整数中的指针或句柄。</param>
    /// <exception cref="T:System.OverflowException">在 32 位的平台上，<paramref name="value" /> 太大，无法表示为一个 <see cref="T:System.UIntPtr" />。</exception>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe UIntPtr(ulong value)
    {
      this.m_value = (void*) checked ((uint) value);
    }

    /// <summary>使用指定的指向未指定类型的指针来初始化 <see cref="T:System.UIntPtr" /> 的新实例。</summary>
    /// <param name="value">指向未指定类型的指针。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public unsafe UIntPtr(void* value)
    {
      this.m_value = value;
    }

    [SecurityCritical]
    private unsafe UIntPtr(SerializationInfo info, StreamingContext context)
    {
      ulong uint64 = info.GetUInt64("value");
      if (UIntPtr.Size == 4 && uint64 > (ulong) uint.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
      this.m_value = (void*) uint64;
    }

    /// <summary>将 32 位无符号整数的值转换成 <see cref="T:System.UIntPtr" />。</summary>
    /// <returns>初始化为 <paramref name="value" /> 的 <see cref="T:System.UIntPtr" /> 新实例。</returns>
    /// <param name="value">32 位无符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [NonVersionable]
    public static explicit operator UIntPtr(uint value)
    {
      return new UIntPtr(value);
    }

    /// <summary>将 64 位无符号整数的值转换成 <see cref="T:System.UIntPtr" />。</summary>
    /// <returns>初始化为 <paramref name="value" /> 的 <see cref="T:System.UIntPtr" /> 新实例。</returns>
    /// <param name="value">64 位无符号整数。</param>
    /// <exception cref="T:System.OverflowException">在 32 位的平台上，<paramref name="value" /> 太大，无法表示为一个 <see cref="T:System.UIntPtr" />。</exception>
    /// <filterpriority>3</filterpriority>
    [NonVersionable]
    public static explicit operator UIntPtr(ulong value)
    {
      return new UIntPtr(value);
    }

    /// <summary>将指定的 <see cref="T:System.UIntPtr" /> 的值转换为 32 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" /> 的内容。</returns>
    /// <param name="value">要转换的指针或句柄。</param>
    /// <exception cref="T:System.OverflowException">在 64 位平台上，<paramref name="value" /> 的值太大，无法表示为 32 位无符号整数。</exception>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator uint(UIntPtr value)
    {
      return (uint) value.m_value;
    }

    /// <summary>将指定的 <see cref="T:System.UIntPtr" /> 的值转换为 64 位无符号整数。</summary>
    /// <returns>
    /// <paramref name="value" /> 的内容。</returns>
    /// <param name="value">要转换的指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator ulong(UIntPtr value)
    {
      return (ulong) value.m_value;
    }

    /// <summary>将指定的指向未指定类型的指针转换为 <see cref="T:System.UIntPtr" />。</summary>
    /// <returns>初始化为 <paramref name="value" /> 的 <see cref="T:System.UIntPtr" /> 新实例。</returns>
    /// <param name="value">指向未指定类型的指针。</param>
    /// <filterpriority>3</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public static unsafe explicit operator UIntPtr(void* value)
    {
      return new UIntPtr(value);
    }

    /// <summary>将指定的 <see cref="T:System.UIntPtr" /> 的值转换为指向未指定的类型的指针。</summary>
    /// <returns>
    /// <paramref name="value" /> 的内容。</returns>
    /// <param name="value">要转换的指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public static unsafe explicit operator void*(UIntPtr value)
    {
      return value.m_value;
    }

    /// <summary>确定 <see cref="T:System.UIntPtr" /> 的两个指定的实例是否相等。</summary>
    /// <returns>如果 <paramref name="value1" /> 等于 <paramref name="value2" />，则为 true；否则为 false。</returns>
    /// <param name="value1">要比较的第一个指针或句柄。</param>
    /// <param name="value2">要比较的第二个指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator ==(UIntPtr value1, UIntPtr value2)
    {
      return value1.m_value == value2.m_value;
    }

    /// <summary>确定 <see cref="T:System.UIntPtr" /> 的两个指定的实例是否不等。</summary>
    /// <returns>如果 <paramref name="value1" /> 不等于 <paramref name="value2" />，则为 true；否则为 false。</returns>
    /// <param name="value1">要比较的第一个指针或句柄。</param>
    /// <param name="value2">要比较的第二个指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator !=(UIntPtr value1, UIntPtr value2)
    {
      return value1.m_value != value2.m_value;
    }

    /// <summary>为无符号指针的值增加偏移量。</summary>
    /// <returns>新的无符号指针，反映向 <paramref name="pointer" /> 增加 <paramref name="offset" /> 的结果。</returns>
    /// <param name="pointer">要为其增加偏移量的无符号指针。</param>
    /// <param name="offset">要增加的偏移量。</param>
    [NonVersionable]
    public static UIntPtr operator +(UIntPtr pointer, int offset)
    {
      return new UIntPtr(pointer.ToUInt32() + (uint) offset);
    }

    /// <summary>从无符号指针的值中减去偏移量。</summary>
    /// <returns>新的无符号指针，反映从 <paramref name="pointer" /> 中减去 <paramref name="offset" /> 的结果。</returns>
    /// <param name="pointer">要从中减去偏移量的无符号指针。</param>
    /// <param name="offset">要减去的偏移量。</param>
    [NonVersionable]
    public static UIntPtr operator -(UIntPtr pointer, int offset)
    {
      return new UIntPtr(pointer.ToUInt32() - (uint) offset);
    }

    [SecurityCritical]
    unsafe void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("value", (ulong) this.m_value);
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 <see cref="T:System.UIntPtr" /> 的实例并且等于此实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象或为 null。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe bool Equals(object obj)
    {
      if (obj is UIntPtr)
        return this.m_value == ((UIntPtr) obj).m_value;
      return false;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      return (int) this.m_value & int.MaxValue;
    }

    /// <summary>将此实例的值转换成 32 位无符号整数。</summary>
    /// <returns>等于此实例的值的 32 位无符号整数。</returns>
    /// <exception cref="T:System.OverflowException">在 64 位平台上，此实例的值太大，无法表示为 32 位无符号整数。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe uint ToUInt32()
    {
      return (uint) this.m_value;
    }

    /// <summary>将此实例的值转换成 64 位无符号整数。</summary>
    /// <returns>等于此实例的值的 64 位无符号整数。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe ulong ToUInt64()
    {
      return (ulong) this.m_value;
    }

    /// <summary>将此实例的数值转换为其等效的字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      return ((uint) this.m_value).ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>为无符号指针的值增加偏移量。</summary>
    /// <returns>新的无符号指针，反映向 <paramref name="pointer" /> 增加 <paramref name="offset" /> 的结果。</returns>
    /// <param name="pointer">要为其增加偏移量的无符号指针。</param>
    /// <param name="offset">要增加的偏移量。</param>
    [NonVersionable]
    public static UIntPtr Add(UIntPtr pointer, int offset)
    {
      return pointer + offset;
    }

    /// <summary>从无符号指针的值中减去偏移量。</summary>
    /// <returns>新的无符号指针，反映从 <paramref name="pointer" /> 中减去 <paramref name="offset" /> 的结果。</returns>
    /// <param name="pointer">要从中减去偏移量的无符号指针。</param>
    /// <param name="offset">要减去的偏移量。</param>
    [NonVersionable]
    public static UIntPtr Subtract(UIntPtr pointer, int offset)
    {
      return pointer - offset;
    }

    /// <summary>将此实例的值转换为指向未指定的类型的指针。</summary>
    /// <returns>指向 <see cref="T:System.Void" /> 的指针，即是说，该指针所指向的内存包含有未指定的类型的数据。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public unsafe void* ToPointer()
    {
      return this.m_value;
    }
  }
}
