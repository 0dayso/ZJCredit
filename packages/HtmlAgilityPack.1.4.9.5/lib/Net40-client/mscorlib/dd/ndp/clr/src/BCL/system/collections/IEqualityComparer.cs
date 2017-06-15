// Decompiled with JetBrains decompiler
// Type: System.Collections.IEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>定义方法以支持对象的相等比较。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IEqualityComparer
  {
    /// <summary>确定指定的对象是否相等。</summary>
    /// <returns>如果指定的对象相等，则为 true；否则为 false。</returns>
    /// <param name="x">要比较的第一个对象。</param>
    /// <param name="y">要比较的第二个对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="x" /> 和 <paramref name="y" /> 的类型不同，它们都无法处理与另一个进行的比较。</exception>
    [__DynamicallyInvokable]
    bool Equals(object x, object y);

    /// <summary>返回指定对象的哈希代码。</summary>
    /// <returns>指定对象的哈希代码。</returns>
    /// <param name="obj">
    /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 的类型为引用类型，<paramref name="obj" /> 为 null。</exception>
    [__DynamicallyInvokable]
    int GetHashCode(object obj);
  }
}
