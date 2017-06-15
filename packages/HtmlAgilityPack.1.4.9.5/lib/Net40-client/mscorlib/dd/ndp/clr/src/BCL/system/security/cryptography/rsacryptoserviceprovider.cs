// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSACryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>使用加密服务提供程序 (CSP) 提供的 <see cref="T:System.Security.Cryptography.RSA" /> 算法的实现执行不对称加密和解密。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
  {
    private int _dwKeySize;
    private CspParameters _parameters;
    private bool _randomKeyContainer;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;
    [SecurityCritical]
    private SafeKeyHandle _safeKeyHandle;
    private static volatile CspProviderFlags s_UseMachineKeyStore;

    /// <summary>获取一个值，该值指示 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 对象是否仅包含一个公钥。</summary>
    /// <returns>如果 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 对象仅包含一个公钥，则为 true；否则为 false。</returns>
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

    /// <summary>获取当前密钥的大小。</summary>
    /// <returns>密钥的大小（以位为单位）。</returns>
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

    /// <summary>获取 <see cref="T:System.Security.Cryptography.RSA" /> 的这一实现中可用的密钥交换算法的名称。</summary>
    /// <returns>如果存在密钥交换算法，则为密钥交换算法的名称；否则为 null。</returns>
    public override string KeyExchangeAlgorithm
    {
      get
      {
        if (this._parameters.KeyNumber == 1)
          return "RSA-PKCS1-KeyEx";
        return (string) null;
      }
    }

    /// <summary>获取 <see cref="T:System.Security.Cryptography.RSA" /> 的这一实现中可用的签名算法的名称。</summary>
    /// <returns>签名算法的名称。</returns>
    public override string SignatureAlgorithm
    {
      get
      {
        return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
      }
    }

    /// <summary>获取或设置一个值，该值指示密钥是否应保留在计算机的密钥存储中（而不是保留在用户配置文件存储中）。</summary>
    /// <returns>如果密钥应保留在计算机的密钥存储中，则为 true；否则为 false。</returns>
    public static bool UseMachineKeyStore
    {
      get
      {
        return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
      }
      set
      {
        RSACryptoServiceProvider.s_UseMachineKeyStore = value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags;
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
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        {
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
        }
        Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
      }
    }

    /// <summary>使用默认密钥初始化 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。</exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider()
      : this(0, new CspParameters(24, (string) null, (string) null, RSACryptoServiceProvider.s_UseMachineKeyStore), true)
    {
    }

    /// <summary>使用指定的密钥大小初始化 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="dwKeySize">要使用的密钥的大小（以位为单位）。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。</exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider(int dwKeySize)
      : this(dwKeySize, new CspParameters(24, (string) null, (string) null, RSACryptoServiceProvider.s_UseMachineKeyStore), false)
    {
    }

    /// <summary>使用指定的参数初始化 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="parameters">要传递给加密服务提供程序 (CSP) 的参数。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取 CSP。</exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider(CspParameters parameters)
      : this(0, parameters, true)
    {
    }

    /// <summary>使用指定的密钥大小和参数初始化 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="dwKeySize">要使用的密钥的大小（以位为单位）。</param>
    /// <param name="parameters">要传递给加密服务提供程序 (CSP) 的参数。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取 CSP。- 或 -不能创建密钥。</exception>
    [SecuritySafeCritical]
    public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
      : this(dwKeySize, parameters, false)
    {
    }

    [SecurityCritical]
    private RSACryptoServiceProvider(int dwKeySize, CspParameters parameters, bool useDefaultKeySize)
    {
      if (dwKeySize < 0)
        throw new ArgumentOutOfRangeException("dwKeySize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Rsa, parameters, RSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
      this.LegalKeySizesValue = new KeySizes[1]
      {
        new KeySizes(384, 16384, 8)
      };
      this._dwKeySize = useDefaultKeySize ? 1024 : dwKeySize;
      if (this._randomKeyContainer && !Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
        return;
      this.GetKeyPair();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DecryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbEncryptedKey, int cbEncryptedKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetDecryptedKey);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void EncryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbKey, int cbKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetEncryptedKey);

    [SecurityCritical]
    private void GetKeyPair()
    {
      if (this._safeKeyHandle != null)
        return;
      lock (this)
      {
        if (this._safeKeyHandle != null)
          return;
        Utils.GetKeyPairHelper(CspAlgorithmType.Rsa, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
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

    /// <summary>导出 <see cref="T:System.Security.Cryptography.RSAParameters" />。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RSA" /> 的参数。</returns>
    /// <param name="includePrivateParameters">要包括私有参数，则为 true；否则为 false。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">不能导出该密钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override RSAParameters ExportParameters(bool includePrivateParameters)
    {
      this.GetKeyPair();
      if (includePrivateParameters && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      RSACspObject rsaCspObject = new RSACspObject();
      Utils._ExportKey(this._safeKeyHandle, includePrivateParameters ? 7 : 6, (object) rsaCspObject);
      return RSACryptoServiceProvider.RSAObjectToStruct(rsaCspObject);
    }

    /// <summary>导出包含与 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 对象关联的密钥信息的 Blob。</summary>
    /// <returns>一个字节数组，包含与 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 对象关联的密钥信息。</returns>
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

    /// <summary>导入指定的 <see cref="T:System.Security.Cryptography.RSAParameters" />。</summary>
    /// <param name="parameters">
    /// <see cref="T:System.Security.Cryptography.RSA" /> 的参数。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -<paramref name="parameters" /> 参数具有缺少的字段。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void ImportParameters(RSAParameters parameters)
    {
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
      {
        this._safeKeyHandle.Dispose();
        this._safeKeyHandle = (SafeKeyHandle) null;
      }
      RSACspObject @object = RSACryptoServiceProvider.RSAStructToObject(parameters);
      this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
      if (RSACryptoServiceProvider.IsPublic(parameters))
      {
        Utils._ImportKey(Utils.StaticProvHandle, 41984, CspProviderFlags.NoFlags, (object) @object, ref this._safeKeyHandle);
      }
      else
      {
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        {
          KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
          containerPermission.AccessEntries.Add(accessEntry);
          containerPermission.Demand();
        }
        if (this._safeProvHandle == null)
          this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
        Utils._ImportKey(this._safeProvHandle, 41984, this._parameters.Flags, (object) @object, ref this._safeKeyHandle);
      }
    }

    /// <summary>导入一个表示 RSA 密钥信息的 Blob。</summary>
    /// <param name="keyBlob">一个表示 RSA 密钥 Blob 的字节数组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void ImportCspBlob(byte[] keyBlob)
    {
      Utils.ImportCspBlobHelper(CspAlgorithmType.Rsa, keyBlob, RSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
    }

    /// <summary>使用指定的哈希算法计算指定输入流的哈希值，并对计算所得的哈希值签名。</summary>
    /// <returns>指定数据的 <see cref="T:System.Security.Cryptography.RSA" /> 签名。</returns>
    /// <param name="inputStream">要计算其哈希值的输入数据。</param>
    /// <param name="halg">用于创建哈希值的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="halg" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="halg" /> 参数不是有效类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignData(Stream inputStream, object halg)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.SignHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(inputStream), algId);
    }

    /// <summary>使用指定的哈希算法计算指定字节数组的哈希值，并对计算所得的哈希值签名。</summary>
    /// <returns>指定数据的 <see cref="T:System.Security.Cryptography.RSA" /> 签名。</returns>
    /// <param name="buffer">要计算其哈希值的输入数据。</param>
    /// <param name="halg">用于创建哈希值的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="halg" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="halg" /> 参数不是有效类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignData(byte[] buffer, object halg)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.SignHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(buffer), algId);
    }

    /// <summary>使用指定的哈希算法计算指定字节数组子集的哈希值，并对结果哈希值签名。</summary>
    /// <returns>指定数据的 <see cref="T:System.Security.Cryptography.RSA" /> 签名。</returns>
    /// <param name="buffer">要计算其哈希值的输入数据。</param>
    /// <param name="offset">数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="count">数组中用作数据的字节数。</param>
    /// <param name="halg">用于创建哈希值的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="halg" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="halg" /> 参数不是有效类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignData(byte[] buffer, int offset, int count, object halg)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.SignHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(buffer, offset, count), algId);
    }

    /// <summary>通过使用提供的公钥确定签名中的哈希值并将其与所提供数据的哈希值进行比较验证数字签名是否有效。</summary>
    /// <returns>如果签名有效，则为 true；否则为 false。</returns>
    /// <param name="buffer">已签名的数据。</param>
    /// <param name="halg">用于创建数据的哈希值的哈希算法名称。</param>
    /// <param name="signature">要验证的签名数据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="halg" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="halg" /> 参数不是有效类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public bool VerifyData(byte[] buffer, object halg, byte[] signature)
    {
      int algId = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
      return this.VerifyHash(Utils.ObjToHashAlgorithm(halg).ComputeHash(buffer), algId, signature);
    }

    /// <summary>通过用私钥对其进行加密来计算指定哈希值的签名。</summary>
    /// <returns>指定哈希值的 <see cref="T:System.Security.Cryptography.RSA" /> 签名。</returns>
    /// <param name="rgbHash">要签名的数据的哈希值。</param>
    /// <param name="str">哈希算法标识符 (OID) 用于创建数据的哈希值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbHash" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -没有私钥。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] SignHash(byte[] rgbHash, string str)
    {
      if (rgbHash == null)
        throw new ArgumentNullException("rgbHash");
      if (this.PublicOnly)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      return this.SignHash(rgbHash, algId);
    }

    [SecuritySafeCritical]
    internal byte[] SignHash(byte[] rgbHash, int calgHash)
    {
      this.GetKeyPair();
      if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 9216, calgHash, rgbHash);
    }

    /// <summary>通过使用提供的公钥确定签名中的哈希值并将其与提供的哈希值进行比较来验证数字签名是否有效。</summary>
    /// <returns>如果签名有效，则为 true；否则为 false。</returns>
    /// <param name="rgbHash">带符号的数据的哈希值。</param>
    /// <param name="str">哈希算法标识符 (OID) 用于创建数据的哈希值。</param>
    /// <param name="rgbSignature">要验证的签名数据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbHash" /> 参数为 null。- 或 -<paramref name="rgbSignature" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -不能验证签名。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
    {
      if (rgbHash == null)
        throw new ArgumentNullException("rgbHash");
      if (rgbSignature == null)
        throw new ArgumentNullException("rgbSignature");
      int algId = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
      return this.VerifyHash(rgbHash, algId, rgbSignature);
    }

    [SecuritySafeCritical]
    internal bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
    {
      this.GetKeyPair();
      return Utils.VerifySign(this._safeKeyHandle, 9216, calgHash, rgbHash, rgbSignature);
    }

    /// <summary>使用 <see cref="T:System.Security.Cryptography.RSA" /> 算法对数据进行加密。</summary>
    /// <returns>已加密的数据。</returns>
    /// <param name="rgb">要加密的数据。</param>
    /// <param name="fOAEP">如果为 true，则使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 <see cref="T:System.Security.Cryptography.RSA" /> 加密；否则，如果为 false，则使用 PKCS#1 1.5 版填充。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -<paramref name="rgb" /> 参数的长度大于最大允许长度。- 或 -<paramref name="fOAEP" /> 参数为 true，而且不支持 OAEP 填充。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgb " />为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public byte[] Encrypt(byte[] rgb, bool fOAEP)
    {
      if (rgb == null)
        throw new ArgumentNullException("rgb");
      this.GetKeyPair();
      byte[] o = (byte[]) null;
      SafeKeyHandle pKeyContext = this._safeKeyHandle;
      byte[] pbKey = rgb;
      int length = pbKey.Length;
      int num = fOAEP ? 1 : 0;
      ObjectHandleOnStack objectHandleOnStack = JitHelpers.GetObjectHandleOnStack<byte[]>(ref o);
      RSACryptoServiceProvider.EncryptKey(pKeyContext, pbKey, length, num != 0, objectHandleOnStack);
      return o;
    }

    /// <summary>使用 <see cref="T:System.Security.Cryptography.RSA" /> 算法对数据进行解密。</summary>
    /// <returns>已解密的数据，它是加密前的原始纯文本。</returns>
    /// <param name="rgb">要解密的数据。</param>
    /// <param name="fOAEP">如果为 true，则使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 <see cref="T:System.Security.Cryptography.RSA" /> 解密；否则，如果为 false，则使用 PKCS#1 1.5 版填充。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法获取加密服务提供程序 (CSP)。- 或 -<paramref name="fOAEP" /> 参数为 true，并且 <paramref name="rgb" /> 参数的长度大于 <see cref="P:System.Security.Cryptography.RSACryptoServiceProvider.KeySize" />。- 或 -<paramref name="fOAEP" /> 参数为 true，而且不支持 OAEP。- 或 -密钥与加密数据不匹配。但是，异常文字可能不准确。例如，可以说 Not enough storage is available to process this command。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgb " />为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public byte[] Decrypt(byte[] rgb, bool fOAEP)
    {
      if (rgb == null)
        throw new ArgumentNullException("rgb");
      this.GetKeyPair();
      if (rgb.Length > this.KeySize / 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_DecDataTooBig", (object) (this.KeySize / 8)));
      if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Decrypt);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      byte[] o = (byte[]) null;
      SafeKeyHandle pKeyContext = this._safeKeyHandle;
      byte[] pbEncryptedKey = rgb;
      int length = pbEncryptedKey.Length;
      int num = fOAEP ? 1 : 0;
      ObjectHandleOnStack objectHandleOnStack = JitHelpers.GetObjectHandleOnStack<byte[]>(ref o);
      RSACryptoServiceProvider.DecryptKey(pKeyContext, pbEncryptedKey, length, num != 0, objectHandleOnStack);
      return o;
    }

    /// <summary>在当前版本中不支持此方法。</summary>
    /// <returns>已解密的数据，它是加密前的原始纯文本。</returns>
    /// <param name="rgb">要解密的数据。</param>
    /// <exception cref="T:System.NotSupportedException">在当前版本中不支持此方法。</exception>
    public override byte[] DecryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>在当前版本中不支持此方法。</summary>
    /// <returns>已加密的数据。</returns>
    /// <param name="rgb">要加密的数据。</param>
    /// <exception cref="T:System.NotSupportedException">在当前版本中不支持此方法。</exception>
    public override byte[] EncryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    private static RSAParameters RSAObjectToStruct(RSACspObject rsaCspObject)
    {
      return new RSAParameters() { Exponent = rsaCspObject.Exponent, Modulus = rsaCspObject.Modulus, P = rsaCspObject.P, Q = rsaCspObject.Q, DP = rsaCspObject.DP, DQ = rsaCspObject.DQ, InverseQ = rsaCspObject.InverseQ, D = rsaCspObject.D };
    }

    private static RSACspObject RSAStructToObject(RSAParameters rsaParams)
    {
      return new RSACspObject() { Exponent = rsaParams.Exponent, Modulus = rsaParams.Modulus, P = rsaParams.P, Q = rsaParams.Q, DP = rsaParams.DP, DQ = rsaParams.DQ, InverseQ = rsaParams.InverseQ, D = rsaParams.D };
    }

    private static bool IsPublic(byte[] keyBlob)
    {
      if (keyBlob == null)
        throw new ArgumentNullException("keyBlob");
      return (int) keyBlob[0] == 6 && (int) keyBlob[11] == 49 && ((int) keyBlob[10] == 65 && (int) keyBlob[9] == 83) && (int) keyBlob[8] == 82;
    }

    private static bool IsPublic(RSAParameters rsaParams)
    {
      return rsaParams.P == null;
    }

    [SecuritySafeCritical]
    protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      using (SafeHashHandle hash = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
      {
        Utils.HashData(hash, data, offset, count);
        return Utils.EndHash(hash);
      }
    }

    [SecuritySafeCritical]
    protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      using (SafeHashHandle hash = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
      {
        byte[] numArray = new byte[4096];
        int cbSize;
        do
        {
          cbSize = data.Read(numArray, 0, numArray.Length);
          if (cbSize > 0)
            Utils.HashData(hash, numArray, 0, cbSize);
        }
        while (cbSize > 0);
        return Utils.EndHash(hash);
      }
    }

    private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
    {
      string name = hashAlgorithm.Name;
      if (name == "MD5")
        return 32771;
      if (name == "SHA1")
        return 32772;
      if (name == "SHA256")
        return 32780;
      if (name == "SHA384")
        return 32781;
      if (name == "SHA512")
        return 32782;
      throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", (object) hashAlgorithm.Name));
    }

    public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (padding == (RSAEncryptionPadding) null)
        throw new ArgumentNullException("padding");
      if (padding == RSAEncryptionPadding.Pkcs1)
        return this.Encrypt(data, false);
      if (padding == RSAEncryptionPadding.OaepSHA1)
        return this.Encrypt(data, true);
      throw RSACryptoServiceProvider.PaddingModeNotSupported();
    }

    public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (padding == (RSAEncryptionPadding) null)
        throw new ArgumentNullException("padding");
      if (padding == RSAEncryptionPadding.Pkcs1)
        return this.Decrypt(data, false);
      if (padding == RSAEncryptionPadding.OaepSHA1)
        return this.Decrypt(data, true);
      throw RSACryptoServiceProvider.PaddingModeNotSupported();
    }

    public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (hash == null)
        throw new ArgumentNullException("hash");
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException("padding");
      if (padding != RSASignaturePadding.Pkcs1)
        throw RSACryptoServiceProvider.PaddingModeNotSupported();
      return this.SignHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm));
    }

    public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (hash == null)
        throw new ArgumentNullException("hash");
      if (signature == null)
        throw new ArgumentNullException("signature");
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException("padding");
      if (padding != RSASignaturePadding.Pkcs1)
        throw RSACryptoServiceProvider.PaddingModeNotSupported();
      return this.VerifyHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm), signature);
    }

    private static Exception PaddingModeNotSupported()
    {
      return (Exception) new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
    }
  }
}
