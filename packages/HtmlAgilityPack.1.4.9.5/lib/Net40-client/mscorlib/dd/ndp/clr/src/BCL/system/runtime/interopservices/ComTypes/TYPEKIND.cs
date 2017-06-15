// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.TYPEKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>指定各种类型的数据和函数。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum TYPEKIND
  {
    [__DynamicallyInvokable] TKIND_ENUM,
    [__DynamicallyInvokable] TKIND_RECORD,
    [__DynamicallyInvokable] TKIND_MODULE,
    [__DynamicallyInvokable] TKIND_INTERFACE,
    [__DynamicallyInvokable] TKIND_DISPATCH,
    [__DynamicallyInvokable] TKIND_COCLASS,
    [__DynamicallyInvokable] TKIND_ALIAS,
    [__DynamicallyInvokable] TKIND_UNION,
    [__DynamicallyInvokable] TKIND_MAX,
  }
}
