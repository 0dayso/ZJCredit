// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Publisher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>提供代码程序集的 Authenticode X.509v3 数字签名作为策略评估的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Publisher : EvidenceBase, IIdentityPermissionFactory
  {
    private X509Certificate m_cert;

    /// <summary>获取发行者的 Authenticode X.509v3 证书。</summary>
    /// <returns>发行者的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />。</returns>
    public X509Certificate Certificate
    {
      get
      {
        return new X509Certificate(this.m_cert);
      }
    }

    /// <summary>用包含发行者公钥的 Authenticode X.509v3 证书初始化 <see cref="T:System.Security.Policy.Publisher" /> 类的新实例。</summary>
    /// <param name="cert">一个 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />，它包含软件发行者的公钥。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cert" /> 参数为 null。</exception>
    public Publisher(X509Certificate cert)
    {
      if (cert == null)
        throw new ArgumentNullException("cert");
      this.m_cert = cert;
    }

    /// <summary>创建与 <see cref="T:System.Security.Policy.Publisher" /> 类的当前实例对应的标识权限。</summary>
    /// <returns>指定 <see cref="T:System.Security.Policy.Publisher" /> 的 <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />。</returns>
    /// <param name="evidence">构造标识权限所依据的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new PublisherIdentityPermission(this.m_cert);
    }

    /// <summary>将当前 <see cref="T:System.Security.Policy.Publisher" /> 与指定的对象比较以判断它们是否等同。</summary>
    /// <returns>如果 <see cref="T:System.Security.Policy.Publisher" /> 类的两个实例相等，则为 true；否则为 false。</returns>
    /// <param name="o">将测试是否与当前对象等同的 <see cref="T:System.Security.Policy.Publisher" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="o" /> 参数不是 <see cref="T:System.Security.Policy.Publisher" /> 对象。</exception>
    public override bool Equals(object o)
    {
      Publisher publisher = o as Publisher;
      if (publisher != null)
        return Publisher.PublicKeyEquals(this.m_cert, publisher.m_cert);
      return false;
    }

    internal static bool PublicKeyEquals(X509Certificate cert1, X509Certificate cert2)
    {
      if (cert1 == null)
        return cert2 == null;
      if (cert2 == null)
        return false;
      byte[] publicKey1 = cert1.GetPublicKey();
      string keyAlgorithm1 = cert1.GetKeyAlgorithm();
      byte[] algorithmParameters1 = cert1.GetKeyAlgorithmParameters();
      byte[] publicKey2 = cert2.GetPublicKey();
      string keyAlgorithm2 = cert2.GetKeyAlgorithm();
      byte[] algorithmParameters2 = cert2.GetKeyAlgorithmParameters();
      int length1 = publicKey1.Length;
      if (length1 != publicKey2.Length)
        return false;
      for (int index = 0; index < length1; ++index)
      {
        if ((int) publicKey1[index] != (int) publicKey2[index])
          return false;
      }
      if (!keyAlgorithm1.Equals(keyAlgorithm2))
        return false;
      int length2 = algorithmParameters1.Length;
      if (algorithmParameters2.Length != length2)
        return false;
      for (int index = 0; index < length2; ++index)
      {
        if ((int) algorithmParameters1[index] != (int) algorithmParameters2[index])
          return false;
      }
      return true;
    }

    /// <summary>获取当前 <see cref="P:System.Security.Policy.Publisher.Certificate" /> 的哈希代码。</summary>
    /// <returns>当前 <see cref="P:System.Security.Policy.Publisher.Certificate" /> 的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_cert.GetHashCode();
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Publisher(this.m_cert);
    }

    /// <summary>创建 <see cref="T:System.Security.Policy.Publisher" /> 的等效副本。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.Publisher" /> 的相同的新副本。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Publisher");
      string name = "version";
      string str = "1";
      securityElement.AddAttribute(name, str);
      SecurityElement child = new SecurityElement("X509v3Certificate", this.m_cert != null ? this.m_cert.GetRawCertDataString() : "");
      securityElement.AddChild(child);
      return securityElement;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Policy.Publisher" /> 的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.Publisher" /> 的表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      MemoryStream memoryStream = new MemoryStream(this.m_cert.GetRawCertData());
      long num = 0;
      memoryStream.Position = num;
      return (object) memoryStream;
    }
  }
}
