// Decompiled with JetBrains decompiler
// Type: System.Reflection.AmbiguousMatchException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>当绑定到成员的操作导致一个以上的成员匹配绑定条件时引发的异常。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AmbiguousMatchException : SystemException
  {
    /// <summary>通过使用空消息字符串和将根源异常设置为 null 来初始化 <see cref="T:System.Reflection.AmbiguousMatchException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public AmbiguousMatchException()
      : base(Environment.GetResourceString("RFLCT.Ambiguous"))
    {
      this.SetErrorCode(-2147475171);
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AmbiguousMatchException" /> 类的一个新实例，将其消息字符串设置为给定消息，将根源异常设置为 null。</summary>
    /// <param name="message">指示此异常的引发原因的字符串。</param>
    [__DynamicallyInvokable]
    public AmbiguousMatchException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147475171);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Reflection.AmbiguousMatchException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public AmbiguousMatchException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147475171);
    }

    internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
