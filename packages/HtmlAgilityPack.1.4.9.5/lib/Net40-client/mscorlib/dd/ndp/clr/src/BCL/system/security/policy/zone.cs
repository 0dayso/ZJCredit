// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Zone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>提供代码程序集的安全区域作为策略评估的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Zone : EvidenceBase, IIdentityPermissionFactory
  {
    private static readonly string[] s_names = new string[6]{ "MyComputer", "Intranet", "Trusted", "Internet", "Untrusted", "NoZone" };
    [OptionalField(VersionAdded = 2)]
    private string m_url;
    private SecurityZone m_zone;

    /// <summary>获取从其中产生代码程序集的区域。</summary>
    /// <returns>从其中产生代码程序集的区域。</returns>
    public SecurityZone SecurityZone
    {
      [SecuritySafeCritical] get
      {
        if (this.m_url != null)
          this.m_zone = Zone._CreateFromUrl(this.m_url);
        return this.m_zone;
      }
    }

    /// <summary>用从其中产生代码程序集的区域初始化 <see cref="T:System.Security.Policy.Zone" /> 类的新实例。</summary>
    /// <param name="zone">关联代码程序集源的区域。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="zone" /> 参数不是有效的 <see cref="T:System.Security.SecurityZone" />。</exception>
    public Zone(SecurityZone zone)
    {
      if (zone < SecurityZone.NoZone || zone > SecurityZone.Untrusted)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
      this.m_zone = zone;
    }

    private Zone(Zone zone)
    {
      this.m_url = zone.m_url;
      this.m_zone = zone.m_zone;
    }

    private Zone(string url)
    {
      this.m_url = url;
      this.m_zone = SecurityZone.NoZone;
    }

    /// <summary>创建具有指定 URL 的新区域。</summary>
    /// <returns>具有指定 URL 的新区域。</returns>
    /// <param name="url">在其中创建区域的 URL。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="url" /> 参数为 null。</exception>
    public static Zone CreateFromUrl(string url)
    {
      if (url == null)
        throw new ArgumentNullException("url");
      return new Zone(url);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern SecurityZone _CreateFromUrl(string url);

    /// <summary>创建与 <see cref="T:System.Security.Policy.Zone" /> 证据类的当前实例对应的标识权限。</summary>
    /// <returns>指定的 <see cref="T:System.Security.Policy.Zone" /> 证据的 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />。</returns>
    /// <param name="evidence">构造标识权限所依据的证据集。</param>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new ZoneIdentityPermission(this.SecurityZone);
    }

    /// <summary>将当前 <see cref="T:System.Security.Policy.Zone" /> 证据对象与指定对象比较以判断它们是否等同。</summary>
    /// <returns>如果两个 <see cref="T:System.Security.Policy.Zone" /> 对象相等，则为 true；否则为 false。</returns>
    /// <param name="o">
    /// <see cref="T:System.Security.Policy.Zone" /> 证据对象，将测试其是否与当前对象等同。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="o" /> 参数不是 <see cref="T:System.Security.Policy.Zone" /> 对象。</exception>
    public override bool Equals(object o)
    {
      Zone zone = o as Zone;
      if (zone == null)
        return false;
      return this.SecurityZone == zone.SecurityZone;
    }

    /// <summary>获取当前区域的哈希代码。</summary>
    /// <returns>当前区域的哈希代码。</returns>
    public override int GetHashCode()
    {
      return (int) this.SecurityZone;
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Zone(this);
    }

    /// <summary>创建证据对象的等效副本。</summary>
    /// <returns>证据对象的完全相同的新副本。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
      securityElement.AddAttribute("version", "1");
      if (this.SecurityZone != SecurityZone.NoZone)
        securityElement.AddChild(new SecurityElement("Zone", Zone.s_names[(int) this.SecurityZone]));
      else
        securityElement.AddChild(new SecurityElement("Zone", Zone.s_names[Zone.s_names.Length - 1]));
      return securityElement;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Policy.Zone" /> 的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.Zone" /> 的表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      return (object) Zone.s_names[(int) this.SecurityZone];
    }
  }
}
