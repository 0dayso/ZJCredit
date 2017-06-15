// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SiteIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_site;

    /// <summary>获取或设置调用代码的站点名称。</summary>
    /// <returns>要与安全提供程序所指定站点名称相比较的站点名称。</returns>
    public string Site
    {
      get
      {
        return this.m_site;
      }
      set
      {
        this.m_site = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.SiteIdentityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public SiteIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> 实例。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.SiteIdentityPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new SiteIdentityPermission(PermissionState.Unrestricted);
      if (this.m_site == null)
        return (IPermission) new SiteIdentityPermission(PermissionState.None);
      return (IPermission) new SiteIdentityPermission(this.m_site);
    }
  }
}
