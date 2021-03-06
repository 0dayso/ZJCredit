﻿// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FlowControl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>描述指令如何改变控制流。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum FlowControl
  {
    [__DynamicallyInvokable] Branch,
    [__DynamicallyInvokable] Break,
    [__DynamicallyInvokable] Call,
    [__DynamicallyInvokable] Cond_Branch,
    [__DynamicallyInvokable] Meta,
    [__DynamicallyInvokable] Next,
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] Phi,
    [__DynamicallyInvokable] Return,
    [__DynamicallyInvokable] Throw,
  }
}
