// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CharSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>规定封送字符串应使用何种字符集。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CharSet
  {
    None = 1,
    [__DynamicallyInvokable] Ansi = 2,
    [__DynamicallyInvokable] Unicode = 3,
    Auto = 4,
  }
}
