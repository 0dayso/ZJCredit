// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IReadOnlyDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>表示键/值对的泛型只读集合。</summary>
  /// <typeparam name="TKey">只读字典中的键的类型。</typeparam>
  /// <typeparam name="TValue">只读字典中的值的类型。</typeparam>
  [__DynamicallyInvokable]
  public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
  {
    /// <summary>获取在只读目录中有指定键的元素。</summary>
    /// <returns>在只读目录中有指定键的元素。</returns>
    /// <param name="key">要定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">检索了属性但没有找到 <paramref name="key" />。</exception>
    [__DynamicallyInvokable]
    TValue this[TKey key] { [__DynamicallyInvokable] get; }

    /// <summary>获取包含只读字典中的密钥的可枚举集合。</summary>
    /// <returns>包含只读字典中的密钥的可枚举集合。</returns>
    [__DynamicallyInvokable]
    IEnumerable<TKey> Keys { [__DynamicallyInvokable] get; }

    /// <summary>获取包含只读字典中的值的可枚举集合。</summary>
    /// <returns>包含只读字典中的值的可枚举集合。</returns>
    [__DynamicallyInvokable]
    IEnumerable<TValue> Values { [__DynamicallyInvokable] get; }

    /// <summary>确定只读字典是否包含具有指定键的元素。</summary>
    /// <returns>如果该只读词典包含一具有指定键的元素，则为 true；否则为 false。</returns>
    /// <param name="key">要定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    bool ContainsKey(TKey key);

    /// <summary>获取与指定的键关联的值。</summary>
    /// <returns>如果实现 <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2" /> 接口的对象包含具有指定键的元素，则为 true；否则为 false。</returns>
    /// <param name="key">要定位的键。</param>
    /// <param name="value">当此方法返回时，如果找到指定键，则返回与该键相关联的值；否则，将返回 <paramref name="value" /> 参数的类型的默认值。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [__DynamicallyInvokable]
    bool TryGetValue(TKey key, out TValue value);
  }
}
