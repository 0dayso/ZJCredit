// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeReflectionExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Reflection
{
  /// <summary>提供检索有关运行时类型的信息的方法。</summary>
  [__DynamicallyInvokable]
  public static class RuntimeReflectionExtensions
  {
    private const BindingFlags everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    private static void CheckAndThrow(Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException("type");
      if (!(t is RuntimeType))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
    }

    private static void CheckAndThrow(MethodInfo m)
    {
      if (m == (MethodInfo) null)
        throw new ArgumentNullException("method");
      if (!(m is RuntimeMethodInfo))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
    }

    /// <summary>检索表示指定类型定义的所有属性的集合。</summary>
    /// <returns>指定类型的属性集合。</returns>
    /// <param name="type">包含属性的类型。</param>
    [__DynamicallyInvokable]
    public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>检索表示指定类型定义的所有事件的集合。</summary>
    /// <returns>指定类型的事件集合。</returns>
    /// <param name="type">包含该事件的类型。</param>
    [__DynamicallyInvokable]
    public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<EventInfo>) type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>检索表示指定类型定义的所有方法的集合。</summary>
    /// <returns>指定类型的方法集合。</returns>
    /// <param name="type">包含方法的类型。</param>
    [__DynamicallyInvokable]
    public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<MethodInfo>) type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>检索表示指定类型定义的所有字段的集合。</summary>
    /// <returns>指定类型的字段集合。</returns>
    /// <param name="type">包含字段的类型。</param>
    [__DynamicallyInvokable]
    public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>检索表示指定属性的对象。</summary>
    /// <returns>表示指定属性的对象，若未找到该属性则为 null。</returns>
    /// <param name="type">包含该属性的类型。</param>
    /// <param name="name">属性的名称。</param>
    [__DynamicallyInvokable]
    public static PropertyInfo GetRuntimeProperty(this Type type, string name)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetProperty(name);
    }

    /// <summary>检索一个表示指定事件的对象。</summary>
    /// <returns>表示指定事件的对象，若未找到该事件则为 null。</returns>
    /// <param name="type">包含该事件的类型。</param>
    /// <param name="name">事件的名称。</param>
    [__DynamicallyInvokable]
    public static EventInfo GetRuntimeEvent(this Type type, string name)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetEvent(name);
    }

    /// <summary>检索表示指定方法的对象。</summary>
    /// <returns>表示指定方法的对象，若未找到该方法则为 null。</returns>
    /// <param name="type">包含方法的类型。</param>
    /// <param name="name">方法的名称。</param>
    /// <param name="parameters">包含方法的参数的数组。</param>
    [__DynamicallyInvokable]
    public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetMethod(name, parameters);
    }

    /// <summary>检索表示指定字段的对象。</summary>
    /// <returns>表示指定字段的对象，若未找到该字段则为 null。</returns>
    /// <param name="type">包含字段的类型。</param>
    /// <param name="name">字段名。</param>
    [__DynamicallyInvokable]
    public static FieldInfo GetRuntimeField(this Type type, string name)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetField(name);
    }

    /// <summary>检索表示在此方法最先声明的直接或间接类上的指定方法的对象。</summary>
    /// <returns>表示在基类中指定的方法的初始声明的对象。</returns>
    /// <param name="method">关于检索信息的方法。</param>
    [__DynamicallyInvokable]
    public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
    {
      RuntimeReflectionExtensions.CheckAndThrow(method);
      return method.GetBaseDefinition();
    }

    /// <summary>返回指定类型和指定接口的接口映射。</summary>
    /// <returns>表示指定接口和类型的接口映射的对象。</returns>
    /// <param name="typeInfo">要检索其映射的类型。</param>
    /// <param name="interfaceType">要检索其映射的接口。</param>
    [__DynamicallyInvokable]
    public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
    {
      if ((Type) typeInfo == (Type) null)
        throw new ArgumentNullException("typeInfo");
      if (!(typeInfo is RuntimeType))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return typeInfo.GetInterfaceMap(interfaceType);
    }

    /// <summary>获取指示指定委托表示的方法的对象。</summary>
    /// <returns>表示该方法的对象。</returns>
    /// <param name="del">要检查的委托。</param>
    [__DynamicallyInvokable]
    public static MethodInfo GetMethodInfo(this Delegate del)
    {
      if (del == null)
        throw new ArgumentNullException("del");
      return del.Method;
    }
  }
}
