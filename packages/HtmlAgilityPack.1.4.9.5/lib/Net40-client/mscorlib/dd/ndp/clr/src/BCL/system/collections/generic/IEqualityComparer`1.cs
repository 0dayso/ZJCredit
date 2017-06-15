// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>定义方法以支持对象的相等比较。</summary>
  /// <typeparam name="T">要比较的对象的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  [__DynamicallyInvokable]
  public interface IEqualityComparer<in T>
  {
    /// <summary>确定指定的对象是否相等。</summary>
    /// <returns>如果指定的对象相等，则为 true；否则为 false。</returns>
    /// <param name="x">要比较的第一个类型为 <paramref name="T" /> 的对象。</param>
    /// <param name="y">要比较的第二个类型为 <paramref name="T" /> 的对象。</param>
    [__DynamicallyInvokable]
    bool Equals(T x, T y);

    /// <summary>返回指定对象的哈希代码。</summary>
    /// <returns>指定对象的哈希代码。</returns>
    /// <param name="obj">
    /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 的类型为引用类型，<paramref name="obj" /> 为 null。</exception>
    [__DynamicallyInvokable]
    int GetHashCode(T obj);
  }
}
