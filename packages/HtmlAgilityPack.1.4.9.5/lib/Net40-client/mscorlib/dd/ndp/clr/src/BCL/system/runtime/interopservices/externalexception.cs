// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ExternalException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
  /// <summary>所有 COM 互操作 异常和结构化异常处理 (SEH) 异常的异常基类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public class ExternalException : SystemException
  {
    /// <summary>获取错误的 HRESULT。</summary>
    /// <returns>错误的 HRESULT。</returns>
    public virtual int ErrorCode
    {
      get
      {
        return this.HResult;
      }
    }

    /// <summary>使用默认属性初始化 ExternalException 类的新实例。</summary>
    public ExternalException()
      : base(Environment.GetResourceString("Arg_ExternalException"))
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>使用指定的错误信息初始化 ExternalException 类的新实例。</summary>
    /// <param name="message">指定异常原因的错误信息。</param>
    public ExternalException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.InteropServices.ExternalException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public ExternalException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>使用指定错误信息和错误的 HRESULT 初始化 ExternalException 类的新实例。</summary>
    /// <param name="message">指定异常原因的错误信息。</param>
    /// <param name="errorCode">错误的 HRESULT。</param>
    public ExternalException(string message, int errorCode)
      : base(message)
    {
      this.SetErrorCode(errorCode);
    }

    /// <summary>从序列化数据初始化 ExternalException 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    protected ExternalException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>返回一个字符串，该字符串包含错误的 HRESULT。</summary>
    /// <returns>表示 HRESULT 的字符串。</returns>
    public override string ToString()
    {
      string message = this.Message;
      string str = this.GetType().ToString() + " (0x" + this.HResult.ToString("X8", (IFormatProvider) CultureInfo.InvariantCulture) + ")";
      if (!string.IsNullOrEmpty(message))
        str = str + ": " + message;
      Exception innerException = this.InnerException;
      if (innerException != null)
        str = str + " ---> " + innerException.ToString();
      if (this.StackTrace != null)
        str = str + Environment.NewLine + this.StackTrace;
      return str;
    }
  }
}
