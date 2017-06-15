// Decompiled with JetBrains decompiler
// Type: System.Text.UnicodeEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>表示 Unicode 字符的 UTF-16 编码。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UnicodeEncoding : Encoding
  {
    internal bool byteOrderMark = true;
    [OptionalField(VersionAdded = 2)]
    internal bool isThrowException;
    internal bool bigEndian;
    /// <summary>表示 Unicode 字符大小（以字节为单位）。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    public const int CharSize = 2;

    /// <summary>初始化 <see cref="T:System.Text.UnicodeEncoding" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public UnicodeEncoding()
      : this(false, true)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UnicodeEncoding" /> 类的新实例。参数指定是否使用 Big-Endian 字节顺序以及 <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> 方法是否返回 Unicode 字节顺序标记。</summary>
    /// <param name="bigEndian">如果为 true，则使用 Big-Endian 字节顺序（从最高有效字节开始）；如果为 false，则使用 Little-Endian 字节顺序（从最低有效字节开始）。</param>
    /// <param name="byteOrderMark">如果为 true，则指定 <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> 方法返回 Unicode 字节顺序标记；否则为 false。有关详细信息，请参阅备注部分。</param>
    [__DynamicallyInvokable]
    public UnicodeEncoding(bool bigEndian, bool byteOrderMark)
      : this(bigEndian, byteOrderMark, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UnicodeEncoding" /> 类的新实例。参数指定是否使用 Big-Endian 字节顺序、是否提供 Unicode 字节顺序标记以及当检测到无效编码时是否引发异常。</summary>
    /// <param name="bigEndian">如果为 true，则使用 Big-Endian 字节顺序（从最高有效字节开始）；如果为 false，则使用 Little-Endian 字节顺序（从最低有效字节开始）。</param>
    /// <param name="byteOrderMark">如果为 true，则指定 <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> 方法返回 Unicode 字节顺序标记；否则为 false。有关详细信息，请参阅备注部分。</param>
    /// <param name="throwOnInvalidBytes">如果为 true，则指定在检测到无效的编码时应当引发异常；否则为 false。</param>
    [__DynamicallyInvokable]
    public UnicodeEncoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidBytes)
      : base(bigEndian ? 1201 : 1200)
    {
      this.isThrowException = throwOnInvalidBytes;
      this.bigEndian = bigEndian;
      this.byteOrderMark = byteOrderMark;
      if (!this.isThrowException)
        return;
      this.SetDefaultFallbacks();
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.isThrowException = false;
    }

    internal override void SetDefaultFallbacks()
    {
      if (this.isThrowException)
      {
        this.encoderFallback = EncoderFallback.ExceptionFallback;
        this.decoderFallback = DecoderFallback.ExceptionFallback;
      }
      else
      {
        this.encoderFallback = (EncoderFallback) new EncoderReplacementFallback("�");
        this.decoderFallback = (DecoderFallback) new DecoderReplacementFallback("�");
      }
    }

    /// <summary>计算对指定字符数组中的一组字符进行编码时产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="index">第一个要编码的字符的索引。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="chars" />。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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

    /// <summary>计算对指定字符串中的字符进行编码时所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="s">包含要编码的字符集的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null 。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="s" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
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
    /// <paramref name="chars" /> 为 null 。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用错误检测和 <paramref name="chars" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <param name="s">包含要编码的字符集的字符串。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">要开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null 。- 或 - <paramref name="bytes" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" /> 或 <paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 - <paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示中的有效范围 <paramref name="chars" />。- 或 - <paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="s" /> 包含无效的字符序列。- 或 - <paramref name="bytes" /> 没有足够的容量从 <paramref name="byteIndex" /> 到要容纳所产生的字节的数组的末尾。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
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
    /// <param name="byteIndex">要开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> is null (Nothing).- 或 - <paramref name="bytes" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" /> 或 <paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 - <paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示中的有效范围 <paramref name="chars" />。- 或 - <paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。- 或 - <paramref name="bytes" /> 没有足够的容量从 <paramref name="byteIndex" /> 到要容纳所产生的字节的数组的末尾。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <returns>在由 <paramref name="bytes" /> 参数指示的位置处写入的实际字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">一个指针，指向开始写入所产生的字节序列的位置。</param>
    /// <param name="byteCount">最多写入的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> is null (Nothing).- 或 - <paramref name="bytes" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。- 或 - <paramref name="byteCount" /> 小于所产生的字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="bytes" />。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> is null (Nothing).- 或 - <paramref name="chars" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteIndex" /> 或 <paramref name="byteCount" /> 或 <paramref name="charIndex" /> 小于零。- 或 - <paramref name="byteindex" /> 和 <paramref name="byteCount" /> 不表示中的有效范围 <paramref name="bytes" />。- 或 - <paramref name="charIndex" /> 不是 <paramref name="chars" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。- 或 - <paramref name="chars" /> 没有足够的容量从 <paramref name="charIndex" /> 到要容纳所产生的字符的数组的末尾。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <returns>在由 <paramref name="chars" /> 参数指示的位置处写入的实际字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">一个指针，指向开始写入所产生的字符集的位置。</param>
    /// <param name="charCount">要写入的最大字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> is null (Nothing).- 或 - <paramref name="chars" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 或 <paramref name="charCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。- 或 - <paramref name="charCount" /> 小于所产生的字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <returns>包含指定字节序列解码结果的 <see cref="T:System.String" /> 对象。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> is null (Nothing).</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="bytes" />。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      int num1 = count << 1;
      if (num1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      char* charStart = chars;
      char* charEnd = chars + count;
      char ch1 = char.MinValue;
      bool flag = false;
      ulong* numPtr1 = (ulong*) (charEnd - 3);
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        if ((int) ch1 > 0)
          num1 += 2;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
        }
      }
      while (true)
      {
        char ch2;
        while ((int) (ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
        {
          if ((int) ch2 == 0)
          {
            if (!this.bigEndian && (int) ch1 == 0 && ((int) chars & 3) == 0)
            {
              ulong* numPtr2 = (ulong*) chars;
              while (numPtr2 < numPtr1)
              {
                if ((-9223231297218904064L & (long) *numPtr2) != 0L)
                {
                  ulong num2 = (ulong) (-576188069258921984L & (long) *numPtr2 ^ -2882066263381583872L);
                  if ((((long) num2 & -281474976710656L) == 0L || ((long) num2 & 281470681743360L) == 0L || (((long) num2 & 4294901760L) == 0L || ((long) num2 & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr2 ^ -2593835887162763264L) != 0L)
                    break;
                }
                numPtr2 += 8;
              }
              chars = (char*) numPtr2;
              if (chars >= charEnd)
                break;
            }
            ch2 = *chars;
            chars += 2;
          }
          else
            num1 += 2;
          if ((int) ch2 >= 55296 && (int) ch2 <= 57343)
          {
            if ((int) ch2 <= 56319)
            {
              if ((int) ch1 > 0)
              {
                chars -= 2;
                num1 -= 2;
                if (encoderFallbackBuffer == null)
                {
                  encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                  encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
                }
                encoderFallbackBuffer.InternalFallback(ch1, ref chars);
                ch1 = char.MinValue;
              }
              else
                ch1 = ch2;
            }
            else if ((int) ch1 == 0)
            {
              num1 -= 2;
              if (encoderFallbackBuffer == null)
              {
                encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
              }
              encoderFallbackBuffer.InternalFallback(ch2, ref chars);
            }
            else
              ch1 = char.MinValue;
          }
          else if ((int) ch1 > 0)
          {
            chars -= 2;
            if (encoderFallbackBuffer == null)
            {
              encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
              encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
            }
            encoderFallbackBuffer.InternalFallback(ch1, ref chars);
            num1 -= 2;
            ch1 = char.MinValue;
          }
        }
        if ((int) ch1 > 0)
        {
          num1 -= 2;
          if (encoder == null || encoder.MustFlush)
          {
            if (!flag)
            {
              if (encoderFallbackBuffer == null)
              {
                encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
              }
              encoderFallbackBuffer.InternalFallback(ch1, ref chars);
              ch1 = char.MinValue;
              flag = true;
            }
            else
              break;
          }
          else
            goto label_43;
        }
        else
          goto label_43;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", (object) ch1), "chars");
label_43:
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      char ch1 = char.MinValue;
      bool flag = false;
      byte* numPtr1 = bytes + byteCount;
      char* charEnd = chars + charCount;
      byte* numPtr2 = bytes;
      char* charStart = chars;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
        }
      }
      while (true)
      {
        char ch2;
        while ((int) (ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
        {
          if ((int) ch2 == 0)
          {
            if (!this.bigEndian && ((int) chars & 3) == 0 && (((int) bytes & 3) == 0 && (int) ch1 == 0))
            {
              ulong* numPtr3 = (ulong*) ((IntPtr) (chars - 3) + (IntPtr) ((numPtr1 - bytes >> 1 < charEnd - chars ? numPtr1 - bytes >> 1 : charEnd - chars) * 2L));
              ulong* numPtr4 = (ulong*) chars;
              ulong* numPtr5 = (ulong*) bytes;
              while (numPtr4 < numPtr3)
              {
                if ((-9223231297218904064L & (long) *numPtr4) != 0L)
                {
                  ulong num = (ulong) (-576188069258921984L & (long) *numPtr4 ^ -2882066263381583872L);
                  if ((((long) num & -281474976710656L) == 0L || ((long) num & 281470681743360L) == 0L || (((long) num & 4294901760L) == 0L || ((long) num & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr4 ^ -2593835887162763264L) != 0L)
                    break;
                }
                *numPtr5 = *numPtr4;
                numPtr4 += 8;
                numPtr5 += 8;
              }
              chars = (char*) numPtr4;
              bytes = (byte*) numPtr5;
              if (chars >= charEnd)
                break;
            }
            else if ((int) ch1 == 0 && !this.bigEndian && (((int) chars & 3) != ((int) bytes & 3) && ((int) bytes & 1) == 0))
            {
              long num = numPtr1 - bytes >> 1 < charEnd - chars ? numPtr1 - bytes >> 1 : charEnd - chars;
              char* chPtr1 = (char*) bytes;
              char* chPtr2 = (char*) ((IntPtr) chars + (IntPtr) (num * 2L) - 2);
              while (chars < chPtr2)
              {
                if ((int) *chars >= 55296 && (int) *chars <= 57343)
                {
                  if ((int) *chars >= 56320 || (int) *(ushort*) ((IntPtr) chars + 2) < 56320 || (int) *(ushort*) ((IntPtr) chars + 2) > 57343)
                    break;
                }
                else if ((int) *(ushort*) ((IntPtr) chars + 2) >= 55296 && (int) *(ushort*) ((IntPtr) chars + 2) <= 57343)
                {
                  *chPtr1 = *chars;
                  chPtr1 += 2;
                  chars += 2;
                  continue;
                }
                *chPtr1 = *chars;
                *(short*) ((IntPtr) chPtr1 + 2) = (short) *(ushort*) ((IntPtr) chars + 2);
                chPtr1 += 2;
                chars += 2;
              }
              bytes = (byte*) chPtr1;
              if (chars >= charEnd)
                break;
            }
            ch2 = *chars;
            chars += 2;
          }
          if ((int) ch2 >= 55296 && (int) ch2 <= 57343)
          {
            if ((int) ch2 <= 56319)
            {
              if ((int) ch1 > 0)
              {
                chars -= 2;
                if (encoderFallbackBuffer == null)
                {
                  encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                  encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
                }
                encoderFallbackBuffer.InternalFallback(ch1, ref chars);
                ch1 = char.MinValue;
                continue;
              }
              ch1 = ch2;
              continue;
            }
            if ((int) ch1 == 0)
            {
              if (encoderFallbackBuffer == null)
              {
                encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
              }
              encoderFallbackBuffer.InternalFallback(ch2, ref chars);
              continue;
            }
            if (bytes + 3 >= numPtr1)
            {
              if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
              {
                encoderFallbackBuffer.MovePrevious();
                encoderFallbackBuffer.MovePrevious();
              }
              else
                chars -= 2;
              this.ThrowBytesOverflow(encoder, bytes == numPtr2);
              ch1 = char.MinValue;
              break;
            }
            if (this.bigEndian)
            {
              *bytes++ = (byte) ((uint) ch1 >> 8);
              *bytes++ = (byte) ch1;
            }
            else
            {
              *bytes++ = (byte) ch1;
              *bytes++ = (byte) ((uint) ch1 >> 8);
            }
            ch1 = char.MinValue;
          }
          else if ((int) ch1 > 0)
          {
            chars -= 2;
            if (encoderFallbackBuffer == null)
            {
              encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
              encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
            }
            encoderFallbackBuffer.InternalFallback(ch1, ref chars);
            ch1 = char.MinValue;
            continue;
          }
          if (bytes + 1 >= numPtr1)
          {
            if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
              encoderFallbackBuffer.MovePrevious();
            else
              chars -= 2;
            this.ThrowBytesOverflow(encoder, bytes == numPtr2);
            break;
          }
          if (this.bigEndian)
          {
            *bytes++ = (byte) ((uint) ch2 >> 8);
            *bytes++ = (byte) ch2;
          }
          else
          {
            *bytes++ = (byte) ch2;
            *bytes++ = (byte) ((uint) ch2 >> 8);
          }
        }
        if ((int) ch1 > 0 && (encoder == null || encoder.MustFlush))
        {
          if (!flag)
          {
            if (encoderFallbackBuffer == null)
            {
              encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
              encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
            }
            encoderFallbackBuffer.InternalFallback(ch1, ref chars);
            ch1 = char.MinValue;
            flag = true;
          }
          else
            break;
        }
        else
          goto label_62;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", (object) ch1), "chars");
label_62:
      if (encoder != null)
      {
        encoder.charLeftOver = ch1;
        encoder.m_charsUsed = (int) (chars - charStart);
      }
      return (int) (bytes - numPtr2);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder) baseDecoder;
      byte* numPtr1 = bytes + count;
      byte* byteStart = bytes;
      int num1 = -1;
      char ch1 = char.MinValue;
      int num2 = count >> 1;
      ulong* numPtr2 = (ulong*) (numPtr1 - 7);
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      if (decoder != null)
      {
        num1 = decoder.lastByte;
        ch1 = decoder.lastChar;
        if ((int) ch1 > 0)
          ++num2;
        if (num1 >= 0 && (count & 1) == 1)
          ++num2;
      }
      while (bytes < numPtr1)
      {
        if (!this.bigEndian && ((int) bytes & 3) == 0 && (num1 == -1 && (int) ch1 == 0))
        {
          ulong* numPtr3 = (ulong*) bytes;
          while (numPtr3 < numPtr2)
          {
            if ((-9223231297218904064L & (long) *numPtr3) != 0L)
            {
              ulong num3 = (ulong) (-576188069258921984L & (long) *numPtr3 ^ -2882066263381583872L);
              if ((((long) num3 & -281474976710656L) == 0L || ((long) num3 & 281470681743360L) == 0L || (((long) num3 & 4294901760L) == 0L || ((long) num3 & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr3 ^ -2593835887162763264L) != 0L)
                break;
            }
            numPtr3 += 8;
          }
          bytes = (byte*) numPtr3;
          if (bytes >= numPtr1)
            break;
        }
        if (num1 < 0)
        {
          num1 = (int) *bytes++;
          if (bytes >= numPtr1)
            break;
        }
        char ch2 = !this.bigEndian ? (char) ((int) *bytes++ << 8 | num1) : (char) (num1 << 8 | (int) *bytes++);
        num1 = -1;
        if ((int) ch2 >= 55296 && (int) ch2 <= 57343)
        {
          if ((int) ch2 <= 56319)
          {
            if ((int) ch1 > 0)
            {
              int num3 = num2 - 1;
              byte[] bytes1;
              if (this.bigEndian)
                bytes1 = new byte[2]
                {
                  (byte) ((uint) ch1 >> 8),
                  (byte) ch1
                };
              else
                bytes1 = new byte[2]
                {
                  (byte) ch1,
                  (byte) ((uint) ch1 >> 8)
                };
              if (decoderFallbackBuffer == null)
              {
                decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
                decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
              }
              num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
            }
            ch1 = ch2;
          }
          else if ((int) ch1 == 0)
          {
            int num3 = num2 - 1;
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[2]
              {
                (byte) ((uint) ch2 >> 8),
                (byte) ch2
              };
            else
              bytes1 = new byte[2]
              {
                (byte) ch2,
                (byte) ((uint) ch2 >> 8)
              };
            if (decoderFallbackBuffer == null)
            {
              decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
              decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
            }
            num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
          }
          else
            ch1 = char.MinValue;
        }
        else if ((int) ch1 > 0)
        {
          int num3 = num2 - 1;
          byte[] bytes1;
          if (this.bigEndian)
            bytes1 = new byte[2]
            {
              (byte) ((uint) ch1 >> 8),
              (byte) ch1
            };
          else
            bytes1 = new byte[2]
            {
              (byte) ch1,
              (byte) ((uint) ch1 >> 8)
            };
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
          }
          num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
          ch1 = char.MinValue;
        }
      }
      if (decoder == null || decoder.MustFlush)
      {
        if ((int) ch1 > 0)
        {
          int num3 = num2 - 1;
          byte[] bytes1;
          if (this.bigEndian)
            bytes1 = new byte[2]
            {
              (byte) ((uint) ch1 >> 8),
              (byte) ch1
            };
          else
            bytes1 = new byte[2]
            {
              (byte) ch1,
              (byte) ((uint) ch1 >> 8)
            };
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
          }
          num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
          ch1 = char.MinValue;
        }
        if (num1 >= 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
          }
          num2 += decoderFallbackBuffer.InternalFallback(new byte[1]{ (byte) num1 }, bytes);
        }
      }
      if ((int) ch1 > 0)
        --num2;
      return num2;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder) baseDecoder;
      int num1 = -1;
      char ch1 = char.MinValue;
      if (decoder != null)
      {
        num1 = decoder.lastByte;
        ch1 = decoder.lastChar;
      }
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte* numPtr1 = bytes + byteCount;
      char* charEnd = chars + charCount;
      byte* byteStart = bytes;
      char* chPtr = chars;
      while (bytes < numPtr1)
      {
        if (!this.bigEndian && ((int) chars & 3) == 0 && (((int) bytes & 3) == 0 && num1 == -1) && (int) ch1 == 0)
        {
          ulong* numPtr2 = (ulong*) (bytes - 7 + (numPtr1 - bytes >> 1 < charEnd - chars ? (IntPtr) (numPtr1 - bytes) : (IntPtr) (charEnd - chars << 1)).ToInt64());
          ulong* numPtr3 = (ulong*) bytes;
          ulong* numPtr4 = (ulong*) chars;
          while (numPtr3 < numPtr2)
          {
            if ((-9223231297218904064L & (long) *numPtr3) != 0L)
            {
              ulong num2 = (ulong) (-576188069258921984L & (long) *numPtr3 ^ -2882066263381583872L);
              if ((((long) num2 & -281474976710656L) == 0L || ((long) num2 & 281470681743360L) == 0L || (((long) num2 & 4294901760L) == 0L || ((long) num2 & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr3 ^ -2593835887162763264L) != 0L)
                break;
            }
            *numPtr4 = *numPtr3;
            numPtr3 += 8;
            numPtr4 += 8;
          }
          chars = (char*) numPtr4;
          bytes = (byte*) numPtr3;
          if (bytes >= numPtr1)
            break;
        }
        if (num1 < 0)
        {
          num1 = (int) *bytes++;
        }
        else
        {
          char ch2 = !this.bigEndian ? (char) ((int) *bytes++ << 8 | num1) : (char) (num1 << 8 | (int) *bytes++);
          num1 = -1;
          if ((int) ch2 >= 55296 && (int) ch2 <= 57343)
          {
            if ((int) ch2 <= 56319)
            {
              if ((int) ch1 > 0)
              {
                byte[] bytes1;
                if (this.bigEndian)
                  bytes1 = new byte[2]
                  {
                    (byte) ((uint) ch1 >> 8),
                    (byte) ch1
                  };
                else
                  bytes1 = new byte[2]
                  {
                    (byte) ch1,
                    (byte) ((uint) ch1 >> 8)
                  };
                if (decoderFallbackBuffer == null)
                {
                  decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
                  decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
                }
                if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
                {
                  bytes -= 2;
                  decoderFallbackBuffer.InternalReset();
                  this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
                  break;
                }
              }
              ch1 = ch2;
              continue;
            }
            if ((int) ch1 == 0)
            {
              byte[] bytes1;
              if (this.bigEndian)
                bytes1 = new byte[2]
                {
                  (byte) ((uint) ch2 >> 8),
                  (byte) ch2
                };
              else
                bytes1 = new byte[2]
                {
                  (byte) ch2,
                  (byte) ((uint) ch2 >> 8)
                };
              if (decoderFallbackBuffer == null)
              {
                decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
                decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
              }
              if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
              {
                bytes -= 2;
                decoderFallbackBuffer.InternalReset();
                this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
                break;
              }
              continue;
            }
            if ((UIntPtr) chars >= (UIntPtr) charEnd - new UIntPtr(2))
            {
              bytes -= 2;
              this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
              break;
            }
            *chars++ = ch1;
            ch1 = char.MinValue;
          }
          else if ((int) ch1 > 0)
          {
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[2]
              {
                (byte) ((uint) ch1 >> 8),
                (byte) ch1
              };
            else
              bytes1 = new byte[2]
              {
                (byte) ch1,
                (byte) ((uint) ch1 >> 8)
              };
            if (decoderFallbackBuffer == null)
            {
              decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
              decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
            }
            if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            {
              bytes -= 2;
              decoderFallbackBuffer.InternalReset();
              this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
              break;
            }
            ch1 = char.MinValue;
          }
          if (chars >= charEnd)
          {
            bytes -= 2;
            this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
            break;
          }
          *chars++ = ch2;
        }
      }
      if (decoder == null || decoder.MustFlush)
      {
        if ((int) ch1 > 0)
        {
          byte[] bytes1;
          if (this.bigEndian)
            bytes1 = new byte[2]
            {
              (byte) ((uint) ch1 >> 8),
              (byte) ch1
            };
          else
            bytes1 = new byte[2]
            {
              (byte) ch1,
              (byte) ((uint) ch1 >> 8)
            };
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
          }
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            bytes -= 2;
            if (num1 >= 0)
              --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
            bytes += 2;
            if (num1 >= 0)
            {
              ++bytes;
              goto label_66;
            }
            else
              goto label_66;
          }
          else
            ch1 = char.MinValue;
        }
        if (num1 >= 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
          }
          if (!decoderFallbackBuffer.InternalFallback(new byte[1]{ (byte) num1 }, bytes, ref chars))
          {
            --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
            ++bytes;
          }
          else
            num1 = -1;
        }
      }
label_66:
      if (decoder != null)
      {
        decoder.m_bytesUsed = (int) (bytes - byteStart);
        decoder.lastChar = ch1;
        decoder.lastByte = num1;
      }
      return (int) (chars - chPtr);
    }

    /// <summary>获取可将 Unicode 字符序列转换为 UTF-16 编码的字节序列的编码器。</summary>
    /// <returns>将 Unicode 字符序列转换为 UTF-16 编码字节序列的 <see cref="T:System.Text.Encoder" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
    }

    /// <summary>获取可以将 UTF-16 编码的字节序列转换为 Unicode 字符序列的解码器。</summary>
    /// <returns>一个 <see cref="T:System.Text.Decoder" />，用于将 UTF-16 编码的字节序列转换为 Unicode 字符序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override System.Text.Decoder GetDecoder()
    {
      return (System.Text.Decoder) new UnicodeEncoding.Decoder(this);
    }

    /// <summary>如果此实例的构造函数请求一个字节顺序标记，则将返回用 UTF-16 格式编码的 Unicode 字节顺序标记。</summary>
    /// <returns>一个包含 Unicode 字节顺序标记的字节数组（如果 <see cref="T:System.Text.UnicodeEncoding" /> 对象配置为提供一个这样的字节数组）。否则，此方法返回一个零长度的字节数组。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override byte[] GetPreamble()
    {
      if (!this.byteOrderMark)
        return EmptyArray<byte>.Value;
      if (this.bigEndian)
        return new byte[2]{ (byte) 254, byte.MaxValue };
      return new byte[2]{ byte.MaxValue, (byte) 254 };
    }

    /// <summary>计算对指定数目的字符进行编码时产生的最大字节数。</summary>
    /// <returns>对指定数目的字符进行编码所产生的最大字节数。</returns>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      long num2 = num1 << 1;
      if (num2 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num2;
    }

    /// <summary>计算对指定数目的字节进行解码时产生的最大字符数。</summary>
    /// <returns>对指定数目的字节进行解码时所产生的最大字符数。</returns>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 的更全面的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) (byteCount >> 1) + (long) (byteCount & 1) + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Text.UnicodeEncoding" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 是 <see cref="T:System.Text.UnicodeEncoding" /> 的一个实例并且等于当前对象，则为 true；否则，为 false。</returns>
    /// <param name="value">要与当前对象进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UnicodeEncoding unicodeEncoding = value as UnicodeEncoding;
      if (unicodeEncoding != null && this.CodePage == unicodeEncoding.CodePage && (this.byteOrderMark == unicodeEncoding.byteOrderMark && this.bigEndian == unicodeEncoding.bigEndian) && this.EncoderFallback.Equals((object) unicodeEncoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) unicodeEncoding.DecoderFallback);
      return false;
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Text.UnicodeEncoding" /> 对象的哈希代码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.CodePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode() + (this.byteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
    }

    [Serializable]
    private class Decoder : DecoderNLS, ISerializable
    {
      internal int lastByte = -1;
      internal char lastChar;

      internal override bool HasState
      {
        get
        {
          if (this.lastByte == -1)
            return (uint) this.lastChar > 0U;
          return true;
        }
      }

      public Decoder(UnicodeEncoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal Decoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.lastByte = (int) info.GetValue("lastByte", typeof (int));
        try
        {
          this.m_encoding = (Encoding) info.GetValue("m_encoding", typeof (Encoding));
          this.lastChar = (char) info.GetValue("lastChar", typeof (char));
          this.m_fallback = (DecoderFallback) info.GetValue("m_fallback", typeof (DecoderFallback));
        }
        catch (SerializationException ex)
        {
          this.m_encoding = (Encoding) new UnicodeEncoding((bool) info.GetValue("bigEndian", typeof (bool)), false);
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("m_encoding", (object) this.m_encoding);
        info.AddValue("m_fallback", (object) this.m_fallback);
        info.AddValue("lastChar", this.lastChar);
        info.AddValue("lastByte", this.lastByte);
        info.AddValue("bigEndian", ((UnicodeEncoding) this.m_encoding).bigEndian);
      }

      public override void Reset()
      {
        this.lastByte = -1;
        this.lastChar = char.MinValue;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }
  }
}
