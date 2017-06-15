// Decompiled with JetBrains decompiler
// Type: System.MemberAccessException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>访问类成员的尝试失败时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MemberAccessException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.MemberAccessException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public MemberAccessException()
      : base(Environment.GetResourceString("Arg_AccessException"))
    {
      this.SetErrorCode(-2146233062);
    }

    /// <summary>用指定的错误消息初始化 <see cref="T:System.MemberAccessException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public MemberAccessException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233062);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.MemberAccessException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不是 null 引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public MemberAccessException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233062);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.MemberAccessException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected MemberAccessException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
