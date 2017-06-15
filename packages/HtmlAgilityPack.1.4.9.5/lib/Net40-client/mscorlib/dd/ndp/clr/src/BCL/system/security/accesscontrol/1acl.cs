// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.GenericAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>表示访问控制列表 (ACL)，并且是 <see cref="T:System.Security.AccessControl.CommonAcl" />、<see cref="T:System.Security.AccessControl.DiscretionaryAcl" />、<see cref="T:System.Security.AccessControl.RawAcl" /> 和 <see cref="T:System.Security.AccessControl.SystemAcl" /> 类的基类。</summary>
  public abstract class GenericAcl : ICollection, IEnumerable
  {
    /// <summary>当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 的修订级别。此值由未与目录服务对象关联的访问控制列表 (ACL) 的 <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> 属性返回。</summary>
    public static readonly byte AclRevision = 2;
    /// <summary>当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 的修订级别。此值由与目录服务对象关联的访问控制列表 (ACL) 的 <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> 属性返回。</summary>
    public static readonly byte AclRevisionDS = 4;
    /// <summary>
    /// <see cref="T:System.Security.AccessControl.GenericAcl" /> 对象的最大允许二进制长度。</summary>
    public static readonly int MaxBinaryLength = (int) ushort.MaxValue;
    internal const int HeaderLength = 8;

    /// <summary>获取 <see cref="T:System.Security.AccessControl.GenericAcl" /> 的修订级别。</summary>
    /// <returns>一个指定 <see cref="T:System.Security.AccessControl.GenericAcl" /> 的修订级别的字节值。</returns>
    public abstract byte Revision { get; }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.GenericAcl.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public abstract int BinaryLength { get; }

    /// <summary>获取或设置指定索引处的 <see cref="T:System.Security.AccessControl.GenericAce" />。</summary>
    /// <returns>位于指定索引处的 <see cref="T:System.Security.AccessControl.GenericAce" />。</returns>
    /// <param name="index">要获取或设置的 <see cref="T:System.Security.AccessControl.GenericAce" /> 的从零开始的索引。</param>
    public abstract GenericAce this[int index] { get; set; }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 对象中访问控制项 (ACE) 的数量。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 对象中 ACE 的数量。</returns>
    public abstract int Count { get; }

    /// <summary>此属性始终设置为 false。实现此属性只是因为它是 <see cref="T:System.Collections.ICollection" /> 接口的实现所必需的属性。</summary>
    /// <returns>始终为 false。</returns>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>该属性始终返回 null。实现此属性只是因为它是 <see cref="T:System.Collections.ICollection" /> 接口的实现所必需的属性。</summary>
    /// <returns>始终返回 null。</returns>
    public virtual object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.GenericAcl" /> 对象的内容封送到指定字节数组中，其位置从指定的偏移量开始。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.GenericAcl" /> 的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.GenericAcl" /> 复制到 <paramref name="array" />。</exception>
    public abstract void GetBinaryForm(byte[] binaryForm, int offset);

    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < this.Count)
        throw new ArgumentOutOfRangeException("array", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      for (int index1 = 0; index1 < this.Count; ++index1)
        array.SetValue((object) this[index1], index + index1);
    }

    /// <summary>将当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 的每个 <see cref="T:System.Security.AccessControl.GenericAce" /> 复制到指定的数组中。</summary>
    /// <param name="array">存放当前 <see cref="T:System.Security.AccessControl.GenericAcl" /> 包含的 <see cref="T:System.Security.AccessControl.GenericAce" /> 对象的副本的数组。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，这是开始复制的位置。</param>
    public void CopyTo(GenericAce[] array, int index)
    {
      ((ICollection) this).CopyTo((Array) array, index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new AceEnumerator(this);
    }

    /// <summary>返回 <see cref="T:System.Security.AccessControl.AceEnumerator" /> 类的新实例。</summary>
    /// <returns>此方法返回的 <see cref="T:Security.AccessControl.AceEnumerator" />。</returns>
    public AceEnumerator GetEnumerator()
    {
      return ((IEnumerable) this).GetEnumerator() as AceEnumerator;
    }
  }
}
