﻿// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadStateException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>当 <see cref="T:System.Threading.Thread" /> 处于对方法调用无效的 <see cref="P:System.Threading.Thread.ThreadState" /> 时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class ThreadStateException : SystemException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.Threading.ThreadStateException" /> 类的新实例。</summary>
    public ThreadStateException()
      : base(Environment.GetResourceString("Arg_ThreadStateException"))
    {
      this.SetErrorCode(-2146233056);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Threading.ThreadStateException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public ThreadStateException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233056);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Threading.ThreadStateException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public ThreadStateException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233056);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Threading.ThreadStateException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected ThreadStateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
