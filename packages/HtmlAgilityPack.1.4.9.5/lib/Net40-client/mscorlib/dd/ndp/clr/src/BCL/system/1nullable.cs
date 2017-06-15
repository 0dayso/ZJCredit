// Decompiled with JetBrains decompiler
// Type: System.Nullable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>支持可分配有 null 的值类型。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Nullable
  {
    /// <summary>比较两个 <see cref="T:System.Nullable`1" /> 对象的相对值。</summary>
    /// <returns>一个整数，指示 <paramref name="n1" /> 和 <paramref name="n2" /> 参数的相对值。返回值描述小于零<paramref name="n1" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false，<paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true。- 或 -<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且 <paramref name="n1" /> 的 <see cref="P:System.Nullable`1.Value" /> 属性的值小于 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.Value" /> 属性的值。零<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false。- 或 -<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且 <paramref name="n1" /> 的 <see cref="P:System.Nullable`1.Value" /> 属性的值等于 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.Value" /> 属性的值。大于零<paramref name="n1" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false。- 或 -<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且 <paramref name="n1" /> 的 <see cref="P:System.Nullable`1.Value" /> 属性的值大于 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.Value" /> 属性的值。</returns>
    /// <param name="n1">
    /// <see cref="T:System.Nullable`1" /> 对象。</param>
    /// <param name="n2">
    /// <see cref="T:System.Nullable`1" /> 对象。</param>
    /// <typeparam name="T">
    /// <paramref name="n1" /> 和 <paramref name="n2" /> 参数的基础值类型。</typeparam>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static int Compare<T>(T? n1, T? n2) where T : struct
    {
      if (n1.HasValue)
      {
        if (n2.HasValue)
          return Comparer<T>.Default.Compare(n1.value, n2.value);
        return 1;
      }
      return n2.HasValue ? -1 : 0;
    }

    /// <summary>指示两个指定的 <see cref="T:System.Nullable`1" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="n1" /> 参数等于 <paramref name="n2" /> 参数，则为 true；否则为 false。返回值取决于所比较的两个参数的 <see cref="P:System.Nullable`1.HasValue" /> 和 <see cref="P:System.Nullable`1.Value" /> 属性。返回值描述true<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false。- 或 -<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且参数的 <see cref="P:System.Nullable`1.Value" /> 属性相等。false一个参数的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，另一个参数的该属性为 false。- 或 -<paramref name="n1" /> 和 <paramref name="n2" /> 的 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且参数的 <see cref="P:System.Nullable`1.Value" /> 属性不相等。</returns>
    /// <param name="n1">
    /// <see cref="T:System.Nullable`1" /> 对象。</param>
    /// <param name="n2">
    /// <see cref="T:System.Nullable`1" /> 对象。</param>
    /// <typeparam name="T">
    /// <paramref name="n1" /> 和 <paramref name="n2" /> 参数的基础值类型。</typeparam>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static bool Equals<T>(T? n1, T? n2) where T : struct
    {
      if (n1.HasValue)
      {
        if (n2.HasValue)
          return EqualityComparer<T>.Default.Equals(n1.value, n2.value);
        return false;
      }
      return !n2.HasValue;
    }

    /// <summary>返回指定可以为 null 的类型的基础类型参数。</summary>
    /// <returns>如果 <paramref name="nullableType" /> 参数为关闭的泛型可以为 null 的类型，则为 <paramref name="nullableType" /> 参数的类型变量；否则为 null。</returns>
    /// <param name="nullableType">一个 <see cref="T:System.Type" /> 对象，描述关闭的泛型可以为 null 的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="nullableType" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static Type GetUnderlyingType(Type nullableType)
    {
      if (nullableType == null)
        throw new ArgumentNullException("nullableType");
      Type type = (Type) null;
      if (nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition && nullableType.GetGenericTypeDefinition() == typeof (Nullable<>))
        type = nullableType.GetGenericArguments()[0];
      return type;
    }
  }
}
