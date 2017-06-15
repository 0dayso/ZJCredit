// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
  /// <summary>当策略禁止代码运行时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class PolicyException : SystemException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.Security.Policy.PolicyException" /> 类的新实例。</summary>
    public PolicyException()
      : base(Environment.GetResourceString("Policy_Default"))
    {
      this.HResult = -2146233322;
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Security.Policy.PolicyException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public PolicyException(string message)
      : base(message)
    {
      this.HResult = -2146233322;
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Security.Policy.PolicyException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="exception">导致当前异常的异常。如果 <paramref name="exception" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public PolicyException(string message, Exception exception)
      : base(message, exception)
    {
      this.HResult = -2146233322;
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Security.Policy.PolicyException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected PolicyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal PolicyException(string message, int hresult)
      : base(message)
    {
      this.HResult = hresult;
    }

    internal PolicyException(string message, int hresult, Exception exception)
      : base(message, exception)
    {
      this.HResult = hresult;
    }
  }
}
