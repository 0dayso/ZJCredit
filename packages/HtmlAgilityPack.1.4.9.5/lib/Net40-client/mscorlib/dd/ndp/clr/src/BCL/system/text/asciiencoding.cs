// Decompiled with JetBrains decompiler
// Type: System.Text.ASCIIEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
  /// <summary>表示 Unicode 字符的 ASCII 字符编码。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ASCIIEncoding : Encoding
  {
    /// <summary>获取一个可以指示当前编码是否使用单字节码位的值。</summary>
    /// <returns>此属性恒为 true。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override bool IsSingleByte
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.ASCIIEncoding" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ASCIIEncoding()
      : base(20127)
    {
    }

    internal override void SetDefaultFallbacks()
    {
      this.encoderFallback = EncoderFallback.ReplacementFallback;
      this.decoderFallback = DecoderFallback.ReplacementFallback;
    }

    /// <summary>计算对指定字符数组中的一组字符进行编码时产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="index">第一个要编码的字符的索引。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
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

    /// <summary>计算对指定 <see cref="T:System.String" /> 中的字符进行编码时所产生的字节数。</summary>
    /// <returns>对指定字符进行编码后生成的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetByteCount(string chars)
    {
      if (chars == null)
        throw new ArgumentNullException("chars");
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
    /// <param name="chars">包含要编码的字符集的 <see cref="T:System.String" />。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">开始写入所产生的字节序列的索引位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。- 或 -<paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 -<paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 -<paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetBytes(string chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      string str = chars;
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
    /// <paramref name="chars" /> 为 null。- 或 -<paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 -<paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 -<paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="chars" /> 为 null。- 或 -<paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="byteCount" /> 少于所产生的字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.EncoderFallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
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
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
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
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
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
    /// <paramref name="bytes" /> 为 null。- 或 -<paramref name="chars" /> 为 null。</exception>
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
    /// <paramref name="bytes" /> 为 null。- 或 -<paramref name="chars" /> 为 null。</exception>
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
    /// <param name="byteIndex">第一个要解码的字节的索引。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得完整的解释）－和－将 <see cref="P:System.Text.Encoding.DecoderFallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string GetString(byte[] bytes, int byteIndex, int byteCount)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return string.Empty;
      fixed (byte* numPtr = bytes)
        return string.CreateStringFromEncoding(numPtr + byteIndex, byteCount, (Encoding) this);
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int charCount, EncoderNLS encoder)
    {
      char ch1 = char.MinValue;
      char* charEnd = chars + charCount;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, false);
        }
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        if ((int) ch1 > 0)
          ++charCount;
        return charCount;
      }
      int num = 0;
      if ((int) ch1 > 0)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, false);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
      }
      char ch2;
      while ((int) (ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
      {
        if ((int) ch2 == 0)
        {
          ch2 = *chars;
          chars += 2;
        }
        if ((int) ch2 > (int) sbyte.MaxValue)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, false);
          }
          encoderFallbackBuffer.InternalFallback(ch2, ref chars);
        }
        else
          ++num;
      }
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      char ch1 = char.MinValue;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* charEnd = chars + charCount;
      byte* numPtr1 = bytes;
      char* charStart = chars;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
        }
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        char ch2 = replacementFallback.DefaultString[0];
        if ((int) ch2 <= (int) sbyte.MaxValue)
        {
          if ((int) ch1 > 0)
          {
            if (byteCount == 0)
              this.ThrowBytesOverflow(encoder, true);
            *bytes++ = (byte) ch2;
            --byteCount;
          }
          if (byteCount < charCount)
          {
            this.ThrowBytesOverflow(encoder, byteCount < 1);
            charEnd = chars + byteCount;
          }
          while (chars < charEnd)
          {
            char ch3 = *chars++;
            *bytes++ = (int) ch3 < 128 ? (byte) ch3 : (byte) ch2;
          }
          if (encoder != null)
          {
            encoder.charLeftOver = char.MinValue;
            encoder.m_charsUsed = (int) (chars - charStart);
          }
          return (int) (bytes - numPtr1);
        }
      }
      byte* numPtr2 = bytes + byteCount;
      if ((int) ch1 > 0)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
      }
      char ch4;
      while ((int) (ch4 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != 0 || chars < charEnd)
      {
        if ((int) ch4 == 0)
        {
          ch4 = *chars;
          chars += 2;
        }
        if ((int) ch4 > (int) sbyte.MaxValue)
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch4, ref chars);
        }
        else
        {
          if (bytes >= numPtr2)
          {
            if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
              chars -= 2;
            else
              encoderFallbackBuffer.MovePrevious();
            this.ThrowBytesOverflow(encoder, bytes == numPtr1);
            break;
          }
          *bytes = (byte) ch4;
          ++bytes;
        }
      }
      if (encoder != null)
      {
        if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.m_charsUsed = (int) (chars - charStart);
      }
      return (int) (bytes - numPtr1);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
    {
      DecoderReplacementFallback replacementFallback = decoder != null ? decoder.Fallback as DecoderReplacementFallback : this.DecoderFallback as DecoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
        return count;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      int num1 = count;
      byte[] bytes1 = new byte[1];
      byte* numPtr = bytes + count;
      while (bytes < numPtr)
      {
        byte num2 = *bytes;
        ++bytes;
        if ((int) num2 >= 128)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          }
          bytes1[0] = num2;
          num1 = num1 - 1 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      byte* numPtr1 = bytes + byteCount;
      byte* numPtr2 = bytes;
      char* chPtr = chars;
      DecoderReplacementFallback replacementFallback = decoder != null ? decoder.Fallback as DecoderReplacementFallback : this.DecoderFallback as DecoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        char ch = replacementFallback.DefaultString[0];
        if (charCount < byteCount)
        {
          this.ThrowCharsOverflow(decoder, charCount < 1);
          numPtr1 = bytes + charCount;
        }
        while (bytes < numPtr1)
        {
          byte num = *bytes++;
          *chars++ = (int) num < 128 ? (char) num : ch;
        }
        if (decoder != null)
          decoder.m_bytesUsed = (int) (bytes - numPtr2);
        return (int) (chars - chPtr);
      }
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte[] bytes1 = new byte[1];
      char* charEnd = chars + charCount;
      while (bytes < numPtr1)
      {
        byte num = *bytes;
        ++bytes;
        if ((int) num >= 128)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr1 - byteCount, charEnd);
          }
          bytes1[0] = num;
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow(decoder, chars == chPtr);
            break;
          }
        }
        else
        {
          if (chars >= charEnd)
          {
            --bytes;
            this.ThrowCharsOverflow(decoder, chars == chPtr);
            break;
          }
          *chars = (char) num;
          chars += 2;
        }
      }
      if (decoder != null)
        decoder.m_bytesUsed = (int) (bytes - numPtr2);
      return (int) (chars - chPtr);
    }

    /// <summary>计算对指定数目的字符进行编码时产生的最大字节数。</summary>
    /// <returns>对指定数目的字符进行编码所产生的最大字节数。</returns>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 小于零。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num *= (long) this.EncoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num;
    }

    /// <summary>计算对指定数目的字节进行解码时产生的最大字符数。</summary>
    /// <returns>对指定数目的字节进行解码时所产生的最大字符数。</returns>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 小于零。- 或 -产生的字节数超过了作为一个整数返回时允许的最大字节数。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    /// <summary>获取可以将 ASCII 编码的字节序列转换为 Unicode 字符序列的解码器。</summary>
    /// <returns>
    /// <see cref="T:System.Text.Decoder" /> 用于将 ASCII 编码的字节序列转换为 Unicode 字符序列。</returns>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Decoder GetDecoder()
    {
      return (Decoder) new DecoderNLS((Encoding) this);
    }

    /// <summary>获取可将 Unicode 字符序列转换为 ASCII 编码的字节序列的编码器。</summary>
    /// <returns>一个 <see cref="T:System.Text.Encoder" />，它将一个 Unicode 字符序列转换为一个 ASCII 编码的字节序列。</returns>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
    }
  }
}
