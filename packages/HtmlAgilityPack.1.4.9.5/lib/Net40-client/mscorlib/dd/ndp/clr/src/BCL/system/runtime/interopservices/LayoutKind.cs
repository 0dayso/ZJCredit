// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.LayoutKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>控制当导出到非托管代码时对象的布局。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum LayoutKind
  {
    [__DynamicallyInvokable] Sequential = 0,
    [__DynamicallyInvokable] Explicit = 2,
    [__DynamicallyInvokable] Auto = 3,
  }
}
