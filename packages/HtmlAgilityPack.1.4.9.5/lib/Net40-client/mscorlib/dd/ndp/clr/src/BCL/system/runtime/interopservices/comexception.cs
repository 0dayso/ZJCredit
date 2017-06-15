// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.COMException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>当从 COM 方法调用返回无法识别的 HRESULT 时引发的异常。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class COMException : ExternalException
  {
    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.InteropServices.COMException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public COMException()
      : base(Environment.GetResourceString("Arg_COMException"))
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>用指定的消息初始化 <see cref="T:System.Runtime.InteropServices.COMException" /> 类的新实例。</summary>
    /// <param name="message">指示异常原因的消息。</param>
    [__DynamicallyInvokable]
    public COMException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.InteropServices.COMException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public COMException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467259);
    }

    /// <summary>使用指定的消息和错误代码初始化 <see cref="T:System.Runtime.InteropServices.COMException" /> 类的新实例。</summary>
    /// <param name="message">指示所发生异常的原因的消息。</param>
    /// <param name="errorCode">与该异常关联的错误代码 (HRESULT) 值。</param>
    [__DynamicallyInvokable]
    public COMException(string message, int errorCode)
      : base(message)
    {
      this.SetErrorCode(errorCode);
    }

    [SecuritySafeCritical]
    internal COMException(int hresult)
      : base(Win32Native.GetMessage(hresult))
    {
      this.SetErrorCode(hresult);
    }

    internal COMException(string message, int hresult, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(hresult);
    }

    /// <summary>从序列化数据初始化 <see cref="T:System.Runtime.InteropServices.COMException" /> 类的新实例。</summary>
    /// <param name="info">保留序列化对象数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</param>
    /// <param name="context">提供有关源或目标的上下文信息的 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    protected COMException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>将异常的内容转换为字符串。</summary>
    /// <returns>包含异常的 <see cref="P:System.Exception.HResult" />、<see cref="P:System.Exception.Message" />、<see cref="P:System.Exception.InnerException" /> 和 <see cref="P:System.Exception.StackTrace" /> 属性的字符串。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    public override string ToString()
    {
      string message = this.Message;
      string str = this.GetType().ToString() + " (0x" + this.HResult.ToString("X8", (IFormatProvider) CultureInfo.InvariantCulture) + ")";
      if (message != null && message.Length > 0)
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
