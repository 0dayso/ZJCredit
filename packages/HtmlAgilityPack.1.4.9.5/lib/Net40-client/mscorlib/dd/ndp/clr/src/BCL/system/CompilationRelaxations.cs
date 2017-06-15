// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CompilationRelaxations
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指定一些参数，这些参数控制由公共语言运行时的实时 (JIT) 编译器生成的代码的严格性。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum CompilationRelaxations
  {
    NoStringInterning = 8,
  }
}
