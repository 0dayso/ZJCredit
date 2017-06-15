// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitHandleCannotBeOpenedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>在尝试打开不存在的系统互斥体或信号量时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class WaitHandleCannotBeOpenedException : ApplicationException
  {
    /// <summary>使用默认值初始化 <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public WaitHandleCannotBeOpenedException()
      : base(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException"))
    {
      this.SetErrorCode(-2146233044);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public WaitHandleCannotBeOpenedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233044);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public WaitHandleCannotBeOpenedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233044);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象，包含有关所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象，它包含有关源或目标的上下文信息。</param>
    protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
