// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;

namespace Microsoft.Win32
{
  /// <summary>指定在创建注册表项时使用的选项。</summary>
  [Flags]
  public enum RegistryOptions
  {
    None = 0,
    Volatile = 1,
  }
}
