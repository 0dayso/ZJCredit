// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>获取有关成员属性的信息并提供对成员元数据的访问。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MemberInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class MemberInfo : ICustomAttributeProvider, _MemberInfo
  {
    /// <summary>在派生类中重写时，获取一个 <see cref="T:System.Reflection.MemberTypes" /> 值，指示此成员的类型（方法、构造函数和事件等）。</summary>
    /// <returns>指示成员类型的 <see cref="T:System.Reflection.MemberTypes" /> 值。</returns>
    public abstract MemberTypes MemberType { get; }

    /// <summary>获取当前成员的名称。</summary>
    /// <returns>包含此成员名称的 <see cref="T:System.String" />。</returns>
    [__DynamicallyInvokable]
    public abstract string Name { [__DynamicallyInvokable] get; }

    /// <summary>获取声明该成员的类。</summary>
    /// <returns>声明该成员的类的 Type 对象。</returns>
    [__DynamicallyInvokable]
    public abstract Type DeclaringType { [__DynamicallyInvokable] get; }

    /// <summary>获取用于获取 MemberInfo 的此实例的类对象。</summary>
    /// <returns>Type 对象，通过它获取了该 MemberInfo 对象。</returns>
    [__DynamicallyInvokable]
    public abstract Type ReflectedType { [__DynamicallyInvokable] get; }

    /// <summary>获取包含此成员自定义特性的集合。</summary>
    /// <returns>包含此成员的自定义特性的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>获取一个值，该值标识元数据元素。</summary>
    /// <returns>一个值，与 <see cref="P:System.Reflection.MemberInfo.Module" /> 一起来唯一标识元数据元素。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Reflection.MemberInfo" /> 表示某数组类型的数组方法（如 Address），该数组类型的元素类型属于尚未完成的动态类型。若要在这种情况下获取元数据标记，请将 <see cref="T:System.Reflection.MemberInfo" /> 对象传递给 <see cref="M:System.Reflection.Emit.ModuleBuilder.GetMethodToken(System.Reflection.MethodInfo)" /> 方法；或者使用 <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethodToken(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" /> 方法直接获取该标记，而不是首先使用 <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethod(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" /> 方法获取 <see cref="T:System.Reflection.MethodInfo" />。</exception>
    public virtual int MetadataToken
    {
      get
      {
        throw new InvalidOperationException();
      }
    }

    /// <summary>获取一个模块，在该模块中已经定义一个类型，该类型用于声明由当前 <see cref="T:System.Reflection.MemberInfo" /> 表示的成员。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Module" />，在其中已经定义一个类型，该类型用于声明由当前 <see cref="T:System.Reflection.MemberInfo" /> 表示的成员。</returns>
    /// <exception cref="T:System.NotImplementedException">此方法未实现。</exception>
    [__DynamicallyInvokable]
    public virtual Module Module
    {
      [__DynamicallyInvokable] get
      {
        if (this is Type)
          return ((Type) this).Module;
        throw new NotImplementedException();
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.MemberInfo" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的 <see cref="T:System.Reflection.MemberInfo" />。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的 <see cref="T:System.Reflection.MemberInfo" />。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(MemberInfo left, MemberInfo right)
    {
      if (left == right)
        return true;
      if (left == null || right == null)
        return false;
      Type type1;
      Type type2;
      if ((type1 = left as Type) != (Type) null && (type2 = right as Type) != (Type) null)
        return type1 == type2;
      MethodBase methodBase1;
      MethodBase methodBase2;
      if ((methodBase1 = left as MethodBase) != (MethodBase) null && (methodBase2 = right as MethodBase) != (MethodBase) null)
        return methodBase1 == methodBase2;
      FieldInfo fieldInfo1;
      FieldInfo fieldInfo2;
      if ((fieldInfo1 = left as FieldInfo) != (FieldInfo) null && (fieldInfo2 = right as FieldInfo) != (FieldInfo) null)
        return fieldInfo1 == fieldInfo2;
      EventInfo eventInfo1;
      EventInfo eventInfo2;
      if ((eventInfo1 = left as EventInfo) != (EventInfo) null && (eventInfo2 = right as EventInfo) != (EventInfo) null)
        return eventInfo1 == eventInfo2;
      PropertyInfo propertyInfo1;
      PropertyInfo propertyInfo2;
      if ((propertyInfo1 = left as PropertyInfo) != (PropertyInfo) null && (propertyInfo2 = right as PropertyInfo) != (PropertyInfo) null)
        return propertyInfo1 == propertyInfo2;
      return false;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.MemberInfo" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的 <see cref="T:System.Reflection.MemberInfo" />。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的 <see cref="T:System.Reflection.MemberInfo" />。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(MemberInfo left, MemberInfo right)
    {
      return !(left == right);
    }

    internal virtual bool CacheEquals(object o)
    {
      throw new NotImplementedException();
    }

    /// <summary>在派生类中重写时，返回应用于此成员的所有自定义特性的数组。</summary>
    /// <returns>一个包含应用于此成员的所有自定义特性的数组，在未定义任何特性时为包含零个元素的数组。</returns>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。属性和事件中忽略此参数，请参见“备注”。</param>
    /// <exception cref="T:System.InvalidOperationException">该成员属于加载到仅反射上下文的类型。请参见如何：将程序集加载到仅反射上下文中。</exception>
    /// <exception cref="T:System.TypeLoadException">未能加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public abstract object[] GetCustomAttributes(bool inherit);

    /// <summary>在派生类中重写时，返回应用于此成员并由 <see cref="T:System.Type" /> 标识的自定义特性的数组。</summary>
    /// <returns>应用于此成员的自定义特性的数组；如果未应用任何可分配给 <paramref name="attributeType" /> 的特性，则为包含零个元素的数组。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。属性和事件中忽略此参数，请参见“备注”。</param>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">如果 <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该成员属于加载到仅反射上下文的类型。请参见如何：将程序集加载到仅反射上下文中。</exception>
    [__DynamicallyInvokable]
    public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>在派生类中重写时，指示是否将指定类型或其派生类型的一个或多个特性应用于此成员。</summary>
    /// <returns>如果将 <paramref name="attributeType" /> 或其任何派生类型的一个或多个实例应用于此成员，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要搜索的自定义属性的类型。该搜索包括派生类型。</param>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。属性和事件中忽略此参数，请参见“备注”。</param>
    [__DynamicallyInvokable]
    public abstract bool IsDefined(Type attributeType, bool inherit);

    /// <summary>返回一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象表示有关已应用于目标成员的特性的数据。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.CustomAttributeData" /> 对象的泛型列表，这些对象表示有关已应用于目标成员的特性的数据。</returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    Type _MemberInfo.GetType()
    {
      return this.GetType();
    }

    void _MemberInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MemberInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MemberInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MemberInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
