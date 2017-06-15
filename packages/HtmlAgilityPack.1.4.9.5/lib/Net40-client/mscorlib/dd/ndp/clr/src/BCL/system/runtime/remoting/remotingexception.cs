// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
  /// <summary>在远程处理过程中出现错误时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class RemotingException : SystemException
  {
    private static string _nullMessage = Environment.GetResourceString("Remoting_Default");

    /// <summary>使用默认属性初始化 <see cref="T:System.Runtime.Remoting.RemotingException" /> 类的新实例。</summary>
    public RemotingException()
      : base(RemotingException._nullMessage)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>用指定的消息初始化 <see cref="T:System.Runtime.Remoting.RemotingException" /> 类的新实例。</summary>
    /// <param name="message">解释异常发生的原因的错误信息。</param>
    public RemotingException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.Remoting.RemotingException" /> 类的新实例。</summary>
    /// <param name="message">解释异常发生的原因的错误信息。</param>
    /// <param name="InnerException">导致当前异常的异常。如果 <paramref name="InnerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public RemotingException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Runtime.Remoting.RemotingException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">有关异常的源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    protected RemotingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
