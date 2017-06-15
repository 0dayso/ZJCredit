// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>表示类类型、接口类型、数组类型、值类型、枚举类型、类型参数、泛型类型定义，以及开放或封闭构造的泛型类型的类型声明。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class TypeInfo : Type, IReflectableType
  {
    /// <summary>获取当前实例泛型类型参数的数组。</summary>
    /// <returns>包含当前实例的泛型类型参数的数组，如果当前实例没有任何泛型类型参数，则为 <see cref="P:System.Array.Length" /> 为零的数组。</returns>
    [__DynamicallyInvokable]
    public virtual Type[] GenericTypeParameters
    {
      [__DynamicallyInvokable] get
      {
        if (this.IsGenericTypeDefinition)
          return this.GetGenericArguments();
        return Type.EmptyTypes;
      }
    }

    /// <summary>获取由当前类型声明的构造函数的集合。</summary>
    /// <returns>由当前类型声明的构造函数的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<ConstructorInfo>) this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>获取由当前类型定义的事件的集合。</summary>
    /// <returns>由当前类型定义的事件的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<EventInfo> DeclaredEvents
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<EventInfo>) this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>获取由当前类型定义的字段的集合。</summary>
    /// <returns>由当前类型定义的字段的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<FieldInfo> DeclaredFields
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<FieldInfo>) this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>获取由当前类型定义的成员的集合。</summary>
    /// <returns>由当前类型定义的成员的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<MemberInfo> DeclaredMembers
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<MemberInfo>) this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>获取由当前类型定义的方法的集合。</summary>
    /// <returns>由当前类型定义的方法的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<MethodInfo> DeclaredMethods
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<MethodInfo>) this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>获取由当前类型定义的嵌套类型的集合。</summary>
    /// <returns>由当前类型定义的嵌套类型的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
    {
      [__DynamicallyInvokable] get
      {
        Type[] typeArray = this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        for (int index = 0; index < typeArray.Length; ++index)
          yield return typeArray[index].GetTypeInfo();
        typeArray = (Type[]) null;
      }
    }

    /// <summary>获取由当前类型定义的属性的集合。</summary>
    /// <returns>由当前类型定义的属性的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<PropertyInfo> DeclaredProperties
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<PropertyInfo>) this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>获取当前类型实现的接口的集合。</summary>
    /// <returns>由当前类型实现的接口的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<Type> ImplementedInterfaces
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<Type>) this.GetInterfaces();
      }
    }

    [FriendAccessAllowed]
    internal TypeInfo()
    {
    }

    [__DynamicallyInvokable]
    TypeInfo IReflectableType.GetTypeInfo()
    {
      return this;
    }

    /// <summary>返回 <see cref="T:System.Type" /> 对象形式的当前类型。</summary>
    /// <returns>当前类型。</returns>
    [__DynamicallyInvokable]
    public virtual Type AsType()
    {
      return (Type) this;
    }

    /// <summary>返回一个值，该值指示指定类型是否可分配给当前的类型。</summary>
    /// <returns>如果可以将指定类型分配给此类型，则为 true；否则为 false。</returns>
    /// <param name="typeInfo">要检查的类型。</param>
    [__DynamicallyInvokable]
    public virtual bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      if ((Type) this == (Type) typeInfo || typeInfo.IsSubclassOf((Type) this))
        return true;
      if (this.IsInterface)
        return typeInfo.ImplementInterface((Type) this);
      if (!this.IsGenericParameter)
        return false;
      foreach (Type parameterConstraint in this.GetGenericParameterConstraints())
      {
        if (!parameterConstraint.IsAssignableFrom((Type) typeInfo))
          return false;
      }
      return true;
    }

    /// <summary>返回表示由当前类型声明的指定公共事件的对象。</summary>
    /// <returns>如果找到对象，则为表示指定的事件的对象；否则为 null。</returns>
    /// <param name="name">事件的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual EventInfo GetDeclaredEvent(string name)
    {
      return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>返回表示由当前类型声明的指定公共字段的对象。</summary>
    /// <returns>如果找到对象，则为表示指定的字段的对象；否则为 null。</returns>
    /// <param name="name">字段的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual FieldInfo GetDeclaredField(string name)
    {
      return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>返回表示由当前类型声明的指定公共方法的对象。</summary>
    /// <returns>如果找到对象，则为表示指定的方法的对象；否则为 null。</returns>
    /// <param name="name">方法的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual MethodInfo GetDeclaredMethod(string name)
    {
      return this.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>返回包含所有与指定名称相匹配在当前类型声明的公共方法的集合。</summary>
    /// <returns>包含匹配 <paramref name="name" />的方法的集合。</returns>
    /// <param name="name">要搜索的方法名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
    {
      MethodInfo[] methodInfoArray = this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < methodInfoArray.Length; ++index)
      {
        MethodInfo methodInfo = methodInfoArray[index];
        if (methodInfo.Name == name)
          yield return methodInfo;
      }
      methodInfoArray = (MethodInfo[]) null;
    }

    /// <summary>返回表示由当前类型声明的指定公共嵌套类型的对象。</summary>
    /// <returns>如果找到对象，则为表示指定的嵌套类型的对象；否则为 null。</returns>
    /// <param name="name">嵌套类型的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual TypeInfo GetDeclaredNestedType(string name)
    {
      Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      if (nestedType == (Type) null)
        return (TypeInfo) null;
      return nestedType.GetTypeInfo();
    }

    /// <summary>返回表示由当前类型声明的指定公共属性的对象。</summary>
    /// <returns>如果找到对象，则为表示指定的属性的对象；否则为 null。</returns>
    /// <param name="name">属性的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public virtual PropertyInfo GetDeclaredProperty(string name)
    {
      return this.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }
  }
}
