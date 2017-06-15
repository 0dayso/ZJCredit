// Decompiled with JetBrains decompiler
// Type: System.IO.StreamWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>实现一个 <see cref="T:System.IO.TextWriter" />，使其以一种特定的编码向流中写入字符。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StreamWriter : TextWriter
  {
    /// <summary>提供 StreamWriter，它不带任何可写入但无法从中读取的后备存储。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly StreamWriter Null = new StreamWriter(Stream.Null, (Encoding) new UTF8Encoding(false, true), 128, true);
    internal const int DefaultBufferSize = 1024;
    private const int DefaultFileStreamBufferSize = 4096;
    private const int MinBufferSize = 128;
    private const int DontCopyOnWriteLineThreshold = 512;
    private Stream stream;
    private Encoding encoding;
    private Encoder encoder;
    private byte[] byteBuffer;
    private char[] charBuffer;
    private int charPos;
    private int charLen;
    private bool autoFlush;
    private bool haveWrittenPreamble;
    private bool closable;
    [NonSerialized]
    private StreamWriter.MdaHelper mdaHelper;
    [NonSerialized]
    private volatile Task _asyncWriteTask;
    private static volatile Encoding _UTF8NoBOM;

    internal static Encoding UTF8NoBOM
    {
      [FriendAccessAllowed] get
      {
        if (StreamWriter._UTF8NoBOM == null)
        {
          UTF8Encoding utF8Encoding = new UTF8Encoding(false, true);
          Thread.MemoryBarrier();
          StreamWriter._UTF8NoBOM = (Encoding) utF8Encoding;
        }
        return StreamWriter._UTF8NoBOM;
      }
    }

    /// <summary>获取或设置一个值，该值指示 <see cref="T:System.IO.StreamWriter" /> 是否在每次调用 <see cref="M:System.IO.StreamWriter.Write(System.Char)" /> 之后，将其缓冲区刷新到基础流。</summary>
    /// <returns>强制 <see cref="T:System.IO.StreamWriter" /> 刷新其缓冲区时，为 true；否则，为 false。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool AutoFlush
    {
      [__DynamicallyInvokable] get
      {
        return this.autoFlush;
      }
      [__DynamicallyInvokable] set
      {
        this.CheckAsyncTaskInProgress();
        this.autoFlush = value;
        if (!value)
          return;
        this.Flush(true, false);
      }
    }

    /// <summary>获取同后备存储连接的基础流。</summary>
    /// <returns>此 StreamWriter 正在写入的基础流。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Stream BaseStream
    {
      [__DynamicallyInvokable] get
      {
        return this.stream;
      }
    }

    internal bool LeaveOpen
    {
      get
      {
        return !this.closable;
      }
    }

    internal bool HaveWrittenPreamble
    {
      set
      {
        this.haveWrittenPreamble = value;
      }
    }

    /// <summary>获取将输出写入到其中的 <see cref="T:System.Text.Encoding" />。</summary>
    /// <returns>在当前实例的构造函数中指定的 <see cref="T:System.Text.Encoding" />；或者如果未指定编码，则为 <see cref="T:System.Text.UTF8Encoding" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override Encoding Encoding
    {
      [__DynamicallyInvokable] get
      {
        return this.encoding;
      }
    }

    private int CharPos_Prop
    {
      set
      {
        this.charPos = value;
      }
    }

    private bool HaveWrittenPreamble_Prop
    {
      set
      {
        this.haveWrittenPreamble = value;
      }
    }

    internal StreamWriter()
      : base((IFormatProvider) null)
    {
    }

    /// <summary>用 UTF-8 编码及默认缓冲区大小，为指定的流初始化 <see cref="T:System.IO.StreamWriter" /> 类的一个新实例。</summary>
    /// <param name="stream">要写入的流。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可写。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream)
      : this(stream, StreamWriter.UTF8NoBOM, 1024, false)
    {
    }

    /// <summary>用指定的编码及默认缓冲区大小，为指定的流初始化 <see cref="T:System.IO.StreamWriter" /> 类的新实例。</summary>
    /// <param name="stream">要写入的流。</param>
    /// <param name="encoding">要使用的字符编码。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 或 <paramref name="encoding" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可写。</exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream, Encoding encoding)
      : this(stream, encoding, 1024, false)
    {
    }

    /// <summary>用指定的编码及缓冲区大小，为指定的流初始化 <see cref="T:System.IO.StreamWriter" /> 类的新实例。</summary>
    /// <param name="stream">要写入的流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="bufferSize">缓冲区大小（以字节为单位）。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 或 <paramref name="encoding" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可写。</exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
      : this(stream, encoding, bufferSize, false)
    {
    }

    /// <summary>用指定的编码及默认缓冲区大小，为指定的流初始化 <see cref="T:System.IO.StreamWriter" /> 类的新实例，有选择性的打开流。</summary>
    /// <param name="stream">要写入的流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="bufferSize">缓冲区大小（以字节为单位）。</param>
    /// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.StreamWriter" /> object is disposed; otherwise, false.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 或 <paramref name="encoding" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可写。</exception>
    [__DynamicallyInvokable]
    public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
      : base((IFormatProvider) null)
    {
      if (stream == null || encoding == null)
        throw new ArgumentNullException(stream == null ? "stream" : "encoding");
      if (!stream.CanWrite)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init(stream, encoding, bufferSize, leaveOpen);
    }

    /// <summary>用默认编码和缓冲区大小，为指定的文件初始化 <see cref="T:System.IO.StreamWriter" /> 类的一个新实例。</summary>
    /// <param name="path">要写入的完整文件路径。<paramref name="path" /> 可以是一个文件名。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。- 或 -<paramref name="path" />包含系统设备的名称（com1、com2 等等）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径不得超过 248 个字符，文件名不得超过 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 包含不正确或无效的文件名、目录名或卷标的语法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public StreamWriter(string path)
      : this(path, false, StreamWriter.UTF8NoBOM, 1024)
    {
    }

    /// <summary>用默认编码和缓冲区大小，为指定的文件初始化 <see cref="T:System.IO.StreamWriter" /> 类的一个新实例。如果该文件存在，则可以将其覆盖或向其追加。如果该文件不存在，此构造函数将创建一个新文件。</summary>
    /// <param name="path">要写入的完整文件路径。 </param>
    /// <param name="append">若要追加数据到该文件中，则为 true；若要覆盖该文件，则为 false。如果指定的文件不存在，该参数无效，且构造函数将创建一个新文件。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空。- 或 -<paramref name="path" />包含系统设备的名称（com1、com2 等等）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 包含不正确或无效的文件名、目录名或卷标的语法。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径不得超过 248 个字符，文件名不得超过 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public StreamWriter(string path, bool append)
      : this(path, append, StreamWriter.UTF8NoBOM, 1024)
    {
    }

    /// <summary>用指定的编码和默认缓冲区大小，为指定的文件初始化 <see cref="T:System.IO.StreamWriter" /> 类的一个新实例。如果该文件存在，则可以将其覆盖或向其追加。如果该文件不存在，此构造函数将创建一个新文件。</summary>
    /// <param name="path">要写入的完整文件路径。 </param>
    /// <param name="append">若要追加数据到该文件中，则为 true；若要覆盖该文件，则为 false。如果指定的文件不存在，该参数无效，且构造函数将创建一个新文件。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空。- 或 -<paramref name="path" />包含系统设备的名称（com1、com2 等等）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 包含不正确或无效的文件名、目录名或卷标的语法。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径不得超过 248 个字符，文件名不得超过 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public StreamWriter(string path, bool append, Encoding encoding)
      : this(path, append, encoding, 1024)
    {
    }

    /// <summary>使用指定编码和缓冲区大小，为指定路径上的指定文件初始化 <see cref="T:System.IO.StreamWriter" /> 类的新实例。如果该文件存在，则可以将其覆盖或向其追加。如果该文件不存在，此构造函数将创建一个新文件。</summary>
    /// <param name="path">要写入的完整文件路径。 </param>
    /// <param name="append">若要追加数据到该文件中，则为 true；若要覆盖该文件，则为 false。如果指定的文件不存在，该参数无效，且构造函数将创建一个新文件。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="bufferSize">缓冲区大小（以字节为单位）。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。- 或 -<paramref name="path" />包含系统设备的名称（com1、com2 等等）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="encoding" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 为负数。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 包含不正确或无效的文件名、目录名或卷标的语法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径不得超过 248 个字符，文件名不得超过 260 个字符。</exception>
    [SecuritySafeCritical]
    public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
      : this(path, append, encoding, bufferSize, true)
    {
    }

    [SecurityCritical]
    internal StreamWriter(string path, bool append, Encoding encoding, int bufferSize, bool checkHost)
      : base((IFormatProvider) null)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init(StreamWriter.CreateFile(path, append, checkHost), encoding, bufferSize, false);
    }

    private void CheckAsyncTaskInProgress()
    {
      Task task = this._asyncWriteTask;
      if (task != null && !task.IsCompleted)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
    }

    [SecuritySafeCritical]
    private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
    {
      this.stream = streamArg;
      this.encoding = encodingArg;
      this.encoder = this.encoding.GetEncoder();
      if (bufferSize < 128)
        bufferSize = 128;
      this.charBuffer = new char[bufferSize];
      this.byteBuffer = new byte[this.encoding.GetMaxByteCount(bufferSize)];
      this.charLen = bufferSize;
      if (this.stream.CanSeek && this.stream.Position > 0L)
        this.haveWrittenPreamble = true;
      this.closable = !shouldLeaveOpen;
      if (!Mda.StreamWriterBufferedDataLost.Enabled)
        return;
      string cs = (string) null;
      if (Mda.StreamWriterBufferedDataLost.CaptureAllocatedCallStack)
        cs = Environment.GetStackTrace((Exception) null, false);
      this.mdaHelper = new StreamWriter.MdaHelper(this, cs);
    }

    [SecurityCritical]
    private static Stream CreateFile(string path, bool append, bool checkHost)
    {
      FileMode mode = append ? FileMode.Append : FileMode.Create;
      return (Stream) new FileStream(path, mode, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
    }

    /// <summary>关闭当前的 StreamWriter 对象和基础流。</summary>
    /// <exception cref="T:System.Text.EncoderFallbackException">当前编码不支持显示半个 Unicode 代理项对。</exception>
    /// <filterpriority>1</filterpriority>
    public override void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.IO.StreamWriter" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    /// <exception cref="T:System.Text.EncoderFallbackException">当前编码不支持显示半个 Unicode 代理项对。</exception>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this.stream == null || !disposing && (!this.LeaveOpen || !(this.stream is __ConsoleStream)))
          return;
        this.CheckAsyncTaskInProgress();
        this.Flush(true, true);
        if (this.mdaHelper == null)
          return;
        GC.SuppressFinalize((object) this.mdaHelper);
      }
      finally
      {
        if (!this.LeaveOpen)
        {
          if (this.stream != null)
          {
            try
            {
              if (disposing)
                this.stream.Close();
            }
            finally
            {
              this.stream = (Stream) null;
              this.byteBuffer = (byte[]) null;
              this.charBuffer = (char[]) null;
              this.encoding = (Encoding) null;
              this.encoder = (Encoder) null;
              this.charLen = 0;
              base.Dispose(disposing);
            }
          }
        }
      }
    }

    /// <summary>清理当前写入器的所有缓冲区，并使所有缓冲数据写入基础流。</summary>
    /// <exception cref="T:System.ObjectDisposedException">当前编写器已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">当前编码不支持显示半个 Unicode 代理项对。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override void Flush()
    {
      this.CheckAsyncTaskInProgress();
      this.Flush(true, true);
    }

    private void Flush(bool flushStream, bool flushEncoder)
    {
      if (this.stream == null)
        __Error.WriterClosed();
      if (this.charPos == 0 && (!flushStream && !flushEncoder || CompatibilitySwitches.IsAppEarlierThanWindowsPhone8))
        return;
      if (!this.haveWrittenPreamble)
      {
        this.haveWrittenPreamble = true;
        byte[] preamble = this.encoding.GetPreamble();
        if (preamble.Length != 0)
          this.stream.Write(preamble, 0, preamble.Length);
      }
      int bytes = this.encoder.GetBytes(this.charBuffer, 0, this.charPos, this.byteBuffer, 0, flushEncoder);
      this.charPos = 0;
      if (bytes > 0)
        this.stream.Write(this.byteBuffer, 0, bytes);
      if (!flushStream)
        return;
      this.stream.Flush();
    }

    /// <summary>将字符写入流。</summary>
    /// <param name="value">要写入流中的字符。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。 </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且当前编写器已关闭。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且由于 <see cref="T:System.IO.StreamWriter" /> 位于基础固定大小流的结尾，缓冲区的内容无法写入该流。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(char value)
    {
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen)
        this.Flush(false, false);
      this.charBuffer[this.charPos] = value;
      this.charPos = this.charPos + 1;
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>将字符数组写入流。</summary>
    /// <param name="buffer">包含要写入的数据的字符数组。如果 <paramref name="buffer" /> 为 null，则不写入任何内容。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。 </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且当前编写器已关闭。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且由于 <see cref="T:System.IO.StreamWriter" /> 位于基础固定大小流的结尾，缓冲区的内容无法写入该流。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(char[] buffer)
    {
      if (buffer == null)
        return;
      this.CheckAsyncTaskInProgress();
      int num1 = 0;
      int length = buffer.Length;
      while (length > 0)
      {
        if (this.charPos == this.charLen)
          this.Flush(false, false);
        int num2 = this.charLen - this.charPos;
        if (num2 > length)
          num2 = length;
        Buffer.InternalBlockCopy((Array) buffer, num1 * 2, (Array) this.charBuffer, this.charPos * 2, num2 * 2);
        this.charPos = this.charPos + num2;
        num1 += num2;
        length -= num2;
      }
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>将字符的子数组写入流。</summary>
    /// <param name="buffer">包含要写入的数据的字符数组。</param>
    /// <param name="index">在开始读取数据时缓冲区中的字符位置。</param>
    /// <param name="count">要写入的最大字符数。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。 </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且当前编写器已关闭。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且由于 <see cref="T:System.IO.StreamWriter" /> 位于基础固定大小流的结尾，缓冲区的内容无法写入该流。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this.CheckAsyncTaskInProgress();
      while (count > 0)
      {
        if (this.charPos == this.charLen)
          this.Flush(false, false);
        int num = this.charLen - this.charPos;
        if (num > count)
          num = count;
        Buffer.InternalBlockCopy((Array) buffer, index * 2, (Array) this.charBuffer, this.charPos * 2, num * 2);
        this.charPos = this.charPos + num;
        index += num;
        count -= num;
      }
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>将字符串写入流。</summary>
    /// <param name="value">要写入流的字符串。如果 <paramref name="value" /> 为 null，则不写入任何内容。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且当前编写器已关闭。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="P:System.IO.StreamWriter.AutoFlush" /> 为 true 或 <see cref="T:System.IO.StreamWriter" /> 缓冲区已满，并且由于 <see cref="T:System.IO.StreamWriter" /> 位于基础固定大小流的结尾，缓冲区的内容无法写入该流。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(string value)
    {
      if (value == null)
        return;
      this.CheckAsyncTaskInProgress();
      int length = value.Length;
      int sourceIndex = 0;
      while (length > 0)
      {
        if (this.charPos == this.charLen)
          this.Flush(false, false);
        int count = this.charLen - this.charPos;
        if (count > length)
          count = length;
        value.CopyTo(sourceIndex, this.charBuffer, this.charPos, count);
        this.charPos = this.charPos + count;
        sourceIndex += count;
        length -= count;
      }
      if (!this.autoFlush)
        return;
      this.Flush(true, false);
    }

    /// <summary>将字符异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <param name="value">要写入流中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteAsync(value);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
      this._asyncWriteTask = task;
      return task;
    }

    private static async Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      if (charPos == charLen)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      charBuffer[charPos] = value;
      ++charPos;
      if (appendNewLine)
      {
        for (int i = 0; i < coreNewLine.Length; ++i)
        {
          if (charPos == charLen)
          {
            configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
            await configuredTaskAwaitable;
            charPos = 0;
          }
          charBuffer[charPos] = coreNewLine[i];
          ++charPos;
        }
      }
      if (autoFlush)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      _this.CharPos_Prop = charPos;
    }

    /// <summary>将字符串异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <param name="value">要写入流的字符串。如果 <paramref name="value" /> 为 null，则不写入任何内容。</param>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(string value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteAsync(value);
      if (value == null)
        return Task.CompletedTask;
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
      this._asyncWriteTask = task;
      return task;
    }

    private static async Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
    {
      int count = value.Length;
      int index = 0;
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      while (count > 0)
      {
        if (charPos == charLen)
        {
          configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
          await configuredTaskAwaitable;
          charPos = 0;
        }
        int count1 = charLen - charPos;
        if (count1 > count)
          count1 = count;
        value.CopyTo(index, charBuffer, charPos, count1);
        charPos += count1;
        index += count1;
        count -= count1;
      }
      if (appendNewLine)
      {
        for (int i = 0; i < coreNewLine.Length; ++i)
        {
          if (charPos == charLen)
          {
            configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
            await configuredTaskAwaitable;
            charPos = 0;
          }
          charBuffer[charPos] = coreNewLine[i];
          ++charPos;
        }
      }
      if (autoFlush)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      _this.CharPos_Prop = charPos;
    }

    /// <summary>将字符的子数组异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <param name="buffer">包含要写入的数据的字符数组。</param>
    /// <param name="index">在开始读取数据时缓冲区中的字符位置。</param>
    /// <param name="count">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> plus <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteAsync(buffer, index, count);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
      this._asyncWriteTask = task;
      return task;
    }

    private static async Task WriteAsyncInternal(StreamWriter _this, char[] buffer, int index, int count, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      while (count > 0)
      {
        if (charPos == charLen)
        {
          configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
          await configuredTaskAwaitable;
          charPos = 0;
        }
        int num = charLen - charPos;
        if (num > count)
          num = count;
        Buffer.InternalBlockCopy((Array) buffer, index * 2, (Array) charBuffer, charPos * 2, num * 2);
        charPos += num;
        index += num;
        count -= num;
      }
      if (appendNewLine)
      {
        for (int i = 0; i < coreNewLine.Length; ++i)
        {
          if (charPos == charLen)
          {
            configuredTaskAwaitable = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
            await configuredTaskAwaitable;
            charPos = 0;
          }
          charBuffer[charPos] = coreNewLine[i];
          ++charPos;
        }
      }
      if (autoFlush)
      {
        configuredTaskAwaitable = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
        await configuredTaskAwaitable;
        charPos = 0;
      }
      _this.CharPos_Prop = charPos;
    }

    /// <summary>将行终止符异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync()
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync();
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, (char[]) null, 0, 0, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>将后跟行终止符的字符异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <param name="value">要写入流中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync(value);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>将后跟行终止符的字符串异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <param name="value">要写入的字符串。如果值为 null，则只写入行终止符。</param>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(string value)
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync(value);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>将后跟行终止符的字符的子数组异步写入该流。</summary>
    /// <returns>表示异步写操作的任务。</returns>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始读取数据时缓冲区中的字符位置。</param>
    /// <param name="count">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> plus <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">流编写器正在由其前一次写入操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamWriter))
        return base.WriteLineAsync(buffer, index, count);
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
      this._asyncWriteTask = task;
      return task;
    }

    /// <summary>异步清除此流的所有缓冲区并导致所有缓冲数据都写入基础设备中。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync()
    {
      if (this.GetType() != typeof (StreamWriter))
        return base.FlushAsync();
      if (this.stream == null)
        __Error.WriterClosed();
      this.CheckAsyncTaskInProgress();
      Task task = this.FlushAsyncInternal(true, true, this.charBuffer, this.charPos);
      this._asyncWriteTask = task;
      return task;
    }

    private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos)
    {
      if (sCharPos == 0 && !flushStream && !flushEncoder)
        return Task.CompletedTask;
      Task task = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this.haveWrittenPreamble, this.encoding, this.encoder, this.byteBuffer, this.stream);
      this.charPos = 0;
      return task;
    }

    private static async Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream)
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      if (!haveWrittenPreamble)
      {
        _this.HaveWrittenPreamble_Prop = true;
        byte[] preamble = encoding.GetPreamble();
        if (preamble.Length != 0)
        {
          configuredTaskAwaitable = stream.WriteAsync(preamble, 0, preamble.Length).ConfigureAwait(false);
          await configuredTaskAwaitable;
        }
      }
      int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
      if (bytes > 0)
      {
        configuredTaskAwaitable = stream.WriteAsync(byteBuffer, 0, bytes).ConfigureAwait(false);
        await configuredTaskAwaitable;
      }
      if (!flushStream)
        return;
      configuredTaskAwaitable = stream.FlushAsync().ConfigureAwait(false);
      await configuredTaskAwaitable;
    }

    private sealed class MdaHelper
    {
      private StreamWriter streamWriter;
      private string allocatedCallstack;

      internal MdaHelper(StreamWriter sw, string cs)
      {
        this.streamWriter = sw;
        this.allocatedCallstack = cs;
      }

      ~MdaHelper()
      {
        if (this.streamWriter.charPos == 0 || this.streamWriter.stream == null || this.streamWriter.stream == Stream.Null)
          return;
        Mda.StreamWriterBufferedDataLost.ReportError(Environment.GetResourceString("IO_StreamWriterBufferedDataLost", (object) this.streamWriter.stream.GetType().FullName, (object) (this.streamWriter.stream is FileStream ? ((FileStream) this.streamWriter.stream).NameInternal : "<unknown>"), (object) (this.allocatedCallstack ?? Environment.GetResourceString("IO_StreamWriterBufferedDataLostCaptureAllocatedFromCallstackNotEnabled"))));
      }
    }
  }
}
