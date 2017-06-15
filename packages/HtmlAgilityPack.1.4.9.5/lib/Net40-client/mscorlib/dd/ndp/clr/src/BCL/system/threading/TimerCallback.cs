// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>表示处理来自 <see cref="T:System.Threading.Timer" /> 的调用的方法。</summary>
  /// <param name="state">一个对象（包含与该委托所调用的方法相关的特定于应用程序的信息）或为 null。</param>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public delegate void TimerCallback(object state);
}
