// Decompiled with JetBrains decompiler
// Type: System.Threading.SemaphoreFullException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>对计数已达到最大值的信号量调用 <see cref="Overload:System.Threading.Semaphore.Release" /> 方法时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(false)]
  [TypeForwardedFrom("System, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  public class SemaphoreFullException : SystemException
  {
    /// <summary>使用默认值初始化 <see cref="T:System.Threading.SemaphoreFullException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SemaphoreFullException()
      : base(Environment.GetResourceString("Threading_SemaphoreFullException"))
    {
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Threading.SemaphoreFullException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public SemaphoreFullException(string message)
      : base(message)
    {
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.SemaphoreFullException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public SemaphoreFullException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Threading.SemaphoreFullException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，它包含有关源或目标的上下文信息。</param>
    protected SemaphoreFullException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
