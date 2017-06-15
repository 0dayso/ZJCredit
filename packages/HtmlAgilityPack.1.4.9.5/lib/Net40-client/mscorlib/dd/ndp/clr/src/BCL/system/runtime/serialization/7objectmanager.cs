// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.TypeLoadExceptionHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  internal class TypeLoadExceptionHolder
  {
    private string m_typeName;

    internal string TypeName
    {
      get
      {
        return this.m_typeName;
      }
    }

    internal TypeLoadExceptionHolder(string typeName)
    {
      this.m_typeName = typeName;
    }
  }
}
