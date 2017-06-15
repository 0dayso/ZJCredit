// Decompiled with JetBrains decompiler
// Type: System.SizedReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
  internal class SizedReference : IDisposable
  {
    internal volatile IntPtr _handle;

    public object Target
    {
      [SecuritySafeCritical] get
      {
        IntPtr h = this._handle;
        if (h == IntPtr.Zero)
          return (object) null;
        object targetOfSizedRef = SizedReference.GetTargetOfSizedRef(h);
        if (!(this._handle == IntPtr.Zero))
          return targetOfSizedRef;
        return (object) null;
      }
    }

    public long ApproximateSize
    {
      [SecuritySafeCritical] get
      {
        IntPtr h = this._handle;
        IntPtr num = IntPtr.Zero;
        if (h == num)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        long approximateSizeOfSizedRef = SizedReference.GetApproximateSizeOfSizedRef(h);
        if (this._handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        return approximateSizeOfSizedRef;
      }
    }

    [SecuritySafeCritical]
    public SizedReference(object target)
    {
      IntPtr num = IntPtr.Zero;
      this._handle = SizedReference.CreateSizedRef(target);
    }

    ~SizedReference()
    {
      this.Free();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr CreateSizedRef(object o);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FreeSizedRef(IntPtr h);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object GetTargetOfSizedRef(IntPtr h);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern long GetApproximateSizeOfSizedRef(IntPtr h);

    [SecuritySafeCritical]
    private void Free()
    {
      IntPtr num = this._handle;
      if (!(num != IntPtr.Zero) || !(Interlocked.CompareExchange(ref this._handle, IntPtr.Zero, num) == num))
        return;
      SizedReference.FreeSizedRef(num);
    }

    public void Dispose()
    {
      this.Free();
      GC.SuppressFinalize((object) this);
    }
  }
}
