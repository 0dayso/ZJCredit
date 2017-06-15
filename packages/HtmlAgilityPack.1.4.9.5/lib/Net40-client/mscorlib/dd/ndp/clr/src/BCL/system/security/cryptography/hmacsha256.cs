// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACSHA256
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.SHA256" /> 哈希函数计算基于哈希值的消息验证代码 (HMAC)。</summary>
  [ComVisible(true)]
  public class HMACSHA256 : HMAC
  {
    /// <summary>使用随机生成的密钥初始化 <see cref="T:System.Security.Cryptography.HMACSHA256" /> 类的新实例。</summary>
    public HMACSHA256()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>使用指定的密钥数据初始化 <see cref="T:System.Security.Cryptography.HMACSHA256" /> 类的新实例。</summary>
    /// <param name="key">
    /// <see cref="T:System.Security.Cryptography.HMACSHA256" /> 加密的机密密钥。该密钥可以是任意长度。但是建议的大小为 64 个字节。如果键的长度超过 64 个字节，将对其进行哈希运算（使用 SHA-256）以派生出一个 64 字节的密钥。如果少于 64 个字节，就填充到 64 个字节。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 参数为 null。</exception>
    public HMACSHA256(byte[] key)
    {
      this.m_hashName = "SHA256";
      Func<HashAlgorithm> func1 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA256Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback1;
      this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback1, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider")));
      Func<HashAlgorithm> func2 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA256Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback2;
      this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback2, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider")));
      this.HashSizeValue = 256;
      this.InitializeKey(key);
    }
  }
}
