// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryValueOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;

namespace Microsoft.Win32
{
  /// <summary>指定从注册表项检索名称/值对时的可选行为。</summary>
  [Flags]
  public enum RegistryValueOptions
  {
    None = 0,
    DoNotExpandEnvironmentNames = 1,
  }
}
