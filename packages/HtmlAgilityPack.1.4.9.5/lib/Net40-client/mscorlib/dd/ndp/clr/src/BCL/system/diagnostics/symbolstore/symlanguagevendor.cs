// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymLanguageVendor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>保存要与符号存储区一起使用的语言供应商的公用 GUID。</summary>
  [ComVisible(true)]
  public class SymLanguageVendor
  {
    /// <summary>指定 Microsoft 语言供应商的 GUID。</summary>
    public static readonly Guid Microsoft = new Guid(-1723120188, (short) -6423, (short) 4562, (byte) 144, (byte) 63, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
  }
}
