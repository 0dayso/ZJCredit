// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IReadOnlyList`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>表示可按照索引访问的元素的只读集合。</summary>
  /// <typeparam name="T">只读列表中元素的类型。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
  {
    /// <summary>获取位于只读列表中指定索引处的元素。</summary>
    /// <returns>在只读列表中指定索引处的元素。</returns>
    /// <param name="index">要获取的元素的索引（索引从零开始）。</param>
    [__DynamicallyInvokable]
    T this[int index] { [__DynamicallyInvokable] get; }
  }
}
