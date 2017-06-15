// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeEventInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  [Serializable]
  internal sealed class RuntimeEventInfo : EventInfo, ISerializable
  {
    private int m_token;
    private EventAttributes m_flags;
    private string m_name;
    [SecurityCritical]
    private unsafe void* m_utf8name;
    private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;
    private RuntimeMethodInfo m_addMethod;
    private RuntimeMethodInfo m_removeMethod;
    private RuntimeMethodInfo m_raiseMethod;
    private MethodInfo[] m_otherMethod;
    private RuntimeType m_declaringType;
    private BindingFlags m_bindingFlags;

    internal BindingFlags BindingFlags
    {
      get
      {
        return this.m_bindingFlags;
      }
    }

    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Event;
      }
    }

    public override unsafe string Name
    {
      [SecuritySafeCritical] get
      {
        if (this.m_name == null)
          this.m_name = new Utf8String(this.m_utf8name).ToString();
        return this.m_name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_declaringType;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return (Type) this.ReflectedTypeInternal;
      }
    }

    private RuntimeType ReflectedTypeInternal
    {
      get
      {
        return this.m_reflectedTypeCache.GetRuntimeType();
      }
    }

    public override int MetadataToken
    {
      get
      {
        return this.m_token;
      }
    }

    public override Module Module
    {
      get
      {
        return (Module) this.GetRuntimeModule();
      }
    }

    public override EventAttributes Attributes
    {
      get
      {
        return this.m_flags;
      }
    }

    internal RuntimeEventInfo()
    {
    }

    [SecurityCritical]
    internal unsafe RuntimeEventInfo(int tkEvent, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
    {
      MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
      this.m_token = tkEvent;
      this.m_reflectedTypeCache = reflectedTypeCache;
      this.m_declaringType = declaredType;
      RuntimeType runtimeType = reflectedTypeCache.GetRuntimeType();
      metadataImport.GetEventProps(tkEvent, out this.m_utf8name, out this.m_flags);
      MetadataImport scope = metadataImport;
      int mdPropEvent = tkEvent;
      RuntimeType declaringType = declaredType;
      RuntimeType reflectedType = runtimeType;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      RuntimeMethodInfo& addOn = @this.m_addMethod;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      RuntimeMethodInfo& removeOn = @this.m_removeMethod;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      RuntimeMethodInfo& fireOn = @this.m_raiseMethod;
      RuntimeMethodInfo runtimeMethodInfo;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      RuntimeMethodInfo& local = @runtimeMethodInfo;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      MethodInfo[]& other = @this.m_otherMethod;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      bool& composedOfAllPrivateMethods = @isPrivate;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      BindingFlags& bindingFlags = @this.m_bindingFlags;
      Associates.AssignAssociates(scope, mdPropEvent, declaringType, reflectedType, addOn, removeOn, fireOn, local, local, other, composedOfAllPrivateMethods, bindingFlags);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal override bool CacheEquals(object o)
    {
      RuntimeEventInfo runtimeEventInfo = o as RuntimeEventInfo;
      if (runtimeEventInfo == null || runtimeEventInfo.m_token != this.m_token)
        return false;
      return RuntimeTypeHandle.GetModule(this.m_declaringType).Equals((object) RuntimeTypeHandle.GetModule(runtimeEventInfo.m_declaringType));
    }

    public override string ToString()
    {
      if ((MethodInfo) this.m_addMethod == (MethodInfo) null || this.m_addMethod.GetParametersNoCopy().Length == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
      return this.m_addMethod.GetParametersNoCopy()[0].ParameterType.FormatTypeName() + " " + this.Name;
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return CustomAttribute.GetCustomAttributes(this, typeof (object) as RuntimeType);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      RuntimeType caType = attributeType.UnderlyingSystemType as RuntimeType;
      if (caType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
      return CustomAttribute.GetCustomAttributes(this, caType);
    }

    [SecuritySafeCritical]
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      RuntimeType caType = attributeType.UnderlyingSystemType as RuntimeType;
      if (caType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
      return CustomAttribute.IsDefined(this, caType);
    }

    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return CustomAttributeData.GetCustomAttributesInternal(this);
    }

    internal RuntimeModule GetRuntimeModule()
    {
      return this.m_declaringType.GetRuntimeModule();
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, (string) null, MemberTypes.Event);
    }

    public override MethodInfo[] GetOtherMethods(bool nonPublic)
    {
      List<MethodInfo> methodInfoList = new List<MethodInfo>();
      if (this.m_otherMethod == null)
        return new MethodInfo[0];
      for (int index = 0; index < this.m_otherMethod.Length; ++index)
      {
        if (Associates.IncludeAccessor(this.m_otherMethod[index], nonPublic))
          methodInfoList.Add(this.m_otherMethod[index]);
      }
      return methodInfoList.ToArray();
    }

    public override MethodInfo GetAddMethod(bool nonPublic)
    {
      if (!Associates.IncludeAccessor((MethodInfo) this.m_addMethod, nonPublic))
        return (MethodInfo) null;
      return (MethodInfo) this.m_addMethod;
    }

    public override MethodInfo GetRemoveMethod(bool nonPublic)
    {
      if (!Associates.IncludeAccessor((MethodInfo) this.m_removeMethod, nonPublic))
        return (MethodInfo) null;
      return (MethodInfo) this.m_removeMethod;
    }

    public override MethodInfo GetRaiseMethod(bool nonPublic)
    {
      if (!Associates.IncludeAccessor((MethodInfo) this.m_raiseMethod, nonPublic))
        return (MethodInfo) null;
      return (MethodInfo) this.m_raiseMethod;
    }
  }
}
