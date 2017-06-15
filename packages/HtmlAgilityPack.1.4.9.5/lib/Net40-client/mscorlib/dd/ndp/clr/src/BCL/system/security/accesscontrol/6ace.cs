// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ObjectAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>控制对目录服务对象的访问。此类表示与某个目录对象关联的访问控制项 (ACE)。</summary>
  public sealed class ObjectAce : QualifiedAce
  {
    internal static readonly int AccessMaskWithObjectType = 315;
    private ObjectAceFlags _objectFlags;
    private Guid _objectAceType;
    private Guid _inheritedObjectAceType;
    private const int ObjectFlagsLength = 4;
    private const int GuidLength = 16;

    /// <summary>获取或设置标志，这些标志指定了 <see cref="P:System.Security.AccessControl.ObjectAce.ObjectAceType" /> 和 <see cref="P:System.Security.AccessControl.ObjectAce.InheritedObjectAceType" /> 属性是否包含用于标识有效对象类型的值。</summary>
    /// <returns>使用逻辑或运算进行组合的一个或多个 <see cref="T:System.Security.AccessControl.ObjectAceFlags" /> 枚举成员。</returns>
    public ObjectAceFlags ObjectAceFlags
    {
      get
      {
        return this._objectFlags;
      }
      set
      {
        this._objectFlags = value;
      }
    }

    /// <summary>获取或设置与此 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象关联的对象类型的 GUID。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象关联的对象类型的 GUID。</returns>
    public Guid ObjectAceType
    {
      get
      {
        return this._objectAceType;
      }
      set
      {
        this._objectAceType = value;
      }
    }

    /// <summary>获取或设置对象类型的 GUID，该对象类型能够继承此 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象所表示的访问控制项 (ACE)。</summary>
    /// <returns>对象类型的 GUID，该对象类型能够继承此 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象所表示的访问控制项 (ACE)。</returns>
    public Guid InheritedObjectAceType
    {
      get
      {
        return this._inheritedObjectAceType;
      }
      set
      {
        this._inheritedObjectAceType = value;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.ObjectAce.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public override int BinaryLength
    {
      get
      {
        return 12 + (((this._objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None ? 16 : 0) + ((this._objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None ? 16 : 0)) + this.SecurityIdentifier.BinaryLength + this.OpaqueLength;
      }
    }

    internal override int MaxOpaqueLengthInternal
    {
      get
      {
        return ObjectAce.MaxOpaqueLength(this.IsCallback);
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.ObjectAce" /> 类的新实例。</summary>
    /// <param name="aceFlags">新的访问控制项 (ACE) 的继承、继承传播和审核条件。</param>
    /// <param name="qualifier">使用新的 ACE。</param>
    /// <param name="accessMask">ACE 的访问掩码。</param>
    /// <param name="sid">与新的 ACE 关联的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="flags">
    /// <paramref name="type" /> 和 <paramref name="inheritedType" /> 参数是否包含有效的对象 GUID。</param>
    /// <param name="type">一个 GUID，标识新的 ACE 所应用到的对象类型。</param>
    /// <param name="inheritedType">一个 GUID，标识能够继承新的 ACE 的对象类型。</param>
    /// <param name="isCallback">如果新的 ACE 是回调类型的 ACE，则为 true。</param>
    /// <param name="opaque">与新的 ACE 关联的不透明数据。只有回调 ACE 类型才允许不透明数据。此数组的长度一定不能大于 <see cref="M:System.Security.AccessControl.ObjectAceMaxOpaqueLength" /> 方法的返回值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">qualifier 参数包含无效的值，或者不透明参数的值的长度大于 <see cref="M:System.Security.AccessControl.ObjectAceMaxOpaqueLength" /> 方法的返回值。</exception>
    public ObjectAce(AceFlags aceFlags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, ObjectAceFlags flags, Guid type, Guid inheritedType, bool isCallback, byte[] opaque)
      : base(ObjectAce.TypeFromQualifier(isCallback, qualifier), aceFlags, accessMask, sid, opaque)
    {
      this._objectFlags = flags;
      this._objectAceType = type;
      this._inheritedObjectAceType = inheritedType;
    }

    private static AceType TypeFromQualifier(bool isCallback, AceQualifier qualifier)
    {
      switch (qualifier)
      {
        case AceQualifier.AccessAllowed:
          return !isCallback ? AceType.AccessAllowedObject : AceType.AccessAllowedCallbackObject;
        case AceQualifier.AccessDenied:
          return !isCallback ? AceType.AccessDeniedObject : AceType.AccessDeniedCallbackObject;
        case AceQualifier.SystemAudit:
          return !isCallback ? AceType.SystemAuditObject : AceType.SystemAuditCallbackObject;
        case AceQualifier.SystemAlarm:
          return !isCallback ? AceType.SystemAlarmObject : AceType.SystemAlarmCallbackObject;
        default:
          throw new ArgumentOutOfRangeException("qualifier", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
    }

    internal bool ObjectTypesMatch(ObjectAceFlags objectFlags, Guid objectType)
    {
      return (this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == (objectFlags & ObjectAceFlags.ObjectAceTypePresent) && ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || this.ObjectAceType.Equals(objectType));
    }

    internal bool InheritedObjectTypesMatch(ObjectAceFlags objectFlags, Guid inheritedObjectType)
    {
      return (this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == (objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) && ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || this.InheritedObjectAceType.Equals(inheritedObjectType));
    }

    internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out AceQualifier qualifier, out int accessMask, out SecurityIdentifier sid, out ObjectAceFlags objectFlags, out Guid objectAceType, out Guid inheritedObjectAceType, out bool isCallback, out byte[] opaque)
    {
      byte[] b = new byte[16];
      GenericAce.VerifyHeader(binaryForm, offset);
      if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
      {
        AceType aceType = (AceType) binaryForm[offset];
        switch (aceType)
        {
          case AceType.AccessAllowedObject:
          case AceType.AccessDeniedObject:
          case AceType.SystemAuditObject:
          case AceType.SystemAlarmObject:
            isCallback = false;
            break;
          case AceType.AccessAllowedCallbackObject:
          case AceType.AccessDeniedCallbackObject:
          case AceType.SystemAuditCallbackObject:
          case AceType.SystemAlarmCallbackObject:
            isCallback = true;
            break;
          default:
            goto label_38;
        }
        if (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessAllowedCallbackObject)
          qualifier = AceQualifier.AccessAllowed;
        else if (aceType == AceType.AccessDeniedObject || aceType == AceType.AccessDeniedCallbackObject)
          qualifier = AceQualifier.AccessDenied;
        else if (aceType == AceType.SystemAuditObject || aceType == AceType.SystemAuditCallbackObject)
          qualifier = AceQualifier.SystemAudit;
        else if (aceType == AceType.SystemAlarmObject || aceType == AceType.SystemAlarmCallbackObject)
          qualifier = AceQualifier.SystemAlarm;
        else
          goto label_38;
        int num1 = offset + 4;
        int num2 = 0;
        accessMask = (int) binaryForm[num1 + 0] + ((int) binaryForm[num1 + 1] << 8) + ((int) binaryForm[num1 + 2] << 16) + ((int) binaryForm[num1 + 3] << 24);
        int num3 = num2 + 4;
        objectFlags = (ObjectAceFlags) ((int) binaryForm[num1 + num3 + 0] + ((int) binaryForm[num1 + num3 + 1] << 8) + ((int) binaryForm[num1 + num3 + 2] << 16) + ((int) binaryForm[num1 + num3 + 3] << 24));
        int num4 = num3 + 4;
        if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
        {
          for (int index = 0; index < 16; ++index)
            b[index] = binaryForm[num1 + num4 + index];
          num4 += 16;
        }
        else
        {
          for (int index = 0; index < 16; ++index)
            b[index] = (byte) 0;
        }
        objectAceType = new Guid(b);
        if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
        {
          for (int index = 0; index < 16; ++index)
            b[index] = binaryForm[num1 + num4 + index];
          num4 += 16;
        }
        else
        {
          for (int index = 0; index < 16; ++index)
            b[index] = (byte) 0;
        }
        inheritedObjectAceType = new Guid(b);
        sid = new SecurityIdentifier(binaryForm, num1 + num4);
        opaque = (byte[]) null;
        int num5 = ((int) binaryForm[offset + 3] << 8) + (int) binaryForm[offset + 2];
        if (num5 % 4 == 0)
        {
          int length = num5 - 4 - 4 - 4 - (int) (byte) sid.BinaryLength;
          if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
            length -= 16;
          if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
            length -= 16;
          if (length > 0)
          {
            opaque = new byte[length];
            for (int index = 0; index < length; ++index)
              opaque[index] = binaryForm[offset + num5 - length + index];
          }
          return true;
        }
      }
label_38:
      qualifier = AceQualifier.AccessAllowed;
      accessMask = 0;
      sid = (SecurityIdentifier) null;
      objectFlags = ObjectAceFlags.None;
      objectAceType = Guid.NewGuid();
      inheritedObjectAceType = Guid.NewGuid();
      isCallback = false;
      opaque = (byte[]) null;
      return false;
    }

    /// <summary>返回回调访问控制项 (ACE) 的不透明数据 BLOB 的最大允许长度（以字节为单位）。</summary>
    /// <returns>回调访问控制项 (ACE) 的不透明数据 BLOB 的最大允许长度（以字节为单位）。</returns>
    /// <param name="isCallback">如果 <see cref="T:System.Security.AccessControl.ObjectAce" /> 为回调 ACE 类型，则为 true。</param>
    public static int MaxOpaqueLength(bool isCallback)
    {
      return 65491 - SecurityIdentifier.MaxBinaryLength;
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.ObjectAce" /> 对象的内容从指定的偏移量开始封送到指定的字节数组中。</summary>
    /// <param name="binaryForm">
    /// <see cref="T:System.Security.AccessControl.ObjectAce" /> 的内容将被封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果值为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.ObjectAce" /> 复制到 <paramref name="array" />。</exception>
    public override void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.MarshalHeader(binaryForm, offset);
      int num1 = offset + 4;
      int num2 = 0;
      binaryForm[num1 + 0] = (byte) this.AccessMask;
      binaryForm[num1 + 1] = (byte) (this.AccessMask >> 8);
      binaryForm[num1 + 2] = (byte) (this.AccessMask >> 16);
      binaryForm[num1 + 3] = (byte) (this.AccessMask >> 24);
      int num3 = num2 + 4;
      binaryForm[num1 + num3 + 0] = (byte) this.ObjectAceFlags;
      binaryForm[num1 + num3 + 1] = (byte) ((uint) this.ObjectAceFlags >> 8);
      binaryForm[num1 + num3 + 2] = (byte) ((uint) this.ObjectAceFlags >> 16);
      binaryForm[num1 + num3 + 3] = (byte) ((uint) this.ObjectAceFlags >> 24);
      int num4 = num3 + 4;
      Guid guid;
      if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
      {
        guid = this.ObjectAceType;
        guid.ToByteArray().CopyTo((Array) binaryForm, num1 + num4);
        num4 += 16;
      }
      if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
      {
        guid = this.InheritedObjectAceType;
        guid.ToByteArray().CopyTo((Array) binaryForm, num1 + num4);
        num4 += 16;
      }
      this.SecurityIdentifier.GetBinaryForm(binaryForm, num1 + num4);
      int num5 = num4 + this.SecurityIdentifier.BinaryLength;
      if (this.GetOpaque() == null)
        return;
      if (this.OpaqueLength > this.MaxOpaqueLengthInternal)
        throw new SystemException();
      this.GetOpaque().CopyTo((Array) binaryForm, num1 + num5);
    }
  }
}
