// Decompiled with JetBrains decompiler
// Type: System.UnhandledExceptionEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示将处理事件的方法，该事件由应用程序域不处理的异常引发。</summary>
  /// <param name="sender">未处理的异常事件的源。</param>
  /// <param name="e">包含事件数据的 <paramref name="UnhandledExceptionEventArgs" />。 </param>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public delegate void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
}
