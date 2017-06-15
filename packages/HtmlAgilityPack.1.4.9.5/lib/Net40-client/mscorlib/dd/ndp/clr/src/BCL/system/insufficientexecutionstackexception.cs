// Decompiled with JetBrains decompiler
// Type: System.InsufficientExecutionStackException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System
{
  /// <summary>执行堆栈不足，大多数方法无法执行时所引发的异常。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class InsufficientExecutionStackException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.InsufficientExecutionStackException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public InsufficientExecutionStackException()
      : base(Environment.GetResourceString("Arg_InsufficientExecutionStackException"))
    {
      this.SetErrorCode(-2146232968);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.InsufficientExecutionStackException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public InsufficientExecutionStackException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232968);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.InsufficientExecutionStackException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public InsufficientExecutionStackException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232968);
    }

    private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
