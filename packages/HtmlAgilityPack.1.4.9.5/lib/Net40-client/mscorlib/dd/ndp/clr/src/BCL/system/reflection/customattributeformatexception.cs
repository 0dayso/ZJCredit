// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeFormatException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>当自定义特性的二进制格式无效时引发的异常。</summary>
  [ComVisible(true)]
  [Serializable]
  public class CustomAttributeFormatException : FormatException
  {
    /// <summary>用默认属性初始化 <see cref="T:System.Reflection.CustomAttributeFormatException" /> 类的新实例。</summary>
    public CustomAttributeFormatException()
      : base(Environment.GetResourceString("Arg_CustomAttributeFormatException"))
    {
      this.SetErrorCode(-2146232827);
    }

    /// <summary>用指定消息初始化 <see cref="T:System.Reflection.CustomAttributeFormatException" /> 类的新实例。</summary>
    /// <param name="message">指示引发异常的原因的消息。</param>
    public CustomAttributeFormatException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232827);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Reflection.CustomAttributeFormatException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public CustomAttributeFormatException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232827);
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.Reflection.CustomAttributeFormatException" /> 类的新实例。</summary>
    /// <param name="info">用于序列化或反序列化自定义特性的数据。</param>
    /// <param name="context">自定义特性的源和目标。</param>
    protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
