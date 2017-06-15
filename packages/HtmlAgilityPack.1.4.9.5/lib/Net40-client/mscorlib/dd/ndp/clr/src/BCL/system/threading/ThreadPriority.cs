// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPriority
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>指定 <see cref="T:System.Threading.Thread" /> 的调度优先级。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public enum ThreadPriority
  {
    Lowest,
    BelowNormal,
    Normal,
    AboveNormal,
    Highest,
  }
}
