// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.GenericAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示一个访问控制项 (ACE)，并且是其他所有 ACE 类的基类。</summary>
  public abstract class GenericAce
  {
    private readonly AceType _type;
    private AceFlags _flags;
    internal ushort _indexInAcl;
    internal const int HeaderLength = 4;

    /// <summary>获取此访问控制项 (ACE) 的类型。</summary>
    /// <returns>此 ACE 的类型。</returns>
    public AceType AceType
    {
      get
      {
        return this._type;
      }
    }

    /// <summary>获取或设置与此 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象关联的 <see cref="T:System.Security.AccessControl.AceFlags" /> 对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象关联的 <see cref="T:System.Security.AccessControl.AceFlags" />。</returns>
    public AceFlags AceFlags
    {
      get
      {
        return this._flags;
      }
      set
      {
        this._flags = value;
      }
    }

    /// <summary>获取一个布尔值，该值指定此访问控制项 (ACE) 是继承的还是显式设置的。</summary>
    /// <returns>如果此 ACE 是继承的，则为 true；否则，为 false。</returns>
    public bool IsInherited
    {
      get
      {
        return (uint) (this.AceFlags & AceFlags.Inherited) > 0U;
      }
    }

    /// <summary>获取指定此访问控制项 (ACE) 的继承属性的标志。</summary>
    /// <returns>指定此 ACE 的继承属性的标志。</returns>
    public InheritanceFlags InheritanceFlags
    {
      get
      {
        InheritanceFlags inheritanceFlags = InheritanceFlags.None;
        if ((this.AceFlags & AceFlags.ContainerInherit) != AceFlags.None)
          inheritanceFlags |= InheritanceFlags.ContainerInherit;
        if ((this.AceFlags & AceFlags.ObjectInherit) != AceFlags.None)
          inheritanceFlags |= InheritanceFlags.ObjectInherit;
        return inheritanceFlags;
      }
    }

    /// <summary>获取指定此访问控制项 (ACE) 的继承传播属性的标志。</summary>
    /// <returns>指定此 ACE 的继承传播属性的标志。</returns>
    public PropagationFlags PropagationFlags
    {
      get
      {
        PropagationFlags propagationFlags = PropagationFlags.None;
        if ((this.AceFlags & AceFlags.InheritOnly) != AceFlags.None)
          propagationFlags |= PropagationFlags.InheritOnly;
        if ((this.AceFlags & AceFlags.NoPropagateInherit) != AceFlags.None)
          propagationFlags |= PropagationFlags.NoPropagateInherit;
        return propagationFlags;
      }
    }

    /// <summary>获取与此访问控制项 (ACE) 关联的审核信息。</summary>
    /// <returns>与此访问控制项 (ACE) 关联的审核信息。</returns>
    public AuditFlags AuditFlags
    {
      get
      {
        AuditFlags auditFlags = AuditFlags.None;
        if ((this.AceFlags & AceFlags.SuccessfulAccess) != AceFlags.None)
          auditFlags |= AuditFlags.Success;
        if ((this.AceFlags & AceFlags.FailedAccess) != AceFlags.None)
          auditFlags |= AuditFlags.Failure;
        return auditFlags;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.GenericAce.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public abstract int BinaryLength { get; }

    internal GenericAce(AceType type, AceFlags flags)
    {
      this._type = type;
      this._flags = flags;
    }

    /// <summary>确定指定的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象是否被视为相等。</summary>
    /// <returns>如果两个 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象。</param>
    /// <param name="right">要比较的第二个 <see cref="T:System.Security.AccessControl.GenericAce" />。</param>
    public static bool operator ==(GenericAce left, GenericAce right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.Equals((object) right);
    }

    /// <summary>确定指定的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象是否被视为不相等。</summary>
    /// <returns>如果两个 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象不相等，则为 true；否则，为 false。</returns>
    /// <param name="left">要比较的第一个 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象。</param>
    /// <param name="right">要比较的第二个 <see cref="T:System.Security.AccessControl.GenericAce" />。</param>
    public static bool operator !=(GenericAce left, GenericAce right)
    {
      return !(left == right);
    }

    internal void MarshalHeader(byte[] binaryForm, int offset)
    {
      int binaryLength = this.BinaryLength;
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < this.BinaryLength)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if (binaryLength > (int) ushort.MaxValue)
        throw new SystemException();
      binaryForm[offset + 0] = (byte) this.AceType;
      binaryForm[offset + 1] = (byte) this.AceFlags;
      binaryForm[offset + 2] = (byte) binaryLength;
      binaryForm[offset + 3] = (byte) (binaryLength >> 8);
    }

    internal static AceFlags AceFlagsFromAuditFlags(AuditFlags auditFlags)
    {
      AceFlags aceFlags = AceFlags.None;
      if ((auditFlags & AuditFlags.Success) != AuditFlags.None)
        aceFlags |= AceFlags.SuccessfulAccess;
      if ((auditFlags & AuditFlags.Failure) != AuditFlags.None)
        aceFlags |= AceFlags.FailedAccess;
      if (aceFlags == AceFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "auditFlags");
      return aceFlags;
    }

    internal static AceFlags AceFlagsFromInheritanceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      AceFlags aceFlags = AceFlags.None;
      if ((inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
        aceFlags |= AceFlags.ContainerInherit;
      if ((inheritanceFlags & InheritanceFlags.ObjectInherit) != InheritanceFlags.None)
        aceFlags |= AceFlags.ObjectInherit;
      if (aceFlags != AceFlags.None)
      {
        if ((propagationFlags & PropagationFlags.NoPropagateInherit) != PropagationFlags.None)
          aceFlags |= AceFlags.NoPropagateInherit;
        if ((propagationFlags & PropagationFlags.InheritOnly) != PropagationFlags.None)
          aceFlags |= AceFlags.InheritOnly;
      }
      return aceFlags;
    }

    internal static void VerifyHeader(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < 4)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if (((int) binaryForm[offset + 3] << 8) + (int) binaryForm[offset + 2] > binaryForm.Length - offset)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
    }

    /// <summary>从指定的二进制数据创建一个 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象。</summary>
    /// <returns>此方法创建的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象。</returns>
    /// <param name="binaryForm">用于创建新 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象的二进制数据。</param>
    /// <param name="offset">开始取消封送的偏移量。</param>
    public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
    {
      GenericAce.VerifyHeader(binaryForm, offset);
      AceType type = (AceType) binaryForm[offset];
      GenericAce genericAce;
      switch (type)
      {
        case AceType.AccessAllowed:
        case AceType.AccessDenied:
        case AceType.SystemAudit:
        case AceType.SystemAlarm:
        case AceType.AccessAllowedCallback:
        case AceType.AccessDeniedCallback:
        case AceType.SystemAuditCallback:
        case AceType.SystemAlarmCallback:
          AceQualifier qualifier1;
          int accessMask1;
          SecurityIdentifier sid1;
          bool isCallback1;
          byte[] opaque1;
          if (CommonAce.ParseBinaryForm(binaryForm, offset, out qualifier1, out accessMask1, out sid1, out isCallback1, out opaque1))
          {
            genericAce = (GenericAce) new CommonAce((AceFlags) binaryForm[offset + 1], qualifier1, accessMask1, sid1, isCallback1, opaque1);
            break;
          }
          goto label_15;
        case AceType.AccessAllowedObject:
        case AceType.AccessDeniedObject:
        case AceType.SystemAuditObject:
        case AceType.SystemAlarmObject:
        case AceType.AccessAllowedCallbackObject:
        case AceType.AccessDeniedCallbackObject:
        case AceType.SystemAuditCallbackObject:
        case AceType.SystemAlarmCallbackObject:
          AceQualifier qualifier2;
          int accessMask2;
          SecurityIdentifier sid2;
          ObjectAceFlags objectFlags;
          Guid objectAceType;
          Guid inheritedObjectAceType;
          bool isCallback2;
          byte[] opaque2;
          if (ObjectAce.ParseBinaryForm(binaryForm, offset, out qualifier2, out accessMask2, out sid2, out objectFlags, out objectAceType, out inheritedObjectAceType, out isCallback2, out opaque2))
          {
            genericAce = (GenericAce) new ObjectAce((AceFlags) binaryForm[offset + 1], qualifier2, accessMask2, sid2, objectFlags, objectAceType, inheritedObjectAceType, isCallback2, opaque2);
            break;
          }
          goto label_15;
        case AceType.AccessAllowedCompound:
          int accessMask3;
          CompoundAceType compoundAceType;
          SecurityIdentifier sid3;
          if (CompoundAce.ParseBinaryForm(binaryForm, offset, out accessMask3, out compoundAceType, out sid3))
          {
            genericAce = (GenericAce) new CompoundAce((AceFlags) binaryForm[offset + 1], accessMask3, compoundAceType, sid3);
            break;
          }
          goto label_15;
        default:
          AceFlags flags = (AceFlags) binaryForm[offset + 1];
          byte[] opaque3 = (byte[]) null;
          int num = (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8);
          if (num % 4 == 0)
          {
            int length = num - 4;
            if (length > 0)
            {
              opaque3 = new byte[length];
              for (int index = 0; index < length; ++index)
                opaque3[index] = binaryForm[offset + num - length + index];
            }
            genericAce = (GenericAce) new CustomAce(type, flags, opaque3);
            break;
          }
          goto label_15;
      }
      if ((genericAce is ObjectAce || (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8) == genericAce.BinaryLength) && (!(genericAce is ObjectAce) || (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8) == genericAce.BinaryLength || (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8) - 32 == genericAce.BinaryLength))
        return genericAce;
label_15:
      throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAceBinaryForm"), "binaryForm");
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象的内容封送到指定字节数组中，其位置从指定的偏移量开始。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.GenericAce" /> 的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.GenericAcl" /> 复制到 <paramref name="array" />。</exception>
    public abstract void GetBinaryForm(byte[] binaryForm, int offset);

    /// <summary>创建此访问控制项 (ACE) 的深层副本。</summary>
    /// <returns>此方法所创建的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象。</returns>
    public GenericAce Copy()
    {
      byte[] binaryForm = new byte[this.BinaryLength];
      this.GetBinaryForm(binaryForm, 0);
      return GenericAce.CreateFromBinaryForm(binaryForm, 0);
    }

    /// <summary>确定指定的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象是否等同于当前的 <see cref="T:System.Security.AccessControl.GenericAce" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象等于当前的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象进行比较的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象。</param>
    public override sealed bool Equals(object o)
    {
      if (o == null)
        return false;
      GenericAce genericAce = o as GenericAce;
      if (genericAce == (GenericAce) null || this.AceType != genericAce.AceType || this.AceFlags != genericAce.AceFlags)
        return false;
      int binaryLength1 = this.BinaryLength;
      int binaryLength2 = genericAce.BinaryLength;
      if (binaryLength1 != binaryLength2)
        return false;
      byte[] binaryForm1 = new byte[binaryLength1];
      byte[] binaryForm2 = new byte[binaryLength2];
      this.GetBinaryForm(binaryForm1, 0);
      genericAce.GetBinaryForm(binaryForm2, 0);
      for (int index = 0; index < binaryForm1.Length; ++index)
      {
        if ((int) binaryForm1[index] != (int) binaryForm2[index])
          return false;
      }
      return true;
    }

    /// <summary>用于 <see cref="T:System.Security.AccessControl.GenericAce" /> 类的一个哈希函数。<see cref="M:System.Security.AccessControl.GenericAce.GetHashCode" /> 方法适合在哈希算法和类似哈希表的数据结构中使用。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象的哈希代码。</returns>
    public override sealed int GetHashCode()
    {
      int binaryLength = this.BinaryLength;
      byte[] binaryForm = new byte[binaryLength];
      this.GetBinaryForm(binaryForm, 0);
      int num1 = 0;
      int index = 0;
      while (index < binaryLength)
      {
        int num2 = (int) binaryForm[index] + ((int) binaryForm[index + 1] << 8) + ((int) binaryForm[index + 2] << 16) + ((int) binaryForm[index + 3] << 24);
        num1 ^= num2;
        index += 4;
      }
      return num1;
    }
  }
}
