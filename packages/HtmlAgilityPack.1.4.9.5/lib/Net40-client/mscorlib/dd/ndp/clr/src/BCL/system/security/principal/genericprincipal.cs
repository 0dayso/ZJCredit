// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.GenericPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
  /// <summary>表示一般主体。</summary>
  [ComVisible(true)]
  [Serializable]
  public class GenericPrincipal : ClaimsPrincipal
  {
    private IIdentity m_identity;
    private string[] m_roles;

    /// <summary>获取当前 <see cref="T:System.Security.Principal.GenericPrincipal" /> 表示的用户的 <see cref="T:System.Security.Principal.GenericIdentity" />。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Principal.GenericPrincipal" /> 表示的用户的 <see cref="T:System.Security.Principal.GenericIdentity" />。</returns>
    public override IIdentity Identity
    {
      get
      {
        return this.m_identity;
      }
    }

    /// <summary>从用户标识和角色名称数组（标识表示的用户属于该数组）初始化 <see cref="T:System.Security.Principal.GenericPrincipal" /> 类的新实例。</summary>
    /// <param name="identity">表示任何用户的 <see cref="T:System.Security.Principal.IIdentity" /> 的基实现。</param>
    /// <param name="roles">
    /// <paramref name="identity" /> 参数表示的用户所属的角色名称数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    public GenericPrincipal(IIdentity identity, string[] roles)
    {
      if (identity == null)
        throw new ArgumentNullException("identity");
      this.m_identity = identity;
      if (roles != null)
      {
        this.m_roles = new string[roles.Length];
        for (int index = 0; index < roles.Length; ++index)
          this.m_roles[index] = roles[index];
      }
      else
        this.m_roles = (string[]) null;
      this.AddIdentityWithRoles(this.m_identity, this.m_roles);
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      ClaimsIdentity subject = (ClaimsIdentity) null;
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          subject = identity;
          break;
        }
      }
      if (this.m_roles != null && this.m_roles.Length != 0 && subject != null)
      {
        subject.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", this.m_roles, subject).Claims);
      }
      else
      {
        if (subject != null)
          return;
        this.AddIdentityWithRoles(this.m_identity, this.m_roles);
      }
    }

    [SecuritySafeCritical]
    private void AddIdentityWithRoles(IIdentity identity, string[] roles)
    {
      ClaimsIdentity claimsIdentity1 = identity as ClaimsIdentity;
      ClaimsIdentity claimsIdentity2 = claimsIdentity1 == null ? new ClaimsIdentity(identity) : claimsIdentity1.Clone();
      if (roles != null && roles.Length != 0)
        claimsIdentity2.ExternalClaims.Add(new RoleClaimProvider("LOCAL AUTHORITY", roles, claimsIdentity2).Claims);
      this.AddIdentity(claimsIdentity2);
    }

    /// <summary>确定当前 <see cref="T:System.Security.Principal.GenericPrincipal" /> 是否属于指定的角色。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.Principal.GenericPrincipal" /> 是指定角色的成员，则为 true；否则为 false。</returns>
    /// <param name="role">要检查其成员资格的角色的名称。</param>
    public override bool IsInRole(string role)
    {
      if (role == null || this.m_roles == null)
        return false;
      for (int index = 0; index < this.m_roles.Length; ++index)
      {
        if (this.m_roles[index] != null && string.Compare(this.m_roles[index], role, StringComparison.OrdinalIgnoreCase) == 0)
          return true;
      }
      return base.IsInRole(role);
    }
  }
}
