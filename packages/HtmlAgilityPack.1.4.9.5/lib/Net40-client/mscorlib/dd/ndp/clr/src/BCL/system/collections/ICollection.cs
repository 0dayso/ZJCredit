// Decompiled with JetBrains decompiler
// Type: System.Collections.ICollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>定义所有非泛型集合的大小、枚举数和同步方法。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface ICollection : IEnumerable
  {
    /// <summary>获取 <see cref="T:System.Collections.ICollection" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.ICollection" /> 中包含的元素个数。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    int Count { [__DynamicallyInvokable] get; }

    /// <summary>获取可用于同步对 <see cref="T:System.Collections.ICollection" /> 的访问的对象。</summary>
    /// <returns>可用于同步对 <see cref="T:System.Collections.ICollection" /> 的访问的对象。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object SyncRoot { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Collections.ICollection" /> 的访问（线程安全）。</summary>
    /// <returns>如果对 <see cref="T:System.Collections.ICollection" /> 的访问是同步的（线程安全），则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    bool IsSynchronized { [__DynamicallyInvokable] get; }

    /// <summary>从特定的 <see cref="T:System.Array" /> 索引处开始，将 <see cref="T:System.Collections.ICollection" /> 的元素复制到一个 <see cref="T:System.Array" /> 中。</summary>
    /// <param name="array">作为从 <see cref="T:System.Collections.ICollection" /> 复制的元素的目标的一维 <see cref="T:System.Array" />。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源 <see cref="T:System.Collections.ICollection" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。- 或 -源 <see cref="T:System.Collections.ICollection" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void CopyTo(Array array, int index);
  }
}
