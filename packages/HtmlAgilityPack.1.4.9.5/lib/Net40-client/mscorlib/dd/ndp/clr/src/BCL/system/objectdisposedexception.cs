// Decompiled with JetBrains decompiler
// Type: System.ObjectDisposedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>对已释放的对象执行操作时所引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ObjectDisposedException : InvalidOperationException
  {
    private string objectName;

    /// <summary>获取描述错误的消息。</summary>
    /// <returns>描述错误的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string objectName = this.ObjectName;
        if (objectName == null || objectName.Length == 0)
          return base.Message;
        return base.Message + Environment.NewLine + Environment.GetResourceString("ObjectDisposed_ObjectName_Name", (object) objectName);
      }
    }

    /// <summary>获取已释放对象的名称。</summary>
    /// <returns>包含已释放对象的名称的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string ObjectName
    {
      [__DynamicallyInvokable] get
      {
        if (this.objectName == null && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
          return string.Empty;
        return this.objectName;
      }
    }

    private ObjectDisposedException()
      : this((string) null, Environment.GetResourceString("ObjectDisposed_Generic"))
    {
    }

    /// <summary>使用包含已释放对象的名称的字符串初始化 <see cref="T:System.ObjectDisposedException" /> 类的新实例。</summary>
    /// <param name="objectName">包含已释放对象的名称的字符串。</param>
    [__DynamicallyInvokable]
    public ObjectDisposedException(string objectName)
      : this(objectName, Environment.GetResourceString("ObjectDisposed_Generic"))
    {
    }

    /// <summary> 使用指定的对象名称和消息初始化 <see cref="T:System.ObjectDisposedException" /> 类的新实例。</summary>
    /// <param name="objectName">已释放对象的名称。</param>
    /// <param name="message">解释异常原因的错误消息。</param>
    [__DynamicallyInvokable]
    public ObjectDisposedException(string objectName, string message)
      : base(message)
    {
      this.SetErrorCode(-2146232798);
      this.objectName = objectName;
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.ObjectDisposedException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 不为 null，则将在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public ObjectDisposedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232798);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.ObjectDisposedException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.objectName = info.GetString("ObjectName");
    }

    /// <summary>使用参数名和其他异常信息检索 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("ObjectName", (object) this.ObjectName, typeof (string));
    }
  }
}
