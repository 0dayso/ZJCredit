// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.Internal.ContractHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics.Contracts.Internal
{
  /// <summary>提供二进制重写工具用来处理协定失败的方法。</summary>
  [Obsolete("Use the ContractHelper class in the System.Runtime.CompilerServices namespace instead.")]
  [__DynamicallyInvokable]
  public static class ContractHelper
  {
    /// <summary>由二进制重写工具用来激活默认失败行为。</summary>
    /// <returns>如果事件已经过处理且不应触发失败，则为空引用（在 Visual Basic 中为 Nothing）；否则返回本地化的失败消息。</returns>
    /// <param name="failureKind">失败的类型。</param>
    /// <param name="userMessage">其他用户信息。</param>
    /// <param name="conditionText">对导致失败的条件的说明。</param>
    /// <param name="innerException">导致当前异常的内部异常。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="failureKind" /> 不是有效的 <see cref="T:System.Diagnostics.Contracts.ContractFailureKind" /> 值。</exception>
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
    {
      return System.Runtime.CompilerServices.ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
    }

    /// <summary>触发默认失败行为。</summary>
    /// <param name="kind">失败的类型。</param>
    /// <param name="displayMessage">要显示的消息。</param>
    /// <param name="userMessage">其他用户信息。</param>
    /// <param name="conditionText">对导致失败的条件的说明。</param>
    /// <param name="innerException">导致当前异常的内部异常。</param>
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
    {
      System.Runtime.CompilerServices.ContractHelper.TriggerFailure(kind, displayMessage, userMessage, conditionText, innerException);
    }
  }
}
