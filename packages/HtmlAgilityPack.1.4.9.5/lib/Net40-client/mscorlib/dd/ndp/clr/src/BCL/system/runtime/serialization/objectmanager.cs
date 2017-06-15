// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.Runtime.Serialization
{
  /// <summary>在对象进行反序列化时对其进行跟踪。</summary>
  [ComVisible(true)]
  public class ObjectManager
  {
    private static RuntimeType TypeOfWindowsIdentity = (RuntimeType) typeof (WindowsIdentity);
    private const int DefaultInitialSize = 16;
    private const int MaxArraySize = 4096;
    private const int ArrayMask = 4095;
    private const int MaxReferenceDepth = 100;
    private DeserializationEventHandler m_onDeserializationHandler;
    private SerializationEventHandler m_onDeserializedHandler;
    internal ObjectHolder[] m_objects;
    internal object m_topObject;
    internal ObjectHolderList m_specialFixupObjects;
    internal long m_fixupCount;
    internal ISurrogateSelector m_selector;
    internal StreamingContext m_context;
    private bool m_isCrossAppDomain;

    internal object TopObject
    {
      get
      {
        return this.m_topObject;
      }
      set
      {
        this.m_topObject = value;
      }
    }

    internal ObjectHolderList SpecialFixupObjects
    {
      get
      {
        if (this.m_specialFixupObjects == null)
          this.m_specialFixupObjects = new ObjectHolderList();
        return this.m_specialFixupObjects;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.ObjectManager" /> 类的新实例。</summary>
    /// <param name="selector">要使用的代理项选择器。<see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> 确定在反序列化给定类型的对象时所使用的正确代理项。在反序列化时，代理项选择器将利用通过流传输的信息创建对象的新实例。</param>
    /// <param name="context">流上下文。<see cref="T:System.Runtime.Serialization.StreamingContext" /> 未被 ObjectManager 使用，但却作为参数传递到任何实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 或具有 <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> 的对象。这些对象可以根据要反序列化的信息的源来执行特定的操作。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecuritySafeCritical]
    public ObjectManager(ISurrogateSelector selector, StreamingContext context)
      : this(selector, context, true, false)
    {
    }

    [SecurityCritical]
    internal ObjectManager(ISurrogateSelector selector, StreamingContext context, bool checkSecurity, bool isCrossAppDomain)
    {
      if (checkSecurity)
        CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
      this.m_objects = new ObjectHolder[16];
      this.m_selector = selector;
      this.m_context = context;
      this.m_isCrossAppDomain = isCrossAppDomain;
    }

    [SecurityCritical]
    private bool CanCallGetType(object obj)
    {
      return !RemotingServices.IsTransparentProxy(obj);
    }

    internal ObjectHolder FindObjectHolder(long objectID)
    {
      int index = (int) (objectID & 4095L);
      if (index >= this.m_objects.Length)
        return (ObjectHolder) null;
      ObjectHolder objectHolder = this.m_objects[index];
      while (objectHolder != null && objectHolder.m_id != objectID)
        objectHolder = objectHolder.m_next;
      return objectHolder;
    }

    internal ObjectHolder FindOrCreateObjectHolder(long objectID)
    {
      ObjectHolder holder = this.FindObjectHolder(objectID);
      if (holder == null)
      {
        holder = new ObjectHolder(objectID);
        this.AddObjectHolder(holder);
      }
      return holder;
    }

    private void AddObjectHolder(ObjectHolder holder)
    {
      if (holder.m_id >= (long) this.m_objects.Length && this.m_objects.Length != 4096)
      {
        int length = 4096;
        if (holder.m_id < 2048L)
        {
          length = this.m_objects.Length * 2;
          while ((long) length <= holder.m_id && length < 4096)
            length *= 2;
          if (length > 4096)
            length = 4096;
        }
        ObjectHolder[] objectHolderArray = new ObjectHolder[length];
        Array.Copy((Array) this.m_objects, (Array) objectHolderArray, this.m_objects.Length);
        this.m_objects = objectHolderArray;
      }
      int index = (int) (holder.m_id & 4095L);
      ObjectHolder objectHolder = this.m_objects[index];
      holder.m_next = objectHolder;
      this.m_objects[index] = holder;
    }

    private bool GetCompletionInfo(FixupHolder fixup, out ObjectHolder holder, out object member, bool bThrowIfMissing)
    {
      member = fixup.m_fixupInfo;
      holder = this.FindObjectHolder(fixup.m_id);
      if (!holder.CompletelyFixed && holder.ObjectValue != null && holder.ObjectValue is ValueType)
      {
        this.SpecialFixupObjects.Add(holder);
        return false;
      }
      if (holder != null && !holder.CanObjectValueChange && holder.ObjectValue != null)
        return true;
      if (!bThrowIfMissing)
        return false;
      if (holder == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_NeverSeen", (object) fixup.m_id));
      if (holder.IsIncompleteObjectReference)
        throw new SerializationException(Environment.GetResourceString("Serialization_IORIncomplete", (object) fixup.m_id));
      throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", (object) fixup.m_id));
    }

    [SecurityCritical]
    private void FixupSpecialObject(ObjectHolder holder)
    {
      ISurrogateSelector selector = (ISurrogateSelector) null;
      if (holder.HasSurrogate)
      {
        ISerializationSurrogate surrogate = holder.Surrogate;
        object obj = surrogate.SetObjectData(holder.ObjectValue, holder.SerializationInfo, this.m_context, selector);
        if (obj != null)
        {
          if (!holder.CanSurrogatedObjectValueChange && obj != holder.ObjectValue)
            throw new SerializationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_NotCyclicallyReferenceableSurrogate"), (object) surrogate.GetType().FullName));
          holder.SetObjectValue(obj, this);
        }
        holder.m_surrogate = (ISerializationSurrogate) null;
        holder.SetFlags();
      }
      else
        this.CompleteISerializableObject(holder.ObjectValue, holder.SerializationInfo, this.m_context);
      holder.SerializationInfo = (SerializationInfo) null;
      holder.RequiresSerInfoFixup = false;
      if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
      {
        // ISSUE: variable of the null type
        __Null local = null;
        ObjectHolder holder1 = holder;
        object objectValue = holder1.ObjectValue;
        this.DoValueTypeFixup((FieldInfo) local, holder1, objectValue);
      }
      this.DoNewlyRegisteredObjectFixups(holder);
    }

    [SecurityCritical]
    private bool ResolveObjectReference(ObjectHolder holder)
    {
      int num = 0;
      try
      {
        object objectValue;
        do
        {
          objectValue = holder.ObjectValue;
          ObjectHolder objectHolder = holder;
          object realObject = ((IObjectReference) objectHolder.ObjectValue).GetRealObject(this.m_context);
          objectHolder.SetObjectValue(realObject, this);
          if (holder.ObjectValue == null)
          {
            holder.SetObjectValue(objectValue, this);
            return false;
          }
          if (num++ == 100)
            throw new SerializationException(Environment.GetResourceString("Serialization_TooManyReferences"));
          if (!(holder.ObjectValue is IObjectReference))
            break;
        }
        while (objectValue != holder.ObjectValue);
      }
      catch (NullReferenceException ex)
      {
        return false;
      }
      holder.IsIncompleteObjectReference = false;
      this.DoNewlyRegisteredObjectFixups(holder);
      return true;
    }

    [SecurityCritical]
    private bool DoValueTypeFixup(FieldInfo memberToFix, ObjectHolder holder, object value)
    {
      FieldInfo[] fieldInfoArray1 = new FieldInfo[4];
      int length = 0;
      int[] numArray = (int[]) null;
      object objectValue = holder.ObjectValue;
      while (holder.RequiresValueTypeFixup)
      {
        if (length + 1 >= fieldInfoArray1.Length)
        {
          FieldInfo[] fieldInfoArray2 = new FieldInfo[fieldInfoArray1.Length * 2];
          Array.Copy((Array) fieldInfoArray1, (Array) fieldInfoArray2, fieldInfoArray1.Length);
          fieldInfoArray1 = fieldInfoArray2;
        }
        ValueTypeFixupInfo valueFixup = holder.ValueFixup;
        objectValue = holder.ObjectValue;
        if (valueFixup.ParentField != (FieldInfo) null)
        {
          FieldInfo parentField = valueFixup.ParentField;
          ObjectHolder objectHolder = this.FindObjectHolder(valueFixup.ContainerID);
          if (objectHolder.ObjectValue != null)
          {
            if (Nullable.GetUnderlyingType(parentField.FieldType) != (Type) null)
            {
              fieldInfoArray1[length] = parentField.FieldType.GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
              ++length;
            }
            fieldInfoArray1[length] = parentField;
            holder = objectHolder;
            ++length;
          }
          else
            break;
        }
        else
        {
          holder = this.FindObjectHolder(valueFixup.ContainerID);
          numArray = valueFixup.ParentIndex;
          if (holder.ObjectValue != null)
            break;
          break;
        }
      }
      if (!(holder.ObjectValue is Array) && holder.ObjectValue != null)
        objectValue = holder.ObjectValue;
      if (length != 0)
      {
        FieldInfo[] flds = new FieldInfo[length];
        for (int index = 0; index < length; ++index)
        {
          FieldInfo fieldInfo = fieldInfoArray1[length - 1 - index];
          SerializationFieldInfo serializationFieldInfo = fieldInfo as SerializationFieldInfo;
          flds[index] = (FieldInfo) serializationFieldInfo == (FieldInfo) null ? fieldInfo : (FieldInfo) serializationFieldInfo.FieldInfo;
        }
        TypedReference target = TypedReference.MakeTypedReference(objectValue, flds);
        if (memberToFix != (FieldInfo) null)
          memberToFix.SetValueDirect(target, value);
        else
          TypedReference.SetTypedReference(target, value);
      }
      else if (memberToFix != (FieldInfo) null)
        FormatterServices.SerializationSetValue((MemberInfo) memberToFix, objectValue, value);
      if (numArray != null && holder.ObjectValue != null)
        ((Array) holder.ObjectValue).SetValue(objectValue, numArray);
      return true;
    }

    [Conditional("SER_LOGGING")]
    private void DumpValueTypeFixup(object obj, FieldInfo[] intermediateFields, FieldInfo memberToFix, object value)
    {
      StringBuilder stringBuilder = new StringBuilder("  " + obj);
      if (intermediateFields != null)
      {
        for (int index = 0; index < intermediateFields.Length; ++index)
          stringBuilder.Append("." + intermediateFields[index].Name);
      }
      stringBuilder.Append("." + memberToFix.Name + "=" + value);
    }

    [SecurityCritical]
    internal void CompleteObject(ObjectHolder holder, bool bObjectFullyComplete)
    {
      FixupHolderList fixupHolderList = holder.m_missingElements;
      object member = (object) null;
      ObjectHolder holder1 = (ObjectHolder) null;
      int num = 0;
      if (holder.ObjectValue == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissingObject", (object) holder.m_id));
      if (fixupHolderList == null)
        return;
      if (holder.HasSurrogate || holder.HasISerializable)
      {
        SerializationInfo serializationInfo1 = holder.m_serInfo;
        if (serializationInfo1 == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupDiscovered"));
        if (fixupHolderList != null)
        {
          for (int index = 0; index < fixupHolderList.m_count; ++index)
          {
            if (fixupHolderList.m_values[index] != null && this.GetCompletionInfo(fixupHolderList.m_values[index], out holder1, out member, bObjectFullyComplete))
            {
              object objectValue = holder1.ObjectValue;
              if (this.CanCallGetType(objectValue))
              {
                SerializationInfo serializationInfo2 = serializationInfo1;
                string name = (string) member;
                object obj = objectValue;
                Type type = obj.GetType();
                serializationInfo2.UpdateValue(name, obj, type);
              }
              else
                serializationInfo1.UpdateValue((string) member, objectValue, typeof (MarshalByRefObject));
              ++num;
              fixupHolderList.m_values[index] = (FixupHolder) null;
              if (!bObjectFullyComplete)
              {
                holder.DecrementFixupsRemaining(this);
                holder1.RemoveDependency(holder.m_id);
              }
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < fixupHolderList.m_count; ++index)
        {
          FixupHolder fixup = fixupHolderList.m_values[index];
          if (fixup != null && this.GetCompletionInfo(fixup, out holder1, out member, bObjectFullyComplete))
          {
            if (holder1.TypeLoadExceptionReachable)
            {
              holder.TypeLoadException = holder1.TypeLoadException;
              if (holder.Reachable)
                throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", (object) holder.TypeLoadException.TypeName));
            }
            if (holder.Reachable)
              holder1.Reachable = true;
            switch (fixup.m_fixupType)
            {
              case 1:
                if (holder.RequiresValueTypeFixup)
                  throw new SerializationException(Environment.GetResourceString("Serialization_ValueTypeFixup"));
                ((Array) holder.ObjectValue).SetValue(holder1.ObjectValue, (int[]) member);
                break;
              case 2:
                MemberInfo fi = (MemberInfo) member;
                if (fi.MemberType != MemberTypes.Field)
                  throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
                if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
                {
                  if (!this.DoValueTypeFixup((FieldInfo) fi, holder, holder1.ObjectValue))
                    throw new SerializationException(Environment.GetResourceString("Serialization_PartialValueTypeFixup"));
                }
                else
                  FormatterServices.SerializationSetValue(fi, holder.ObjectValue, holder1.ObjectValue);
                if (holder1.RequiresValueTypeFixup)
                {
                  holder1.ValueTypeFixupPerformed = true;
                  break;
                }
                break;
              default:
                throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
            }
            ++num;
            fixupHolderList.m_values[index] = (FixupHolder) null;
            if (!bObjectFullyComplete)
            {
              holder.DecrementFixupsRemaining(this);
              holder1.RemoveDependency(holder.m_id);
            }
          }
        }
      }
      this.m_fixupCount = this.m_fixupCount - (long) num;
      if (fixupHolderList.m_count != num)
        return;
      holder.m_missingElements = (FixupHolderList) null;
    }

    [SecurityCritical]
    private void DoNewlyRegisteredObjectFixups(ObjectHolder holder)
    {
      if (holder.CanObjectValueChange)
        return;
      LongList dependentObjects = holder.DependentObjects;
      if (dependentObjects == null)
        return;
      dependentObjects.StartEnumeration();
      while (dependentObjects.MoveNext())
      {
        ObjectHolder objectHolder = this.FindObjectHolder(dependentObjects.Current);
        objectHolder.DecrementFixupsRemaining(this);
        if (objectHolder.DirectlyDependentObjects == 0)
        {
          if (objectHolder.ObjectValue != null)
            this.CompleteObject(objectHolder, true);
          else
            objectHolder.MarkForCompletionWhenAvailable();
        }
      }
    }

    /// <summary>返回具有指定对象 ID 的对象。</summary>
    /// <returns>如果先前已存储，则返回具有指定对象 ID 的对象；或者，如果尚未注册这种对象，则为 null。</returns>
    /// <param name="objectID">所请求对象的 ID。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectID" /> 参数小于或等于零。</exception>
    public virtual object GetObject(long objectID)
    {
      if (objectID <= 0L)
        throw new ArgumentOutOfRangeException("objectID", Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
      ObjectHolder objectHolder = this.FindObjectHolder(objectID);
      if (objectHolder == null || objectHolder.CanObjectValueChange)
        return (object) null;
      return objectHolder.ObjectValue;
    }

    /// <summary>在反序列化对象时注册该对象，并将其与 <paramref name="objectID" /> 相关联。</summary>
    /// <param name="obj">要注册的对象。</param>
    /// <param name="objectID">要注册的对象的 ID。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectID" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="objectID" /> 已为 <paramref name="obj" /> 以外的对象进行了注册。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void RegisterObject(object obj, long objectID)
    {
      this.RegisterObject(obj, objectID, (SerializationInfo) null, 0L, (MemberInfo) null);
    }

    /// <summary>在反序列化对象时注册该对象，同时将其与 <paramref name="objectID" /> 相关联并记录与其一起使用的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="obj">要注册的对象。</param>
    /// <param name="objectID">要注册的对象的 ID。</param>
    /// <param name="info">当 <paramref name="obj" /> 实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 或具有 <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> 时使用的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。<paramref name="info" /> 将用任何所需的链接地址信息来完成，然后在所需的对象完成时传递给该对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectID" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="objectID" /> 已为 <paramref name="obj" /> 以外的对象进行了注册。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecurityCritical]
    public void RegisterObject(object obj, long objectID, SerializationInfo info)
    {
      this.RegisterObject(obj, objectID, info, 0L, (MemberInfo) null);
    }

    /// <summary>在对象成员反序列化时注册该对象成员，同时将其与 <paramref name="objectID" /> 相关联并记录 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="obj">要注册的对象。</param>
    /// <param name="objectID">要注册的对象的 ID。</param>
    /// <param name="info">当 <paramref name="obj" /> 实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 或具有 <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> 时使用的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。<paramref name="info" /> 将用任何所需的链接地址信息来完成，然后在所需的对象完成时传递给该对象。</param>
    /// <param name="idOfContainingObj">包含 <paramref name="obj" /> 的对象的 ID。仅当 <paramref name="obj" /> 是值类型时才需要此参数。</param>
    /// <param name="member">
    /// <paramref name="obj" /> 所在的包含对象中的字段。仅当 <paramref name="obj" /> 是值类型时此参数才有意义。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectID" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="objectID" /> 已经为 <paramref name="obj" /> 之外的对象注册，或者 <paramref name="member" /> 不为 <see cref="T:System.Reflection.FieldInfo" /> 且 <paramref name="member" /> 不为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecurityCritical]
    public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
    {
      this.RegisterObject(obj, objectID, info, idOfContainingObj, member, (int[]) null);
    }

    internal void RegisterString(string obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
    {
      this.AddObjectHolder(new ObjectHolder(obj, objectID, info, (ISerializationSurrogate) null, idOfContainingObj, (FieldInfo) member, (int[]) null));
    }

    /// <summary>在对象中包含的数组的成员进行反序列化时注册该成员，同时将其与 <paramref name="objectID" /> 相关联并记录 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="obj">要注册的对象。</param>
    /// <param name="objectID">要注册的对象的 ID。</param>
    /// <param name="info">当 <paramref name="obj" /> 实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 或具有 <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> 时使用的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。<paramref name="info" /> 将用任何所需的链接地址信息来完成，然后在所需的对象完成时传递给该对象。</param>
    /// <param name="idOfContainingObj">包含 <paramref name="obj" /> 的对象的 ID。仅当 <paramref name="obj" /> 是值类型时才需要此参数。</param>
    /// <param name="member">
    /// <paramref name="obj" /> 所在的包含对象中的字段。仅当 <paramref name="obj" /> 是值类型时此参数才有意义。</param>
    /// <param name="arrayIndex">如果 <paramref name="obj" /> 为 <see cref="T:System.ValueType" /> 和数组的成员，<paramref name="arrayIndex" /> 则包含 <paramref name="obj" /> 所在数组中的索引。如果 <paramref name="obj" /> 不是同时为 <see cref="T:System.ValueType" /> 和数组成员，则将忽略 <paramref name="arrayIndex" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectID" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <paramref name="objectID" /> 已经为 <paramref name="obj" /> 之外的对象注册，或者 <paramref name="member" /> 不是 <see cref="T:System.Reflection.FieldInfo" /> 且 <paramref name="member" /> 不是 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecurityCritical]
    public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member, int[] arrayIndex)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (objectID <= 0L)
        throw new ArgumentOutOfRangeException("objectID", Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
      if (member != (MemberInfo) null && !(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
        throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
      ISerializationSurrogate surrogate = (ISerializationSurrogate) null;
      if (this.m_selector != null)
      {
        ISurrogateSelector selector;
        surrogate = this.m_selector.GetSurrogate(!this.CanCallGetType(obj) ? typeof (MarshalByRefObject) : obj.GetType(), this.m_context, out selector);
      }
      if (obj is IDeserializationCallback)
        this.AddOnDeserialization(new DeserializationEventHandler(((IDeserializationCallback) obj).OnDeserialization));
      if (arrayIndex != null)
        arrayIndex = (int[]) arrayIndex.Clone();
      ObjectHolder objectHolder = this.FindObjectHolder(objectID);
      if (objectHolder == null)
      {
        ObjectHolder holder = new ObjectHolder(obj, objectID, info, surrogate, idOfContainingObj, (FieldInfo) member, arrayIndex);
        this.AddObjectHolder(holder);
        if (holder.RequiresDelayedFixup)
          this.SpecialFixupObjects.Add(holder);
        this.AddOnDeserialized(obj);
      }
      else
      {
        if (objectHolder.ObjectValue != null)
          throw new SerializationException(Environment.GetResourceString("Serialization_RegisterTwice"));
        objectHolder.UpdateData(obj, info, surrogate, idOfContainingObj, (FieldInfo) member, arrayIndex, this);
        if (objectHolder.DirectlyDependentObjects > 0)
          this.CompleteObject(objectHolder, false);
        if (objectHolder.RequiresDelayedFixup)
          this.SpecialFixupObjects.Add(objectHolder);
        if (objectHolder.CompletelyFixed)
        {
          this.DoNewlyRegisteredObjectFixups(objectHolder);
          objectHolder.DependentObjects = (LongList) null;
        }
        if (objectHolder.TotalDependentObjects > 0)
          this.AddOnDeserialized(obj);
        else
          this.RaiseOnDeserializedEvent(obj);
      }
    }

    [SecurityCritical]
    internal void CompleteISerializableObject(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (!(obj is ISerializable))
        throw new ArgumentException(Environment.GetResourceString("Serialization_NotISer"));
      RuntimeType t = (RuntimeType) obj.GetType();
      RuntimeConstructorInfo runtimeConstructorInfo;
      try
      {
        runtimeConstructorInfo = !(t == ObjectManager.TypeOfWindowsIdentity) || !this.m_isCrossAppDomain ? ObjectManager.GetConstructor(t) : WindowsIdentity.GetSpecialSerializationCtor();
      }
      catch (Exception ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", (object) t), ex);
      }
      runtimeConstructorInfo.SerializationInvoke(obj, info, context);
    }

    internal static RuntimeConstructorInfo GetConstructor(RuntimeType t)
    {
      RuntimeConstructorInfo serializationCtor = t.GetSerializationCtor();
      if ((ConstructorInfo) serializationCtor == (ConstructorInfo) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", (object) t.FullName));
      return serializationCtor;
    }

    /// <summary>执行所有记录的修正。</summary>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">修正未成功完成。</exception>
    [SecuritySafeCritical]
    public virtual void DoFixups()
    {
      int num = -1;
      while (num != 0)
      {
        num = 0;
        ObjectHolderListEnumerator fixupEnumerator = this.SpecialFixupObjects.GetFixupEnumerator();
        while (fixupEnumerator.MoveNext())
        {
          ObjectHolder current = fixupEnumerator.Current;
          if (current.ObjectValue == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", (object) current.m_id));
          if (current.TotalDependentObjects == 0)
          {
            if (current.RequiresSerInfoFixup)
            {
              this.FixupSpecialObject(current);
              ++num;
            }
            else if (!current.IsIncompleteObjectReference)
              this.CompleteObject(current, true);
            if (current.IsIncompleteObjectReference && this.ResolveObjectReference(current))
              ++num;
          }
        }
      }
      if (this.m_fixupCount == 0L)
      {
        if (this.TopObject is TypeLoadExceptionHolder)
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", (object) ((TypeLoadExceptionHolder) this.TopObject).TypeName));
      }
      else
      {
        for (int index = 0; index < this.m_objects.Length; ++index)
        {
          for (ObjectHolder holder = this.m_objects[index]; holder != null; holder = holder.m_next)
          {
            if (holder.TotalDependentObjects > 0)
              this.CompleteObject(holder, true);
          }
          if (this.m_fixupCount == 0L)
            return;
        }
        throw new SerializationException(Environment.GetResourceString("Serialization_IncorrectNumberOfFixups"));
      }
    }

    private void RegisterFixup(FixupHolder fixup, long objectToBeFixed, long objectRequired)
    {
      ObjectHolder createObjectHolder = this.FindOrCreateObjectHolder(objectToBeFixed);
      if (createObjectHolder.RequiresSerInfoFixup && fixup.m_fixupType == 2)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupType"));
      FixupHolder fixup1 = fixup;
      createObjectHolder.AddFixup(fixup1, this);
      this.FindOrCreateObjectHolder(objectRequired).AddDependency(objectToBeFixed);
      this.m_fixupCount = this.m_fixupCount + 1L;
    }

    /// <summary>记录对象成员的修正，以便在稍后执行。</summary>
    /// <param name="objectToBeFixed">需要对 <paramref name="objectRequired" /> 对象的引用的对象的 ID。</param>
    /// <param name="member">将在其中执行修正的 <paramref name="objectToBeFixed" /> 的成员。</param>
    /// <param name="objectRequired">
    /// <paramref name="objectToBeFixed" /> 需要的对象的 ID。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectToBeFixed" /> 或 <paramref name="objectRequired" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="member" /> 参数为 null。</exception>
    public virtual void RecordFixup(long objectToBeFixed, MemberInfo member, long objectRequired)
    {
      if (objectToBeFixed <= 0L || objectRequired <= 0L)
        throw new ArgumentOutOfRangeException(objectToBeFixed <= 0L ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
      if (member == (MemberInfo) null)
        throw new ArgumentNullException("member");
      if (!(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) member.GetType().ToString()));
      this.RegisterFixup(new FixupHolder(objectRequired, (object) member, 2), objectToBeFixed, objectRequired);
    }

    /// <summary>记录对象成员的修正，以便在稍后执行。</summary>
    /// <param name="objectToBeFixed">需要对 <paramref name="objectRequired" /> 的引用的对象的 ID。</param>
    /// <param name="memberName">将在其中执行修正的 <paramref name="objectToBeFixed" /> 的成员名称。</param>
    /// <param name="objectRequired">
    /// <paramref name="objectToBeFixed" /> 需要的对象的 ID。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="objectToBeFixed" /> 或 <paramref name="objectRequired" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="memberName" /> 参数为 null。</exception>
    public virtual void RecordDelayedFixup(long objectToBeFixed, string memberName, long objectRequired)
    {
      if (objectToBeFixed <= 0L || objectRequired <= 0L)
        throw new ArgumentOutOfRangeException(objectToBeFixed <= 0L ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
      if (memberName == null)
        throw new ArgumentNullException("memberName");
      this.RegisterFixup(new FixupHolder(objectRequired, (object) memberName, 4), objectToBeFixed, objectRequired);
    }

    /// <summary>记录数组中一个元素的修正。</summary>
    /// <param name="arrayToBeFixed">用于记录修正的数组的 ID。</param>
    /// <param name="index">为其请求修正的 <paramref name="arrayFixup" /> 中的索引。</param>
    /// <param name="objectRequired">当前数组元素在修正完成后将指向的对象的 ID。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayToBeFixed" /> 或 <paramref name="objectRequired" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="index" /> 参数为 null。</exception>
    public virtual void RecordArrayElementFixup(long arrayToBeFixed, int index, long objectRequired)
    {
      int[] indices = new int[1]{ index };
      this.RecordArrayElementFixup(arrayToBeFixed, indices, objectRequired);
    }

    /// <summary>记录数组中指定元素的修正，以便在稍后执行。</summary>
    /// <param name="arrayToBeFixed">用于记录修正的数组的 ID。</param>
    /// <param name="indices">为其请求修正的多维数组中的索引。</param>
    /// <param name="objectRequired">数组元素在修正完成后将指向的对象的 ID。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayToBeFixed" /> 或 <paramref name="objectRequired" /> 参数小于或等于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="indices" /> 参数为 null。</exception>
    public virtual void RecordArrayElementFixup(long arrayToBeFixed, int[] indices, long objectRequired)
    {
      if (arrayToBeFixed <= 0L || objectRequired <= 0L)
        throw new ArgumentOutOfRangeException(arrayToBeFixed <= 0L ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
      if (indices == null)
        throw new ArgumentNullException("indices");
      this.RegisterFixup(new FixupHolder(objectRequired, (object) indices, 1), arrayToBeFixed, objectRequired);
    }

    /// <summary>对任何实现 <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> 的注册对象引发反序列化事件。</summary>
    public virtual void RaiseDeserializationEvent()
    {
      if (this.m_onDeserializedHandler != null)
        this.m_onDeserializedHandler(this.m_context);
      if (this.m_onDeserializationHandler == null)
        return;
      this.m_onDeserializationHandler((object) null);
    }

    internal virtual void AddOnDeserialization(DeserializationEventHandler handler)
    {
      this.m_onDeserializationHandler = this.m_onDeserializationHandler + handler;
    }

    internal virtual void RemoveOnDeserialization(DeserializationEventHandler handler)
    {
      this.m_onDeserializationHandler = this.m_onDeserializationHandler - handler;
    }

    [SecuritySafeCritical]
    internal virtual void AddOnDeserialized(object obj)
    {
      this.m_onDeserializedHandler = SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).AddOnDeserialized(obj, this.m_onDeserializedHandler);
    }

    internal virtual void RaiseOnDeserializedEvent(object obj)
    {
      SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).InvokeOnDeserialized(obj, this.m_context);
    }

    /// <summary>调用使用 <see cref="T:System.Runtime.Serialization.OnDeserializingAttribute" /> 标记的方法。</summary>
    /// <param name="obj">包含要调用的方法的类型实例。</param>
    public void RaiseOnDeserializingEvent(object obj)
    {
      SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).InvokeOnDeserializing(obj, this.m_context);
    }
  }
}
