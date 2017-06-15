// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComSourceInterfacesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>为属性化类标识公开为 COM 事件源的一组接口。</summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComSourceInterfacesAttribute : Attribute
  {
    internal string _val;

    /// <summary>获取事件源接口的完全限定名。</summary>
    /// <returns>事件源接口的完全限定名。</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>使用事件源接口的名称初始化 <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> 类的新实例。</summary>
    /// <param name="sourceInterfaces">事件源接口的完全限定名列表，名称之间用 null 分隔。</param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(string sourceInterfaces)
    {
      this._val = sourceInterfaces;
    }

    /// <summary>使用要用作源接口的类型初始化 <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> 类的新实例。</summary>
    /// <param name="sourceInterface">源接口的 <see cref="T:System.Type" />。</param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface)
    {
      this._val = sourceInterface.FullName;
    }

    /// <summary>在以要使用的类型作为源接口的情况下初始化 <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> 类的新实例。</summary>
    /// <param name="sourceInterface1">默认源接口的 <see cref="T:System.Type" />。</param>
    /// <param name="sourceInterface2">源接口的 <see cref="T:System.Type" />。</param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
    {
      this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
    }

    /// <summary>在以要使用的类型作为源接口的情况下初始化 ComSourceInterfacesAttribute 类的新实例。</summary>
    /// <param name="sourceInterface1">默认源接口的 <see cref="T:System.Type" />。</param>
    /// <param name="sourceInterface2">源接口的 <see cref="T:System.Type" />。</param>
    /// <param name="sourceInterface3">源接口的 <see cref="T:System.Type" />。</param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
    {
      this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName + "\0" + sourceInterface3.FullName;
    }

    /// <summary>在以要使用的类型作为源接口的情况下初始化 <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> 类的新实例。</summary>
    /// <param name="sourceInterface1">默认源接口的 <see cref="T:System.Type" />。</param>
    /// <param name="sourceInterface2">源接口的 <see cref="T:System.Type" />。</param>
    /// <param name="sourceInterface3">源接口的 <see cref="T:System.Type" />。</param>
    /// <param name="sourceInterface4">源接口的 <see cref="T:System.Type" />。</param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
    {
      this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName + "\0" + sourceInterface3.FullName + "\0" + sourceInterface4.FullName;
    }
  }
}
