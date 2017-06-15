// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ContractHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>提供二进制重写工具用来处理协定失败的方法。</summary>
  [__DynamicallyInvokable]
  public static class ContractHelper
  {
    private static readonly object lockObject = new object();
    private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;
    internal const int COR_E_CODECONTRACTFAILED = -2146233022;

    internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
    {
      [SecurityCritical] add
      {
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (ContractHelper.lockObject)
          ContractHelper.contractFailedEvent += value;
      }
      [SecurityCritical] remove
      {
        lock (ContractHelper.lockObject)
          ContractHelper.contractFailedEvent -= value;
      }
    }

    /// <summary>由二进制重写工具用来激活默认失败行为。</summary>
    /// <returns>如果事件已经过处理且不应触发失败，则为空引用（在 Visual Basic 中为 Nothing）；否则返回本地化的失败消息。</returns>
    /// <param name="failureKind">指定故障类型的枚举值之一。</param>
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
      string resultFailureMessage = "Contract failed";
      ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref resultFailureMessage);
      return resultFailureMessage;
    }

    /// <summary>触发默认失败行为。</summary>
    /// <param name="kind">指定故障类型的枚举值之一。</param>
    /// <param name="displayMessage">要显示的消息。</param>
    /// <param name="userMessage">其他用户信息。</param>
    /// <param name="conditionText">对导致失败的条件的说明。</param>
    /// <param name="innerException">导致当前异常的内部异常。</param>
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
    {
      ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
    }

    [DebuggerNonUserCode]
    [SecuritySafeCritical]
    private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
    {
      if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) failureKind), "failureKind");
      string str1 = "contract failed.";
      ContractFailedEventArgs e = (ContractFailedEventArgs) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      string str2;
      try
      {
        str1 = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
        EventHandler<ContractFailedEventArgs> eventHandler = ContractHelper.contractFailedEvent;
        if (eventHandler != null)
        {
          e = new ContractFailedEventArgs(failureKind, str1, conditionText, innerException);
          foreach (EventHandler<ContractFailedEventArgs> invocation in eventHandler.GetInvocationList())
          {
            try
            {
              invocation((object) null, e);
            }
            catch (Exception ex)
            {
              e.thrownDuringHandler = ex;
              e.SetUnwind();
            }
          }
          if (e.Unwind)
          {
            if (Environment.IsCLRHosted)
              ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, str1, conditionText, innerException);
            if (innerException == null)
              innerException = e.thrownDuringHandler;
            throw new ContractException(failureKind, str1, userMessage, conditionText, innerException);
          }
        }
      }
      finally
      {
        str2 = e == null || !e.Handled ? str1 : (string) null;
      }
      resultFailureMessage = str2;
    }

    [DebuggerNonUserCode]
    [SecuritySafeCritical]
    private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
    {
      if (Environment.IsCLRHosted)
      {
        ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
        throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
      }
      if (!Environment.UserInteractive)
        throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
      string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind));
      Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
    }

    private static string GetResourceNameForFailure(ContractFailureKind failureKind)
    {
      string str;
      switch (failureKind)
      {
        case ContractFailureKind.Precondition:
          str = "PreconditionFailed";
          break;
        case ContractFailureKind.Postcondition:
          str = "PostconditionFailed";
          break;
        case ContractFailureKind.PostconditionOnException:
          str = "PostconditionOnExceptionFailed";
          break;
        case ContractFailureKind.Invariant:
          str = "InvariantFailed";
          break;
        case ContractFailureKind.Assert:
          str = "AssertionFailed";
          break;
        case ContractFailureKind.Assume:
          str = "AssumptionFailed";
          break;
        default:
          Contract.Assume(false, "Unreachable code");
          str = "AssumptionFailed";
          break;
      }
      return str;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
    {
      string resourceNameForFailure = ContractHelper.GetResourceNameForFailure(failureKind);
      string resourceString;
      if (!string.IsNullOrEmpty(conditionText))
        resourceString = Environment.GetResourceString(resourceNameForFailure + "_Cnd", (object) conditionText);
      else
        resourceString = Environment.GetResourceString(resourceNameForFailure);
      if (!string.IsNullOrEmpty(userMessage))
        return resourceString + "  " + userMessage;
      return resourceString;
    }

    [SecuritySafeCritical]
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
    {
      string exceptionAsString = (string) null;
      if (innerException != null)
        exceptionAsString = innerException.ToString();
      Environment.TriggerCodeContractFailure(failureKind, message, conditionText, exceptionAsString);
    }
  }
}
