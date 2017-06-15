// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncStateMachineAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指示是否使用 Async (Visual Basic) 或 async（C# 参考） 修饰符标记方法。</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AsyncStateMachineAttribute : StateMachineAttribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.AsyncStateMachineAttribute" /> 类的新实例。</summary>
    /// <param name="stateMachineType">用于实现状态机方法的基础状态机类型的类型对象。</param>
    [__DynamicallyInvokable]
    public AsyncStateMachineAttribute(Type stateMachineType)
      : base(stateMachineType)
    {
    }
  }
}
