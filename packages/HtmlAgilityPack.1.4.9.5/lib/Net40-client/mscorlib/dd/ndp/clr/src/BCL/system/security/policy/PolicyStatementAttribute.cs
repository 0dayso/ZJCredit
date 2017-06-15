// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyStatementAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>为代码组上的安全策略定义特殊的特性标志。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum PolicyStatementAttribute
  {
    Nothing = 0,
    Exclusive = 1,
    LevelFinal = 2,
    All = LevelFinal | Exclusive,
  }
}
