// Decompiled with JetBrains decompiler
// Type: System.IO.BinaryReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.IO
{
  /// <summary>用特定的编码将基元数据类型读作二进制值。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class BinaryReader : IDisposable
  {
    private const int MaxCharBytesSize = 128;
    private Stream m_stream;
    private byte[] m_buffer;
    private Decoder m_decoder;
    private byte[] m_charBytes;
    private char[] m_singleChar;
    private char[] m_charBuffer;
    private int m_maxCharsSize;
    private bool m_2BytesPerChar;
    private bool m_isMemoryStream;
    private bool m_leaveOpen;

    /// <summary>公开对 <see cref="T:System.IO.BinaryReader" /> 的基础流的访问。</summary>
    /// <returns>与 BinaryReader 关联的基础流。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Stream BaseStream
    {
      [__DynamicallyInvokable] get
      {
        return this.m_stream;
      }
    }

    /// <summary>基于所指定的流和特定的 UTF-8 编码，初始化 <see cref="T:System.IO.BinaryReader" /> 类的新实例。</summary>
    /// <param name="input">输入流。</param>
    /// <exception cref="T:System.ArgumentException">该流不支持读取，为null 或已关闭。</exception>
    [__DynamicallyInvokable]
    public BinaryReader(Stream input)
      : this(input, (Encoding) new UTF8Encoding(), false)
    {
    }

    /// <summary>基于所指定的流和特定的字符编码，初始化 <see cref="T:System.IO.BinaryReader" /> 类的新实例。</summary>
    /// <param name="input">输入流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">该流不支持读取，为null 或已关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="encoding" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public BinaryReader(Stream input, Encoding encoding)
      : this(input, encoding, false)
    {
    }

    /// <summary>基于所提供的流和特定的字符编码，初始化 <see cref="T:System.IO.BinaryReader" /> 类的新实例，有选择性的打开流。</summary>
    /// <param name="input">输入流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="leaveOpen">如果在释放 <see cref="T:System.IO.BinaryReader" /> 对象后保持流处于打开状态，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">该流不支持读取，为null 或已关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="encoding" /> 或 <paramref name="input" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public BinaryReader(Stream input, Encoding encoding, bool leaveOpen)
    {
      if (input == null)
        throw new ArgumentNullException("input");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (!input.CanRead)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
      this.m_stream = input;
      this.m_decoder = encoding.GetDecoder();
      this.m_maxCharsSize = encoding.GetMaxCharCount(128);
      int length = encoding.GetMaxByteCount(1);
      if (length < 16)
        length = 16;
      this.m_buffer = new byte[length];
      this.m_2BytesPerChar = encoding is UnicodeEncoding;
      this.m_isMemoryStream = this.m_stream.GetType() == typeof (MemoryStream);
      this.m_leaveOpen = leaveOpen;
    }

    /// <summary>关闭当前阅读器及基础流。</summary>
    /// <filterpriority>2</filterpriority>
    public virtual void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放 <see cref="T:System.IO.BinaryReader" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        Stream stream = this.m_stream;
        this.m_stream = (Stream) null;
        if (stream != null && !this.m_leaveOpen)
          stream.Close();
      }
      this.m_stream = (Stream) null;
      this.m_buffer = (byte[]) null;
      this.m_decoder = (Decoder) null;
      this.m_charBytes = (byte[]) null;
      this.m_singleChar = (char[]) null;
      this.m_charBuffer = (char[]) null;
    }

    /// <summary>释放 <see cref="T:System.IO.BinaryReader" /> 类的当前实例所使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>返回下一个可用的字符，并且不提升字节或字符的位置。</summary>
    /// <returns>下一个可用的字符，或者，如果没有可用字符或者流不支持查找时为 -1。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentException">当前字符无法通过使用为该流选择的 <see cref="T:System.Text.Encoding" /> 解码到内部字符缓冲区中。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int PeekChar()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (!this.m_stream.CanSeek)
        return -1;
      long position = this.m_stream.Position;
      int num = this.Read();
      this.m_stream.Position = position;
      return num;
    }

    /// <summary>从基础流中读取字符，并根据所使用的 Encoding 和从流中读取的特定字符，提升流的当前位置。</summary>
    /// <returns>输入流中的下一个字符，如果当前无可用字符则为 -1。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Read()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      return this.InternalReadOneChar();
    }

    /// <summary>从当前流中读取 Boolean 值，并使该流的当前位置提升 1 个字节。</summary>
    /// <returns>如果字节为非零，则为 true，否则为 false。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool ReadBoolean()
    {
      this.FillBuffer(1);
      return (uint) this.m_buffer[0] > 0U;
    }

    /// <summary>从当前流中读取下一个字节，并使流的当前位置提升 1 个字节。</summary>
    /// <returns>从当前流中读取的下一个字节。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte ReadByte()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      int num1 = this.m_stream.ReadByte();
      int num2 = -1;
      if (num1 == num2)
        __Error.EndOfFile();
      return (byte) num1;
    }

    /// <summary>从此流中读取 1 个有符号字节，并使流的当前位置提升 1 个字节。</summary>
    /// <returns>从当前流中读取的一个有符号字节。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual sbyte ReadSByte()
    {
      this.FillBuffer(1);
      return (sbyte) this.m_buffer[0];
    }

    /// <summary>从当前流中读取下一个字符，并根据所使用的 Encoding 和从流中读取的特定字符，提升流的当前位置。</summary>
    /// <returns>从当前流中读取的字符。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentException">读取了一个代理项字符。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual char ReadChar()
    {
      int num1 = this.Read();
      int num2 = -1;
      if (num1 == num2)
        __Error.EndOfFile();
      return (char) num1;
    }

    /// <summary>从当前流中读取 2 字节有符号整数，并使流的当前位置提升 2 个字节。</summary>
    /// <returns>从当前流中读取的 2 字节有符号整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual short ReadInt16()
    {
      this.FillBuffer(2);
      return (short) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8);
    }

    /// <summary>使用 Little-Endian 编码从当前流中读取 2 字节无符号整数，并将流的位置提升 2 个字节。</summary>
    /// <returns>从该流中读取的 2 字节无符号整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual ushort ReadUInt16()
    {
      this.FillBuffer(2);
      return (ushort) ((uint) this.m_buffer[0] | (uint) this.m_buffer[1] << 8);
    }

    /// <summary>从当前流中读取 4 字节有符号整数，并使流的当前位置提升 4 个字节。</summary>
    /// <returns>从当前流中读取的 2 字节有符号整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int ReadInt32()
    {
      if (this.m_isMemoryStream)
      {
        if (this.m_stream == null)
          __Error.FileNotOpen();
        return (this.m_stream as MemoryStream).InternalReadInt32();
      }
      this.FillBuffer(4);
      return (int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24;
    }

    /// <summary>从当前流中读取 4 字节无符号整数并使流的当前位置提升 4 个字节。</summary>
    /// <returns>从该流中读取的 4 字节无符号整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual uint ReadUInt32()
    {
      this.FillBuffer(4);
      return (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>从当前流中读取 8 字节有符号整数，并使流的当前位置提升 8 个字节。</summary>
    /// <returns>从当前流中读取的 8 字节有符号整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual long ReadInt64()
    {
      this.FillBuffer(8);
      return (long) (uint) ((int) this.m_buffer[4] | (int) this.m_buffer[5] << 8 | (int) this.m_buffer[6] << 16 | (int) this.m_buffer[7] << 24) << 32 | (long) (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>从当前流中读取 8 字节无符号整数并使流的当前位置提升 8 个字节。</summary>
    /// <returns>从该流中读取的 8 字节无符号整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual ulong ReadUInt64()
    {
      this.FillBuffer(8);
      return (ulong) (uint) ((int) this.m_buffer[4] | (int) this.m_buffer[5] << 8 | (int) this.m_buffer[6] << 16 | (int) this.m_buffer[7] << 24) << 32 | (ulong) (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>从当前流中读取 4 字节浮点值，并使流的当前位置提升 4 个字节。</summary>
    /// <returns>从当前流中读取的 4 字节浮点值。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe float ReadSingle()
    {
      this.FillBuffer(4);
      return *(float*) &(uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>从当前流中读取 8 字节浮点值，并使流的当前位置提升 8 个字节。</summary>
    /// <returns>从当前流中读取的 8 字节浮点值。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe double ReadDouble()
    {
      this.FillBuffer(8);
      return *(double*) &((ulong) (uint) ((int) this.m_buffer[4] | (int) this.m_buffer[5] << 8 | (int) this.m_buffer[6] << 16 | (int) this.m_buffer[7] << 24) << 32 | (ulong) (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24));
    }

    /// <summary>从当前流中读取十进制数值，并将该流的当前位置提升十六个字节。</summary>
    /// <returns>从当前流中读取的十进制数值。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Decimal ReadDecimal()
    {
      this.FillBuffer(16);
      try
      {
        return Decimal.ToDecimal(this.m_buffer);
      }
      catch (ArgumentException ex)
      {
        throw new IOException(Environment.GetResourceString("Arg_DecBitCtor"), (Exception) ex);
      }
    }

    /// <summary>从当前流中读取一个字符串。字符串有长度前缀，一次 7 位地被编码为整数。</summary>
    /// <returns>正被读取的字符串。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string ReadString()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      int num = 0;
      int capacity = this.Read7BitEncodedInt();
      if (capacity < 0)
        throw new IOException(Environment.GetResourceString("IO.IO_InvalidStringLen_Len", (object) capacity));
      if (capacity == 0)
        return string.Empty;
      if (this.m_charBytes == null)
        this.m_charBytes = new byte[128];
      if (this.m_charBuffer == null)
        this.m_charBuffer = new char[this.m_maxCharsSize];
      StringBuilder sb = (StringBuilder) null;
      do
      {
        int byteCount = this.m_stream.Read(this.m_charBytes, 0, capacity - num > 128 ? 128 : capacity - num);
        if (byteCount == 0)
          __Error.EndOfFile();
        int chars = this.m_decoder.GetChars(this.m_charBytes, 0, byteCount, this.m_charBuffer, 0);
        if (num == 0 && byteCount == capacity)
          return new string(this.m_charBuffer, 0, chars);
        if (sb == null)
          sb = StringBuilderCache.Acquire(capacity);
        sb.Append(this.m_charBuffer, 0, chars);
        num += byteCount;
      }
      while (num < capacity);
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>从字符数组中的指定点开始，从流中读取指定的字符数。</summary>
    /// <returns>读入缓冲区的总字符数。如果当前可用的字节没有请求的那么多，此数可能小于所请求的字符数；如果到达了流的末尾，此数可能为零。</returns>
    /// <param name="buffer">将数据读入的缓冲区。</param>
    /// <param name="index">缓冲区中的起始点，在该处开始读入缓冲区。</param>
    /// <param name="count">要读取的字符数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。- 或 -要读取的解码字符数大于 <paramref name="count" />。如果 Unicode 解码器返回回退字符或代理项对，则可能发生此情况。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int Read(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      return this.InternalReadChars(buffer, index, count);
    }

    [SecurityCritical]
    private unsafe int InternalReadChars(char[] buffer, int index, int count)
    {
      int charCount = count;
      if (this.m_charBytes == null)
        this.m_charBytes = new byte[128];
      while (charCount > 0)
      {
        int count1 = charCount;
        DecoderNLS decoderNls = this.m_decoder as DecoderNLS;
        if (decoderNls != null && decoderNls.HasState && count1 > 1)
          --count1;
        if (this.m_2BytesPerChar)
          count1 <<= 1;
        if (count1 > 128)
          count1 = 128;
        int num = 0;
        int byteCount;
        byte[] numArray;
        if (this.m_isMemoryStream)
        {
          MemoryStream memoryStream = this.m_stream as MemoryStream;
          num = memoryStream.InternalGetPosition();
          int count2 = count1;
          byteCount = memoryStream.InternalEmulateRead(count2);
          numArray = memoryStream.InternalGetBuffer();
        }
        else
        {
          byteCount = this.m_stream.Read(this.m_charBytes, 0, count1);
          numArray = this.m_charBytes;
        }
        if (byteCount == 0)
          return count - charCount;
        int chars;
        fixed (byte* numPtr = numArray)
          fixed (char* chPtr = buffer)
            chars = this.m_decoder.GetChars(numPtr + num, byteCount, chPtr + index, charCount, false);
        charCount -= chars;
        index += chars;
      }
      return count - charCount;
    }

    private int InternalReadOneChar()
    {
      int num1 = 0;
      long num2;
      long num3 = num2 = 0L;
      if (this.m_stream.CanSeek)
        num3 = this.m_stream.Position;
      if (this.m_charBytes == null)
        this.m_charBytes = new byte[128];
      if (this.m_singleChar == null)
        this.m_singleChar = new char[1];
      while (num1 == 0)
      {
        int byteCount = this.m_2BytesPerChar ? 2 : 1;
        int num4 = this.m_stream.ReadByte();
        this.m_charBytes[0] = (byte) num4;
        if (num4 == -1)
          byteCount = 0;
        if (byteCount == 2)
        {
          int num5 = this.m_stream.ReadByte();
          this.m_charBytes[1] = (byte) num5;
          if (num5 == -1)
            byteCount = 1;
        }
        if (byteCount == 0)
          return -1;
        try
        {
          num1 = this.m_decoder.GetChars(this.m_charBytes, 0, byteCount, this.m_singleChar, 0);
        }
        catch
        {
          if (this.m_stream.CanSeek)
            this.m_stream.Seek(num3 - this.m_stream.Position, SeekOrigin.Current);
          throw;
        }
      }
      if (num1 == 0)
        return -1;
      return (int) this.m_singleChar[0];
    }

    /// <summary>从当前流中读取指定的字符数，并以字符数组的形式返回数据，然后根据所使用的 Encoding 和从流中读取的特定字符，将当前位置前移。</summary>
    /// <returns>包含从基础流中读取的数据的字节数组。如果到达了流的末尾，则该字符数组可能小于所请求的字符数。</returns>
    /// <param name="count">要读取的字符数。</param>
    /// <exception cref="T:System.ArgumentException">要读取的解码字符数大于 <paramref name="count" />。如果 Unicode 解码器返回回退字符或代理项对，则可能发生此情况。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负数。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual char[] ReadChars(int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (count == 0)
        return EmptyArray<char>.Value;
      char[] buffer = new char[count];
      int length = this.InternalReadChars(buffer, 0, count);
      if (length != count)
      {
        char[] chArray = new char[length];
        Buffer.InternalBlockCopy((Array) buffer, 0, (Array) chArray, 0, 2 * length);
        buffer = chArray;
      }
      return buffer;
    }

    /// <summary>从字节数组中的指定点开始，从流中读取指定的字节数。</summary>
    /// <returns>读入 <paramref name="buffer" /> 的字节数。如果可用的字节没有请求的那么多，此数可能小于所请求的字节数；如果到达了流的末尾，此数可能为零。</returns>
    /// <param name="buffer">将数据读入的缓冲区。</param>
    /// <param name="index">缓冲区中的起始点，在该处开始读入缓冲区。</param>
    /// <param name="count">要读取的字节数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。- 或 -要读取的解码字符数大于 <paramref name="count" />。如果 Unicode 解码器返回回退字符或代理项对，则可能发生此情况。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Read(byte[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      return this.m_stream.Read(buffer, index, count);
    }

    /// <summary>从当前流中读取指定的字节数以写入字节数组中，并将当前位置前移相应的字节数。</summary>
    /// <returns>包含从基础流中读取的数据的字节数组。如果到达了流的末尾，则该字节数组可能小于所请求的字节数。</returns>
    /// <param name="count">要读取的字节数。此值必须为 0 或非负数字，否则将出现异常。</param>
    /// <exception cref="T:System.ArgumentException">要读取的解码字符数大于 <paramref name="count" />。如果 Unicode 解码器返回回退字符或代理项对，则可能发生此情况。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负数。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte[] ReadBytes(int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (count == 0)
        return EmptyArray<byte>.Value;
      byte[] buffer = new byte[count];
      int length = 0;
      do
      {
        int num = this.m_stream.Read(buffer, length, count);
        if (num != 0)
        {
          length += num;
          count -= num;
        }
        else
          break;
      }
      while (count > 0);
      if (length != buffer.Length)
      {
        byte[] numArray = new byte[length];
        Buffer.InternalBlockCopy((Array) buffer, 0, (Array) numArray, 0, length);
        buffer = numArray;
      }
      return buffer;
    }

    /// <summary>用从流中读取的指定字节数填充内部缓冲区。</summary>
    /// <param name="numBytes">要读取的字节数。</param>
    /// <exception cref="T:System.IO.EndOfStreamException">在可以读取 <paramref name="numBytes" /> 之前到达了流的末尾。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">请求的 <paramref name="numBytes" /> 大于内部缓冲区大小。</exception>
    [__DynamicallyInvokable]
    protected virtual void FillBuffer(int numBytes)
    {
      if (this.m_buffer != null && (numBytes < 0 || numBytes > this.m_buffer.Length))
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_BinaryReaderFillBuffer"));
      int offset = 0;
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (numBytes == 1)
      {
        int num = this.m_stream.ReadByte();
        if (num == -1)
          __Error.EndOfFile();
        this.m_buffer[0] = (byte) num;
      }
      else
      {
        do
        {
          int num = this.m_stream.Read(this.m_buffer, offset, numBytes - offset);
          if (num == 0)
            __Error.EndOfFile();
          offset += num;
        }
        while (offset < numBytes);
      }
    }

    /// <summary>以压缩格式读入 32 位整数。</summary>
    /// <returns>压缩格式的 32 位整数。</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">流已损坏。</exception>
    [__DynamicallyInvokable]
    protected internal int Read7BitEncodedInt()
    {
      int num1 = 0;
      int num2 = 0;
      while (num2 != 35)
      {
        byte num3 = this.ReadByte();
        num1 |= ((int) num3 & (int) sbyte.MaxValue) << num2;
        num2 += 7;
        if (((int) num3 & 128) == 0)
          return num1;
      }
      throw new FormatException(Environment.GetResourceString("Format_Bad7BitInt32"));
    }
  }
}
