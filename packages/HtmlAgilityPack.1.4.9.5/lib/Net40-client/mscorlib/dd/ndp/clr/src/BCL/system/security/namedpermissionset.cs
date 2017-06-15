// Decompiled with JetBrains decompiler
// Type: System.Security.NamedPermissionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
  /// <summary>定义具有名称以及相关说明的权限集。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class NamedPermissionSet : PermissionSet
  {
    private string m_name;
    private string m_description;
    [OptionalField(VersionAdded = 2)]
    internal string m_descrResource;
    private static object s_InternalSyncObject;

    /// <summary>获取或设置当前命名的权限集的名称。</summary>
    /// <returns>命名的权限集的名称。</returns>
    /// <exception cref="T:System.ArgumentException">名称为 null 或者是一个空字符串（“”）。</exception>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        NamedPermissionSet.CheckName(value);
        this.m_name = value;
      }
    }

    /// <summary>获取或设置当前命名的权限集的文本说明。</summary>
    /// <returns>命名的权限集的文本说明。</returns>
    public string Description
    {
      get
      {
        if (this.m_descrResource != null)
        {
          this.m_description = Environment.GetResourceString(this.m_descrResource);
          this.m_descrResource = (string) null;
        }
        return this.m_description;
      }
      set
      {
        this.m_description = value;
        this.m_descrResource = (string) null;
      }
    }

    private static object InternalSyncObject
    {
      get
      {
        if (NamedPermissionSet.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref NamedPermissionSet.s_InternalSyncObject, obj, (object) null);
        }
        return NamedPermissionSet.s_InternalSyncObject;
      }
    }

    internal NamedPermissionSet()
    {
    }

    /// <summary>用指定的名称初始化 <see cref="T:System.Security.NamedPermissionSet" /> 类的新的空实例。</summary>
    /// <param name="name">新命名的权限集名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为 null 或者是空字符串 ("")。</exception>
    public NamedPermissionSet(string name)
    {
      NamedPermissionSet.CheckName(name);
      this.m_name = name;
    }

    /// <summary>在无限制状态或完全受限状态，用指定名称初始化 <see cref="T:System.Security.NamedPermissionSet" /> 类的新实例。</summary>
    /// <param name="name">新命名的权限集名。</param>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为 null 或者是空字符串 ("")。</exception>
    public NamedPermissionSet(string name, PermissionState state)
      : base(state)
    {
      NamedPermissionSet.CheckName(name);
      this.m_name = name;
    }

    /// <summary>用来自权限集的指定名称初始化 <see cref="T:System.Security.NamedPermissionSet" /> 类的新实例。</summary>
    /// <param name="name">命名的权限集的名称。</param>
    /// <param name="permSet">从中取得新命名的权限集的值的权限集。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为 null 或者是空字符串 ("")。</exception>
    public NamedPermissionSet(string name, PermissionSet permSet)
      : base(permSet)
    {
      NamedPermissionSet.CheckName(name);
      this.m_name = name;
    }

    /// <summary>从另一个命名的权限集初始化 <see cref="T:System.Security.NamedPermissionSet" /> 类的新实例。</summary>
    /// <param name="permSet">从中创建新实例的命名的权限集。</param>
    public NamedPermissionSet(NamedPermissionSet permSet)
      : base((PermissionSet) permSet)
    {
      this.m_name = permSet.m_name;
      this.m_description = permSet.Description;
    }

    internal NamedPermissionSet(SecurityElement permissionSetXml)
      : base(PermissionState.None)
    {
      this.FromXml(permissionSetXml);
    }

    private static void CheckName(string name)
    {
      if (name == null || name.Equals(""))
        throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"));
    }

    /// <summary>从命名的权限集创建权限集副本。</summary>
    /// <returns>作为命名的权限集内权限副本的权限集。</returns>
    public override PermissionSet Copy()
    {
      return (PermissionSet) new NamedPermissionSet(this);
    }

    /// <summary>用不同的名称但相同的权限创建一个命名的权限集副本。</summary>
    /// <returns>具有新名称的命名的权限集副本。</returns>
    /// <param name="name">新命名的权限集名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为 null 或者是空字符串 ("")。</exception>
    public NamedPermissionSet Copy(string name)
    {
      return new NamedPermissionSet(this) { Name = name };
    }

    /// <summary>创建命名权限集的 XML 元素说明。</summary>
    /// <returns>命名的权限集的 XML 表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override SecurityElement ToXml()
    {
      SecurityElement xml = this.ToXml("System.Security.NamedPermissionSet");
      if (this.m_name != null && !this.m_name.Equals(""))
        xml.AddAttribute("Name", SecurityElement.Escape(this.m_name));
      if (this.Description != null && !this.Description.Equals(""))
        xml.AddAttribute("Description", SecurityElement.Escape(this.Description));
      return xml;
    }

    /// <summary>通过 XML 编码用指定的状态重新构造命名的权限集。</summary>
    /// <param name="et">包含命名的权限集 XML 表示形式的安全元素。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="et" /> 参数不是命名的权限集的有效表示形式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="et" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override void FromXml(SecurityElement et)
    {
      this.FromXml(et, false, false);
    }

    internal override void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
    {
      if (et == null)
        throw new ArgumentNullException("et");
      string str1 = et.Attribute("Name");
      this.m_name = str1 == null ? (string) null : str1;
      string str2 = et.Attribute("Description");
      this.m_description = str2 == null ? "" : str2;
      this.m_descrResource = (string) null;
      base.FromXml(et, allowInternalOnly, ignoreTypeLoadFailures);
    }

    internal void FromXmlNameOnly(SecurityElement et)
    {
      string str = et.Attribute("Name");
      this.m_name = str == null ? (string) null : str;
    }

    /// <summary>确定指定的 <see cref="T:System.Security.NamedPermissionSet" /> 对象是否等于当前的 <see cref="T:System.Security.NamedPermissionSet" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Security.NamedPermissionSet" /> 等于当前的 <see cref="T:System.Security.NamedPermissionSet" /> 对象，则为 true；否则，为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.Security.NamedPermissionSet" /> 进行比较的 <see cref="T:System.Security.NamedPermissionSet" /> 对象。</param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>获取适合在哈希算法和类似哈希表的数据结构中使用的 <see cref="T:System.Security.NamedPermissionSet" /> 对象的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.NamedPermissionSet" /> 对象的哈希代码。</returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
