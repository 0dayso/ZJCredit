// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ResourceScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>标识可共享资源的范围。</summary>
  [Flags]
  public enum ResourceScope
  {
    None = 0,
    Machine = 1,
    Process = 2,
    AppDomain = 4,
    Library = 8,
    Private = 16,
    Assembly = 32,
  }
}
