// Decompiled with JetBrains decompiler
// Type: System.EventHandler`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>表示将在事件提供数据时处理该事件的方法。</summary>
  /// <param name="sender">事件源。</param>
  /// <param name="e">一个包含事件数据的对象。</param>
  /// <typeparam name="TEventArgs">由该事件生成的事件数据的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  [Serializable]
  public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
}
