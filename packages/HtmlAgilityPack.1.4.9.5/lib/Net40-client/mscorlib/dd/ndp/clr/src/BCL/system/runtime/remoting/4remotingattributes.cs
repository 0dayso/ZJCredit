// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.RemotingMethodCachedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  internal class RemotingMethodCachedData : RemotingCachedData
  {
    private MethodBase RI;
    private ParameterInfo[] _parameters;
    private RemotingMethodCachedData.MethodCacheFlags flags;
    private string _typeAndAssemblyName;
    private string _methodName;
    private Type _returnType;
    private int[] _inRefArgMap;
    private int[] _outRefArgMap;
    private int[] _outOnlyArgMap;
    private int[] _nonRefOutArgMap;
    private int[] _marshalRequestMap;
    private int[] _marshalResponseMap;

    internal string TypeAndAssemblyName
    {
      [SecurityCritical] get
      {
        if (this._typeAndAssemblyName == null)
          this.UpdateNames();
        return this._typeAndAssemblyName;
      }
    }

    internal string MethodName
    {
      [SecurityCritical] get
      {
        if (this._methodName == null)
          this.UpdateNames();
        return this._methodName;
      }
    }

    internal ParameterInfo[] Parameters
    {
      get
      {
        if (this._parameters == null)
          this._parameters = this.RI.GetParameters();
        return this._parameters;
      }
    }

    internal int[] OutRefArgMap
    {
      get
      {
        if (this._outRefArgMap == null)
          this.GetArgMaps();
        return this._outRefArgMap;
      }
    }

    internal int[] OutOnlyArgMap
    {
      get
      {
        if (this._outOnlyArgMap == null)
          this.GetArgMaps();
        return this._outOnlyArgMap;
      }
    }

    internal int[] NonRefOutArgMap
    {
      get
      {
        if (this._nonRefOutArgMap == null)
          this.GetArgMaps();
        return this._nonRefOutArgMap;
      }
    }

    internal int[] MarshalRequestArgMap
    {
      get
      {
        if (this._marshalRequestMap == null)
          this.GetArgMaps();
        return this._marshalRequestMap;
      }
    }

    internal int[] MarshalResponseArgMap
    {
      get
      {
        if (this._marshalResponseMap == null)
          this.GetArgMaps();
        return this._marshalResponseMap;
      }
    }

    internal Type ReturnType
    {
      get
      {
        if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType) == RemotingMethodCachedData.MethodCacheFlags.None)
        {
          MethodInfo methodInfo = this.RI as MethodInfo;
          if (methodInfo != (MethodInfo) null)
          {
            Type returnType = methodInfo.ReturnType;
            if (returnType != typeof (void))
              this._returnType = returnType;
          }
          this.flags = this.flags | RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType;
        }
        return this._returnType;
      }
    }

    internal RemotingMethodCachedData(RuntimeMethodInfo ri)
    {
      this.RI = (MethodBase) ri;
    }

    internal RemotingMethodCachedData(RuntimeConstructorInfo ri)
    {
      this.RI = (MethodBase) ri;
    }

    internal override SoapAttribute GetSoapAttributeNoLock()
    {
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (SoapMethodAttribute), true);
      SoapAttribute soapAttribute = customAttributes == null || customAttributes.Length == 0 ? (SoapAttribute) new SoapMethodAttribute() : (SoapAttribute) customAttributes[0];
      soapAttribute.SetReflectInfo((object) this.RI);
      return soapAttribute;
    }

    [SecurityCritical]
    private void UpdateNames()
    {
      MethodBase methodBase = this.RI;
      this._methodName = methodBase.Name;
      if (!(methodBase.DeclaringType != (Type) null))
        return;
      this._typeAndAssemblyName = RemotingServices.GetDefaultQualifiedTypeName((RuntimeType) methodBase.DeclaringType);
    }

    private void GetArgMaps()
    {
      lock (this)
      {
        if (this._inRefArgMap != null)
          return;
        int[] local_2 = (int[]) null;
        int[] local_3 = (int[]) null;
        int[] local_4 = (int[]) null;
        int[] local_5 = (int[]) null;
        int[] local_6 = (int[]) null;
        int[] local_7 = (int[]) null;
        ArgMapper.GetParameterMaps(this.Parameters, out local_2, out local_3, out local_4, out local_5, out local_6, out local_7);
        this._inRefArgMap = local_2;
        this._outRefArgMap = local_3;
        this._outOnlyArgMap = local_4;
        this._nonRefOutArgMap = local_5;
        this._marshalRequestMap = local_6;
        this._marshalResponseMap = local_7;
      }
    }

    internal bool IsOneWayMethod()
    {
      if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay) != RemotingMethodCachedData.MethodCacheFlags.None)
        return (uint) (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > 0U;
      RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay;
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (OneWayAttribute), true);
      if (customAttributes != null && customAttributes.Length != 0)
        methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOneWay;
      this.flags = this.flags | methodCacheFlags;
      return (uint) (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > 0U;
    }

    internal bool IsOverloaded()
    {
      if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded) == RemotingMethodCachedData.MethodCacheFlags.None)
      {
        RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded;
        MethodBase methodBase = this.RI;
        RuntimeMethodInfo runtimeMethodInfo;
        if ((MethodInfo) (runtimeMethodInfo = methodBase as RuntimeMethodInfo) != (MethodInfo) null)
        {
          if (runtimeMethodInfo.IsOverloaded)
            methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
        }
        else
        {
          RuntimeConstructorInfo runtimeConstructorInfo;
          if (!((ConstructorInfo) (runtimeConstructorInfo = methodBase as RuntimeConstructorInfo) != (ConstructorInfo) null))
            throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_Method"));
          if (runtimeConstructorInfo.IsOverloaded)
            methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
        }
        this.flags = this.flags | methodCacheFlags;
      }
      return (uint) (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) > 0U;
    }

    [Flags]
    [Serializable]
    private enum MethodCacheFlags
    {
      None = 0,
      CheckedOneWay = 1,
      IsOneWay = 2,
      CheckedOverloaded = 4,
      IsOverloaded = 8,
      CheckedForAsync = 16,
      CheckedForReturnType = 32,
    }
  }
}
