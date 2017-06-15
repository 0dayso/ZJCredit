// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.IConstructionCallMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>表示对象的结构调用请求。</summary>
  [ComVisible(true)]
  public interface IConstructionCallMessage : IMethodCallMessage, IMethodMessage, IMessage
  {
    /// <summary>获取或设置激活远程对象的激活器。</summary>
    /// <returns>激活远程对象的激活器。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IActivator Activator { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>获取调用站点激活特性。</summary>
    /// <returns>调用站点激活特性。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    object[] CallSiteActivationAttributes { [SecurityCritical] get; }

    /// <summary>获取要激活的远程类型的完整类型名称。</summary>
    /// <returns>要激活的远程类型的完整类型名称。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    string ActivationTypeName { [SecurityCritical] get; }

    /// <summary>获取要激活的远程对象的类型。</summary>
    /// <returns>要激活的远程对象的类型。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    Type ActivationType { [SecurityCritical] get; }

    /// <summary>获取上下文属性的列表，这些属性定义要在其中创建对象的上下文。</summary>
    /// <returns>要在其中构造对象的上下文的属性列表。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    IList ContextProperties { [SecurityCritical] get; }
  }
}
