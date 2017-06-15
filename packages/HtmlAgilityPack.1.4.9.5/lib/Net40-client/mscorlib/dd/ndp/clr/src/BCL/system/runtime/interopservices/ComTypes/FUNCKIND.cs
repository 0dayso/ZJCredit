// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.FUNCKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>定义如何访问函数。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum FUNCKIND
  {
    [__DynamicallyInvokable] FUNC_VIRTUAL,
    [__DynamicallyInvokable] FUNC_PUREVIRTUAL,
    [__DynamicallyInvokable] FUNC_NONVIRTUAL,
    [__DynamicallyInvokable] FUNC_STATIC,
    [__DynamicallyInvokable] FUNC_DISPATCH,
  }
}
