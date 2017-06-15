// Decompiled with JetBrains decompiler
// Type: System.Threading.ParameterizedThreadStart
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>表示在 <see cref="T:System.Threading.Thread" /> 上执行的方法。</summary>
  /// <param name="obj">包含该线程过程的数据的对象。</param>
  /// <filterpriority>1</filterpriority>
  [ComVisible(false)]
  public delegate void ParameterizedThreadStart(object obj);
}
