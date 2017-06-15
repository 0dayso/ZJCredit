// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.PKCS1MaskGenerationMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>根据 PKCS #1 计算用于密钥交换算法的掩码。</summary>
  [ComVisible(true)]
  public class PKCS1MaskGenerationMethod : MaskGenerationMethod
  {
    private string HashNameValue;

    /// <summary>获取或设置用于生成掩码的哈希算法类型的名称。</summary>
    /// <returns>实现用于计算掩码的哈希算法的类型名称。</returns>
    public string HashName
    {
      get
      {
        return this.HashNameValue;
      }
      set
      {
        this.HashNameValue = value;
        if (this.HashNameValue != null)
          return;
        this.HashNameValue = "SHA1";
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.PKCS1MaskGenerationMethod" /> 类的新实例。</summary>
    public PKCS1MaskGenerationMethod()
    {
      this.HashNameValue = "SHA1";
    }

    /// <summary>用指定长度的指定随机种子生成并返回掩码。</summary>
    /// <returns>长度等于 <paramref name="cbReturn" /> 参数的随机生成的掩码。</returns>
    /// <param name="rgbSeed">用于计算掩码的随机种子。</param>
    /// <param name="cbReturn">生成的掩码的长度（以字节为单位）。</param>
    public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
    {
      HashAlgorithm hashAlgorithm = (HashAlgorithm) CryptoConfig.CreateFromName(this.HashNameValue);
      byte[] counter = new byte[4];
      byte[] numArray = new byte[cbReturn];
      uint num = 0;
      int dstOffset = 0;
      while (dstOffset < numArray.Length)
      {
        Utils.ConvertIntToByteArray(num++, ref counter);
        hashAlgorithm.TransformBlock(rgbSeed, 0, rgbSeed.Length, rgbSeed, 0);
        hashAlgorithm.TransformFinalBlock(counter, 0, 4);
        byte[] hash = hashAlgorithm.Hash;
        hashAlgorithm.Initialize();
        if (numArray.Length - dstOffset > hash.Length)
          Buffer.BlockCopy((Array) hash, 0, (Array) numArray, dstOffset, hash.Length);
        else
          Buffer.BlockCopy((Array) hash, 0, (Array) numArray, dstOffset, numArray.Length - dstOffset);
        dstOffset += hashAlgorithm.Hash.Length;
      }
      return numArray;
    }
  }
}
