// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACSHA1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希函数计算基于哈希值的消息验证代码 (HMAC)。</summary>
  [ComVisible(true)]
  public class HMACSHA1 : HMAC
  {
    /// <summary>使用随机生成的密钥初始化 <see cref="T:System.Security.Cryptography.HMACSHA1" /> 类的新实例。</summary>
    public HMACSHA1()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>使用指定的密钥数据初始化 <see cref="T:System.Security.Cryptography.HMACSHA1" /> 类的新实例。</summary>
    /// <param name="key">
    /// <see cref="T:System.Security.Cryptography.HMACSHA1" /> 加密的机密密钥。密钥的长度不限，但如果该密钥是 64 个字节，就会经过散列处理（使用 SHA-1）以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 参数为 null。</exception>
    public HMACSHA1(byte[] key)
      : this(key, false)
    {
    }

    /// <summary>使用指定的密钥数据和一个指定是否使用 SHA1 算法托管版本的值，来初始化 <see cref="T:System.Security.Cryptography.HMACSHA1" /> 类的新实例。</summary>
    /// <param name="key">
    /// <see cref="T:System.Security.Cryptography.HMACSHA1" /> 加密的机密密钥。密钥的长度不限，但如果该密钥超过 64 个字节，就会经过散列处理（使用 SHA-1）以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
    /// <param name="useManagedSha1">如果使用 SHA1 算法的托管实现（<see cref="T:System.Security.Cryptography.SHA1Managed" /> 类），则为 true；如果使用非托管实现（<see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> 类），则为 false。</param>
    public HMACSHA1(byte[] key, bool useManagedSha1)
    {
      this.m_hashName = "SHA1";
      if (useManagedSha1)
      {
        this.m_hash1 = (HashAlgorithm) new SHA1Managed();
        this.m_hash2 = (HashAlgorithm) new SHA1Managed();
      }
      else
      {
        this.m_hash1 = (HashAlgorithm) new SHA1CryptoServiceProvider();
        this.m_hash2 = (HashAlgorithm) new SHA1CryptoServiceProvider();
      }
      this.HashSizeValue = 160;
      this.InitializeKey(key);
    }
  }
}
