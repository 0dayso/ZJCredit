// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.HashMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>通过测试程序集的哈希值确定该程序集是否属于代码组。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class HashMembershipCondition : ISerializable, IDeserializationCallback, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IReportMatchMembershipCondition
  {
    private byte[] m_value;
    private HashAlgorithm m_hashAlg;
    private SecurityElement m_element;
    private object s_InternalSyncObject;
    private const string s_tagHashValue = "HashValue";
    private const string s_tagHashAlgorithm = "HashAlgorithm";

    private object InternalSyncObject
    {
      get
      {
        if (this.s_InternalSyncObject == null)
          Interlocked.CompareExchange(ref this.s_InternalSyncObject, new object(), (object) null);
        return this.s_InternalSyncObject;
      }
    }

    /// <summary>获取或设置用于成员条件的哈希算法。</summary>
    /// <returns>用于成员条件的哈希算法。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将 <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> 设置为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public HashAlgorithm HashAlgorithm
    {
      get
      {
        if (this.m_hashAlg == null && this.m_element != null)
          this.ParseHashAlgorithm();
        return this.m_hashAlg;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("HashAlgorithm");
        this.m_hashAlg = value;
      }
    }

    /// <summary>获取或设置要针对其测试成员条件的哈希值。</summary>
    /// <returns>要针对其测试成员条件的哈希值。</returns>
    /// <exception cref="T:System.ArgumentNullException">尝试将 <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> 设置为 null。</exception>
    public byte[] HashValue
    {
      get
      {
        if (this.m_value == null && this.m_element != null)
          this.ParseHashValue();
        if (this.m_value == null)
          return (byte[]) null;
        byte[] numArray = new byte[this.m_value.Length];
        Array.Copy((Array) this.m_value, (Array) numArray, this.m_value.Length);
        return numArray;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        this.m_value = new byte[value.Length];
        Array.Copy((Array) value, (Array) this.m_value, value.Length);
      }
    }

    internal HashMembershipCondition()
    {
    }

    private HashMembershipCondition(SerializationInfo info, StreamingContext context)
    {
      this.m_value = (byte[]) info.GetValue("HashValue", typeof (byte[]));
      string hashName = (string) info.GetValue("HashAlgorithm", typeof (string));
      if (hashName != null)
        this.m_hashAlg = HashAlgorithm.Create(hashName);
      else
        this.m_hashAlg = (HashAlgorithm) new SHA1Managed();
    }

    /// <summary>用确定成员身份的哈希算法和哈希值初始化 <see cref="T:System.Security.Policy.HashMembershipCondition" /> 类的新实例。</summary>
    /// <param name="hashAlg">将用于计算程序集的哈希值的哈希算法。</param>
    /// <param name="value">要进行测试的哈希值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="hashAlg" /> 参数为 null。- 或 -<paramref name="value" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="hashAlg" /> 参数不是有效的哈希算法。</exception>
    public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (hashAlg == null)
        throw new ArgumentNullException("hashAlg");
      this.m_value = new byte[value.Length];
      Array.Copy((Array) value, (Array) this.m_value, value.Length);
      this.m_hashAlg = hashAlg;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("HashValue", (object) this.HashValue);
      info.AddValue("HashAlgorithm", (object) this.HashAlgorithm.ToString());
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">证据集，将根据它进行测试。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public bool Check(Evidence evidence)
    {
      object usedEvidence = (object) null;
      return ((IReportMatchMembershipCondition) this).Check(evidence, out usedEvidence);
    }

    bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
    {
      usedEvidence = (object) null;
      if (evidence == null)
        return false;
      Hash hostEvidence = evidence.GetHostEvidence<Hash>();
      if (hostEvidence != null)
      {
        if (this.m_value == null && this.m_element != null)
          this.ParseHashValue();
        if (this.m_hashAlg == null && this.m_element != null)
          this.ParseHashAlgorithm();
        byte[] first = (byte[]) null;
        lock (this.InternalSyncObject)
          first = hostEvidence.GenerateHash(this.m_hashAlg);
        if (first != null && HashMembershipCondition.CompareArrays(first, this.m_value))
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>当前成员条件的完全相同的新副本。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public IMembershipCondition Copy()
    {
      if (this.m_value == null && this.m_element != null)
        this.ParseHashValue();
      if (this.m_hashAlg == null && this.m_element != null)
        this.ParseHashAlgorithm();
      return (IMembershipCondition) new HashMembershipCondition(this.m_hashAlg, this.m_value);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Policy.PolicyLevel" /> 创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <param name="level">用于解析命名的权限集引用的策略级别上下文。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_value == null && this.m_element != null)
        this.ParseHashValue();
      if (this.m_hashAlg == null && this.m_element != null)
        this.ParseHashAlgorithm();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.HashMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_value != null)
        element.AddAttribute("HashValue", Hex.EncodeHexString(this.HashValue));
      if (this.m_hashAlg != null)
        element.AddAttribute("HashAlgorithm", this.HashAlgorithm.GetType().FullName);
      return element;
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">策略级别上下文，用于解析命名的权限集引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 参数不是有效的成员条件元素。</exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
      lock (this.InternalSyncObject)
      {
        this.m_element = e;
        this.m_value = (byte[]) null;
        this.m_hashAlg = (HashAlgorithm) null;
      }
    }

    /// <summary>确定指定对象中的 <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> 和 <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> 是否等效于包含在当前 <see cref="T:System.Security.Policy.HashMembershipCondition" /> 中的 <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> 和 <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" />。</summary>
    /// <returns>如果指定对象中的 <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> 和 <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> 等效于包含在当前 <see cref="T:System.Security.Policy.HashMembershipCondition" /> 中的 <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> 和 <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" />，则为 true；否则为 false。</returns>
    /// <param name="o">与当前的 <see cref="T:System.Security.Policy.HashMembershipCondition" /> 比较的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override bool Equals(object o)
    {
      HashMembershipCondition membershipCondition = o as HashMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_hashAlg == null && this.m_element != null)
          this.ParseHashAlgorithm();
        if (membershipCondition.m_hashAlg == null && membershipCondition.m_element != null)
          membershipCondition.ParseHashAlgorithm();
        if (this.m_hashAlg != null && membershipCondition.m_hashAlg != null && this.m_hashAlg.GetType() == membershipCondition.m_hashAlg.GetType())
        {
          if (this.m_value == null && this.m_element != null)
            this.ParseHashValue();
          if (membershipCondition.m_value == null && membershipCondition.m_element != null)
            membershipCondition.ParseHashValue();
          if (this.m_value.Length != membershipCondition.m_value.Length)
            return false;
          for (int index = 0; index < this.m_value.Length; ++index)
          {
            if ((int) this.m_value[index] != (int) membershipCondition.m_value[index])
              return false;
          }
          return true;
        }
      }
      return false;
    }

    /// <summary>获取当前成员条件的哈希代码。</summary>
    /// <returns>当前成员条件的哈希代码。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override int GetHashCode()
    {
      if (this.m_hashAlg == null && this.m_element != null)
        this.ParseHashAlgorithm();
      int num = this.m_hashAlg != null ? this.m_hashAlg.GetType().GetHashCode() : 0;
      if (this.m_value == null && this.m_element != null)
        this.ParseHashValue();
      int byteArrayHashCode = HashMembershipCondition.GetByteArrayHashCode(this.m_value);
      return num ^ byteArrayHashCode;
    }

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>成员条件状态的字符串表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string ToString()
    {
      if (this.m_hashAlg == null)
        this.ParseHashAlgorithm();
      return Environment.GetResourceString("Hash_ToString", (object) this.m_hashAlg.GetType().AssemblyQualifiedName, (object) Hex.EncodeHexString(this.HashValue));
    }

    private void ParseHashValue()
    {
      lock (this.InternalSyncObject)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("HashValue");
        if (local_2 != null)
        {
          this.m_value = Hex.DecodeHexString(local_2);
          if (this.m_value == null || this.m_hashAlg == null)
            return;
          this.m_element = (SecurityElement) null;
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", (object) "HashValue", (object) this.GetType().FullName));
      }
    }

    private void ParseHashAlgorithm()
    {
      lock (this.InternalSyncObject)
      {
        if (this.m_element == null)
          return;
        string local_2 = this.m_element.Attribute("HashAlgorithm");
        this.m_hashAlg = local_2 == null ? (HashAlgorithm) new SHA1Managed() : HashAlgorithm.Create(local_2);
        if (this.m_value == null || this.m_hashAlg == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    private static bool CompareArrays(byte[] first, byte[] second)
    {
      if (first.Length != second.Length)
        return false;
      int length = first.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) first[index] != (int) second[index])
          return false;
      }
      return true;
    }

    private static int GetByteArrayHashCode(byte[] baData)
    {
      if (baData == null)
        return 0;
      int num = 0;
      for (int index = 0; index < baData.Length; ++index)
        num = num << 8 ^ (int) baData[index] ^ num >> 24;
      return num;
    }
  }
}
