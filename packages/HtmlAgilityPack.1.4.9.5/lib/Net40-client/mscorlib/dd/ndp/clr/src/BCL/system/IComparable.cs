// Decompiled with JetBrains decompiler
// Type: System.IComparable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>定义一种特定于类型的通用比较方法，值类型或类通过实现此方法对其实例进行排序。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IComparable
  {
    /// <summary>将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。</summary>
    /// <returns>一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此实例在排序顺序中位于 <paramref name="obj" /> 之前。零此实例在排序顺序中的位置与 <paramref name="obj" /> 相同。大于零此实例在排序顺序中位于 <paramref name="obj" /> 之后。</returns>
    /// <param name="obj">与此实例进行比较的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 不具有与此实例相同的类型。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    int CompareTo(object obj);
  }
}
