// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.NTAccount
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Principal
{
  /// <summary>表示用户或组帐户。</summary>
  [ComVisible(false)]
  public sealed class NTAccount : IdentityReference
  {
    private readonly string _Name;
    internal const int MaximumAccountNameLength = 256;
    internal const int MaximumDomainNameLength = 255;

    /// <summary>返回此 <see cref="T:System.Security.Principal.NTAccount" /> 对象的大写字符串表示形式。</summary>
    /// <returns>此 <see cref="T:System.Security.Principal.NTAccount" /> 对象的大写字符串表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string Value
    {
      get
      {
        return this.ToString();
      }
    }

    /// <summary>使用指定的域名和帐户名初始化 <see cref="T:System.Security.Principal.NTAccount" /> 类的新实例。</summary>
    /// <param name="domainName">域的名称。此参数可以为 null 或空字符串。为 null 值的域名如同空字符串一样处理。</param>
    /// <param name="accountName">帐户的名称。此参数不能为 null 或空字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="accountName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="accountName" /> 是空字符串。- 或 -<paramref name="accountName" /> 过长。- 或 -<paramref name="domainName" /> 过长。</exception>
    public NTAccount(string domainName, string accountName)
    {
      if (accountName == null)
        throw new ArgumentNullException("accountName");
      if (accountName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "accountName");
      if (accountName.Length > 256)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), "accountName");
      if (domainName != null && domainName.Length > (int) byte.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_DomainNameTooLong"), "domainName");
      if (domainName == null || domainName.Length == 0)
        this._Name = accountName;
      else
        this._Name = domainName + "\\" + accountName;
    }

    /// <summary>使用指定的名称初始化 <see cref="T:System.Security.Principal.NTAccount" /> 类的新实例。</summary>
    /// <param name="name">用于创建 <see cref="T:System.Security.Principal.NTAccount" /> 对象的名称。此参数不能为 null 或空字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是空字符串。- 或 -<paramref name="name" /> 过长。</exception>
    public NTAccount(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "name");
      if (name.Length > 512)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), "name");
      this._Name = name;
    }

    /// <summary>比较两个 <see cref="T:System.Security.Principal.NTAccount" /> 对象以确定它们是否相等。如果这两个对象具有与 <see cref="P:System.Security.Principal.NTAccount.Value" /> 属性返回的规范名称表示形式相同的规范名称表示形式，或是都为 null，则将它们视为相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">用于相等比较的左操作数。此参数可以为 null。</param>
    /// <param name="right">用于相等比较的右操作数。此参数可以为 null。</param>
    public static bool operator ==(NTAccount left, NTAccount right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.ToString().Equals(right.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>比较两个 <see cref="T:System.Security.Principal.NTAccount" /> 对象以确定它们是否不相等。如果它们的规范名称表示形式与 <see cref="P:System.Security.Principal.NTAccount.Value" /> 属性返回的表示形式不同，或其中一个对象为 null 而另一个对象不是，它们将被视为不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 不相等，则为 true；否则为 false。</returns>
    /// <param name="left">用于不相等比较的左操作数。此参数可以为 null。</param>
    /// <param name="right">用于不相等比较的右操作数。此参数可以为 null。</param>
    public static bool operator !=(NTAccount left, NTAccount right)
    {
      return !(left == right);
    }

    /// <summary>返回一个值，该值指示指定类型是否为 <see cref="T:System.Security.Principal.NTAccount" /> 类的有效转换类型。</summary>
    /// <returns>如果 <paramref name="targetType" /> 为 <see cref="T:System.Security.Principal.NTAccount" /> 类的有效转换类型，则为 true；否则为 false。</returns>
    /// <param name="targetType">查询其能否作为 <see cref="T:System.Security.Principal.NTAccount" /> 的有效转换类型的类型。以下目标类型是有效的：- <see cref="T:System.Security.Principal.NTAccount" />- <see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
    public override bool IsValidTargetType(Type targetType)
    {
      return targetType == typeof (SecurityIdentifier) || targetType == typeof (NTAccount);
    }

    /// <summary>将 <see cref="T:System.Security.Principal.NTAccount" /> 对象表示的帐户名转换为另一 <see cref="T:System.Security.Principal.IdentityReference" /> 派生的类型。</summary>
    /// <returns>转换后的标识。</returns>
    /// <param name="targetType">从 <see cref="T:System.Security.Principal.NTAccount" /> 进行的转换的目标类型。目标类型必须为由 <see cref="M:System.Security.Principal.NTAccount.IsValidTargetType(System.Type)" /> 方法视为有效的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="targetType " />为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="targetType " /> 不是 <see cref="T:System.Security.Principal.IdentityReference" /> 类型。</exception>
    /// <exception cref="T:System.Security.Principal.IdentityNotMappedException">未能转换部分或所有标识引用。</exception>
    /// <exception cref="T:System.SystemException">源帐户名称过长。- 或 -返回了 Win32 错误。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public override IdentityReference Translate(Type targetType)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException("targetType");
      if (targetType == typeof (NTAccount))
        return (IdentityReference) this;
      if (!(targetType == typeof (SecurityIdentifier)))
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
      IdentityReferenceCollection sourceAccounts = new IdentityReferenceCollection(1);
      sourceAccounts.Add((IdentityReference) this);
      Type targetType1 = targetType;
      int num = 1;
      return NTAccount.Translate(sourceAccounts, targetType1, num != 0)[0];
    }

    /// <summary>返回一个值，该值指示此 <see cref="T:System.Security.Principal.NTAccount" /> 对象是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="o" /> 是与此 <see cref="T:System.Security.Principal.NTAccount" /> 对象有相同基础类型和值的对象，则为 true；否则为 false。</returns>
    /// <param name="o">要与此 <see cref="T:System.Security.Principal.NTAccount" /> 对象比较的对象，或 null。</param>
    public override bool Equals(object o)
    {
      if (o == null)
        return false;
      NTAccount ntAccount = o as NTAccount;
      if (ntAccount == (NTAccount) null)
        return false;
      return this == ntAccount;
    }

    /// <summary>用作当前 <see cref="T:System.Security.Principal.NTAccount" /> 对象的一个哈希函数。<see cref="M:System.Security.Principal.NTAccount.GetHashCode" /> 方法适合在哈希算法和类似哈希表的数据结构中使用。</summary>
    /// <returns>当前 <see cref="T:System.Security.Principal.NTAccount" /> 对象的哈希值。</returns>
    public override int GetHashCode()
    {
      return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._Name);
    }

    /// <summary>以域\帐户 格式返回 <see cref="T:System.Security.Principal.NTAccount" /> 对象所表示的帐户的帐户名。</summary>
    /// <returns>域\帐户 格式的帐户名。</returns>
    public override string ToString()
    {
      return this._Name;
    }

    [SecurityCritical]
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, bool forceSuccess)
    {
      bool someFailed = false;
      IdentityReferenceCollection referenceCollection = NTAccount.Translate(sourceAccounts, targetType, out someFailed);
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
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, out bool someFailed)
    {
      if (sourceAccounts == null)
        throw new ArgumentNullException("sourceAccounts");
      if (targetType == typeof (SecurityIdentifier))
        return NTAccount.TranslateToSids(sourceAccounts, out someFailed);
      throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
    }

    [SecurityCritical]
    private static IdentityReferenceCollection TranslateToSids(IdentityReferenceCollection sourceAccounts, out bool someFailed)
    {
      if (sourceAccounts == null)
        throw new ArgumentNullException("sourceAccounts");
      if (sourceAccounts.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), "sourceAccounts");
      SafeLsaPolicyHandle handle = SafeLsaPolicyHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle1 = SafeLsaMemoryHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
      try
      {
        Win32Native.UNICODE_STRING[] names = new Win32Native.UNICODE_STRING[sourceAccounts.Count];
        int index1 = 0;
        foreach (IdentityReference sourceAccount in sourceAccounts)
        {
          NTAccount ntAccount = sourceAccount as NTAccount;
          if (ntAccount == (NTAccount) null)
            throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), "sourceAccounts");
          names[index1].Buffer = ntAccount.ToString();
          if (names[index1].Buffer.Length * 2 + 2 > (int) ushort.MaxValue)
            throw new SystemException();
          names[index1].Length = (ushort) (names[index1].Buffer.Length * 2);
          names[index1].MaximumLength = (ushort) ((uint) names[index1].Length + 2U);
          ++index1;
        }
        handle = System.Security.Principal.Win32.LsaOpenPolicy((string) null, PolicyRights.POLICY_LOOKUP_NAMES);
        someFailed = false;
        uint num = !System.Security.Principal.Win32.LsaLookupNames2Supported ? Win32Native.LsaLookupNames(handle, sourceAccounts.Count, names, ref invalidHandle1, ref invalidHandle2) : Win32Native.LsaLookupNames2(handle, 0, sourceAccounts.Count, names, ref invalidHandle1, ref invalidHandle2);
        if ((int) num == -1073741801 || (int) num == -1073741670)
          throw new OutOfMemoryException();
        if ((int) num == -1073741790)
          throw new UnauthorizedAccessException();
        if ((int) num == -1073741709 || (int) num == 263)
          someFailed = true;
        else if ((int) num != 0)
          throw new SystemException(Win32Native.GetMessage(Win32Native.LsaNtStatusToWinError((int) num)));
        IdentityReferenceCollection referenceCollection = new IdentityReferenceCollection(sourceAccounts.Count);
        if ((int) num == 0 || (int) num == 263)
        {
          if (System.Security.Principal.Win32.LsaLookupNames2Supported)
          {
            invalidHandle2.Initialize((uint) sourceAccounts.Count, (uint) Marshal.SizeOf(typeof (Win32Native.LSA_TRANSLATED_SID2)));
            System.Security.Principal.Win32.InitializeReferencedDomainsPointer(invalidHandle1);
            Win32Native.LSA_TRANSLATED_SID2[] array = new Win32Native.LSA_TRANSLATED_SID2[sourceAccounts.Count];
            invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_SID2>(0UL, array, 0, array.Length);
            for (int index2 = 0; index2 < sourceAccounts.Count; ++index2)
            {
              Win32Native.LSA_TRANSLATED_SID2 lsaTranslatedSiD2 = array[index2];
              switch (lsaTranslatedSiD2.Use)
              {
                case 1:
                case 2:
                case 4:
                case 5:
                case 9:
                  referenceCollection.Add((IdentityReference) new SecurityIdentifier(lsaTranslatedSiD2.Sid, true));
                  break;
                default:
                  someFailed = true;
                  referenceCollection.Add(sourceAccounts[index2]);
                  break;
              }
            }
          }
          else
          {
            invalidHandle2.Initialize((uint) sourceAccounts.Count, (uint) Marshal.SizeOf(typeof (Win32Native.LSA_TRANSLATED_SID)));
            System.Security.Principal.Win32.InitializeReferencedDomainsPointer(invalidHandle1);
            Win32Native.LSA_REFERENCED_DOMAIN_LIST referencedDomainList = invalidHandle1.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
            SecurityIdentifier[] securityIdentifierArray = new SecurityIdentifier[referencedDomainList.Entries];
            for (int index2 = 0; index2 < referencedDomainList.Entries; ++index2)
            {
              Win32Native.LSA_TRUST_INFORMATION trustInformation = (Win32Native.LSA_TRUST_INFORMATION) Marshal.PtrToStructure(new IntPtr((long) referencedDomainList.Domains + (long) (index2 * Marshal.SizeOf(typeof (Win32Native.LSA_TRUST_INFORMATION)))), typeof (Win32Native.LSA_TRUST_INFORMATION));
              securityIdentifierArray[index2] = new SecurityIdentifier(trustInformation.Sid, true);
            }
            Win32Native.LSA_TRANSLATED_SID[] array = new Win32Native.LSA_TRANSLATED_SID[sourceAccounts.Count];
            invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_SID>(0UL, array, 0, array.Length);
            for (int index2 = 0; index2 < sourceAccounts.Count; ++index2)
            {
              Win32Native.LSA_TRANSLATED_SID lsaTranslatedSid = array[index2];
              switch (lsaTranslatedSid.Use)
              {
                case 1:
                case 2:
                case 4:
                case 5:
                case 9:
                  referenceCollection.Add((IdentityReference) new SecurityIdentifier(securityIdentifierArray[lsaTranslatedSid.DomainIndex], lsaTranslatedSid.Rid));
                  break;
                default:
                  someFailed = true;
                  referenceCollection.Add(sourceAccounts[index2]);
                  break;
              }
            }
          }
        }
        else
        {
          for (int index2 = 0; index2 < sourceAccounts.Count; ++index2)
            referenceCollection.Add(sourceAccounts[index2]);
        }
        return referenceCollection;
      }
      finally
      {
        handle.Dispose();
        invalidHandle1.Dispose();
        invalidHandle2.Dispose();
      }
    }
  }
}
