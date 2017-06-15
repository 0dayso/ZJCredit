// Decompiled with JetBrains decompiler
// Type: System.StackOverflowException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>因包含的嵌套方法调用过多而导致执行堆栈溢出时引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class StackOverflowException : SystemException
  {
    /// <summary>初始化 <see cref="T:System.StackOverflowException" /> 类的新实例，将新实例的 <see cref="P:System.Exception.Message" /> 属性设置为系统提供的描述错误的消息，如“所请求的操作导致堆栈溢出”。 此消息将当前系统区域性考虑在内。</summary>
    public StackOverflowException()
      : base(Environment.GetResourceString("Arg_StackOverflowException"))
    {
      this.SetErrorCode(-2147023895);
    }

    /// <summary>用指定的错误消息初始化 <see cref="T:System.StackOverflowException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。消息的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    public StackOverflowException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147023895);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.StackOverflowException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不是 null 引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    public StackOverflowException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147023895);
    }

    internal StackOverflowException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
