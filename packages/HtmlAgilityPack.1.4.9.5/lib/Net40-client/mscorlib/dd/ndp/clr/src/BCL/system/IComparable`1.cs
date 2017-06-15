// Decompiled with JetBrains decompiler
// Type: System.IComparable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>定义由值类型或类实现的通用比较方法，以为排序实例创建类型特定的比较方法。</summary>
  /// <typeparam name="T">要比较的对象的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public interface IComparable<in T>
  {
    /// <summary>将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。</summary>
    /// <returns>一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此实例在排序顺序中位于 <paramref name="other" /> 之前。零此实例在排序顺序中的位置与 <paramref name="other" /> 相同。大于零此实例在排序顺序中位于 <paramref name="other" /> 之后。 </returns>
    /// <param name="other">与此实例进行比较的对象。</param>
    [__DynamicallyInvokable]
    int CompareTo(T other);
  }
}
