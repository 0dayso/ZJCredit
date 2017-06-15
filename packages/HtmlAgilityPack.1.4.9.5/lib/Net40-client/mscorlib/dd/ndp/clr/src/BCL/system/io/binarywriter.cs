// Decompiled with JetBrains decompiler
// Type: System.IO.BinaryWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.IO
{
  /// <summary>以二进制形式将基元类型写入流，并支持用特定的编码写入字符串。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class BinaryWriter : IDisposable
  {
    /// <summary>指定无后备存储区的 <see cref="T:System.IO.BinaryWriter" />。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly BinaryWriter Null = new BinaryWriter();
    /// <summary>持有基础流。</summary>
    [__DynamicallyInvokable]
    protected Stream OutStream;
    private byte[] _buffer;
    private Encoding _encoding;
    private Encoder _encoder;
    [OptionalField]
    private bool _leaveOpen;
    [OptionalField]
    private char[] _tmpOneCharBuffer;
    private byte[] _largeByteBuffer;
    private int _maxChars;
    private const int LargeByteBufferSize = 256;

    /// <summary>获取 <see cref="T:System.IO.BinaryWriter" /> 的基础流。</summary>
    /// <returns>与 BinaryWriter 关联的基础流。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual Stream BaseStream
    {
      [__DynamicallyInvokable] get
      {
        this.Flush();
        return this.OutStream;
      }
    }

    /// <summary>初始化向流中写入的 <see cref="T:System.IO.BinaryWriter" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected BinaryWriter()
    {
      this.OutStream = Stream.Null;
      this._buffer = new byte[16];
      this._encoding = (Encoding) new UTF8Encoding(false, true);
      this._encoder = this._encoding.GetEncoder();
    }

    /// <summary>基于所指定的流和特定的 UTF-8 编码，初始化 <see cref="T:System.IO.BinaryWriter" /> 类的新实例。</summary>
    /// <param name="output">输出流。</param>
    /// <exception cref="T:System.ArgumentException">该流不支持写入或者该流已关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="output" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public BinaryWriter(Stream output)
      : this(output, (Encoding) new UTF8Encoding(false, true), false)
    {
    }

    /// <summary>基于所指定的流和特定的字符编码，初始化 <see cref="T:System.IO.BinaryWriter" /> 类的新实例。</summary>
    /// <param name="output">输出流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">该流不支持写入或者该流已关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="output" /> 或 <paramref name="encoding" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public BinaryWriter(Stream output, Encoding encoding)
      : this(output, encoding, false)
    {
    }

    /// <summary>基于所提供的流和特定的字符编码，初始化 <see cref="T:System.IO.BinaryWriter" /> 类的新实例，有选择性的打开流。</summary>
    /// <param name="output">输出流。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <param name="leaveOpen">如果在释放 <see cref="T:System.IO.BinaryWriter" /> 对象之后打开流对象，则为 true；否则为， false。</param>
    /// <exception cref="T:System.ArgumentException">该流不支持写入或者该流已关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="output" /> 或 <paramref name="encoding" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
    {
      if (output == null)
        throw new ArgumentNullException("output");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (!output.CanWrite)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
      this.OutStream = output;
      this._buffer = new byte[16];
      this._encoding = encoding;
      this._encoder = this._encoding.GetEncoder();
      this._leaveOpen = leaveOpen;
    }

    /// <summary>关闭当前的 <see cref="T:System.IO.BinaryWriter" /> 和基础流。</summary>
    /// <filterpriority>1</filterpriority>
    public virtual void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放由 <see cref="T:System.IO.BinaryWriter" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">true 表示释放托管资源和非托管资源；false 表示仅释放非托管资源。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._leaveOpen)
        this.OutStream.Flush();
      else
        this.OutStream.Close();
    }

    /// <summary>释放由 <see cref="T:System.IO.BinaryWriter" /> 类的当前实例占用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>清理当前编写器的所有缓冲区，使所有缓冲数据写入基础设备。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Flush()
    {
      this.OutStream.Flush();
    }

    /// <summary>设置当前流中的位置。</summary>
    /// <returns>包含当前流的位置。</returns>
    /// <param name="offset">相对于 <paramref name="origin" /> 的字节偏移量。</param>
    /// <param name="origin">
    /// <see cref="T:System.IO.SeekOrigin" /> 的一个字段，指示获取新位置所依据的参考点。</param>
    /// <exception cref="T:System.IO.IOException">文件指针被移到无效位置。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="T:System.IO.SeekOrigin" /> 值无效。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual long Seek(int offset, SeekOrigin origin)
    {
      return this.OutStream.Seek((long) offset, origin);
    }

    /// <summary>将单字节 Boolean 值写入当前流，其中 0 表示 false，1 表示 true。</summary>
    /// <param name="value">要写入的 Boolean 值（0 或 1）。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(bool value)
    {
      this._buffer[0] = value ? (byte) 1 : (byte) 0;
      this.OutStream.Write(this._buffer, 0, 1);
    }

    /// <summary>将一个无符号字节写入当前流，并将流的位置提升 1 个字节。</summary>
    /// <param name="value">要写入的无符号字节。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(byte value)
    {
      this.OutStream.WriteByte(value);
    }

    /// <summary>将一个有符号字节写入当前流，并将流的位置提升 1 个字节。</summary>
    /// <param name="value">要写入的有符号字节。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(sbyte value)
    {
      this.OutStream.WriteByte((byte) value);
    }

    /// <summary>将字节数组写入基础流。</summary>
    /// <param name="buffer">包含要写入的数据的字节数组。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      this.OutStream.Write(buffer, 0, buffer.Length);
    }

    /// <summary>将字节数组部分写入当前流。</summary>
    /// <param name="buffer">包含要写入的数据的字节数组。</param>
    /// <param name="index">
    /// <paramref name="buffer" /> 中开始写入的起始点。</param>
    /// <param name="count">要写入的字节数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(byte[] buffer, int index, int count)
    {
      this.OutStream.Write(buffer, index, count);
    }

    /// <summary>将 Unicode 字符写入当前流，并根据所使用的 Encoding 和向流中写入的特定字符，提升流的当前位置。</summary>
    /// <param name="ch">要写入的非代理项 Unicode 字符。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ch" /> 是单一代理项字符。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(char ch)
    {
      if (char.IsSurrogate(ch))
        throw new ArgumentException(Environment.GetResourceString("Arg_SurrogatesNotAllowedAsSingleChar"));
      int bytes1;
      fixed (byte* bytes2 = this._buffer)
        bytes1 = this._encoder.GetBytes(&ch, 1, bytes2, 16, true);
      this.OutStream.Write(this._buffer, 0, bytes1);
    }

    /// <summary>将字符数组写入当前流，并根据所使用的 Encoding 和向流中写入的特定字符，提升流的当前位置。</summary>
    /// <param name="chars">包含要写入的数据的字符数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException("chars");
      byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
      this.OutStream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>将字符数组部分写入当前流，并根据所使用的 Encoding（可能还根据向流中写入的特定字符），提升流的当前位置。</summary>
    /// <param name="chars">包含要写入的数据的字符数组。</param>
    /// <param name="index">
    /// <paramref name="chars" /> 中开始写入的起始点。</param>
    /// <param name="count">要写入的字符数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(char[] chars, int index, int count)
    {
      byte[] bytes = this._encoding.GetBytes(chars, index, count);
      this.OutStream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>将 8 字节浮点值写入当前流，并将流的位置提升 8 个字节。</summary>
    /// <param name="value">要写入的 8 字节浮点值。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(double value)
    {
      ulong num = (ulong) *(long*) &value;
      this._buffer[0] = (byte) num;
      this._buffer[1] = (byte) (num >> 8);
      this._buffer[2] = (byte) (num >> 16);
      this._buffer[3] = (byte) (num >> 24);
      this._buffer[4] = (byte) (num >> 32);
      this._buffer[5] = (byte) (num >> 40);
      this._buffer[6] = (byte) (num >> 48);
      this._buffer[7] = (byte) (num >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    /// <summary>将一个十进制值写入当前流，并将流位置提升十六个字节。</summary>
    /// <param name="value">要写入的十进制值。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(Decimal value)
    {
      Decimal.GetBytes(value, this._buffer);
      this.OutStream.Write(this._buffer, 0, 16);
    }

    /// <summary>将 2 字节有符号整数写入当前流，并将流的位置提升 2 个字节。</summary>
    /// <param name="value">要写入的 2 字节有符号整数。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(short value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) ((uint) value >> 8);
      this.OutStream.Write(this._buffer, 0, 2);
    }

    /// <summary>将 2 字节无符号整数写入当前流，并将流的位置提升 2 个字节。</summary>
    /// <param name="value">要写入的 2 字节无符号整数。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(ushort value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) ((uint) value >> 8);
      this.OutStream.Write(this._buffer, 0, 2);
    }

    /// <summary>将 4 字节有符号整数写入当前流，并将流的位置提升 4 个字节。</summary>
    /// <param name="value">要写入的 4 字节有符号整数。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(int value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    /// <summary>将 4 字节无符号整数写入当前流，并将流的位置提升 4 个字节。</summary>
    /// <param name="value">要写入的 4 字节无符号整数。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(uint value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    /// <summary>将 8 字节有符号整数写入当前流，并将流的位置提升 8 个字节。</summary>
    /// <param name="value">要写入的 8 字节有符号整数。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(long value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this._buffer[4] = (byte) (value >> 32);
      this._buffer[5] = (byte) (value >> 40);
      this._buffer[6] = (byte) (value >> 48);
      this._buffer[7] = (byte) (value >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    /// <summary>将 8 字节无符号整数写入当前流，并将流的位置提升 8 个字节。</summary>
    /// <param name="value">要写入的 8 字节无符号整数。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(ulong value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this._buffer[4] = (byte) (value >> 32);
      this._buffer[5] = (byte) (value >> 40);
      this._buffer[6] = (byte) (value >> 48);
      this._buffer[7] = (byte) (value >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    /// <summary>将 4 字节浮点值写入当前流，并将流的位置提升 4 个字节。</summary>
    /// <param name="value">要写入的 4 字节浮点值。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(float value)
    {
      uint num = *(uint*) &value;
      this._buffer[0] = (byte) num;
      this._buffer[1] = (byte) (num >> 8);
      this._buffer[2] = (byte) (num >> 16);
      this._buffer[3] = (byte) (num >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    /// <summary>将有长度前缀的字符串按 <see cref="T:System.IO.BinaryWriter" /> 的当前编码写入此流，并根据所使用的编码和写入流的特定字符，提升流的当前位置。</summary>
    /// <param name="value">要写入的值。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      int byteCount = this._encoding.GetByteCount(value);
      this.Write7BitEncodedInt(byteCount);
      if (this._largeByteBuffer == null)
      {
        this._largeByteBuffer = new byte[256];
        this._maxChars = 256 / this._encoding.GetMaxByteCount(1);
      }
      if (byteCount <= 256)
      {
        this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
        this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
      }
      else
      {
        int num = 0;
        int length = value.Length;
        while (length > 0)
        {
          int charCount = length > this._maxChars ? this._maxChars : length;
          string str = value;
          char* chPtr = (char*) str;
          if ((IntPtr) chPtr != IntPtr.Zero)
            chPtr += RuntimeHelpers.OffsetToStringData;
          int bytes1;
          fixed (byte* bytes2 = this._largeByteBuffer)
            bytes1 = this._encoder.GetBytes(chPtr + num, charCount, bytes2, 256, charCount == length);
          str = (string) null;
          this.OutStream.Write(this._largeByteBuffer, 0, bytes1);
          num += charCount;
          length -= charCount;
        }
      }
    }

    /// <summary>以压缩格式写出 32 位整数。</summary>
    /// <param name="value">要写出的 32 位整数。</param>
    /// <exception cref="T:System.IO.EndOfStreamException">已到达流的末尾。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已关闭。</exception>
    /// <exception cref="T:System.IO.IOException">流已关闭。</exception>
    [__DynamicallyInvokable]
    protected void Write7BitEncodedInt(int value)
    {
      uint num = (uint) value;
      while (num >= 128U)
      {
        this.Write((byte) (num | 128U));
        num >>= 7;
      }
      this.Write((byte) num);
    }
  }
}
