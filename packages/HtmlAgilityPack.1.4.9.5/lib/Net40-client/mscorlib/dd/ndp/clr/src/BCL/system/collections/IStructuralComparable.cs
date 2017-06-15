// Decompiled with JetBrains decompiler
// Type: System.Collections.IStructuralComparable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  /// <summary>支持集合对象的结构比较。</summary>
  [__DynamicallyInvokable]
  public interface IStructuralComparable
  {
    /// <summary>确定当前集合对象在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。</summary>
    /// <returns>一个指示当前集合对象与 <paramref name="other" /> 的关系的整数，如下表所示。返回值说明-1当前实例位于 <paramref name="other" /> 之前。0当前实例与 <paramref name="other" /> 位于同一位置。1当前实例位于 <paramref name="other" /> 之后。</returns>
    /// <param name="other">要与当前实例进行比较的对象。</param>
    /// <param name="comparer">一个将当前集合对象的成员与 <paramref name="other" /> 的对应成员进行比较的对象。</param>
    /// <exception cref="T:System.ArgumentException">此实例与 <paramref name="other" /> 不是同一类型。</exception>
    [__DynamicallyInvokable]
    int CompareTo(object other, IComparer comparer);
  }
}
