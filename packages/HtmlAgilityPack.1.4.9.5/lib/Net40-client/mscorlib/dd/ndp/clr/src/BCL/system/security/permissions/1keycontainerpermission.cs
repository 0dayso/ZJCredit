// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAccessEntryCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>表示 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象的集合。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
  {
    private ArrayList m_list;
    private KeyContainerPermissionFlags m_globalFlags;

    /// <summary>获取集合中指定索引处的项。</summary>
    /// <returns>集合中指定索引处的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</returns>
    /// <param name="index">要访问的元素从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 大于或等于集合计数。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="index" /> 为负数。</exception>
    public KeyContainerPermissionAccessEntry this[int index]
    {
      get
      {
        if (index < 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (index >= this.Count)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        return (KeyContainerPermissionAccessEntry) this.m_list[index];
      }
    }

    /// <summary>获取集合中项的数目。</summary>
    /// <returns>集合中的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象数。</returns>
    public int Count
    {
      get
      {
        return this.m_list.Count;
      }
    }

    /// <summary>获取一个值，该值指示集合是否是同步的（线程安全）。</summary>
    /// <returns>所有情况下均为 false。</returns>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取可用于同步对集合的访问的对象。</summary>
    /// <returns>可用于同步集合访问的对象。</returns>
    public object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    private KeyContainerPermissionAccessEntryCollection()
    {
    }

    internal KeyContainerPermissionAccessEntryCollection(KeyContainerPermissionFlags globalFlags)
    {
      this.m_list = new ArrayList();
      this.m_globalFlags = globalFlags;
    }

    /// <summary>将 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象添加到集合中。</summary>
    /// <returns>新元素位置处插入的索引。</returns>
    /// <param name="accessEntry">要添加的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="accessEntry" /> 为 null。</exception>
    public int Add(KeyContainerPermissionAccessEntry accessEntry)
    {
      if (accessEntry == null)
        throw new ArgumentNullException("accessEntry");
      int index = this.m_list.IndexOf((object) accessEntry);
      if (index == -1)
      {
        if (accessEntry.Flags != this.m_globalFlags)
          return this.m_list.Add((object) accessEntry);
        return -1;
      }
      ((KeyContainerPermissionAccessEntry) this.m_list[index]).Flags &= accessEntry.Flags;
      return index;
    }

    /// <summary>从集合中移除所有 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</summary>
    public void Clear()
    {
      this.m_list.Clear();
    }

    /// <summary>获取指定的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象在该集合中的索引（如果它在该集合中存在）。</summary>
    /// <returns>该集合中指定的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象的索引，或者如果未找到匹配项则为 –1。</returns>
    /// <param name="accessEntry">要定位的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</param>
    public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
    {
      return this.m_list.IndexOf((object) accessEntry);
    }

    /// <summary>从该集合中移除指定的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</summary>
    /// <param name="accessEntry">要移除的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="accessEntry" /> 为 null。</exception>
    public void Remove(KeyContainerPermissionAccessEntry accessEntry)
    {
      if (accessEntry == null)
        throw new ArgumentNullException("accessEntry");
      this.m_list.Remove((object) accessEntry);
    }

    /// <summary>返回 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> 对象，该对象可用于循环访问集合中的对象。</summary>
    /// <returns>可用于循环访问集合的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> 对象。</returns>
    public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
    {
      return new KeyContainerPermissionAccessEntryEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new KeyContainerPermissionAccessEntryEnumerator(this);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0 || index >= array.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (index + this.Count > array.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        array.SetValue((object) this[index1], index);
        ++index;
      }
    }

    /// <summary>将集合中的元素复制到一个兼容的一维数组（从目标数组的指定索引处开始）。</summary>
    /// <param name="array">一维 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 数组，它是从当前集合复制的元素的目标位置。</param>
    /// <param name="index">
    /// <paramref name="array" /> 内的索引，从此处开始复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -集合中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
    {
      ((ICollection) this).CopyTo((Array) array, index);
    }
  }
}
