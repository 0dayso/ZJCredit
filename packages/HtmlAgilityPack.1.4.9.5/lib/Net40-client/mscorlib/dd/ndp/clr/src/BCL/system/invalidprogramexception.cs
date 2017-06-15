// Decompiled with JetBrains decompiler
// Type: System.InvalidProgramException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当程序包含无效的 Microsoft 中间语言 (MSIL) 或元数据时引发的异常。这通常表示生成程序的编译器中有 bug。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class InvalidProgramException : SystemException
  {
    /// <summary>使用默认属性初始化 <see cref="T:System.InvalidProgramException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public InvalidProgramException()
      : base(Environment.GetResourceString("InvalidProgram_Default"))
    {
      this.SetErrorCode(-2146233030);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.InvalidProgramException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public InvalidProgramException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233030);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.InvalidProgramException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public InvalidProgramException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233030);
    }

    internal InvalidProgramException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
