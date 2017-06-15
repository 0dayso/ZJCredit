// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.TrustManagerUIContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>指定信任关系管理器应该用于作出信任决定的用户界面 (UI) 类型。</summary>
  [ComVisible(true)]
  public enum TrustManagerUIContext
  {
    Install,
    Upgrade,
    Run,
  }
}
