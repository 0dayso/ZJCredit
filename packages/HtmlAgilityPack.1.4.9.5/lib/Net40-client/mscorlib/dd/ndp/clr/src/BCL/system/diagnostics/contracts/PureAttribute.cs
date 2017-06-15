// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.PureAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>指示一个类型或方法为纯类型或纯方法，即它不进行任何可视的状态更改。</summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
  [__DynamicallyInvokable]
  public sealed class PureAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Diagnostics.Contracts.PureAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public PureAttribute()
    {
    }
  }
}
