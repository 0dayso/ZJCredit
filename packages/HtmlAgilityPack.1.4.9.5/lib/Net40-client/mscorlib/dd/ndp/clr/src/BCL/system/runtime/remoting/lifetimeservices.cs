// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.LifetimeServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>控制 .NET 远程处理生存期服务。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class LifetimeServices
  {
    private static bool s_isLeaseTime = false;
    private static bool s_isRenewOnCallTime = false;
    private static bool s_isSponsorshipTimeout = false;
    private static long s_leaseTimeTicks = TimeSpan.FromMinutes(5.0).Ticks;
    private static long s_renewOnCallTimeTicks = TimeSpan.FromMinutes(2.0).Ticks;
    private static long s_sponsorshipTimeoutTicks = TimeSpan.FromMinutes(2.0).Ticks;
    private static long s_pollTimeTicks = TimeSpan.FromMilliseconds(10000.0).Ticks;
    private static object s_LifetimeSyncObject = (object) null;

    private static object LifetimeSyncObject
    {
      get
      {
        if (LifetimeServices.s_LifetimeSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref LifetimeServices.s_LifetimeSyncObject, obj, (object) null);
        }
        return LifetimeServices.s_LifetimeSyncObject;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.AppDomain" /> 的初始租约时间范围。</summary>
    /// <returns>可以在 <see cref="T:System.AppDomain" /> 中拥有租约的对象的初始租约 <see cref="T:System.TimeSpan" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。仅在设置该属性值时才会引发此异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    public static TimeSpan LeaseTime
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_leaseTimeTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          if (LifetimeServices.s_isLeaseTime)
            throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", (object) "LeaseTime"));
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_leaseTimeTicks, value);
          LifetimeServices.s_isLeaseTime = true;
        }
      }
    }

    /// <summary>获取或设置每当调用到达服务器对象时延续租约的时间。</summary>
    /// <returns>每次调用后延续当前 <see cref="T:System.AppDomain" /> 中的生存期租约的 <see cref="T:System.TimeSpan" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。仅在设置该属性值时才会引发此异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    public static TimeSpan RenewOnCallTime
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          if (LifetimeServices.s_isRenewOnCallTime)
            throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", (object) "RenewOnCallTime"));
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks, value);
          LifetimeServices.s_isRenewOnCallTime = true;
        }
      }
    }

    /// <summary>获取或设置租约管理器等待主办方返回租约续订时间的时间。</summary>
    /// <returns>初始主办关系超时。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。仅在设置该属性值时才会引发此异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    public static TimeSpan SponsorshipTimeout
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          if (LifetimeServices.s_isSponsorshipTimeout)
            throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", (object) "SponsorshipTimeout"));
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks, value);
          LifetimeServices.s_isSponsorshipTimeout = true;
        }
      }
    }

    /// <summary>获取或设置每次激活租约管理器以清除到期租约之间的时间间隔。</summary>
    /// <returns>租约管理器检查到期租约后休眠的默认时间。</returns>
    /// <exception cref="T:System.Security.SecurityException">在调用堆栈上部，至少有一个调用方没有配置远程处理类型和通道的权限。仅在设置该属性值时才会引发此异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
    /// </PermissionSet>
    public static TimeSpan LeaseManagerPollTime
    {
      get
      {
        return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_pollTimeTicks);
      }
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        lock (LifetimeServices.LifetimeSyncObject)
        {
          LifetimeServices.SetTimeSpan(ref LifetimeServices.s_pollTimeTicks, value);
          if (!LeaseManager.IsInitialized())
            return;
          LeaseManager.GetLeaseManager().ChangePollTime(value);
        }
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.Remoting.Lifetime.LifetimeServices" /> 的实例。</summary>
    [Obsolete("Do not create instances of the LifetimeServices class.  Call the static methods directly on this type instead", true)]
    public LifetimeServices()
    {
    }

    private static TimeSpan GetTimeSpan(ref long ticks)
    {
      return TimeSpan.FromTicks(Volatile.Read(ref ticks));
    }

    private static void SetTimeSpan(ref long ticks, TimeSpan value)
    {
      Volatile.Write(ref ticks, value.Ticks);
    }

    [SecurityCritical]
    internal static ILease GetLeaseInitial(MarshalByRefObject obj)
    {
      return LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime).GetLease(obj) ?? LifetimeServices.CreateLease(obj);
    }

    [SecurityCritical]
    internal static ILease GetLease(MarshalByRefObject obj)
    {
      return LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime).GetLease(obj);
    }

    [SecurityCritical]
    internal static ILease CreateLease(MarshalByRefObject obj)
    {
      return LifetimeServices.CreateLease(LifetimeServices.LeaseTime, LifetimeServices.RenewOnCallTime, LifetimeServices.SponsorshipTimeout, obj);
    }

    [SecurityCritical]
    internal static ILease CreateLease(TimeSpan leaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject obj)
    {
      LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
      return (ILease) new Lease(leaseTime, renewOnCallTime, sponsorshipTimeout, obj);
    }
  }
}
