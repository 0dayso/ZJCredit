// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryAssemblyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryAssemblyInfo
  {
    internal string assemblyString;
    private Assembly assembly;

    internal BinaryAssemblyInfo(string assemblyString)
    {
      this.assemblyString = assemblyString;
    }

    internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
    {
      this.assemblyString = assemblyString;
      this.assembly = assembly;
    }

    internal Assembly GetAssembly()
    {
      if (this.assembly == (Assembly) null)
      {
        this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
        if (this.assembly == (Assembly) null)
          throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyNotFound", (object) this.assemblyString));
      }
      return this.assembly;
    }
  }
}
