// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeNamedParameter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  internal struct CustomAttributeNamedParameter
  {
    private string m_argumentName;
    private CustomAttributeEncoding m_fieldOrProperty;
    private CustomAttributeEncoding m_padding;
    private CustomAttributeType m_type;
    private CustomAttributeEncodedArgument m_encodedArgument;

    public CustomAttributeEncodedArgument EncodedArgument
    {
      get
      {
        return this.m_encodedArgument;
      }
    }

    public CustomAttributeNamedParameter(string argumentName, CustomAttributeEncoding fieldOrProperty, CustomAttributeType type)
    {
      if (argumentName == null)
        throw new ArgumentNullException("argumentName");
      this.m_argumentName = argumentName;
      this.m_fieldOrProperty = fieldOrProperty;
      this.m_padding = fieldOrProperty;
      this.m_type = type;
      this.m_encodedArgument = new CustomAttributeEncodedArgument();
    }
  }
}
