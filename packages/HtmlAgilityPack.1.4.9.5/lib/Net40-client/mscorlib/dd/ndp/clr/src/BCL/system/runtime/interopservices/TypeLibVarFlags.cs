// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibVarFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>描述从中导入此变量的 COM 类型库中 <see cref="T:System.Runtime.InteropServices.VARFLAGS" /> 的原始设置。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibVarFlags
  {
    FReadOnly = 1,
    FSource = 2,
    FBindable = 4,
    FRequestEdit = 8,
    FDisplayBind = 16,
    FDefaultBind = 32,
    FHidden = 64,
    FRestricted = 128,
    FDefaultCollelem = 256,
    FUiDefault = 512,
    FNonBrowsable = 1024,
    FReplaceable = 2048,
    FImmediateBind = 4096,
  }
}
