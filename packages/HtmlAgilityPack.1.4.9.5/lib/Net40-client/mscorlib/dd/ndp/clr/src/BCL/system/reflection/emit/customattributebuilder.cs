// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.CustomAttributeBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
  /// <summary>帮助生成自定义特性。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_CustomAttributeBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class CustomAttributeBuilder : _CustomAttributeBuilder
  {
    internal ConstructorInfo m_con;
    internal object[] m_constructorArgs;
    internal byte[] m_blob;

    /// <summary>已知自定义特性的构造函数和该构造函数的参数，初始化 CustomAttributeBuilder 类的实例。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="constructorArgs">自定义属性的构造函数的参数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="con" /> 为静态或私有。- 或 -所提供的参数数量与该构造函数的调用约定所要求的构造函数的参数数量不匹配。- 或 -所提供参数的类型与构造函数中声明的参数类型不匹配。- 或 -提供的参数是引用类型，而不是 <see cref="T:System.String" /> 或 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="constructorArgs" /> 为 null。</exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
    }

    /// <summary>已知自定义特性的构造函数、该构造函数的参数以及一组命名的属性/值对，初始化 CustomAttributeBuilder 类的实例。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="constructorArgs">自定义属性的构造函数的参数。</param>
    /// <param name="namedProperties">自定义属性 (Attribute) 的命名属性 (Property)。</param>
    /// <param name="propertyValues">自定义属性 (Attribute) 的命名属性 (Property) 的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="namedProperties" /> 和 <paramref name="propertyValues" /> 数组的长度不相同。- 或 -<paramref name="con" /> 为静态或私有。- 或 -所提供的参数数量与该构造函数的调用约定所要求的构造函数的参数数量不匹配。- 或 -所提供参数的类型与构造函数中声明的参数类型不匹配。- 或 -属性值的类型与命名属性的类型不匹配。- 或 -属性没有 setter 方法。- 或 -该属性不属于与构造函数相同的类或基类。- 或 -提供的参数或命名的属性是引用类型，而不是 <see cref="T:System.String" /> 或 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">其中一个参数为 null。</exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, namedProperties, propertyValues, new FieldInfo[0], new object[0]);
    }

    /// <summary>已知自定义特性的构造函数、该构造函数的参数以及一组命名的字段/值对，初始化 CustomAttributeBuilder 类的实例。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="constructorArgs">自定义属性的构造函数的参数。</param>
    /// <param name="namedFields">自定义属性的命名字段。</param>
    /// <param name="fieldValues">自定义属性的命名字段的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="namedFields" /> 和 <paramref name="fieldValues" /> 数组的长度不相同。- 或 -<paramref name="con" /> 为静态或私有。- 或 -所提供的参数数量与该构造函数的调用约定所要求的构造函数的参数数量不匹配。- 或 -所提供参数的类型与构造函数中声明的参数类型不匹配。- 或 -字段值的类型与命名字段的类型不匹配。- 或 -该字段不属于与构造函数相同的类或基类。- 或 -提供的参数或命名的字段是引用类型，而不是 <see cref="T:System.String" /> 或 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">其中一个参数为 null。</exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, new PropertyInfo[0], new object[0], namedFields, fieldValues);
    }

    /// <summary>已知自定义特性的构造函数、该构造函数的参数、一组命名的属性/值对以及一组命名的字段/值对，初始化 CustomAttributeBuilder 类的实例。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="constructorArgs">自定义属性的构造函数的参数。</param>
    /// <param name="namedProperties">自定义属性 (Attribute) 的命名属性 (Property)。</param>
    /// <param name="propertyValues">自定义属性 (Attribute) 的命名属性 (Property) 的值。</param>
    /// <param name="namedFields">自定义属性的命名字段。</param>
    /// <param name="fieldValues">自定义属性的命名字段的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="namedProperties" /> 和 <paramref name="propertyValues" /> 数组的长度不相同。- 或 -<paramref name="namedFields" /> 和 <paramref name="fieldValues" /> 数组的长度不相同。- 或 -<paramref name="con" /> 为静态或私有。- 或 -所提供的参数数量与该构造函数的调用约定所要求的构造函数的参数数量不匹配。- 或 -所提供参数的类型与构造函数中声明的参数类型不匹配。- 或 -属性值的类型与命名属性的类型不匹配。- 或 -字段值的类型与相应的字段类型的类型不匹配。- 或 -属性没有 setter。- 或 -该属性或字段不属于与构造函数相同的类或基类。- 或 -提供的参数、命名的属性或命名的字段是引用类型，而不是 <see cref="T:System.String" /> 或 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">其中一个参数为 null。</exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
    }

    private bool ValidateType(Type t)
    {
      if (t.IsPrimitive || t == typeof (string) || t == typeof (Type))
        return true;
      if (t.IsEnum)
      {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType(t)))
        {
          case TypeCode.SByte:
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
          case TypeCode.Int64:
          case TypeCode.UInt64:
            return true;
          default:
            return false;
        }
      }
      else
      {
        if (!t.IsArray)
          return t == typeof (object);
        if (t.GetArrayRank() != 1)
          return false;
        return this.ValidateType(t.GetElementType());
      }
    }

    internal void InitCustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (constructorArgs == null)
        throw new ArgumentNullException("constructorArgs");
      if (namedProperties == null)
        throw new ArgumentNullException("namedProperties");
      if (propertyValues == null)
        throw new ArgumentNullException("propertyValues");
      if (namedFields == null)
        throw new ArgumentNullException("namedFields");
      if (fieldValues == null)
        throw new ArgumentNullException("fieldValues");
      if (namedProperties.Length != propertyValues.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"), "namedProperties, propertyValues");
      if (namedFields.Length != fieldValues.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"), "namedFields, fieldValues");
      if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadConstructor"));
      if ((con.CallingConvention & CallingConventions.Standard) != CallingConventions.Standard)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadConstructorCallConv"));
      this.m_con = con;
      this.m_constructorArgs = new object[constructorArgs.Length];
      Array.Copy((Array) constructorArgs, (Array) this.m_constructorArgs, constructorArgs.Length);
      Type[] parameterTypes = con.GetParameterTypes();
      if (parameterTypes.Length != constructorArgs.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterCountsForConstructor"));
      for (int index = 0; index < parameterTypes.Length; ++index)
      {
        if (!this.ValidateType(parameterTypes[index]))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
      }
      for (int index = 0; index < parameterTypes.Length; ++index)
      {
        if (constructorArgs[index] != null)
        {
          TypeCode typeCode = Type.GetTypeCode(parameterTypes[index]);
          if (typeCode != Type.GetTypeCode(constructorArgs[index].GetType()) && (typeCode != TypeCode.Object || !this.ValidateType(constructorArgs[index].GetType())))
            throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterTypeForConstructor", (object) index));
        }
      }
      BinaryWriter writer = new BinaryWriter((Stream) new MemoryStream());
      writer.Write((ushort) 1);
      for (int index = 0; index < constructorArgs.Length; ++index)
        this.EmitValue(writer, parameterTypes[index], constructorArgs[index]);
      writer.Write((ushort) (namedProperties.Length + namedFields.Length));
      for (int index = 0; index < namedProperties.Length; ++index)
      {
        if (namedProperties[index] == (PropertyInfo) null)
          throw new ArgumentNullException("namedProperties[" + (object) index + "]");
        Type propertyType = namedProperties[index].PropertyType;
        if (propertyValues[index] == null && propertyType.IsPrimitive)
          throw new ArgumentNullException("propertyValues[" + (object) index + "]");
        if (!this.ValidateType(propertyType))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
        if (!namedProperties[index].CanWrite)
          throw new ArgumentException(Environment.GetResourceString("Argument_NotAWritableProperty"));
        if (namedProperties[index].DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && (!con.DeclaringType.IsSubclassOf(namedProperties[index].DeclaringType) && !TypeBuilder.IsTypeEqual(namedProperties[index].DeclaringType, con.DeclaringType)) && (!(namedProperties[index].DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf((Type) ((TypeBuilder) namedProperties[index].DeclaringType).BakedRuntimeType)))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadPropertyForConstructorBuilder"));
        if (propertyValues[index] != null && propertyType != typeof (object) && Type.GetTypeCode(propertyValues[index].GetType()) != Type.GetTypeCode(propertyType))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
        writer.Write((byte) 84);
        this.EmitType(writer, propertyType);
        this.EmitString(writer, namedProperties[index].Name);
        this.EmitValue(writer, propertyType, propertyValues[index]);
      }
      for (int index = 0; index < namedFields.Length; ++index)
      {
        if (namedFields[index] == (FieldInfo) null)
          throw new ArgumentNullException("namedFields[" + (object) index + "]");
        Type fieldType = namedFields[index].FieldType;
        if (fieldValues[index] == null && fieldType.IsPrimitive)
          throw new ArgumentNullException("fieldValues[" + (object) index + "]");
        if (!this.ValidateType(fieldType))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
        if (namedFields[index].DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && (!con.DeclaringType.IsSubclassOf(namedFields[index].DeclaringType) && !TypeBuilder.IsTypeEqual(namedFields[index].DeclaringType, con.DeclaringType)) && (!(namedFields[index].DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf((Type) ((TypeBuilder) namedFields[index].DeclaringType).BakedRuntimeType)))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldForConstructorBuilder"));
        if (fieldValues[index] != null && fieldType != typeof (object) && Type.GetTypeCode(fieldValues[index].GetType()) != Type.GetTypeCode(fieldType))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
        writer.Write((byte) 83);
        this.EmitType(writer, fieldType);
        this.EmitString(writer, namedFields[index].Name);
        this.EmitValue(writer, fieldType, fieldValues[index]);
      }
      this.m_blob = ((MemoryStream) writer.BaseStream).ToArray();
    }

    private void EmitType(BinaryWriter writer, Type type)
    {
      if (type.IsPrimitive)
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Boolean:
            writer.Write((byte) 2);
            break;
          case TypeCode.Char:
            writer.Write((byte) 3);
            break;
          case TypeCode.SByte:
            writer.Write((byte) 4);
            break;
          case TypeCode.Byte:
            writer.Write((byte) 5);
            break;
          case TypeCode.Int16:
            writer.Write((byte) 6);
            break;
          case TypeCode.UInt16:
            writer.Write((byte) 7);
            break;
          case TypeCode.Int32:
            writer.Write((byte) 8);
            break;
          case TypeCode.UInt32:
            writer.Write((byte) 9);
            break;
          case TypeCode.Int64:
            writer.Write((byte) 10);
            break;
          case TypeCode.UInt64:
            writer.Write((byte) 11);
            break;
          case TypeCode.Single:
            writer.Write((byte) 12);
            break;
          case TypeCode.Double:
            writer.Write((byte) 13);
            break;
        }
      }
      else if (type.IsEnum)
      {
        writer.Write((byte) 85);
        this.EmitString(writer, type.AssemblyQualifiedName);
      }
      else if (type == typeof (string))
        writer.Write((byte) 14);
      else if (type == typeof (Type))
        writer.Write((byte) 80);
      else if (type.IsArray)
      {
        writer.Write((byte) 29);
        this.EmitType(writer, type.GetElementType());
      }
      else
        writer.Write((byte) 81);
    }

    private void EmitString(BinaryWriter writer, string str)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      uint num = (uint) bytes.Length;
      if (num <= (uint) sbyte.MaxValue)
        writer.Write((byte) num);
      else if (num <= 16383U)
      {
        writer.Write((byte) (num >> 8 | 128U));
        writer.Write((byte) (num & (uint) byte.MaxValue));
      }
      else
      {
        writer.Write((byte) (num >> 24 | 192U));
        writer.Write((byte) (num >> 16 & (uint) byte.MaxValue));
        writer.Write((byte) (num >> 8 & (uint) byte.MaxValue));
        writer.Write((byte) (num & (uint) byte.MaxValue));
      }
      writer.Write(bytes);
    }

    private void EmitValue(BinaryWriter writer, Type type, object value)
    {
      if (type.IsEnum)
      {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType(type)))
        {
          case TypeCode.SByte:
            writer.Write((sbyte) value);
            break;
          case TypeCode.Byte:
            writer.Write((byte) value);
            break;
          case TypeCode.Int16:
            writer.Write((short) value);
            break;
          case TypeCode.UInt16:
            writer.Write((ushort) value);
            break;
          case TypeCode.Int32:
            writer.Write((int) value);
            break;
          case TypeCode.UInt32:
            writer.Write((uint) value);
            break;
          case TypeCode.Int64:
            writer.Write((long) value);
            break;
          case TypeCode.UInt64:
            writer.Write((ulong) value);
            break;
        }
      }
      else if (type == typeof (string))
      {
        if (value == null)
          writer.Write(byte.MaxValue);
        else
          this.EmitString(writer, (string) value);
      }
      else if (type == typeof (Type))
      {
        if (value == null)
        {
          writer.Write(byte.MaxValue);
        }
        else
        {
          string @string = TypeNameBuilder.ToString((Type) value, TypeNameBuilder.Format.AssemblyQualifiedName);
          if (@string == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeForCA", (object) value.GetType()));
          this.EmitString(writer, @string);
        }
      }
      else if (type.IsArray)
      {
        if (value == null)
        {
          writer.Write(uint.MaxValue);
        }
        else
        {
          Array array = (Array) value;
          Type elementType = type.GetElementType();
          writer.Write(array.Length);
          for (int index = 0; index < array.Length; ++index)
            this.EmitValue(writer, elementType, array.GetValue(index));
        }
      }
      else if (type.IsPrimitive)
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Boolean:
            writer.Write((bool) value ? (byte) 1 : (byte) 0);
            break;
          case TypeCode.Char:
            writer.Write(Convert.ToUInt16((char) value));
            break;
          case TypeCode.SByte:
            writer.Write((sbyte) value);
            break;
          case TypeCode.Byte:
            writer.Write((byte) value);
            break;
          case TypeCode.Int16:
            writer.Write((short) value);
            break;
          case TypeCode.UInt16:
            writer.Write((ushort) value);
            break;
          case TypeCode.Int32:
            writer.Write((int) value);
            break;
          case TypeCode.UInt32:
            writer.Write((uint) value);
            break;
          case TypeCode.Int64:
            writer.Write((long) value);
            break;
          case TypeCode.UInt64:
            writer.Write((ulong) value);
            break;
          case TypeCode.Single:
            writer.Write((float) value);
            break;
          case TypeCode.Double:
            writer.Write((double) value);
            break;
        }
      }
      else if (type == typeof (object))
      {
        Type type1 = value == null ? typeof (string) : (value is Type ? typeof (Type) : value.GetType());
        if (type1 == typeof (object))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterTypeForCAB", (object) type1.ToString()));
        this.EmitType(writer, type1);
        this.EmitValue(writer, type1, value);
      }
      else
      {
        string str = "null";
        if (value != null)
          str = value.GetType().ToString();
        throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterTypeForCAB", (object) str));
      }
    }

    [SecurityCritical]
    internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner)
    {
      this.CreateCustomAttribute(mod, tkOwner, mod.GetConstructorToken(this.m_con).Token, false);
    }

    [SecurityCritical]
    internal int PrepareCreateCustomAttributeToDisk(ModuleBuilder mod)
    {
      return mod.InternalGetConstructorToken(this.m_con, true).Token;
    }

    [SecurityCritical]
    internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner, int tkAttrib, bool toDisk)
    {
      TypeBuilder.DefineCustomAttribute(mod, tkOwner, tkAttrib, this.m_blob, toDisk, typeof (DebuggableAttribute) == this.m_con.DeclaringType);
    }

    void _CustomAttributeBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _CustomAttributeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _CustomAttributeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _CustomAttributeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
