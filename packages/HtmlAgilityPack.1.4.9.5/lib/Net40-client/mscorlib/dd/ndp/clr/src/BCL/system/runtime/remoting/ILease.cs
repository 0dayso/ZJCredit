// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.ILease
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>定义远程处理生存期服务所使用的生存期租约对象。</summary>
  [ComVisible(true)]
  public interface ILease
  {
    /// <summary>获取或设置对远程对象的调用续订 <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" /> 的时间。</summary>
    /// <returns>对远程对象的调用续订 <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" /> 的时间。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    TimeSpan RenewOnCallTime { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>获取或设置等待主办方返回租约续订时间的时间。</summary>
    /// <returns>等待主办方返回租约续订时间的时间。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    TimeSpan SponsorshipTimeout { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>获取或设置租约的初始时间。</summary>
    /// <returns>租约的初始时间。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    TimeSpan InitialLeaseTime { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>获取租约的剩余时间。</summary>
    /// <returns>租约的剩余时间。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    TimeSpan CurrentLeaseTime { [SecurityCritical] get; }

    /// <summary>获取租约的当前 <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" />。</summary>
    /// <returns>租约的当前 <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    LeaseState CurrentState { [SecurityCritical] get; }

    /// <summary>为该租约注册主办方，并将其续订指定的 <see cref="T:System.TimeSpan" />。</summary>
    /// <param name="obj">主办方的回调对象。</param>
    /// <param name="renewalTime">租约的续订期时间长度。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    [SecurityCritical]
    void Register(ISponsor obj, TimeSpan renewalTime);

    /// <summary>在不续订租约的前提下为该租约注册主办方。</summary>
    /// <param name="obj">主办方的回调对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    void Register(ISponsor obj);

    /// <summary>从主办方列表中移除主办方。</summary>
    /// <param name="obj">要注销的主办方。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    void Unregister(ISponsor obj);

    /// <summary>将租约续订指定的时间。</summary>
    /// <returns>该租约新的到期时间。</returns>
    /// <param name="renewalTime">租约的续订期时间长度。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方通过接口引用进行调用，它没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    TimeSpan Renew(TimeSpan renewalTime);
  }
}
