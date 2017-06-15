// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SignatureDescription
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>包含有关数字签名的属性的信息。</summary>
  [ComVisible(true)]
  public class SignatureDescription
  {
    private string _strKey;
    private string _strDigest;
    private string _strFormatter;
    private string _strDeformatter;

    /// <summary>获取或设置签名说明的密钥算法。</summary>
    /// <returns>签名说明的密钥算法。</returns>
    public string KeyAlgorithm
    {
      get
      {
        return this._strKey;
      }
      set
      {
        this._strKey = value;
      }
    }

    /// <summary>获取或设置签名说明的摘要算法。</summary>
    /// <returns>签名说明的摘要算法。</returns>
    public string DigestAlgorithm
    {
      get
      {
        return this._strDigest;
      }
      set
      {
        this._strDigest = value;
      }
    }

    /// <summary>获取或设置签名说明的格式化程序算法。</summary>
    /// <returns>签名说明的格式化程序算法。</returns>
    public string FormatterAlgorithm
    {
      get
      {
        return this._strFormatter;
      }
      set
      {
        this._strFormatter = value;
      }
    }

    /// <summary>获取或设置签名说明的反格式化程序算法。</summary>
    /// <returns>签名说明的反格式化程序算法。</returns>
    public string DeformatterAlgorithm
    {
      get
      {
        return this._strDeformatter;
      }
      set
      {
        this._strDeformatter = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SignatureDescription" /> 类的新实例。</summary>
    public SignatureDescription()
    {
    }

    /// <summary>从指定的 <see cref="T:System.Security.SecurityElement" /> 初始化 <see cref="T:System.Security.Cryptography.SignatureDescription" /> 类的新实例。</summary>
    /// <param name="el">从中获取签名说明的算法的 <see cref="T:System.Security.SecurityElement" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="el" /> 参数为 null。</exception>
    public SignatureDescription(SecurityElement el)
    {
      if (el == null)
        throw new ArgumentNullException("el");
      this._strKey = el.SearchForTextOfTag("Key");
      this._strDigest = el.SearchForTextOfTag("Digest");
      this._strFormatter = el.SearchForTextOfTag("Formatter");
      this._strDeformatter = el.SearchForTextOfTag("Deformatter");
    }

    /// <summary>使用 <see cref="P:System.Security.Cryptography.SignatureDescription.DeformatterAlgorithm" /> 属性创建具有指定密钥的 <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> 实例。</summary>
    /// <returns>新创建的 <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> 实例。</returns>
    /// <param name="key">要在 <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> 中使用的密钥。</param>
    public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureDeformatter signatureDeformatter = (AsymmetricSignatureDeformatter) CryptoConfig.CreateFromName(this._strDeformatter);
      AsymmetricAlgorithm key1 = key;
      signatureDeformatter.SetKey(key1);
      return signatureDeformatter;
    }

    /// <summary>使用 <see cref="P:System.Security.Cryptography.SignatureDescription.FormatterAlgorithm" /> 属性创建具有指定密钥的 <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> 实例。</summary>
    /// <returns>新创建的 <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> 实例。</returns>
    /// <param name="key">要在 <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> 中使用的密钥。</param>
    public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureFormatter signatureFormatter = (AsymmetricSignatureFormatter) CryptoConfig.CreateFromName(this._strFormatter);
      AsymmetricAlgorithm key1 = key;
      signatureFormatter.SetKey(key1);
      return signatureFormatter;
    }

    /// <summary>使用 <see cref="P:System.Security.Cryptography.SignatureDescription.DigestAlgorithm" /> 属性创建 <see cref="T:System.Security.Cryptography.HashAlgorithm" /> 实例。</summary>
    /// <returns>新创建的 <see cref="T:System.Security.Cryptography.HashAlgorithm" /> 实例。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual HashAlgorithm CreateDigest()
    {
      return (HashAlgorithm) CryptoConfig.CreateFromName(this._strDigest);
    }
  }
}
