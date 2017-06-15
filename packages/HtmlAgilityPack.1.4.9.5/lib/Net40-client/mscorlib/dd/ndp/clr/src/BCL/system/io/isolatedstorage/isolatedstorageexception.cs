﻿// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
  /// <summary>独立存储中的操作失败时所引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class IsolatedStorageException : Exception
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> 类的新实例。</summary>
    public IsolatedStorageException()
      : base(Environment.GetResourceString("IsolatedStorage_Exception"))
    {
      this.SetErrorCode(-2146233264);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    public IsolatedStorageException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233264);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public IsolatedStorageException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233264);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected IsolatedStorageException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}