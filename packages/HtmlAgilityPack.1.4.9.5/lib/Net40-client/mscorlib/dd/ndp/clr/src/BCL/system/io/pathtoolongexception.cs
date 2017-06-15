// Decompiled with JetBrains decompiler
// Type: System.IO.PathTooLongException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>当路径名或文件名长度超过系统定义的最大长度时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class PathTooLongException : IOException
  {
    /// <summary>初始化 <see cref="T:System.IO.PathTooLongException" /> 类的新实例，使其 HRESULT 设置为 COR_E_PATHTOOLONG。</summary>
    [__DynamicallyInvokable]
    public PathTooLongException()
      : base(Environment.GetResourceString("IO.PathTooLong"))
    {
      this.SetErrorCode(-2147024690);
    }

    /// <summary>初始化 <see cref="T:System.IO.PathTooLongException" /> 类的新实例，使其消息字符串设置为 <paramref name="message" />，而其 HRESULT 设置为 COR_E_PATHTOOLONG。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public PathTooLongException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024690);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.IO.PathTooLongException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public PathTooLongException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024690);
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.IO.PathTooLongException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected PathTooLongException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
