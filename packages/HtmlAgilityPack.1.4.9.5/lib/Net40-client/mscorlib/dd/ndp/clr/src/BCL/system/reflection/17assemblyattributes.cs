// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblySignatureKeyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>提供从更早、更简单的名称键值到更大、散列值算法更安全的键值的迁移。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class AssemblySignatureKeyAttribute : Attribute
  {
    private string _publicKey;
    private string _countersignature;

    /// <summary>获取用于签名程序集的强名称的公钥。</summary>
    /// <returns>此程序集的公钥。</returns>
    [__DynamicallyInvokable]
    public string PublicKey
    {
      [__DynamicallyInvokable] get
      {
        return this._publicKey;
      }
    }

    /// <summary>获取此程序集强名称的副署。</summary>
    /// <returns>此签名密钥的副署。</returns>
    [__DynamicallyInvokable]
    public string Countersignature
    {
      [__DynamicallyInvokable] get
      {
        return this._countersignature;
      }
    }

    /// <summary>使用指定的公钥和副署创建 <see cref="T:System.Reflection.AssemblySignatureKeyAttribute" /> 类的新实例。</summary>
    /// <param name="publicKey">公开或标识密钥。</param>
    /// <param name="countersignature">强名称密钥的签名密钥的副署。</param>
    [__DynamicallyInvokable]
    public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
    {
      this._publicKey = publicKey;
      this._countersignature = countersignature;
    }
  }
}
