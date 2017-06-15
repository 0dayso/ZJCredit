// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACSHA512
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.SHA512" /> 哈希函数计算基于哈希值的消息验证代码 (HMAC)。</summary>
  [ComVisible(true)]
  public class HMACSHA512 : HMAC
  {
    private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

    private int BlockSize
    {
      get
      {
        return !this.m_useLegacyBlockSize ? 128 : 64;
      }
    }

    /// <summary>针对与 .NET Framework 2.0 Service Pack 1 实现不一致的 <see cref="T:System.Security.Cryptography.HMACSHA512" /> 算法的 .NET Framework 2.0实现，提供一种解决方法。</summary>
    /// <returns>如果支持 .NET Framework 2.0 Service Pack 1 应用程序与 .NET Framework 2.0 应用程序交互，则为 true；否则为 false。</returns>
    public bool ProduceLegacyHmacValues
    {
      get
      {
        return this.m_useLegacyBlockSize;
      }
      set
      {
        this.m_useLegacyBlockSize = value;
        this.BlockSizeValue = this.BlockSize;
        this.InitializeKey(this.KeyValue);
      }
    }

    /// <summary>使用随机生成的密钥初始化 <see cref="T:System.Security.Cryptography.HMACSHA512" /> 类的新实例。</summary>
    public HMACSHA512()
      : this(Utils.GenerateRandom(128))
    {
    }

    /// <summary>使用指定的密钥数据初始化 <see cref="T:System.Security.Cryptography.HMACSHA512" /> 类的新实例。</summary>
    /// <param name="key">
    /// <see cref="T:System.Security.Cryptography.HMACSHA512" /> 加密的机密密钥。该密钥可以是任意长度。但是建议的大小为 128 个字节。如果键的长度超过 128 个字节，将对其进行哈希运算（使用 SHA-512）以派生出一个 128 字节的密钥。如果少于 128 个字节，就填充到 128 个字节。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public HMACSHA512(byte[] key)
    {
      this.m_hashName = "SHA512";
      Func<HashAlgorithm> func1 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA512Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback1;
      this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback1, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider")));
      Func<HashAlgorithm> func2 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA512Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback2;
      this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback2, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider")));
      this.HashSizeValue = 512;
      this.BlockSizeValue = this.BlockSize;
      this.InitializeKey(key);
    }
  }
}
