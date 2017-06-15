// Decompiled with JetBrains decompiler
// Type: System.Collections.IEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>支持对非泛型集合的简单迭代。</summary>
  /// <filterpriority>1</filterpriority>
  [Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IEnumerator
  {
    /// <summary>获取集合中的当前元素。</summary>
    /// <returns>集合中的当前元素。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object Current { [__DynamicallyInvokable] get; }

    /// <summary>将枚举数推进到集合的下一个元素。</summary>
    /// <returns>如果枚举数已成功地推进到下一个元素，则为 true；如果枚举数传递到集合的末尾，则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool MoveNext();

    /// <summary>将枚举数设置为其初始位置，该位置位于集合中第一个元素之前。</summary>
    /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Reset();
  }
}
