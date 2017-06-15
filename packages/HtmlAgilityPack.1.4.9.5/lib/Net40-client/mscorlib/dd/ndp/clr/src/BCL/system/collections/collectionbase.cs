// Decompiled with JetBrains decompiler
// Type: System.Collections.CollectionBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>为强类型集合提供 abstract 基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class CollectionBase : IList, ICollection, IEnumerable
  {
    private ArrayList list;

    /// <summary>获取一个 <see cref="T:System.Collections.ArrayList" />，它包含 <see cref="T:System.Collections.CollectionBase" /> 实例中元素的列表。</summary>
    /// <returns>表示 <see cref="T:System.Collections.CollectionBase" /> 实例本身的 <see cref="T:System.Collections.ArrayList" />。检索此属性的值的运算复杂度为 O(1)。</returns>
    protected ArrayList InnerList
    {
      get
      {
        if (this.list == null)
          this.list = new ArrayList();
        return this.list;
      }
    }

    /// <summary>获取一个 <see cref="T:System.Collections.IList" />，它包含 <see cref="T:System.Collections.CollectionBase" /> 实例中元素的列表。</summary>
    /// <returns>表示 <see cref="T:System.Collections.CollectionBase" /> 实例本身的 <see cref="T:System.Collections.IList" />。</returns>
    protected IList List
    {
      get
      {
        return (IList) this;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Collections.CollectionBase" /> 可包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.CollectionBase" /> 可包含的元素数。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <see cref="P:System.Collections.CollectionBase.Capacity" /> 设置为小于 <see cref="P:System.Collections.CollectionBase.Count" /> 的值。</exception>
    /// <exception cref="T:System.OutOfMemoryException">系统中没有足够的可用内存。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public int Capacity
    {
      get
      {
        return this.InnerList.Capacity;
      }
      set
      {
        this.InnerList.Capacity = value;
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.CollectionBase" /> 实例中的元素数。不能重写此属性。</summary>
    /// <returns>包含在 <see cref="T:System.Collections.CollectionBase" /> 实例中的元素数。检索此属性的值的运算复杂度为 O(1)。</returns>
    /// <filterpriority>2</filterpriority>
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        if (this.list != null)
          return this.list.Count;
        return 0;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return this.InnerList.IsReadOnly;
      }
    }

    bool IList.IsFixedSize
    {
      get
      {
        return this.InnerList.IsFixedSize;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return this.InnerList.IsSynchronized;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return this.InnerList.SyncRoot;
      }
    }

    object IList.this[int index]
    {
      get
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        return this.InnerList[index];
      }
      set
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.OnValidate(value);
        object oldValue = this.InnerList[index];
        this.OnSet(index, oldValue, value);
        this.InnerList[index] = value;
        try
        {
          this.OnSetComplete(index, oldValue, value);
        }
        catch
        {
          this.InnerList[index] = oldValue;
          throw;
        }
      }
    }

    /// <summary>使用默认初始容量初始化 <see cref="T:System.Collections.CollectionBase" /> 类的新实例。</summary>
    protected CollectionBase()
    {
      this.list = new ArrayList();
    }

    /// <summary>使用指定的容量初始化 <see cref="T:System.Collections.CollectionBase" /> 类的新实例。</summary>
    /// <param name="capacity">新列表最初可以存储的元素数。</param>
    protected CollectionBase(int capacity)
    {
      this.list = new ArrayList(capacity);
    }

    /// <summary>从 <see cref="T:System.Collections.CollectionBase" /> 实例移除所有对象。不能重写此方法。</summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void Clear()
    {
      this.OnClear();
      this.InnerList.Clear();
      this.OnClearComplete();
    }

    /// <summary>移除 <see cref="T:System.Collections.CollectionBase" /> 实例的指定索引处的元素。此方法不可重写。</summary>
    /// <param name="index">要移除的元素的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。- 或 -<paramref name="index" /> 等于或大于 <see cref="P:System.Collections.CollectionBase.Count" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      object obj = this.InnerList[index];
      this.OnValidate(obj);
      this.OnRemove(index, obj);
      this.InnerList.RemoveAt(index);
      try
      {
        this.OnRemoveComplete(index, obj);
      }
      catch
      {
        this.InnerList.Insert(index, obj);
        throw;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.InnerList.CopyTo(array, index);
    }

    bool IList.Contains(object value)
    {
      return this.InnerList.Contains(value);
    }

    int IList.Add(object value)
    {
      this.OnValidate(value);
      this.OnInsert(this.InnerList.Count, value);
      int index = this.InnerList.Add(value);
      try
      {
        this.OnInsertComplete(index, value);
      }
      catch
      {
        this.InnerList.RemoveAt(index);
        throw;
      }
      return index;
    }

    void IList.Remove(object value)
    {
      this.OnValidate(value);
      int index = this.InnerList.IndexOf(value);
      if (index < 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_RemoveArgNotFound"));
      this.OnRemove(index, value);
      this.InnerList.RemoveAt(index);
      try
      {
        this.OnRemoveComplete(index, value);
      }
      catch
      {
        this.InnerList.Insert(index, value);
        throw;
      }
    }

    int IList.IndexOf(object value)
    {
      return this.InnerList.IndexOf(value);
    }

    void IList.Insert(int index, object value)
    {
      if (index < 0 || index > this.Count)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      this.OnValidate(value);
      this.OnInsert(index, value);
      this.InnerList.Insert(index, value);
      try
      {
        this.OnInsertComplete(index, value);
      }
      catch
      {
        this.InnerList.RemoveAt(index);
        throw;
      }
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.CollectionBase" /> 实例的枚举器。</summary>
    /// <returns>用于 <see cref="T:System.Collections.CollectionBase" /> 实例的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public IEnumerator GetEnumerator()
    {
      return this.InnerList.GetEnumerator();
    }

    /// <summary>当在 <see cref="T:System.Collections.CollectionBase" /> 实例中设置值之前执行其他自定义进程。</summary>
    /// <param name="index">从零开始的索引，可在该位置找到 <paramref name="oldValue" />。</param>
    /// <param name="oldValue">要用 <paramref name="newValue" /> 替换的值。</param>
    /// <param name="newValue">
    /// <paramref name="index" /> 处的元素的新值。</param>
    protected virtual void OnSet(int index, object oldValue, object newValue)
    {
    }

    /// <summary>在向 <see cref="T:System.Collections.CollectionBase" /> 实例中插入新元素之前执行其他自定义进程。</summary>
    /// <param name="index">从零开始的索引，在该处插入 <paramref name="value" />。</param>
    /// <param name="value">
    /// <paramref name="index" /> 处的元素的新值。</param>
    protected virtual void OnInsert(int index, object value)
    {
    }

    /// <summary>当清除 <see cref="T:System.Collections.CollectionBase" /> 实例的内容时执行其他自定义进程。</summary>
    protected virtual void OnClear()
    {
    }

    /// <summary>当从 <see cref="T:System.Collections.CollectionBase" /> 实例移除元素时执行其他自定义进程。</summary>
    /// <param name="index">从零开始的索引，可在该位置找到 <paramref name="value" />。</param>
    /// <param name="value">要从 <paramref name="index" /> 移除的元素的值。</param>
    protected virtual void OnRemove(int index, object value)
    {
    }

    /// <summary>当验证值时执行其他自定义进程。</summary>
    /// <param name="value">要验证的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    protected virtual void OnValidate(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
    }

    /// <summary>当在 <see cref="T:System.Collections.CollectionBase" /> 实例中设置值后执行其他自定义进程。</summary>
    /// <param name="index">从零开始的索引，可在该位置找到 <paramref name="oldValue" />。</param>
    /// <param name="oldValue">要用 <paramref name="newValue" /> 替换的值。</param>
    /// <param name="newValue">
    /// <paramref name="index" /> 处的元素的新值。</param>
    protected virtual void OnSetComplete(int index, object oldValue, object newValue)
    {
    }

    /// <summary>在向 <see cref="T:System.Collections.CollectionBase" /> 实例中插入新元素之后执行其他自定义进程。</summary>
    /// <param name="index">从零开始的索引，在该处插入 <paramref name="value" />。</param>
    /// <param name="value">
    /// <paramref name="index" /> 处的元素的新值。</param>
    protected virtual void OnInsertComplete(int index, object value)
    {
    }

    /// <summary>在清除 <see cref="T:System.Collections.CollectionBase" /> 实例的内容之后执行其他自定义进程。</summary>
    protected virtual void OnClearComplete()
    {
    }

    /// <summary>在从 <see cref="T:System.Collections.CollectionBase" /> 实例中移除元素之后执行其他自定义进程。</summary>
    /// <param name="index">从零开始的索引，可在该位置找到 <paramref name="value" />。</param>
    /// <param name="value">要从 <paramref name="index" /> 移除的元素的值。</param>
    protected virtual void OnRemoveComplete(int index, object value)
    {
    }
  }
}
