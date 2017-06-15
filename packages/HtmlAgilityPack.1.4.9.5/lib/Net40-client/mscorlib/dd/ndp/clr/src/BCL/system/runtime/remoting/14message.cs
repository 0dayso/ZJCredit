// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.SerializationMonkey
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class SerializationMonkey : ISerializable, IFieldInfo
  {
    internal ISerializationRootObject _obj;
    internal string[] fieldNames;
    internal Type[] fieldTypes;

    public string[] FieldNames
    {
      [SecurityCritical] get
      {
        return this.fieldNames;
      }
      [SecurityCritical] set
      {
        this.fieldNames = value;
      }
    }

    public Type[] FieldTypes
    {
      [SecurityCritical] get
      {
        return this.fieldTypes;
      }
      [SecurityCritical] set
      {
        this.fieldTypes = value;
      }
    }

    [SecurityCritical]
    internal SerializationMonkey(SerializationInfo info, StreamingContext ctx)
    {
      this._obj.RootSetObjectData(info, ctx);
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }
  }
}
