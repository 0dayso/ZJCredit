// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.HostProtectionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许使用声明性安全操作来确定宿主保护要求。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
  {
    private HostProtectionResource m_resources;

    /// <summary>获取或设置标志，这些标志指定可能对宿主有害的功能类别。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.HostProtectionResource" /> 值的按位组合。默认值为 <see cref="F:System.Security.Permissions.HostProtectionResource.None" />。</returns>
    public HostProtectionResource Resources
    {
      get
      {
        return this.m_resources;
      }
      set
      {
        this.m_resources = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开同步。</summary>
    /// <returns>如果公开同步，则为 true；否则为 false。默认值为 false。</returns>
    public bool Synchronization
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.Synchronization) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.Synchronization : this.m_resources & ~HostProtectionResource.Synchronization;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开共享状态。</summary>
    /// <returns>如果公开共享状态，则为 true；否则为 false。默认值为 false。</returns>
    public bool SharedState
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SharedState) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SharedState : this.m_resources & ~HostProtectionResource.SharedState;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开外部进程管理。</summary>
    /// <returns>如果公开外部进程管理，则为 true，否则为 false。默认值为 false。</returns>
    public bool ExternalProcessMgmt
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.ExternalProcessMgmt) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.ExternalProcessMgmt : this.m_resources & ~HostProtectionResource.ExternalProcessMgmt;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开自影响的进程管理。</summary>
    /// <returns>如果公开自影响的进程管理，则为 true；否则为 false。默认值为 false。</returns>
    public bool SelfAffectingProcessMgmt
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SelfAffectingProcessMgmt) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SelfAffectingProcessMgmt : this.m_resources & ~HostProtectionResource.SelfAffectingProcessMgmt;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开外部线程处理。</summary>
    /// <returns>如果公开外部线程处理，则为 true；否则为 false。默认值为 false。</returns>
    public bool ExternalThreading
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.ExternalThreading) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.ExternalThreading : this.m_resources & ~HostProtectionResource.ExternalThreading;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开自影响的线程处理。</summary>
    /// <returns>如果公开自影响的线程处理，则为 true；否则为 false。默认值为 false。</returns>
    public bool SelfAffectingThreading
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SelfAffectingThreading) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SelfAffectingThreading : this.m_resources & ~HostProtectionResource.SelfAffectingThreading;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开安全基础结构。</summary>
    /// <returns>如果公开安全基础结构，则为 true；否则为 false。默认值为 false。</returns>
    [ComVisible(true)]
    public bool SecurityInfrastructure
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SecurityInfrastructure) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SecurityInfrastructure : this.m_resources & ~HostProtectionResource.SecurityInfrastructure;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否公开用户界面。</summary>
    /// <returns>如果公开用户界面，则为 true；否则为 false。默认值为 false。</returns>
    public bool UI
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.UI) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.UI : this.m_resources & ~HostProtectionResource.UI;
      }
    }

    /// <summary>获取或设置一个值，该值指示当操作被终止时资源是否可能泄漏内存。</summary>
    /// <returns>如果操作终止时资源可能泄漏内存，则为 true；否则为 false。</returns>
    public bool MayLeakOnAbort
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.MayLeakOnAbort) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.MayLeakOnAbort : this.m_resources & ~HostProtectionResource.MayLeakOnAbort;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> 类的新实例。</summary>
    public HostProtectionAttribute()
      : base(SecurityAction.LinkDemand)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 值初始化 <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="action" /> 不是 <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />。</exception>
    public HostProtectionAttribute(SecurityAction action)
      : base(action)
    {
      if (action != SecurityAction.LinkDemand)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
    }

    /// <summary>创建并返回一个新的宿主保护权限。</summary>
    /// <returns>与当前特性对应的 <see cref="T:System.Security.IPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new HostProtectionPermission(PermissionState.Unrestricted);
      return (IPermission) new HostProtectionPermission(this.m_resources);
    }
  }
}
