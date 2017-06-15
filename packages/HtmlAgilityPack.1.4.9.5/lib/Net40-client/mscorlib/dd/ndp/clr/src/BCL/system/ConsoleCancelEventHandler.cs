// Decompiled with JetBrains decompiler
// Type: System.ConsoleCancelEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>表示将要处理 <see cref="T:System.Console" /> 的 <see cref="E:System.Console.CancelKeyPress" /> 事件的方法。</summary>
  /// <param name="sender">事件源。</param>
  /// <param name="e">含事件数据的 <see cref="T:System.ConsoleCancelEventArgs" /> 对象。 </param>
  /// <filterpriority>2</filterpriority>
  public delegate void ConsoleCancelEventHandler(object sender, ConsoleCancelEventArgs e);
}
