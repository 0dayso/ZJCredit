// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PublisherIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_x509cert;
    private string m_certFile;
    private string m_signedFile;

    /// <summary>获取或设置标识调用代码发行者的 Authenticode X.509v3 证书。</summary>
    /// <returns>X.509 证书的十六进制表示形式。</returns>
    public string X509Certificate
    {
      get
      {
        return this.m_x509cert;
      }
      set
      {
        this.m_x509cert = value;
      }
    }

    /// <summary>获取或设置包含 Authenticode X.509v3 证书的证书文件。</summary>
    /// <returns>X.509 证书文件（通常有 .cer 扩展名）的文件路径。</returns>
    public string CertFile
    {
      get
      {
        return this.m_certFile;
      }
      set
      {
        this.m_certFile = value;
      }
    }

    /// <summary>获取或设置一个已签名的文件，将从该文件提取 Authenticode X.509v3 证书。</summary>
    /// <returns>带有 Authenticode 签名的文件的文件路径。</returns>
    public string SignedFile
    {
      get
      {
        return this.m_signedFile;
      }
      set
      {
        this.m_signedFile = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.PublisherIdentityPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    public PublisherIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
      this.m_x509cert = (string) null;
      this.m_certFile = (string) null;
      this.m_signedFile = (string) null;
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> 实例。</summary>
    /// <returns>对应于此特性的 <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new PublisherIdentityPermission(PermissionState.Unrestricted);
      if (this.m_x509cert != null)
        return (IPermission) new PublisherIdentityPermission(new System.Security.Cryptography.X509Certificates.X509Certificate(Hex.DecodeHexString(this.m_x509cert)));
      if (this.m_certFile != null)
        return (IPermission) new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.m_certFile));
      if (this.m_signedFile != null)
        return (IPermission) new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.m_signedFile));
      return (IPermission) new PublisherIdentityPermission(PermissionState.None);
    }
  }
}
