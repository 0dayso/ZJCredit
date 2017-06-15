// Decompiled with JetBrains decompiler
// Type: System.IO.IOException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>发生 I/O 错误时引发的异常。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class IOException : SystemException
  {
    [NonSerialized]
    private string _maybeFullPath;

    /// <summary>初始化 <see cref="T:System.IO.IOException" /> 类的新实例，使其消息字符串设置为空字符串 ("")，其 HRESULT 设置为 COR_E_IO，而其内部异常设置为空引用。</summary>
    [__DynamicallyInvokable]
    public IOException()
      : base(Environment.GetResourceString("Arg_IOException"))
    {
      this.SetErrorCode(-2146232800);
    }

    /// <summary>初始化 <see cref="T:System.IO.IOException" /> 类的新实例，使其消息字符串设置为 <paramref name="message" />，其 HRESULT 设置为 COR_E_IO，而其内部异常设置为 null。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public IOException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232800);
    }

    /// <summary>初始化 <see cref="T:System.IO.IOException" /> 类的新实例，使其消息字符串设置为 <paramref name="message" />，而其 HRESULT 由用户定义。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="hresult">标识已发生的错误的整数。</param>
    [__DynamicallyInvokable]
    public IOException(string message, int hresult)
      : base(message)
    {
      this.SetErrorCode(hresult);
    }

    internal IOException(string message, int hresult, string maybeFullPath)
      : base(message)
    {
      this.SetErrorCode(hresult);
      this._maybeFullPath = maybeFullPath;
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.IO.IOException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public IOException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232800);
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.IO.IOException" /> 类的新实例。</summary>
    /// <param name="info">用于序列化或反序列化对象的数据。</param>
    /// <param name="context">对象的源和目标。</param>
    protected IOException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
