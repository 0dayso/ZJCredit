// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.ProxyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Proxies
{
  /// <summary>指示对象类型需要自定义代理。</summary>
  [SecurityCritical]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ProxyAttribute : Attribute, IContextAttribute
  {
    /// <summary>或者创建未初始化的 <see cref="T:System.MarshalByRefObject" />，或者创建透明代理，具体取决于指定类型是否可以存在于当前上下文中。</summary>
    /// <returns>未初始化的 <see cref="T:System.MarshalByRefObject" /> 或透明代理。</returns>
    /// <param name="serverType">要创建其实例的对象类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual MarshalByRefObject CreateInstance(Type serverType)
    {
      if (serverType == (Type) null)
        throw new ArgumentNullException("serverType");
      RuntimeType serverType1 = serverType as RuntimeType;
      if (serverType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      if (!serverType.IsContextful)
        throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
      if (serverType.IsAbstract)
        throw new RemotingException(Environment.GetResourceString("Acc_CreateAbst"));
      return this.CreateInstanceInternal(serverType1);
    }

    internal MarshalByRefObject CreateInstanceInternal(RuntimeType serverType)
    {
      return ActivationServices.CreateInstance(serverType);
    }

    /// <summary>创建由指定的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 描述并位于服务器上的远程对象的远程处理代理的实例。</summary>
    /// <returns>在指定的 <see cref="T:System.Runtime.Remoting.ObjRef" /> 中说明的远程对象的远程处理代理的新实例。</returns>
    /// <param name="objRef">对要为其创建代理的远程对象的对象引用。</param>
    /// <param name="serverType">远程对象所在的服务器的类型。</param>
    /// <param name="serverObject">服务器对象。</param>
    /// <param name="serverContext">服务器对象所在的上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
    {
      RemotingProxy remotingProxy = new RemotingProxy(serverType);
      if (serverContext != null)
        RealProxy.SetStubData((RealProxy) remotingProxy, (object) serverContext.InternalContextID);
      if (objRef != null && objRef.GetServerIdentity().IsAllocated)
        remotingProxy.SetSrvInfo(objRef.GetServerIdentity(), objRef.GetDomainID());
      remotingProxy.Initialized = true;
      Type type = serverType;
      if (!type.IsContextful && !type.IsMarshalByRef && serverContext != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
      return (RealProxy) remotingProxy;
    }

    /// <summary>检查指定的上下文。</summary>
    /// <returns>指定的上下文。</returns>
    /// <param name="ctx">要验证的上下文。</param>
    /// <param name="msg">远程调用消息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      return true;
    }

    /// <summary>获取新上下文的属性。</summary>
    /// <param name="msg">要对其检索上下文的消息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public void GetPropertiesForNewContext(IConstructionCallMessage msg)
    {
    }
  }
}
