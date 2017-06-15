// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.RegistryPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.RegistryPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_read;
    private string m_write;
    private string m_create;
    private string m_viewAcl;
    private string m_changeAcl;

    /// <summary>获取或设置指定注册表项的读访问权限。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供读取访问权限。</returns>
    public string Read
    {
      get
      {
        return this.m_read;
      }
      set
      {
        this.m_read = value;
      }
    }

    /// <summary>获取或设置指定注册表项的写访问权限。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供写访问权限。</returns>
    public string Write
    {
      get
      {
        return this.m_write;
      }
      set
      {
        this.m_write = value;
      }
    }

    /// <summary>获取或设置指定注册表项的创建级别访问权限。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供创建级别访问权限。</returns>
    public string Create
    {
      get
      {
        return this.m_create;
      }
      set
      {
        this.m_create = value;
      }
    }

    /// <summary>获取或设置指定注册表项的查看访问控制。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供查看访问控制。</returns>
    public string ViewAccessControl
    {
      get
      {
        return this.m_viewAcl;
      }
      set
      {
        this.m_viewAcl = value;
      }
    }

    /// <summary>获取或设置指定注册表项的更改访问控制。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供更改访问控制。.</returns>
    public string ChangeAccessControl
    {
      get
      {
        return this.m_changeAcl;
      }
      set
      {
        this.m_changeAcl = value;
      }
    }

    /// <summary>获取或设置一组可以查看和修改的指定注册表项。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供创建、读取和写访问权限。</returns>
    /// <exception cref="T:System.NotSupportedException">调用 get 访问器；提供它仅为了与 C# 编译器兼容。</exception>
    public string ViewAndModify
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_create = value;
      }
    }

    /// <summary>获取或设置指定注册表项的完全访问权限。</summary>
    /// <returns>一个由分号分隔的注册表项路径列表，用于提供完全访问权限。</returns>
    /// <exception cref="T:System.NotSupportedException">调用 get 访问器；提供它仅为了与 C# 编译器兼容。</exception>
    [Obsolete("Please use the ViewAndModify property instead.")]
    public string All
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_create = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.RegistryPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="action" /> 参数不是有效的 <see cref="T:System.Security.Permissions.SecurityAction" />。</exception>
    public RegistryPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.RegistryPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.RegistryPermission" />。</returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new RegistryPermission(PermissionState.Unrestricted);
      RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
      if (this.m_read != null)
        registryPermission.SetPathList(RegistryPermissionAccess.Read, this.m_read);
      if (this.m_write != null)
        registryPermission.SetPathList(RegistryPermissionAccess.Write, this.m_write);
      if (this.m_create != null)
        registryPermission.SetPathList(RegistryPermissionAccess.Create, this.m_create);
      if (this.m_viewAcl != null)
        registryPermission.SetPathList(AccessControlActions.View, this.m_viewAcl);
      if (this.m_changeAcl != null)
        registryPermission.SetPathList(AccessControlActions.Change, this.m_changeAcl);
      return (IPermission) registryPermission;
    }
  }
}
