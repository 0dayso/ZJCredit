// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.WinRTTypeNameConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
  internal static class WinRTTypeNameConverter
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string ConvertToWinRTTypeName(Type managedType, out bool isPrimitive);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Type GetTypeFromWinRTTypeName(string typeName, out bool isPrimitive);
  }
}
