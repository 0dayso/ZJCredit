// Decompiled with JetBrains decompiler
// Type: System.ExecutionEngineException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>当公共语言运行时的执行引擎中存在内部错误时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
  [ComVisible(true)]
  [Serializable]
  public sealed class ExecutionEngineException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.ExecutionEngineException" /> 类的新实例。</summary>
    public ExecutionEngineException()
      : base(Environment.GetResourceString("Arg_ExecutionEngineException"))
    {
      this.SetErrorCode(-2146233082);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.ExecutionEngineException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    public ExecutionEngineException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233082);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.ExecutionEngineException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    public ExecutionEngineException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233082);
    }

    internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
