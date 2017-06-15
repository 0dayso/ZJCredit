// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ReflectionPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>通过 <see cref="N:System.Reflection" /> API 控制对非公共类型和成员的访问。控制 <see cref="N:System.Reflection.Emit" /> API 的一些功能。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    internal const ReflectionPermissionFlag AllFlagsAndMore = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
    private ReflectionPermissionFlag m_flags;

    /// <summary>获取或设置允许用于当前权限的反射类型。</summary>
    /// <returns>当前权限的设置标志。</returns>
    /// <exception cref="T:System.ArgumentException">试图将此属性设置为无效值。要查阅有效值，请参见 <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />。</exception>
    public ReflectionPermissionFlag Flags
    {
      get
      {
        return this.m_flags;
      }
      set
      {
        this.VerifyAccess(value);
        this.m_flags = value;
      }
    }

    /// <summary>用指定的完全受限制或不受限制的权限初始化 <see cref="T:System.Security.Permissions.ReflectionPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public ReflectionPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.SetUnrestricted(true);
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.SetUnrestricted(false);
      }
    }

    /// <summary>用指定的访问权限初始化 <see cref="T:System.Security.Permissions.ReflectionPermission" /> 类的新实例。</summary>
    /// <param name="flag">
    /// <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> 值。</exception>
    public ReflectionPermission(ReflectionPermissionFlag flag)
    {
      this.VerifyAccess(flag);
      this.SetUnrestricted(false);
      this.m_flags = flag;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (unrestricted)
        this.m_flags = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
      else
        this.Reset();
    }

    private void Reset()
    {
      this.m_flags = ReflectionPermissionFlag.NoFlags;
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.m_flags == (ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess);
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="other">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="other" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      if (!this.VerifyType(other))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      ReflectionPermission reflectionPermission = (ReflectionPermission) other;
      if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
        return (IPermission) new ReflectionPermission(PermissionState.Unrestricted);
      return (IPermission) new ReflectionPermission(this.m_flags | reflectionPermission.m_flags);
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.m_flags == ReflectionPermissionFlag.NoFlags;
      try
      {
        ReflectionPermission reflectionPermission = (ReflectionPermission) target;
        if (reflectionPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        return (this.m_flags & ~reflectionPermission.m_flags) == ReflectionPermissionFlag.NoFlags;
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
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      ReflectionPermissionFlag flag = ((ReflectionPermission) target).m_flags & this.m_flags;
      if (flag == ReflectionPermissionFlag.NoFlags)
        return (IPermission) null;
      return (IPermission) new ReflectionPermission(flag);
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      if (this.IsUnrestricted())
        return (IPermission) new ReflectionPermission(PermissionState.Unrestricted);
      return (IPermission) new ReflectionPermission(this.m_flags);
    }

    private void VerifyAccess(ReflectionPermissionFlag type)
    {
      if ((type & ~(ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess)) != ReflectionPermissionFlag.NoFlags)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) type));
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.ReflectionPermission");
      if (!this.IsUnrestricted())
        permissionElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof (ReflectionPermissionFlag), (object) this.m_flags));
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
        this.m_flags = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
      }
      else
      {
        this.Reset();
        this.SetUnrestricted(false);
        string str = esd.Attribute("Flags");
        if (str == null)
          return;
        this.m_flags = (ReflectionPermissionFlag) Enum.Parse(typeof (ReflectionPermissionFlag), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return ReflectionPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 4;
    }
  }
}
