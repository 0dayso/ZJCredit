// Decompiled with JetBrains decompiler
// Type: System.MissingMemberException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>试图动态访问不存在的类成员时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingMemberException : MemberAccessException, ISerializable
  {
    /// <summary>保留缺少成员的类名。</summary>
    protected string ClassName;
    /// <summary>保留缺少成员的名称。</summary>
    protected string MemberName;
    /// <summary>保留缺少成员的签名。</summary>
    protected byte[] Signature;

    /// <summary>获取显示类名、成员名和缺少成员签名的文本字符串。</summary>
    /// <returns>错误消息字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.ClassName == null)
          return base.Message;
        return Environment.GetResourceString("MissingMember_Name", (object) (this.ClassName + "." + this.MemberName + (this.Signature != null ? " " + MissingMemberException.FormatSignature(this.Signature) : "")));
      }
    }

    /// <summary>初始化 <see cref="T:System.MissingMemberException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public MissingMemberException()
      : base(Environment.GetResourceString("Arg_MissingMemberException"))
    {
      this.SetErrorCode(-2146233070);
    }

    /// <summary>使用指定的错误信息初始化 <see cref="T:System.MissingMemberException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public MissingMemberException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233070);
    }

    /// <summary>使用指定错误消息以及对内部异常（为该异常的根源）的引用来初始化 <see cref="T:System.MissingMemberException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前 Exception 的 <see cref="T:System.Exception" /> 的实例。如果 <paramref name="inner" /> 不是空引用（在 Visual Basic 中为 Nothing），则在处理 <paramref name="inner" /> 的 catch 块中引发当前的 Exception。</param>
    [__DynamicallyInvokable]
    public MissingMemberException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233070);
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.MissingMemberException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    protected MissingMemberException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.ClassName = info.GetString("MMClassName");
      this.MemberName = info.GetString("MMMemberName");
      this.Signature = (byte[]) info.GetValue("MMSignature", typeof (byte[]));
    }

    private MissingMemberException(string className, string memberName, byte[] signature)
    {
      this.ClassName = className;
      this.MemberName = memberName;
      this.Signature = signature;
    }

    /// <summary>使用指定的类名称和成员名称初始化 <see cref="T:System.MissingMemberException" /> 类的新实例。</summary>
    /// <param name="className">试图访问不存在的成员所用的类名称。</param>
    /// <param name="memberName">无法访问的成员名称。</param>
    public MissingMemberException(string className, string memberName)
    {
      this.ClassName = className;
      this.MemberName = memberName;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string FormatSignature(byte[] signature);

    /// <summary>用类名、成员名、缺少成员的签名和其他异常信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
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
      info.AddValue("MMClassName", (object) this.ClassName, typeof (string));
      info.AddValue("MMMemberName", (object) this.MemberName, typeof (string));
      info.AddValue("MMSignature", (object) this.Signature, typeof (byte[]));
    }
  }
}
