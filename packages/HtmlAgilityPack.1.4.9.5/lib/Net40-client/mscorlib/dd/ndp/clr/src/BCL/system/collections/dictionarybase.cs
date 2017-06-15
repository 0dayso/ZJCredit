// Decompiled with JetBrains decompiler
// Type: System.Collections.DictionaryBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>为键/值对的强类型集合提供 abstract 基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
  {
    private Hashtable hashtable;

    /// <summary>获取包含在 <see cref="T:System.Collections.DictionaryBase" /> 实例中的元素的列表。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.Hashtable" />，表示 <see cref="T:System.Collections.DictionaryBase" /> 实例本身。</returns>
    protected Hashtable InnerHashtable
    {
      get
      {
        if (this.hashtable == null)
          this.hashtable = new Hashtable();
        return this.hashtable;
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.DictionaryBase" /> 实例中的元素的列表。</summary>
    /// <returns>表示 <see cref="T:System.Collections.DictionaryBase" /> 实例本身的 <see cref="T:System.Collections.IDictionary" />。</returns>
    protected IDictionary Dictionary
    {
      get
      {
        return (IDictionary) this;
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.DictionaryBase" /> 实例中的元素数。</summary>
    /// <returns>包含在 <see cref="T:System.Collections.DictionaryBase" /> 实例中的元素数。</returns>
    /// <filterpriority>2</filterpriority>
    public int Count
    {
      get
      {
        if (this.hashtable != null)
          return this.hashtable.Count;
        return 0;
      }
    }

    bool IDictionary.IsReadOnly
    {
      get
      {
        return this.InnerHashtable.IsReadOnly;
      }
    }

    bool IDictionary.IsFixedSize
    {
      get
      {
        return this.InnerHashtable.IsFixedSize;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return this.InnerHashtable.IsSynchronized;
      }
    }

    ICollection IDictionary.Keys
    {
      get
      {
        return this.InnerHashtable.Keys;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return this.InnerHashtable.SyncRoot;
      }
    }

    ICollection IDictionary.Values
    {
      get
      {
        return this.InnerHashtable.Values;
      }
    }

    object IDictionary.this[object key]
    {
      get
      {
        object currentValue = this.InnerHashtable[key];
        this.OnGet(key, currentValue);
        return currentValue;
      }
      set
      {
        this.OnValidate(key, value);
        bool flag = true;
        object oldValue = this.InnerHashtable[key];
        if (oldValue == null)
          flag = this.InnerHashtable.Contains(key);
        this.OnSet(key, oldValue, value);
        this.InnerHashtable[key] = value;
        try
        {
          this.OnSetComplete(key, oldValue, value);
        }
        catch
        {
          if (flag)
            this.InnerHashtable[key] = oldValue;
          else
            this.InnerHashtable.Remove(key);
          throw;
        }
      }
    }

    /// <summary>将 <see cref="T:System.Collections.DictionaryBase" /> 元素复制到位于指定索引处的一维 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，为从 <see cref="T:System.Collections.DictionaryBase" /> 实例复制的 <see cref="T:System.Collections.DictionaryEntry" /> 对象的目标位置。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源 <see cref="T:System.Collections.DictionaryBase" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    /// <exception cref="T:System.InvalidCastException">源 <see cref="T:System.Collections.DictionaryBase" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <filterpriority>2</filterpriority>
    public void CopyTo(Array array, int index)
    {
      this.InnerHashtable.CopyTo(array, index);
    }

    bool IDictionary.Contains(object key)
    {
      return this.InnerHashtable.Contains(key);
    }

    void IDictionary.Add(object key, object value)
    {
      this.OnValidate(key, value);
      this.OnInsert(key, value);
      this.InnerHashtable.Add(key, value);
      try
      {
        this.OnInsertComplete(key, value);
      }
      catch
      {
        this.InnerHashtable.Remove(key);
        throw;
      }
    }

    /// <summary>清除 <see cref="T:System.Collections.DictionaryBase" /> 实例的内容。</summary>
    /// <filterpriority>2</filterpriority>
    public void Clear()
    {
      this.OnClear();
      this.InnerHashtable.Clear();
      this.OnClearComplete();
    }

    void IDictionary.Remove(object key)
    {
      if (!this.InnerHashtable.Contains(key))
        return;
      object obj = this.InnerHashtable[key];
      this.OnValidate(key, obj);
      this.OnRemove(key, obj);
      this.InnerHashtable.Remove(key);
      try
      {
        this.OnRemoveComplete(key, obj);
      }
      catch
      {
        this.InnerHashtable.Add(key, obj);
        throw;
      }
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.DictionaryBase" /> 实例的 <see cref="T:System.Collections.IDictionaryEnumerator" />。</summary>
    /// <returns>用于 <see cref="T:System.Collections.DictionaryBase" /> 实例的 <see cref="T:System.Collections.IDictionaryEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    public IDictionaryEnumerator GetEnumerator()
    {
      return this.InnerHashtable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.InnerHashtable.GetEnumerator();
    }

    /// <summary>获取 <see cref="T:System.Collections.DictionaryBase" /> 实例中带有指定键和值的元素。</summary>
    /// <returns>包含带有指定键和值的元素的 <see cref="T:System.Object" />。</returns>
    /// <param name="key">要获取的元素的键。</param>
    /// <param name="currentValue">与 <paramref name="key" /> 相关联的元素的当前值。</param>
    protected virtual object OnGet(object key, object currentValue)
    {
      return currentValue;
    }

    /// <summary>当在 <see cref="T:System.Collections.DictionaryBase" /> 实例中设置值之前执行其他自定义进程。</summary>
    /// <param name="key">要定位的元素的键。</param>
    /// <param name="oldValue">与 <paramref name="key" /> 相关联的元素的旧值。</param>
    /// <param name="newValue">与 <paramref name="key" /> 相关联的元素的新值。</param>
    protected virtual void OnSet(object key, object oldValue, object newValue)
    {
    }

    /// <summary>在向 <see cref="T:System.Collections.DictionaryBase" /> 实例中插入新元素之前执行其他自定义进程。</summary>
    /// <param name="key">要插入的元素的键。</param>
    /// <param name="value">要插入的元素的值。</param>
    protected virtual void OnInsert(object key, object value)
    {
    }

    /// <summary>在清除 <see cref="T:System.Collections.DictionaryBase" /> 实例的内容之前执行其他自定义进程。</summary>
    protected virtual void OnClear()
    {
    }

    /// <summary>当从 <see cref="T:System.Collections.DictionaryBase" /> 实例移除元素时执行其他自定义进程。</summary>
    /// <param name="key">要移除的元素的键。</param>
    /// <param name="value">要移除的元素的值。</param>
    protected virtual void OnRemove(object key, object value)
    {
    }

    /// <summary>在验证具有指定键和值的元素时执行其他自定义进程。</summary>
    /// <param name="key">要验证的元素的键。</param>
    /// <param name="value">要验证的元素的值。</param>
    protected virtual void OnValidate(object key, object value)
    {
    }

    /// <summary>当在 <see cref="T:System.Collections.DictionaryBase" /> 实例中设置值后执行其他自定义进程。</summary>
    /// <param name="key">要定位的元素的键。</param>
    /// <param name="oldValue">与 <paramref name="key" /> 相关联的元素的旧值。</param>
    /// <param name="newValue">与 <paramref name="key" /> 相关联的元素的新值。</param>
    protected virtual void OnSetComplete(object key, object oldValue, object newValue)
    {
    }

    /// <summary>在向 <see cref="T:System.Collections.DictionaryBase" /> 实例中插入新元素之后执行其他自定义进程。</summary>
    /// <param name="key">要插入的元素的键。</param>
    /// <param name="value">要插入的元素的值。</param>
    protected virtual void OnInsertComplete(object key, object value)
    {
    }

    /// <summary>在清除 <see cref="T:System.Collections.DictionaryBase" /> 实例的内容之后执行其他自定义进程。</summary>
    protected virtual void OnClearComplete()
    {
    }

    /// <summary>在从 <see cref="T:System.Collections.DictionaryBase" /> 实例中移除元素之后执行其他自定义进程。</summary>
    /// <param name="key">要移除的元素的键。</param>
    /// <param name="value">要移除的元素的值。</param>
    protected virtual void OnRemoveComplete(object key, object value)
    {
    }
  }
}
