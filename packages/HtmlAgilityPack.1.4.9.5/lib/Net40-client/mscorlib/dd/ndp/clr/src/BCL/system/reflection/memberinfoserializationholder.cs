// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberInfoSerializationHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  [Serializable]
  internal class MemberInfoSerializationHolder : ISerializable, IObjectReference
  {
    private string m_memberName;
    private RuntimeType m_reflectedType;
    private string m_signature;
    private string m_signature2;
    private MemberTypes m_memberType;
    private SerializationInfo m_info;

    internal MemberInfoSerializationHolder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      string string1 = info.GetString("AssemblyName");
      string string2 = info.GetString("ClassName");
      if (string1 == null || string2 == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      this.m_reflectedType = FormatterServices.LoadAssemblyFromString(string1).GetType(string2, true, false) as RuntimeType;
      this.m_memberName = info.GetString("Name");
      this.m_signature = info.GetString("Signature");
      this.m_signature2 = (string) info.GetValueNoThrow("Signature2", typeof (string));
      this.m_memberType = (MemberTypes) info.GetInt32("MemberType");
      this.m_info = info;
    }

    public static void GetSerializationInfo(SerializationInfo info, string name, RuntimeType reflectedClass, string signature, MemberTypes type)
    {
      MemberInfoSerializationHolder.GetSerializationInfo(info, name, reflectedClass, signature, (string) null, type, (Type[]) null);
    }

    public static void GetSerializationInfo(SerializationInfo info, string name, RuntimeType reflectedClass, string signature, string signature2, MemberTypes type, Type[] genericArguments)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      string fullName1 = reflectedClass.Module.Assembly.FullName;
      string fullName2 = reflectedClass.FullName;
      info.SetType(typeof (MemberInfoSerializationHolder));
      info.AddValue("Name", (object) name, typeof (string));
      info.AddValue("AssemblyName", (object) fullName1, typeof (string));
      info.AddValue("ClassName", (object) fullName2, typeof (string));
      info.AddValue("Signature", (object) signature, typeof (string));
      info.AddValue("Signature2", (object) signature2, typeof (string));
      info.AddValue("MemberType", (int) type);
      info.AddValue("GenericArguments", (object) genericArguments, typeof (Type[]));
    }

    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    [SecurityCritical]
    public virtual object GetRealObject(StreamingContext context)
    {
      if (this.m_memberName == null || this.m_reflectedType == (RuntimeType) null || this.m_memberType == (MemberTypes) 0)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding;
      switch (this.m_memberType)
      {
        case MemberTypes.Constructor:
          if (this.m_signature == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_NullSignature"));
          ConstructorInfo[] constructorInfoArray = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Constructor, bindingAttr) as ConstructorInfo[];
          if (constructorInfoArray.Length == 1)
            return (object) constructorInfoArray[0];
          if (constructorInfoArray.Length > 1)
          {
            for (int index = 0; index < constructorInfoArray.Length; ++index)
            {
              if (this.m_signature2 != null)
              {
                if (((RuntimeConstructorInfo) constructorInfoArray[index]).SerializationToString().Equals(this.m_signature2))
                  return (object) constructorInfoArray[index];
              }
              else if (constructorInfoArray[index].ToString().Equals(this.m_signature))
                return (object) constructorInfoArray[index];
            }
          }
          throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
        case MemberTypes.Event:
          EventInfo[] eventInfoArray = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Event, bindingAttr) as EventInfo[];
          if (eventInfoArray.Length == 0)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          return (object) eventInfoArray[0];
        case MemberTypes.Field:
          FieldInfo[] fieldInfoArray = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Field, bindingAttr) as FieldInfo[];
          if (fieldInfoArray.Length == 0)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          return (object) fieldInfoArray[0];
        case MemberTypes.Method:
          MethodInfo methodInfo1 = (MethodInfo) null;
          if (this.m_signature == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_NullSignature"));
          Type[] typeArray = this.m_info.GetValueNoThrow("GenericArguments", typeof (Type[])) as Type[];
          MethodInfo[] methodInfoArray = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Method, bindingAttr) as MethodInfo[];
          if (methodInfoArray.Length == 1)
            methodInfo1 = methodInfoArray[0];
          else if (methodInfoArray.Length > 1)
          {
            for (int index = 0; index < methodInfoArray.Length; ++index)
            {
              if (this.m_signature2 != null)
              {
                if (((RuntimeMethodInfo) methodInfoArray[index]).SerializationToString().Equals(this.m_signature2))
                {
                  methodInfo1 = methodInfoArray[index];
                  break;
                }
              }
              else if (methodInfoArray[index].ToString().Equals(this.m_signature))
              {
                methodInfo1 = methodInfoArray[index];
                break;
              }
              if (typeArray != null && methodInfoArray[index].IsGenericMethod && methodInfoArray[index].GetGenericArguments().Length == typeArray.Length)
              {
                MethodInfo methodInfo2 = methodInfoArray[index].MakeGenericMethod(typeArray);
                if (this.m_signature2 != null)
                {
                  if (((RuntimeMethodInfo) methodInfo2).SerializationToString().Equals(this.m_signature2))
                  {
                    methodInfo1 = methodInfo2;
                    break;
                  }
                }
                else if (methodInfo2.ToString().Equals(this.m_signature))
                {
                  methodInfo1 = methodInfo2;
                  break;
                }
              }
            }
          }
          if (methodInfo1 == (MethodInfo) null)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          if (!methodInfo1.IsGenericMethodDefinition)
            return (object) methodInfo1;
          if (typeArray == null)
            return (object) methodInfo1;
          if (typeArray[0] == (Type) null)
            return (object) null;
          return (object) methodInfo1.MakeGenericMethod(typeArray);
        case MemberTypes.Property:
          PropertyInfo[] propertyInfoArray = this.m_reflectedType.GetMember(this.m_memberName, MemberTypes.Property, bindingAttr) as PropertyInfo[];
          if (propertyInfoArray.Length == 0)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
          if (propertyInfoArray.Length == 1)
            return (object) propertyInfoArray[0];
          if (propertyInfoArray.Length > 1)
          {
            for (int index = 0; index < propertyInfoArray.Length; ++index)
            {
              if (this.m_signature2 != null)
              {
                if (((RuntimePropertyInfo) propertyInfoArray[index]).SerializationToString().Equals(this.m_signature2))
                  return (object) propertyInfoArray[index];
              }
              else if (propertyInfoArray[index].ToString().Equals(this.m_signature))
                return (object) propertyInfoArray[index];
            }
          }
          throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMember", (object) this.m_memberName));
        default:
          throw new ArgumentException(Environment.GetResourceString("Serialization_MemberTypeNotRecognized"));
      }
    }
  }
}
