// Decompiled with JetBrains decompiler
// Type: System.IO.TextWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>表示可以编写一个有序字符系列的编写器。此类为抽象类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class TextWriter : MarshalByRefObject, IDisposable
  {
    /// <summary>提供 TextWriter，它不带任何可写入但无法从中读取的后备存储。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly TextWriter Null = (TextWriter) new TextWriter.NullTextWriter();
    [NonSerialized]
    private static Action<object> _WriteCharDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_0);
    [NonSerialized]
    private static Action<object> _WriteStringDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_1);
    [NonSerialized]
    private static Action<object> _WriteCharArrayRangeDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_2);
    [NonSerialized]
    private static Action<object> _WriteLineCharDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_3);
    [NonSerialized]
    private static Action<object> _WriteLineStringDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_4);
    [NonSerialized]
    private static Action<object> _WriteLineCharArrayRangeDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_5);
    [NonSerialized]
    private static Action<object> _FlushDelegate = new Action<object>(TextWriter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__72_6);
    /// <summary>存储用于此 TextWriter 的换行符。</summary>
    [__DynamicallyInvokable]
    protected char[] CoreNewLine = new char[2]{ '\r', '\n' };
    private const string InitialNewLine = "\r\n";
    private IFormatProvider InternalFormatProvider;

    /// <summary>获取控制格式设置的对象。</summary>
    /// <returns>特定区域性的 <see cref="T:System.IFormatProvider" /> 对象，或者如果未指定任何其他区域性，则为当前区域性的格式设置。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual IFormatProvider FormatProvider
    {
      [__DynamicallyInvokable] get
      {
        if (this.InternalFormatProvider == null)
          return (IFormatProvider) Thread.CurrentThread.CurrentCulture;
        return this.InternalFormatProvider;
      }
    }

    /// <summary>当在派生类中重写时，返回用来写输出的该字符编码。</summary>
    /// <returns>用来写入输出的字符编码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract Encoding Encoding { [__DynamicallyInvokable] get; }

    /// <summary>获取或设置由当前 TextWriter 使用的行结束符字符串。</summary>
    /// <returns>当前 TextWriter 的行结束符字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string NewLine
    {
      [__DynamicallyInvokable] get
      {
        return new string(this.CoreNewLine);
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          value = "\r\n";
        this.CoreNewLine = value.ToCharArray();
      }
    }

    /// <summary>初始化 <see cref="T:System.IO.TextWriter" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected TextWriter()
    {
      this.InternalFormatProvider = (IFormatProvider) null;
    }

    /// <summary>使用指定的格式提供程序初始化 <see cref="T:System.IO.TextWriter" /> 类的新实例。</summary>
    /// <param name="formatProvider">控制格式设置的 <see cref="T:System.IFormatProvider" /> 对象。</param>
    [__DynamicallyInvokable]
    protected TextWriter(IFormatProvider formatProvider)
    {
      this.InternalFormatProvider = formatProvider;
    }

    /// <summary>关闭当前编写器并释放任何与该编写器关联的系统资源。</summary>
    /// <filterpriority>1</filterpriority>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.IO.TextWriter" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>释放由 <see cref="T:System.IO.TextWriter" /> 对象使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>清理当前编写器的所有缓冲区，使所有缓冲数据写入基础设备。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Flush()
    {
    }

    /// <summary>在指定的 TextWriter 周围创建线程安全包装。</summary>
    /// <returns>线程安全包装。</returns>
    /// <param name="writer">要同步的 TextWriter。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="writer" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static TextWriter Synchronized(TextWriter writer)
    {
      if (writer == null)
        throw new ArgumentNullException("writer");
      if (writer is TextWriter.SyncTextWriter)
        return writer;
      return (TextWriter) new TextWriter.SyncTextWriter(writer);
    }

    /// <summary>将字符写入该文本字符串或流。</summary>
    /// <param name="value">要写入文本流中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(char value)
    {
    }

    /// <summary>将字符数组写入该文本字符串或流。</summary>
    /// <param name="buffer">要写入文本流中的字符数组。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(char[] buffer)
    {
      if (buffer == null)
        return;
      this.Write(buffer, 0, buffer.Length);
    }

    /// <summary>将字符的子数组写入文本字符串或流。</summary>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始接收数据时缓存中的字符位置。</param>
    /// <param name="count">要写入的字符数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      for (int index1 = 0; index1 < count; ++index1)
        this.Write(buffer[index + index1]);
    }

    /// <summary>将 Boolean 值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 Boolean 值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(bool value)
    {
      this.Write(value ? "True" : "False");
    }

    /// <summary>将 4 字节有符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 4 字节有符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(int value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>将 4 字节无符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 4 字节无符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(uint value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>将 8 字节有符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 8 字节有符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(long value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>将 8 字节无符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 8 字节无符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(ulong value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>将 4 字节浮点值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 4 字节浮点值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(float value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>将 8 字节浮点值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 8 字节浮点值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(double value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>将十进制值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的十进制值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(Decimal value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>以异步形式将字符串写入到文本字符串或流。</summary>
    /// <param name="value">要写入的字符串。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(string value)
    {
      if (value == null)
        return;
      this.Write(value.ToCharArray());
    }

    /// <summary>通过在对象上调用 ToString 方法将此对象的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的对象。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(object value)
    {
      if (value == null)
        return;
      IFormattable formattable = value as IFormattable;
      if (formattable != null)
        this.Write(formattable.ToString((string) null, this.FormatProvider));
      else
        this.Write(value.ToString());
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object)" /> 方法相同的语义将格式化字符串和新行写入文本字符串或流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要格式化和写入的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0 （0）、或大于或等于要设置格式的对象数 (用于该方法重载，为一)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(string format, object arg0)
    {
      this.Write(string.Format(this.FormatProvider, format, arg0));
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> 方法相同的语义将格式化字符串和新行写入文本字符串或流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要格式化和写入的第一个对象。</param>
    /// <param name="arg1">要格式化和写入的第二个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0 （0） 或大于或等于要设置格式的对象数 (用于该方法重载，为二)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(string format, object arg0, object arg1)
    {
      this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object,System.Object,System.Object)" /> 方法相同的语义将格式化字符串和新行写入文本字符串或流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要格式化和写入的第一个对象。</param>
    /// <param name="arg1">要格式化和写入的第二个对象。</param>
    /// <param name="arg2">要设置格式和写入的第三个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0 （0）、或大于或等于要设置格式的对象数 (用于该方法重载，为三)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(string format, object arg0, object arg1, object arg2)
    {
      this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object[])" /> 方法相同的语义将格式化字符串和新行写入文本字符串或流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg">一个对象数组，其中包含零个或多个要设置格式和写入的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="arg" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0（零）或大于等于 <paramref name="arg" /> 数组的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Write(string format, params object[] arg)
    {
      this.Write(string.Format(this.FormatProvider, format, arg));
    }

    /// <summary>将行结束符的字符串写入文本字符串或流。</summary>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine()
    {
      this.Write(this.CoreNewLine);
    }

    /// <summary>将后跟行结束符的字符写入文本字符串或流。</summary>
    /// <param name="value">要写入文本流中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(char value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的字符数组写入文本字符串或流。</summary>
    /// <param name="buffer">从其读取数据的字符数组。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(char[] buffer)
    {
      this.Write(buffer);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的字符子数组写入文本字符串或流。</summary>
    /// <param name="buffer">从其读取数据的字符数组。</param>
    /// <param name="index">在开始读取数据时 <paramref name="buffer" /> 中的字符位置。</param>
    /// <param name="count">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentException">缓冲区长度减去 <paramref name="index" /> 小于 <paramref name="count" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(char[] buffer, int index, int count)
    {
      this.Write(buffer, index, count);
      this.WriteLine();
    }

    /// <summary>将后面带有行结束符的 Boolean 值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 Boolean 值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(bool value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的 4 字节有符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 4 字节有符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(int value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的 4 字节无符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 4 字节无符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void WriteLine(uint value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的 8 字节有符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 8 字节有符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(long value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的 8 字节无符号整数的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 8 字节无符号整数。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void WriteLine(ulong value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的 4 字节浮点值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 4 字节浮点值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(float value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的 8 字节浮点值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的 8 字节浮点值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(double value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后面带有行结束符的十进制值的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的十进制值。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(Decimal value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>将后跟行结束符的字符串写入文本字符串或流。</summary>
    /// <param name="value">要写入的字符串。如果 <paramref name="value" /> 为 null，则只写入行终止符。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string value)
    {
      if (value == null)
      {
        this.WriteLine();
      }
      else
      {
        int length1 = value.Length;
        int length2 = this.CoreNewLine.Length;
        char[] chArray = new char[length1 + length2];
        value.CopyTo(0, chArray, 0, length1);
        if (length2 == 2)
        {
          chArray[length1] = this.CoreNewLine[0];
          chArray[length1 + 1] = this.CoreNewLine[1];
        }
        else if (length2 == 1)
          chArray[length1] = this.CoreNewLine[0];
        else
          Buffer.InternalBlockCopy((Array) this.CoreNewLine, 0, (Array) chArray, length1 * 2, length2 * 2);
        this.Write(chArray, 0, length1 + length2);
      }
    }

    /// <summary>通过在对象上调用 ToString 方法将后跟行结束符的此对象的文本表示形式写入文本字符串或流。</summary>
    /// <param name="value">要写入的对象。如果 <paramref name="value" /> 为 null，则只写入行终止符。</param>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(object value)
    {
      if (value == null)
      {
        this.WriteLine();
      }
      else
      {
        IFormattable formattable = value as IFormattable;
        if (formattable != null)
          this.WriteLine(formattable.ToString((string) null, this.FormatProvider));
        else
          this.WriteLine(value.ToString());
      }
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object)" /> 方法相同的语义将格式化字符串和新行写入文本字符串或流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要格式化和写入的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0 （0）、或大于或等于要设置格式的对象数 (用于该方法重载，为一)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, object arg0)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg0));
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> 方法相同的语义将格式化字符串和新行写入文本字符串或流。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要格式化和写入的第一个对象。</param>
    /// <param name="arg1">要格式化和写入的第二个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0 （0）、或大于或等于要设置格式的对象数 (用于该方法重载，为二)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, object arg0, object arg1)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object)" /> 相同的语义写出格式化的字符串和一个新行。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要格式化和写入的第一个对象。</param>
    /// <param name="arg1">要格式化和写入的第二个对象。</param>
    /// <param name="arg2">要设置格式和写入的第三个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0 （0）、或大于或等于要设置格式的对象数 (用于该方法重载，为三)。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
    }

    /// <summary>使用与 <see cref="M:System.String.Format(System.String,System.Object)" /> 相同的语义写出格式化的字符串和一个新行。</summary>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg">一个对象数组，其中包含零个或多个要设置格式和写入的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">字符串或对象作为 null 传入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:System.IO.TextWriter" /> 是关闭的。</exception>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 不是有效的复合格式字符串。- 或 - 格式项的索引小于 0（零）或大于等于 <paramref name="arg" /> 数组的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, params object[] arg)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg));
    }

    /// <summary>以异步形式将字符写入到下一个文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入文本流中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteAsync(char value)
    {
      Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteCharDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>以异步形式将字符串写入到下一字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入的字符串。如果 <paramref name="value" /> 为 null，则不会将任何内容写入文本流。</param>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteAsync(string value)
    {
      Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteStringDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>以异步形式将字符数组写入到下一个字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">要写入文本流中的字符数组。如果 <paramref name="buffer" /> 为 null，则不写入任何内容。</param>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task WriteAsync(char[] buffer)
    {
      if (buffer == null)
        return Task.CompletedTask;
      return this.WriteAsync(buffer, 0, buffer.Length);
    }

    /// <summary>将字符的子数组异步写入文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始接收数据时缓存中的字符位置。</param>
    /// <param name="count">要写入的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> plus <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteAsync(char[] buffer, int index, int count)
    {
      Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
      return Task.Factory.StartNew(TextWriter._WriteCharArrayRangeDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>将后跟行结束符的字符异步写入文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入文本流中的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync(char value)
    {
      Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteLineCharDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>将后跟行结束符的字符串异步写入文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="value">要写入的字符串。如果值为 null，则只写入行终止符。</param>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync(string value)
    {
      Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteLineStringDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>将后跟行结束符的字符数组异步写入文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">要写入文本流中的字符数组。如果字符数组为 null，则仅写入行结束符。</param>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task WriteLineAsync(char[] buffer)
    {
      if (buffer == null)
        return Task.CompletedTask;
      return this.WriteLineAsync(buffer, 0, buffer.Length);
    }

    /// <summary>将后跟行结束符的字符子数组异步写入文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">要从中写出数据的字符数组。</param>
    /// <param name="index">在开始接收数据时缓存中的字符位置。</param>
    /// <param name="count">要写入的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> plus <paramref name="count" /> 大于缓冲区长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync(char[] buffer, int index, int count)
    {
      Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
      return Task.Factory.StartNew(TextWriter._WriteLineCharArrayRangeDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>将行结束符的字符串异步写入文本字符串或流。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">文本编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync()
    {
      return this.WriteAsync(this.CoreNewLine);
    }

    /// <summary>异步清理当前编写器的所有缓冲区，使所有缓冲数据写入基础设备。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <exception cref="T:System.ObjectDisposedException">文本编写器已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">编写器正在由其前一次写操作使用。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task FlushAsync()
    {
      return Task.Factory.StartNew(TextWriter._FlushDelegate, (object) this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    [Serializable]
    private sealed class NullTextWriter : TextWriter
    {
      public override Encoding Encoding
      {
        get
        {
          return Encoding.Default;
        }
      }

      internal NullTextWriter()
        : base((IFormatProvider) CultureInfo.InvariantCulture)
      {
      }

      public override void Write(char[] buffer, int index, int count)
      {
      }

      public override void Write(string value)
      {
      }

      public override void WriteLine()
      {
      }

      public override void WriteLine(string value)
      {
      }

      public override void WriteLine(object value)
      {
      }
    }

    [Serializable]
    internal sealed class SyncTextWriter : TextWriter, IDisposable
    {
      private TextWriter _out;

      public override Encoding Encoding
      {
        get
        {
          return this._out.Encoding;
        }
      }

      public override IFormatProvider FormatProvider
      {
        get
        {
          return this._out.FormatProvider;
        }
      }

      public override string NewLine
      {
        [MethodImpl(MethodImplOptions.Synchronized)] get
        {
          return this._out.NewLine;
        }
        [MethodImpl(MethodImplOptions.Synchronized)] set
        {
          this._out.NewLine = value;
        }
      }

      internal SyncTextWriter(TextWriter t)
        : base(t.FormatProvider)
      {
        this._out = t;
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Close()
      {
        this._out.Close();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      protected override void Dispose(bool disposing)
      {
        if (!disposing)
          return;
        this._out.Dispose();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Flush()
      {
        this._out.Flush();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(char value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(char[] buffer)
      {
        this._out.Write(buffer);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(char[] buffer, int index, int count)
      {
        this._out.Write(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(bool value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(int value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(uint value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(long value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(ulong value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(float value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(double value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(Decimal value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(object value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, object arg0)
      {
        this._out.Write(format, arg0);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, object arg0, object arg1)
      {
        this._out.Write(format, arg0, arg1);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, object arg0, object arg1, object arg2)
      {
        this._out.Write(format, arg0, arg1, arg2);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, params object[] arg)
      {
        this._out.Write(format, arg);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine()
      {
        this._out.WriteLine();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(char value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(Decimal value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(char[] buffer)
      {
        this._out.WriteLine(buffer);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(char[] buffer, int index, int count)
      {
        this._out.WriteLine(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(bool value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(int value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(uint value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(long value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(ulong value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(float value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(double value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(object value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, object arg0)
      {
        this._out.WriteLine(format, arg0);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, object arg0, object arg1)
      {
        this._out.WriteLine(format, arg0, arg1);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, object arg0, object arg1, object arg2)
      {
        this._out.WriteLine(format, arg0, arg1, arg2);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, params object[] arg)
      {
        this._out.WriteLine(format, arg);
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteAsync(char value)
      {
        this.Write(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteAsync(string value)
      {
        this.Write(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteAsync(char[] buffer, int index, int count)
      {
        this.Write(buffer, index, count);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteLineAsync(char value)
      {
        this.WriteLine(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteLineAsync(string value)
      {
        this.WriteLine(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteLineAsync(char[] buffer, int index, int count)
      {
        this.WriteLine(buffer, index, count);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task FlushAsync()
      {
        this.Flush();
        return Task.CompletedTask;
      }
    }
  }
}
