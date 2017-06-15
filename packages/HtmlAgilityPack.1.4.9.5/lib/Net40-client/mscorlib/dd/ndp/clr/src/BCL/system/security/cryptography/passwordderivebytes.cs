// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.PasswordDeriveBytes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
  /// <summary>使用 PBKDF1 算法的扩展从密码派生密钥。</summary>
  [ComVisible(true)]
  public class PasswordDeriveBytes : DeriveBytes
  {
    private int _extraCount;
    private int _prefix;
    private int _iterations;
    private byte[] _baseValue;
    private byte[] _extra;
    private byte[] _salt;
    private string _hashName;
    private byte[] _password;
    private HashAlgorithm _hash;
    private CspParameters _cspParams;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;

    private SafeProvHandle ProvHandle
    {
      [SecurityCritical] get
      {
        if (this._safeProvHandle == null)
        {
          lock (this)
          {
            if (this._safeProvHandle == null)
            {
              SafeProvHandle local_2 = Utils.AcquireProvHandle(this._cspParams);
              Thread.MemoryBarrier();
              this._safeProvHandle = local_2;
            }
          }
        }
        return this._safeProvHandle;
      }
    }

    /// <summary>获取或设置操作的哈希算法的名称。</summary>
    /// <returns>操作的哈希算法的名称。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">哈希值的名称是固定的，尝试更改该值。</exception>
    public string HashName
    {
      get
      {
        return this._hashName;
      }
      set
      {
        if (this._baseValue != null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", (object) "HashName"));
        this._hashName = value;
        this._hash = (HashAlgorithm) CryptoConfig.CreateFromName(this._hashName);
      }
    }

    /// <summary>获取或设置操作的迭代数。</summary>
    /// <returns>操作的迭代数。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">迭代数是固定的，尝试更改该值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">无法设置此属性，因为其值超出了范围。此属性需要非负数。</exception>
    public int IterationCount
    {
      get
      {
        return this._iterations;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
        if (this._baseValue != null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", (object) "IterationCount"));
        this._iterations = value;
      }
    }

    /// <summary>获取或设置操作的密钥 salt 值。</summary>
    /// <returns>操作的密钥 salt 值。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">密钥 salt 值是固定的，尝试更改该值。</exception>
    public byte[] Salt
    {
      get
      {
        if (this._salt == null)
          return (byte[]) null;
        return (byte[]) this._salt.Clone();
      }
      set
      {
        if (this._baseValue != null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", (object) "Salt"));
        if (value == null)
          this._salt = (byte[]) null;
        else
          this._salt = (byte[]) value.Clone();
      }
    }

    /// <summary>使用用来导出密钥的密码和密钥 salt 初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="strPassword">从其导出密钥的密码。</param>
    /// <param name="rgbSalt">用以导出密钥的密钥 salt。</param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt)
      : this(strPassword, rgbSalt, new CspParameters())
    {
    }

    /// <summary>通过指定用来派生密钥的密码和密钥 salt 初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">为其派生密钥的密码。</param>
    /// <param name="salt">用以导出密钥的密钥 salt。</param>
    public PasswordDeriveBytes(byte[] password, byte[] salt)
      : this(password, salt, new CspParameters())
    {
    }

    /// <summary>使用用来导出密钥的密码、密钥 salt、哈希名和迭代数初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="strPassword">从其导出密钥的密码。</param>
    /// <param name="rgbSalt">用以导出密钥的密钥 salt。</param>
    /// <param name="strHashName">操作的哈希算法的名称。</param>
    /// <param name="iterations">操作的迭代数。</param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations)
      : this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
    {
    }

    /// <summary>通过指定用来派生密钥的密码、密钥 salt、哈希名和迭代数初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">为其派生密钥的密码。</param>
    /// <param name="salt">用以导出密钥的密钥 salt。</param>
    /// <param name="hashName">用于派生密钥的哈希算法。</param>
    /// <param name="iterations">用于派生密钥的迭代数。</param>
    public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations)
      : this(password, salt, hashName, iterations, new CspParameters())
    {
    }

    /// <summary>使用用来导出密钥的密码、密钥 salt 和加密服务提供程序 (CSP) 参数初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="strPassword">从其导出密钥的密码。</param>
    /// <param name="rgbSalt">用以导出密钥的密钥 salt。</param>
    /// <param name="cspParams">操作的 CSP 参数。</param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams)
      : this(strPassword, rgbSalt, "SHA1", 100, cspParams)
    {
    }

    /// <summary>通过指定用来派生密钥的密码、密钥 salt 和加密服务提供程序 (CSP) 初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">为其派生密钥的密码。</param>
    /// <param name="salt">用以导出密钥的密钥 salt。</param>
    /// <param name="cspParams">用于操作的加密服务提供程序 (CSP) 参数。</param>
    public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams)
      : this(password, salt, "SHA1", 100, cspParams)
    {
    }

    /// <summary>使用用来导出密钥的密码、密钥 salt、哈希名、迭代数和加密服务提供程序 (CSP) 参数初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="strPassword">从其导出密钥的密码。</param>
    /// <param name="rgbSalt">用以导出密钥的密钥 salt。</param>
    /// <param name="strHashName">操作的哈希算法的名称。</param>
    /// <param name="iterations">操作的迭代数。</param>
    /// <param name="cspParams">操作的 CSP 参数。</param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams)
      : this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
    {
    }

    /// <summary>通过指定用来派生密钥的密码、密钥 salt、哈希名、迭代数和加密服务提供程序 (CSP) 初始化 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">为其派生密钥的密码。</param>
    /// <param name="salt">用以导出密钥的密钥 salt。</param>
    /// <param name="hashName">用于派生密钥的哈希算法。</param>
    /// <param name="iterations">用于派生密钥的迭代数。</param>
    /// <param name="cspParams">用于操作的加密服务提供程序 (CSP) 参数。</param>
    [SecuritySafeCritical]
    public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
    {
      this.IterationCount = iterations;
      this.Salt = salt;
      this.HashName = hashName;
      this._password = password;
      this._cspParams = cspParams;
    }

    /// <summary>返回伪随机密钥字节。</summary>
    /// <returns>由伪随机密钥字节组成的字节数组。</returns>
    /// <param name="cb">要生成的伪随机密钥字节数。</param>
    [SecuritySafeCritical]
    [Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
    public override byte[] GetBytes(int cb)
    {
      int num = 0;
      byte[] numArray = new byte[cb];
      if (this._baseValue == null)
        this.ComputeBaseValue();
      else if (this._extra != null)
      {
        num = this._extra.Length - this._extraCount;
        if (num >= cb)
        {
          Buffer.InternalBlockCopy((Array) this._extra, this._extraCount, (Array) numArray, 0, cb);
          if (num > cb)
            this._extraCount = this._extraCount + cb;
          else
            this._extra = (byte[]) null;
          return numArray;
        }
        Buffer.InternalBlockCopy((Array) this._extra, num, (Array) numArray, 0, num);
        this._extra = (byte[]) null;
      }
      byte[] bytes = this.ComputeBytes(cb - num);
      Buffer.InternalBlockCopy((Array) bytes, 0, (Array) numArray, num, cb - num);
      if (bytes.Length + num > cb)
      {
        this._extra = bytes;
        this._extraCount = cb - num;
      }
      return numArray;
    }

    /// <summary>重置操作的状态。</summary>
    public override void Reset()
    {
      this._prefix = 0;
      this._extra = (byte[]) null;
      this._baseValue = (byte[]) null;
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (this._hash != null)
        this._hash.Dispose();
      if (this._baseValue != null)
        Array.Clear((Array) this._baseValue, 0, this._baseValue.Length);
      if (this._extra != null)
        Array.Clear((Array) this._extra, 0, this._extra.Length);
      if (this._password != null)
        Array.Clear((Array) this._password, 0, this._password.Length);
      if (this._salt == null)
        return;
      Array.Clear((Array) this._salt, 0, this._salt.Length);
    }

    /// <summary>从 <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> 对象导出加密密钥。</summary>
    /// <returns>导出的密钥。</returns>
    /// <param name="algname">为其导出密钥的算法名称。</param>
    /// <param name="alghashname">用于导出密钥的哈希算法名称。</param>
    /// <param name="keySize">要导出的密钥的大小（以位为单位）。</param>
    /// <param name="rgbIV">用于导出密钥的初始化向量 (IV)。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="keySize" /> 参数不正确。- 或 - 无法获取加密服务提供程序 (CSP)。- 或 - <paramref name="algname" /> 参数不是有效的算法名称。- 或 - <paramref name="alghashname" /> 参数不是有效的哈希算法名称。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
    {
      if (keySize < 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      int algId1 = X509Utils.NameOrOidToAlgId(alghashname, OidGroup.HashAlgorithm);
      if (algId1 == 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
      int algId2 = X509Utils.NameOrOidToAlgId(algname, OidGroup.AllGroups);
      if (algId2 == 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
      if (rgbIV == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidIV"));
      byte[] o = (byte[]) null;
      SafeProvHandle provHandle = this.ProvHandle;
      int algid = algId2;
      int algidHash = algId1;
      byte[] password = this._password;
      int length1 = this._password.Length;
      int dwFlags = keySize << 16;
      byte[] IV = rgbIV;
      int length2 = IV.Length;
      ObjectHandleOnStack objectHandleOnStack = JitHelpers.GetObjectHandleOnStack<byte[]>(ref o);
      PasswordDeriveBytes.DeriveKey(provHandle, algid, algidHash, password, length1, dwFlags, IV, length2, objectHandleOnStack);
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DeriveKey(SafeProvHandle hProv, int algid, int algidHash, byte[] password, int cbPassword, int dwFlags, byte[] IV, int cbIV, ObjectHandleOnStack retKey);

    private byte[] ComputeBaseValue()
    {
      this._hash.Initialize();
      this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
      if (this._salt != null)
        this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
      this._hash.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      this._baseValue = this._hash.Hash;
      this._hash.Initialize();
      for (int index = 1; index < this._iterations - 1; ++index)
      {
        this._hash.ComputeHash(this._baseValue);
        this._baseValue = this._hash.Hash;
      }
      return this._baseValue;
    }

    [SecurityCritical]
    private byte[] ComputeBytes(int cb)
    {
      int dstOffsetBytes1 = 0;
      this._hash.Initialize();
      int byteCount = this._hash.HashSize / 8;
      byte[] numArray = new byte[(cb + byteCount - 1) / byteCount * byteCount];
      using (CryptoStream cs = new CryptoStream(Stream.Null, (ICryptoTransform) this._hash, CryptoStreamMode.Write))
      {
        this.HashPrefix(cs);
        cs.Write(this._baseValue, 0, this._baseValue.Length);
        cs.Close();
      }
      Buffer.InternalBlockCopy((Array) this._hash.Hash, 0, (Array) numArray, dstOffsetBytes1, byteCount);
      int dstOffsetBytes2 = dstOffsetBytes1 + byteCount;
      while (cb > dstOffsetBytes2)
      {
        this._hash.Initialize();
        using (CryptoStream cs = new CryptoStream(Stream.Null, (ICryptoTransform) this._hash, CryptoStreamMode.Write))
        {
          this.HashPrefix(cs);
          cs.Write(this._baseValue, 0, this._baseValue.Length);
          cs.Close();
        }
        Buffer.InternalBlockCopy((Array) this._hash.Hash, 0, (Array) numArray, dstOffsetBytes2, byteCount);
        dstOffsetBytes2 += byteCount;
      }
      return numArray;
    }

    private void HashPrefix(CryptoStream cs)
    {
      int index = 0;
      byte[] buffer = new byte[3]{ (byte) 48, (byte) 48, (byte) 48 };
      if (this._prefix > 999)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_TooManyBytes"));
      if (this._prefix >= 100)
      {
        buffer[0] += (byte) (this._prefix / 100);
        ++index;
      }
      if (this._prefix >= 10)
      {
        buffer[index] += (byte) (this._prefix % 100 / 10);
        ++index;
      }
      if (this._prefix > 0)
      {
        buffer[index] += (byte) (this._prefix % 10);
        int count = index + 1;
        cs.Write(buffer, 0, count);
      }
      this._prefix = this._prefix + 1;
    }
  }
}
