// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Permissions;

namespace System.Security.Principal
{
  /// <summary>允许代码检查 Windows 用户的 Windows 组成员身份。</summary>
  [ComVisible(true)]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, SecurityInfrastructure = true)]
  public class WindowsPrincipal : ClaimsPrincipal
  {
    private WindowsIdentity m_identity;
    private string[] m_roles;
    private Hashtable m_rolesTable;
    private bool m_rolesLoaded;

    /// <summary>获取当前用户的标识。</summary>
    /// <returns>当前主体的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象。</returns>
    public override IIdentity Identity
    {
      get
      {
        return (IIdentity) this.m_identity;
      }
    }

    /// <summary>从此主体中获取所有 Windows 用户声明。</summary>
    /// <returns>从此主体中声明的所有 Windows 用户的集合。</returns>
    public virtual IEnumerable<Claim> UserClaims
    {
      get
      {
        foreach (ClaimsIdentity identity in this.Identities)
        {
          WindowsIdentity windowsIdentity = identity as WindowsIdentity;
          if (windowsIdentity != null)
          {
            foreach (Claim userClaim in windowsIdentity.UserClaims)
              yield return userClaim;
          }
        }
      }
    }

    /// <summary>从此主体中获取所有 Windows 设备声明。</summary>
    /// <returns>从此主体中声明的所有 Windows 设备的集合。</returns>
    public virtual IEnumerable<Claim> DeviceClaims
    {
      get
      {
        foreach (ClaimsIdentity identity in this.Identities)
        {
          WindowsIdentity windowsIdentity = identity as WindowsIdentity;
          if (windowsIdentity != null)
          {
            foreach (Claim deviceClaim in windowsIdentity.DeviceClaims)
              yield return deviceClaim;
          }
        }
      }
    }

    private WindowsPrincipal()
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象初始化 <see cref="T:System.Security.Principal.WindowsPrincipal" /> 类的新实例。</summary>
    /// <param name="ntIdentity">根据其构造 <see cref="T:System.Security.Principal.WindowsPrincipal" /> 新实例的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ntIdentity" /> 为 null。</exception>
    public WindowsPrincipal(WindowsIdentity ntIdentity)
      : base((IIdentity) ntIdentity)
    {
      if (ntIdentity == null)
        throw new ArgumentNullException("ntIdentity");
      this.m_identity = ntIdentity;
    }

    [OnDeserialized]
    [SecuritySafeCritical]
    private void OnDeserializedMethod(StreamingContext context)
    {
      ClaimsIdentity claimsIdentity = (ClaimsIdentity) null;
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          claimsIdentity = identity;
          break;
        }
      }
      if (claimsIdentity != null)
        return;
      this.AddIdentity((ClaimsIdentity) this.m_identity);
    }

    /// <summary>确定当前主体是否属于具有指定名称的 Windows 用户组。</summary>
    /// <returns>如果当前主体是指定的 Windows 用户组的成员，则为 true；否则为 false。</returns>
    /// <param name="role">要对其检查成员身份的 Windows 用户组的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public override bool IsInRole(string role)
    {
      if (role == null || role.Length == 0)
        return false;
      NTAccount ntAccount = new NTAccount(role);
      IdentityReferenceCollection sourceAccounts = new IdentityReferenceCollection(1);
      sourceAccounts.Add((IdentityReference) ntAccount);
      Type targetType = typeof (SecurityIdentifier);
      int num = 0;
      SecurityIdentifier sid = NTAccount.Translate(sourceAccounts, targetType, num != 0)[0] as SecurityIdentifier;
      if (sid != (SecurityIdentifier) null && this.IsInRole(sid))
        return true;
      return base.IsInRole(role);
    }

    /// <summary>确定当前主体是否属于具有指定 <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> 的 Windows 用户组。</summary>
    /// <returns>如果当前主体是指定的 Windows 用户组的成员，则为 true；否则为 false。</returns>
    /// <param name="role">
    /// <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="role" /> 不是有效的 <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> 值。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public virtual bool IsInRole(WindowsBuiltInRole role)
    {
      if (role < WindowsBuiltInRole.Administrator || role > WindowsBuiltInRole.Replicator)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) role), "role");
      return this.IsInRole((int) role);
    }

    /// <summary>确定当前主体是否属于具有指定相对标识符 (RID) 的 Windows 用户组。</summary>
    /// <returns>如果当前主体是指定的 Windows 用户组的成员（即在特定的角色中），则为 true；否则为 false。</returns>
    /// <param name="rid">在其中检查主体的成员资格状态的 Windows 用户组的 RID。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public virtual bool IsInRole(int rid)
    {
      return this.IsInRole(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[2]{ 32, rid }));
    }

    /// <summary>确定当前主体是否属于具有指定的安全标识符 (SID) 的 Windows 用户组。</summary>
    /// <returns>如果当前主体是指定的 Windows 用户组的成员，则为 true；否则为 false。</returns>
    /// <param name="sid">唯一标识 Windows 用户组的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sid" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">Windows 返回了 Win32 错误。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public virtual bool IsInRole(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      if (this.m_identity.AccessToken.IsInvalid)
        return false;
      SafeAccessTokenHandle phNewToken = SafeAccessTokenHandle.InvalidHandle;
      if (this.m_identity.ImpersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_identity.AccessToken, 8U, IntPtr.Zero, 2U, 2U, out phNewToken))
        throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
      bool IsMember = false;
      if (!Win32Native.CheckTokenMembership(this.m_identity.ImpersonationLevel != TokenImpersonationLevel.None ? this.m_identity.AccessToken : phNewToken, sid.BinaryForm, out IsMember))
        throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
      phNewToken.Dispose();
      return IsMember;
    }
  }
}
