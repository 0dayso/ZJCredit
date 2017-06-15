// Decompiled with JetBrains decompiler
// Type: System.Comparison`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>表示比较同一类型的两个对象的方法。</summary>
  /// <returns>一个有符号整数，指示 <paramref name="x" /> 与 <paramref name="y" /> 的相对值，如下表所示。值含义小于 0<paramref name="x" /> 小于 <paramref name="y" />。0<paramref name="x" /> 等于 <paramref name="y" />。大于 0<paramref name="x" /> 大于 <paramref name="y" />。</returns>
  /// <param name="x">要比较的第一个对象。</param>
  /// <param name="y">要比较的第二个对象。</param>
  /// <typeparam name="T">要比较的对象的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public delegate int Comparison<in T>(T x, T y);
}
