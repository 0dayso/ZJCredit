// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryHive
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  /// <summary>表示外部计算机上的顶级节点的可能值。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum RegistryHive
  {
    ClassesRoot = -2147483648,
    CurrentUser = -2147483647,
    LocalMachine = -2147483646,
    Users = -2147483645,
    PerformanceData = -2147483644,
    CurrentConfig = -2147483643,
    DynData = -2147483642,
  }
}
