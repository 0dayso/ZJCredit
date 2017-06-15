// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Reflection
{
  /// <summary>包含检索自定义特性的静态方法。</summary>
  [__DynamicallyInvokable]
  public static class CustomAttributeExtensions
  {
    /// <summary>检索应用于指定程序集的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="attributeType" /> 匹配的自定义特性，如果未找到此类特性，则为 null。</returns>
    /// <param name="element">要检查的程序集。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>检索应用于指定模块的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="attributeType" /> 匹配的自定义特性，如果未找到此类特性，则为 null。</returns>
    /// <param name="element">要检查的模块。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this Module element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="attributeType" /> 匹配的自定义特性，如果未找到此类特性，则为 null。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="attributeType" /> 匹配的自定义特性，如果未找到此类特性，则为 null。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>检索应用于指定程序集的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="T" /> 相匹配的自定义属性；否则，如果没有找到这类属性，则为 null。</returns>
    /// <param name="element">要检查的程序集。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>检索应用于指定模块的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="T" /> 相匹配的自定义属性；否则，如果没有找到这类属性，则为 null。</returns>
    /// <param name="element">要检查的模块。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this Module element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="T" /> 相匹配的自定义属性；否则，如果没有找到这类属性，则为 null。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性。</summary>
    /// <returns>与 <paramref name="T" /> 相匹配的自定义属性；否则，如果没有找到这类属性，则为 null。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性，并可选择检查该成员的上级。</summary>
    /// <returns>与 <paramref name="attributeType" /> 匹配的自定义特性，如果未找到此类特性，则为 null。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
    {
      return Attribute.GetCustomAttribute(element, attributeType, inherit);
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性，并可选择检查该参数的上级。</summary>
    /// <returns>匹配 <paramref name="attributeType" /> 的自定义特性，如果未找到此类特性，则为 null。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
    {
      return Attribute.GetCustomAttribute(element, attributeType, inherit);
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性，并可选择检查该成员的上级。</summary>
    /// <returns>与 <paramref name="T" /> 相匹配的自定义属性；否则，如果没有找到这类属性，则为 null。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T), inherit);
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性，并可选择检查该参数的上级。</summary>
    /// <returns>与 <paramref name="T" /> 相匹配的自定义属性；否则，如果没有找到这类属性，则为 null。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T), inherit);
    }

    /// <summary>检索应用于指定程序集的自定义特性集合。</summary>
    /// <returns>将应用于 <paramref name="element" /> 的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>检索应用于指定模块的自定义特性集合。</summary>
    /// <returns>将应用于 <paramref name="element" /> 的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的模块。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>检索应用于指定成员的自定义特性集合。</summary>
    /// <returns>将应用于 <paramref name="element" /> 的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>检索应用于指定参数的自定义特性的集合。</summary>
    /// <returns>将应用于 <paramref name="element" /> 的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>检索应用于指定成员的自定义特性集合，并可选择检查该成员的上级。</summary>
    /// <returns>将应用于与指定的条件匹配的 <paramref name="element" /> 的自定义特性的集合，如果此类特性不存在，则为空集。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, inherit);
    }

    /// <summary>检索应用于指定参数的自定义特性集合，并可选择检查该参数的上级。</summary>
    /// <returns>将应用于 <paramref name="element" /> 的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, inherit);
    }

    /// <summary>检索应用于指定程序集的指定类型的自定义特性集合</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="attributeType" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的程序集。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>检索应用于指定模块的指定类型的自定义特性集合。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="attributeType" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的模块。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性集合。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="attributeType" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性集合。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="attributeType" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>检索应用于指定程序集的指定类型的自定义特性集合</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="T" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的程序集。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>检索应用于指定模块的指定类型的自定义特性集合。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="T" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的模块。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性集合。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="T" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性集合。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="T" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性集合，并可选择检查该成员的上级。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="attributeType" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType, inherit);
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性集合，并可选择检查该参数的上级。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="attributeType" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType, inherit);
    }

    /// <summary>检索应用于指定成员的指定类型的自定义特性集合，并可选择检查该成员的上级。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="T" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
    {
      return (IEnumerable<T>) CustomAttributeExtensions.GetCustomAttributes(element, typeof (T), inherit);
    }

    /// <summary>检索应用于指定参数的指定类型的自定义特性集合，并可选择检查该参数的上级。</summary>
    /// <returns>将应用于与 <paramref name="element" /> 并与 <paramref name="T" /> 匹配的自定义特性的集合，如果此类特性不存在，则为空集合。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <typeparam name="T">要搜索的特性类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
    {
      return (IEnumerable<T>) CustomAttributeExtensions.GetCustomAttributes(element, typeof (T), inherit);
    }

    /// <summary>确定是否将指定类型的任何自定义属性应用于指定的程序集。</summary>
    /// <returns>如果将指定类型的特性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">要检查的程序集。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this Assembly element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>确定是否将指定类型的任何自定义属性应用于指定的模块。</summary>
    /// <returns>如果将指定类型的特性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">要检查的模块。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this Module element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>确定是否将指定类型的任何自定义属性应用于指定的成员。</summary>
    /// <returns>如果将指定类型的特性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this MemberInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>确定是否将指定类型的任何自定义属性应用于指定的参数。</summary>
    /// <returns>如果将指定类型的特性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this ParameterInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>指示一个指定类型的自定义特性是否应用于一个指定的数字，并选择性地应用于其的上级。</summary>
    /// <returns>如果将指定类型的特性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">要检查的成员。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
    {
      return Attribute.IsDefined(element, attributeType, inherit);
    }

    /// <summary>指示一个指定类型的自定义特性是否应用于一个指定的参数，并选择性地应用于其的上级。</summary>
    /// <returns>如果将指定类型的特性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">要检查的参数。</param>
    /// <param name="attributeType">要搜索的特性类型。</param>
    /// <param name="inherit">如果检查 <paramref name="element" /> 的上级，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
    {
      return Attribute.IsDefined(element, attributeType, inherit);
    }
  }
}
