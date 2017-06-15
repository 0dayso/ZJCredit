// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示安全性说明符。安全性说明符包含所有者、主要组、自由访问控制列表 (DACL) 和系统访问控制列表 (SACL)。</summary>
  public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
  {
    private bool _isContainer;
    private bool _isDS;
    private RawSecurityDescriptor _rawSd;
    private SystemAcl _sacl;
    private DiscretionaryAcl _dacl;

    internal override sealed GenericAcl GenericSacl
    {
      get
      {
        return (GenericAcl) this._sacl;
      }
    }

    internal override sealed GenericAcl GenericDacl
    {
      get
      {
        return (GenericAcl) this._dacl;
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的对象是否为容器对象。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的对象是一个容器对象，则为 true；否则为 false。</returns>
    public bool IsContainer
    {
      get
      {
        return this._isContainer;
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的对象是否为目录对象。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的对象是一个目录对象，则为 true；否则为 false。</returns>
    public bool IsDS
    {
      get
      {
        return this._isDS;
      }
    }

    /// <summary>获取指定 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的行为的值。</summary>
    /// <returns>使用逻辑或运算组合的一个或多个 <see cref="T:System.Security.AccessControl.ControlFlags" /> 枚举值。</returns>
    public override ControlFlags ControlFlags
    {
      get
      {
        return this._rawSd.ControlFlags;
      }
    }

    /// <summary>获取或设置与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的对象所有者。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的对象所有者。</returns>
    public override SecurityIdentifier Owner
    {
      get
      {
        return this._rawSd.Owner;
      }
      set
      {
        this._rawSd.Owner = value;
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的主要组。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的主要组。</returns>
    public override SecurityIdentifier Group
    {
      get
      {
        return this._rawSd.Group;
      }
      set
      {
        this._rawSd.Group = value;
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的系统访问控制列表 (SACL)。SACL 包含审核规则。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的 SACL。</returns>
    public SystemAcl SystemAcl
    {
      get
      {
        return this._sacl;
      }
      set
      {
        if (value != null)
        {
          if (value.IsContainer != this.IsContainer)
            throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "value");
          if (value.IsDS != this.IsDS)
            throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "value");
        }
        this._sacl = value;
        if (this._sacl != null)
        {
          this._rawSd.SystemAcl = this._sacl.RawAcl;
          this.AddControlFlags(ControlFlags.SystemAclPresent);
        }
        else
        {
          this._rawSd.SystemAcl = (RawAcl) null;
          this.RemoveControlFlags(ControlFlags.SystemAclPresent);
        }
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的自由访问控制列表 (DACL)。DACL 包含访问规则。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的 DACL。</returns>
    public DiscretionaryAcl DiscretionaryAcl
    {
      get
      {
        return this._dacl;
      }
      set
      {
        if (value != null)
        {
          if (value.IsContainer != this.IsContainer)
            throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "value");
          if (value.IsDS != this.IsDS)
            throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "value");
        }
        this._dacl = value != null ? value : DiscretionaryAcl.CreateAllowEveryoneFullAccess(this.IsDS, this.IsContainer);
        this._rawSd.DiscretionaryAcl = this._dacl.RawAcl;
        this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的系统访问控制列表 (SACL) 是否按规范顺序。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的 SACL 处于规范顺序，则为 true；否则为 false。</returns>
    public bool IsSystemAclCanonical
    {
      get
      {
        if (this.SystemAcl != null)
          return this.SystemAcl.IsCanonical;
        return true;
      }
    }

    /// <summary>获取一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的自由访问控制列表 (DACL) 是否按规范顺序。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的 DACL 处于规范顺序，则为 true；否则为 false。</returns>
    public bool IsDiscretionaryAclCanonical
    {
      get
      {
        if (this.DiscretionaryAcl != null)
          return this.DiscretionaryAcl.IsCanonical;
        return true;
      }
    }

    internal bool IsSystemAclPresent
    {
      get
      {
        return (uint) (this._rawSd.ControlFlags & ControlFlags.SystemAclPresent) > 0U;
      }
    }

    internal bool IsDiscretionaryAclPresent
    {
      get
      {
        return (uint) (this._rawSd.ControlFlags & ControlFlags.DiscretionaryAclPresent) > 0U;
      }
    }

    /// <summary>使用指定信息初始化 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的安全性说明符与某个容器对象关联，则为 true。</param>
    /// <param name="isDS">如果新的安全性说明符与某个目录对象关联，则为 true。</param>
    /// <param name="flags">指定新的 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的行为的标志。</param>
    /// <param name="owner">新 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的所有者。</param>
    /// <param name="group">新 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的主要组。</param>
    /// <param name="systemAcl">新的 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的系统访问控制列表 (SACL)。</param>
    /// <param name="discretionaryAcl">新的 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的自由访问控制列表 (DACL)。</param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
    {
      this.CreateFromParts(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
    }

    private CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
      : this(isContainer, isDS, flags, owner, group, systemAcl == null ? (SystemAcl) null : new SystemAcl(isContainer, isDS, systemAcl), discretionaryAcl == null ? (DiscretionaryAcl) null : new DiscretionaryAcl(isContainer, isDS, discretionaryAcl))
    {
    }

    /// <summary>从指定的 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象初始化 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的安全性说明符与某个容器对象关联，则为 true。</param>
    /// <param name="isDS">如果新的安全性说明符与某个目录对象关联，则为 true。</param>
    /// <param name="rawSecurityDescriptor">用来从中创建新 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象。</param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
      : this(isContainer, isDS, rawSecurityDescriptor, false)
    {
    }

    internal CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor, bool trusted)
    {
      if (rawSecurityDescriptor == null)
        throw new ArgumentNullException("rawSecurityDescriptor");
      this.CreateFromParts(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, rawSecurityDescriptor.SystemAcl == null ? (SystemAcl) null : new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl, trusted), rawSecurityDescriptor.DiscretionaryAcl == null ? (DiscretionaryAcl) null : new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl, trusted));
    }

    /// <summary>使用指定的安全性说明符定义语言 (SDDL) 字符串初始化 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的安全性说明符与某个容器对象关联，则为 true。</param>
    /// <param name="isDS">如果新的安全性说明符与某个目录对象关联，则为 true。</param>
    /// <param name="sddlForm">用于创建新 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的 SDDL 字符串。</param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm)
      : this(isContainer, isDS, new RawSecurityDescriptor(sddlForm), true)
    {
    }

    /// <summary>使用指定的字节值数组初始化 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的安全性说明符与某个容器对象关联，则为 true。</param>
    /// <param name="isDS">如果新的安全性说明符与某个目录对象关联，则为 true。</param>
    /// <param name="binaryForm">用于创建新的 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象的字节值数组。</param>
    /// <param name="offset">
    /// <paramref name="binaryForm" /> 数组中的偏移量，在此位置开始复制。</param>
    public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
      : this(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset), true)
    {
    }

    private void CreateFromParts(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
    {
      if (systemAcl != null && systemAcl.IsContainer != isContainer)
        throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "systemAcl");
      if (discretionaryAcl != null && discretionaryAcl.IsContainer != isContainer)
        throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "discretionaryAcl");
      this._isContainer = isContainer;
      if (systemAcl != null && systemAcl.IsDS != isDS)
        throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "systemAcl");
      if (discretionaryAcl != null && discretionaryAcl.IsDS != isDS)
        throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "discretionaryAcl");
      this._isDS = isDS;
      this._sacl = systemAcl;
      if (discretionaryAcl == null)
        discretionaryAcl = DiscretionaryAcl.CreateAllowEveryoneFullAccess(this._isDS, this._isContainer);
      this._dacl = discretionaryAcl;
      ControlFlags controlFlags = flags | ControlFlags.DiscretionaryAclPresent;
      this._rawSd = new RawSecurityDescriptor(systemAcl != null ? controlFlags | ControlFlags.SystemAclPresent : controlFlags & ~ControlFlags.SystemAclPresent, owner, group, systemAcl == null ? (RawAcl) null : systemAcl.RawAcl, discretionaryAcl.RawAcl);
    }

    /// <summary>为与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的系统访问控制列表 (SACL) 设置继承保护。受保护的 SACL 不会从父容器继承审核规则。</summary>
    /// <param name="isProtected">若要保护 SACL 不被继承，则为 true。</param>
    /// <param name="preserveInheritance">若要在 SACL 中保留继承的审核规则，则为 true；若要从 SACL 中移除继承的审核规则，则为 false。</param>
    public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
    {
      if (!isProtected)
      {
        this.RemoveControlFlags(ControlFlags.SystemAclProtected);
      }
      else
      {
        if (!preserveInheritance && this.SystemAcl != null)
          this.SystemAcl.RemoveInheritedAces();
        this.AddControlFlags(ControlFlags.SystemAclProtected);
      }
    }

    /// <summary>为与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的自由访问控制列表 (DACL) 设置继承保护。受保护的 DACL 不会从父容器继承访问规则。</summary>
    /// <param name="isProtected">若要保护 DACL 不被继承，则为 true。</param>
    /// <param name="preserveInheritance">若要在 DACL 中保留继承的访问规则，则为 true；若要从 DACL 中移除继承的访问规则，则为 false。</param>
    public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
    {
      if (!isProtected)
      {
        this.RemoveControlFlags(ControlFlags.DiscretionaryAclProtected);
      }
      else
      {
        if (!preserveInheritance && this.DiscretionaryAcl != null)
          this.DiscretionaryAcl.RemoveInheritedAces();
        this.AddControlFlags(ControlFlags.DiscretionaryAclProtected);
      }
      if (this.DiscretionaryAcl == null || !this.DiscretionaryAcl.EveryOneFullAccessForNullDacl)
        return;
      this.DiscretionaryAcl.EveryOneFullAccessForNullDacl = false;
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的自由访问控制列表 (DACL) 中移除指定的安全性标识符的所有访问规则。</summary>
    /// <param name="sid">要为其移除访问规则的安全性标识符。</param>
    public void PurgeAccessControl(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      if (this.DiscretionaryAcl == null)
        return;
      this.DiscretionaryAcl.Purge(sid);
    }

    /// <summary>从与此 <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> 对象关联的系统访问控制列表 (SACL) 中移除指定的安全性标识符的所有审核规则。</summary>
    /// <param name="sid">要为其移除审核规则的安全性标识符。</param>
    public void PurgeAudit(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      if (this.SystemAcl == null)
        return;
      this.SystemAcl.Purge(sid);
    }

    /// <summary>设置<see cref="P:System.Security.AccessControl.CommonSecurityDescriptor.DiscretionaryAcl" />为此属性<see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" />实例并设置<see cref="F:System.Security.AccessControl.ControlFlags.DiscretionaryAclPresent" />标志。</summary>
    /// <param name="revision">新的 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象的修订级别。</param>
    /// <param name="trusted">此 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public void AddDiscretionaryAcl(byte revision, int trusted)
    {
      this.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, revision, trusted);
      this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
    }

    /// <summary>设置<see cref="P:System.Security.AccessControl.CommonSecurityDescriptor.SystemAcl" />为此属性<see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" />实例并设置<see cref="F:System.Security.AccessControl.ControlFlags.SystemAclPresent" />标志。</summary>
    /// <param name="revision">新的 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象的修订级别。</param>
    /// <param name="trusted">此 <see cref="T:System.Security.AccessControl.SystemAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public void AddSystemAcl(byte revision, int trusted)
    {
      this.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, revision, trusted);
      this.AddControlFlags(ControlFlags.SystemAclPresent);
    }

    internal void UpdateControlFlags(ControlFlags flagsToUpdate, ControlFlags newFlags)
    {
      this._rawSd.SetFlags(newFlags | this._rawSd.ControlFlags & ~flagsToUpdate);
    }

    internal void AddControlFlags(ControlFlags flags)
    {
      this._rawSd.SetFlags(this._rawSd.ControlFlags | flags);
    }

    internal void RemoveControlFlags(ControlFlags flags)
    {
      this._rawSd.SetFlags(this._rawSd.ControlFlags & ~flags);
    }
  }
}
