// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.CustomPropertyImpl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class CustomPropertyImpl : ICustomProperty
  {
    private PropertyInfo m_property;

    public string Name
    {
      get
      {
        return this.m_property.Name;
      }
    }

    public bool CanRead
    {
      get
      {
        return this.m_property.GetGetMethod() != (MethodInfo) null;
      }
    }

    public bool CanWrite
    {
      get
      {
        return this.m_property.GetSetMethod() != (MethodInfo) null;
      }
    }

    public Type Type
    {
      get
      {
        return this.m_property.PropertyType;
      }
    }

    public CustomPropertyImpl(PropertyInfo propertyInfo)
    {
      if (propertyInfo == (PropertyInfo) null)
        throw new ArgumentNullException("propertyInfo");
      this.m_property = propertyInfo;
    }

    public object GetValue(object target)
    {
      return this.InvokeInternal(target, (object[]) null, true);
    }

    public object GetValue(object target, object indexValue)
    {
      return this.InvokeInternal(target, new object[1]{ indexValue }, 1 != 0);
    }

    public void SetValue(object target, object value)
    {
      this.InvokeInternal(target, new object[1]{ value }, 0 != 0);
    }

    public void SetValue(object target, object value, object indexValue)
    {
      this.InvokeInternal(target, new object[2]
      {
        indexValue,
        value
      }, 0 != 0);
    }

    [SecuritySafeCritical]
    private object InvokeInternal(object target, object[] args, bool getValue)
    {
      IGetProxyTarget getProxyTarget = target as IGetProxyTarget;
      if (getProxyTarget != null)
        target = getProxyTarget.GetTarget();
      MethodInfo methodInfo = getValue ? this.m_property.GetGetMethod(true) : this.m_property.GetSetMethod(true);
      if (methodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString(getValue ? "Arg_GetMethNotFnd" : "Arg_SetMethNotFnd"));
      if (!methodInfo.IsPublic)
        throw new MethodAccessException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_MethodAccessException_WithMethodName"), (object) methodInfo.ToString(), (object) methodInfo.DeclaringType.FullName));
      RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
      // ISSUE: variable of the null type
      __Null local1 = null;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) local1)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
      object obj = target;
      int num = 0;
      // ISSUE: variable of the null type
      __Null local2 = null;
      object[] parameters = args;
      // ISSUE: variable of the null type
      __Null local3 = null;
      return runtimeMethodInfo.UnsafeInvoke(obj, (BindingFlags) num, (Binder) local2, parameters, (CultureInfo) local3);
    }
  }
}
