// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.WriteObjectInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class WriteObjectInfo
  {
    internal int objectInfoId;
    internal object obj;
    internal Type objectType;
    internal bool isSi;
    internal bool isNamed;
    internal bool isTyped;
    internal bool isArray;
    internal SerializationInfo si;
    internal SerObjectInfoCache cache;
    internal object[] memberData;
    internal ISerializationSurrogate serializationSurrogate;
    internal StreamingContext context;
    internal SerObjectInfoInit serObjectInfoInit;
    internal long objectId;
    internal long assemId;
    private string binderTypeName;
    private string binderAssemblyString;

    internal WriteObjectInfo()
    {
    }

    internal void ObjectEnd()
    {
      WriteObjectInfo.PutObjectInfo(this.serObjectInfoInit, this);
    }

    private void InternalInit()
    {
      this.obj = (object) null;
      this.objectType = (Type) null;
      this.isSi = false;
      this.isNamed = false;
      this.isTyped = false;
      this.isArray = false;
      this.si = (SerializationInfo) null;
      this.cache = (SerObjectInfoCache) null;
      this.memberData = (object[]) null;
      this.objectId = 0L;
      this.assemId = 0L;
      this.binderTypeName = (string) null;
      this.binderAssemblyString = (string) null;
    }

    [SecurityCritical]
    internal static WriteObjectInfo Serialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
    {
      WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
      object obj1 = obj;
      ISurrogateSelector surrogateSelector1 = surrogateSelector;
      StreamingContext context1 = context;
      SerObjectInfoInit serObjectInfoInit1 = serObjectInfoInit;
      IFormatterConverter converter1 = converter;
      ObjectWriter objectWriter1 = objectWriter;
      SerializationBinder binder1 = binder;
      objectInfo.InitSerialize(obj1, surrogateSelector1, context1, serObjectInfoInit1, converter1, objectWriter1, binder1);
      return objectInfo;
    }

    [SecurityCritical]
    internal void InitSerialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
    {
      this.context = context;
      this.obj = obj;
      this.serObjectInfoInit = serObjectInfoInit;
      this.objectType = !RemotingServices.IsTransparentProxy(obj) ? obj.GetType() : Converter.typeofMarshalByRefObject;
      if (this.objectType.IsArray)
      {
        this.isArray = true;
        this.InitNoMembers();
      }
      else
      {
        this.InvokeSerializationBinder(binder);
        objectWriter.ObjectManager.RegisterObject(obj);
        ISurrogateSelector selector;
        if (surrogateSelector != null && (this.serializationSurrogate = surrogateSelector.GetSurrogate(this.objectType, context, out selector)) != null)
        {
          this.si = new SerializationInfo(this.objectType, converter);
          if (!this.objectType.IsPrimitive)
            this.serializationSurrogate.GetObjectData(obj, this.si, context);
          this.InitSiWrite();
        }
        else if (obj is ISerializable)
        {
          if (!this.objectType.IsSerializable)
            throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", (object) this.objectType.FullName, (object) this.objectType.Assembly.FullName));
          this.si = new SerializationInfo(this.objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
          ((ISerializable) obj).GetObjectData(this.si, context);
          this.InitSiWrite();
          WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
        }
        else
        {
          this.InitMemberInfo();
          WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
        }
      }
    }

    [Conditional("SER_LOGGING")]
    private void DumpMemberInfo()
    {
      int num = 0;
      while (num < this.cache.memberInfos.Length)
        ++num;
    }

    [SecurityCritical]
    internal static WriteObjectInfo Serialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
    {
      WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
      Type objectType1 = objectType;
      ISurrogateSelector surrogateSelector1 = surrogateSelector;
      StreamingContext context1 = context;
      SerObjectInfoInit serObjectInfoInit1 = serObjectInfoInit;
      IFormatterConverter converter1 = converter;
      SerializationBinder binder1 = binder;
      objectInfo.InitSerialize(objectType1, surrogateSelector1, context1, serObjectInfoInit1, converter1, binder1);
      return objectInfo;
    }

    [SecurityCritical]
    internal void InitSerialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
    {
      this.objectType = objectType;
      this.context = context;
      this.serObjectInfoInit = serObjectInfoInit;
      if (objectType.IsArray)
      {
        this.InitNoMembers();
      }
      else
      {
        this.InvokeSerializationBinder(binder);
        ISurrogateSelector selector = (ISurrogateSelector) null;
        if (surrogateSelector != null)
          this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out selector);
        if (this.serializationSurrogate != null)
        {
          this.si = new SerializationInfo(objectType, converter);
          this.cache = new SerObjectInfoCache(objectType);
          this.isSi = true;
        }
        else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
        {
          this.si = new SerializationInfo(objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
          this.cache = new SerObjectInfoCache(objectType);
          WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
          this.isSi = true;
        }
        if (this.isSi)
          return;
        this.InitMemberInfo();
        WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
      }
    }

    private void InitSiWrite()
    {
      SerializationInfoEnumerator serializationInfoEnumerator = (SerializationInfoEnumerator) null;
      this.isSi = true;
      serializationInfoEnumerator = this.si.GetEnumerator();
      int memberCount = this.si.MemberCount;
      TypeInformation typeInformation = (TypeInformation) null;
      string fullTypeName = this.si.FullTypeName;
      string assemblyName = this.si.AssemblyName;
      bool hasTypeForwardedFrom = false;
      if (!this.si.IsFullTypeNameSetExplicit)
      {
        typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
        fullTypeName = typeInformation.FullTypeName;
        hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
      }
      if (!this.si.IsAssemblyNameSetExplicit)
      {
        if (typeInformation == null)
          typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
        assemblyName = typeInformation.AssemblyString;
        hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
      }
      this.cache = new SerObjectInfoCache(fullTypeName, assemblyName, hasTypeForwardedFrom);
      this.cache.memberNames = new string[memberCount];
      this.cache.memberTypes = new Type[memberCount];
      this.memberData = new object[memberCount];
      SerializationInfoEnumerator enumerator = this.si.GetEnumerator();
      int index = 0;
      while (enumerator.MoveNext())
      {
        this.cache.memberNames[index] = enumerator.Name;
        this.cache.memberTypes[index] = enumerator.ObjectType;
        this.memberData[index] = enumerator.Value;
        ++index;
      }
      this.isNamed = true;
      this.isTyped = false;
    }

    private static void CheckTypeForwardedFrom(SerObjectInfoCache cache, Type objectType, string binderAssemblyString)
    {
      if (!cache.hasTypeForwardedFrom || binderAssemblyString != null || FormatterServices.UnsafeTypeForwardersIsEnabled())
        return;
      Assembly assembly = objectType.Assembly;
      if (!SerializationInfo.IsAssemblyNameAssignmentSafe(assembly.FullName, cache.assemblyString) && !assembly.IsFullyTrusted)
        throw new SecurityException(Environment.GetResourceString("Serialization_RequireFullTrust", (object) objectType));
    }

    private void InitNoMembers()
    {
      this.cache = (SerObjectInfoCache) this.serObjectInfoInit.seenBeforeTable[(object) this.objectType];
      if (this.cache != null)
        return;
      this.cache = new SerObjectInfoCache(this.objectType);
      this.serObjectInfoInit.seenBeforeTable.Add((object) this.objectType, (object) this.cache);
    }

    [SecurityCritical]
    private void InitMemberInfo()
    {
      this.cache = (SerObjectInfoCache) this.serObjectInfoInit.seenBeforeTable[(object) this.objectType];
      if (this.cache == null)
      {
        this.cache = new SerObjectInfoCache(this.objectType);
        this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
        int length = this.cache.memberInfos.Length;
        this.cache.memberNames = new string[length];
        this.cache.memberTypes = new Type[length];
        for (int index = 0; index < length; ++index)
        {
          this.cache.memberNames[index] = this.cache.memberInfos[index].Name;
          this.cache.memberTypes[index] = this.GetMemberType(this.cache.memberInfos[index]);
        }
        this.serObjectInfoInit.seenBeforeTable.Add((object) this.objectType, (object) this.cache);
      }
      if (this.obj != null)
        this.memberData = FormatterServices.GetObjectData(this.obj, this.cache.memberInfos);
      this.isTyped = true;
      this.isNamed = true;
    }

    internal string GetTypeFullName()
    {
      return this.binderTypeName ?? this.cache.fullTypeName;
    }

    internal string GetAssemblyString()
    {
      return this.binderAssemblyString ?? this.cache.assemblyString;
    }

    private void InvokeSerializationBinder(SerializationBinder binder)
    {
      if (binder == null)
        return;
      binder.BindToName(this.objectType, out this.binderAssemblyString, out this.binderTypeName);
    }

    internal Type GetMemberType(MemberInfo objMember)
    {
      if (objMember is FieldInfo)
        return ((FieldInfo) objMember).FieldType;
      if (objMember is PropertyInfo)
        return ((PropertyInfo) objMember).PropertyType;
      throw new SerializationException(Environment.GetResourceString("Serialization_SerMemberInfo", (object) objMember.GetType()));
    }

    internal void GetMemberInfo(out string[] outMemberNames, out Type[] outMemberTypes, out object[] outMemberData)
    {
      outMemberNames = this.cache.memberNames;
      outMemberTypes = this.cache.memberTypes;
      outMemberData = this.memberData;
      if (this.isSi && !this.isNamed)
        throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableMemberInfo"));
    }

    private static WriteObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
    {
      WriteObjectInfo writeObjectInfo1;
      if (!serObjectInfoInit.oiPool.IsEmpty())
      {
        writeObjectInfo1 = (WriteObjectInfo) serObjectInfoInit.oiPool.Pop();
        writeObjectInfo1.InternalInit();
      }
      else
      {
        writeObjectInfo1 = new WriteObjectInfo();
        WriteObjectInfo writeObjectInfo2 = writeObjectInfo1;
        SerObjectInfoInit serObjectInfoInit1 = serObjectInfoInit;
        int num1 = serObjectInfoInit1.objectInfoIdCount;
        int num2 = num1 + 1;
        serObjectInfoInit1.objectInfoIdCount = num2;
        int num3 = num1;
        writeObjectInfo2.objectInfoId = num3;
      }
      return writeObjectInfo1;
    }

    private static void PutObjectInfo(SerObjectInfoInit serObjectInfoInit, WriteObjectInfo objectInfo)
    {
      serObjectInfoInit.oiPool.Push((object) objectInfo);
    }
  }
}
