// Decompiled with JetBrains decompiler
// Type: System.IO.BufferedStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>将缓冲层添加到另一个流上的读取和写入操作。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public sealed class BufferedStream : Stream
  {
    private const int _DefaultBufferSize = 4096;
    private Stream _stream;
    private byte[] _buffer;
    private readonly int _bufferSize;
    private int _readPos;
    private int _readLen;
    private int _writePos;
    private BeginEndAwaitableAdapter _beginEndAwaitable;
    private Task<int> _lastSyncCompletedReadTask;
    private const int MaxShadowBufferSize = 81920;

    internal Stream UnderlyingStream
    {
      [FriendAccessAllowed] get
      {
        return this._stream;
      }
    }

    internal int BufferSize
    {
      [FriendAccessAllowed] get
      {
        return this._bufferSize;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持读取。</summary>
    /// <returns>如果流支持读取，则为 true；如果流已关闭或是通过只写访问方式打开的，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanRead
    {
      get
      {
        if (this._stream != null)
          return this._stream.CanRead;
        return false;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持写入。</summary>
    /// <returns>如果流支持写入，则为 true；如果流已关闭或是通过只读访问方式打开的，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanWrite
    {
      get
      {
        if (this._stream != null)
          return this._stream.CanWrite;
        return false;
      }
    }

    /// <summary>获取一个值，该值指示当前流是否支持查找。</summary>
    /// <returns>如果流支持查找，则为 true；如果流已关闭或者如果流是由操作系统句柄（如管道或到控制台的输出）构造的，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public override bool CanSeek
    {
      get
      {
        if (this._stream != null)
          return this._stream.CanSeek;
        return false;
      }
    }

    /// <summary>获取流长度，长度以字节为单位。</summary>
    /// <returns>流长度，以字节为单位。</returns>
    /// <exception cref="T:System.IO.IOException">基础流为 null 或已关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override long Length
    {
      get
      {
        this.EnsureNotClosed();
        if (this._writePos > 0)
          this.FlushWrite();
        return this._stream.Length;
      }
    }

    /// <summary>获取当前流内的位置。</summary>
    /// <returns>当前流内的位置。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">传递给 <see cref="M:System.IO.BufferedStream.Seek(System.Int64,System.IO.SeekOrigin)" /> 的值为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如流被关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override long Position
    {
      get
      {
        this.EnsureNotClosed();
        this.EnsureCanSeek();
        return this._stream.Position + (long) (this._readPos - this._readLen + this._writePos);
      }
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        this.EnsureNotClosed();
        this.EnsureCanSeek();
        if (this._writePos > 0)
          this.FlushWrite();
        this._readPos = 0;
        this._readLen = 0;
        this._stream.Seek(value, SeekOrigin.Begin);
      }
    }

    private BufferedStream()
    {
    }

    /// <summary>使用默认的缓冲区大小 4096 字节初始化 <see cref="T:System.IO.BufferedStream" /> 类的新实例。</summary>
    /// <param name="stream">当前流。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    public BufferedStream(Stream stream)
      : this(stream, 4096)
    {
    }

    /// <summary>使用指定的缓冲区大小初始化 <see cref="T:System.IO.BufferedStream" /> 类的新实例。</summary>
    /// <param name="stream">当前流。</param>
    /// <param name="bufferSize">缓冲区大小，以字节为单位。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负。</exception>
    public BufferedStream(Stream stream, int bufferSize)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) "bufferSize"));
      this._stream = stream;
      this._bufferSize = bufferSize;
      if (this._stream.CanRead || this._stream.CanWrite)
        return;
      __Error.StreamIsClosed();
    }

    private void EnsureNotClosed()
    {
      if (this._stream != null)
        return;
      __Error.StreamIsClosed();
    }

    private void EnsureCanSeek()
    {
      if (this._stream.CanSeek)
        return;
      __Error.SeekNotSupported();
    }

    private void EnsureCanRead()
    {
      if (this._stream.CanRead)
        return;
      __Error.ReadNotSupported();
    }

    private void EnsureCanWrite()
    {
      if (this._stream.CanWrite)
        return;
      __Error.WriteNotSupported();
    }

    private void EnsureBeginEndAwaitableAllocated()
    {
      if (this._beginEndAwaitable != null)
        return;
      this._beginEndAwaitable = new BeginEndAwaitableAdapter();
    }

    private void EnsureShadowBufferAllocated()
    {
      if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
        return;
      byte[] numArray = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
      Buffer.InternalBlockCopy((Array) this._buffer, 0, (Array) numArray, 0, this._writePos);
      this._buffer = numArray;
    }

    private void EnsureBufferAllocated()
    {
      if (this._buffer != null)
        return;
      this._buffer = new byte[this._bufferSize];
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (this._stream == null)
          return;
        try
        {
          this.Flush();
        }
        finally
        {
          this._stream.Close();
        }
      }
      finally
      {
        this._stream = (Stream) null;
        this._buffer = (byte[]) null;
        this._lastSyncCompletedReadTask = (Task<int>) null;
        base.Dispose(disposing);
      }
    }

    /// <summary>清除此流的所有缓冲区并导致所有缓冲数据都写入基础设备中。</summary>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.IO.IOException">数据源或储存库未打开。</exception>
    /// <filterpriority>2</filterpriority>
    public override void Flush()
    {
      this.EnsureNotClosed();
      if (this._writePos > 0)
        this.FlushWrite();
      else if (this._readPos < this._readLen)
      {
        if (!this._stream.CanSeek)
          return;
        this.FlushRead();
        if (!this._stream.CanWrite && !(this._stream is BufferedStream))
          return;
        this._stream.Flush();
      }
      else
      {
        if (this._stream.CanWrite || this._stream is BufferedStream)
          this._stream.Flush();
        this._writePos = this._readPos = this._readLen = 0;
      }
    }

    /// <summary>异步清理这个流的所有缓冲区，并使所有缓冲数据写入基础设备，并且监控取消请求。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <param name="cancellationToken">针对取消请求监视的标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
        return (Task) Task.FromCancellation<int>(cancellationToken);
      this.EnsureNotClosed();
      return BufferedStream.FlushAsyncInternal(cancellationToken, this, this._stream, this._writePos, this._readPos, this._readLen);
    }

    private static async Task FlushAsyncInternal(CancellationToken cancellationToken, BufferedStream _this, Stream stream, int writePos, int readPos, int readLen)
    {
      SemaphoreSlim sem = _this.EnsureAsyncActiveSemaphoreInitialized();
      await sem.WaitAsync().ConfigureAwait(false);
      try
      {
        if (writePos > 0)
          await _this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
        else if (readPos < readLen)
        {
          if (!stream.CanSeek)
            return;
          _this.FlushRead();
          if (!stream.CanRead && !(stream is BufferedStream))
            return;
          await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
        else
        {
          if (!stream.CanWrite && !(stream is BufferedStream))
            return;
          await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
      }
      finally
      {
        sem.Release();
      }
    }

    private void FlushRead()
    {
      if (this._readPos - this._readLen != 0)
        this._stream.Seek((long) (this._readPos - this._readLen), SeekOrigin.Current);
      this._readPos = 0;
      this._readLen = 0;
    }

    private void ClearReadBufferBeforeWrite()
    {
      if (this._readPos == this._readLen)
      {
        this._readPos = this._readLen = 0;
      }
      else
      {
        if (!this._stream.CanSeek)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed"));
        this.FlushRead();
      }
    }

    private void FlushWrite()
    {
      this._stream.Write(this._buffer, 0, this._writePos);
      this._writePos = 0;
      this._stream.Flush();
    }

    private async Task FlushWriteAsync(CancellationToken cancellationToken)
    {
      await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
      this._writePos = 0;
      await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    private int ReadFromBuffer(byte[] array, int offset, int count)
    {
      int byteCount = this._readLen - this._readPos;
      if (byteCount == 0)
        return 0;
      if (byteCount > count)
        byteCount = count;
      Buffer.InternalBlockCopy((Array) this._buffer, this._readPos, (Array) array, offset, byteCount);
      this._readPos = this._readPos + byteCount;
      return byteCount;
    }

    private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
    {
      try
      {
        error = (Exception) null;
        return this.ReadFromBuffer(array, offset, count);
      }
      catch (Exception ex)
      {
        error = ex;
        return 0;
      }
    }

    /// <summary>将字节从当前缓冲流复制到数组。</summary>
    /// <returns>读入 <paramref name="array" /> 中的总字节数。如果可用的字节没有所请求的那么多，总字节数可能小于请求的字节数；或者如果在可读取任何数据前就已到达流的末尾，则为零。</returns>
    /// <param name="array">将字节复制到的缓冲区。</param>
    /// <param name="offset">缓冲区中的字节偏移量，从此处开始读取字节。</param>
    /// <param name="count">要读取的字节数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 的长度减去 <paramref name="offset" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">流未打开或为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override int Read([In, Out] byte[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this.EnsureNotClosed();
      this.EnsureCanRead();
      int num1 = this.ReadFromBuffer(array, offset, count);
      if (num1 == count)
        return num1;
      int num2 = num1;
      if (num1 > 0)
      {
        count -= num1;
        offset += num1;
      }
      this._readPos = this._readLen = 0;
      if (this._writePos > 0)
        this.FlushWrite();
      if (count >= this._bufferSize)
        return this._stream.Read(array, offset, count) + num2;
      this.EnsureBufferAllocated();
      this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
      return this.ReadFromBuffer(array, offset, count) + num2;
    }

    /// <summary>开始异步读操作。（考虑使用<see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />进行替换，请参见“备注”部分。）</summary>
    /// <returns>表示异步读取（可能仍处于挂起状态）的对象。</returns>
    /// <param name="buffer">数据读入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="callback">可选的异步回调，在完成读取时调用。</param>
    /// <param name="state">一个用户提供的对象，它将该特定的异步读取请求与其他请求区别开来。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">试图在流的末尾之外进行异步读取。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="offset" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持读取操作。</exception>
    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._stream == null)
        __Error.ReadNotSupported();
      this.EnsureCanRead();
      int num = 0;
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          Exception error;
          num = this.ReadFromBuffer(buffer, offset, count, out error);
          flag = num == count || error != null;
          if (flag)
          {
            Stream.SynchronousAsyncResult synchronousAsyncResult = error == null ? new Stream.SynchronousAsyncResult(num, state) : new Stream.SynchronousAsyncResult(error, state, false);
            if (callback != null)
              callback((IAsyncResult) synchronousAsyncResult);
            return (IAsyncResult) synchronousAsyncResult;
          }
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.BeginReadFromUnderlyingStream(buffer, offset + num, count - num, callback, state, num, semaphoreLockTask);
    }

    private IAsyncResult BeginReadFromUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, int bytesAlreadySatisfied, Task semaphoreLockTask)
    {
      return TaskToApm.Begin((Task) this.ReadFromUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, bytesAlreadySatisfied, semaphoreLockTask, true), callback, state);
    }

    /// <summary>等待挂起的异步读取操作完成。（考虑使用<see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />进行替换，请参见“备注”部分。）</summary>
    /// <returns>从流中读取的字节数，介于 0 （零）和您请求的字节数之间。流仅在流的末尾返回 0；否则应一直阻塞到至少有 1 个字节可用为止。</returns>
    /// <param name="asyncResult">对所等待的挂起异步请求的引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此 <see cref="T:System.IAsyncResult" /> 对象不是通过对该类调用 <see cref="M:System.IO.BufferedStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 来创建的。</exception>
    public override int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (asyncResult is Stream.SynchronousAsyncResult)
        return Stream.SynchronousAsyncResult.EndRead(asyncResult);
      return TaskToApm.End<int>(asyncResult);
    }

    private Task<int> LastSyncCompletedReadTask(int val)
    {
      Task<int> task1 = this._lastSyncCompletedReadTask;
      if (task1 != null && task1.Result == val)
        return task1;
      Task<int> task2 = Task.FromResult<int>(val);
      this._lastSyncCompletedReadTask = task2;
      return task2;
    }

    /// <summary>从当前流异步读取字节序列，将流中的位置向前移动读取的字节数，并监控取消请求。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可能小于所请求的字节数，或者如果已到达流的末尾时，则为 0（零）。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="cancellationToken">针对取消请求监视的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次读取操作使用。</exception>
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
      this.EnsureNotClosed();
      this.EnsureCanRead();
      int num = 0;
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          Exception error;
          num = this.ReadFromBuffer(buffer, offset, count, out error);
          flag = num == count || error != null;
          if (flag)
            return error == null ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(error);
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.ReadFromUnderlyingStreamAsync(buffer, offset + num, count - num, cancellationToken, num, semaphoreLockTask, false);
    }

    private async Task<int> ReadFromUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask, bool useApmPattern)
    {
      await semaphoreLockTask.ConfigureAwait(false);
      try
      {
        int num1 = this.ReadFromBuffer(array, offset, count);
        if (num1 == count)
          return bytesAlreadySatisfied + num1;
        if (num1 > 0)
        {
          count -= num1;
          offset += num1;
          bytesAlreadySatisfied += num1;
        }
        this._readPos = this._readLen = 0;
        if (this._writePos > 0)
          await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
        Stream stream;
        if (count >= this._bufferSize)
        {
          int num;
          if (useApmPattern)
          {
            this.EnsureBeginEndAwaitableAllocated();
            this._stream.BeginRead(array, offset, count, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
            num = bytesAlreadySatisfied;
            stream = this._stream;
            IAsyncResult asyncResult = await this._beginEndAwaitable;
            return num + stream.EndRead(asyncResult);
          }
          num = bytesAlreadySatisfied;
          int num2 = await this._stream.ReadAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
          return num + num2;
        }
        this.EnsureBufferAllocated();
        BufferedStream bufferedStream;
        if (useApmPattern)
        {
          this.EnsureBeginEndAwaitableAllocated();
          this._stream.BeginRead(this._buffer, 0, this._bufferSize, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
          bufferedStream = this;
          int num2 = bufferedStream._readLen;
          stream = this._stream;
          IAsyncResult asyncResult = await this._beginEndAwaitable;
          bufferedStream._readLen = stream.EndRead(asyncResult);
          bufferedStream = (BufferedStream) null;
          stream = (Stream) null;
        }
        else
        {
          bufferedStream = this;
          int num2 = bufferedStream._readLen;
          int num3 = await this._stream.ReadAsync(this._buffer, 0, this._bufferSize, cancellationToken).ConfigureAwait(false);
          bufferedStream._readLen = num3;
          bufferedStream = (BufferedStream) null;
        }
        return bytesAlreadySatisfied + this.ReadFromBuffer(array, offset, count);
      }
      finally
      {
        this.EnsureAsyncActiveSemaphoreInitialized().Release();
      }
    }

    /// <summary>从基础流中读取一个字节，并返回转换为 int 的该字节；或者如果从流的末尾读取则返回 -1。</summary>
    /// <returns>转换为 int 的字节，或者如果从流的末尾读取则为 -1。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如流被关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override int ReadByte()
    {
      this.EnsureNotClosed();
      this.EnsureCanRead();
      if (this._readPos == this._readLen)
      {
        if (this._writePos > 0)
          this.FlushWrite();
        this.EnsureBufferAllocated();
        this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
        this._readPos = 0;
      }
      if (this._readPos == this._readLen)
        return -1;
      byte[] numArray = this._buffer;
      int num = this._readPos;
      this._readPos = num + 1;
      int index = num;
      return (int) numArray[index];
    }

    private void WriteToBuffer(byte[] array, ref int offset, ref int count)
    {
      int byteCount = Math.Min(this._bufferSize - this._writePos, count);
      if (byteCount <= 0)
        return;
      this.EnsureBufferAllocated();
      Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, byteCount);
      this._writePos = this._writePos + byteCount;
      count -= byteCount;
      offset += byteCount;
    }

    private void WriteToBuffer(byte[] array, ref int offset, ref int count, out Exception error)
    {
      try
      {
        error = (Exception) null;
        this.WriteToBuffer(array, ref offset, ref count);
      }
      catch (Exception ex)
      {
        error = ex;
      }
    }

    /// <summary>将字节复制到缓冲流，并将缓冲流内的当前位置前进写入的字节数。</summary>
    /// <param name="array">字节数组，从该字节数组将 <paramref name="count" /> 个字节复制到当前缓冲流中。</param>
    /// <param name="offset">缓冲区中的偏移量，从此处开始将字节复制到当前缓冲流中。</param>
    /// <param name="count">要写入当前缓冲流中的字节数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 的长度减去 <paramref name="offset" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">流关闭或为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override void Write(byte[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this.EnsureNotClosed();
      this.EnsureCanWrite();
      if (this._writePos == 0)
        this.ClearReadBufferBeforeWrite();
      int count1 = checked (this._writePos + count);
      if (checked (count1 + count) < checked (this._bufferSize + this._bufferSize))
      {
        this.WriteToBuffer(array, ref offset, ref count);
        if (this._writePos < this._bufferSize)
          return;
        this._stream.Write(this._buffer, 0, this._writePos);
        this._writePos = 0;
        this.WriteToBuffer(array, ref offset, ref count);
      }
      else
      {
        if (this._writePos > 0)
        {
          if (count1 <= this._bufferSize + this._bufferSize && count1 <= 81920)
          {
            this.EnsureShadowBufferAllocated();
            Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, count);
            this._stream.Write(this._buffer, 0, count1);
            this._writePos = 0;
            return;
          }
          this._stream.Write(this._buffer, 0, this._writePos);
          this._writePos = 0;
        }
        this._stream.Write(array, offset, count);
      }
    }

    /// <summary>开始异步写操作。（考虑使用<see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />进行替换，请参见“备注”部分。）</summary>
    /// <returns>一个表示异步写入（可能仍处于挂起状态）的对象。</returns>
    /// <param name="buffer">包含要写入当前流的数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到当前流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="callback">异步写操作完成后调用的方法。</param>
    /// <param name="state">一个用户提供的对象，它将该特定的异步写入请求与其他请求区别开来。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="buffer" /> 长度减去 <paramref name="offset" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._stream == null)
        __Error.ReadNotSupported();
      this.EnsureCanWrite();
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          if (this._writePos == 0)
            this.ClearReadBufferBeforeWrite();
          flag = count < this._bufferSize - this._writePos;
          if (flag)
          {
            Exception error;
            this.WriteToBuffer(buffer, ref offset, ref count, out error);
            Stream.SynchronousAsyncResult synchronousAsyncResult = error == null ? new Stream.SynchronousAsyncResult(state) : new Stream.SynchronousAsyncResult(error, state, true);
            if (callback != null)
              callback((IAsyncResult) synchronousAsyncResult);
            return (IAsyncResult) synchronousAsyncResult;
          }
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.BeginWriteToUnderlyingStream(buffer, offset, count, callback, state, semaphoreLockTask);
    }

    private IAsyncResult BeginWriteToUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, Task semaphoreLockTask)
    {
      return TaskToApm.Begin(this.WriteToUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, semaphoreLockTask, true), callback, state);
    }

    /// <summary>结束异步写入操作，在 I/O 操作完成之前一直阻止。（考虑使用<see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" />进行替换，请参见“备注”部分。）</summary>
    /// <param name="asyncResult">挂起的异步请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此 <see cref="T:System.IAsyncResult" /> 对象不是通过对该类调用 <see cref="M:System.IO.BufferedStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 来创建的。</exception>
    public override void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (asyncResult is Stream.SynchronousAsyncResult)
        Stream.SynchronousAsyncResult.EndWrite(asyncResult);
      else
        TaskToApm.End(asyncResult);
    }

    /// <summary>将字节序列异步写入当前流，通过写入的字节数提前该流的当前位置，并监视取消请求数。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="cancellationToken">针对取消请求监视的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次写入操作使用。</exception>
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
        return (Task) Task.FromCancellation<int>(cancellationToken);
      this.EnsureNotClosed();
      this.EnsureCanWrite();
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task semaphoreLockTask = semaphoreSlim.WaitAsync();
      if (semaphoreLockTask.Status == TaskStatus.RanToCompletion)
      {
        bool flag = true;
        try
        {
          if (this._writePos == 0)
            this.ClearReadBufferBeforeWrite();
          flag = count < this._bufferSize - this._writePos;
          if (flag)
          {
            Exception error;
            this.WriteToBuffer(buffer, ref offset, ref count, out error);
            return error == null ? Task.CompletedTask : Task.FromException(error);
          }
        }
        finally
        {
          if (flag)
            semaphoreSlim.Release();
        }
      }
      return this.WriteToUnderlyingStreamAsync(buffer, offset, count, cancellationToken, semaphoreLockTask, false);
    }

    private async Task WriteToUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, Task semaphoreLockTask, bool useApmPattern)
    {
      await semaphoreLockTask.ConfigureAwait(false);
      try
      {
        if (this._writePos == 0)
          this.ClearReadBufferBeforeWrite();
        int totalUserBytes = checked (this._writePos + count);
        Stream stream;
        if (checked (totalUserBytes + count) < checked (this._bufferSize + this._bufferSize))
        {
          this.WriteToBuffer(array, ref offset, ref count);
          if (this._writePos < this._bufferSize)
            return;
          if (useApmPattern)
          {
            this.EnsureBeginEndAwaitableAllocated();
            this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
            stream = this._stream;
            IAsyncResult asyncResult = await this._beginEndAwaitable;
            stream.EndWrite(asyncResult);
            stream = (Stream) null;
          }
          else
            await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
          this._writePos = 0;
          this.WriteToBuffer(array, ref offset, ref count);
        }
        else
        {
          if (this._writePos > 0)
          {
            if (totalUserBytes <= this._bufferSize + this._bufferSize && totalUserBytes <= 81920)
            {
              this.EnsureShadowBufferAllocated();
              Buffer.InternalBlockCopy((Array) array, offset, (Array) this._buffer, this._writePos, count);
              if (useApmPattern)
              {
                this.EnsureBeginEndAwaitableAllocated();
                this._stream.BeginWrite(this._buffer, 0, totalUserBytes, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
                stream = this._stream;
                IAsyncResult asyncResult = await this._beginEndAwaitable;
                stream.EndWrite(asyncResult);
                stream = (Stream) null;
              }
              else
                await this._stream.WriteAsync(this._buffer, 0, totalUserBytes, cancellationToken).ConfigureAwait(false);
              this._writePos = 0;
              return;
            }
            if (useApmPattern)
            {
              this.EnsureBeginEndAwaitableAllocated();
              this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
              stream = this._stream;
              IAsyncResult asyncResult = await this._beginEndAwaitable;
              stream.EndWrite(asyncResult);
              stream = (Stream) null;
            }
            else
              await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
            this._writePos = 0;
          }
          if (useApmPattern)
          {
            this.EnsureBeginEndAwaitableAllocated();
            this._stream.BeginWrite(array, offset, count, BeginEndAwaitableAdapter.Callback, (object) this._beginEndAwaitable);
            stream = this._stream;
            IAsyncResult asyncResult = await this._beginEndAwaitable;
            stream.EndWrite(asyncResult);
            stream = (Stream) null;
          }
          else
            await this._stream.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
        }
      }
      finally
      {
        this.EnsureAsyncActiveSemaphoreInitialized().Release();
      }
    }

    /// <summary>将一个字节写入缓冲流的当前位置。</summary>
    /// <param name="value">要写入流的字节。</param>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override void WriteByte(byte value)
    {
      this.EnsureNotClosed();
      if (this._writePos == 0)
      {
        this.EnsureCanWrite();
        this.ClearReadBufferBeforeWrite();
        this.EnsureBufferAllocated();
      }
      if (this._writePos >= this._bufferSize - 1)
        this.FlushWrite();
      byte[] numArray = this._buffer;
      int num1 = this._writePos;
      this._writePos = num1 + 1;
      int index = num1;
      int num2 = (int) value;
      numArray[index] = (byte) num2;
    }

    /// <summary>设置当前缓冲流中的位置。</summary>
    /// <returns>当前缓冲流中的新位置。</returns>
    /// <param name="offset">相对于 <paramref name="origin" /> 的字节偏移量。</param>
    /// <param name="origin">
    /// <see cref="T:System.IO.SeekOrigin" /> 类型的值，指示用于获得新位置的参考点。</param>
    /// <exception cref="T:System.IO.IOException">流未打开或为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override long Seek(long offset, SeekOrigin origin)
    {
      this.EnsureNotClosed();
      this.EnsureCanSeek();
      if (this._writePos > 0)
      {
        this.FlushWrite();
        return this._stream.Seek(offset, origin);
      }
      if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
        offset -= (long) (this._readLen - this._readPos);
      long position = this.Position;
      long num = this._stream.Seek(offset, origin);
      this._readPos = (int) (num - (position - (long) this._readPos));
      if (0 <= this._readPos && this._readPos < this._readLen)
        this._stream.Seek((long) (this._readLen - this._readPos), SeekOrigin.Current);
      else
        this._readPos = this._readLen = 0;
      return num;
    }

    /// <summary>设置缓冲流的长度。</summary>
    /// <param name="value">一个整数，指示当前缓冲流的所需长度（以字节为单位）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="value" /> 为负数。</exception>
    /// <exception cref="T:System.IO.IOException">流未打开或为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">流不同时支持写入和查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    public override void SetLength(long value)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegFileSize"));
      this.EnsureNotClosed();
      this.EnsureCanSeek();
      this.EnsureCanWrite();
      this.Flush();
      this._stream.SetLength(value);
    }
  }
}
