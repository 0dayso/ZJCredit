// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.RemotingTypeCachedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  internal class RemotingTypeCachedData : RemotingCachedData
  {
    private RuntimeType RI;
    private RemotingTypeCachedData.LastCalledMethodClass _lastMethodCalled;
    private System.Runtime.Remoting.TypeInfo _typeInfo;
    private string _qualifiedTypeName;
    private string _assemblyName;
    private string _simpleAssemblyName;

    internal System.Runtime.Remoting.TypeInfo TypeInfo
    {
      [SecurityCritical] get
      {
        if (this._typeInfo == null)
          this._typeInfo = new System.Runtime.Remoting.TypeInfo(this.RI);
        return this._typeInfo;
      }
    }

    internal string QualifiedTypeName
    {
      [SecurityCritical] get
      {
        if (this._qualifiedTypeName == null)
          this._qualifiedTypeName = RemotingServices.DetermineDefaultQualifiedTypeName((Type) this.RI);
        return this._qualifiedTypeName;
      }
    }

    internal string AssemblyName
    {
      get
      {
        if (this._assemblyName == null)
          this._assemblyName = this.RI.Module.Assembly.FullName;
        return this._assemblyName;
      }
    }

    internal string SimpleAssemblyName
    {
      [SecurityCritical] get
      {
        if (this._simpleAssemblyName == null)
          this._simpleAssemblyName = this.RI.GetRuntimeAssembly().GetSimpleName();
        return this._simpleAssemblyName;
      }
    }

    internal RemotingTypeCachedData(RuntimeType ri)
    {
      this.RI = ri;
    }

    internal override SoapAttribute GetSoapAttributeNoLock()
    {
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (SoapTypeAttribute), true);
      SoapAttribute soapAttribute = customAttributes == null || customAttributes.Length == 0 ? (SoapAttribute) new SoapTypeAttribute() : (SoapAttribute) customAttributes[0];
      soapAttribute.SetReflectInfo((object) this.RI);
      return soapAttribute;
    }

    internal MethodBase GetLastCalledMethod(string newMeth)
    {
      RemotingTypeCachedData.LastCalledMethodClass calledMethodClass = this._lastMethodCalled;
      if (calledMethodClass == null)
        return (MethodBase) null;
      string str = calledMethodClass.methodName;
      MethodBase methodBase = calledMethodClass.MB;
      if (methodBase == (MethodBase) null || str == null)
        return (MethodBase) null;
      if (str.Equals(newMeth))
        return methodBase;
      return (MethodBase) null;
    }

    internal void SetLastCalledMethod(string newMethName, MethodBase newMB)
    {
      this._lastMethodCalled = new RemotingTypeCachedData.LastCalledMethodClass()
      {
        methodName = newMethName,
        MB = newMB
      };
    }

    private class LastCalledMethodClass
    {
      public string methodName;
      public MethodBase MB;
    }
  }
}
