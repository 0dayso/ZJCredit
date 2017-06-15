// Decompiled with JetBrains decompiler
// Type: System.IO.UnmanagedMemoryAccessor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>提供从托管代码随机访问非托管内存块的能力。</summary>
  public class UnmanagedMemoryAccessor : IDisposable
  {
    [SecurityCritical]
    private SafeBuffer _buffer;
    private long _offset;
    private long _capacity;
    private FileAccess _access;
    private bool _isOpen;
    private bool _canRead;
    private bool _canWrite;

    /// <summary>获取访问器的容量。</summary>
    /// <returns>访问器的容量。</returns>
    public long Capacity
    {
      get
      {
        return this._capacity;
      }
    }

    /// <summary>确定访问器是否可读。</summary>
    /// <returns>如果访问器可读，则为 true；否则为 false。</returns>
    public bool CanRead
    {
      get
      {
        if (this._isOpen)
          return this._canRead;
        return false;
      }
    }

    /// <summary>确定访问器是否可写。</summary>
    /// <returns>如果访问器可写，则为 true；否则为 false。</returns>
    public bool CanWrite
    {
      get
      {
        if (this._isOpen)
          return this._canWrite;
        return false;
      }
    }

    /// <summary>确定访问器当前是否由进程打开。</summary>
    /// <returns>如果访问器已打开，则为 true；否则为 false。</returns>
    protected bool IsOpen
    {
      get
      {
        return this._isOpen;
      }
    }

    /// <summary>初始化 <see cref="T:System.IO.UnmanagedMemoryAccessor" /> 类的新实例。</summary>
    protected UnmanagedMemoryAccessor()
    {
      this._isOpen = false;
    }

    /// <summary>使用指定的缓冲区、偏移量和容量初始化 <see cref="T:System.IO.UnmanagedMemoryAccessor" /> 类的新实例。</summary>
    /// <param name="buffer">要包含访问器的缓冲区。</param>
    /// <param name="offset">启动访问器的字节位置。</param>
    /// <param name="capacity">要分配的内存大小（以字节为单位）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="capacity" /> 之和大于 <paramref name="buffer" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="capacity" /> 小于零。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="offset" /> 与 <paramref name="capacity" /> 之和将环绕地址空间的高端。</exception>
    [SecuritySafeCritical]
    public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity)
    {
      this.Initialize(buffer, offset, capacity, FileAccess.Read);
    }

    /// <summary>使用指定的缓冲区、偏移量、容量和访问权限初始化 <see cref="T:System.IO.UnmanagedMemoryAccessor" /> 类的新实例。</summary>
    /// <param name="buffer">要包含访问器的缓冲区。</param>
    /// <param name="offset">启动访问器的字节位置。</param>
    /// <param name="capacity">要分配的内存大小（以字节为单位）。</param>
    /// <param name="access">内存允许的访问类型。默认值为 <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="capacity" /> 之和大于 <paramref name="buffer" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="capacity" /> 小于零。- 或 -<paramref name="access" /> 不是有效的 <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> 枚举值。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="offset" /> 与 <paramref name="capacity" /> 之和将环绕地址空间的高端。</exception>
    [SecuritySafeCritical]
    public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity, FileAccess access)
    {
      this.Initialize(buffer, offset, capacity, access);
    }

    /// <summary>设置访问器的初始值。</summary>
    /// <param name="buffer">要包含访问器的缓冲区。</param>
    /// <param name="offset">启动访问器的字节位置。</param>
    /// <param name="capacity">要分配的内存大小（以字节为单位）。</param>
    /// <param name="access">内存允许的访问类型。默认值为 <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="capacity" /> 之和大于 <paramref name="buffer" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="capacity" /> 小于零。- 或 -<paramref name="access" /> 不是有效的 <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> 枚举值。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="offset" /> 与 <paramref name="capacity" /> 之和将环绕地址空间的高端。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected unsafe void Initialize(SafeBuffer buffer, long offset, long capacity, FileAccess access)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0L)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (capacity < 0L)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.ByteLength < (ulong) (offset + capacity))
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndCapacityOutOfBounds"));
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException("access");
      if (this._isOpen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        buffer.AcquirePointer(ref pointer);
        if ((UIntPtr) (ulong) ((long) pointer + offset + capacity) < (UIntPtr) pointer)
          throw new ArgumentException(Environment.GetResourceString("Argument_UnmanagedMemAccessorWrapAround"));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          buffer.ReleasePointer();
      }
      this._offset = offset;
      this._buffer = buffer;
      this._capacity = capacity;
      this._access = access;
      this._isOpen = true;
      this._canRead = (uint) (this._access & FileAccess.Read) > 0U;
      this._canWrite = (uint) (this._access & FileAccess.Write) > 0U;
    }

    /// <summary>释放由 <see cref="T:System.IO.UnmanagedMemoryAccessor" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
      this._isOpen = false;
    }

    /// <summary>释放由 <see cref="T:System.IO.UnmanagedMemoryAccessor" /> 使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>从访问器读取一个布尔值。</summary>
    /// <returns>true 或 false。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    public bool ReadBoolean(long position)
    {
      int sizeOfType = 1;
      this.EnsureSafeToRead(position, sizeOfType);
      return (uint) this.InternalReadByte(position) > 0U;
    }

    /// <summary>从访问器读取一个字节值。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    public byte ReadByte(long position)
    {
      int sizeOfType = 1;
      this.EnsureSafeToRead(position, sizeOfType);
      return this.InternalReadByte(position);
    }

    /// <summary>从访问器读取一个字符。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe char ReadChar(long position)
    {
      int sizeOfType = 2;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return (char) *(ushort*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 16 位整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe short ReadInt16(long position)
    {
      int sizeOfType = 2;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(short*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 32 位整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe int ReadInt32(long position)
    {
      int sizeOfType = 4;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(int*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 64 位整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe long ReadInt64(long position)
    {
      int sizeOfType = 8;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(long*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个小数值。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。- 或 -要读取的小数无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public Decimal ReadDecimal(long position)
    {
      int sizeOfType = 16;
      this.EnsureSafeToRead(position, sizeOfType);
      int[] numArray = new int[4];
      this.ReadArray<int>(position, numArray, 0, numArray.Length);
      return new Decimal(numArray);
    }

    /// <summary>从访问器读取一个单精度浮点值。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe float ReadSingle(long position)
    {
      int sizeOfType = 4;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(float*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个双精度浮点值。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe double ReadDouble(long position)
    {
      int sizeOfType = 8;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(double*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 8 位带符号整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe sbyte ReadSByte(long position)
    {
      int sizeOfType = 1;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return (sbyte) *pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 16 位无符号整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe ushort ReadUInt16(long position)
    {
      int sizeOfType = 2;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(ushort*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 32 位无符号整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe uint ReadUInt32(long position)
    {
      int sizeOfType = 4;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return *(uint*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>从访问器读取一个 64 位无符号整数。</summary>
    /// <returns>读取的值。</returns>
    /// <param name="position">访问器中起始读取位置的字节偏移量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读取值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe ulong ReadUInt64(long position)
    {
      int sizeOfType = 8;
      this.EnsureSafeToRead(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        return (ulong) *(long*) pointer;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将 <paramref name="T" /> 类型的结构从访问器读取到提供的引用中。</summary>
    /// <param name="position">访问器中开始读取的位置。</param>
    /// <param name="structure">包含读取数据的结构。</param>
    /// <typeparam name="T">结构的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供读入 <paramref name="T" /> 类型的结构。- 或 -T 是包含一个或多个引用类型的值类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecurityCritical]
    public void Read<T>(long position, out T structure) where T : struct
    {
      if (position < 0L)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (!this._isOpen)
        throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
      uint num = Marshal.SizeOfType(typeof (T));
      if (position > this._capacity - (long) num)
      {
        if (position >= this._capacity)
          throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
        throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToRead", (object) typeof (T).FullName), "position");
      }
      structure = this._buffer.Read<T>((ulong) (this._offset + position));
    }

    /// <summary>将 <paramref name="T" /> 类型的结构从访问器读取到 <paramref name="T" /> 类型的数组中。</summary>
    /// <returns>读入 <paramref name="array" /> 的结构数。如果可用结构较少，则此值可能小于 <paramref name="count" />；如果到达访问器末尾，则为零。</returns>
    /// <param name="position">访问器中的字节偏移量，从此处开始读取。</param>
    /// <param name="array">包含从访问器读取的结构的数组。</param>
    /// <param name="offset">
    /// <paramref name="array" /> 中要将第一个复制的结构放置到的索引。</param>
    /// <param name="count">要从访问器读取的 <paramref name="T" /> 类型的结构的数目。</param>
    /// <typeparam name="T">结构的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 不足够大，无法包含结构的 <paramref name="count" />（从 <paramref name="position" /> 开始）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecurityCritical]
    public int ReadArray<T>(long position, T[] array, int offset, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException("array", "Buffer cannot be null.");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndLengthOutOfBounds"));
      if (!this.CanRead)
      {
        if (!this._isOpen)
          throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
      }
      if (position < 0L)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      uint num1 = Marshal.AlignedSizeOf<T>();
      if (position >= this._capacity)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      int count1 = count;
      long num2 = this._capacity - position;
      if (num2 < 0L)
      {
        count1 = 0;
      }
      else
      {
        ulong num3 = (ulong) num1 * (ulong) count;
        if ((ulong) num2 < num3)
          count1 = (int) (num2 / (long) num1);
      }
      this._buffer.ReadArray<T>((ulong) (this._offset + position), array, offset, count1);
      return count1;
    }

    /// <summary>将一个布尔值写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    public void Write(long position, bool value)
    {
      int sizeOfType = 1;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte num = value ? (byte) 1 : (byte) 0;
      this.InternalWrite(position, num);
    }

    /// <summary>将一个字节值写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    public void Write(long position, byte value)
    {
      int sizeOfType = 1;
      this.EnsureSafeToWrite(position, sizeOfType);
      this.InternalWrite(position, value);
    }

    /// <summary>将一个字符写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, char value)
    {
      int sizeOfType = 2;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(short*) pointer = (short) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 16 位整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, short value)
    {
      int sizeOfType = 2;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(short*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 32 位整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, int value)
    {
      int sizeOfType = 4;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(int*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 64 位整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">position 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, long value)
    {
      int sizeOfType = 8;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(long*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个小数值写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。- 或 -小数无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public void Write(long position, Decimal value)
    {
      int sizeOfType = 16;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte[] buffer = new byte[16];
      Decimal.GetBytes(value, buffer);
      int[] array = new int[4];
      int num1 = (int) buffer[12] | (int) buffer[13] << 8 | (int) buffer[14] << 16 | (int) buffer[15] << 24;
      int num2 = (int) buffer[0] | (int) buffer[1] << 8 | (int) buffer[2] << 16 | (int) buffer[3] << 24;
      int num3 = (int) buffer[4] | (int) buffer[5] << 8 | (int) buffer[6] << 16 | (int) buffer[7] << 24;
      int num4 = (int) buffer[8] | (int) buffer[9] << 8 | (int) buffer[10] << 16 | (int) buffer[11] << 24;
      array[0] = num2;
      array[1] = num3;
      array[2] = num4;
      array[3] = num1;
      this.WriteArray<int>(position, array, 0, array.Length);
    }

    /// <summary>将一个 Single 写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, float value)
    {
      int sizeOfType = 4;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(float*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 Double 值写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    public unsafe void Write(long position, double value)
    {
      int sizeOfType = 8;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(double*) pointer = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 8 位整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, sbyte value)
    {
      int sizeOfType = 1;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *pointer = (byte) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 16 位无符号整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, ushort value)
    {
      int sizeOfType = 2;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(short*) pointer = (short) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 32 位无符号整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, uint value)
    {
      int sizeOfType = 4;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(int*) pointer = (int) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个 64 位无符号整数写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="position" /> 后面没有足够的字节数可供写入值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe void Write(long position, ulong value)
    {
      int sizeOfType = 8;
      this.EnsureSafeToWrite(position, sizeOfType);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        pointer += this._offset + position;
        *(long*) pointer = (long) value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    /// <summary>将一个结构写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="structure">要写入的结构。</param>
    /// <typeparam name="T">结构的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">取值函数的 <paramref name="position" /> 后面没有足够的字节数可供写入 <paramref name="T" /> 类型的结构。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecurityCritical]
    public void Write<T>(long position, ref T structure) where T : struct
    {
      if (position < 0L)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (!this._isOpen)
        throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
      uint num = Marshal.SizeOfType(typeof (T));
      if (position > this._capacity - (long) num)
      {
        if (position >= this._capacity)
          throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
        throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToWrite", (object) typeof (T).FullName), "position");
      }
      this._buffer.Write<T>((ulong) (this._offset + position), structure);
    }

    /// <summary>将结构从 <paramref name="T" /> 类型的数组写入访问器。</summary>
    /// <param name="position">访问器中起始写入位置的字节偏移量。</param>
    /// <param name="array">要写入访问器的数组。</param>
    /// <param name="offset">在 <paramref name="array" /> 中从其开始写入的索引。</param>
    /// <param name="count">要写入的 <paramref name="array" /> 中的结构数。</param>
    /// <typeparam name="T">结构的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">取值函数的 <paramref name="position" /> 后面没有足够的字节数可供写入 <paramref name="count" /> 所指定数量的结构。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="position" /> 小于零或大于访问器的容量。- 或 -<paramref name="offset" /> 或 <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">访问器不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">已释放访问器。</exception>
    [SecurityCritical]
    public void WriteArray<T>(long position, T[] array, int offset, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException("array", "Buffer cannot be null.");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndLengthOutOfBounds"));
      if (position < 0L)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (position >= this.Capacity)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      if (!this._isOpen)
        throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
      this._buffer.WriteArray<T>((ulong) (this._offset + position), array, offset, count);
    }

    [SecuritySafeCritical]
    private unsafe byte InternalReadByte(long position)
    {
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        return (pointer + this._offset)[position];
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    [SecuritySafeCritical]
    private unsafe void InternalWrite(long position, byte value)
    {
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this._buffer.AcquirePointer(ref pointer);
        (pointer + this._offset)[position] = value;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this._buffer.ReleasePointer();
      }
    }

    private void EnsureSafeToRead(long position, int sizeOfType)
    {
      if (!this._isOpen)
        throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
      if (position < 0L)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (position <= this._capacity - (long) sizeOfType)
        return;
      if (position >= this._capacity)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToRead"), "position");
    }

    private void EnsureSafeToWrite(long position, int sizeOfType)
    {
      if (!this._isOpen)
        throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
      if (position < 0L)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (position <= this._capacity - (long) sizeOfType)
        return;
      if (position >= this._capacity)
        throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
      throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToWrite", (object) "Byte"), "position");
    }
  }
}
