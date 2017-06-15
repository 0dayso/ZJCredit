// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PrincipalPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
  {
    private bool m_authenticated = true;
    private string m_name;
    private string m_role;

    /// <summary>获取或设置与当前用户关联的身份名称。</summary>
    /// <returns>与基于角色的基础安全提供程序提供的名称匹配的名称。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>获取或设置指定安全角色的成员条件。</summary>
    /// <returns>基于角色的基础安全提供程序中的角色名称。</returns>
    public string Role
    {
      get
      {
        return this.m_role;
      }
      set
      {
        this.m_role = value;
      }
    }

    /// <summary>获取或设置一个指示当前主题是否已经过基于角色的基础安全提供程序验证的值。</summary>
    /// <returns>如果当前用户的身份已经过验证，则为 true；否则，为 false。</returns>
    public bool Authenticated
    {
      get
      {
        return this.m_authenticated;
      }
      set
      {
        this.m_authenticated = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.PrincipalPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public PrincipalPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.PrincipalPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.PrincipalPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new PrincipalPermission(PermissionState.Unrestricted);
      return (IPermission) new PrincipalPermission(this.m_name, this.m_role, this.m_authenticated);
    }
  }
}
