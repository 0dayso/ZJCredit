// Decompiled with JetBrains decompiler
// Type: System.Reflection.ReflectionContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>表示可提供反射对象的上下文。</summary>
  [__DynamicallyInvokable]
  public abstract class ReflectionContext
  {
    /// <summary>初始化 <see cref="T:System.Reflection.ReflectionContext" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected ReflectionContext()
    {
    }

    /// <summary>在本反射上下文中，获取由另一个反射上下文表示的程序集的表示形式。</summary>
    /// <returns>在此反射上下文中的程序集合的表示。</returns>
    /// <param name="assembly">用来在该上下文中表示的程序集的外部表示。</param>
    [__DynamicallyInvokable]
    public abstract Assembly MapAssembly(Assembly assembly);

    /// <summary>在本反射上下文中，获取由另一个反射上下文表示的类型的表示形式。</summary>
    /// <returns>在此反射上下文中的类型的表示。</returns>
    /// <param name="type">用来在该上下文中表示的类型的外部表示。</param>
    [__DynamicallyInvokable]
    public abstract TypeInfo MapType(TypeInfo type);

    /// <summary>获取本反射上下文中特定对象的类的表示形式。</summary>
    /// <returns>一个对象，表示指定对象的类型。。</returns>
    /// <param name="value">要表示的对象。</param>
    [__DynamicallyInvokable]
    public virtual TypeInfo GetTypeForObject(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return this.MapType(value.GetType().GetTypeInfo());
    }
  }
}
