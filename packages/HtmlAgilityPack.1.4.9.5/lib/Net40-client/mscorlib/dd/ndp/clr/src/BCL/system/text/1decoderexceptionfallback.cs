// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderExceptionFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Text
{
  /// <summary>当编码的输入字节序列无法转换为解码的输出字符时引发 <see cref="T:System.Text.DecoderFallbackException" />。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
  {
    /// <summary>获取当前 <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> 对象中待处理的字符数。</summary>
    /// <returns>返回值始终为零。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <filterpriority>1</filterpriority>
    public override int Remaining
    {
      get
      {
        return 0;
      }
    }

    /// <summary>当无法对输入字节序列解码时引发 <see cref="T:System.Text.DecoderFallbackException" />。不使用名义返回值。</summary>
    /// <returns>无。没有返回值，原因是 <see cref="M:System.Text.DecoderExceptionFallbackBuffer.Fallback(System.Byte[],System.Int32)" /> 方法始终引发异常。名义返回值为 true。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <param name="bytesUnknown">字节的输入数组。</param>
    /// <param name="index">输入中字节的索引位置。</param>
    /// <exception cref="T:System.Text.DecoderFallbackException">此方法总是引发一个异常，该异常报告无法解码的输入字节的值和索引位置。</exception>
    /// <filterpriority>1</filterpriority>
    public override bool Fallback(byte[] bytesUnknown, int index)
    {
      this.Throw(bytesUnknown, index);
      return true;
    }

    /// <summary>检索异常数据缓冲区中的下一个字符。</summary>
    /// <returns>返回值始终为 Unicode 字符 NULL (U+0000)。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <filterpriority>2</filterpriority>
    public override char GetNextChar()
    {
      return char.MinValue;
    }

    /// <summary>导致对 <see cref="M:System.Text.DecoderExceptionFallbackBuffer.GetNextChar" /> 的下一个调用访问当前位置之前的异常数据缓冲区字符位置。</summary>
    /// <returns>返回值始终为 false。即使返回值是不变的，也仍会定义一个返回值，原因是此方法会实现一个抽象方法。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool MovePrevious()
    {
      return false;
    }

    private void Throw(byte[] bytesUnknown, int index)
    {
      StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
      int index1;
      for (index1 = 0; index1 < bytesUnknown.Length && index1 < 20; ++index1)
      {
        stringBuilder.Append("[");
        stringBuilder.Append(bytesUnknown[index1].ToString("X2", (IFormatProvider) CultureInfo.InvariantCulture));
        stringBuilder.Append("]");
      }
      if (index1 == 20)
        stringBuilder.Append(" ...");
      throw new DecoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageBytesIndex", (object) stringBuilder, (object) index), bytesUnknown, index);
    }
  }
}
