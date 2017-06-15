// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PublisherIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>表示软件发行者的身份标识。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    private bool m_unrestricted;
    private X509Certificate[] m_certs;

    /// <summary>获取或设置表示软件发行者身份标识的 Authenticode X.509v3 证书。</summary>
    /// <returns>表示软件发行者身份标识的 X.509 证书。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> 不是有效证书。</exception>
    /// <exception cref="T:System.NotSupportedException">无法设置该属性，因为其标识不明确。</exception>
    public X509Certificate Certificate
    {
      get
      {
        if (this.m_certs == null || this.m_certs.Length < 1)
          return (X509Certificate) null;
        if (this.m_certs.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        if (this.m_certs[0] == null)
          return (X509Certificate) null;
        return new X509Certificate(this.m_certs[0]);
      }
      set
      {
        PublisherIdentityPermission.CheckCertificate(value);
        this.m_unrestricted = false;
        this.m_certs = new X509Certificate[1];
        this.m_certs[0] = new X509Certificate(value);
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public PublisherIdentityPermission(PermissionState state)
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

    /// <summary>使用指定的 Authenticode X.509v3 证书初始化 <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> 类的新实例。</summary>
    /// <param name="certificate">X.509 证书表示软件发行者的身份标识。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="certificate" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="certificate" /> 参数不是有效的证书。</exception>
    public PublisherIdentityPermission(X509Certificate certificate)
    {
      this.Certificate = certificate;
    }

    private static void CheckCertificate(X509Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (certificate.GetRawCertData() == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_UninitializedCertificate"));
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      PublisherIdentityPermission identityPermission = new PublisherIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_certs != null)
      {
        identityPermission.m_certs = new X509Certificate[this.m_certs.Length];
        for (int index = 0; index < this.m_certs.Length; ++index)
          identityPermission.m_certs[index] = this.m_certs[index] == null ? (X509Certificate) null : new X509Certificate(this.m_certs[index]);
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
        return !this.m_unrestricted && (this.m_certs == null || this.m_certs.Length == 0);
      PublisherIdentityPermission identityPermission = target as PublisherIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_certs != null)
      {
        foreach (X509Certificate mCert1 in this.m_certs)
        {
          bool flag = false;
          if (identityPermission.m_certs != null)
          {
            foreach (X509Certificate mCert2 in identityPermission.m_certs)
            {
              if (mCert1.Equals(mCert2))
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
      PublisherIdentityPermission identityPermission = target as PublisherIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new PublisherIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_certs == null || identityPermission.m_certs == null || (this.m_certs.Length == 0 || identityPermission.m_certs.Length == 0))
        return (IPermission) null;
      ArrayList arrayList = new ArrayList();
      foreach (X509Certificate mCert1 in this.m_certs)
      {
        foreach (X509Certificate mCert2 in identityPermission.m_certs)
        {
          if (mCert1.Equals(mCert2))
            arrayList.Add((object) new X509Certificate(mCert1));
        }
      }
      if (arrayList.Count == 0)
        return (IPermission) null;
      return (IPermission) new PublisherIdentityPermission(PermissionState.None) { m_certs = (X509Certificate[]) arrayList.ToArray(typeof (X509Certificate)) };
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -这两个权限不相等。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_certs == null || this.m_certs.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      PublisherIdentityPermission identityPermission = target as PublisherIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new PublisherIdentityPermission(PermissionState.None) { m_unrestricted = true };
      if (this.m_certs == null || this.m_certs.Length == 0)
      {
        if (identityPermission.m_certs == null || identityPermission.m_certs.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_certs == null || identityPermission.m_certs.Length == 0)
        return this.Copy();
      ArrayList arrayList = new ArrayList();
      foreach (X509Certificate mCert in this.m_certs)
        arrayList.Add((object) mCert);
      foreach (X509Certificate mCert in identityPermission.m_certs)
      {
        bool flag = false;
        foreach (X509Certificate other in arrayList)
        {
          if (mCert.Equals(other))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          arrayList.Add((object) mCert);
      }
      return (IPermission) new PublisherIdentityPermission(PermissionState.None) { m_certs = (X509Certificate[]) arrayList.ToArray(typeof (X509Certificate)) };
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public override void FromXml(SecurityElement esd)
    {
      this.m_unrestricted = false;
      this.m_certs = (X509Certificate[]) null;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string strA = esd.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string hexString1 = esd.Attribute("X509v3Certificate");
        ArrayList arrayList = new ArrayList();
        if (hexString1 != null)
          arrayList.Add((object) new X509Certificate(Hex.DecodeHexString(hexString1)));
        ArrayList children = esd.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string hexString2 = securityElement.Attribute("X509v3Certificate");
            if (hexString2 != null)
              arrayList.Add((object) new X509Certificate(Hex.DecodeHexString(hexString2)));
          }
        }
        if (arrayList.Count == 0)
          return;
        this.m_certs = (X509Certificate[]) arrayList.ToArray(typeof (X509Certificate));
      }
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.PublisherIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_certs != null)
      {
        if (this.m_certs.Length == 1)
        {
          permissionElement.AddAttribute("X509v3Certificate", this.m_certs[0].GetRawCertDataString());
        }
        else
        {
          for (int index = 0; index < this.m_certs.Length; ++index)
          {
            SecurityElement child = new SecurityElement("Cert");
            child.AddAttribute("X509v3Certificate", this.m_certs[index].GetRawCertDataString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return PublisherIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 10;
    }
  }
}
