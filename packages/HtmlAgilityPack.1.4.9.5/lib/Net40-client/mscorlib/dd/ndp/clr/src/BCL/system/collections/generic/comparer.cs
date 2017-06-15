// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Comparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
  /// <summary>为 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口的实现提供基类。</summary>
  /// <typeparam name="T">要比较的对象的类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [TypeDependency("System.Collections.Generic.ObjectComparer`1")]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Comparer<T> : IComparer, IComparer<T>
  {
    private static volatile Comparer<T> defaultComparer;

    /// <summary>返回由泛型参数指定的类型的默认排序顺序比较器。</summary>
    /// <returns>继承 <see cref="T:System.Collections.Generic.Comparer`1" /> 并作为 <paramref name="T" /> 类型的排序顺序比较器的对象。</returns>
    [__DynamicallyInvokable]
    public static Comparer<T> Default
    {
      [__DynamicallyInvokable] get
      {
        Comparer<T> comparer = Comparer<T>.defaultComparer;
        if (comparer == null)
        {
          comparer = Comparer<T>.CreateComparer();
          Comparer<T>.defaultComparer = comparer;
        }
        return comparer;
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.Comparer`1" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Comparer()
    {
    }

    /// <summary>用指定的比较创建一个比较器。 </summary>
    /// <returns>新的比较器。</returns>
    /// <param name="comparison">要使用的比较。</param>
    [__DynamicallyInvokable]
    public static Comparer<T> Create(Comparison<T> comparison)
    {
      if (comparison == null)
        throw new ArgumentNullException("comparison");
      return (Comparer<T>) new ComparisonComparer<T>(comparison);
    }

    [SecuritySafeCritical]
    private static Comparer<T> CreateComparer()
    {
      RuntimeType genericParameter1 = (RuntimeType) typeof (T);
      if (typeof (IComparable<T>).IsAssignableFrom((Type) genericParameter1))
        return (Comparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (GenericComparer<int>), genericParameter1);
      if (genericParameter1.IsGenericType && genericParameter1.GetGenericTypeDefinition() == typeof (Nullable<>))
      {
        RuntimeType genericParameter2 = (RuntimeType) genericParameter1.GetGenericArguments()[0];
        if (typeof (IComparable<>).MakeGenericType((Type) genericParameter2).IsAssignableFrom((Type) genericParameter2))
          return (Comparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (NullableComparer<int>), genericParameter2);
      }
      return (Comparer<T>) new ObjectComparer<T>();
    }

    /// <summary>在派生类中重写时，对同一类型的两个对象执行比较并返回一个值，指示一个对象是小于、等于还是大于另一个对象。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="x" /> 与 <paramref name="y" /> 的相对值，如下表所示。值含义小于零<paramref name="x" /> 小于 <paramref name="y" />。零<paramref name="x" /> 等于 <paramref name="y" />。大于零<paramref name="x" /> 大于 <paramref name="y" />。</returns>
    /// <param name="x">要比较的第一个对象。</param>
    /// <param name="y">要比较的第二个对象。</param>
    /// <exception cref="T:System.ArgumentException">类型 <paramref name="T" /> 没有实现 <see cref="T:System.IComparable`1" /> 泛型接口或 <see cref="T:System.IComparable" /> 接口。</exception>
    [__DynamicallyInvokable]
    public abstract int Compare(T x, T y);

    [__DynamicallyInvokable]
    int IComparer.Compare(object x, object y)
    {
      if (x == null)
        return y != null ? -1 : 0;
      if (y == null)
        return 1;
      if (x is T && y is T)
        return this.Compare((T) x, (T) y);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return 0;
    }
  }
}
