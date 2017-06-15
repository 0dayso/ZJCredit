// Decompiled with JetBrains decompiler
// Type: System.Threading.EventResetMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>指示在接收信号后是自动重置 <see cref="T:System.Threading.EventWaitHandle" /> 还是手动重置。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public enum EventResetMode
  {
    [__DynamicallyInvokable] AutoReset,
    [__DynamicallyInvokable] ManualReset,
  }
}
