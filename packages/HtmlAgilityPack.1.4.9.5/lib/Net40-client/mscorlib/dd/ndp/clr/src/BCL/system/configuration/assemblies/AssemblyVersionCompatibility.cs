// Decompiled with JetBrains decompiler
// Type: System.Configuration.Assemblies.AssemblyVersionCompatibility
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
  /// <summary>定义不同类型程序集版本的兼容性。.NET Framework 1.0 版中没有提供这项功能。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum AssemblyVersionCompatibility
  {
    SameMachine = 1,
    SameProcess = 2,
    SameDomain = 3,
  }
}
