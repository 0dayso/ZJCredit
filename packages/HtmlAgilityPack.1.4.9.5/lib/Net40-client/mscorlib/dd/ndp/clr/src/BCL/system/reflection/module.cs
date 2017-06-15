// Decompiled with JetBrains decompiler
// Type: System.Reflection.Module
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>在模块上执行反射。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Module))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class Module : _Module, ISerializable, ICustomAttributeProvider
  {
    /// <summary>一个 TypeFilter 对象，它根据名称筛选此模块中定义的类型列表。此字段区分大小写且为只读。</summary>
    public static readonly TypeFilter FilterTypeName;
    /// <summary>一个 TypeFilter 对象，它根据名称筛选此模块中定义的类型列表。此字段不区分大小写且为只读。</summary>
    public static readonly TypeFilter FilterTypeNameIgnoreCase;
    private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

    /// <summary>获取包含此模型自定义特性的集合。</summary>
    /// <returns>包含此模块的自定义特性的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>获取元数据流版本。</summary>
    /// <returns>表示元数据流版本的 32 位整数。高序位的两个字节表示主版本号，低序位的两个字节表示次版本号。</returns>
    public virtual int MDStreamVersion
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.MDStreamVersion;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取表示此模块的完全限定名和路径的字符串。</summary>
    /// <returns>完全限定的模块名。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual string FullyQualifiedName
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个通用唯一标识符 (UUID)，该标识符可用于区分一个模块的两个版本。</summary>
    /// <returns>一个 <see cref="T:System.Guid" />，可用于区分一个模块的两个版本。</returns>
    public virtual Guid ModuleVersionId
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.ModuleVersionId;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个标记，该标记用于标识元数据中的模块。</summary>
    /// <returns>一个整数标记，用于标识元数据中的当前模块。</returns>
    public virtual int MetadataToken
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.MetadataToken;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取表示模块名的字符串。</summary>
    /// <returns>模块名。</returns>
    public virtual string ScopeName
    {
      get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.ScopeName;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取 String，它表示移除了路径的模块名。</summary>
    /// <returns>不带路径的模块名。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.Name;
        throw new NotImplementedException();
      }
    }

    /// <summary>为此 <see cref="T:System.Reflection.Module" /> 实例获取适当的 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>Assembly 对象。</returns>
    [__DynamicallyInvokable]
    public virtual Assembly Assembly
    {
      [__DynamicallyInvokable] get
      {
        RuntimeModule runtimeModule = this as RuntimeModule;
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule.Assembly;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取模块的句柄。</summary>
    /// <returns>当前模块的 <see cref="T:System.ModuleHandle" /> 结构。</returns>
    public ModuleHandle ModuleHandle
    {
      get
      {
        return this.GetModuleHandle();
      }
    }

    static Module()
    {
      __Filters filters = new __Filters();
      // ISSUE: virtual method pointer
      IntPtr method1 = __vmethodptr(filters, FilterTypeName);
      Module.FilterTypeName = new TypeFilter((object) filters, method1);
      // ISSUE: virtual method pointer
      IntPtr method2 = __vmethodptr(filters, FilterTypeNameIgnoreCase);
      Module.FilterTypeNameIgnoreCase = new TypeFilter((object) filters, method2);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Module" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(Module left, Module right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimeModule || right is RuntimeModule))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Module" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(Module left, Module right)
    {
      return !(left == right);
    }

    /// <summary>确定此模块和指定的对象是否相等。</summary>
    /// <returns>如果 <paramref name="o" /> 等于此实例，则为 true；否则为 false。</returns>
    /// <param name="o">与该实例进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      return base.Equals(o);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>返回模块的名称。</summary>
    /// <returns>表示此模块的名称的 String。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ScopeName;
    }

    /// <summary>返回所有自定义属性。</summary>
    /// <returns>包含所有自定义特性的 Object 类型的数组。</returns>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    public virtual object[] GetCustomAttributes(bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取指定类型的自定义特性。</summary>
    /// <returns>Object 类型的数组，包含指定类型的所有自定义特性。</returns>
    /// <param name="attributeType">要获取的属性的类型。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是一个由运行库提供的 <see cref="T:System.Type" /> 对象。例如，<paramref name="attributeType" /> 是一个 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象。</exception>
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回一个值，该值指示是否已将指定的特性类型应用于此模块。</summary>
    /// <returns>如果一个或多个 <paramref name="attributeType" /> 实例已应用于此模块，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要测试的自定义特性的类型。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是一个由运行库提供的 <see cref="T:System.Type" /> 对象。例如，<paramref name="attributeType" /> 是一个 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象。</exception>
    public virtual bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回当前模块的 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象可以在只反射上下文中使用。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象泛型列表，这些对象表示已应用于当前模块的特性的有关数据。</returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    /// <summary>返回由指定的元数据标记标识的方法或构造函数。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodBase" /> 对象，表示由指定的元数据标记标识的方法或构造函数。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的方法或构造函数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的方法或构造函数的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public MethodBase ResolveMethod(int metadataToken)
    {
      return this.ResolveMethod(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的方法或构造函数。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodBase" /> 对象，表示由指定的元数据标记标识的方法。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的方法或构造函数。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的方法或构造函数的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数），并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>返回由指定的元数据标记标识的字段。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.FieldInfo" /> 对象，表示由指定的元数据标记标识的字段。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个字段。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的字段的标记。- 或 -<paramref name="metadataToken" /> 标识一个字段，该字段的父 TypeSpec 具有一个包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数）的签名。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public FieldInfo ResolveField(int metadataToken)
    {
      return this.ResolveField(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的字段。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.FieldInfo" /> 对象，表示由指定的元数据标记标识的字段。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个字段。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的字段的标记。- 或 -<paramref name="metadataToken" /> 标识一个字段，该字段的父 TypeSpec 具有一个包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数）的签名，并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>返回由指定的元数据标记标识的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示由指定的元数据标记标识的类型。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的类型的标记。- 或 -<paramref name="metadataToken" /> 是一个 TypeSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public Type ResolveType(int metadataToken)
    {
      return this.ResolveType(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示由指定的元数据标记标识的类型。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个类型。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的类型的标记。- 或 -<paramref name="metadataToken" /> 是一个 TypeSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数），并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>返回由指定的元数据标记标识的类型或成员。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberInfo" /> 对象，表示由指定的元数据标记标识的类型或成员。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的类型或成员。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的类型或成员的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec 或 TypeSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数）。- 或 -<paramref name="metadataToken" /> 标识一个属性或事件。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public MemberInfo ResolveMember(int metadataToken)
    {
      return this.ResolveMember(metadataToken, (Type[]) null, (Type[]) null);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的类型或成员。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberInfo" /> 对象，表示由指定的元数据标记标识的类型或成员。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的类型或成员。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的类型或成员的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec 或 TypeSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数），并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。- 或 -<paramref name="metadataToken" /> 标识一个属性或事件。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
      throw new NotImplementedException();
    }

    /// <summary>返回由元数据标记标识的签名 Blob。</summary>
    /// <returns>一个字节数组，表示签名 Blob。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个签名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效 MemberRef、MethodDef、TypeSpec、签名或 FieldDef 标记。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public virtual byte[] ResolveSignature(int metadataToken)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveSignature(metadataToken);
      throw new NotImplementedException();
    }

    /// <summary>返回由指定元数据标记标识的字符串。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含来自元数据字符串堆的一个字符串值。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块的字符串堆中的一个字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的字符串的标记。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public virtual string ResolveString(int metadataToken)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.ResolveString(metadataToken);
      throw new NotImplementedException();
    }

    /// <summary>获取一对值，这一对值指示某个模块中代码的性质和该模块的目标平台。</summary>
    /// <param name="peKind">当此方法返回时，为 <see cref="T:System.Reflection.PortableExecutableKinds" /> 值的组合，用于指示模块中代码的性质。</param>
    /// <param name="machine">当此方法返回时，为 <see cref="T:System.Reflection.ImageFileMachine" /> 值中的一个，用于指示模块的目标平台。</param>
    public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        runtimeModule.GetPEKind(out peKind, out machine);
      throw new NotImplementedException();
    }

    /// <summary>提供序列化对象的 <see cref="T:System.Runtime.Serialization.ISerializable" /> 实现。</summary>
    /// <param name="info">序列化或反序列化对象所需的信息和数据。</param>
    /// <param name="context">序列化的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回指定的类型，并按指定的区分大小写搜索模块。</summary>
    /// <returns>如果该类型在此模块中，则为表示给定类型的 Type 对象；否则为 null。</returns>
    /// <param name="className">要定位的类型的名称。该名称必须是用命名空间完全限定的。</param>
    /// <param name="ignoreCase">对于不区分大小写的搜索，为 true；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="className" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用该类初始值设定项，并引发异常。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="className" /> 是零长度字符串。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="className" /> 需要一个无法找到的依赖程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="className" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到只反射上下文中，<paramref name="className" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="className" /> 需要一个依赖程序集，但该文件不是一个有效的程序集。- 或 -<paramref name="className" /> 需要一个针对高于当前加载版本的运行库版本编译的依赖程序集。</exception>
    [ComVisible(true)]
    public virtual Type GetType(string className, bool ignoreCase)
    {
      return this.GetType(className, false, ignoreCase);
    }

    /// <summary>返回指定类型，并执行区分大小写的搜索。</summary>
    /// <returns>如果该类型在此模块中，则为表示给定类型的 Type 对象；否则为 null。</returns>
    /// <param name="className">要定位的类型的名称。该名称必须是用命名空间完全限定的。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="className" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用该类初始值设定项，并引发异常。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="className" /> 是零长度字符串。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="className" /> 需要一个无法找到的依赖程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="className" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到只反射上下文中，<paramref name="className" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="className" /> 需要一个依赖程序集，但该文件不是一个有效的程序集。- 或 -<paramref name="className" /> 需要一个针对高于当前加载版本的运行库版本编译的依赖程序集。</exception>
    [ComVisible(true)]
    public virtual Type GetType(string className)
    {
      return this.GetType(className, false, false);
    }

    /// <summary>返回指定的类型，指定是否对该模块进行区分大小写的搜索；如果找不到该类型，则指定是否引发异常。</summary>
    /// <returns>如果已在此模块中声明指定类型，则为一个表示指定类型的 <see cref="T:System.Type" /> 对象；否则为 null。</returns>
    /// <param name="className">要定位的类型的名称。该名称必须是用命名空间完全限定的。</param>
    /// <param name="throwOnError">如果为 true，则在找不到该类型时引发异常；如果为 false，则返回 null。</param>
    /// <param name="ignoreCase">对于不区分大小写的搜索，为 true；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="className" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用该类初始值设定项，并引发异常。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="className" /> 是零长度字符串。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 为 true，找不到该类型。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="className" /> 需要一个无法找到的依赖程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="className" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到只反射上下文中，<paramref name="className" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="className" /> 需要一个依赖程序集，但该文件不是一个有效的程序集。- 或 -<paramref name="className" /> 需要一个针对高于当前加载版本的运行库版本编译的依赖程序集。</exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回给定的筛选器和筛选条件接受的类数组。</summary>
    /// <returns>包含筛选器接受的类的 Type 类型数组。</returns>
    /// <param name="filter">用于筛选类的委托。</param>
    /// <param name="filterCriteria">用于筛选类的对象。</param>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">未能加载模块中的一个或多个类。</exception>
    public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
    {
      Type[] types = this.GetTypes();
      int length = 0;
      for (int index = 0; index < types.Length; ++index)
      {
        if (filter != null && !filter(types[index], filterCriteria))
          types[index] = (Type) null;
        else
          ++length;
      }
      if (length == types.Length)
        return types;
      Type[] typeArray = new Type[length];
      int num = 0;
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] != (Type) null)
          typeArray[num++] = types[index];
      }
      return typeArray;
    }

    /// <summary>返回在此模块内定义的所有类型。</summary>
    /// <returns>Type 类型的数组，包含在此实例反射的模块内定义的类型。</returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">未能加载模块中的一个或多个类。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public virtual Type[] GetTypes()
    {
      throw new NotImplementedException();
    }

    /// <summary>获取一个值，该值指示此对象是否是资源。</summary>
    /// <returns>如果此对象是资源，则为 true；否则为 false。</returns>
    public virtual bool IsResource()
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.IsResource();
      throw new NotImplementedException();
    }

    /// <summary>返回在模块中定义的全局字段。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.FieldInfo" /> 对象数组，这些对象表示在模块中定义的全局字段；如果没有全局字段，则返回一个空数组。</returns>
    public FieldInfo[] GetFields()
    {
      return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>返回在模块中定义的全局字段，这些字段与指定的绑定标志匹配。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.FieldInfo" /> 类型的数组，该类型表示在模块中定义的全局字段，这些字段与指定的绑定标志匹配；如果不存在与这些绑定标志匹配的全局字段，则返回一个空数组。</returns>
    /// <param name="bindingFlags">用来限制搜索的 <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.GetFields(bindingFlags);
      throw new NotImplementedException();
    }

    /// <summary>返回具有指定名称的字段。</summary>
    /// <returns>一个具有指定名称的 FieldInfo 对象；如果该字段不存在，则为 null。</returns>
    /// <param name="name">字段名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public FieldInfo GetField(string name)
    {
      return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>返回具有指定名称和绑定特性的字段。</summary>
    /// <returns>一个具有指定名称及绑定特性的 FieldInfo 对象；如果该字段不存在，则为 null。</returns>
    /// <param name="name">字段名。</param>
    /// <param name="bindingAttr">用于控制搜索的 BindingFlags 位标志之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.GetField(name, bindingAttr);
      throw new NotImplementedException();
    }

    /// <summary>返回在模块中定义的全局方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象数组，这些对象表示在模块中定义的所有全局方法；如果没有全局方法，则返回一个空数组。</returns>
    public MethodInfo[] GetMethods()
    {
      return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>返回在模块中定义的全局方法，这些方法与指定的绑定标志匹配。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 类型的数组，该类型表示在模块中定义的全局方法，这些方法与指定的绑定标志匹配；如果不存在与这些绑定标志匹配的全局方法，则返回一个空数组。</returns>
    /// <param name="bindingFlags">用来限制搜索的 <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      RuntimeModule runtimeModule = this as RuntimeModule;
      if ((Module) runtimeModule != (Module) null)
        return runtimeModule.GetMethods(bindingFlags);
      throw new NotImplementedException();
    }

    /// <summary>返回具有指定名称、绑定信息、调用约定、参数类型和修饰符的方法。</summary>
    /// <returns>符合指定条件的 MethodInfo 对象；如果该方法不存在，则为 null。</returns>
    /// <param name="name">方法名。</param>
    /// <param name="bindingAttr">用于控制搜索的 BindingFlags 位标志之一。</param>
    /// <param name="binder">实现 Binder 的对象，它包含与此方法相关的属性。</param>
    /// <param name="callConvention">该方法的调用约定。</param>
    /// <param name="types">要搜索的参数类型。</param>
    /// <param name="modifiers">参数修饰符数组，用来与参数签名进行绑定，这些参数签名中的类型已经被修改。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null，<paramref name="types" /> 为 null，或者 <paramref name="types" /> (i) 为 null。</exception>
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>返回具有指定名称和参数类型的方法。</summary>
    /// <returns>符合指定条件的 MethodInfo 对象；如果该方法不存在，则为 null。</returns>
    /// <param name="name">方法名。</param>
    /// <param name="types">要搜索的参数类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null，<paramref name="types" /> 为 null，或者 <paramref name="types" /> (i) 为 null。</exception>
    public MethodInfo GetMethod(string name, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, types, (ParameterModifier[]) null);
    }

    /// <summary>返回具有指定名称的方法。</summary>
    /// <returns>一个具有指定名称的 MethodInfo 对象；如果该方法不存在，则为 null。</returns>
    /// <param name="name">方法名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    public MethodInfo GetMethod(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>返回符合指定条件的方法实现。</summary>
    /// <returns>包含指定的实现信息的 MethodInfo 对象；如果该方法不存在，则为 null。</returns>
    /// <param name="name">方法名。</param>
    /// <param name="bindingAttr">用于控制搜索的 BindingFlags 位标志之一。</param>
    /// <param name="binder">实现 Binder 的对象，它包含与此方法相关的属性。</param>
    /// <param name="callConvention">该方法的调用约定。</param>
    /// <param name="types">要搜索的参数类型。</param>
    /// <param name="modifiers">参数修饰符数组，用来与参数签名进行绑定，这些参数签名中的类型已经被修改。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    /// <paramref name="types" /> 为 null。</exception>
    protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotImplementedException();
    }

    internal virtual ModuleHandle GetModuleHandle()
    {
      return ModuleHandle.EmptyHandle;
    }

    /// <summary>返回与证书（包括在此模块所属的程序集的 Authenticode 签名中）对应的 X509Certificate 对象。如果此程序集没有 Authenticode 签名，则返回 null。</summary>
    /// <returns>X509Certificate 对象；如果此模块所属的程序集没有 Authenticode 签名，则为 null。</returns>
    public virtual X509Certificate GetSignerCertificate()
    {
      throw new NotImplementedException();
    }

    void _Module.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Module.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Module.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
