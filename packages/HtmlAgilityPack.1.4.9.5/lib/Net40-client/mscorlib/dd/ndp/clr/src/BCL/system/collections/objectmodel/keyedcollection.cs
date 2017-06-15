// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.KeyedCollection`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
  /// <summary>提供键嵌入在值中的集合的抽象基类。</summary>
  /// <typeparam name="TKey">集合中的键的类型。</typeparam>
  /// <typeparam name="TItem">集合中的项的类型。</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_KeyedCollectionDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
  {
    private const int defaultThreshold = 0;
    private IEqualityComparer<TKey> comparer;
    private System.Collections.Generic.Dictionary<TKey, TItem> dict;
    private int keyCount;
    private int threshold;

    /// <summary>获取用于确定集合中的键是否相等的泛型相等比较器。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 泛型接口的实现，用于确定集合中的键是否相等。</returns>
    [__DynamicallyInvokable]
    public IEqualityComparer<TKey> Comparer
    {
      [__DynamicallyInvokable] get
      {
        return this.comparer;
      }
    }

    /// <summary>获取具有指定键的元素。</summary>
    /// <returns>带有指定键的元素。如果未找到具有指定键的元素，则引发异常。</returns>
    /// <param name="key">要获取的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">An element with the specified key does not exist in the collection.</exception>
    [__DynamicallyInvokable]
    public TItem this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        if ((object) key == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
        if (this.dict != null)
          return this.dict[key];
        foreach (TItem obj in (IEnumerable<TItem>) this.Items)
        {
          if (this.comparer.Equals(this.GetKeyForItem(obj), key))
            return obj;
        }
        ThrowHelper.ThrowKeyNotFoundException();
        return default (TItem);
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 的查找字典。</summary>
    /// <returns>如果存在，则为 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 的查找字典；否则为 null。</returns>
    [__DynamicallyInvokable]
    protected IDictionary<TKey, TItem> Dictionary
    {
      [__DynamicallyInvokable] get
      {
        return (IDictionary<TKey, TItem>) this.dict;
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 类的新实例，该实例使用默认的相等比较器。</summary>
    [__DynamicallyInvokable]
    protected KeyedCollection()
      : this((IEqualityComparer<TKey>) null, 0)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 类的新实例，该实例使用指定的相等比较器。</summary>
    /// <param name="comparer">比较键时要使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 泛型接口的实现，如果为 null，则使用从 <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" /> 获取的该类型的键的默认相等比较器。</param>
    [__DynamicallyInvokable]
    protected KeyedCollection(IEqualityComparer<TKey> comparer)
      : this(comparer, 0)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 类的新实例，该实例使用指定的相等比较器并在超过指定阈值时创建一个查找字典。</summary>
    /// <param name="comparer">比较键时要使用的 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 泛型接口的实现，如果为 null，则使用从 <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" /> 获取的该类型的键的默认相等比较器。</param>
    /// <param name="dictionaryCreationThreshold">在不创建查找字典的情况下集合可容纳的元素的数目（0 表示添加第一项时创建查找字典）；或者为 -1，表示指定永远不会创建查找字典。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="dictionaryCreationThreshold" /> is less than –1.</exception>
    [__DynamicallyInvokable]
    protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
    {
      if (comparer == null)
        comparer = (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
      if (dictionaryCreationThreshold == -1)
        dictionaryCreationThreshold = int.MaxValue;
      if (dictionaryCreationThreshold < -1)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionaryCreationThreshold, ExceptionResource.ArgumentOutOfRange_InvalidThreshold);
      this.comparer = comparer;
      this.threshold = dictionaryCreationThreshold;
    }

    /// <summary>确定集合是否包含具有指定键的元素。</summary>
    /// <returns>如果 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 包含具有指定键的元素，则为 true；否则为 false。</returns>
    /// <param name="key">要在 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 中定位的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    [__DynamicallyInvokable]
    public bool Contains(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.dict != null)
        return this.dict.ContainsKey(key);
      if ((object) key != null)
      {
        foreach (TItem obj in (IEnumerable<TItem>) this.Items)
        {
          if (this.comparer.Equals(this.GetKeyForItem(obj), key))
            return true;
        }
      }
      return false;
    }

    private bool ContainsItem(TItem item)
    {
      TKey keyForItem;
      if (this.dict == null || (object) (keyForItem = this.GetKeyForItem(item)) == null)
        return this.Items.Contains(item);
      TItem x;
      if (this.dict.TryGetValue(keyForItem, out x))
        return EqualityComparer<TItem>.Default.Equals(x, item);
      return false;
    }

    /// <summary>从 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 中移除带有指定键的元素。</summary>
    /// <returns>如果成功移除了元素，则为 true；否则为 false。如果未在 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 中找到 <paramref name="key" />，则此方法还返回 false。</returns>
    /// <param name="key">要移除的元素的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    [__DynamicallyInvokable]
    public bool Remove(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.dict != null)
      {
        if (this.dict.ContainsKey(key))
          return base.Remove(this.dict[key]);
        return false;
      }
      if ((object) key != null)
      {
        for (int index = 0; index < this.Items.Count; ++index)
        {
          if (this.comparer.Equals(this.GetKeyForItem(this.Items[index]), key))
          {
            this.RemoveItem(index);
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>更改与查找字典中指定元素相关联的键。</summary>
    /// <param name="item">要更改其键的元素。</param>
    /// <param name="newKey">
    /// <paramref name="item" /> 的新键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="item" /> is null.-or-<paramref name="key" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="item" /> is not found.-or-<paramref name="key" /> already exists in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</exception>
    [__DynamicallyInvokable]
    protected void ChangeItemKey(TItem item, TKey newKey)
    {
      if (!this.ContainsItem(item))
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_ItemNotExist);
      TKey keyForItem = this.GetKeyForItem(item);
      if (this.comparer.Equals(keyForItem, newKey))
        return;
      if ((object) newKey != null)
        this.AddKey(newKey, item);
      if ((object) keyForItem == null)
        return;
      this.RemoveKey(keyForItem);
    }

    /// <summary>从 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 中移除所有元素。</summary>
    [__DynamicallyInvokable]
    protected override void ClearItems()
    {
      base.ClearItems();
      if (this.dict != null)
        this.dict.Clear();
      this.keyCount = 0;
    }

    /// <summary>在派生类中实现时，将从指定元素提取键。</summary>
    /// <returns>指定元素的键。</returns>
    /// <param name="item">从中提取键的元素。</param>
    [__DynamicallyInvokable]
    protected abstract TKey GetKeyForItem(TItem item);

    /// <summary>将元素插入 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 的指定索引处。</summary>
    /// <param name="index">从零开始的索引，应在该位置插入 <paramref name="item" />。</param>
    /// <param name="item">要插入的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
    [__DynamicallyInvokable]
    protected override void InsertItem(int index, TItem item)
    {
      TKey keyForItem = this.GetKeyForItem(item);
      if ((object) keyForItem != null)
        this.AddKey(keyForItem, item);
      this.InsertItem(index, item);
    }

    /// <summary>移除 <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> 的指定索引处的元素。</summary>
    /// <param name="index">要移除的元素的索引。</param>
    [__DynamicallyInvokable]
    protected override void RemoveItem(int index)
    {
      TKey keyForItem = this.GetKeyForItem(this.Items[index]);
      if ((object) keyForItem != null)
        this.RemoveKey(keyForItem);
      base.RemoveItem(index);
    }

    /// <summary>使用指定项替换指定索引处的项。</summary>
    /// <param name="index">要替换的项的从零开始的索引。</param>
    /// <param name="item">新项目。</param>
    [__DynamicallyInvokable]
    protected override void SetItem(int index, TItem item)
    {
      TKey keyForItem1 = this.GetKeyForItem(item);
      TKey keyForItem2 = this.GetKeyForItem(this.Items[index]);
      if (this.comparer.Equals(keyForItem2, keyForItem1))
      {
        if ((object) keyForItem1 != null && this.dict != null)
          this.dict[keyForItem1] = item;
      }
      else
      {
        if ((object) keyForItem1 != null)
          this.AddKey(keyForItem1, item);
        if ((object) keyForItem2 != null)
          this.RemoveKey(keyForItem2);
      }
      this.SetItem(index, item);
    }

    private void AddKey(TKey key, TItem item)
    {
      if (this.dict != null)
        this.dict.Add(key, item);
      else if (this.keyCount == this.threshold)
      {
        this.CreateDictionary();
        this.dict.Add(key, item);
      }
      else
      {
        if (this.Contains(key))
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
        this.keyCount = this.keyCount + 1;
      }
    }

    private void CreateDictionary()
    {
      this.dict = new System.Collections.Generic.Dictionary<TKey, TItem>(this.comparer);
      foreach (TItem obj in (IEnumerable<TItem>) this.Items)
      {
        TKey keyForItem = this.GetKeyForItem(obj);
        if ((object) keyForItem != null)
          this.dict.Add(keyForItem, obj);
      }
    }

    private void RemoveKey(TKey key)
    {
      if (this.dict != null)
        this.dict.Remove(key);
      else
        this.keyCount = this.keyCount - 1;
    }
  }
}
