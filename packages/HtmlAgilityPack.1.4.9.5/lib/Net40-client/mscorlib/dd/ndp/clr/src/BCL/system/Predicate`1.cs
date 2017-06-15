// Decompiled with JetBrains decompiler
// Type: System.Predicate`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>表示定义一组条件并确定指定对象是否符合这些条件的方法。</summary>
  /// <returns>如果 <paramref name="obj" /> 符合由此委托表示的方法中定义的条件，则为 true；否则为 false。</returns>
  /// <param name="obj">要按照由此委托表示的方法中定义的条件进行比较的对象。</param>
  /// <typeparam name="T">要比较的对象的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public delegate bool Predicate<in T>(T obj);
}
