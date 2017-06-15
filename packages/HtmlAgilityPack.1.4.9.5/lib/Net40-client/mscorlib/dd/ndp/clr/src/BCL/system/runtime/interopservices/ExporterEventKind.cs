// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ExporterEventKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>描述类型库导出程序在导出类型库时生成的回调。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum ExporterEventKind
  {
    NOTIF_TYPECONVERTED,
    NOTIF_CONVERTWARNING,
    ERROR_REFTOINVALIDASSEMBLY,
  }
}
