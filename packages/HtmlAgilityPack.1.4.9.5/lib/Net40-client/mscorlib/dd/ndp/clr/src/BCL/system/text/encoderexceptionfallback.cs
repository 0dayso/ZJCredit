// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderExceptionFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>为不能转换为输出字节序列的输入字符提供一个称为“回退”的失败处理机制。如果输入字符无法转换为输出字节序列，则回退引发异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class EncoderExceptionFallback : EncoderFallback
  {
    /// <summary>获取此实例可以返回的最大字符数。</summary>
    /// <returns>返回值始终为零。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int MaxCharCount
    {
      [__DynamicallyInvokable] get
      {
        return 0;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.EncoderExceptionFallback" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EncoderExceptionFallback()
    {
    }

    /// <summary>返回编码器回退缓冲区，如果无法将字符序列转换为字节序列，则该缓冲区引发异常。</summary>
    /// <returns>当无法编码字符序列时，解码器回退缓冲区引发异常。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override EncoderFallbackBuffer CreateFallbackBuffer()
    {
      return (EncoderFallbackBuffer) new EncoderExceptionFallbackBuffer();
    }

    /// <summary>指示当前 <see cref="T:System.Text.EncoderExceptionFallback" /> 对象与指定对象是否相等。</summary>
    /// <returns>如果 <paramref name="value" /> 不为 null（在 Visual Basic .NET 中为 Nothing），并且是 <see cref="T:System.Text.EncoderExceptionFallback" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="value">从 <see cref="T:System.Text.EncoderExceptionFallback" /> 类派生的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      return value is EncoderExceptionFallback;
    }

    /// <summary>检索此实例的哈希代码。</summary>
    /// <returns>返回值始终是相同的任意值，没有特别的意义。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return 654;
    }
  }
}
