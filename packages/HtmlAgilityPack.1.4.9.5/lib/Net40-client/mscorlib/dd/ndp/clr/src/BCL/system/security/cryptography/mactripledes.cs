// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MACTripleDES
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用 <see cref="T:System.Security.Cryptography.TripleDES" /> 计算输入数据 <see cref="T:System.Security.Cryptography.CryptoStream" /> 的消息验证代码 (MAC)。</summary>
  [ComVisible(true)]
  public class MACTripleDES : KeyedHashAlgorithm
  {
    private ICryptoTransform m_encryptor;
    private CryptoStream _cs;
    private TailStream _ts;
    private const int m_bitsPerByte = 8;
    private int m_bytesPerBlock;
    private TripleDES des;

    /// <summary>获取或设置哈希算法中使用的填充模式。</summary>
    /// <returns>哈希算法中使用的填充模式。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法设置属性，因为填充模式无效。</exception>
    [ComVisible(false)]
    public PaddingMode Padding
    {
      get
      {
        return this.des.Padding;
      }
      set
      {
        if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
        this.des.Padding = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.MACTripleDES" /> 类的新实例。</summary>
    public MACTripleDES()
    {
      this.KeyValue = new byte[24];
      Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
      this.des = TripleDES.Create();
      this.HashSizeValue = this.des.BlockSize;
      this.m_bytesPerBlock = this.des.BlockSize / 8;
      this.des.IV = new byte[this.m_bytesPerBlock];
      this.des.Padding = PaddingMode.Zeros;
      this.m_encryptor = (ICryptoTransform) null;
    }

    /// <summary>使用指定的密钥数据初始化 <see cref="T:System.Security.Cryptography.MACTripleDES" /> 类的新实例。</summary>
    /// <param name="rgbKey">
    /// <see cref="T:System.Security.Cryptography.MACTripleDES" /> 加密的机密密钥。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbKey" /> 参数为 null。</exception>
    public MACTripleDES(byte[] rgbKey)
      : this("System.Security.Cryptography.TripleDES", rgbKey)
    {
    }

    /// <summary>使用 <see cref="T:System.Security.Cryptography.MACTripleDES" /> 的指定实现，用指定的密钥数据初始化 <see cref="T:System.Security.Cryptography.TripleDES" /> 类的新实例。</summary>
    /// <param name="strTripleDES">要使用的 <see cref="T:System.Security.Cryptography.TripleDES" /> 实现的名称。</param>
    /// <param name="rgbKey">
    /// <see cref="T:System.Security.Cryptography.MACTripleDES" /> 加密的机密密钥。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rgbKey" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    /// <paramref name="strTripleDES" /> 参数不是 <see cref="T:System.Security.Cryptography.TripleDES" /> 实现的有效名称。</exception>
    public MACTripleDES(string strTripleDES, byte[] rgbKey)
    {
      if (rgbKey == null)
        throw new ArgumentNullException("rgbKey");
      this.des = strTripleDES != null ? TripleDES.Create(strTripleDES) : TripleDES.Create();
      this.HashSizeValue = this.des.BlockSize;
      this.KeyValue = (byte[]) rgbKey.Clone();
      this.m_bytesPerBlock = this.des.BlockSize / 8;
      this.des.IV = new byte[this.m_bytesPerBlock];
      this.des.Padding = PaddingMode.Zeros;
      this.m_encryptor = (ICryptoTransform) null;
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.MACTripleDES" /> 的实例。</summary>
    public override void Initialize()
    {
      this.m_encryptor = (ICryptoTransform) null;
    }

    /// <summary>将写入对象的数据路由到 <see cref="T:System.Security.Cryptography.TripleDES" /> 加密器以计算消息验证代码 (MAC)。</summary>
    /// <param name="rgbData">输入数据。</param>
    /// <param name="ibStart">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="cbSize">数组中用作数据的字节数。</param>
    protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
    {
      if (this.m_encryptor == null)
      {
        this.des.Key = this.Key;
        this.m_encryptor = this.des.CreateEncryptor();
        this._ts = new TailStream(this.des.BlockSize / 8);
        this._cs = new CryptoStream((Stream) this._ts, this.m_encryptor, CryptoStreamMode.Write);
      }
      this._cs.Write(rgbData, ibStart, cbSize);
    }

    /// <summary>在所有数据写入对象后返回计算所得的消息验证代码 (MAC)。</summary>
    /// <returns>计算所得的 MAC。</returns>
    protected override byte[] HashFinal()
    {
      if (this.m_encryptor == null)
      {
        this.des.Key = this.Key;
        this.m_encryptor = this.des.CreateEncryptor();
        this._ts = new TailStream(this.des.BlockSize / 8);
        this._cs = new CryptoStream((Stream) this._ts, this.m_encryptor, CryptoStreamMode.Write);
      }
      this._cs.FlushFinalBlock();
      return this._ts.Buffer;
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.MACTripleDES" /> 实例使用的资源。</summary>
    /// <param name="disposing">如果该方法是从 <see cref="M:System.IDisposable.Dispose" /> 实现调用的，则为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.des != null)
          this.des.Clear();
        if (this.m_encryptor != null)
          this.m_encryptor.Dispose();
        if (this._cs != null)
          this._cs.Clear();
        if (this._ts != null)
          this._ts.Clear();
      }
      base.Dispose(disposing);
    }
  }
}
