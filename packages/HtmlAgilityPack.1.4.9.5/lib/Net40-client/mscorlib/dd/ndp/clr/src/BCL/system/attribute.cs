// Decompiled with JetBrains decompiler
// Type: System.Attribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>表示自定义属性的基类。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Attribute))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Attribute : _Attribute
  {
    /// <summary>当在派生类中实现时，获取该 <see cref="T:System.Attribute" /> 的唯一标识符。</summary>
    /// <returns>一个表示该属性的唯一标识符的 <see cref="T:System.Object" />。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object TypeId
    {
      get
      {
        return (object) this.GetType();
      }
    }

    /// <summary>初始化 <see cref="T:System.Attribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Attribute()
    {
    }

    private static Attribute[] InternalGetCustomAttributes(PropertyInfo element, Type type, bool inherit)
    {
      Attribute[] attributes = (Attribute[]) element.GetCustomAttributes(type, inherit);
      if (!inherit)
        return attributes;
      Dictionary<Type, AttributeUsageAttribute> types = new Dictionary<Type, AttributeUsageAttribute>(11);
      List<Attribute> attributeList = new List<Attribute>();
      Attribute.CopyToArrayList(attributeList, attributes, types);
      Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
      for (PropertyInfo parentDefinition = Attribute.GetParentDefinition(element, indexParameterTypes); parentDefinition != (PropertyInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition, indexParameterTypes))
      {
        Attribute[] customAttributes = Attribute.GetCustomAttributes((MemberInfo) parentDefinition, type, false);
        Attribute.AddAttributesToList(attributeList, customAttributes, types);
      }
      Array destinationArray = (Array) Attribute.CreateAttributeArrayHelper(type, attributeList.Count);
      Array.Copy((Array) attributeList.ToArray(), 0, destinationArray, 0, attributeList.Count);
      return (Attribute[]) destinationArray;
    }

    private static bool InternalIsDefined(PropertyInfo element, Type attributeType, bool inherit)
    {
      if (element.IsDefined(attributeType, inherit))
        return true;
      if (inherit && Attribute.InternalGetAttributeUsage(attributeType).Inherited)
      {
        Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
        for (PropertyInfo parentDefinition = Attribute.GetParentDefinition(element, indexParameterTypes); parentDefinition != (PropertyInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition, indexParameterTypes))
        {
          if (parentDefinition.IsDefined(attributeType, false))
            return true;
        }
      }
      return false;
    }

    private static PropertyInfo GetParentDefinition(PropertyInfo property, Type[] propertyParameters)
    {
      MethodInfo methodInfo = property.GetGetMethod(true);
      if (methodInfo == (MethodInfo) null)
        methodInfo = property.GetSetMethod(true);
      RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
      {
        RuntimeMethodInfo parentDefinition = runtimeMethodInfo.GetParentDefinition();
        if ((MethodInfo) parentDefinition != (MethodInfo) null)
          return parentDefinition.DeclaringType.GetProperty(property.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, property.PropertyType, propertyParameters, (ParameterModifier[]) null);
      }
      return (PropertyInfo) null;
    }

    private static Attribute[] InternalGetCustomAttributes(EventInfo element, Type type, bool inherit)
    {
      Attribute[] attributes = (Attribute[]) element.GetCustomAttributes(type, inherit);
      if (!inherit)
        return attributes;
      Dictionary<Type, AttributeUsageAttribute> types = new Dictionary<Type, AttributeUsageAttribute>(11);
      List<Attribute> attributeList = new List<Attribute>();
      Attribute.CopyToArrayList(attributeList, attributes, types);
      for (EventInfo parentDefinition = Attribute.GetParentDefinition(element); parentDefinition != (EventInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
      {
        Attribute[] customAttributes = Attribute.GetCustomAttributes((MemberInfo) parentDefinition, type, false);
        Attribute.AddAttributesToList(attributeList, customAttributes, types);
      }
      Array destinationArray = (Array) Attribute.CreateAttributeArrayHelper(type, attributeList.Count);
      Array.Copy((Array) attributeList.ToArray(), 0, destinationArray, 0, attributeList.Count);
      return (Attribute[]) destinationArray;
    }

    private static EventInfo GetParentDefinition(EventInfo ev)
    {
      RuntimeMethodInfo runtimeMethodInfo = ev.GetAddMethod(true) as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
      {
        RuntimeMethodInfo parentDefinition = runtimeMethodInfo.GetParentDefinition();
        if ((MethodInfo) parentDefinition != (MethodInfo) null)
          return parentDefinition.DeclaringType.GetEvent(ev.Name);
      }
      return (EventInfo) null;
    }

    private static bool InternalIsDefined(EventInfo element, Type attributeType, bool inherit)
    {
      if (element.IsDefined(attributeType, inherit))
        return true;
      if (inherit && Attribute.InternalGetAttributeUsage(attributeType).Inherited)
      {
        for (EventInfo parentDefinition = Attribute.GetParentDefinition(element); parentDefinition != (EventInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
        {
          if (parentDefinition.IsDefined(attributeType, false))
            return true;
        }
      }
      return false;
    }

    private static ParameterInfo GetParentDefinition(ParameterInfo param)
    {
      RuntimeMethodInfo runtimeMethodInfo = param.Member as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
      {
        RuntimeMethodInfo parentDefinition = runtimeMethodInfo.GetParentDefinition();
        if ((MethodInfo) parentDefinition != (MethodInfo) null)
          return parentDefinition.GetParameters()[param.Position];
      }
      return (ParameterInfo) null;
    }

    private static Attribute[] InternalParamGetCustomAttributes(ParameterInfo param, Type type, bool inherit)
    {
      List<Type> typeList = new List<Type>();
      if (type == (Type) null)
        type = typeof (Attribute);
      object[] customAttributes1 = param.GetCustomAttributes(type, false);
      for (int index = 0; index < customAttributes1.Length; ++index)
      {
        Type type1 = customAttributes1[index].GetType();
        if (!Attribute.InternalGetAttributeUsage(type1).AllowMultiple)
          typeList.Add(type1);
      }
      Attribute[] attributeArray1 = customAttributes1.Length != 0 ? (Attribute[]) customAttributes1 : Attribute.CreateAttributeArrayHelper(type, 0);
      if (param.Member.DeclaringType == (Type) null || !inherit)
        return attributeArray1;
      for (ParameterInfo parentDefinition = Attribute.GetParentDefinition(param); parentDefinition != null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
      {
        object[] customAttributes2 = parentDefinition.GetCustomAttributes(type, false);
        int elementCount = 0;
        for (int index = 0; index < customAttributes2.Length; ++index)
        {
          Type type1 = customAttributes2[index].GetType();
          AttributeUsageAttribute attributeUsage = Attribute.InternalGetAttributeUsage(type1);
          if (attributeUsage.Inherited && !typeList.Contains(type1))
          {
            if (!attributeUsage.AllowMultiple)
              typeList.Add(type1);
            ++elementCount;
          }
          else
            customAttributes2[index] = (object) null;
        }
        Attribute[] attributeArrayHelper = Attribute.CreateAttributeArrayHelper(type, elementCount);
        int index1 = 0;
        for (int index2 = 0; index2 < customAttributes2.Length; ++index2)
        {
          if (customAttributes2[index2] != null)
          {
            attributeArrayHelper[index1] = (Attribute) customAttributes2[index2];
            ++index1;
          }
        }
        Attribute[] attributeArray2 = attributeArray1;
        attributeArray1 = Attribute.CreateAttributeArrayHelper(type, attributeArray2.Length + index1);
        Array.Copy((Array) attributeArray2, (Array) attributeArray1, attributeArray2.Length);
        int length = attributeArray2.Length;
        for (int index2 = 0; index2 < attributeArrayHelper.Length; ++index2)
          attributeArray1[length + index2] = attributeArrayHelper[index2];
      }
      return attributeArray1;
    }

    private static bool InternalParamIsDefined(ParameterInfo param, Type type, bool inherit)
    {
      if (param.IsDefined(type, false))
        return true;
      if (param.Member.DeclaringType == (Type) null || !inherit)
        return false;
      for (ParameterInfo parentDefinition = Attribute.GetParentDefinition(param); parentDefinition != null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
      {
        object[] customAttributes = parentDefinition.GetCustomAttributes(type, false);
        for (int index = 0; index < customAttributes.Length; ++index)
        {
          AttributeUsageAttribute attributeUsage = Attribute.InternalGetAttributeUsage(customAttributes[index].GetType());
          if (customAttributes[index] is Attribute && attributeUsage.Inherited)
            return true;
        }
      }
      return false;
    }

    private static void CopyToArrayList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
    {
      for (int index = 0; index < attributes.Length; ++index)
      {
        attributeList.Add(attributes[index]);
        Type type = attributes[index].GetType();
        if (!types.ContainsKey(type))
          types[type] = Attribute.InternalGetAttributeUsage(type);
      }
    }

    private static Type[] GetIndexParameterTypes(PropertyInfo element)
    {
      ParameterInfo[] indexParameters = element.GetIndexParameters();
      if (indexParameters.Length == 0)
        return Array.Empty<Type>();
      Type[] typeArray = new Type[indexParameters.Length];
      for (int index = 0; index < indexParameters.Length; ++index)
        typeArray[index] = indexParameters[index].ParameterType;
      return typeArray;
    }

    private static void AddAttributesToList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
    {
      for (int index = 0; index < attributes.Length; ++index)
      {
        Type type = attributes[index].GetType();
        AttributeUsageAttribute attributeUsageAttribute = (AttributeUsageAttribute) null;
        types.TryGetValue(type, out attributeUsageAttribute);
        if (attributeUsageAttribute == null)
        {
          attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type);
          types[type] = attributeUsageAttribute;
          if (attributeUsageAttribute.Inherited)
            attributeList.Add(attributes[index]);
        }
        else if (attributeUsageAttribute.Inherited && attributeUsageAttribute.AllowMultiple)
          attributeList.Add(attributes[index]);
      }
    }

    private static AttributeUsageAttribute InternalGetAttributeUsage(Type type)
    {
      object[] customAttributes = type.GetCustomAttributes(typeof (AttributeUsageAttribute), false);
      if (customAttributes.Length == 1)
        return (AttributeUsageAttribute) customAttributes[0];
      if (customAttributes.Length == 0)
        return AttributeUsageAttribute.Default;
      throw new FormatException(Environment.GetResourceString("Format_AttributeUsage", (object) type));
    }

    [SecuritySafeCritical]
    private static Attribute[] CreateAttributeArrayHelper(Type elementType, int elementCount)
    {
      return (Attribute[]) Array.UnsafeCreateInstance(elementType, elementCount);
    }

    /// <summary>检索应用于类型的成员的自定义属性的数组。参数指定成员和要搜索的自定义属性的类型。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="type" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法或属性成员。</param>
    /// <param name="type">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element, Type type)
    {
      return Attribute.GetCustomAttributes(element, type, true);
    }

    /// <summary>检索应用于类型的成员的自定义属性的数组。参数指定成员、要搜索的自定义属性的类型以及是否搜索成员的祖先。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="type" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法或属性成员。</param>
    /// <param name="type">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element, Type type, bool inherit)
    {
      if (element == (MemberInfo) null)
        throw new ArgumentNullException("element");
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (!type.IsSubclassOf(typeof (Attribute)) && type != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      switch (element.MemberType)
      {
        case MemberTypes.Event:
          return Attribute.InternalGetCustomAttributes((EventInfo) element, type, inherit);
        case MemberTypes.Property:
          return Attribute.InternalGetCustomAttributes((PropertyInfo) element, type, inherit);
        default:
          return element.GetCustomAttributes(type, inherit) as Attribute[];
      }
    }

    /// <summary>检索应用于类型的成员的自定义属性的数组。参数指定成员。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法或属性成员。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>检索应用于类型的成员的自定义属性的数组。参数指定成员、要搜索的自定义属性的类型以及是否搜索成员的祖先。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法或属性成员。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element, bool inherit)
    {
      if (element == (MemberInfo) null)
        throw new ArgumentNullException("element");
      switch (element.MemberType)
      {
        case MemberTypes.Event:
          return Attribute.InternalGetCustomAttributes((EventInfo) element, typeof (Attribute), inherit);
        case MemberTypes.Property:
          return Attribute.InternalGetCustomAttributes((PropertyInfo) element, typeof (Attribute), inherit);
        default:
          return element.GetCustomAttributes(typeof (Attribute), inherit) as Attribute[];
      }
    }

    /// <summary>确定是否将任意自定义属性应用于类型成员。参数指定成员和要搜索的自定义属性的类型。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法、类型或属性成员。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDefined(MemberInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, true);
    }

    /// <summary>确定是否将任意自定义属性应用于类型成员。参数指定成员、要搜索的自定义属性的类型以及是否搜索成员的祖先。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法、类型或属性成员。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
    {
      if (element == (MemberInfo) null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      switch (element.MemberType)
      {
        case MemberTypes.Event:
          return Attribute.InternalIsDefined((EventInfo) element, attributeType, inherit);
        case MemberTypes.Property:
          return Attribute.InternalIsDefined((PropertyInfo) element, attributeType, inherit);
        default:
          return element.IsDefined(attributeType, inherit);
      }
    }

    /// <summary>检索应用于类型成员的自定义属性。参数指定成员和要搜索的自定义属性的类型。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法或属性成员。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>检索应用于类型成员的自定义属性。参数指定成员、要搜索的自定义属性的类型以及是否搜索成员的祖先。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.MemberInfo" /> 类派生的对象，该类描述类的构造函数、事件、字段、方法或属性成员。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="element" /> 不是构造函数、方法、属性、事件、类型或字段。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>检索应用于方法参数的自定义属性的数组。参数指定方法参数。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>检索应用于方法参数的自定义属性的数组。参数指定方法参数和要搜索的自定义属性的类型。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttributes(element, attributeType, true);
    }

    /// <summary>检索应用于方法参数的自定义属性的数组。参数指定方法参数、要搜索的自定义属性的类型以及是否搜索方法参数的祖先。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType, bool inherit)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      if (element.Member == (MemberInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParameterInfo"), "element");
      if (element.Member.MemberType == MemberTypes.Method & inherit)
        return Attribute.InternalParamGetCustomAttributes(element, attributeType, inherit);
      return element.GetCustomAttributes(attributeType, inherit) as Attribute[];
    }

    /// <summary>检索应用于方法参数的自定义属性的数组。参数指定方法参数以及是否搜索方法参数的祖先。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="element" /> 的 <see cref="P:System.Reflection.ParameterInfo.Member" /> 属性为 null. </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element, bool inherit)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (element.Member == (MemberInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParameterInfo"), "element");
      if (element.Member.MemberType == MemberTypes.Method & inherit)
        return Attribute.InternalParamGetCustomAttributes(element, (Type) null, inherit);
      return element.GetCustomAttributes(typeof (Attribute), inherit) as Attribute[];
    }

    /// <summary>确定是否将任意自定义属性应用于方法参数。参数指定方法参数和要搜索的自定义属性的类型。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDefined(ParameterInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, true);
    }

    /// <summary>确定是否将任意自定义属性应用于方法参数。参数指定方法参数、要搜索的自定义属性的类型以及是否搜索方法参数的祖先。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.ExecutionEngineException">
    /// <paramref name="element" /> 不是方法、构造函数或类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDefined(ParameterInfo element, Type attributeType, bool inherit)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      switch (element.Member.MemberType)
      {
        case MemberTypes.Constructor:
          return element.IsDefined(attributeType, false);
        case MemberTypes.Method:
          return Attribute.InternalParamIsDefined(element, attributeType, inherit);
        case MemberTypes.Property:
          return element.IsDefined(attributeType, false);
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParamInfo"));
      }
    }

    /// <summary>检索应用于方法参数的自定义属性。参数指定方法参数和要搜索的自定义属性的类型。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>检索应用于方法参数的自定义属性。参数指定方法参数、要搜索的自定义属性的类型以及是否搜索方法参数的祖先。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.ParameterInfo" /> 类派生的对象，该类描述类成员的参数。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">如果为 true，则指定还在 <paramref name="element" /> 的祖先中搜索自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>检索应用于模块的自定义属性的数组。参数指定模块和要搜索的自定义属性的类型。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute[] GetCustomAttributes(Module element, Type attributeType)
    {
      return Attribute.GetCustomAttributes(element, attributeType, true);
    }

    /// <summary>检索应用于模块的自定义属性的数组。参数指定模块。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute[] GetCustomAttributes(Module element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>检索应用于模块的自定义属性的数组。参数指定模块及忽略的搜索选项。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute[] GetCustomAttributes(Module element, bool inherit)
    {
      if (element == (Module) null)
        throw new ArgumentNullException("element");
      return (Attribute[]) element.GetCustomAttributes(typeof (Attribute), inherit);
    }

    /// <summary>检索应用于模块的自定义属性的数组。参数指定模块、要搜索的自定义属性的类型以及忽略的搜索选项。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute[] GetCustomAttributes(Module element, Type attributeType, bool inherit)
    {
      if (element == (Module) null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return (Attribute[]) element.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>确定是否将指定类型的任何自定义属性应用于模块。参数指定模块和要搜索的自定义属性的类型。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool IsDefined(Module element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, false);
    }

    /// <summary>确定是否将任意自定义属性应用于模块。参数指定模块、要搜索的自定义属性的类型以及忽略的搜索选项。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool IsDefined(Module element, Type attributeType, bool inherit)
    {
      if (element == (Module) null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return element.IsDefined(attributeType, false);
    }

    /// <summary>检索应用于模块的自定义属性。参数指定模块和要搜索的自定义属性的类型。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute GetCustomAttribute(Module element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>检索应用于模块的自定义属性。参数指定模块、要搜索的自定义属性的类型以及忽略的搜索选项。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Module" /> 类派生的对象，该类描述可移植的可执行文件。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute GetCustomAttribute(Module element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>检索应用于程序集的自定义属性的数组。参数指定程序集和要搜索的自定义属性的类型。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType)
    {
      return Attribute.GetCustomAttributes(element, attributeType, true);
    }

    /// <summary>检索应用于程序集的自定义属性的数组。参数指定程序集、要搜索的自定义属性的类型以及忽略的搜索选项。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType, bool inherit)
    {
      if (element == (Assembly) null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return (Attribute[]) element.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>检索应用于程序集的自定义属性的数组。参数指定程序集。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(Assembly element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>检索应用于程序集的自定义属性的数组。参数指定程序集及忽略的搜索选项。</summary>
    /// <returns>一个 <see cref="T:System.Attribute" /> 数组，包含应用于 <paramref name="element" /> 的自定义属性；如果不存在此类自定义属性，则为空数组。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute[] GetCustomAttributes(Assembly element, bool inherit)
    {
      if (element == (Assembly) null)
        throw new ArgumentNullException("element");
      return (Attribute[]) element.GetCustomAttributes(typeof (Attribute), inherit);
    }

    /// <summary>确定是否将任意自定义属性应用于程序集。参数指定程序集和要搜索的自定义属性的类型。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsDefined(Assembly element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, true);
    }

    /// <summary>确定是否将任意自定义属性应用于程序集。参数指定程序集、要搜索的自定义属性的类型以及忽略的搜索选项。</summary>
    /// <returns>如果类型 <paramref name="attributeType" /> 的某个自定义属性应用于 <paramref name="element" />，则为 true；否则为 false。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <filterpriority>1</filterpriority>
    public static bool IsDefined(Assembly element, Type attributeType, bool inherit)
    {
      if (element == (Assembly) null)
        throw new ArgumentNullException("element");
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return element.IsDefined(attributeType, false);
    }

    /// <summary>检索应用于指定程序集的自定义属性。参数指定程序集和要搜索的自定义属性的类型。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(Assembly element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>检索应用于程序集的自定义属性。参数指定程序集、要搜索的自定义属性的类型以及忽略的搜索选项。</summary>
    /// <returns>一个引用，指向应用于 <paramref name="element" /> 的 <paramref name="attributeType" /> 类型的单个自定义属性；如果没有此类属性，则为 null。</returns>
    /// <param name="element">一个从 <see cref="T:System.Reflection.Assembly" /> 类派生的对象，该类描述可重用模块集合。</param>
    /// <param name="attributeType">要搜索的自定义属性的类型或基类型。</param>
    /// <param name="inherit">此参数被忽略，并且不会影响此方法的操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="element" /> 或 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不从 <see cref="T:System.Attribute" /> 派生。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">找到多个请求的属性。</exception>
    /// <filterpriority>1</filterpriority>
    public static Attribute GetCustomAttribute(Assembly element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与此实例进行比较的 <see cref="T:System.Object" /> 或 null。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      RuntimeType runtimeType = (RuntimeType) this.GetType();
      if ((RuntimeType) obj.GetType() != runtimeType)
        return false;
      object obj1 = (object) this;
      FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < fields.Length; ++index)
      {
        if (!Attribute.AreFieldValuesEqual(((RtFieldInfo) fields[index]).UnsafeGetValue(obj1), ((RtFieldInfo) fields[index]).UnsafeGetValue(obj)))
          return false;
      }
      return true;
    }

    private static bool AreFieldValuesEqual(object thisValue, object thatValue)
    {
      if (thisValue == null && thatValue == null)
        return true;
      if (thisValue == null || thatValue == null)
        return false;
      if (thisValue.GetType().IsArray)
      {
        if (!thisValue.GetType().Equals(thatValue.GetType()))
          return false;
        Array array1 = thisValue as Array;
        Array array2 = thatValue as Array;
        if (array1.Length != array2.Length)
          return false;
        for (int index = 0; index < array1.Length; ++index)
        {
          if (!Attribute.AreFieldValuesEqual(array1.GetValue(index), array2.GetValue(index)))
            return false;
        }
      }
      else if (!thisValue.Equals(thatValue))
        return false;
      return true;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      Type type = this.GetType();
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      object obj1 = (object) null;
      for (int index = 0; index < fields.Length; ++index)
      {
        object obj2 = ((RtFieldInfo) fields[index]).UnsafeGetValue((object) this);
        if (obj2 != null && !obj2.GetType().IsArray)
          obj1 = obj2;
        if (obj1 != null)
          break;
      }
      if (obj1 != null)
        return obj1.GetHashCode();
      return type.GetHashCode();
    }

    /// <summary>当在派生类中重写时，返回一个指示此实例是否等于指定对象的值。</summary>
    /// <returns>如果该实例等于 <paramref name="obj" />，则为 true；否则，为 false。</returns>
    /// <param name="obj">与 <see cref="T:System.Attribute" /> 的此实例进行比较的 <see cref="T:System.Object" />。</param>
    /// <filterpriority>2</filterpriority>
    public virtual bool Match(object obj)
    {
      return this.Equals(obj);
    }

    /// <summary>当在派生类中重写时，指示此实例的值是否是派生类的默认值。</summary>
    /// <returns>如果该实例是此类的默认属性，则为 true；否则，为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsDefaultAttribute()
    {
      return false;
    }

    void _Attribute.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Attribute.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Attribute.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Attribute.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
