// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolBinder1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示托管代码的符号联编程序。</summary>
  [ComVisible(true)]
  public interface ISymbolBinder1
  {
    /// <summary>获取当前文件的符号读取器的接口。</summary>
    /// <returns>
    /// <see cref="T:System.Diagnostics.SymbolStore.ISymbolReader" /> 接口，它读取调试符号。</returns>
    /// <param name="importer">引用元数据导入接口的 <see cref="T:System.IntPtr" />。</param>
    /// <param name="filename">需要读取器接口的文件名称。</param>
    /// <param name="searchPath">用于查找符号文件的搜索路径。</param>
    ISymbolReader GetReader(IntPtr importer, string filename, string searchPath);
  }
}
