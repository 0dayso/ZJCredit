// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ICollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>定义操作泛型集合的方法。</summary>
  /// <typeparam name="T">集合中元素的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface ICollection<T> : IEnumerable<T>, IEnumerable
  {
    /// <summary>获取 <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素个数。</returns>
    [__DynamicallyInvokable]
    int Count { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否为只读。</summary>
    /// <returns>如果 <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    bool IsReadOnly { [__DynamicallyInvokable] get; }

    /// <summary>将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 中。</summary>
    /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 的对象。</param>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读。</exception>
    [__DynamicallyInvokable]
    void Add(T item);

    /// <summary>从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除所有项。</summary>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读。</exception>
    [__DynamicallyInvokable]
    void Clear();

    /// <summary>确定 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否包含特定值。</summary>
    /// <returns>如果在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中找到 <paramref name="item" />，则为 true；否则为 false。</returns>
    /// <param name="item">要在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中定位的对象。</param>
    [__DynamicallyInvokable]
    bool Contains(T item);

    /// <summary>从特定的 <see cref="T:System.Array" /> 索引开始，将 <see cref="T:System.Collections.Generic.ICollection`1" /> 的元素复制到一个 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">作为从 <see cref="T:System.Collections.Generic.ICollection`1" /> 复制的元素的目标的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex" /> 小于 0。</exception>
    /// <exception cref="T:System.ArgumentException">源 <see cref="T:System.Collections.Generic.ICollection`1" /> 中的元素数目大于从 <paramref name="arrayIndex" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    [__DynamicallyInvokable]
    void CopyTo(T[] array, int arrayIndex);

    /// <summary>从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除特定对象的第一个匹配项。</summary>
    /// <returns>如果已从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中成功移除 <paramref name="item" />，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1" /> 中没有找到 <paramref name="item" />，该方法也会返回 false。</returns>
    /// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除的对象。</param>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读。</exception>
    [__DynamicallyInvokable]
    bool Remove(T item);
  }
}
