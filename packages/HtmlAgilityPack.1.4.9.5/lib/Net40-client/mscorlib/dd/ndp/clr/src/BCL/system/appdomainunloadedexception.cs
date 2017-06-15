// Decompiled with JetBrains decompiler
// Type: System.AppDomainUnloadedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当尝试访问已卸载的应用程序域时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class AppDomainUnloadedException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.AppDomainUnloadedException" /> 类的新实例。</summary>
    public AppDomainUnloadedException()
      : base(Environment.GetResourceString("Arg_AppDomainUnloadedException"))
    {
      this.SetErrorCode(-2146234348);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.AppDomainUnloadedException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    public AppDomainUnloadedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146234348);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.AppDomainUnloadedException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    public AppDomainUnloadedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146234348);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.AppDomainUnloadedException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
