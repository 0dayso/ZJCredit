// Decompiled with JetBrains decompiler
// Type: System.IO.StringReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>实现从字符串进行读取的 <see cref="T:System.IO.TextReader" />。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StringReader : TextReader
  {
    private string _s;
    private int _pos;
    private int _length;

    /// <summary>初始化从指定字符串进行读取的 <see cref="T:System.IO.StringReader" /> 类的新实例。</summary>
    /// <param name="s">应将 <see cref="T:System.IO.StringReader" /> 初始化为的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public StringReader(string s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      this._s = s;
      this._length = s == null ? 0 : s.Length;
    }

    /// <summary>关闭 <see cref="T:System.IO.StringReader" />。</summary>
    /// <filterpriority>2</filterpriority>
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放由 <see cref="T:System.IO.StringReader" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">true 表示释放托管资源和非托管资源；false 表示仅释放非托管资源。</param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      this._s = (string) null;
      this._pos = 0;
      this._length = 0;
      base.Dispose(disposing);
    }

    /// <summary>返回下一个可用的字符，但不使用它。</summary>
    /// <returns>一个表示下一个要读取的字符的整数；如果没有更多可读取的字符或该流不支持查找，则为 -1。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前读取器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int Peek()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      if (this._pos == this._length)
        return -1;
      return (int) this._s[this._pos];
    }

    /// <summary>读取输入字符串中的下一个字符并将该字符的位置提升一个字符。</summary>
    /// <returns>基础字符串中的下一个字符，或者如果没有更多的可用字符，则为 -1。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前读取器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int Read()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      if (this._pos == this._length)
        return -1;
      string str = this._s;
      int num = this._pos;
      this._pos = num + 1;
      int index = num;
      return (int) str[index];
    }

    /// <summary>读取输入字符串中的字符块，并将字符位置提升 <paramref name="count" />。</summary>
    /// <returns>读入缓冲区的总字符数。如果当前没有那么多字符可用，则总字符数可能会少于所请求的字符数，或者如果已到达基础字符串的结尾，则总字符数为零。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">缓存区中的起始索引。</param>
    /// <param name="count">要读取的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前读取器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int Read([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._s == null)
        __Error.ReaderClosed();
      int count1 = this._length - this._pos;
      if (count1 > 0)
      {
        if (count1 > count)
          count1 = count;
        this._s.CopyTo(this._pos, buffer, index, count1);
        this._pos = this._pos + count1;
      }
      return count1;
    }

    /// <summary>读取从当前位置到字符串的结尾的所有字符并将它们作为单个字符串返回。</summary>
    /// <returns>从当前位置到基础字符串的结尾之间的内容。</returns>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前读取器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ReadToEnd()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      string str = this._pos != 0 ? this._s.Substring(this._pos, this._length - this._pos) : this._s;
      this._pos = this._length;
      return str;
    }

    /// <summary>从当前字符串中读取一行字符并将数据作为字符串返回。</summary>
    /// <returns>当前字符串中的下一行；或为 null （如果到达了字符串的末尾）。</returns>
    /// <exception cref="T:System.ObjectDisposedException">当前读取器已关闭。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ReadLine()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      int index;
      for (index = this._pos; index < this._length; ++index)
      {
        char ch = this._s[index];
        switch (ch)
        {
          case '\r':
          case '\n':
            string str = this._s.Substring(this._pos, index - this._pos);
            this._pos = index + 1;
            if ((int) ch != 13 || this._pos >= this._length || (int) this._s[this._pos] != 10)
              return str;
            this._pos = this._pos + 1;
            return str;
          default:
            goto default;
        }
      }
      if (index <= this._pos)
        return (string) null;
      string str1 = this._s.Substring(this._pos, index - this._pos);
      this._pos = index;
      return str1;
    }

    /// <summary>从当前字符串中异步读取一行字符并将数据作为字符串返回。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含来自字符串读取器的下一行或为 null 如果读取所有字符。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">下一行中的字符数大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">字符串读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Task<string> ReadLineAsync()
    {
      return Task.FromResult<string>(this.ReadLine());
    }

    /// <summary>异步读取从当前位置到字符串的结尾的所有字符并将它们作为单个字符串返回。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数值包括字符串来自当前位置到结束字符串字符。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">字符数大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">字符串读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Task<string> ReadToEndAsync()
    {
      return Task.FromResult<string>(this.ReadToEnd());
    }

    /// <summary>异步从当前字符串中读取指定数目的字符并从指定索引开始将该数据写入缓冲区。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可能小于所请求的字节数，或者如果已到达字符串的末尾时，则为 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">最多读取的字符数。如果在写入指定数目的字符到缓冲区之前，就已经达到字符串的末尾，则方法返回。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">字符串读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
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

    /// <summary>异步从当前字符串中读取指定数目的字符并从指定索引开始将该数据写入缓冲区。</summary>
    /// <returns>表示异步读取操作的任务。<paramref name="TResult" /> 参数的值包含读入缓冲区的总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可能小于所请求的字节数，或者如果已到达字符串的末尾时，则为 0（零）。</returns>
    /// <param name="buffer">此方法返回时，包含指定的字符数组，该数组的 <paramref name="index" /> 和 (<paramref name="index" /> + <paramref name="count" /> - 1) 之间的值由从当前源中读取的字符替换。</param>
    /// <param name="index">在 <paramref name="buffer" /> 中开始写入的位置。</param>
    /// <param name="count">最多读取的字符数。如果在写入指定数目的字符到缓冲区之前，就已经达到字符串的末尾，则方法返回。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.ObjectDisposedException">字符串读取器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次读取操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
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
