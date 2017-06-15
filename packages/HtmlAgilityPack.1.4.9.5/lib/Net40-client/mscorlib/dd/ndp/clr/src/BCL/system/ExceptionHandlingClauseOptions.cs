// Decompiled with JetBrains decompiler
// Type: System.Reflection.ExceptionHandlingClauseOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>标识异常处理子句的种类。</summary>
  [Flags]
  [ComVisible(true)]
  public enum ExceptionHandlingClauseOptions
  {
    Clause = 0,
    Filter = 1,
    Finally = 2,
    Fault = 4,
  }
}
