// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.AssemblyBuilderAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>定义动态程序集的访问模式。</summary>
  [ComVisible(true)]
  [Flags]
  [Serializable]
  public enum AssemblyBuilderAccess
  {
    Run = 1,
    Save = 2,
    RunAndSave = Save | Run,
    ReflectionOnly = 6,
    RunAndCollect = 9,
  }
}
