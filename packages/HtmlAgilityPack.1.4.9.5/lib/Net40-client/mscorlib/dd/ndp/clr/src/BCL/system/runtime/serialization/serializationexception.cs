// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>在序列化或反序列化期间出错时所引发的异常。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SerializationException : SystemException
  {
    private static string _nullMessage = Environment.GetResourceString("Arg_SerializationException");

    /// <summary>使用默认属性初始化 <see cref="T:System.Runtime.Serialization.SerializationException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SerializationException()
      : base(SerializationException._nullMessage)
    {
      this.SetErrorCode(-2146233076);
    }

    /// <summary>用指定的消息初始化 <see cref="T:System.Runtime.Serialization.SerializationException" /> 类的新实例。</summary>
    /// <param name="message">指出发生异常的原因。</param>
    [__DynamicallyInvokable]
    public SerializationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233076);
    }

    /// <summary>使用指定的错误消息以及对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Runtime.Serialization.SerializationException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public SerializationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233076);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Runtime.Serialization.SerializationException" /> 类的新实例。</summary>
    /// <param name="info">以名称/值形式保存已序列化对象数据的序列化信息对象。</param>
    /// <param name="context">有关异常的源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    protected SerializationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
