// Decompiled with JetBrains decompiler
// Type: System.Text.Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
  /// <summary>表示字符编码。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Encoding : ICloneable
  {
    [OptionalField(VersionAdded = 2)]
    private bool m_isReadOnly = true;
    private static volatile Encoding defaultEncoding;
    private static volatile Encoding unicodeEncoding;
    private static volatile Encoding bigEndianUnicode;
    private static volatile Encoding utf7Encoding;
    private static volatile Encoding utf8Encoding;
    private static volatile Encoding utf32Encoding;
    private static volatile Encoding asciiEncoding;
    private static volatile Encoding latin1Encoding;
    private static volatile Hashtable encodings;
    private const int MIMECONTF_MAILNEWS = 1;
    private const int MIMECONTF_BROWSER = 2;
    private const int MIMECONTF_SAVABLE_MAILNEWS = 256;
    private const int MIMECONTF_SAVABLE_BROWSER = 512;
    private const int CodePageDefault = 0;
    private const int CodePageNoOEM = 1;
    private const int CodePageNoMac = 2;
    private const int CodePageNoThread = 3;
    private const int CodePageNoSymbol = 42;
    private const int CodePageUnicode = 1200;
    private const int CodePageBigEndian = 1201;
    private const int CodePageWindows1252 = 1252;
    private const int CodePageMacGB2312 = 10008;
    private const int CodePageGB2312 = 20936;
    private const int CodePageMacKorean = 10003;
    private const int CodePageDLLKorean = 20949;
    private const int ISO2022JP = 50220;
    private const int ISO2022JPESC = 50221;
    private const int ISO2022JPSISO = 50222;
    private const int ISOKorean = 50225;
    private const int ISOSimplifiedCN = 50227;
    private const int EUCJP = 51932;
    private const int ChineseHZ = 52936;
    private const int DuplicateEUCCN = 51936;
    private const int EUCCN = 936;
    private const int EUCKR = 51949;
    internal const int CodePageASCII = 20127;
    internal const int ISO_8859_1 = 28591;
    private const int ISCIIAssemese = 57006;
    private const int ISCIIBengali = 57003;
    private const int ISCIIDevanagari = 57002;
    private const int ISCIIGujarathi = 57010;
    private const int ISCIIKannada = 57008;
    private const int ISCIIMalayalam = 57009;
    private const int ISCIIOriya = 57007;
    private const int ISCIIPanjabi = 57011;
    private const int ISCIITamil = 57004;
    private const int ISCIITelugu = 57005;
    private const int GB18030 = 54936;
    private const int ISO_8859_8I = 38598;
    private const int ISO_8859_8_Visual = 28598;
    private const int ENC50229 = 50229;
    private const int CodePageUTF7 = 65000;
    private const int CodePageUTF8 = 65001;
    private const int CodePageUTF32 = 12000;
    private const int CodePageUTF32BE = 12001;
    internal int m_codePage;
    internal CodePageDataItem dataItem;
    [NonSerialized]
    internal bool m_deserializedFromEverett;
    [OptionalField(VersionAdded = 2)]
    internal EncoderFallback encoderFallback;
    [OptionalField(VersionAdded = 2)]
    internal DecoderFallback decoderFallback;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (Encoding.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, (object) null);
        }
        return Encoding.s_InternalSyncObject;
      }
    }

    /// <summary>在派生类中重写时，获取可与邮件代理正文标记一起使用的当前编码的名称。</summary>
    /// <returns>可与邮件代理正文标记一起使用的当前 <see cref="T:System.Text.Encoding" /> 的名称。- 或 -如果当前 <see cref="T:System.Text.Encoding" /> 无法使用，则为空字符串 ("")。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual string BodyName
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.BodyName;
      }
    }

    /// <summary>在派生类中重写时，获取当前编码的用户可读说明。</summary>
    /// <returns>当前 <see cref="T:System.Text.Encoding" /> 的可读说明。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string EncodingName
    {
      [__DynamicallyInvokable] get
      {
        return Environment.GetResourceString("Globalization.cp_" + (object) this.m_codePage);
      }
    }

    /// <summary>在派生类中重写时，获取可与邮件代理头标记一起使用的当前编码的名称。</summary>
    /// <returns>与邮件代理头标记一起使用的当前 <see cref="T:System.Text.Encoding" /> 的名称。- 或 -如果当前 <see cref="T:System.Text.Encoding" /> 无法使用，则为空字符串 ("")。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual string HeaderName
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.HeaderName;
      }
    }

    /// <summary>在派生类中重写时，获取在 Internet 编号分配管理机构 (IANA) 注册的当前编码的名称。</summary>
    /// <returns>当前 <see cref="T:System.Text.Encoding" /> 的 IANA 名称。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string WebName
    {
      [__DynamicallyInvokable] get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.WebName;
      }
    }

    /// <summary>在派生类中重写时，获取与当前编码最紧密对应的 Windows 操作系统代码页。</summary>
    /// <returns>与当前 <see cref="T:System.Text.Encoding" /> 最紧密对应的 Windows 操作系统代码页。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int WindowsCodePage
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.UIFamilyCodePage;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示浏览器客户端是否可以使用当前的编码显示内容。</summary>
    /// <returns>如果浏览器客户端可以使用当前的 <see cref="T:System.Text.Encoding" /> 显示内容，则为 true；否则，为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsBrowserDisplay
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 2U) > 0U;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示浏览器客户端是否可以使用当前的编码保存内容。</summary>
    /// <returns>如果浏览器客户端可以使用当前的 <see cref="T:System.Text.Encoding" /> 保存内容，则为 true；否则，为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsBrowserSave
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 512U) > 0U;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示邮件和新闻客户端是否可以使用当前的编码显示内容。</summary>
    /// <returns>如果邮件和新闻客户端可以使用当前的 <see cref="T:System.Text.Encoding" /> 显示内容，则为 true；否则，为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsMailNewsDisplay
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 1U) > 0U;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示邮件和新闻客户端是否可以使用当前的编码保存内容。</summary>
    /// <returns>如果邮件和新闻客户端可以使用当前的 <see cref="T:System.Text.Encoding" /> 保存内容，则为 true；否则，为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsMailNewsSave
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 256U) > 0U;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示当前的编码是否使用单字节码位。</summary>
    /// <returns>如果当前的 <see cref="T:System.Text.Encoding" /> 使用单字节码位，则为 true；否则，为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual bool IsSingleByte
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>获取或设置当前 <see cref="T:System.Text.Encoding" /> 对象的 <see cref="T:System.Text.EncoderFallback" /> 对象。</summary>
    /// <returns>当前 <see cref="T:System.Text.Encoding" /> 对象的编码回退对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">设置操作中的值为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">由于当前 <see cref="T:System.Text.Encoding" /> 对象为只读，所以无法在设置操作中赋值。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public EncoderFallback EncoderFallback
    {
      [__DynamicallyInvokable] get
      {
        return this.encoderFallback;
      }
      set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value");
        this.encoderFallback = value;
      }
    }

    /// <summary>获取或设置当前 <see cref="T:System.Text.Encoding" /> 对象的 <see cref="T:System.Text.DecoderFallback" /> 对象。</summary>
    /// <returns>当前 <see cref="T:System.Text.Encoding" /> 对象的解码回退对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">设置操作中的值为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">由于当前 <see cref="T:System.Text.Encoding" /> 对象为只读，所以无法在设置操作中赋值。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public DecoderFallback DecoderFallback
    {
      [__DynamicallyInvokable] get
      {
        return this.decoderFallback;
      }
      set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException("value");
        this.decoderFallback = value;
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值指示当前的编码是否为只读。</summary>
    /// <returns>true if the current <see cref="T:System.Text.Encoding" /> is read-only; otherwise, false.默认值为 true。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public bool IsReadOnly
    {
      get
      {
        return this.m_isReadOnly;
      }
    }

    /// <summary>获取 ASCII（7 位）字符集的编码。</summary>
    /// <returns>ASCII（7 位）字符集的编码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding ASCII
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.asciiEncoding == null)
          Encoding.asciiEncoding = (Encoding) new ASCIIEncoding();
        return Encoding.asciiEncoding;
      }
    }

    private static Encoding Latin1
    {
      get
      {
        if (Encoding.latin1Encoding == null)
          Encoding.latin1Encoding = (Encoding) new Latin1Encoding();
        return Encoding.latin1Encoding;
      }
    }

    /// <summary>在派生类中重写时，获取当前 <see cref="T:System.Text.Encoding" /> 的代码页标识符。</summary>
    /// <returns>当前 <see cref="T:System.Text.Encoding" /> 的代码页标识符。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int CodePage
    {
      [__DynamicallyInvokable] get
      {
        return this.m_codePage;
      }
    }

    /// <summary>获取操作系统的当前 ANSI 代码页的编码。</summary>
    /// <returns>操作系统的当前 ANSI 代码页的编码。</returns>
    /// <filterpriority>1</filterpriority>
    public static Encoding Default
    {
      [SecuritySafeCritical] get
      {
        if (Encoding.defaultEncoding == null)
          Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
        return Encoding.defaultEncoding;
      }
    }

    /// <summary>获取使用 Little-Endian 字节顺序的 UTF-16 格式的编码。</summary>
    /// <returns>使用 Little-Endian 字节顺序的 UTF-16 格式的编码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding Unicode
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.unicodeEncoding == null)
          Encoding.unicodeEncoding = (Encoding) new UnicodeEncoding(false, true);
        return Encoding.unicodeEncoding;
      }
    }

    /// <summary>获取使用 Big Endian 字节顺序的 UTF-16 格式的编码。</summary>
    /// <returns>使用 Big Endian 字节顺序的 UTF-16 格式的编码对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding BigEndianUnicode
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.bigEndianUnicode == null)
          Encoding.bigEndianUnicode = (Encoding) new UnicodeEncoding(true, true);
        return Encoding.bigEndianUnicode;
      }
    }

    /// <summary>获取 UTF-7 格式的编码。</summary>
    /// <returns>UTF-7 格式的编码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding UTF7
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.utf7Encoding == null)
          Encoding.utf7Encoding = (Encoding) new UTF7Encoding();
        return Encoding.utf7Encoding;
      }
    }

    /// <summary>获取 UTF-8 格式的编码。</summary>
    /// <returns>UTF-8 格式的编码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding UTF8
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.utf8Encoding == null)
          Encoding.utf8Encoding = (Encoding) new UTF8Encoding(true);
        return Encoding.utf8Encoding;
      }
    }

    /// <summary>获取使用 Little-Endian 字节顺序的 UTF-32 格式的编码。</summary>
    /// <returns>使用 Little-Endian 字节顺序的 UTF-32 格式的编码对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding UTF32
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.utf32Encoding == null)
          Encoding.utf32Encoding = (Encoding) new UTF32Encoding(false, true);
        return Encoding.utf32Encoding;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.Encoding" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Encoding()
      : this(0)
    {
    }

    /// <summary>初始化对应于指定代码页的 <see cref="T:System.Text.Encoding" /> 类的新实例。</summary>
    /// <param name="codePage">首选编码的代码页标识符。- 或 -0，使用默认编码。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="codePage" /> 小于零。</exception>
    [__DynamicallyInvokable]
    protected Encoding(int codePage)
    {
      if (codePage < 0)
        throw new ArgumentOutOfRangeException("codePage");
      this.m_codePage = codePage;
      this.SetDefaultFallbacks();
    }

    /// <summary>初始化的新实例<see cref="T:System.Text.Encoding" />对应于与指定编码器和解码器回退策略指定的代码页的类。</summary>
    /// <param name="codePage">编码的代码页标识符。</param>
    /// <param name="encoderFallback">一个对象，在无法用当前编码对字符进行编码时，该对象可用来提供错误处理过程。</param>
    /// <param name="decoderFallback">一个对象，在无法用当前编码对字节序列进行解码时，该对象可用来提供错误处理过程。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="codePage" /> 小于零。</exception>
    [__DynamicallyInvokable]
    protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      if (codePage < 0)
        throw new ArgumentOutOfRangeException("codePage");
      this.m_codePage = codePage;
      this.encoderFallback = encoderFallback ?? (EncoderFallback) new InternalEncoderBestFitFallback(this);
      this.decoderFallback = decoderFallback ?? (DecoderFallback) new InternalDecoderBestFitFallback(this);
    }

    internal virtual void SetDefaultFallbacks()
    {
      this.encoderFallback = (EncoderFallback) new InternalEncoderBestFitFallback(this);
      this.decoderFallback = (DecoderFallback) new InternalDecoderBestFitFallback(this);
    }

    internal void OnDeserializing()
    {
      this.encoderFallback = (EncoderFallback) null;
      this.decoderFallback = (DecoderFallback) null;
      this.m_isReadOnly = true;
    }

    internal void OnDeserialized()
    {
      if (this.encoderFallback == null || this.decoderFallback == null)
      {
        this.m_deserializedFromEverett = true;
        this.SetDefaultFallbacks();
      }
      this.dataItem = (CodePageDataItem) null;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.OnDeserializing();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.OnDeserialized();
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.dataItem = (CodePageDataItem) null;
    }

    internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.m_codePage = (int) info.GetValue("m_codePage", typeof (int));
      this.dataItem = (CodePageDataItem) null;
      try
      {
        this.m_isReadOnly = (bool) info.GetValue("m_isReadOnly", typeof (bool));
        this.encoderFallback = (EncoderFallback) info.GetValue("encoderFallback", typeof (EncoderFallback));
        this.decoderFallback = (DecoderFallback) info.GetValue("decoderFallback", typeof (DecoderFallback));
      }
      catch (SerializationException ex)
      {
        this.m_deserializedFromEverett = true;
        this.m_isReadOnly = true;
        this.SetDefaultFallbacks();
      }
    }

    internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("m_isReadOnly", this.m_isReadOnly);
      info.AddValue("encoderFallback", (object) this.EncoderFallback);
      info.AddValue("decoderFallback", (object) this.DecoderFallback);
      info.AddValue("m_codePage", this.m_codePage);
      info.AddValue("dataItem", (object) null);
      info.AddValue("Encoding+m_codePage", this.m_codePage);
      info.AddValue("Encoding+dataItem", (object) null);
    }

    /// <summary>将整个字节数组从一种编码转换为另一种编码。</summary>
    /// <returns>
    /// <see cref="T:System.Byte" /> 类型的数组包含将 <paramref name="bytes" /> 从 <paramref name="srcEncoding" /> 转换为 <paramref name="dstEncoding" /> 的结果。</returns>
    /// <param name="srcEncoding">
    /// <paramref name="bytes" /> 的编码格式。</param>
    /// <param name="dstEncoding">目标编码格式。</param>
    /// <param name="bytes">要转换的字节。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="srcEncoding" /> 为 null。- 或 - <paramref name="dstEncoding" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－srcEncoding。将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－dstEncoding。将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
    }

    /// <summary>将字节数组内某个范围的字节从一种编码转换为另一种编码。</summary>
    /// <returns>一个 <see cref="T:System.Byte" /> 类型的数组，其中包含将 <paramref name="bytes" /> 中某个范围的字节从 <paramref name="srcEncoding" /> 转换为 <paramref name="dstEncoding" /> 的结果。</returns>
    /// <param name="srcEncoding">源数组 <paramref name="bytes" /> 的编码。</param>
    /// <param name="dstEncoding">输出数组的编码。</param>
    /// <param name="bytes">要转换的字节数组。</param>
    /// <param name="index">要转换的 <paramref name="bytes" /> 中第一个元素的索引。</param>
    /// <param name="count">要转换的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="srcEncoding" /> 为 null。- 或 - <paramref name="dstEncoding" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 和 <paramref name="count" /> 不指定字节数组中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－srcEncoding。将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－dstEncoding。将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
    {
      if (srcEncoding == null || dstEncoding == null)
        throw new ArgumentNullException(srcEncoding == null ? "srcEncoding" : "dstEncoding", Environment.GetResourceString("ArgumentNull_Array"));
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
    }

    /// <summary>注册编码的提供程序。</summary>
    /// <param name="provider">子类<see cref="T:System.Text.EncodingProvider" />提供对其他字符编码的访问。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="provider" /> 为 null。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void RegisterProvider(EncodingProvider provider)
    {
      EncodingProvider.AddProvider(provider);
    }

    /// <summary>返回与指定代码页标识符关联的编码。</summary>
    /// <returns>与指定代码页关联的编码。</returns>
    /// <param name="codepage">首选编码的代码页标识符。可能的值都在 <see cref="T:System.Text.Encoding" /> 类主题中出现的表的“代码页”的一列中列了出来。- 或 -0（零），使用默认编码。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="codepage" /> 小于零或大于 65535。 </exception>
    /// <exception cref="T:System.ArgumentException">基础平台不支持 <paramref name="codepage" />。 </exception>
    /// <exception cref="T:System.NotSupportedException">基础平台不支持 <paramref name="codepage" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(int codepage)
    {
      Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
      if (encoding != null)
        return encoding;
      if (codepage < 0 || codepage > (int) ushort.MaxValue)
        throw new ArgumentOutOfRangeException("codepage", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) (int) ushort.MaxValue));
      if (Encoding.encodings != null)
        encoding = (Encoding) Encoding.encodings[(object) codepage];
      if (encoding == null)
      {
        lock (Encoding.InternalSyncObject)
        {
          if (Encoding.encodings == null)
            Encoding.encodings = new Hashtable();
          if ((encoding = (Encoding) Encoding.encodings[(object) codepage]) != null)
            return encoding;
          switch (codepage)
          {
            case 28591:
              encoding = Encoding.Latin1;
              break;
            case 65001:
              encoding = Encoding.UTF8;
              break;
            case 1252:
              encoding = (Encoding) new SBCSCodePageEncoding(codepage);
              break;
            case 20127:
              encoding = Encoding.ASCII;
              break;
            case 1200:
              encoding = Encoding.Unicode;
              break;
            case 1201:
              encoding = Encoding.BigEndianUnicode;
              break;
            case 0:
              encoding = Encoding.Default;
              break;
            case 1:
            case 2:
            case 3:
            case 42:
              throw new ArgumentException(Environment.GetResourceString("Argument_CodepageNotSupported", (object) codepage), "codepage");
            default:
              encoding = Encoding.GetEncodingCodePage(codepage) ?? Encoding.GetEncodingRare(codepage);
              break;
          }
          Encoding.encodings.Add((object) codepage, (object) encoding);
        }
      }
      return encoding;
    }

    /// <summary>返回与指定代码页标识符关联的编码。参数指定一个错误处理程序，用于处理无法编码的字符和无法解码的字节序列。</summary>
    /// <returns>与指定代码页关联的编码。</returns>
    /// <param name="codepage">首选编码的代码页标识符。可能的值都在 <see cref="T:System.Text.Encoding" /> 类主题中出现的表的“代码页”的一列中列了出来。- 或 -0（零），使用默认编码。</param>
    /// <param name="encoderFallback">一个对象，在无法用当前编码对字符进行编码时，该对象可用来提供错误处理过程。</param>
    /// <param name="decoderFallback">一个对象，在无法用当前编码对字节序列进行解码时，该对象可用来提供错误处理过程。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="codepage" /> 小于零或大于 65535。 </exception>
    /// <exception cref="T:System.ArgumentException">基础平台不支持 <paramref name="codepage" />。 </exception>
    /// <exception cref="T:System.NotSupportedException">基础平台不支持 <paramref name="codepage" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
      if (encodingFromProvider != null)
        return encodingFromProvider;
      Encoding encoding = (Encoding) Encoding.GetEncoding(codepage).Clone();
      EncoderFallback encoderFallback1 = encoderFallback;
      encoding.EncoderFallback = encoderFallback1;
      DecoderFallback decoderFallback1 = decoderFallback;
      encoding.DecoderFallback = decoderFallback1;
      return encoding;
    }

    [SecurityCritical]
    private static Encoding GetEncodingRare(int codepage)
    {
      switch (codepage)
      {
        case 57002:
        case 57003:
        case 57004:
        case 57005:
        case 57006:
        case 57007:
        case 57008:
        case 57009:
        case 57010:
        case 57011:
          return (Encoding) new ISCIIEncoding(codepage);
        case 65000:
          return Encoding.UTF7;
        case 52936:
        case 50220:
        case 50221:
        case 50222:
        case 50225:
          return (Encoding) new ISO2022Encoding(codepage);
        case 54936:
          return (Encoding) new GB18030Encoding();
        case 51932:
          return (Encoding) new EUCJPEncoding();
        case 51936:
        case 50227:
          return (Encoding) new DBCSCodePageEncoding(codepage, 936);
        case 51949:
          return (Encoding) new DBCSCodePageEncoding(codepage, 20949);
        case 12001:
          return (Encoding) new UTF32Encoding(true, true);
        case 38598:
          return (Encoding) new SBCSCodePageEncoding(codepage, 28598);
        case 50229:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_CodePage50229"));
        case 10003:
          return (Encoding) new DBCSCodePageEncoding(10003, 20949);
        case 10008:
          return (Encoding) new DBCSCodePageEncoding(10008, 20936);
        case 12000:
          return Encoding.UTF32;
        default:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) codepage));
      }
    }

    [SecurityCritical]
    private static Encoding GetEncodingCodePage(int CodePage)
    {
      switch (BaseCodePageEncoding.GetCodePageByteSize(CodePage))
      {
        case 1:
          return (Encoding) new SBCSCodePageEncoding(CodePage);
        case 2:
          return (Encoding) new DBCSCodePageEncoding(CodePage);
        default:
          return (Encoding) null;
      }
    }

    /// <summary>返回与指定代码页名称关联的编码。</summary>
    /// <returns>与指定代码页关联的编码。</returns>
    /// <param name="name">首选编码的代码页名称。<see cref="P:System.Text.Encoding.WebName" /> 属性返回的值是有效的。可能的值都在 <see cref="T:System.Text.Encoding" /> 类主题中出现的表的“名称”一列中列了出来。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 不是有效的代码页名称。- 或 -基础平台不支持 <paramref name="name" /> 所指示的代码页。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(string name)
    {
      return EncodingProvider.GetEncodingFromProvider(name) ?? Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
    }

    /// <summary>返回与指定代码页名称关联的编码。参数指定一个错误处理程序，用于处理无法编码的字符和无法解码的字节序列。</summary>
    /// <returns>与指定代码页关联的编码。</returns>
    /// <param name="name">首选编码的代码页名称。<see cref="P:System.Text.Encoding.WebName" /> 属性返回的值是有效的。可能的值都在 <see cref="T:System.Text.Encoding" /> 类主题中出现的表的“名称”一列中列了出来。</param>
    /// <param name="encoderFallback">一个对象，在无法用当前编码对字符进行编码时，该对象可用来提供错误处理过程。</param>
    /// <param name="decoderFallback">一个对象，在无法用当前编码对字节序列进行解码时，该对象可用来提供错误处理过程。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 不是有效的代码页名称。- 或 -基础平台不支持 <paramref name="name" /> 所指示的代码页。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      return EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback) ?? Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
    }

    /// <summary>返回包含所有编码的数组。</summary>
    /// <returns>包含所有编码的数组。</returns>
    /// <filterpriority>1</filterpriority>
    public static EncodingInfo[] GetEncodings()
    {
      return EncodingTable.GetEncodings();
    }

    /// <summary>在派生类中重写时，返回指定所用编码的字节序列。</summary>
    /// <returns>一个字节数组，包含指定所用编码的字节序列。- 或 -长度为零的字节数组（如果不需要前导码）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte[] GetPreamble()
    {
      return EmptyArray<byte>.Value;
    }

    private void GetDataItem()
    {
      if (this.dataItem != null)
        return;
      this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
      if (this.dataItem == null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.m_codePage));
    }

    /// <summary>当在派生类中重写时，创建当前 <see cref="T:System.Text.Encoding" /> 对象的一个浅表副本。</summary>
    /// <returns>当前 <see cref="T:System.Text.Encoding" /> 对象的副本。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual object Clone()
    {
      Encoding encoding = (Encoding) this.MemberwiseClone();
      int num = 0;
      encoding.m_isReadOnly = num != 0;
      return (object) encoding;
    }

    /// <summary>在派生类中重写时，计算对指定字符数组中的所有字符进行编码所产生的字节数。</summary>
    /// <returns>对指定字符数组中的所有字符进行编码后产生的字节数。</returns>
    /// <param name="chars">包含要编码的字符的字符数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetByteCount(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetByteCount(chars, 0, chars.Length);
    }

    /// <summary>在派生类中重写时，计算对指定字符串中的字符进行编码所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="s">包含要编码的字符集的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetByteCount(string s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      char[] charArray = s.ToCharArray();
      return this.GetByteCount(charArray, 0, charArray.Length);
    }

    /// <summary>在派生类中重写时，计算对指定字符数组中的一组字符进行编码所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="index">第一个要编码的字符的索引。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="chars" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetByteCount(char[] chars, int index, int count);

    /// <summary>在派生类中重写时，计算对一组字符（从指定的字符指针处开始）进行编码所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetByteCount(char* chars, int count)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[count];
      for (int index = 0; index < count; ++index)
        chars1[index] = chars[index];
      return this.GetByteCount(chars1, 0, count);
    }

    [SecurityCritical]
    internal virtual unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      return this.GetByteCount(chars, count);
    }

    /// <summary>在派生类中重写时，将指定字符数组中的所有字符编码为一个字节序列。</summary>
    /// <returns>一个字节数组，包含对指定的字符集进行编码的结果。</returns>
    /// <param name="chars">包含要编码的字符的字符数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte[] GetBytes(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetBytes(chars, 0, chars.Length);
    }

    /// <summary>在派生类中重写时，将指定字符数组中的一组字符编码为一个字节序列。</summary>
    /// <returns>一个字节数组，包含对指定的字符集进行编码的结果。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="index">第一个要编码的字符的索引。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="chars" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte[] GetBytes(char[] chars, int index, int count)
    {
      byte[] bytes = new byte[this.GetByteCount(chars, index, count)];
      this.GetBytes(chars, index, count, bytes, 0);
      return bytes;
    }

    /// <summary>在派生类中重写时，将指定字符数组中的一组字符编码为指定的字节数组。</summary>
    /// <returns>写入 <paramref name="bytes" /> 的实际字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">要开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 - <paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 - <paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

    /// <summary>在派生类中重写时，将指定字符串中的所有字符编码为一个字节序列。</summary>
    /// <returns>一个字节数组，包含对指定的字符集进行编码的结果。</returns>
    /// <param name="s">包含要编码的字符的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual byte[] GetBytes(string s)
    {
      if (s == null)
        throw new ArgumentNullException("s", Environment.GetResourceString("ArgumentNull_String"));
      byte[] bytes = new byte[this.GetByteCount(s)];
      this.GetBytes(s, 0, s.Length, bytes, 0);
      return bytes;
    }

    /// <summary>在派生类中重写时，将指定字符串中的一组字符编码为指定的字节数组。</summary>
    /// <returns>写入 <paramref name="bytes" /> 的实际字节数。</returns>
    /// <param name="s">包含要编码的字符集的字符串。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">要开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 - <paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 - <paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
    }

    [SecurityCritical]
    internal virtual unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      return this.GetBytes(chars, charCount, bytes, byteCount);
    }

    /// <summary>在派生类中重写时，将一组字符（从指定的字符指针开始）编码为一个字节序列，并从指定的字节指针开始存储该字节序列。</summary>
    /// <returns>在由 <paramref name="bytes" /> 参数指示的位置处写入的实际字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">一个指针，指向开始写入所产生的字节序列的位置。</param>
    /// <param name="byteCount">最多写入的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="byteCount" /> 少于所产生的字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[charCount];
      for (int index = 0; index < charCount; ++index)
        chars1[index] = chars[index];
      byte[] bytes1 = new byte[byteCount];
      int bytes2 = this.GetBytes(chars1, 0, charCount, bytes1, 0);
      if (bytes2 < byteCount)
        byteCount = bytes2;
      for (int index = 0; index < byteCount; ++index)
        bytes[index] = bytes1[index];
      return byteCount;
    }

    /// <summary>在派生类中重写时，计算对指定字节数组中的所有字节进行解码所产生的字符数。</summary>
    /// <returns>对指定字节序列进行解码所产生的字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetCharCount(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetCharCount(bytes, 0, bytes.Length);
    }

    /// <summary>在派生类中重写时，计算对字节序列（从指定字节数组开始）进行解码所产生的字符数。</summary>
    /// <returns>对指定字节序列进行解码所产生的字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetCharCount(byte[] bytes, int index, int count);

    /// <summary>在派生类中重写时，计算对字节序列（从指定的字节指针开始）进行解码所产生的字符数。</summary>
    /// <returns>对指定字节序列进行解码所产生的字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetCharCount(byte* bytes, int count)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] bytes1 = new byte[count];
      for (int index = 0; index < count; ++index)
        bytes1[index] = bytes[index];
      return this.GetCharCount(bytes1, 0, count);
    }

    [SecurityCritical]
    internal virtual unsafe int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
    {
      return this.GetCharCount(bytes, count);
    }

    /// <summary>在派生类中重写时，将指定字节数组中的所有字节解码为一组字符。</summary>
    /// <returns>一个字节数组，包含对指定的字节序列进行解码的结果。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual char[] GetChars(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetChars(bytes, 0, bytes.Length);
    }

    /// <summary>在派生类中重写时，将指定字节数组中的一个字节序列解码为一组字符。</summary>
    /// <returns>一个字节数组，包含对指定的字节序列进行解码的结果。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual char[] GetChars(byte[] bytes, int index, int count)
    {
      char[] chars = new char[this.GetCharCount(bytes, index, count)];
      this.GetChars(bytes, index, count, chars, 0);
      return chars;
    }

    /// <summary>在派生类中重写时，将指定字节数组中的字节序列解码为指定的字符数组。</summary>
    /// <returns>写入 <paramref name="chars" /> 的实际字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="byteIndex">第一个要解码的字节的索引。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">要用于包含所产生的字符集的字符数组。</param>
    /// <param name="charIndex">开始写入所产生的字符集的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。- 或 - <paramref name="chars" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteIndex" />、<paramref name="byteCount" /> 或 <paramref name="charIndex" /> 小于零。- 或 - <paramref name="byteindex" /> 和 <paramref name="byteCount" /> 不表示 <paramref name="bytes" /> 中的有效范围。- 或 - <paramref name="charIndex" /> 不是 <paramref name="chars" /> 中的有效索引。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="chars" /> 中从 <paramref name="charIndex" /> 到数组结尾没有足够容量来容纳所产生的字符。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

    /// <summary>在派生类中重写时，将一个字节序列（从指定的字节指针开始）解码为一组字符，并从指定的字符指针开始存储该组字符。</summary>
    /// <returns>在由 <paramref name="chars" /> 参数指示的位置处写入的实际字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">一个指针，指向开始写入所产生的字符集的位置。</param>
    /// <param name="charCount">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。- 或 - <paramref name="chars" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 或 <paramref name="charCount" /> 小于零。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="charCount" /> 少于所产生的字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] bytes1 = new byte[byteCount];
      for (int index = 0; index < byteCount; ++index)
        bytes1[index] = bytes[index];
      char[] chars1 = new char[charCount];
      int chars2 = this.GetChars(bytes1, 0, byteCount, chars1, 0);
      if (chars2 < charCount)
        charCount = chars2;
      for (int index = 0; index < charCount; ++index)
        chars[index] = chars1[index];
      return charCount;
    }

    [SecurityCritical]
    internal virtual unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      return this.GetChars(bytes, byteCount, chars, charCount);
    }

    /// <summary>当在派生类中重写，解码指定的指定地址处开始转换为字符串的字节数。</summary>
    /// <returns>包含指定字节序列解码结果的字符串。 </returns>
    /// <param name="bytes">指向字节数组的指针。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" />为 null 指针。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅.NET Framework 中的字符编码有关的完整说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public unsafe string GetString(byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return string.CreateStringFromEncoding(bytes, byteCount, this);
    }

    /// <summary>使用默认范式获取一个值，该值指示当前编码是否始终被规范化。</summary>
    /// <returns>如果当前 <see cref="T:System.Text.Encoding" /> 始终被规范化，则为 true；否则，为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public bool IsAlwaysNormalized()
    {
      return this.IsAlwaysNormalized(NormalizationForm.FormC);
    }

    /// <summary>在派生类中重写时，使用指定范式获取一个值，该值指示当前编码是否始终被规范化。</summary>
    /// <returns>如果始终使用指定的 <see cref="T:System.Text.NormalizationForm" /> 值规范化当前的 <see cref="T:System.Text.Encoding" /> 对象，则为 true；否则为 false。默认值为 false。</returns>
    /// <param name="form">
    /// <see cref="T:System.Text.NormalizationForm" /> 值之一。 </param>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public virtual bool IsAlwaysNormalized(NormalizationForm form)
    {
      return false;
    }

    /// <summary>在派生类中重写时，获取一个解码器，该解码器将已编码的字节序列转换为字符序列。</summary>
    /// <returns>一个 <see cref="T:System.Text.Decoder" />，它将已编码的字节序列转换为字符序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual Decoder GetDecoder()
    {
      return (Decoder) new Encoding.DefaultDecoder(this);
    }

    [SecurityCritical]
    private static Encoding CreateDefaultEncoding()
    {
      int acp = Win32Native.GetACP();
      return acp != 1252 ? Encoding.GetEncoding(acp) : (Encoding) new SBCSCodePageEncoding(acp);
    }

    /// <summary>在派生类中重写时，获取一个解码器，该解码器将 Unicode 字符序列转换为已编码的字节序列。</summary>
    /// <returns>一个 <see cref="T:System.Text.Encoder" />，它将 Unicode 字符序列转换为已编码的字节序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual Encoder GetEncoder()
    {
      return (Encoder) new Encoding.DefaultEncoder(this);
    }

    /// <summary>在派生类中重写时，计算对指定数目的字符进行编码所产生的最大字节数。</summary>
    /// <returns>对指定数目的字符进行编码所产生的最大字节数。</returns>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 小于零。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetMaxByteCount(int charCount);

    /// <summary>在派生类中重写时，计算对指定数目的字节进行解码时所产生的最大字符数。</summary>
    /// <returns>对指定数目的字节进行解码时所产生的最大字符数。</returns>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetMaxCharCount(int byteCount);

    /// <summary>在派生类中重写时，将指定字节数组中的所有字节解码为一个字符串。</summary>
    /// <returns>包含指定字节序列解码结果的字符串。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <exception cref="T:System.ArgumentException">字节数组中包含无效的 Unicode 码位。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual string GetString(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetString(bytes, 0, bytes.Length);
    }

    /// <summary>在派生类中重写时，将指定字节数组中的一个字节序列解码为一个字符串。</summary>
    /// <returns>包含指定字节序列解码结果的字符串。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentException">字节数组中包含无效的 Unicode 码位。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual string GetString(byte[] bytes, int index, int count)
    {
      return new string(this.GetChars(bytes, index, count));
    }

    /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前实例。</summary>
    /// <returns>如果 <paramref name="value" /> 是 <see cref="T:System.Text.Encoding" /> 的一个实例并且等于当前实例，则为 true；否则，为 false。</returns>
    /// <param name="value">与当前实例进行比较的 <see cref="T:System.Object" />。 </param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      Encoding encoding = value as Encoding;
      if (encoding != null && this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals((object) encoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) encoding.DecoderFallback);
      return false;
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>当前实例的哈希代码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
    }

    internal virtual char[] GetBestFitUnicodeToBytesData()
    {
      return EmptyArray<char>.Value;
    }

    internal virtual char[] GetBestFitBytesToUnicodeData()
    {
      return EmptyArray<char>.Value;
    }

    internal void ThrowBytesOverflow()
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowBytes", (object) this.EncodingName, (object) this.EncoderFallback.GetType()), "bytes");
    }

    [SecurityCritical]
    internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
    {
      if (((encoder == null ? 1 : (encoder.m_throwOnOverflow ? 1 : 0)) | (nothingEncoded ? 1 : 0)) != 0)
      {
        if (encoder != null && encoder.InternalHasFallbackBuffer)
          encoder.FallbackBuffer.InternalReset();
        this.ThrowBytesOverflow();
      }
      encoder.ClearMustFlush();
    }

    internal void ThrowCharsOverflow()
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowChars", (object) this.EncodingName, (object) this.DecoderFallback.GetType()), "chars");
    }

    [SecurityCritical]
    internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
    {
      if (((decoder == null ? 1 : (decoder.m_throwOnOverflow ? 1 : 0)) | (nothingDecoded ? 1 : 0)) != 0)
      {
        if (decoder != null && decoder.InternalHasFallbackBuffer)
          decoder.FallbackBuffer.InternalReset();
        this.ThrowCharsOverflow();
      }
      decoder.ClearMustFlush();
    }

    [Serializable]
    internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
    {
      private Encoding m_encoding;
      [NonSerialized]
      private bool m_hasInitializedEncoding;
      [NonSerialized]
      internal char charLeftOver;

      public DefaultEncoder(Encoding encoding)
      {
        this.m_encoding = encoding;
        this.m_hasInitializedEncoding = true;
      }

      internal DefaultEncoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        try
        {
          this.m_fallback = (EncoderFallback) info.GetValue("m_fallback", typeof (EncoderFallback));
          this.charLeftOver = (char) info.GetValue("charLeftOver", typeof (char));
        }
        catch (SerializationException ex)
        {
        }
      }

      [SecurityCritical]
      public object GetRealObject(StreamingContext context)
      {
        if (this.m_hasInitializedEncoding)
          return (object) this;
        Encoder encoder = this.m_encoding.GetEncoder();
        if (this.m_fallback != null)
          encoder.m_fallback = this.m_fallback;
        if ((int) this.charLeftOver != 0)
        {
          EncoderNLS encoderNls = encoder as EncoderNLS;
          if (encoderNls != null)
            encoderNls.charLeftOver = this.charLeftOver;
        }
        return (object) encoder;
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("encoding", (object) this.m_encoding);
      }

      public override int GetByteCount(char[] chars, int index, int count, bool flush)
      {
        return this.m_encoding.GetByteCount(chars, index, count);
      }

      [SecurityCritical]
      public override unsafe int GetByteCount(char* chars, int count, bool flush)
      {
        return this.m_encoding.GetByteCount(chars, count);
      }

      public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
      {
        return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
      }

      [SecurityCritical]
      public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
      {
        return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
      }
    }

    [Serializable]
    internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
    {
      private Encoding m_encoding;
      [NonSerialized]
      private bool m_hasInitializedEncoding;

      public DefaultDecoder(Encoding encoding)
      {
        this.m_encoding = encoding;
        this.m_hasInitializedEncoding = true;
      }

      internal DefaultDecoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        try
        {
          this.m_fallback = (DecoderFallback) info.GetValue("m_fallback", typeof (DecoderFallback));
        }
        catch (SerializationException ex)
        {
          this.m_fallback = (DecoderFallback) null;
        }
      }

      [SecurityCritical]
      public object GetRealObject(StreamingContext context)
      {
        if (this.m_hasInitializedEncoding)
          return (object) this;
        Decoder decoder = this.m_encoding.GetDecoder();
        if (this.m_fallback != null)
          decoder.m_fallback = this.m_fallback;
        return (object) decoder;
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("encoding", (object) this.m_encoding);
      }

      public override int GetCharCount(byte[] bytes, int index, int count)
      {
        return this.GetCharCount(bytes, index, count, false);
      }

      public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
      {
        return this.m_encoding.GetCharCount(bytes, index, count);
      }

      [SecurityCritical]
      public override unsafe int GetCharCount(byte* bytes, int count, bool flush)
      {
        return this.m_encoding.GetCharCount(bytes, count);
      }

      public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
      {
        return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
      }

      public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
      {
        return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
      }

      [SecurityCritical]
      public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
      {
        return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
      }
    }

    internal class EncodingCharBuffer
    {
      [SecurityCritical]
      private unsafe char* chars;
      [SecurityCritical]
      private unsafe char* charStart;
      [SecurityCritical]
      private unsafe char* charEnd;
      private int charCountResult;
      private Encoding enc;
      private DecoderNLS decoder;
      [SecurityCritical]
      private unsafe byte* byteStart;
      [SecurityCritical]
      private unsafe byte* byteEnd;
      [SecurityCritical]
      private unsafe byte* bytes;
      private DecoderFallbackBuffer fallbackBuffer;

      internal unsafe bool MoreData
      {
        [SecurityCritical] get
        {
          return this.bytes < this.byteEnd;
        }
      }

      internal unsafe int BytesUsed
      {
        [SecurityCritical] get
        {
          return (int) (this.bytes - this.byteStart);
        }
      }

      internal int Count
      {
        get
        {
          return this.charCountResult;
        }
      }

      [SecurityCritical]
      internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
      {
        this.enc = enc;
        this.decoder = decoder;
        this.chars = charStart;
        this.charStart = charStart;
        this.charEnd = charStart + charCount;
        this.byteStart = byteStart;
        this.bytes = byteStart;
        this.byteEnd = byteStart + byteCount;
        this.fallbackBuffer = this.decoder != null ? this.decoder.FallbackBuffer : enc.DecoderFallback.CreateFallbackBuffer();
        this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
      }

      [SecurityCritical]
      internal unsafe bool AddChar(char ch, int numBytes)
      {
        if ((IntPtr) this.chars != IntPtr.Zero)
        {
          if (this.chars >= this.charEnd)
          {
            this.bytes = this.bytes - numBytes;
            this.enc.ThrowCharsOverflow(this.decoder, this.bytes <= this.byteStart);
            return false;
          }
          char* chPtr = this.chars;
          this.chars = (char*) ((IntPtr) chPtr + 2);
          *chPtr = ch;
        }
        this.charCountResult = this.charCountResult + 1;
        return true;
      }

      [SecurityCritical]
      internal bool AddChar(char ch)
      {
        return this.AddChar(ch, 1);
      }

      [SecurityCritical]
      internal unsafe bool AddChar(char ch1, char ch2, int numBytes)
      {
        if ((UIntPtr) this.chars >= (UIntPtr) this.charEnd - new UIntPtr(2))
        {
          this.bytes = this.bytes - numBytes;
          this.enc.ThrowCharsOverflow(this.decoder, this.bytes <= this.byteStart);
          return false;
        }
        if (this.AddChar(ch1, numBytes))
          return this.AddChar(ch2, numBytes);
        return false;
      }

      [SecurityCritical]
      internal unsafe void AdjustBytes(int count)
      {
        this.bytes = this.bytes + count;
      }

      [SecurityCritical]
      internal unsafe bool EvenMoreData(int count)
      {
        return this.bytes <= this.byteEnd - count;
      }

      [SecurityCritical]
      internal unsafe byte GetNextByte()
      {
        if (this.bytes >= this.byteEnd)
          return 0;
        byte* numPtr = this.bytes;
        this.bytes = numPtr + 1;
        return *numPtr;
      }

      [SecurityCritical]
      internal bool Fallback(byte fallbackByte)
      {
        return this.Fallback(new byte[1]{ fallbackByte });
      }

      [SecurityCritical]
      internal bool Fallback(byte byte1, byte byte2)
      {
        return this.Fallback(new byte[2]{ byte1, byte2 });
      }

      [SecurityCritical]
      internal bool Fallback(byte byte1, byte byte2, byte byte3, byte byte4)
      {
        return this.Fallback(new byte[4]{ byte1, byte2, byte3, byte4 });
      }

      [SecurityCritical]
      internal unsafe bool Fallback(byte[] byteBuffer)
      {
        if ((IntPtr) this.chars != IntPtr.Zero)
        {
          char* chPtr = this.chars;
          if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
          {
            this.bytes = this.bytes - byteBuffer.Length;
            this.fallbackBuffer.InternalReset();
            this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
            return false;
          }
          this.charCountResult = this.charCountResult + (int) (this.chars - chPtr);
        }
        else
          this.charCountResult = this.charCountResult + this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
        return true;
      }
    }

    internal class EncodingByteBuffer
    {
      [SecurityCritical]
      private unsafe byte* bytes;
      [SecurityCritical]
      private unsafe byte* byteStart;
      [SecurityCritical]
      private unsafe byte* byteEnd;
      [SecurityCritical]
      private unsafe char* chars;
      [SecurityCritical]
      private unsafe char* charStart;
      [SecurityCritical]
      private unsafe char* charEnd;
      private int byteCountResult;
      private Encoding enc;
      private EncoderNLS encoder;
      internal EncoderFallbackBuffer fallbackBuffer;

      internal unsafe bool MoreData
      {
        [SecurityCritical] get
        {
          if (this.fallbackBuffer.Remaining <= 0)
            return this.chars < this.charEnd;
          return true;
        }
      }

      internal unsafe int CharsUsed
      {
        [SecurityCritical] get
        {
          return (int) (this.chars - this.charStart);
        }
      }

      internal int Count
      {
        get
        {
          return this.byteCountResult;
        }
      }

      [SecurityCritical]
      internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
      {
        this.enc = inEncoding;
        this.encoder = inEncoder;
        this.charStart = inCharStart;
        this.chars = inCharStart;
        this.charEnd = inCharStart + inCharCount;
        this.bytes = inByteStart;
        this.byteStart = inByteStart;
        this.byteEnd = inByteStart + inByteCount;
        if (this.encoder == null)
        {
          this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
        }
        else
        {
          this.fallbackBuffer = this.encoder.FallbackBuffer;
          if (this.encoder.m_throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.encoder.Encoding.EncodingName, (object) this.encoder.Fallback.GetType()));
        }
        this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, (IntPtr) this.bytes != IntPtr.Zero);
      }

      [SecurityCritical]
      internal unsafe bool AddByte(byte b, int moreBytesExpected)
      {
        if ((IntPtr) this.bytes != IntPtr.Zero)
        {
          if (this.bytes >= this.byteEnd - moreBytesExpected)
          {
            this.MovePrevious(true);
            return false;
          }
          byte* numPtr = this.bytes;
          this.bytes = numPtr + 1;
          *numPtr = b;
        }
        this.byteCountResult = this.byteCountResult + 1;
        return true;
      }

      [SecurityCritical]
      internal bool AddByte(byte b1)
      {
        return this.AddByte(b1, 0);
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2)
      {
        return this.AddByte(b1, b2, 0);
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
      {
        if (this.AddByte(b1, 1 + moreBytesExpected))
          return this.AddByte(b2, moreBytesExpected);
        return false;
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, byte b3)
      {
        return this.AddByte(b1, b2, b3, 0);
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, byte b3, int moreBytesExpected)
      {
        if (this.AddByte(b1, 2 + moreBytesExpected) && this.AddByte(b2, 1 + moreBytesExpected))
          return this.AddByte(b3, moreBytesExpected);
        return false;
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, byte b3, byte b4)
      {
        if (this.AddByte(b1, 3) && this.AddByte(b2, 2) && this.AddByte(b3, 1))
          return this.AddByte(b4, 0);
        return false;
      }

      [SecurityCritical]
      internal unsafe void MovePrevious(bool bThrow)
      {
        if (this.fallbackBuffer.bFallingBack)
          this.fallbackBuffer.MovePrevious();
        else if (this.chars > this.charStart)
          this.chars = (char*) ((IntPtr) this.chars - 2);
        if (!bThrow)
          return;
        this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
      }

      [SecurityCritical]
      internal unsafe bool Fallback(char charFallback)
      {
        return this.fallbackBuffer.InternalFallback(charFallback, ref this.chars);
      }

      [SecurityCritical]
      internal unsafe char GetNextChar()
      {
        char ch = this.fallbackBuffer.InternalGetNextChar();
        if ((int) ch == 0 && this.chars < this.charEnd)
        {
          char* chPtr = this.chars;
          this.chars = (char*) ((IntPtr) chPtr + 2);
          ch = *chPtr;
        }
        return ch;
      }
    }
  }
}
