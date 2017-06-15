// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.SecurityIdentifier
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Principal
{
  /// <summary>表示安全标识符 (SID)，并提供 SID 的封送和比较操作。</summary>
  [ComVisible(false)]
  public sealed class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
  {
    internal static readonly long MaxIdentifierAuthority = 281474976710655;
    internal static readonly byte MaxSubAuthorities = 15;
    /// <summary>返回安全标识符的二进制表示形式的最小大小（以字节为单位）。</summary>
    public static readonly int MinBinaryLength = 8;
    /// <summary>返回安全标识符的二进制表示形式的最大大小（以字节为单位）。</summary>
    public static readonly int MaxBinaryLength = 8 + (int) SecurityIdentifier.MaxSubAuthorities * 4;
    private IdentifierAuthority _IdentifierAuthority;
    private int[] _SubAuthorities;
    private byte[] _BinaryForm;
    private SecurityIdentifier _AccountDomainSid;
    private bool _AccountDomainSidInitialized;
    private string _SddlForm;

    internal static byte Revision
    {
      get
      {
        return 1;
      }
    }

    internal byte[] BinaryForm
    {
      get
      {
        return this._BinaryForm;
      }
    }

    internal IdentifierAuthority IdentifierAuthority
    {
      get
      {
        return this._IdentifierAuthority;
      }
    }

    internal int SubAuthorityCount
    {
      get
      {
        return this._SubAuthorities.Length;
      }
    }

    /// <summary>返回由 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的安全标识符 (SID) 的长度（以字节为单位）。</summary>
    /// <returns>由 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的 SID 的长度（以字节为单位）。</returns>
    public int BinaryLength
    {
      get
      {
        return this._BinaryForm.Length;
      }
    }

    /// <summary>如果由 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的 SID 表示 Windows 帐户 SID，则从该 SID 返回帐户域安全标识符 (SID) 部分。如果该 SID 不表示 Windows 帐户 SID，则此属性返回 <see cref="T:System.ArgumentNullException" />。</summary>
    /// <returns>如果由 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的 SID 表示 Windows 帐户 SID，则从该 SID 返回帐户域 SID 部分；否则，它返回 <see cref="T:System.ArgumentNullException" />。</returns>
    public SecurityIdentifier AccountDomainSid
    {
      [SecuritySafeCritical] get
      {
        if (!this._AccountDomainSidInitialized)
        {
          this._AccountDomainSid = this.GetAccountDomainSid();
          this._AccountDomainSidInitialized = true;
        }
        return this._AccountDomainSid;
      }
    }

    /// <summary>返回由此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的安全标识符 (SID) 的安全说明符定义语言 (SDDL) 字符串（全部大写）。</summary>
    /// <returns>由 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的 SID 的 SDDL 字符串（全部大写）。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string Value
    {
      get
      {
        return this.ToString().ToUpper(CultureInfo.InvariantCulture);
      }
    }

    /// <summary>使用安全说明符定义语言 (SDDL) 格式的指定安全标识符 (SID) 初始化 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的新实例。</summary>
    /// <param name="sddlForm">用于创建 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的 SID 的 SDDL 字符串。</param>
    [SecuritySafeCritical]
    public SecurityIdentifier(string sddlForm)
    {
      if (sddlForm == null)
        throw new ArgumentNullException("sddlForm");
      byte[] resultSid;
      int sidFromString = System.Security.Principal.Win32.CreateSidFromString(sddlForm, out resultSid);
      switch (sidFromString)
      {
        case 1337:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "sddlForm");
        case 8:
          throw new OutOfMemoryException();
        case 0:
          this.CreateFromBinaryForm(resultSid, 0);
          break;
        default:
          throw new SystemException(Win32Native.GetMessage(sidFromString));
      }
    }

    /// <summary>使用安全标识符 (SID) 的指定二进制表示形式初始化 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的新实例。</summary>
    /// <param name="binaryForm">表示 SID 的字节数组。</param>
    /// <param name="offset">要用作 <paramref name="binaryForm" /> 中的起始索引的字节偏移量。</param>
    public SecurityIdentifier(byte[] binaryForm, int offset)
    {
      this.CreateFromBinaryForm(binaryForm, offset);
    }

    /// <summary>使用表示安全标识符 (SID) 二进制形式的整数，初始化 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的新实例。</summary>
    /// <param name="binaryForm">表示 SID 的二进制形式的整数。</param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public SecurityIdentifier(IntPtr binaryForm)
      : this(binaryForm, true)
    {
    }

    [SecurityCritical]
    internal SecurityIdentifier(IntPtr binaryForm, bool noDemand)
      : this(System.Security.Principal.Win32.ConvertIntPtrSidToByteArraySid(binaryForm), 0)
    {
    }

    /// <summary>使用指定的已知安全标识符 (SID) 类型和域 SID 初始化 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的新实例。</summary>
    /// <param name="sidType">枚举值之一。此值不得为 <see cref="F:System.Security.Principal.WellKnownSidType.LogonIdsSid" />。</param>
    /// <param name="domainSid">域 SID。以下 <see cref="T:System.Security.Principal.WellKnownSidType" /> 值需要此值。任何其他 <see cref="T:System.Security.Principal.WellKnownSidType" /> 值都忽略此参数。- <see cref="F:System.Security.Principal.WellKnownSidType.AccountAdministratorSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountGuestSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountKrbtgtSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainUsersSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainGuestsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountComputersSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountControllersSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountCertAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountSchemaAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountEnterpriseAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountPolicyAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountRasAndIasServersSid" /></param>
    [SecuritySafeCritical]
    public SecurityIdentifier(WellKnownSidType sidType, SecurityIdentifier domainSid)
    {
      if (sidType == WellKnownSidType.LogonIdsSid)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_CannotCreateLogonIdsSid"), "sidType");
      if (!System.Security.Principal.Win32.WellKnownSidApisSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
      if (sidType < WellKnownSidType.NullSid || sidType > WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "sidType");
      if (sidType >= WellKnownSidType.AccountAdministratorSid && sidType <= WellKnownSidType.AccountRasAndIasServersSid)
      {
        if (domainSid == (SecurityIdentifier) null)
          throw new ArgumentNullException("domainSid", Environment.GetResourceString("IdentityReference_DomainSidRequired", (object) sidType));
        SecurityIdentifier resultSid;
        int accountDomainSid = System.Security.Principal.Win32.GetWindowsAccountDomainSid(domainSid, out resultSid);
        switch (accountDomainSid)
        {
          case 122:
            throw new OutOfMemoryException();
          case 1257:
            throw new ArgumentException(Environment.GetResourceString("IdentityReference_NotAWindowsDomain"), "domainSid");
          case 0:
            if (resultSid != domainSid)
              throw new ArgumentException(Environment.GetResourceString("IdentityReference_NotAWindowsDomain"), "domainSid");
            break;
          default:
            throw new SystemException(Win32Native.GetMessage(accountDomainSid));
        }
      }
      byte[] resultSid1;
      int wellKnownSid = System.Security.Principal.Win32.CreateWellKnownSid(sidType, domainSid, out resultSid1);
      switch (wellKnownSid)
      {
        case 87:
          throw new ArgumentException(Win32Native.GetMessage(wellKnownSid), "sidType/domainSid");
        case 0:
          this.CreateFromBinaryForm(resultSid1, 0);
          break;
        default:
          throw new SystemException(Win32Native.GetMessage(wellKnownSid));
      }
    }

    internal SecurityIdentifier(SecurityIdentifier domainSid, uint rid)
    {
      int[] subAuthorities = new int[domainSid.SubAuthorityCount + 1];
      int index;
      for (index = 0; index < domainSid.SubAuthorityCount; ++index)
        subAuthorities[index] = domainSid.GetSubAuthority(index);
      subAuthorities[index] = (int) rid;
      this.CreateFromParts(domainSid.IdentifierAuthority, subAuthorities);
    }

    internal SecurityIdentifier(IdentifierAuthority identifierAuthority, int[] subAuthorities)
    {
      this.CreateFromParts(identifierAuthority, subAuthorities);
    }

    /// <summary>比较两个 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象以确定它们是否相等。如果这两个对象具有与 <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> 属性返回的规范表示形式相同的规范表示形式，或是二者都为 null，则将它们视为相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">用于相等比较的左操作数。此参数可以为 null。</param>
    /// <param name="right">用于相等比较的右操作数。此参数可以为 null。</param>
    public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.CompareTo(right) == 0;
    }

    /// <summary>比较两个 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象以确定它们是否不相等。如果二者的规范名称表示形式与 <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> 属性返回的表示形式不同，或其中一个对象为 null 而另一个对象不是，它们将被视为不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 与 <paramref name="right" /> 不相等，则为 true；否则为 false。</returns>
    /// <param name="left">用于不相等比较的左操作数。此参数可以为 null。</param>
    /// <param name="right">用于不相等比较的右操作数。此参数可以为 null。</param>
    public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right)
    {
      return !(left == right);
    }

    private void CreateFromParts(IdentifierAuthority identifierAuthority, int[] subAuthorities)
    {
      if (subAuthorities == null)
        throw new ArgumentNullException("subAuthorities");
      if (subAuthorities.Length > (int) SecurityIdentifier.MaxSubAuthorities)
        throw new ArgumentOutOfRangeException("subAuthorities.Length", (object) subAuthorities.Length, Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", (object) SecurityIdentifier.MaxSubAuthorities));
      if (identifierAuthority < IdentifierAuthority.NullAuthority || identifierAuthority > (IdentifierAuthority) SecurityIdentifier.MaxIdentifierAuthority)
        throw new ArgumentOutOfRangeException("identifierAuthority", (object) identifierAuthority, Environment.GetResourceString("IdentityReference_IdentifierAuthorityTooLarge"));
      this._IdentifierAuthority = identifierAuthority;
      this._SubAuthorities = new int[subAuthorities.Length];
      subAuthorities.CopyTo((Array) this._SubAuthorities, 0);
      this._BinaryForm = new byte[8 + 4 * this.SubAuthorityCount];
      this._BinaryForm[0] = SecurityIdentifier.Revision;
      this._BinaryForm[1] = (byte) this.SubAuthorityCount;
      for (byte index = 0; (int) index < 6; ++index)
        this._BinaryForm[2 + (int) index] = (byte) ((ulong) this._IdentifierAuthority >> (5 - (int) index) * 8 & (ulong) byte.MaxValue);
      for (byte index1 = 0; (int) index1 < this.SubAuthorityCount; ++index1)
      {
        for (byte index2 = 0; (int) index2 < 4; ++index2)
          this._BinaryForm[8 + 4 * (int) index1 + (int) index2] = (byte) ((ulong) this._SubAuthorities[(int) index1] >> (int) index2 * 8);
      }
    }

    private void CreateFromBinaryForm(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", (object) offset, Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < SecurityIdentifier.MinBinaryLength)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if ((int) binaryForm[offset] != (int) SecurityIdentifier.Revision)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), "binaryForm");
      if ((int) binaryForm[offset + 1] > (int) SecurityIdentifier.MaxSubAuthorities)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", (object) SecurityIdentifier.MaxSubAuthorities), "binaryForm");
      int num = 8 + 4 * (int) binaryForm[offset + 1];
      if (binaryForm.Length - offset < num)
        throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"), "binaryForm");
      IdentifierAuthority identifierAuthority = (IdentifierAuthority) (((long) binaryForm[offset + 2] << 40) + ((long) binaryForm[offset + 3] << 32) + ((long) binaryForm[offset + 4] << 24) + ((long) binaryForm[offset + 5] << 16) + ((long) binaryForm[offset + 6] << 8) + (long) binaryForm[offset + 7]);
      int[] subAuthorities = new int[(int) binaryForm[offset + 1]];
      for (byte index = 0; (int) index < (int) binaryForm[offset + 1]; ++index)
        subAuthorities[(int) index] = (int) binaryForm[offset + 8 + 4 * (int) index + 0] + ((int) binaryForm[offset + 8 + 4 * (int) index + 1] << 8) + ((int) binaryForm[offset + 8 + 4 * (int) index + 2] << 16) + ((int) binaryForm[offset + 8 + 4 * (int) index + 3] << 24);
      this.CreateFromParts(identifierAuthority, subAuthorities);
    }

    /// <summary>返回一个值，该值指示此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="o" /> 是与此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象有相同基础类型和值的对象，则为 true；否则为 false。</returns>
    /// <param name="o">要与此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象进行比较的对象，或 null。</param>
    public override bool Equals(object o)
    {
      if (o == null)
        return false;
      SecurityIdentifier securityIdentifier = o as SecurityIdentifier;
      if (securityIdentifier == (SecurityIdentifier) null)
        return false;
      return this == securityIdentifier;
    }

    /// <summary>指示指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象是否等于当前的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象。</summary>
    /// <returns>如果 <paramref name="sid" /> 的值和当前 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的值相等，则为 true。</returns>
    /// <param name="sid">要与当前对象进行比较的对象。</param>
    public bool Equals(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        return false;
      return this == sid;
    }

    /// <summary>用作当前 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的哈希函数。<see cref="M:System.Security.Principal.SecurityIdentifier.GetHashCode" /> 方法适合于哈希算法和诸如哈希表的数据结构。</summary>
    /// <returns>当前 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的哈希值。</returns>
    public override int GetHashCode()
    {
      int hashCode = ((long) this.IdentifierAuthority).GetHashCode();
      for (int index = 0; index < this.SubAuthorityCount; ++index)
        hashCode ^= this.GetSubAuthority(index);
      return hashCode;
    }

    /// <summary>以安全说明符定义语言 (SDDL) 格式返回 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的帐户的安全标识符 (SID)。S-1-5-9 就是一个 SDDL 格式。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的帐户的 SID（SDDL 格式）。</returns>
    public override string ToString()
    {
      if (this._SddlForm == null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("S-1-{0}", (object) this._IdentifierAuthority);
        for (int index = 0; index < this.SubAuthorityCount; ++index)
          stringBuilder.AppendFormat("-{0}", (object) (uint) this._SubAuthorities[index]);
        this._SddlForm = stringBuilder.ToString();
      }
      return this._SddlForm;
    }

    internal static bool IsValidTargetTypeStatic(Type targetType)
    {
      return targetType == typeof (NTAccount) || targetType == typeof (SecurityIdentifier);
    }

    /// <summary>返回一个值，该值指示指定类型是否为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的有效转换类型。</summary>
    /// <returns>如果 <paramref name="targetType" /> 为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的有效转换类型，则为 true；否则为 false。</returns>
    /// <param name="targetType">查询其能否作为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 的有效转换类型的类型。以下目标类型是有效的：- <see cref="T:System.Security.Principal.NTAccount" />- <see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
    public override bool IsValidTargetType(Type targetType)
    {
      return SecurityIdentifier.IsValidTargetTypeStatic(targetType);
    }

    [SecurityCritical]
    internal SecurityIdentifier GetAccountDomainSid()
    {
      SecurityIdentifier resultSid;
      int accountDomainSid = System.Security.Principal.Win32.GetWindowsAccountDomainSid(this, out resultSid);
      switch (accountDomainSid)
      {
        case 122:
          throw new OutOfMemoryException();
        case 1257:
          resultSid = (SecurityIdentifier) null;
          goto case 0;
        case 0:
          return resultSid;
        default:
          throw new SystemException(Win32Native.GetMessage(accountDomainSid));
      }
    }

    /// <summary>返回一个值，该值指示由此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的安全标识符 (SID) 是否为有效的 Windows 帐户 SID。</summary>
    /// <returns>如果由此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的 SID 为有效 Windows 帐户 SID，则为 true；否则为 false。</returns>
    [SecuritySafeCritical]
    public bool IsAccountSid()
    {
      if (!this._AccountDomainSidInitialized)
      {
        this._AccountDomainSid = this.GetAccountDomainSid();
        this._AccountDomainSidInitialized = true;
      }
      return !(this._AccountDomainSid == (SecurityIdentifier) null);
    }

    /// <summary>将 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的帐户名转换为另一 <see cref="T:System.Security.Principal.IdentityReference" /> 派生的类型。</summary>
    /// <returns>转换后的标识。</returns>
    /// <param name="targetType">从 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 进行的转换的目标类型。目标类型必须为由 <see cref="M:System.Security.Principal.SecurityIdentifier.IsValidTargetType(System.Type)" /> 方法视为有效的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="targetType " />为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="targetType " /> 不是 <see cref="T:System.Security.Principal.IdentityReference" /> 类型。</exception>
    /// <exception cref="T:System.Security.Principal.IdentityNotMappedException">未能转换部分或所有标识引用。</exception>
    /// <exception cref="T:System.SystemException">返回了 Win32 错误。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public override IdentityReference Translate(Type targetType)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException("targetType");
      if (targetType == typeof (SecurityIdentifier))
        return (IdentityReference) this;
      if (!(targetType == typeof (NTAccount)))
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
      IdentityReferenceCollection sourceSids = new IdentityReferenceCollection(1);
      sourceSids.Add((IdentityReference) this);
      Type targetType1 = targetType;
      int num = 1;
      return SecurityIdentifier.Translate(sourceSids, targetType1, num != 0)[0];
    }

    /// <summary>用指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象同当前 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象进行比较。</summary>
    /// <returns>一个有符号数字，指示此实例和 <paramref name="sid" /> 的相对值。返回值说明小于零此实例小于 <paramref name="sid" />。零此实例等于 <paramref name="sid" />。大于零此实例大于 <paramref name="sid" />。</returns>
    /// <param name="sid">要与当前对象进行比较的对象。</param>
    public int CompareTo(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      if (this.IdentifierAuthority < sid.IdentifierAuthority)
        return -1;
      if (this.IdentifierAuthority > sid.IdentifierAuthority)
        return 1;
      if (this.SubAuthorityCount < sid.SubAuthorityCount)
        return -1;
      if (this.SubAuthorityCount > sid.SubAuthorityCount)
        return 1;
      for (int index = 0; index < this.SubAuthorityCount; ++index)
      {
        int num = this.GetSubAuthority(index) - sid.GetSubAuthority(index);
        if (num != 0)
          return num;
      }
      return 0;
    }

    internal int GetSubAuthority(int index)
    {
      return this._SubAuthorities[index];
    }

    /// <summary>返回一个值，该值指示 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象是否与指定的已知安全标识符 (SID) 类型匹配。</summary>
    /// <returns>如果 <paramref name="type" /> 为 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象的 SID 类型，则为 true；否则为 false。</returns>
    /// <param name="type">一个要与 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象进行比较的值。</param>
    [SecuritySafeCritical]
    public bool IsWellKnown(WellKnownSidType type)
    {
      return System.Security.Principal.Win32.IsWellKnownSid(this, type);
    }

    /// <summary>将 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类表示的指定安全标识符 (SID) 的二进制表示形式复制到一个字节数组。</summary>
    /// <param name="binaryForm">要接收复制的 SID 的字节数组。</param>
    /// <param name="offset">要用作 <paramref name="binaryForm" /> 中的起始索引的字节偏移量。</param>
    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this._BinaryForm.CopyTo((Array) binaryForm, offset);
    }

    /// <summary>返回一个值，该值指示由此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的安全标识符 (SID) 是否与指定 SID 同属一个域。</summary>
    /// <returns>如果由此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象表示的 SID 与 <paramref name="sid" /> SID 同属一个域，则为 true；否则为 false。</returns>
    /// <param name="sid">与此 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象进行比较的 SID。</param>
    [SecuritySafeCritical]
    public bool IsEqualDomainSid(SecurityIdentifier sid)
    {
      return System.Security.Principal.Win32.IsEqualDomainSid(this, sid);
    }

    [SecurityCritical]
    private static IdentityReferenceCollection TranslateToNTAccounts(IdentityReferenceCollection sourceSids, out bool someFailed)
    {
      if (sourceSids == null)
        throw new ArgumentNullException("sourceSids");
      if (sourceSids.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), "sourceSids");
      IntPtr[] sids = new IntPtr[sourceSids.Count];
      GCHandle[] gcHandleArray = new GCHandle[sourceSids.Count];
      SafeLsaPolicyHandle handle = SafeLsaPolicyHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle1 = SafeLsaMemoryHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
      try
      {
        int index1 = 0;
        foreach (IdentityReference sourceSid in sourceSids)
        {
          SecurityIdentifier securityIdentifier = sourceSid as SecurityIdentifier;
          if (securityIdentifier == (SecurityIdentifier) null)
            throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), "sourceSids");
          gcHandleArray[index1] = GCHandle.Alloc((object) securityIdentifier.BinaryForm, GCHandleType.Pinned);
          sids[index1] = gcHandleArray[index1].AddrOfPinnedObject();
          ++index1;
        }
        handle = System.Security.Principal.Win32.LsaOpenPolicy((string) null, PolicyRights.POLICY_LOOKUP_NAMES);
        someFailed = false;
        uint num = Win32Native.LsaLookupSids(handle, sourceSids.Count, sids, ref invalidHandle1, ref invalidHandle2);
        switch (num)
        {
          case 3221225495:
          case 3221225626:
            throw new OutOfMemoryException();
          case 3221225506:
            throw new UnauthorizedAccessException();
          case 3221225587:
          case 263:
            someFailed = true;
            goto case 0;
          case 0:
            invalidHandle2.Initialize((uint) sourceSids.Count, (uint) Marshal.SizeOf(typeof (Win32Native.LSA_TRANSLATED_NAME)));
            System.Security.Principal.Win32.InitializeReferencedDomainsPointer(invalidHandle1);
            IdentityReferenceCollection referenceCollection = new IdentityReferenceCollection(sourceSids.Count);
            if ((int) num == 0 || (int) num == 263)
            {
              Win32Native.LSA_REFERENCED_DOMAIN_LIST referencedDomainList = invalidHandle1.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
              string[] strArray = new string[referencedDomainList.Entries];
              for (int index2 = 0; index2 < referencedDomainList.Entries; ++index2)
              {
                Win32Native.LSA_TRUST_INFORMATION trustInformation = (Win32Native.LSA_TRUST_INFORMATION) Marshal.PtrToStructure(new IntPtr((long) referencedDomainList.Domains + (long) (index2 * Marshal.SizeOf(typeof (Win32Native.LSA_TRUST_INFORMATION)))), typeof (Win32Native.LSA_TRUST_INFORMATION));
                strArray[index2] = Marshal.PtrToStringUni(trustInformation.Name.Buffer, (int) trustInformation.Name.Length / 2);
              }
              Win32Native.LSA_TRANSLATED_NAME[] array = new Win32Native.LSA_TRANSLATED_NAME[sourceSids.Count];
              invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_NAME>(0UL, array, 0, array.Length);
              for (int index2 = 0; index2 < sourceSids.Count; ++index2)
              {
                Win32Native.LSA_TRANSLATED_NAME lsaTranslatedName = array[index2];
                switch (lsaTranslatedName.Use)
                {
                  case 1:
                  case 2:
                  case 4:
                  case 5:
                  case 9:
                    string stringUni = Marshal.PtrToStringUni(lsaTranslatedName.Name.Buffer, (int) lsaTranslatedName.Name.Length / 2);
                    string domainName = strArray[lsaTranslatedName.DomainIndex];
                    referenceCollection.Add((IdentityReference) new NTAccount(domainName, stringUni));
                    break;
                  default:
                    someFailed = true;
                    referenceCollection.Add(sourceSids[index2]);
                    break;
                }
              }
            }
            else
            {
              for (int index2 = 0; index2 < sourceSids.Count; ++index2)
                referenceCollection.Add(sourceSids[index2]);
            }
            return referenceCollection;
          default:
            throw new SystemException(Win32Native.GetMessage(Win32Native.LsaNtStatusToWinError((int) num)));
        }
      }
      finally
      {
        for (int index = 0; index < sourceSids.Count; ++index)
        {
          if (gcHandleArray[index].IsAllocated)
            gcHandleArray[index].Free();
        }
        handle.Dispose();
        invalidHandle1.Dispose();
        invalidHandle2.Dispose();
      }
    }

    [SecurityCritical]
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceSids, Type targetType, bool forceSuccess)
    {
      bool someFailed = false;
      IdentityReferenceCollection referenceCollection = SecurityIdentifier.Translate(sourceSids, targetType, out someFailed);
      if (forceSuccess & someFailed)
      {
        IdentityReferenceCollection unmappedIdentities = new IdentityReferenceCollection();
        foreach (IdentityReference identity in referenceCollection)
        {
          if (identity.GetType() != targetType)
            unmappedIdentities.Add(identity);
        }
        throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), unmappedIdentities);
      }
      return referenceCollection;
    }

    [SecurityCritical]
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceSids, Type targetType, out bool someFailed)
    {
      if (sourceSids == null)
        throw new ArgumentNullException("sourceSids");
      if (targetType == typeof (NTAccount))
        return SecurityIdentifier.TranslateToNTAccounts(sourceSids, out someFailed);
      throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
    }
  }
}
