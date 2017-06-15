// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.KeyedHashAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>显示所有加密哈希算法实现均必须从中派生的抽象类。</summary>
  [ComVisible(true)]
  public abstract class KeyedHashAlgorithm : HashAlgorithm
  {
    /// <summary>用于哈希算法的密钥。</summary>
    protected byte[] KeyValue;

    /// <summary>获取或设置用于哈希算法的密钥。</summary>
    /// <returns>用于哈希算法的密钥。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">尝试在哈希计算已开始后更改 <see cref="P:System.Security.Cryptography.KeyedHashAlgorithm.Key" /> 属性。</exception>
    public virtual byte[] Key
    {
      get
      {
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (this.State != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
        this.KeyValue = (byte[]) value.Clone();
      }
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.KeyValue != null)
          Array.Clear((Array) this.KeyValue, 0, this.KeyValue.Length);
        this.KeyValue = (byte[]) null;
      }
      base.Dispose(disposing);
    }

    /// <summary>创建加密哈希算法的默认实现的实例。</summary>
    /// <returns>新 <see cref="T:System.Security.Cryptography.HMACSHA1" /> 实例，除非已更改默认设置。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static KeyedHashAlgorithm Create()
    {
      return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
    }

    /// <summary>创建加密哈希算法的指定实现的实例。</summary>
    /// <returns>指定的加密哈希算法的新实例。</returns>
    /// <param name="algName">要使用的加密哈希算法实现。下表显示 <paramref name="algName" /> 参数的有效值以及它们映射到的算法。参数值Implements System.Security.Cryptography.HMAC<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.KeyedHashAlgorithm<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />System.Security.Cryptography.HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />System.Security.Cryptography.HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />System.Security.Cryptography.HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />System.Security.Cryptography.HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />System.Security.Cryptography.HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" />System.Security.Cryptography.MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
    public static KeyedHashAlgorithm Create(string algName)
    {
      return (KeyedHashAlgorithm) CryptoConfig.CreateFromName(algName);
    }
  }
}
