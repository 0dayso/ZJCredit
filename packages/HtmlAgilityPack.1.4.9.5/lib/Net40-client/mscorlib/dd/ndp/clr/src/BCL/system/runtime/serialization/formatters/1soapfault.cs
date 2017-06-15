// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.ServerFault
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>获取有关服务器错误的信息。此类不能被继承。</summary>
  [SoapType(Embedded = true)]
  [ComVisible(true)]
  [Serializable]
  public sealed class ServerFault
  {
    private string exceptionType;
    private string message;
    private string stackTrace;
    private Exception exception;

    /// <summary>获取或设置服务器引发的异常的类型。</summary>
    /// <returns>服务器引发的异常的类型。</returns>
    public string ExceptionType
    {
      get
      {
        return this.exceptionType;
      }
      set
      {
        this.exceptionType = value;
      }
    }

    /// <summary>获取或设置伴随在服务器上引发的异常的异常消息。</summary>
    /// <returns>伴随在服务器上引发的异常的异常消息。</returns>
    public string ExceptionMessage
    {
      get
      {
        return this.message;
      }
      set
      {
        this.message = value;
      }
    }

    /// <summary>获取或设置在服务器上引发异常的线程的堆栈跟踪。</summary>
    /// <returns>在服务器上引发异常的线程的堆栈跟踪。</returns>
    public string StackTrace
    {
      get
      {
        return this.stackTrace;
      }
      set
      {
        this.stackTrace = value;
      }
    }

    internal Exception Exception
    {
      get
      {
        return this.exception;
      }
    }

    internal ServerFault(Exception exception)
    {
      this.exception = exception;
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.Formatters.ServerFault" /> 类的新实例。</summary>
    /// <param name="exceptionType">服务器上发生的异常的实例。</param>
    /// <param name="message">伴随异常的消息。</param>
    /// <param name="stackTrace">在服务器上引发异常的线程的堆栈跟踪。</param>
    public ServerFault(string exceptionType, string message, string stackTrace)
    {
      this.exceptionType = exceptionType;
      this.message = message;
      this.stackTrace = stackTrace;
    }
  }
}
