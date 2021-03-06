﻿// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.SystemTypeMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class SystemTypeMarshaler
  {
    [SecurityCritical]
    internal static unsafe void ConvertToNative(Type managedType, TypeNameNative* pNativeType)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      string str;
      if (managedType != (Type) null)
      {
        if (managedType.GetType() != typeof (RuntimeType))
          throw new ArgumentException(Environment.GetResourceString("Argument_WinRTSystemRuntimeType", (object) managedType.GetType().ToString()));
        bool isPrimitive;
        string winRtTypeName = WinRTTypeNameConverter.ConvertToWinRTTypeName(managedType, out isPrimitive);
        if (winRtTypeName != null)
        {
          str = winRtTypeName;
          pNativeType->typeKind = !isPrimitive ? TypeKind.Metadata : TypeKind.Primitive;
        }
        else
        {
          str = managedType.AssemblyQualifiedName;
          pNativeType->typeKind = TypeKind.Projection;
        }
      }
      else
      {
        str = "";
        pNativeType->typeKind = TypeKind.Projection;
      }
      string sourceString = str;
      int length = sourceString.Length;
      IntPtr num = (IntPtr) &pNativeType->typeName;
      Marshal.ThrowExceptionForHR(UnsafeNativeMethods.WindowsCreateString(sourceString, length, (IntPtr*) num), new IntPtr(-1));
    }

    [SecurityCritical]
    internal static unsafe void ConvertToManaged(TypeNameNative* pNativeType, ref Type managedType)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      string @string = WindowsRuntimeMarshal.HStringToString(pNativeType->typeName);
      if (string.IsNullOrEmpty(@string))
        managedType = (Type) null;
      else if (pNativeType->typeKind == TypeKind.Projection)
      {
        managedType = Type.GetType(@string, true);
      }
      else
      {
        bool isPrimitive;
        managedType = WinRTTypeNameConverter.GetTypeFromWinRTTypeName(@string, out isPrimitive);
        if (isPrimitive != (pNativeType->typeKind == TypeKind.Primitive))
          throw new ArgumentException(Environment.GetResourceString("Argument_Unexpected_TypeSource"));
      }
    }

    [SecurityCritical]
    internal static unsafe void ClearNative(TypeNameNative* pNativeType)
    {
      TypeNameNative typeNameNative = *pNativeType;
      UnsafeNativeMethods.WindowsDeleteString(pNativeType->typeName);
    }
  }
}
