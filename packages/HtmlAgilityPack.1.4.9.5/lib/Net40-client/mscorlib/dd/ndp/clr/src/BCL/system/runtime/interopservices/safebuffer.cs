// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>提供可用于读写的受控内存缓冲区。尝试访问受控缓冲区（不足和溢出）之外的访问内存将引发异常。</summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
  {
    private static readonly UIntPtr Uninitialized = UIntPtr.Size == 4 ? (UIntPtr) uint.MaxValue : (UIntPtr) ulong.MaxValue;
    private UIntPtr _numBytes;

    /// <summary>获取缓冲区的大小（以字节为单位）。</summary>
    /// <returns>内存缓冲区中的字节数。</returns>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public ulong ByteLength
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        if (this._numBytes == SafeBuffer.Uninitialized)
          throw SafeBuffer.NotInitialized();
        return (ulong) this._numBytes;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> 类的新实例，并指定是否可靠地释放缓冲区句柄。</summary>
    /// <param name="ownsHandle">如果为 true，则在完成阶段可靠地释放句柄；如果为 false，则阻止可靠释放（建议不要这样做）。</param>
    [__DynamicallyInvokable]
    protected SafeBuffer(bool ownsHandle)
      : base(ownsHandle)
    {
      this._numBytes = SafeBuffer.Uninitialized;
    }

    /// <summary>定义内存区域的分配大小（以字节为单位）。在使用 <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> 实例之前，必须调用此方法。</summary>
    /// <param name="numBytes">缓冲区中的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="numBytes" /> 小于零。- 或 -<paramref name="numBytes" /> 大于可用地址空间。</exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public void Initialize(ulong numBytes)
    {
      if (numBytes < 0UL)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (IntPtr.Size == 4 && numBytes > (ulong) uint.MaxValue)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
      if (numBytes >= (ulong) SafeBuffer.Uninitialized)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
      this._numBytes = (UIntPtr) numBytes;
    }

    /// <summary>通过使用指定的元素数和元素大小，指定内存缓冲区的分配大小。在使用 <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> 实例之前，必须调用此方法。</summary>
    /// <param name="numElements">缓冲区中元素的数目。</param>
    /// <param name="sizeOfEachElement">缓冲区中每个元素的大小。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="numElements" /> 小于零。- 或 -<paramref name="sizeOfEachElement" /> 小于零。- 或 -<paramref name="numElements" /> 与 <paramref name="sizeOfEachElement" /> 的乘积大于可用地址空间。</exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public void Initialize(uint numElements, uint sizeOfEachElement)
    {
      if (numElements < 0U)
        throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (sizeOfEachElement < 0U)
        throw new ArgumentOutOfRangeException("sizeOfEachElement", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (IntPtr.Size == 4 && numElements * sizeOfEachElement > uint.MaxValue)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
      if ((ulong) (numElements * sizeOfEachElement) >= (ulong) SafeBuffer.Uninitialized)
        throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
      this._numBytes = (UIntPtr) checked (numElements * sizeOfEachElement);
    }

    /// <summary>通过指定值类型的数目，定义内存区域的分配大小。在使用 <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> 实例之前，必须调用此方法。</summary>
    /// <param name="numElements">要为其分配内存的值类型的元素数。</param>
    /// <typeparam name="T">要为其分配内存的值类型。</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="numElements" /> 小于零。- 或 -<paramref name="numElements" /> 与每个元素大小的乘积大于可用地址空间。</exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public void Initialize<T>(uint numElements) where T : struct
    {
      this.Initialize(numElements, Marshal.AlignedSizeOf<T>());
    }

    /// <summary>从内存块的 <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> 对象中获取一个指针。</summary>
    /// <param name="pointer">通过引用传递的字节指针，用于从 <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> 对象内接收指针。您必须在调用此方法之前将此指针设置为 null。</param>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public unsafe void AcquirePointer(ref byte* pointer)
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        bool success = false;
        this.DangerousAddRef(ref success);
        pointer = (byte*) (void*) this.handle;
      }
    }

    /// <summary>释放由 <see cref="M:System.Runtime.InteropServices.SafeBuffer.AcquirePointer(System.Byte*@)" /> 方法获取的指针。</summary>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void ReleasePointer()
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      this.DangerousRelease();
    }

    /// <summary>按指定的偏移量从内存中读取值类型。</summary>
    /// <returns>从内存中读取的值类型。</returns>
    /// <param name="byteOffset">从中读取值类型的位置。可能必须考虑对齐问题。</param>
    /// <typeparam name="T">要读取的值类型。</typeparam>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe T Read<T>(ulong byteOffset) where T : struct
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, (ulong) sizeofT);
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      T structure;
      try
      {
        this.DangerousAddRef(ref success);
        SafeBuffer.GenericPtrToStructure<T>(ptr, out structure, sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
      return structure;
    }

    /// <summary>从自偏移量开始的内存中读取指定数量的值类型，并将它们写入从索引开始的数组中。</summary>
    /// <param name="byteOffset">从其开始读取的位置。</param>
    /// <param name="array">要写入的输出数组。</param>
    /// <param name="index">输出数组中要开始写入的位置。</param>
    /// <param name="count">要从输入数组中读取并写入输出数组的值类型的数目。</param>
    /// <typeparam name="T">要读取的值类型。</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">数组的长度减去索引小于 <paramref name="count" /> 。</exception>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe void ReadArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      uint num = Marshal.AlignedSizeOf<T>();
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, checked ((ulong) ((long) num * (long) count)));
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.DangerousAddRef(ref success);
        for (int index1 = 0; index1 < count; ++index1)
          SafeBuffer.GenericPtrToStructure<T>(ptr + (long) num * (long) index1, out array[index1 + index], sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
    }

    /// <summary>将值类型写入内存中的给定位置。</summary>
    /// <param name="byteOffset">开始写入的位置。可能必须考虑对齐问题。</param>
    /// <param name="value">要写入的值。</param>
    /// <typeparam name="T">要写入的值类型。</typeparam>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, (ulong) sizeofT);
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.DangerousAddRef(ref success);
        SafeBuffer.GenericStructureToPtr<T>(ref value, ptr, sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
    }

    /// <summary>通过读取从输入数组中指定位置开始的字节，将指定数目的值类型写入内存位置。</summary>
    /// <param name="byteOffset">内存中要写入的位置。</param>
    /// <param name="array">输入数组。</param>
    /// <param name="index">数组中从其开始读取的偏移量。</param>
    /// <param name="count">要写入的值类型的数目。</param>
    /// <typeparam name="T">要写入的值类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">输入数组的长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.InvalidOperationException">未调用 <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> 方法。</exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe void WriteArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      uint num = Marshal.AlignedSizeOf<T>();
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, checked ((ulong) ((long) num * (long) count)));
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.DangerousAddRef(ref success);
        for (int index1 = 0; index1 < count; ++index1)
          SafeBuffer.GenericStructureToPtr<T>(ref array[index1 + index], ptr + (long) num * (long) index1, sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private unsafe void SpaceCheck(byte* ptr, ulong sizeInBytes)
    {
      if ((ulong) this._numBytes < sizeInBytes)
        SafeBuffer.NotEnoughRoom();
      if ((ulong) (ptr - (byte*) (void*) this.handle) <= (ulong) this._numBytes - sizeInBytes)
        return;
      SafeBuffer.NotEnoughRoom();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static void NotEnoughRoom()
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static InvalidOperationException NotInitialized()
    {
      return new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustCallInitialize"));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void GenericPtrToStructure<T>(byte* ptr, out T structure, uint sizeofT) where T : struct
    {
      structure = default (T);
      SafeBuffer.PtrToStructureNative(ptr, __makeref (structure), sizeofT);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void PtrToStructureNative(byte* ptr, TypedReference structure, uint sizeofT);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void GenericStructureToPtr<T>(ref T structure, byte* ptr, uint sizeofT) where T : struct
    {
      SafeBuffer.StructureToPtrNative(__makeref (structure), ptr, sizeofT);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void StructureToPtrNative(TypedReference structure, byte* ptr, uint sizeofT);
  }
}
