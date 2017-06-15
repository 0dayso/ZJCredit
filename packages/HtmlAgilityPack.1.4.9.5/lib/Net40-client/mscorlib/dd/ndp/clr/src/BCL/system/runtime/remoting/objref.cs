// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.TypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting
{
  [Serializable]
  internal class TypeInfo : IRemotingTypeInfo
  {
    private string serverType;
    private string[] serverHierarchy;
    private string[] interfacesImplemented;

    public virtual string TypeName
    {
      [SecurityCritical] get
      {
        return this.serverType;
      }
      [SecurityCritical] set
      {
        this.serverType = value;
      }
    }

    internal string ServerType
    {
      get
      {
        return this.serverType;
      }
      set
      {
        this.serverType = value;
      }
    }

    private string[] ServerHierarchy
    {
      get
      {
        return this.serverHierarchy;
      }
      set
      {
        this.serverHierarchy = value;
      }
    }

    private string[] InterfacesImplemented
    {
      get
      {
        return this.interfacesImplemented;
      }
      set
      {
        this.interfacesImplemented = value;
      }
    }

    [SecurityCritical]
    internal TypeInfo(RuntimeType typeOfObj)
    {
      this.ServerType = TypeInfo.GetQualifiedTypeName(typeOfObj);
      RuntimeType runtimeType = (RuntimeType) typeOfObj.BaseType;
      int length = 0;
      while ((Type) runtimeType != typeof (MarshalByRefObject) && runtimeType != (RuntimeType) null)
      {
        runtimeType = (RuntimeType) runtimeType.BaseType;
        ++length;
      }
      string[] strArray1 = (string[]) null;
      if (length > 0)
      {
        strArray1 = new string[length];
        RuntimeType type = (RuntimeType) typeOfObj.BaseType;
        for (int index = 0; index < length; ++index)
        {
          strArray1[index] = TypeInfo.GetQualifiedTypeName(type);
          type = (RuntimeType) type.BaseType;
        }
      }
      this.ServerHierarchy = strArray1;
      Type[] interfaces = typeOfObj.GetInterfaces();
      string[] strArray2 = (string[]) null;
      bool isInterface = typeOfObj.IsInterface;
      if ((uint) interfaces.Length > 0U | isInterface)
      {
        strArray2 = new string[interfaces.Length + (isInterface ? 1 : 0)];
        for (int index = 0; index < interfaces.Length; ++index)
          strArray2[index] = TypeInfo.GetQualifiedTypeName((RuntimeType) interfaces[index]);
        if (isInterface)
        {
          string[] strArray3 = strArray2;
          int index = strArray3.Length - 1;
          string qualifiedTypeName = TypeInfo.GetQualifiedTypeName(typeOfObj);
          strArray3[index] = qualifiedTypeName;
        }
      }
      this.InterfacesImplemented = strArray2;
    }

    [SecurityCritical]
    public virtual bool CanCastTo(Type castType, object o)
    {
      if ((Type) null != castType)
      {
        if (castType == typeof (MarshalByRefObject) || castType == typeof (object))
          return true;
        if (castType.IsInterface)
        {
          if (this.interfacesImplemented != null)
            return this.CanCastTo(castType, this.InterfacesImplemented);
          return false;
        }
        if (castType.IsMarshalByRef && (this.CompareTypes(castType, this.serverType) || this.serverHierarchy != null && this.CanCastTo(castType, this.ServerHierarchy)))
          return true;
      }
      return false;
    }

    [SecurityCritical]
    internal static string GetQualifiedTypeName(RuntimeType type)
    {
      if (type == (RuntimeType) null)
        return (string) null;
      return RemotingServices.GetDefaultQualifiedTypeName(type);
    }

    internal static bool ParseTypeAndAssembly(string typeAndAssembly, out string typeName, out string assemName)
    {
      if (typeAndAssembly == null)
      {
        typeName = (string) null;
        assemName = (string) null;
        return false;
      }
      int length = typeAndAssembly.IndexOf(',');
      if (length == -1)
      {
        typeName = typeAndAssembly;
        assemName = (string) null;
        return true;
      }
      typeName = typeAndAssembly.Substring(0, length);
      assemName = typeAndAssembly.Substring(length + 1).Trim();
      return true;
    }

    [SecurityCritical]
    private bool CompareTypes(Type type1, string type2)
    {
      Type qualifiedTypeName = RemotingServices.InternalGetTypeFromQualifiedTypeName(type2);
      return type1 == qualifiedTypeName;
    }

    [SecurityCritical]
    private bool CanCastTo(Type castType, string[] types)
    {
      bool flag = false;
      if ((Type) null != castType)
      {
        for (int index = 0; index < types.Length; ++index)
        {
          if (this.CompareTypes(castType, types[index]))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }
  }
}
