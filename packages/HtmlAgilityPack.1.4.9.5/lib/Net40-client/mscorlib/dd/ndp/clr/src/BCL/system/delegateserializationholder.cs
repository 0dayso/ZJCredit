// Decompiled with JetBrains decompiler
// Type: System.DelegateSerializationHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
  [Serializable]
  internal sealed class DelegateSerializationHolder : IObjectReference, ISerializable
  {
    private DelegateSerializationHolder.DelegateEntry m_delegateEntry;
    private MethodInfo[] m_methods;

    [SecurityCritical]
    private DelegateSerializationHolder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      bool flag = true;
      try
      {
        this.m_delegateEntry = (DelegateSerializationHolder.DelegateEntry) info.GetValue("Delegate", typeof (DelegateSerializationHolder.DelegateEntry));
      }
      catch
      {
        this.m_delegateEntry = this.OldDelegateWireFormat(info, context);
        flag = false;
      }
      if (!flag)
        return;
      DelegateSerializationHolder.DelegateEntry delegateEntry = this.m_delegateEntry;
      int length = 0;
      for (; delegateEntry != null; delegateEntry = delegateEntry.delegateEntry)
      {
        if (delegateEntry.target != null)
        {
          string name = delegateEntry.target as string;
          if (name != null)
            delegateEntry.target = info.GetValue(name, typeof (object));
        }
        ++length;
      }
      MethodInfo[] methodInfoArray = new MethodInfo[length];
      int index;
      for (index = 0; index < length; ++index)
      {
        string name = "method" + (object) index;
        methodInfoArray[index] = (MethodInfo) info.GetValueNoThrow(name, typeof (MethodInfo));
        if (methodInfoArray[index] == (MethodInfo) null)
          break;
      }
      if (index != length)
        return;
      this.m_methods = methodInfoArray;
    }

    [SecurityCritical]
    internal static DelegateSerializationHolder.DelegateEntry GetDelegateSerializationInfo(SerializationInfo info, Type delegateType, object target, MethodInfo method, int targetIndex)
    {
      if (method == (MethodInfo) null)
        throw new ArgumentNullException("method");
      if (!method.IsPublic || method.DeclaringType != (Type) null && !method.DeclaringType.IsVisible)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      Type baseType = delegateType.BaseType;
      if (baseType == (Type) null || baseType != typeof (Delegate) && baseType != typeof (MulticastDelegate))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
      if (method.DeclaringType == (Type) null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalMethodSerialization"));
      DelegateSerializationHolder.DelegateEntry delegateEntry = new DelegateSerializationHolder.DelegateEntry(delegateType.FullName, delegateType.Module.Assembly.FullName, target, method.ReflectedType.Module.Assembly.FullName, method.ReflectedType.FullName, method.Name);
      if (info.MemberCount == 0)
      {
        info.SetType(typeof (DelegateSerializationHolder));
        info.AddValue("Delegate", (object) delegateEntry, typeof (DelegateSerializationHolder.DelegateEntry));
      }
      if (target != null)
      {
        string name = "target" + (object) targetIndex;
        info.AddValue(name, delegateEntry.target);
        delegateEntry.target = (object) name;
      }
      string name1 = "method" + (object) targetIndex;
      info.AddValue(name1, (object) method);
      return delegateEntry;
    }

    private void ThrowInsufficientState(string field)
    {
      throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientDeserializationState", (object) field));
    }

    private DelegateSerializationHolder.DelegateEntry OldDelegateWireFormat(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      string string1 = info.GetString("DelegateType");
      string string2 = info.GetString("DelegateAssembly");
      object obj = info.GetValue("Target", typeof (object));
      string string3 = info.GetString("TargetTypeAssembly");
      string string4 = info.GetString("TargetTypeName");
      string string5 = info.GetString("MethodName");
      string assembly = string2;
      object target = obj;
      string targetTypeAssembly = string3;
      string targetTypeName = string4;
      string methodName = string5;
      return new DelegateSerializationHolder.DelegateEntry(string1, assembly, target, targetTypeAssembly, targetTypeName, methodName);
    }

    [SecurityCritical]
    private Delegate GetDelegate(DelegateSerializationHolder.DelegateEntry de, int index)
    {
      Delegate @delegate;
      try
      {
        if (de.methodName == null || de.methodName.Length == 0)
          this.ThrowInsufficientState("MethodName");
        if (de.assembly == null || de.assembly.Length == 0)
          this.ThrowInsufficientState("DelegateAssembly");
        if (de.targetTypeName == null || de.targetTypeName.Length == 0)
          this.ThrowInsufficientState("TargetTypeName");
        RuntimeType type1 = (RuntimeType) Assembly.GetType_Compat(de.assembly, de.type);
        RuntimeType type2 = (RuntimeType) Assembly.GetType_Compat(de.targetTypeAssembly, de.targetTypeName);
        if (this.m_methods != null)
        {
          object firstArgument = de.target != null ? RemotingServices.CheckCast(de.target, type2) : (object) null;
          @delegate = Delegate.CreateDelegateNoSecurityCheck(type1, firstArgument, this.m_methods[index]);
        }
        else
          @delegate = de.target == null ? Delegate.CreateDelegate((Type) type1, (Type) type2, de.methodName) : Delegate.CreateDelegate((Type) type1, RemotingServices.CheckCast(de.target, type2), de.methodName);
        if (!(@delegate.Method != (MethodInfo) null) || @delegate.Method.IsPublic)
        {
          if (@delegate.Method.DeclaringType != (Type) null)
          {
            if (@delegate.Method.DeclaringType.IsVisible)
              goto label_16;
          }
          else
            goto label_16;
        }
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      }
      catch (Exception ex)
      {
        if (ex is SerializationException)
          throw ex;
        throw new SerializationException(ex.Message, ex);
      }
label_16:
      return @delegate;
    }

    [SecurityCritical]
    public object GetRealObject(StreamingContext context)
    {
      int length1 = 0;
      for (DelegateSerializationHolder.DelegateEntry delegateEntry = this.m_delegateEntry; delegateEntry != null; delegateEntry = delegateEntry.Entry)
        ++length1;
      int num = length1 - 1;
      if (length1 == 1)
        return (object) this.GetDelegate(this.m_delegateEntry, 0);
      object[] objArray = new object[length1];
      for (DelegateSerializationHolder.DelegateEntry de = this.m_delegateEntry; de != null; de = de.Entry)
      {
        --length1;
        objArray[length1] = (object) this.GetDelegate(de, num - length1);
      }
      MulticastDelegate multicastDelegate = (MulticastDelegate) objArray[0];
      object[] invocationList = objArray;
      int length2 = invocationList.Length;
      return (object) multicastDelegate.NewMulticastDelegate(invocationList, length2);
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DelegateSerHolderSerial"));
    }

    [Serializable]
    internal class DelegateEntry
    {
      internal string type;
      internal string assembly;
      internal object target;
      internal string targetTypeAssembly;
      internal string targetTypeName;
      internal string methodName;
      internal DelegateSerializationHolder.DelegateEntry delegateEntry;

      internal DelegateSerializationHolder.DelegateEntry Entry
      {
        get
        {
          return this.delegateEntry;
        }
        set
        {
          this.delegateEntry = value;
        }
      }

      internal DelegateEntry(string type, string assembly, object target, string targetTypeAssembly, string targetTypeName, string methodName)
      {
        this.type = type;
        this.assembly = assembly;
        this.target = target;
        this.targetTypeAssembly = targetTypeAssembly;
        this.targetTypeName = targetTypeName;
        this.methodName = methodName;
      }
    }
  }
}
