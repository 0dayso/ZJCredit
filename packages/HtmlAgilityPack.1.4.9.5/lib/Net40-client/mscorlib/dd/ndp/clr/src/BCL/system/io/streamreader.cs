// Decompiled with JetBrains decompiler
// Type: System.IO.StreamReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>实现一个 <see cref="T:System.IO.TextReader" />，使其以一种特定的编码从字节流中读取字符。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StreamReader : TextReader
  {
    /// <summary>空流周围的 <see cref="T:System.IO.StreamReader" />。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly StreamReader Null = (StreamReader) new StreamReader.NullStreamReader();
    private const int DefaultFileStreamBufferSize = 4096;
    private const int MinBufferSize = 128;
    private Stream stream;
    private Encoding encoding;
    private Decoder decoder;
    private byte[] byteBuffer;
    private char[] charBuffer;
    private byte[] _preamble;
    private int charPos;
    private int charLen;
    private int byteLen;
    private int bytePos;
    private int _maxCharsPerBuffer;
    private bool _detectEncoding;
    private bool _checkPreamble;
    private bool _isBlocked;
    private bool _closable;
    [NonSerialized]
    private volatile Task _asyncReadTask;

    internal static int DefaultBufferSize
    {
      get
      {
        return 1024;
      }
    }

    /// <summary>获取当前 <see cref="T:System.IO.StreamReader" /> 对象正在使用的当前字符编码。</summary>
    /// <returns>当前读取器所使用的当前字符编码。第一次调用 <see cref="T:System.IO.StreamReader" /> 的任何 <see cref="Overload:System.IO.StreamReader.Read" /> 方法后，该值可能会不同，因为直到第一次调用 <see cref="Overload:System.IO.StreamReader.Read" /> 方法时，才会进行编码的自动检测。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Encoding CurrentEncoding
    {
      [__DynamicallyInvokable] get
      {
        return this.encoding;
      }
    }

    /// <summary>返回基础流。</summary>
    /// <returns>基础流。</returns>
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
        return !this._closable;
      }
    }

    /// <summary>获取一个值，该值指示当前的流位置是否在流结尾。</summary>
    /// <returns>如果当前流位置位于流的末尾，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">基础流已释放。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool EndOfStream
    {
      [__DynamicallyInvokable] get
      {
        if (this.stream == null)
          __Error.ReaderClosed();
        this.CheckAsyncTaskInProgress();
        if (this.charPos < this.charLen)
          return false;
        return this.ReadBuffer() == 0;
      }
    }

    private int CharLen_Prop
    {
      get
      {
        return this.charLen;
      }
      set
      {
        this.charLen = value;
      }
    }

    private int CharPos_Prop
    {
      get
      {
        return this.charPos;
      }
      set
      {
        this.charPos = value;
      }
    }

    private int ByteLen_Prop
    {
      get
      {
        return this.byteLen;
      }
      set
      {
        this.byteLen = value;
      }
    }

    private int BytePos_Prop
    {
      get
      {
        return this.bytePos;
      }
      set
      {
        this.bytePos = value;
      }
    }

    private byte[] Preamble_Prop
    {
      get
      {
        return this._preamble;
      }
    }

    private bool CheckPreamble_Prop
    {
      get
      {
        return this._checkPreamble;
      }
    }

    private Decoder Decoder_Prop
    {
      get
      {
        return this.decoder;
      }
    }

    private bool DetectEncoding_Prop
    {
      get
      {
        return this._detectEncoding;
      }
    }

    private char[] CharBuffer_Prop
    {
      get
      {
        return this.charBuffer;
      }
    }

    private byte[] ByteBuffer_Prop
    {
      get
      {
        return this.byteBuffer;
      }
    }

    private bool IsBlocked_Prop
    {
      get
      {
        return this._isBlocked;
      }
      set
      {
        this._isBlocked = value;
      }
    }

    private Stream Stream_Prop
    {
      get
      {
        return this.stream;
      }
    }

    private int MaxCharsPerBuffer_Prop
    {
      get
      {
        return this._maxCharsPerBuffer;
      }
    }

    internal StreamReader()
    {
    }

    /// <summary>为指定的流初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例。</summary>
    /// <param name="stream">要读取的流。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不支持读取。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream)
      : this(stream, true)
    {
    }

    /// <summary>用指定的字节顺序标记检测选项，为指定的流初始化 <see cref="T:System.IO.StreamReader" /> 类的一个新实例。</summary>
    /// <param name="stream">要读取的流。</param>
    /// <param name="detectEncodingFromByteOrderMarks">指示是否在文件头查找字节顺序标记。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不支持读取。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
      : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
    {
    }

    /// <summary>用指定的字符编码为指定的流初始化 <see cref="T:System.IO.StreamReader" /> 类的一个新实例。</summary>
    /// <param name="stream">要读取的流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不支持读取。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 或 <paramref name="encoding" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding)
      : this(stream, encoding, true, StreamReader.DefaultBufferSize, false)
    {
    }

    /// <summary>为指定的流初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例，带有指定的字符编码和字节顺序标记检测选项。</summary>
    /// <param name="stream">要读取的流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="detectEncodingFromByteOrderMarks">指示是否在文件头查找字节顺序标记。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不支持读取。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 或 <paramref name="encoding" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
      : this(stream, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
    {
    }

    /// <summary>为指定的流初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例，带有指定的字符编码、字节顺序标记检测选项和缓冲区大小。</summary>
    /// <param name="stream">要读取的流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="detectEncodingFromByteOrderMarks">指示是否在文件头查找字节顺序标记。</param>
    /// <param name="bufferSize">最小缓冲区大小。</param>
    /// <exception cref="T:System.ArgumentException">流不支持读取。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 或 <paramref name="encoding" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bufferSize" /> 小于或等于零。</exception>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
      : this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
    {
    }

    /// <summary>为指定的流初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例，带有指定的字符编码、字节顺序标记检测选项和缓冲区大小，有选择性的打开流。</summary>
    /// <param name="stream">要读取的流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="detectEncodingFromByteOrderMarks">如果要在文件开头查找字节顺序标记，则为 true；否则为 false。</param>
    /// <param name="bufferSize">最小缓冲区大小。</param>
    /// <param name="leaveOpen">如果在释放 <see cref="T:System.IO.StreamReader" /> 对象后保持流处于打开状态，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
    {
      if (stream == null || encoding == null)
        throw new ArgumentNullException(stream == null ? "stream" : "encoding");
      if (!stream.CanRead)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
    }

    /// <summary>为指定的文件名初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例。</summary>
    /// <param name="path">要读取的完整文件路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 包括不正确或无效的文件名、目录名或卷标的语法。</exception>
    public StreamReader(string path)
      : this(path, true)
    {
    }

    /// <summary>为指定的文件名初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例，带有指定的字节顺序标记检测选项。</summary>
    /// <param name="path">要读取的完整文件路径。</param>
    /// <param name="detectEncodingFromByteOrderMarks">指示是否在文件头查找字节顺序标记。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 包括不正确或无效的文件名、目录名或卷标的语法。</exception>
    public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
      : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
    {
    }

    /// <summary>用指定的字符编码，为指定的文件名初始化 <see cref="T:System.IO.StreamReader" /> 类的一个新实例。</summary>
    /// <param name="path">要读取的完整文件路径。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="encoding" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包括不正确或无效的文件名、目录名或卷标的语法。</exception>
    public StreamReader(string path, Encoding encoding)
      : this(path, encoding, true, StreamReader.DefaultBufferSize)
    {
    }

    /// <summary>为指定的文件名初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例，带有指定的字符编码和字节顺序标记检测选项。</summary>
    /// <param name="path">要读取的完整文件路径。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="detectEncodingFromByteOrderMarks">指示是否在文件头查找字节顺序标记。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="encoding" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包括不正确或无效的文件名、目录名或卷标的语法。</exception>
    public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
      : this(path, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
    {
    }

    /// <summary>为指定的文件名初始化 <see cref="T:System.IO.StreamReader" /> 类的新实例，带有指定字符编码、字节顺序标记检测选项和缓冲区大小。</summary>
    /// <param name="path">要读取的完整文件路径。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="detectEncodingFromByteOrderMarks">指示是否在文件头查找字节顺序标记。</param>
    /// <param name="bufferSize">最小缓冲区大小（以 16 位字符的数目为单位）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="encoding" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包括不正确或无效的文件名、目录名或卷标的语法。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="buffersize" /> 小于或等于零。</exception>
    [SecuritySafeCritical]
    public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
      : this(path, encoding, detectEncodingFromByteOrderMarks, bufferSize, true)
    {
    }

    [SecurityCritical]
    internal StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool checkHost)
    {
      if (path == null || encoding == null)
        throw new ArgumentNullException(path == null ? "path" : "encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      this.Init((Stream) new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost), encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
    }

    private void CheckAsyncTaskInProgress()
    {
      Task task = this._asyncReadTask;
      if (task != null && !task.IsCompleted)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
    }

    private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
    {
      this.stream = stream;
      this.encoding = encoding;
      this.decoder = encoding.GetDecoder();
      if (bufferSize < 128)
        bufferSize = 128;
      this.byteBuffer = new byte[bufferSize];
      this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
      this.charBuffer = new char[this._maxCharsPerBuffer];
      this.byteLen = 0;
      this.bytePos = 0;
      this._detectEncoding = detectEncodingFromByteOrderMarks;
      this._preamble = encoding.GetPreamble();
      this._checkPreamble = (uint) this._preamble.Length > 0U;
      this._isBlocked = false;
      this._closable = !leaveOpen;
    }

    internal void Init(Stream stream)
    {
      this.stream = stream;
      this._closable = true;
    }

    /// <summary>关闭 <see cref="T:System.IO.StreamReader" /> 对象和基础流，并释放与读取器关联的所有系统资源。</summary>
    /// <filterpriority>1</filterpriority>
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>关闭基础流，释放 <see cref="T:System.IO.StreamReader" /> 使用的未托管资源，同时还可以根据需要释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!(!this.LeaveOpen & disposing) || this.stream == null)
          return;
        this.stream.Close();
      }
      finally
      {
        if (!this.LeaveOpen && this.stream != null)
        {
          this.stream = (Stream) null;
          this.encoding = (Encoding) null;
          this.decoder = (Decoder) null;
          this.byteBuffer = (byte[]) null;
          this.charBuffer = (char[]) null;
          this.charPos = 0;
          this.charLen = 0;
          base.Dispose(disposing);
        }
      }
    }

    /// <summary>清除内部缓冲区。</summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void DiscardBufferedData()
    {
      this.CheckAsyncTaskInProgress();
      this.byteLen = 0;
      this.charLen = 0;
      this.charPos = 0;
      if (this.encoding != null)
        this.decoder = this.encoding.GetDecoder();
      this._isBlocked = false;
    }

    /// <summary>返回下一个可用字符，但不使用它。</summary>
    /// <returns>为表示下一个要读取的字符的整数，或者，如果没有要读取的字符或该流不支持查找，则为 -1。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int Peek()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen && (this._isBlocked || this.ReadBuffer() == 0))
        return -1;
      return (int) this.charBuffer[this.charPos];
    }

    /// <summary>读取输入流中的下一个字符并使该字符位置提升一个字符。</summary>
    /// <returns>输入流中表示为 <see cref="T:System.Int32" /> 对象的下一个字符。如果不再有可用的字符，则为 -1。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int Read()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen && this.ReadBuffer() == 0)
        return -1;
      int num = (int) this.charBuffer[this.charPos];
      this.charPos = this.charPos + 1;
      return num;
    }

    /// <summary>从指定的索引位置开始将来自当前流的指定的最多字符读到缓冲区。</summary>
    /// <returns>已读取的字符数，或者如果已到达流结尾并且未读取任何数据，则为 0。该数小于或等于 <paramref name="count" /> 参数，具体取决于流中是否有可用的数据。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index + count - 1" />) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">开始写入的 <paramref name="buffer" /> 的索引。</param>
    /// <param name="count">要读取的最大字符数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误，如流被关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int Read([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      int num1 = 0;
      bool readToUserBuffer = false;
      while (count > 0)
      {
        int num2 = this.charLen - this.charPos;
        if (num2 == 0)
          num2 = this.ReadBuffer(buffer, index + num1, count, out readToUserBuffer);
        if (num2 != 0)
        {
          if (num2 > count)
            num2 = count;
          if (!readToUserBuffer)
          {
            Buffer.InternalBlockCopy((Array) this.charBuffer, this.charPos * 2, (Array) buffer, (index + num1) * 2, num2 * 2);
            this.charPos = this.charPos + num2;
          }
          num1 += num2;
          count -= num2;
          if (this._isBlocked)
            break;
        }
        else
          break;
      }
      return num1;
    }

    /// <summary>读取来自流的当前位置到结尾的所有字符。</summary>
    /// <returns>字符串形式的流的其余部分（从当前位置到结尾）。如果当前位置位于流结尾，则返回空字符串 (“”)。</returns>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ReadToEnd()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      StringBuilder stringBuilder = new StringBuilder(this.charLen - this.charPos);
      do
      {
        stringBuilder.Append(this.charBuffer, this.charPos, this.charLen - this.charPos);
        this.charPos = this.charLen;
        this.ReadBuffer();
      }
      while (this.charLen > 0);
      return stringBuilder.ToString();
    }

    /// <summary>从当前流中读取指定的最大字符数并从指定的索引位置开始将该数据写入缓冲区。</summary>
    /// <returns>已读取的字符数。该数字将小于或等于 <paramref name="count" />，取决于是否所有的输入字符都已读取。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index + count - 1" />) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.StreamReader" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    [__DynamicallyInvokable]
    public override int ReadBlock([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      return base.ReadBlock(buffer, index, count);
    }

    private void CompressBuffer(int n)
    {
      Buffer.InternalBlockCopy((Array) this.byteBuffer, n, (Array) this.byteBuffer, 0, this.byteLen - n);
      this.byteLen = this.byteLen - n;
    }

    private void DetectEncoding()
    {
      if (this.byteLen < 2)
        return;
      this._detectEncoding = false;
      bool flag = false;
      if ((int) this.byteBuffer[0] == 254 && (int) this.byteBuffer[1] == (int) byte.MaxValue)
      {
        this.encoding = (Encoding) new UnicodeEncoding(true, true);
        this.CompressBuffer(2);
        flag = true;
      }
      else if ((int) this.byteBuffer[0] == (int) byte.MaxValue && (int) this.byteBuffer[1] == 254)
      {
        if (this.byteLen < 4 || (int) this.byteBuffer[2] != 0 || (int) this.byteBuffer[3] != 0)
        {
          this.encoding = (Encoding) new UnicodeEncoding(false, true);
          this.CompressBuffer(2);
          flag = true;
        }
        else
        {
          this.encoding = (Encoding) new UTF32Encoding(false, true);
          this.CompressBuffer(4);
          flag = true;
        }
      }
      else if (this.byteLen >= 3 && (int) this.byteBuffer[0] == 239 && ((int) this.byteBuffer[1] == 187 && (int) this.byteBuffer[2] == 191))
      {
        this.encoding = Encoding.UTF8;
        this.CompressBuffer(3);
        flag = true;
      }
      else if (this.byteLen >= 4 && (int) this.byteBuffer[0] == 0 && ((int) this.byteBuffer[1] == 0 && (int) this.byteBuffer[2] == 254) && (int) this.byteBuffer[3] == (int) byte.MaxValue)
      {
        this.encoding = (Encoding) new UTF32Encoding(true, true);
        this.CompressBuffer(4);
        flag = true;
      }
      else if (this.byteLen == 2)
        this._detectEncoding = true;
      if (!flag)
        return;
      this.decoder = this.encoding.GetDecoder();
      this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
      this.charBuffer = new char[this._maxCharsPerBuffer];
    }

    private bool IsPreamble()
    {
      if (!this._checkPreamble)
        return this._checkPreamble;
      int num1 = this.byteLen >= this._preamble.Length ? this._preamble.Length - this.bytePos : this.byteLen - this.bytePos;
      int num2 = 0;
      while (num2 < num1)
      {
        if ((int) this.byteBuffer[this.bytePos] != (int) this._preamble[this.bytePos])
        {
          this.bytePos = 0;
          this._checkPreamble = false;
          break;
        }
        ++num2;
        this.bytePos = this.bytePos + 1;
      }
      if (this._checkPreamble && this.bytePos == this._preamble.Length)
      {
        this.CompressBuffer(this._preamble.Length);
        this.bytePos = 0;
        this._checkPreamble = false;
        this._detectEncoding = false;
      }
      return this._checkPreamble;
    }

    internal virtual int ReadBuffer()
    {
      this.charLen = 0;
      this.charPos = 0;
      if (!this._checkPreamble)
        this.byteLen = 0;
      do
      {
        if (this._checkPreamble)
        {
          int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
          if (num == 0)
          {
            if (this.byteLen > 0)
            {
              this.charLen = this.charLen + this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
              this.bytePos = this.byteLen = 0;
            }
            return this.charLen;
          }
          this.byteLen = this.byteLen + num;
        }
        else
        {
          this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
          if (this.byteLen == 0)
            return this.charLen;
        }
        this._isBlocked = this.byteLen < this.byteBuffer.Length;
        if (!this.IsPreamble())
        {
          if (this._detectEncoding && this.byteLen >= 2)
            this.DetectEncoding();
          this.charLen = this.charLen + this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
        }
      }
      while (this.charLen == 0);
      return this.charLen;
    }

    private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
    {
      this.charLen = 0;
      this.charPos = 0;
      if (!this._checkPreamble)
        this.byteLen = 0;
      int charIndex = 0;
      readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
      do
      {
        if (this._checkPreamble)
        {
          int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
          if (num == 0)
          {
            if (this.byteLen > 0)
            {
              if (readToUserBuffer)
              {
                charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + charIndex);
                this.charLen = 0;
              }
              else
              {
                charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, charIndex);
                this.charLen = this.charLen + charIndex;
              }
            }
            return charIndex;
          }
          this.byteLen = this.byteLen + num;
        }
        else
        {
          this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
          if (this.byteLen == 0)
            break;
        }
        this._isBlocked = this.byteLen < this.byteBuffer.Length;
        if (!this.IsPreamble())
        {
          if (this._detectEncoding && this.byteLen >= 2)
          {
            this.DetectEncoding();
            readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
          }
          this.charPos = 0;
          if (readToUserBuffer)
          {
            charIndex += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + charIndex);
            this.charLen = 0;
          }
          else
          {
            charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, charIndex);
            this.charLen = this.charLen + charIndex;
          }
        }
      }
      while (charIndex == 0);
      this._isBlocked = this._isBlocked & charIndex < desiredChars;
      return charIndex;
    }

    /// <summary>从当前流中读取一行字符并将数据作为字符串返回。</summary>
    /// <returns>输入流中的下一行；如果到达了输入流的末尾，则为 null。</returns>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ReadLine()
    {
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      if (this.charPos == this.charLen && this.ReadBuffer() == 0)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      do
      {
        int index = this.charPos;
        do
        {
          char ch = this.charBuffer[index];
          switch (ch)
          {
            case '\r':
            case '\n':
              string str;
              if (stringBuilder != null)
              {
                stringBuilder.Append(this.charBuffer, this.charPos, index - this.charPos);
                str = stringBuilder.ToString();
              }
              else
                str = new string(this.charBuffer, this.charPos, index - this.charPos);
              this.charPos = index + 1;
              if ((int) ch == 13 && (this.charPos < this.charLen || this.ReadBuffer() > 0) && (int) this.charBuffer[this.charPos] == 10)
                this.charPos = this.charPos + 1;
              return str;
            default:
              ++index;
              continue;
          }
        }
        while (index < this.charLen);
        int charCount = this.charLen - this.charPos;
        if (stringBuilder == null)
          stringBuilder = new StringBuilder(charCount + 80);
        stringBuilder.Append(this.charBuffer, this.charPos, charCount);
      }
      while (this.ReadBuffer() > 0);
      return stringBuilder.ToString();
    }

    /// <summary>从当前流中异步读取一行字符并将数据作为字符串返回。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含来自流的下一行；如果读取了所有字符，则为 null。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">下一行中的字符数大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<string> ReadLineAsync()
    {
      if (this.GetType() != typeof (StreamReader))
        return base.ReadLineAsync();
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<string> task = this.ReadLineAsyncInternal();
      this._asyncReadTask = (Task) task;
      return task;
    }

    private async Task<string> ReadLineAsyncInternal()
    {
      bool flag1 = this.CharPos_Prop == this.CharLen_Prop;
      ConfiguredTaskAwaitable<int> configuredTaskAwaitable;
      if (flag1)
      {
        configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
        flag1 = await configuredTaskAwaitable == 0;
      }
      if (flag1)
        return (string) null;
      StringBuilder sb = (StringBuilder) null;
      do
      {
        char[] tmpCharBuffer = this.CharBuffer_Prop;
        int tmpCharLen = this.CharLen_Prop;
        int tmpCharPos = this.CharPos_Prop;
        int i = tmpCharPos;
        do
        {
          char ch = tmpCharBuffer[i];
          switch (ch)
          {
            case '\r':
            case '\n':
              string s;
              if (sb != null)
              {
                sb.Append(tmpCharBuffer, tmpCharPos, i - tmpCharPos);
                s = sb.ToString();
              }
              else
                s = new string(tmpCharBuffer, tmpCharPos, i - tmpCharPos);
              this.CharPos_Prop = tmpCharPos = i + 1;
              bool flag2 = (int) ch == 13;
              if (flag2)
              {
                bool flag3 = tmpCharPos < tmpCharLen;
                if (!flag3)
                {
                  configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
                  flag3 = await configuredTaskAwaitable > 0;
                }
                flag2 = flag3;
              }
              if (flag2)
              {
                tmpCharPos = this.CharPos_Prop;
                if ((int) this.CharBuffer_Prop[tmpCharPos] == 10)
                {
                  int num = tmpCharPos + 1;
                  tmpCharPos = num;
                  this.CharPos_Prop = num;
                }
              }
              return s;
            default:
              ++i;
              continue;
          }
        }
        while (i < tmpCharLen);
        i = tmpCharLen - tmpCharPos;
        if (sb == null)
          sb = new StringBuilder(i + 80);
        sb.Append(tmpCharBuffer, tmpCharPos, i);
        tmpCharBuffer = (char[]) null;
        configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
      }
      while (await configuredTaskAwaitable > 0);
      return sb.ToString();
    }

    /// <summary>异步读取来自流的当前位置到结尾的所有字符并将它们作为一个字符串返回。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数值包括带有从流的当前位置到结尾的字符的字符串。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">字符数大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<string> ReadToEndAsync()
    {
      if (this.GetType() != typeof (StreamReader))
        return base.ReadToEndAsync();
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<string> endAsyncInternal = this.ReadToEndAsyncInternal();
      this._asyncReadTask = (Task) endAsyncInternal;
      return endAsyncInternal;
    }

    private async Task<string> ReadToEndAsyncInternal()
    {
      StringBuilder sb = new StringBuilder(this.CharLen_Prop - this.CharPos_Prop);
      do
      {
        int charPosProp = this.CharPos_Prop;
        sb.Append(this.CharBuffer_Prop, charPosProp, this.CharLen_Prop - charPosProp);
        this.CharPos_Prop = this.CharLen_Prop;
        int num = await this.ReadBufferAsync().ConfigureAwait(false);
      }
      while (this.CharLen_Prop > 0);
      return sb.ToString();
    }

    /// <summary>从当前流中异步读取指定的最大字符，并且从指定的索引位置开始将该数据写入缓冲区。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可小于所请求的字节数；如果已到达流结尾时，则为 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。如果在将指定的字符数写入缓冲区之前已到达流结尾，则将返回当前方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamReader))
        return base.ReadAsync(buffer, index, count);
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<int> task = this.ReadAsyncInternal(buffer, index, count);
      this._asyncReadTask = (Task) task;
      return task;
    }

    internal override async Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
    {
      bool flag = this.CharPos_Prop == this.CharLen_Prop;
      ConfiguredTaskAwaitable<int> configuredTaskAwaitable;
      if (flag)
      {
        configuredTaskAwaitable = this.ReadBufferAsync().ConfigureAwait(false);
        flag = await configuredTaskAwaitable == 0;
      }
      if (flag)
        return 0;
      int charsRead = 0;
      bool readToUserBuffer = false;
      byte[] tmpByteBuffer = this.ByteBuffer_Prop;
      Stream tmpStream = this.Stream_Prop;
      while (count > 0)
      {
        int n = this.CharLen_Prop - this.CharPos_Prop;
        if (n == 0)
        {
          this.CharLen_Prop = 0;
          this.CharPos_Prop = 0;
          if (!this.CheckPreamble_Prop)
            this.ByteLen_Prop = 0;
          readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
          do
          {
            if (this.CheckPreamble_Prop)
            {
              int bytePosProp = this.BytePos_Prop;
              configuredTaskAwaitable = tmpStream.ReadAsync(tmpByteBuffer, bytePosProp, tmpByteBuffer.Length - bytePosProp).ConfigureAwait(false);
              int num = await configuredTaskAwaitable;
              if (num == 0)
              {
                if (this.ByteLen_Prop > 0)
                {
                  if (readToUserBuffer)
                  {
                    n = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
                    this.CharLen_Prop = 0;
                  }
                  else
                  {
                    n = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
                    this.CharLen_Prop = this.CharLen_Prop + n;
                  }
                }
                this.IsBlocked_Prop = true;
                break;
              }
              this.ByteLen_Prop = this.ByteLen_Prop + num;
            }
            else
            {
              configuredTaskAwaitable = tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
              this.ByteLen_Prop = await configuredTaskAwaitable;
              if (this.ByteLen_Prop == 0)
              {
                this.IsBlocked_Prop = true;
                break;
              }
            }
            this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
            if (!this.IsPreamble())
            {
              if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
              {
                this.DetectEncoding();
                readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
              }
              this.CharPos_Prop = 0;
              if (readToUserBuffer)
              {
                n += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
                this.CharLen_Prop = 0;
              }
              else
              {
                n = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
                this.CharLen_Prop = this.CharLen_Prop + n;
              }
            }
          }
          while (n == 0);
          if (n == 0)
            break;
        }
        if (n > count)
          n = count;
        if (!readToUserBuffer)
        {
          Buffer.InternalBlockCopy((Array) this.CharBuffer_Prop, this.CharPos_Prop * 2, (Array) buffer, (index + charsRead) * 2, n * 2);
          this.CharPos_Prop = this.CharPos_Prop + n;
        }
        charsRead += n;
        count -= n;
        if (this.IsBlocked_Prop)
          break;
      }
      return charsRead;
    }

    /// <summary>从当前流中异步读取指定的最大字符，并且从指定的索引位置开始将该数据写入缓冲区。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可小于所请求的字节数；如果已到达流结尾时，则为 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">要读取的最大字符数。如果在将指定的字符数写入到缓冲区之前就已经达到流结尾，则将返回此方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (StreamReader))
        return base.ReadBlockAsync(buffer, index, count);
      if (this.stream == null)
        __Error.ReaderClosed();
      this.CheckAsyncTaskInProgress();
      Task<int> task = base.ReadBlockAsync(buffer, index, count);
      this._asyncReadTask = (Task) task;
      return task;
    }

    private async Task<int> ReadBufferAsync()
    {
      this.CharLen_Prop = 0;
      this.CharPos_Prop = 0;
      byte[] tmpByteBuffer = this.ByteBuffer_Prop;
      Stream tmpStream = this.Stream_Prop;
      if (!this.CheckPreamble_Prop)
        this.ByteLen_Prop = 0;
      do
      {
        if (this.CheckPreamble_Prop)
        {
          int bytePosProp = this.BytePos_Prop;
          int num = await tmpStream.ReadAsync(tmpByteBuffer, bytePosProp, tmpByteBuffer.Length - bytePosProp).ConfigureAwait(false);
          if (num == 0)
          {
            if (this.ByteLen_Prop > 0)
            {
              this.CharLen_Prop = this.CharLen_Prop + this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
              this.BytePos_Prop = 0;
              this.ByteLen_Prop = 0;
            }
            return this.CharLen_Prop;
          }
          this.ByteLen_Prop = this.ByteLen_Prop + num;
        }
        else
        {
          this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
          if (this.ByteLen_Prop == 0)
            return this.CharLen_Prop;
        }
        this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
        if (!this.IsPreamble())
        {
          if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
            this.DetectEncoding();
          this.CharLen_Prop = this.CharLen_Prop + this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
        }
      }
      while (this.CharLen_Prop == 0);
      return this.CharLen_Prop;
    }

    private class NullStreamReader : StreamReader
    {
      public override Stream BaseStream
      {
        get
        {
          return Stream.Null;
        }
      }

      public override Encoding CurrentEncoding
      {
        get
        {
          return Encoding.Unicode;
        }
      }

      internal NullStreamReader()
      {
        this.Init(Stream.Null);
      }

      protected override void Dispose(bool disposing)
      {
      }

      public override int Peek()
      {
        return -1;
      }

      public override int Read()
      {
        return -1;
      }

      public override int Read(char[] buffer, int index, int count)
      {
        return 0;
      }

      public override string ReadLine()
      {
        return (string) null;
      }

      public override string ReadToEnd()
      {
        return string.Empty;
      }

      internal override int ReadBuffer()
      {
        return 0;
      }
    }
  }
}
