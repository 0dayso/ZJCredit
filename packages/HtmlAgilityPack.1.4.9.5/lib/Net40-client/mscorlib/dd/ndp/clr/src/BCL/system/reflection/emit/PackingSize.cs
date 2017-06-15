// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PackingSize
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>指定在封送类型时用于确定字段的内存对齐方式的两个因数中的一个。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum PackingSize
  {
    [__DynamicallyInvokable] Unspecified = 0,
    [__DynamicallyInvokable] Size1 = 1,
    [__DynamicallyInvokable] Size2 = 2,
    [__DynamicallyInvokable] Size4 = 4,
    [__DynamicallyInvokable] Size8 = 8,
    [__DynamicallyInvokable] Size16 = 16,
    [__DynamicallyInvokable] Size32 = 32,
    [__DynamicallyInvokable] Size64 = 64,
    [__DynamicallyInvokable] Size128 = 128,
  }
}
