// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryView
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace Microsoft.Win32
{
  /// <summary>指定要在 64 位操作系统上针对的注册表视图。</summary>
  public enum RegistryView
  {
    Default = 0,
    Registry64 = 256,
    Registry32 = 512,
  }
}
