// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingTimeoutException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
  /// <summary>在以前指定的某个时期内无法到达服务器或客户端时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class RemotingTimeoutException : RemotingException
  {
    private static string _nullMessage = Environment.GetResourceString("Remoting_Default");

    /// <summary>使用默认属性初始化 <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> 类的新实例。</summary>
    public RemotingTimeoutException()
      : base(RemotingTimeoutException._nullMessage)
    {
    }

    /// <summary>用指定的消息初始化 <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> 类的新实例。</summary>
    /// <param name="message">指示异常发生的原因的消息。</param>
    public RemotingTimeoutException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233077);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="InnerException">导致当前异常的异常。如果 <paramref name="InnerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public RemotingTimeoutException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.SetErrorCode(-2146233077);
    }

    internal RemotingTimeoutException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
