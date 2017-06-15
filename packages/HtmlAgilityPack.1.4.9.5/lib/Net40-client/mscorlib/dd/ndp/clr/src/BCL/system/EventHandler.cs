// Decompiled with JetBrains decompiler
// Type: System.EventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示将处理不包含事件数据的事件的方法。</summary>
  /// <param name="sender">事件源。</param>
  /// <param name="e">不包含事件数据的对象。</param>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public delegate void EventHandler(object sender, EventArgs e);
}
