// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricSignatureFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有不对称签名格式化程序实现均从中派生的基类。</summary>
  [ComVisible(true)]
  public abstract class AsymmetricSignatureFormatter
  {
    /// <summary>当在派生类中重写时，设置用于创建签名的不对称算法。</summary>
    /// <param name="key">用于创建签名的 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 实现的实例。</param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>当在派生类中重写时，设置用于创建签名的哈希算法。</summary>
    /// <param name="strName">要用于创建签名的哈希算法的名称。</param>
    public abstract void SetHashAlgorithm(string strName);

    /// <summary>通过指定的哈希值创建签名。</summary>
    /// <returns>指定哈希值的签名。</returns>
    /// <param name="hash">用于创建签名的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="hash" /> 参数为 null。</exception>
    public virtual byte[] CreateSignature(HashAlgorithm hash)
    {
      if (hash == null)
        throw new ArgumentNullException("hash");
      this.SetHashAlgorithm(hash.ToString());
      return this.CreateSignature(hash.Hash);
    }

    /// <summary>当在派生类中重写时，创建指定数据的签名。</summary>
    /// <returns>
    /// <paramref name="rgbHash" /> 参数的数字签名。</returns>
    /// <param name="rgbHash">要签名的数据。</param>
    public abstract byte[] CreateSignature(byte[] rgbHash);
  }
}
