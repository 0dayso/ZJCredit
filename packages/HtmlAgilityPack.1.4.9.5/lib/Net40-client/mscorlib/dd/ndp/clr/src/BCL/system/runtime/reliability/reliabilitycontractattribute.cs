// Decompiled with JetBrains decompiler
// Type: System.Runtime.ConstrainedExecution.ReliabilityContractAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ConstrainedExecution
{
  /// <summary>定义某些代码的作者和依赖于这些代码的开发人员之间的可靠性协定。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
  public sealed class ReliabilityContractAttribute : Attribute
  {
    private Consistency _consistency;
    private Cer _cer;

    /// <summary>获取 <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> 可靠性协定的值。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> 值之一。</returns>
    public Consistency ConsistencyGuarantee
    {
      get
      {
        return this._consistency;
      }
    }

    /// <summary>获取确定在受约束的执行区域 (CER) 下调用时方法、类型或程序集的行为的值。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> 值之一。</returns>
    public Cer Cer
    {
      get
      {
        return this._cer;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> 保证和 <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> 值初始化 <see cref="T:System.Runtime.ConstrainedExecution.ReliabilityContractAttribute" /> 类的新实例。</summary>
    /// <param name="consistencyGuarantee">
    /// <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> 值之一。</param>
    /// <param name="cer">
    /// <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> 值之一。</param>
    public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
    {
      this._consistency = consistencyGuarantee;
      this._cer = cer;
    }
  }
}
