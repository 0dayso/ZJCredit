// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.InternalRemotingServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>定义供 .NET Framework 远程处理基础结构使用的实用工具方法。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class InternalRemotingServices
  {
    /// <summary>发送一条有关连接到非托管调试器的远程处理信道的消息。</summary>
    /// <param name="s">要放在消息中的字符串。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [Conditional("_LOGGING")]
    public static void DebugOutChnl(string s)
    {
      Message.OutToUnmanagedDebugger("CHNL:" + s + "\n");
    }

    /// <summary>发送有关连接到内部调试器的远程处理信道的任意数量的消息。</summary>
    /// <param name="messages">包含任意数量消息的 <see cref="T:System.Object" /> 类型的数组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [Conditional("_LOGGING")]
    public static void RemotingTrace(params object[] messages)
    {
    }

    /// <summary>指示内部调试器检查某个条件，并在该条件为 false 时显示一条消息。</summary>
    /// <param name="condition">若要禁止显示消息，则为 true；否则为 false。</param>
    /// <param name="message">要在 <paramref name="condition" /> 为 false 时显示的消息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [Conditional("_DEBUG")]
    public static void RemotingAssert(bool condition, string message)
    {
    }

    /// <summary>为从客户端到服务器的每个方法调用设置远程服务器对象的内部标识信息。</summary>
    /// <param name="m">表示远程对象上的方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" />。</param>
    /// <param name="srvID">远程服务器对象的内部标识信息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static void SetServerIdentity(MethodCall m, object srvID)
    {
      ((IInternalMessage) m).ServerIdentityObject = (ServerIdentity) srvID;
    }

    internal static RemotingMethodCachedData GetReflectionCachedData(MethodBase mi)
    {
      RuntimeMethodInfo runtimeMethodInfo;
      if ((MethodInfo) (runtimeMethodInfo = mi as RuntimeMethodInfo) != (MethodInfo) null)
        return runtimeMethodInfo.RemotingCache;
      RuntimeConstructorInfo runtimeConstructorInfo;
      if ((ConstructorInfo) (runtimeConstructorInfo = mi as RuntimeConstructorInfo) != (ConstructorInfo) null)
        return runtimeConstructorInfo.RemotingCache;
      throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
    }

    internal static RemotingTypeCachedData GetReflectionCachedData(RuntimeType type)
    {
      return type.RemotingCache;
    }

    internal static RemotingCachedData GetReflectionCachedData(MemberInfo mi)
    {
      MethodBase mi1;
      if ((mi1 = mi as MethodBase) != (MethodBase) null)
        return (RemotingCachedData) InternalRemotingServices.GetReflectionCachedData(mi1);
      RuntimeType type;
      if ((type = mi as RuntimeType) != (RuntimeType) null)
        return (RemotingCachedData) InternalRemotingServices.GetReflectionCachedData(type);
      RuntimeFieldInfo runtimeFieldInfo;
      if ((FieldInfo) (runtimeFieldInfo = mi as RuntimeFieldInfo) != (FieldInfo) null)
        return (RemotingCachedData) runtimeFieldInfo.RemotingCache;
      SerializationFieldInfo serializationFieldInfo;
      if ((FieldInfo) (serializationFieldInfo = mi as SerializationFieldInfo) != (FieldInfo) null)
        return (RemotingCachedData) serializationFieldInfo.RemotingCache;
      throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
    }

    internal static RemotingCachedData GetReflectionCachedData(RuntimeParameterInfo reflectionObject)
    {
      return (RemotingCachedData) reflectionObject.RemotingCache;
    }

    /// <summary>获取指定类成员或方法参数与 SOAP 有关的相应特性。</summary>
    /// <returns>指定的类成员或方法参数的与 SOAP 相关的属性。</returns>
    /// <param name="reflectionObject">类成员或方法参数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
    {
      MemberInfo mi = reflectionObject as MemberInfo;
      RuntimeParameterInfo reflectionObject1 = reflectionObject as RuntimeParameterInfo;
      if (mi != (MemberInfo) null)
        return InternalRemotingServices.GetReflectionCachedData(mi).GetSoapAttribute();
      if (reflectionObject1 != null)
        return InternalRemotingServices.GetReflectionCachedData(reflectionObject1).GetSoapAttribute();
      return (SoapAttribute) null;
    }
  }
}
