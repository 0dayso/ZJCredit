// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.StateMachineAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>允许您确定一个方法是否为状态机方法。</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StateMachineAttribute : Attribute
  {
    /// <summary>返回编译器生成的实现状态机方法的基础状态机类型的类型对象。</summary>
    /// <returns>获取这用于执行状态计算机方法生成的编译器基础状态的计算机的类型对象。</returns>
    [__DynamicallyInvokable]
    public Type StateMachineType { [__DynamicallyInvokable] get; private set; }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.StateMachineAttribute" /> 类的新实例。</summary>
    /// <param name="stateMachineType">这用于执行状态计算机方法生成的编译器基础状态的计算机的类型对象。</param>
    [__DynamicallyInvokable]
    public StateMachineAttribute(Type stateMachineType)
    {
      this.StateMachineType = stateMachineType;
    }
  }
}
