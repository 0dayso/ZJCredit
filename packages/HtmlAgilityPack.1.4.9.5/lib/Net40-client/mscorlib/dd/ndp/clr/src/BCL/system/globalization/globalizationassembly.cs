// Decompiled with JetBrains decompiler
// Type: System.Globalization.GlobalizationAssembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Security;

namespace System.Globalization
{
  internal sealed class GlobalizationAssembly
  {
    [SecurityCritical]
    internal static unsafe byte* GetGlobalizationResourceBytePtr(Assembly assembly, string tableName)
    {
      UnmanagedMemoryStream unmanagedMemoryStream = assembly.GetManifestResourceStream(tableName) as UnmanagedMemoryStream;
      if (unmanagedMemoryStream != null)
      {
        byte* positionPointer = unmanagedMemoryStream.PositionPointer;
        if ((IntPtr) positionPointer != IntPtr.Zero)
          return positionPointer;
      }
      throw new InvalidOperationException();
    }
  }
}
