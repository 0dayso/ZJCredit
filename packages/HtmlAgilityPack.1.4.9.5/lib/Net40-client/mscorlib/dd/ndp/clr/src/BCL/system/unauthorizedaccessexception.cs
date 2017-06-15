// Decompiled with JetBrains decompiler
// Type: System.UnauthorizedAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当操作系统因 I/O 错误或指定类型的安全错误而拒绝访问时所引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UnauthorizedAccessException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.UnauthorizedAccessException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public UnauthorizedAccessException()
      : base(Environment.GetResourceString("Arg_UnauthorizedAccessException"))
    {
      this.SetErrorCode(-2147024891);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.UnauthorizedAccessException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public UnauthorizedAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024891);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.UnauthorizedAccessException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public UnauthorizedAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147024891);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.UnauthorizedAccessException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
