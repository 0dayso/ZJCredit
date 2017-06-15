// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SymmetricAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有对称算法的实现都必须从中继承的抽象基类。</summary>
  [ComVisible(true)]
  public abstract class SymmetricAlgorithm : IDisposable
  {
    /// <summary>表示加密操作的块大小（以位为单位）。</summary>
    protected int BlockSizeValue;
    /// <summary>表示加密操作的反馈大小（以位为单位）。</summary>
    protected int FeedbackSizeValue;
    /// <summary>表示对称算法的初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />)。</summary>
    protected byte[] IVValue;
    /// <summary>表示对称算法的密钥。</summary>
    protected byte[] KeyValue;
    /// <summary>指定对称算法支持的块大小（以位为单位）。</summary>
    protected KeySizes[] LegalBlockSizesValue;
    /// <summary>指定对称算法支持的密钥大小（以位为单位）。</summary>
    protected KeySizes[] LegalKeySizesValue;
    /// <summary>表示对称算法使用的密钥的大小（以位为单位）。</summary>
    protected int KeySizeValue;
    /// <summary>表示对称算法中使用的密码模式。</summary>
    protected CipherMode ModeValue;
    /// <summary>表示对称算法中使用的填充模式。</summary>
    protected PaddingMode PaddingValue;

    /// <summary>获取或设置加密操作的块大小（以位为单位）。</summary>
    /// <returns>块大小（以位为单位）。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">块大小无效。</exception>
    public virtual int BlockSize
    {
      get
      {
        return this.BlockSizeValue;
      }
      set
      {
        for (int index = 0; index < this.LegalBlockSizesValue.Length; ++index)
        {
          if (this.LegalBlockSizesValue[index].SkipSize == 0)
          {
            if (this.LegalBlockSizesValue[index].MinSize == value)
            {
              this.BlockSizeValue = value;
              this.IVValue = (byte[]) null;
              return;
            }
          }
          else
          {
            int minSize = this.LegalBlockSizesValue[index].MinSize;
            while (minSize <= this.LegalBlockSizesValue[index].MaxSize)
            {
              if (minSize == value)
              {
                if (this.BlockSizeValue == value)
                  return;
                this.BlockSizeValue = value;
                this.IVValue = (byte[]) null;
                return;
              }
              minSize += this.LegalBlockSizesValue[index].SkipSize;
            }
          }
        }
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidBlockSize"));
      }
    }

    /// <summary>获取或设置加密操作的反馈大小（以位为单位）。</summary>
    /// <returns>反馈大小（以位为单位）。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">反馈大小大于块大小。</exception>
    public virtual int FeedbackSize
    {
      get
      {
        return this.FeedbackSizeValue;
      }
      set
      {
        if (value <= 0 || value > this.BlockSizeValue || value % 8 != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFeedbackSize"));
        this.FeedbackSizeValue = value;
      }
    }

    /// <summary>获取或设置对称算法的初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />)。</summary>
    /// <returns>初始化向量。</returns>
    /// <exception cref="T:System.ArgumentNullException">试图将初始化向量设置为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">试图将初始化向量设置为无效大小。</exception>
    public virtual byte[] IV
    {
      get
      {
        if (this.IVValue == null)
          this.GenerateIV();
        return (byte[]) this.IVValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (value.Length != this.BlockSizeValue / 8)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
        this.IVValue = (byte[]) value.Clone();
      }
    }

    /// <summary>获取或设置对称算法的密钥。</summary>
    /// <returns>用于对称算法的密钥。</returns>
    /// <exception cref="T:System.ArgumentNullException">试图将密钥设置为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">密钥大小无效。</exception>
    public virtual byte[] Key
    {
      get
      {
        if (this.KeyValue == null)
          this.GenerateKey();
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (!this.ValidKeySize(value.Length * 8))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        this.KeyValue = (byte[]) value.Clone();
        this.KeySizeValue = value.Length * 8;
      }
    }

    /// <summary>获取对称算法支持的块大小（以位为单位）。</summary>
    /// <returns>一个数组，包含此算法支持的块大小。</returns>
    public virtual KeySizes[] LegalBlockSizes
    {
      get
      {
        return (KeySizes[]) this.LegalBlockSizesValue.Clone();
      }
    }

    /// <summary>获取对称算法支持的密钥大小（以位为单位）。</summary>
    /// <returns>一个数组，包含此算法支持的密钥大小。</returns>
    public virtual KeySizes[] LegalKeySizes
    {
      get
      {
        return (KeySizes[]) this.LegalKeySizesValue.Clone();
      }
    }

    /// <summary>获取或设置对称算法所用密钥的大小（以位为单位）。</summary>
    /// <returns>对称算法所用密钥的大小（以位为单位）。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">密钥大小无效。</exception>
    public virtual int KeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        if (!this.ValidKeySize(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        this.KeySizeValue = value;
        this.KeyValue = (byte[]) null;
      }
    }

    /// <summary>获取或设置对称算法的运算模式。</summary>
    /// <returns>对称算法的运算模式。默认值为 <see cref="F:System.Security.Cryptography.CipherMode.CBC" />。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">该密码模式不是 <see cref="T:System.Security.Cryptography.CipherMode" /> 值之一。</exception>
    public virtual CipherMode Mode
    {
      get
      {
        return this.ModeValue;
      }
      set
      {
        if (value < CipherMode.CBC || CipherMode.CFB < value)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidCipherMode"));
        this.ModeValue = value;
      }
    }

    /// <summary>获取或设置对称算法中使用的填充模式。</summary>
    /// <returns>对称算法中使用的填充模式。默认值为 <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">该填充模式不是 <see cref="T:System.Security.Cryptography.PaddingMode" /> 值之一。</exception>
    public virtual PaddingMode Padding
    {
      get
      {
        return this.PaddingValue;
      }
      set
      {
        if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
        this.PaddingValue = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> 类的新实例。</summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">从对称算法派生的类的实现无效。</exception>
    protected SymmetricAlgorithm()
    {
      this.ModeValue = CipherMode.CBC;
      this.PaddingValue = PaddingMode.PKCS7;
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> 类的当前实例所使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> 类使用的所有资源。</summary>
    public void Clear()
    {
      this.Dispose();
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.KeyValue != null)
      {
        Array.Clear((Array) this.KeyValue, 0, this.KeyValue.Length);
        this.KeyValue = (byte[]) null;
      }
      if (this.IVValue == null)
        return;
      Array.Clear((Array) this.IVValue, 0, this.IVValue.Length);
      this.IVValue = (byte[]) null;
    }

    /// <summary>确定指定的密钥大小对当前算法是否有效。</summary>
    /// <returns>如果指定的密钥大小对当前算法有效，则为 true；否则，为 false。</returns>
    /// <param name="bitLength">用于检查有效密钥大小的长度（以位为单位）。</param>
    public bool ValidKeySize(int bitLength)
    {
      KeySizes[] legalKeySizes = this.LegalKeySizes;
      if (legalKeySizes == null)
        return false;
      for (int index = 0; index < legalKeySizes.Length; ++index)
      {
        if (legalKeySizes[index].SkipSize == 0)
        {
          if (legalKeySizes[index].MinSize == bitLength)
            return true;
        }
        else
        {
          int minSize = legalKeySizes[index].MinSize;
          while (minSize <= legalKeySizes[index].MaxSize)
          {
            if (minSize == bitLength)
              return true;
            minSize += legalKeySizes[index].SkipSize;
          }
        }
      }
      return false;
    }

    /// <summary>创建用于执行对称算法的默认加密对象。</summary>
    /// <returns>用于执行对称算法的默认加密对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static SymmetricAlgorithm Create()
    {
      return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
    }

    /// <summary>创建用于执行对称算法的指定加密对象。</summary>
    /// <returns>一个加密对象，用于执行对称算法。</returns>
    /// <param name="algName">要使用的 <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> 类的特定实现的名称。</param>
    public static SymmetricAlgorithm Create(string algName)
    {
      return (SymmetricAlgorithm) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>用当前的 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> 属性和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称加密器对象。</summary>
    /// <returns>对称加密器对象。</returns>
    public virtual ICryptoTransform CreateEncryptor()
    {
      return this.CreateEncryptor(this.Key, this.IV);
    }

    /// <summary>当在派生类中重写时，用指定的 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> 属性和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称加密器对象。</summary>
    /// <returns>对称加密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的密钥。</param>
    /// <param name="rgbIV">用于对称算法的初始化向量。</param>
    public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

    /// <summary>用当前的 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> 属性和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称解密器对象。</summary>
    /// <returns>对称解密器对象。</returns>
    public virtual ICryptoTransform CreateDecryptor()
    {
      return this.CreateDecryptor(this.Key, this.IV);
    }

    /// <summary>当在派生类中重写时，用指定的 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> 属性和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称解密器对象。</summary>
    /// <returns>对称解密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的密钥。</param>
    /// <param name="rgbIV">用于对称算法的初始化向量。</param>
    public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

    /// <summary>当在派生类中重写时，生成用于该算法的随机密钥 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />)。</summary>
    public abstract void GenerateKey();

    /// <summary>当在派生类中重写时，生成用于该算法的随机初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />)。</summary>
    public abstract void GenerateIV();
  }
}
