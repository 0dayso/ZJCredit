// Decompiled with JetBrains decompiler
// Type: System.IntPtr
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>用于表示指针或句柄的平台特定类型。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct IntPtr : ISerializable
  {
    [SecurityCritical]
    private unsafe void* m_value;
    /// <summary>表示已初始化为零的指针或句柄的只读字段。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly IntPtr Zero;

    /// <summary>获取此实例的大小。</summary>
    /// <returns>此进程中的指针或句柄的大小（以字节为单位）。此属性的值在 32 位进程中为 4，在 64 位进程中为 8。通过 C# 和 Visual Basic 编译器编译代码时，可以通过设置 /platform 开关定义该进程类型。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Size
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), NonVersionable, __DynamicallyInvokable] get
      {
        return 4;
      }
    }

    /// <summary>使用指定的 32 位指针或句柄初始化 <see cref="T:System.IntPtr" /> 的新实例。</summary>
    /// <param name="value">32 位有符号整数中包含的指针或句柄。</param>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe IntPtr(int value)
    {
      this.m_value = (void*) value;
    }

    /// <summary>使用指定的 64 位指针初始化 <see cref="T:System.IntPtr" /> 的新实例。</summary>
    /// <param name="value">64 位有符号整数中包含的指针或句柄。</param>
    /// <exception cref="T:System.OverflowException">在 32 位平台上， <paramref name="value" /> 太大或太小而无法表示为 <see cref="T:System.IntPtr" />。</exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe IntPtr(long value)
    {
      this.m_value = (void*) checked ((int) value);
    }

    /// <summary>使用指定的指向未指定类型的指针初始化 <see cref="T:System.IntPtr" /> 的新实例。</summary>
    /// <param name="value">指向未指定类型的指针。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public unsafe IntPtr(void* value)
    {
      this.m_value = value;
    }

    [SecurityCritical]
    private unsafe IntPtr(SerializationInfo info, StreamingContext context)
    {
      long int64 = info.GetInt64("value");
      if (IntPtr.Size == 4 && (int64 > (long) int.MaxValue || int64 < (long) int.MinValue))
        throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
      this.m_value = (void*) int64;
    }

    /// <summary>将 32 位有符号整数的值转换为 <see cref="T:System.IntPtr" />。</summary>
    /// <returns>初始化为 <see cref="T:System.IntPtr" /> 的 <paramref name="value" /> 新实例。</returns>
    /// <param name="value">32 位带符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static explicit operator IntPtr(int value)
    {
      return new IntPtr(value);
    }

    /// <summary>将 64 位有符号整数值转换为 <see cref="T:System.IntPtr" />。</summary>
    /// <returns>初始化为 <see cref="T:System.IntPtr" /> 的 <paramref name="value" /> 新实例。</returns>
    /// <param name="value">64 位带符号整数。</param>
    /// <exception cref="T:System.OverflowException">在 32 位平台上， <paramref name="value" /> 太大而无法表示为 <see cref="T:System.IntPtr" />。</exception>
    /// <filterpriority>3</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static explicit operator IntPtr(long value)
    {
      return new IntPtr(value);
    }

    /// <summary>将指向未指定类型的指定指针转换为 <see cref="T:System.IntPtr" />。</summary>
    /// <returns>初始化为 <see cref="T:System.IntPtr" /> 的 <paramref name="value" /> 新实例。</returns>
    /// <param name="value">指向未指定类型的指针。</param>
    /// <filterpriority>3</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static unsafe explicit operator IntPtr(void* value)
    {
      return new IntPtr(value);
    }

    /// <summary>将指定的 <see cref="T:System.IntPtr" /> 的值转换为指向未指定类型的指针。</summary>
    /// <returns>
    /// <paramref name="value" /> 的内容。</returns>
    /// <param name="value">要转换的指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public static unsafe explicit operator void*(IntPtr value)
    {
      return value.m_value;
    }

    /// <summary>将指定的 <see cref="T:System.IntPtr" /> 的值转换为 32 位有符号整数。</summary>
    /// <returns>
    /// <paramref name="value" /> 的内容。</returns>
    /// <param name="value">要转换的指针或句柄。</param>
    /// <exception cref="T:System.OverflowException">在 64 位平台上的值 <paramref name="value" /> 太大而无法表示为 32 位有符号整数。</exception>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator int(IntPtr value)
    {
      return (int) value.m_value;
    }

    /// <summary>将指定的 <see cref="T:System.IntPtr" /> 的值转换为 64 位有符号整数。</summary>
    /// <returns>
    /// <paramref name="value" /> 的内容。</returns>
    /// <param name="value">要转换的指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator long(IntPtr value)
    {
      return (long) (int) value.m_value;
    }

    /// <summary>确定 <see cref="T:System.IntPtr" /> 的两个指定的实例是否相等。</summary>
    /// <returns>如果 <paramref name="value1" /> 等于 <paramref name="value2" />，则为 true否则为 false。</returns>
    /// <param name="value1">要比较的第一个指针或句柄。</param>
    /// <param name="value2">要比较的第二个指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator ==(IntPtr value1, IntPtr value2)
    {
      return value1.m_value == value2.m_value;
    }

    /// <summary>确定 <see cref="T:System.IntPtr" /> 的两个指定的实例是否不等。</summary>
    /// <returns>如果 <paramref name="value1" /> 不等于 <paramref name="value2" />，则为 true否则为 false。</returns>
    /// <param name="value1">要比较的第一个指针或句柄。</param>
    /// <param name="value2">要比较的第二个指针或句柄。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator !=(IntPtr value1, IntPtr value2)
    {
      return value1.m_value != value2.m_value;
    }

    /// <summary>为指针值添加偏移量。</summary>
    /// <returns>反映为 <paramref name="offset" /> 增加 <paramref name="pointer" /> 的新指针。</returns>
    /// <param name="pointer">要为其增加偏移量的指针。</param>
    /// <param name="offset">要增加的偏移量。</param>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr operator +(IntPtr pointer, int offset)
    {
      return new IntPtr(pointer.ToInt32() + offset);
    }

    /// <summary>从指针值中减去偏移量。</summary>
    /// <returns>反映从 <paramref name="offset" /> 中减去 <paramref name="pointer" /> 的新指针。</returns>
    /// <param name="pointer">要从中减去偏移量的指针。</param>
    /// <param name="offset">要减去的偏移量。</param>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr operator -(IntPtr pointer, int offset)
    {
      return new IntPtr(pointer.ToInt32() - offset);
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal unsafe bool IsNull()
    {
      return (IntPtr) this.m_value == IntPtr.Zero;
    }

    [SecurityCritical]
    unsafe void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("value", (long) (int) this.m_value);
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 true 是 <paramref name="obj" /> 的实例并且等于此实例的值，则为 <see cref="T:System.IntPtr" />；否则为 false。</returns>
    /// <param name="obj">要与此示例比较的对象，或 null。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe bool Equals(object obj)
    {
      if (obj is IntPtr)
        return this.m_value == ((IntPtr) obj).m_value;
      return false;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      return (int) this.m_value;
    }

    /// <summary>将此实例的值转换为 32 位有符号整数。</summary>
    /// <returns>与此实例的值相等的 32 位有符号整数。</returns>
    /// <exception cref="T:System.OverflowException">在 64 位平台上，此实例的值是太大或太小而无法表示为 32 位有符号整数。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe int ToInt32()
    {
      return (int) this.m_value;
    }

    /// <summary>将此实例的值转换为 64 位有符号整数。</summary>
    /// <returns>与此实例的值相等的 64 位有符号整数。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe long ToInt64()
    {
      return (long) (int) this.m_value;
    }

    /// <summary>将当前 <see cref="T:System.IntPtr" /> 对象的数值转换为其等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      return ((int) this.m_value).ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>将当前 <see cref="T:System.IntPtr" /> 对象的数值转换为其等效字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.IntPtr" /> 对象的值的字符串表示形式。</returns>
    /// <param name="format">控制当前 <see cref="T:System.IntPtr" /> 对象转换方式的格式规范。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string ToString(string format)
    {
      return ((int) this.m_value).ToString(format, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>为指针值添加偏移量。</summary>
    /// <returns>反映为 <paramref name="offset" /> 增加 <paramref name="pointer" /> 的新指针。</returns>
    /// <param name="pointer">要为其增加偏移量的指针。</param>
    /// <param name="offset">要增加的偏移量。</param>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr Add(IntPtr pointer, int offset)
    {
      return pointer + offset;
    }

    /// <summary>从指针值中减去偏移量。</summary>
    /// <returns>反映从 <paramref name="offset" /> 中减去 <paramref name="pointer" /> 的新指针。</returns>
    /// <param name="pointer">要从中减去偏移量的指针。</param>
    /// <param name="offset">要减去的偏移量。</param>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr Subtract(IntPtr pointer, int offset)
    {
      return pointer - offset;
    }

    /// <summary>将此实例的值转换为指向未指定类型的指针。</summary>
    /// <returns>指向 <see cref="T:System.Void" /> 的指针，即是说，该指针所指向的内存包含有未指定类型的数据。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    public unsafe void* ToPointer()
    {
      return this.m_value;
    }
  }
}
