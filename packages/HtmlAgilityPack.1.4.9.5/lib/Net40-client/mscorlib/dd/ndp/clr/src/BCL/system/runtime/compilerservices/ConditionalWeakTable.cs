// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ConditionalWeakTable`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>使编译器可以将对象字段动态附加到托管对象。</summary>
  /// <typeparam name="TKey">字段所附加到的引用类型。</typeparam>
  /// <typeparam name="TValue">字段的类型。此类型必须是引用类型。</typeparam>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public sealed class ConditionalWeakTable<TKey, TValue> where TKey : class where TValue : class
  {
    private int[] _buckets;
    private ConditionalWeakTable<TKey, TValue>.Entry[] _entries;
    private int _freeList;
    private const int _initialCapacity = 5;
    private readonly object _lock;
    private bool _invalid;

    internal ICollection<TKey> Keys
    {
      [SecuritySafeCritical] get
      {
        List<TKey> keyList = new List<TKey>();
        lock (this._lock)
        {
          for (int local_3 = 0; local_3 < this._buckets.Length; ++local_3)
          {
            for (int local_4 = this._buckets[local_3]; local_4 != -1; local_4 = this._entries[local_4].next)
            {
              TKey local_5 = (TKey) this._entries[local_4].depHnd.GetPrimary();
              if ((object) local_5 != null)
                keyList.Add(local_5);
            }
          }
        }
        return (ICollection<TKey>) keyList;
      }
    }

    internal ICollection<TValue> Values
    {
      [SecuritySafeCritical] get
      {
        List<TValue> objList = new List<TValue>();
        lock (this._lock)
        {
          for (int local_3 = 0; local_3 < this._buckets.Length; ++local_3)
          {
            for (int local_4 = this._buckets[local_3]; local_4 != -1; local_4 = this._entries[local_4].next)
            {
              object local_5 = (object) null;
              object local_6 = (object) null;
              this._entries[local_4].depHnd.GetPrimaryAndSecondary(out local_5, out local_6);
              if (local_5 != null)
                objList.Add((TValue) local_6);
            }
          }
        }
        return (ICollection<TValue>) objList;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" /> 类的新实例。</summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public ConditionalWeakTable()
    {
      this._buckets = new int[0];
      this._entries = new ConditionalWeakTable<TKey, TValue>.Entry[0];
      this._freeList = -1;
      this._lock = new object();
      this.Resize();
    }

    /// <summary>确保释放资源并在垃圾回收器回收时执行其他清理操作<see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" />对象。</summary>
    [SecuritySafeCritical]
    ~ConditionalWeakTable()
    {
      if (Environment.HasShutdownStarted || this._lock == null)
        return;
      lock (this._lock)
      {
        if (this._invalid)
          return;
        ConditionalWeakTable<TKey, TValue>.Entry[] local_2 = this._entries;
        this._invalid = true;
        this._entries = (ConditionalWeakTable<TKey, TValue>.Entry[]) null;
        this._buckets = (int[]) null;
        for (int local_3 = 0; local_3 < local_2.Length; ++local_3)
          local_2[local_3].depHnd.Free();
      }
    }

    /// <summary>获取指定键的值。</summary>
    /// <returns>如果找到 <paramref name="key" />，则为 true；否则为 false。</returns>
    /// <param name="key">一个键，表示具有所附加的属性的对象。</param>
    /// <param name="value">此方法返回时，将包含所附加的属性值。如果找不到 <paramref name="key" />，则 <paramref name="value" /> 包含默认值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        return this.TryGetValueWorker(key, out value);
      }
    }

    /// <summary>将键添加到表中。</summary>
    /// <param name="key">要添加的键。<paramref name="key" /> 表示属性所附加到的对象。</param>
    /// <param name="value">该键的属性值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="key" /> 已存在。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void Add(TKey key, TValue value)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        this._invalid = true;
        if (this.FindEntry(key) != -1)
        {
          this._invalid = false;
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
        }
        this.CreateEntry(key, value);
        this._invalid = false;
      }
    }

    /// <summary>从表中移除某个键及其值。</summary>
    /// <returns>如果找到并移除该键，则为 true；否则为 false。</returns>
    /// <param name="key">要移除的键。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Remove(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        this._invalid = true;
        int local_2 = RuntimeHelpers.GetHashCode((object) key) & int.MaxValue;
        int local_3 = local_2 % this._buckets.Length;
        int local_4 = -1;
        for (int local_5 = this._buckets[local_3]; local_5 != -1; local_5 = this._entries[local_5].next)
        {
          if (this._entries[local_5].hashCode == local_2 && this._entries[local_5].depHnd.GetPrimary() == (object) key)
          {
            if (local_4 == -1)
              this._buckets[local_3] = this._entries[local_5].next;
            else
              this._entries[local_4].next = this._entries[local_5].next;
            this._entries[local_5].depHnd.Free();
            this._entries[local_5].next = this._freeList;
            this._freeList = local_5;
            this._invalid = false;
            return true;
          }
          local_4 = local_5;
        }
        this._invalid = false;
        return false;
      }
    }

    /// <summary>以原子方式在表中搜索指定键，并返回对应的值。如果表中不存在该键，此方法将调用一个回调方法来创建绑定到指定键的值。</summary>
    /// <returns>如果表中已存在 <paramref name="key" />，则为附加到 <paramref name="key" /> 的值；否则为 <paramref name="createValueCallback" /> 委托返回的新值。</returns>
    /// <param name="key">要搜索的键。<paramref name="key" /> 表示属性所附加到的对象。</param>
    /// <param name="createValueCallback">可以为给定 <paramref name="key" /> 创建值的方法的委托。它只有一个 <paramref name="TKey" /> 类型的参数，并返回一个 <paramref name="TValue" /> 类型的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 或 <paramref name="createValueCallback" /> 为 null。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public TValue GetValue(TKey key, ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
    {
      if (createValueCallback == null)
        throw new ArgumentNullException("createValueCallback");
      TValue obj1;
      if (this.TryGetValue(key, out obj1))
        return obj1;
      TValue obj2 = createValueCallback(key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        this._invalid = true;
        if (this.TryGetValueWorker(key, out obj1))
        {
          this._invalid = false;
          return obj1;
        }
        this.CreateEntry(key, obj2);
        this._invalid = false;
        return obj2;
      }
    }

    /// <summary>以原子方式在表中搜索指定键，并返回对应的值。如果表中不存在该键，此方法将调用表示表值的类的默认构造函数，以创建绑定到指定键的值。</summary>
    /// <returns>如果表中已存在 <paramref name="key" />，则为对应于 <paramref name="key" /> 的值；否则为 <paramref name="TValue" /> 范型类型参数定义的类的默认构造函数创建的新值。</returns>
    /// <param name="key">要搜索的键。<paramref name="key" /> 表示属性所附加到的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MissingMemberException" />。表示表中值的类未定义默认构造函数。</exception>
    [__DynamicallyInvokable]
    public TValue GetOrCreateValue(TKey key)
    {
      return this.GetValue(key, (ConditionalWeakTable<TKey, TValue>.CreateValueCallback) (k => Activator.CreateInstance<TValue>()));
    }

    [SecuritySafeCritical]
    [FriendAccessAllowed]
    internal TKey FindEquivalentKeyUnsafe(TKey key, out TValue value)
    {
      lock (this._lock)
      {
        for (int local_2 = 0; local_2 < this._buckets.Length; ++local_2)
        {
          for (int local_3 = this._buckets[local_2]; local_3 != -1; local_3 = this._entries[local_3].next)
          {
            object local_4;
            object local_5;
            this._entries[local_3].depHnd.GetPrimaryAndSecondary(out local_4, out local_5);
            if (object.Equals(local_4, (object) key))
            {
              value = (TValue) local_5;
              return (TKey) local_4;
            }
          }
        }
      }
      value = default (TValue);
      return default (TKey);
    }

    [SecuritySafeCritical]
    internal void Clear()
    {
      lock (this._lock)
      {
        for (int local_3 = 0; local_3 < this._buckets.Length; ++local_3)
          this._buckets[local_3] = -1;
        int local_2;
        for (local_2 = 0; local_2 < this._entries.Length; ++local_2)
        {
          if (this._entries[local_2].depHnd.IsAllocated)
            this._entries[local_2].depHnd.Free();
          this._entries[local_2].next = local_2 - 1;
        }
        this._freeList = local_2 - 1;
      }
    }

    [SecurityCritical]
    private bool TryGetValueWorker(TKey key, out TValue value)
    {
      int entry = this.FindEntry(key);
      if (entry != -1)
      {
        object primary = (object) null;
        object secondary = (object) null;
        this._entries[entry].depHnd.GetPrimaryAndSecondary(out primary, out secondary);
        if (primary != null)
        {
          value = (TValue) secondary;
          return true;
        }
      }
      value = default (TValue);
      return false;
    }

    [SecurityCritical]
    private void CreateEntry(TKey key, TValue value)
    {
      if (this._freeList == -1)
        this.Resize();
      int num = RuntimeHelpers.GetHashCode((object) key) & int.MaxValue;
      int index1 = num % this._buckets.Length;
      int index2 = this._freeList;
      this._freeList = this._entries[index2].next;
      this._entries[index2].hashCode = num;
      this._entries[index2].depHnd = new DependentHandle((object) key, (object) value);
      this._entries[index2].next = this._buckets[index1];
      this._buckets[index1] = index2;
    }

    [SecurityCritical]
    private void Resize()
    {
      int length = this._buckets.Length;
      bool flag = false;
      for (int index = 0; index < this._entries.Length; ++index)
      {
        if (this._entries[index].depHnd.IsAllocated && this._entries[index].depHnd.GetPrimary() == null)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        length = HashHelpers.GetPrime(this._buckets.Length == 0 ? 6 : this._buckets.Length * 2);
      int num = -1;
      int[] numArray = new int[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = -1;
      ConditionalWeakTable<TKey, TValue>.Entry[] entryArray = new ConditionalWeakTable<TKey, TValue>.Entry[length];
      int index1;
      for (index1 = 0; index1 < this._entries.Length; ++index1)
      {
        DependentHandle dependentHandle = this._entries[index1].depHnd;
        if (dependentHandle.IsAllocated && dependentHandle.GetPrimary() != null)
        {
          int index2 = this._entries[index1].hashCode % length;
          entryArray[index1].depHnd = dependentHandle;
          entryArray[index1].hashCode = this._entries[index1].hashCode;
          entryArray[index1].next = numArray[index2];
          numArray[index2] = index1;
        }
        else
        {
          this._entries[index1].depHnd.Free();
          entryArray[index1].depHnd = new DependentHandle();
          entryArray[index1].next = num;
          num = index1;
        }
      }
      for (; index1 != entryArray.Length; ++index1)
      {
        entryArray[index1].depHnd = new DependentHandle();
        entryArray[index1].next = num;
        num = index1;
      }
      this._buckets = numArray;
      this._entries = entryArray;
      this._freeList = num;
    }

    [SecurityCritical]
    private int FindEntry(TKey key)
    {
      int num = RuntimeHelpers.GetHashCode((object) key) & int.MaxValue;
      for (int index = this._buckets[num % this._buckets.Length]; index != -1; index = this._entries[index].next)
      {
        if (this._entries[index].hashCode == num && this._entries[index].depHnd.GetPrimary() == (object) key)
          return index;
      }
      return -1;
    }

    private void VerifyIntegrity()
    {
      if (this._invalid)
        throw new InvalidOperationException(Environment.GetResourceString("CollectionCorrupted"));
    }

    /// <summary>表示一个方法，该方法用于创建非默认值以将其作为键/值对组成部分添加到 <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" /> 对象。</summary>
    /// <returns>表示要附加到指定键的值的引用类型实例。</returns>
    /// <param name="key">属于要创建的值的键。</param>
    [__DynamicallyInvokable]
    public delegate TValue CreateValueCallback(TKey key) where TKey : class where TValue : class;

    private struct Entry
    {
      public DependentHandle depHnd;
      public int hashCode;
      public int next;
    }
  }
}
