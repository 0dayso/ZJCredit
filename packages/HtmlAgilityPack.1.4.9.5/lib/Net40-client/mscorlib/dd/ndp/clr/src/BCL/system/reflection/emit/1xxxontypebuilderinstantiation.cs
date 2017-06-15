// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ConstructorOnTypeBuilderInstantiation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection.Emit
{
  internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
  {
    internal ConstructorInfo m_ctor;
    private TypeBuilderInstantiation m_type;

    public override MemberTypes MemberType
    {
      get
      {
        return this.m_ctor.MemberType;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_ctor.Name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_type;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return (Type) this.m_type;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        ConstructorBuilder constructorBuilder = this.m_ctor as ConstructorBuilder;
        if ((ConstructorInfo) constructorBuilder != (ConstructorInfo) null)
          return constructorBuilder.MetadataTokenInternal;
        return this.m_ctor.MetadataToken;
      }
    }

    public override Module Module
    {
      get
      {
        return this.m_ctor.Module;
      }
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        return this.m_ctor.MethodHandle;
      }
    }

    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_ctor.Attributes;
      }
    }

    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_ctor.CallingConvention;
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
        return false;
      }
    }

    public override bool IsGenericMethod
    {
      get
      {
        return false;
      }
    }

    internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
    {
      this.m_ctor = constructor;
      this.m_type = type;
    }

    internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
    {
      return (ConstructorInfo) new ConstructorOnTypeBuilderInstantiation(Constructor, type);
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_ctor.GetParameterTypes();
    }

    internal override Type GetReturnType()
    {
      return this.DeclaringType;
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_ctor.GetCustomAttributes(inherit);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_ctor.GetCustomAttributes(attributeType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_ctor.IsDefined(attributeType, inherit);
    }

    public new Type GetType()
    {
      return base.GetType();
    }

    public override ParameterInfo[] GetParameters()
    {
      return this.m_ctor.GetParameters();
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_ctor.GetMethodImplementationFlags();
    }

    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public override Type[] GetGenericArguments()
    {
      return this.m_ctor.GetGenericArguments();
    }

    public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new InvalidOperationException();
    }
  }
}
