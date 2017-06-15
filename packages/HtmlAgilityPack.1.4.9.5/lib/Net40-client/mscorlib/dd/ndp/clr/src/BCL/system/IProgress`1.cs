// Decompiled with JetBrains decompiler
// Type: System.IProgress`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>定义进度更新的提供程序。</summary>
  /// <typeparam name="T">进度更新值的类型。此类型参数是逆变。即可以使用指定的类型或派生程度更低的类型。有关协变和逆变的详细信息，请参阅 泛型中的协变和逆变。</typeparam>
  [__DynamicallyInvokable]
  public interface IProgress<in T>
  {
    /// <summary>报告进度更新。</summary>
    /// <param name="value">进度更新之后的值。</param>
    [__DynamicallyInvokable]
    void Report(T value);
  }
}
