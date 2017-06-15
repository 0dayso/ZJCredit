// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationUnpinDeployment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationUnpinDeployment
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationUnpinDeployment.OpFlags Flags;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionAppId Application;
    public IntPtr Reference;

    [SecuritySafeCritical]
    public StoreOperationUnpinDeployment(IDefinitionAppId app, StoreApplicationReference reference)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationUnpinDeployment));
      this.Flags = StoreOperationUnpinDeployment.OpFlags.Nothing;
      this.Application = app;
      this.Reference = reference.ToIntPtr();
    }

    [SecurityCritical]
    public void Destroy()
    {
      StoreApplicationReference.Destroy(this.Reference);
    }

    [System.Flags]
    public enum OpFlags
    {
      Nothing = 0,
    }

    public enum Disposition
    {
      Failed,
      Unpinned,
    }
  }
}
