// Decompiled with JetBrains decompiler
// Type: System.TypeInitializationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>作为类初始值设定项引发的异常的包装器而引发的异常。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class TypeInitializationException : SystemException
  {
    private string _typeName;

    /// <summary>获取未能初始化类型的完全限定名。</summary>
    /// <returns>未能初始化类型的完全限定名。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string TypeName
    {
      [__DynamicallyInvokable] get
      {
        if (this._typeName == null)
          return string.Empty;
        return this._typeName;
      }
    }

    private TypeInitializationException()
      : base(Environment.GetResourceString("TypeInitialization_Default"))
    {
      this.SetErrorCode(-2146233036);
    }

    private TypeInitializationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233036);
    }

    /// <summary>用默认错误消息、指定的类型名称和对内部异常（为该异常的根源）的引用来初始化 <see cref="T:System.TypeInitializationException" /> 类的新实例。</summary>
    /// <param name="fullTypeName">未能初始化类型的完全限定名。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public TypeInitializationException(string fullTypeName, Exception innerException)
      : base(Environment.GetResourceString("TypeInitialization_Type", (object) fullTypeName), innerException)
    {
      this._typeName = fullTypeName;
      this.SetErrorCode(-2146233036);
    }

    internal TypeInitializationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._typeName = info.GetString("TypeName");
    }

    /// <summary>设置带有文件名和附加异常信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("TypeName", (object) this.TypeName, typeof (string));
    }
  }
}
