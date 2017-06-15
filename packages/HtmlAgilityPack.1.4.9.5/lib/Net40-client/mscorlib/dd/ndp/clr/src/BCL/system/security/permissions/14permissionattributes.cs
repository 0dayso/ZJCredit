// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UrlIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_url;

    /// <summary>获取或设置调用代码的完整 URL。</summary>
    /// <returns>要与主应用程序所指定 URL 匹配的 URL。</returns>
    public string Url
    {
      get
      {
        return this.m_url;
      }
      set
      {
        this.m_url = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.UrlIdentityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public UrlIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.UrlIdentityPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.UrlIdentityPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new UrlIdentityPermission(PermissionState.Unrestricted);
      if (this.m_url == null)
        return (IPermission) new UrlIdentityPermission(PermissionState.None);
      return (IPermission) new UrlIdentityPermission(this.m_url);
    }
  }
}
