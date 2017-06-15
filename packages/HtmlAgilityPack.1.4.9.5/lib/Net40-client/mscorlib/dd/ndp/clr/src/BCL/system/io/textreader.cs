// Decompiled with JetBrains decompiler
// Type: System.IO.TextReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>表示可读取有序字符系列的读取器。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class TextReader : MarshalByRefObject, IDisposable
  {
    [NonSerialized]
    private static Func<object, string> _ReadLineDelegate = new Func<object, string>(TextReader.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__22_0);
    [NonSerialized]
    private static Func<object, int> _ReadDelegate = new Func<object, int>(TextReader.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__22_1);
    /// <summary>提供一个无数据可供读取的 TextReader。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly TextReader Null = (TextReader) new TextReader.NullTextReader();

    /// <summary>初始化 <see cref="T:System.IO.TextReader" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected TextReader()
    {
    }

    /// <summary>关闭 <see cref="T:System.IO.TextReader" /> 并释放与该 TextReader 关联的所有系统资源。</summary>
    /// <filterpriority>1</filterpriority>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.IO.TextReader" /> 对象使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.IO.TextReader" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>读取下一个字符，而不更改读取器状态或字符源。返回下一个可用字符，而实际上并不从读取器中读取此字符。</summary>
    /// <returns>一个表示下一个要读取的字符的整数；如果没有更多可读取的字符或该读取器不支持查找，则为 -1。</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Peek()
    {
      return -1;
    }

    /// <summary>读取文本读取器中的下一个字符并使该字符的位置前移一个字符。</summary>
    /// <returns>文本读取器中的下一个字符，或为 -1（如果没有更多可用字符）。默认实现将返回 -1。</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Read()
    {
      return -1;
    }

    /// <summary>从当前读取器中读取指定数目的字符并从指定索引开始将该数据写入缓冲区。</summary>
    /// <returns>已读取的字符数。该数会小于或等于 <paramref name="count" />，具体取决于读取器中是否有可用的数据。如果调用此方法时没有留下更多的字符供读取，则此方法返回 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。如果在将指定数量的字符读入缓冲区之前就已达读取器的末尾，则返回该方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Read([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num1 = 0;
      do
      {
        int num2 = this.Read();
        if (num2 != -1)
          buffer[index + num1++] = (char) num2;
        else
          break;
      }
      while (num1 < count);
      return num1;
    }

    /// <summary>读取从当前位置到文本读取器末尾的所有字符并将它们作为一个字符串返回。</summary>
    /// <returns>一个包含从当前位置到文本读取器末尾的所有字符的字符串。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">下一行中的字符数大于 <see cref="F:System.Int32.MaxValue" /></exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual string ReadToEnd()
    {
      char[] buffer = new char[4096];
      StringBuilder stringBuilder = new StringBuilder(4096);
      int charCount;
      while ((charCount = this.Read(buffer, 0, buffer.Length)) != 0)
        stringBuilder.Append(buffer, 0, charCount);
      return stringBuilder.ToString();
    }

    /// <summary>从当前文本读取器中读取指定的最大字符数并从指定索引处开始将该数据写入缓冲区。</summary>
    /// <returns>已读取的字符数。该数字将小于或等于 <paramref name="count" />，具体取决于是否所有的输入字符都已读取。</returns>
    /// <param name="buffer">此方法返回时，此参数包含指定的字符数组，该数组中从 <paramref name="index" /> 到 (<paramref name="index" /> + <paramref name="count" /> -1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int ReadBlock([In, Out] char[] buffer, int index, int count)
    {
      int num1 = 0;
      int num2;
      do
      {
        num1 += num2 = this.Read(buffer, index + num1, count - num1);
      }
      while (num2 > 0 && num1 < count);
      return num1;
    }

    /// <summary>从文本读取器中读取一行字符并将数据作为字符串返回。</summary>
    /// <returns>读取器中的下一行，或为 null （如果已读取了所有字符）。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">下一行中的字符数大于 <see cref="F:System.Int32.MaxValue" /></exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual string ReadLine()
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num;
      while (true)
      {
        num = this.Read();
        switch (num)
        {
          case -1:
            goto label_6;
          case 13:
          case 10:
            goto label_2;
          default:
            stringBuilder.Append((char) num);
            continue;
        }
      }
