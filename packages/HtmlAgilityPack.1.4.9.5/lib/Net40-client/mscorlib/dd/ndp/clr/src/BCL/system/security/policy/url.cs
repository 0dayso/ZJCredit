// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Url
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>提供从其中产生代码程序集的 URL 作为策略评估的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Url : EvidenceBase, IIdentityPermissionFactory
  {
    private URLString m_url;

    /// <summary>获取从其中产生代码程序集的 URL。</summary>
    /// <returns>从其中产生代码程序集的 URL。</returns>
    public string Value
    {
      get
      {
        return this.m_url.ToString();
      }
    }

    internal Url(string name, bool parsed)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.m_url = new URLString(name, parsed);
    }

    /// <summary>用从其中产生代码程序集的 URL 初始化 <see cref="T:System.Security.Policy.Url" /> 类的新实例。</summary>
    /// <param name="name">关联代码程序集源的 URL。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public Url(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.m_url = new URLString(name);
    }

    private Url(Url url)
    {
      this.m_url = url.m_url;
    }

    internal URLString GetURLString()
    {
      return this.m_url;
    }

    /// <summary>创建与 <see cref="T:System.Security.Policy.Url" /> 证据类的当前实例对应的标识权限。</summary>
    /// <returns>指定的 <see cref="T:System.Security.Policy.Url" /> 证据的 <see cref="T:System.Security.Permissions.UrlIdentityPermission" />。</returns>
    /// <param name="evidence">构造标识权限所依据的证据集。</param>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new UrlIdentityPermission(this.m_url);
    }

    /// <summary>将当前 <see cref="T:System.Security.Policy.Url" /> 证据对象与指定对象比较以判断它们是否等同。</summary>
    /// <returns>如果两个 <see cref="T:System.Security.Policy.Url" /> 对象相等，则为 true；否则为 false。</returns>
    /// <param name="o">
    /// <see cref="T:System.Security.Policy.Url" /> 证据对象，将测试其是否与当前对象等同。</param>
    public override bool Equals(object o)
    {
      Url url = o as Url;
      if (url == null)
        return false;
      return url.m_url.Equals(this.m_url);
    }

    /// <summary>获取当前 URL 的哈希代码。</summary>
    /// <returns>当前 URL 的哈希代码。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override int GetHashCode()
    {
      return this.m_url.GetHashCode();
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Url(this);
    }

    /// <summary>创建证据对象的新副本。</summary>
    /// <returns>证据对象的完全相同的新副本。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
      securityElement.AddAttribute("version", "1");
      if (this.m_url != null)
        securityElement.AddChild(new SecurityElement("Url", this.m_url.ToString()));
      return securityElement;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Policy.Url" /> 的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.Url" /> 的表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      return (object) this.m_url.NormalizeUrl();
    }
  }
}
