// Decompiled with JetBrains decompiler
// Type: System.Text.Encoder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>将一组字符转换为一个字节序列。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Encoder
  {
    internal EncoderFallback m_fallback;
    [NonSerialized]
    internal EncoderFallbackBuffer m_fallbackBuffer;

    /// <summary>获取或设置当前 <see cref="T:System.Text.Encoder" /> 对象的 <see cref="T:System.Text.EncoderFallback" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.Text.EncoderFallback" /> 对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">设置操作中的值为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentException">无法在设置操作中赋新值，这是因为当前 <see cref="T:System.Text.EncoderFallbackBuffer" /> 对象含有尚未编码的数据。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public EncoderFallback Fallback
    {
      [__DynamicallyInvokable] get
      {
        return this.m_fallback;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (this.m_fallbackBuffer != null && this.m_fallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_FallbackBufferNotEmpty"), "value");
        this.m_fallback = value;
        this.m_fallbackBuffer = (EncoderFallbackBuffer) null;
      }
    }

    /// <summary>获取与当前 <see cref="T:System.Text.Encoder" /> 对象关联的 <see cref="T:System.Text.EncoderFallbackBuffer" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.Text.EncoderFallbackBuffer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public EncoderFallbackBuffer FallbackBuffer
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_fallbackBuffer == null)
          this.m_fallbackBuffer = this.m_fallback == null ? EncoderFallback.ReplacementFallback.CreateFallbackBuffer() : this.m_fallback.CreateFallbackBuffer();
        return this.m_fallbackBuffer;
      }
    }

    internal bool InternalHasFallbackBuffer
    {
      get
      {
        return this.m_fallbackBuffer != null;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.Encoder" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Encoder()
    {
    }

    internal void SerializeEncoder(SerializationInfo info)
    {
      info.AddValue("m_fallback", (object) this.m_fallback);
    }

    /// <summary>在派生类中重写时，将编码器设置回它的初始状态。</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Reset()
    {
      char[] chars = new char[0];
      byte[] bytes = new byte[this.GetByteCount(chars, 0, 0, true)];
      this.GetBytes(chars, 0, 0, bytes, 0, true);
      if (this.m_fallbackBuffer == null)
        return;
      this.m_fallbackBuffer.Reset();
    }

    /// <summary>在派生类中重写时，计算对指定字符数组中的一组字符进行编码所产生的字节数。一个参数指示计算后是否要清除编码器的内部状态。</summary>
    /// <returns>通过对指定字符和内部缓冲区中的所有字符进行编码而产生的字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="index">第一个要编码的字符的索引。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <param name="flush">如果要在计算后模拟编码器内部状态的清除过程，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="chars" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetByteCount(char[] chars, int index, int count, bool flush);

    /// <summary>在派生类中重写时，计算对一组字符（从指定的字符指针处开始）进行编码所产生的字节数。一个参数指示计算后是否要清除编码器的内部状态。</summary>
    /// <returns>通过对指定字符和内部缓冲区中的所有字符进行编码而产生的字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="count">要编码的字符的数目。</param>
    /// <param name="flush">如果要在计算后模拟编码器内部状态的清除过程，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 是 null（在 Visual Basic .NET 中为 Nothing）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetByteCount(char* chars, int count, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[count];
      for (int index = 0; index < count; ++index)
        chars1[index] = chars[index];
      return this.GetByteCount(chars1, 0, count, flush);
    }

    /// <summary>在派生类中重写时，将指定字符数组中的一组字符和内部缓冲区中的任何字符编码到指定的字节数组中。一个参数指示转换后是否清除编码器的内部状态。</summary>
    /// <returns>写入 <paramref name="bytes" /> 的实际字节数。</returns>
    /// <param name="chars">包含要编码的字符集的字符数组。</param>
    /// <param name="charIndex">第一个要编码的字符的索引。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">要包含所产生的字节序列的字节数组。</param>
    /// <param name="byteIndex">开始写入所产生的字节序列的索引位置。</param>
    /// <param name="flush">如果要在转换后清除编码器的内部状态，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null (Nothing)。- 或 -<paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" /> 或 <paramref name="byteIndex" /> 小于零。- 或 -<paramref name="charIndex" /> 和 <paramref name="charCount" /> 不表示 <paramref name="chars" /> 中的有效范围。- 或 -<paramref name="byteIndex" /> 不是 <paramref name="bytes" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="bytes" /> 中从 <paramref name="byteIndex" /> 到数组结尾没有足够的容量来容纳所产生的字节。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

    /// <summary>在派生类中重写时，将一组字符（从指定的字符指针处开始）和内部缓冲区中的任何字符编码为从指定字节指针开始存储的字节序列。一个参数指示转换后是否清除编码器的内部状态。</summary>
    /// <returns>在由 <paramref name="bytes" /> 参数指示的位置处写入的实际字节数。</returns>
    /// <param name="chars">指向第一个要编码的字符的指针。</param>
    /// <param name="charCount">要编码的字符的数目。</param>
    /// <param name="bytes">一个指针，指向开始写入所产生的字节序列的位置。</param>
    /// <param name="byteCount">最多写入的字节数。</param>
    /// <param name="flush">如果要在转换后清除编码器的内部状态，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 为 null (Nothing)。- 或 -<paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="byteCount" /> 少于所产生的字节数。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[charCount];
      for (int index = 0; index < charCount; ++index)
        chars1[index] = chars[index];
      byte[] bytes1 = new byte[byteCount];
      int bytes2 = this.GetBytes(chars1, 0, charCount, bytes1, 0, flush);
      if (bytes2 < byteCount)
        byteCount = bytes2;
      for (int index = 0; index < byteCount; ++index)
        bytes[index] = bytes1[index];
      return byteCount;
    }

    /// <summary>将 Unicode 字符数组转换为编码的字节序列并将结果存储在字节数组中。</summary>
    /// <param name="chars">要转换的字符数组。</param>
    /// <param name="charIndex">要转换的 <paramref name="chars" /> 的第一个元素。</param>
    /// <param name="charCount">要转换的 <paramref name="chars" /> 的元素数。</param>
    /// <param name="bytes">一个数组，其中存储已转换的字节。</param>
    /// <param name="byteIndex">用来存储数据的 <paramref name="bytes" /> 的第一个元素。</param>
    /// <param name="byteCount">要用于转换的 <paramref name="bytes" /> 的最大元素数。</param>
    /// <param name="flush">如果要指示没有要转换的更多数据，则为 true；否则为 false。</param>
    /// <param name="charsUsed">此方法在返回时包含用于转换的 <paramref name="chars" /> 中的字符数。该参数未经初始化即被传递。</param>
    /// <param name="bytesUsed">此方法在返回时包含转换所产生的字节数。该参数未经初始化即被传递。</param>
    /// <param name="completed">此方法返回时，如果 <paramref name="charCount" /> 指定的所有字符均已转换，则包含 true；否则包含 false。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 或 <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" />、<paramref name="byteIndex" /> 或 <paramref name="byteCount" /> 小于零。- 或 -<paramref name="chars" /> 的长度 -<paramref name="charIndex" /> 小于 <paramref name="charCount" />。- 或 -<paramref name="bytes" /> 的长度 -<paramref name="byteIndex" /> 小于 <paramref name="byteCount" />。</exception>
    /// <exception cref="T:System.ArgumentException">输出缓冲区太小，无法包含任何已转换的输入。输出缓冲区应大于或等于 <see cref="Overload:System.Text.Encoder.GetByteCount" /> 方法指示的大小。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      charsUsed = charCount;
      while (charsUsed > 0)
      {
        if (this.GetByteCount(chars, charIndex, charsUsed, flush) <= byteCount)
        {
          bytesUsed = this.GetBytes(chars, charIndex, charsUsed, bytes, byteIndex, flush);
          completed = charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        charsUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }

    /// <summary>将 Unicode 字符的缓冲区转换为编码的字节序列，然后将结果存储在另一个缓冲区中。</summary>
    /// <param name="chars">要转换的 UTF-16 编码字符的字符串地址。</param>
    /// <param name="charCount">要转换的 <paramref name="chars" /> 中的字符数。</param>
    /// <param name="bytes">用来存储已转换字节的缓冲区的地址。</param>
    /// <param name="byteCount">要用于转换的 <paramref name="bytes" /> 中的最大字节数。</param>
    /// <param name="flush">如果要指示没有要转换的更多数据，则为 true；否则为 false。</param>
    /// <param name="charsUsed">此方法在返回时包含用于转换的 <paramref name="chars" /> 中的字符数。该参数未经初始化即被传递。</param>
    /// <param name="bytesUsed">此方法在返回时包含用于转换的字节数。该参数未经初始化即被传递。</param>
    /// <param name="completed">此方法返回时，如果 <paramref name="charCount" /> 指定的所有字符均已转换，则包含 true；否则包含 false。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 或 <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">输出缓冲区太小，无法包含任何已转换的输入。输出缓冲区应大于或等于 <see cref="Overload:System.Text.Encoder.GetByteCount" /> 方法指示的大小。</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Encoder.Fallback" /> 设置为 <see cref="T:System.Text.EncoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      charsUsed = charCount;
      while (charsUsed > 0)
      {
        if (this.GetByteCount(chars, charsUsed, flush) <= byteCount)
        {
          bytesUsed = this.GetBytes(chars, charsUsed, bytes, byteCount, flush);
          completed = charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        charsUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }
  }
}
