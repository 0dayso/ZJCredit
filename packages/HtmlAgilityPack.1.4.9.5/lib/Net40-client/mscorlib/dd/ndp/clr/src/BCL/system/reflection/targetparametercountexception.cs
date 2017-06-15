// Decompiled with JetBrains decompiler
// Type: System.Reflection.TargetParameterCountException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>当调用的参数数目与预期的数目不匹配时引发的异常。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class TargetParameterCountException : ApplicationException
  {
    /// <summary>用空消息字符串和异常的根源初始化 <see cref="T:System.Reflection.TargetParameterCountException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TargetParameterCountException()
      : base(Environment.GetResourceString("Arg_TargetParameterCountException"))
    {
      this.SetErrorCode(-2147352562);
    }

    /// <summary>用设置为给定消息的消息字符串和根源异常初始化 <see cref="T:System.Reflection.TargetParameterCountException" /> 类的新实例。</summary>
    /// <param name="message">描述此异常的引发原因的 String。</param>
    [__DynamicallyInvokable]
    public TargetParameterCountException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147352562);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Reflection.TargetParameterCountException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TargetParameterCountException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147352562);
    }

    internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
