// Decompiled with JetBrains decompiler
// Type: System.IO.FileLoadException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>当找到托管程序集却不能加载它时引发的异常。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FileLoadException : IOException
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
    /// <returns>一个 <see cref="T:System.String" />，包含具有无效图像的文件的名称；或者，如果没有将文件名传递给当前实例的构造函数，则为空引用。</returns>
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

    /// <summary>初始化 <see cref="T:System.IO.FileLoadException" /> 类的新实例，将新实例的 <see cref="P:System.Exception.Message" /> 属性设置为描述错误的系统提供的消息（如“无法加载指定文件”）。此消息将考虑当前系统区域性。</summary>
    [__DynamicallyInvokable]
    public FileLoadException()
      : base(Environment.GetResourceString("IO.FileLoad"))
    {
      this.SetErrorCode(-2146232799);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.IO.FileLoadException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    [__DynamicallyInvokable]
    public FileLoadException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232799);
    }

    /// <summary>使用指定错误信息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.IO.FileLoadException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public FileLoadException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232799);
    }

    /// <summary>使用指定错误信息和不能加载的文件的名称来初始化 <see cref="T:System.IO.FileLoadException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="fileName">一个 <see cref="T:System.String" />，它包含未加载的文件的名称。</param>
    [__DynamicallyInvokable]
    public FileLoadException(string message, string fileName)
      : base(message)
    {
      this.SetErrorCode(-2146232799);
      this._fileName = fileName;
    }

    /// <summary>使用指定的错误信息、不能加载的文件的名称和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.IO.FileLoadException" /> 类的新实例。</summary>
    /// <param name="message">描述该错误的 <see cref="T:System.String" />。<paramref name="message" /> 的内容被设计为人可理解的形式。此构造函数的调用方需要确保此字符串已针对当前系统区域性进行了本地化。</param>
    /// <param name="fileName">一个 <see cref="T:System.String" />，它包含未加载的文件的名称。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public FileLoadException(string message, string fileName, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232799);
      this._fileName = fileName;
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.IO.FileLoadException" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    protected FileLoadException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._fileName = info.GetString("FileLoad_FileName");
      try
      {
        this._fusionLog = info.GetString("FileLoad_FusionLog");
      }
      catch
      {
        this._fusionLog = (string) null;
      }
    }

    private FileLoadException(string fileName, string fusionLog, int hResult)
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
      this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, this.HResult);
    }

    /// <summary>返回当前异常的完全限定名，还可能返回错误信息、内部异常的名称和堆栈跟踪。</summary>
    /// <returns>一个字符串，它包含此异常的完全限定名，还可能包含错误信息、内部异常的名称和堆栈跟踪（取决于所使用的 <see cref="T:System.IO.FileLoadException" /> 构造函数）。</returns>
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

    /// <summary>使用文件名和其他异常信息来设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("FileLoad_FileName", (object) this._fileName, typeof (string));
      try
      {
        info.AddValue("FileLoad_FusionLog", (object) this.FusionLog, typeof (string));
      }
      catch (SecurityException ex)
      {
      }
    }

    [SecuritySafeCritical]
    internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
    {
      string s1 = (string) null;
      FileLoadException.GetFileLoadExceptionMessage(hResult, JitHelpers.GetStringHandleOnStack(ref s1));
      string s2 = (string) null;
      FileLoadException.GetMessageForHR(hResult, JitHelpers.GetStringHandleOnStack(ref s2));
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, s1, (object) fileName, (object) s2);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetFileLoadExceptionMessage(int hResult, StringHandleOnStack retString);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMessageForHR(int hresult, StringHandleOnStack retString);
  }
}
