// Decompiled with JetBrains decompiler
// Type: System.CLRConfig
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  [FriendAccessAllowed]
  internal class CLRConfig
  {
    [FriendAccessAllowed]
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CheckLegacyManagedDeflateStream();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CheckThrowUnobservedTaskExceptions();
  }
}
