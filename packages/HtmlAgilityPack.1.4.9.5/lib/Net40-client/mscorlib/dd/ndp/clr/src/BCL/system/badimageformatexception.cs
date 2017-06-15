// Decompiled with JetBrains decompiler
// Type: System.BadImageFormatException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
  /// <summary>当动态链接库 (DLL) 或可执行程序的文件映像无效时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class BadImageFormatException : SystemException
  {
    private string _fileName;
    private string _fusionLog;

    /// <summary>获取错误消息和引发此异常的文件的名称。</summary>
    /// <returns>包含错误消息和引发此异常的文件名称的字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        this.SetMessageField();
        return this._message;
      }
    }

    /// <summary>获取导致该异常的文件的名称。</summary>
    /// <returns>带有无效图像的文件的名称，或一个空引用（如果未向当前实例的构造函数传递任何文件名）。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string FileName
    {
      [__DynamicallyInvokable] get
      {
        return this._fileName;
      }
    }

    /// <summary>获取描述程序集加载失败的原因的日志文件。</summary>
    /// <returns>一个 String，包含由程序集缓存报告的错误。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    public string FusionLog
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this._fusionLog;
      }
    }

    /// <summary>初始化 <see cref="T:System.BadImageFormatException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public BadImageFormatException()
      : base(Environment.GetResourceString("Arg_BadImageFormatException"))
    {
      this.SetErrorCode(-2147024885);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.BadImageFormatException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024885);
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.BadImageFormatException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147024885);
    }

    /// <summary>用指定的错误消息和文件名初始化 <see cref="T:System.BadImageFormatException" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    /// <param name="fileName">包含无效图像的文件的全名。</param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message, string fileName)
      : base(message)
    {
      this.SetErrorCode(-2147024885);
      this._fileName = fileName;
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.BadImageFormatException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="fileName">包含无效图像的文件的全名。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message, string fileName, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147024885);
      this._fileName = fileName;
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.BadImageFormatException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected BadImageFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._fileName = info.GetString("BadImageFormat_FileName");
      try
      {
        this._fusionLog = info.GetString("BadImageFormat_FusionLog");
      }
      catch
      {
        this._fusionLog = (string) null;
      }
    }

    private BadImageFormatException(string fileName, string fusionLog, int hResult)
      : base((string) null)
    {
      this.SetErrorCode(hResult);
      this._fileName = fileName;
      this._fusionLog = fusionLog;
      this.SetMessageField();
    }

    private void SetMessageField()
    {
      if (this._message != null)
        return;
      if (this._fileName == null && this.HResult == -2146233088)
        this._message = Environment.GetResourceString("Arg_BadImageFormatException");
      else
        this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, this.HResult);
    }

    /// <summary>返回该异常的完全限定名，还可能返回错误消息、内部异常的名称和堆栈跟踪。</summary>
    /// <returns>一个字符串，包含该异常的完全限定名，还可能包含错误信息、内部异常的名称和堆栈跟踪。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      string str = this.GetType().FullName + ": " + this.Message;
      if (this._fileName != null && this._fileName.Length != 0)
        str = str + Environment.NewLine + Environment.GetResourceString("IO.FileName_Name", (object) this._fileName);
      if (this.InnerException != null)
        str = str + " ---> " + this.InnerException.ToString();
      if (this.StackTrace != null)
        str = str + Environment.NewLine + this.StackTrace;
      try
      {
        if (this.FusionLog != null)
        {
          if (str == null)
            str = " ";
          str += Environment.NewLine;
          str += Environment.NewLine;
          str += this.FusionLog;
        }
      }
      catch (SecurityException ex)
      {
      }
      return str;
    }

    /// <summary>用文件名、程序集缓存日志和其他异常信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("BadImageFormat_FileName", (object) this._fileName, typeof (string));
      try
      {
        info.AddValue("BadImageFormat_FusionLog", (object) this.FusionLog, typeof (string));
      }
      catch (SecurityException ex)
      {
      }
    }
  }
}
