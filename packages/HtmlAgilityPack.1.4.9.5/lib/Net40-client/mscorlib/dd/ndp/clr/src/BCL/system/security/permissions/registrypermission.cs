// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.RegistryPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>控制访问注册表变量的能力。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private StringExpressionSet m_read;
    private StringExpressionSet m_write;
    private StringExpressionSet m_create;
    [OptionalField(VersionAdded = 2)]
    private StringExpressionSet m_viewAcl;
    [OptionalField(VersionAdded = 2)]
    private StringExpressionSet m_changeAcl;
    private bool m_unrestricted;

    /// <summary>用指定的完全受限制或不受限制的权限初始化 <see cref="T:System.Security.Permissions.RegistryPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public RegistryPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_unrestricted = true;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_unrestricted = false;
      }
    }

    /// <summary>用对指定注册表变量的指定访问权限初始化 <see cref="T:System.Security.Permissions.RegistryPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值之一。</param>
    /// <param name="pathList">授予对其的访问权限的注册表变量列表（以分号分隔）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 参数不是一个有效的字符串。</exception>
    public RegistryPermission(RegistryPermissionAccess access, string pathList)
    {
      this.SetPathList(access, pathList);
    }

    /// <summary>使用对指定注册表变量的指定访问权限和对注册表控制信息的指定访问权限，初始化 <see cref="T:System.Security.Permissions.RegistryPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值之一。</param>
    /// <param name="control">
    /// <see cref="T:System.Security.AccessControl.AccessControlActions" /> 值的按位组合。</param>
    /// <param name="pathList">授予对其的访问权限的注册表变量列表（以分号分隔）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 参数不是一个有效的字符串。</exception>
    public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
    {
      this.m_unrestricted = false;
      this.AddPathList(access, control, pathList);
    }

    /// <summary>将指定注册变量名的新访问权设置为权限的现有状态。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值之一。</param>
    /// <param name="pathList">注册表变量的列表（用分号分隔）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 参数不是一个有效的字符串。</exception>
    public void SetPathList(RegistryPermissionAccess access, string pathList)
    {
      this.VerifyAccess(access);
      this.m_unrestricted = false;
      if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
        this.m_read = (StringExpressionSet) null;
      if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
        this.m_write = (StringExpressionSet) null;
      if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
        this.m_create = (StringExpressionSet) null;
      this.AddPathList(access, pathList);
    }

    internal void SetPathList(AccessControlActions control, string pathList)
    {
      this.m_unrestricted = false;
      if ((control & AccessControlActions.View) != AccessControlActions.None)
        this.m_viewAcl = (StringExpressionSet) null;
      if ((control & AccessControlActions.Change) != AccessControlActions.None)
        this.m_changeAcl = (StringExpressionSet) null;
      this.AddPathList(RegistryPermissionAccess.NoAccess, control, pathList);
    }

    /// <summary>将指定注册表变量的访问权限添加到现有权限状态。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值之一。</param>
    /// <param name="pathList">注册表变量的列表（用分号分隔）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 参数不是一个有效的字符串。</exception>
    public void AddPathList(RegistryPermissionAccess access, string pathList)
    {
      this.AddPathList(access, AccessControlActions.None, pathList);
    }

    /// <summary>将对指定注册表变量的访问权限添加到该权限的现有状态中，指定注册表权限访问和访问控制操作。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值之一。</param>
    /// <param name="control">
    /// <see cref="T:System.Security.AccessControl.AccessControlActions" /> 值之一。</param>
    /// <param name="pathList">注册表变量的列表（用分号分隔）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 参数不是一个有效的字符串。</exception>
    [SecuritySafeCritical]
    public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
    {
      this.VerifyAccess(access);
      if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_read == null)
          this.m_read = new StringExpressionSet();
        this.m_read.AddExpressions(pathList);
      }
      if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_write == null)
          this.m_write = new StringExpressionSet();
        this.m_write.AddExpressions(pathList);
      }
      if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_create == null)
          this.m_create = new StringExpressionSet();
        this.m_create.AddExpressions(pathList);
      }
      if ((control & AccessControlActions.View) != AccessControlActions.None)
      {
        if (this.m_viewAcl == null)
          this.m_viewAcl = new StringExpressionSet();
        this.m_viewAcl.AddExpressions(pathList);
      }
      if ((control & AccessControlActions.Change) == AccessControlActions.None)
        return;
      if (this.m_changeAcl == null)
        this.m_changeAcl = new StringExpressionSet();
      this.m_changeAcl.AddExpressions(pathList);
    }

    /// <summary>获取具有指定 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 的所有注册表变量的路径。</summary>
    /// <returns>注册表变量列表（用分号分隔），带有指定的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />。</returns>
    /// <param name="access">表示单一类型的注册表变量访问的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 不是有效的 <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> 值。- 或 -<paramref name="access" /> 为 <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" />（它表示多种类型的注册表变量访问）或 <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" /> （它不表示任何类型的注册表变量访问）。</exception>
    [SecuritySafeCritical]
    public string GetPathList(RegistryPermissionAccess access)
    {
      this.VerifyAccess(access);
      this.ExclusiveAccess(access);
      if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_read == null)
          return "";
        return this.m_read.UnsafeToString();
      }
      if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_write == null)
          return "";
        return this.m_write.UnsafeToString();
      }
      if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess && this.m_create != null)
        return this.m_create.UnsafeToString();
      return "";
    }

    private void VerifyAccess(RegistryPermissionAccess access)
    {
      if ((access & ~RegistryPermissionAccess.AllAccess) != RegistryPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access));
    }

    private void ExclusiveAccess(RegistryPermissionAccess access)
    {
      if (access == RegistryPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
      if ((access & access - 1) != RegistryPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
    }

    private bool IsEmpty()
    {
      if (this.m_unrestricted || this.m_read != null && !this.m_read.IsEmpty() || (this.m_write != null && !this.m_write.IsEmpty() || this.m_create != null && !this.m_create.IsEmpty()) || this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsEmpty();
      return true;
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.m_unrestricted;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    [SecuritySafeCritical]
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      RegistryPermission registryPermission = target as RegistryPermission;
      if (registryPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (registryPermission.IsUnrestricted())
        return true;
      if (this.IsUnrestricted() || this.m_read != null && !this.m_read.IsSubsetOf(registryPermission.m_read) || (this.m_write != null && !this.m_write.IsSubsetOf(registryPermission.m_write) || this.m_create != null && !this.m_create.IsSubsetOf(registryPermission.m_create)) || this.m_viewAcl != null && !this.m_viewAcl.IsSubsetOf(registryPermission.m_viewAcl))
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsSubsetOf(registryPermission.m_changeAcl);
      return true;
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    [SecuritySafeCritical]
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted())
        return target.Copy();
      RegistryPermission registryPermission = (RegistryPermission) target;
      if (registryPermission.IsUnrestricted())
        return this.Copy();
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? (StringExpressionSet) null : this.m_read.Intersect(registryPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? (StringExpressionSet) null : this.m_write.Intersect(registryPermission.m_write);
      StringExpressionSet stringExpressionSet3 = this.m_create == null ? (StringExpressionSet) null : this.m_create.Intersect(registryPermission.m_create);
      StringExpressionSet stringExpressionSet4 = this.m_viewAcl == null ? (StringExpressionSet) null : this.m_viewAcl.Intersect(registryPermission.m_viewAcl);
      StringExpressionSet stringExpressionSet5 = this.m_changeAcl == null ? (StringExpressionSet) null : this.m_changeAcl.Intersect(registryPermission.m_changeAcl);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && ((stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty())) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new RegistryPermission(PermissionState.None) { m_unrestricted = false, m_read = stringExpressionSet1, m_write = stringExpressionSet2, m_create = stringExpressionSet3, m_viewAcl = stringExpressionSet4, m_changeAcl = stringExpressionSet5 };
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="other">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="other" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    [SecuritySafeCritical]
    public override IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      if (!this.VerifyType(other))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      RegistryPermission registryPermission = (RegistryPermission) other;
      if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
        return (IPermission) new RegistryPermission(PermissionState.Unrestricted);
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? registryPermission.m_read : this.m_read.Union(registryPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? registryPermission.m_write : this.m_write.Union(registryPermission.m_write);
      StringExpressionSet stringExpressionSet3 = this.m_create == null ? registryPermission.m_create : this.m_create.Union(registryPermission.m_create);
      StringExpressionSet stringExpressionSet4 = this.m_viewAcl == null ? registryPermission.m_viewAcl : this.m_viewAcl.Union(registryPermission.m_viewAcl);
      StringExpressionSet stringExpressionSet5 = this.m_changeAcl == null ? registryPermission.m_changeAcl : this.m_changeAcl.Union(registryPermission.m_changeAcl);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && ((stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty())) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new RegistryPermission(PermissionState.None) { m_unrestricted = false, m_read = stringExpressionSet1, m_write = stringExpressionSet2, m_create = stringExpressionSet3, m_viewAcl = stringExpressionSet4, m_changeAcl = stringExpressionSet5 };
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
      if (this.m_unrestricted)
      {
        registryPermission.m_unrestricted = true;
      }
      else
      {
        registryPermission.m_unrestricted = false;
        if (this.m_read != null)
          registryPermission.m_read = this.m_read.Copy();
        if (this.m_write != null)
          registryPermission.m_write = this.m_write.Copy();
        if (this.m_create != null)
          registryPermission.m_create = this.m_create.Copy();
        if (this.m_viewAcl != null)
          registryPermission.m_viewAcl = this.m_viewAcl.Copy();
        if (this.m_changeAcl != null)
          registryPermission.m_changeAcl = this.m_changeAcl.Copy();
      }
      return (IPermission) registryPermission;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    [SecuritySafeCritical]
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.RegistryPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_read != null && !this.m_read.IsEmpty())
          permissionElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.UnsafeToString()));
        if (this.m_write != null && !this.m_write.IsEmpty())
          permissionElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.UnsafeToString()));
        if (this.m_create != null && !this.m_create.IsEmpty())
          permissionElement.AddAttribute("Create", SecurityElement.Escape(this.m_create.UnsafeToString()));
        if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
          permissionElement.AddAttribute("ViewAccessControl", SecurityElement.Escape(this.m_viewAcl.UnsafeToString()));
        if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
          permissionElement.AddAttribute("ChangeAccessControl", SecurityElement.Escape(this.m_changeAcl.UnsafeToString()));
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号无效。</exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_unrestricted = true;
      }
      else
      {
        this.m_unrestricted = false;
        this.m_read = (StringExpressionSet) null;
        this.m_write = (StringExpressionSet) null;
        this.m_create = (StringExpressionSet) null;
        this.m_viewAcl = (StringExpressionSet) null;
        this.m_changeAcl = (StringExpressionSet) null;
        string str1 = esd.Attribute("Read");
        if (str1 != null)
          this.m_read = new StringExpressionSet(str1);
        string str2 = esd.Attribute("Write");
        if (str2 != null)
          this.m_write = new StringExpressionSet(str2);
        string str3 = esd.Attribute("Create");
        if (str3 != null)
          this.m_create = new StringExpressionSet(str3);
        string str4 = esd.Attribute("ViewAccessControl");
        if (str4 != null)
          this.m_viewAcl = new StringExpressionSet(str4);
        string str5 = esd.Attribute("ChangeAccessControl");
        if (str5 == null)
          return;
        this.m_changeAcl = new StringExpressionSet(str5);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return RegistryPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 5;
    }
  }
}
