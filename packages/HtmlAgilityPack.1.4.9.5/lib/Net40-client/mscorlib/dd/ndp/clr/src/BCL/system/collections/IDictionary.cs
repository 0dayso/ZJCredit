// Decompiled with JetBrains decompiler
// Type: System.Collections.IDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>表示键/值对的非通用集合。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IDictionary : ICollection, IEnumerable
  {
    /// <summary>获取或设置具有指定键的元素。</summary>
    /// <returns>具有指定键的元素，如果该键不存在，则为 null。</returns>
    /// <param name="key">要获取或设置的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.IDictionary" /> 对象为只读。- 或 -设置该属性，集合中不存在 <paramref name="key" />，而且 <see cref="T:System.Collections.IDictionary" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object this[object key] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取 <see cref="T:System.Collections.ICollection" /> 对象，它包含 <see cref="T:System.Collections.IDictionary" /> 对象的键。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ICollection" /> 对象，它包含 <see cref="T:System.Collections.IDictionary" /> 对象的键。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    ICollection Keys { [__DynamicallyInvokable] get; }

    /// <summary>获取 <see cref="T:System.Collections.ICollection" /> 对象，它包含 <see cref="T:System.Collections.IDictionary" /> 对象中的值。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ICollection" /> 对象，它包含 <see cref="T:System.Collections.IDictionary" /> 对象中的值。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    ICollection Values { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.IDictionary" /> 对象是否为只读。</summary>
    /// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool IsReadOnly { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.IDictionary" /> 对象是否具有固定大小。</summary>
    /// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool IsFixedSize { [__DynamicallyInvokable] get; }

    /// <summary>确定 <see cref="T:System.Collections.IDictionary" /> 对象是否包含具有指定键的元素。</summary>
    /// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.IDictionary" /> 对象中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool Contains(object key);

    /// <summary>在 <see cref="T:System.Collections.IDictionary" /> 对象中添加一个带有所提供的键和值的元素。</summary>
    /// <param name="key">用作要添加的元素的键的 <see cref="T:System.Object" />。</param>
    /// <param name="value">用作要添加的元素的值的 <see cref="T:System.Object" />。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="T:System.Collections.IDictionary" /> 对象中已存在具有相同键的元素。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IDictionary" /> 为只读。- 或 -<see cref="T:System.Collections.IDictionary" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Add(object key, object value);

    /// <summary>从 <see cref="T:System.Collections.IDictionary" /> 对象中移除所有元素。</summary>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IDictionary" /> 对象是只读的。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Clear();

    /// <summary>返回一个用于 <see cref="T:System.Collections.IDictionary" /> 对象的 <see cref="T:System.Collections.IDictionaryEnumerator" /> 对象。</summary>
    /// <returns>一个用于 <see cref="T:System.Collections.IDictionary" /> 对象的 <see cref="T:System.Collections.IDictionaryEnumerator" /> 对象。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    IDictionaryEnumerator GetEnumerator();

    /// <summary>从 <see cref="T:System.Collections.IDictionary" /> 对象中移除带有指定键的元素。</summary>
    /// <param name="key">要移除的元素的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.IDictionary" /> 对象是只读的。- 或 -<see cref="T:System.Collections.IDictionary" /> 具有固定大小。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Remove(object key);
  }
}
