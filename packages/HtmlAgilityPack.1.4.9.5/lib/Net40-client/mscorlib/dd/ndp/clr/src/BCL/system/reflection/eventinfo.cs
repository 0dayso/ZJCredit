// Decompiled with JetBrains decompiler
// Type: System.Reflection.EventInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>发现事件的属性并提供对事件元数据的访问权。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_EventInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class EventInfo : MemberInfo, _EventInfo
  {
    /// <summary>获取一个 <see cref="T:System.Reflection.MemberTypes" /> 值，该值指示此成员是事件。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberTypes" /> 值，指示此成员是事件。</returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Event;
      }
    }

    /// <summary>获取此事件的属性。</summary>
    /// <returns>此事件的只读特性。</returns>
    [__DynamicallyInvokable]
    public abstract EventAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>获取 <see cref="T:System.Reflection.MethodInfo" /> 对象 <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> 事件的一个方法，包括非公共方法。</summary>
    /// <returns>
    /// <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> 方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象。</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo AddMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetAddMethod(true);
      }
    }

    /// <summary>获取 MethodInfo 对象，以移除该事件的一个方法，包括非公共方法。</summary>
    /// <returns>用于移除该事件方法的 MethodInfo 对象。</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo RemoveMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetRemoveMethod(true);
      }
    }

    /// <summary>获取返回在引发该事件时所调用的方法，含非公开的方法。</summary>
    /// <returns>引发该事件时所调用的方法。</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo RaiseMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetRaiseMethod(true);
      }
    }

    /// <summary>获取与此事件关联的基础事件处理程序委托的 Type 对象。</summary>
    /// <returns>表示委托事件处理程序的只读 Type 对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [__DynamicallyInvokable]
    public virtual Type EventHandlerType
    {
      [__DynamicallyInvokable] get
      {
        ParameterInfo[] parametersNoCopy = this.GetAddMethod(true).GetParametersNoCopy();
        Type c = typeof (Delegate);
        for (int index = 0; index < parametersNoCopy.Length; ++index)
        {
          Type parameterType = parametersNoCopy[index].ParameterType;
          if (parameterType.IsSubclassOf(c))
            return parameterType;
        }
        return (Type) null;
      }
    }

    /// <summary>获取一个值，通过该值指示 EventInfo 是否具有一个有特殊意义的名称。</summary>
    /// <returns>如果此事件具有一个特殊名称，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & EventAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示此事件是否为多路广播。</summary>
    /// <returns>如果该委托是多路广播委托的实例，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsMulticast
    {
      [__DynamicallyInvokable] get
      {
        return typeof (MulticastDelegate).IsAssignableFrom(this.EventHandlerType);
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.EventInfo" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(EventInfo left, EventInfo right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimeEventInfo || right is RuntimeEventInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.EventInfo" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(EventInfo left, EventInfo right)
    {
      return !(left == right);
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

    /// <summary>返回在元数据中使用 .other 指令与事件相关联的方法，指定是否包括非公共方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示已通过在元数据中使用 .other 指令与事件相关联的方法。如果没有与该规范匹配的方法，则返回空数组。</returns>
    /// <param name="nonPublic">如果包括非公共方法，则为 true；否则为 false。</param>
    /// <exception cref="T:System.NotImplementedException">此方法未实现。</exception>
    public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
    {
      throw new NotImplementedException();
    }

    /// <summary>当在派生类中重写时，检索事件的 <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> 方法的 MethodInfo 对象，指定是否返回非公共方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于将事件处理程序委托添加到事件源的方法。</returns>
    /// <param name="nonPublic">如果可以返回非公共方法，则为 true；否则为 false。</param>
    /// <exception cref="T:System.MethodAccessException">
    /// <paramref name="nonPublic" /> 为 true，表示用于添加事件处理程序委托的方法为非公共的，并且调用方无权对非公共方法进行反射。</exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetAddMethod(bool nonPublic);

    /// <summary>当在派生类中重写时，检索用于移除事件的方法的 MethodInfo 对象，指定是否返回非公共方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于从事件源中移除事件处理程序委托的方法。</returns>
    /// <param name="nonPublic">如果可以返回非公共方法，则为 true；否则为 false。</param>
    /// <exception cref="T:System.MethodAccessException">
    /// <paramref name="nonPublic" /> 为 true，表示用于添加事件处理程序委托的方法为非公共的，并且调用方无权对非公共方法进行反射。</exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetRemoveMethod(bool nonPublic);

    /// <summary>当在派生类中重写时，返回引发事件时调用的方法，指定是否返回非公共方法。</summary>
    /// <returns>在引发事件时调用的 MethodInfo 对象。</returns>
    /// <param name="nonPublic">如果可以返回非公共方法，则为 true；否则为 false。</param>
    /// <exception cref="T:System.MethodAccessException">
    /// <paramref name="nonPublic" /> 为 true，表示用于添加事件处理程序委托的方法为非公共的，并且调用方无权对非公共方法进行反射。</exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetRaiseMethod(bool nonPublic);

    /// <summary>返回在元数据中使用 .other 指令与事件相关联的公共方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示已在元数据中通过使用 .other 指令与事件相关联的公共方法。如果没有此类公共方法，则返回空数组。</returns>
    public MethodInfo[] GetOtherMethods()
    {
      return this.GetOtherMethods(false);
    }

    /// <summary>返回用于将事件处理程序委托添加到事件源的方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于将事件处理程序委托添加到事件源的方法。</returns>
    [__DynamicallyInvokable]
    public MethodInfo GetAddMethod()
    {
      return this.GetAddMethod(false);
    }

    /// <summary>返回用于从事件源中移除事件处理程序委托的方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于从事件源中移除事件处理程序委托的方法。</returns>
    [__DynamicallyInvokable]
    public MethodInfo GetRemoveMethod()
    {
      return this.GetRemoveMethod(false);
    }

    /// <summary>返回在引发该事件时所调用的方法。</summary>
    /// <returns>引发该事件时所调用的方法。</returns>
    [__DynamicallyInvokable]
    public MethodInfo GetRaiseMethod()
    {
      return this.GetRaiseMethod(false);
    }

    /// <summary>将事件处理程序添加到事件源。</summary>
    /// <param name="target">事件源。</param>
    /// <param name="handler">封装目标引发事件时将调用的方法。</param>
    /// <exception cref="T:System.InvalidOperationException">该事件没有公共的 add 访问器。</exception>
    /// <exception cref="T:System.ArgumentException">传入的处理程序无法使用。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。调用方无权访问该成员。</exception>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。<paramref name="target" /> 参数为 null 并且该事件不是静态的。- 或 -目标上没有声明 <see cref="T:System.Reflection.EventInfo" />。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual void AddEventHandler(object target, Delegate handler)
    {
      MethodInfo addMethod = this.GetAddMethod();
      // ISSUE: variable of the null type
      __Null local = null;
      if (addMethod == (MethodInfo) local)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
      if (addMethod.ReturnType == typeof (EventRegistrationToken))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
      object obj = target;
      object[] parameters = new object[1]{ (object) handler };
      addMethod.Invoke(obj, parameters);
    }

    /// <summary>从事件源中移除事件处理程序。</summary>
    /// <param name="target">事件源。</param>
    /// <param name="handler">将解除与由目标引发的事件的关联的委托。</param>
    /// <exception cref="T:System.InvalidOperationException">该事件没有公共的 remove 访问器。</exception>
    /// <exception cref="T:System.ArgumentException">传入的处理程序无法使用。</exception>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。<paramref name="target" /> 参数为 null 并且该事件不是静态的。- 或 -目标上没有声明 <see cref="T:System.Reflection.EventInfo" />。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。调用方无权访问该成员。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual void RemoveEventHandler(object target, Delegate handler)
    {
      MethodInfo removeMethod = this.GetRemoveMethod();
      // ISSUE: variable of the null type
      __Null local = null;
      if (removeMethod == (MethodInfo) local)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicRemoveMethod"));
      if (removeMethod.GetParametersNoCopy()[0].ParameterType == typeof (EventRegistrationToken))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
      object obj = target;
      object[] parameters = new object[1]{ (object) handler };
      removeMethod.Invoke(obj, parameters);
    }

    Type _EventInfo.GetType()
    {
      return this.GetType();
    }

    void _EventInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _EventInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
