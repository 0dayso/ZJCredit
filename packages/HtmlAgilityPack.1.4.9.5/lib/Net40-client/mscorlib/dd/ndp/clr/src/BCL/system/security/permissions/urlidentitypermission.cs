// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UrlIdentityPermission
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
  /// <summary>为代码源自的 URL 定义标识权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    [OptionalField(VersionAdded = 2)]
    private bool m_unrestricted;
    [OptionalField(VersionAdded = 2)]
    private URLString[] m_urls;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermission;
    private URLString m_url;

    /// <summary>获取或设置表示 Internet 代码标识的 URL。</summary>
    /// <returns>表示 Internet 代码标识的 URL。</returns>
    /// <exception cref="T:System.NotSupportedException">无法检索该 URL，因为其标识不明确。</exception>
    public string Url
    {
      get
      {
        if (this.m_urls == null)
          return "";
        if (this.m_urls.Length == 1)
          return this.m_urls[0].ToString();
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
      }
      set
      {
        this.m_unrestricted = false;
        if (value == null || value.Length == 0)
        {
          this.m_urls = (URLString[]) null;
        }
        else
        {
          this.m_urls = new URLString[1];
          this.m_urls[0] = new URLString(value);
        }
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public UrlIdentityPermission(PermissionState state)
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

    /// <summary>初始化 <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> 类的新实例，以表示 <paramref name="site" /> 描述的 URL 标识。</summary>
    /// <param name="site">URL 或通配符表达式。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="site" /> 参数为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="site" /> 参数的长度为零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="site" /> 参数的 URL、目录或者站点部分无效。</exception>
    public UrlIdentityPermission(string site)
    {
      if (site == null)
        throw new ArgumentNullException("site");
      this.Url = site;
    }

    internal UrlIdentityPermission(URLString site)
    {
      this.m_unrestricted = false;
      this.m_urls = new URLString[1];
      this.m_urls[0] = site;
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
        if (this.m_url == null)
          return;
        this.m_unrestricted = false;
        this.m_urls = new URLString[1];
        this.m_urls[0] = this.m_url;
        this.m_url = (URLString) null;
      }
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = this.ToXml().ToString();
      if (this.m_urls == null || this.m_urls.Length != 1)
        return;
      this.m_url = this.m_urls[0];
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = (string) null;
      this.m_url = (URLString) null;
    }

    internal void AppendOrigin(ArrayList originList)
    {
      if (this.m_urls == null)
      {
        originList.Add((object) "");
      }
      else
      {
        for (int index = 0; index < this.m_urls.Length; ++index)
          originList.Add((object) this.m_urls[index].ToString());
      }
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      UrlIdentityPermission identityPermission = new UrlIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_urls != null)
      {
        identityPermission.m_urls = new URLString[this.m_urls.Length];
        for (int index = 0; index < this.m_urls.Length; ++index)
          identityPermission.m_urls[index] = (URLString) this.m_urls[index].Copy();
      }
      return (IPermission) identityPermission;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -Url 属性不是有效的 URL。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return !this.m_unrestricted && (this.m_urls == null || this.m_urls.Length == 0);
      UrlIdentityPermission identityPermission = target as UrlIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_urls != null)
      {
        foreach (URLString mUrl1 in this.m_urls)
        {
          bool flag = false;
          if (identityPermission.m_urls != null)
          {
            foreach (URLString mUrl2 in identityPermission.m_urls)
            {
              if (mUrl1.IsSubsetOf((SiteString) mUrl2))
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
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -Url 属性不是有效的 URL。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      UrlIdentityPermission identityPermission = target as UrlIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new UrlIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_urls == null || identityPermission.m_urls == null || (this.m_urls.Length == 0 || identityPermission.m_urls.Length == 0))
        return (IPermission) null;
      List<URLString> urlStringList = new List<URLString>();
      foreach (URLString mUrl1 in this.m_urls)
      {
        foreach (URLString mUrl2 in identityPermission.m_urls)
        {
          URLString urlString = (URLString) mUrl1.Intersect((SiteString) mUrl2);
          if (urlString != null)
            urlStringList.Add(urlString);
        }
      }
      if (urlStringList.Count == 0)
        return (IPermission) null;
      return (IPermission) new UrlIdentityPermission(PermissionState.None) { m_urls = urlStringList.ToArray() };
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -<see cref="P:System.Security.Permissions.UrlIdentityPermission.Url" /> 属性不是有效的 URL。- 或 -两个权限不相等，而且其中一个不是另一个的子集。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_urls == null || this.m_urls.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      UrlIdentityPermission identityPermission = target as UrlIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new UrlIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_urls == null || this.m_urls.Length == 0)
      {
        if (identityPermission.m_urls == null || identityPermission.m_urls.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_urls == null || identityPermission.m_urls.Length == 0)
        return this.Copy();
      List<URLString> urlStringList = new List<URLString>();
      foreach (URLString mUrl in this.m_urls)
        urlStringList.Add(mUrl);
      foreach (URLString mUrl in identityPermission.m_urls)
      {
        bool flag = false;
        foreach (URLString url in urlStringList)
        {
          if (mUrl.Equals(url))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          urlStringList.Add(mUrl);
      }
      return (IPermission) new UrlIdentityPermission(PermissionState.None) { m_urls = urlStringList.ToArray() };
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
      this.m_urls = (URLString[]) null;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string strA = esd.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string url1 = esd.Attribute("Url");
        List<URLString> urlStringList = new List<URLString>();
        if (url1 != null)
          urlStringList.Add(new URLString(url1, true));
        ArrayList children = esd.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string url2 = securityElement.Attribute("Url");
            if (url2 != null)
              urlStringList.Add(new URLString(url2, true));
          }
        }
        if (urlStringList.Count == 0)
          return;
        this.m_urls = urlStringList.ToArray();
      }
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.UrlIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_urls != null)
      {
        if (this.m_urls.Length == 1)
        {
          permissionElement.AddAttribute("Url", this.m_urls[0].ToString());
        }
        else
        {
          for (int index = 0; index < this.m_urls.Length; ++index)
          {
            SecurityElement child = new SecurityElement("Url");
            child.AddAttribute("Url", this.m_urls[index].ToString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return UrlIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 13;
    }
  }
}
