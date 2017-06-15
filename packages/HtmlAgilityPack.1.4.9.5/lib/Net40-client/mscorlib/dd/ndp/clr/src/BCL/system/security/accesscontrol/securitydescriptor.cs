// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.GenericSecurityDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示安全性说明符。安全性说明符包含所有者、主要组、自由访问控制列表 (DACL) 和系统访问控制列表 (SACL)。</summary>
  public abstract class GenericSecurityDescriptor
  {
    internal const int HeaderLength = 20;
    internal const int OwnerFoundAt = 4;
    internal const int GroupFoundAt = 8;
    internal const int SaclFoundAt = 12;
    internal const int DaclFoundAt = 16;

    internal abstract GenericAcl GenericSacl { get; }

    internal abstract GenericAcl GenericDacl { get; }

    private bool IsCraftedAefaDacl
    {
      get
      {
        if (this.GenericDacl is DiscretionaryAcl)
          return (this.GenericDacl as DiscretionaryAcl).EveryOneFullAccessForNullDacl;
        return false;
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象的修订级别。</summary>
    /// <returns>一个指定 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 的修订级别的字节值。</returns>
    public static byte Revision
    {
      get
      {
        return 1;
      }
    }

    /// <summary>获取指定 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象的行为的值。</summary>
    /// <returns>使用逻辑或运算组合的一个或多个 <see cref="T:System.Security.AccessControl.ControlFlags" /> 枚举值。</returns>
    public abstract ControlFlags ControlFlags { get; }

    /// <summary>获取或设置与此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象关联的对象所有者。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象关联的对象所有者。</returns>
    public abstract SecurityIdentifier Owner { get; set; }

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象的主要组。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象的主要组。</returns>
    public abstract SecurityIdentifier Group { get; set; }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.GenericSecurityDescriptor.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public int BinaryLength
    {
      get
      {
        int num = 20;
        if (this.Owner != (SecurityIdentifier) null)
          num += this.Owner.BinaryLength;
        if (this.Group != (SecurityIdentifier) null)
          num += this.Group.BinaryLength;
        if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
          num += this.GenericSacl.BinaryLength;
        if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
          num += this.GenericDacl.BinaryLength;
        return num;
      }
    }

    private static void MarshalInt(byte[] binaryForm, int offset, int number)
    {
      binaryForm[offset + 0] = (byte) number;
      binaryForm[offset + 1] = (byte) (number >> 8);
      binaryForm[offset + 2] = (byte) (number >> 16);
      binaryForm[offset + 3] = (byte) (number >> 24);
    }

    internal static int UnmarshalInt(byte[] binaryForm, int offset)
    {
      return (int) binaryForm[offset + 0] + ((int) binaryForm[offset + 1] << 8) + ((int) binaryForm[offset + 2] << 16) + ((int) binaryForm[offset + 3] << 24);
    }

    /// <summary>返回一个布尔值，该值指定与此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象关联的安全性说明符是否能够转换为安全性说明符定义语言 (SDDL) 格式。</summary>
    /// <returns>如果与此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象关联的安全性说明符能够转换为安全性说明符定义语言 (SDDL) 格式，则为 true；否则为 false。</returns>
    public static bool IsSddlConversionSupported()
    {
      return true;
    }

    /// <summary>返回此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象表示的安全性说明符指定区域的安全说明符定义语言 (SDDL) 表示形式。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象关联的安全性说明符指定部分的 SDDL 表示形式。</returns>
    /// <param name="includeSections">指定要获取安全性说明符的哪些部分（访问规则、审核规则、主要组、所有者）。</param>
    [SecuritySafeCritical]
    public string GetSddlForm(AccessControlSections includeSections)
    {
      byte[] binaryForm = new byte[this.BinaryLength];
      this.GetBinaryForm(binaryForm, 0);
      SecurityInfos si = (SecurityInfos) 0;
      if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
        si |= SecurityInfos.Owner;
      if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
        si |= SecurityInfos.Group;
      if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
        si |= SecurityInfos.SystemAcl;
      if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
        si |= SecurityInfos.DiscretionaryAcl;
      string resultSddl;
      switch (Win32.ConvertSdToSddl(binaryForm, 1, si, out resultSddl))
      {
        case 87:
        case 1305:
          throw new InvalidOperationException();
        case 0:
          return resultSddl;
        default:
          throw new InvalidOperationException();
      }
    }

    /// <summary>返回一个表示此 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 对象中包含的信息的字节值数组。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> 复制到 <paramref name="array" />。</exception>
    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < this.BinaryLength)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      int num1 = offset;
      int binaryLength = this.BinaryLength;
      byte num2 = !(this is RawSecurityDescriptor) || (this.ControlFlags & ControlFlags.RMControlValid) == ControlFlags.None ? (byte) 0 : (this as RawSecurityDescriptor).ResourceManagerControl;
      int num3 = (int) this.ControlFlags;
      if (this.IsCraftedAefaDacl)
        num3 &= -5;
      binaryForm[offset + 0] = GenericSecurityDescriptor.Revision;
      binaryForm[offset + 1] = num2;
      binaryForm[offset + 2] = (byte) num3;
      binaryForm[offset + 3] = (byte) (num3 >> 8);
      int offset1 = offset + 4;
      int offset2 = offset + 8;
      int offset3 = offset + 12;
      int offset4 = offset + 16;
      offset += 20;
      if (this.Owner != (SecurityIdentifier) null)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset1, offset - num1);
        this.Owner.GetBinaryForm(binaryForm, offset);
        offset += this.Owner.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset1, 0);
      if (this.Group != (SecurityIdentifier) null)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset2, offset - num1);
        this.Group.GetBinaryForm(binaryForm, offset);
        offset += this.Group.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset2, 0);
      if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset3, offset - num1);
        this.GenericSacl.GetBinaryForm(binaryForm, offset);
        offset += this.GenericSacl.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset3, 0);
      if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
      {
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset4, offset - num1);
        this.GenericDacl.GetBinaryForm(binaryForm, offset);
        offset += this.GenericDacl.BinaryLength;
      }
      else
        GenericSecurityDescriptor.MarshalInt(binaryForm, offset4, 0);
    }
  }
}
