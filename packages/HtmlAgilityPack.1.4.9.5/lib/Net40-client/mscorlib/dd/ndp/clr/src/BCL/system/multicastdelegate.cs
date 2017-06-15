// Decompiled with JetBrains decompiler
// Type: System.MulticastDelegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>表示多路广播委托；即，其调用列表中可以拥有多个元素的委托。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class MulticastDelegate : Delegate
  {
    [SecurityCritical]
    private object _invocationList;
    [SecurityCritical]
    private IntPtr _invocationCount;

    /// <summary>初始化 <see cref="T:System.MulticastDelegate" /> 类的新实例。</summary>
    /// <param name="target">在其上定义 <paramref name="method" /> 的对象。</param>
    /// <param name="method">为其创建委托的方法的名称。</param>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    protected MulticastDelegate(object target, string method)
      : base(target, method)
    {
    }

    /// <summary>初始化 <see cref="T:System.MulticastDelegate" /> 类的新实例。</summary>
    /// <param name="target">在其上定义 <paramref name="method" /> 的对象的类型。</param>
    /// <param name="method">为其创建委托的静态方法的名称。</param>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    protected MulticastDelegate(Type target, string method)
      : base(target, method)
    {
    }

    /// <summary>确定两个 <see cref="T:System.MulticastDelegate" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="d1" /> 和 <paramref name="d2" /> 具有相同的调用列表，则为 true；否则为 false。</returns>
    /// <param name="d1">左操作数。</param>
    /// <param name="d2">右操作数。 </param>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2)
    {
      if (d1 == null)
        return d2 == null;
      return d1.Equals((object) d2);
    }

    /// <summary>确定两个 <see cref="T:System.MulticastDelegate" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="d1" /> 和 <paramref name="d2" /> 不具有相同的调用列表，则为 true；否则为 false。</returns>
    /// <param name="d1">左操作数。</param>
    /// <param name="d2">右操作数。 </param>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
    {
      if (d1 == null)
        return d2 != null;
      return !d1.Equals((object) d2);
    }

    [SecuritySafeCritical]
    internal bool IsUnmanagedFunctionPtr()
    {
      return this._invocationCount == (IntPtr) -1;
    }

    [SecuritySafeCritical]
    internal bool InvocationListLogicallyNull()
    {
      if (this._invocationList != null && !(this._invocationList is LoaderAllocator))
        return this._invocationList is DynamicResolver;
      return true;
    }

    /// <summary>用序列化该实例所需的所有数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">一个对象，它保存将此实例序列化或反序列化所需的全部数据。</param>
    /// <param name="context">（保留）存储和检索序列化数据的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> is null. </exception>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">A serialization error occurred.</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      int targetIndex = 0;
      object[] objArray = this._invocationList as object[];
      if (objArray == null)
      {
        MethodInfo method = this.Method;
        if (!(method is RuntimeMethodInfo) || this.IsUnmanagedFunctionPtr())
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidDelegateType"));
        if (!this.InvocationListLogicallyNull() && !this._invocationCount.IsNull() && !this._methodPtrAux.IsNull())
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidDelegateType"));
        DelegateSerializationHolder.GetDelegateSerializationInfo(info, this.GetType(), this.Target, method, targetIndex);
      }
      else
      {
        DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry) null;
        int index = (int) this._invocationCount;
        while (--index >= 0)
        {
          MulticastDelegate multicastDelegate = (MulticastDelegate) objArray[index];
          MethodInfo method = multicastDelegate.Method;
          if (method is RuntimeMethodInfo && !this.IsUnmanagedFunctionPtr() && (multicastDelegate.InvocationListLogicallyNull() || multicastDelegate._invocationCount.IsNull() || multicastDelegate._methodPtrAux.IsNull()))
          {
            DelegateSerializationHolder.DelegateEntry serializationInfo = DelegateSerializationHolder.GetDelegateSerializationInfo(info, multicastDelegate.GetType(), multicastDelegate.Target, method, targetIndex++);
            if (delegateEntry != null)
              delegateEntry.Entry = serializationInfo;
            delegateEntry = serializationInfo;
          }
        }
        if (delegateEntry == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidDelegateType"));
      }
    }

    /// <summary>确定此多路广播委托和指定的对象是否相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 和此实例具有相同的调用列表，则为 true；否则为 false。</returns>
    /// <param name="obj">与该实例进行比较的对象。 </param>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override sealed bool Equals(object obj)
    {
      if (obj == null || !Delegate.InternalEqualTypes((object) this, obj))
        return false;
      MulticastDelegate d = obj as MulticastDelegate;
      if (d == null)
        return false;
      if (this._invocationCount != (IntPtr) 0)
      {
        if (this.InvocationListLogicallyNull())
        {
          if (this.IsUnmanagedFunctionPtr())
          {
            if (!d.IsUnmanagedFunctionPtr())
              return false;
            return Delegate.CompareUnmanagedFunctionPtrs((Delegate) this, (Delegate) d);
          }
          if (d._invocationList is Delegate)
            return this.Equals(d._invocationList);
          return base.Equals(obj);
        }
        if (this._invocationList is Delegate)
          return this._invocationList.Equals(obj);
        return this.InvocationListEquals(d);
      }
      if (!this.InvocationListLogicallyNull())
      {
        if (!this._invocationList.Equals(d._invocationList))
          return false;
        return base.Equals((object) d);
      }
      if (d._invocationList is Delegate)
        return this.Equals(d._invocationList);
      return base.Equals((object) d);
    }

    [SecuritySafeCritical]
    private bool InvocationListEquals(MulticastDelegate d)
    {
      object[] objArray = this._invocationList as object[];
      if (d._invocationCount != this._invocationCount)
        return false;
      int num = (int) this._invocationCount;
      for (int index = 0; index < num; ++index)
      {
        if (!((Delegate) objArray[index]).Equals((d._invocationList as object[])[index]))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    private bool TrySetSlot(object[] a, int index, object o)
    {
      if (a[index] == null && Interlocked.CompareExchange<object>(ref a[index], o, (object) null) == null)
        return true;
      if (a[index] != null)
      {
        MulticastDelegate multicastDelegate1 = (MulticastDelegate) o;
        MulticastDelegate multicastDelegate2 = (MulticastDelegate) a[index];
        if (multicastDelegate2._methodPtr == multicastDelegate1._methodPtr && multicastDelegate2._target == multicastDelegate1._target && multicastDelegate2._methodPtrAux == multicastDelegate1._methodPtrAux)
          return true;
      }
      return false;
    }

    [SecurityCritical]
    private MulticastDelegate NewMulticastDelegate(object[] invocationList, int invocationCount, bool thisIsMultiCastAlready)
    {
      MulticastDelegate multicastDelegate1 = Delegate.InternalAllocLike((Delegate) this);
      if (thisIsMultiCastAlready)
      {
        multicastDelegate1._methodPtr = this._methodPtr;
        multicastDelegate1._methodPtrAux = this._methodPtrAux;
      }
      else
      {
        multicastDelegate1._methodPtr = this.GetMulticastInvoke();
        multicastDelegate1._methodPtrAux = this.GetInvokeMethod();
      }
      MulticastDelegate multicastDelegate2;
      multicastDelegate2._target = (object) (multicastDelegate2 = multicastDelegate1);
      multicastDelegate1._invocationList = (object) invocationList;
      multicastDelegate1._invocationCount = (IntPtr) invocationCount;
      return multicastDelegate1;
    }

    [SecurityCritical]
    internal MulticastDelegate NewMulticastDelegate(object[] invocationList, int invocationCount)
    {
      return this.NewMulticastDelegate(invocationList, invocationCount, false);
    }

    [SecurityCritical]
    internal void StoreDynamicMethod(MethodInfo dynamicMethod)
    {
      if (this._invocationCount != (IntPtr) 0)
        ((Delegate) this._invocationList)._methodBase = (object) dynamicMethod;
      else
        this._methodBase = (object) dynamicMethod;
    }

    /// <summary>将此 <see cref="T:System.Delegate" /> 与指定的 <see cref="T:System.Delegate" /> 合并，以形成一个新委托。</summary>
    /// <returns>一个委托，它是 <see cref="T:System.MulticastDelegate" /> 调用列表的新根。</returns>
    /// <param name="follow">将与此委托进行合并的委托。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="follow" /> does not have the same type as this instance.</exception>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    [SecuritySafeCritical]
    protected override sealed Delegate CombineImpl(Delegate follow)
    {
      if (follow == null)
        return (Delegate) this;
      if (!Delegate.InternalEqualTypes((object) this, (object) follow))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTypeMis"));
      MulticastDelegate multicastDelegate = (MulticastDelegate) follow;
      int num = 1;
      object[] objArray1 = multicastDelegate._invocationList as object[];
      if (objArray1 != null)
        num = (int) multicastDelegate._invocationCount;
      object[] objArray2 = this._invocationList as object[];
      if (objArray2 == null)
      {
        int invocationCount = 1 + num;
        object[] invocationList = new object[invocationCount];
        invocationList[0] = (object) this;
        if (objArray1 == null)
        {
          invocationList[1] = (object) multicastDelegate;
        }
        else
        {
          for (int index = 0; index < num; ++index)
            invocationList[1 + index] = objArray1[index];
        }
        return (Delegate) this.NewMulticastDelegate(invocationList, invocationCount);
      }
      int index1 = (int) this._invocationCount;
      int invocationCount1 = index1 + num;
      object[] objArray3 = (object[]) null;
      if (invocationCount1 <= objArray2.Length)
      {
        objArray3 = objArray2;
        if (objArray1 == null)
        {
          if (!this.TrySetSlot(objArray3, index1, (object) multicastDelegate))
            objArray3 = (object[]) null;
        }
        else
        {
          for (int index2 = 0; index2 < num; ++index2)
          {
            if (!this.TrySetSlot(objArray3, index1 + index2, objArray1[index2]))
            {
              objArray3 = (object[]) null;
              break;
            }
          }
        }
      }
      if (objArray3 == null)
      {
        int length = objArray2.Length;
        while (length < invocationCount1)
          length *= 2;
        objArray3 = new object[length];
        for (int index2 = 0; index2 < index1; ++index2)
          objArray3[index2] = objArray2[index2];
        if (objArray1 == null)
        {
          objArray3[index1] = (object) multicastDelegate;
        }
        else
        {
          for (int index2 = 0; index2 < num; ++index2)
            objArray3[index1 + index2] = objArray1[index2];
        }
      }
      return (Delegate) this.NewMulticastDelegate(objArray3, invocationCount1, true);
    }

    [SecurityCritical]
    private object[] DeleteFromInvocationList(object[] invocationList, int invocationCount, int deleteIndex, int deleteCount)
    {
      int length = (this._invocationList as object[]).Length;
      while (length / 2 >= invocationCount - deleteCount)
        length /= 2;
      object[] objArray = new object[length];
      for (int index = 0; index < deleteIndex; ++index)
        objArray[index] = invocationList[index];
      for (int index = deleteIndex + deleteCount; index < invocationCount; ++index)
        objArray[index - deleteCount] = invocationList[index];
      return objArray;
    }

    private bool EqualInvocationLists(object[] a, object[] b, int start, int count)
    {
      for (int index = 0; index < count; ++index)
      {
        if (!a[start + index].Equals(b[index]))
          return false;
      }
      return true;
    }

    /// <summary>从此 <see cref="T:System.MulticastDelegate" /> 的调用列表中移除与指定委托相等的元素。</summary>
    /// <returns>如果在此实例的调用列表中找到 <paramref name="value" />，则为其调用列表中没有 <paramref name="value" /> 的新 <see cref="T:System.Delegate" />；否则为此实例（带有其原始调用列表）。</returns>
    /// <param name="value">要在调用列表中搜索的委托。</param>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    [SecuritySafeCritical]
    protected override sealed Delegate RemoveImpl(Delegate value)
    {
      MulticastDelegate multicastDelegate = value as MulticastDelegate;
      if (multicastDelegate == null)
        return (Delegate) this;
      if (!(multicastDelegate._invocationList is object[]))
      {
        object[] invocationList = this._invocationList as object[];
        if (invocationList == null)
        {
          if (this.Equals((object) value))
            return (Delegate) null;
        }
        else
        {
          int invocationCount = (int) this._invocationCount;
          int deleteIndex = invocationCount;
          while (--deleteIndex >= 0)
          {
            if (value.Equals(invocationList[deleteIndex]))
            {
              if (invocationCount == 2)
                return (Delegate) invocationList[1 - deleteIndex];
              return (Delegate) this.NewMulticastDelegate(this.DeleteFromInvocationList(invocationList, invocationCount, deleteIndex, 1), invocationCount - 1, true);
            }
          }
        }
      }
      else
      {
        object[] objArray = this._invocationList as object[];
        if (objArray != null)
        {
          int invocationCount = (int) this._invocationCount;
          int num = (int) multicastDelegate._invocationCount;
          for (int index = invocationCount - num; index >= 0; --index)
          {
            if (this.EqualInvocationLists(objArray, multicastDelegate._invocationList as object[], index, num))
            {
              if (invocationCount - num == 0)
                return (Delegate) null;
              if (invocationCount - num == 1)
                return (Delegate) objArray[index != 0 ? 0 : invocationCount - 1];
              return (Delegate) this.NewMulticastDelegate(this.DeleteFromInvocationList(objArray, invocationCount, index, num), invocationCount - num, true);
            }
          }
        }
      }
      return (Delegate) this;
    }

    /// <summary>按照调用顺序返回此多路广播委托的调用列表。</summary>
    /// <returns>一个委托数组，这些委托的调用列表合起来与此实例的调用列表一致。</returns>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override sealed Delegate[] GetInvocationList()
    {
      object[] objArray = this._invocationList as object[];
      Delegate[] delegateArray;
      if (objArray == null)
      {
        delegateArray = new Delegate[1]
        {
          (Delegate) this
        };
      }
      else
      {
        int length = (int) this._invocationCount;
        delegateArray = new Delegate[length];
        for (int index = 0; index < length; ++index)
          delegateArray[index] = (Delegate) objArray[index];
      }
      return delegateArray;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override sealed int GetHashCode()
    {
      if (this.IsUnmanagedFunctionPtr())
        return ValueType.GetHashCodeOfPtr(this._methodPtr) ^ ValueType.GetHashCodeOfPtr(this._methodPtrAux);
      object[] objArray = this._invocationList as object[];
      if (objArray == null)
        return base.GetHashCode();
      int num = 0;
      for (int index = 0; index < (int) this._invocationCount; ++index)
        num = num * 33 + objArray[index].GetHashCode();
      return num;
    }

    [SecuritySafeCritical]
    internal override object GetTarget()
    {
      if (this._invocationCount != (IntPtr) 0)
      {
        if (this.InvocationListLogicallyNull())
          return (object) null;
        object[] objArray = this._invocationList as object[];
        if (objArray != null)
        {
          int num = (int) this._invocationCount;
          return ((Delegate) objArray[num - 1]).GetTarget();
        }
        Delegate @delegate = this._invocationList as Delegate;
        if (@delegate != null)
          return @delegate.GetTarget();
      }
      return base.GetTarget();
    }

    /// <summary>返回由当前的 <see cref="T:System.MulticastDelegate" /> 表示的静态方法。</summary>
    /// <returns>由当前的 <see cref="T:System.MulticastDelegate" /> 表示的静态方法。</returns>
    [SecuritySafeCritical]
    protected override MethodInfo GetMethodImpl()
    {
      if (this._invocationCount != (IntPtr) 0 && this._invocationList != null)
      {
        object[] objArray = this._invocationList as object[];
        if (objArray != null)
        {
          int index = (int) this._invocationCount - 1;
          return ((Delegate) objArray[index]).Method;
        }
        MulticastDelegate multicastDelegate = this._invocationList as MulticastDelegate;
        if (multicastDelegate != null)
          return multicastDelegate.GetMethodImpl();
      }
      else if (this.IsUnmanagedFunctionPtr())
      {
        if (this._methodBase == null || !(this._methodBase is MethodInfo))
        {
          IRuntimeMethodInfo methodHandle = this.FindMethodHandle();
          RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(methodHandle);
          if (RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType))
            runtimeType = this.GetType() as RuntimeType;
          this._methodBase = (object) (MethodInfo) RuntimeType.GetMethodBase(runtimeType, methodHandle);
        }
        return (MethodInfo) this._methodBase;
      }
      return base.GetMethodImpl();
    }

    [DebuggerNonUserCode]
    private void ThrowNullThisInDelegateToInstance()
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_DlgtNullInst"));
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorClosed(object target, IntPtr methodPtr)
    {
      if (target == null)
        this.ThrowNullThisInDelegateToInstance();
      this._target = target;
      this._methodPtr = methodPtr;
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorClosedStatic(object target, IntPtr methodPtr)
    {
      this._target = target;
      this._methodPtr = methodPtr;
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorRTClosed(object target, IntPtr methodPtr)
    {
      this._target = target;
      this._methodPtr = this.AdjustTarget(target, methodPtr);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = methodPtr;
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureClosed(object target, IntPtr methodPtr, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorClosed(target, methodPtr);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureClosedStatic(object target, IntPtr methodPtr, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorClosedStatic(target, methodPtr);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureRTClosed(object target, IntPtr methodPtr, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorRTClosed(target, methodPtr);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorOpened(target, methodPtr, shuffleThunk);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = this.GetCallStub(methodPtr);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorVirtualDispatch(target, methodPtr, shuffleThunk);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorCollectibleClosedStatic(object target, IntPtr methodPtr, IntPtr gchandle)
    {
      this._target = target;
      this._methodPtr = methodPtr;
      this._methodBase = GCHandle.InternalGet(gchandle);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorCollectibleOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr gchandle)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = methodPtr;
      this._methodBase = GCHandle.InternalGet(gchandle);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorCollectibleVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr gchandle)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = this.GetCallStub(methodPtr);
      this._methodBase = GCHandle.InternalGet(gchandle);
    }
  }
}
