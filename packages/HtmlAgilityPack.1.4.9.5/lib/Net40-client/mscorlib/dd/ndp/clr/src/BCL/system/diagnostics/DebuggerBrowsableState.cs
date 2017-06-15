// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerBrowsableState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指定调试器的显示方式。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public enum DebuggerBrowsableState
  {
    [__DynamicallyInvokable] Never = 0,
    [__DynamicallyInvokable] Collapsed = 2,
    [__DynamicallyInvokable] RootHidden = 3,
  }
}
