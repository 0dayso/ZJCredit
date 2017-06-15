// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSACryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>定义访问 <see cref="T:System.Security.Cryptography.DSA" /> 算法的加密服务提供程序 (CSP) 实现的包装对象。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
  {
    private int _dwKeySize;
    private CspParameters _parameters;
    private bool _randomKeyContainer;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;
    [SecurityCritical]
    private SafeKeyHandle _safeKeyHandle;
    private SHA1CryptoServiceProvider _sha1;
    private static volatile CspProviderFlags s_UseMachineKeyStore;

    /// <summary>获取一个值，该值指示 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 对象是否仅包含一个公钥。</summary>
    /// <returns>如果 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 对象仅包含一个公钥，则为 true；否则为 false。</returns>
    [ComVisible(false)]
    public bool PublicOnly
    {
      [SecuritySafeCritical] get
      {
        this.GetKeyPair();
        return (int) Utils._GetKeyParameter(this._safeKeyHandle, 2U)[0] == 1;
      }
    }

    /// <summary>获取描述有关加密密钥对的附加信息的 <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> 对象。</summary>
    /// <returns>描述有关加密密钥对的附加信息的 <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> 对象。</returns>
    [ComVisible(false)]
    public CspKeyContainerInfo CspKeyContainerInfo
    {
      [SecuritySafeCritical] get
      {
        this.GetKeyPair();
        return new CspKeyContainerInfo(this._parameters, this._randomKeyContainer);
      }
    }

    /// <summary>获取不对称算法使用的密钥的大小（以位为单位）。</summary>
    /// <returns>不对称算法使用的密钥的大小。</returns>
    public override int KeySize
    {
      [SecuritySafeCritical] get
      {
        this.GetKeyPair();
        byte[] keyParameter = Utils._GetKeyParameter(this._safeKeyHandle, 1U);
        this._dwKeySize = (int) keyParameter[0] | (int) keyParameter[1] << 8 | (int) keyParameter[2] << 16 | (int) keyParameter[3] << 24;
        return this._dwKeySize;
      }
    }

    /// <summary>获取密钥交换算法的名称。</summary>
    /// <returns>密钥交换算法的名称。</returns>
    public override string KeyExchangeAlgorithm
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>获取签名算法的名称。</summary>
    /// <returns>签名算法的名称。</returns>
    public override string SignatureAlgorithm
    {
      get
      {
        return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
      }
    }

    /// <summary>获取或设置一个值，该值指示密钥是否应保留在计算机的密钥存储中（而不是保留在用户配置文件存储中）。</summary>
    /// <returns>如果密钥应保留在计算机的密钥存储中，则为 true；否则为 false。</returns>
    public static bool UseMachineKeyStore
    {
      get
      {
        return DSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
      }
      set
      {
        DSACryptoServiceProvider.s_UseMachineKeyStore = value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
      }
    }

    /// <summary>获取或设置一个值，该值指示密钥是否应保留在加密服务提供程序 (CSP) 中。</summary>
    /// <returns>如果密钥应保留在 CSP 中，则为 true；否则为 false。</returns>
    public bool PersistKeyInCsp
    {
      [SecuritySafeCritical] get
      {
        if (this._safeProvHandle == null)
        {
          lock (this)
          {
            if (this._safeProvHandle == null)
              this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
          }
        }
        return Utils.GetPersistKeyInCsp(this._safeProvHandle);
      }
      [SecuritySafeCritical] set
      {
        bool persistKeyInCsp = this.PersistKeyInCsp;
        if (value == persistKeyInCsp)
          return;
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        if (!value)
        {
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Delete);
          containerPermission.AccessEntries.Add(accessEntry);
        }
        else
        {
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Create);
          containerPermission.AccessEntries.Add(accessEntry);
        }
        containerPermission.Demand();
        Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 类的新实例。</summary>
    public DSACryptoServiceProvider()
      : this(0, new CspParameters(13, (string) null, (string) null, DSACryptoServiceProvider.s_UseMachineKeyStore))
    {
    }

    /// <summary>使用指定的密钥大小初始化 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="dwKeySize">不对称算法的密钥的大小（以位为单位）。</param>
    public DSACryptoServiceProvider(int dwKeySize)
      : this(dwKeySize, new CspParameters(13, (string) null, (string) null, DSACryptoServiceProvider.s_UseMachineKeyStore))
    {
    }

    /// <summary>用加密服务提供程序 (CSP) 的指定参数初始化 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="parameters">CSP 的参数。</param>
    public DSACryptoServiceProvider(CspParameters parameters)
      : this(0, parameters)
    {
    }

    /// <summary>用加密服务提供程序 (CSP) 的指定密钥大小和参数初始化 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="dwKeySize">加密算法的密钥的大小（以位为单位）。</param>
    /// <param name="parameters">CSP 的参数。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取 CSP。- 或 -不能创建密钥。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="dwKeySize" /> 超出范围。</exception>
    [SecuritySafeCritical]
    public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
    {
      if (dwKeySize < 0)
        throw new ArgumentOutOfRangeException("dwKeySize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Dss, parameters, DSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
      this.LegalKeySizesValue = new KeySizes[1]
      {
        new KeySizes(512, 1024, 64)
      };
      this._dwKeySize = dwKeySize;
      this._sha1 = new SHA1CryptoServiceProvider();
      if (this._randomKeyContainer && !Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
        return;
      this.GetKeyPair();
    }

    [SecurityCritical]
    private void GetKeyPair()
    {
      if (this._safeKeyHandle != null)
        return;
      lock (this)
      {
        if (this._safeKeyHandle != null)
          return;
        Utils.GetKeyPairHelper(CspAlgorithmType.Dss, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
      }
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
        this._safeKeyHandle.Dispose();
      if (this._safeProvHandle == null || this._safeProvHandle.IsClosed)
        return;
      this._safeProvHandle.Dispose();
    }

    /// <summary>导出 <see cref="T:System.Security.Cryptography.DSAParameters" />。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.DSA" /> 的参数。</returns>
    /// <param name="includePrivateParameters">要包括私有参数，则为 true；否则为 false。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">不能导出该密钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override DSAParameters ExportParameters(bool includePrivateParameters)
    {
      this.GetKeyPair();
      if (includePrivateParameters)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      DSACspObject dsaCspObject = new DSACspObject();
      Utils._ExportKey(this._safeKeyHandle, includePrivateParameters ? 7 : 6, (object) dsaCspObject);
      return DSACryptoServiceProvider.DSAObjectToStruct(dsaCspObject);
    }

    /// <summary>导出包含与 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 对象关联的密钥信息的 Blob。</summary>
    /// <returns>一个字节数组，包含与 <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> 对象关联的密钥信息。</returns>
    /// <param name="includePrivateParameters">要包括私钥，则为 true；否则为 false。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public byte[] ExportCspBlob(bool includePrivateParameters)
    {
      this.GetKeyPair();
      return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
    }

    /// <summary>导入指定的 <see cref="T:System.Security.Cryptography.DSAParameters" />。</summary>
    /// <param name="parameters">
    /// <see cref="T:System.Security.Cryptography.DSA" /> 的参数。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -<paramref name="parameters" /> 参数具有缺少的字段。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void ImportParameters(DSAParameters parameters)
    {
      DSACspObject @object = DSACryptoServiceProvider.DSAStructToObject(parameters);
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
        this._safeKeyHandle.Dispose();
      this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
      if (DSACryptoServiceProvider.IsPublic(parameters))
      {
        Utils._ImportKey(Utils.StaticDssProvHandle, 8704, CspProviderFlags.NoFlags, (object) @object, ref this._safeKeyHandle);
      }
      else
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
        if (this._safeProvHandle == null)
          this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
        Utils._ImportKey(this._safeProvHandle, 8704, this._parameters.Flags, (object) @object, ref this._safeKeyHandle);
      }
    }

    /// <summary>导入一个表示 DSA 密钥信息的 Blob。</summary>
    /// <param name="keyBlob">一个表示 DSA 密钥 Blob 的字节数组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void ImportCspBlob(byte[] keyBlob)
    {
      Utils.ImportCspBlobHelper(CspAlgorithmType.Dss, keyBlob, DSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
    }

    /// <summary>计算指定输入流的哈希值并对结果哈希值签名。</summary>
    /// <returns>指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</returns>
    /// <param name="inputStream">要计算其哈希值的输入数据。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignData(Stream inputStream)
    {
      return this.SignHash(this._sha1.ComputeHash(inputStream), (string) null);
    }

    /// <summary>计算指定字节数组的哈希值并对结果哈希值签名。</summary>
    /// <returns>指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</returns>
    /// <param name="buffer">要计算其哈希值的输入数据。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignData(byte[] buffer)
    {
      return this.SignHash(this._sha1.ComputeHash(buffer), (string) null);
    }

    /// <summary>对字节数组从指定起始点到指定结束点进行签名。</summary>
    /// <returns>指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</returns>
    /// <param name="buffer">要签名的输入数据。</param>
    /// <param name="offset">数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="count">数组中用作数据的字节数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignData(byte[] buffer, int offset, int count)
    {
      return this.SignHash(this._sha1.ComputeHash(buffer, offset, count), (string) null);
    }

    /// <summary>通过将指定的签名数据与为指定数据计算的签名进行比较来验证指定的签名数据。</summary>
    /// <returns>如果签名验证为有效，则为 true；否则，为 false。</returns>
    /// <param name="rgbData">已签名的数据。</param>
    /// <param name="rgbSignature">要验证的签名数据。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
    {
      return this.VerifyHash(this._sha1.ComputeHash(rgbData), (string) null, rgbSignature);
    }

    /// <summary>创建指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</summary>
    /// <returns>指定数据的数字签名。</returns>
    /// <param name="rgbHash">要签名的数据。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override byte[] CreateSignature(byte[] rgbHash)
    {
      return this.SignHash(rgbHash, (string) null);
    }

    /// <summary>验证指定数据的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</summary>
    /// <returns>如果 <paramref name="rgbSignature" /> 与使用指定的哈希算法和密钥在 <paramref name="rgbHash" /> 上计算出的签名匹配，则为 true；否则为 false。</returns>
    /// <param name="rgbHash">用 <paramref name="rgbSignature" /> 签名的数据。</param>
    /// <param name="rgbSignature">要为 <paramref name="rgbData" /> 验证的签名。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
    {
      return this.VerifyHash(rgbHash, (string) null, rgbSignature);
    }

    /// <summary>通过用私钥对其进行加密来计算指定哈希值的签名。</summary>
    /// <returns>指定哈希值的 <see cref="T:System.Security.Cryptography.DSA" /> 签名。</returns>
    /// <param name="rgbHash">要签名的数据的哈希值。</param>
    /// <param name="str">用于创建数据的哈希值的哈希算法名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbHash" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -没有私钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public byte[] SignHash(byte[] rgbHash, string str)
    {
      if (rgbHash == null)
        throw new ArgumentNullException("rgbHash");
      if (this.PublicOnly)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      if (rgbHash.Length != this._sha1.HashSize / 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHashSize", (object) "SHA1", (object) (this._sha1.HashSize / 8)));
      this.GetKeyPair();
      if (!this.CspKeyContainerInfo.RandomlyGenerated)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 8704, algId, rgbHash);
    }

    /// <summary>通过将指定的签名数据与为指定哈希值计算的签名进行比较来验证指定的签名数据。</summary>
    /// <returns>如果签名验证为有效，则为 true；否则，为 false。</returns>
    /// <param name="rgbHash">要签名的数据的哈希值。</param>
    /// <param name="str">用于创建数据的哈希值的哈希算法名称。</param>
    /// <param name="rgbSignature">要验证的签名数据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbHash" /> 参数为 null。- 或 -<paramref name="rgbSignature" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -不能验证签名。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
    {
      if (rgbHash == null)
        throw new ArgumentNullException("rgbHash");
      if (rgbSignature == null)
        throw new ArgumentNullException("rgbSignature");
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      if (rgbHash.Length != this._sha1.HashSize / 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHashSize", (object) "SHA1", (object) (this._sha1.HashSize / 8)));
      this.GetKeyPair();
      return Utils.VerifySign(this._safeKeyHandle, 8704, algId, rgbHash, rgbSignature);
    }

    private static DSAParameters DSAObjectToStruct(DSACspObject dsaCspObject)
    {
      return new DSAParameters() { P = dsaCspObject.P, Q = dsaCspObject.Q, G = dsaCspObject.G, Y = dsaCspObject.Y, J = dsaCspObject.J, X = dsaCspObject.X, Seed = dsaCspObject.Seed, Counter = dsaCspObject.Counter };
    }

    private static DSACspObject DSAStructToObject(DSAParameters dsaParams)
    {
      return new DSACspObject() { P = dsaParams.P, Q = dsaParams.Q, G = dsaParams.G, Y = dsaParams.Y, J = dsaParams.J, X = dsaParams.X, Seed = dsaParams.Seed, Counter = dsaParams.Counter };
    }

    private static bool IsPublic(DSAParameters dsaParams)
    {
      return dsaParams.X == null;
    }

    private static bool IsPublic(byte[] keyBlob)
    {
      if (keyBlob == null)
        throw new ArgumentNullException("keyBlob");
      return (int) keyBlob[0] == 6 && ((int) keyBlob[11] == 49 || (int) keyBlob[11] == 51) && ((int) keyBlob[10] == 83 && (int) keyBlob[9] == 83 && (int) keyBlob[8] == 68);
    }
  }
}
