// Decompiled with JetBrains decompiler
// Type: System.Resources.IResourceReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>提供从资源文件读取数据的基功能。</summary>
  [ComVisible(true)]
  public interface IResourceReader : IEnumerable, IDisposable
  {
    /// <summary>释放与资源阅读器关联的所有资源后将该阅读器关闭。</summary>
    void Close();

    /// <summary>返回此阅读器的资源的字典枚举数。</summary>
    /// <returns>此阅读器的资源的字典枚举数。</returns>
    IDictionaryEnumerator GetEnumerator();
  }
}
