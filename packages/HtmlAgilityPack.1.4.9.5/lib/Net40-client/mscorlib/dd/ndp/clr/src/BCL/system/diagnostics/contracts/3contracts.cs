// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>指定在某个字段的可见性低于方法时可在方法协定中使用该字段。</summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Field)]
  [__DynamicallyInvokable]
  public sealed class ContractPublicPropertyNameAttribute : Attribute
  {
    private string _publicName;

    /// <summary>获取要应用于字段的属性名称。</summary>
    /// <returns>要应用于字段的属性名称。</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this._publicName;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute" /> 类的新实例。</summary>
    /// <param name="name">要应用于字段的属性名称。</param>
    [__DynamicallyInvokable]
    public ContractPublicPropertyNameAttribute(string name)
    {
      this._publicName = name;
    }
  }
}
