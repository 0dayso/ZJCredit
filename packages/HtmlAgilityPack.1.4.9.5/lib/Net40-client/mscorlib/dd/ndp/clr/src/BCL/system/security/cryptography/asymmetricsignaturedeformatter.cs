// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricSignatureDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有不对称签名反格式化程序实现均从中派生的抽象基类。</summary>
  [ComVisible(true)]
  public abstract class AsymmetricSignatureDeformatter
  {
    /// <summary>当在派生类中重写时，设置用于验证签名的公钥。</summary>
    /// <param name="key">包含公钥的 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 的实现实例。</param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>当在派生类中重写时，设置用于验证签名的哈希算法。</summary>
    /// <param name="strName">用于验证签名的哈希算法的名称。</param>
    public abstract void SetHashAlgorithm(string strName);

    /// <summary>通过指定的哈希值验证签名。</summary>
    /// <returns>如果签名对哈希值有效，则为 true；否则为 false。</returns>
    /// <param name="hash">用于验证签名的哈希算法。</param>
    /// <param name="rgbSignature">要验证的签名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="hash" /> 参数为 null。</exception>
    public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
    {
      if (hash == null)
        throw new ArgumentNullException("hash");
      this.SetHashAlgorithm(hash.ToString());
      return this.VerifySignature(hash.Hash, rgbSignature);
    }

    /// <summary>当在派生类中重写时，验证指定数据的签名。</summary>
    /// <returns>如果 <paramref name="rgbSignature" /> 与使用指定的哈希算法和密钥在 <paramref name="rgbHash" /> 上计算出的签名匹配，则为 true；否则为 false。</returns>
    /// <param name="rgbHash">用 <paramref name="rgbSignature" /> 签名的数据。</param>
    /// <param name="rgbSignature">要为 <paramref name="rgbHash" /> 验证的签名。</param>
    public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
  }
}
