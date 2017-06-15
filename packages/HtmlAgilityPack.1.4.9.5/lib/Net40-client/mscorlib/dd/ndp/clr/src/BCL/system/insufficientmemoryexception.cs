// Decompiled with JetBrains decompiler
// Type: System.InsufficientMemoryException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System
{
  /// <summary>当检测到没有足够的可用内存时引发的异常。此类不能被继承。</summary>
  [Serializable]
  public sealed class InsufficientMemoryException : OutOfMemoryException
  {
    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.InsufficientMemoryException" /> 类的新实例。</summary>
    public InsufficientMemoryException()
      : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
    {
      this.SetErrorCode(-2146233027);
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.InsufficientMemoryException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    public InsufficientMemoryException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233027);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.InsufficientMemoryException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public InsufficientMemoryException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233027);
    }

    private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
