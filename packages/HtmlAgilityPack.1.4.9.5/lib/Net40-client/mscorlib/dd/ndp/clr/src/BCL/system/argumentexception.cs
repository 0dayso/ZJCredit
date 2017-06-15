// Decompiled with JetBrains decompiler
// Type: System.ArgumentException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>当向方法提供的参数之一无效时引发的异常。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ArgumentException : SystemException, ISerializable
  {
    private string m_paramName;

    /// <summary>获取错误消息和参数名；如果未设置参数名，则仅获取错误消息。</summary>
    /// <returns>描述异常的详细信息的文本字符串。此属性的值采用以下两种形式之一：条件值<paramref name="paramName" /> 是空引用（在 Visual Basic 中为 Nothing）或长度为零。传递到构造函数的 <paramref name="message" /> 字符串。<paramref name="paramName" /> 不是空引用（在 Visual Basic 中为 Nothing）并且长度大于零。附有无效参数名的 <paramref name="message" /> 字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string message = base.Message;
        if (string.IsNullOrEmpty(this.m_paramName))
          return message;
        string resourceString = Environment.GetResourceString("Arg_ParamName_Name", (object) this.m_paramName);
        return message + Environment.NewLine + resourceString;
      }
    }

    /// <summary>获取导致该异常的参数的名称。</summary>
    /// <returns>参数名称。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual string ParamName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_paramName;
      }
    }

    /// <summary>初始化 <see cref="T:System.ArgumentException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ArgumentException()
      : base(Environment.GetResourceString("Arg_ArgumentException"))
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.ArgumentException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    [__DynamicallyInvokable]
    public ArgumentException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.ArgumentException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public ArgumentException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>使用指定错误消息、参数名和对内部异常（为该异常根源）的引用来初始化 <see cref="T:System.ArgumentException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="paramName">导致当前异常的参数的名称。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public ArgumentException(string message, string paramName, Exception innerException)
      : base(message, innerException)
    {
      this.m_paramName = paramName;
      this.SetErrorCode(-2147024809);
    }

    /// <summary>使用指定的错误消息和导致此异常的参数的名称来初始化 <see cref="T:System.ArgumentException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="paramName">导致当前异常的参数的名称。</param>
    [__DynamicallyInvokable]
    public ArgumentException(string message, string paramName)
      : base(message)
    {
      this.m_paramName = paramName;
      this.SetErrorCode(-2147024809);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.ArgumentException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected ArgumentException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_paramName = info.GetString("ParamName");
    }

    /// <summary>使用参数名和附加异常信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is a null reference (Nothing in Visual Basic). </exception>
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
      info.AddValue("ParamName", (object) this.m_paramName, typeof (string));
    }
  }
}
