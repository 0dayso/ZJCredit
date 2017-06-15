// Decompiled with JetBrains decompiler
// Type: System.Reflection.TargetException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>表示当试图调用无效目标时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class TargetException : ApplicationException
  {
    /// <summary>用空消息和异常的根源初始化 <see cref="T:System.Reflection.TargetException" /> 类的新实例。</summary>
    public TargetException()
    {
      this.SetErrorCode(-2146232829);
    }

    /// <summary>用给定消息和根源异常初始化 <see cref="T:System.Reflection.TargetException" /> 类的新实例。</summary>
    /// <param name="message">描述异常发生原因的 String。</param>
    public TargetException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232829);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Reflection.TargetException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public TargetException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232829);
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.Reflection.TargetException" /> 类的新实例。</summary>
    /// <param name="info">用于序列化或反序列化对象的数据。</param>
    /// <param name="context">对象的源和目标。</param>
    protected TargetException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
