// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.FormatterServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Runtime.Serialization
{
  /// <summary>提供静态方法，以协助实现用于序列化的 <see cref="T:System.Runtime.Serialization.Formatter" />。此类不能被继承。</summary>
  [ComVisible(true)]
  public static class FormatterServices
  {
    internal static Dictionary<MemberHolder, MemberInfo[]> m_MemberInfoTable = new Dictionary<MemberHolder, MemberInfo[]>(32);
    [SecurityCritical]
    private static bool unsafeTypeForwardersIsEnabled = false;
    [SecurityCritical]
    private static volatile bool unsafeTypeForwardersIsEnabledInitialized = false;
    private static object s_FormatterServicesSyncObject = (object) null;
    private static readonly Type[] advancedTypes = new Type[4]{ typeof (DelegateSerializationHolder), typeof (ObjRef), typeof (IEnvoyInfo), typeof (ISponsor) };
    private static Binder s_binder = Type.DefaultBinder;

    private static object formatterServicesSyncObject
    {
      get
      {
        if (FormatterServices.s_FormatterServicesSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref FormatterServices.s_FormatterServicesSyncObject, obj, (object) null);
        }
        return FormatterServices.s_FormatterServicesSyncObject;
      }
    }

    [SecuritySafeCritical]
    static FormatterServices()
    {
    }

    private static MemberInfo[] GetSerializableMembers(RuntimeType type)
    {
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      int length = 0;
      for (int index = 0; index < fields.Length; ++index)
      {
        if ((fields[index].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
          ++length;
      }
      if (length == fields.Length)
        return (MemberInfo[]) fields;
      FieldInfo[] fieldInfoArray = new FieldInfo[length];
      int index1 = 0;
      for (int index2 = 0; index2 < fields.Length; ++index2)
      {
        if ((fields[index2].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
        {
          fieldInfoArray[index1] = fields[index2];
          ++index1;
        }
      }
      return (MemberInfo[]) fieldInfoArray;
    }

    private static bool CheckSerializable(RuntimeType type)
    {
      return type.IsSerializable;
    }

    private static MemberInfo[] InternalGetSerializableMembers(RuntimeType type)
    {
      if (type.IsInterface)
        return new MemberInfo[0];
      if (!FormatterServices.CheckSerializable(type))
        throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", (object) type.FullName, (object) type.Module.Assembly.FullName));
      MemberInfo[] memberInfoArray1 = FormatterServices.GetSerializableMembers(type);
      RuntimeType parentType = (RuntimeType) type.BaseType;
      if (parentType != (RuntimeType) null && parentType != (RuntimeType) typeof (object))
      {
        RuntimeType[] parentTypes1 = (RuntimeType[]) null;
        int parentTypeCount = 0;
        bool parentTypes2 = FormatterServices.GetParentTypes(parentType, out parentTypes1, out parentTypeCount);
        if (parentTypeCount > 0)
        {
          List<SerializationFieldInfo> serializationFieldInfoList = new List<SerializationFieldInfo>();
          for (int index = 0; index < parentTypeCount; ++index)
          {
            RuntimeType type1 = parentTypes1[index];
            if (!FormatterServices.CheckSerializable(type1))
              throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", (object) type1.FullName, (object) type1.Module.Assembly.FullName));
            FieldInfo[] fields = type1.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            string namePrefix = parentTypes2 ? type1.Name : type1.FullName;
            foreach (FieldInfo fieldInfo in fields)
            {
              if (!fieldInfo.IsNotSerialized)
                serializationFieldInfoList.Add(new SerializationFieldInfo((RuntimeFieldInfo) fieldInfo, namePrefix));
            }
          }
          if (serializationFieldInfoList != null && serializationFieldInfoList.Count > 0)
          {
            MemberInfo[] memberInfoArray2 = new MemberInfo[serializationFieldInfoList.Count + memberInfoArray1.Length];
            Array.Copy((Array) memberInfoArray1, (Array) memberInfoArray2, memberInfoArray1.Length);
            ((ICollection) serializationFieldInfoList).CopyTo((Array) memberInfoArray2, memberInfoArray1.Length);
            memberInfoArray1 = memberInfoArray2;
          }
        }
      }
      return memberInfoArray1;
    }

    private static bool GetParentTypes(RuntimeType parentType, out RuntimeType[] parentTypes, out int parentTypeCount)
    {
      parentTypes = (RuntimeType[]) null;
      parentTypeCount = 0;
      bool flag = true;
      RuntimeType runtimeType1 = (RuntimeType) typeof (object);
      for (RuntimeType runtimeType2 = parentType; runtimeType2 != runtimeType1; runtimeType2 = (RuntimeType) runtimeType2.BaseType)
      {
        if (!runtimeType2.IsInterface)
        {
          string name1 = runtimeType2.Name;
          for (int index = 0; flag && index < parentTypeCount; ++index)
          {
            string name2 = parentTypes[index].Name;
            if (name2.Length == name1.Length && (int) name2[0] == (int) name1[0] && name1 == name2)
            {
              flag = false;
              break;
            }
          }
          if (parentTypes == null || parentTypeCount == parentTypes.Length)
          {
            RuntimeType[] runtimeTypeArray = new RuntimeType[Math.Max(parentTypeCount * 2, 12)];
            if (parentTypes != null)
              Array.Copy((Array) parentTypes, 0, (Array) runtimeTypeArray, 0, parentTypeCount);
            parentTypes = runtimeTypeArray;
          }
          RuntimeType[] runtimeTypeArray1 = parentTypes;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& local = @parentTypeCount;
          // ISSUE: explicit reference operation
          int num1 = ^local;
          int num2 = num1 + 1;
          // ISSUE: explicit reference operation
          ^local = num2;
          int index1 = num1;
          RuntimeType runtimeType3 = runtimeType2;
          runtimeTypeArray1[index1] = runtimeType3;
        }
      }
      return flag;
    }

    /// <summary>获取指定 <see cref="T:System.Type" /> 的类的所有可序列化成员。</summary>
    /// <returns>非瞬态、非静态成员的 <see cref="T:System.Reflection.MemberInfo" /> 类型的数组。</returns>
    /// <param name="type">正在序列化的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static MemberInfo[] GetSerializableMembers(Type type)
    {
      return FormatterServices.GetSerializableMembers(type, new StreamingContext(StreamingContextStates.All));
    }

    /// <summary>获取属于指定 <see cref="T:System.Type" /> 的类且位于提供的 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 中的所有可序列化成员。</summary>
    /// <returns>非瞬态、非静态成员的 <see cref="T:System.Reflection.MemberInfo" /> 类型的数组。</returns>
    /// <param name="type">正在序列化或克隆的类型。</param>
    /// <param name="context">发生序列化的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (!(type is RuntimeType))
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) type.ToString()));
      MemberHolder key = new MemberHolder(type, context);
      if (FormatterServices.m_MemberInfoTable.ContainsKey(key))
        return FormatterServices.m_MemberInfoTable[key];
      MemberInfo[] serializableMembers;
      lock (FormatterServices.formatterServicesSyncObject)
      {
        if (FormatterServices.m_MemberInfoTable.ContainsKey(key))
          return FormatterServices.m_MemberInfoTable[key];
        serializableMembers = FormatterServices.InternalGetSerializableMembers((RuntimeType) type);
        FormatterServices.m_MemberInfoTable[key] = serializableMembers;
      }
      return serializableMembers;
    }

    /// <summary>确定是否能够在 <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> 属性设置为 Low 时反序列化指定的 <see cref="T:System.Type" />。</summary>
    /// <param name="t">要检查反序列化能力的 <see cref="T:System.Type" />。</param>
    /// <param name="securityLevel">
    /// <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> 属性值。</param>
    /// <exception cref="T:System.Security.SecurityException">
    /// <paramref name="t" /> 参数是高级类型，不能在 <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> 属性设置为 Low 时被反序列化。</exception>
    public static void CheckTypeSecurity(Type t, TypeFilterLevel securityLevel)
    {
      if (securityLevel != TypeFilterLevel.Low)
        return;
      for (int index = 0; index < FormatterServices.advancedTypes.Length; ++index)
      {
        if (FormatterServices.advancedTypes[index].IsAssignableFrom(t))
          throw new SecurityException(Environment.GetResourceString("Serialization_TypeSecurity", (object) FormatterServices.advancedTypes[index].FullName, (object) t.FullName));
      }
    }

    /// <summary>创建指定对象类型的新实例。</summary>
    /// <returns>指定类型的归零对象。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetUninitializedObject(Type type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (!(type is RuntimeType))
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) type.ToString()));
      return FormatterServices.nativeGetUninitializedObject((RuntimeType) type);
    }

    /// <summary>创建指定对象类型的新实例。</summary>
    /// <returns>指定类型的归零对象。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="type" /> 参数不是有效的公共语言运行时类型。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetSafeUninitializedObject(Type type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (!(type is RuntimeType))
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) type.ToString()));
      if (type != typeof (ConstructionCall) && type != typeof (LogicalCallContext))
      {
        if (type != typeof (SynchronizationAttribute))
        {
          try
          {
            return FormatterServices.nativeGetSafeUninitializedObject((RuntimeType) type);
          }
          catch (SecurityException ex)
          {
            throw new SerializationException(Environment.GetResourceString("Serialization_Security", (object) type.FullName), (Exception) ex);
          }
        }
      }
      return FormatterServices.nativeGetUninitializedObject((RuntimeType) type);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object nativeGetSafeUninitializedObject(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object nativeGetUninitializedObject(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool GetEnableUnsafeTypeForwarders();

    [SecuritySafeCritical]
    internal static bool UnsafeTypeForwardersIsEnabled()
    {
      if (!FormatterServices.unsafeTypeForwardersIsEnabledInitialized)
      {
        FormatterServices.unsafeTypeForwardersIsEnabled = FormatterServices.GetEnableUnsafeTypeForwarders();
        FormatterServices.unsafeTypeForwardersIsEnabledInitialized = true;
      }
      return FormatterServices.unsafeTypeForwardersIsEnabled;
    }

    [SecurityCritical]
    internal static void SerializationSetValue(MemberInfo fi, object target, object value)
    {
      RtFieldInfo rtFieldInfo = fi as RtFieldInfo;
      if ((FieldInfo) rtFieldInfo != (FieldInfo) null)
      {
        rtFieldInfo.CheckConsistency(target);
        rtFieldInfo.UnsafeSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, (CultureInfo) null);
      }
      else
      {
        SerializationFieldInfo serializationFieldInfo = fi as SerializationFieldInfo;
        if (!((FieldInfo) serializationFieldInfo != (FieldInfo) null))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldInfo"));
        serializationFieldInfo.InternalSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, (CultureInfo) null);
      }
    }

    /// <summary>使用从对象的数据数组中提取的每个字段的值填充指定的对象。</summary>
    /// <returns>新填充的对象。</returns>
    /// <param name="obj">要填充的对象。</param>
    /// <param name="members">
    /// <see cref="T:System.Reflection.MemberInfo" /> 的数组，它描述要填充的字段和属性。</param>
    /// <param name="data">
    /// <see cref="T:System.Object" /> 的数组，它指定要填充的每个字段和属性的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" />、<paramref name="members" /> 或 <paramref name="data" /> 参数为 null。<paramref name="members" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="members" /> 的长度不匹配 <paramref name="data" /> 的长度。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="members" /> 的一个元素不是 <see cref="T:System.Reflection.FieldInfo" /> 的实例。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (members == null)
        throw new ArgumentNullException("members");
      if (data == null)
        throw new ArgumentNullException("data");
      if (members.Length != data.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_DataLengthDifferent"));
      for (int index = 0; index < members.Length; ++index)
      {
        MemberInfo fi = members[index];
        if (fi == (MemberInfo) null)
          throw new ArgumentNullException("members", Environment.GetResourceString("ArgumentNull_NullMember", (object) index));
        if (data[index] != null)
        {
          if (fi.MemberType != MemberTypes.Field)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
          FormatterServices.SerializationSetValue(fi, obj, data[index]);
        }
      }
      return obj;
    }

    /// <summary>从指定对象提取数据并将其以对象数组的形式返回。</summary>
    /// <returns>
    /// <see cref="T:System.Object" /> 的数组，它包含存储在 <paramref name="members" /> 中并与 <paramref name="obj" /> 关联的数据。</returns>
    /// <param name="obj">要写入格式化程序的对象。</param>
    /// <param name="members">要从对象中提取的成员。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 或 <paramref name="members" /> 参数为 null。<paramref name="members" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="members" /> 的一个元素不表示字段。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object[] GetObjectData(object obj, MemberInfo[] members)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (members == null)
        throw new ArgumentNullException("members");
      int length = members.Length;
      object[] objArray = new object[length];
      for (int index = 0; index < length; ++index)
      {
        MemberInfo memberInfo = members[index];
        if (memberInfo == (MemberInfo) null)
          throw new ArgumentNullException("members", Environment.GetResourceString("ArgumentNull_NullMember", (object) index));
        if (memberInfo.MemberType != MemberTypes.Field)
          throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
        RtFieldInfo rtFieldInfo = memberInfo as RtFieldInfo;
        if ((FieldInfo) rtFieldInfo != (FieldInfo) null)
        {
          rtFieldInfo.CheckConsistency(obj);
          objArray[index] = rtFieldInfo.UnsafeGetValue(obj);
        }
        else
          objArray[index] = ((SerializationFieldInfo) memberInfo).InternalGetValue(obj);
      }
      return objArray;
    }

    /// <summary>对指定的 <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> 返回序列化代理项。</summary>
    /// <returns>用于指定的 <paramref name="innerSurrogate" /> 的 <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />。</returns>
    /// <param name="innerSurrogate">指定的代理项。</param>
    [SecurityCritical]
    [ComVisible(false)]
    public static ISerializationSurrogate GetSurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
    {
      if (innerSurrogate == null)
        throw new ArgumentNullException("innerSurrogate");
      return (ISerializationSurrogate) new SurrogateForCyclicalReference(innerSurrogate);
    }

    /// <summary>在提供的 <see cref="T:System.Reflection.Assembly" /> 中查找指定对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>命名对象的 <see cref="T:System.Type" />。</returns>
    /// <param name="assem">要在其中查找对象的程序集。</param>
    /// <param name="name">对象的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assem" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetTypeFromAssembly(Assembly assem, string name)
    {
      if (assem == (Assembly) null)
        throw new ArgumentNullException("assem");
      return assem.GetType(name, false, false);
    }

    internal static Assembly LoadAssemblyFromString(string assemblyName)
    {
      return Assembly.Load(assemblyName);
    }

    internal static Assembly LoadAssemblyFromStringNoThrow(string assemblyName)
    {
      try
      {
        return FormatterServices.LoadAssemblyFromString(assemblyName);
      }
      catch (Exception ex)
      {
      }
      return (Assembly) null;
    }

    internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      object[] customAttributes = type.GetCustomAttributes(typeof (TypeForwardedFromAttribute), false);
      if (customAttributes != null && customAttributes.Length != 0)
      {
        hasTypeForwardedFrom = true;
        return ((TypeForwardedFromAttribute) customAttributes[0]).AssemblyFullName;
      }
      hasTypeForwardedFrom = false;
      return type.Assembly.FullName;
    }

    internal static string GetClrTypeFullName(Type type)
    {
      if (type.IsArray)
        return FormatterServices.GetClrTypeFullNameForArray(type);
      return FormatterServices.GetClrTypeFullNameForNonArrayTypes(type);
    }

    private static string GetClrTypeFullNameForArray(Type type)
    {
      int arrayRank = type.GetArrayRank();
      if (arrayRank == 1)
        return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}", (object) FormatterServices.GetClrTypeFullName(type.GetElementType()), (object) "[]");
      StringBuilder stringBuilder = new StringBuilder(FormatterServices.GetClrTypeFullName(type.GetElementType())).Append("[");
      for (int index = 1; index < arrayRank; ++index)
        stringBuilder.Append(",");
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    private static string GetClrTypeFullNameForNonArrayTypes(Type type)
    {
      if (!type.IsGenericType)
        return type.FullName;
      Type[] genericArguments = type.GetGenericArguments();
      StringBuilder stringBuilder1 = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append("[");
      foreach (Type type1 in genericArguments)
      {
        stringBuilder1.Append("[").Append(FormatterServices.GetClrTypeFullName(type1)).Append(", ");
        bool hasTypeForwardedFrom;
        stringBuilder1.Append(FormatterServices.GetClrAssemblyName(type1, out hasTypeForwardedFrom)).Append("],");
      }
      StringBuilder stringBuilder2 = stringBuilder1;
      int startIndex = stringBuilder2.Length - 1;
      int length = 1;
      return stringBuilder2.Remove(startIndex, length).Append("]").ToString();
    }
  }
}
