﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.RemotingFieldCachedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Metadata
{
  internal class RemotingFieldCachedData : RemotingCachedData
  {
    private FieldInfo RI;

    internal RemotingFieldCachedData(RuntimeFieldInfo ri)
    {
      this.RI = (FieldInfo) ri;
    }

    internal RemotingFieldCachedData(SerializationFieldInfo ri)
    {
      this.RI = (FieldInfo) ri;
    }

    internal override SoapAttribute GetSoapAttributeNoLock()
    {
      object[] customAttributes = this.RI.GetCustomAttributes(typeof (SoapFieldAttribute), false);
      SoapAttribute soapAttribute = customAttributes == null || customAttributes.Length == 0 ? (SoapAttribute) new SoapFieldAttribute() : (SoapAttribute) customAttributes[0];
      soapAttribute.SetReflectInfo((object) this.RI);
      return soapAttribute;
    }
  }
}
