// Decompiled with JetBrains decompiler
// Type: System.Buffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>操作基元类型的数组。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Buffer
  {
    /// <summary>将指定数目的字节从起始于特定偏移量的源数组复制到起始于特定偏移量的目标数组。</summary>
    /// <param name="src">源缓冲区。</param>
    /// <param name="srcOffset">
    /// <paramref name="src" /> 的字节偏移量，从零开始。</param>
    /// <param name="dst">目标缓冲区。</param>
    /// <param name="dstOffset">
    /// <paramref name="dst" /> 的字节偏移量，从零开始。</param>
    /// <param name="count">要复制的字节数。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="src" /> 或 <paramref name="dst" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="src" /> 或 <paramref name="dst" /> 不是基元数组。- 或 -<paramref name="src" /> 中的字节数小于 <paramref name="srcOffset" /> 和 <paramref name="count" />。- 或 -<paramref name="dst" /> 中的字节数小于 <paramref name="dstOffset" /> 和 <paramref name="count" />。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="srcOffset" />、<paramref name="dstOffset" /> 或 <paramref name="count" /> 小于 0。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalBlockCopy(Array src, int srcOffsetBytes, Array dst, int dstOffsetBytes, int byteCount);

    [SecurityCritical]
    internal static unsafe int IndexOfByte(byte* src, byte value, int index, int count)
    {
      byte* numPtr;
      for (numPtr = src + index; ((int) numPtr & 3) != 0; ++numPtr)
      {
        if (count == 0)
          return -1;
        if ((int) *numPtr == (int) value)
          return (int) (numPtr - src);
        --count;
      }
      uint num1 = ((uint) value << 8) + (uint) value;
      uint num2 = (num1 << 16) + num1;
      while (count > 3)
      {
        uint num3 = *(uint*) numPtr ^ num2;
        uint num4 = 2130640639U + num3;
        if ((int) ((num3 ^ uint.MaxValue ^ num4) & 2164326656U) != 0)
        {
          int num5 = (int) (numPtr - src);
          if ((int) *numPtr == (int) value)
            return num5;
          if ((int) numPtr[1] == (int) value)
            return num5 + 1;
          if ((int) numPtr[2] == (int) value)
            return num5 + 2;
          if ((int) numPtr[3] == (int) value)
            return num5 + 3;
        }
        count -= 4;
        numPtr += 4;
      }
      while (count > 0)
      {
        if ((int) *numPtr == (int) value)
          return (int) (numPtr - src);
        --count;
        ++numPtr;
      }
      return -1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsPrimitiveTypeArray(Array array);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern byte _GetByte(Array array, int index);

    /// <summary>检索指定数组中指定位置的字节。</summary>
    /// <returns>返回数组中的 <paramref name="index" /> 字节。</returns>
    /// <param name="array">一个数组。</param>
    /// <param name="index">数组中的位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 不是基元。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 为负或大于 <paramref name="array" /> 的长度。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="array" />大于 2 GB。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static byte GetByte(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (!Buffer.IsPrimitiveTypeArray(array))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePrimArray"), "array");
      if (index < 0 || index >= Buffer._ByteLength(array))
        throw new ArgumentOutOfRangeException("index");
      return Buffer._GetByte(array, index);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _SetByte(Array array, int index, byte value);

    /// <summary>将指定的值分配给指定数组中特定位置处的字节。</summary>
    /// <param name="array">一个数组。</param>
    /// <param name="index">数组中的位置。</param>
    /// <param name="value">要分配的值。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 不是基元。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 为负或大于 <paramref name="array" /> 的长度。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="array" />大于 2 GB。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetByte(Array array, int index, byte value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (!Buffer.IsPrimitiveTypeArray(array))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePrimArray"), "array");
      if (index < 0 || index >= Buffer._ByteLength(array))
        throw new ArgumentOutOfRangeException("index");
      Buffer._SetByte(array, index, value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _ByteLength(Array array);

    /// <summary>返回指定数组中的字节数。</summary>
    /// <returns>数组中的字节数。</returns>
    /// <param name="array">一个数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 不是基元。 </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="array" />大于 2 GB。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int ByteLength(Array array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (!Buffer.IsPrimitiveTypeArray(array))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePrimArray"), "array");
      return Buffer._ByteLength(array);
    }

    [SecurityCritical]
    internal static unsafe void ZeroMemory(byte* src, long len)
    {
      while (len-- > 0L)
        src[len] = (byte) 0;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void Memcpy(byte[] dest, int destIndex, byte* src, int srcIndex, int len)
    {
      if (len == 0)
        return;
      fixed (byte* numPtr = dest)
        Buffer.Memcpy(numPtr + destIndex, src + srcIndex, len);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void Memcpy(byte* pDest, int destIndex, byte[] src, int srcIndex, int len)
    {
      if (len == 0)
        return;
      fixed (byte* numPtr = src)
        Buffer.Memcpy(pDest + destIndex, numPtr + srcIndex, len);
    }

    [FriendAccessAllowed]
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe void Memcpy(byte* dest, byte* src, int len)
    {
      Buffer.Memmove(dest, src, (uint) len);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void Memmove(byte* dest, byte* src, uint len)
    {
      if ((uint) dest - (uint) src >= len)
      {
        switch (len)
        {
          case 0:
            return;
          case 1:
            *dest = *src;
            return;
          case 2:
            *(short*) dest = *(short*) src;
            return;
          case 3:
            *(short*) dest = *(short*) src;
            dest[2] = src[2];
            return;
          case 4:
            *(int*) dest = *(int*) src;
            return;
          case 5:
            *(int*) dest = *(int*) src;
            dest[4] = src[4];
            return;
          case 6:
            *(int*) dest = *(int*) src;
            *(short*) (dest + 4) = *(short*) (src + 4);
            return;
          case 7:
            *(int*) dest = *(int*) src;
            *(short*) (dest + 4) = *(short*) (src + 4);
            dest[6] = src[6];
            return;
          case 8:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            return;
          case 9:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            dest[8] = src[8];
            return;
          case 10:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(short*) (dest + 8) = *(short*) (src + 8);
            return;
          case 11:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(short*) (dest + 8) = *(short*) (src + 8);
            dest[10] = src[10];
            return;
          case 12:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(int*) (dest + 8) = *(int*) (src + 8);
            return;
          case 13:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(int*) (dest + 8) = *(int*) (src + 8);
            dest[12] = src[12];
            return;
          case 14:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(int*) (dest + 8) = *(int*) (src + 8);
            *(short*) (dest + 12) = *(short*) (src + 12);
            return;
          case 15:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(int*) (dest + 8) = *(int*) (src + 8);
            *(short*) (dest + 12) = *(short*) (src + 12);
            dest[14] = src[14];
            return;
          case 16:
            *(int*) dest = *(int*) src;
            *(int*) (dest + 4) = *(int*) (src + 4);
            *(int*) (dest + 8) = *(int*) (src + 8);
            *(int*) (dest + 12) = *(int*) (src + 12);
            return;
          default:
            if (len < 512U)
            {
              if (((int) dest & 3) != 0)
              {
                if (((int) dest & 1) != 0)
                {
                  *dest = *src;
                  ++src;
                  ++dest;
                  --len;
                  if (((int) dest & 2) == 0)
                    goto label_25;
                }
                *(short*) dest = *(short*) src;
                src += 2;
                dest += 2;
                len -= 2U;
              }
label_25:
              for (uint index = len / 16U; index > 0U; --index)
              {
                *(int*) dest = *(int*) src;
                *(int*) (dest + 4) = *(int*) (src + 4);
                *(int*) (dest + (new IntPtr(2) * 4).ToInt64()) = *(int*) (src + (new IntPtr(2) * 4).ToInt64());
                *(int*) (dest + (new IntPtr(3) * 4).ToInt64()) = *(int*) (src + (new IntPtr(3) * 4).ToInt64());
                dest += 16;
                src += 16;
              }
              if (((int) len & 8) != 0)
              {
                *(int*) dest = *(int*) src;
                *(int*) (dest + 4) = *(int*) (src + 4);
                dest += 8;
                src += 8;
              }
              if (((int) len & 4) != 0)
              {
                *(int*) dest = *(int*) src;
                dest += 4;
                src += 4;
              }
              if (((int) len & 2) != 0)
              {
                *(short*) dest = *(short*) src;
                dest += 2;
                src += 2;
              }
              if (((int) len & 1) == 0)
                return;
              *dest = *src;
              return;
            }
            break;
        }
      }
      Buffer._Memmove(dest, src, len);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static unsafe void _Memmove(byte* dest, byte* src, uint len)
    {
      Buffer.__Memmove(dest, src, len);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void __Memmove(byte* dest, byte* src, uint len);

    /// <summary>将复制为发件人在内存中的一个地址到另一个长整型值指定的字节数。</summary>
    /// <param name="source">要复制的字节的地址。</param>
    /// <param name="destination">目标地址。</param>
    /// <param name="destinationSizeInBytes">可用的目标内存块中的字节数。</param>
    /// <param name="sourceBytesToCopy">要复制的字节数。  </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceBytesToCopy" /> 大于 <paramref name="destinationSizeInBytes" />。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void MemoryCopy(void* source, void* destination, long destinationSizeInBytes, long sourceBytesToCopy)
    {
      if (sourceBytesToCopy > destinationSizeInBytes)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
      Buffer.Memmove((byte*) destination, (byte*) source, checked ((uint) sourceBytesToCopy));
    }

    /// <summary>一个指定为发件人在内存中的一个地址到另一个无符号长整型值的字节数的副本。</summary>
    /// <param name="source">要复制的字节的地址。</param>
    /// <param name="destination">目标地址。</param>
    /// <param name="destinationSizeInBytes">可用的目标内存块中的字节数。</param>
    /// <param name="sourceBytesToCopy">要复制的字节数。   </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceBytesToCopy" /> 大于 <paramref name="destinationSizeInBytes" />。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void MemoryCopy(void* source, void* destination, ulong destinationSizeInBytes, ulong sourceBytesToCopy)
    {
      if (sourceBytesToCopy > destinationSizeInBytes)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.sourceBytesToCopy);
      Buffer.Memmove((byte*) destination, (byte*) source, checked ((uint) sourceBytesToCopy));
    }
  }
}
