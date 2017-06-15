// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>控制对系统和用户环境变量的访问。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private StringExpressionSet m_read;
    private StringExpressionSet m_write;
    private bool m_unrestricted;

    /// <summary>用指定的受限制或无限制的权限初始化 <see cref="T:System.Security.Permissions.EnvironmentPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public EnvironmentPermission(PermissionState state)
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

    /// <summary>用对指定环境变量的指定访问权限初始化 <see cref="T:System.Security.Permissions.EnvironmentPermission" /> 类的新实例。</summary>
    /// <param name="flag">
    /// <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值之一。</param>
    /// <param name="pathList">授予对其的访问权限的环境变量列表（以分号分隔）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pathList" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值。</exception>
    public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
    {
      this.SetPathList(flag, pathList);
    }

    /// <summary>将对指定环境变量的指定访问权限设置为该权限的现有状态。</summary>
    /// <param name="flag">
    /// <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值之一。</param>
    /// <param name="pathList">环境变量的列表（用分号分隔）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pathList" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值。</exception>
    public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
    {
      this.VerifyFlag(flag);
      this.m_unrestricted = false;
      if ((flag & EnvironmentPermissionAccess.Read) != EnvironmentPermissionAccess.NoAccess)
        this.m_read = (StringExpressionSet) null;
      if ((flag & EnvironmentPermissionAccess.Write) != EnvironmentPermissionAccess.NoAccess)
        this.m_write = (StringExpressionSet) null;
      this.AddPathList(flag, pathList);
    }

    /// <summary>将指定环境变量的访问权限添加到该权限的现有状态。</summary>
    /// <param name="flag">
    /// <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值之一。</param>
    /// <param name="pathList">环境变量的列表（用分号分隔）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pathList" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值。</exception>
    [SecuritySafeCritical]
    public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
    {
      this.VerifyFlag(flag);
      if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
      {
        if (this.m_read == null)
          this.m_read = (StringExpressionSet) new EnvironmentStringExpressionSet();
        this.m_read.AddExpressions(pathList);
      }
      if (!this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
        return;
      if (this.m_write == null)
        this.m_write = (StringExpressionSet) new EnvironmentStringExpressionSet();
      this.m_write.AddExpressions(pathList);
    }

    /// <summary>获取所有具有指定 <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 的环境变量。</summary>
    /// <returns>选定标志的环境变量列表（以分号分隔）。</returns>
    /// <param name="flag">代表单一的环境变量访问类型的 <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 不是有效的 <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> 值。- 或 -<paramref name="flag" /> 为 <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.AllAccess" />（表示多种类型的环境变量访问）或 <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.NoAccess" />（不表示任何类型的环境变量访问）。</exception>
    public string GetPathList(EnvironmentPermissionAccess flag)
    {
      this.VerifyFlag(flag);
      this.ExclusiveFlag(flag);
      if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
      {
        if (this.m_read == null)
          return "";
        return this.m_read.ToString();
      }
      if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Write) && this.m_write != null)
        return this.m_write.ToString();
      return "";
    }

    private void VerifyFlag(EnvironmentPermissionAccess flag)
    {
      if ((flag & ~EnvironmentPermissionAccess.AllAccess) != EnvironmentPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flag));
    }

    private void ExclusiveFlag(EnvironmentPermissionAccess flag)
    {
      if (flag == EnvironmentPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
      if ((flag & flag - 1) != EnvironmentPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
    }

    private bool FlagIsSet(EnvironmentPermissionAccess flag, EnvironmentPermissionAccess question)
    {
      return (uint) (flag & question) > 0U;
    }

    private bool IsEmpty()
    {
      if (this.m_unrestricted || this.m_read != null && !this.m_read.IsEmpty())
        return false;
      if (this.m_write != null)
        return this.m_write.IsEmpty();
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
      try
      {
        EnvironmentPermission environmentPermission = (EnvironmentPermission) target;
        if (environmentPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        return (this.m_read == null || this.m_read.IsSubsetOf(environmentPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(environmentPermission.m_write));
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
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
      EnvironmentPermission environmentPermission = (EnvironmentPermission) target;
      if (environmentPermission.IsUnrestricted())
        return this.Copy();
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? (StringExpressionSet) null : this.m_read.Intersect(environmentPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? (StringExpressionSet) null : this.m_write.Intersect(environmentPermission.m_write);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new EnvironmentPermission(PermissionState.None) { m_unrestricted = false, m_read = stringExpressionSet1, m_write = stringExpressionSet2 };
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
      EnvironmentPermission environmentPermission = (EnvironmentPermission) other;
      if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
        return (IPermission) new EnvironmentPermission(PermissionState.Unrestricted);
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? environmentPermission.m_read : this.m_read.Union(environmentPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? environmentPermission.m_write : this.m_write.Union(environmentPermission.m_write);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new EnvironmentPermission(PermissionState.None) { m_unrestricted = false, m_read = stringExpressionSet1, m_write = stringExpressionSet2 };
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
      if (this.m_unrestricted)
      {
        environmentPermission.m_unrestricted = true;
      }
      else
      {
        environmentPermission.m_unrestricted = false;
        if (this.m_read != null)
          environmentPermission.m_read = this.m_read.Copy();
        if (this.m_write != null)
          environmentPermission.m_write = this.m_write.Copy();
      }
      return (IPermission) environmentPermission;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.EnvironmentPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_read != null && !this.m_read.IsEmpty())
          permissionElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
        if (this.m_write != null && !this.m_write.IsEmpty())
          permissionElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
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
        string str1 = esd.Attribute("Read");
        if (str1 != null)
          this.m_read = (StringExpressionSet) new EnvironmentStringExpressionSet(str1);
        string str2 = esd.Attribute("Write");
        if (str2 == null)
          return;
        this.m_write = (StringExpressionSet) new EnvironmentStringExpressionSet(str2);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return EnvironmentPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 0;
    }
  }
}
