// Decompiled with JetBrains decompiler
// Type: System.Collections.IComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>公开一种比较两个对象的方法。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IComparer
  {
    /// <summary>比较两个对象并返回一个值，该值指示一个对象小于、等于还是大于另一个对象。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="x" /> 与 <paramref name="y" /> 的相对值，如下表所示。值含义小于零<paramref name="x" /> 小于 <paramref name="y" />。零<paramref name="x" /> 等于 <paramref name="y" />。大于零<paramref name="x" /> 大于 <paramref name="y" />。</returns>
    /// <param name="x">要比较的第一个对象。</param>
    /// <param name="y">要比较的第二个对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="x" /> 和 <paramref name="y" /> 都不实现 <see cref="T:System.IComparable" /> 接口。- 或 -<paramref name="x" /> 和 <paramref name="y" /> 的类型不同，它们都无法处理与另一个进行的比较。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    int Compare(object x, object y);
  }
}
