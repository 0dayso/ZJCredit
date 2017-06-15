// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>指定一个单独的类型包含此类型的代码协定。</summary>
  [Conditional("CONTRACTS_FULL")]
  [Conditional("DEBUG")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ContractClassAttribute : Attribute
  {
    private Type _typeWithContracts;

    /// <summary>获取包含此类型的代码协定的类型。</summary>
    /// <returns>包含此类型的代码协定的类型。</returns>
    [__DynamicallyInvokable]
    public Type TypeContainingContracts
    {
      [__DynamicallyInvokable] get
      {
        return this._typeWithContracts;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Contracts.ContractClassAttribute" /> 类的新实例。</summary>
    /// <param name="typeContainingContracts">包含此类型的代码协定的类型。</param>
    [__DynamicallyInvokable]
    public ContractClassAttribute(Type typeContainingContracts)
    {
      this._typeWithContracts = typeContainingContracts;
    }
  }
}
