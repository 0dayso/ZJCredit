// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.TripleDES
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示三重数据加密标准算法的基类，<see cref="T:System.Security.Cryptography.TripleDES" /> 的所有实现都必须从此基类派生。</summary>
  [ComVisible(true)]
  public abstract class TripleDES : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]{ new KeySizes(64, 64, 0) };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]{ new KeySizes(128, 192, 64) };

    /// <summary>获取或设置 <see cref="T:System.Security.Cryptography.TripleDES" /> 算法的机密密钥。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.TripleDES" /> 算法的机密密钥。</returns>
    /// <exception cref="T:System.ArgumentNullException">试图将密钥设置为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">尝试设置长度无效的密钥。- 或 -尝试设置弱密钥（请参见 <see cref="M:System.Security.Cryptography.TripleDES.IsWeakKey(System.Byte[])" />）。</exception>
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
          while (TripleDES.IsWeakKey(this.KeyValue));
        }
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (!this.ValidKeySize(value.Length * 8))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        if (TripleDES.IsWeakKey(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
        this.KeyValue = (byte[]) value.Clone();
        this.KeySizeValue = value.Length * 8;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.TripleDES" /> 类的新实例。</summary>
    protected TripleDES()
    {
      this.KeySizeValue = 192;
      this.BlockSizeValue = 64;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = TripleDES.s_legalBlockSizes;
      this.LegalKeySizesValue = TripleDES.s_legalKeySizes;
    }

    /// <summary>创建加密对象的实例以执行 <see cref="T:System.Security.Cryptography.TripleDES" /> 算法。</summary>
    /// <returns>加密对象的实例。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static TripleDES Create()
    {
      return TripleDES.Create("System.Security.Cryptography.TripleDES");
    }

    /// <summary>创建加密对象的实例以执行 <see cref="T:System.Security.Cryptography.TripleDES" /> 算法的指定实现。</summary>
    /// <returns>加密对象的实例。</returns>
    /// <param name="str">要使用的 <see cref="T:System.Security.Cryptography.TripleDES" /> 的特定实现的名称。</param>
    public static TripleDES Create(string str)
    {
      return (TripleDES) CryptoConfig.CreateFromName(str);
    }

    /// <summary>确定指定的密钥是否为弱密钥。</summary>
    /// <returns>如果该密钥为弱密钥，则为 true；否则，为 false。</returns>
    /// <param name="rgbKey">要进行漏洞测试的机密密钥。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <paramref name="rgbKey" /> 参数的大小无效。</exception>
    public static bool IsWeakKey(byte[] rgbKey)
    {
      if (!TripleDES.IsLegalKeySize(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      byte[] rgbKey1 = Utils.FixupKeyParity(rgbKey);
      return TripleDES.EqualBytes(rgbKey1, 0, 8, 8) || rgbKey1.Length == 24 && TripleDES.EqualBytes(rgbKey1, 8, 16, 8);
    }

    private static bool EqualBytes(byte[] rgbKey, int start1, int start2, int count)
    {
      if (start1 < 0)
        throw new ArgumentOutOfRangeException("start1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (start2 < 0)
        throw new ArgumentOutOfRangeException("start2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (start1 + count > rgbKey.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (start2 + count > rgbKey.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      for (int index = 0; index < count; ++index)
      {
        if ((int) rgbKey[start1 + index] != (int) rgbKey[start2 + index])
          return false;
      }
      return true;
    }

    private static bool IsLegalKeySize(byte[] rgbKey)
    {
      return rgbKey != null && (rgbKey.Length == 16 || rgbKey.Length == 24);
    }
  }
}
