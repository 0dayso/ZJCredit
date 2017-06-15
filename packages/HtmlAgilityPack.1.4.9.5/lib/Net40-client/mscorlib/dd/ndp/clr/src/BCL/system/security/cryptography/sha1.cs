// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>计算输入数据的 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值。</summary>
  [ComVisible(true)]
  public abstract class SHA1 : HashAlgorithm
  {
    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SHA1" /> 的新实例。</summary>
    /// <exception cref="T:System.InvalidOperationException">针对此对象的策略不符合 FIPS 算法。</exception>
    protected SHA1()
    {
      this.HashSizeValue = 160;
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.SHA1" /> 的默认实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.SHA1" /> 的新实例。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static SHA1 Create()
    {
      return SHA1.Create("System.Security.Cryptography.SHA1");
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.SHA1" /> 的指定实现的实例。</summary>
    /// <returns>使用指定实现的 <see cref="T:System.Security.Cryptography.SHA1" /> 的新实例。</returns>
    /// <param name="hashName">要使用的 <see cref="T:System.Security.Cryptography.SHA1" /> 的特定实现的名称。</param>
    public static SHA1 Create(string hashName)
    {
      return (SHA1) CryptoConfig.CreateFromName(hashName);
    }
  }
}
