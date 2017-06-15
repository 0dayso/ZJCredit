// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RIPEMD160
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示 MD160 哈希算法的所有实现均从中继承的抽象类。</summary>
  [ComVisible(true)]
  public abstract class RIPEMD160 : HashAlgorithm
  {
    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RIPEMD160" /> 类的新实例。</summary>
    protected RIPEMD160()
    {
      this.HashSizeValue = 160;
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.RIPEMD160" /> 哈希算法的默认实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RIPEMD160" /> 哈希算法的新实例。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">该算法在使用时启用了联邦信息处理标准 (FIPS) 模式，但它与 FIPS 不兼容。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static RIPEMD160 Create()
    {
      return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.RIPEMD160" /> 哈希算法的指定实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.RIPEMD160" /> 的指定实现的新实例。</returns>
    /// <param name="hashName">要使用的 <see cref="T:System.Security.Cryptography.RIPEMD160" /> 的特定实现的名称。</param>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="hashName" /> 参数描述的算法在使用时启用了联邦信息处理标准 (FIPS) 模式，但它与 FIPS 不兼容。</exception>
    public static RIPEMD160 Create(string hashName)
    {
      return (RIPEMD160) CryptoConfig.CreateFromName(hashName);
    }
  }
}
