// Decompiled with JetBrains decompiler
// Type: System.IO.EndOfStreamException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>读操作试图超出流的末尾时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class EndOfStreamException : IOException
  {
    /// <summary>初始化 <see cref="T:System.IO.EndOfStreamException" /> 类的新实例，将其消息字符串设置为系统提供的消息，其 HRESULT 设置为 COR_E_ENDOFSTREAM。</summary>
    [__DynamicallyInvokable]
    public EndOfStreamException()
      : base(Environment.GetResourceString("Arg_EndOfStreamException"))
    {
      this.SetErrorCode(-2147024858);
    }

    /// <summary>初始化 <see cref="T:System.IO.EndOfStreamException" /> 类的新实例，使其消息字符串设置为 <paramref name="message" />，其 HRESULT 设置为 COR_E_ENDOFSTREAM。</summary>
    /// <param name="message">描述错误的字符串。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public EndOfStreamException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024858);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.IO.EndOfStreamException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的字符串。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public EndOfStreamException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024858);
    }

    /// <summary>使用指定的序列化和上下文信息初始化 <see cref="T:System.IO.EndOfStreamException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected EndOfStreamException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
