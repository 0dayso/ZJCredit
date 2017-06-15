// Decompiled with JetBrains decompiler
// Type: System.Func`4
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  /// <summary>封装一个具有三个参数并返回 <paramref name="TResult" /> 参数指定的类型值的方法。</summary>
  /// <returns>此委托封装的方法的返回值。</returns>
  /// <param name="arg1">此委托封装的方法的第一个参数。</param>
  /// <param name="arg2">此委托封装的方法的第二个参数。</param>
  /// <param name="arg3">此委托封装的方法的第三个参数。</param>
  /// <typeparam name="T1">此委托封装的方法的第一个参数类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <typeparam name="T2">此委托封装的方法的第二个参数类型。</typeparam>
  /// <typeparam name="T3">此委托封装的方法的第三个参数类型。</typeparam>
  /// <typeparam name="TResult">此委托封装的方法的返回值类型。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>2</filterpriority>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  public delegate TResult Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
}
