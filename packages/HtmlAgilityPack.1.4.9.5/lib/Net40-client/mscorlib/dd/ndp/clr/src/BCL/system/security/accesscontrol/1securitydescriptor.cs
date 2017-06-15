// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RawSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示安全性说明符。安全性说明符包含所有者、主要组、自由访问控制列表 (DACL) 和系统访问控制列表 (SACL)。</summary>
  public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
  {
    private SecurityIdentifier _owner;
    private SecurityIdentifier _group;
    private ControlFlags _flags;
    private RawAcl _sacl;
    private RawAcl _dacl;
    private byte _rmControl;

    internal override GenericAcl GenericSacl
    {
      get
      {
        return (GenericAcl) this._sacl;
      }
    }

    internal override GenericAcl GenericDacl
    {
      get
      {
        return (GenericAcl) this._dacl;
      }
    }

    /// <summary>获取指定 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的行为的值。</summary>
    /// <returns>使用逻辑或运算组合的一个或多个 <see cref="T:System.Security.AccessControl.ControlFlags" /> 枚举值。</returns>
    public override ControlFlags ControlFlags
    {
      get
      {
        return this._flags;
      }
    }

    /// <summary>获取或设置与此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象关联的对象所有者。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象关联的对象所有者。</returns>
    public override SecurityIdentifier Owner
    {
      get
      {
        return this._owner;
      }
      set
      {
        this._owner = value;
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的主要组。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的主要组。</returns>
    public override SecurityIdentifier Group
    {
      get
      {
        return this._group;
      }
      set
      {
        this._group = value;
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的系统访问控制列表 (SACL)。SACL 包含审核规则。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的 SACL。</returns>
    public RawAcl SystemAcl
    {
      get
      {
        return this._sacl;
      }
      set
      {
        this._sacl = value;
      }
    }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的自由访问控制列表 (DACL)。DACL 包含访问规则。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的 DACL。</returns>
    public RawAcl DiscretionaryAcl
    {
      get
      {
        return this._dacl;
      }
      set
      {
        this._dacl = value;
      }
    }

    /// <summary>获取或设置表示与此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象关联的资源管理器控制位的字节值。</summary>
    /// <returns>一个表示与此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象关联的资源管理器控制位的字节值。</returns>
    public byte ResourceManagerControl
    {
      get
      {
        return this._rmControl;
      }
      set
      {
        this._rmControl = value;
      }
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="flags">指定新的 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的行为的标志。</param>
    /// <param name="owner">新 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的所有者。</param>
    /// <param name="group">新 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的主要组。</param>
    /// <param name="systemAcl">新的 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的系统访问控制列表 (SACL)。</param>
    /// <param name="discretionaryAcl">新的 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的自由访问控制列表 (DACL)。</param>
    public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
    {
      this.CreateFromParts(flags, owner, group, systemAcl, discretionaryAcl);
    }

    /// <summary>使用指定的安全性说明符定义语言 (SDDL) 字符串初始化 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="sddlForm">用于创建新 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的 SDDL 字符串。</param>
    [SecuritySafeCritical]
    public RawSecurityDescriptor(string sddlForm)
      : this(RawSecurityDescriptor.BinaryFormFromSddlForm(sddlForm), 0)
    {
    }

    /// <summary>使用指定的字节值数组初始化 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 类的新实例。</summary>
    /// <param name="binaryForm">用于创建新的 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的字节值数组。</param>
    /// <param name="offset">
    /// <paramref name="binaryForm" /> 数组中第一个要复制的元素的偏移量。</param>
    public RawSecurityDescriptor(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < 20)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if ((int) binaryForm[offset + 0] != (int) GenericSecurityDescriptor.Revision)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorRevision"));
      byte num1 = binaryForm[offset + 1];
      ControlFlags flags = (ControlFlags) ((int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8));
      if ((flags & ControlFlags.SelfRelative) == ControlFlags.None)
        throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorSelfRelativeForm"), "binaryForm");
      int num2 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 4);
      SecurityIdentifier owner = num2 == 0 ? (SecurityIdentifier) null : new SecurityIdentifier(binaryForm, offset + num2);
      int num3 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 8);
      SecurityIdentifier group = num3 == 0 ? (SecurityIdentifier) null : new SecurityIdentifier(binaryForm, offset + num3);
      int num4 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 12);
      RawAcl systemAcl = (flags & ControlFlags.SystemAclPresent) == ControlFlags.None || num4 == 0 ? (RawAcl) null : new RawAcl(binaryForm, offset + num4);
      int num5 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 16);
      RawAcl discretionaryAcl = (flags & ControlFlags.DiscretionaryAclPresent) == ControlFlags.None || num5 == 0 ? (RawAcl) null : new RawAcl(binaryForm, offset + num5);
      this.CreateFromParts(flags, owner, group, systemAcl, discretionaryAcl);
      if ((flags & ControlFlags.RMControlValid) == ControlFlags.None)
        return;
      this.ResourceManagerControl = num1;
    }

    private void CreateFromParts(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
    {
      this.SetFlags(flags);
      this.Owner = owner;
      this.Group = group;
      this.SystemAcl = systemAcl;
      this.DiscretionaryAcl = discretionaryAcl;
      this.ResourceManagerControl = (byte) 0;
    }

    [SecurityCritical]
    private static byte[] BinaryFormFromSddlForm(string sddlForm)
    {
      if (sddlForm == null)
        throw new ArgumentNullException("sddlForm");
      IntPtr resultSd = IntPtr.Zero;
      uint resultSdLength = 0;
      byte[] destination = (byte[]) null;
      try
      {
        if (1 != Win32Native.ConvertStringSdToSd(sddlForm, (uint) GenericSecurityDescriptor.Revision, out resultSd, ref resultSdLength))
        {
          switch (Marshal.GetLastWin32Error())
          {
            case 87:
            case 1336:
            case 1338:
            case 1305:
              throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidSDSddlForm"), "sddlForm");
            case 8:
              throw new OutOfMemoryException();
            case 1337:
              throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSidInSDDLString"), "sddlForm");
            case 0:
              break;
            default:
              throw new SystemException();
          }
        }
        destination = new byte[(int) resultSdLength];
        Marshal.Copy(resultSd, destination, 0, (int) resultSdLength);
      }
      finally
      {
        if (resultSd != IntPtr.Zero)
          Win32Native.LocalFree(resultSd);
      }
      return destination;
    }

    /// <summary>将此 <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> 对象的 <see cref="P:System.Security.AccessControl.RawSecurityDescriptor.ControlFlags" /> 属性设置为指定值。</summary>
    /// <param name="flags">使用逻辑或运算组合的一个或多个 <see cref="T:System.Security.AccessControl.ControlFlags" /> 枚举值。</param>
    public void SetFlags(ControlFlags flags)
    {
      this._flags = flags | ControlFlags.SelfRelative;
    }
  }
}
