// Decompiled with JetBrains decompiler
// Type: System.Threading.ContextCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>表示要在新上下文中调用的方法。</summary>
  /// <param name="state">一个对象，包含回调方法在每次执行时要使用的信息。</param>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public delegate void ContextCallback(object state);
}
