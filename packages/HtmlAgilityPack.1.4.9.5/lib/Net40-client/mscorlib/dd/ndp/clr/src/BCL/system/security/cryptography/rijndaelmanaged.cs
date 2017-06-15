// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RijndaelManaged
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>访问 <see cref="T:System.Security.Cryptography.Rijndael" /> 算法的托管版本。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class RijndaelManaged : Rijndael
  {
    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RijndaelManaged" /> 类的新实例。</summary>
    /// <exception cref="T:System.InvalidOperationException">此类不符合 FIPS 算法。</exception>
    public RijndaelManaged()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
    }

    /// <summary>使用指定的 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> 和初始化向量（<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />）创建对称 <see cref="T:System.Security.Cryptography.Rijndael" /> 加密器对象。</summary>
    /// <returns>对称 <see cref="T:System.Security.Cryptography.Rijndael" /> 加密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的机密密钥。密钥大小必须为 128、192或 256 位。</param>
    /// <param name="rgbIV">将用于对称算法的 IV。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbKey" /> 参数为 null。- 或 -<paramref name="rgbIV" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> 属性的值不是 <see cref="F:System.Security.Cryptography.CipherMode.ECB" />、<see cref="F:System.Security.Cryptography.CipherMode.CBC" /> 或 <see cref="F:System.Security.Cryptography.CipherMode.CFB" />。</exception>
    public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Encrypt);
    }

    /// <summary>使用指定的 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> 和初始化向量（<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />）创建对称 <see cref="T:System.Security.Cryptography.Rijndael" /> 解密器对象。</summary>
    /// <returns>对称 <see cref="T:System.Security.Cryptography.Rijndael" /> 解密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的机密密钥。密钥大小必须为 128、192或 256 位。</param>
    /// <param name="rgbIV">将用于对称算法的 IV。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbKey" /> 参数为 null。- 或 -<paramref name="rgbIV" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> 属性的值不是 <see cref="F:System.Security.Cryptography.CipherMode.ECB" />、<see cref="F:System.Security.Cryptography.CipherMode.CBC" /> 或 <see cref="F:System.Security.Cryptography.CipherMode.CFB" />。</exception>
    public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Decrypt);
    }

    /// <summary>生成用于该算法的随机 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />。</summary>
    public override void GenerateKey()
    {
      this.KeyValue = Utils.GenerateRandom(this.KeySizeValue / 8);
    }

    /// <summary>生成用于该算法的随机初始化向量（<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />）。</summary>
    public override void GenerateIV()
    {
      this.IVValue = Utils.GenerateRandom(this.BlockSizeValue / 8);
    }

    private ICryptoTransform NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, RijndaelManagedTransformMode encryptMode)
    {
      if (rgbKey == null)
        rgbKey = Utils.GenerateRandom(this.KeySizeValue / 8);
      if (rgbIV == null)
        rgbIV = Utils.GenerateRandom(this.BlockSizeValue / 8);
      return (ICryptoTransform) new RijndaelManagedTransform(rgbKey, mode, rgbIV, this.BlockSizeValue, feedbackSize, this.PaddingValue, encryptMode);
    }
  }
}
