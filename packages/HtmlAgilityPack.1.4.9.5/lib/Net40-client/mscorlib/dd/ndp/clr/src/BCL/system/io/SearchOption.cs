// Decompiled with JetBrains decompiler
// Type: System.IO.SearchOption
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>指定是搜索当前目录，还是搜索当前目录及其所有子目录。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum SearchOption
  {
    TopDirectoryOnly,
    AllDirectories,
  }
}
