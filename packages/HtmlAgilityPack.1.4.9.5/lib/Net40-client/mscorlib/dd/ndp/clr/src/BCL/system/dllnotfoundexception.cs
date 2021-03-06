﻿// Decompiled with JetBrains decompiler
// Type: System.DllNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当未找到在 DLL 导入中指定的 DLL 时所引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class DllNotFoundException : TypeLoadException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.DllNotFoundException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public DllNotFoundException()
      : base(Environment.GetResourceString("Arg_DllNotFoundException"))
    {
      this.SetErrorCode(-2146233052);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.DllNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public DllNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233052);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.DllNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public DllNotFoundException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233052);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.DllNotFoundException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected DllNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
