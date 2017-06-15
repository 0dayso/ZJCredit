// Decompiled with JetBrains decompiler
// Type: System.Resources.MissingManifestResourceException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
  /// <summary>主程序集不包含非特定区域性的资源和适当的附属程序集缺少时引发的异常。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingManifestResourceException : SystemException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.Resources.MissingManifestResourceException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public MissingManifestResourceException()
      : base(Environment.GetResourceString("Arg_MissingManifestResourceException"))
    {
      this.SetErrorCode(-2146233038);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Resources.MissingManifestResourceException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public MissingManifestResourceException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233038);
    }

    /// <summary>使用指定错误消息和对导致此异常的内部异常的引用来初始化 <see cref="T:System.Resources.MissingManifestResourceException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public MissingManifestResourceException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233038);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Resources.MissingManifestResourceException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">有关异常的源或目标的上下文信息。</param>
    protected MissingManifestResourceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
