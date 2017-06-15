// Decompiled with JetBrains decompiler
// Type: System.ConsoleModifiers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>表示键盘上的 Shift、Alt 和 Ctrl 修改键。</summary>
  /// <filterpriority>2</filterpriority>
  [Flags]
  [Serializable]
  public enum ConsoleModifiers
  {
    Alt = 1,
    Shift = 2,
    Control = 4,
  }
}
