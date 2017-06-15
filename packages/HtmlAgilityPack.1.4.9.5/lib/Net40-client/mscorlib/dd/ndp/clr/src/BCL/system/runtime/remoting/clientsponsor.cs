// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.ClientSponsor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>为生存期主办方类提供默认实现。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ClientSponsor : MarshalByRefObject, ISponsor
  {
    private Hashtable sponsorTable = new Hashtable(10);
    private TimeSpan m_renewalTime = TimeSpan.FromMinutes(2.0);

    /// <summary>获取或设置当请求续订时被发起对象的生存期所延长的 <see cref="T:System.TimeSpan" />。</summary>
    /// <returns>当请求续订时被发起对象的生存期所延长的 <see cref="T:System.TimeSpan" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public TimeSpan RenewalTime
    {
      get
      {
        return this.m_renewalTime;
      }
      set
      {
        this.m_renewalTime = value;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 类的新实例。</summary>
    public ClientSponsor()
    {
    }

    /// <summary>用被发起对象的续订时间来初始化 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 类的新实例。</summary>
    /// <param name="renewalTime">当请求续订时被发起对象的生存期所延长的 <see cref="T:System.TimeSpan" />。</param>
    public ClientSponsor(TimeSpan renewalTime)
    {
      this.m_renewalTime = renewalTime;
    }

    /// <summary>在垃圾回收器回收当前 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 的资源之前释放它们。</summary>
    [SecuritySafeCritical]
    ~ClientSponsor()
    {
    }

    /// <summary>为主办关系注册指定的 <see cref="T:System.MarshalByRefObject" />。</summary>
    /// <returns>如果注册成功，则为 true；否则为 false。</returns>
    /// <param name="obj">为主办关系注册到 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public bool Register(MarshalByRefObject obj)
    {
      ILease lease = (ILease) obj.GetLifetimeService();
      if (lease == null)
        return false;
      lease.Register((ISponsor) this);
      lock (this.sponsorTable)
        this.sponsorTable[(object) obj] = (object) lease;
      return true;
    }

    /// <summary>从由当前 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 发起的对象列表中注销指定的 <see cref="T:System.MarshalByRefObject" />。</summary>
    /// <param name="obj">要注销的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Unregister(MarshalByRefObject obj)
    {
      ILease lease = (ILease) null;
      lock (this.sponsorTable)
        lease = (ILease) this.sponsorTable[(object) obj];
      if (lease == null)
        return;
      lease.Unregister((ISponsor) this);
    }

    /// <summary>请求发起客户端续订指定对象的租约。</summary>
    /// <returns>指定对象的附加租用时间。</returns>
    /// <param name="lease">需要续订租约的对象的生存期租约。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public TimeSpan Renewal(ILease lease)
    {
      return this.m_renewalTime;
    }

    /// <summary>清空注册到当前 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 的列表对象。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Close()
    {
      lock (this.sponsorTable)
      {
        IDictionaryEnumerator local_2 = this.sponsorTable.GetEnumerator();
        while (local_2.MoveNext())
          ((ILease) local_2.Value).Unregister((ISponsor) this);
        this.sponsorTable.Clear();
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> 的新实例，为当前对象提供租约。</summary>
    /// <returns>当前对象的 <see cref="T:System.Runtime.Remoting.Lifetime.ILease" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      return (object) null;
    }
  }
}
