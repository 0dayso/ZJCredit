// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示一个访问控制项 (ACE)。</summary>
  public sealed class CommonAce : QualifiedAce
  {
    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.CommonAce" /> 对象的二进制表示形式的长度（以字节为单位）。将此长度用于 <see cref="M:System.Security.AccessControl.CommonAce.GetBinaryForm(System.Byte[],System.Int32)" /> 方法，以便将 ACL 封送到二进制数组中。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.CommonAce" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public override int BinaryLength
    {
      get
      {
        return 8 + this.SecurityIdentifier.BinaryLength + this.OpaqueLength;
      }
    }

    internal override int MaxOpaqueLengthInternal
    {
      get
      {
        return CommonAce.MaxOpaqueLength(this.IsCallback);
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.CommonAce" /> 类的新实例。</summary>
    /// <param name="flags">为新的访问控制项 (ACE) 指定有关继承、继承传播和审核条件的信息的标志。</param>
    /// <param name="qualifier">新的 ACE 的使用情况。</param>
    /// <param name="accessMask">ACE 的访问掩码。</param>
    /// <param name="sid">与新的 ACE 关联的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    /// <param name="isCallback">设置为 true 则指定新的 ACE 为回调类型的 ACE。</param>
    /// <param name="opaque">与新的 ACE 关联的不透明数据。只有回调 ACE 类型才允许不透明数据。此数组的长度一定不能大于 <see cref="M:System.Security.AccessControl.CommonAce.MaxOpaqueLength(System.Boolean)" /> 方法的返回值。</param>
    public CommonAce(AceFlags flags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, bool isCallback, byte[] opaque)
      : base(CommonAce.TypeFromQualifier(isCallback, qualifier), flags, accessMask, sid, opaque)
    {
    }

    private static AceType TypeFromQualifier(bool isCallback, AceQualifier qualifier)
    {
      switch (qualifier)
      {
        case AceQualifier.AccessAllowed:
          return !isCallback ? AceType.AccessAllowed : AceType.AccessAllowedCallback;
        case AceQualifier.AccessDenied:
          return !isCallback ? AceType.AccessDenied : AceType.AccessDeniedCallback;
        case AceQualifier.SystemAudit:
          return !isCallback ? AceType.SystemAudit : AceType.SystemAuditCallback;
        case AceQualifier.SystemAlarm:
          return !isCallback ? AceType.SystemAlarm : AceType.SystemAlarmCallback;
        default:
          throw new ArgumentOutOfRangeException("qualifier", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
    }

    internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out AceQualifier qualifier, out int accessMask, out SecurityIdentifier sid, out bool isCallback, out byte[] opaque)
    {
      GenericAce.VerifyHeader(binaryForm, offset);
      if (binaryForm.Length - offset >= 8 + SecurityIdentifier.MinBinaryLength)
      {
        AceType aceType = (AceType) binaryForm[offset];
        switch (aceType)
        {
          case AceType.AccessAllowed:
          case AceType.AccessDenied:
          case AceType.SystemAudit:
          case AceType.SystemAlarm:
            isCallback = false;
            break;
          case AceType.AccessAllowedCallback:
          case AceType.AccessDeniedCallback:
          case AceType.SystemAuditCallback:
          case AceType.SystemAlarmCallback:
            isCallback = true;
            break;
          default:
            goto label_18;
        }
        if (aceType == AceType.AccessAllowed || aceType == AceType.AccessAllowedCallback)
          qualifier = AceQualifier.AccessAllowed;
        else if (aceType == AceType.AccessDenied || aceType == AceType.AccessDeniedCallback)
          qualifier = AceQualifier.AccessDenied;
        else if (aceType == AceType.SystemAudit || aceType == AceType.SystemAuditCallback)
          qualifier = AceQualifier.SystemAudit;
        else if (aceType == AceType.SystemAlarm || aceType == AceType.SystemAlarmCallback)
          qualifier = AceQualifier.SystemAlarm;
        else
          goto label_18;
        int num1 = offset + 4;
        int num2 = 0;
        accessMask = (int) binaryForm[num1 + 0] + ((int) binaryForm[num1 + 1] << 8) + ((int) binaryForm[num1 + 2] << 16) + ((int) binaryForm[num1 + 3] << 24);
        int num3 = num2 + 4;
        sid = new SecurityIdentifier(binaryForm, num1 + num3);
        opaque = (byte[]) null;
        int num4 = ((int) binaryForm[offset + 3] << 8) + (int) binaryForm[offset + 2];
        if (num4 % 4 == 0)
        {
          int length = num4 - 4 - 4 - (int) (byte) sid.BinaryLength;
          if (length > 0)
          {
            opaque = new byte[length];
            for (int index = 0; index < length; ++index)
              opaque[index] = binaryForm[offset + num4 - length + index];
          }
          return true;
        }
      }
label_18:
      qualifier = AceQualifier.AccessAllowed;
      accessMask = 0;
      sid = (SecurityIdentifier) null;
      isCallback = false;
      opaque = (byte[]) null;
      return false;
    }

    /// <summary>获取回调访问控制项 (ACE) 的不透明数据 BLOB 的最大允许长度。</summary>
    /// <returns>不透明数据 BLOB 的允许长度。</returns>
    /// <param name="isCallback">设置为 true 则指定 <see cref="T:System.Security.AccessControl.CommonAce" /> 对象为回调 ACE 类型。</param>
    public static int MaxOpaqueLength(bool isCallback)
    {
      return 65527 - SecurityIdentifier.MaxBinaryLength;
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.CommonAce" /> 对象的内容从指定的偏移量开始封送到指定的字节数组中。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.CommonAce" /> 对象的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.CommonAce" /> 复制到 <paramref name="binaryForm" /> 数组。</exception>
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
      this.SecurityIdentifier.GetBinaryForm(binaryForm, num1 + num3);
      int num4 = num3 + this.SecurityIdentifier.BinaryLength;
      if (this.GetOpaque() == null)
        return;
      if (this.OpaqueLength > this.MaxOpaqueLengthInternal)
        throw new SystemException();
      this.GetOpaque().CopyTo((Array) binaryForm, num1 + num4);
    }
  }
}
