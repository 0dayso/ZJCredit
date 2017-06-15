// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CompoundAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示复合访问控制项 (ACE)。</summary>
  public sealed class CompoundAce : KnownAce
  {
    private CompoundAceType _compoundAceType;
    private const int AceTypeLength = 4;

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.CompoundAce" /> 对象的类型。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.CompoundAce" /> 对象的类型。</returns>
    public CompoundAceType CompoundAceType
    {
      get
      {
        return this._compoundAceType;
      }
      set
      {
        this._compoundAceType = value;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.CompoundAce" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.CompoundAce.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.CompoundAce" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public override int BinaryLength
    {
      get
      {
        return 12 + this.SecurityIdentifier.BinaryLength;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.CompoundAce" /> 类的新实例。</summary>
    /// <param name="flags">包含标志，这些标志为新的访问控制项 (ACE) 指定有关继承、继承传播和审核条件的信息。</param>
    /// <param name="accessMask">ACE 的访问掩码。</param>
    /// <param name="compoundAceType">
    /// <see cref="T:System.Security.AccessControl.CompoundAceType" /> 枚举中的一个值。</param>
    /// <param name="sid">与新的 ACE 关联的 <see cref="T:System.Security.Principal.SecurityIdentifier" />。</param>
    public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid)
      : base(AceType.AccessAllowedCompound, flags, accessMask, sid)
    {
      this._compoundAceType = compoundAceType;
    }

    internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out int accessMask, out CompoundAceType compoundAceType, out SecurityIdentifier sid)
    {
      GenericAce.VerifyHeader(binaryForm, offset);
      if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
      {
        int num1 = offset + 4;
        int num2 = 0;
        accessMask = (int) binaryForm[num1 + 0] + ((int) binaryForm[num1 + 1] << 8) + ((int) binaryForm[num1 + 2] << 16) + ((int) binaryForm[num1 + 3] << 24);
        int num3 = num2 + 4;
        compoundAceType = (CompoundAceType) ((int) binaryForm[num1 + num3 + 0] + ((int) binaryForm[num1 + num3 + 1] << 8));
        int num4 = num3 + 4;
        sid = new SecurityIdentifier(binaryForm, num1 + num4);
        return true;
      }
      accessMask = 0;
      compoundAceType = (CompoundAceType) 0;
      sid = (SecurityIdentifier) null;
      return false;
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.CompoundAce" /> 对象的内容封送到指定字节数组中，其位置从指定的偏移量开始。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.CompoundAce" /> 的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.CompoundAce" /> 复制到 <paramref name="array" />。</exception>
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
      binaryForm[num1 + num3 + 0] = (byte) (ushort) this.CompoundAceType;
      binaryForm[num1 + num3 + 1] = (byte) ((uint) (ushort) this.CompoundAceType >> 8);
      binaryForm[num1 + num3 + 2] = (byte) 0;
      binaryForm[num1 + num3 + 3] = (byte) 0;
      int num4 = num3 + 4;
      this.SecurityIdentifier.GetBinaryForm(binaryForm, num1 + num4);
    }
  }
}
