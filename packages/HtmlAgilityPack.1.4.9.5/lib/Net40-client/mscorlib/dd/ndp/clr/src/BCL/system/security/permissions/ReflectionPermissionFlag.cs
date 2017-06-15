// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ReflectionPermissionFlag
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定 <see cref="N:System.Reflection" /> 和 <see cref="N:System.Reflection.Emit" /> 命名空间的允许用法。</summary>
  [ComVisible(true)]
  [Flags]
  [Serializable]
  public enum ReflectionPermissionFlag
  {
    NoFlags = 0,
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] TypeInformation = 1,
    MemberAccess = 2,
    [Obsolete("This permission is no longer used by the CLR.")] ReflectionEmit = 4,
    [ComVisible(false)] RestrictedMemberAccess = 8,
    [Obsolete("This permission has been deprecated. Use PermissionState.Unrestricted to get full access.")] AllFlags = ReflectionEmit | MemberAccess | TypeInformation,
  }
}
