// Decompiled with JetBrains decompiler
// Type: System.DuplicateWaitObjectException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当对象在同步对象数组中不止一次出现时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class DuplicateWaitObjectException : ArgumentException
  {
    private static volatile string _duplicateWaitObjectMessage;

    private static string DuplicateWaitObjectMessage
    {
      get
      {
        if (DuplicateWaitObjectException._duplicateWaitObjectMessage == null)
          DuplicateWaitObjectException._duplicateWaitObjectMessage = Environment.GetResourceString("Arg_DuplicateWaitObjectException");
        return DuplicateWaitObjectException._duplicateWaitObjectMessage;
      }
    }

    /// <summary>初始化 <see cref="T:System.DuplicateWaitObjectException" /> 类的新实例。</summary>
    public DuplicateWaitObjectException()
      : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>使用导致此异常的参数的名称初始化 <see cref="T:System.DuplicateWaitObjectException" /> 类的新实例。</summary>
    /// <param name="parameterName">导致异常的参数的名称。</param>
    public DuplicateWaitObjectException(string parameterName)
      : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>使用指定错误消息和导致此异常的参数的名称来初始化 <see cref="T:System.DuplicateWaitObjectException" /> 类的新实例。</summary>
    /// <param name="parameterName">导致异常的参数的名称。</param>
    /// <param name="message">描述错误的消息。</param>
    public DuplicateWaitObjectException(string parameterName, string message)
      : base(message, parameterName)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.DuplicateWaitObjectException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public DuplicateWaitObjectException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233047);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.DuplicateWaitObjectException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
