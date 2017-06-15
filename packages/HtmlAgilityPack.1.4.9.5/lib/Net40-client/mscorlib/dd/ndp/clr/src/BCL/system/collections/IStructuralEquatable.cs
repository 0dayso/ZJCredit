// Decompiled with JetBrains decompiler
// Type: System.Collections.IStructuralEquatable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  /// <summary>定义方法以支持对象的结构相等性比较。</summary>
  [__DynamicallyInvokable]
  public interface IStructuralEquatable
  {
    /// <summary>确定某个对象与当前实例在结构上是否相等。</summary>
    /// <returns>如果两个对象相等，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前实例进行比较的对象。</param>
    /// <param name="comparer">一个可确定当前实例与 <paramref name="other" /> 是否相等的对象。</param>
    [__DynamicallyInvokable]
    bool Equals(object other, IEqualityComparer comparer);

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>当前实例的哈希代码。</returns>
    /// <param name="comparer">一个计算当前对象的哈希代码的对象。</param>
    [__DynamicallyInvokable]
    int GetHashCode(IEqualityComparer comparer);
  }
}
