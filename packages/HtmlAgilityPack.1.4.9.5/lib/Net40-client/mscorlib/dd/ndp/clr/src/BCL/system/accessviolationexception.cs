// Decompiled with JetBrains decompiler
// Type: System.AccessViolationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>在试图读写受保护内存时引发的异常。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class AccessViolationException : SystemException
  {
    private IntPtr _ip;
    private IntPtr _target;
    private int _accessType;

    /// <summary>使用由系统提供的用来描述错误的消息初始化 <see cref="T:System.AccessViolationException" /> 类的新实例。</summary>
    public AccessViolationException()
      : base(Environment.GetResourceString("Arg_AccessViolationException"))
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>使用指定的描述错误的消息初始化 <see cref="T:System.AccessViolationException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    public AccessViolationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.AccessViolationException" /> 类的新实例。</summary>
    /// <param name="message">描述该异常的消息。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    public AccessViolationException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.AccessViolationException" /> 类的一个新实例。</summary>
    /// <param name="info">保存序列化对象数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected AccessViolationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
