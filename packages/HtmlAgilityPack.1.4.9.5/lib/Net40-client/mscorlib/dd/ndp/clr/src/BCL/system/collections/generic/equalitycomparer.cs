// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.EqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
  /// <summary>为 <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> 泛型接口的实现提供基类。</summary>
  /// <typeparam name="T">要比较的对象的类型。</typeparam>
  [TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
  {
    private static volatile EqualityComparer<T> defaultComparer;

    /// <summary>返回一个默认的相等比较器，用于比较此泛型参数指定的类型。</summary>
    /// <returns>用于类型 <paramref name="T" /> 的 <see cref="T:System.Collections.Generic.EqualityComparer`1" /> 类的默认实例。</returns>
    [__DynamicallyInvokable]
    public static EqualityComparer<T> Default
    {
      [__DynamicallyInvokable] get
      {
        EqualityComparer<T> equalityComparer = EqualityComparer<T>.defaultComparer;
        if (equalityComparer == null)
        {
          equalityComparer = EqualityComparer<T>.CreateComparer();
          EqualityComparer<T>.defaultComparer = equalityComparer;
        }
        return equalityComparer;
      }
    }

    /// <summary>初始化 <see cref="T:System.Collections.Generic.EqualityComparer`1" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected EqualityComparer()
    {
    }

    [SecuritySafeCritical]
    private static EqualityComparer<T> CreateComparer()
    {
      RuntimeType genericParameter1 = (RuntimeType) typeof (T);
      if ((Type) genericParameter1 == typeof (byte))
        return (EqualityComparer<T>) new ByteEqualityComparer();
      if (typeof (IEquatable<T>).IsAssignableFrom((Type) genericParameter1))
        return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (GenericEqualityComparer<int>), genericParameter1);
      if (genericParameter1.IsGenericType && genericParameter1.GetGenericTypeDefinition() == typeof (Nullable<>))
      {
        RuntimeType genericParameter2 = (RuntimeType) genericParameter1.GetGenericArguments()[0];
        if (typeof (IEquatable<>).MakeGenericType((Type) genericParameter2).IsAssignableFrom((Type) genericParameter2))
          return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (NullableEqualityComparer<int>), genericParameter2);
      }
      if (genericParameter1.IsEnum)
      {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType((Type) genericParameter1)))
        {
          case TypeCode.SByte:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (SByteEnumEqualityComparer<sbyte>), genericParameter1);
          case TypeCode.Byte:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (EnumEqualityComparer<int>), genericParameter1);
          case TypeCode.Int16:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (ShortEnumEqualityComparer<short>), genericParameter1);
          case TypeCode.Int64:
          case TypeCode.UInt64:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (LongEnumEqualityComparer<long>), genericParameter1);
        }
      }
      return (EqualityComparer<T>) new ObjectEqualityComparer<T>();
    }

    /// <summary>在派生类中重写时，确定类型 <paramref name="T" /> 的两个对象是否相等。</summary>
    /// <returns>如果指定的对象相等，则为 true；否则为 false。</returns>
    /// <param name="x">要比较的第一个对象。</param>
    /// <param name="y">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public abstract bool Equals(T x, T y);

    /// <summary>在派生类中重写时，用作指定对象的哈希算法和数据结构（如哈希表）的哈希函数。</summary>
    /// <returns>指定对象的哈希代码。</returns>
    /// <param name="obj">要为其获取哈希代码的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
    [__DynamicallyInvokable]
    public abstract int GetHashCode(T obj);

    internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
    {
      int num = startIndex + count;
      for (int index = startIndex; index < num; ++index)
      {
        if (this.Equals(array[index], value))
          return index;
      }
      return -1;
    }

    internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
    {
      int num = startIndex - count + 1;
      for (int index = startIndex; index >= num; --index)
      {
        if (this.Equals(array[index], value))
          return index;
      }
      return -1;
    }

    [__DynamicallyInvokable]
    int IEqualityComparer.GetHashCode(object obj)
    {
      if (obj == null)
        return 0;
      if (obj is T)
        return this.GetHashCode((T) obj);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return 0;
    }

    [__DynamicallyInvokable]
    bool IEqualityComparer.Equals(object x, object y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      if (x is T && y is T)
        return this.Equals((T) x, (T) y);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return false;
    }
  }
}
