// Decompiled with JetBrains decompiler
// Type: System.Delegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>表示委托，委托是一种数据结构，它引用静态方法或引用类实例及该类的实例方法。</summary>
  /// <filterpriority>2</filterpriority>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Delegate : ICloneable, ISerializable
  {
    [SecurityCritical]
    internal object _target;
    [SecurityCritical]
    internal object _methodBase;
    [SecurityCritical]
    internal IntPtr _methodPtr;
    [SecurityCritical]
    internal IntPtr _methodPtrAux;

    /// <summary>获取委托所表示的方法。</summary>
    /// <returns>描述委托所表示的方法的 <see cref="T:System.Reflection.MethodInfo" />。</returns>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MethodInfo Method
    {
      [__DynamicallyInvokable] get
      {
        return this.GetMethodImpl();
      }
    }

    /// <summary>获取类实例，当前委托将对其调用实例方法。</summary>
    /// <returns>如果委托表示实例方法，则为当前委托对其调用实例方法的对象；如果委托表示静态方法，则为 null。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public object Target
    {
      [__DynamicallyInvokable] get
      {
        return this.GetTarget();
      }
    }

    /// <summary>初始化一个委托，该委托对指定的类实例调用指定的实例方法。</summary>
    /// <param name="target">类实例，委托对其调用 <paramref name="method" />.</param>
    /// <param name="method">委托表示的实例方法的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">There was an error binding to the target method.</exception>
    [SecuritySafeCritical]
    protected Delegate(object target, string method)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      if (method == null)
        throw new ArgumentNullException("method");
      object target1 = target;
      RuntimeType methodType = (RuntimeType) target1.GetType();
      string method1 = method;
      int num = 10;
      if (!this.BindToMethodName(target1, methodType, method1, (DelegateBindingFlags) num))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
    }

    /// <summary>初始化一个委托，该委托从指定的类调用指定的静态方法。</summary>
    /// <param name="target">表示定义 <paramref name="method" /> 的类的 <see cref="T:System.Type" />。</param>
    /// <param name="method">委托表示的静态方法的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="target" /> represents an open generic type.</exception>
    [SecuritySafeCritical]
    protected Delegate(Type target, string method)
    {
      if (target == (Type) null)
        throw new ArgumentNullException("target");
      if (target.IsGenericType && target.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), "target");
      if (method == null)
        throw new ArgumentNullException("method");
      RuntimeType methodType = target as RuntimeType;
      if (methodType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "target");
      this.BindToMethodName((object) null, methodType, method, DelegateBindingFlags.StaticMethodOnly | DelegateBindingFlags.OpenDelegateOnly | DelegateBindingFlags.CaselessMatching);
    }

    private Delegate()
    {
    }

    /// <summary>确定指定的委托是否相等。</summary>
    /// <returns>如果 <paramref name="d1" /> 等于 <paramref name="d2" />，则为 true；否则为 false。</returns>
    /// <param name="d1">要比较的第一个委托。</param>
    /// <param name="d2">要比较的第二个委托。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(Delegate d1, Delegate d2)
    {
      if (d1 == null)
        return d2 == null;
      return d1.Equals((object) d2);
    }

    /// <summary>确定指定的委托是否不相等。</summary>
    /// <returns>如果 <paramref name="d1" /> 不等于 <paramref name="d2" />，则为 true；否则为 false。</returns>
    /// <param name="d1">要比较的第一个委托。</param>
    /// <param name="d2">要比较的第二个委托。 </param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(Delegate d1, Delegate d2)
    {
      if (d1 == null)
        return d2 != null;
      return !d1.Equals((object) d2);
    }

    /// <summary>动态调用（后期绑定）由当前委托所表示的方法。</summary>
    /// <returns>委托所表示的方法返回的对象。</returns>
    /// <param name="args">作为参数传递给当前委托所表示的方法的对象数组。- 或 -
    /// 如果当前委托所表示的方法不需要参数，则为 null。</param>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).-or- The number, order, or type of parameters listed in <paramref name="args" /> is invalid. </exception>
    /// <exception cref="T:System.ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it. </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">The method represented by the delegate is an instance method and the target object is null.-or- One of the encapsulated methods throws an exception. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public object DynamicInvoke(params object[] args)
    {
      return this.DynamicInvokeImpl(args);
    }

    /// <summary>动态调用（后期绑定）由当前委托所表示的方法。</summary>
    /// <returns>委托所表示的方法返回的对象。</returns>
    /// <param name="args">作为参数传递给当前委托所表示的方法的对象数组。- 或 -
    /// 如果当前委托所表示的方法不需要参数，则为 null。</param>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).-or- The number, order, or type of parameters listed in <paramref name="args" /> is invalid. </exception>
    /// <exception cref="T:System.ArgumentException">The method represented by the delegate is invoked on an object or a class that does not support it. </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">The method represented by the delegate is an instance method and the target object is null.-or- One of the encapsulated methods throws an exception. </exception>
    [SecuritySafeCritical]
    protected virtual object DynamicInvokeImpl(object[] args)
    {
      return ((RuntimeMethodInfo) RuntimeType.GetMethodBase((RuntimeType) this.GetType(), new RuntimeMethodHandleInternal(this.GetInvokeMethod()))).UnsafeInvoke((object) this, BindingFlags.Default, (Binder) null, args, (CultureInfo) null);
    }

    /// <summary>确定指定的对象和当前委托的类型是否相同，是否共享相同的目标、方法和调用列表。</summary>
    /// <returns>如果 <paramref name="obj" /> 和当前委托有相同的目标、方法和调用列表，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前委托进行比较的对象。 </param>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj == null || !Delegate.InternalEqualTypes((object) this, obj))
        return false;
      Delegate right = (Delegate) obj;
      if (this._target == right._target && this._methodPtr == right._methodPtr && this._methodPtrAux == right._methodPtrAux)
        return true;
      if (this._methodPtrAux.IsNull())
      {
        if (!right._methodPtrAux.IsNull() || this._target != right._target)
          return false;
      }
      else
      {
        if (right._methodPtrAux.IsNull())
          return false;
        if (this._methodPtrAux == right._methodPtrAux)
          return true;
      }
      if (this._methodBase == null || right._methodBase == null || (!(this._methodBase is MethodInfo) || !(right._methodBase is MethodInfo)))
        return Delegate.InternalEqualMethodHandles(this, right);
      return this._methodBase.Equals(right._methodBase);
    }

    /// <summary>返回委托的哈希代码。</summary>
    /// <returns>委托的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.GetType().GetHashCode();
    }

    /// <summary>将两个委托的调用列表连接在一起。</summary>
    /// <returns>新的委托，它的调用列表将 <paramref name="a" /> 和 <paramref name="b" /> 的调用列表按该顺序连接在一起。如果 <paramref name="b" /> 为 null，则返回 <paramref name="a" />，如果 <paramref name="a" /> 为空引用，则返回 <paramref name="b" />，如果 <paramref name="a" /> 和 <paramref name="b" /> 均为空引用，则返回空引用。</returns>
    /// <param name="a">最先出现其调用列表的委托。</param>
    /// <param name="b">最后出现其调用列表的委托。</param>
    /// <exception cref="T:System.ArgumentException">Both <paramref name="a" /> and <paramref name="b" /> are not null, and <paramref name="a" /> and <paramref name="b" /> are not instances of the same delegate type. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Delegate Combine(Delegate a, Delegate b)
    {
      if (a == null)
        return b;
      return a.CombineImpl(b);
    }

    /// <summary>将委托数组的调用列表连接在一起。</summary>
    /// <returns>新的委托，该委托的调用列表将 <paramref name="delegates" /> 数组中的委托的调用列表连接在一起。如果 <paramref name="delegates" /> 为 null，<paramref name="delegates" /> 包含零个元素，或 <paramref name="delegates" /> 中的每项均为 null，则返回 null。</returns>
    /// <param name="delegates">要组合的委托数组。</param>
    /// <exception cref="T:System.ArgumentException">Not all the non-null entries in <paramref name="delegates" /> are instances of the same delegate type. </exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static Delegate Combine(params Delegate[] delegates)
    {
      if (delegates == null || delegates.Length == 0)
        return (Delegate) null;
      Delegate a = delegates[0];
      for (int index = 1; index < delegates.Length; ++index)
        a = Delegate.Combine(a, delegates[index]);
      return a;
    }

    /// <summary>返回委托的调用列表。</summary>
    /// <returns>委托构成的数组，表示当前委托的调用列表。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Delegate[] GetInvocationList()
    {
      return new Delegate[1]{ this };
    }

    /// <summary>获取当前委托所表示的静态方法。</summary>
    /// <returns>描述当前委托表示的静态方法的 <see cref="T:System.Reflection.MethodInfo" />。</returns>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
    [SecuritySafeCritical]
    protected virtual MethodInfo GetMethodImpl()
    {
      if (this._methodBase == null || !(this._methodBase is MethodInfo))
      {
        IRuntimeMethodInfo methodHandle = this.FindMethodHandle();
        RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(methodHandle);
        if ((RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType)) && (uint) (RuntimeMethodHandle.GetAttributes(methodHandle) & MethodAttributes.Static) <= 0U)
        {
          if (this._methodPtrAux == (IntPtr) 0)
          {
            Type type = this._target.GetType();
            Type genericTypeDefinition = runtimeType.GetGenericTypeDefinition();
            for (; type != (Type) null; type = type.BaseType)
            {
              if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
              {
                runtimeType = type as RuntimeType;
                break;
              }
            }
          }
          else
            runtimeType = (RuntimeType) this.GetType().GetMethod("Invoke").GetParameters()[0].ParameterType;
        }
        this._methodBase = (object) (MethodInfo) RuntimeType.GetMethodBase(runtimeType, methodHandle);
      }
      return (MethodInfo) this._methodBase;
    }

    /// <summary>从一个委托的调用列表中移除另一个委托的最后一个调用列表。</summary>
    /// <returns>一个新委托，其调用列表的构成方法为：获取 <paramref name="source" /> 的调用列表，如果在 <paramref name="source" /> 的调用列表中找到了 <paramref name="value" /> 的调用列表，则从中移除 <paramref name="value" /> 的最后一个调用列表。如果 <paramref name="value" /> 为 null，或在 <paramref name="source" /> 的调用列表中没有找到 <paramref name="value" /> 的调用列表，则返回 <paramref name="source" />。如果 <paramref name="value" /> 的调用列表等于 <paramref name="source" /> 的调用列表，或 <paramref name="source" /> 为空引用，则返回空引用。</returns>
    /// <param name="source">委托，将从中移除 <paramref name="value" /> 的调用列表。</param>
    /// <param name="value">委托，它提供将从其中移除 <paramref name="source" /> 的调用列表的调用列表。</param>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
    /// <exception cref="T:System.ArgumentException">The delegate types do not match.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Delegate Remove(Delegate source, Delegate value)
    {
      if (source == null)
        return (Delegate) null;
      if (value == null)
        return source;
      if (!Delegate.InternalEqualTypes((object) source, (object) value))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTypeMis"));
      return source.RemoveImpl(value);
    }

    /// <summary>从一个委托的调用列表中移除另一个委托的所有调用列表。</summary>
    /// <returns>一个新委托，其调用列表的构成方法为：获取 <paramref name="source" /> 的调用列表，如果在 <paramref name="source" /> 的调用列表中找到了 <paramref name="value" /> 的调用列表，则从中移除 <paramref name="value" /> 的所有调用列表。如果 <paramref name="value" /> 为 null，或在 <paramref name="source" /> 的调用列表中没有找到 <paramref name="value" /> 的调用列表，则返回 <paramref name="source" />。如果 <paramref name="value" /> 的调用列表等于 <paramref name="source" /> 的调用列表，如果 <paramref name="source" /> 只包含等于 <paramref name="value" /> 的调用列表的一系列调用列表，或者如果 <paramref name="source" /> 为空引用，则返回空引用。</returns>
    /// <param name="source">委托，将从中移除 <paramref name="value" /> 的调用列表。</param>
    /// <param name="value">委托，它提供将从其中移除 <paramref name="source" /> 的调用列表的调用列表。</param>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
    /// <exception cref="T:System.ArgumentException">The delegate types do not match.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Delegate RemoveAll(Delegate source, Delegate value)
    {
      Delegate @delegate;
      do
      {
        @delegate = source;
        source = Delegate.Remove(source, value);
      }
      while (@delegate != source);
      return @delegate;
    }

    /// <summary>将指定多路广播（可组合）委托和当前多路广播（可组合）委托的调用列表连接起来。</summary>
    /// <returns>新的多路广播（可组合）委托，其调用列表将当前多路广播（可组合）委托的调用列表和 <paramref name="d" /> 的调用列表连接在一起；或者如果 <paramref name="d" /> 为 null，则返回当前多路广播（可组合）委托。</returns>
    /// <param name="d">多路广播（可组合）委托，其调用列表要追加到当前多路广播（可组合）委托的调用列表的结尾。</param>
    /// <exception cref="T:System.MulticastNotSupportedException">Always thrown. </exception>
    protected virtual Delegate CombineImpl(Delegate d)
    {
      throw new MulticastNotSupportedException(Environment.GetResourceString("Multicast_Combine"));
    }

    /// <summary>从一个委托的调用列表中移除另一个委托的调用列表。</summary>
    /// <returns>一个新委托，其调用列表的构成方法为：获取当前委托的调用列表，如果在当前委托的调用列表中找到了 <paramref name="value" /> 的调用列表，则从中移除 <paramref name="value" /> 的调用列表。如果 <paramref name="value" /> 为 null，或者在当前委托的调用列表中没有找到 <paramref name="value" /> 的调用列表，则返回当前委托。如果 <paramref name="value" /> 的调用列表等于当前委托的调用列表，则返回 null。</returns>
    /// <param name="d">委托，它提供要从当前委托的调用列表中移除的调用列表。</param>
    /// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private). </exception>
    protected virtual Delegate RemoveImpl(Delegate d)
    {
      if (!d.Equals((object) this))
        return this;
      return (Delegate) null;
    }

    /// <summary>创建委托的浅表副本。</summary>
    /// <returns>委托的浅表副本。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual object Clone()
    {
      return this.MemberwiseClone();
    }

    /// <summary>创建指定类型的委托，该委托表示要对指定的类实例调用的指定实例方法。</summary>
    /// <returns>指定的类型的委托，表示要对指定的类实例调用的指定的实例方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="target">类实例，对其调用 <paramref name="method" />。</param>
    /// <param name="method">委托要表示的实例方法的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />. -or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or- <paramref name="method" /> is not an instance method. -or-<paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static Delegate CreateDelegate(Type type, object target, string method)
    {
      return Delegate.CreateDelegate(type, target, method, false, true);
    }

    /// <summary>创建指定类型的委托，该委托表示要按指定的大小写敏感度对指定类实例调用的指定实例方法。</summary>
    /// <returns>指定的类型的委托，表示要对指定的类实例调用的指定的实例方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="target">类实例，对其调用 <paramref name="method" />。</param>
    /// <param name="method">委托要表示的实例方法的名称。</param>
    /// <param name="ignoreCase">一个布尔值，它指示在比较方法名称时是否忽略大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or- <paramref name="method" /> is not an instance method. -or-<paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
    {
      return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
    }

    /// <summary>使用用于指定是否区分大小写的值和针对绑定失败的指定行为，创建指定类型的委托，该委托表示要对指定类实例调用的指定实例方法。</summary>
    /// <returns>指定的类型的委托，表示要对指定的类实例调用的指定的实例方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="target">类实例，对其调用 <paramref name="method" />。</param>
    /// <param name="method">委托要表示的实例方法的名称。</param>
    /// <param name="ignoreCase">一个布尔值，它指示在比较方法名称时是否忽略大小写。 </param>
    /// <param name="throwOnBindFailure">为 true，表示无法绑定 <paramref name="method" /> 时引发异常；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-  <paramref name="method" /> is not an instance method. -or-<paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is true.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (target == null)
        throw new ArgumentNullException("target");
      if (method == null)
        throw new ArgumentNullException("method");
      RuntimeType type1 = type as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (type1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      if (!type1.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      Delegate delegate1 = (Delegate) Delegate.InternalAlloc(type1);
      Delegate delegate2 = delegate1;
      object target1 = target;
      RuntimeType methodType = (RuntimeType) target1.GetType();
      string method1 = method;
      int num = 26 | (ignoreCase ? 32 : 0);
      if (!delegate2.BindToMethodName(target1, methodType, method1, (DelegateBindingFlags) num))
      {
        if (throwOnBindFailure)
          throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
        delegate1 = (Delegate) null;
      }
      return delegate1;
    }

    /// <summary>创建指定类型的委托，该委托表示指定类的指定静态方法。</summary>
    /// <returns>指定类型的委托，该委托表示指定类的指定静态方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="target">表示实现 <paramref name="method" /> 的类的 <see cref="T:System.Type" />。</param>
    /// <param name="method">委托要表示的静态方法的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or- <paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="target" /> is not a RuntimeType.-or-<paramref name="target" /> is an open generic type.That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is true.-or-<paramref name="method" /> is not a static method (Shared method in Visual Basic). -or-<paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is true.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static Delegate CreateDelegate(Type type, Type target, string method)
    {
      return Delegate.CreateDelegate(type, target, method, false, true);
    }

    /// <summary>使用用于指定是否区分大小写的值创建指定类型的委托，该委托表示指定类的指定静态方法。</summary>
    /// <returns>指定类型的委托，该委托表示指定类的指定静态方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="target">表示实现 <paramref name="method" /> 的类的 <see cref="T:System.Type" />。</param>
    /// <param name="method">委托要表示的静态方法的名称。</param>
    /// <param name="ignoreCase">一个布尔值，它指示在比较方法名称时是否忽略大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or- <paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="target" /> is not a RuntimeType.-or-<paramref name="target" /> is an open generic type.That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is true.-or-<paramref name="method" /> is not a static method (Shared method in Visual Basic). -or-<paramref name="method" /> cannot be bound, for example because it cannot be found.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
    {
      return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
    }

    /// <summary>使用用于指定是否区分大小写的值和针对绑定失败的指定行为，创建指定类型的委托，该委托表示指定类的指定静态方法。</summary>
    /// <returns>指定类型的委托，该委托表示指定类的指定静态方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="target">表示实现 <paramref name="method" /> 的类的 <see cref="T:System.Type" />。</param>
    /// <param name="method">委托要表示的静态方法的名称。</param>
    /// <param name="ignoreCase">一个布尔值，它指示在比较方法名称时是否忽略大小写。</param>
    /// <param name="throwOnBindFailure">为 true，表示无法绑定 <paramref name="method" /> 时引发异常；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="target" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or- <paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="target" /> is not a RuntimeType.-or-<paramref name="target" /> is an open generic type.That is, its <see cref="P:System.Type.ContainsGenericParameters" /> property is true.-or-<paramref name="method" /> is not a static method (Shared method in Visual Basic). -or-<paramref name="method" /> cannot be bound, for example because it cannot be found, and <paramref name="throwOnBindFailure" /> is true. </exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (target == (Type) null)
        throw new ArgumentNullException("target");
      if (target.IsGenericType && target.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), "target");
      if (method == null)
        throw new ArgumentNullException("method");
      RuntimeType type1 = type as RuntimeType;
      RuntimeType methodType = target as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (type1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      if (methodType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "target");
      if (!type1.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      Delegate @delegate = (Delegate) Delegate.InternalAlloc(type1);
      if (!@delegate.BindToMethodName((object) null, methodType, method, (DelegateBindingFlags) (5 | (ignoreCase ? 32 : 0))))
      {
        if (throwOnBindFailure)
          throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
        @delegate = (Delegate) null;
      }
      return @delegate;
    }

    /// <summary>使用针对绑定失败的指定行为，创建用于表示指定静态方法的指定类型的委托。</summary>
    /// <returns>表示指定静态方法的指定类型的委托。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="method">描述委托要表示的静态或实例方法的 <see cref="T:System.Reflection.MethodInfo" />。</param>
    /// <param name="throwOnBindFailure">为 true，表示无法绑定 <paramref name="method" /> 时引发异常；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="method" /> cannot be bound, and <paramref name="throwOnBindFailure" /> is true.-or-<paramref name="method" /> is not a RuntimeMethodInfo.See 反射中的运行时类型.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (method == (MethodInfo) null)
        throw new ArgumentNullException("method");
      RuntimeType rtType = type as RuntimeType;
      // ISSUE: variable of the null type
      __Null local1 = null;
      if (rtType == (RuntimeType) local1)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
      if (!rtType.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
      RuntimeMethodInfo rtMethod = runtimeMethodInfo;
      // ISSUE: variable of the null type
      __Null local2 = null;
      int num = 132;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark = @stackCrawlMark;
      Delegate delegateInternal = Delegate.CreateDelegateInternal(rtType, rtMethod, (object) local2, (DelegateBindingFlags) num, stackMark);
      // ISSUE: variable of the null type
      __Null local3 = null;
      if (!(delegateInternal == local3 & throwOnBindFailure))
        return delegateInternal;
      throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
    }

    /// <summary>使用指定的第一个参数创建指定类型的委托，该委托表示指定的静态方法或实例方法。</summary>
    /// <returns>指定类型的委托，表示指定的静态或实例方法。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="firstArgument">委托要绑定到的对象，或为 null，后者表示将 <paramref name="method" /> 视为 static（在 Visual Basic 中为 Shared）。</param>
    /// <param name="method">描述委托要表示的静态或实例方法的 <see cref="T:System.Reflection.MethodInfo" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="method" /> cannot be bound.-or-<paramref name="method" /> is not a RuntimeMethodInfo.See 反射中的运行时类型.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
    {
      return Delegate.CreateDelegate(type, firstArgument, method, true);
    }

    /// <summary>使用指定的第一个参数和针对绑定失败的指定行为，创建表示指定的静态方法或实例方法的指定类型的委托。</summary>
    /// <returns>表示指定的静态方法或实例方法的指定类型的委托，如果 <paramref name="throwOnBindFailure" /> 为 false，并且委托无法绑定到 <paramref name="method" />，则为 null。</returns>
    /// <param name="type">一个 <see cref="T:System.Type" />，表示要创建的委托的类型。</param>
    /// <param name="firstArgument">一个 <see cref="T:System.Object" />，它是委托表示的方法的第一个参数。对于实例方法，它必须与实例类型兼容。</param>
    /// <param name="method">描述委托要表示的静态或实例方法的 <see cref="T:System.Reflection.MethodInfo" />。</param>
    /// <param name="throwOnBindFailure">为 true，表示无法绑定 <paramref name="method" /> 时引发异常；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or-<paramref name="method" /> cannot be bound, and <paramref name="throwOnBindFailure" /> is true.-or-<paramref name="method" /> is not a RuntimeMethodInfo.See 反射中的运行时类型.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (method == (MethodInfo) null)
        throw new ArgumentNullException("method");
      RuntimeType rtType = type as RuntimeType;
      // ISSUE: variable of the null type
      __Null local1 = null;
      if (rtType == (RuntimeType) local1)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
      if (!rtType.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
      RuntimeMethodInfo rtMethod = runtimeMethodInfo;
      object firstArgument1 = firstArgument;
      int num = 128;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark = @stackCrawlMark;
      Delegate delegateInternal = Delegate.CreateDelegateInternal(rtType, rtMethod, firstArgument1, (DelegateBindingFlags) num, stackMark);
      // ISSUE: variable of the null type
      __Null local2 = null;
      if (!(delegateInternal == local2 & throwOnBindFailure))
        return delegateInternal;
      throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
    }

    /// <summary>不支持。</summary>
    /// <param name="info">不支持。</param>
    /// <param name="context">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException();
    }

    [SecurityCritical]
    internal static Delegate CreateDelegateNoSecurityCheck(Type type, object target, RuntimeMethodHandle method)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (method.IsNullHandle())
        throw new ArgumentNullException("method");
      RuntimeType type1 = type as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (type1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      if (!type1.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      MulticastDelegate multicastDelegate = Delegate.InternalAlloc(type1);
      object target1 = target;
      IRuntimeMethodInfo methodInfo = method.GetMethodInfo();
      RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method.GetMethodInfo());
      int num = 192;
      if (multicastDelegate.BindToMethodInfo(target1, methodInfo, declaringType, (DelegateBindingFlags) num))
        return (Delegate) multicastDelegate;
      throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
    }

    [SecurityCritical]
    internal static Delegate CreateDelegateNoSecurityCheck(RuntimeType type, object firstArgument, MethodInfo method)
    {
      if (type == (RuntimeType) null)
        throw new ArgumentNullException("type");
      if (method == (MethodInfo) null)
        throw new ArgumentNullException("method");
      RuntimeMethodInfo rtMethod = method as RuntimeMethodInfo;
      if ((MethodInfo) rtMethod == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
      if (!type.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      Delegate @delegate = Delegate.UnsafeCreateDelegate(type, rtMethod, firstArgument, DelegateBindingFlags.SkipSecurityChecks | DelegateBindingFlags.RelaxedSignature);
      if (@delegate != null)
        return @delegate;
      throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
    }

    /// <summary>创建指定类型的委托以表示指定的静态方法。</summary>
    /// <returns>表示指定静态方法的指定类型的委托。</returns>
    /// <param name="type">要创建的委托的 <see cref="T:System.Type" />。</param>
    /// <param name="method">描述委托要表示的静态或实例方法的 <see cref="T:System.Reflection.MethodInfo" />。.NET Framework 1.0 和 1.1 版中仅支持静态方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> is null.-or- <paramref name="method" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> does not inherit <see cref="T:System.MulticastDelegate" />.-or-<paramref name="type" /> is not a RuntimeType.See 反射中的运行时类型.-or- <paramref name="method" /> is not a static method, and the .NET Framework version is 1.0 or 1.1. -or-<paramref name="method" /> cannot be bound.-or-<paramref name="method" /> is not a RuntimeMethodInfo.See 反射中的运行时类型.</exception>
    /// <exception cref="T:System.MissingMethodException">The Invoke method of <paramref name="type" /> is not found. </exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have the permissions necessary to access <paramref name="method" />. </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static Delegate CreateDelegate(Type type, MethodInfo method)
    {
      return Delegate.CreateDelegate(type, method, true);
    }

    [SecuritySafeCritical]
    internal static Delegate CreateDelegateInternal(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags, ref StackCrawlMark stackMark)
    {
      bool flag1 = (rtMethod.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
      bool flag2 = (rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
      if (flag1 | flag2)
      {
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) (flag1 ? rtMethod.FullName : rtType.FullName)));
      }
      return Delegate.UnsafeCreateDelegate(rtType, rtMethod, firstArgument, flags);
    }

    [SecurityCritical]
    internal static Delegate UnsafeCreateDelegate(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags)
    {
      Delegate @delegate = (Delegate) Delegate.InternalAlloc(rtType);
      if (@delegate.BindToMethodInfo(firstArgument, (IRuntimeMethodInfo) rtMethod, rtMethod.GetDeclaringTypeInternal(), flags))
        return @delegate;
      return (Delegate) null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool BindToMethodName(object target, RuntimeType methodType, string method, DelegateBindingFlags flags);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool BindToMethodInfo(object target, IRuntimeMethodInfo method, RuntimeType methodType, DelegateBindingFlags flags);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern MulticastDelegate InternalAlloc(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MulticastDelegate InternalAllocLike(Delegate d);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool InternalEqualTypes(object a, object b);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void DelegateConstruct(object target, IntPtr slot);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetMulticastInvoke();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetInvokeMethod();

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IRuntimeMethodInfo FindMethodHandle();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool InternalEqualMethodHandles(Delegate left, Delegate right);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr AdjustTarget(object target, IntPtr methodPtr);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetCallStub(IntPtr methodPtr);

    [SecuritySafeCritical]
    internal virtual object GetTarget()
    {
      if (!this._methodPtrAux.IsNull())
        return (object) null;
      return this._target;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CompareUnmanagedFunctionPtrs(Delegate d1, Delegate d2);
  }
}
