// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ServerException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
  /// <summary>当客户端连接到无法引发异常的非 .NET Framework 应用程序时，则引发该异常，以向客户端传达错误。</summary>
  [ComVisible(true)]
  [Serializable]
  public class ServerException : SystemException
  {
    private static string _nullMessage = Environment.GetResourceString("Remoting_Default");

    /// <summary>使用默认属性初始化 <see cref="T:System.Runtime.Remoting.ServerException" /> 类的新实例。</summary>
    public ServerException()
      : base(ServerException._nullMessage)
    {
      this.SetErrorCode(-2146233074);
    }

    /// <summary>用指定的消息初始化 <see cref="T:System.Runtime.Remoting.ServerException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息</param>
    public ServerException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233074);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.Remoting.ServerException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="InnerException">导致当前异常的异常。如果 <paramref name="InnerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public ServerException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.SetErrorCode(-2146233074);
    }

    internal ServerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
