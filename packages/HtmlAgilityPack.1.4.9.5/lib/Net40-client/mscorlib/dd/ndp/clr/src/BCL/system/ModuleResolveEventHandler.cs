// Decompiled with JetBrains decompiler
// Type: System.Reflection.ModuleResolveEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>表示将要处理 <see cref="T:System.Reflection.Assembly" /> 的 <see cref="E:System.Reflection.Assembly.ModuleResolve" /> 事件的方法。</summary>
  /// <returns>满足请求的模块。</returns>
  /// <param name="sender">曾作为事件源的程序集。</param>
  /// <param name="e">由描述事件的对象提供的参数。</param>
  [ComVisible(true)]
  [Serializable]
  public delegate Module ModuleResolveEventHandler(object sender, ResolveEventArgs e);
}
