// Decompiled with JetBrains decompiler
// Type: System.Text.UTF32Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Text
{
  /// <summary>表示 Unicode 字符的 UTF-32 编码。</summary>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class UTF32Encoding : Encoding
  {
    private bool emitUTF32ByteOrderMark;
    private bool isThrowException;
    private bool bigEndian;

    /// <summary>初始化 <see cref="T:System.Text.UTF32Encoding" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public UTF32Encoding()
      : this(false, true, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UTF32Encoding" /> 类的新实例。参数指定是否使用 Big-Endian 字节顺序以及 <see cref="M:System.Text.UTF32Encoding.GetPreamble" /> 方法是否返回 Unicode Unicode 字节顺序标记。</summary>
    /// <param name="bigEndian">如果为 true，则使用 Big-Endian 字节顺序（从最高有效字节开始）；如果为 false，则使用 Little-Endian 字节顺序（从最低有效字节开始）。</param>
    /// <param name="byteOrderMark">如果为 true，则指定提供 Unicode 字节顺序标记；否则为 false。</param>
    [__DynamicallyInvokable]
    public UTF32Encoding(bool bigEndian, bool byteOrderMark)
      : this(bigEndian, byteOrderMark, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.UTF32Encoding" /> 类的新实例。参数指定是否使用 Big-Endian 字节顺序、是否提供 Unicode 字节顺序标记以及当检测到无效编码时是否引发异常。</summary>
    /// <param name="bigEndian">如果为 true，则使用 Big-Endian 字节顺序（从最高有效字节开始）；如果为 false，则使用 Little-Endian 字节顺序（从最低有效字节开始）。</param>
    /// <param name="byteOrderMark">如果为 true，则指定提供 Unicode 字节顺序标记；否则为 false。</param>
    /// <param name="throwOnInvalidCharacters">如果为 true，则指定在检测到无效的编码时应当引发异常；否则为 false。</param>
    [__DynamicallyInvokable]
    public UTF32Encoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidCharacters)
      : base(bigEndian ? 12001 : 12000)
    {
      this.bigEndian = bigEndian;
      this.emitUTF32ByteOrderMark = byteOrderMark;
      this.isThrowException = throwOnInvalidCharacters;
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
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <param name="s">包含要编码的字符集的 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="s" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 - 产生的字节数将大于可能返回一个整数作为最大数量。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="chars" /> 包含无效的字符序列。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 为完整的说明）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
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
    /// <returns>在由 <paramref name="bytes" /> 参数指示的位置处写入的实际字节数。</returns>
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
    public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetChars(bytes, byteCount, chars, charCount, (DecoderNLS) null);
    }

    /// <summary>将字节数组中某个范围的字节解码为一个字符串。</summary>
    /// <returns>包含指定字节序列解码结果的字符串。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="index" /> 和 <paramref name="count" /> 不表示中的有效范围 <paramref name="bytes" />。</exception>
    /// <exception cref="T:System.ArgumentException">启用了错误检测，和 <paramref name="bytes" /> 包含无效的字节序列。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退 （请参阅 .NET Framework 中的字符编码 有关的完整说明）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
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
      char* charEnd = chars + count;
      char* charStart = chars;
      int num = 0;
      char ch1 = char.MinValue;
      EncoderFallbackBuffer fallbackBuffer;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        fallbackBuffer = encoder.FallbackBuffer;
        if (fallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
      }
      else
        fallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
      while (true)
      {
        char ch2;
        while ((int) (ch2 = fallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
        {
          if ((int) ch2 == 0)
          {
            ch2 = *chars;
            chars += 2;
          }
          if ((int) ch1 != 0)
          {
            if (char.IsLowSurrogate(ch2))
            {
              ch1 = char.MinValue;
              num += 4;
            }
            else
            {
              chars -= 2;
              fallbackBuffer.InternalFallback(ch1, ref chars);
              ch1 = char.MinValue;
            }
          }
          else if (char.IsHighSurrogate(ch2))
            ch1 = ch2;
          else if (char.IsLowSurrogate(ch2))
            fallbackBuffer.InternalFallback(ch2, ref chars);
          else
            num += 4;
        }
        if ((encoder == null || encoder.MustFlush) && (int) ch1 > 0)
        {
          fallbackBuffer.InternalFallback(ch1, ref chars);
          ch1 = char.MinValue;
        }
        else
          break;
      }
      if (num < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      char* charStart = chars;
      char* charEnd = chars + charCount;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      char ch1 = char.MinValue;
      EncoderFallbackBuffer fallbackBuffer;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        fallbackBuffer = encoder.FallbackBuffer;
        if (encoder.m_throwOnOverflow && fallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
      }
      else
        fallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
      while (true)
      {
        char ch2;
        while ((int) (ch2 = fallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
        {
          if ((int) ch2 == 0)
          {
            ch2 = *chars;
            chars += 2;
          }
          if ((int) ch1 != 0)
          {
            if (char.IsLowSurrogate(ch2))
            {
              uint surrogate = this.GetSurrogate(ch1, ch2);
              ch1 = char.MinValue;
              if (bytes + 3 >= numPtr2)
              {
                if (fallbackBuffer.bFallingBack)
                {
                  fallbackBuffer.MovePrevious();
                  fallbackBuffer.MovePrevious();
                }
                else
                  chars -= 2;
                this.ThrowBytesOverflow(encoder, bytes == numPtr1);
                ch1 = char.MinValue;
                break;
              }
              if (this.bigEndian)
              {
                *bytes++ = (byte) 0;
                *bytes++ = (byte) (surrogate >> 16);
                *bytes++ = (byte) (surrogate >> 8);
                *bytes++ = (byte) surrogate;
              }
              else
              {
                *bytes++ = (byte) surrogate;
                *bytes++ = (byte) (surrogate >> 8);
                *bytes++ = (byte) (surrogate >> 16);
                *bytes++ = (byte) 0;
              }
            }
            else
            {
              chars -= 2;
              fallbackBuffer.InternalFallback(ch1, ref chars);
              ch1 = char.MinValue;
            }
          }
          else if (char.IsHighSurrogate(ch2))
            ch1 = ch2;
          else if (char.IsLowSurrogate(ch2))
          {
            fallbackBuffer.InternalFallback(ch2, ref chars);
          }
          else
          {
            if (bytes + 3 >= numPtr2)
            {
              if (fallbackBuffer.bFallingBack)
                fallbackBuffer.MovePrevious();
              else
                chars -= 2;
              this.ThrowBytesOverflow(encoder, bytes == numPtr1);
              break;
            }
            if (this.bigEndian)
            {
              *bytes++ = (byte) 0;
              *bytes++ = (byte) 0;
              *bytes++ = (byte) ((uint) ch2 >> 8);
              *bytes++ = (byte) ch2;
            }
            else
            {
              *bytes++ = (byte) ch2;
              *bytes++ = (byte) ((uint) ch2 >> 8);
              *bytes++ = (byte) 0;
              *bytes++ = (byte) 0;
            }
          }
        }
        if ((encoder == null || encoder.MustFlush) && (int) ch1 > 0)
        {
          fallbackBuffer.InternalFallback(ch1, ref chars);
          ch1 = char.MinValue;
        }
        else
          break;
      }
      if (encoder != null)
      {
        encoder.charLeftOver = ch1;
        encoder.m_charsUsed = (int) (chars - charStart);
      }
      return (int) (bytes - numPtr1);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      UTF32Encoding.UTF32Decoder utF32Decoder = (UTF32Encoding.UTF32Decoder) baseDecoder;
      int num1 = 0;
      byte* numPtr = bytes + count;
      byte* byteStart = bytes;
      int length = 0;
      uint num2 = 0;
      DecoderFallbackBuffer fallbackBuffer;
      if (utF32Decoder != null)
      {
        length = utF32Decoder.readByteCount;
        num2 = (uint) utF32Decoder.iChar;
        fallbackBuffer = utF32Decoder.FallbackBuffer;
      }
      else
        fallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(byteStart, (char*) null);
      while (bytes < numPtr && num1 >= 0)
      {
        num2 = !this.bigEndian ? (num2 >> 8) + ((uint) *bytes++ << 24) : (num2 << 8) + (uint) *bytes++;
        ++length;
        if (length >= 4)
        {
          length = 0;
          if (num2 > 1114111U || num2 >= 55296U && num2 <= 57343U)
          {
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[4]
              {
                (byte) (num2 >> 24),
                (byte) (num2 >> 16),
                (byte) (num2 >> 8),
                (byte) num2
              };
            else
              bytes1 = new byte[4]
              {
                (byte) num2,
                (byte) (num2 >> 8),
                (byte) (num2 >> 16),
                (byte) (num2 >> 24)
              };
            num1 += fallbackBuffer.InternalFallback(bytes1, bytes);
            num2 = 0U;
          }
          else
          {
            if (num2 >= 65536U)
              ++num1;
            ++num1;
            num2 = 0U;
          }
        }
      }
      if (length > 0 && (utF32Decoder == null || utF32Decoder.MustFlush))
      {
        byte[] bytes1 = new byte[length];
        if (this.bigEndian)
        {
          while (length > 0)
          {
            bytes1[--length] = (byte) num2;
            num2 >>= 8;
          }
        }
        else
        {
          while (length > 0)
          {
            bytes1[--length] = (byte) (num2 >> 24);
            num2 <<= 8;
          }
        }
        num1 += fallbackBuffer.InternalFallback(bytes1, bytes);
      }
      if (num1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      UTF32Encoding.UTF32Decoder utF32Decoder = (UTF32Encoding.UTF32Decoder) baseDecoder;
      char* chPtr1 = chars;
      char* chPtr2 = chars + charCount;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      int length = 0;
      uint iChar = 0;
      DecoderFallbackBuffer fallbackBuffer;
      if (utF32Decoder != null)
      {
        length = utF32Decoder.readByteCount;
        iChar = (uint) utF32Decoder.iChar;
        fallbackBuffer = baseDecoder.FallbackBuffer;
      }
      else
        fallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(bytes, chars + charCount);
      while (bytes < numPtr2)
      {
        iChar = !this.bigEndian ? (iChar >> 8) + ((uint) *bytes++ << 24) : (iChar << 8) + (uint) *bytes++;
        ++length;
        if (length >= 4)
        {
          length = 0;
          if (iChar > 1114111U || iChar >= 55296U && iChar <= 57343U)
          {
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[4]
              {
                (byte) (iChar >> 24),
                (byte) (iChar >> 16),
                (byte) (iChar >> 8),
                (byte) iChar
              };
            else
              bytes1 = new byte[4]
              {
                (byte) iChar,
                (byte) (iChar >> 8),
                (byte) (iChar >> 16),
                (byte) (iChar >> 24)
              };
            if (!fallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            {
              bytes -= 4;
              iChar = 0U;
              fallbackBuffer.InternalReset();
              this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
              break;
            }
            iChar = 0U;
          }
          else
          {
            if (iChar >= 65536U)
            {
              if ((UIntPtr) chars >= (UIntPtr) chPtr2 - new UIntPtr(2))
              {
                bytes -= 4;
                iChar = 0U;
                this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
                break;
              }
              *chars++ = this.GetHighSurrogate(iChar);
              iChar = (uint) this.GetLowSurrogate(iChar);
            }
            else if (chars >= chPtr2)
            {
              bytes -= 4;
              iChar = 0U;
              this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
              break;
            }
            *chars++ = (char) iChar;
            iChar = 0U;
          }
        }
      }
      if (length > 0 && (utF32Decoder == null || utF32Decoder.MustFlush))
      {
        byte[] bytes1 = new byte[length];
        int num = length;
        if (this.bigEndian)
        {
          while (num > 0)
          {
            bytes1[--num] = (byte) iChar;
            iChar >>= 8;
          }
        }
        else
        {
          while (num > 0)
          {
            bytes1[--num] = (byte) (iChar >> 24);
            iChar <<= 8;
          }
        }
        if (!fallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
        {
          fallbackBuffer.InternalReset();
          this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
        }
        else
        {
          length = 0;
          iChar = 0U;
        }
      }
      if (utF32Decoder != null)
      {
        utF32Decoder.iChar = (int) iChar;
        utF32Decoder.readByteCount = length;
        utF32Decoder.m_bytesUsed = (int) (bytes - numPtr1);
      }
      return (int) (chars - chPtr1);
    }

    private uint GetSurrogate(char cHigh, char cLow)
    {
      return (uint) (((int) cHigh - 55296) * 1024 + ((int) cLow - 56320) + 65536);
    }

    private char GetHighSurrogate(uint iChar)
    {
      return (char) ((iChar - 65536U) / 1024U + 55296U);
    }

    private char GetLowSurrogate(uint iChar)
    {
      return (char) ((iChar - 65536U) % 1024U + 56320U);
    }

    /// <summary>获取可以将 UTF-32 编码的字节序列转换为 Unicode 字符序列的解码器。</summary>
    /// <returns>一个 <see cref="T:System.Text.Decoder" />，用于将 UTF-32 编码的字节序列转换为 Unicode 字符序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Decoder GetDecoder()
    {
      return (Decoder) new UTF32Encoding.UTF32Decoder(this);
    }

    /// <summary>获取可将 Unicode 字符序列转换为 UTF-32 编码的字节序列的编码器。</summary>
    /// <returns>一个 <see cref="T:System.Text.Encoder" />，用于将 Unicode 字符序列转换为 UTF-32 编码的字节序列。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
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
      long num2 = num1 * 4L;
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
      int num = byteCount / 2 + 2;
      if (this.DecoderFallback.MaxCharCount > 2)
        num = num * this.DecoderFallback.MaxCharCount / 2;
      if (num > int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return num;
    }

    /// <summary>返回采用 UTF-32 格式编码的 Unicode 字节顺序标记（如果 <see cref="T:System.Text.UTF32Encoding" /> 对象配置为提供一个这样的标记）。</summary>
    /// <returns>一个包含 Unicode 字节顺序标记的字节数组（如果 <see cref="T:System.Text.UTF32Encoding" /> 对象配置为提供一个这样的字节数组）。否则，此方法返回一个零长度的字节数组。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override byte[] GetPreamble()
    {
      if (!this.emitUTF32ByteOrderMark)
        return EmptyArray<byte>.Value;
      if (this.bigEndian)
        return new byte[4]{ (byte) 0, (byte) 0, (byte) 254, byte.MaxValue };
      return new byte[4]{ byte.MaxValue, (byte) 254, (byte) 0, (byte) 0 };
    }

    /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Text.UTF32Encoding" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 是 <see cref="T:System.Text.UTF32Encoding" /> 的一个实例并且等于当前对象，则为 true；否则，为 false。</returns>
    /// <param name="value">要与当前对象进行比较的 <see cref="T:System.Object" />。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UTF32Encoding utF32Encoding = value as UTF32Encoding;
      if (utF32Encoding != null && this.emitUTF32ByteOrderMark == utF32Encoding.emitUTF32ByteOrderMark && (this.bigEndian == utF32Encoding.bigEndian && this.EncoderFallback.Equals((object) utF32Encoding.EncoderFallback)))
        return this.DecoderFallback.Equals((object) utF32Encoding.DecoderFallback);
      return false;
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Text.UTF32Encoding" /> 对象的哈希代码。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode() + this.CodePage + (this.emitUTF32ByteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
    }

    [Serializable]
    internal class UTF32Decoder : DecoderNLS
    {
      internal int iChar;
      internal int readByteCount;

      internal override bool HasState
      {
        get
        {
          return (uint) this.readByteCount > 0U;
        }
      }

      public UTF32Decoder(UTF32Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      public override void Reset()
      {
        this.iChar = 0;
        this.readByteCount = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }
    }
  }
}
