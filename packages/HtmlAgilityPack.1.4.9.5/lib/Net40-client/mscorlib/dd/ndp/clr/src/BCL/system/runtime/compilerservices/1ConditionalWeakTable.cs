// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DependentHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
  [ComVisible(false)]
  internal struct DependentHandle
  {
    private IntPtr _handle;

    public bool IsAllocated
    {
      get
      {
        return this._handle != (IntPtr) 0;
      }
    }

    [SecurityCritical]
    public DependentHandle(object primary, object secondary)
    {
      IntPtr dependentHandle = (IntPtr) 0;
      DependentHandle.nInitialize(primary, secondary, out dependentHandle);
      this._handle = dependentHandle;
    }

    [SecurityCritical]
    public object GetPrimary()
    {
      object primary;
      DependentHandle.nGetPrimary(this._handle, out primary);
      return primary;
    }

    [SecurityCritical]
    public void GetPrimaryAndSecondary(out object primary, out object secondary)
    {
      DependentHandle.nGetPrimaryAndSecondary(this._handle, out primary, out secondary);
    }

    [SecurityCritical]
    public void Free()
    {
      if (!(this._handle != (IntPtr) 0))
        return;
      IntPtr dependentHandle = this._handle;
      this._handle = (IntPtr) 0;
      DependentHandle.nFree(dependentHandle);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nInitialize(object primary, object secondary, out IntPtr dependentHandle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nGetPrimary(IntPtr dependentHandle, out object primary);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nGetPrimaryAndSecondary(IntPtr dependentHandle, out object primary, out object secondary);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nFree(IntPtr dependentHandle);
  }
}
