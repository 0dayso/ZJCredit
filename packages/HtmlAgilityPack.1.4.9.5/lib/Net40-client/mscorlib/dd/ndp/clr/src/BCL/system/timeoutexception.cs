// Decompiled with JetBrains decompiler
// Type: System.TimeoutException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当为进程或操作分配的时间已过期时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TimeoutException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.TimeoutException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TimeoutException()
      : base(Environment.GetResourceString("Arg_TimeoutException"))
    {
      this.SetErrorCode(-2146233083);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.TimeoutException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public TimeoutException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233083);
    }

    /// <summary>使用指定的错误信息和内部异常初始化 <see cref="T:System.TimeoutException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TimeoutException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233083);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.TimeoutException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，它包含有关源或目标的上下文信息。<paramref name="context" /> 参数保留供将来使用，并可指定为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">类名为 null 或 <see cref="P:System.Exception.HResult" /> 为零 (0)。</exception>
    protected TimeoutException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
