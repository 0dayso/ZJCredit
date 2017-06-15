// Decompiled with JetBrains decompiler
// Type: System.IO.MemoryStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>创建一个流，其后备存储为内存。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MemoryStream : Stream
  {
    private byte[] _buffer;
    private int _origin;
    private int _position;
    private int _length;
    private int _capacity;
    private bool _expandable;
    private bool _writable;
    private bool _exposable;
    private bool _isOpen;
    [NonSerialized]
    private Task<int> _lastReadTask;
    private const int MemStreamMaxLength = 2147483647;

    /// <summary>获取一个值，该值指示当前流是否支持读取。</summary>
    /// <returns>如果流是打开的，则为 true。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool CanRead
    {
      [__DynamicallyInvokable] get
      {
        return this._isOpen;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持查找。</summary>
    /// <returns>如果流是打开的，则为 true。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool CanSeek
    {
      [__DynamicallyInvokable] get
      {
        return this._isOpen;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持写入。</summary>
    /// <returns>如果流支持写入，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool CanWrite
    {
      [__DynamicallyInvokable] get
      {
        return this._writable;
      }
    }

    /// <summary>获取或设置分配给该流的字节数。</summary>
    /// <returns>流的缓冲区的可使用部分的长度。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">容量设置为负或小于流的当前长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">对不能修改其容量的流调用 set。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Capacity
    {
      [__DynamicallyInvokable] get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return this._capacity - this._origin;
      }
      [__DynamicallyInvokable] set
      {
        if ((long) value < this.Length)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (!this._isOpen)
          __Error.StreamIsClosed();
        if (!this._expandable && value != this.Capacity)
          __Error.MemoryStreamNotExpandable();
        if (!this._expandable || value == this._capacity)
          return;
        if (value > 0)
        {
          byte[] numArray = new byte[value];
          if (this._length > 0)
            Buffer.InternalBlockCopy((Array) this._buffer, 0, (Array) numArray, 0, this._length);
          this._buffer = numArray;
        }
        else
          this._buffer = (byte[]) null;
        this._capacity = value;
      }
    }

    /// <summary>获取流的长度（以字节为单位）。</summary>
    /// <returns>流的长度（以字节为单位）。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override long Length
    {
      [__DynamicallyInvokable] get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return (long) (this._length - this._origin);
      }
    }

    /// <summary>获取或设置流中的当前位置。</summary>
    /// <returns>流中的当前位置。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">此位置设置为负值或大于 <see cref="F:System.Int32.MaxValue" /> 的值。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override long Position
    {
      [__DynamicallyInvokable] get
      {
        if (!this._isOpen)
          __Error.StreamIsClosed();
        return (long) (this._position - this._origin);
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (!this._isOpen)
          __Error.StreamIsClosed();
        if (value > (long) int.MaxValue)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
        this._position = this._origin + (int) value;
      }
    }

    /// <summary>使用初始化为零的可扩展容量初始化 <see cref="T:System.IO.MemoryStream" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public MemoryStream()
      : this(0)
    {
    }

    /// <summary>使用按指定要求初始化的可扩展容量初始化 <see cref="T:System.IO.MemoryStream" /> 类的新实例。</summary>
    /// <param name="capacity">内部数组的初始大小（以字节为单位）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 为负数。</exception>
    [__DynamicallyInvokable]
    public MemoryStream(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
      this._buffer = new byte[capacity];
      this._capacity = capacity;
      this._expandable = true;
      this._writable = true;
      this._exposable = true;
      this._origin = 0;
      this._isOpen = true;
    }

    /// <summary>基于指定的字节数组初始化 <see cref="T:System.IO.MemoryStream" /> 类的无法调整大小的新实例。</summary>
    /// <param name="buffer">从中创建当前流的无符号字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer)
      : this(buffer, true)
    {
    }

    /// <summary>在 <see cref="P:System.IO.MemoryStream.CanWrite" /> 属性按指定设置的状态下，基于指定的字节数组初始化 <see cref="T:System.IO.MemoryStream" /> 类的无法调整大小的新实例。</summary>
    /// <param name="buffer">从中创建此流的无符号字节的数组。</param>
    /// <param name="writable">
    /// <see cref="P:System.IO.MemoryStream.CanWrite" /> 属性的设置，确定该流是否支持写入。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, bool writable)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      this._buffer = buffer;
      this._length = this._capacity = buffer.Length;
      this._writable = writable;
      this._exposable = false;
      this._origin = 0;
      this._isOpen = true;
    }

    /// <summary>基于字节数组的指定区域（索引）初始化 <see cref="T:System.IO.MemoryStream" /> 类的无法调整大小的新实例。</summary>
    /// <param name="buffer">从中创建此流的无符号字节的数组。</param>
    /// <param name="index">
    /// <paramref name="buffer" /> 内的索引，流从此处开始。</param>
    /// <param name="count">流的长度（以字节为单位）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, int index, int count)
      : this(buffer, index, count, true, false)
    {
    }

    /// <summary>在 <see cref="P:System.IO.MemoryStream.CanWrite" /> 属性按指定设置的状态下，基于字节数组的指定区域，初始化 <see cref="T:System.IO.MemoryStream" /> 类的无法调整大小的新实例。</summary>
    /// <param name="buffer">从中创建此流的无符号字节的数组。</param>
    /// <param name="index">
    /// <paramref name="buffer" /> 内的索引，流从此处开始。</param>
    /// <param name="count">流的长度（以字节为单位）。</param>
    /// <param name="writable">
    /// <see cref="P:System.IO.MemoryStream.CanWrite" /> 属性的设置，确定该流是否支持写入。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, int index, int count, bool writable)
      : this(buffer, index, count, writable, false)
    {
    }

    /// <summary>在 <see cref="P:System.IO.MemoryStream.CanWrite" /> 属性和调用 <see cref="M:System.IO.MemoryStream.GetBuffer" /> 的能力按指定设置的状态下，基于字节数组的指定区域初始化 <see cref="T:System.IO.MemoryStream" /> 类的新实例。</summary>
    /// <param name="buffer">从中创建此流的无符号字节的数组。</param>
    /// <param name="index">
    /// <paramref name="buffer" /> 内的索引，流从此处开始。</param>
    /// <param name="count">流的长度（以字节为单位）。</param>
    /// <param name="writable">
    /// <see cref="P:System.IO.MemoryStream.CanWrite" /> 属性的设置，确定该流是否支持写入。</param>
    /// <param name="publiclyVisible">设置为 true 可以启用 <see cref="M:System.IO.MemoryStream.GetBuffer" />，它返回无符号字节数组，流从该数组创建；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    [__DynamicallyInvokable]
    public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this._buffer = buffer;
      this._origin = this._position = index;
      this._length = this._capacity = index + count;
      this._writable = writable;
      this._exposable = publiclyVisible;
      this._expandable = false;
      this._isOpen = true;
    }

    private void EnsureWriteable()
    {
      if (this.CanWrite)
        return;
      __Error.WriteNotSupported();
    }

    /// <summary>释放 <see cref="T:System.IO.MemoryStream" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        this._isOpen = false;
        this._writable = false;
        this._expandable = false;
        this._lastReadTask = (Task<int>) null;
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    private bool EnsureCapacity(int value)
    {
      if (value < 0)
        throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
      if (value <= this._capacity)
        return false;
      int num = value;
      if (num < 256)
        num = 256;
      if (num < this._capacity * 2)
        num = this._capacity * 2;
      if ((uint) (this._capacity * 2) > 2147483591U)
        num = value > 2147483591 ? value : 2147483591;
      this.Capacity = num;
      return true;
    }

    /// <summary>重写 <see cref="M:System.IO.Stream.Flush" /> 方法以便不执行任何操作。</summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override void Flush()
    {
    }

    /// <summary>异步清除此流的所有缓冲区，并监视取消请求。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <param name="cancellationToken">要监视取消请求的标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
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

    /// <summary>返回从中创建此流的无符号字节的数组。</summary>
    /// <returns>创建此流所用的字节数组；或者如果在当前实例的构造期间没有向 <see cref="T:System.IO.MemoryStream" /> 构造函数提供字节数组，则为基础数组。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">未使用公共可见缓冲区创建 MemoryStream 的实例。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual byte[] GetBuffer()
    {
      if (!this._exposable)
        throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_MemStreamBuffer"));
      return this._buffer;
    }

    /// <summary>返回从中创建此流的无符号字节的数组。用于指示转换是否成功的返回值。</summary>
    /// <returns>如果转换成功，则为 true；否则为 false。</returns>
    /// <param name="buffer">用于创建此流的字节数组段。</param>
    [__DynamicallyInvokable]
    public virtual bool TryGetBuffer(out ArraySegment<byte> buffer)
    {
      if (!this._exposable)
      {
        buffer = new ArraySegment<byte>();
        return false;
      }
      buffer = new ArraySegment<byte>(this._buffer, this._origin, this._length - this._origin);
      return true;
    }

    internal byte[] InternalGetBuffer()
    {
      return this._buffer;
    }

    [FriendAccessAllowed]
    internal void InternalGetOriginAndLength(out int origin, out int length)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      origin = this._origin;
      length = this._length;
    }

    internal int InternalGetPosition()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      return this._position;
    }

    internal int InternalReadInt32()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      int num = this._position = this._position + 4;
      if (num > this._length)
      {
        this._position = this._length;
        __Error.EndOfFile();
      }
      return (int) this._buffer[num - 4] | (int) this._buffer[num - 3] << 8 | (int) this._buffer[num - 2] << 16 | (int) this._buffer[num - 1] << 24;
    }

    internal int InternalEmulateRead(int count)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      int num = this._length - this._position;
      if (num > count)
        num = count;
      if (num < 0)
        num = 0;
      this._position = this._position + num;
      return num;
    }

    /// <summary>从当前流中读取字节块并将数据写入缓冲区。</summary>
    /// <returns>写入缓冲区中的总字节数。如果字节数当前不可用，则总字节数可能小于所请求的字节数；如果在读取到任何字节前已到达流结尾，则为零。</returns>
    /// <param name="buffer">当此方法返回时，包含指定的字节数组，该数组中从 <paramref name="offset" /> 到 (<paramref name="offset" /> + <paramref name="count" /> -1) 之间的值由从当前流中读取的字符替换。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始存储当前流中的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="offset" /> 的结果小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流实例已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int Read([In, Out] byte[] buffer, int offset, int count)
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
      int byteCount = this._length - this._position;
      if (byteCount > count)
        byteCount = count;
      if (byteCount <= 0)
        return 0;
      if (byteCount <= 8)
      {
        int num = byteCount;
        while (--num >= 0)
          buffer[offset + num] = this._buffer[this._position + num];
      }
      else
        Buffer.InternalBlockCopy((Array) this._buffer, this._position, (Array) buffer, offset, byteCount);
      this._position = this._position + byteCount;
      return byteCount;
    }

    /// <summary>从当前流异步读取字节的序列，将流中的位置提升读取的字节数，并监视取消请求。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可小于所请求的字节数；如果已到达流结尾时，则为 0（零）。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
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
      catch (OperationCanceledException ex)
      {
        return Task.FromCancellation<int>(ex);
      }
      catch (Exception ex)
      {
        return Task.FromException<int>(ex);
      }
    }

    /// <summary>从当前流中读取一个字节。</summary>
    /// <returns>强制转换为 <see cref="T:System.Int32" /> 的字节；或者如果已到达流的末尾，则为 -1。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前流实例已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int ReadByte()
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (this._position >= this._length)
        return -1;
      byte[] numArray = this._buffer;
      int num = this._position;
      this._position = num + 1;
      int index = num;
      return (int) numArray[index];
    }

    /// <summary>使用指定的缓冲区大小和取消令牌，从当前流中异步读取所有字节并将其写入到另一个流中。</summary>
    /// <returns>表示异步复制操作的任务。</returns>
    /// <param name="destination">当前流的内容将复制到的流。</param>
    /// <param name="bufferSize">缓冲区的大小（以字节为单位）。此值必须大于零。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="buffersize" /> 为负数或零。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流或目标流已释放。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持阅读，或目标流不支持写入。</exception>
    [__DynamicallyInvokable]
    public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
      if (destination == null)
        throw new ArgumentNullException("destination");
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (!this.CanRead && !this.CanWrite)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!destination.CanRead && !destination.CanWrite)
        throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (!destination.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      if (this.GetType() != typeof (MemoryStream))
        return base.CopyToAsync(destination, bufferSize, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      int offset = this._position;
      int count = this.InternalEmulateRead(this._length - this._position);
      MemoryStream memoryStream = destination as MemoryStream;
      if (memoryStream == null)
        return destination.WriteAsync(this._buffer, offset, count, cancellationToken);
      try
      {
        memoryStream.Write(this._buffer, offset, count);
        return Task.CompletedTask;
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
    }

    /// <summary>将当前流中的位置设置为指定值。</summary>
    /// <returns>流内的新位置，通过将初始引用点和偏移量合并计算而得。</returns>
    /// <param name="offset">流内的新位置。它是相对于 <paramref name="loc" /> 参数的位置，而且可正可负。</param>
    /// <param name="loc">类型 <see cref="T:System.IO.SeekOrigin" /> 的值，它用作查找引用点。</param>
    /// <exception cref="T:System.IO.IOException">试图在流的开始位置之前查找。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="T:System.IO.SeekOrigin" /> 无效。- 或 -<paramref name="offset" /> 导致算法溢出。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流实例已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override long Seek(long offset, SeekOrigin loc)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      if (offset > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
      switch (loc)
      {
        case SeekOrigin.Begin:
          int num1 = this._origin + (int) offset;
          if (offset < 0L || num1 < this._origin)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          this._position = num1;
          break;
        case SeekOrigin.Current:
          int num2 = this._position + (int) offset;
          if ((long) this._position + offset < (long) this._origin || num2 < this._origin)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          this._position = num2;
          break;
        case SeekOrigin.End:
          int num3 = this._length + (int) offset;
          if ((long) this._length + offset < (long) this._origin || num3 < this._origin)
            throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
          this._position = num3;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
      }
      return (long) this._position;
    }

    /// <summary>将当前流的长度设为指定值。</summary>
    /// <param name="value">用于设置长度的值。</param>
    /// <exception cref="T:System.NotSupportedException">当前流无法调整大小，而且 <paramref name="value" /> 大于当前容量。- 或 - 当前流不支持写入。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为负或者大于 <see cref="T:System.IO.MemoryStream" /> 的最大长度，其中最大长度为 (<see cref="F:System.Int32.MaxValue" />- origin)，origin 为基础缓冲区中作为流的起点的索引。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override void SetLength(long value)
    {
      if (value < 0L || value > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
      this.EnsureWriteable();
      if (value > (long) (int.MaxValue - this._origin))
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
      int num = this._origin + (int) value;
      if (!this.EnsureCapacity(num) && num > this._length)
        Array.Clear((Array) this._buffer, this._length, num - this._length);
      this._length = num;
      if (this._position <= num)
        return;
      this._position = num;
    }

    /// <summary>将流内容写入字节数组，而与 <see cref="P:System.IO.MemoryStream.Position" /> 属性无关。</summary>
    /// <returns>新的字节数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte[] ToArray()
    {
      byte[] numArray = new byte[this._length - this._origin];
      Buffer.InternalBlockCopy((Array) this._buffer, this._origin, (Array) numArray, 0, this._length - this._origin);
      return numArray;
    }

    /// <summary>使用从缓冲区读取的数据将字节块写入当前流。</summary>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到当前流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。有关其他信息，请参见 <see cref="P:System.IO.Stream.CanWrite" />。- 或 - 当前位置到流结尾的距离小于 <paramref name="count" /> 字节，并且无法修改容量。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="offset" /> 的结果小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流实例已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(byte[] buffer, int offset, int count)
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
      this.EnsureWriteable();
      int num1 = this._position + count;
      if (num1 < 0)
        throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
      if (num1 > this._length)
      {
        bool flag = this._position > this._length;
        if (num1 > this._capacity && this.EnsureCapacity(num1))
          flag = false;
        if (flag)
          Array.Clear((Array) this._buffer, this._length, num1 - this._length);
        this._length = num1;
      }
      if (count <= 8 && buffer != this._buffer)
      {
        int num2 = count;
        while (--num2 >= 0)
          this._buffer[this._position + num2] = buffer[offset + num2];
      }
      else
        Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._buffer, this._position, count);
      this._position = num1;
    }

    /// <summary>将字节的序列异步写入当前流，将该流中的当前位置向前移动写入的字节数，并监视取消请求。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
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
      catch (OperationCanceledException ex)
      {
        return (Task) Task.FromCancellation<VoidTaskResult>(ex);
      }
      catch (Exception ex)
      {
        return Task.FromException(ex);
      }
    }

    /// <summary>将一个字节写入当前位置上的当前流。</summary>
    /// <param name="value">要写入的字节。</param>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。有关其他信息，请参见 <see cref="P:System.IO.Stream.CanWrite" />。- 或 - 当前位置位于流的末尾，而且容量不能被修改。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override void WriteByte(byte value)
    {
      if (!this._isOpen)
        __Error.StreamIsClosed();
      this.EnsureWriteable();
      if (this._position >= this._length)
      {
        int num = this._position + 1;
        bool flag = this._position > this._length;
        if (num >= this._capacity && this.EnsureCapacity(num))
          flag = false;
        if (flag)
          Array.Clear((Array) this._buffer, this._length, this._position - this._length);
        this._length = num;
      }
      byte[] numArray = this._buffer;
      int num1 = this._position;
      this._position = num1 + 1;
      int index = num1;
      int num2 = (int) value;
      numArray[index] = (byte) num2;
    }

    /// <summary>将此内存流的整个内容写入到另一个流中。</summary>
    /// <param name="stream">要写入此内存流的流。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流或目标流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteTo(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream", Environment.GetResourceString("ArgumentNull_Stream"));
      if (!this._isOpen)
        __Error.StreamIsClosed();
      stream.Write(this._buffer, this._origin, this._length - this._origin);
    }
  }
}
