// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.TypeBuilderInstantiation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  internal sealed class TypeBuilderInstantiation : TypeInfo
  {
    internal Hashtable m_hashtable = new Hashtable();
    private Type m_type;
    private Type[] m_inst;
    private string m_strFullQualName;

    public override Type DeclaringType
    {
      get
      {
        return this.m_type.DeclaringType;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return this.m_type.ReflectedType;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_type.Name;
      }
    }

    public override Module Module
    {
      get
      {
        return this.m_type.Module;
      }
    }

    public override Guid GUID
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public override Assembly Assembly
    {
      get
      {
        return this.m_type.Assembly;
      }
    }

    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public override string FullName
    {
      get
      {
        if (this.m_strFullQualName == null)
          this.m_strFullQualName = TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.FullName);
        return this.m_strFullQualName;
      }
    }

    public override string Namespace
    {
      get
      {
        return this.m_type.Namespace;
      }
    }

    public override string AssemblyQualifiedName
    {
      get
      {
        return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.AssemblyQualifiedName);
      }
    }

    public override Type BaseType
    {
      get
      {
        Type baseType = this.m_type.BaseType;
        if (baseType == (Type) null)
          return (Type) null;
        TypeBuilderInstantiation builderInstantiation = baseType as TypeBuilderInstantiation;
        if ((Type) builderInstantiation == (Type) null)
          return baseType;
        return builderInstantiation.Substitute(this.GetGenericArguments());
      }
    }

    public override Type UnderlyingSystemType
    {
      get
      {
        return (Type) this;
      }
    }

    public override bool IsGenericTypeDefinition
    {
      get
      {
        return false;
      }
    }

    public override bool IsGenericType
    {
      get
      {
        return true;
      }
    }

    public override bool IsConstructedGenericType
    {
      get
      {
        return true;
      }
    }

    public override bool IsGenericParameter
    {
      get
      {
        return false;
      }
    }

    public override int GenericParameterPosition
    {
      get
      {
        throw new InvalidOperationException();
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
        return false;
      }
    }

    public override MethodBase DeclaringMethod
    {
      get
      {
        return (MethodBase) null;
      }
    }

    private TypeBuilderInstantiation(Type type, Type[] inst)
    {
      this.m_type = type;
      this.m_inst = inst;
      this.m_hashtable = new Hashtable();
    }

    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    internal static Type MakeGenericType(Type type, Type[] typeArguments)
    {
      if (!type.IsGenericTypeDefinition)
        throw new InvalidOperationException();
      if (typeArguments == null)
        throw new ArgumentNullException("typeArguments");
      foreach (Type typeArgument in typeArguments)
      {
        if (typeArgument == (Type) null)
          throw new ArgumentNullException("typeArguments");
      }
      return (Type) new TypeBuilderInstantiation(type, typeArguments);
    }

    public override string ToString()
    {
      return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.ToString);
    }

    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType("&".ToCharArray(), (Type) this, 0);
    }

    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType("[]".ToCharArray(), (Type) this, 0);
    }

    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      string str = "";
      for (int index = 1; index < rank; ++index)
        str += ",";
      return SymbolType.FormCompoundType(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str).ToCharArray(), (Type) this, 0);
    }

    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      throw new NotSupportedException();
    }

    private Type Substitute(Type[] substitutes)
    {
      Type[] genericArguments = this.GetGenericArguments();
      Type[] typeArray = new Type[genericArguments.Length];
      for (int index = 0; index < typeArray.Length; ++index)
      {
        Type type = genericArguments[index];
        typeArray[index] = !(type is TypeBuilderInstantiation) ? (!(type is GenericTypeParameterBuilder) ? type : substitutes[type.GenericParameterPosition]) : (type as TypeBuilderInstantiation).Substitute(substitutes);
      }
      return this.GetGenericTypeDefinition().MakeGenericType(typeArray);
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override Type GetInterface(string name, bool ignoreCase)
    {
      throw new NotSupportedException();
    }

    public override Type[] GetInterfaces()
    {
      throw new NotSupportedException();
    }

    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override EventInfo[] GetEvents()
    {
      throw new NotSupportedException();
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      throw new NotSupportedException();
    }

    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.m_type.Attributes;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return false;
    }

    public override Type GetElementType()
    {
      throw new NotSupportedException();
    }

    protected override bool HasElementTypeImpl()
    {
      return false;
    }

    public override Type[] GetGenericArguments()
    {
      return this.m_inst;
    }

    protected override bool IsValueTypeImpl()
    {
      return this.m_type.IsValueType;
    }

    public override Type GetGenericTypeDefinition()
    {
      return this.m_type;
    }

    public override Type MakeGenericType(params Type[] inst)
    {
      throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
    }

    public override bool IsAssignableFrom(Type c)
    {
      throw new NotSupportedException();
    }

    [ComVisible(true)]
    public override bool IsSubclassOf(Type c)
    {
      throw new NotSupportedException();
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException();
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException();
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException();
    }
  }
}
