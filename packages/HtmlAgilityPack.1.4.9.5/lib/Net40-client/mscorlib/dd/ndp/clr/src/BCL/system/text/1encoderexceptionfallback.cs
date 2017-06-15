// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderExceptionFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>当输入字符无法转换为编码的输出字节序列时引发 <see cref="T:System.Text.EncoderFallbackException" />。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
  {
    /// <summary>获取当前 <see cref="T:System.Text.EncoderExceptionFallbackBuffer" /> 对象中待处理的字符数。</summary>
    /// <returns>返回值始终为零。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <filterpriority>1</filterpriority>
    public override int Remaining
    {
      get
      {
        return 0;
      }
    }

    /// <summary>因为无法对输入字符进行编码而引发异常。参数指定无法转换的字符的值和索引位置。</summary>
    /// <returns>无。不返回值，因为 <see cref="M:System.Text.EncoderExceptionFallbackBuffer.Fallback(System.Char,System.Int32)" /> 方法始终引发异常。</returns>
    /// <param name="charUnknown">一个输入字符。</param>
    /// <param name="index">该字符在输入缓冲区中的索引位置。</param>
    /// <exception cref="T:System.Text.EncoderFallbackException">无法对 <paramref name="charUnknown" /> 进行编码。此方法始终引发异常，该异常报告 <paramref name="charUnknown" /> 和 <paramref name="index" /> 参数的值。</exception>
    /// <filterpriority>1</filterpriority>
    public override bool Fallback(char charUnknown, int index)
    {
      throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", (object) (int) charUnknown, (object) index), charUnknown, index);
    }

    /// <summary>因为无法对输入字符进行编码而引发异常。参数指定输入中代理项对的值和索引位置，未使用名义返回值。</summary>
    /// <returns>无。不返回值，因为 <see cref="M:System.Text.EncoderExceptionFallbackBuffer.Fallback(System.Char,System.Char,System.Int32)" /> 方法始终引发异常。</returns>
    /// <param name="charUnknownHigh">输入对的高代理项。</param>
    /// <param name="charUnknownLow">输入对的低代理项。</param>
    /// <param name="index">该代理项对在输入缓冲区中的索引位置。</param>
    /// <exception cref="T:System.Text.EncoderFallbackException">无法对由 <paramref name="charUnknownHigh" /> 和 <paramref name="charUnknownLow" /> 表示的字符进行编码。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charUnknownHigh" /> 或 <paramref name="charUnknownLow" /> 是无效的。<paramref name="charUnknownHigh" /> 不介于 U+D800 和 U+DBFF 之间（包括这两者），或者 <paramref name="charUnknownLow" /> 不介于 U+DC00 和 U+DFFF 之间（包括这两者）。</exception>
    /// <filterpriority>1</filterpriority>
    public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", (object) char.ConvertToUtf32(charUnknownHigh, charUnknownLow), (object) index), charUnknownHigh, charUnknownLow, index);
    }

    /// <summary>检索异常回退缓冲区中的下一个字符。</summary>
    /// <returns>返回值始终为 Unicode 字符 NULL (U+0000)。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <filterpriority>2</filterpriority>
    public override char GetNextChar()
    {
      return char.MinValue;
    }

    /// <summary>导致下一个 <see cref="M:System.Text.EncoderExceptionFallbackBuffer.GetNextChar" /> 方法调用访问当前位置之前的异常数据缓冲区字符位置。</summary>
    /// <returns>返回值始终为 false。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool MovePrevious()
    {
      return false;
    }
  }
}
