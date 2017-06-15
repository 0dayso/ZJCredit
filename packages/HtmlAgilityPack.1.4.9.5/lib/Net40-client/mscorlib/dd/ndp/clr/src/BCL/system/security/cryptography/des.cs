// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DES
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有 <see cref="T:System.Security.Cryptography.DES" /> 实现都必须从中派生的数据加密标准 (DES) 算法的基类。</summary>
  [ComVisible(true)]
  public abstract class DES : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]{ new KeySizes(64, 64, 0) };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]{ new KeySizes(64, 64, 0) };

    /// <summary>获取或设置数据加密标准 (<see cref="T:System.Security.Cryptography.DES" />) 算法的机密密钥。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.DES" /> 算法的机密密钥。</returns>
    /// <exception cref="T:System.ArgumentNullException">试图将密钥设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">尝试设置长度不等于 <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.BlockSizeValue" /> 的密钥。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">尝试设置弱密钥（请参见 <see cref="M:System.Security.Cryptography.DES.IsWeakKey(System.Byte[])" />）或半弱密钥（请参见 <see cref="M:System.Security.Cryptography.DES.IsSemiWeakKey(System.Byte[])" />）。</exception>
    public override byte[] Key
    {
      get
      {
        if (this.KeyValue == null)
        {
          do
          {
            this.GenerateKey();
          }
          while (DES.IsWeakKey(this.KeyValue) || DES.IsSemiWeakKey(this.KeyValue));
        }
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (!this.ValidKeySize(value.Length * 8))
          throw new ArgumentException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        if (DES.IsWeakKey(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "DES");
        if (DES.IsSemiWeakKey(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_SemiWeak"), "DES");
        this.KeyValue = (byte[]) value.Clone();
        this.KeySizeValue = value.Length * 8;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.DES" /> 类的新实例。</summary>
    protected DES()
    {
      this.KeySizeValue = 64;
      this.BlockSizeValue = 64;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = DES.s_legalBlockSizes;
      this.LegalKeySizesValue = DES.s_legalKeySizes;
    }

    /// <summary>创建加密对象的实例以执行数据加密标准（<see cref="T:System.Security.Cryptography.DES" />）算法。</summary>
    /// <returns>加密对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static DES Create()
    {
      return DES.Create("System.Security.Cryptography.DES");
    }

    /// <summary>创建加密对象的实例以执行数据加密标准 (<see cref="T:System.Security.Cryptography.DES" />) 算法的指定实现。</summary>
    /// <returns>加密对象。</returns>
    /// <param name="algName">要使用的 <see cref="T:System.Security.Cryptography.DES" /> 的特定实现的名称。</param>
    public static DES Create(string algName)
    {
      return (DES) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>确定指定的密钥是否为弱密钥。</summary>
    /// <returns>如果该密钥为弱密钥，则为 true；否则，为 false。</returns>
    /// <param name="rgbKey">要进行漏洞测试的机密密钥。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="rgbKey" /> 参数的大小无效。</exception>
    public static bool IsWeakKey(byte[] rgbKey)
    {
      if (!DES.IsLegalKeySize(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      switch (DES.QuadWordFromBigEndian(Utils.FixupKeyParity(rgbKey)))
      {
        case 72340172838076673:
        case 18374403900871474942:
        case 2242545357694045710:
        case 16204198716015505905:
          return true;
        default:
          return false;
      }
    }

    /// <summary>确定指定的密钥是否为半弱密钥。</summary>
    /// <returns>如果该密钥为半弱密钥，则为 true；否则，为 false。</returns>
    /// <param name="rgbKey">要进行半弱漏洞测试的机密密钥。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="rgbKey" /> 参数的大小无效。</exception>
    public static bool IsSemiWeakKey(byte[] rgbKey)
    {
      if (!DES.IsLegalKeySize(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      switch (DES.QuadWordFromBigEndian(Utils.FixupKeyParity(rgbKey)))
      {
        case 143554428589179390:
        case 18303189645120372225:
        case 2296870857142767345:
        case 16149873216566784270:
        case 135110050437988849:
        case 16141428838415593729:
        case 2305315235293957886:
        case 18311634023271562766:
        case 80784550989267214:
        case 2234100979542855169:
        case 16212643094166696446:
        case 18365959522720284401:
          return true;
        default:
          return false;
      }
    }

    private static bool IsLegalKeySize(byte[] rgbKey)
    {
      return rgbKey != null && rgbKey.Length == 8;
    }

    private static ulong QuadWordFromBigEndian(byte[] block)
    {
      return (ulong) ((long) block[0] << 56 | (long) block[1] << 48 | (long) block[2] << 40 | (long) block[3] << 32 | (long) block[4] << 24 | (long) block[5] << 16 | (long) block[6] << 8) | (ulong) block[7];
    }
  }
}
