// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractClassForAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>指定一个类是某个类型的协定。</summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ContractClassForAttribute : Attribute
  {
    private Type _typeIAmAContractFor;

    /// <summary>获取此代码协定应用于的类型。</summary>
    /// <returns>此协定应用于的类型。</returns>
    [__DynamicallyInvokable]
    public Type TypeContractsAreFor
    {
      [__DynamicallyInvokable] get
      {
        return this._typeIAmAContractFor;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Contracts.ContractClassForAttribute" /> 类的新实例，并指定使用当前类作为协定的类型。</summary>
    /// <param name="typeContractsAreFor">使用当前类作为协定的类型。</param>
    [__DynamicallyInvokable]
    public ContractClassForAttribute(Type typeContractsAreFor)
    {
      this._typeIAmAContractFor = typeContractsAreFor;
    }
  }
}
