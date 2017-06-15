// Decompiled with JetBrains decompiler
// Type: System.ArgumentOutOfRangeException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>当参数值超出调用的方法所定义的允许取值范围时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ArgumentOutOfRangeException : ArgumentException, ISerializable
  {
    private static volatile string _rangeMessage;
    private object m_actualValue;

    private static string RangeMessage
    {
      get
      {
        if (ArgumentOutOfRangeException._rangeMessage == null)
          ArgumentOutOfRangeException._rangeMessage = Environment.GetResourceString("Arg_ArgumentOutOfRangeException");
        return ArgumentOutOfRangeException._rangeMessage;
      }
    }

    /// <summary>获取错误消息和无效参数值的字符串表示形式；或者，如果该参数值为 null，则仅获取错误消息。</summary>
    /// <returns>此异常的文本消息。此属性的值采用以下两种形式之一。条件值<paramref name="actualValue" /> 为 null。传递到构造函数的 <paramref name="message" /> 字符串。<paramref name="actualValue" /> 不为null。附有无效参数值字符串表示形式的 <paramref name="message" /> 字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string message = base.Message;
        if (this.m_actualValue == null)
          return message;
        string resourceString = Environment.GetResourceString("ArgumentOutOfRange_ActualValue", (object) this.m_actualValue.ToString());
        if (message == null)
          return resourceString;
        return message + Environment.NewLine + resourceString;
      }
    }

    /// <summary>获取导致此异常的参数值。</summary>
    /// <returns>Object，它包含导致当前 <see cref="T:System.Exception" /> 的参数的值。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual object ActualValue
    {
      [__DynamicallyInvokable] get
      {
        return this.m_actualValue;
      }
    }

    /// <summary>初始化 <see cref="T:System.ArgumentOutOfRangeException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException()
      : base(ArgumentOutOfRangeException.RangeMessage)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>使用导致此异常的参数的名称初始化 <see cref="T:System.ArgumentOutOfRangeException" /> 类的新实例。</summary>
    /// <param name="paramName">导致此异常的参数的名称。</param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string paramName)
      : base(ArgumentOutOfRangeException.RangeMessage, paramName)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>使用指定的错误消息和导致此异常的参数的名称来初始化 <see cref="T:System.ArgumentOutOfRangeException" /> 类的新实例。</summary>
    /// <param name="paramName">导致异常的参数的名称。</param>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string paramName, string message)
      : base(message, paramName)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>使用指定的错误消息和引发此异常的异常初始化 <see cref="T:System.ArgumentOutOfRangeException" /> 类的新实例。</summary>
    /// <param name="message">说明发生此异常的原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233086);
    }

    /// <summary>使用指定的错误消息、参数名和参数值来初始化 <see cref="T:System.ArgumentOutOfRangeException" /> 类的新实例。</summary>
    /// <param name="paramName">导致异常的参数的名称。</param>
    /// <param name="actualValue">导致此异常的参数值。</param>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public ArgumentOutOfRangeException(string paramName, object actualValue, string message)
      : base(message, paramName)
    {
      this.m_actualValue = actualValue;
      this.SetErrorCode(-2146233086);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.ArgumentOutOfRangeException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">对象，描述序列化数据的源或目标。</param>
    protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_actualValue = info.GetValue("ActualValue", typeof (object));
    }

    /// <summary>设置带有无效参数值和附加异常信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">对象，描述序列化数据的源或目标。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 对象为 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("ActualValue", this.m_actualValue, typeof (object));
    }
  }
}
