// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderReplacementFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Text
{
  /// <summary>表示无法对原始输入字符进行编码时使用的替代输入字符串。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
  {
    private int fallbackCount = -1;
    private int fallbackIndex = -1;
    private string strDefault;

    /// <summary>获取待处理的替换回退缓冲区中的字符数。</summary>
    /// <returns>尚未处理的替换回退缓冲区中的字符数。</returns>
    /// <filterpriority>1</filterpriority>
    public override int Remaining
    {
      get
      {
        if (this.fallbackCount >= 0)
          return this.fallbackCount;
        return 0;
      }
    }

    /// <summary>使用 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象的值初始化 <see cref="T:System.Text.EncoderReplacementFallbackBuffer" /> 类的新实例。</summary>
    /// <param name="fallback">一个 <see cref="T:System.Text.EncoderReplacementFallback" /> 对象。</param>
    public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
    {
      this.strDefault = fallback.DefaultString + fallback.DefaultString;
    }

    /// <summary>准备好替换回退缓冲区，以使用当前替换字符串。</summary>
    /// <returns>如果替换字符串非空，则为 true；如果替换字符串为空，则为 false。</returns>
    /// <param name="charUnknown">一个输入字符。除非引发了异常，否则在该操作中会忽略此参数。</param>
    /// <param name="index">该字符在输入缓冲区中的索引位置。在该操作中会忽略此参数。</param>
    /// <exception cref="T:System.ArgumentException">此方法会在 <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> 方法读取了替换回退缓冲区中的所有字符之前被再次调用。</exception>
    /// <filterpriority>1</filterpriority>
    public override bool Fallback(char charUnknown, int index)
    {
      if (this.fallbackCount >= 1)
      {
        if (char.IsHighSurrogate(charUnknown) && this.fallbackCount >= 0 && char.IsLowSurrogate(this.strDefault[this.fallbackIndex + 1]))
          this.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknown, this.strDefault[this.fallbackIndex + 1]));
        this.ThrowLastCharRecursive((int) charUnknown);
      }
      this.fallbackCount = this.strDefault.Length / 2;
      this.fallbackIndex = -1;
      return (uint) this.fallbackCount > 0U;
    }

    /// <summary>指示当无法对输入代理项对进行编码时是否可以使用替换字符串，或者是否可以忽略代理项对。参数指定代理项对及其在输入中的索引位置。</summary>
    /// <returns>如果替换字符串非空，则为 true；如果替换字符串为空，则为 false。</returns>
    /// <param name="charUnknownHigh">输入对的高代理项。</param>
    /// <param name="charUnknownLow">输入对的低代理项。</param>
    /// <param name="index">该代理项对在输入缓冲区中的索引位置。</param>
    /// <exception cref="T:System.ArgumentException">此方法会在 <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> 方法读取了所有替换字符串字符之前被再次调用。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charUnknownHigh" /> 的数值小于 U+D800 或小于 U+D800。- 或 -<paramref name="charUnknownLow" /> 的数值小于 U+DC00 或大于 U+DC00。</exception>
    /// <filterpriority>1</filterpriority>
    public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      if (this.fallbackCount >= 1)
        this.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknownHigh, charUnknownLow));
      this.fallbackCount = this.strDefault.Length;
      this.fallbackIndex = -1;
      return (uint) this.fallbackCount > 0U;
    }

    /// <summary>检索替换回退缓冲区中的下一个字符。</summary>
    /// <returns>应用程序可对其进行编码的替换回退缓冲区中的下一个 Unicode 字符。</returns>
    /// <filterpriority>2</filterpriority>
    public override char GetNextChar()
    {
      this.fallbackCount = this.fallbackCount - 1;
      this.fallbackIndex = this.fallbackIndex + 1;
      if (this.fallbackCount < 0)
        return char.MinValue;
      if (this.fallbackCount != int.MaxValue)
        return this.strDefault[this.fallbackIndex];
      this.fallbackCount = -1;
      return char.MinValue;
    }

    /// <summary>导致下一个 <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> 方法调用访问当前字符位置之前的替换回退缓冲区中的字符位置。</summary>
    /// <returns>如果 <see cref="M:System.Text.EncoderReplacementFallbackBuffer.MovePrevious" /> 操作成功，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool MovePrevious()
    {
      if (this.fallbackCount < -1 || this.fallbackIndex < 0)
        return false;
      this.fallbackIndex = this.fallbackIndex - 1;
      this.fallbackCount = this.fallbackCount + 1;
      return true;
    }

    /// <summary>初始化 <see cref="T:System.Text.EncoderReplacementFallbackBuffer" /> 的此实例中的所有内部状态信息和数据。</summary>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override unsafe void Reset()
    {
      this.fallbackCount = -1;
      this.fallbackIndex = 0;
      this.charStart = (char*) null;
      this.bFallingBack = false;
    }
  }
}
