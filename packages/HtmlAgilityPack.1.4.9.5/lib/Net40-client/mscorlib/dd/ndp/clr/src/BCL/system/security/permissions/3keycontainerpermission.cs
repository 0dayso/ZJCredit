// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>控制访问密钥容器的能力。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private KeyContainerPermissionFlags m_flags;
    private KeyContainerPermissionAccessEntryCollection m_accessEntries;

    /// <summary>获取密钥容器权限标志，该标志适用于所有与权限关联的密钥容器。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> 值的按位组合。</returns>
    public KeyContainerPermissionFlags Flags
    {
      get
      {
        return this.m_flags;
      }
    }

    /// <summary>获取与当前权限关联的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象的集合。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />，包含了此 <see cref="T:System.Security.Permissions.KeyContainerPermission" /> 的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</returns>
    public KeyContainerPermissionAccessEntryCollection AccessEntries
    {
      get
      {
        return this.m_accessEntries;
      }
    }

    /// <summary>用受限制或无限制的权限初始化 <see cref="T:System.Security.Permissions.KeyContainerPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public KeyContainerPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_flags = KeyContainerPermissionFlags.AllFlags;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_flags = KeyContainerPermissionFlags.NoFlags;
      }
      this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
    }

    /// <summary>用指定的访问权限初始化 <see cref="T:System.Security.Permissions.KeyContainerPermission" /> 类的新实例。</summary>
    /// <param name="flags">
    /// <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> 值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flags" /> 不是 <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> 值的有效组合。</exception>
    public KeyContainerPermission(KeyContainerPermissionFlags flags)
    {
      KeyContainerPermission.VerifyFlags(flags);
      this.m_flags = flags;
      this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
    }

    /// <summary>用指定的全局访问和特定的密钥容器访问权限来初始化 <see cref="T:System.Security.Permissions.KeyContainerPermission" /> 类的新实例。</summary>
    /// <param name="flags">
    /// <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> 值的按位组合。</param>
    /// <param name="accessList">
    /// <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象数组，标识特定的密钥容器访问权限。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flags" /> 不是 <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> 值的有效组合。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="accessList" /> 为 null。</exception>
    public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
    {
      if (accessList == null)
        throw new ArgumentNullException("accessList");
      KeyContainerPermission.VerifyFlags(flags);
      this.m_flags = flags;
      this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
      for (int index = 0; index < accessList.Length; ++index)
        this.m_accessEntries.Add(accessList[index]);
    }

    /// <summary>确定当前权限是否无限制。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      if (this.m_flags != KeyContainerPermissionFlags.AllFlags)
        return false;
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
      {
        if ((accessEntry.Flags & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.AllFlags)
          return false;
      }
      return true;
    }

    private bool IsEmpty()
    {
      if (this.Flags != KeyContainerPermissionFlags.NoFlags)
        return false;
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
      {
        if (accessEntry.Flags != KeyContainerPermissionFlags.NoFlags)
          return false;
      }
      return true;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是 null，并且未指定与当前权限属于同一类型的权限。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      KeyContainerPermission target1 = (KeyContainerPermission) target;
      if ((this.m_flags & target1.m_flags) != this.m_flags)
        return false;
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
      {
        KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(accessEntry, target1);
        if ((accessEntry.Flags & applicableFlags) != accessEntry.Flags)
          return false;
      }
      foreach (KeyContainerPermissionAccessEntry accessEntry in target1.AccessEntries)
      {
        KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(accessEntry, this);
        if ((applicableFlags & accessEntry.Flags) != applicableFlags)
          return false;
      }
      return true;
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是 null，并且未指定与当前权限属于同一类型的权限。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      KeyContainerPermission target1 = (KeyContainerPermission) target;
      if (this.IsEmpty() || target1.IsEmpty())
        return (IPermission) null;
      KeyContainerPermission containerPermission = new KeyContainerPermission(target1.m_flags & this.m_flags);
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
        containerPermission.AddAccessEntryAndIntersect(accessEntry, target1);
      foreach (KeyContainerPermissionAccessEntry accessEntry in target1.AccessEntries)
        containerPermission.AddAccessEntryAndIntersect(accessEntry, this);
      if (!containerPermission.IsEmpty())
        return (IPermission) containerPermission;
      return (IPermission) null;
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是 null，并且未指定与当前权限属于同一类型的权限。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      KeyContainerPermission target1 = (KeyContainerPermission) target;
      if (this.IsUnrestricted() || target1.IsUnrestricted())
        return (IPermission) new KeyContainerPermission(PermissionState.Unrestricted);
      KeyContainerPermission containerPermission = new KeyContainerPermission(this.m_flags | target1.m_flags);
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
        containerPermission.AddAccessEntryAndUnion(accessEntry, target1);
      foreach (KeyContainerPermissionAccessEntry accessEntry in target1.AccessEntries)
        containerPermission.AddAccessEntryAndUnion(accessEntry, this);
      if (!containerPermission.IsEmpty())
        return (IPermission) containerPermission;
      return (IPermission) null;
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      if (this.IsEmpty())
        return (IPermission) null;
      KeyContainerPermission containerPermission = new KeyContainerPermission(this.m_flags);
      foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
        containerPermission.AccessEntries.Add(accessEntry);
      return (IPermission) containerPermission;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>
    /// <see cref="T:System.Security.SecurityElement" />，包含权限的 XML 编码（包括状态信息）。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.KeyContainerPermission");
      if (!this.IsUnrestricted())
      {
        permissionElement.AddAttribute("Flags", this.m_flags.ToString());
        if (this.AccessEntries.Count > 0)
        {
          SecurityElement child1 = new SecurityElement("AccessList");
          foreach (KeyContainerPermissionAccessEntry accessEntry in this.AccessEntries)
          {
            SecurityElement child2 = new SecurityElement("AccessEntry");
            child2.AddAttribute("KeyStore", accessEntry.KeyStore);
            child2.AddAttribute("ProviderName", accessEntry.ProviderName);
            SecurityElement securityElement1 = child2;
            string name1 = "ProviderType";
            int num = accessEntry.ProviderType;
            string string1 = num.ToString((string) null, (IFormatProvider) null);
            securityElement1.AddAttribute(name1, string1);
            child2.AddAttribute("KeyContainerName", accessEntry.KeyContainerName);
            SecurityElement securityElement2 = child2;
            string name2 = "KeySpec";
            num = accessEntry.KeySpec;
            string string2 = num.ToString((string) null, (IFormatProvider) null);
            securityElement2.AddAttribute(name2, string2);
            child2.AddAttribute("Flags", accessEntry.Flags.ToString());
            child1.AddChild(child2);
          }
          permissionElement.AddChild(child1);
        }
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="securityElement">
    /// <see cref="T:System.Security.SecurityElement" />，包含用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="securityElement" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="securityElement" /> 不是一个有效的权限元素。- 或 -不支持 <paramref name="securityElement" /> 的版本号。</exception>
    public override void FromXml(SecurityElement securityElement)
    {
      CodeAccessPermission.ValidateElement(securityElement, (IPermission) this);
      if (XMLUtil.IsUnrestricted(securityElement))
      {
        this.m_flags = KeyContainerPermissionFlags.AllFlags;
        this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
      }
      else
      {
        this.m_flags = KeyContainerPermissionFlags.NoFlags;
        string str = securityElement.Attribute("Flags");
        if (str != null)
        {
          KeyContainerPermissionFlags flags = (KeyContainerPermissionFlags) Enum.Parse(typeof (KeyContainerPermissionFlags), str);
          KeyContainerPermission.VerifyFlags(flags);
          this.m_flags = flags;
        }
        this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
        if (securityElement.InternalChildren == null || securityElement.InternalChildren.Count == 0)
          return;
        foreach (SecurityElement child in securityElement.Children)
        {
          if (child != null && string.Equals(child.Tag, "AccessList"))
            this.AddAccessEntries(child);
        }
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return KeyContainerPermission.GetTokenIndex();
    }

    private void AddAccessEntries(SecurityElement securityElement)
    {
      if (securityElement.InternalChildren == null || securityElement.InternalChildren.Count == 0)
        return;
      foreach (SecurityElement child in securityElement.Children)
      {
        if (child != null && string.Equals(child.Tag, "AccessEntry"))
        {
          int count = child.m_lAttributes.Count;
          string keyStore = (string) null;
          string providerName = (string) null;
          int providerType = -1;
          string keyContainerName = (string) null;
          int keySpec = -1;
          KeyContainerPermissionFlags flags = KeyContainerPermissionFlags.NoFlags;
          int index = 0;
          while (index < count)
          {
            string a = (string) child.m_lAttributes[index];
            string str = (string) child.m_lAttributes[index + 1];
            if (string.Equals(a, "KeyStore"))
              keyStore = str;
            if (string.Equals(a, "ProviderName"))
              providerName = str;
            else if (string.Equals(a, "ProviderType"))
              providerType = Convert.ToInt32(str, (IFormatProvider) null);
            else if (string.Equals(a, "KeyContainerName"))
              keyContainerName = str;
            else if (string.Equals(a, "KeySpec"))
              keySpec = Convert.ToInt32(str, (IFormatProvider) null);
            else if (string.Equals(a, "Flags"))
              flags = (KeyContainerPermissionFlags) Enum.Parse(typeof (KeyContainerPermissionFlags), str);
            index += 2;
          }
          this.AccessEntries.Add(new KeyContainerPermissionAccessEntry(keyStore, providerName, providerType, keyContainerName, keySpec, flags));
        }
      }
    }

    private void AddAccessEntryAndUnion(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
    {
      KeyContainerPermissionAccessEntry accessEntry1 = new KeyContainerPermissionAccessEntry(accessEntry);
      accessEntry1.Flags |= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
      this.AccessEntries.Add(accessEntry1);
    }

    private void AddAccessEntryAndIntersect(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
    {
      KeyContainerPermissionAccessEntry accessEntry1 = new KeyContainerPermissionAccessEntry(accessEntry);
      accessEntry1.Flags &= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
      this.AccessEntries.Add(accessEntry1);
    }

    internal static void VerifyFlags(KeyContainerPermissionFlags flags)
    {
      if ((flags & ~KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flags));
    }

    private static KeyContainerPermissionFlags GetApplicableFlags(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
    {
      KeyContainerPermissionFlags containerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
      bool flag = true;
      int index = target.AccessEntries.IndexOf(accessEntry);
      if (index != -1)
        return target.AccessEntries[index].Flags;
      foreach (KeyContainerPermissionAccessEntry accessEntry1 in target.AccessEntries)
      {
        if (accessEntry.IsSubsetOf(accessEntry1))
        {
          if (!flag)
          {
            containerPermissionFlags &= accessEntry1.Flags;
          }
          else
          {
            containerPermissionFlags = accessEntry1.Flags;
            flag = false;
          }
        }
      }
      if (flag)
        containerPermissionFlags = target.Flags;
      return containerPermissionFlags;
    }

    private static int GetTokenIndex()
    {
      return 16;
    }
  }
}
