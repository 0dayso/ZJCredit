// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IAsyncStateMachine
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>表示为异步方法生成的状态机。此类别仅供编译器使用。</summary>
  [__DynamicallyInvokable]
  public interface IAsyncStateMachine
  {
    /// <summary>移动此状态机至其下一个状态。</summary>
    [__DynamicallyInvokable]
    void MoveNext();

    /// <summary>使用堆分配的副本配置该状态机。</summary>
    /// <param name="stateMachine">堆分配的副本。</param>
    [__DynamicallyInvokable]
    void SetStateMachine(IAsyncStateMachine stateMachine);
  }
}
