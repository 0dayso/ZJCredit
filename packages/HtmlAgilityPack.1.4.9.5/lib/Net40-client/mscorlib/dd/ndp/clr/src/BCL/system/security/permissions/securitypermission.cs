// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>描述应用于代码的安全权限集。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private SecurityPermissionFlag m_flags;
    private const string _strHeaderAssertion = "Assertion";
    private const string _strHeaderUnmanagedCode = "UnmanagedCode";
    private const string _strHeaderExecution = "Execution";
    private const string _strHeaderSkipVerification = "SkipVerification";
    private const string _strHeaderControlThread = "ControlThread";
    private const string _strHeaderControlEvidence = "ControlEvidence";
    private const string _strHeaderControlPolicy = "ControlPolicy";
    private const string _strHeaderSerializationFormatter = "SerializationFormatter";
    private const string _strHeaderControlDomainPolicy = "ControlDomainPolicy";
    private const string _strHeaderControlPrincipal = "ControlPrincipal";
    private const string _strHeaderControlAppDomain = "ControlAppDomain";

    /// <summary>获取或设置安全权限标志。</summary>
    /// <returns>当前权限的状态，由 <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> 所定义的任何权限位的按位“或”组合来表示。</returns>
    /// <exception cref="T:System.ArgumentException">试图将此属性设置为无效值。要查阅有效值，请参见 <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />。</exception>
    public SecurityPermissionFlag Flags
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

    /// <summary>用指定的受限制或无限制的权限初始化 <see cref="T:System.Security.Permissions.SecurityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public SecurityPermission(PermissionState state)
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
        this.Reset();
      }
    }

    /// <summary>使用指定的标志初始设置状态初始化 <see cref="T:System.Security.Permissions.SecurityPermission" /> 类的新实例。</summary>
    /// <param name="flag">权限的初始状态，由 <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> 所定义的任何权限位的按位“或”组合来表示。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> 参数不是有效的 <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> 值。</exception>
    public SecurityPermission(SecurityPermissionFlag flag)
    {
      this.VerifyAccess(flag);
      this.SetUnrestricted(false);
      this.m_flags = flag;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (!unrestricted)
        return;
      this.m_flags = SecurityPermissionFlag.AllFlags;
    }

    private void Reset()
    {
      this.m_flags = SecurityPermissionFlag.NoFlags;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.m_flags == SecurityPermissionFlag.NoFlags;
      SecurityPermission securityPermission = target as SecurityPermission;
      if (securityPermission != null)
        return (this.m_flags & ~securityPermission.m_flags) == SecurityPermissionFlag.NoFlags;
      throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      SecurityPermission securityPermission = (SecurityPermission) target;
      if (securityPermission.IsUnrestricted() || this.IsUnrestricted())
        return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
      return (IPermission) new SecurityPermission(this.m_flags | securityPermission.m_flags);
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限对象，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      SecurityPermission securityPermission = (SecurityPermission) target;
      SecurityPermissionFlag flag;
      if (securityPermission.IsUnrestricted())
      {
        if (this.IsUnrestricted())
          return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
        flag = this.m_flags;
      }
      else
        flag = !this.IsUnrestricted() ? this.m_flags & securityPermission.m_flags : securityPermission.m_flags;
      if (flag == SecurityPermissionFlag.NoFlags)
        return (IPermission) null;
      return (IPermission) new SecurityPermission(flag);
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      if (this.IsUnrestricted())
        return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
      return (IPermission) new SecurityPermission(this.m_flags);
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.m_flags == SecurityPermissionFlag.AllFlags;
    }

    private void VerifyAccess(SecurityPermissionFlag type)
    {
      if ((type & ~SecurityPermissionFlag.AllFlags) != SecurityPermissionFlag.NoFlags)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) type));
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.SecurityPermission");
      if (!this.IsUnrestricted())
        permissionElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof (SecurityPermissionFlag), (object) this.m_flags));
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号不受支持。</exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_flags = SecurityPermissionFlag.AllFlags;
      }
      else
      {
        this.Reset();
        this.SetUnrestricted(false);
        string str = esd.Attribute("Flags");
        if (str == null)
          return;
        this.m_flags = (SecurityPermissionFlag) Enum.Parse(typeof (SecurityPermissionFlag), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return SecurityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 6;
    }
  }
}
