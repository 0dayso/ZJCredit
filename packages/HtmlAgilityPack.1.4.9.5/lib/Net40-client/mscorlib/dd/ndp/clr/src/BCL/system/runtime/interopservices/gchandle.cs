// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GCHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
  /// <summary>提供用于从非托管内存访问托管对象的方法。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public struct GCHandle
  {
    private static volatile bool s_probeIsActive = Mda.IsInvalidGCHandleCookieProbeEnabled();
    private const GCHandleType MaxHandleType = GCHandleType.Pinned;
    private IntPtr m_handle;
    private static volatile GCHandleCookieTable s_cookieTable;

    /// <summary>获取或设置该句柄表示的对象。</summary>
    /// <returns>该句柄表示的对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">The handle was freed, or never initialized. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public object Target
    {
      [SecurityCritical, __DynamicallyInvokable] get
      {
        if (this.m_handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        return GCHandle.InternalGet(this.GetHandleValue());
      }
      [SecurityCritical, __DynamicallyInvokable] set
      {
        if (this.m_handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        GCHandle.InternalSet(this.GetHandleValue(), value, this.IsPinned());
      }
    }

    /// <summary>获取一个值，该值指示是否分配了句柄。</summary>
    /// <returns>如果分配了句柄，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsAllocated
    {
      [__DynamicallyInvokable] get
      {
        return this.m_handle != IntPtr.Zero;
      }
    }

    [SecuritySafeCritical]
    static GCHandle()
    {
      if (!GCHandle.s_probeIsActive)
        return;
      GCHandle.s_cookieTable = new GCHandleCookieTable();
    }

    [SecurityCritical]
    internal GCHandle(object value, GCHandleType type)
    {
      if ((uint) type > 3U)
        throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      this.m_handle = GCHandle.InternalAlloc(value, type);
      if (type != GCHandleType.Pinned)
        return;
      this.SetIsPinned();
    }

    [SecurityCritical]
    internal GCHandle(IntPtr handle)
    {
      GCHandle.InternalCheckDomain(handle);
      this.m_handle = handle;
    }

    /// <summary>
    /// <see cref="T:System.Runtime.InteropServices.GCHandle" /> 以内部整数表示形式存储。</summary>
    /// <returns>使用内部整数表示形式的已存储 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</returns>
    /// <param name="value">一个 <see cref="T:System.IntPtr" />，它指示需要该转换的句柄。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static explicit operator GCHandle(IntPtr value)
    {
      return GCHandle.FromIntPtr(value);
    }

    /// <summary>
    /// <see cref="T:System.Runtime.InteropServices.GCHandle" /> 以内部整数表示形式存储。</summary>
    /// <returns>整数值。</returns>
    /// <param name="value">需要该整数的 <see cref="T:System.Runtime.InteropServices.GCHandle" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static explicit operator IntPtr(GCHandle value)
    {
      return GCHandle.ToIntPtr(value);
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="a" /> 和 <paramref name="b" /> 参数相等，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</param>
    /// <param name="b">要与 <paramref name="a" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。  </param>
    [__DynamicallyInvokable]
    public static bool operator ==(GCHandle a, GCHandle b)
    {
      return a.m_handle == b.m_handle;
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 和 <paramref name="b" /> 参数不相等，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</param>
    /// <param name="b">要与 <paramref name="a" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。  </param>
    [__DynamicallyInvokable]
    public static bool operator !=(GCHandle a, GCHandle b)
    {
      return a.m_handle != b.m_handle;
    }

    /// <summary>为指定的对象分配 <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" /> 句柄。</summary>
    /// <returns>一个新的 <see cref="T:System.Runtime.InteropServices.GCHandle" />，它保护对象不被垃圾回收。当不再需要 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 时，必须通过 <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> 将其释放。</returns>
    /// <param name="value">使用 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 的对象。</param>
    /// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static GCHandle Alloc(object value)
    {
      return new GCHandle(value, GCHandleType.Normal);
    }

    /// <summary>为指定的对象分配指定类型的句柄。</summary>
    /// <returns>指定的类型的新 <see cref="T:System.Runtime.InteropServices.GCHandle" />。当不再需要 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 时，必须通过 <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> 将其释放。</returns>
    /// <param name="value">使用 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 的对象。</param>
    /// <param name="type">
    /// <see cref="T:System.Runtime.InteropServices.GCHandleType" /> 值之一，指示要创建的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 的类型。</param>
    /// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static GCHandle Alloc(object value, GCHandleType type)
    {
      return new GCHandle(value, type);
    }

    /// <summary>释放 <see cref="T:System.Runtime.InteropServices.GCHandle" />。</summary>
    /// <exception cref="T:System.InvalidOperationException">The handle was freed or never initialized. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void Free()
    {
      IntPtr num = this.m_handle;
      if (!(num != IntPtr.Zero) || !(Interlocked.CompareExchange(ref this.m_handle, IntPtr.Zero, num) == num))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
      if (GCHandle.s_probeIsActive)
        GCHandle.s_cookieTable.RemoveHandleIfPresent(num);
      GCHandle.InternalFree((IntPtr) ((int) num & -2));
    }

    /// <summary>在 <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" /> 句柄中检索对象的地址。</summary>
    /// <returns>
    /// <see cref="T:System.IntPtr" /> 形式的固定对象的地址。</returns>
    /// <exception cref="T:System.InvalidOperationException">The handle is any type other than <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" />. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public IntPtr AddrOfPinnedObject()
    {
      if (this.IsPinned())
        return GCHandle.InternalAddrOfPinnedObject(this.GetHandleValue());
      if (this.m_handle == IntPtr.Zero)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotPinned"));
    }

    /// <summary>返回从某个托管对象的句柄创建的新 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</summary>
    /// <returns>对应于值参数的新的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</returns>
    /// <param name="value">某个托管对象的 <see cref="T:System.IntPtr" /> 句柄，将从该句柄创建 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</param>
    /// <exception cref="T:System.InvalidOperationException">The value of the <paramref name="value" /> parameter is <see cref="F:System.IntPtr.Zero" />.</exception>
    [SecurityCritical]
    public static GCHandle FromIntPtr(IntPtr value)
    {
      if (value == IntPtr.Zero)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
      IntPtr handle = value;
      if (GCHandle.s_probeIsActive)
      {
        handle = GCHandle.s_cookieTable.GetHandle(value);
        if (IntPtr.Zero == handle)
        {
          Mda.FireInvalidGCHandleCookieProbe(value);
          return new GCHandle(IntPtr.Zero);
        }
      }
      return new GCHandle(handle);
    }

    /// <summary>返回 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象的内部整数表示形式。</summary>
    /// <returns>表示 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象的 <see cref="T:System.IntPtr" /> 对象。 </returns>
    /// <param name="value">要从其中检索内部整数表示形式的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</param>
    public static IntPtr ToIntPtr(GCHandle value)
    {
      if (GCHandle.s_probeIsActive)
        return GCHandle.s_cookieTable.FindOrAddHandle(value.m_handle);
      return value.m_handle;
    }

    /// <summary>返回当前 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象的一个标识符。</summary>
    /// <returns>当前 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象的一个标识符。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_handle.GetHashCode();
    }

    /// <summary>确定指定的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象是否等于当前的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</summary>
    /// <returns>如果指定的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象等于当前的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="o">将与当前 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象进行比较的 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      if (o == null || !(o is GCHandle))
        return false;
      return this.m_handle == ((GCHandle) o).m_handle;
    }

    internal IntPtr GetHandleValue()
    {
      return new IntPtr((int) this.m_handle & -2);
    }

    internal bool IsPinned()
    {
      return (uint) ((int) this.m_handle & 1) > 0U;
    }

    internal void SetIsPinned()
    {
      this.m_handle = new IntPtr((int) this.m_handle | 1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr InternalAlloc(object value, GCHandleType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalFree(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InternalGet(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalSet(IntPtr handle, object value, bool isPinned);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InternalCompareExchange(IntPtr handle, object value, object oldValue, bool isPinned);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr InternalAddrOfPinnedObject(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalCheckDomain(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern GCHandleType InternalGetHandleType(IntPtr handle);
  }
}
