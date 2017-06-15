// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PrincipalPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Principal;
using System.Security.Util;
using System.Threading;

namespace System.Security.Permissions
{
  /// <summary>允许使用为声明和强制安全性操作定义的语言结构来检查活动用户（请参见 <see cref="T:System.Security.Principal.IPrincipal" />）。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission, IBuiltInPermission
  {
    private IDRole[] m_array;

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" />。</exception>
    public PrincipalPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_array = new IDRole[1];
        this.m_array[0] = new IDRole();
        this.m_array[0].m_authenticated = true;
        this.m_array[0].m_id = (string) null;
        this.m_array[0].m_role = (string) null;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_array = new IDRole[1];
        this.m_array[0] = new IDRole();
        this.m_array[0].m_authenticated = false;
        this.m_array[0].m_id = "";
        this.m_array[0].m_role = "";
      }
    }

    /// <summary>初始化指定的 <paramref name="name" /> 和 <paramref name="role" /> 的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 类的新实例。</summary>
    /// <param name="name">
    /// <see cref="T:System.Security.Principal.IPrincipal" /> 对象的用户名。</param>
    /// <param name="role">
    /// <see cref="T:System.Security.Principal.IPrincipal" /> 对象的用户角色（例如“管理员”）。</param>
    public PrincipalPermission(string name, string role)
    {
      this.m_array = new IDRole[1];
      this.m_array[0] = new IDRole();
      this.m_array[0].m_authenticated = true;
      this.m_array[0].m_id = name;
      this.m_array[0].m_role = role;
    }

    /// <summary>初始化指定 <paramref name="name" />、<paramref name="role" /> 和身份验证状态的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 类的新实例。</summary>
    /// <param name="name">
    /// <see cref="T:System.Security.Principal.IPrincipal" /> 对象的用户名。</param>
    /// <param name="role">
    /// <see cref="T:System.Security.Principal.IPrincipal" /> 对象的用户角色（例如“管理员”）。</param>
    /// <param name="isAuthenticated">true 表示用户身份已验证，否则为 false。</param>
    public PrincipalPermission(string name, string role, bool isAuthenticated)
    {
      this.m_array = new IDRole[1];
      this.m_array[0] = new IDRole();
      this.m_array[0].m_authenticated = isAuthenticated;
      this.m_array[0].m_id = name;
      this.m_array[0].m_role = role;
    }

    private PrincipalPermission(IDRole[] array)
    {
      this.m_array = array;
    }

    private bool IsEmpty()
    {
      for (int index = 0; index < this.m_array.Length; ++index)
      {
        if (this.m_array[index].m_id == null || !this.m_array[index].m_id.Equals("") || (this.m_array[index].m_role == null || !this.m_array[index].m_role.Equals("")) || this.m_array[index].m_authenticated)
          return false;
      }
      return true;
    }

    private bool VerifyType(IPermission perm)
    {
      return perm != null && !(perm.GetType() != this.GetType());
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      for (int index = 0; index < this.m_array.Length; ++index)
      {
        if (this.m_array[index].m_id != null || this.m_array[index].m_role != null || !this.m_array[index].m_authenticated)
          return false;
      }
      return true;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数是与当前权限属于不同类型的对象。</exception>
    public bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      try
      {
        PrincipalPermission principalPermission = (PrincipalPermission) target;
        if (principalPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        for (int index1 = 0; index1 < this.m_array.Length; ++index1)
        {
          bool flag = false;
          for (int index2 = 0; index2 < principalPermission.m_array.Length; ++index2)
          {
            if (principalPermission.m_array[index2].m_authenticated == this.m_array[index1].m_authenticated && (principalPermission.m_array[index2].m_id == null || this.m_array[index1].m_id != null && this.m_array[index1].m_id.Equals(principalPermission.m_array[index2].m_id)) && (principalPermission.m_array[index2].m_role == null || this.m_array[index1].m_role != null && this.m_array[index1].m_role.Equals(principalPermission.m_array[index2].m_role)))
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            return false;
        }
        return true;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。交集为空时，新权限将为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不为 null，并且不是与当前权限属于相同类的实例。</exception>
    public IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted())
        return target.Copy();
      PrincipalPermission principalPermission = (PrincipalPermission) target;
      if (principalPermission.IsUnrestricted())
        return this.Copy();
      List<IDRole> idRoleList = (List<IDRole>) null;
      for (int index1 = 0; index1 < this.m_array.Length; ++index1)
      {
        for (int index2 = 0; index2 < principalPermission.m_array.Length; ++index2)
        {
          if (principalPermission.m_array[index2].m_authenticated == this.m_array[index1].m_authenticated)
          {
            if (principalPermission.m_array[index2].m_id == null || this.m_array[index1].m_id == null || this.m_array[index1].m_id.Equals(principalPermission.m_array[index2].m_id))
            {
              if (idRoleList == null)
                idRoleList = new List<IDRole>();
              idRoleList.Add(new IDRole()
              {
                m_id = principalPermission.m_array[index2].m_id == null ? this.m_array[index1].m_id : principalPermission.m_array[index2].m_id,
                m_role = principalPermission.m_array[index2].m_role == null || this.m_array[index1].m_role == null || this.m_array[index1].m_role.Equals(principalPermission.m_array[index2].m_role) ? (principalPermission.m_array[index2].m_role == null ? this.m_array[index1].m_role : principalPermission.m_array[index2].m_role) : "",
                m_authenticated = principalPermission.m_array[index2].m_authenticated
              });
            }
            else if (principalPermission.m_array[index2].m_role == null || this.m_array[index1].m_role == null || this.m_array[index1].m_role.Equals(principalPermission.m_array[index2].m_role))
            {
              if (idRoleList == null)
                idRoleList = new List<IDRole>();
              idRoleList.Add(new IDRole()
              {
                m_id = "",
                m_role = principalPermission.m_array[index2].m_role == null ? this.m_array[index1].m_role : principalPermission.m_array[index2].m_role,
                m_authenticated = principalPermission.m_array[index2].m_authenticated
              });
            }
          }
        }
      }
      if (idRoleList == null)
        return (IPermission) null;
      IDRole[] array = new IDRole[idRoleList.Count];
      IEnumerator enumerator = (IEnumerator) idRoleList.GetEnumerator();
      int num = 0;
      while (enumerator.MoveNext())
        array[num++] = (IDRole) enumerator.Current;
      return (IPermission) new PrincipalPermission(array);
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="other">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="other" /> 参数是与当前权限属于不同类型的对象。</exception>
    public IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      if (!this.VerifyType(other))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      PrincipalPermission principalPermission = (PrincipalPermission) other;
      if (this.IsUnrestricted() || principalPermission.IsUnrestricted())
        return (IPermission) new PrincipalPermission(PermissionState.Unrestricted);
      IDRole[] array = new IDRole[this.m_array.Length + principalPermission.m_array.Length];
      int index1;
      for (index1 = 0; index1 < this.m_array.Length; ++index1)
        array[index1] = this.m_array[index1];
      for (int index2 = 0; index2 < principalPermission.m_array.Length; ++index2)
        array[index1 + index2] = principalPermission.m_array[index2];
      return (IPermission) new PrincipalPermission(array);
    }

    /// <summary>确定指定的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 对象是否等于当前的 <see cref="T:System.Security.Permissions.PrincipalPermission" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 等于当前的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 进行比较的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 对象。</param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      IPermission target = obj as IPermission;
      return (obj == null || target != null) && this.IsSubsetOf(target) && (target == null || target.IsSubsetOf((IPermission) this));
    }

    /// <summary>获取适合在哈希算法和类似哈希表的数据结构中使用的 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 对象的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.Permissions.PrincipalPermission" /> 对象的哈希代码。</returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      int num = 0;
      for (int index = 0; index < this.m_array.Length; ++index)
        num += this.m_array[index].GetHashCode();
      return num;
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public IPermission Copy()
    {
      return (IPermission) new PrincipalPermission(this.m_array);
    }

    [SecurityCritical]
    private void ThrowSecurityException()
    {
      AssemblyName assemblyName = (AssemblyName) null;
      Evidence evidence = (Evidence) null;
      PermissionSet.s_fullTrust.Assert();
      try
      {
        Assembly callingAssembly = Assembly.GetCallingAssembly();
        assemblyName = callingAssembly.GetName();
        if (callingAssembly != Assembly.GetExecutingAssembly())
          evidence = callingAssembly.Evidence;
      }
      catch
      {
      }
      PermissionSet.RevertAssert();
      throw new SecurityException(Environment.GetResourceString("Security_PrincipalPermission"), assemblyName, (PermissionSet) null, (PermissionSet) null, (MethodInfo) null, SecurityAction.Demand, (object) this, (IPermission) this, evidence);
    }

    /// <summary>在运行时确定当前主体是否与当前权限所指定的主体相匹配。</summary>
    /// <exception cref="T:System.Security.SecurityException">当前用户未通过对当前权限所指定的用户进行的安全检查。- 或 -当前 <see cref="T:System.Security.Principal.IPrincipal" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void Demand()
    {
      new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
      IPrincipal currentPrincipal = Thread.CurrentPrincipal;
      if (currentPrincipal == null)
        this.ThrowSecurityException();
      if (this.m_array == null)
        return;
      int length = this.m_array.Length;
      bool flag = false;
      for (int index = 0; index < length; ++index)
      {
        if (this.m_array[index].m_authenticated)
        {
          IIdentity identity = currentPrincipal.Identity;
          if (identity.IsAuthenticated && (this.m_array[index].m_id == null || string.Compare(identity.Name, this.m_array[index].m_id, StringComparison.OrdinalIgnoreCase) == 0))
          {
            if (this.m_array[index].m_role == null)
            {
              flag = true;
            }
            else
            {
              WindowsPrincipal windowsPrincipal = currentPrincipal as WindowsPrincipal;
              flag = windowsPrincipal == null || !(this.m_array[index].Sid != (SecurityIdentifier) null) ? currentPrincipal.IsInRole(this.m_array[index].m_role) : windowsPrincipal.IsInRole(this.m_array[index].Sid);
            }
            if (flag)
              break;
          }
        }
        else
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this.ThrowSecurityException();
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public SecurityElement ToXml()
    {
      SecurityElement element = new SecurityElement("IPermission");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Permissions.PrincipalPermission");
      element.AddAttribute("version", "1");
      int length = this.m_array.Length;
      for (int index = 0; index < length; ++index)
        element.AddChild(this.m_array[index].ToXml());
      return element;
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="elem">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elem" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elem" /> 参数不是有效的权限元素。- 或 -<paramref name="elem" /> 参数的版本号无效。</exception>
    public void FromXml(SecurityElement elem)
    {
      CodeAccessPermission.ValidateElement(elem, (IPermission) this);
      if (elem.InternalChildren != null && elem.InternalChildren.Count != 0)
      {
        int count = elem.InternalChildren.Count;
        int num = 0;
        this.m_array = new IDRole[count];
        foreach (SecurityElement child in elem.Children)
        {
          IDRole idRole = new IDRole();
          idRole.FromXml(child);
          this.m_array[num++] = idRole;
        }
      }
      else
        this.m_array = new IDRole[0];
    }

    /// <summary>创建并返回表示当前权限的字符串。</summary>
    /// <returns>当前权限的一种表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return PrincipalPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 8;
    }
  }
}
