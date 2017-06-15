// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Site
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>提供从其中产生代码程序集的网站作为策略评估的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Site : EvidenceBase, IIdentityPermissionFactory
  {
    private SiteString m_name;

    /// <summary>获取或设置从其中产生代码程序集的网站。</summary>
    /// <returns>代码程序集出自的网站的名称。</returns>
    public string Name
    {
      get
      {
        return this.m_name.ToString();
      }
    }

    /// <summary>用从其中产生代码程序集的网站初始化 <see cref="T:System.Security.Policy.Site" /> 类的新实例。</summary>
    /// <param name="name">关联代码程序集源的网站。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public Site(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.m_name = new SiteString(name);
    }

    private Site(SiteString name)
    {
      this.m_name = name;
    }

    /// <summary>从指定的 URL 创建新的 <see cref="T:System.Security.Policy.Site" /> 对象。</summary>
    /// <returns>一个新的站点对象。</returns>
    /// <param name="url">用于创建新 <see cref="T:System.Security.Policy.Site" /> 对象的 URL 。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="url" /> 参数不是有效的 URL。- 或 -<paramref name="url" /> 参数是一个文件名。</exception>
    public static Site CreateFromUrl(string url)
    {
      return new Site(Site.ParseSiteFromUrl(url));
    }

    private static SiteString ParseSiteFromUrl(string name)
    {
      if (string.Compare(new URLString(name).Scheme, "file", StringComparison.OrdinalIgnoreCase) == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
      return new SiteString(new URLString(name).Host);
    }

    internal SiteString GetSiteString()
    {
      return this.m_name;
    }

    /// <summary>创建与当前 <see cref="T:System.Security.Policy.Site" /> 对象对应的标识权限。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.Site" /> 对象的站点标识授予权限。</returns>
    /// <param name="evidence">用于构造标识权限的证据。</param>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new SiteIdentityPermission(this.Name);
    }

    /// <summary>将当前 <see cref="T:System.Security.Policy.Site" /> 与指定的对象比较以判断它们是否等同。</summary>
    /// <returns>如果 <see cref="T:System.Security.Policy.Site" /> 类的两个实例相等，则为 true；否则，为 false。</returns>
    /// <param name="o">与当前对象等同的要测试的对象。</param>
    public override bool Equals(object o)
    {
      Site site = o as Site;
      if (site == null)
        return false;
      return string.Equals(this.Name, site.Name, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>返回当前网站名称的哈希代码。</summary>
    /// <returns>当前网站名称的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Site(this.m_name);
    }

    /// <summary>创建当前 <see cref="T:System.Security.Policy.Site" /> 对象的等效副本。</summary>
    /// <returns>新对象与当前 <see cref="T:System.Security.Policy.Site" /> 对象相同。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
      securityElement.AddAttribute("version", "1");
      if (this.m_name != null)
        securityElement.AddChild(new SecurityElement("Name", this.m_name.ToString()));
      return securityElement;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Policy.Site" /> 对象的字符串表示形式。</summary>
    /// <returns>当前站点的表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      return (object) this.m_name.ToString().ToUpper(CultureInfo.InvariantCulture);
    }
  }
}
