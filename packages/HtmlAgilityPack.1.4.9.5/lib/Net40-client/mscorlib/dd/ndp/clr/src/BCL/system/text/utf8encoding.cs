// Decompiled with JetBrains decompiler
// Type: System.Text.UTF8Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>表示 Unicode 字符的 UTF-8 编码。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UTF8Encoding : Encoding
  {
    private const int UTF8_CODEPAGE = 65001;
    private bool emitUTF8Identifier;
    private bool isThrowException;
    private const int FinalByte = 536870912;
    private const int SupplimentarySeq = 268435456;
    private const int ThreeByteSeq = 134217728;

    /// <summary>初始化 <see cref="T:System.Text.UTF8Encoding" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public UTF8Encoding()
      : this(false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UTF8Encoding" /> 类的新实例。参数指定是否提供一个 Unicode 字节顺序标记。</summary>
    /// <param name="encoderShouldEmitUTF8Identifier">如果为 true，则指定 <see cref="M:System.Text.UTF8Encoding.GetPreamble" /> 方法返回 Unicode 字节顺序标记；否则为 false。有关详细信息，请参阅备注部分。</param>
    [__DynamicallyInvokable]
    public UTF8Encoding(bool encoderShouldEmitUTF8Identifier)
      : this(encoderShouldEmitUTF8Identifier, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UTF8Encoding" /> 类的新实例。参数指定是否提供 Unicode 字节顺序标记，以及是否在检测到无效的编码时引发异常。</summary>
    /// <param name="encoderShouldEmitUTF8Identifier">如果为 true，则指定 <see cref="M:System.Text.UTF8Encoding.GetPreamble" /> 方法应返回 Unicode 字节顺序标记；否则为 false。有关详细信息，请参阅备注部分。</param>
    /// <param name="throwOnInvalidBytes">如果为 true，则在检测到无效的编码时引发异常；否则为 false。</param>
    [__DynamicallyInvokable]
    public UTF8Encoding(bool encoderShouldEmitUTF8Identifier, bool throwOnInvalidBytes)
      : base(65001)
    {
      this.emitUTF8Identifier = encoderShouldEmitUTF8Identifier;
      this.isThrowException = throwOnInvalidBytes;
      if (!this.isThrowException)
        return;
      this.SetDefaultFallbacks();
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
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="chars" />。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－<see cref="P:System.Text.Encoding.EncoderFallback" /> 属性设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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

    /// <summary>计算对指定 <see cref="T:System.String" /> 中的字符进行编码时所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetByteCount(string chars)
    {
      if (chars == null)
        throw new ArgumentNullException("s");
      string str = chars;
      char* chars1 = (char*) str;
      if ((IntPtr) chars1 != IntPtr.Zero)
        chars1 += RuntimeHelpers.OffsetToStringData;
      return this.GetByteCount(chars1, chars.Length, (EncoderNLS) null);
    }

    /// <summary>计算对从指定的字符指针开始的一组字符进行编码时产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 有关的完整说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <param name="byteIndex">要开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" /> 或 <paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 - <paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示中的有效范围 <paramref name="chars" />。- 或 - <paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="s" /> 包含无效的字符序列。- 或 - <paramref name="bytes" /> 没有足够的容量从 <paramref name="byteIndex" /> 到要容纳所产生的字节的数组的末尾。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="chars" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" /> 或 <paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 - <paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示中的有效范围 <paramref name="chars" />。- 或 - <paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。- 或 - <paramref name="bytes" /> 没有足够的容量从 <paramref name="byteIndex" /> 到要容纳所产生的字节的数组的末尾。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="chars" /> 为 null。- 或 - <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。- 或 - <paramref name="byteCount" /> 小于所产生的字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="bytes" />。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> 为 null。- 或 - <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteIndex" /> 或 <paramref name="byteCount" /> 或 <paramref name="charIndex" /> 小于零。- 或 - <paramref name="byteindex" /> 和 <paramref name="byteCount" /> 不表示中的有效范围 <paramref name="bytes" />。- 或 - <paramref name="charIndex" /> 不是 <paramref name="chars" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。- 或 - <paramref name="chars" /> 没有足够的容量从 <paramref name="charIndex" /> 到要容纳所产生的字符的数组的末尾。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> 为 null。- 或 - <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 或 <paramref name="charCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。- 或 - <paramref name="charCount" /> 小于所产生的字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="bytes" />。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
      EncoderFallbackBuffer encoderFallbackBuffer1 = (EncoderFallbackBuffer) null;
      char* chars1 = chars;
      char* chPtr1 = chars1 + count;
      int num1 = count;
      int ch1 = 0;
      if (baseEncoder != null)
      {
        UTF8Encoding.UTF8Encoder utF8Encoder = (UTF8Encoding.UTF8Encoder) baseEncoder;
        ch1 = utF8Encoder.surrogateChar;
        if (utF8Encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer1 = utF8Encoder.FallbackBuffer;
          if (encoderFallbackBuffer1.Remaining > 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) utF8Encoder.Fallback.GetType()));
          encoderFallbackBuffer1.InternalInitialize(chars, chPtr1, (EncoderNLS) utF8Encoder, false);
        }
      }
      while (true)
      {
        if (chars1 >= chPtr1)
        {
          if (ch1 == 0)
          {
            ch1 = encoderFallbackBuffer1 != null ? (int) encoderFallbackBuffer1.InternalGetNextChar() : 0;
            if (ch1 > 0)
            {
              ++num1;
              goto label_23;
            }
          }
          else if (encoderFallbackBuffer1 != null && encoderFallbackBuffer1.bFallingBack)
          {
            ch1 = (int) encoderFallbackBuffer1.InternalGetNextChar();
            ++num1;
            if (UTF8Encoding.InRange(ch1, 56320, 57343))
            {
              ch1 = 65533;
              ++num1;
              goto label_25;
            }
            else if (ch1 > 0)
              goto label_23;
            else
              break;
          }
          if (ch1 > 0 && (baseEncoder == null || baseEncoder.MustFlush))
          {
            ++num1;
            goto label_25;
          }
          else
            goto label_83;
        }
        else if (ch1 > 0)
        {
          int ch2 = (int) *chars1;
          ++num1;
          int start = 56320;
          int end = 57343;
          if (UTF8Encoding.InRange(ch2, start, end))
          {
            ch1 = 65533;
            chars1 += 2;
            goto label_25;
          }
          else
            goto label_25;
        }
        else
        {
          if (encoderFallbackBuffer1 != null)
          {
            ch1 = (int) encoderFallbackBuffer1.InternalGetNextChar();
            if (ch1 > 0)
            {
              ++num1;
              goto label_23;
            }
          }
          ch1 = (int) *chars1;
          chars1 += 2;
        }
label_23:
        if (UTF8Encoding.InRange(ch1, 55296, 56319))
        {
          --num1;
          continue;
        }
label_25:
        if (UTF8Encoding.InRange(ch1, 55296, 57343))
        {
          if (encoderFallbackBuffer1 == null)
          {
            encoderFallbackBuffer1 = baseEncoder != null ? baseEncoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            EncoderFallbackBuffer encoderFallbackBuffer2 = encoderFallbackBuffer1;
            char* charStart = chars;
            IntPtr num2 = (IntPtr) count * 2;
            IntPtr num3 = (IntPtr) charStart + num2;
            EncoderNLS encoder = baseEncoder;
            int num4 = 0;
            encoderFallbackBuffer2.InternalInitialize(charStart, (char*) num3, encoder, num4 != 0);
          }
          encoderFallbackBuffer1.InternalFallback((char) ch1, ref chars1);
          --num1;
          ch1 = 0;
        }
        else
        {
          if (ch1 > (int) sbyte.MaxValue)
          {
            if (ch1 > 2047)
              ++num1;
            ++num1;
          }
          if (encoderFallbackBuffer1 != null && (ch1 = (int) encoderFallbackBuffer1.InternalGetNextChar()) != 0)
          {
            ++num1;
            goto label_23;
          }
          else
          {
            int num2 = UTF8Encoding.PtrDiff(chPtr1, chars1);
            if (num2 <= 13)
            {
              char* chPtr2 = chPtr1;
              while (chars1 < chPtr2)
              {
                ch1 = (int) *chars1;
                chars1 += 2;
                if (ch1 > (int) sbyte.MaxValue)
                  goto label_23;
              }
              goto label_83;
            }
            else
            {
              char* chPtr2 = chars1 + num2 - 7;
label_81:
              while (chars1 < chPtr2)
              {
                int ch2 = (int) *chars1;
                chars1 += 2;
                if (ch2 > (int) sbyte.MaxValue)
                {
                  if (ch2 > 2047)
                  {
                    if ((ch2 & 63488) != 55296)
                      ++num1;
                    else
                      goto label_74;
                  }
                  ++num1;
                }
                if (((int) chars1 & 2) != 0)
                {
                  ch2 = (int) *chars1;
                  chars1 += 2;
                  if (ch2 > (int) sbyte.MaxValue)
                  {
                    if (ch2 > 2047)
                    {
                      if ((ch2 & 63488) != 55296)
                        ++num1;
                      else
                        goto label_74;
                    }
                    ++num1;
                  }
                }
                while (chars1 < chPtr2)
                {
                  int num3 = *(int*) chars1;
                  int num4 = *(int*) (chars1 + 2);
                  if (((num3 | num4) & -8323200) != 0)
                  {
                    if (((num3 | num4) & -134154240) == 0)
                    {
                      if ((num3 & -8388608) != 0)
                        ++num1;
                      if ((num3 & 65408) != 0)
                        ++num1;
                      if ((num4 & -8388608) != 0)
                        ++num1;
                      if ((num4 & 65408) != 0)
                        ++num1;
                    }
                    else
                      goto label_73;
                  }
                  chars1 += 4;
                  num3 = *(int*) chars1;
                  int num5 = *(int*) (chars1 + 2);
                  if (((num3 | num5) & -8323200) != 0)
                  {
                    if (((num3 | num5) & -134154240) == 0)
                    {
                      if ((num3 & -8388608) != 0)
                        ++num1;
                      if ((num3 & 65408) != 0)
                        ++num1;
                      if ((num5 & -8388608) != 0)
                        ++num1;
                      if ((num5 & 65408) != 0)
                        ++num1;
                    }
                    else
                      goto label_73;
                  }
                  chars1 += 4;
                  continue;
label_73:
                  ch2 = (int) (ushort) num3;
                  chars1 += 2;
                  if (ch2 <= (int) sbyte.MaxValue)
                    goto label_81;
                  else
                    goto label_74;
                }
                break;
label_74:
                if (ch2 > 2047)
                {
                  if (UTF8Encoding.InRange(ch2, 55296, 57343))
                  {
                    int ch3 = (int) *chars1;
                    if (ch2 > 56319 || !UTF8Encoding.InRange(ch3, 56320, 57343))
                    {
                      chars1 -= 2;
                      break;
                    }
                    chars1 += 2;
                  }
                  ++num1;
                }
                ++num1;
              }
              ch1 = 0;
            }
          }
        }
      }
      --num1;
label_83:
      return num1;
    }

    [SecurityCritical]
    private static unsafe int PtrDiff(char* a, char* b)
    {
      return (int) ((uint) ((sbyte*) a - (sbyte*) b) >> 1);
    }

    [SecurityCritical]
    private static unsafe int PtrDiff(byte* a, byte* b)
    {
      return (int) (a - b);
    }

    private static bool InRange(int ch, int start, int end)
    {
      return (uint) (ch - start) <= (uint) (end - start);
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
    {
      UTF8Encoding.UTF8Encoder utF8Encoder = (UTF8Encoding.UTF8Encoder) null;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* chars1 = chars;
      byte* b = bytes;
      char* chPtr1 = chars1 + charCount;
      byte* a = b + byteCount;
      int ch1 = 0;
      if (baseEncoder != null)
      {
        utF8Encoder = (UTF8Encoding.UTF8Encoder) baseEncoder;
        ch1 = utF8Encoder.surrogateChar;
        if (utF8Encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = utF8Encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && utF8Encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) utF8Encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(chars, chPtr1, (EncoderNLS) utF8Encoder, true);
        }
      }
      while (true)
      {
        if (chars1 >= chPtr1)
        {
          if (ch1 == 0)
          {
            ch1 = encoderFallbackBuffer != null ? (int) encoderFallbackBuffer.InternalGetNextChar() : 0;
            if (ch1 > 0)
              goto label_19;
          }
          else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
          {
            int num = ch1;
            ch1 = (int) encoderFallbackBuffer.InternalGetNextChar();
            if (UTF8Encoding.InRange(ch1, 56320, 57343))
            {
              ch1 = ch1 + (num << 10) - 56613888;
              goto label_20;
            }
            else if (ch1 <= 0)
              goto label_80;
            else
              goto label_19;
          }
          if (ch1 <= 0 || utF8Encoder != null && !utF8Encoder.MustFlush)
            goto label_80;
          else
            goto label_20;
        }
        else if (ch1 > 0)
        {
          int ch2 = (int) *chars1;
          if (UTF8Encoding.InRange(ch2, 56320, 57343))
          {
            ch1 = ch2 + (ch1 << 10) - 56613888;
            chars1 += 2;
            goto label_20;
          }
          else
            goto label_20;
        }
        else
        {
          if (encoderFallbackBuffer != null)
          {
            ch1 = (int) encoderFallbackBuffer.InternalGetNextChar();
            if (ch1 > 0)
              goto label_19;
          }
          ch1 = (int) *chars1;
          chars1 += 2;
        }
label_19:
        if (UTF8Encoding.InRange(ch1, 55296, 56319))
          continue;
label_20:
        if (UTF8Encoding.InRange(ch1, 55296, 57343))
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = baseEncoder != null ? baseEncoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(chars, chPtr1, baseEncoder, true);
          }
          encoderFallbackBuffer.InternalFallback((char) ch1, ref chars1);
          ch1 = 0;
        }
        else
        {
          int num1 = 1;
          if (ch1 > (int) sbyte.MaxValue)
          {
            if (ch1 > 2047)
            {
              if (ch1 > (int) ushort.MaxValue)
                ++num1;
              ++num1;
            }
            ++num1;
          }
          if (b <= a - num1)
          {
            if (ch1 <= (int) sbyte.MaxValue)
            {
              *b = (byte) ch1;
            }
            else
            {
              int num2;
              if (ch1 <= 2047)
              {
                num2 = (int) (byte) (-64 | ch1 >> 6);
              }
              else
              {
                int num3;
                if (ch1 <= (int) ushort.MaxValue)
                {
                  num3 = (int) (byte) (-32 | ch1 >> 12);
                }
                else
                {
                  *b = (byte) (-16 | ch1 >> 18);
                  ++b;
                  num3 = (int) sbyte.MinValue | ch1 >> 12 & 63;
                }
                *b = (byte) num3;
                ++b;
                num2 = (int) sbyte.MinValue | ch1 >> 6 & 63;
              }
              *b = (byte) num2;
              ++b;
              *b = (byte) ((int) sbyte.MinValue | ch1 & 63);
            }
            ++b;
            if (encoderFallbackBuffer == null || (ch1 = (int) encoderFallbackBuffer.InternalGetNextChar()) == 0)
            {
              int num2 = UTF8Encoding.PtrDiff(chPtr1, chars1);
              int num3 = UTF8Encoding.PtrDiff(a, b);
              if (num2 <= 13)
              {
                if (num3 < num2)
                {
                  ch1 = 0;
                }
                else
                {
                  char* chPtr2 = chPtr1;
                  while (chars1 < chPtr2)
                  {
                    ch1 = (int) *chars1;
                    chars1 += 2;
                    if (ch1 <= (int) sbyte.MaxValue)
                    {
                      *b = (byte) ch1;
                      ++b;
                    }
                    else
                      goto label_19;
                  }
                  goto label_54;
                }
              }
              else
              {
                if (num3 < num2)
                  num2 = num3;
                char* chPtr2 = chars1 + num2 - 5;
                while (chars1 < chPtr2)
                {
                  int ch2 = (int) *chars1;
                  chars1 += 2;
                  if (ch2 <= (int) sbyte.MaxValue)
                  {
                    *b = (byte) ch2;
                    ++b;
                    if (((int) chars1 & 2) != 0)
                    {
                      ch2 = (int) *chars1;
                      chars1 += 2;
                      if (ch2 <= (int) sbyte.MaxValue)
                      {
                        *b = (byte) ch2;
                        ++b;
                      }
                      else
                        goto label_67;
                    }
                    while (chars1 < chPtr2)
                    {
                      int num4 = *(int*) chars1;
                      int num5 = *(int*) (chars1 + 2);
                      if (((num4 | num5) & -8323200) == 0)
                      {
                        *b = (byte) num4;
                        b[1] = (byte) (num4 >> 16);
                        chars1 += 4;
                        b[2] = (byte) num5;
                        b[3] = (byte) (num5 >> 16);
                        b += 4;
                      }
                      else
                      {
                        ch2 = (int) (ushort) num4;
                        chars1 += 2;
                        if (ch2 <= (int) sbyte.MaxValue)
                        {
                          *b = (byte) ch2;
                          ++b;
                          break;
                        }
                        goto label_67;
                      }
                    }
                    continue;
                  }
label_67:
                  int num6;
                  if (ch2 <= 2047)
                  {
                    num6 = -64 | ch2 >> 6;
                  }
                  else
                  {
                    int num4;
                    if (!UTF8Encoding.InRange(ch2, 55296, 57343))
                    {
                      num4 = -32 | ch2 >> 12;
                    }
                    else
                    {
                      if (ch2 > 56319)
                      {
                        chars1 -= 2;
                        break;
                      }
                      int ch3 = (int) *chars1;
                      chars1 += 2;
                      if (!UTF8Encoding.InRange(ch3, 56320, 57343))
                      {
                        chars1 -= 2;
                        break;
                      }
                      ch2 = ch3 + (ch2 << 10) - 56613888;
                      *b = (byte) (-16 | ch2 >> 18);
                      ++b;
                      num4 = (int) sbyte.MinValue | ch2 >> 12 & 63;
                    }
                    *b = (byte) num4;
                    chPtr2 -= 2;
                    ++b;
                    num6 = (int) sbyte.MinValue | ch2 >> 6 & 63;
                  }
                  *b = (byte) num6;
                  chPtr2 -= 2;
                  byte* numPtr = b + 1;
                  *numPtr = (byte) ((int) sbyte.MinValue | ch2 & 63);
                  b = numPtr + 1;
                }
                ch1 = 0;
              }
            }
            else
              goto label_19;
          }
          else
            break;
        }
      }
      if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
      {
        encoderFallbackBuffer.MovePrevious();
        if (ch1 > (int) ushort.MaxValue)
          encoderFallbackBuffer.MovePrevious();
      }
      else
      {
        chars1 -= 2;
        if (ch1 > (int) ushort.MaxValue)
          chars1 -= 2;
      }
      this.ThrowBytesOverflow((EncoderNLS) utF8Encoder, b == bytes);
      ch1 = 0;
      goto label_80;
