// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStorageContainment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定独立存储区所允许的用途。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum IsolatedStorageContainment
  {
    None = 0,
    DomainIsolationByUser = 16,
    ApplicationIsolationByUser = 21,
    AssemblyIsolationByUser = 32,
    DomainIsolationByMachine = 48,
    AssemblyIsolationByMachine = 64,
    ApplicationIsolationByMachine = 69,
    DomainIsolationByRoamingUser = 80,
    AssemblyIsolationByRoamingUser = 96,
    ApplicationIsolationByRoamingUser = 101,
    AdministerIsolatedStorageByUser = 112,
    UnrestrictedIsolatedStorage = 240,
  }
}
