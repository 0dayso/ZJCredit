// Decompiled with JetBrains decompiler
// Type: System.Converter`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>表示将对象从一种类型转换为另一种类型的方法。</summary>
  /// <returns>
  /// <paramref name="TOutput" />，它表示已转换的 <paramref name="TInput" />。</returns>
  /// <param name="input">要转换的对象。</param>
  /// <typeparam name="TInput">要转换的对象的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <typeparam name="TOutput">要将输入对象转换到的类型。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
}
