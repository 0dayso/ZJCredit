// Decompiled with JetBrains decompiler
// Type: System.Threading.LazyThreadSafetyMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  /// <summary>指定 <see cref="T:System.Lazy`1" /> 实例如何同步多个线程间的访问。</summary>
  [__DynamicallyInvokable]
  public enum LazyThreadSafetyMode
  {
    [__DynamicallyInvokable] None,
    [__DynamicallyInvokable] PublicationOnly,
    [__DynamicallyInvokable] ExecutionAndPublication,
  }
}
