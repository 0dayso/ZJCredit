// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationPinDeployment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationPinDeployment
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationPinDeployment.OpFlags Flags;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionAppId Application;
    [MarshalAs(UnmanagedType.I8)]
    public long ExpirationTime;
    public IntPtr Reference;

    [SecuritySafeCritical]
    public StoreOperationPinDeployment(IDefinitionAppId AppId, StoreApplicationReference Ref)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationPinDeployment));
      this.Flags = StoreOperationPinDeployment.OpFlags.NeverExpires;
      this.Application = AppId;
      this.Reference = Ref.ToIntPtr();
      this.ExpirationTime = 0L;
    }

    public StoreOperationPinDeployment(IDefinitionAppId AppId, DateTime Expiry, StoreApplicationReference Ref)
    {
      this = new StoreOperationPinDeployment(AppId, Ref);
      this.Flags = this.Flags | StoreOperationPinDeployment.OpFlags.NeverExpires;
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
      NeverExpires = 1,
    }

    public enum Disposition
    {
      Failed,
      Pinned,
    }
  }
}
