// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA384
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>计算输入数据的 <see cref="T:System.Security.Cryptography.SHA384" /> 哈希值。</summary>
  [ComVisible(true)]
  public abstract class SHA384 : HashAlgorithm
  {
    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SHA384" /> 的新实例。</summary>
    protected SHA384()
    {
      this.HashSizeValue = 384;
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.SHA384" /> 的默认实现的实例。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.SHA384" /> 的新实例。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">该算法在使用中启用了联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static SHA384 Create()
    {
      return SHA384.Create("System.Security.Cryptography.SHA384");
    }

    /// <summary>创建 <see cref="T:System.Security.Cryptography.SHA384" /> 的指定实现的实例。</summary>
    /// <returns>使用指定实现的 <see cref="T:System.Security.Cryptography.SHA384" /> 的新实例。</returns>
    /// <param name="hashName">要使用的 <see cref="T:System.Security.Cryptography.SHA384" /> 的特定实现的名称。</param>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="hashName" /> 参数描述的算法在使用中已启用联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    public static SHA384 Create(string hashName)
    {
      return (SHA384) CryptoConfig.CreateFromName(hashName);
    }
  }
}
