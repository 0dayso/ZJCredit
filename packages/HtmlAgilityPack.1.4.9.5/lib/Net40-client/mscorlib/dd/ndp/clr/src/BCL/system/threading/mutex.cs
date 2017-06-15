// Decompiled with JetBrains decompiler
// Type: System.Threading.Mutex
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>还可用于进程间同步的同步基元。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class Mutex : WaitHandle
  {
    private static bool dummyBool;

    /// <summary>使用可指示调用线程是否应具有互斥体的初始所有权以及字符串是否为互斥体的名称的 Boolean 值和当线程返回时可指示调用线程是否已赋予互斥体的初始所有权的 Boolean 值初始化 <see cref="T:System.Threading.Mutex" /> 类的新实例。</summary>
    /// <param name="initiallyOwned">如果为 true，则给予调用线程已命名的系统互斥体的初始所属权（如果已命名的系统互斥体是通过此调用创建的）；否则为 false。</param>
    /// <param name="name">
    /// <see cref="T:System.Threading.Mutex" /> 的名称。如果值为 null，则 <see cref="T:System.Threading.Mutex" /> 是未命名的。</param>
    /// <param name="createdNew">在此方法返回时，如果创建了局部互斥体（即，如果 <paramref name="name" /> 为 null 或空字符串）或指定的命名系统互斥体，则包含布尔值 true；如果指定的命名系统互斥体已存在，则为 false。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">命名的互斥体存在并具有访问控制安全性，但用户不具有 <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">无法创建命名的互斥体，原因可能是与其他类型的等待句柄同名。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 长度超过 260 个字符。</exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex(bool initiallyOwned, string name, out bool createdNew)
      : this(initiallyOwned, name, out createdNew, (MutexSecurity) null)
    {
    }

    /// <summary>使用可指示调用线程是否应具有互斥体的初始所有权以及字符串是否为互斥体的名称的 Boolean 值和当线程返回时可指示调用线程是否已赋予互斥体的初始所有权以及访问控制安全是否已应用到命名互斥体的 Boolean 变量初始化 <see cref="T:System.Threading.Mutex" /> 类的新实例。</summary>
    /// <param name="initiallyOwned">如果为 true，则给予调用线程已命名的系统互斥体的初始所属权（如果已命名的系统互斥体是通过此调用创建的）；否则为 false。</param>
    /// <param name="name">系统互斥体的名称。如果值为 null，则 <see cref="T:System.Threading.Mutex" /> 是未命名的。</param>
    /// <param name="createdNew">在此方法返回时，如果创建了局部互斥体（即，如果 <paramref name="name" /> 为 null 或空字符串）或指定的命名系统互斥体，则包含布尔值 true；如果指定的命名系统互斥体已存在，则为 false。此参数未经初始化即被传递。</param>
    /// <param name="mutexSecurity">
    /// <see cref="T:System.Security.AccessControl.MutexSecurity" /> 对象，表示应用于已命名的系统互斥体的访问控制安全性。</param>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">命名的互斥体存在并具有访问控制安全性，但用户不具有 <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">无法创建命名的互斥体，原因可能是与其他类型的等待句柄同名。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 长度超过 260 个字符。</exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public unsafe Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity mutexSecurity)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (mutexSecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = mutexSecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      this.CreateMutexWithGuaranteedCleanup(initiallyOwned, name, out createdNew, securityAttributes);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal Mutex(bool initiallyOwned, string name, out bool createdNew, Win32Native.SECURITY_ATTRIBUTES secAttrs)
    {
      if (name != null && 260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      this.CreateMutexWithGuaranteedCleanup(initiallyOwned, name, out createdNew, secAttrs);
    }

    /// <summary>使用 Boolean 值（指示调用线程是否应具有互斥体的初始所有权以及字符串是否为互斥体的名称）初始化 <see cref="T:System.Threading.Mutex" /> 类的新实例。</summary>
    /// <param name="initiallyOwned">如果为 true，则给予调用线程已命名的系统互斥体的初始所属权（如果已命名的系统互斥体是通过此调用创建的）；否则为 false。</param>
    /// <param name="name">
    /// <see cref="T:System.Threading.Mutex" /> 的名称。如果值为 null，则 <see cref="T:System.Threading.Mutex" /> 是未命名的。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">命名的互斥体存在并具有访问控制安全性，但用户不具有 <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">无法创建命名的互斥体，原因可能是与其他类型的等待句柄同名。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 长度超过 260 个字符。</exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex(bool initiallyOwned, string name)
      : this(initiallyOwned, name, out Mutex.dummyBool)
    {
    }

    /// <summary>使用 Boolean 值（指示调用线程是否应具有互斥体的初始所有权）初始化 <see cref="T:System.Threading.Mutex" /> 类的新实例。</summary>
    /// <param name="initiallyOwned">如果给调用线程赋予互斥体的初始所属权，则为 true；否则为 false。</param>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex(bool initiallyOwned)
      : this(initiallyOwned, (string) null, out Mutex.dummyBool)
    {
    }

    /// <summary>使用默认属性初始化 <see cref="T:System.Threading.Mutex" /> 类的新实例。</summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public Mutex()
      : this(false, (string) null, out Mutex.dummyBool)
    {
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private Mutex(SafeWaitHandle handle)
    {
      this.SetHandleInternal(handle);
      this.hasThreadAffinity = true;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal void CreateMutexWithGuaranteedCleanup(bool initiallyOwned, string name, out bool createdNew, Win32Native.SECURITY_ATTRIBUTES secAttrs)
    {
      RuntimeHelpers.CleanupCode backoutCode = new RuntimeHelpers.CleanupCode(this.MutexCleanupCode);
      Mutex.MutexCleanupInfo cleanupInfo = new Mutex.MutexCleanupInfo((SafeWaitHandle) null, false);
      Mutex.MutexTryCodeHelper mutexTryCodeHelper = new Mutex.MutexTryCodeHelper(initiallyOwned, cleanupInfo, name, secAttrs, this);
      RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(new RuntimeHelpers.TryCode(mutexTryCodeHelper.MutexTryCode), backoutCode, (object) cleanupInfo);
      createdNew = mutexTryCodeHelper.m_newMutex;
    }

    [SecurityCritical]
    [PrePrepareMethod]
    private void MutexCleanupCode(object userData, bool exceptionThrown)
    {
      Mutex.MutexCleanupInfo mutexCleanupInfo = (Mutex.MutexCleanupInfo) userData;
      if (this.hasThreadAffinity)
        return;
      if (mutexCleanupInfo.mutexHandle != null && !mutexCleanupInfo.mutexHandle.IsInvalid)
      {
        if (mutexCleanupInfo.inCriticalRegion)
          Win32Native.ReleaseMutex(mutexCleanupInfo.mutexHandle);
        mutexCleanupInfo.mutexHandle.Dispose();
      }
      if (!mutexCleanupInfo.inCriticalRegion)
        return;
      Thread.EndCriticalRegion();
      Thread.EndThreadAffinity();
    }

    /// <summary>打开指定的已命名的互斥体（如果已经存在）。</summary>
    /// <returns>表示已命名的系统互斥体的对象。</returns>
    /// <param name="name">要打开的系统互斥体的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是一个空字符串。- 或 -<paramref name="name" /> 长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">命名的 mutex 不存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的互斥体存在，但用户不具备使用它所需的安全访问权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static Mutex OpenExisting(string name)
    {
      return Mutex.OpenExisting(name, MutexRights.Modify | MutexRights.Synchronize);
    }

    /// <summary>利用所需的安全访问权限，打开指定的已命名的互斥体（如果已经存在）。</summary>
    /// <returns>表示已命名的系统互斥体的对象。</returns>
    /// <param name="name">要打开的系统互斥体的名称。</param>
    /// <param name="rights">表示所需的安全访问权限的枚举值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是一个空字符串。- 或 -<paramref name="name" /> 长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">命名的 mutex 不存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的 mutex 存在，但是用户不具备所需的安全访问权。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Mutex OpenExisting(string name, MutexRights rights)
    {
      Mutex result;
      switch (Mutex.OpenExistingWorker(name, rights, out result))
      {
        case WaitHandle.OpenExistingResult.NameNotFound:
          throw new WaitHandleCannotBeOpenedException();
        case WaitHandle.OpenExistingResult.PathNotFound:
          __Error.WinIOError(3, name);
          return result;
        case WaitHandle.OpenExistingResult.NameInvalid:
          throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) name));
        default:
          return result;
      }
    }

    /// <summary>打开指定的已命名的互斥体（如果已经存在），并返回指示操作是否成功的值。</summary>
    /// <returns>如果命名互斥体成功打开，则为 true；否则为 false。</returns>
    /// <param name="name">要打开的系统互斥体的名称。</param>
    /// <param name="result">当此方法返回时，如果调用成功，则包含表示命名互斥体的 <see cref="T:System.Threading.Mutex" /> 对象；否则为 null。该参数未经初始化即被处理。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是一个空字符串。- 或 -<paramref name="name" /> 长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的互斥体存在，但用户不具备使用它所需的安全访问权限。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static bool TryOpenExisting(string name, out Mutex result)
    {
      return Mutex.OpenExistingWorker(name, MutexRights.Modify | MutexRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>利用所需的安全访问权限，打开指定的已命名的互斥体（如果已经存在），并返回指示操作是否成功的值。</summary>
    /// <returns>如果命名互斥体成功打开，则为 true；否则为 false。</returns>
    /// <param name="name">要打开的系统互斥体的名称。</param>
    /// <param name="rights">表示所需的安全访问权限的枚举值的按位组合。</param>
    /// <param name="result">当此方法返回时，如果调用成功，则包含表示命名互斥体的 <see cref="T:System.Threading.Mutex" /> 对象；否则为 null。该参数未经初始化即被处理。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是一个空字符串。- 或 -<paramref name="name" /> 长度超过 260 个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.IO.IOException">发生了一个 Win32 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">已命名的互斥体存在，但用户不具备使用它所需的安全访问权限。</exception>
    [SecurityCritical]
    public static bool TryOpenExisting(string name, MutexRights rights, out Mutex result)
    {
      return Mutex.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
    }

    [SecurityCritical]
    private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, MutexRights rights, out Mutex result)
    {
      if (name == null)
        throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_WithParamName"));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if (260 < name.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", (object) name));
      result = (Mutex) null;
      SafeWaitHandle handle = Win32Native.OpenMutex((int) rights, false, name);
      if (handle.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (2 == lastWin32Error || 123 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameNotFound;
        if (3 == lastWin32Error)
          return WaitHandle.OpenExistingResult.PathNotFound;
        if (name != null && name.Length != 0 && 6 == lastWin32Error)
          return WaitHandle.OpenExistingResult.NameInvalid;
        __Error.WinIOError(lastWin32Error, name);
      }
      result = new Mutex(handle);
      return WaitHandle.OpenExistingResult.Success;
    }

    /// <summary>释放 <see cref="T:System.Threading.Mutex" /> 一次。</summary>
    /// <exception cref="T:System.ApplicationException">调用线程不拥有互斥体。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public void ReleaseMutex()
    {
      if (!Win32Native.ReleaseMutex(this.safeWaitHandle))
        throw new ApplicationException(Environment.GetResourceString("Arg_SynchronizationLockException"));
      Thread.EndCriticalRegion();
      Thread.EndThreadAffinity();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static int CreateMutexHandle(bool initiallyOwned, string name, Win32Native.SECURITY_ATTRIBUTES securityAttribute, out SafeWaitHandle mutexHandle)
    {
      bool flag = false;
      int num;
      do
      {
        mutexHandle = Win32Native.CreateMutex(securityAttribute, initiallyOwned, name);
        num = Marshal.GetLastWin32Error();
        if (mutexHandle.IsInvalid && num == 5)
        {
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            try
            {
            }
            finally
            {
              Thread.BeginThreadAffinity();
              flag = true;
            }
            mutexHandle = Win32Native.OpenMutex(1048577, false, name);
            num = mutexHandle.IsInvalid ? Marshal.GetLastWin32Error() : 183;
          }
          finally
          {
            if (flag)
              Thread.EndThreadAffinity();
          }
        }
        else
          goto label_12;
      }
      while (num == 2);
      if (num == 0)
        num = 183;
label_12:
      return num;
    }

    /// <summary>获取表示已命名的互斥体的访问控制安全性的 <see cref="T:System.Security.AccessControl.MutexSecurity" /> 对象。</summary>
    /// <returns>表示已命名的互斥体的访问控制安全性的 <see cref="T:System.Security.AccessControl.MutexSecurity" /> 对象。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">当前 <see cref="T:System.Threading.Mutex" /> 对象表示一个已命名的系统互斥体，但用户不具备 <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />。- 或 -当前 <see cref="T:System.Threading.Mutex" /> 对象表示一个已命名的系统互斥体，但它未用 <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" /> 打开。</exception>
    /// <exception cref="T:System.NotSupportedException">在 Windows 98 或 Windows Millennium Edition 中不受支持。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public MutexSecurity GetAccessControl()
    {
      return new MutexSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>设置已命名的系统互斥体的访问控制安全性。</summary>
    /// <param name="mutexSecurity">
    /// <see cref="T:System.Security.AccessControl.MutexSecurity" /> 对象，表示应用于已命名的系统互斥体的访问控制安全性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mutexSecurity" /> 为 null。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户不具备 <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />。- 或 -互斥体未用 <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" /> 打开。</exception>
    /// <exception cref="T:System.SystemException">当前 <see cref="T:System.Threading.Mutex" /> 对象不表示已命名的系统互斥体。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void SetAccessControl(MutexSecurity mutexSecurity)
    {
      if (mutexSecurity == null)
        throw new ArgumentNullException("mutexSecurity");
      mutexSecurity.Persist(this.safeWaitHandle);
    }

    internal class MutexTryCodeHelper
    {
      private bool m_initiallyOwned;
      private Mutex.MutexCleanupInfo m_cleanupInfo;
      internal bool m_newMutex;
      private string m_name;
      [SecurityCritical]
      private Win32Native.SECURITY_ATTRIBUTES m_secAttrs;
      private Mutex m_mutex;

      [SecurityCritical]
      [PrePrepareMethod]
      internal MutexTryCodeHelper(bool initiallyOwned, Mutex.MutexCleanupInfo cleanupInfo, string name, Win32Native.SECURITY_ATTRIBUTES secAttrs, Mutex mutex)
      {
        this.m_initiallyOwned = initiallyOwned;
        this.m_cleanupInfo = cleanupInfo;
        this.m_name = name;
        this.m_secAttrs = secAttrs;
        this.m_mutex = mutex;
      }

      [SecurityCritical]
      [PrePrepareMethod]
      internal void MutexTryCode(object userData)
      {
        SafeWaitHandle mutexHandle = (SafeWaitHandle) null;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          if (this.m_initiallyOwned)
          {
            this.m_cleanupInfo.inCriticalRegion = true;
            Thread.BeginThreadAffinity();
            Thread.BeginCriticalRegion();
          }
        }
        int errorCode = 0;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          errorCode = Mutex.CreateMutexHandle(this.m_initiallyOwned, this.m_name, this.m_secAttrs, out mutexHandle);
        }
        if (mutexHandle.IsInvalid)
        {
          mutexHandle.SetHandleAsInvalid();
          if (this.m_name != null && this.m_name.Length != 0 && 6 == errorCode)
            throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", (object) this.m_name));
          __Error.WinIOError(errorCode, this.m_name);
        }
        this.m_newMutex = errorCode != 183;
        this.m_mutex.SetHandleInternal(mutexHandle);
        this.m_mutex.hasThreadAffinity = true;
      }
    }

    internal class MutexCleanupInfo
    {
      [SecurityCritical]
      internal SafeWaitHandle mutexHandle;
      internal bool inCriticalRegion;

      [SecurityCritical]
      internal MutexCleanupInfo(SafeWaitHandle mutexHandle, bool inCriticalRegion)
      {
        this.mutexHandle = mutexHandle;
        this.inCriticalRegion = inCriticalRegion;
      }
    }
  }
}
