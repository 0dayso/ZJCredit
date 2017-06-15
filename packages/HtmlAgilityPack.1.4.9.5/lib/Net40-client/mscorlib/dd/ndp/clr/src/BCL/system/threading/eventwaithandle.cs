// Decompiled with JetBrains decompiler
// Type: System.Threading.EventWaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>表示一个线程同步事件。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class EventWaitHandle : WaitHandle
  {
    /// <summary>初始化 <see cref="T:System.Threading.EventWaitHandle" /> 类的新实例，并指定等待句柄最初是否处于终止状态，以及它是自动重置还是手动重置。</summary>
    /// <param name="initialState">如果为 true，则将初始状态设置为终止；如果为 false，则将初始状态设置为非终止。</param>
    /// <param name="mode">
    /// <see cref="T:System.Threading.EventResetMode" /> 值之一，它确定事件是自动重置还是手动重置。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public EventWaitHandle(bool initialState, EventResetMode mode)
      : this(initialState, mode, (string) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Threading.EventWaitHandle" /> 类的新实例，并指定在此调用后创建的等待句柄最初是否处于终止状态，它是自动重置还是手动重置，以及系统同步事件的名称。</summary>
    /// <param name="initialState">如果命名事件是通过此调用创建的，则 true 将初始状态设置为终止；false 将初始状态设置为非终止。</param>
    /// <param name="mode">
    /// <see cref="T:System.Threading.EventResetMode" /> 值之一，它确定事件是自动重置还是手动重置。</param>
    /// <param name="name">系统范围内同步事件的名称。</param>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">命名事件存在并具有访问控制安全性，但用户不具有 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">无法创建命名事件，原因可能是与另一个不同类型的等待句柄同名。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度超过 260 个字符。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public EventWaitHandle(bool initialState, EventResetMode mode, string name)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      SafeWaitHandle @event;
      if (mode != EventResetMode.AutoReset)
      {
        if (mode == EventResetMode.ManualReset)
          @event = Win32Native.CreateEvent((Win32Native.SECURITY_ATTRIBUTES) null, true, initialState, name);
        else
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag", (object) name));
      }
      else
        @event = Win32Native.CreateEvent((Win32Native.SECURITY_ATTRIBUTES) null, false, initialState, name);
      if (@event.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        @event.SetHandleAsInvalid();
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        __Error.WinIOError(lastWin32Error, name);
      }
      this.SetHandleInternal(@event);
    }

    /// <summary>初始化 <see cref="T:System.Threading.EventWaitHandle" /> 类的新实例，并指定在此调用后创建的等待句柄最初是否处于终止状态，它是自动重置还是手动重置，系统同步事件的名称，以及一个 Boolean 变量（其值在调用后表示是否创建了已命名的系统事件）。</summary>
    /// <param name="initialState">如果命名事件是通过此调用创建的，则 true 将初始状态设置为终止；false 将初始状态设置为非终止。</param>
    /// <param name="mode">
    /// <see cref="T:System.Threading.EventResetMode" /> 值之一，它确定事件是自动重置还是手动重置。</param>
    /// <param name="name">系统范围内同步事件的名称。</param>
    /// <param name="createdNew">在此方法返回时，如果创建了本地事件（即，如果 <paramref name="name" /> 为 null 或空字符串）或指定的命名系统事件，则包含 true；如果指定的命名系统事件已存在，则为 false。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">命名事件存在并具有访问控制安全性，但用户不具有 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">无法创建命名事件，原因可能是与另一个不同类型的等待句柄同名。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度超过 260 个字符。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew)
      : this(initialState, mode, name, out createdNew, (EventWaitHandleSecurity) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Threading.EventWaitHandle" /> 类的新实例，并指定在此调用后创建的等待句柄最初是否处于终止状态，它是自动重置还是手动重置，系统同步事件的名称，一个 Boolean 变量（其值在调用后表示是否创建了已命名的系统事件），以及应用于已命名的事件（如果创建了该事件）的访问控制安全性。</summary>
    /// <param name="initialState">如果命名事件是通过此调用创建的，则 true 将初始状态设置为终止；false 将初始状态设置为非终止。</param>
    /// <param name="mode">
    /// <see cref="T:System.Threading.EventResetMode" /> 值之一，它确定事件是自动重置还是手动重置。</param>
    /// <param name="name">系统范围内同步事件的名称。</param>
    /// <param name="createdNew">在此方法返回时，如果创建了本地事件（即，如果 <paramref name="name" /> 为 null 或空字符串）或指定的命名系统事件，则包含 true；如果指定的命名系统事件已存在，则为 false。该参数未经初始化即被传递。</param>
    /// <param name="eventSecurity">一个 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 对象，表示应用于已命名的系统事件的访问控制安全性。</param>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">命名事件存在并具有访问控制安全性，但用户不具有 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">无法创建命名事件，原因可能是与另一个不同类型的等待句柄同名。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度超过 260 个字符。</exception>
    [SecurityCritical]
    public unsafe EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew, EventWaitHandleSecurity eventSecurity)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (eventSecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = eventSecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      bool isManualReset;
      if (mode != EventResetMode.AutoReset)
      {
        if (mode == EventResetMode.ManualReset)
          isManualReset = true;
        else
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag", (object) name));
      }
      else
        isManualReset = false;
      SafeWaitHandle @event = Win32Native.CreateEvent(securityAttributes, isManualReset, initialState, name);
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (@event.IsInvalid)
      {
        @event.SetHandleAsInvalid();
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        __Error.WinIOError(lastWin32Error, name);
      }
      createdNew = lastWin32Error != 183;
      this.SetHandleInternal(@event);
    }

    [SecurityCritical]
    private EventWaitHandle(SafeWaitHandle handle)
    {
      this.SetHandleInternal(handle);
    }

    /// <summary>打开指定名称为同步事件（如果已经存在）。</summary>
    /// <returns>一个对象，表示已命名的系统事件。</returns>
    /// <param name="name">要打开的系统同步事件的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是空字符串。- 或 -<paramref name="name" /> 的长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">命名的系统事件不存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的事件存在，但用户不具备使用它所需的安全访问权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static EventWaitHandle OpenExisting(string name)
    {
      return EventWaitHandle.OpenExisting(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
    }

    /// <summary>用安全访问权限打开指定名称为同步事件（如果已经存在）。</summary>
    /// <returns>一个对象，表示已命名的系统事件。</returns>
    /// <param name="name">要打开的系统同步事件的名称。</param>
    /// <param name="rights">表示所需的安全访问权限的列举值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是空字符串。- 或 -<paramref name="name" /> 的长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">命名的系统事件不存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的事件存在，但用户不具备所需的安全访问权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static EventWaitHandle OpenExisting(string name, EventWaitHandleRights rights)
    {
      EventWaitHandle result;
      switch (EventWaitHandle.OpenExistingWorker(name, rights, out result))
      {
        case WaitHandle.OpenExistingResult.NameNotFound:
          throw new WaitHandleCannotBeOpenedException();
        case WaitHandle.OpenExistingResult.PathNotFound:
          __Error.WinIOError(3, "");
          return result;
        case WaitHandle.OpenExistingResult.NameInvalid:
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        default:
          return result;
      }
    }

    /// <summary>打开指定名称为同步事件（如果已经存在），并返回指示操作是否成功的值。</summary>
    /// <returns>如果命名同步事件成功打开，则为 true；否则为 false。</returns>
    /// <param name="name">要打开的系统同步事件的名称。</param>
    /// <param name="result">当此方法返回时，如果调用成功，则包含表示命名同步事件的 <see cref="T:System.Threading.EventWaitHandle" /> 对象；否则为 null。该参数未经初始化即被处理。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是空字符串。- 或 -<paramref name="name" /> 的长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的事件存在，但用户不具备所需的安全访问权限。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static bool TryOpenExisting(string name, out EventWaitHandle result)
    {
      return EventWaitHandle.OpenExistingWorker(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>用安全访问权限打开指定名称为同步事件（如果已经存在），并返回指示操作是否成功的值。</summary>
    /// <returns>如果命名同步事件成功打开，则为 true；否则为 false。</returns>
    /// <param name="name">要打开的系统同步事件的名称。</param>
    /// <param name="rights">表示所需的安全访问权限的列举值的按位组合。</param>
    /// <param name="result">当此方法返回时，如果调用成功，则包含表示命名同步事件的 <see cref="T:System.Threading.EventWaitHandle" /> 对象；否则为 null。该参数未经初始化即被处理。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是空字符串。- 或 -<paramref name="name" /> 的长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的事件存在，但用户不具备所需的安全访问权限。</exception>
    [SecurityCritical]
    public static bool TryOpenExisting(string name, EventWaitHandleRights rights, out EventWaitHandle result)
    {
      return EventWaitHandle.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
    }

    [SecurityCritical]
    private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, EventWaitHandleRights rights, out EventWaitHandle result)
    {
      if (name == null)
        throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_WithParamName"));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      result = (EventWaitHandle) null;
      SafeWaitHandle handle = Win32Native.OpenEvent((int) rights, false, name);
      if (handle.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (2 == lastWin32Error || 123 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameNotFound;
        if (3 == lastWin32Error)
          return WaitHandle.OpenExistingResult.PathNotFound;
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameInvalid;
        __Error.WinIOError(lastWin32Error, "");
      }
      result = new EventWaitHandle(handle);
      return WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>将事件状态设置为非终止状态，导致线程阻止。</summary>
    /// <returns>如果该操作成功，则为 true；否则，为 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">之前已对此 <see cref="T:System.Threading.EventWaitHandle" /> 调用 <see cref="M:System.Threading.EventWaitHandle.Close" /> 方法。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Reset()
    {
      int num = Win32Native.ResetEvent(this.safeWaitHandle) ? 1 : 0;
      if (num != 0)
        return num != 0;
      __Error.WinIOError();
      return num != 0;
    }

    /// <summary>将事件状态设置为终止状态，允许一个或多个等待线程继续。</summary>
    /// <returns>如果该操作成功，则为 true；否则，为 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">之前已对此 <see cref="T:System.Threading.EventWaitHandle" /> 调用 <see cref="M:System.Threading.EventWaitHandle.Close" /> 方法。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Set()
    {
      int num = Win32Native.SetEvent(this.safeWaitHandle) ? 1 : 0;
      if (num != 0)
        return num != 0;
      __Error.WinIOError();
      return num != 0;
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 对象，该对象表示由当前 <see cref="T:System.Threading.EventWaitHandle" /> 对象表示的已命名系统事件的访问控制安全性。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 对象，表示已命名系统事件的访问控制安全性。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">当前 <see cref="T:System.Threading.EventWaitHandle" /> 对象表示一个已命名的系统事件，但用户不具备 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" /> 权限。- 或 -当前 <see cref="T:System.Threading.EventWaitHandle" /> 对象表示一个已命名的系统事件，但它不是以 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" /> 打开的。</exception>
    /// <exception cref="T:System.NotSupportedException">在 Windows 98 或 Windows Millennium Edition 中不受支持。</exception>
    /// <exception cref="T:System.ObjectDisposedException">之前已对此 <see cref="T:System.Threading.EventWaitHandle" /> 调用 <see cref="M:System.Threading.EventWaitHandle.Close" /> 方法。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public EventWaitHandleSecurity GetAccessControl()
    {
      return new EventWaitHandleSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>设置已命名的系统事件的访问控制安全性。</summary>
    /// <param name="eventSecurity">一个 <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> 对象，表示应用于已命名的系统事件的访问控制安全性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventSecurity" /> 为 null。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户不具备 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" /> 权限。- 或 -事件不是以 <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" /> 打开的。</exception>
    /// <exception cref="T:System.SystemException">当前 <see cref="T:System.Threading.EventWaitHandle" /> 对象不表示已命名的系统事件。</exception>
    /// <exception cref="T:System.ObjectDisposedException">之前已对此 <see cref="T:System.Threading.EventWaitHandle" /> 调用 <see cref="M:System.Threading.EventWaitHandle.Close" /> 方法。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
    {
      if (eventSecurity == null)
        throw new ArgumentNullException("eventSecurity");
      eventSecurity.Persist(this.safeWaitHandle);
    }
  }
}
