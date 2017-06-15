// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示非对称算法的所有实现都必须从中继承的抽象基类。</summary>
  [ComVisible(true)]
  public abstract class AsymmetricAlgorithm : IDisposable
  {
    /// <summary>表示非对称算法所用密钥模块的大小（以位为单位）。</summary>
    protected int KeySizeValue;
    /// <summary>指定非对称算法支持的密钥大小。</summary>
    protected KeySizes[] LegalKeySizesValue;

    /// <summary>获取或设置非对称算法所用密钥模块的大小（以位为单位）。</summary>
    /// <returns>非对称算法所用密钥模块的大小（以位为单位）。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">密钥模块的大小无效。</exception>
    public virtual int KeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        for (int index = 0; index < this.LegalKeySizesValue.Length; ++index)
        {
          if (this.LegalKeySizesValue[index].SkipSize == 0)
          {
            if (this.LegalKeySizesValue[index].MinSize == value)
            {
              this.KeySizeValue = value;
              return;
            }
          }
          else
          {
            int minSize = this.LegalKeySizesValue[index].MinSize;
            while (minSize <= this.LegalKeySizesValue[index].MaxSize)
            {
              if (minSize == value)
              {
                this.KeySizeValue = value;
                return;
              }
              minSize += this.LegalKeySizesValue[index].SkipSize;
            }
          }
        }
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      }
    }

    /// <summary>获取非对称算法支持的密钥大小。</summary>
    /// <returns>一个数组，它包含非对称算法支持的密钥大小。</returns>
    public virtual KeySizes[] LegalKeySizes
    {
      get
      {
        return (KeySizes[]) this.LegalKeySizesValue.Clone();
      }
    }

    /// <summary>当在派生类中实现时，请获取签名算法的名称。否则，始终将引发 <see cref="T:System.NotImplementedException" />。</summary>
    /// <returns>签名算法的名称。</returns>
    public virtual string SignatureAlgorithm
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>当在派生类中重写时，请获取密钥交换算法的名称。否则，将引发 <see cref="T:System.NotImplementedException" />。</summary>
    /// <returns>密钥交换算法的名称。</returns>
    public virtual string KeyExchangeAlgorithm
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 类的当前实例所使用的所有资源。</summary>
    public void Dispose()
    {
      this.Clear();
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 类使用的所有资源。</summary>
    public void Clear()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>创建用于执行非对称算法的默认加密对象。</summary>
    /// <returns>新的 <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> 实例，除非已使用 &lt;cryptoClass&gt; 元素更改默认设置。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static AsymmetricAlgorithm Create()
    {
      return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
    }

    /// <summary>创建非对称算法的指定实现的实例。</summary>
    /// <returns>所指定的非对称算法实现的新实例。</returns>
    /// <param name="algName">要使用的非对称算法实现。下表显示 <paramref name="algName" /> 参数的有效值以及它们映射到的算法。参数值Implements System.Security.Cryptography.AsymmetricAlgorithm<see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />RSA<see cref="T:System.Security.Cryptography.RSA" />System.Security.Cryptography.RSA<see cref="T:System.Security.Cryptography.RSA" />DSA<see cref="T:System.Security.Cryptography.DSA" />System.Security.Cryptography.DSA<see cref="T:System.Security.Cryptography.DSA" />ECDsa<see cref="T:System.Security.Cryptography.ECDsa" />ECDsaCng<see cref="T:System.Security.Cryptography.ECDsaCng" />System.Security.Cryptography.ECDsaCng<see cref="T:System.Security.Cryptography.ECDsaCng" />ECDH<see cref="T:System.Security.Cryptography.ECDiffieHellman" />ECDiffieHellman<see cref="T:System.Security.Cryptography.ECDiffieHellman" />ECDiffieHellmanCng<see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" />System.Security.Cryptography.ECDiffieHellmanCng<see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /></param>
    public static AsymmetricAlgorithm Create(string algName)
    {
      return (AsymmetricAlgorithm) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>当在派生类中重写时，从 XML 字符串重新构造 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 对象。否则，将引发 <see cref="T:System.NotImplementedException" />。</summary>
    /// <param name="xmlString">用于重新构造 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 对象的 XML 字符串。</param>
    public virtual void FromXmlString(string xmlString)
    {
      throw new NotImplementedException();
    }

    /// <summary>当在派生类中重写时，创建并返回当前 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 对象的 XML 字符串表示形式。否则，将引发 <see cref="T:System.NotImplementedException" />。</summary>
    /// <returns>当前 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 对象的 XML 字符串编码。</returns>
    /// <param name="includePrivateParameters">若要包含私有参数，则为 true；否则为 false。</param>
    public virtual string ToXmlString(bool includePrivateParameters)
    {
      throw new NotImplementedException();
    }
  }
}
