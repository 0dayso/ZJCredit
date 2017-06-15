// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MD5
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示 <see cref="T:System.Security.Cryptography.MD5" /> 哈希算法的所有实现均从中继承的抽象类。</summary>
  [ComVisible(true)]
  public abstract class MD5 : HashAlgorithm
  {
    /// <summary>初始化 <see cref="T:System.Security.Cryptography.MD5" /> 的新实例。</summary>
    protected MD5()
    {
      this.HashSizeValue = 128;
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.MD5" /> 哈希算法的默认实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.MD5" /> 哈希算法的新实例。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">该算法在使用中启用了联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static MD5 Create()
    {
      return MD5.Create("System.Security.Cryptography.MD5");
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.MD5" /> 哈希算法的指定实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.MD5" /> 的指定实现的新实例。</returns>
    /// <param name="algName">要使用的 <see cref="T:System.Security.Cryptography.MD5" /> 的特定实现的名称。</param>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="algName" /> 参数描述的算法在使用中已启用联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    public static MD5 Create(string algName)
    {
      return (MD5) CryptoConfig.CreateFromName(algName);
    }
  }
}
