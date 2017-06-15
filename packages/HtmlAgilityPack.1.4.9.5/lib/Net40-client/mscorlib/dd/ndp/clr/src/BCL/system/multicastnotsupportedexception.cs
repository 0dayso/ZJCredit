// Decompiled with JetBrains decompiler
// Type: System.MulticastNotSupportedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>尝试组合两个基于 <see cref="T:System.Delegate" /> 类型而非 <see cref="T:System.MulticastDelegate" /> 类型的委托时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class MulticastNotSupportedException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.MulticastNotSupportedException" /> 类的新实例。</summary>
    public MulticastNotSupportedException()
      : base(Environment.GetResourceString("Arg_MulticastNotSupportedException"))
    {
      this.SetErrorCode(-2146233068);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.MulticastNotSupportedException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    public MulticastNotSupportedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233068);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.MulticastNotSupportedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    public MulticastNotSupportedException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233068);
    }

    internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
