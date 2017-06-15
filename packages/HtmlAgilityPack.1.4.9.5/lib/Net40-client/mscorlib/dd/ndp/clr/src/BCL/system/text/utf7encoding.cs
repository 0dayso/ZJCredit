// Decompiled with JetBrains decompiler
// Type: System.Text.UTF7Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>表示 Unicode 字符的 UTF-7 编码。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UTF7Encoding : Encoding
  {
    private const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    private const string directChars = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const string optionalChars = "!\"#$%&*;<=>@[]^_`{|}";
    private byte[] base64Bytes;
    private sbyte[] base64Values;
    private bool[] directEncode;
    [OptionalField(VersionAdded = 2)]
    private bool m_allowOptionals;
    private const int UTF7_CODEPAGE = 65000;

    /// <summary>初始化 <see cref="T:System.Text.UTF7Encoding" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public UTF7Encoding()
      : this(false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UTF7Encoding" /> 类的新实例。一个参数指定是否允许可选字符。</summary>
    /// <param name="allowOptionals">如果为 true，则允许指定可选字符；否则为 false。</param>
    [__DynamicallyInvokable]
    public UTF7Encoding(bool allowOptionals)
      : base(65000)
    {
      this.m_allowOptionals = allowOptionals;
      this.MakeTables();
    }

    private void MakeTables()
    {
      this.base64Bytes = new byte[64];
      for (int index = 0; index < 64; ++index)
        this.base64Bytes[index] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[index];
      this.base64Values = new sbyte[128];
      for (int index = 0; index < 128; ++index)
        this.base64Values[index] = (sbyte) -1;
      for (int index = 0; index < 64; ++index)
        this.base64Values[(int) this.base64Bytes[index]] = (sbyte) index;
      this.directEncode = new bool[128];
      int length1 = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Length;
      for (int index = 0; index < length1; ++index)
        this.directEncode[(int) "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[index]] = true;
      if (!this.m_allowOptionals)
        return;
      int length2 = "!\"#$%&*;<=>@[]^_`{|}".Length;
      for (int index = 0; index < length2; ++index)
        this.directEncode[(int) "!\"#$%&*;<=>@[]^_`{|}"[index]] = true;
    }

    internal override void SetDefaultFallbacks()
    {
      this.encoderFallback = (EncoderFallback) new EncoderReplacementFallback(string.Empty);
      this.decoderFallback = (DecoderFallback) new UTF7Encoding.DecoderUTF7Fallback();
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
      if (this.m_deserializedFromEverett)
        this.m_allowOptionals = this.directEncode[(int) "!\"#$%&*;<=>@[]^_`{|}"[0]];
      this.MakeTables();
    }

    /// <summary>获取一个值，该值指示指定的对象是否等于当前的 <see cref="T:System.Text.UTF7Encoding" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 是一个 <see cref="T:System.Text.UTF7Encoding" /> 对象且等于当前的 <see cref="T:System.Text.UTF7Encoding" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="value">要与当前的 <see cref="T:System.Text.UTF7Encoding" /> 对象进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UTF7Encoding utF7Encoding = value as UTF7Encoding;
      if (utF7Encoding != null && this.m_allowOptionals == utF7Encoding.m_allowOptionals && this.EncoderFallback.Equals((object) utF7Encoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) utF7Encoding.DecoderFallback);
      return false;
    }

    /// <summary>返回当前 <see cref="T:System.Text.UTF7Encoding" /> 对象的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.CodePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
    }

    /// <summary>计算对指定字符数组中的一组字符进行编码时产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="index">第一个要编码的字符的索引。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetByteCount(char[] chars, int index, int count)
    {
      if (chars == null)
        throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - index < count)
        throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length == 0)
        return 0;
      fixed (char* chPtr = chars)
        return this.GetByteCount(chPtr + index, count, (EncoderNLS) null);
    }

    /// <summary>计算对指定 <see cref="T:System.String" /> 对象中的字符进行编码时所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="s">包含要编码的字符集的 <see cref="T:System.String" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override unsafe int GetByteCount(string s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      string str = s;
      char* chars = (char*) str;
      if ((IntPtr) chars != IntPtr.Zero)
        chars += RuntimeHelpers.OffsetToStringData;
      return this.GetByteCount(chars, s.Length, (EncoderNLS) null);
    }

    /// <summary>计算对从指定的字符指针开始的一组字符进行编码时产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 是 null （在 Visual Basic .NET 中为 Nothing ）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public override unsafe int GetByteCount(char* chars, int count)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetByteCount(chars, count, (EncoderNLS) null);
    }

    /// <summary>将指定 <see cref="T:System.String" /> 中的一组字符编码到指定的字节数组中。</summary>
    /// <returns>写入 <paramref name="bytes" /> 的实际字节数。</returns>
    /// <param name="s">包含要编码的字符集的 <see cref="T:System.String" />。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null (Nothing)。- 或 -<paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 -<paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 -<paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override unsafe int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (s == null || bytes == null)
        throw new ArgumentNullException(s == null ? "s" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (s.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException("s", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      string str = s;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      fixed (byte* numPtr = bytes)
        return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, (EncoderNLS) null);
    }

    /// <summary>将指定字符数组中的一组字符编码到指定的字节数组中。</summary>
    /// <returns>写入 <paramref name="bytes" /> 的实际字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null (Nothing)。- 或 -<paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 -<paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 -<paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (chars.Length == 0)
        return 0;
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      fixed (char* chPtr = chars)
        fixed (byte* numPtr = bytes)
          return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, (EncoderNLS) null);
    }

    /// <summary>将从指定的字符指针开始的一组字符编码为一个字节序列，并从指定的字节指针开始存储该字节序列。</summary>
    /// <returns>在由 <paramref name="bytes" /> 指示的位置处写入的实际字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">一个指针，指向开始写入所产生的字节序列的位置。</param>
    /// <param name="byteCount">最多写入的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null (Nothing)。- 或 -<paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="byteCount" /> 少于所产生的字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetBytes(chars, charCount, bytes, byteCount, (EncoderNLS) null);
    }

    /// <summary>计算对指定字节数组中的一个字节序列进行解码所产生的字符数。</summary>
    /// <returns>对指定字节序列进行解码所产生的字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。- 或 -产生的字符数超过了作为一个整数返回时允许的最大字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetCharCount(byte[] bytes, int index, int count)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return 0;
      fixed (byte* numPtr = bytes)
        return this.GetCharCount(numPtr + index, count, (DecoderNLS) null);
    }

    /// <summary>计算对一个字节序列（从指定的字节指针开始）进行解码所产生的字符数。</summary>
    /// <returns>对指定字节序列进行解码所产生的字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 -产生的字符数超过了作为一个整数返回时允许的最大字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public override unsafe int GetCharCount(byte* bytes, int count)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetCharCount(bytes, count, (DecoderNLS) null);
    }

    /// <summary>将指定字节数组中的一个字节序列解码为指定的字符数组。</summary>
    /// <returns>写入 <paramref name="chars" /> 的实际字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="byteIndex">第一个要解码的字节的索引。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">要用于包含所产生的字符集的字符数组。</param>
    /// <param name="charIndex">开始写入所产生的字符集的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。- 或 -<paramref name="chars" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteIndex" />、<paramref name="byteCount" /> 或 <paramref name="charIndex" /> 小于零。- 或 -<paramref name="byteindex" /> 和 <paramref name="byteCount" /> 不表示 <paramref name="bytes" /> 中的有效范围。- 或 -<paramref name="charIndex" /> 不是 <paramref name="chars" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="chars" /> 中从 <paramref name="charIndex" /> 到数组结尾没有足够容量来容纳所产生的字符。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      if (bytes == null || chars == null)
        throw new ArgumentNullException(bytes == null ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (charIndex < 0 || charIndex > chars.Length)
        throw new ArgumentOutOfRangeException("charIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (bytes.Length == 0)
        return 0;
      int charCount = chars.Length - charIndex;
      if (chars.Length == 0)
        chars = new char[1];
      fixed (byte* numPtr = bytes)
        fixed (char* chPtr = chars)
          return this.GetChars(numPtr + byteIndex, byteCount, chPtr + charIndex, charCount, (DecoderNLS) null);
    }

    /// <summary>将从指定的字节指针开始的一个字节序列解码为一组字符，并从指定的字符指针开始存储这组字符。</summary>
    /// <returns>在由 <paramref name="chars" /> 指示的位置处写入的实际字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">一个指针，指向开始写入所产生的字符集的位置。</param>
    /// <param name="charCount">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。- 或 -<paramref name="chars" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 或 <paramref name="charCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="charCount" /> 少于所产生的字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetChars(bytes, byteCount, chars, charCount, (DecoderNLS) null);
    }

    /// <summary>将字节数组中某个范围的字节解码为一个字符串。</summary>
    /// <returns>包含指定字节序列解码结果的 <see cref="T:System.String" />。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override unsafe string GetString(byte[] bytes, int index, int count)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return string.Empty;
      fixed (byte* numPtr = bytes)
        return string.CreateStringFromEncoding(numPtr + index, count, (Encoding) this);
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
    {
      return this.GetBytes(chars, count, (byte*) null, 0, baseEncoder);
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
    {
      UTF7Encoding.Encoder encoder = (UTF7Encoding.Encoder) baseEncoder;
      int num1 = 0;
      int num2 = -1;
      Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer((Encoding) this, (EncoderNLS) encoder, bytes, byteCount, chars, charCount);
      if (encoder != null)
      {
        num1 = encoder.bits;
        num2 = encoder.bitCount;
        while (num2 >= 6)
        {
          num2 -= 6;
          if (!encodingByteBuffer.AddByte(this.base64Bytes[num1 >> num2 & 63]))
            this.ThrowBytesOverflow((EncoderNLS) encoder, encodingByteBuffer.Count == 0);
        }
      }
      while (encodingByteBuffer.MoreData)
      {
        char nextChar = encodingByteBuffer.GetNextChar();
        if ((int) nextChar < 128 && this.directEncode[(int) nextChar])
        {
          if (num2 >= 0)
          {
            if (num2 > 0)
            {
              if (encodingByteBuffer.AddByte(this.base64Bytes[num1 << 6 - num2 & 63]))
                num2 = 0;
              else
                break;
            }
            if (encodingByteBuffer.AddByte((byte) 45))
              num2 = -1;
            else
              break;
          }
          if (!encodingByteBuffer.AddByte((byte) nextChar))
            break;
        }
        else if (num2 < 0 && (int) nextChar == 43)
        {
          if (!encodingByteBuffer.AddByte((byte) 43, (byte) 45))
            break;
        }
        else
        {
          if (num2 < 0)
          {
            if (encodingByteBuffer.AddByte((byte) 43))
              num2 = 0;
            else
              break;
          }
          num1 = num1 << 16 | (int) nextChar;
          num2 += 16;
          while (num2 >= 6)
          {
            num2 -= 6;
            if (!encodingByteBuffer.AddByte(this.base64Bytes[num1 >> num2 & 63]))
            {
              num2 += 6;
              encodingByteBuffer.GetNextChar();
              break;
            }
          }
          if (num2 >= 6)
            break;
        }
      }
      if (num2 >= 0 && (encoder == null || encoder.MustFlush))
      {
        if (num2 > 0 && encodingByteBuffer.AddByte(this.base64Bytes[num1 << 6 - num2 & 63]))
          num2 = 0;
        if (encodingByteBuffer.AddByte((byte) 45))
        {
          num1 = 0;
          num2 = -1;
        }
        else
        {
          int num3 = (int) encodingByteBuffer.GetNextChar();
        }
      }
      if ((IntPtr) bytes != IntPtr.Zero && encoder != null)
      {
        encoder.bits = num1;
        encoder.bitCount = num2;
        encoder.m_charsUsed = encodingByteBuffer.CharsUsed;
      }
      return encodingByteBuffer.Count;
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      return this.GetChars(bytes, count, (char*) null, 0, baseDecoder);
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      UTF7Encoding.Decoder decoder = (UTF7Encoding.Decoder) baseDecoder;
      Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer((Encoding) this, (DecoderNLS) decoder, chars, charCount, bytes, byteCount);
      int num1 = 0;
      int num2 = -1;
      bool flag = false;
      if (decoder != null)
      {
        num1 = decoder.bits;
        num2 = decoder.bitCount;
        flag = decoder.firstByte;
      }
      if (num2 >= 16)
      {
        if (!encodingCharBuffer.AddChar((char) (num1 >> num2 - 16 & (int) ushort.MaxValue)))
          this.ThrowCharsOverflow((DecoderNLS) decoder, true);
        num2 -= 16;
      }
      while (encodingCharBuffer.MoreData)
      {
        byte nextByte = encodingCharBuffer.GetNextByte();
        int num3;
        if (num2 >= 0)
        {
          sbyte num4;
          if ((int) nextByte < 128 && (int) (num4 = this.base64Values[(int) nextByte]) >= 0)
          {
            flag = false;
            num1 = num1 << 6 | (int) (byte) num4;
            num2 += 6;
            if (num2 >= 16)
            {
              num3 = num1 >> num2 - 16 & (int) ushort.MaxValue;
              num2 -= 16;
            }
            else
              continue;
          }
          else
          {
            num2 = -1;
            if ((int) nextByte != 45)
            {
              if (encodingCharBuffer.Fallback(nextByte))
                continue;
              break;
            }
            if (flag)
              num3 = 43;
            else
              continue;
          }
        }
        else
        {
          if ((int) nextByte == 43)
          {
            num2 = 0;
            flag = true;
            continue;
          }
          if ((int) nextByte >= 128)
          {
            if (encodingCharBuffer.Fallback(nextByte))
              continue;
            break;
          }
          num3 = (int) nextByte;
        }
        if (num3 >= 0 && !encodingCharBuffer.AddChar((char) num3))
        {
          if (num2 >= 0)
          {
            encodingCharBuffer.AdjustBytes(1);
            num2 += 16;
            break;
          }
          break;
        }
      }
      if ((IntPtr) chars != IntPtr.Zero && decoder != null)
      {
        if (decoder.MustFlush)
        {
          decoder.bits = 0;
          decoder.bitCount = -1;
          decoder.firstByte = false;
        }
        else
        {
          decoder.bits = num1;
          decoder.bitCount = num2;
          decoder.firstByte = flag;
        }
        decoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
      }
      return encodingCharBuffer.Count;
    }

    /// <summary>获取可以将 UTF-7 编码的字节序列转换为 Unicode 字符序列的解码器。</summary>
    /// <returns>
    /// <see cref="T:System.Text.Decoder" /> 用于将 UTF-7 编码的字节序列转换为 Unicode 字符序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override System.Text.Decoder GetDecoder()
    {
      return (System.Text.Decoder) new UTF7Encoding.Decoder(this);
    }

    /// <summary>获取可将 Unicode 字符序列转换为 UTF-7 编码的字节序列的编码器。</summary>
    /// <returns>一个 <see cref="T:System.Text.Encoder" />，用于将 Unicode 字符序列转换为 UTF-7 编码的字节序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override System.Text.Encoder GetEncoder()
    {
      return (System.Text.Encoder) new UTF7Encoding.Encoder(this);
    }

    /// <summary>计算对指定数目的字符进行编码时产生的最大字节数。</summary>
    /// <returns>对指定数目的字符进行编码所产生的最大字节数。</returns>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 小于零。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount * 3L + 2L;
      long num2 = (long) int.MaxValue;
      if (num1 > num2)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num1;
    }

    /// <summary>计算对指定数目的字节进行解码时产生的最大字符数。</summary>
    /// <returns>对指定数目的字节进行解码时所产生的最大字符数。</returns>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 小于零。- 或 -产生的字符数超过了作为一个整数返回时允许的最大字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      int num = byteCount;
      if (num == 0)
        num = 1;
      return num;
    }

    [Serializable]
    private class Decoder : DecoderNLS, ISerializable
    {
      internal int bits;
      internal int bitCount;
      internal bool firstByte;

      internal override bool HasState
      {
        get
        {
          return this.bitCount != -1;
        }
      }

      public Decoder(UTF7Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal Decoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.bits = (int) info.GetValue("bits", typeof (int));
        this.bitCount = (int) info.GetValue("bitCount", typeof (int));
        this.firstByte = (bool) info.GetValue("firstByte", typeof (bool));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("bits", this.bits);
        info.AddValue("bitCount", this.bitCount);
        info.AddValue("firstByte", this.firstByte);
      }

      public override void Reset()
      {
        this.bits = 0;
        this.bitCount = -1;
        this.firstByte = false;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }

    [Serializable]
    private class Encoder : EncoderNLS, ISerializable
    {
      internal int bits;
      internal int bitCount;

      internal override bool HasState
      {
        get
        {
          if (this.bits == 0)
            return this.bitCount != -1;
          return true;
        }
      }

      public Encoder(UTF7Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal Encoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.bits = (int) info.GetValue("bits", typeof (int));
        this.bitCount = (int) info.GetValue("bitCount", typeof (int));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("bits", this.bits);
        info.AddValue("bitCount", this.bitCount);
      }

      public override void Reset()
      {
        this.bitCount = -1;
        this.bits = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }

    [Serializable]
    internal sealed class DecoderUTF7Fallback : DecoderFallback
    {
      public override int MaxCharCount
      {
        get
        {
          return 1;
        }
      }

      public override DecoderFallbackBuffer CreateFallbackBuffer()
      {
        return (DecoderFallbackBuffer) new UTF7Encoding.DecoderUTF7FallbackBuffer(this);
      }

      public override bool Equals(object value)
      {
        return value is UTF7Encoding.DecoderUTF7Fallback;
      }

      public override int GetHashCode()
      {
        return 984;
      }
    }

    internal sealed class DecoderUTF7FallbackBuffer : DecoderFallbackBuffer
    {
      private int iCount = -1;
      private char cFallback;
      private int iSize;

      public override int Remaining
      {
        get
        {
          if (this.iCount <= 0)
            return 0;
          return this.iCount;
        }
      }

      public DecoderUTF7FallbackBuffer(UTF7Encoding.DecoderUTF7Fallback fallback)
      {
      }

      public override bool Fallback(byte[] bytesUnknown, int index)
      {
        this.cFallback = (char) bytesUnknown[0];
        if ((int) this.cFallback == 0)
          return false;
        this.iCount = this.iSize = 1;
        return true;
      }

      public override char GetNextChar()
      {
        int num = this.iCount;
        this.iCount = num - 1;
        if (num > 0)
          return this.cFallback;
        return char.MinValue;
      }

      public override bool MovePrevious()
      {
        if (this.iCount >= 0)
          this.iCount = this.iCount + 1;
        if (this.iCount >= 0)
          return this.iCount <= this.iSize;
        return false;
      }

      [SecuritySafeCritical]
      public override unsafe void Reset()
      {
        this.iCount = -1;
        this.byteStart = (byte*) null;
      }

      [SecurityCritical]
      internal override unsafe int InternalFallback(byte[] bytes, byte* pBytes)
      {
        if (bytes.Length != 1)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
        return (int) bytes[0] != 0 ? 1 : 0;
      }
    }
  }
}
