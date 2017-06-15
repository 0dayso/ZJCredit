// Decompiled with JetBrains decompiler
// Type: System.IEquatable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>定义一个通用的方法，由值类型或类实现以创建类型特定的方法，用于确定实例间的相等性。</summary>
  /// <typeparam name="T">要比较的对象的类型。</typeparam>
  [__DynamicallyInvokable]
  public interface IEquatable<T>
  {
    /// <summary>指示当前对象是否等于同一类型的另一个对象。</summary>
    /// <returns>如果当前对象等于 <paramref name="other" /> 参数，则为 true；否则为 false。</returns>
    /// <param name="other">与此对象进行比较的对象。</param>
    [__DynamicallyInvokable]
    bool Equals(T other);
  }
}
