// Decompiled with JetBrains decompiler
// Type: System.IO.FileNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>尝试访问磁盘上不存在的文件失败时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FileNotFoundException : IOException
  {
    private string _fileName;
    private string _fusionLog;

    /// <summary>获取解释异常原因的错误消息。</summary>
    /// <returns>错误消息。</returns>
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

    /// <summary>获取无法找到的文件的名称。</summary>
    /// <returns>文件的名称，如果没有将文件名传递给此实例的构造函数，则为 null。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string FileName
    {
      [__DynamicallyInvokable] get
      {
        return this._fileName;
      }
    }

    /// <summary>获取日志文件，该文件描述加载程序集失败的原因。</summary>
    /// <returns>由程序集缓存报告的错误。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
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

    /// <summary>初始化 <see cref="T:System.IO.FileNotFoundException" /> 类的新实例，使其消息字符串设置为系统所提供的消息，其 HRESULT 设置为 COR_E_FILENOTFOUND。</summary>
    [__DynamicallyInvokable]
    public FileNotFoundException()
      : base(Environment.GetResourceString("IO.FileNotFound"))
    {
      this.SetErrorCode(-2147024894);
    }

    /// <summary>初始化 <see cref="T:System.IO.FileNotFoundException" /> 类的新实例，使其消息字符串设置为 <paramref name="message" />，其 HRESULT 设置为 COR_E_FILENOTFOUND。</summary>
    /// <param name="message">错误说明。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024894);
    }

    /// <summary>使用指定错误消息和对导致此异常的内部异常的引用来初始化 <see cref="T:System.IO.FileNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">错误说明。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024894);
    }

    /// <summary>初始化 <see cref="T:System.IO.FileNotFoundException" /> 类的新实例，使其消息字符串设置为 <paramref name="message" />（用于指定无法找到的文件名），其 HRESULT 设置为 COR_E_FILENOTFOUND。</summary>
    /// <param name="message">错误说明。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="fileName">包含无效图像的文件的全名。</param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message, string fileName)
      : base(message)
    {
      this.SetErrorCode(-2147024894);
      this._fileName = fileName;
    }

    /// <summary>使用指定错误消息和对导致此异常的内部异常的引用来初始化 <see cref="T:System.IO.FileNotFoundException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="fileName">包含无效图像的文件的全名。</param>
    /// <param name="innerException">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message, string fileName, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024894);
      this._fileName = fileName;
    }

    /// <summary>用指定的序列化和上下文信息初始化 <see cref="T:System.IO.FileNotFoundException" /> 类的新实例。</summary>
    /// <param name="info">保存有关所引发异常的序列化对象数据的对象。</param>
    /// <param name="context">一个包含有关源或目标的上下文信息的对象。</param>
    protected FileNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._fileName = info.GetString("FileNotFound_FileName");
      try
      {
        this._fusionLog = info.GetString("FileNotFound_FusionLog");
      }
      catch
      {
        this._fusionLog = (string) null;
      }
    }

    private FileNotFoundException(string fileName, string fusionLog, int hResult)
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
      {
        this._message = Environment.GetResourceString("IO.FileNotFound");
      }
      else
      {
        if (this._fileName == null)
          return;
        this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, this.HResult);
      }
    }

    /// <summary>返回该异常的完全限定名，还可能返回错误消息、内部异常的名称和堆栈跟踪。</summary>
    /// <returns>此异常的完全限定名，还可能包含错误消息、内部异常的名称和堆栈跟踪。</returns>
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

    /// <summary>设置带有文件名和附加异常信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">保存有关所引发异常的序列化对象数据的对象。</param>
    /// <param name="context">包含有关源或目标的上下文信息的对象。</param>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("FileNotFound_FileName", (object) this._fileName, typeof (string));
      try
      {
        info.AddValue("FileNotFound_FusionLog", (object) this.FusionLog, typeof (string));
      }
      catch (SecurityException ex)
      {
      }
    }
  }
}
