// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricKeyExchangeFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有非对称密钥交换格式化程序均从中派生的基类。</summary>
  [ComVisible(true)]
  public abstract class AsymmetricKeyExchangeFormatter
  {
    /// <summary>当在派生类中重写时，获取非对称密钥交换的参数。</summary>
    /// <returns>XML 格式的字符串，它包含非对称密钥交换操作的参数。</returns>
    public abstract string Parameters { get; }

    /// <summary>当在派生类中重写时，设置用于加密机密信息的公钥。</summary>
    /// <param name="key">包含公钥的 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 实现的实例。</param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>当在派生类中重写时，从指定的输入数据创建加密的密钥交换数据。</summary>
    /// <returns>要发送给目标接收者的加密的密钥交换数据。</returns>
    /// <param name="data">在密钥交换中要传递的机密信息。</param>
    public abstract byte[] CreateKeyExchange(byte[] data);

    /// <summary>当在派生类中重写时，从指定的输入数据创建加密的密钥交换数据。</summary>
    /// <returns>要发送给目标接收者的加密的密钥交换数据。</returns>
    /// <param name="data">在密钥交换中要传递的机密信息。</param>
    /// <param name="symAlgType">在当前版本中不使用此参数。</param>
    public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
  }
}
