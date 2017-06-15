// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.PropagationFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定如何将访问面控制项 (ACE) 传播到子对象。仅当存在继承标志时，这些标志才有意义。</summary>
  [Flags]
  public enum PropagationFlags
  {
    None = 0,
    NoPropagateInherit = 1,
    InheritOnly = 2,
  }
}
