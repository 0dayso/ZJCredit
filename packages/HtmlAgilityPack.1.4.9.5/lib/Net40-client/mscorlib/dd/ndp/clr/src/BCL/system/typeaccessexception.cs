// Decompiled with JetBrains decompiler
// Type: System.TypeAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System
{
  /// <summary>当方法试图使用它无权访问的类型时引发的异常。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public class TypeAccessException : TypeLoadException
  {
    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.TypeAccessException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TypeAccessException()
      : base(Environment.GetResourceString("Arg_TypeAccessException"))
    {
      this.SetErrorCode(-2146233021);
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.TypeAccessException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方必须确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public TypeAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233021);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.TypeAccessException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方必须确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TypeAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233021);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.TypeAccessException" /> 类的新实例。</summary>
    /// <param name="info">包含序列化数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected TypeAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.SetErrorCode(-2146233021);
    }
  }
}
