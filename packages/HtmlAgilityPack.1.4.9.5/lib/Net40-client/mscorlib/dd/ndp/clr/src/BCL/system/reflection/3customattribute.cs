// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeEncodedArgument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  internal struct CustomAttributeEncodedArgument
  {
    private long m_primitiveValue;
    private CustomAttributeEncodedArgument[] m_arrayValue;
    private string m_stringValue;
    private CustomAttributeType m_type;

    public CustomAttributeType CustomAttributeType
    {
      get
      {
        return this.m_type;
      }
    }

    public long PrimitiveValue
    {
      get
      {
        return this.m_primitiveValue;
      }
    }

    public CustomAttributeEncodedArgument[] ArrayValue
    {
      get
      {
        return this.m_arrayValue;
      }
    }

    public string StringValue
    {
      get
      {
        return this.m_stringValue;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ParseAttributeArguments(IntPtr pCa, int cCa, ref CustomAttributeCtorParameter[] CustomAttributeCtorParameters, ref CustomAttributeNamedParameter[] CustomAttributeTypedArgument, RuntimeAssembly assembly);

    [SecurityCritical]
    internal static void ParseAttributeArguments(ConstArray attributeBlob, ref CustomAttributeCtorParameter[] customAttributeCtorParameters, ref CustomAttributeNamedParameter[] customAttributeNamedParameters, RuntimeModule customAttributeModule)
    {
      if ((Module) customAttributeModule == (Module) null)
        throw new ArgumentNullException("customAttributeModule");
      if (customAttributeCtorParameters.Length == 0 && customAttributeNamedParameters.Length == 0)
        return;
      CustomAttributeEncodedArgument.ParseAttributeArguments(attributeBlob.Signature, attributeBlob.Length, ref customAttributeCtorParameters, ref customAttributeNamedParameters, (RuntimeAssembly) customAttributeModule.Assembly);
    }
  }
}
