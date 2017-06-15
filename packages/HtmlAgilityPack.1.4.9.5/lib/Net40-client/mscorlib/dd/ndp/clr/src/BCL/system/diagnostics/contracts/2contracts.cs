// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractVerificationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>指示分析工具假定程序集、类型或成员的正确性，而不执行静态验证。</summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
  [__DynamicallyInvokable]
  public sealed class ContractVerificationAttribute : Attribute
  {
    private bool _value;

    /// <summary>获取指示是否验证目标的协定的值。</summary>
    /// <returns>如果需要验证，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool Value
    {
      [__DynamicallyInvokable] get
      {
        return this._value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Contracts.ContractVerificationAttribute" /> 类的新实例。</summary>
    /// <param name="value">若为 true，则需要验证；否则为 false。</param>
    [__DynamicallyInvokable]
    public ContractVerificationAttribute(bool value)
    {
      this._value = value;
    }
  }
}
