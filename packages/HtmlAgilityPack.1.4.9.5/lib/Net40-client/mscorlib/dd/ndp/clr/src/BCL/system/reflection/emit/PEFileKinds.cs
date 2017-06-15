// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PEFileKinds
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>指定可移植可执行 (PE) 文件的类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum PEFileKinds
  {
    Dll = 1,
    ConsoleApplication = 2,
    WindowApplication = 3,
  }
}
