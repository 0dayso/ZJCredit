﻿// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractFailureKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>指定失败的协定的类型。</summary>
  [__DynamicallyInvokable]
  public enum ContractFailureKind
  {
    [__DynamicallyInvokable] Precondition,
    [__DynamicallyInvokable] Postcondition,
    [__DynamicallyInvokable] PostconditionOnException,
    [__DynamicallyInvokable] Invariant,
    [__DynamicallyInvokable] Assert,
    [__DynamicallyInvokable] Assume,
  }
}