label_2:
      if (num == 13 && this.Peek() == 10)
        this.Read();
      return stringBuilder.ToString();
label_6:
      if (stringBuilder.Length > 0)
        return stringBuilder.ToString();
      return (string) null;
    }

    /// <summary>异步读取一行字符并将数据作为字符串返回。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含来自文本读取器的下一行或为 null 如果读取所有字符。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">下一行中的字符数大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文本读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task<string> ReadLineAsync()
    {
      return Task<string>.Factory.StartNew(TextReader._ReadLineDelegate, (object) this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>异步读取从当前位置到文本读取器末尾的所有字符并将它们作为一个字符串返回。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数值包括字符串来自当前位置到结束文本读取器字符。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">字符数大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文本读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual async Task<string> ReadToEndAsync()
    {
      char[] chars = new char[4096];
      StringBuilder sb = new StringBuilder(4096);
      while (true)
      {
        int num = await this.ReadAsyncInternal(chars, 0, chars.Length).ConfigureAwait(false);
        int len;
        if ((len = num) != 0)
          sb.Append(chars, 0, len);
        else
          break;
      }
      return sb.ToString();
    }

    /// <summary>异步从当前文本读取器中读取指定最大字符数并从指定索引开始将该数据写入缓冲区。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可能小于所请求的字节数，或者如果已达到文本的末尾时，则为 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。如果在将指定数量的字符读入缓冲区之前已到达文本的末尾，则当前方法将返回。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文本读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return this.ReadAsyncInternal(buffer, index, count);
    }

    internal virtual Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
    {
      Tuple<TextReader, char[], int, int> tuple = new Tuple<TextReader, char[], int, int>(this, buffer, index, count);
      return Task<int>.Factory.StartNew(TextReader._ReadDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>异步从当前文本读取器中读取指定最大字符数并从指定索引开始将该数据写入缓冲区。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可能小于所请求的字节数，或者如果已达到文本的末尾时，则为 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。如果在将指定数量的字符读入缓冲区之前已到达文本的末尾，则当前方法将返回。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文本读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return this.ReadBlockAsyncInternal(buffer, index, count);
    }

    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    private async Task<int> ReadBlockAsyncInternal(char[] buffer, int index, int count)
    {
      int n = 0;
      int num;
      do
      {
        num = await this.ReadAsyncInternal(buffer, index + n, count - n).ConfigureAwait(false);
        n += num;
      }
      while (num > 0 && n < count);
      return n;
    }

    /// <summary>在指定 TextReader 周围创建线程安全包装。</summary>
    /// <returns>一个线程安全的 <see cref="T:System.IO.TextReader" />。</returns>
    /// <param name="reader">要同步的 TextReader。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="reader" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static TextReader Synchronized(TextReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      if (reader is TextReader.SyncTextReader)
        return reader;
      return (TextReader) new TextReader.SyncTextReader(reader);
    }

    [Serializable]
    private sealed class NullTextReader : TextReader
    {
      public override int Read(char[] buffer, int index, int count)
      {
        return 0;
      }

      public override string ReadLine()
      {
        return (string) null;
      }
    }

    [Serializable]
    internal sealed class SyncTextReader : TextReader
    {
      internal TextReader _in;

      internal SyncTextReader(TextReader t)
      {
        this._in = t;
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Close()
      {
        this._in.Close();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      protected override void Dispose(bool disposing)
      {
        if (!disposing)
          return;
        this._in.Dispose();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int Peek()
      {
        return this._in.Peek();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int Read()
      {
        return this._in.Read();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int Read([In, Out] char[] buffer, int index, int count)
      {
        return this._in.Read(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int ReadBlock([In, Out] char[] buffer, int index, int count)
      {
        return this._in.ReadBlock(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override string ReadLine()
      {
        return this._in.ReadLine();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override string ReadToEnd()
      {
        return this._in.ReadToEnd();
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task<string> ReadLineAsync()
      {
        return Task.FromResult<string>(this.ReadLine());
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task<string> ReadToEndAsync()
      {
        return Task.FromResult<string>(this.ReadToEnd());
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
      {
        if (buffer == null)
          throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (buffer.Length - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task<int> ReadAsync(char[] buffer, int index, int count)
      {
        if (buffer == null)
          throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (buffer.Length - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return Task.FromResult<int>(this.Read(buffer, index, count));
      }
    }
  }
}
