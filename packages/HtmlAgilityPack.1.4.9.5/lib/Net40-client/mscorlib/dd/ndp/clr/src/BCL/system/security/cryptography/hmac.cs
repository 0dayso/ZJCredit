// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMAC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示基于哈希的消息验证代码 (HMAC) 的所有实现必须从中派生的抽象类。</summary>
  [ComVisible(true)]
  public abstract class HMAC : KeyedHashAlgorithm
  {
    private int blockSizeValue = 64;
    internal string m_hashName;
    internal HashAlgorithm m_hash1;
    internal HashAlgorithm m_hash2;
    private byte[] m_inner;
    private byte[] m_outer;
    private bool m_hashing;

    /// <summary>获取或设置哈希值中使用的块大小。</summary>
    /// <returns>哈希值中使用的块大小。</returns>
    protected int BlockSizeValue
    {
      get
      {
        return this.blockSizeValue;
      }
      set
      {
        this.blockSizeValue = value;
      }
    }

    /// <summary>获取或设置用于哈希算法的密钥。</summary>
    /// <returns>用于哈希算法的密钥。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">试图在哈希计算开始后更改 <see cref="P:System.Security.Cryptography.HMAC.Key" /> 属性。</exception>
    public override byte[] Key
    {
      get
      {
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (this.m_hashing)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
        this.InitializeKey(value);
      }
    }

    /// <summary>获取或设置用于哈希计算的哈希算法的名称。</summary>
    /// <returns>哈希算法的名称。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法更改当前的哈希算法。</exception>
    public string HashName
    {
      get
      {
        return this.m_hashName;
      }
      set
      {
        if (this.m_hashing)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_HashNameSet"));
        this.m_hashName = value;
        this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
        this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
      }
    }

    private void UpdateIOPadBuffers()
    {
      if (this.m_inner == null)
        this.m_inner = new byte[this.BlockSizeValue];
      if (this.m_outer == null)
        this.m_outer = new byte[this.BlockSizeValue];
      for (int index = 0; index < this.BlockSizeValue; ++index)
      {
        this.m_inner[index] = (byte) 54;
        this.m_outer[index] = (byte) 92;
      }
      for (int index = 0; index < this.KeyValue.Length; ++index)
      {
        this.m_inner[index] ^= this.KeyValue[index];
        this.m_outer[index] ^= this.KeyValue[index];
      }
    }

    internal void InitializeKey(byte[] key)
    {
      this.m_inner = (byte[]) null;
      this.m_outer = (byte[]) null;
      if (key.Length > this.BlockSizeValue)
        this.KeyValue = this.m_hash1.ComputeHash(key);
      else
        this.KeyValue = (byte[]) key.Clone();
      this.UpdateIOPadBuffers();
    }

    /// <summary>创建基于哈希的消息验证代码 (HMAC) 默认实现的实例。</summary>
    /// <returns>新的 SHA-1 实例，除非已使用 &lt;cryptoClass&gt; 元素更改默认设置。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static HMAC Create()
    {
      return HMAC.Create("System.Security.Cryptography.HMAC");
    }

    /// <summary>创建基于哈希的消息验证代码 (HMAC) 指定实现的实例。</summary>
    /// <returns>指定的 HMAC 实现的新实例。</returns>
    /// <param name="algorithmName">要使用的 HMAC 实现。下表显示 <paramref name="algorithmName" /> 参数的有效值以及它们映射到的算法。参数值Implements System.Security.Cryptography.HMAC<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.KeyedHashAlgorithm<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />System.Security.Cryptography.HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />System.Security.Cryptography.HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />System.Security.Cryptography.HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />System.Security.Cryptography.HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />System.Security.Cryptography.HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" />System.Security.Cryptography.MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
    public static HMAC Create(string algorithmName)
    {
      return (HMAC) CryptoConfig.CreateFromName(algorithmName);
    }

    /// <summary>初始化默认 <see cref="T:System.Security.Cryptography.HMAC" /> 实现的实例。</summary>
    public override void Initialize()
    {
      this.m_hash1.Initialize();
      this.m_hash2.Initialize();
      this.m_hashing = false;
    }

    /// <summary>当在派生类中重写时，将写入对象的数据路由给默认 <see cref="T:System.Security.Cryptography.HMAC" /> 哈希算法以计算哈希值。</summary>
    /// <param name="rgb">输入数据。</param>
    /// <param name="ib">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="cb">数组中用作数据的字节数。</param>
    protected override void HashCore(byte[] rgb, int ib, int cb)
    {
      if (!this.m_hashing)
      {
        this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
        this.m_hashing = true;
      }
      this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
    }

    /// <summary>当在派生类中重写时，在加密流对象处理完最后的数据后完成哈希计算。</summary>
    /// <returns>字节数组中计算所得的哈希代码。</returns>
    protected override byte[] HashFinal()
    {
      if (!this.m_hashing)
      {
        this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
        this.m_hashing = true;
      }
      this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      byte[] numArray = this.m_hash1.HashValue;
      this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
      this.m_hash2.TransformBlock(numArray, 0, numArray.Length, numArray, 0);
      this.m_hashing = false;
      this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      return this.m_hash2.HashValue;
    }

    /// <summary>密钥更改合法时释放由 <see cref="T:System.Security.Cryptography.HMAC" /> 类使用的非托管资源，并可选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_hash1 != null)
          this.m_hash1.Dispose();
        if (this.m_hash2 != null)
          this.m_hash2.Dispose();
        if (this.m_inner != null)
          Array.Clear((Array) this.m_inner, 0, this.m_inner.Length);
        if (this.m_outer != null)
          Array.Clear((Array) this.m_outer, 0, this.m_outer.Length);
      }
      base.Dispose(disposing);
    }

    internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
    {
      if (!CryptoConfig.AllowOnlyFipsAlgorithms)
        return createStandardHashAlgorithmCallback();
      try
      {
        return createFipsHashAlgorithmCallback();
      }
      catch (PlatformNotSupportedException ex)
      {
        throw new InvalidOperationException(ex.Message, (Exception) ex);
      }
    }
  }
}
