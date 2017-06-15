// Decompiled with JetBrains decompiler
// Type: System.ArgumentNullException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>当将空引用（在 Visual Basic 中为 Nothing）传递给不接受它作为有效参数的方法时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ArgumentNullException : ArgumentException
  {
    /// <summary>初始化 <see cref="T:System.ArgumentNullException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ArgumentNullException()
      : base(Environment.GetResourceString("ArgumentNull_Generic"))
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>使用导致此异常的参数的名称初始化 <see cref="T:System.ArgumentNullException" /> 类的新实例。</summary>
    /// <param name="paramName">导致异常的参数的名称。</param>
    [__DynamicallyInvokable]
    public ArgumentNullException(string paramName)
      : base(Environment.GetResourceString("ArgumentNull_Generic"), paramName)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>使用指定的错误消息和引发此异常的异常初始化 <see cref="T:System.ArgumentNullException" /> 类的新实例。</summary>
    /// <param name="message">说明发生此异常的原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。 </param>
    [__DynamicallyInvokable]
    public ArgumentNullException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>使用指定的错误消息和导致此异常的参数的名称来初始化 <see cref="T:System.ArgumentNullException" /> 类的实例。</summary>
    /// <param name="paramName">导致异常的参数的名称。</param>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public ArgumentNullException(string paramName, string message)
      : base(message, paramName)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.ArgumentNullException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">对象，描述序列化数据的源或目标。</param>
    [SecurityCritical]
    protected ArgumentNullException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
