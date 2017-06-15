// Decompiled with JetBrains decompiler
// Type: System.Tuple
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>提供用于创造元组对象的静态方法。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  [__DynamicallyInvokable]
  public static class Tuple
  {
    /// <summary>创建新的 1 元组，即单一实例。</summary>
    /// <returns>值为 (<paramref name="item1" />) 的元组。</returns>
    /// <param name="item1">元组仅有的分量的值。</param>
    /// <typeparam name="T1">元组的唯一一个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1> Create<T1>(T1 item1)
    {
      return new Tuple<T1>(item1);
    }

    /// <summary>创建新的 2 元组，即二元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />) 的 2 元组。</returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
      return new Tuple<T1, T2>(item1, item2);
    }

    /// <summary>创建新的 3 元组，即三元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />) 的 3 元组。</returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <param name="item3">此元组的第三个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    /// <typeparam name="T3">元组的第三个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
    {
      return new Tuple<T1, T2, T3>(item1, item2, item3);
    }

    /// <summary>创建新的 4 元组，即四元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />) 的 4 元组。</returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <param name="item3">此元组的第三个分量的值。</param>
    /// <param name="item4">此元组的第四个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    /// <typeparam name="T3">元组的第三个分量的类型。</typeparam>
    /// <typeparam name="T4">此元组的第四个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
    }

    /// <summary>创建新的 5 元组，即五元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />) 的 5 元组。</returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <param name="item3">此元组的第三个分量的值。</param>
    /// <param name="item4">此元组的第四个分量的值。</param>
    /// <param name="item5">此元组的第五个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    /// <typeparam name="T3">元组的第三个分量的类型。</typeparam>
    /// <typeparam name="T4">此元组的第四个分量的类型。</typeparam>
    /// <typeparam name="T5">此元组的第五个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
    }

    /// <summary>创建新的 6 元组，即六元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />, <paramref name="item6" />) 的 6 元组。</returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <param name="item3">此元组的第三个分量的值。</param>
    /// <param name="item4">此元组的第四个分量的值。</param>
    /// <param name="item5">此元组的第五个分量的值。</param>
    /// <param name="item6">此元组的第六个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    /// <typeparam name="T3">元组的第三个分量的类型。</typeparam>
    /// <typeparam name="T4">此元组的第四个分量的类型。</typeparam>
    /// <typeparam name="T5">此元组的第五个分量的类型。</typeparam>
    /// <typeparam name="T6">此元组的第六个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
    }

    /// <summary>创建新的 7 元组，即七元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />, <paramref name="item6" />, <paramref name="item7" />)的 7 元组。</returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <param name="item3">此元组的第三个分量的值。</param>
    /// <param name="item4">此元组的第四个分量的值。</param>
    /// <param name="item5">此元组的第五个分量的值。</param>
    /// <param name="item6">此元组的第六个分量的值。</param>
    /// <param name="item7">元组的第七个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    /// <typeparam name="T3">元组的第三个分量的类型。</typeparam>
    /// <typeparam name="T4">此元组的第四个分量的类型。</typeparam>
    /// <typeparam name="T5">此元组的第五个分量的类型。</typeparam>
    /// <typeparam name="T6">此元组的第六个分量的类型。</typeparam>
    /// <typeparam name="T7">元组的第七个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
    }

    /// <summary>创建新的 8 元组，即八元组。</summary>
    /// <returns>值为 (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />, <paramref name="item6" />, <paramref name="item7" />, <paramref name="item8" />) 的 8 元组（八元组）。 </returns>
    /// <param name="item1">此元组的第一个分量的值。</param>
    /// <param name="item2">此元组的第二个分量的值。</param>
    /// <param name="item3">此元组的第三个分量的值。</param>
    /// <param name="item4">此元组的第四个分量的值。</param>
    /// <param name="item5">此元组的第五个分量的值。</param>
    /// <param name="item6">此元组的第六个分量的值。</param>
    /// <param name="item7">元组的第七个分量的值。</param>
    /// <param name="item8">元组的第八个分量的值。</param>
    /// <typeparam name="T1">此元组的第一个分量的类型。</typeparam>
    /// <typeparam name="T2">元组的第二个分量的类型。</typeparam>
    /// <typeparam name="T3">元组的第三个分量的类型。</typeparam>
    /// <typeparam name="T4">此元组的第四个分量的类型。</typeparam>
    /// <typeparam name="T5">此元组的第五个分量的类型。</typeparam>
    /// <typeparam name="T6">此元组的第六个分量的类型。</typeparam>
    /// <typeparam name="T7">元组的第七个分量的类型。</typeparam>
    /// <typeparam name="T8">元组的第八个分量的类型。</typeparam>
    [__DynamicallyInvokable]
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
    {
      return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(item1, item2, item3, item4, item5, item6, item7, new Tuple<T8>(item8));
    }

    internal static int CombineHashCodes(int h1, int h2)
    {
      return (h1 << 5) + h1 ^ h2;
    }

    internal static int CombineHashCodes(int h1, int h2, int h3)
    {
      return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), h3);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
    {
      return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), Tuple.CombineHashCodes(h3, h4));
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
    {
      return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), h5);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
    {
      return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6));
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
    {
      return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7));
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
    {
      return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7, h8));
    }
  }
}
