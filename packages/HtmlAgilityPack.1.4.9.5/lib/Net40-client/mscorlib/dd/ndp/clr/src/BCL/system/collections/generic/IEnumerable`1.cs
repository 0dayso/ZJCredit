// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IEnumerable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>公开枚举数，该枚举数支持在指定类型的集合上进行简单迭代。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <typeparam name="T">要枚举的对象的类型。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IEnumerable<out T> : IEnumerable
  {
    /// <summary>返回一个循环访问集合的枚举器。</summary>
    /// <returns>用于循环访问集合的枚举数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    IEnumerator<T> GetEnumerator();
  }
}
