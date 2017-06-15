// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SEHException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>表示结构化异常处理程序 (SEH) 错误。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SEHException : ExternalException
  {
    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.SEHException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SEHException()
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>用指定的消息初始化 <see cref="T:System.Runtime.InteropServices.SEHException" /> 类的新实例。</summary>
    /// <param name="message">指示异常原因的消息。</param>
    [__DynamicallyInvokable]
    public SEHException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.InteropServices.SEHException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public SEHException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>从序列化数据初始化 <see cref="T:System.Runtime.InteropServices.SEHException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    protected SEHException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>指示是否可以从异常中恢复，以及代码是否可以从引发异常的地方继续。</summary>
    /// <returns>始终为 false，因为未实现可恢复的异常。</returns>
    [__DynamicallyInvokable]
    public virtual bool CanResume()
    {
      return false;
    }
  }
}
