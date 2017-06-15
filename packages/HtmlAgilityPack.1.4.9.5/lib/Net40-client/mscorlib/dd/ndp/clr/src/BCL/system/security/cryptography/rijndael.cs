// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Rijndael
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示 <see cref="T:System.Security.Cryptography.Rijndael" /> 对称加密算法的所有实现必须从其继承的基类。</summary>
  [ComVisible(true)]
  public abstract class Rijndael : SymmetricAlgorithm
  {
    private static KeySizes[] s_legalBlockSizes = new KeySizes[1]{ new KeySizes(128, 256, 64) };
    private static KeySizes[] s_legalKeySizes = new KeySizes[1]{ new KeySizes(128, 256, 64) };

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.Rijndael" /> 的新实例。</summary>
    protected Rijndael()
    {
      this.KeySizeValue = 256;
      this.BlockSizeValue = 128;
      this.FeedbackSizeValue = this.BlockSizeValue;
      this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
      this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
    }

    /// <summary>创建加密对象以执行 <see cref="T:System.Security.Cryptography.Rijndael" /> 算法。</summary>
    /// <returns>加密对象。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">该算法在使用中启用了联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static Rijndael Create()
    {
      return Rijndael.Create("System.Security.Cryptography.Rijndael");
    }

    /// <summary>创建加密对象以执行 <see cref="T:System.Security.Cryptography.Rijndael" /> 算法的指定实现。</summary>
    /// <returns>加密对象。</returns>
    /// <param name="algName">要创建的 <see cref="T:System.Security.Cryptography.Rijndael" /> 特定实现的名称。</param>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="algName" /> 参数描述的算法在使用中已启用联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    public static Rijndael Create(string algName)
    {
      return (Rijndael) CryptoConfig.CreateFromName(algName);
    }
  }
}
