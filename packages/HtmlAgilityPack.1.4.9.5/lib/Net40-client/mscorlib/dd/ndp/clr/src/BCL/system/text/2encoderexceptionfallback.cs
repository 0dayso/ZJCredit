// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderFallbackException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Text
{
  /// <summary>编码器回退操作失败时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class EncoderFallbackException : ArgumentException
  {
    private char charUnknown;
    private char charUnknownHigh;
    private char charUnknownLow;
    private int index;

    /// <summary>获取导致异常的输入字符。</summary>
    /// <returns>无法编码的字符。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public char CharUnknown
    {
      [__DynamicallyInvokable] get
      {
        return this.charUnknown;
      }
    }

    /// <summary>获取导致异常的代理项对的高组件字符。</summary>
    /// <returns>无法编码的代理项对的高组件字符。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public char CharUnknownHigh
    {
      [__DynamicallyInvokable] get
      {
        return this.charUnknownHigh;
      }
    }

    /// <summary>获取导致异常的代理项对的低组件字符。</summary>
    /// <returns>无法编码的代理项对的低组件字符。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public char CharUnknownLow
    {
      [__DynamicallyInvokable] get
      {
        return this.charUnknownLow;
      }
    }

    /// <summary>获取导致异常的字符在输入缓冲区中的索引位置。</summary>
    /// <returns>无法编码的字符在输入缓冲区中的索引位置。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Index
    {
      [__DynamicallyInvokable] get
      {
        return this.index;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.EncoderFallbackException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EncoderFallbackException()
      : base(Environment.GetResourceString("Arg_ArgumentException"))
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>初始化 <see cref="T:System.Text.EncoderFallbackException" /> 类的新实例。一个参数指定错误信息。</summary>
    /// <param name="message">错误信息。</param>
    [__DynamicallyInvokable]
    public EncoderFallbackException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>初始化 <see cref="T:System.Text.EncoderFallbackException" /> 类的新实例。参数指定错误信息和导致此异常的内部异常。</summary>
    /// <param name="message">错误信息。</param>
    /// <param name="innerException">导致此异常的异常。</param>
    [__DynamicallyInvokable]
    public EncoderFallbackException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024809);
    }

    internal EncoderFallbackException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal EncoderFallbackException(string message, char charUnknown, int index)
      : base(message)
    {
      this.charUnknown = charUnknown;
      this.index = index;
    }

    internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
      : base(message)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      this.charUnknownHigh = charUnknownHigh;
      this.charUnknownLow = charUnknownLow;
      this.index = index;
    }

    /// <summary>指示导致异常的输入是否为代理项对。</summary>
    /// <returns>如果输入是代理项对，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsUnknownSurrogate()
    {
      return (uint) this.charUnknownHigh > 0U;
    }
  }
}
