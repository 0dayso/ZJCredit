// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibTypeFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>描述从中导入该类型的 COM 类型库中 <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> 的原始设置。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibTypeFlags
  {
    FAppObject = 1,
    FCanCreate = 2,
    FLicensed = 4,
    FPreDeclId = 8,
    FHidden = 16,
    FControl = 32,
    FDual = 64,
    FNonExtensible = 128,
    FOleAutomation = 256,
    FRestricted = 512,
    FAggregatable = 1024,
    FReplaceable = 2048,
    FDispatchable = 4096,
    FReverseBind = 8192,
  }
}
