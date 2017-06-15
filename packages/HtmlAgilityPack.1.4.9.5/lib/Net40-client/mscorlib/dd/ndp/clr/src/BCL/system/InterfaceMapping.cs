// Decompiled with JetBrains decompiler
// Type: System.Reflection.InterfaceMapping
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>将某个接口的映射检索到实现该接口的类上的实际方法中。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public struct InterfaceMapping
  {
    /// <summary>表示用于创建接口映射的类型。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public Type TargetType;
    /// <summary>显示表示接口的类型。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public Type InterfaceType;
    /// <summary>显示实现接口的方法。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public MethodInfo[] TargetMethods;
    /// <summary>显示在接口上定义的方法。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public MethodInfo[] InterfaceMethods;
  }
}
