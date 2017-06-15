// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>表示键/值对的泛型集合。</summary>
  /// <typeparam name="TKey">字典中键的类型。</typeparam>
  /// <typeparam name="TValue">字典中值的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
  {
    /// <summary>获取或设置具有指定键的元素。</summary>
    /// <returns>带有指定键的元素。</returns>
    /// <param name="key">要获取或设置的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">检索了属性但没有找到 <paramref name="key" />。</exception>
    /// <exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.Generic.IDictionary`2" /> 为只读。</exception>
    [__DynamicallyInvokable]
    TValue this[TKey key] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>获取包含 <see cref="T:System.Collections.Generic.IDictionary`2" /> 的键的 <see cref="T:System.Collections.Generic.ICollection`1" />。</summary>
    /// <returns>一个 <see cref="T:System.Collections.Generic.ICollection`1" />，它包含实现 <see cref="T:System.Collections.Generic.IDictionary`2" /> 的对象的键。</returns>
    [__DynamicallyInvokable]
    ICollection<TKey> Keys { [__DynamicallyInvokable] get; }

    /// <summary>获取包含 <see cref="T:System.Collections.Generic.IDictionary`2" /> 中的值的 <see cref="T:System.Collections.Generic.ICollection`1" />。</summary>
    /// <returns>一个 <see cref="T:System.Collections.Generic.ICollection`1" />，它包含实现 <see cref="T:System.Collections.Generic.IDictionary`2" /> 的对象中的值。</returns>
    [__DynamicallyInvokable]
    ICollection<TValue> Values { [__DynamicallyInvokable] get; }

    /// <summary>确定 <see cref="T:System.Collections.Generic.IDictionary`2" /> 是否包含具有指定键的元素。</summary>
    /// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.Generic.IDictionary`2" /> 中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    bool ContainsKey(TKey key);

    /// <summary>在 <see cref="T:System.Collections.Generic.IDictionary`2" /> 中添加一个带有所提供的键和值的元素。</summary>
    /// <param name="key">用作要添加的元素的键的对象。</param>
    /// <param name="value">用作要添加的元素的值的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="T:System.Collections.Generic.IDictionary`2" /> 中已存在具有相同键的元素。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.IDictionary`2" /> 为只读。</exception>
    [__DynamicallyInvokable]
    void Add(TKey key, TValue value);

    /// <summary>从 <see cref="T:System.Collections.Generic.IDictionary`2" /> 中移除带有指定键的元素。</summary>
    /// <returns>如果该元素已成功移除，则为 true；否则为 false。This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
    /// <param name="key">要移除的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <see cref="T:System.Collections.Generic.IDictionary`2" /> 为只读。</exception>
    [__DynamicallyInvokable]
    bool Remove(TKey key);

    /// <summary>获取与指定键关联的值。</summary>
    /// <returns>如果实现 <see cref="T:System.Collections.Generic.IDictionary`2" /> 的对象包含具有指定键的元素，则为 true；否则，为 false。</returns>
    /// <param name="key">要获取其值的键。</param>
    /// <param name="value">当此方法返回时，如果找到指定键，则返回与该键相关联的值；否则，将返回 <paramref name="value" /> 参数的类型的默认值。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    bool TryGetValue(TKey key, out TValue value);
  }
}
