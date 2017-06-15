// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibFuncFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>描述从中导入该方法的 COM 类型库中 FUNCFLAGS 的原始设置。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibFuncFlags
  {
    FRestricted = 1,
    FSource = 2,
    FBindable = 4,
    FRequestEdit = 8,
    FDisplayBind = 16,
    FDefaultBind = 32,
    FHidden = 64,
    FUsesGetLastError = 128,
    FDefaultCollelem = 256,
    FUiDefault = 512,
    FNonBrowsable = 1024,
    FReplaceable = 2048,
    FImmediateBind = 4096,
  }
}
