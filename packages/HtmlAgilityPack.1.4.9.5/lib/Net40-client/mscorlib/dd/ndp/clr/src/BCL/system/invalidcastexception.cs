// Decompiled with JetBrains decompiler
// Type: System.InvalidCastException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>因无效类型转换或显式转换引发的异常。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class InvalidCastException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.InvalidCastException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public InvalidCastException()
      : base(Environment.GetResourceString("Arg_InvalidCastException"))
    {
      this.SetErrorCode(-2147467262);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.InvalidCastException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public InvalidCastException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467262);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.InvalidCastException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public InvalidCastException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467262);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.InvalidCastException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected InvalidCastException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>使用指定的消息和错误代码初始化 <see cref="T:System.InvalidCastException" /> 类的新实例。</summary>
    /// <param name="message">指示所发生异常的原因的消息。</param>
    /// <param name="errorCode">与异常关联的错误代码 (HRESULT) 值。</param>
    [__DynamicallyInvokable]
    public InvalidCastException(string message, int errorCode)
      : base(message)
    {
      this.SetErrorCode(errorCode);
    }
  }
}
