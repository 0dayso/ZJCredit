// Decompiled with JetBrains decompiler
// Type: System.Exception
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System
{
  /// <summary>表示在应用程序执行过程中发生的错误。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <filterpriority>1</filterpriority>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Exception))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Exception : ISerializable, _Exception
  {
    [OptionalField]
    private static object s_EDILock = new object();
    private string _className;
    private MethodBase _exceptionMethod;
    private string _exceptionMethodString;
    internal string _message;
    private IDictionary _data;
    private Exception _innerException;
    private string _helpURL;
    private object _stackTrace;
    [OptionalField]
    private object _watsonBuckets;
    private string _stackTraceString;
    private string _remoteStackTraceString;
    private int _remoteStackIndex;
    private object _dynamicMethods;
    internal int _HResult;
    private string _source;
    private IntPtr _xptrs;
    private int _xcode;
    [OptionalField]
    private UIntPtr _ipForWatsonBuckets;
    [OptionalField(VersionAdded = 4)]
    private SafeSerializationManager _safeSerializationManager;
    private const int _COMPlusExceptionCode = -532462766;

    /// <summary>获取描述当前异常的消息。</summary>
    /// <returns>解释异常原因的错误消息或空字符串 ("")。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual string Message
    {
      [__DynamicallyInvokable] get
      {
        if (this._message != null)
          return this._message;
        if (this._className == null)
          this._className = this.GetClassName();
        return Environment.GetResourceString("Exception_WasThrown", (object) this._className);
      }
    }

    /// <summary>获取提供有关异常的其他用户定义信息的键/值对集合。</summary>
    /// <returns>一个对象，它实现 <see cref="T:System.Collections.IDictionary" /> 接口并包含用户定义的键/值对的集合。默认值为空集合。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual IDictionary Data
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this._data == null)
          this._data = !Exception.IsImmutableAgileException(this) ? (IDictionary) new ListDictionaryInternal() : (IDictionary) new EmptyReadOnlyDictionaryInternal();
        return this._data;
      }
    }

    /// <summary>获取导致当前异常的 <see cref="T:System.Exception" /> 实例。</summary>
    /// <returns>描述导致当前异常的错误的一个对象。<see cref="P:System.Exception.InnerException" /> 属性返回的值与传递到 <see cref="M:System.Exception.#ctor(System.String,System.Exception)" /> 构造函数中的值相同，如果没有向构造函数提供内部异常值，则为 null。此属性是只读的。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public Exception InnerException
    {
      [__DynamicallyInvokable] get
      {
        return this._innerException;
      }
    }

    /// <summary>获取引发当前异常的方法。</summary>
    /// <returns>引发当前异常的 <see cref="T:System.Reflection.MethodBase" />。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public MethodBase TargetSite
    {
      [SecuritySafeCritical] get
      {
        return this.GetTargetSiteInternal();
      }
    }

    /// <summary>获取调用堆栈上的即时框架字符串表示形式。</summary>
    /// <returns>用于描述调用堆栈的直接帧的字符串。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual string StackTrace
    {
      [__DynamicallyInvokable] get
      {
        return this.GetStackTrace(true);
      }
    }

    /// <summary>获取或设置指向与此异常关联的帮助文件链接。</summary>
    /// <returns>统一资源名称 (URN) 或统一资源定位器 (URL)。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string HelpLink
    {
      [__DynamicallyInvokable] get
      {
        return this._helpURL;
      }
      [__DynamicallyInvokable] set
      {
        this._helpURL = value;
      }
    }

    /// <summary>获取或设置导致错误的应用程序或对象的名称。</summary>
    /// <returns>导致错误的应用程序或对象的名称。</returns>
    /// <exception cref="T:System.ArgumentException">The object must be a runtime <see cref="N:System.Reflection" /> object</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string Source
    {
      [__DynamicallyInvokable] get
      {
        if (this._source == null)
        {
          System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(this, true);
          if (stackTrace.FrameCount > 0)
          {
            Module module = stackTrace.GetFrame(0).GetMethod().Module;
            RuntimeModule runtimeModule = module as RuntimeModule;
            if ((Module) runtimeModule == (Module) null)
            {
              ModuleBuilder moduleBuilder = module as ModuleBuilder;
              if (!((Module) moduleBuilder != (Module) null))
                throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
              runtimeModule = (RuntimeModule) moduleBuilder.InternalModule;
            }
            this._source = runtimeModule.GetRuntimeAssembly().GetSimpleName();
          }
        }
        return this._source;
      }
      [__DynamicallyInvokable] set
      {
        this._source = value;
      }
    }

    internal UIntPtr IPForWatsonBuckets
    {
      get
      {
        return this._ipForWatsonBuckets;
      }
    }

    internal object WatsonBuckets
    {
      get
      {
        return this._watsonBuckets;
      }
    }

    internal string RemoteStackTrace
    {
      get
      {
        return this._remoteStackTraceString;
      }
    }

    /// <summary>获取或设置 HRESULT（一个分配给特定异常的编码数字值）。</summary>
    /// <returns>HRESULT 值。</returns>
    [__DynamicallyInvokable]
    public int HResult
    {
      [__DynamicallyInvokable] get
      {
        return this._HResult;
      }
      [__DynamicallyInvokable] protected set
      {
        this._HResult = value;
      }
    }

    internal bool IsTransient
    {
      [SecuritySafeCritical] get
      {
        return Exception.nIsTransient(this._HResult);
      }
    }

    /// <summary>当异常被序列化用来创建包含有关该异常的徐列出数据的异常状态对象时会出现该问题。</summary>
    protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
    {
      add
      {
        this._safeSerializationManager.SerializeObjectState += value;
      }
      remove
      {
        this._safeSerializationManager.SerializeObjectState -= value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Exception" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public Exception()
    {
      this.Init();
    }

    /// <summary>用指定的错误消息初始化 <see cref="T:System.Exception" /> 类的新实例。</summary>
    /// <param name="message">描述错误的消息。</param>
    [__DynamicallyInvokable]
    public Exception(string message)
    {
      this.Init();
      this._message = message;
    }

    /// <summary>使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Exception" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误消息。</param>
    /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
    [__DynamicallyInvokable]
    public Exception(string message, Exception innerException)
    {
      this.Init();
      this._message = message;
      this._innerException = innerException;
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Exception" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存关于所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含关于源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
    [SecuritySafeCritical]
    protected Exception(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this._className = info.GetString("ClassName");
      this._message = info.GetString("Message");
      this._data = (IDictionary) info.GetValueNoThrow("Data", typeof (IDictionary));
      this._innerException = (Exception) info.GetValue("InnerException", typeof (Exception));
      this._helpURL = info.GetString("HelpURL");
      this._stackTraceString = info.GetString("StackTraceString");
      this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
      this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
      this._exceptionMethodString = (string) info.GetValue("ExceptionMethod", typeof (string));
      this.HResult = info.GetInt32("HResult");
      this._source = info.GetString("Source");
      this._watsonBuckets = info.GetValueNoThrow("WatsonBuckets", typeof (byte[]));
      this._safeSerializationManager = info.GetValueNoThrow("SafeSerializationManager", typeof (SafeSerializationManager)) as SafeSerializationManager;
      if (this._className == null || this.HResult == 0)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      if (context.State != StreamingContextStates.CrossAppDomain)
        return;
      this._remoteStackTraceString = this._remoteStackTraceString + this._stackTraceString;
      this._stackTraceString = (string) null;
    }

    private void Init()
    {
      this._message = (string) null;
      this._stackTrace = (object) null;
      this._dynamicMethods = (object) null;
      this.HResult = -2146233088;
      this._xcode = -532462766;
      this._xptrs = (IntPtr) 0;
      this._watsonBuckets = (object) null;
      this._ipForWatsonBuckets = UIntPtr.Zero;
      this._safeSerializationManager = new SafeSerializationManager();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsImmutableAgileException(Exception e);

    [FriendAccessAllowed]
    internal void AddExceptionDataForRestrictedErrorInfo(string restrictedError, string restrictedErrorReference, string restrictedCapabilitySid, object restrictedErrorObject, bool hasrestrictedLanguageErrorObject = false)
    {
      IDictionary data = this.Data;
      if (data == null)
        return;
      data.Add((object) "RestrictedDescription", (object) restrictedError);
      data.Add((object) "RestrictedErrorReference", (object) restrictedErrorReference);
      data.Add((object) "RestrictedCapabilitySid", (object) restrictedCapabilitySid);
      data.Add((object) "__RestrictedErrorObject", restrictedErrorObject == null ? (object) (Exception.__RestrictedErrorObject) null : (object) new Exception.__RestrictedErrorObject(restrictedErrorObject));
      data.Add((object) "__HasRestrictedLanguageErrorObject", (object) hasrestrictedLanguageErrorObject);
    }

    internal bool TryGetRestrictedLanguageErrorObject(out object restrictedErrorObject)
    {
      restrictedErrorObject = (object) null;
      if (this.Data == null || !this.Data.Contains((object) "__HasRestrictedLanguageErrorObject"))
        return false;
      if (this.Data.Contains((object) "__RestrictedErrorObject"))
      {
        Exception.__RestrictedErrorObject restrictedErrorObject1 = this.Data[(object) "__RestrictedErrorObject"] as Exception.__RestrictedErrorObject;
        if (restrictedErrorObject1 != null)
          restrictedErrorObject = restrictedErrorObject1.RealErrorObject;
      }
      return (bool) this.Data[(object) "__HasRestrictedLanguageErrorObject"];
    }

    private string GetClassName()
    {
      if (this._className == null)
        this._className = this.GetType().ToString();
      return this._className;
    }

    /// <summary>当在派生类中重写时，返回 <see cref="T:System.Exception" />，它是一个或多个并发的异常的根源。</summary>
    /// <returns>异常链中第一个被引发的异常。如果当前异常的 <see cref="P:System.Exception.InnerException" /> 属性是 null 引用（Visual Basic 中为 Nothing），则此属性返回当前异常。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Exception GetBaseException()
    {
      Exception innerException = this.InnerException;
      Exception exception = this;
      for (; innerException != null; innerException = innerException.InnerException)
        exception = innerException;
      return exception;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IRuntimeMethodInfo GetMethodFromStackTrace(object stackTrace);

    [SecuritySafeCritical]
    private MethodBase GetExceptionMethodFromStackTrace()
    {
      IRuntimeMethodInfo methodFromStackTrace = Exception.GetMethodFromStackTrace(this._stackTrace);
      if (methodFromStackTrace == null)
        return (MethodBase) null;
      return RuntimeType.GetMethodBase(methodFromStackTrace);
    }

    [SecurityCritical]
    private MethodBase GetTargetSiteInternal()
    {
      if (this._exceptionMethod != (MethodBase) null)
        return this._exceptionMethod;
      if (this._stackTrace == null)
        return (MethodBase) null;
      this._exceptionMethod = this._exceptionMethodString == null ? this.GetExceptionMethodFromStackTrace() : this.GetExceptionMethodFromString();
      return this._exceptionMethod;
    }

    private string GetStackTrace(bool needFileInfo)
    {
      string stackTrace1 = this._stackTraceString;
      string stackTrace2 = this._remoteStackTraceString;
      if (!needFileInfo)
      {
        stackTrace1 = this.StripFileInfo(stackTrace1, false);
        stackTrace2 = this.StripFileInfo(stackTrace2, true);
      }
      if (stackTrace1 != null)
        return stackTrace2 + stackTrace1;
      if (this._stackTrace == null)
        return stackTrace2;
      string stackTrace3 = Environment.GetStackTrace(this, needFileInfo);
      return stackTrace2 + stackTrace3;
    }

    [FriendAccessAllowed]
    internal void SetErrorCode(int hr)
    {
      this.HResult = hr;
    }

    /// <summary>创建并返回当前异常的字符串表示形式。</summary>
    /// <returns>当前异常的字符串表示形式。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString(true, true);
    }

    private string ToString(bool needFileLineInfo, bool needMessage)
    {
      string str1 = needMessage ? this.Message : (string) null;
      string str2 = str1 == null || str1.Length <= 0 ? this.GetClassName() : this.GetClassName() + ": " + str1;
      if (this._innerException != null)
        str2 = str2 + " ---> " + this._innerException.ToString(needFileLineInfo, needMessage) + Environment.NewLine + "   " + Environment.GetResourceString("Exception_EndOfInnerExceptionStack");
      string stackTrace = this.GetStackTrace(needFileLineInfo);
      if (stackTrace != null)
        str2 = str2 + Environment.NewLine + stackTrace;
      return str2;
    }

    [SecurityCritical]
    private string GetExceptionMethodString()
    {
      MethodBase targetSiteInternal = this.GetTargetSiteInternal();
      if (targetSiteInternal == (MethodBase) null)
        return (string) null;
      if (targetSiteInternal is DynamicMethod.RTDynamicMethod)
        return (string) null;
      char ch = '\n';
      StringBuilder stringBuilder = new StringBuilder();
      if (targetSiteInternal is ConstructorInfo)
      {
        RuntimeConstructorInfo runtimeConstructorInfo = (RuntimeConstructorInfo) targetSiteInternal;
        Type reflectedType = runtimeConstructorInfo.ReflectedType;
        stringBuilder.Append(1);
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeConstructorInfo.Name);
        if (reflectedType != (Type) null)
        {
          stringBuilder.Append(ch);
          stringBuilder.Append(reflectedType.Assembly.FullName);
          stringBuilder.Append(ch);
          stringBuilder.Append(reflectedType.FullName);
        }
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeConstructorInfo.ToString());
      }
      else
      {
        RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo) targetSiteInternal;
        Type declaringType = runtimeMethodInfo.DeclaringType;
        stringBuilder.Append(8);
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeMethodInfo.Name);
        stringBuilder.Append(ch);
        stringBuilder.Append(runtimeMethodInfo.Module.Assembly.FullName);
        stringBuilder.Append(ch);
        if (declaringType != (Type) null)
        {
          stringBuilder.Append(declaringType.FullName);
          stringBuilder.Append(ch);
        }
        stringBuilder.Append(runtimeMethodInfo.ToString());
      }
      return stringBuilder.ToString();
    }

    [SecurityCritical]
    private MethodBase GetExceptionMethodFromString()
    {
      string[] strArray = this._exceptionMethodString.Split(new char[2]
      {
        char.MinValue,
        '\n'
      });
      if (strArray.Length != 5)
        throw new SerializationException();
      SerializationInfo info = new SerializationInfo(typeof (MemberInfoSerializationHolder), (IFormatterConverter) new FormatterConverter());
      info.AddValue("MemberType", (object) int.Parse(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture), typeof (int));
      info.AddValue("Name", (object) strArray[1], typeof (string));
      info.AddValue("AssemblyName", (object) strArray[2], typeof (string));
      info.AddValue("ClassName", (object) strArray[3]);
      info.AddValue("Signature", (object) strArray[4]);
      StreamingContext context = new StreamingContext(StreamingContextStates.All);
      try
      {
        return (MethodBase) new MemberInfoSerializationHolder(info, context).GetRealObject(context);
      }
      catch (SerializationException ex)
      {
        return (MethodBase) null;
      }
    }

    /// <summary>当在派生类中重写时，用关于异常的信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存关于所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含关于源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic). </exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      string str = this._stackTraceString;
      if (this._stackTrace != null)
      {
        if (str == null)
          str = Environment.GetStackTrace(this, true);
        if (this._exceptionMethod == (MethodBase) null)
          this._exceptionMethod = this.GetExceptionMethodFromStackTrace();
      }
      if (this._source == null)
        this._source = this.Source;
      info.AddValue("ClassName", (object) this.GetClassName(), typeof (string));
      info.AddValue("Message", (object) this._message, typeof (string));
      info.AddValue("Data", (object) this._data, typeof (IDictionary));
      info.AddValue("InnerException", (object) this._innerException, typeof (Exception));
      info.AddValue("HelpURL", (object) this._helpURL, typeof (string));
      info.AddValue("StackTraceString", (object) str, typeof (string));
      info.AddValue("RemoteStackTraceString", (object) this._remoteStackTraceString, typeof (string));
      info.AddValue("RemoteStackIndex", (object) this._remoteStackIndex, typeof (int));
      info.AddValue("ExceptionMethod", (object) this.GetExceptionMethodString(), typeof (string));
      info.AddValue("HResult", this.HResult);
      info.AddValue("Source", (object) this._source, typeof (string));
      info.AddValue("WatsonBuckets", this._watsonBuckets, typeof (byte[]));
      if (this._safeSerializationManager == null || !this._safeSerializationManager.IsActive)
        return;
      info.AddValue("SafeSerializationManager", (object) this._safeSerializationManager, typeof (SafeSerializationManager));
      this._safeSerializationManager.CompleteSerialization((object) this, info, context);
    }

    internal Exception PrepForRemoting()
    {
      string str;
      if (this._remoteStackIndex == 0)
        str = Environment.NewLine + "Server stack trace: " + Environment.NewLine + this.StackTrace + Environment.NewLine + Environment.NewLine + "Exception rethrown at [" + (object) this._remoteStackIndex + "]: " + Environment.NewLine;
      else
        str = this.StackTrace + Environment.NewLine + Environment.NewLine + "Exception rethrown at [" + (object) this._remoteStackIndex + "]: " + Environment.NewLine;
      this._remoteStackTraceString = str;
      this._remoteStackIndex = this._remoteStackIndex + 1;
      return this;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      this._stackTrace = (object) null;
      this._ipForWatsonBuckets = UIntPtr.Zero;
      if (this._safeSerializationManager == null)
        this._safeSerializationManager = new SafeSerializationManager();
      else
        this._safeSerializationManager.CompleteDeserialization((object) this);
    }

    internal void InternalPreserveStackTrace()
    {
      string stackTrace;
      if (AppDomain.IsAppXModel())
      {
        stackTrace = this.GetStackTrace(true);
        string source = this.Source;
      }
      else
        stackTrace = this.StackTrace;
      if (stackTrace != null && stackTrace.Length > 0)
        this._remoteStackTraceString = stackTrace + Environment.NewLine;
      this._stackTrace = (object) null;
      this._stackTraceString = (string) null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void PrepareForForeignExceptionRaise();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetStackTracesDeepCopy(Exception exception, out object currentStackTrace, out object dynamicMethodArray);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SaveStackTracesFromDeepCopy(Exception exception, object currentStackTrace, object dynamicMethodArray);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object CopyStackTrace(object currentStackTrace);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object CopyDynamicMethods(object currentDynamicMethods);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string StripFileInfo(string stackTrace, bool isRemoteStackTrace);

    [SecuritySafeCritical]
    internal object DeepCopyStackTrace(object currentStackTrace)
    {
      if (currentStackTrace != null)
        return Exception.CopyStackTrace(currentStackTrace);
      return (object) null;
    }

    [SecuritySafeCritical]
    internal object DeepCopyDynamicMethods(object currentDynamicMethods)
    {
      if (currentDynamicMethods != null)
        return Exception.CopyDynamicMethods(currentDynamicMethods);
      return (object) null;
    }

    [SecuritySafeCritical]
    internal void GetStackTracesDeepCopy(out object currentStackTrace, out object dynamicMethodArray)
    {
      Exception.GetStackTracesDeepCopy(this, out currentStackTrace, out dynamicMethodArray);
    }

    [SecuritySafeCritical]
    internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
    {
      if (Exception.IsImmutableAgileException(this))
        return;
      try
      {
      }
      finally
      {
        object currentStackTrace = exceptionDispatchInfo.BinaryStackTraceArray == null ? (object) null : this.DeepCopyStackTrace(exceptionDispatchInfo.BinaryStackTraceArray);
        object dynamicMethodArray = exceptionDispatchInfo.DynamicMethodArray == null ? (object) null : this.DeepCopyDynamicMethods(exceptionDispatchInfo.DynamicMethodArray);
        lock (Exception.s_EDILock)
        {
          this._watsonBuckets = exceptionDispatchInfo.WatsonBuckets;
          this._ipForWatsonBuckets = exceptionDispatchInfo.IPForWatsonBuckets;
          this._remoteStackTraceString = exceptionDispatchInfo.RemoteStackTrace;
          Exception.SaveStackTracesFromDeepCopy(this, currentStackTrace, dynamicMethodArray);
        }
        this._stackTraceString = (string) null;
        Exception.PrepareForForeignExceptionRaise();
      }
    }

    [SecurityCritical]
    internal virtual string InternalToString()
    {
      try
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy).Assert();
      }
      catch
      {
      }
      return this.ToString(true, true);
    }

    /// <summary>获取当前实例的运行时类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示当前实例的确切运行时类型。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public new Type GetType()
    {
      return base.GetType();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nIsTransient(int hr);

    [SecuritySafeCritical]
    internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
    {
      string s = (string) null;
      Exception.GetMessageFromNativeResources(kind, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMessageFromNativeResources(Exception.ExceptionMessageKind kind, StringHandleOnStack retMesg);

    [Serializable]
    internal class __RestrictedErrorObject
    {
      [NonSerialized]
      private object _realErrorObject;

      public object RealErrorObject
      {
        get
        {
          return this._realErrorObject;
        }
      }

      internal __RestrictedErrorObject(object errorObject)
      {
        this._realErrorObject = errorObject;
      }
    }

    internal enum ExceptionMessageKind
    {
      ThreadAbort = 1,
      ThreadInterrupted = 2,
      OutOfMemory = 3,
    }
  }
}
