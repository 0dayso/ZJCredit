﻿// Decompiled with JetBrains decompiler
// Type: System.NotSupportedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当调用的方法不受支持，或试图读取、查找或写入不支持调用功能的流时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class NotSupportedException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.NotSupportedException" /> 类的新实例，将新实例的 <see cref="P:System.Exception.Message" /> 属性设置为系统提供的描述错误的消息。此消息将考虑当前系统区域性。</summary>
    [__DynamicallyInvokable]
    public NotSupportedException()
      : base(Environment.GetResourceString("Arg_NotSupportedException"))
    {
      this.SetErrorCode(-2146233067);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.NotSupportedException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public NotSupportedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233067);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.NotSupportedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public NotSupportedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233067);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.NotSupportedException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected NotSupportedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
