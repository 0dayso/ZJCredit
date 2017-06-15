// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibExporterFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示应该如何生成类型库。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibExporterFlags
  {
    None = 0,
    OnlyReferenceRegistered = 1,
    CallerResolvedReferences = 2,
    OldNames = 4,
    ExportAs32Bit = 16,
    ExportAs64Bit = 32,
  }
}
