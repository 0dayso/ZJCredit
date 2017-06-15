// Decompiled with JetBrains decompiler
// Type: System.IO.Stream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>提供字节序列的一般视图。这是一个抽象类。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Stream : MarshalByRefObject, IDisposable
  {
    /// <summary>无后备存储区的 Stream。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly Stream Null = (Stream) new Stream.NullStream();
    private const int _DefaultCopyBufferSize = 81920;
    [NonSerialized]
    private Stream.ReadWriteTask _activeReadWriteTask;
    [NonSerialized]
    private SemaphoreSlim _asyncActiveSemaphore;

    /// <summary>当在派生类中重写时，获取指示当前流是否支持读取的值。</summary>
    /// <returns>如果流支持读取，为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool CanRead { [__DynamicallyInvokable] get; }

    /// <summary>当在派生类中重写时，获取指示当前流是否支持查找功能的值。</summary>
    /// <returns>如果流支持查找，为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool CanSeek { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值确定当前流是否可以超时。</summary>
    /// <returns>一个确定当前流是否可以超时的值。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual bool CanTimeout
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>当在派生类中重写时，获取指示当前流是否支持写入功能的值。</summary>
    /// <returns>如果流支持写入，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool CanWrite { [__DynamicallyInvokable] get; }

    /// <summary>当在派生类中重写时，获取流长度（以字节为单位）。</summary>
    /// <returns>表示流长度（以字节为单位）的长值。</returns>
    /// <exception cref="T:System.NotSupportedException">从 Stream 派生的类不支持查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract long Length { [__DynamicallyInvokable] get; }

    /// <summary>当在派生类中重写时，获取或设置当前流中的位置。</summary>
    /// <returns>流中的当前位置。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持查找。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract long Position { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取或设置一个值（以毫秒为单位），该值确定流在超时前尝试读取多长时间。</summary>
    /// <returns>一个确定流在超时前尝试读取多长时间的值（以毫秒为单位）。</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.IO.Stream.ReadTimeout" /> 方法总是引发 <see cref="T:System.InvalidOperationException" />。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int ReadTimeout
    {
      [__DynamicallyInvokable] get
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
      [__DynamicallyInvokable] set
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
    }

    /// <summary>获取或设置一个值（以毫秒为单位），该值确定流在超时前尝试写入多长时间。</summary>
    /// <returns>一个确定流在超时前尝试写入多长时间的值（以毫秒为单位）。</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.IO.Stream.WriteTimeout" /> 方法总是引发 <see cref="T:System.InvalidOperationException" />。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int WriteTimeout
    {
      [__DynamicallyInvokable] get
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
      [__DynamicallyInvokable] set
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
      }
    }

    /// <summary>初始化 <see cref="T:System.IO.Stream" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Stream()
    {
    }

    internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
    {
      return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, (Func<SemaphoreSlim>) (() => new SemaphoreSlim(1, 1)));
    }

    /// <summary>从当前流中异步读取字节并将其写入到另一个流中。</summary>
    /// <returns>表示异步复制操作的任务。</returns>
    /// <param name="destination">当前流的内容将复制到的流。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流或目标流已释放。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持阅读，或目标流不支持写入。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task CopyToAsync(Stream destination)
    {
      return this.CopyToAsync(destination, 81920);
    }

    /// <summary>使用指定的缓冲区大小，从当前流中异步读取字节并将其写入到另一流中。</summary>
    /// <returns>表示异步复制操作的任务。</returns>
    /// <param name="destination">当前流的内容将复制到的流。</param>
    /// <param name="bufferSize">缓冲区的大小（以字节为单位）。此值必须大于零。默认大小为 81920。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="buffersize" /> 为负数或零。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流或目标流已释放。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持阅读，或目标流不支持写入。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task CopyToAsync(Stream destination, int bufferSize)
    {
      return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
    }

    /// <summary>使用指定的缓冲区大小和取消令牌，从当前流中异步读取字节并将其写入到另一个流中。</summary>
    /// <returns>表示异步复制操作的任务。</returns>
    /// <param name="destination">当前流的内容将复制到的流。</param>
    /// <param name="bufferSize">缓冲区的大小（以字节为单位）。此值必须大于零。默认大小为 81920。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="buffersize" /> 为负数或零。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前流或目标流已释放。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持阅读，或目标流不支持写入。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
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
      return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
    }

    private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
      byte[] buffer = new byte[bufferSize];
      while (true)
      {
        int num = await this.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
        int bytesRead;
        if ((bytesRead = num) != 0)
          await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
        else
          break;
      }
    }

    /// <summary>从当前流中读取字节并将其写入到另一流中。</summary>
    /// <param name="destination">当前流的内容将复制到的流。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持读取。- 或 -<paramref name="destination" /> 不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在调用 <paramref name="destination" /> 方法前当前流或 <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> 已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(Stream destination)
    {
      if (destination == null)
        throw new ArgumentNullException("destination");
      if (!this.CanRead && !this.CanWrite)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!destination.CanRead && !destination.CanWrite)
        throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (!destination.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      this.InternalCopyTo(destination, 81920);
    }

    /// <summary>使用指定的缓冲区大小，从当前流中读取字节并将其写入到另一流中。</summary>
    /// <param name="destination">当前流的内容将复制到的流。</param>
    /// <param name="bufferSize">缓冲区的大小。此值必须大于零。默认大小为 81920。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数或零。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不支持读取。- 或 -<paramref name="destination" /> 不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在调用 <paramref name="destination" /> 方法前当前流或 <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> 已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    [__DynamicallyInvokable]
    public void CopyTo(Stream destination, int bufferSize)
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
      this.InternalCopyTo(destination, bufferSize);
    }

    private void InternalCopyTo(Stream destination, int bufferSize)
    {
      byte[] buffer = new byte[bufferSize];
      int count;
      while ((count = this.Read(buffer, 0, buffer.Length)) != 0)
        destination.Write(buffer, 0, count);
    }

    /// <summary>关闭当前流并释放与之关联的所有资源（如套接字和文件句柄）。不直接调用此方法，而应确保流得以正确释放。</summary>
    /// <filterpriority>1</filterpriority>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.IO.Stream" /> 使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Close();
    }

    /// <summary>释放由 <see cref="T:System.IO.Stream" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>当在派生类中重写时，将清除该流的所有缓冲区，并使得所有缓冲数据被写入到基础设备。</summary>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract void Flush();

    /// <summary>异步清除此流的所有缓冲区并导致所有缓冲数据都写入基础设备中。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task FlushAsync()
    {
      return this.FlushAsync(CancellationToken.None);
    }

    /// <summary>异步清理这个流的所有缓冲区，并使所有缓冲数据写入基础设备，并且监控取消请求。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task FlushAsync(CancellationToken cancellationToken)
    {
      TaskFactory factory = Task.Factory;
      CancellationToken cancellationToken1 = cancellationToken;
      int num = 8;
      TaskScheduler @default = TaskScheduler.Default;
      return factory.StartNew((Action<object>) (state => ((Stream) state).Flush()), (object) this, cancellationToken1, (TaskCreationOptions) num, @default);
    }

    /// <summary>分配 <see cref="T:System.Threading.WaitHandle" /> 对象。</summary>
    /// <returns>对已分配的 WaitHandle 的引用。</returns>
    [Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
    protected virtual WaitHandle CreateWaitHandle()
    {
      return (WaitHandle) new ManualResetEvent(false);
    }

    /// <summary>开始异步读操作。（考虑使用 <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <returns>表示异步读取的 <see cref="T:System.IAsyncResult" />（可能仍处于挂起状态）。</returns>
    /// <param name="buffer">数据读入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="callback">可选的异步回调，在完成读取时调用。</param>
    /// <param name="state">一个用户提供的对象，它将该特定的异步读取请求与其他请求区别开来。</param>
    /// <exception cref="T:System.IO.IOException">尝试的异步读取超过了流的结尾，或者发生了磁盘错误。</exception>
    /// <exception cref="T:System.ArgumentException">一个或多个参数无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <exception cref="T:System.NotSupportedException">当前 Stream 实现不支持读取操作。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return this.BeginReadInternal(buffer, offset, count, callback, state, false);
    }

    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
    {
      if (!this.CanRead)
        __Error.ReadNotSupported();
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return this.BlockingBeginRead(buffer, offset, count, callback, state);
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task asyncWaiter = (Task) null;
      if (serializeAsynchronously)
        asyncWaiter = semaphoreSlim.WaitAsync();
      else
        semaphoreSlim.Wait();
      Stream.ReadWriteTask readWriteTask1 = new Stream.ReadWriteTask(true, (Func<object, int>) (param0 =>
      {
        Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
        int num = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
        readWriteTask2.ClearBeginState();
        return num;
      }), state, this, buffer, offset, count, callback);
      if (asyncWaiter != null)
        this.RunReadWriteTaskWhenReady(asyncWaiter, readWriteTask1);
      else
        this.RunReadWriteTask(readWriteTask1);
      return (IAsyncResult) readWriteTask1;
    }

    /// <summary>等待挂起的异步读取完成。（考虑使用 <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <returns>从流中读取的字节数，介于零 (0) 和所请求的字节数之间。流仅在流结尾返回零 (0)，否则在至少有 1 个字节可用之前应一直进行阻止。</returns>
    /// <param name="asyncResult">对要完成的挂起异步请求的引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">处于挂起状态的读取操作的句柄不可用。- 或 -悬挂操作不支持读取。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="asyncResult" /> 并非源自当前流上的 <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 方法。</exception>
    /// <exception cref="T:System.IO.IOException">此流关闭或发生内部错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int EndRead(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return Stream.BlockingEndRead(asyncResult);
      Stream.ReadWriteTask readWriteTask = this._activeReadWriteTask;
      if (readWriteTask == null)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
      if (readWriteTask != asyncResult)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
      if (!readWriteTask._isRead)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
      try
      {
        return readWriteTask.GetAwaiter().GetResult();
      }
      finally
      {
        this._activeReadWriteTask = (Stream.ReadWriteTask) null;
        this._asyncActiveSemaphore.Release();
      }
    }

    /// <summary>从当前流异步读取字节序列，并将流中的位置提升读取的字节数。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可小于所请求的字节数；如果已到达流结尾时，则为 0（零）。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
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
    public Task<int> ReadAsync(byte[] buffer, int offset, int count)
    {
      return this.ReadAsync(buffer, offset, count, CancellationToken.None);
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
    public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        return this.BeginEndReadAsync(buffer, offset, count);
      return Task.FromCancellation<int>(cancellationToken);
    }

    private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
    {
      Stream.ReadWriteParameters args1 = new Stream.ReadWriteParameters() { Buffer = buffer, Offset = offset, Count = count };
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> func = (Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult>) ((stream, args, callback, state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state));
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> beginMethod;
      return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, args1, beginMethod, (Func<Stream, IAsyncResult, int>) ((stream, asyncResult) => stream.EndRead(asyncResult)));
    }

    /// <summary>开始异步写操作。（考虑使用 <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <returns>表示异步写入的 IAsyncResult（可能仍处于挂起状态）。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从此处开始写入。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="callback">可选的异步回调，在完成写入时调用。</param>
    /// <param name="state">一个用户提供的对象，它将该特定的异步写入请求与其他请求区别开来。</param>
    /// <exception cref="T:System.IO.IOException">尝试进行的异步写入超过了流的结尾，或者发生了磁盘错误。</exception>
    /// <exception cref="T:System.ArgumentException">一个或多个参数无效。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <exception cref="T:System.NotSupportedException">当前 Stream 实现不支持写入操作。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return this.BeginWriteInternal(buffer, offset, count, callback, state, false);
    }

    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
    {
      if (!this.CanWrite)
        __Error.WriteNotSupported();
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        return this.BlockingBeginWrite(buffer, offset, count, callback, state);
      SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
      Task asyncWaiter = (Task) null;
      if (serializeAsynchronously)
        asyncWaiter = semaphoreSlim.WaitAsync();
      else
        semaphoreSlim.Wait();
      Stream.ReadWriteTask readWriteTask1 = new Stream.ReadWriteTask(false, (Func<object, int>) (param0 =>
      {
        Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
        readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
        readWriteTask2.ClearBeginState();
        return 0;
      }), state, this, buffer, offset, count, callback);
      if (asyncWaiter != null)
        this.RunReadWriteTaskWhenReady(asyncWaiter, readWriteTask1);
      else
        this.RunReadWriteTask(readWriteTask1);
      return (IAsyncResult) readWriteTask1;
    }

    private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
    {
      if (asyncWaiter.IsCompleted)
        this.RunReadWriteTask(readWriteTask);
      else
        asyncWaiter.ContinueWith((Action<Task, object>) ((t, state) =>
        {
          Tuple<Stream, Stream.ReadWriteTask> tuple = (Tuple<Stream, Stream.ReadWriteTask>) state;
          tuple.Item1.RunReadWriteTask(tuple.Item2);
        }), (object) Tuple.Create<Stream, Stream.ReadWriteTask>(this, readWriteTask), new CancellationToken(), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
    {
      this._activeReadWriteTask = readWriteTask;
      readWriteTask.m_taskScheduler = TaskScheduler.Default;
      readWriteTask.ScheduleAndStart(false);
    }

    /// <summary>结束异步写操作。（考虑使用 <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> 进行替换；请参见“备注”部分。）</summary>
    /// <param name="asyncResult">对未完成的异步 I/O 请求的引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asyncResult" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">处于挂起状态的写入操作的句柄不可用。- 或 -悬挂操作不支持写入。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="asyncResult" /> 并非源自当前流上的 <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> 方法。</exception>
    /// <exception cref="T:System.IO.IOException">此流关闭或发生内部错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual void EndWrite(IAsyncResult asyncResult)
    {
      if (asyncResult == null)
        throw new ArgumentNullException("asyncResult");
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        Stream.BlockingEndWrite(asyncResult);
      }
      else
      {
        Stream.ReadWriteTask readWriteTask = this._activeReadWriteTask;
        if (readWriteTask == null)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
        if (readWriteTask != asyncResult)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
        if (readWriteTask._isRead)
          throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
        try
        {
          readWriteTask.GetAwaiter().GetResult();
        }
        finally
        {
          this._activeReadWriteTask = (Stream.ReadWriteTask) null;
          this._asyncActiveSemaphore.Release();
        }
      }
    }

    /// <summary>将字节序列异步写入当前流，并将流的当前位置提升写入的字节数。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
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
    public Task WriteAsync(byte[] buffer, int offset, int count)
    {
      return this.WriteAsync(buffer, offset, count, CancellationToken.None);
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
    public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!cancellationToken.IsCancellationRequested)
        return this.BeginEndWriteAsync(buffer, offset, count);
      return Task.FromCancellation(cancellationToken);
    }

    private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
    {
      Stream.ReadWriteParameters args1 = new Stream.ReadWriteParameters() { Buffer = buffer, Offset = offset, Count = count };
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> func = (Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult>) ((stream, args, callback, state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state));
      Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> beginMethod;
      return (Task) TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, args1, beginMethod, (Func<Stream, IAsyncResult, VoidTaskResult>) ((stream, asyncResult) =>
      {
        stream.EndWrite(asyncResult);
        return new VoidTaskResult();
      }));
    }

    /// <summary>当在派生类中重写时，设置当前流中的位置。</summary>
    /// <returns>当前流中的新位置。</returns>
    /// <param name="offset">相对于 <paramref name="origin" /> 参数的字节偏移量。</param>
    /// <param name="origin">
    /// <see cref="T:System.IO.SeekOrigin" /> 类型的值，指示用于获取新位置的参考点。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持查找，例如在流通过管道或控制台输出构造的情况下即为如此。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract long Seek(long offset, SeekOrigin origin);

    /// <summary>当在派生类中重写时，设置当前流的长度。</summary>
    /// <param name="value">所需的当前流的长度（以字节表示）。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入和查找，例如在流通过管道或控制台输出构造的情况下即为如此。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract void SetLength(long value);

    /// <summary>当在派生类中重写时，从当前流读取字节序列，并将此流中的位置提升读取的字节数。</summary>
    /// <returns>读入缓冲区中的总字节数。如果很多字节当前不可用，则总字节数可能小于请求的字节数；如果已到达流结尾，则为零 (0)。</returns>
    /// <param name="buffer">字节数组。此方法返回时，该缓冲区包含指定的字符数组，该数组的 <paramref name="offset" /> 和 (<paramref name="offset" /> + <paramref name="count" /> -1) 之间的值由从当前源中读取的字节替换。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始存储从当前流中读取的数据。</param>
    /// <param name="count">要从当前流中最多读取的字节数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int Read([In, Out] byte[] buffer, int offset, int count);

    /// <summary>从流中读取一个字节，并将流内的位置向前提升一个字节，或者如果已到达流结尾，则返回 -1。</summary>
    /// <returns>强制转换为 Int32 的无符号字节，如果到达流的末尾，则为 -1。</returns>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int ReadByte()
    {
      byte[] buffer = new byte[1];
      if (this.Read(buffer, 0, 1) == 0)
        return -1;
      return (int) buffer[0];
    }

    /// <summary>当在派生类中重写时，向当前流中写入字节序列，并将此流中的当前位置提升写入的字节数。</summary>
    /// <param name="buffer">字节数组。此方法将 <paramref name="count" /> 个字节从 <paramref name="buffer" /> 复制到当前流。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节复制到当前流。</param>
    /// <param name="count">要写入当前流的字节数。</param>
    /// <exception cref="T:System.ArgumentException">总和 <paramref name="offset" /> 和 <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" />  是 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">将出现 I/O 错误，如找不到指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="M:System.IO.Stream.Write(System.Byte[],System.Int32,System.Int32)" /> 流关闭后调用。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract void Write(byte[] buffer, int offset, int count);

    /// <summary>将一个字节写入流内的当前位置，并将流内的位置向前提升一个字节。</summary>
    /// <param name="value">要写入流中的字节。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">该流不支持写入，或者该流已关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">在流关闭后调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteByte(byte value)
    {
      this.Write(new byte[1]{ value }, 0, 1);
    }

    /// <summary>在指定的 <see cref="T:System.IO.Stream" /> 对象周围创建线程安全（同步）包装。</summary>
    /// <returns>一个线程安全的 <see cref="T:System.IO.Stream" /> 对象。</returns>
    /// <param name="stream">要同步的 <see cref="T:System.IO.Stream" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Stream Synchronized(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      if (stream is Stream.SyncStream)
        return stream;
      return (Stream) new Stream.SyncStream(stream);
    }

    /// <summary>提供对 <see cref="T:System.Diagnostics.Contracts.Contract" /> 的支持。</summary>
    [Obsolete("Do not call or override this method.")]
    protected virtual void ObjectInvariant()
    {
    }

    internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      Stream.SynchronousAsyncResult synchronousAsyncResult;
      try
      {
        synchronousAsyncResult = new Stream.SynchronousAsyncResult(this.Read(buffer, offset, count), state);
      }
      catch (IOException ex)
      {
        object asyncStateObject = state;
        int num = 0;
        synchronousAsyncResult = new Stream.SynchronousAsyncResult((Exception) ex, asyncStateObject, num != 0);
      }
      if (callback != null)
        callback((IAsyncResult) synchronousAsyncResult);
      return (IAsyncResult) synchronousAsyncResult;
    }

    internal static int BlockingEndRead(IAsyncResult asyncResult)
    {
      return Stream.SynchronousAsyncResult.EndRead(asyncResult);
    }

    internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      Stream.SynchronousAsyncResult synchronousAsyncResult;
      try
      {
        this.Write(buffer, offset, count);
        synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
      }
      catch (IOException ex)
      {
        object asyncStateObject = state;
        int num = 1;
        synchronousAsyncResult = new Stream.SynchronousAsyncResult((Exception) ex, asyncStateObject, num != 0);
      }
      if (callback != null)
        callback((IAsyncResult) synchronousAsyncResult);
      return (IAsyncResult) synchronousAsyncResult;
    }

    internal static void BlockingEndWrite(IAsyncResult asyncResult)
    {
      Stream.SynchronousAsyncResult.EndWrite(asyncResult);
    }

    private struct ReadWriteParameters
    {
      internal byte[] Buffer;
      internal int Offset;
      internal int Count;
    }

    private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
    {
      internal readonly bool _isRead;
      internal Stream _stream;
      internal byte[] _buffer;
      internal int _offset;
      internal int _count;
      private AsyncCallback _callback;
      private ExecutionContext _context;
      [SecurityCritical]
      private static ContextCallback s_invokeAsyncCallback;

      [SecuritySafeCritical]
      [MethodImpl(MethodImplOptions.NoInlining)]
      public ReadWriteTask(bool isRead, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback)
        : base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        this._isRead = isRead;
        this._stream = stream;
        this._buffer = buffer;
        this._offset = offset;
        this._count = count;
        if (callback == null)
          return;
        this._callback = callback;
        this._context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
        this.AddCompletionAction((ITaskCompletionAction) this);
      }

      internal void ClearBeginState()
      {
        this._stream = (Stream) null;
        this._buffer = (byte[]) null;
      }

      [SecurityCritical]
      private static void InvokeAsyncCallback(object completedTask)
      {
        Stream.ReadWriteTask readWriteTask1 = (Stream.ReadWriteTask) completedTask;
        AsyncCallback asyncCallback = readWriteTask1._callback;
        readWriteTask1._callback = (AsyncCallback) null;
        Stream.ReadWriteTask readWriteTask2 = readWriteTask1;
        asyncCallback((IAsyncResult) readWriteTask2);
      }

      [SecuritySafeCritical]
      void ITaskCompletionAction.Invoke(Task completingTask)
      {
        ExecutionContext executionContext = this._context;
        if (executionContext == null)
        {
          AsyncCallback asyncCallback = this._callback;
          this._callback = (AsyncCallback) null;
          Task task = completingTask;
          asyncCallback((IAsyncResult) task);
        }
        else
        {
          this._context = (ExecutionContext) null;
          ContextCallback callback = Stream.ReadWriteTask.s_invokeAsyncCallback;
          if (callback == null)
            Stream.ReadWriteTask.s_invokeAsyncCallback = callback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback);
          using (executionContext)
            ExecutionContext.Run(executionContext, callback, (object) this, true);
        }
      }
    }

    [Serializable]
    private sealed class NullStream : Stream
    {
      private static Task<int> s_nullReadTask;

      public override bool CanRead
      {
        get
        {
          return true;
        }
      }

      public override bool CanWrite
      {
        get
        {
          return true;
        }
      }

      public override bool CanSeek
      {
        get
        {
          return true;
        }
      }

      public override long Length
      {
        get
        {
          return 0;
        }
      }

      public override long Position
      {
        get
        {
          return 0;
        }
        set
        {
        }
      }

      internal NullStream()
      {
      }

      protected override void Dispose(bool disposing)
      {
      }

      public override void Flush()
      {
      }

      [ComVisible(false)]
      public override Task FlushAsync(CancellationToken cancellationToken)
      {
        if (!cancellationToken.IsCancellationRequested)
          return Task.CompletedTask;
        return Task.FromCancellation(cancellationToken);
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this.CanRead)
          __Error.ReadNotSupported();
        return this.BlockingBeginRead(buffer, offset, count, callback, state);
      }

      public override int EndRead(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException("asyncResult");
        return Stream.BlockingEndRead(asyncResult);
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this.CanWrite)
          __Error.WriteNotSupported();
        return this.BlockingBeginWrite(buffer, offset, count, callback, state);
      }

      public override void EndWrite(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException("asyncResult");
        Stream.BlockingEndWrite(asyncResult);
      }

      public override int Read([In, Out] byte[] buffer, int offset, int count)
      {
        return 0;
      }

      [ComVisible(false)]
      public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
      {
        Task<int> task = Stream.NullStream.s_nullReadTask;
        if (task == null)
          Stream.NullStream.s_nullReadTask = task = new Task<int>(false, 0, (TaskCreationOptions) 16384, CancellationToken.None);
        return task;
      }

      public override int ReadByte()
      {
        return -1;
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
      }

      [ComVisible(false)]
      public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
      {
        if (!cancellationToken.IsCancellationRequested)
          return Task.CompletedTask;
        return Task.FromCancellation(cancellationToken);
      }

      public override void WriteByte(byte value)
      {
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
        return 0;
      }

      public override void SetLength(long length)
      {
      }
    }

    internal sealed class SynchronousAsyncResult : IAsyncResult
    {
      private readonly object _stateObject;
      private readonly bool _isWrite;
      private ManualResetEvent _waitHandle;
      private ExceptionDispatchInfo _exceptionInfo;
      private bool _endXxxCalled;
      private int _bytesRead;

      public bool IsCompleted
      {
        get
        {
          return true;
        }
      }

      public WaitHandle AsyncWaitHandle
      {
        get
        {
          return (WaitHandle) LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, (Func<ManualResetEvent>) (() => new ManualResetEvent(true)));
        }
      }

      public object AsyncState
      {
        get
        {
          return this._stateObject;
        }
      }

      public bool CompletedSynchronously
      {
        get
        {
          return true;
        }
      }

      internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
      {
        this._bytesRead = bytesRead;
        this._stateObject = asyncStateObject;
      }

      internal SynchronousAsyncResult(object asyncStateObject)
      {
        this._stateObject = asyncStateObject;
        this._isWrite = true;
      }

      internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
      {
        this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
        this._stateObject = asyncStateObject;
        this._isWrite = isWrite;
      }

      internal void ThrowIfError()
      {
        if (this._exceptionInfo == null)
          return;
        this._exceptionInfo.Throw();
      }

      internal static int EndRead(IAsyncResult asyncResult)
      {
        Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
        if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
          __Error.WrongAsyncResult();
        if (synchronousAsyncResult._endXxxCalled)
          __Error.EndReadCalledTwice();
        synchronousAsyncResult._endXxxCalled = true;
        synchronousAsyncResult.ThrowIfError();
        return synchronousAsyncResult._bytesRead;
      }

      internal static void EndWrite(IAsyncResult asyncResult)
      {
        Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
        if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
          __Error.WrongAsyncResult();
        if (synchronousAsyncResult._endXxxCalled)
          __Error.EndWriteCalledTwice();
        synchronousAsyncResult._endXxxCalled = true;
        synchronousAsyncResult.ThrowIfError();
      }
    }

    [Serializable]
    internal sealed class SyncStream : Stream, IDisposable
    {
      private Stream _stream;
      [NonSerialized]
      private bool? _overridesBeginRead;
      [NonSerialized]
      private bool? _overridesBeginWrite;

      public override bool CanRead
      {
        get
        {
          return this._stream.CanRead;
        }
      }

      public override bool CanWrite
      {
        get
        {
          return this._stream.CanWrite;
        }
      }

      public override bool CanSeek
      {
        get
        {
          return this._stream.CanSeek;
        }
      }

      [ComVisible(false)]
      public override bool CanTimeout
      {
        get
        {
          return this._stream.CanTimeout;
        }
      }

      public override long Length
      {
        get
        {
          lock (this._stream)
            return this._stream.Length;
        }
      }

      public override long Position
      {
        get
        {
          lock (this._stream)
            return this._stream.Position;
        }
        set
        {
          lock (this._stream)
            this._stream.Position = value;
        }
      }

      [ComVisible(false)]
      public override int ReadTimeout
      {
        get
        {
          return this._stream.ReadTimeout;
        }
        set
        {
          this._stream.ReadTimeout = value;
        }
      }

      [ComVisible(false)]
      public override int WriteTimeout
      {
        get
        {
          return this._stream.WriteTimeout;
        }
        set
        {
          this._stream.WriteTimeout = value;
        }
      }

      internal SyncStream(Stream stream)
      {
        if (stream == null)
          throw new ArgumentNullException("stream");
        this._stream = stream;
      }

      public override void Close()
      {
        lock (this._stream)
        {
          try
          {
            this._stream.Close();
          }
          finally
          {
            base.Dispose(true);
          }
        }
      }

      protected override void Dispose(bool disposing)
      {
        lock (this._stream)
        {
          try
          {
            if (!disposing)
              return;
            this._stream.Dispose();
          }
          finally
          {
            base.Dispose(disposing);
          }
        }
      }

      public override void Flush()
      {
        lock (this._stream)
          this._stream.Flush();
      }

      public override int Read([In, Out] byte[] bytes, int offset, int count)
      {
        lock (this._stream)
          return this._stream.Read(bytes, offset, count);
      }

      public override int ReadByte()
      {
        lock (this._stream)
          return this._stream.ReadByte();
      }

      private static bool OverridesBeginMethod(Stream stream, string methodName)
      {
        foreach (MethodInfo method in stream.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
        {
          if (method.DeclaringType == typeof (Stream) && method.Name == methodName)
            return false;
        }
        return true;
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this._overridesBeginRead.HasValue)
          this._overridesBeginRead = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginRead"));
        lock (this._stream)
          return this._overridesBeginRead.Value ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true);
      }

      public override int EndRead(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException("asyncResult");
        lock (this._stream)
          return this._stream.EndRead(asyncResult);
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
        lock (this._stream)
          return this._stream.Seek(offset, origin);
      }

      public override void SetLength(long length)
      {
        lock (this._stream)
          this._stream.SetLength(length);
      }

      public override void Write(byte[] bytes, int offset, int count)
      {
        lock (this._stream)
          this._stream.Write(bytes, offset, count);
      }

      public override void WriteByte(byte b)
      {
        lock (this._stream)
          this._stream.WriteByte(b);
      }

      [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
      public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
      {
        if (!this._overridesBeginWrite.HasValue)
          this._overridesBeginWrite = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginWrite"));
        lock (this._stream)
          return this._overridesBeginWrite.Value ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true);
      }

      public override void EndWrite(IAsyncResult asyncResult)
      {
        if (asyncResult == null)
          throw new ArgumentNullException("asyncResult");
        lock (this._stream)
          this._stream.EndWrite(asyncResult);
      }
    }
  }
}
