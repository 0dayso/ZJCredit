// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.TripleDESCryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>定义访问 <see cref="T:System.Security.Cryptography.TripleDES" /> 算法的加密服务提供程序 (CSP) 版本的包装对象。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class TripleDESCryptoServiceProvider : TripleDES
  {
    /// <summary>初始化 <see cref="T:System.Security.Cryptography.TripleDESCryptoServiceProvider" /> 类的新实例。</summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <see cref="T:System.Security.Cryptography.TripleDES" /> 加密服务提供程序不可用。</exception>
    [SecuritySafeCritical]
    public TripleDESCryptoServiceProvider()
    {
      if (!Utils.HasAlgorithm(26115, 0))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
      this.FeedbackSizeValue = 8;
    }

    /// <summary>使用指定的密钥 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) 和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称 <see cref="T:System.Security.Cryptography.TripleDES" /> 加密器对象。</summary>
    /// <returns>对称 <see cref="T:System.Security.Cryptography.TripleDES" /> 加密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的密钥。</param>
    /// <param name="rgbIV">用于对称算法的初始化向量。说明初始化向量必须长 8 字节。如果它的长度大于 8 字节，则会截断它，而不会引发异常。调用 <see cref="M:System.Security.Cryptography.TripleDESCryptoServiceProvider.CreateEncryptor(System.Byte[],System.Byte[])" /> 之前，请检查初始化向量的长度，如果此向量长度太长，则将引发异常。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> 属性的值为 <see cref="F:System.Security.Cryptography.CipherMode.OFB" />。- 或 -<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> 属性的值为 <see cref="F:System.Security.Cryptography.CipherMode.CFB" />，而 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> 属性的值不是 8。- 或 -使用了无效的密钥大小。- 或 -算法密钥大小不可用。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
    {
      if (TripleDES.IsWeakKey(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
    }

    /// <summary>使用指定的密钥 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) 和初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) 创建对称 <see cref="T:System.Security.Cryptography.TripleDES" /> 解密器对象。</summary>
    /// <returns>对称 <see cref="T:System.Security.Cryptography.TripleDES" /> 解密器对象。</returns>
    /// <param name="rgbKey">用于对称算法的密钥。</param>
    /// <param name="rgbIV">用于对称算法的初始化向量。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> 属性的值为 <see cref="F:System.Security.Cryptography.CipherMode.OFB" />。- 或 -<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> 属性的值为 <see cref="F:System.Security.Cryptography.CipherMode.CFB" />，而 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> 属性的值不是 8。- 或 -使用了无效的密钥大小。- 或 -算法密钥大小不可用。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
    {
      if (TripleDES.IsWeakKey(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
    }

    /// <summary>生成用于该算法的随机 <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />。</summary>
    public override void GenerateKey()
    {
      this.KeyValue = new byte[this.KeySizeValue / 8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
      while (TripleDES.IsWeakKey(this.KeyValue))
        Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
    }

    /// <summary>生成用于该算法的随机初始化向量 (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />)。</summary>
    public override void GenerateIV()
    {
      this.IVValue = new byte[8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
    }

    [SecurityCritical]
    private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, CryptoAPITransformMode encryptMode)
    {
      int cArgs = 0;
      int[] rgArgIds = new int[10];
      object[] rgArgValues = new object[10];
      int algid = 26115;
      if (mode == CipherMode.OFB)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
      if (mode == CipherMode.CFB && feedbackSize != 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
      if (rgbKey == null)
      {
        rgbKey = new byte[this.KeySizeValue / 8];
        Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
      }
      if (mode != CipherMode.CBC)
      {
        rgArgIds[cArgs] = 4;
        rgArgValues[cArgs] = (object) mode;
        ++cArgs;
      }
      if (mode != CipherMode.ECB)
      {
        if (rgbIV == null)
        {
          rgbIV = new byte[8];
          Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
        }
        if (rgbIV.Length < 8)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
        rgArgIds[cArgs] = 1;
        rgArgValues[cArgs] = (object) rgbIV;
        ++cArgs;
      }
      if (mode == CipherMode.OFB || mode == CipherMode.CFB)
      {
        rgArgIds[cArgs] = 5;
        rgArgValues[cArgs] = (object) feedbackSize;
        ++cArgs;
      }
      if (rgbKey.Length == 16)
        algid = 26121;
      return (ICryptoTransform) new CryptoAPITransform(algid, cArgs, rgArgIds, rgArgValues, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, false, encryptMode);
    }
  }
}
