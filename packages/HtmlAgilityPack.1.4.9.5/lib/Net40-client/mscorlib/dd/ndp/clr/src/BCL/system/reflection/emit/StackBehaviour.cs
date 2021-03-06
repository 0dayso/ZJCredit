﻿// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.StackBehaviour
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>描述如何将值推到堆栈上或从堆栈中弹出。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum StackBehaviour
  {
    [__DynamicallyInvokable] Pop0,
    [__DynamicallyInvokable] Pop1,
    [__DynamicallyInvokable] Pop1_pop1,
    [__DynamicallyInvokable] Popi,
    [__DynamicallyInvokable] Popi_pop1,
    [__DynamicallyInvokable] Popi_popi,
    [__DynamicallyInvokable] Popi_popi8,
    [__DynamicallyInvokable] Popi_popi_popi,
    [__DynamicallyInvokable] Popi_popr4,
    [__DynamicallyInvokable] Popi_popr8,
    [__DynamicallyInvokable] Popref,
    [__DynamicallyInvokable] Popref_pop1,
    [__DynamicallyInvokable] Popref_popi,
    [__DynamicallyInvokable] Popref_popi_popi,
    [__DynamicallyInvokable] Popref_popi_popi8,
    [__DynamicallyInvokable] Popref_popi_popr4,
    [__DynamicallyInvokable] Popref_popi_popr8,
    [__DynamicallyInvokable] Popref_popi_popref,
    [__DynamicallyInvokable] Push0,
    [__DynamicallyInvokable] Push1,
    [__DynamicallyInvokable] Push1_push1,
    [__DynamicallyInvokable] Pushi,
    [__DynamicallyInvokable] Pushi8,
    [__DynamicallyInvokable] Pushr4,
    [__DynamicallyInvokable] Pushr8,
    [__DynamicallyInvokable] Pushref,
    [__DynamicallyInvokable] Varpop,
    [__DynamicallyInvokable] Varpush,
    [__DynamicallyInvokable] Popref_popi_pop1,
  }
}
