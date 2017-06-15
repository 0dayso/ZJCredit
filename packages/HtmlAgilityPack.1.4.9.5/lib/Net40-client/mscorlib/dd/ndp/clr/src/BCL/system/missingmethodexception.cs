// Decompiled with JetBrains decompiler
// Type: System.MissingMethodException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>试图动态访问不存在的方法时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingMethodException : MissingMemberException, ISerializable
  {
    /// <summary>获取显示类名、方法名称和缺少方法签名的文本字符串。此属性为只读。</summary>
    /// <returns>错误消息字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.ClassName == null)
          return base.Message;
        return Environment.GetResourceString("MissingMethod_Name", (object) (this.ClassName + "." + this.MemberName + (this.Signature != null ? " " + MissingMemberException.FormatSignature(this.Signature) : "")));
      }
    }

    /// <summary>初始化 <see cref="T:System.MissingMethodException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public MissingMethodException()
      : base(Environment.GetResourceString("Arg_MissingMethodException"))
    {
      this.SetErrorCode(-2146233069);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.MissingMethodException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。</param>
    [__DynamicallyInvokable]
    public MissingMethodException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233069);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.MissingMethodException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public MissingMethodException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233069);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.MissingMethodException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected MissingMethodException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    private MissingMethodException(string className, string methodName, byte[] signature)
    {
      this.ClassName = className;
      this.MemberName = methodName;
      this.Signature = signature;
    }

    /// <summary>使用指定的类名称和方法名称初始化 <see cref="T:System.MissingMethodException" /> 类的新实例。</summary>
    /// <param name="className">试图访问不存在的方法时所用的类名。</param>
    /// <param name="methodName">无法访问的方法名。</param>
    public MissingMethodException(string className, string methodName)
    {
      this.ClassName = className;
      this.MemberName = methodName;
    }
  }
}
