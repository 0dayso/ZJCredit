// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
  /// <summary>检测到安全性错误时引发的异常。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SecurityException : SystemException
  {
    private string m_debugString;
    private SecurityAction m_action;
    [NonSerialized]
    private Type m_typeOfPermissionThatFailed;
    private string m_permissionThatFailed;
    private string m_demanded;
    private string m_granted;
    private string m_refused;
    private string m_denied;
    private string m_permitOnly;
    private AssemblyName m_assemblyName;
    private byte[] m_serializedMethodInfo;
    private string m_strMethodInfo;
    private SecurityZone m_zone;
    private string m_url;
    private const string ActionName = "Action";
    private const string FirstPermissionThatFailedName = "FirstPermissionThatFailed";
    private const string DemandedName = "Demanded";
    private const string GrantedSetName = "GrantedSet";
    private const string RefusedSetName = "RefusedSet";
    private const string DeniedName = "Denied";
    private const string PermitOnlyName = "PermitOnly";
    private const string Assembly_Name = "Assembly";
    private const string MethodName_Serialized = "Method";
    private const string MethodName_String = "Method_String";
    private const string ZoneName = "Zone";
    private const string UrlName = "Url";

    /// <summary>获取或设置导致异常的安全操作。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</returns>
    [ComVisible(false)]
    public SecurityAction Action
    {
      get
      {
        return this.m_action;
      }
      set
      {
        this.m_action = value;
      }
    }

    /// <summary>获取或设置失败权限的类型。</summary>
    /// <returns>失败权限的类型。</returns>
    public Type PermissionType
    {
      [SecuritySafeCritical] get
      {
        if (this.m_typeOfPermissionThatFailed == (Type) null)
        {
          object obj = XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed) ?? XMLUtil.XmlStringToSecurityObject(this.m_demanded);
          if (obj != null)
            this.m_typeOfPermissionThatFailed = obj.GetType();
        }
        return this.m_typeOfPermissionThatFailed;
      }
      set
      {
        this.m_typeOfPermissionThatFailed = value;
      }
    }

    /// <summary>获取或设置导致要求失败的权限集或权限集集合中的第一个权限。</summary>
    /// <returns>一个 <see cref="T:System.Security.IPermission" /> 对象，表示第一个失败的权限。</returns>
    public IPermission FirstPermissionThatFailed
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return (IPermission) XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed);
      }
      set
      {
        this.m_permissionThatFailed = XMLUtil.SecurityObjectToXmlString((object) value);
      }
    }

    /// <summary>获取或设置引发异常的权限的状态。</summary>
    /// <returns>在引发异常时权限的状态。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    public string PermissionState
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_demanded;
      }
      set
      {
        this.m_demanded = value;
      }
    }

    /// <summary>获取或设置失败的要求的安全权限、权限集或权限集集合。</summary>
    /// <returns>权限、权限集或权限集集合对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [ComVisible(false)]
    public object Demanded
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return XMLUtil.XmlStringToSecurityObject(this.m_demanded);
      }
      set
      {
        this.m_demanded = XMLUtil.SecurityObjectToXmlString(value);
      }
    }

    /// <summary>获取或设置导致 <see cref="T:System.Security.SecurityException" /> 的程序集的被授予的权限集。</summary>
    /// <returns>程序集的被授予的权限集的 XML 表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    public string GrantedSet
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_granted;
      }
      set
      {
        this.m_granted = value;
      }
    }

    /// <summary>获取或设置导致 <see cref="T:System.Security.SecurityException" /> 的程序集的被拒绝的权限集。</summary>
    /// <returns>程序集的被拒绝的权限集的 XML 表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    public string RefusedSet
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_refused;
      }
      set
      {
        this.m_refused = value;
      }
    }

    /// <summary>获取或设置导致要求失败的被拒绝的安全权限、权限集或权限集集合。</summary>
    /// <returns>权限、权限集或权限集集合对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [ComVisible(false)]
    public object DenySetInstance
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return XMLUtil.XmlStringToSecurityObject(this.m_denied);
      }
      set
      {
        this.m_denied = XMLUtil.SecurityObjectToXmlString(value);
      }
    }

    /// <summary>获取或设置导致安全检查失败的唯一允许堆栈帧的一部分权限、权限集或权限集集合。</summary>
    /// <returns>权限、权限集或权限集集合对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [ComVisible(false)]
    public object PermitOnlySetInstance
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return XMLUtil.XmlStringToSecurityObject(this.m_permitOnly);
      }
      set
      {
        this.m_permitOnly = XMLUtil.SecurityObjectToXmlString(value);
      }
    }

    /// <summary>获取或设置有关失败的程序集的信息。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.AssemblyName" />，用于标识失败的程序集。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [ComVisible(false)]
    public AssemblyName FailedAssemblyInfo
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_assemblyName;
      }
      set
      {
        this.m_assemblyName = value;
      }
    }

    /// <summary>获取或设置关于与异常关联的方法的信息。</summary>
    /// <returns>一个描述方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [ComVisible(false)]
    public MethodInfo Method
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.getMethod();
      }
      set
      {
        RuntimeMethodInfo runtimeMethodInfo = value as RuntimeMethodInfo;
        this.m_serializedMethodInfo = SecurityException.ObjectToByteArray((object) runtimeMethodInfo);
        if (!((MethodInfo) runtimeMethodInfo != (MethodInfo) null))
          return;
        this.m_strMethodInfo = runtimeMethodInfo.ToString();
      }
    }

    /// <summary>获取或设置导致异常的程序集区域。</summary>
    /// <returns>
    /// <see cref="T:System.Security.SecurityZone" /> 值之一，用于标识导致异常的程序集区域。</returns>
    public SecurityZone Zone
    {
      get
      {
        return this.m_zone;
      }
      set
      {
        this.m_zone = value;
      }
    }

    /// <summary>获取或设置导致异常的程序集的 URL。</summary>
    /// <returns>一个标识程序集位置的 URL。</returns>
    public string Url
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_url;
      }
      set
      {
        this.m_url = value;
      }
    }

    /// <summary>使用默认属性初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SecurityException()
      : base(SecurityException.GetResString("Arg_SecurityException"))
    {
      this.SetErrorCode(-2146233078);
    }

    /// <summary>使用指定的错误消息初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    [__DynamicallyInvokable]
    public SecurityException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233078);
    }

    /// <summary>用指定的错误消息和导致引发异常的权限类型初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="type">导致引发异常的权限类型。</param>
    [SecuritySafeCritical]
    public SecurityException(string message, Type type)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.m_typeOfPermissionThatFailed = type;
    }

    /// <summary>用指定的错误消息、引发异常的权限类型和权限状态来初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="type">导致引发异常的权限类型。</param>
    /// <param name="state">导致引发异常的权限状态。</param>
    [SecuritySafeCritical]
    public SecurityException(string message, Type type, string state)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.m_typeOfPermissionThatFailed = type;
      this.m_demanded = state;
    }

    /// <summary>使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="inner" /> 参数不为 null，则当前异常将在处理内部异常的 catch 块中引发。</param>
    [__DynamicallyInvokable]
    public SecurityException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233078);
    }

    [SecurityCritical]
    internal SecurityException(PermissionSet grantedSetObj, PermissionSet refusedSetObj)
      : base(SecurityException.GetResString("Arg_SecurityException"))
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      if (grantedSetObj != null)
        this.m_granted = grantedSetObj.ToXml().ToString();
      if (refusedSetObj == null)
        return;
      this.m_refused = refusedSetObj.ToXml().ToString();
    }

    [SecurityCritical]
    internal SecurityException(string message, PermissionSet grantedSetObj, PermissionSet refusedSetObj)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      if (grantedSetObj != null)
        this.m_granted = grantedSetObj.ToXml().ToString();
      if (refusedSetObj == null)
        return;
      this.m_refused = refusedSetObj.ToXml().ToString();
    }

    /// <summary>用序列化数据初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info " /> 为  null。</exception>
    [SecuritySafeCritical]
    protected SecurityException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      try
      {
        this.m_action = (SecurityAction) info.GetValue("Action", typeof (SecurityAction));
        this.m_permissionThatFailed = (string) info.GetValueNoThrow("FirstPermissionThatFailed", typeof (string));
        this.m_demanded = (string) info.GetValueNoThrow("Demanded", typeof (string));
        this.m_granted = (string) info.GetValueNoThrow("GrantedSet", typeof (string));
        this.m_refused = (string) info.GetValueNoThrow("RefusedSet", typeof (string));
        this.m_denied = (string) info.GetValueNoThrow("Denied", typeof (string));
        this.m_permitOnly = (string) info.GetValueNoThrow("PermitOnly", typeof (string));
        this.m_assemblyName = (AssemblyName) info.GetValueNoThrow("Assembly", typeof (AssemblyName));
        this.m_serializedMethodInfo = (byte[]) info.GetValueNoThrow("Method", typeof (byte[]));
        this.m_strMethodInfo = (string) info.GetValueNoThrow("Method_String", typeof (string));
        this.m_zone = (SecurityZone) info.GetValue("Zone", typeof (SecurityZone));
        this.m_url = (string) info.GetValueNoThrow("Url", typeof (string));
      }
      catch
      {
        this.m_action = (SecurityAction) 0;
        this.m_permissionThatFailed = "";
        this.m_demanded = "";
        this.m_granted = "";
        this.m_refused = "";
        this.m_denied = "";
        this.m_permitOnly = "";
        this.m_assemblyName = (AssemblyName) null;
        this.m_serializedMethodInfo = (byte[]) null;
        this.m_strMethodInfo = (string) null;
        this.m_zone = SecurityZone.NoZone;
        this.m_url = "";
      }
    }

    /// <summary>对于因授予权限集不足而导致的异常，初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="assemblyName">一个 <see cref="T:System.Reflection.AssemblyName" />，指定导致异常的程序集的名称。</param>
    /// <param name="grant">一个 <see cref="T:System.Security.PermissionSet" />，表示已授予程序集的权限。</param>
    /// <param name="refused">一个 <see cref="T:System.Security.PermissionSet" />，表示被拒绝的权限或权限集。</param>
    /// <param name="method">一个 <see cref="T:System.Reflection.MethodInfo" />，表示遇到异常的方法。</param>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    /// <param name="demanded">要求的权限、权限集或权限集集合。</param>
    /// <param name="permThatFailed">一个 <see cref="T:System.Security.IPermission" />，表示失败的权限。</param>
    /// <param name="evidence">导致异常的程序集的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    [SecuritySafeCritical]
    public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.Action = action;
      if (permThatFailed != null)
        this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
      this.FirstPermissionThatFailed = permThatFailed;
      this.Demanded = demanded;
      this.m_granted = grant == null ? "" : grant.ToXml().ToString();
      this.m_refused = refused == null ? "" : refused.ToXml().ToString();
      this.m_denied = "";
      this.m_permitOnly = "";
      this.m_assemblyName = assemblyName;
      this.Method = method;
      this.m_url = "";
      this.m_zone = SecurityZone.NoZone;
      if (evidence != null)
      {
        System.Security.Policy.Url hostEvidence1 = evidence.GetHostEvidence<System.Security.Policy.Url>();
        if (hostEvidence1 != null)
          this.m_url = hostEvidence1.GetURLString().ToString();
        System.Security.Policy.Zone hostEvidence2 = evidence.GetHostEvidence<System.Security.Policy.Zone>();
        if (hostEvidence2 != null)
          this.m_zone = hostEvidence2.SecurityZone;
      }
      this.m_debugString = this.ToString(true, false);
    }

    /// <summary>对于因堆栈上的 Deny 而导致的异常，初始化 <see cref="T:System.Security.SecurityException" /> 类的新实例。</summary>
    /// <param name="message">解释异常原因的错误信息。</param>
    /// <param name="deny">被拒绝的权限或权限集。</param>
    /// <param name="permitOnly">唯一允许的权限或权限集。</param>
    /// <param name="method">一个 <see cref="T:System.Reflection.MethodInfo" />，用于标识遇到异常的方法。</param>
    /// <param name="demanded">要求的权限、权限集或权限集集合。</param>
    /// <param name="permThatFailed">一个 <see cref="T:System.Security.IPermission" />，用于标识失败的权限。</param>
    [SecuritySafeCritical]
    public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.Action = SecurityAction.Demand;
      if (permThatFailed != null)
        this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
      this.FirstPermissionThatFailed = permThatFailed;
      this.Demanded = demanded;
      this.m_granted = "";
      this.m_refused = "";
      this.DenySetInstance = deny;
      this.PermitOnlySetInstance = permitOnly;
      this.m_assemblyName = (AssemblyName) null;
      this.Method = method;
      this.m_zone = SecurityZone.NoZone;
      this.m_url = "";
      this.m_debugString = this.ToString(true, false);
    }

    [SecuritySafeCritical]
    internal static string GetResString(string sResourceName)
    {
      PermissionSet.s_fullTrust.Assert();
      return Environment.GetResourceString(sResourceName);
    }

    [SecurityCritical]
    internal static Exception MakeSecurityException(AssemblyName asmName, Evidence asmEvidence, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
    {
      HostProtectionPermission protectionPermission = permThatFailed as HostProtectionPermission;
      if (protectionPermission != null)
        return (Exception) new HostProtectionException(SecurityException.GetResString("HostProtection_HostProtection"), HostProtectionPermission.protectedResources, protectionPermission.Resources);
      string message = "";
      MethodInfo method = (MethodInfo) null;
      try
      {
        message = granted != null || refused != null || demand != null ? (demand == null || !(demand is IPermission) ? (permThatFailed == null ? SecurityException.GetResString("Security_GenericNoType") : string.Format((IFormatProvider) CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), (object) permThatFailed.GetType().AssemblyQualifiedName)) : string.Format((IFormatProvider) CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), (object) demand.GetType().AssemblyQualifiedName)) : SecurityException.GetResString("Security_NoAPTCA");
        method = SecurityRuntime.GetMethodInfo(rmh);
      }
      catch (Exception ex)
      {
        if (ex is ThreadAbortException)
          throw;
      }
      return (Exception) new SecurityException(message, asmName, granted, refused, method, action, demand, permThatFailed, asmEvidence);
    }

    private static byte[] ObjectToByteArray(object obj)
    {
      if (obj == null)
        return (byte[]) null;
      MemoryStream memoryStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        binaryFormatter.Serialize((Stream) memoryStream, obj);
        return memoryStream.ToArray();
      }
      catch (NotSupportedException ex)
      {
        return (byte[]) null;
      }
    }

    private static object ByteArrayToObject(byte[] array)
    {
      if (array == null || array.Length == 0)
        return (object) null;
      return new BinaryFormatter().Deserialize((Stream) new MemoryStream(array));
    }

    private MethodInfo getMethod()
    {
      return (MethodInfo) SecurityException.ByteArrayToObject(this.m_serializedMethodInfo);
    }

    private void ToStringHelper(StringBuilder sb, string resourceString, object attr)
    {
      if (attr == null)
        return;
      string str = attr as string ?? attr.ToString();
      if (str.Length == 0)
        return;
      sb.Append(Environment.NewLine);
      sb.Append(SecurityException.GetResString(resourceString));
      sb.Append(Environment.NewLine);
      sb.Append(str);
    }

    [SecurityCritical]
    private string ToString(bool includeSensitiveInfo, bool includeBaseInfo)
    {
      PermissionSet.s_fullTrust.Assert();
      StringBuilder sb = new StringBuilder();
      if (includeBaseInfo)
        sb.Append(base.ToString());
      if (this.Action > (SecurityAction) 0)
        this.ToStringHelper(sb, "Security_Action", (object) this.Action);
      this.ToStringHelper(sb, "Security_TypeFirstPermThatFailed", (object) this.PermissionType);
      if (includeSensitiveInfo)
      {
        this.ToStringHelper(sb, "Security_FirstPermThatFailed", (object) this.m_permissionThatFailed);
        this.ToStringHelper(sb, "Security_Demanded", (object) this.m_demanded);
        this.ToStringHelper(sb, "Security_GrantedSet", (object) this.m_granted);
        this.ToStringHelper(sb, "Security_RefusedSet", (object) this.m_refused);
        this.ToStringHelper(sb, "Security_Denied", (object) this.m_denied);
        this.ToStringHelper(sb, "Security_PermitOnly", (object) this.m_permitOnly);
        this.ToStringHelper(sb, "Security_Assembly", (object) this.m_assemblyName);
        this.ToStringHelper(sb, "Security_Method", (object) this.m_strMethodInfo);
      }
      if (this.m_zone != SecurityZone.NoZone)
        this.ToStringHelper(sb, "Security_Zone", (object) this.m_zone);
      if (includeSensitiveInfo)
        this.ToStringHelper(sb, "Security_Url", (object) this.m_url);
      return sb.ToString();
    }

    [SecurityCritical]
    private bool CanAccessSensitiveInfo()
    {
      bool flag = false;
      try
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy).Demand();
        flag = true;
      }
      catch (SecurityException ex)
      {
      }
      return flag;
    }

    /// <summary>返回当前 <see cref="T:System.Security.SecurityException" /> 的表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.SecurityException" /> 的字符串表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString(this.CanAccessSensitiveInfo(), true);
    }

    /// <summary>通过有关 <see cref="T:System.Security.SecurityException" /> 的信息设置 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("Action", (object) this.m_action, typeof (SecurityAction));
      info.AddValue("FirstPermissionThatFailed", (object) this.m_permissionThatFailed, typeof (string));
      info.AddValue("Demanded", (object) this.m_demanded, typeof (string));
      info.AddValue("GrantedSet", (object) this.m_granted, typeof (string));
      info.AddValue("RefusedSet", (object) this.m_refused, typeof (string));
      info.AddValue("Denied", (object) this.m_denied, typeof (string));
      info.AddValue("PermitOnly", (object) this.m_permitOnly, typeof (string));
      info.AddValue("Assembly", (object) this.m_assemblyName, typeof (AssemblyName));
      info.AddValue("Method", (object) this.m_serializedMethodInfo, typeof (byte[]));
      info.AddValue("Method_String", (object) this.m_strMethodInfo, typeof (string));
      info.AddValue("Zone", (object) this.m_zone, typeof (SecurityZone));
      info.AddValue("Url", (object) this.m_url, typeof (string));
    }
  }
}
