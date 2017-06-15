// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>标记每个已定义为 MemberInfo 的派生类的成员类型。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum MemberTypes
  {
    Constructor = 1,
    Event = 2,
    Field = 4,
    Method = 8,
    Property = 16,
    TypeInfo = 32,
    Custom = 64,
    NestedType = 128,
    All = NestedType | TypeInfo | Property | Method | Field | Event | Constructor,
  }
}
