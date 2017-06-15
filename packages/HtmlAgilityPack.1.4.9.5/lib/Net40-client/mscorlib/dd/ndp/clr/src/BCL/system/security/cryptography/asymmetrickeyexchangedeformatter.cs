// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricKeyExchangeDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有非对称密钥交换反格式化程序均从中派生的基类。</summary>
  [ComVisible(true)]
  public abstract class AsymmetricKeyExchangeDeformatter
  {
    /// <summary>当在派生类中重写时，获取或设置非对称密钥交换的参数。</summary>
    /// <returns>XML 格式的字符串，它包含非对称密钥交换操作的参数。</returns>
    public abstract string Parameters { get; set; }

    /// <summary>当在派生类中重写时，设置用于解密机密信息的私钥。</summary>
    /// <param name="key">包含私钥的 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 实现的实例。</param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>当在派生类中重写时，从加密的密钥交换数据中提取机密信息。</summary>
    /// <returns>从密钥交换数据导出的机密信息。</returns>
    /// <param name="rgb">其中隐藏有机密信息的密钥交换数据。</param>
    public abstract byte[] DecryptKeyExchange(byte[] rgb);
  }
}
