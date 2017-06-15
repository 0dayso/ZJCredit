// Decompiled with JetBrains decompiler
// Type: System.Collections.ReadOnlyCollectionBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>为强类型非泛型只读集合提供 abstract 基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
  {
    private ArrayList list;

    /// <summary>获取包含在 <see cref="T:System.Collections.ReadOnlyCollectionBase" /> 实例中的元素的列表。</summary>
    /// <returns>表示 <see cref="T:System.Collections.ReadOnlyCollectionBase" /> 实例本身的 <see cref="T:System.Collections.ArrayList" />。</returns>
    protected ArrayList InnerList
    {
      get
      {
        if (this.list == null)
          this.list = new ArrayList();
        return this.list;
      }
    }

    /// <summary>获取包含在 <see cref="T:System.Collections.ReadOnlyCollectionBase" /> 实例中的元素数。</summary>
    /// <returns>包含在 <see cref="T:System.Collections.ReadOnlyCollectionBase" /> 实例中的元素数。检索此属性的值的运算复杂度为 O(1)。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int Count
    {
      get
      {
        return this.InnerList.Count;
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

    void ICollection.CopyTo(Array array, int index)
    {
      this.InnerList.CopyTo(array, index);
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.ReadOnlyCollectionBase" /> 实例的枚举器。</summary>
    /// <returns>用于 <see cref="T:System.Collections.ReadOnlyCollectionBase" /> 实例的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual IEnumerator GetEnumerator()
    {
      return this.InnerList.GetEnumerator();
    }
  }
}
