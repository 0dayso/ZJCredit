// Decompiled with JetBrains decompiler
// Type: System.Security.VerificationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
  /// <summary>在以下情况引发的异常：安全策略要求代码为类型安全的代码，并且验证过程无法验证该代码是否为类型安全的代码。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class VerificationException : SystemException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.Security.VerificationException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public VerificationException()
      : base(Environment.GetResourceString("Verification_Exception"))
    {
      this.SetErrorCode(-2146233075);
    }

    /// <summary>用说明性消息初始化 <see cref="T:System.Security.VerificationException" /> 类的新实例。</summary>
    /// <param name="message">一条指示异常发生原因的消息。</param>
    [__DynamicallyInvokable]
    public VerificationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233075);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Security.VerificationException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public VerificationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233075);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Security.VerificationException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected VerificationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
