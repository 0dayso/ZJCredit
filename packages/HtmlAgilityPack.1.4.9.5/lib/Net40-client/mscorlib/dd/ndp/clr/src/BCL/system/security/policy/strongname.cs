// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.StrongName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>提供代码程序集的强名称作为策略评估的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IDelayEvaluatedEvidence
  {
    private StrongNamePublicKeyBlob m_publicKeyBlob;
    private string m_name;
    private Version m_version;
    [NonSerialized]
    private RuntimeAssembly m_assembly;
    [NonSerialized]
    private bool m_wasUsed;

    /// <summary>获取当前 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" />。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" />。</returns>
    public StrongNamePublicKeyBlob PublicKey
    {
      get
      {
        return this.m_publicKeyBlob;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.Policy.StrongName" /> 的简单名称。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.StrongName" /> 的简单名称部分。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Version" />。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Version" />。</returns>
    public Version Version
    {
      get
      {
        return this.m_version;
      }
    }

    bool IDelayEvaluatedEvidence.IsVerified
    {
      [SecurityCritical] get
      {
        if (!((Assembly) this.m_assembly != (Assembly) null))
          return true;
        return this.m_assembly.IsStrongNameVerified;
      }
    }

    bool IDelayEvaluatedEvidence.WasUsed
    {
      get
      {
        return this.m_wasUsed;
      }
    }

    internal StrongName()
    {
    }

    /// <summary>用强名称公钥 Blob、名称和版本初始化 <see cref="T:System.Security.Policy.StrongName" /> 类的新实例。</summary>
    /// <param name="blob">软件发行者的 <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" />。</param>
    /// <param name="name">强名称中的简单名称部分。</param>
    /// <param name="version">强名称的 <see cref="T:System.Version" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="blob" /> 参数为 null。- 或 -<paramref name="name" /> 参数为 null。- 或 -<paramref name="version" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数是空字符串 ("")。</exception>
    public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
      : this(blob, name, version, (Assembly) null)
    {
    }

    internal StrongName(StrongNamePublicKeyBlob blob, string name, Version version, Assembly assembly)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
      if (blob == null)
        throw new ArgumentNullException("blob");
      if (version == (Version) null)
        throw new ArgumentNullException("version");
      RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
      if (assembly != (Assembly) null && (Assembly) runtimeAssembly == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
      this.m_publicKeyBlob = blob;
      this.m_name = name;
      this.m_version = version;
      this.m_assembly = runtimeAssembly;
    }

    void IDelayEvaluatedEvidence.MarkUsed()
    {
      this.m_wasUsed = true;
    }

    internal static bool CompareNames(string asmName, string mcName)
    {
      if (mcName.Length > 0)
      {
        string str = mcName;
        int index = str.Length - 1;
        if ((int) str[index] == 42 && mcName.Length - 1 <= asmName.Length)
          return string.Compare(mcName, 0, asmName, 0, mcName.Length - 1, StringComparison.OrdinalIgnoreCase) == 0;
      }
      return string.Compare(mcName, asmName, StringComparison.OrdinalIgnoreCase) == 0;
    }

    /// <summary>创建与当前 <see cref="T:System.Security.Policy.StrongName" /> 对应的 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />。</summary>
    /// <returns>指定 <see cref="T:System.Security.Policy.StrongName" /> 的 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />。</returns>
    /// <param name="evidence">构造 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> 所依据的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new StrongNameIdentityPermission(this.m_publicKeyBlob, this.m_name, this.m_version);
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new StrongName(this.m_publicKeyBlob, this.m_name, this.m_version);
    }

    /// <summary>创建当前 <see cref="T:System.Security.Policy.StrongName" /> 的等效副本。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongName" /> 的相同的新副本。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("StrongName");
      securityElement.AddAttribute("version", "1");
      if (this.m_publicKeyBlob != null)
        securityElement.AddAttribute("Key", Hex.EncodeHexString(this.m_publicKeyBlob.PublicKey));
      if (this.m_name != null)
        securityElement.AddAttribute("Name", this.m_name);
      if (this.m_version != (Version) null)
        securityElement.AddAttribute("Version", this.m_version.ToString());
      return securityElement;
    }

    internal void FromXml(SecurityElement element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (string.Compare(element.Tag, "StrongName", StringComparison.Ordinal) != 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
      this.m_publicKeyBlob = (StrongNamePublicKeyBlob) null;
      this.m_version = (Version) null;
      string hexString = element.Attribute("Key");
      if (hexString != null)
        this.m_publicKeyBlob = new StrongNamePublicKeyBlob(Hex.DecodeHexString(hexString));
      this.m_name = element.Attribute("Name");
      string version = element.Attribute("Version");
      if (version == null)
        return;
      this.m_version = new Version(version);
    }

    /// <summary>创建当前 <see cref="T:System.Security.Policy.StrongName" /> 的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongName" /> 的表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    /// <summary>确定指定的强名称是否等于当前强名称。</summary>
    /// <returns>如果指定的强名称等于当前强名称，则为 true；否则为 false。</returns>
    /// <param name="o">与当前强名称进行比较的强名称。</param>
    public override bool Equals(object o)
    {
      StrongName strongName = o as StrongName;
      if (strongName != null && object.Equals((object) this.m_publicKeyBlob, (object) strongName.m_publicKeyBlob) && object.Equals((object) this.m_name, (object) strongName.m_name))
        return object.Equals((object) this.m_version, (object) strongName.m_version);
      return false;
    }

    /// <summary>获取当前 <see cref="T:System.Security.Policy.StrongName" /> 的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.StrongName" /> 的哈希代码。</returns>
    public override int GetHashCode()
    {
      if (this.m_publicKeyBlob != null)
        return this.m_publicKeyBlob.GetHashCode();
      if (this.m_name != null || this.m_version != (Version) null)
        return (this.m_name == null ? 0 : this.m_name.GetHashCode()) + (this.m_version == (Version) null ? 0 : this.m_version.GetHashCode());
      return typeof (StrongName).GetHashCode();
    }

    internal object Normalize()
    {
      MemoryStream memoryStream = new MemoryStream();
      BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream);
      byte[] buffer = this.m_publicKeyBlob.PublicKey;
      binaryWriter.Write(buffer);
      int major = this.m_version.Major;
      binaryWriter.Write(major);
      string str = this.m_name;
      binaryWriter.Write(str);
      long num = 0;
      memoryStream.Position = num;
      return (object) memoryStream;
    }
  }
}
