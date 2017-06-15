// Decompiled with JetBrains decompiler
// Type: System.IO.UnmanagedMemoryStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>提供从托管代码访问非托管内存块的能力。</summary>
  /// <filterpriority>2</filterpriority>
  public class UnmanagedMemoryStream : Stream
  {
    private const long UnmanagedMemStreamMaxLength = 9223372036854775807;
    [SecurityCritical]
    private SafeBuffer _buffer;
    [SecurityCritical]
    private unsafe byte* _mem;
    private long _length;
    private long _capacity;
    private long _position;
    private long _offset;
    private FileAccess _access;
    internal bool _isOpen;
    [NonSerialized]
    private Task<int> _lastReadTask;

    /// <summary>获取一个值，该值指示流是否支持读取。</summary>
    /// <returns>如果对象是用一个构造函数创建的，而该构造函数的 <paramref name="access" /> 参数不包括读取流，或者如果流已关闭，则为 false，否则为 true。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanRead
    {
      get
      {
        if (this._isOpen)
          return (uint) (this._access & FileAccess.Read) > 0U;
        return false;
      }
    }

    /// <summary>获取一个值，该值指示流是否支持查找。</summary>
    /// <returns>如果流已关闭，则为 false；否则为 true。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanSeek
    {
      get
      {
        return this._isOpen;
      }
    }

    /// <summary>获取一个值，该值指示流是否支持写入。</summary>
    /// <returns>如果对象是用一个构造函数创建的，而该构造函数的 <paramref name="access" /> 参数值支持写入，或者对象是用一个不带参数的构造函数创建的，或者如果流已关闭，则为 false，否则为 true。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanWrite
    {
      get
      {
        if (this._isOpen)
          return (uint) (this._access & FileAccess.Write) > 0U;
        return false;
      }
    }

    /// <summary>获取流中数据的长度。</summary>
    /// <returns>流中数据的长度。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    public override long Length
    {
      get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return Interlocked.Read(ref this._length);
      }
    }

    /// <summary>获取流的长度（大小）或分配给流的内存总量（容量）。</summary>
    /// <returns>流的大小或容量。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    public long Capacity
    {
      get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return this._capacity;
      }
    }

    /// <summary>获取或设置流中的当前位置。</summary>
    /// <returns>流中的当前新位置。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">位置被设置为小于 0 的值，或者位置大于 <see cref="F:System.Int32.MaxValue" /> 或在添加到当前指针时导致溢出。</exception>
    /// <filterpriority>2</filterpriority>
    public override unsafe long Position
    {
      get
      {
        if (!this.CanSeek)
          __Error.StreamIsClosed();
        return Interlocked.Read(ref this._position);
      }
      [SecuritySafeCritical] set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (!this.CanSeek)
          __Error.StreamIsClosed();
        if (value > (long) int.MaxValue || this._mem + value < this._mem)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
        Interlocked.Exchange(ref this._position, value);
      }
    }

    /// <summary>获取或设置基于流中当前位置的指向流的字节指针。</summary>
    /// <returns>字节指针。</returns>
    /// <exception cref="T:System.IndexOutOfRangeException">当前位置大于流的容量。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">所设置的位置不是当前流中的有效位置。</exception>
    /// <exception cref="T:System.IO.IOException">指针被设置为比流的开始位置小的值。</exception>
    /// <exception cref="T:System.NotSupportedException">流已初始化，可用于 <see cref="T:System.Runtime.InteropServices.SafeBuffer" />。<see cref="P:System.IO.UnmanagedMemoryStream.PositionPointer" /> 属性仅对使用 <see cref="T:System.Byte" /> 指针初始化的流有效。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [CLSCompliant(false)]
    public unsafe byte* PositionPointer
    {
      [SecurityCritical] get
      {
        if (this._buffer != null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
        long num1 = Interlocked.Read(ref this._position);
        if (num1 > this._capacity)
          throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_UMSPosition"));
        IntPtr num2 = (IntPtr) (this._mem + num1);
        if (this._isOpen)
          return (byte*) num2;
        __Error.StreamIsClosed();
        return (byte*) num2;
      }
      [SecurityCritical] set
      {
        if (this._buffer != null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
        if (!this._isOpen)
          __Error.StreamIsClosed();
        if (new IntPtr(value - this._mem).ToInt64() > long.MaxValue)
          throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength"));
        if (value < this._mem)
          throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
        Interlocked.Exchange(ref this._position, value - this._mem);
      }
    }

    internal unsafe byte* Pointer
    {
      [SecurityCritical] get
      {
        if (this._buffer != null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
        return this._mem;
      }
    }

    /// <summary>初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <exception cref="T:System.Security.SecurityException">用户没有必需的权限。</exception>
    [SecuritySafeCritical]
    protected unsafe UnmanagedMemoryStream()
    {
      this._mem = (byte*) null;
      this._isOpen = false;
    }

    /// <summary>在具有指定的偏移量和长度的安全缓冲区中初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <param name="buffer">要包含非托管内存流的缓冲区。</param>
    /// <param name="offset">启动非托管内存流的缓冲区字节位置。</param>
    /// <param name="length">非托管内存流的长度。</param>
    [SecuritySafeCritical]
    public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
    {
      this.Initialize(buffer, offset, length, FileAccess.Read, false);
    }

    /// <summary>在具有指定的偏移量、长度和文件访问的安全缓冲区中初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <param name="buffer">要包含非托管内存流的缓冲区。</param>
    /// <param name="offset">启动非托管内存流的缓冲区字节位置。</param>
    /// <param name="length">非托管内存流的长度。</param>
    /// <param name="access">非托管内存流的文件访问模式。</param>
    [SecuritySafeCritical]
    public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
    {
      this.Initialize(buffer, offset, length, access, false);
    }

    [SecurityCritical]
    internal UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
    {
      this.Initialize(buffer, offset, length, access, skipSecurityCheck);
    }

    /// <summary>用指定的位置和内存长度初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <param name="pointer">指向非托管内存位置的指针。</param>
    /// <param name="length">要使用的内存的长度。</param>
    /// <exception cref="T:System.Security.SecurityException">用户没有必需的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pointer" /> 值为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> 值小于零。- 或 -<paramref name="length" /> 太大，引起溢出。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe UnmanagedMemoryStream(byte* pointer, long length)
    {
      byte* pointer1 = pointer;
      long num1 = length;
      int num2 = 1;
      int num3 = 0;
      this.Initialize(pointer1, num1, num1, (FileAccess) num2, num3 != 0);
    }

    /// <summary>使用指定的位置、内存长度、内存总量和文件访问值初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <param name="pointer">指向非托管内存位置的指针。</param>
    /// <param name="length">要使用的内存的长度。</param>
    /// <param name="capacity">分配给流的内存总量。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值之一。</param>
    /// <exception cref="T:System.Security.SecurityException">用户没有必需的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pointer" /> 值为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> 值小于零。- 或 - <paramref name="capacity" /> 值小于零。- 或 -<paramref name="length" /> 值大于 <paramref name="capacity" /> 值。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
    {
      this.Initialize(pointer, length, capacity, access, false);
    }

    [SecurityCritical]
    internal unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
    {
      this.Initialize(pointer, length, capacity, access, skipSecurityCheck);
    }

    /// <summary>在具有指定的偏移量、长度和文件访问的安全缓冲区中初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <param name="buffer">要包含非托管内存流的缓冲区。</param>
    /// <param name="offset">启动非托管内存流的缓冲区字节位置。</param>
    /// <param name="length">非托管内存流的长度。</param>
    /// <param name="access">非托管内存流的文件访问模式。</param>
    [SecuritySafeCritical]
    protected void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
    {
      this.Initialize(buffer, offset, length, access, false);
    }

    [SecurityCritical]
    internal unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0L)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length < 0L)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.ByteLength < (ulong) (offset + length))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSafeBufferOffLen"));
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException("access");
      if (this._isOpen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
      if (!skipSecurityCheck)
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        buffer.AcquirePointer(ref pointer);
        if (pointer + offset + length < pointer)
          throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround"));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          buffer.ReleasePointer();
      }
      this._offset = offset;
      this._buffer = buffer;
      this._length = length;
      this._capacity = length;
      this._access = access;
      this._isOpen = true;
    }

    /// <summary>使用指向非托管内存位置的指针初始化 <see cref="T:System.IO.UnmanagedMemoryStream" /> 类的新实例。</summary>
    /// <param name="pointer">指向非托管内存位置的指针。</param>
    /// <param name="length">要使用的内存的长度。</param>
    /// <param name="capacity">分配给流的内存总量。</param>
    /// <param name="access">
    /// <see cref="T:System.IO.FileAccess" /> 值之一。</param>
    /// <exception cref="T:System.Security.SecurityException">用户没有必需的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pointer" /> 值为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> 值小于零。- 或 - <paramref name="capacity" /> 值小于零。- 或 -<paramref name="length" /> 值太大，导致溢出。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
    {
      this.Initialize(pointer, length, capacity, access, false);
    }

    [SecurityCritical]
    internal unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
    {
      if ((IntPtr) pointer == IntPtr.Zero)
        throw new ArgumentNullException("pointer");
      if (length < 0L || capacity < 0L)
        throw new ArgumentOutOfRangeException(length < 0L ? "length" : "capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length > capacity)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_LengthGreaterThanCapacity"));
      if ((UIntPtr) ((ulong) pointer + (ulong) capacity) < (UIntPtr) pointer)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround"));
      if (access < FileAccess.Read || access > FileAccess.ReadWrite)
        throw new ArgumentOutOfRangeException("access", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      if (this._isOpen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
      if (!skipSecurityCheck)
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      this._mem = pointer;
      this._offset = 0L;
      this._length = length;
      this._capacity = capacity;
      this._access = access;
      this._isOpen = true;
    }

    /// <summary>释放由 <see cref="T:System.IO.UnmanagedMemoryStream" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [SecuritySafeCritical]
    protected override unsafe void Dispose(bool disposing)
    {
      this._isOpen = false;
      this._mem = (byte*) null;
      base.Dispose(disposing);
    }

    /// <summary>重写 <see cref="M:System.IO.Stream.Flush" /> 方法以便不执行任何操作。</summary>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    public override void Flush()
    {
      if (this._isOpen)
        return;
      __Error.StreamIsClosed();
    }

    /// <summary>重写 <see cref="M:System.IO.Stream.FlushAsync(System.Threading.CancellationToken)" /> 方法，以便取消操作（如果已指定），但不执行其他任何操作。可以开始于 .NET Framework 2015</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    [ComVisible(false)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      try
      {
        this.Flush();
        return Task.CompletedTask;
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
    }

    /// <summary>将指定数量的字节读入指定的数组。</summary>
    /// <returns>读入缓冲区中的总字节数。如果很多字节当前不可用，则总字节数可能小于请求的字节数；如果已到达流结尾，则为零 (0)。</returns>
    /// <param name="buffer">此方法返回时包含指定的字节数组，数组中 <paramref name="offset" /> 和 (<paramref name="offset" /> + <paramref name="count" /> - 1) 之间的值被从当前源中读取的字节替换。此参数未经初始化即被传递。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始存储从当前流中读取的数据。</param>
    /// <param name="count">要从当前流中读取的最大字节数。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">基础内存不支持读取。- 或 - <see cref="P:System.IO.UnmanagedMemoryStream.CanRead" /> 属性设置为 false。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 参数设置为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 参数小于零。- 或 - <paramref name="count" /> 参数小于零。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区数组的长度减去 <paramref name="offset" /> 参数小于 <paramref name="count" /> 参数。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override unsafe int Read([In, Out] byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanRead)
        __Error.ReadNotSupported();
      long num1 = Interlocked.Read(ref this._position);
      long num2 = Interlocked.Read(ref this._length) - num1;
      if (num2 > (long) count)
        num2 = (long) count;
      if (num2 <= 0L)
        return 0;
      int len = (int) num2;
      if (len < 0)
        len = 0;
      if (this._buffer != null)
      {
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          Buffer.Memcpy(buffer, offset, pointer + num1 + this._offset, 0, len);
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        Buffer.Memcpy(buffer, offset, this._mem + num1, 0, len);
      Interlocked.Exchange(ref this._position, num1 + num2);
      return len;
    }

    /// <summary>将指定数量的字节异步读入指定的数组。可以开始于 .NET Framework 2015</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可小于所请求的字节数；如果已到达流结尾时，则为 0（零）。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    [ComVisible(false)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<int>(cancellationToken);
      try
      {
        int result = this.Read(buffer, offset, count);
        Task<int> task = this._lastReadTask;
        return task == null || task.Result != result ? (this._lastReadTask = Task.FromResult<int>(result)) : task;
      }
      catch (Exception ex)
      {
        return Task.FromException<int>(ex);
      }
    }

    /// <summary>从流中读取一个字节，并将流内的位置前移一个字节，或者如果已到达流的末尾，则返回 -1。</summary>
    /// <returns>转换为 <see cref="T:System.Int32" /> 对象的无符号字节，或者如果到达流的末尾，则为 -1。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">基础内存不支持读取。- 或 -当前位置在流的末尾。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override unsafe int ReadByte()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanRead)
        __Error.ReadNotSupported();
      long index = Interlocked.Read(ref this._position);
      long num1 = Interlocked.Read(ref this._length);
      if (index >= num1)
        return -1;
      Interlocked.Exchange(ref this._position, index + 1L);
      int num2;
      if (this._buffer != null)
      {
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          num2 = (int) (pointer + index)[this._offset];
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        num2 = (int) this._mem[index];
      return num2;
    }

    /// <summary>将当前流的当前位置设置为给定值。</summary>
    /// <returns>流中的新位置。</returns>
    /// <param name="offset">相对于 <paramref name="origin" /> 的点，从此处开始查找。</param>
    /// <param name="loc">使用 <see cref="T:System.IO.SeekOrigin" /> 类型的值，将开始位置、结束位置或当前位置指定为 <paramref name="origin" /> 的参考点。</param>
    /// <exception cref="T:System.IO.IOException">尝试在流的开始位置之前查找。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 值大于流的最大大小。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="loc" /> 无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    public override long Seek(long offset, SeekOrigin loc)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (offset > long.MaxValue)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength"));
      switch (loc)
      {
        case SeekOrigin.Begin:
          if (offset < 0L)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          Interlocked.Exchange(ref this._position, offset);
          break;
        case SeekOrigin.Current:
          long num1 = Interlocked.Read(ref this._position);
          if (offset + num1 < 0L)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          Interlocked.Exchange(ref this._position, offset + num1);
          break;
        case SeekOrigin.End:
          long num2 = Interlocked.Read(ref this._length);
          if (num2 + offset < 0L)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          Interlocked.Exchange(ref this._position, num2 + offset);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
      }
      return Interlocked.Read(ref this._position);
    }

    /// <summary>将流的长度设置为指定的值。</summary>
    /// <param name="value">流的长度。</param>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">基础内存不支持写入。- 或 -尝试写入流，但 <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> 属性为 false。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">指定的 <paramref name="value" /> 超出流的容量。- 或 -指定的 <paramref name="value" /> 是负数。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override unsafe void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._buffer != null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (value > this._capacity)
        throw new IOException(Environment.GetResourceString("IO.IO_FixedCapacity"));
      long num1 = Interlocked.Read(ref this._position);
      long num2 = Interlocked.Read(ref this._length);
      if (value > num2)
        Buffer.ZeroMemory(this._mem + num2, value - num2);
      Interlocked.Exchange(ref this._length, value);
      long num3 = value;
      if (num1 <= num3)
        return;
      Interlocked.Exchange(ref this._position, value);
    }

    /// <summary>使用缓冲区中的数据将字节块写入当前流。</summary>
    /// <param name="buffer">字节数组，从该字节数组将字节复制到当前流中。</param>
    /// <param name="offset">缓冲区中的偏移量，从此处开始将字节复制到当前流中。</param>
    /// <param name="count">要写入当前流的字节数。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">基础内存不支持写入。- 或 -尝试写入流，但 <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> 属性为 false。- 或 -<paramref name="count" /> 值大于流的容量。- 或 -位置在流容量的末尾。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">其中一个指定的参数小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 参数减去 <paramref name="buffer" /> 参数的长度小于 <paramref name="count" /> 参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 参数为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override unsafe void Write(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      long num1 = Interlocked.Read(ref this._position);
      long num2 = Interlocked.Read(ref this._length);
      long num3 = num1 + (long) count;
      if (num3 < 0L)
        throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
      if (num3 > this._capacity)
        throw new NotSupportedException(Environment.GetResourceString("IO.IO_FixedCapacity"));
      if (this._buffer == null)
      {
        if (num1 > num2)
          Buffer.ZeroMemory(this._mem + num2, num1 - num2);
        if (num3 > num2)
          Interlocked.Exchange(ref this._length, num3);
      }
      if (this._buffer != null)
      {
        if (this._capacity - num1 < (long) count)
          throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          Buffer.Memcpy(pointer + num1 + this._offset, 0, buffer, offset, count);
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        Buffer.Memcpy(this._mem + num1, 0, buffer, offset, count);
      Interlocked.Exchange(ref this._position, num3);
    }

    /// <summary>将字节的序列异步写入当前流，将该流中的当前位置向前移动写入的字节数，并监视取消请求。可以开始于 .NET Framework 2015</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    [ComVisible(false)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      try
      {
        this.Write(buffer, offset, count);
        return Task.CompletedTask;
      }
      catch (Exception ex)
      {
        return (Task) Task.FromException<int>(ex);
      }
    }

    /// <summary>一个字节写入文件流中的当前位置。</summary>
    /// <param name="value">写入流的字节值。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">基础内存不支持写入。- 或 -尝试写入流，但 <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> 属性为 false。- 或 - 当前位置在流容量的末尾。</exception>
    /// <exception cref="T:System.IO.IOException">提供的 <paramref name="value" /> 导致流超出它的最大容量。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override unsafe void WriteByte(byte value)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      long index = Interlocked.Read(ref this._position);
      long num1 = Interlocked.Read(ref this._length);
      long num2 = index + 1L;
      if (index >= num1)
      {
        if (num2 < 0L)
          throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
        if (num2 > this._capacity)
          throw new NotSupportedException(Environment.GetResourceString("IO.IO_FixedCapacity"));
        if (this._buffer == null)
        {
          if (index > num1)
            Buffer.ZeroMemory(this._mem + num1, index - num1);
          Interlocked.Exchange(ref this._length, num2);
        }
      }
      if (this._buffer != null)
      {
        byte* pointer = (byte*) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this._buffer.AcquirePointer(ref pointer);
          (pointer + index)[this._offset] = value;
        }
        finally
        {
          if ((IntPtr) pointer != IntPtr.Zero)
            this._buffer.ReleasePointer();
        }
      }
      else
        this._mem[index] = value;
      Interlocked.Exchange(ref this._position, num2);
    }
  }
}
