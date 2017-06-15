// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509Certificate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
  /// <summary>提供帮助你使用 X.509 v.3 证书的方法。</summary>
  [ComVisible(true)]
  [Serializable]
  public class X509Certificate : IDisposable, IDeserializationCallback, ISerializable
  {
    private const string m_format = "X509";
    private string m_subjectName;
    private string m_issuerName;
    private byte[] m_serialNumber;
    private byte[] m_publicKeyParameters;
    private byte[] m_publicKeyValue;
    private string m_publicKeyOid;
    private byte[] m_rawData;
    private byte[] m_thumbprint;
    private DateTime m_notBefore;
    private DateTime m_notAfter;
    [SecurityCritical]
    private SafeCertContextHandle m_safeCertContext;
    private bool m_certContextCloned;

    /// <summary>获取非托管 PCCERT_CONTEXT 结构所描述的 Microsoft Cryptographic API 证书上下文的句柄。</summary>
    /// <returns>表示非托管 PCCERT_CONTEXT 结构的 <see cref="T:System.IntPtr" /> 结构。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ComVisible(false)]
    public IntPtr Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.m_safeCertContext.pCertContext;
      }
    }

    /// <summary>获取颁发此 X.509v3 证书的证书颁发机构的名称。</summary>
    /// <returns>颁发此 X.509v3 证书的证书颁发机构的名称。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书句柄无效。</exception>
    public string Issuer
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_issuerName == null)
          this.m_issuerName = X509Utils._GetIssuerName(this.m_safeCertContext, false);
        return this.m_issuerName;
      }
    }

    /// <summary>获取证书的主题可分辨名称。</summary>
    /// <returns>证书的主题可分辨名称。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书句柄无效。</exception>
    public string Subject
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_subjectName == null)
          this.m_subjectName = X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, false);
        return this.m_subjectName;
      }
    }

    internal SafeCertContextHandle CertContext
    {
      [SecurityCritical] get
      {
        return this.m_safeCertContext;
      }
    }

    private DateTime NotAfter
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_notAfter == DateTime.MinValue)
        {
          Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME();
          X509Utils._GetDateNotAfter(this.m_safeCertContext, ref fileTime);
          this.m_notAfter = DateTime.FromFileTime(fileTime.ToTicks());
        }
        return this.m_notAfter;
      }
    }

    private DateTime NotBefore
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_notBefore == DateTime.MinValue)
        {
          Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME();
          X509Utils._GetDateNotBefore(this.m_safeCertContext, ref fileTime);
          this.m_notBefore = DateTime.FromFileTime(fileTime.ToTicks());
        }
        return this.m_notBefore;
      }
    }

    private byte[] RawData
    {
      [SecurityCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_rawData == null)
          this.m_rawData = X509Utils._GetCertRawData(this.m_safeCertContext);
        return (byte[]) this.m_rawData.Clone();
      }
    }

    private string SerialNumber
    {
      [SecuritySafeCritical] get
      {
        this.ThrowIfContextInvalid();
        if (this.m_serialNumber == null)
          this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
        return Hex.EncodeHexStringFromInt(this.m_serialNumber);
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    public X509Certificate()
    {
      this.Init();
    }

    /// <summary>初始化从表示 X.509v3 证书的字节序列定义的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="data">一个包含 X.509 证书数据的字节数组。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    public X509Certificate(byte[] data)
      : this()
    {
      if (data == null || data.Length == 0)
        return;
      this.LoadCertificateFromBlob(data, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用一个字节数组和一个密码初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="rawData">一个包含 X.509 证书数据的字节数组。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    public X509Certificate(byte[] rawData, string password)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用一个字节数组和一个密码初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="rawData">包含 X.509 证书数据的字节数组。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    public X509Certificate(byte[] rawData, SecureString password)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用一个字节数组、一个密码和一个密钥存储标志初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="rawData">一个包含 X.509 证书数据的字节数组。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>使用一个字节数组、一个密码和一个密钥存储标志初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="rawData">包含 X.509 证书数据的字节数组。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>使用 PKCS7 签名文件的名称初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="fileName">PKCS7 签名文件的名称。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用 PKCS7 签名文件的名称和一个用于访问该证书的密码初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="fileName">PKCS7 签名文件的名称。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, string password)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用一个证书文件名和一个密码初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="fileName">一个证书文件的名称。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, SecureString password)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用 PKCS7 签名文件的名称、一个用于访问该证书的密码和一个密钥存储标志初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="fileName">PKCS7 签名文件的名称。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>使用一个证书文件名、一个密码和一个密钥存储标志初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的新实例。</summary>
    /// <param name="fileName">一个证书文件的名称。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
      : this()
    {
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>使用非托管 PCCERT_CONTEXT 结构的句柄初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的一个新实例。</summary>
    /// <param name="handle">非托管 PCCERT_CONTEXT 结构的一个句柄。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentException">句柄参数未表示有效的 PCCERT_CONTEXT 结构。</exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public X509Certificate(IntPtr handle)
      : this()
    {
      if (handle == IntPtr.Zero)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHandle"), "handle");
      X509Utils._DuplicateCertContext(handle, ref this.m_safeCertContext);
    }

    /// <summary>使用另一个 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的一个新实例。</summary>
    /// <param name="cert">从中初始化此类的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cert" /> 参数的值为 null。</exception>
    [SecuritySafeCritical]
    public X509Certificate(X509Certificate cert)
      : this()
    {
      if (cert == null)
        throw new ArgumentNullException("cert");
      if (!(cert.m_safeCertContext.pCertContext != IntPtr.Zero))
        return;
      this.m_safeCertContext = cert.GetCertContextForCloning();
      this.m_certContextCloned = true;
    }

    /// <summary>使用一个 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象和一个 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 结构初始化 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 类的一个新实例。</summary>
    /// <param name="info">一个描述序列化信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</param>
    /// <param name="context">一个描述如何执行序列化的 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 结构。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    public X509Certificate(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] rawData = (byte[]) info.GetValue("RawData", typeof (byte[]));
      if (rawData == null)
        return;
      this.LoadCertificateFromBlob(rawData, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    [SecuritySafeCritical]
    private void Init()
    {
      this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
    }

    /// <summary>依据指定的 PKCS7 签名文件创建 X.509v3 证书。</summary>
    /// <returns>新创建的 X.509 证书。</returns>
    /// <param name="filename">从中创建 X.509 证书的 PKCS7 签名文件的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="filename" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public static X509Certificate CreateFromCertFile(string filename)
    {
      return new X509Certificate(filename);
    }

    /// <summary>依据指定的签名文件创建 X.509v3 证书。</summary>
    /// <returns>新创建的 X.509 证书。</returns>
    /// <param name="filename">从中创建 X.509 证书的签名文件的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    public static X509Certificate CreateFromSignedFile(string filename)
    {
      return new X509Certificate(filename);
    }

    /// <summary>返回已向其颁发证书的主体的名称。</summary>
    /// <returns>已向其颁发证书的主体的名称。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书上下文无效。</exception>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated.  Please use the Subject property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual string GetName()
    {
      this.ThrowIfContextInvalid();
      return X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, true);
    }

    /// <summary>返回颁发此 X.509v3 证书的证书颁发机构的名称。</summary>
    /// <returns>颁发此 X.509 证书的证书颁发机构的名称。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">发生了与证书有关的错误。例如：证书文件不存在。证书无效。证书的密码不正确。</exception>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated.  Please use the Issuer property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual string GetIssuerName()
    {
      this.ThrowIfContextInvalid();
      return X509Utils._GetIssuerName(this.m_safeCertContext, true);
    }

    /// <summary>将 X.509v3 证书的序列号作为字节数组返回。</summary>
    /// <returns>字节数组形式的 X.509 证书的序列号。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书上下文无效。</exception>
    [SecuritySafeCritical]
    public virtual byte[] GetSerialNumber()
    {
      this.ThrowIfContextInvalid();
      if (this.m_serialNumber == null)
        this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
      return (byte[]) this.m_serialNumber.Clone();
    }

    /// <summary>将 X.509v3 证书的序列号作为十六进制字符串返回。</summary>
    /// <returns>十六进制字符串形式的 X.509 证书的序列号。</returns>
    public virtual string GetSerialNumberString()
    {
      return this.SerialNumber;
    }

    /// <summary>将 X.509v3 证书的密钥算法参数作为字节数组返回。</summary>
    /// <returns>字节数组形式的 X.509 证书的密钥算法参数。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书上下文无效。</exception>
    [SecuritySafeCritical]
    public virtual byte[] GetKeyAlgorithmParameters()
    {
      this.ThrowIfContextInvalid();
      if (this.m_publicKeyParameters == null)
        this.m_publicKeyParameters = X509Utils._GetPublicKeyParameters(this.m_safeCertContext);
      return (byte[]) this.m_publicKeyParameters.Clone();
    }

    /// <summary>将 X.509v3 证书的密钥算法参数作为十六进制字符串返回。</summary>
    /// <returns>十六进制字符串形式的 X.509 证书的密钥算法参数。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书上下文无效。</exception>
    [SecuritySafeCritical]
    public virtual string GetKeyAlgorithmParametersString()
    {
      this.ThrowIfContextInvalid();
      return Hex.EncodeHexString(this.GetKeyAlgorithmParameters());
    }

    /// <summary>将此 X.509v3 证书的密钥算法信息作为字符串返回。</summary>
    /// <returns>字符串形式的 X.509 证书的密钥算法信息。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书上下文无效。</exception>
    [SecuritySafeCritical]
    public virtual string GetKeyAlgorithm()
    {
      this.ThrowIfContextInvalid();
      if (this.m_publicKeyOid == null)
        this.m_publicKeyOid = X509Utils._GetPublicKeyOid(this.m_safeCertContext);
      return this.m_publicKeyOid;
    }

    /// <summary>将 X.509v3 证书的公钥作为字节数组返回。</summary>
    /// <returns>字节数组形式的 X.509 证书的公钥。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">证书上下文无效。</exception>
    [SecuritySafeCritical]
    public virtual byte[] GetPublicKey()
    {
      this.ThrowIfContextInvalid();
      if (this.m_publicKeyValue == null)
        this.m_publicKeyValue = X509Utils._GetPublicKeyValue(this.m_safeCertContext);
      return (byte[]) this.m_publicKeyValue.Clone();
    }

    /// <summary>将 X.509v3 证书的公钥作为十六进制字符串返回。</summary>
    /// <returns>十六进制字符串形式的 X.509 证书的公钥。</returns>
    public virtual string GetPublicKeyString()
    {
      return Hex.EncodeHexString(this.GetPublicKey());
    }

    /// <summary>将整个 X.509v3 证书的原始数据作为字节数组返回。</summary>
    /// <returns>包含 X.509 证书数据的字节数组。</returns>
    [SecuritySafeCritical]
    public virtual byte[] GetRawCertData()
    {
      return this.RawData;
    }

    /// <summary>将整个 X.509v3 证书的原始数据作为十六进制字符串返回。</summary>
    /// <returns>十六进制字符串形式的 X.509 证书数据。</returns>
    public virtual string GetRawCertDataString()
    {
      return Hex.EncodeHexString(this.GetRawCertData());
    }

    /// <summary>将 X.509v3 证书的哈希值作为字节数组返回。</summary>
    /// <returns>X.509 证书的哈希值。</returns>
    public virtual byte[] GetCertHash()
    {
      this.SetThumbprint();
      return (byte[]) this.m_thumbprint.Clone();
    }

    /// <summary>将 X.509v3 证书的 SHA1 哈希值作为十六进制字符串返回。</summary>
    /// <returns>X.509 证书哈希值的十六进制字符串表示形式。</returns>
    public virtual string GetCertHashString()
    {
      this.SetThumbprint();
      return Hex.EncodeHexString(this.m_thumbprint);
    }

    /// <summary>返回此 X.509v3 证书的有效日期。</summary>
    /// <returns>此 X.509 证书的有效日期。</returns>
    public virtual string GetEffectiveDateString()
    {
      return this.NotBefore.ToString();
    }

    /// <summary>返回此 X.509v3 证书的到期日期。</summary>
    /// <returns>此 X.509 证书的到期日期。</returns>
    public virtual string GetExpirationDateString()
    {
      return this.NotAfter.ToString();
    }

    /// <summary>比较两个 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象是否相等。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象等于 <paramref name="other" /> 参数指定的对象，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前对象进行比较的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。 </param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      if (!(obj is X509Certificate))
        return false;
      return this.Equals((X509Certificate) obj);
    }

    /// <summary>比较两个 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象是否相等。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象等于 <paramref name="other" /> 参数指定的对象，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前对象进行比较的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</param>
    [SecuritySafeCritical]
    public virtual bool Equals(X509Certificate other)
    {
      if (other == null)
        return false;
      if (this.m_safeCertContext.IsInvalid)
        return other.m_safeCertContext.IsInvalid;
      return this.Issuer.Equals(other.Issuer) && this.SerialNumber.Equals(other.SerialNumber);
    }

    /// <summary>返回整数形式的 X.509v3 证书的哈希代码。</summary>
    /// <returns>整数形式的 X.509 证书的哈希代码。</returns>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      if (this.m_safeCertContext.IsInvalid)
        return 0;
      this.SetThumbprint();
      int num = 0;
      for (int index = 0; index < this.m_thumbprint.Length && index < 4; ++index)
        num = num << 8 | (int) this.m_thumbprint[index];
      return num;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字符串表示形式。</returns>
    public override string ToString()
    {
      return this.ToString(false);
    }

    /// <summary>返回当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字符串表示形式，如果指定，带有其他信息。</summary>
    /// <returns>当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字符串表示形式。</returns>
    /// <param name="fVerbose">true to produce the verbose form of the string representation; otherwise, false.</param>
    [SecuritySafeCritical]
    public virtual string ToString(bool fVerbose)
    {
      if (!fVerbose || this.m_safeCertContext.IsInvalid)
        return this.GetType().FullName;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("[Subject]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.Subject);
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Issuer]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.Issuer);
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Serial Number]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.SerialNumber);
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Not Before]" + Environment.NewLine + "  ");
      stringBuilder.Append(X509Certificate.FormatDate(this.NotBefore));
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Not After]" + Environment.NewLine + "  ");
      stringBuilder.Append(X509Certificate.FormatDate(this.NotAfter));
      stringBuilder.Append(Environment.NewLine + Environment.NewLine + "[Thumbprint]" + Environment.NewLine + "  ");
      stringBuilder.Append(this.GetCertHashString());
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    /// <summary>将指定的日期和时间转换为字符串。</summary>
    /// <returns>
    /// <see cref="T:System.DateTime" /> 对象的值的字符串表示形式。</returns>
    /// <param name="date">要转换的日期和时间。</param>
    protected static string FormatDate(DateTime date)
    {
      CultureInfo cultureInfo = CultureInfo.CurrentCulture;
      if (!cultureInfo.DateTimeFormat.Calendar.IsValidDay(date.Year, date.Month, date.Day, 0))
      {
        if (cultureInfo.DateTimeFormat.Calendar is UmAlQuraCalendar)
        {
          cultureInfo = cultureInfo.Clone() as CultureInfo;
          cultureInfo.DateTimeFormat.Calendar = (Calendar) new HijriCalendar();
        }
        else
          cultureInfo = CultureInfo.InvariantCulture;
      }
      return date.ToString((IFormatProvider) cultureInfo);
    }

    /// <summary>返回此 X.509v3 证书的格式的名称。</summary>
    /// <returns>此 X.509 证书的格式。</returns>
    public virtual string GetFormat()
    {
      return "X509";
    }

    /// <summary>使用字节数组中的数据填充 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</summary>
    /// <param name="rawData">一个包含 X.509 证书数据的字节数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(byte[] rawData)
    {
      this.Reset();
      this.LoadCertificateFromBlob(rawData, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用一个字节数组中的数据、一个密码和用于确定如何导入私钥的标志填充 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</summary>
    /// <param name="rawData">一个包含 X.509 证书数据的字节数组。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>使用一个字节数组中的数据、一个密码和一个密钥存储标志填充 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</summary>
    /// <param name="rawData">包含 X.509 证书数据的字节数组。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="rawData" /> 参数为 null。- 或 -<paramref name="rawData" /> 参数的长度为 0。</exception>
    [SecurityCritical]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromBlob(rawData, (object) password, keyStorageFlags);
    }

    /// <summary>使用证书文件中的信息填充 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</summary>
    /// <param name="fileName">以字符串形式表示的证书文件的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(string fileName)
    {
      this.Reset();
      this.LoadCertificateFromFile(fileName, (object) null, X509KeyStorageFlags.DefaultKeySet);
    }

    /// <summary>使用一个证书文件中的信息、一个密码和一个 <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> 值填充 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</summary>
    /// <param name="fileName">以字符串形式表示的证书文件的名称。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>使用一个证书文件中的信息，一个密码和一个密钥存储标志填充 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。</summary>
    /// <param name="fileName">一个证书文件的名称。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <param name="keyStorageFlags">一个枚举值的按位组合，这些值控制在何处以及如何导入证书。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    [SecurityCritical]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
    {
      this.Reset();
      this.LoadCertificateFromFile(fileName, (object) password, keyStorageFlags);
    }

    /// <summary>以 <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> 值之一所描述的格式将当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象导出到字节数组。</summary>
    /// <returns>表示当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字节数组。</returns>
    /// <param name="contentType">描述如何设置输出数据格式的 <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> 值之一。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">将 <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />、<see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" /> 或 <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> 之外的一个值传递给 <paramref name="contentType" /> 参数。- 或 -无法导出证书。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public virtual byte[] Export(X509ContentType contentType)
    {
      return this.ExportHelper(contentType, (object) null);
    }

    /// <summary>使用指定的密码，以 <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> 值之一所描述的格式将当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象导出到字节数组。</summary>
    /// <returns>表示当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字节数组。</returns>
    /// <param name="contentType">描述如何设置输出数据格式的 <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> 值之一。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">将 <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />、<see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" /> 或 <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> 之外的一个值传递给 <paramref name="contentType" /> 参数。- 或 -无法导出证书。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public virtual byte[] Export(X509ContentType contentType, string password)
    {
      return this.ExportHelper(contentType, (object) password);
    }

    /// <summary>使用指定的格式和密码将当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象导出到字节数组。</summary>
    /// <returns>表示当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象的字节数组。</returns>
    /// <param name="contentType">描述如何设置输出数据格式的 <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> 值之一。</param>
    /// <param name="password">访问 X.509 证书数据所需的密码。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">将 <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />、<see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" /> 或 <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> 之外的一个值传递给 <paramref name="contentType" /> 参数。- 或 -无法导出证书。</exception>
    [SecuritySafeCritical]
    public virtual byte[] Export(X509ContentType contentType, SecureString password)
    {
      return this.ExportHelper(contentType, (object) password);
    }

    /// <summary>重置 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> 对象的状态。</summary>
    [SecurityCritical]
    [ComVisible(false)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual void Reset()
    {
      this.m_subjectName = (string) null;
      this.m_issuerName = (string) null;
      this.m_serialNumber = (byte[]) null;
      this.m_publicKeyParameters = (byte[]) null;
      this.m_publicKeyValue = (byte[]) null;
      this.m_publicKeyOid = (string) null;
      this.m_rawData = (byte[]) null;
      this.m_thumbprint = (byte[]) null;
      this.m_notBefore = DateTime.MinValue;
      this.m_notAfter = DateTime.MinValue;
      if (!this.m_safeCertContext.IsInvalid)
      {
        if (!this.m_certContextCloned)
          this.m_safeCertContext.Dispose();
        this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
      }
      this.m_certContextCloned = false;
    }

    /// <summary>释放由当前 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>释放由此使用的非托管资源的所有<see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />并选择性地释放托管的资源。 </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    [SecuritySafeCritical]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.Reset();
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (this.m_safeCertContext.IsInvalid)
        info.AddValue("RawData", (object) null);
      else
        info.AddValue("RawData", (object) this.RawData);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    [SecurityCritical]
    internal SafeCertContextHandle GetCertContextForCloning()
    {
      this.m_certContextCloned = true;
      return this.m_safeCertContext;
    }

    [SecurityCritical]
    private void ThrowIfContextInvalid()
    {
      if (this.m_safeCertContext.IsInvalid)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
    }

    [SecuritySafeCritical]
    private void SetThumbprint()
    {
      this.ThrowIfContextInvalid();
      if (this.m_thumbprint != null)
        return;
      this.m_thumbprint = X509Utils._GetThumbprint(this.m_safeCertContext);
    }

    [SecurityCritical]
    private byte[] ExportHelper(X509ContentType contentType, object password)
    {
      switch (contentType)
      {
        case X509ContentType.Cert:
        case X509ContentType.SerializedCert:
          IntPtr num = IntPtr.Zero;
          byte[] numArray = (byte[]) null;
          SafeCertStoreHandle memoryStore = X509Utils.ExportCertToMemoryStore(this);
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            num = X509Utils.PasswordToHGlobalUni(password);
            numArray = X509Utils._ExportCertificatesToBlob(memoryStore, contentType, num);
          }
          finally
          {
            if (num != IntPtr.Zero)
              Marshal.ZeroFreeGlobalAllocUnicode(num);
            memoryStore.Dispose();
          }
          if (numArray == null)
            throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_ExportFailed"));
          return numArray;
        case X509ContentType.Pfx:
          new KeyContainerPermission(KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Export).Demand();
          goto case X509ContentType.Cert;
        default:
          throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_InvalidContentType"));
      }
    }

    [SecuritySafeCritical]
    private void LoadCertificateFromBlob(byte[] rawData, object password, X509KeyStorageFlags keyStorageFlags)
    {
      if (rawData == null || rawData.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullArray"), "rawData");
      if (X509Utils.MapContentType(X509Utils._QueryCertBlobType(rawData)) == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
        new KeyContainerPermission(KeyContainerPermissionFlags.Create).Demand();
      uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = X509Utils.PasswordToHGlobalUni(password);
        X509Utils._LoadCertFromBlob(rawData, num, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, ref this.m_safeCertContext);
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.ZeroFreeGlobalAllocUnicode(num);
      }
    }

    [SecurityCritical]
    private void LoadCertificateFromFile(string fileName, object password, X509KeyStorageFlags keyStorageFlags)
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      new FileIOPermission(FileIOPermissionAccess.Read, Path.GetFullPathInternal(fileName)).Demand();
      if (X509Utils.MapContentType(X509Utils._QueryCertFileType(fileName)) == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
        new KeyContainerPermission(KeyContainerPermissionFlags.Create).Demand();
      uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = X509Utils.PasswordToHGlobalUni(password);
        X509Utils._LoadCertFromFile(fileName, num, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, ref this.m_safeCertContext);
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.ZeroFreeGlobalAllocUnicode(num);
      }
    }
  }
}
