// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongNamePublicKeyBlob
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>表示强名称的公钥信息（称为 Blob）。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNamePublicKeyBlob
  {
    internal byte[] PublicKey;

    internal StrongNamePublicKeyBlob()
    {
    }

    /// <summary>使用公钥 Blob 的原始字节初始化 <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> 类的新实例。</summary>
    /// <param name="publicKey">表示原始公钥数据的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="publicKey" /> 参数为 null。</exception>
    public StrongNamePublicKeyBlob(byte[] publicKey)
    {
      if (publicKey == null)
        throw new ArgumentNullException("PublicKey");
      this.PublicKey = new byte[publicKey.Length];
      Array.Copy((Array) publicKey, 0, (Array) this.PublicKey, 0, publicKey.Length);
    }

    internal StrongNamePublicKeyBlob(string publicKey)
    {
      this.PublicKey = Hex.DecodeHexString(publicKey);
    }

    private static bool CompareArrays(byte[] first, byte[] second)
    {
      if (first.Length != second.Length)
        return false;
      int length = first.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) first[index] != (int) second[index])
          return false;
      }
      return true;
    }

    internal bool Equals(StrongNamePublicKeyBlob blob)
    {
      if (blob == null)
        return false;
      return StrongNamePublicKeyBlob.CompareArrays(this.PublicKey, blob.PublicKey);
    }

    /// <summary>获取或设置一个值，该值指示当前公钥 Blob 是否等于所指定的公钥 Blob。</summary>
    /// <returns>如果当前对象的公钥 Blob 等于 <paramref name="obj" /> 参数的公钥 Blob，则值为 true，否则值为 false。</returns>
    /// <param name="obj">包含公钥 Blob 的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is StrongNamePublicKeyBlob))
        return false;
      return this.Equals((StrongNamePublicKeyBlob) obj);
    }

    private static int GetByteArrayHashCode(byte[] baData)
    {
      if (baData == null)
        return 0;
      int num = 0;
      for (int index = 0; index < baData.Length; ++index)
        num = num << 8 ^ (int) baData[index] ^ num >> 24;
      return num;
    }

    /// <summary>返回一个基于公钥的哈希代码。</summary>
    /// <returns>基于公钥的哈希代码。</returns>
    public override int GetHashCode()
    {
      return StrongNamePublicKeyBlob.GetByteArrayHashCode(this.PublicKey);
    }

    /// <summary>创建并返回公钥 Blob 的字符串表示形式。</summary>
    /// <returns>公钥 Blob 的十六进制版本。</returns>
    public override string ToString()
    {
      return Hex.EncodeHexString(this.PublicKey);
    }
  }
}
