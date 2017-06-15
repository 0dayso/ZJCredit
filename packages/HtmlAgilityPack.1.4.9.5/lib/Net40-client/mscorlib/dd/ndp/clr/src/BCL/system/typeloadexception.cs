// Decompiled with JetBrains decompiler
// Type: System.TypeLoadException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>类型加载失败发生时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TypeLoadException : SystemException, ISerializable
  {
    private string ClassName;
    private string AssemblyName;
    private string MessageArg;
    internal int ResourceId;

    /// <summary>获取此异常的错误消息。</summary>
    /// <returns>错误消息字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        this.SetMessageField();
        return this._message;
      }
    }

    /// <summary>获取导致异常的类型的完全限定名。</summary>
    /// <returns>完全限定的类型名。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string TypeName
    {
      [__DynamicallyInvokable] get
      {
        if (this.ClassName == null)
          return string.Empty;
        return this.ClassName;
      }
    }

    /// <summary>初始化 <see cref="T:System.TypeLoadException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public TypeLoadException()
      : base(Environment.GetResourceString("Arg_TypeLoadException"))
    {
      this.SetErrorCode(-2146233054);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.TypeLoadException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public TypeLoadException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233054);
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.TypeLoadException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public TypeLoadException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233054);
    }

    [SecurityCritical]
    private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId)
      : base((string) null)
    {
      this.SetErrorCode(-2146233054);
      this.ClassName = className;
      this.AssemblyName = assemblyName;
      this.MessageArg = messageArg;
      this.ResourceId = resourceId;
      this.SetMessageField();
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.TypeLoadException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 对象为 null。</exception>
    protected TypeLoadException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.ClassName = info.GetString("TypeLoadClassName");
      this.AssemblyName = info.GetString("TypeLoadAssemblyName");
      this.MessageArg = info.GetString("TypeLoadMessageArg");
      this.ResourceId = info.GetInt32("TypeLoadResourceID");
    }

    [SecurityCritical]
    private void SetMessageField()
    {
      if (this._message != null)
        return;
      if (this.ClassName == null && this.ResourceId == 0)
      {
        this._message = Environment.GetResourceString("Arg_TypeLoadException");
      }
      else
      {
        if (this.AssemblyName == null)
          this.AssemblyName = Environment.GetResourceString("IO_UnknownFileName");
        if (this.ClassName == null)
          this.ClassName = Environment.GetResourceString("IO_UnknownFileName");
        string s = (string) null;
        TypeLoadException.GetTypeLoadExceptionMessage(this.ResourceId, JitHelpers.GetStringHandleOnStack(ref s));
        this._message = string.Format((IFormatProvider) CultureInfo.CurrentCulture, s, (object) this.ClassName, (object) this.AssemblyName, (object) this.MessageArg);
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypeLoadExceptionMessage(int resourceId, StringHandleOnStack retString);

    /// <summary>使用类名、方法名称、资源 ID 和附加异常信息来设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
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
      info.AddValue("TypeLoadClassName", (object) this.ClassName, typeof (string));
      info.AddValue("TypeLoadAssemblyName", (object) this.AssemblyName, typeof (string));
      info.AddValue("TypeLoadMessageArg", (object) this.MessageArg, typeof (string));
      info.AddValue("TypeLoadResourceID", this.ResourceId);
    }
  }
}
