// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IEnumerator`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>支持在泛型集合上进行简单迭代。</summary>
  /// <typeparam name="T">要枚举的对象的类型。此类型参数是协变。即可以使用指定的类型或派生程度更高的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public interface IEnumerator<out T> : IDisposable, IEnumerator
  {
    /// <summary>获取集合中位于枚举数当前位置的元素。</summary>
    /// <returns>集合中位于枚举数当前位置的元素。</returns>
    [__DynamicallyInvokable]
    T Current { [__DynamicallyInvokable] get; }
  }
}
