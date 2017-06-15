// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractFailedEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
  /// <summary>为 <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> 事件提供方法和数据。</summary>
  [__DynamicallyInvokable]
  public sealed class ContractFailedEventArgs : EventArgs
  {
    private ContractFailureKind _failureKind;
    private string _message;
    private string _condition;
    private Exception _originalException;
    private bool _handled;
    private bool _unwind;
    internal Exception thrownDuringHandler;

    /// <summary>获取描述 <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> 事件的消息。</summary>
    /// <returns>描述事件的消息。</returns>
    [__DynamicallyInvokable]
    public string Message
    {
      [__DynamicallyInvokable] get
      {
        return this._message;
      }
    }

    /// <summary>获取协定失败的条件。</summary>
    /// <returns>失败的条件。</returns>
    [__DynamicallyInvokable]
    public string Condition
    {
      [__DynamicallyInvokable] get
      {
        return this._condition;
      }
    }

    /// <summary>获取失败的协定的类型。</summary>
    /// <returns>用于指定失败的协定的类型的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public ContractFailureKind FailureKind
    {
      [__DynamicallyInvokable] get
      {
        return this._failureKind;
      }
    }

    /// <summary>获取导致 <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> 事件的原始异常。</summary>
    /// <returns>导致事件的异常。</returns>
    [__DynamicallyInvokable]
    public Exception OriginalException
    {
      [__DynamicallyInvokable] get
      {
        return this._originalException;
      }
    }

    /// <summary>指示是否已处理 <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> 事件。</summary>
    /// <returns>如果事件已被处理，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool Handled
    {
      [__DynamicallyInvokable] get
      {
        return this._handled;
      }
    }

    /// <summary>指示是否应该应用代码协定升级策略。</summary>
    /// <returns>若为 true，则应用升级策略；否则为 false。默认值为 false。</returns>
    [__DynamicallyInvokable]
    public bool Unwind
    {
      [__DynamicallyInvokable] get
      {
        return this._unwind;
      }
    }

    /// <summary>为 <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> 事件提供数据。</summary>
    /// <param name="failureKind">用于指定失败的协定的枚举值之一。</param>
    /// <param name="message">事件的消息。</param>
    /// <param name="condition">事件的条件。</param>
    /// <param name="originalException">导致事件的异常。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
    {
      this._failureKind = failureKind;
      this._message = message;
      this._condition = condition;
      this._originalException = originalException;
    }

    /// <summary>将 <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Handled" /> 属性设置为 true。</summary>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void SetHandled()
    {
      this._handled = true;
    }

    /// <summary>将 <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Unwind" /> 属性设置为 true。</summary>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void SetUnwind()
    {
      this._unwind = true;
    }
  }
}
