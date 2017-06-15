// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderExceptionFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>为不能转换为输入字符的已编码输入字节序列提供失败处理机制（称为“回退”）。回退引发异常，而不是解码输入字节序列。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecoderExceptionFallback : DecoderFallback
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

    /// <summary>初始化 <see cref="T:System.Text.DecoderExceptionFallback" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public DecoderExceptionFallback()
    {
    }

    /// <summary>返回解码器回退缓冲区，如果无法将字节序列转换为字符，该缓冲区将引发异常。</summary>
    /// <returns>当无法解码字节序列时，解码器回退缓冲区引发异常。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override DecoderFallbackBuffer CreateFallbackBuffer()
    {
      return (DecoderFallbackBuffer) new DecoderExceptionFallbackBuffer();
    }

    /// <summary>指示当前 <see cref="T:System.Text.DecoderExceptionFallback" /> 对象与指定对象是否相等。</summary>
    /// <returns>如果 <paramref name="value" /> 不为 null 且是一个 <see cref="T:System.Text.DecoderExceptionFallback" /> 对象，则为 true；否则，为 false。</returns>
    /// <param name="value">从 <see cref="T:System.Text.DecoderExceptionFallback" /> 类派生的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      return value is DecoderExceptionFallback;
    }

    /// <summary>检索此实例的哈希代码。</summary>
    /// <returns>返回值始终是相同的任意值，没有特别的意义。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return 879;
    }
  }
}
