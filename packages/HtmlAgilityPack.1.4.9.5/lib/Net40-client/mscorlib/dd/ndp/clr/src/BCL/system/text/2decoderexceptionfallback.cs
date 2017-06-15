// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderFallbackException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Text
{
  /// <summary>解码器回退操作失败时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecoderFallbackException : ArgumentException
  {
    private byte[] bytesUnknown;
    private int index;

    /// <summary>获取导致异常的输入字节序列。</summary>
    /// <returns>无法解码的输入字节数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public byte[] BytesUnknown
    {
      [__DynamicallyInvokable] get
      {
        return this.bytesUnknown;
      }
    }

    /// <summary>获取导致异常的字节在输入字节序列中的索引位置。</summary>
    /// <returns>无法解码的字节在输入字节数组中的索引位置。索引位置是从零开始的。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Index
    {
      [__DynamicallyInvokable] get
      {
        return this.index;
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.DecoderFallbackException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public DecoderFallbackException()
      : base(Environment.GetResourceString("Arg_ArgumentException"))
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>初始化 <see cref="T:System.Text.DecoderFallbackException" /> 类的新实例。一个参数指定错误信息。</summary>
    /// <param name="message">错误信息。</param>
    [__DynamicallyInvokable]
    public DecoderFallbackException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>初始化 <see cref="T:System.Text.DecoderFallbackException" /> 类的新实例。参数指定错误信息和导致此异常的内部异常。</summary>
    /// <param name="message">错误信息。</param>
    /// <param name="innerException">导致此异常的异常。</param>
    [__DynamicallyInvokable]
    public DecoderFallbackException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024809);
    }

    internal DecoderFallbackException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>初始化 <see cref="T:System.Text.DecoderFallbackException" /> 类的新实例。参数指定错误信息、被解码的字节数组和无法被解码的字节的索引。</summary>
    /// <param name="message">错误信息。</param>
    /// <param name="bytesUnknown">输入字节数组。</param>
    /// <param name="index">无法解码的字节在 <paramref name="bytesUnknown" /> 中的索引位置。</param>
    [__DynamicallyInvokable]
    public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
      : base(message)
    {
      this.bytesUnknown = bytesUnknown;
      this.index = index;
    }
  }
}
