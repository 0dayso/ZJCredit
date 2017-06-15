// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ComponentGuaranteesOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>描述可以跨多个版本的组件、类型或类型成员的兼容性保证。</summary>
  [Flags]
  [Serializable]
  public enum ComponentGuaranteesOptions
  {
    None = 0,
    Exchange = 1,
    Stable = 2,
    SideBySide = 4,
  }
}
