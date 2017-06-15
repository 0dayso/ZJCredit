// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Aes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
  /// <summary>表示高级加密标准 (AES) 的所有实现都必须从中继承的抽象基类。</summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  public abstract class Aes : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]{ new KeySizes(128, 128, 0) };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]{ new KeySizes(128, 256, 64) };

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.Aes" /> 类的新实例。</summary>
    protected Aes()
    {
      this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
      this.LegalKeySizesValue = Aes.s_legalKeySizes;
      this.BlockSizeValue = 128;
      this.FeedbackSizeValue = 8;
      this.KeySizeValue = 256;
      this.ModeValue = CipherMode.CBC;
    }

    /// <summary>创建用于执行对称算法的加密对象。</summary>
    /// <returns>用于执行对称算法的加密对象。</returns>
    public static Aes Create()
    {
      return Aes.Create("AES");
    }

    /// <summary>创建一个加密对象，该对象指定用于执行对称算法的 AES 的实现。</summary>
    /// <returns>用于执行对称算法的加密对象。</returns>
    /// <param name="algorithmName">要使用的 AES 的特定实现的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="algorithmName" /> 参数为 null。</exception>
    public static Aes Create(string algorithmName)
    {
      if (algorithmName == null)
        throw new ArgumentNullException("algorithmName");
      return CryptoConfig.CreateFromName(algorithmName) as Aes;
    }
  }
}
