// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IReadOnlyCollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>表示元素的强类型的只读集合。</summary>
  /// <typeparam name="T">元素的类型。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
  {
    /// <summary>获取集合中的元素数。</summary>
    /// <returns>集合中的元素数。</returns>
    [__DynamicallyInvokable]
    int Count { [__DynamicallyInvokable] get; }
  }
}