label_54:
      ch1 = 0;
label_80:
      if (utF8Encoder != null)
      {
        utF8Encoder.surrogateChar = ch1;
        utF8Encoder.m_charsUsed = (int) (chars1 - chars);
      }
      return (int) (b - bytes);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      byte* numPtr1 = bytes;
      byte* a = numPtr1 + count;
      int num1 = count;
      int ch = 0;
      DecoderFallbackBuffer fallback = (DecoderFallbackBuffer) null;
      if (baseDecoder != null)
      {
        ch = ((UTF8Encoding.UTF8Decoder) baseDecoder).bits;
        num1 -= ch >> 30;
      }
label_2:
      while (numPtr1 < a)
      {
        if (ch != 0)
        {
          int num2 = (int) *numPtr1;
          ++numPtr1;
          if ((num2 & -64) != 128)
          {
            --numPtr1;
            num1 += ch >> 30;
          }
          else
          {
            ch = ch << 6 | num2 & 63;
            if ((ch & 536870912) == 0)
            {
              if ((ch & 268435456) != 0)
              {
                if ((ch & 8388608) != 0 || UTF8Encoding.InRange(ch & 496, 16, 256))
                  continue;
              }
              else if ((ch & 992) != 0 && (ch & 992) != 864)
                continue;
            }
            else if ((ch & 270467072) == 268435456)
            {
              --num1;
              goto label_27;
            }
            else
              goto label_27;
          }
        }
        else
          goto label_15;
label_12:
        if (fallback == null)
        {
          fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
          fallback.InternalInitialize(bytes, (char*) null);
        }
        num1 += this.FallbackInvalidByteSequence(numPtr1, ch, fallback);
        ch = 0;
        continue;
label_15:
        ch = (int) *numPtr1;
        ++numPtr1;
label_16:
        if (ch > (int) sbyte.MaxValue)
        {
          --num1;
          if ((ch & 64) != 0)
          {
            if ((ch & 32) != 0)
            {
              if ((ch & 16) != 0)
              {
                int num2 = ch & 15;
                if (num2 > 4)
                {
                  ch = num2 | 240;
                  goto label_12;
                }
                else
                {
                  ch = num2 | 1347226624;
                  --num1;
                  continue;
                }
              }
              else
              {
                ch = ch & 15 | 1210220544;
                --num1;
                continue;
              }
            }
            else
            {
              int num2 = ch & 31;
              if (num2 <= 1)
              {
                ch = num2 | 192;
                goto label_12;
              }
              else
              {
                ch = num2 | 8388608;
                continue;
              }
            }
          }
          else
            goto label_12;
        }
label_27:
        int num3 = UTF8Encoding.PtrDiff(a, numPtr1);
        if (num3 <= 13)
        {
          byte* numPtr2 = a;
          while (numPtr1 < numPtr2)
          {
            ch = (int) *numPtr1;
            ++numPtr1;
            if (ch > (int) sbyte.MaxValue)
              goto label_16;
          }
          ch = 0;
          break;
        }
        byte* numPtr3 = numPtr1 + num3 - 7;
        while (numPtr1 < numPtr3)
        {
          int num2 = (int) *numPtr1;
          ++numPtr1;
          if (num2 <= (int) sbyte.MaxValue)
          {
            if (((int) numPtr1 & 1) != 0)
            {
              num2 = (int) *numPtr1;
              ++numPtr1;
              if (num2 > (int) sbyte.MaxValue)
                goto label_45;
            }
            int num4;
            if (((int) numPtr1 & 2) != 0)
            {
              num4 = (int) *(ushort*) numPtr1;
              if ((num4 & 32896) == 0)
                numPtr1 += 2;
              else
                goto label_44;
            }
            while (numPtr1 < numPtr3)
            {
              num4 = *(int*) numPtr1;
              int num5 = *(int*) (numPtr1 + 4);
              if (((num4 | num5) & -2139062144) == 0)
              {
                numPtr1 += 8;
                if (numPtr1 < numPtr3)
                {
                  num4 = *(int*) numPtr1;
                  int num6 = *(int*) (numPtr1 + 4);
                  if (((num4 | num6) & -2139062144) == 0)
                    numPtr1 += 8;
                  else
                    goto label_44;
                }
                else
                  break;
              }
              else
                goto label_44;
            }
            break;
label_44:
            num2 = num4 & (int) byte.MaxValue;
            ++numPtr1;
            if (num2 <= (int) sbyte.MaxValue)
              continue;
          }
label_45:
          int num7 = (int) *numPtr1;
          ++numPtr1;
          if ((num2 & 64) != 0 && (num7 & -64) == 128)
          {
            int num4 = num7 & 63;
            if ((num2 & 32) != 0)
            {
              int num5 = num4 | (num2 & 15) << 6;
              if ((num2 & 16) != 0)
              {
                int num6 = (int) *numPtr1;
                if (UTF8Encoding.InRange(num5 >> 4, 1, 16) && (num6 & -64) == 128)
                {
                  int num8 = num5 << 6 | num6 & 63;
                  if (((int) numPtr1[1] & -64) == 128)
                  {
                    numPtr1 += 2;
                    --num1;
                  }
                  else
                    goto label_57;
                }
                else
                  goto label_57;
              }
              else
              {
                int num6 = (int) *numPtr1;
                if ((num5 & 992) != 0 && (num5 & 992) != 864 && (num6 & -64) == 128)
                {
                  ++numPtr1;
                  --num1;
                }
                else
                  goto label_57;
              }
            }
            else if ((num2 & 30) == 0)
              goto label_57;
            --num1;
            continue;
          }
label_57:
          numPtr1 -= 2;
          ch = 0;
          goto label_2;
        }
        ch = 0;
      }
      if (ch != 0)
      {
        num1 += ch >> 30;
        if (baseDecoder == null || baseDecoder.MustFlush)
        {
          if (fallback == null)
          {
            fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            fallback.InternalInitialize(bytes, (char*) null);
          }
          num1 += this.FallbackInvalidByteSequence(numPtr1, ch, fallback);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      byte* pSrc = bytes;
      char* pTarget = chars;
      byte* a = pSrc + byteCount;
      char* chPtr1 = pTarget + charCount;
      int ch = 0;
      DecoderFallbackBuffer fallback = (DecoderFallbackBuffer) null;
      if (baseDecoder != null)
        ch = ((UTF8Encoding.UTF8Decoder) baseDecoder).bits;
label_2:
      while (pSrc < a)
      {
        if (ch != 0)
        {
          int num = (int) *pSrc;
          ++pSrc;
          if ((num & -64) != 128)
          {
            --pSrc;
          }
          else
          {
            ch = ch << 6 | num & 63;
            if ((ch & 536870912) == 0)
            {
              if ((ch & 268435456) != 0)
              {
                if ((ch & 8388608) != 0 || UTF8Encoding.InRange(ch & 496, 16, 256))
                  continue;
              }
              else if ((ch & 992) != 0 && (ch & 992) != 864)
                continue;
            }
            else if ((ch & 270467072) > 268435456 && pTarget < chPtr1)
            {
              *pTarget = (char) ((ch >> 10 & 2047) - 10304);
              pTarget += 2;
              ch = (ch & 1023) + 56320;
              goto label_29;
            }
            else
              goto label_29;
          }
        }
        else
          goto label_17;
label_12:
        if (fallback == null)
        {
          fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
          fallback.InternalInitialize(bytes, chPtr1);
        }
        if (!this.FallbackInvalidByteSequence(ref pSrc, ch, fallback, ref pTarget))
        {
          fallback.InternalReset();
          this.ThrowCharsOverflow(baseDecoder, pTarget == chars);
          ch = 0;
          break;
        }
        ch = 0;
        continue;
label_17:
        ch = (int) *pSrc;
        ++pSrc;
label_18:
        if (ch > (int) sbyte.MaxValue)
        {
          if ((ch & 64) != 0)
          {
            if ((ch & 32) != 0)
            {
              if ((ch & 16) != 0)
              {
                int num = ch & 15;
                if (num > 4)
                {
                  ch = num | 240;
                  goto label_12;
                }
                else
                {
                  ch = num | 1347226624;
                  continue;
                }
              }
              else
              {
                ch = ch & 15 | 1210220544;
                continue;
              }
            }
            else
            {
              int num = ch & 31;
              if (num <= 1)
              {
                ch = num | 192;
                goto label_12;
              }
              else
              {
                ch = num | 8388608;
                continue;
              }
            }
          }
          else
            goto label_12;
        }
label_29:
        if (pTarget >= chPtr1)
        {
          int num = ch & 2097151;
          if (num > (int) sbyte.MaxValue)
          {
            if (num > 2047)
            {
              if (num >= 56320 && num <= 57343)
              {
                --pSrc;
                pTarget -= 2;
              }
              else if (num > (int) ushort.MaxValue)
                --pSrc;
              --pSrc;
            }
            --pSrc;
          }
          --pSrc;
          this.ThrowCharsOverflow(baseDecoder, pTarget == chars);
          ch = 0;
          break;
        }
        *pTarget = (char) ch;
        pTarget += 2;
        int num1 = UTF8Encoding.PtrDiff(chPtr1, pTarget);
        int num2 = UTF8Encoding.PtrDiff(a, pSrc);
        if (num2 <= 13)
        {
          if (num1 < num2)
          {
            ch = 0;
          }
          else
          {
            byte* numPtr = a;
            while (pSrc < numPtr)
            {
              ch = (int) *pSrc;
              ++pSrc;
              if (ch <= (int) sbyte.MaxValue)
              {
                *pTarget = (char) ch;
                pTarget += 2;
              }
              else
                goto label_18;
            }
            ch = 0;
            break;
          }
        }
        else
        {
          if (num1 < num2)
            num2 = num1;
          char* chPtr2 = pTarget + num2 - 7;
          while (pTarget < chPtr2)
          {
            int num3 = (int) *pSrc;
            ++pSrc;
            if (num3 <= (int) sbyte.MaxValue)
            {
              *pTarget = (char) num3;
              pTarget += 2;
              if (((int) pSrc & 1) != 0)
              {
                num3 = (int) *pSrc;
                ++pSrc;
                if (num3 <= (int) sbyte.MaxValue)
                {
                  *pTarget = (char) num3;
                  pTarget += 2;
                }
                else
                  goto label_62;
              }
              int num4;
              if (((int) pSrc & 2) != 0)
              {
                num4 = (int) *(ushort*) pSrc;
                if ((num4 & 32896) == 0)
                {
                  *pTarget = (char) (num4 & (int) sbyte.MaxValue);
                  pSrc += 2;
                  *(short*) ((IntPtr) pTarget + 2) = (short) (ushort) (num4 >> 8 & (int) sbyte.MaxValue);
                  pTarget += 2;
                }
                else
                  goto label_60;
              }
              while (pTarget < chPtr2)
              {
                num4 = *(int*) pSrc;
                int num5 = *(int*) (pSrc + 4);
                if (((num4 | num5) & -2139062144) == 0)
                {
                  *pTarget = (char) (num4 & (int) sbyte.MaxValue);
                  *(short*) ((IntPtr) pTarget + 2) = (short) (ushort) (num4 >> 8 & (int) sbyte.MaxValue);
                  pTarget[2] = (char) (num4 >> 16 & (int) sbyte.MaxValue);
                  pTarget[3] = (char) (num4 >> 24 & (int) sbyte.MaxValue);
                  pSrc += 8;
                  pTarget[4] = (char) (num5 & (int) sbyte.MaxValue);
                  pTarget[5] = (char) (num5 >> 8 & (int) sbyte.MaxValue);
                  pTarget[6] = (char) (num5 >> 16 & (int) sbyte.MaxValue);
                  pTarget[7] = (char) (num5 >> 24 & (int) sbyte.MaxValue);
                  pTarget += 8;
                }
                else
                  goto label_60;
              }
              break;
label_60:
              num3 = num4 & (int) byte.MaxValue;
              ++pSrc;
              if (num3 <= (int) sbyte.MaxValue)
              {
                *pTarget = (char) num3;
                pTarget += 2;
                continue;
              }
            }
label_62:
            int num6 = (int) *pSrc;
            ++pSrc;
            if ((num3 & 64) != 0 && (num6 & -64) == 128)
            {
              int num4 = num6 & 63;
              int num5;
              if ((num3 & 32) != 0)
              {
                int num7 = num4 | (num3 & 15) << 6;
                if ((num3 & 16) != 0)
                {
                  int num8 = (int) *pSrc;
                  if (UTF8Encoding.InRange(num7 >> 4, 1, 16) && (num8 & -64) == 128)
                  {
                    int num9 = num7 << 6 | num8 & 63;
                    int num10 = (int) pSrc[1];
                    if ((num10 & -64) == 128)
                    {
                      pSrc += 2;
                      int num11 = num9 << 6 | num10 & 63;
                      *pTarget = (char) ((num11 >> 10 & 2047) - 10304);
                      pTarget += 2;
                      num5 = (num11 & 1023) - 9216;
                      chPtr2 -= 2;
                    }
                    else
                      goto label_75;
                  }
                  else
                    goto label_75;
                }
                else
                {
                  int num8 = (int) *pSrc;
                  if ((num7 & 992) != 0 && (num7 & 992) != 864 && (num8 & -64) == 128)
                  {
                    ++pSrc;
                    num5 = num7 << 6 | num8 & 63;
                    chPtr2 -= 2;
                  }
                  else
                    goto label_75;
                }
              }
              else
              {
                int num7 = num3 & 31;
                if (num7 > 1)
                  num5 = num7 << 6 | num4;
                else
                  goto label_75;
              }
              *pTarget = (char) num5;
              pTarget += 2;
              chPtr2 -= 2;
              continue;
            }
label_75:
            pSrc -= 2;
            ch = 0;
            goto label_2;
          }
          ch = 0;
        }
      }
      if (ch != 0 && (baseDecoder == null || baseDecoder.MustFlush))
      {
        if (fallback == null)
        {
          fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
          fallback.InternalInitialize(bytes, chPtr1);
        }
        if (!this.FallbackInvalidByteSequence(ref pSrc, ch, fallback, ref pTarget))
        {
          fallback.InternalReset();
          this.ThrowCharsOverflow(baseDecoder, pTarget == chars);
        }
        ch = 0;
      }
      if (baseDecoder != null)
      {
        ((UTF8Encoding.UTF8Decoder) baseDecoder).bits = ch;
        baseDecoder.m_bytesUsed = (int) (pSrc - bytes);
      }
      return UTF8Encoding.PtrDiff(pTarget, chars);
    }

    [SecurityCritical]
    private unsafe bool FallbackInvalidByteSequence(ref byte* pSrc, int ch, DecoderFallbackBuffer fallback, ref char* pTarget)
    {
      byte* pSrc1 = pSrc;
      byte[] bytesUnknown = this.GetBytesUnknown(ref pSrc1, ch);
      if (fallback.InternalFallback(bytesUnknown, pSrc, ref pTarget))
        return true;
      pSrc = pSrc1;
      return false;
    }

    [SecurityCritical]
    private unsafe int FallbackInvalidByteSequence(byte* pSrc, int ch, DecoderFallbackBuffer fallback)
    {
      byte[] bytesUnknown = this.GetBytesUnknown(ref pSrc, ch);
      return fallback.InternalFallback(bytesUnknown, pSrc);
    }

    [SecurityCritical]
    private unsafe byte[] GetBytesUnknown(ref byte* pSrc, int ch)
    {
      byte[] numArray;
      if (ch < 256 && ch >= 0)
      {
        --pSrc;
        numArray = new byte[1]{ (byte) ch };
      }
      else if ((ch & 402653184) == 0)
      {
        --pSrc;
        numArray = new byte[1]
        {
          (byte) (ch & 31 | 192)
        };
      }
      else if ((ch & 268435456) != 0)
      {
        if ((ch & 8388608) != 0)
        {
          pSrc -= 3;
          numArray = new byte[3]
          {
            (byte) (ch >> 12 & 7 | 240),
            (byte) (ch >> 6 & 63 | 128),
            (byte) (ch & 63 | 128)
          };
        }
        else if ((ch & 131072) != 0)
        {
          pSrc -= 2;
          numArray = new byte[2]
          {
            (byte) (ch >> 6 & 7 | 240),
            (byte) (ch & 63 | 128)
          };
        }
        else
        {
          --pSrc;
          numArray = new byte[1]
          {
            (byte) (ch & 7 | 240)
          };
        }
      }
      else if ((ch & 8388608) != 0)
      {
        pSrc -= 2;
        numArray = new byte[2]
        {
          (byte) (ch >> 6 & 15 | 224),
          (byte) (ch & 63 | 128)
        };
      }
      else
      {
        --pSrc;
        numArray = new byte[1]
        {
          (byte) (ch & 15 | 224)
        };
      }
      return numArray;
    }

    /// <summary>获取可以将 UTF-8 编码的字节序列转换为 Unicode 字符序列的解码器。</summary>
    /// <returns>可以将 UTF-8 编码的字节序列转换为 Unicode 字符序列的解码器。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Decoder GetDecoder()
    {
      return (Decoder) new UTF8Encoding.UTF8Decoder(this);
    }

    /// <summary>获取可将 Unicode 字符序列转换为 UTF-8 编码的字节序列的编码器。</summary>
    /// <returns>一个 <see cref="T:System.Text.Encoder" />，用于将 Unicode 字符序列转换为 UTF-8 编码的字节序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new UTF8Encoding.UTF8Encoder(this);
    }

    /// <summary>计算对指定数目的字符进行编码时产生的最大字节数。</summary>
    /// <returns>对指定数目的字符进行编码所产生的最大字节数。</returns>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      long num2 = num1 * 3L;
      if (num2 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num2;
    }

    /// <summary>计算对指定数目的字节进行解码时产生的最大字符数。</summary>
    /// <returns>对指定数目的字节进行解码时所产生的最大字符数。</returns>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    /// <summary>返回一个采用 UTF-8 格式编码的 Unicode 字节顺序标记（如果 <see cref="T:System.Text.UTF8Encoding" /> 编码对象配置为提供一个这样的标记）。</summary>
    /// <returns>一个包含 Unicode 字节顺序标记的字节数组（如果 <see cref="T:System.Text.UTF8Encoding" /> 编码对象配置为提供一个这样的字节数组）。否则，此方法返回一个零长度的字节数组。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override byte[] GetPreamble()
    {
      if (!this.emitUTF8Identifier)
        return EmptyArray<byte>.Value;
      return new byte[3]{ (byte) 239, (byte) 187, (byte) 191 };
    }

    /// <summary>确定指定的对象是否等于当前 <see cref="T:System.Text.UTF8Encoding" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 是 <see cref="T:System.Text.UTF8Encoding" /> 的一个实例并且等于当前对象，则为 true；否则，为 false。</returns>
    /// <param name="value">要与当前实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UTF8Encoding utF8Encoding = value as UTF8Encoding;
      if (utF8Encoding != null && this.emitUTF8Identifier == utF8Encoding.emitUTF8Identifier && this.EncoderFallback.Equals((object) utF8Encoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) utF8Encoding.DecoderFallback);
      return false;
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>当前实例的哈希代码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode() + 65001 + (this.emitUTF8Identifier ? 1 : 0);
    }

    [Serializable]
    internal class UTF8Encoder : EncoderNLS, ISerializable
    {
      internal int surrogateChar;

      internal override bool HasState
      {
        get
        {
          return (uint) this.surrogateChar > 0U;
        }
      }

      public UTF8Encoder(UTF8Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal UTF8Encoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        this.surrogateChar = (int) info.GetValue("surrogateChar", typeof (int));
        try
        {
          this.m_fallback = (EncoderFallback) info.GetValue("m_fallback", typeof (EncoderFallback));
        }
        catch (SerializationException ex)
        {
          this.m_fallback = (EncoderFallback) null;
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("surrogateChar", this.surrogateChar);
        info.AddValue("m_fallback", (object) this.m_fallback);
        info.AddValue("storedSurrogate", this.surrogateChar > 0);
        info.AddValue("mustFlush", false);
      }

      public override void Reset()
      {
        this.surrogateChar = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }

    [Serializable]
    internal class UTF8Decoder : DecoderNLS, ISerializable
    {
      internal int bits;

      internal override bool HasState
      {
        get
        {
          return (uint) this.bits > 0U;
        }
      }

      public UTF8Decoder(UTF8Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal UTF8Decoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        try
        {
          this.bits = (int) info.GetValue("wbits", typeof (int));
          this.m_fallback = (DecoderFallback) info.GetValue("m_fallback", typeof (DecoderFallback));
        }
        catch (SerializationException ex)
        {
          this.bits = 0;
          this.m_fallback = (DecoderFallback) null;
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException("info");
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("wbits", this.bits);
        info.AddValue("m_fallback", (object) this.m_fallback);
        info.AddValue("bits", 0);
        info.AddValue("trailCount", 0);
        info.AddValue("isSurrogate", false);
        info.AddValue("byteSequence", 0);
      }

      public override void Reset()
      {
        this.bits = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }
  }
}
