// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongNameIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>为强名称定义标识权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    private bool m_unrestricted;
    private StrongName2[] m_strongNames;

    /// <summary>获取或设置定义强名称标识命名空间的公钥 Blob。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> 包含标识的公钥，或 null（如果没有密钥）。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性值被设置为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">无法检索该属性值，因为它包含不明确的标识。</exception>
    public StrongNamePublicKeyBlob PublicKey
    {
      get
      {
        if (this.m_strongNames == null || this.m_strongNames.Length == 0)
          return (StrongNamePublicKeyBlob) null;
        if (this.m_strongNames.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        return this.m_strongNames[0].m_publicKeyBlob;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("PublicKey");
        this.m_unrestricted = false;
        if (this.m_strongNames != null && this.m_strongNames.Length == 1)
        {
          this.m_strongNames[0].m_publicKeyBlob = value;
        }
        else
        {
          this.m_strongNames = new StrongName2[1];
          this.m_strongNames[0] = new StrongName2(value, "", new Version());
        }
      }
    }

    /// <summary>获取或设置强名称标识的简单名称部分。</summary>
    /// <returns>标识的简单名称。</returns>
    /// <exception cref="T:System.ArgumentException">该值是空字符串 ("")。</exception>
    /// <exception cref="T:System.NotSupportedException">无法检索该属性值，因为它包含不明确的标识。</exception>
    public string Name
    {
      get
      {
        if (this.m_strongNames == null || this.m_strongNames.Length == 0)
          return "";
        if (this.m_strongNames.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        return this.m_strongNames[0].m_name;
      }
      set
      {
        if (value != null && value.Length == 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
        this.m_unrestricted = false;
        if (this.m_strongNames != null && this.m_strongNames.Length == 1)
        {
          this.m_strongNames[0].m_name = value;
        }
        else
        {
          this.m_strongNames = new StrongName2[1];
          this.m_strongNames[0] = new StrongName2((StrongNamePublicKeyBlob) null, value, new Version());
        }
      }
    }

    /// <summary>获取或设置标识的版本号。</summary>
    /// <returns>标识的版本。</returns>
    /// <exception cref="T:System.NotSupportedException">无法检索该属性值，因为它包含不明确的标识。</exception>
    public Version Version
    {
      get
      {
        if (this.m_strongNames == null || this.m_strongNames.Length == 0)
          return new Version();
        if (this.m_strongNames.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        return this.m_strongNames[0].m_version;
      }
      set
      {
        this.m_unrestricted = false;
        if (this.m_strongNames != null && this.m_strongNames.Length == 1)
        {
          this.m_strongNames[0].m_version = value;
        }
        else
        {
          this.m_strongNames = new StrongName2[1];
          this.m_strongNames[0] = new StrongName2((StrongNamePublicKeyBlob) null, "", value);
        }
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public StrongNameIdentityPermission(PermissionState state)
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

    /// <summary>为指定的强名称标识初始化 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> 类的新实例。</summary>
    /// <param name="blob">定义强名称标识命名空间的公钥。</param>
    /// <param name="name">强名称标识中的简单名称部分。这与程序集的名称对应。</param>
    /// <param name="version">标识的版本号。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="blob" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数是空字符串 ("")。</exception>
    public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
    {
      if (blob == null)
        throw new ArgumentNullException("blob");
      if (name != null && name.Equals(""))
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
      this.m_unrestricted = false;
      this.m_strongNames = new StrongName2[1];
      this.m_strongNames[0] = new StrongName2(blob, name, version);
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      StrongNameIdentityPermission identityPermission = new StrongNameIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_strongNames != null)
      {
        identityPermission.m_strongNames = new StrongName2[this.m_strongNames.Length];
        for (int index = 0; index < this.m_strongNames.Length; ++index)
          identityPermission.m_strongNames[index] = this.m_strongNames[index].Copy();
      }
      return (IPermission) identityPermission;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return !this.m_unrestricted && (this.m_strongNames == null || this.m_strongNames.Length == 0);
      StrongNameIdentityPermission identityPermission = target as StrongNameIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_strongNames != null)
      {
        foreach (StrongName2 mStrongName1 in this.m_strongNames)
        {
          bool flag = false;
          if (identityPermission.m_strongNames != null)
          {
            foreach (StrongName2 mStrongName2 in identityPermission.m_strongNames)
            {
              if (mStrongName1.IsSubsetOf(mStrongName2))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新的权限，表示当前权限与指定权限的交集，或为 null（如果交集为空）。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      StrongNameIdentityPermission identityPermission = target as StrongNameIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_strongNames == null || identityPermission.m_strongNames == null || (this.m_strongNames.Length == 0 || identityPermission.m_strongNames.Length == 0))
        return (IPermission) null;
      List<StrongName2> strongName2List = new List<StrongName2>();
      foreach (StrongName2 mStrongName1 in this.m_strongNames)
      {
        foreach (StrongName2 mStrongName2 in identityPermission.m_strongNames)
        {
          StrongName2 strongName2 = mStrongName1.Intersect(mStrongName2);
          if (strongName2 != null)
            strongName2List.Add(strongName2);
        }
      }
      if (strongName2List.Count == 0)
        return (IPermission) null;
      return (IPermission) new StrongNameIdentityPermission(PermissionState.None) { m_strongNames = strongName2List.ToArray() };
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -这两个权限不相等，而且其中一个是另一个的子集。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_strongNames == null || this.m_strongNames.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      StrongNameIdentityPermission identityPermission = target as StrongNameIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_strongNames == null || this.m_strongNames.Length == 0)
      {
        if (identityPermission.m_strongNames == null || identityPermission.m_strongNames.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_strongNames == null || identityPermission.m_strongNames.Length == 0)
        return this.Copy();
      List<StrongName2> strongName2List = new List<StrongName2>();
      foreach (StrongName2 mStrongName in this.m_strongNames)
        strongName2List.Add(mStrongName);
      foreach (StrongName2 mStrongName in identityPermission.m_strongNames)
      {
        bool flag = false;
        foreach (StrongName2 target1 in strongName2List)
        {
          if (mStrongName.Equals(target1))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          strongName2List.Add(mStrongName);
      }
      return (IPermission) new StrongNameIdentityPermission(PermissionState.None) { m_strongNames = strongName2List.ToArray() };
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="e">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 参数不是有效的权限元素。- 或 -<paramref name="e" /> 参数的版本号无效。</exception>
    public override void FromXml(SecurityElement e)
    {
      this.m_unrestricted = false;
      this.m_strongNames = (StrongName2[]) null;
      CodeAccessPermission.ValidateElement(e, (IPermission) this);
      string strA = e.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string publicKey1 = e.Attribute("PublicKeyBlob");
        string name1 = e.Attribute("Name");
        string version1 = e.Attribute("AssemblyVersion");
        List<StrongName2> strongName2List = new List<StrongName2>();
        if (publicKey1 != null || name1 != null || version1 != null)
        {
          StrongName2 strongName2 = new StrongName2(publicKey1 == null ? (StrongNamePublicKeyBlob) null : new StrongNamePublicKeyBlob(publicKey1), name1, version1 == null ? (Version) null : new Version(version1));
          strongName2List.Add(strongName2);
        }
        ArrayList children = e.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string name2 = "PublicKeyBlob";
            string publicKey2 = securityElement.Attribute(name2);
            string name3 = "Name";
            string name4 = securityElement.Attribute(name3);
            string name5 = "AssemblyVersion";
            string version2 = securityElement.Attribute(name5);
            if (publicKey2 != null || name4 != null || version2 != null)
            {
              StrongName2 strongName2 = new StrongName2(publicKey2 == null ? (StrongNamePublicKeyBlob) null : new StrongNamePublicKeyBlob(publicKey2), name4, version2 == null ? (Version) null : new Version(version2));
              strongName2List.Add(strongName2);
            }
          }
        }
        if (strongName2List.Count == 0)
          return;
        this.m_strongNames = strongName2List.ToArray();
      }
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.StrongNameIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_strongNames != null)
      {
        if (this.m_strongNames.Length == 1)
        {
          if (this.m_strongNames[0].m_publicKeyBlob != null)
            permissionElement.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.m_strongNames[0].m_publicKeyBlob.PublicKey));
          if (this.m_strongNames[0].m_name != null)
            permissionElement.AddAttribute("Name", this.m_strongNames[0].m_name);
          if (this.m_strongNames[0].m_version != null)
            permissionElement.AddAttribute("AssemblyVersion", this.m_strongNames[0].m_version.ToString());
        }
        else
        {
          for (int index = 0; index < this.m_strongNames.Length; ++index)
          {
            SecurityElement child = new SecurityElement("StrongName");
            if (this.m_strongNames[index].m_publicKeyBlob != null)
              child.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.m_strongNames[index].m_publicKeyBlob.PublicKey));
            if (this.m_strongNames[index].m_name != null)
              child.AddAttribute("Name", this.m_strongNames[index].m_name);
            if (this.m_strongNames[index].m_version != null)
              child.AddAttribute("AssemblyVersion", this.m_strongNames[index].m_version.ToString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return StrongNameIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 12;
    }
  }
}
