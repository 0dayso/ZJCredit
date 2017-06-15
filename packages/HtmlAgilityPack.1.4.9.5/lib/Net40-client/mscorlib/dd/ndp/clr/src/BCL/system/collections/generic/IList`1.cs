// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IList`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>表示可按照索引单独访问的一组对象。</summary>
  /// <typeparam name="T">列表中元素的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
  {
    /// <summary>获取或设置位于指定索引处的元素。</summary>
    /// <returns>位于指定索引处的元素。</returns>
    /// <param name="index">要获得或设置的元素从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <see cref="T:System.Collections.Generic.IList`1" /> 中的有效索引。</exception>
    /// <exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.Generic.IList`1" /> 为只读。</exception>
    [__DynamicallyInvokable]
    T this[int index] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>确定 <see cref="T:System.Collections.Generic.IList`1" /> 中特定项的索引。</summary>
    /// <returns>如果在列表中找到，则为 <paramref name="item" /> 的索引；否则为 -1。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.IList`1" /> 中定位的对象。</param>
    [__DynamicallyInvokable]
    int IndexOf(T item);

    /// <summary>将一个项插入指定索引处的 <see cref="T:System.Collections.Generic.IList`1" />。</summary>
    /// <param name="index">从零开始的索引，应在该位置插入 <paramref name="item" />。</param>
    /// <param name="item">要插入到 <see cref="T:System.Collections.Generic.IList`1" /> 中的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <see cref="T:System.Collections.Generic.IList`1" /> 中的有效索引。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.IList`1" /> 为只读。</exception>
    [__DynamicallyInvokable]
    void Insert(int index, T item);

    /// <summary>移除指定索引处的 <see cref="T:System.Collections.Generic.IList`1" /> 项。</summary>
    /// <param name="index">要移除的项的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <see cref="T:System.Collections.Generic.IList`1" /> 中的有效索引。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.IList`1" /> 为只读。</exception>
    [__DynamicallyInvokable]
    void RemoveAt(int index);
  }
}
