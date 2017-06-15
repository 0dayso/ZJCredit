// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.SerObjectInfoCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class SerObjectInfoCache
  {
    internal string fullTypeName;
    internal string assemblyString;
    internal bool hasTypeForwardedFrom;
    internal MemberInfo[] memberInfos;
    internal string[] memberNames;
    internal Type[] memberTypes;

    internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
    {
      this.fullTypeName = typeName;
      this.assemblyString = assemblyName;
      this.hasTypeForwardedFrom = hasTypeForwardedFrom;
    }

    internal SerObjectInfoCache(Type type)
    {
      TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
      this.fullTypeName = typeInformation.FullTypeName;
      this.assemblyString = typeInformation.AssemblyString;
      this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
    }
  }
}
