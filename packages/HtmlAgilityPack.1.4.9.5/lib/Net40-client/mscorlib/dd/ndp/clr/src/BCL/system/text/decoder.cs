// Decompiled with JetBrains decompiler
// Type: System.Text.Decoder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>将一个编码字节序列转换为一组字符。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Decoder
  {
    internal DecoderFallback m_fallback;
    [NonSerialized]
    internal DecoderFallbackBuffer m_fallbackBuffer;

    /// <summary>获取或设置当前 <see cref="T:System.Text.Decoder" /> 对象的 <see cref="T:System.Text.DecoderFallback" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.Text.DecoderFallback" /> 对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">设置操作中的值为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentException">无法在设置操作中赋新值，这是因为当前 <see cref="T:System.Text.DecoderFallbackBuffer" /> 对象含有尚未解码的数据。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public DecoderFallback Fallback
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
        this.m_fallbackBuffer = (DecoderFallbackBuffer) null;
      }
    }

    /// <summary>获取与当前 <see cref="T:System.Text.Decoder" /> 对象关联的 <see cref="T:System.Text.DecoderFallbackBuffer" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.Text.DecoderFallbackBuffer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public DecoderFallbackBuffer FallbackBuffer
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_fallbackBuffer == null)
          this.m_fallbackBuffer = this.m_fallback == null ? DecoderFallback.ReplacementFallback.CreateFallbackBuffer() : this.m_fallback.CreateFallbackBuffer();
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

    /// <summary>初始化 <see cref="T:System.Text.Decoder" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Decoder()
    {
    }

    internal void SerializeDecoder(SerializationInfo info)
    {
      info.AddValue("m_fallback", (object) this.m_fallback);
    }

    /// <summary>在派生类中重写时，将解码器设置回它的初始状态。</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Reset()
    {
      byte[] bytes = new byte[0];
      char[] chars = new char[this.GetCharCount(bytes, 0, 0, true)];
      this.GetChars(bytes, 0, 0, chars, 0, true);
      if (this.m_fallbackBuffer == null)
        return;
      this.m_fallbackBuffer.Reset();
    }

    /// <summary>在派生类中重写时，计算对字节序列（从指定字节数组开始）进行解码所产生的字符数。</summary>
    /// <returns>对指定的字节序列和内部缓冲区中的任何字节进行解码所产生的字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetCharCount(byte[] bytes, int index, int count);

    /// <summary>在派生类中重写时，计算对字节序列（从指定字节数组开始）进行解码所产生的字符数。一个参数，指示计算后是否要清除解码器的内部状态。</summary>
    /// <returns>对指定的字节序列和内部缓冲区中的任何字节进行解码所产生的字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="index">第一个要解码的字节的索引。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <param name="flush">如果要在计算后模拟编码器内部状态的清除过程，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 或 <paramref name="count" /> 小于零。- 或 -<paramref name="index" /> 和 <paramref name="count" /> 不表示 <paramref name="bytes" /> 中的有效范围。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush)
    {
      return this.GetCharCount(bytes, index, count);
    }

    /// <summary>在派生类中重写时，计算对字节序列（从指定的字节指针开始）进行解码所产生的字符数。一个参数，指示计算后是否要清除解码器的内部状态。</summary>
    /// <returns>对指定的字节序列和内部缓冲区中的任何字节进行解码所产生的字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="count">要解码的字节数。</param>
    /// <param name="flush">如果要在计算后模拟编码器内部状态的清除过程，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 是 null （在 Visual Basic .NET 中为 Nothing ）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetCharCount(byte* bytes, int count, bool flush)
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

    /// <summary>在派生类中重写时，将指定字节数组的字节序列和内部缓冲区中的任何字节解码到指定的字符数组。</summary>
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
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

    /// <summary>在派生类中重写时，将指定字节数组的字节序列和内部缓冲区中的任何字节解码到指定的字符数组。一个参数，指示转换后是否要清除解码器的内部状态。</summary>
    /// <returns>写入 <paramref name="chars" /> 参数的实际字符数。</returns>
    /// <param name="bytes">包含要解码的字节序列的字节数组。</param>
    /// <param name="byteIndex">第一个要解码的字节的索引。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">要用于包含所产生的字符集的字符数组。</param>
    /// <param name="charIndex">开始写入所产生的字符集的索引位置。</param>
    /// <param name="flush">如果要在转换后清除解码器的内部状态，则为 true；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。- 或 -<paramref name="chars" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteIndex" />、<paramref name="byteCount" /> 或 <paramref name="charIndex" /> 小于零。- 或 -<paramref name="byteindex" /> 和 <paramref name="byteCount" /> 不表示 <paramref name="bytes" /> 中的有效范围。- 或 -<paramref name="charIndex" /> 不是 <paramref name="chars" /> 中的有效索引。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="chars" /> 中从 <paramref name="charIndex" /> 到数组结尾没有足够容量来容纳所产生的字符。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
    {
      return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
    }

    /// <summary>在派生类中重写时，将字节序列（从指定的字节指针处开始）和任何内部缓冲区中的字节解码为从指定字符指针开始存储的一组字符。一个参数，指示转换后是否要清除解码器的内部状态。</summary>
    /// <returns>在由 <paramref name="chars" /> 参数指示的位置处写入的实际字符数。</returns>
    /// <param name="bytes">指向第一个要解码的字节的指针。</param>
    /// <param name="byteCount">要解码的字节数。</param>
    /// <param name="chars">一个指针，指向开始写入所产生的字符集的位置。</param>
    /// <param name="charCount">要写入的最大字符数。</param>
    /// <param name="flush">如果要在转换后清除解码器的内部状态，则为 true；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> 为 null (Nothing)。- 或 -<paramref name="chars" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="byteCount" /> 或 <paramref name="charCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="charCount" /> 少于所产生的字符数。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] bytes1 = new byte[byteCount];
      for (int index = 0; index < byteCount; ++index)
        bytes1[index] = bytes[index];
      char[] chars1 = new char[charCount];
      int chars2 = this.GetChars(bytes1, 0, byteCount, chars1, 0, flush);
      if (chars2 < charCount)
        charCount = chars2;
      for (int index = 0; index < charCount; ++index)
        chars[index] = chars1[index];
      return charCount;
    }

    /// <summary>将已编码字节的数组转换为 UTF-16 编码字符，然后将结果存储在字符数组中。</summary>
    /// <param name="bytes">要转换的字节数组。</param>
    /// <param name="byteIndex">要转换的 <paramref name="bytes" /> 的第一个元素。</param>
    /// <param name="byteCount">要转换的 <paramref name="bytes" /> 的元素数。</param>
    /// <param name="chars">一个数组，存储已转换的字符。</param>
    /// <param name="charIndex">存储数据的 <paramref name="chars" /> 中的第一个元素。</param>
    /// <param name="charCount">要用于转换的 <paramref name="chars" /> 中的最大元素数。</param>
    /// <param name="flush">如果要指示没有要转换的更多数据，则为 true；否则为 false。</param>
    /// <param name="bytesUsed">此方法在返回时包含用于转换的字节数。该参数未经初始化即被传递。</param>
    /// <param name="charsUsed">此方法在返回时包含转换产生的 <paramref name="chars" /> 中的字符数。该参数未经初始化即被传递。</param>
    /// <param name="completed">此方法返回时，如果 <paramref name="byteCount" /> 指定的所有字符均已转换，则包含 true；否则包含 false。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 或 <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charIndex" />、<paramref name="charCount" />、<paramref name="byteIndex" /> 或 <paramref name="byteCount" /> 小于零。- 或 -<paramref name="chars" /> 的长度 -<paramref name="charIndex" /> 小于 <paramref name="charCount" />。- 或 -<paramref name="bytes" /> 的长度 -<paramref name="byteIndex" /> 小于 <paramref name="byteCount" />。</exception>
    /// <exception cref="T:System.ArgumentException">输出缓冲区太小，无法包含任何已转换的输入。输出缓冲区应大于或等于 <see cref="Overload:System.Text.Decoder.GetCharCount" /> 方法指示的大小。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
      if (bytes == null || chars == null)
        throw new ArgumentNullException(bytes == null ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      bytesUsed = byteCount;
      while (bytesUsed > 0)
      {
        if (this.GetCharCount(bytes, byteIndex, bytesUsed, flush) <= charCount)
        {
          charsUsed = this.GetChars(bytes, byteIndex, bytesUsed, chars, charIndex, flush);
          completed = bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        bytesUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }

    /// <summary>将已编码字节的缓冲区转换为 UTF-16 编码字符，然后将结果存储在另一个缓冲区中。</summary>
    /// <param name="bytes">包含要转换的字节序列的缓冲区地址。</param>
    /// <param name="byteCount">要转换的 <paramref name="bytes" /> 中的字节数。</param>
    /// <param name="chars">要存储已转换字符的缓冲区地址。</param>
    /// <param name="charCount">要用于转换的 <paramref name="chars" /> 中的最大字符数。</param>
    /// <param name="flush">如果要指示没有要转换的更多数据，则为 true；否则为 false。</param>
    /// <param name="bytesUsed">此方法在返回时包含转换所产生的字节数。该参数未经初始化即被传递。</param>
    /// <param name="charsUsed">此方法在返回时包含用于转换的 <paramref name="chars" /> 中的字符数。该参数未经初始化即被传递。</param>
    /// <param name="completed">此方法返回时，如果 <paramref name="byteCount" /> 指定的所有字符均已转换，则包含 true；否则包含 false。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="chars" /> 或 <paramref name="bytes" /> 为 null (Nothing)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 或 <paramref name="byteCount" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">输出缓冲区太小，无法包含任何已转换的输入。输出缓冲区应大于或等于 <see cref="Overload:System.Text.Decoder.GetCharCount" /> 方法指示的大小。</exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">发生回退（请参见.NET Framework 中的字符编码以获得更详细的解释）－和－将 <see cref="P:System.Text.Decoder.Fallback" /> 设置为 <see cref="T:System.Text.DecoderExceptionFallback" />。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      bytesUsed = byteCount;
      while (bytesUsed > 0)
      {
        if (this.GetCharCount(bytes, bytesUsed, flush) <= charCount)
        {
          charsUsed = this.GetChars(bytes, bytesUsed, chars, charCount, flush);
          completed = bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        bytesUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }
  }
}
