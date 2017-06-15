// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SiteIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>为作为代码来源地的网站定义标识权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    [OptionalField(VersionAdded = 2)]
    private bool m_unrestricted;
    [OptionalField(VersionAdded = 2)]
    private SiteString[] m_sites;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermission;
    private SiteString m_site;

    /// <summary>获取或设置当前站点。</summary>
    /// <returns>当前站点。</returns>
    /// <exception cref="T:System.NotSupportedException">无法检索该站点标识，因为其标识不明确。</exception>
    public string Site
    {
      get
      {
        if (this.m_sites == null)
          return "";
        if (this.m_sites.Length == 1)
          return this.m_sites[0].ToString();
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
      }
      set
      {
        this.m_unrestricted = false;
        this.m_sites = new SiteString[1];
        this.m_sites[0] = new SiteString(value);
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public SiteIdentityPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_unrestricted = true;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_unrestricted = false;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> 类的新实例，以表示指定的站点标识。</summary>
    /// <param name="site">站点名称或通配符表达式。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="site" /> 参数不是有效的字符串，也不与有效的通配符站点名称匹配。</exception>
    public SiteIdentityPermission(string site)
    {
      this.Site = site;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_serializedPermission != null)
      {
        this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
        this.m_serializedPermission = (string) null;
      }
      else
      {
        if (this.m_site == null)
          return;
        this.m_unrestricted = false;
        this.m_sites = new SiteString[1];
        this.m_sites[0] = this.m_site;
        this.m_site = (SiteString) null;
      }
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = this.ToXml().ToString();
      if (this.m_sites == null || this.m_sites.Length != 1)
        return;
      this.m_site = this.m_sites[0];
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = (string) null;
      this.m_site = (SiteString) null;
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      SiteIdentityPermission identityPermission = new SiteIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_sites != null)
      {
        identityPermission.m_sites = new SiteString[this.m_sites.Length];
        for (int index = 0; index < this.m_sites.Length; ++index)
          identityPermission.m_sites[index] = this.m_sites[index].Copy();
      }
      return (IPermission) identityPermission;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return !this.m_unrestricted && (this.m_sites == null || this.m_sites.Length == 0);
      SiteIdentityPermission identityPermission = target as SiteIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_sites != null)
      {
        foreach (SiteString mSite1 in this.m_sites)
        {
          bool flag = false;
          if (identityPermission.m_sites != null)
          {
            foreach (SiteString mSite2 in identityPermission.m_sites)
            {
              if (mSite1.IsSubsetOf(mSite2))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      SiteIdentityPermission identityPermission = target as SiteIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new SiteIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_sites == null || identityPermission.m_sites == null || (this.m_sites.Length == 0 || identityPermission.m_sites.Length == 0))
        return (IPermission) null;
      List<SiteString> siteStringList = new List<SiteString>();
      foreach (SiteString mSite1 in this.m_sites)
      {
        foreach (SiteString mSite2 in identityPermission.m_sites)
        {
          SiteString siteString = mSite1.Intersect(mSite2);
          if (siteString != null)
            siteStringList.Add(siteString);
        }
      }
      if (siteStringList.Count == 0)
        return (IPermission) null;
      return (IPermission) new SiteIdentityPermission(PermissionState.None) { m_sites = siteStringList.ToArray() };
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -这两个权限不相等，而且其中一个不是另一个的子集。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_sites == null || this.m_sites.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      SiteIdentityPermission identityPermission = target as SiteIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new SiteIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_sites == null || this.m_sites.Length == 0)
      {
        if (identityPermission.m_sites == null || identityPermission.m_sites.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_sites == null || identityPermission.m_sites.Length == 0)
        return this.Copy();
      List<SiteString> siteStringList = new List<SiteString>();
      foreach (SiteString mSite in this.m_sites)
        siteStringList.Add(mSite);
      foreach (SiteString mSite in identityPermission.m_sites)
      {
        bool flag = false;
        foreach (SiteString siteString in siteStringList)
        {
          if (mSite.Equals((object) siteString))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          siteStringList.Add(mSite);
      }
      return (IPermission) new SiteIdentityPermission(PermissionState.None) { m_sites = siteStringList.ToArray() };
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号无效。</exception>
    public override void FromXml(SecurityElement esd)
    {
      this.m_unrestricted = false;
      this.m_sites = (SiteString[]) null;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string strA = esd.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string site1 = esd.Attribute("Site");
        List<SiteString> siteStringList = new List<SiteString>();
        if (site1 != null)
          siteStringList.Add(new SiteString(site1));
        ArrayList children = esd.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string site2 = securityElement.Attribute("Site");
            if (site2 != null)
              siteStringList.Add(new SiteString(site2));
          }
        }
        if (siteStringList.Count == 0)
          return;
        this.m_sites = siteStringList.ToArray();
      }
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.SiteIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_sites != null)
      {
        if (this.m_sites.Length == 1)
        {
          permissionElement.AddAttribute("Site", this.m_sites[0].ToString());
        }
        else
        {
          for (int index = 0; index < this.m_sites.Length; ++index)
          {
            SecurityElement child = new SecurityElement("Site");
            child.AddAttribute("Site", this.m_sites[index].ToString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return SiteIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 11;
    }
  }
}
