// Decompiled with JetBrains decompiler
// Type: System.Reflection.TargetInvocationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>由通过反射调用的方法引发的异常。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class TargetInvocationException : ApplicationException
  {
    private TargetInvocationException()
      : base(Environment.GetResourceString("Arg_TargetInvocationException"))
    {
      this.SetErrorCode(-2146232828);
    }

    private TargetInvocationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232828);
    }

    /// <summary>用对作为此异常原因的内部异常的引用初始化 <see cref="T:System.Reflection.TargetInvocationException" /> 类的新实例。</summary>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TargetInvocationException(Exception inner)
      : base(Environment.GetResourceString("Arg_TargetInvocationException"), inner)
    {
      this.SetErrorCode(-2146232828);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Reflection.TargetInvocationException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TargetInvocationException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232828);
    }

    internal TargetInvocationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
