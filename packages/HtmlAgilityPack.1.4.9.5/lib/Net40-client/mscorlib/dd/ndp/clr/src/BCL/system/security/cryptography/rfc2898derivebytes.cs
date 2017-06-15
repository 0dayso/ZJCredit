// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Rfc2898DeriveBytes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
  /// <summary>通过使用基于 <see cref="T:System.Security.Cryptography.HMACSHA1" /> 的伪随机数生成器，实现基于密码的密钥派生功能 (PBKDF2)。</summary>
  [ComVisible(true)]
  public class Rfc2898DeriveBytes : DeriveBytes
  {
    private CspParameters m_cspParams = new CspParameters();
    private byte[] m_buffer;
    private byte[] m_salt;
    private HMACSHA1 m_hmacsha1;
    private byte[] m_password;
    private uint m_iterations;
    private uint m_block;
    private int m_startIndex;
    private int m_endIndex;
    private const int BlockSize = 20;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;

    /// <summary>获取或设置操作的迭代数。</summary>
    /// <returns>操作的迭代数。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">迭代次数小于 1。</exception>
    public int IterationCount
    {
      get
      {
        return (int) this.m_iterations;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
        this.m_iterations = (uint) value;
        this.Initialize();
      }
    }

    /// <summary>获取或设置操作的密钥 salt 值。</summary>
    /// <returns>操作的密钥 salt 值。</returns>
    /// <exception cref="T:System.ArgumentException">指定的 salt 大小小于 8 字节。</exception>
    /// <exception cref="T:System.ArgumentNullException">salt 为 null。</exception>
    public byte[] Salt
    {
      get
      {
        return (byte[]) this.m_salt.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (value.Length < 8)
          throw new ArgumentException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_FewBytesSalt"));
        this.m_salt = (byte[]) value.Clone();
        this.Initialize();
      }
    }

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
              SafeProvHandle local_2 = Utils.AcquireProvHandle(this.m_cspParams);
              Thread.MemoryBarrier();
              this._safeProvHandle = local_2;
            }
          }
        }
        return this._safeProvHandle;
      }
    }

    /// <summary>通过使用密码和 salt 大小派生密钥，初始化 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">用于派生密钥的密码。</param>
    /// <param name="saltSize">你希望类生成的随机 salt 的大小。</param>
    /// <exception cref="T:System.ArgumentException">指定的 salt 大小小于 8 字节。</exception>
    /// <exception cref="T:System.ArgumentNullException">密码或 salt 为 null。</exception>
    public Rfc2898DeriveBytes(string password, int saltSize)
      : this(password, saltSize, 1000)
    {
    }

    /// <summary>通过使用密码、salt 值和迭代次数派生密钥，初始化 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">用于派生密钥的密码。</param>
    /// <param name="saltSize">你希望类生成的随机 salt 的大小。</param>
    /// <param name="iterations">操作的迭代数。</param>
    /// <exception cref="T:System.ArgumentException">指定的 salt 大小小于 8 字节或迭代次数小于 1。</exception>
    /// <exception cref="T:System.ArgumentNullException">密码或 salt 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="iterations " />超出了范围。此参数需要非负数。</exception>
    [SecuritySafeCritical]
    public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
    {
      if (saltSize < 0)
        throw new ArgumentOutOfRangeException("saltSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] data = new byte[saltSize];
      Utils.StaticRandomNumberGenerator.GetBytes(data);
      this.Salt = data;
      this.IterationCount = iterations;
      this.m_password = new UTF8Encoding(false).GetBytes(password);
      this.m_hmacsha1 = new HMACSHA1(this.m_password);
      this.Initialize();
    }

    /// <summary>通过使用密码和 salt 值派生密钥，初始化 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">用于派生密钥的密码。</param>
    /// <param name="salt">用于派生密钥的密钥 salt。</param>
    /// <exception cref="T:System.ArgumentException">指定的 salt 大小小于 8 字节或迭代次数小于 1。</exception>
    /// <exception cref="T:System.ArgumentNullException">密码或 salt 为 null。</exception>
    public Rfc2898DeriveBytes(string password, byte[] salt)
      : this(password, salt, 1000)
    {
    }

    /// <summary>通过使用密码、salt 值和迭代次数派生密钥，初始化 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">用于派生密钥的密码。</param>
    /// <param name="salt">用于派生密钥的密钥 salt。</param>
    /// <param name="iterations">操作的迭代数。</param>
    /// <exception cref="T:System.ArgumentException">指定的 salt 大小小于 8 字节或迭代次数小于 1。</exception>
    /// <exception cref="T:System.ArgumentNullException">密码或 salt 为 null。</exception>
    public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
      : this(new UTF8Encoding(false).GetBytes(password), salt, iterations)
    {
    }

    /// <summary>通过使用密码、salt 值和迭代次数派生密钥，初始化 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 类的新实例。</summary>
    /// <param name="password">用于派生密钥的密码。</param>
    /// <param name="salt">用于派生密钥的密钥 salt。</param>
    /// <param name="iterations">操作的迭代数。</param>
    /// <exception cref="T:System.ArgumentException">指定的 salt 大小小于 8 字节或迭代次数小于 1。</exception>
    /// <exception cref="T:System.ArgumentNullException">密码或 salt 为 null。</exception>
    [SecuritySafeCritical]
    public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
    {
      this.Salt = salt;
      this.IterationCount = iterations;
      this.m_password = password;
      this.m_hmacsha1 = new HMACSHA1(password);
      this.Initialize();
    }

    /// <summary>返回此对象的伪随机密钥。</summary>
    /// <returns>由伪随机密钥字节组成的字节数组。</returns>
    /// <param name="cb">要生成的伪随机密钥字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="cb " />超出了范围。此参数需要非负数。</exception>
    public override byte[] GetBytes(int cb)
    {
      if (cb <= 0)
        throw new ArgumentOutOfRangeException("cb", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      byte[] numArray1 = new byte[cb];
      int dstOffsetBytes = 0;
      int byteCount = this.m_endIndex - this.m_startIndex;
      if (byteCount > 0)
      {
        if (cb >= byteCount)
        {
          Buffer.InternalBlockCopy((Array) this.m_buffer, this.m_startIndex, (Array) numArray1, 0, byteCount);
          this.m_startIndex = this.m_endIndex = 0;
          dstOffsetBytes += byteCount;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) this.m_buffer, this.m_startIndex, (Array) numArray1, 0, cb);
          this.m_startIndex = this.m_startIndex + cb;
          return numArray1;
        }
      }
      while (dstOffsetBytes < cb)
      {
        byte[] numArray2 = this.Func();
        int num1 = cb - dstOffsetBytes;
        if (num1 > 20)
        {
          Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffsetBytes, 20);
          dstOffsetBytes += 20;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffsetBytes, num1);
          int num2 = dstOffsetBytes + num1;
          Buffer.InternalBlockCopy((Array) numArray2, num1, (Array) this.m_buffer, this.m_startIndex, 20 - num1);
          this.m_endIndex = this.m_endIndex + (20 - num1);
          return numArray1;
        }
      }
      return numArray1;
    }

    /// <summary>重置操作的状态。</summary>
    public override void Reset()
    {
      this.Initialize();
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (this.m_hmacsha1 != null)
        this.m_hmacsha1.Dispose();
      if (this.m_buffer != null)
        Array.Clear((Array) this.m_buffer, 0, this.m_buffer.Length);
      if (this.m_salt == null)
        return;
      Array.Clear((Array) this.m_salt, 0, this.m_salt.Length);
    }

    private void Initialize()
    {
      if (this.m_buffer != null)
        Array.Clear((Array) this.m_buffer, 0, this.m_buffer.Length);
      this.m_buffer = new byte[20];
      this.m_block = 1U;
      this.m_startIndex = this.m_endIndex = 0;
    }

    private byte[] Func()
    {
      byte[] inputBuffer1 = Utils.Int(this.m_block);
      this.m_hmacsha1.TransformBlock(this.m_salt, 0, this.m_salt.Length, (byte[]) null, 0);
      this.m_hmacsha1.TransformBlock(inputBuffer1, 0, inputBuffer1.Length, (byte[]) null, 0);
      this.m_hmacsha1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      byte[] inputBuffer2 = this.m_hmacsha1.HashValue;
      this.m_hmacsha1.Initialize();
      byte[] numArray = inputBuffer2;
      for (int index1 = 2; (long) index1 <= (long) this.m_iterations; ++index1)
      {
        this.m_hmacsha1.TransformBlock(inputBuffer2, 0, inputBuffer2.Length, (byte[]) null, 0);
        this.m_hmacsha1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
        inputBuffer2 = this.m_hmacsha1.HashValue;
        for (int index2 = 0; index2 < 20; ++index2)
          numArray[index2] ^= inputBuffer2[index2];
        this.m_hmacsha1.Initialize();
      }
      this.m_block = this.m_block + 1U;
      return numArray;
    }

    /// <summary>从 <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> 对象导出加密密钥。</summary>
    /// <returns>导出的密钥。</returns>
    /// <param name="algname">为其导出密钥的算法名称。</param>
    /// <param name="alghashname">用于导出密钥的哈希算法名称。</param>
    /// <param name="keySize">要导出的密钥的大小（以位为单位）。</param>
    /// <param name="rgbIV">用于导出密钥的初始化向量 (IV)。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="keySize" /> 参数不正确。- 或 - 无法获取加密服务提供程序 (CSP)。- 或 - <paramref name="algname" /> 参数不是有效的算法名称。- 或 - <paramref name="alghashname" /> 参数不是有效的哈希算法名称。</exception>
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
      byte[] password = this.m_password;
      int length1 = this.m_password.Length;
      int dwFlags = keySize << 16;
      byte[] IV = rgbIV;
      int length2 = IV.Length;
      ObjectHandleOnStack objectHandleOnStack = JitHelpers.GetObjectHandleOnStack<byte[]>(ref o);
      Rfc2898DeriveBytes.DeriveKey(provHandle, algid, algidHash, password, length1, dwFlags, IV, length2, objectHandleOnStack);
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DeriveKey(SafeProvHandle hProv, int algid, int algidHash, byte[] password, int cbPassword, int dwFlags, byte[] IV, int cbIV, ObjectHandleOnStack retKey);
  }
}
