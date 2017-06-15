// Decompiled with JetBrains decompiler
// Type: System.Runtime.ConstrainedExecution.Cer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ConstrainedExecution
{
  /// <summary>在受约束的执行区域内调用时指定方法的行为。</summary>
  [Serializable]
  public enum Cer
  {
    None,
    MayFail,
    Success,
  }
}
