// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IHashElementEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("9D46FB70-7B54-4f4f-9331-BA9E87833FF5")]
  [ComImport]
  internal interface IHashElementEntry
  {
    HashElementEntry AllData { [SecurityCritical] get; }

    uint index { [SecurityCritical] get; }

    byte Transform { [SecurityCritical] get; }

    object TransformMetadata { [SecurityCritical] get; }

    byte DigestMethod { [SecurityCritical] get; }

    object DigestValue { [SecurityCritical] get; }

    string Xml { [SecurityCritical] get; }
  }
}
