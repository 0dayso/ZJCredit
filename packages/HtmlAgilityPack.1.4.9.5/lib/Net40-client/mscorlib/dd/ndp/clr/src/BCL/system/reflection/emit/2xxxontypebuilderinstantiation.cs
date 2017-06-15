// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FieldOnTypeBuilderInstantiation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection.Emit
{
  internal sealed class FieldOnTypeBuilderInstantiation : FieldInfo
  {
    private FieldInfo m_field;
    private TypeBuilderInstantiation m_type;

    internal FieldInfo FieldInfo
    {
      get
      {
        return this.m_field;
      }
    }

    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Field;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_field.Name;
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
        FieldBuilder fieldBuilder = this.m_field as FieldBuilder;
        if ((FieldInfo) fieldBuilder != (FieldInfo) null)
          return fieldBuilder.MetadataTokenInternal;
        return this.m_field.MetadataToken;
      }
    }

    public override Module Module
    {
      get
      {
        return this.m_field.Module;
      }
    }

    public override RuntimeFieldHandle FieldHandle
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override Type FieldType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override FieldAttributes Attributes
    {
      get
      {
        return this.m_field.Attributes;
      }
    }

    internal FieldOnTypeBuilderInstantiation(FieldInfo field, TypeBuilderInstantiation type)
    {
      this.m_field = field;
      this.m_type = type;
    }

    internal static FieldInfo GetField(FieldInfo Field, TypeBuilderInstantiation type)
    {
      FieldInfo fieldInfo;
      if (type.m_hashtable.Contains((object) Field))
      {
        fieldInfo = type.m_hashtable[(object) Field] as FieldInfo;
      }
      else
      {
        fieldInfo = (FieldInfo) new FieldOnTypeBuilderInstantiation(Field, type);
        type.m_hashtable[(object) Field] = (object) fieldInfo;
      }
      return fieldInfo;
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_field.GetCustomAttributes(inherit);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_field.GetCustomAttributes(attributeType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_field.IsDefined(attributeType, inherit);
    }

    public new Type GetType()
    {
      return base.GetType();
    }

    public override Type[] GetRequiredCustomModifiers()
    {
      return this.m_field.GetRequiredCustomModifiers();
    }

    public override Type[] GetOptionalCustomModifiers()
    {
      return this.m_field.GetOptionalCustomModifiers();
    }

    public override void SetValueDirect(TypedReference obj, object value)
    {
      throw new NotImplementedException();
    }

    public override object GetValueDirect(TypedReference obj)
    {
      throw new NotImplementedException();
    }

    public override object GetValue(object obj)
    {
      throw new InvalidOperationException();
    }

    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      throw new InvalidOperationException();
    }
  }
}
