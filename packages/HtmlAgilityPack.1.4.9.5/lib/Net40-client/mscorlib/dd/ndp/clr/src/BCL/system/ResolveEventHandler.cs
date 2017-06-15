// Decompiled with JetBrains decompiler
// Type: System.ResolveEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示处理 <see cref="T:System.AppDomain" /> 的 <see cref="E:System.AppDomain.TypeResolve" />、<see cref="E:System.AppDomain.ResourceResolve" /> 或 <see cref="E:System.AppDomain.AssemblyResolve" /> 事件的方法。</summary>
  /// <returns>解析类型、程序集或资源的程序集；如果无法解析程序集，则为 null。</returns>
  /// <param name="sender">事件源。</param>
  /// <param name="args">事件数据。</param>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public delegate Assembly ResolveEventHandler(object sender, ResolveEventArgs args);
}
