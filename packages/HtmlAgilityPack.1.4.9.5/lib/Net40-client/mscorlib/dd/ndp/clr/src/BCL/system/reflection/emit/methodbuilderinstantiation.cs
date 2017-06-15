// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodBuilderInstantiation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection.Emit
{
  internal sealed class MethodBuilderInstantiation : MethodInfo
  {
    internal MethodInfo m_method;
    private Type[] m_inst;

    public override MemberTypes MemberType
    {
      get
      {
        return this.m_method.MemberType;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_method.Name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return this.m_method.DeclaringType;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return this.m_method.ReflectedType;
      }
    }

    public override Module Module
    {
      get
      {
        return this.m_method.Module;
      }
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_method.Attributes;
      }
    }

    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_method.CallingConvention;
      }
    }

    public override bool IsGenericMethodDefinition
    {
      get
      {
        return false;
      }
    }

    public override bool ContainsGenericParameters
    {
      get
      {
        for (int index = 0; index < this.m_inst.Length; ++index)
        {
          if (this.m_inst[index].ContainsGenericParameters)
            return true;
        }
        return this.DeclaringType != (Type) null && this.DeclaringType.ContainsGenericParameters;
      }
    }

    public override bool IsGenericMethod
    {
      get
      {
        return true;
      }
    }

    public override Type ReturnType
    {
      get
      {
        return this.m_method.ReturnType;
      }
    }

    public override ParameterInfo ReturnParameter
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    internal MethodBuilderInstantiation(MethodInfo method, Type[] inst)
    {
      this.m_method = method;
      this.m_inst = inst;
    }

    internal static MethodInfo MakeGenericMethod(MethodInfo method, Type[] inst)
    {
      if (!method.IsGenericMethodDefinition)
        throw new InvalidOperationException();
      return (MethodInfo) new MethodBuilderInstantiation(method, inst);
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_method.GetParameterTypes();
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_method.GetCustomAttributes(inherit);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_method.GetCustomAttributes(attributeType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_method.IsDefined(attributeType, inherit);
    }

    public new Type GetType()
    {
      return base.GetType();
    }

    public override ParameterInfo[] GetParameters()
    {
      throw new NotSupportedException();
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_method.GetMethodImplementationFlags();
    }

    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public override Type[] GetGenericArguments()
    {
      return this.m_inst;
    }

    public override MethodInfo GetGenericMethodDefinition()
    {
      return this.m_method;
    }

    public override MethodInfo MakeGenericMethod(params Type[] arguments)
    {
      throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
    }

    public override MethodInfo GetBaseDefinition()
    {
      throw new NotSupportedException();
    }
  }
}
