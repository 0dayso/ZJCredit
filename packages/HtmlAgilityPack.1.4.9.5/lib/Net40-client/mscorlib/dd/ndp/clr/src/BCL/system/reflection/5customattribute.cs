// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeCtorParameter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  internal struct CustomAttributeCtorParameter
  {
    private CustomAttributeType m_type;
    private CustomAttributeEncodedArgument m_encodedArgument;

    public CustomAttributeEncodedArgument CustomAttributeEncodedArgument
    {
      get
      {
        return this.m_encodedArgument;
      }
    }

    public CustomAttributeCtorParameter(CustomAttributeType type)
    {
      this.m_type = type;
      this.m_encodedArgument = new CustomAttributeEncodedArgument();
    }
  }
}
