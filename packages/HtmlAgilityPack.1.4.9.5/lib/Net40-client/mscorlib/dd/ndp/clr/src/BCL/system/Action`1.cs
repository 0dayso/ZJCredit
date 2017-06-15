// Decompiled with JetBrains decompiler
// Type: System.Action`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>封装一个方法，该方法只有一个参数并且不返回值。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <param name="obj">此委托封装的方法的参数。</param>
  /// <typeparam name="T">此委托封装的方法的参数类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public delegate void Action<in T>(T obj);
}
