// Decompiled with JetBrains decompiler
// Type: System.IO.StringWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>实现一个用于将信息写入字符串的 <see cref="T:System.IO.TextWriter" />。该信息存储在基础 <see cref="T:System.Text.StringBuilder" /> 中。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StringWriter : TextWriter
  {
    private static volatile UnicodeEncoding m_encoding;
    private StringBuilder _sb;
    private bool _isOpen;

    /// <summary>获取将输出写入到其中的 <see cref="T:System.Text.Encoding" />。</summary>
    /// <returns>用来写入输出的 Encoding。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Encoding Encoding
    {
      [__DynamicallyInvokable] get
      {
        if (StringWriter.m_encoding == null)
          StringWriter.m_encoding = new UnicodeEncoding(false, false);
        return (Encoding) StringWriter.m_encoding;
      }
    }

    /// <summary>初始化 <see cref="T:System.IO.StringWriter" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public StringWriter()
      : this(new StringBuilder(), (IFormatProvider) CultureInfo.CurrentCulture)
    {
    }

    /// <summary>使用指定的格式控制初始化 <see cref="T:System.IO.StringWriter" /> 类的新实例。</summary>
    /// <param name="formatProvider">控制格式设置的 <see cref="T:System.IFormatProvider" /> 对象。</param>
    [__DynamicallyInvokable]
    public StringWriter(IFormatProvider formatProvider)
      : this(new StringBuilder(), formatProvider)
    {
    }

    /// <summary>初始化写入指定 <see cref="T:System.Text.StringBuilder" /> 的 <see cref="T:System.IO.StringWriter" /> 类的新实例。</summary>
    /// <param name="sb">要写入的 StringBuilder。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sb" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StringWriter(StringBuilder sb)
      : this(sb, (IFormatProvider) CultureInfo.CurrentCulture)
    {
    }

    /// <summary>初始化写入指定 <see cref="T:System.Text.StringBuilder" /> 并具有指定格式提供程序的 <see cref="T:System.IO.StringWriter" /> 类的新实例。</summary>
    /// <param name="sb">要写入的 StringBuilder。</param>
    /// <param name="formatProvider">控制格式设置的 <see cref="T:System.IFormatProvider" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sb" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
      : base(formatProvider)
    {
      if (sb == null)
        throw new ArgumentNullException("sb", Environment.GetResourceString("ArgumentNull_Buffer"));
      this._sb = sb;
      this._isOpen = true;
    }

    /// <summary>关闭当前的 <see cref="T:System.IO.StringWriter" /> 和基础流。</summary>
    /// <filterpriority>1</filterpriority>
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放由 <see cref="T:System.IO.StringWriter" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">true 表示释放托管资源和非托管资源；false 表示仅释放非托管资源。</param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      this._isOpen = false;
      base.Dispose(disposing);
    }

    /// <summary>返回基础 <see cref="T:System.Text.StringBuilder" />。</summary>
    /// <returns>基础 StringBuilder。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual StringBuilder GetStringBuilder()
    {
      return this._sb;
    }

    /// <summary>将字符写入该字符串。</summary>
    /// <param name="value">要写入的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">编写器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(char value)
    {
      if (!this._isOpen)
        __Error.WriterClosed();
      this._sb.Append(value);
    }

    /// <summary>将字符的子数组写入该字符串。</summary>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始读取数据缓存中的位置。</param>
    /// <param name="count">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">(<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />。Length.</exception>
    /// <exception cref="T:System.ObjectDisposedException">编写器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
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
      if (!this._isOpen)
        __Error.WriterClosed();
      this._sb.Append(buffer, index, count);
    }

    /// <summary>将字符串写入当前流。</summary>
    /// <param name="value">要写入的字符串。</param>
    /// <exception cref="T:System.ObjectDisposedException">编写器已关闭。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override void Write(string value)
    {
      if (!this._isOpen)
        __Error.WriterClosed();
      if (value == null)
        return;
      this._sb.Append(value);
    }

    /// <summary>以异步方式将字符写入流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入字符串中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">字符串编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">字符串编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char value)
    {
      this.Write(value);
      return Task.CompletedTask;
    }

    /// <summary>以异步方式将字符串写入当前流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入的字符串。如果 <paramref name="value" /> 为 null，则不会将任何内容写入文本流。</param>
    /// <exception cref="T:System.ObjectDisposedException">字符串编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">字符串编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(string value)
    {
      this.Write(value);
      return Task.CompletedTask;
    }

    /// <summary>将字符的子数组异步写入该字符串。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始读取数据缓存中的位置。</param>
    /// <param name="count">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> plus <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">字符串编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">字符串编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char[] buffer, int index, int count)
    {
      this.Write(buffer, index, count);
      return Task.CompletedTask;
    }

    /// <summary>以异步方式将后跟行结束符的字符写入该字符串。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入字符串中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">字符串编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">字符串编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char value)
    {
      this.WriteLine(value);
      return Task.CompletedTask;
    }

    /// <summary>将一后跟行结束符的字符串异步写入当前字符串。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入的字符串。如果值为 null，则只写入行终止符。</param>
    /// <exception cref="T:System.ObjectDisposedException">字符串编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">字符串编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(string value)
    {
      this.WriteLine(value);
      return Task.CompletedTask;
    }

    /// <summary>将后跟行结束符的字符子数组异步写入该字符串。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始读取数据缓存中的位置。</param>
    /// <param name="count">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> plus <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">字符串编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">字符串编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char[] buffer, int index, int count)
    {
      this.WriteLine(buffer, index, count);
      return Task.CompletedTask;
    }

    /// <summary>异步清理当前编写器的所有缓冲区，使所有缓冲数据写入基础设备。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync()
    {
      return Task.CompletedTask;
    }

    /// <summary>返回包含迄今为止写入到当前 StringWriter 中的字符的字符串。</summary>
    /// <returns>包含写入到当前 StringWriter 中的字符的字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this._sb.ToString();
    }
  }
}
