// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>筛选在 <see cref="T:System.Type" /> 对象数组中表示的类。</summary>
  /// <returns>如果包括已筛选列表中的 <see cref="T:System.Type" />，则为 true；否则为 false。</returns>
  /// <param name="m">筛选器应用到的 Type 对象。</param>
  /// <param name="filterCriteria">用于筛选列表的任意对象。</param>
  [ComVisible(true)]
  [Serializable]
  public delegate bool TypeFilter(Type m, object filterCriteria);
}
