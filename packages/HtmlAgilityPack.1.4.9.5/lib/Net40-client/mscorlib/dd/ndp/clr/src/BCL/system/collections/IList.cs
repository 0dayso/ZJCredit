// Decompiled with JetBrains decompiler
// Type: System.Collections.IList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>表示可按照索引单独访问的对象的非泛型集合。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IList : ICollection, IEnumerable
  {
    /// <summary>获取或设置位于指定索引处的元素。</summary>
    /// <returns>位于指定索引处的元素。</returns>
    /// <param name="index">要获得或设置的元素从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <see cref="T:System.Collections.IList" /> 中的有效索引。</exception>
    /// <exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.IList" /> 为只读。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object this[int index] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.IList" /> 是否为只读。</summary>
    /// <returns>如果 <see cref="T:System.Collections.IList" /> 为只读，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool IsReadOnly { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.IList" /> 是否具有固定大小。</summary>
    /// <returns>如果 <see cref="T:System.Collections.IList" /> 具有固定大小，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool IsFixedSize { [__DynamicallyInvokable] get; }

    /// <summary>将某项添加到 <see cref="T:System.Collections.IList" /> 中。</summary>
    /// <returns>新元素所插入到的位置，或为 -1 以指示未将该项插入到集合中。</returns>
    /// <param name="value">要添加到 <see cref="T:System.Collections.IList" /> 的对象。</param>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IList" /> 为只读。- 或 -<see cref="T:System.Collections.IList" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    int Add(object value);

    /// <summary>确定 <see cref="T:System.Collections.IList" /> 是否包含特定值。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.IList" /> 中找到 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.IList" /> 中定位的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool Contains(object value);

    /// <summary>从 <see cref="T:System.Collections.IList" /> 中移除所有项。</summary>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IList" /> 为只读。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Clear();

    /// <summary>确定 <see cref="T:System.Collections.IList" /> 中特定项的索引。</summary>
    /// <returns>如果在列表中找到，则为 <paramref name="value" /> 的索引；否则为 -1。</returns>
    /// <param name="value">要在 <see cref="T:System.Collections.IList" /> 中定位的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    int IndexOf(object value);

    /// <summary>将一个项插入指定索引处的 <see cref="T:System.Collections.IList" />。</summary>
    /// <param name="index">从零开始的索引，应在该位置插入 <paramref name="value" />。</param>
    /// <param name="value">要插入 <see cref="T:System.Collections.IList" /> 的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <see cref="T:System.Collections.IList" /> 中的有效索引。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IList" /> 为只读。- 或 -<see cref="T:System.Collections.IList" /> 具有固定大小。</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="value" /> 在 <see cref="T:System.Collections.IList" /> 中是 null 引用。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Insert(int index, object value);

    /// <summary>从 <see cref="T:System.Collections.IList" /> 中移除特定对象的第一个匹配项。</summary>
    /// <param name="value">要从 <see cref="T:System.Collections.IList" /> 中移除的对象。</param>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IList" /> 为只读。- 或 -<see cref="T:System.Collections.IList" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Remove(object value);

    /// <summary>移除指定索引处的 <see cref="T:System.Collections.IList" /> 项。</summary>
    /// <param name="index">要移除的项的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 不是 <see cref="T:System.Collections.IList" /> 中的有效索引。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IList" /> 为只读。- 或 -<see cref="T:System.Collections.IList" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void RemoveAt(int index);
  }
}
