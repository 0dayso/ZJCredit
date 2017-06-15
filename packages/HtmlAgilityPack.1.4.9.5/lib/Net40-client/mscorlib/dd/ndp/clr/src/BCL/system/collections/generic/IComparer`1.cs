// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>定义类型为比较两个对象而实现的方法。</summary>
  /// <typeparam name="T">要比较的对象的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public interface IComparer<in T>
  {
    /// <summary>比较两个对象并返回一个值，该值指示一个对象小于、等于还是大于另一个对象。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="x" /> 与 <paramref name="y" /> 的相对值，如下表所示。值含义小于零<paramref name="x" /> 小于 <paramref name="y" />。零<paramref name="x" /> 等于 <paramref name="y" />。大于零<paramref name="x" /> 大于 <paramref name="y" />。</returns>
    /// <param name="x">要比较的第一个对象。</param>
    /// <param name="y">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    int Compare(T x, T y);
  }
}
