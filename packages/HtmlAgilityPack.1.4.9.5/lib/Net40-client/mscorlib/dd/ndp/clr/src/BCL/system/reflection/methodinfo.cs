// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>发现方法的属性并提供对方法元数据的访问。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class MethodInfo : MethodBase, _MethodInfo
  {
    /// <summary>获取一个 <see cref="T:System.Reflection.MemberTypes" /> 值，该值指示此成员是方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberTypes" /> 值，指示此成员是方法。</returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Method;
      }
    }

    /// <summary>获取此方法的返回类型。</summary>
    /// <returns>此方法的返回类型。</returns>
    [__DynamicallyInvokable]
    public virtual Type ReturnType
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个 <see cref="T:System.Reflection.ParameterInfo" /> 对象，该对象包含有关方法的返回类型的信息（例如返回类型是否具有自定义修饰符）。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.ParameterInfo" /> 对象，包含有关返回类型的信息。</returns>
    /// <exception cref="T:System.NotImplementedException">此方法未实现。</exception>
    [__DynamicallyInvokable]
    public virtual ParameterInfo ReturnParameter
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取返回类型的自定义属性。</summary>
    /// <returns>表示返回类型自定义属性的 ICustomAttributeProvider 对象。</returns>
    public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

    /// <summary>指示两个 <see cref="T:System.Reflection.MethodInfo" /> 对象是否相等。</summary>
    /// <returns>如果 true 等于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(MethodInfo left, MethodInfo right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimeMethodInfo || right is RuntimeMethodInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.MethodInfo" /> 对象是否不相等。</summary>
    /// <returns>如果 true 不等于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(MethodInfo left, MethodInfo right)
    {
      return !(left == right);
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 true 等于此实例的类型和值，则为 <paramref name="obj" />；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象，或为 null。</param>
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

    /// <summary>当在派生类中被重写时，为直接或间接的基类（用该实例表示的方法首先在此类中声明）上的方法返回 <see cref="T:System.Reflection.MethodInfo" /> 对象。</summary>
    /// <returns>表示此方法第一个实现的 <see cref="T:System.Reflection.MethodInfo" /> 对象。</returns>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetBaseDefinition();

    /// <summary>返回 <see cref="T:System.Type" /> 对象的数组，这些对象表示泛型方法的类型实参或泛型方法定义的类型形参。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，这些对象表示泛型方法的类型变量或泛型方法定义的类型参数。如果当前方法不是泛型方法，则返回空数组。</returns>
    /// <exception cref="T:System.NotSupportedException">不支持此方法。</exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public override Type[] GetGenericArguments()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，该对象表示可从其构造当前方法的泛型方法定义。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，表示可从其构造当前方法的泛型方法定义。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前方法不是泛型方法。即，<see cref="P:System.Reflection.MethodInfo.IsGenericMethod" /> 返回 false。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持此方法。</exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual MethodInfo GetGenericMethodDefinition()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>用类型数组的元素替代当前泛型方法定义的类型参数，并返回表示结果构造方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，表示通过将当前泛型方法定义的类型参数替换为 <paramref name="typeArguments" /> 的元素生成的构造方法。</returns>
    /// <param name="typeArguments">要替换当前泛型方法定义的类型参数的类型数组。</param>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Reflection.MethodInfo" /> 不表示泛型方法定义。即，<see cref="P:System.Reflection.MethodInfo.IsGenericMethodDefinition" /> 返回 false。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeArguments" /> 为 null。- 或 - <paramref name="typeArguments" /> 的所有元素均为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeArguments" /> 中元素的数目与当前泛型方法定义的类型参数的数目不同。- 或 - <paramref name="typeArguments" /> 的某个元素不满足为当前泛型方法定义的相应类型参数指定的约束。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持此方法。</exception>
    [__DynamicallyInvokable]
    public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>从此方法创建指定类型的委托。</summary>
    /// <returns>此方法的委托。</returns>
    /// <param name="delegateType">要创建的委托的类型。</param>
    [__DynamicallyInvokable]
    public virtual Delegate CreateDelegate(Type delegateType)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>从此方法创建具有指定目标的指定类型的委托。</summary>
    /// <returns>此方法的委托。</returns>
    /// <param name="delegateType">要创建的委托的类型。</param>
    /// <param name="target">由委托将其作为目标的对象。</param>
    [__DynamicallyInvokable]
    public virtual Delegate CreateDelegate(Type delegateType, object target)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    Type _MethodInfo.GetType()
    {
      return this.GetType();
    }

    void _MethodInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
