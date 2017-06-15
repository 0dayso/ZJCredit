// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderReplacementFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Text
{
  /// <summary>表示无法对原始输入字节序列解码时发出的替代输出字符串。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
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

    /// <summary>使用 <see cref="T:System.Text.DecoderReplacementFallback" /> 对象的值初始化 <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> 类的新实例。</summary>
    /// <param name="fallback">含有替换字符串的 <see cref="T:System.Text.DecoderReplacementFallback" /> 对象。</param>
    public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
    {
      this.strDefault = fallback.DefaultString;
    }

    /// <summary>准备好替换回退缓冲区，以使用当前替换字符串。</summary>
    /// <returns>如果替换字符串非空，则为 true；如果替换字符串为空，则为 false。</returns>
    /// <param name="bytesUnknown">一个输入字节序列。除非引发了异常，否则将忽略此参数。</param>
    /// <param name="index">
    /// <paramref name="bytesUnknown" /> 中字节的索引位置。在该操作中会忽略此参数。</param>
    /// <exception cref="T:System.ArgumentException">在 <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> 方法读取了替换回退缓冲区中的所有字符之前，此方法会被再次调用。</exception>
    /// <filterpriority>1</filterpriority>
    public override bool Fallback(byte[] bytesUnknown, int index)
    {
      if (this.fallbackCount >= 1)
        this.ThrowLastBytesRecursive(bytesUnknown);
      if (this.strDefault.Length == 0)
        return false;
      this.fallbackCount = this.strDefault.Length;
      this.fallbackIndex = -1;
      return true;
    }

    /// <summary>检索替换回退缓冲区中的下一个字符。</summary>
    /// <returns>替换回退缓冲区中的下一个字符。</returns>
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

    /// <summary>导致下一个 <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> 调用访问替换回退缓冲区中当前字符位置之前的字符位置。</summary>
    /// <returns>如果 <see cref="M:System.Text.DecoderReplacementFallbackBuffer.MovePrevious" /> 操作成功，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool MovePrevious()
    {
      if (this.fallbackCount < -1 || this.fallbackIndex < 0)
        return false;
      this.fallbackIndex = this.fallbackIndex - 1;
      this.fallbackCount = this.fallbackCount + 1;
      return true;
    }

    /// <summary>初始化 <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> 对象中的所有内部状态信息和数据。</summary>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public override unsafe void Reset()
    {
      this.fallbackCount = -1;
      this.fallbackIndex = -1;
      this.byteStart = (byte*) null;
    }

    [SecurityCritical]
    internal override unsafe int InternalFallback(byte[] bytes, byte* pBytes)
    {
      return this.strDefault.Length;
    }
  }
}
