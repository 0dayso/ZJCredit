// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>表示要由线程池线程执行的回调方法。</summary>
  /// <param name="state">包含回调方法要使用的信息的对象。</param>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public delegate void WaitCallback(object state);
}
