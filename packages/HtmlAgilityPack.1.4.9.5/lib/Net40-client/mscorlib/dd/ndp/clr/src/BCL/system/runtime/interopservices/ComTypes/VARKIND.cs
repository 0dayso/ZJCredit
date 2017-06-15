// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.VARKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>定义变量的种类。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum VARKIND
  {
    [__DynamicallyInvokable] VAR_PERINSTANCE,
    [__DynamicallyInvokable] VAR_STATIC,
    [__DynamicallyInvokable] VAR_CONST,
    [__DynamicallyInvokable] VAR_DISPATCH,
  }
}
