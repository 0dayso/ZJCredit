// Decompiled with JetBrains decompiler
// Type: System.RuntimeMethodHandleInternal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  internal struct RuntimeMethodHandleInternal
  {
    internal IntPtr m_handle;

    internal static RuntimeMethodHandleInternal EmptyHandle
    {
      get
      {
        return new RuntimeMethodHandleInternal();
      }
    }

    internal IntPtr Value
    {
      [SecurityCritical] get
      {
        return this.m_handle;
      }
    }

    [SecurityCritical]
    internal RuntimeMethodHandleInternal(IntPtr value)
    {
      this.m_handle = value;
    }

    internal bool IsNullHandle()
    {
      return this.m_handle.IsNull();
    }
  }
}
