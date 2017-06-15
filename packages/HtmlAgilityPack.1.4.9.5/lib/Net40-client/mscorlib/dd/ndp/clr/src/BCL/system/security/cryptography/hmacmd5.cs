// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACMD5
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.MD5" /> 哈希函数计算基于哈希值的消息验证代码 (HMAC)。</summary>
  [ComVisible(true)]
  public class HMACMD5 : HMAC
  {
    /// <summary>使用随机生成的密钥初始化 <see cref="T:System.Security.Cryptography.HMACMD5" /> 类的新实例。</summary>
    public HMACMD5()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>使用指定的密钥初始化 <see cref="T:System.Security.Cryptography.HMACMD5" /> 类的新实例。</summary>
    /// <param name="key">
    /// <see cref="T:System.Security.Cryptography.HMACMD5" /> 加密的机密密钥。密钥的长度不限，但如果超过 64 个字节，就会对其进行哈希计算（使用 SHA-1），以派生一个 64 个字节的密钥。因此，建议的密钥大小为 64 个字节。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 参数为 null。</exception>
    public HMACMD5(byte[] key)
    {
      this.m_hashName = "MD5";
      this.m_hash1 = (HashAlgorithm) new MD5CryptoServiceProvider();
      this.m_hash2 = (HashAlgorithm) new MD5CryptoServiceProvider();
      this.HashSizeValue = 128;
      this.InitializeKey(key);
    }
  }
}
