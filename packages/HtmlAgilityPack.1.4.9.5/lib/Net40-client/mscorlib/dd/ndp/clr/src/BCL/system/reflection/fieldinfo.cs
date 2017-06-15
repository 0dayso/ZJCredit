// Decompiled with JetBrains decompiler
// Type: System.Reflection.FieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>发现字段属性并提供对字段元数据的访问权。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_FieldInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class FieldInfo : MemberInfo, _FieldInfo
  {
    /// <summary>获取一个 <see cref="T:System.Reflection.MemberTypes" /> 值，该值指示此成员是字段。</summary>
    /// <returns>指示此成员是字段的 <see cref="T:System.Reflection.MemberTypes" /> 值。</returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Field;
      }
    }

    /// <summary>获取 RuntimeFieldHandle，它是字段的内部元数据表示形式的句柄。</summary>
    /// <returns>某个字段的内部元数据表示形式的句柄。</returns>
    [__DynamicallyInvokable]
    public abstract RuntimeFieldHandle FieldHandle { [__DynamicallyInvokable] get; }

    /// <summary>获取此字段对象的类型。</summary>
    /// <returns>此字段对象的类型。</returns>
    [__DynamicallyInvokable]
    public abstract Type FieldType { [__DynamicallyInvokable] get; }

    /// <summary>获取与此字段关联的特性。</summary>
    /// <returns>此字段的 FieldAttributes。</returns>
    [__DynamicallyInvokable]
    public abstract FieldAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，通过该值指示此字段是否为公共字段。</summary>
    /// <returns>如果此字段为公共字段，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
      }
    }

    /// <summary>获取一个值，通过该值指示此字段是否为私有字段。</summary>
    /// <returns>如果此字段为私有字段，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsPrivate
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
      }
    }

    /// <summary>获取一个值，该值指示此字段的可见性是否由 <see cref="F:System.Reflection.FieldAttributes.Family" /> 描述；也就是说，此字段仅在其类和派生类内可见。</summary>
    /// <returns>如果对此字段的访问由 <see cref="F:System.Reflection.FieldAttributes.Family" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFamily
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
      }
    }

    /// <summary>获取一个值，该值指示此字段的潜在可见性是否由 <see cref="F:System.Reflection.FieldAttributes.Assembly" /> 描述；也就是说，此字段只对同一程序集中的其他类型可见，而对该程序集以外的派生类型则不可见。</summary>
    /// <returns>如果此字段的可见性由 <see cref="F:System.Reflection.FieldAttributes.Assembly" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
      }
    }

    /// <summary>获取一个值，该值指示此字段的可见性是否由 <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" /> 描述；也就是说，可从派生类访问此字段，但仅当这些派生类在同一程序集中时。</summary>
    /// <returns>如果对此字段的访问由 <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFamilyAndAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
      }
    }

    /// <summary>获取一个值，该值指示此字段的潜在可见性是否由 <see cref="F:System.Reflection.FieldAttributes.FamORAssem" /> 描述；也就是说，可通过派生类（无论其位置如何）和同一程序集中的类访问此字段。</summary>
    /// <returns>如果对此字段的访问由 <see cref="F:System.Reflection.FieldAttributes.FamORAssem" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFamilyOrAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
      }
    }

    /// <summary>获取一个值，通过该值指示此字段是否为静态字段。</summary>
    /// <returns>如果此字段为静态字段，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsStatic
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.Static) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示此字段是否只能在构造函数的主体中设置。</summary>
    /// <returns>如果字段设置了 InitOnly 属性，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsInitOnly
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.InitOnly) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示该值是否在编译时写入并且不能更改。</summary>
    /// <returns>如果字段设置了 Literal 属性，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsLiteral
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.Literal) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示此字段是否有 NotSerialized 特性。</summary>
    /// <returns>如果字段设置了 NotSerialized 属性，则为 true；否则为 false。</returns>
    public bool IsNotSerialized
    {
      get
      {
        return (uint) (this.Attributes & FieldAttributes.NotSerialized) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示是否已在 <see cref="T:System.Reflection.FieldAttributes" /> 枚举数中设置相应的 SpecialName 特性。</summary>
    /// <returns>如果在 <see cref="T:System.Reflection.FieldAttributes" /> 中设置了 SpecialName 特性，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示是否已在 <see cref="T:System.Reflection.FieldAttributes" /> 中设置相应的 PinvokeImpl 特性。</summary>
    /// <returns>如果在 <see cref="T:System.Reflection.FieldAttributes" /> 中设置了 PinvokeImpl 特性，则为 true；否则为 false。</returns>
    public bool IsPinvokeImpl
    {
      get
      {
        return (uint) (this.Attributes & FieldAttributes.PinvokeImpl) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示当前字段在当前信任级别上是安全关键的还是安全可靠关键的。</summary>
    /// <returns>如果当前字段在当前信任级别上是安全关键的或安全可靠关键的，则为 true；如果它是透明的，则为 false。</returns>
    public virtual bool IsSecurityCritical
    {
      get
      {
        return this.FieldHandle.IsSecurityCritical();
      }
    }

    /// <summary>获取一个值，该值指示当前字段在当前信任级别上是否是安全可靠关键的。</summary>
    /// <returns>如果当前字段在当前信任级别上是安全可靠关键的，则为 true；如果它是安全关键的或透明的，则为 false。</returns>
    public virtual bool IsSecuritySafeCritical
    {
      get
      {
        return this.FieldHandle.IsSecuritySafeCritical();
      }
    }

    /// <summary>获取一个值，该值指示当前字段在当前信任级别上是否是透明的。</summary>
    /// <returns>如果该字段在当前信任级别上是安全透明的，则为 true；否则为 false。</returns>
    public virtual bool IsSecurityTransparent
    {
      get
      {
        return this.FieldHandle.IsSecurityTransparent();
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.FieldInfo" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(FieldInfo left, FieldInfo right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimeFieldInfo || right is RuntimeFieldInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.FieldInfo" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(FieldInfo left, FieldInfo right)
    {
      return !(left == right);
    }

    /// <summary>获取由指定句柄表示的字段的 <see cref="T:System.Reflection.FieldInfo" />。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.FieldInfo" /> 对象，表示由 <paramref name="handle" /> 指定的字段。</returns>
    /// <param name="handle">
    /// <see cref="T:System.RuntimeFieldHandle" /> 结构，它包含字段的内部元数据表示形式的句柄。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="handle" /> 无效。</exception>
    [__DynamicallyInvokable]
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      FieldInfo fieldInfo = RuntimeType.GetFieldInfo(handle.GetRuntimeFieldInfo());
      Type declaringType = fieldInfo.DeclaringType;
      if (declaringType != (Type) null && declaringType.IsGenericType)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FieldDeclaringTypeGeneric"), (object) fieldInfo.Name, (object) declaringType.GetGenericTypeDefinition()));
      return fieldInfo;
    }

    /// <summary>获取由指定句柄表示的指定泛型类型字段的 <see cref="T:System.Reflection.FieldInfo" />。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.FieldInfo" /> 对象，表示由 <paramref name="handle" /> 指定的字段，该字段的类型为 <paramref name="declaringType" /> 指定的泛型类型。</returns>
    /// <param name="handle">
    /// <see cref="T:System.RuntimeFieldHandle" /> 结构，它包含字段的内部元数据表示形式的句柄。</param>
    /// <param name="declaringType">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构，它包含定义该字段的泛型类型的句柄。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="handle" /> 无效。- 或 -<paramref name="declaringType" /> 与 <paramref name="handle" /> 不兼容。例如，<paramref name="declaringType" /> 是泛型类型定义的运行时类型句柄，且 <paramref name="handle" /> 来自于构造类型。请参阅“备注”。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      return RuntimeType.GetFieldInfo(declaringType.GetRuntimeType(), handle.GetRuntimeFieldInfo());
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

    /// <summary>获取一个类型数组，其中的类型标识属性的必需自定义修饰符。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，标识当前属性（例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />）需要的自定义修饰符。</returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      throw new NotImplementedException();
    }

    /// <summary>获取一个类型数组，其中的类型标识字段的可选自定义修饰符。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，这些对象标识当前字段（如 <see cref="T:System.Runtime.CompilerServices.IsConst" />）的可选自定义修饰符。</returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      throw new NotImplementedException();
    }

    /// <summary>设置给定对象支持的字段值。</summary>
    /// <param name="obj">
    /// <see cref="T:System.TypedReference" /> 结构，封装指向某个位置的托管指针和可以存储在该位置的类型的运行时表示形式。</param>
    /// <param name="value">分配给字段的值。</param>
    /// <exception cref="T:System.NotSupportedException">调用方需要公共语言规范 (CLS) 替换选项，但却调用了此方法。</exception>
    [CLSCompliant(false)]
    public virtual void SetValueDirect(TypedReference obj, object value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
    }

    /// <summary>返回给定对象所支持的字段的值。</summary>
    /// <returns>包含字段值的 Object。</returns>
    /// <param name="obj">
    /// <see cref="T:System.TypedReference" /> 结构，封装指向某个位置的托管指针和可能存储在该位置的类型的运行时表示形式。</param>
    /// <exception cref="T:System.NotSupportedException">调用方需要公共语言规范 (CLS) 替换选项，但却调用了此方法。</exception>
    [CLSCompliant(false)]
    public virtual object GetValueDirect(TypedReference obj)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
    }

    /// <summary>在派生类中被重写时，返回给定对象支持的字段的值。</summary>
    /// <returns>包含此实例反映的字段值的对象。</returns>
    /// <param name="obj">其字段值将返回的对象。</param>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。此字段是非静态的且 <paramref name="obj" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">字段被标记为文本，但是该字段没有一个可接受的文本类型。</exception>
    /// <exception cref="T:System.FieldAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。调用方没有访问此字段的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 类既不声明该方法也不继承该方法。</exception>
    [__DynamicallyInvokable]
    public abstract object GetValue(object obj);

    /// <summary>由编译器返回与字段关联的文本值。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，它包含与该字段关联的文本值。如果文本值是一个元素值为零的类类型，则返回值为 null。</returns>
    /// <exception cref="T:System.InvalidOperationException">非托管元数据中的常数表不包含当前字段的常数值。</exception>
    /// <exception cref="T:System.FormatException">值的类型不是公共语言规范 (CLS) 许可的类型。请参见“ECMA Partition II”（ECMA 第二部分）规范中的“Metadata Logical Format: Other Structures, Element Types used in Signatures”（元数据逻辑格式：其他结构，在签名中使用的元素类型）。</exception>
    /// <exception cref="T:System.NotSupportedException">未设置此字段的常数值。</exception>
    public virtual object GetRawConstantValue()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
    }

    /// <summary>在派生类中被重写时，设置给定对象支持的字段的值。</summary>
    /// <param name="obj">将设置其字段值的对象。</param>
    /// <param name="value">分配给字段的值。</param>
    /// <param name="invokeAttr">指定所需的绑定类型（例如，Binder.CreateInstance 或 Binder.ExactBinding）的 Binder 的字段。</param>
    /// <param name="binder">一组通过反射启用绑定、参数类型强制和成员调用的属性。如果 <paramref name="binder" /> 为 null，则使用 Binder.DefaultBinding。</param>
    /// <param name="culture">特定区域性的软件首选项。</param>
    /// <exception cref="T:System.FieldAccessException">调用方没有访问此字段的权限。</exception>
    /// <exception cref="T:System.Reflection.TargetException">
    /// <paramref name="obj" /> 参数为 null 并且该字段是一个实例字段。</exception>
    /// <exception cref="T:System.ArgumentException">对象上不存在该字段。- 或 -<paramref name="value" /> 参数无法转换并存储在该字段中。</exception>
    public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

    /// <summary>设置给定对象支持的字段值。</summary>
    /// <param name="obj">将设置其字段值的对象。</param>
    /// <param name="value">分配给字段的值。</param>
    /// <exception cref="T:System.FieldAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。调用方没有访问此字段的权限。</exception>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。<paramref name="obj" /> 参数为 null 并且该字段是一个实例字段。</exception>
    /// <exception cref="T:System.ArgumentException">对象上不存在该字段。- 或 -<paramref name="value" /> 参数无法转换并存储在该字段中。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public void SetValue(object obj, object value)
    {
      this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, (CultureInfo) null);
    }

    Type _FieldInfo.GetType()
    {
      return this.GetType();
    }

    void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
