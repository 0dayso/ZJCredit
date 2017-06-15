// Decompiled with JetBrains decompiler
// Type: System.ApplicationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>用作应用程序定义的异常的基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class ApplicationException : Exception
  {
    /// <summary>初始化 <see cref="T:System.ApplicationException" /> 类的新实例。</summary>
    public ApplicationException()
      : base(Environment.GetResourceString("Arg_ApplicationException"))
    {
      this.SetErrorCode(-2146232832);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.ApplicationException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    public ApplicationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232832);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.ApplicationException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    public ApplicationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232832);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.ApplicationException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected ApplicationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
