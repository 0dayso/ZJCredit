// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Debugger
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
  /// <summary>启用与调试器的通讯。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class Debugger
  {
    /// <summary>用常数表示消息的默认类别。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly string DefaultCategory;

    /// <summary>获取一个值，它指示调试器是否已附加到进程。</summary>
    /// <returns>如果调试器已连接，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static extern bool IsAttached { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Debugger" /> 类的新实例。</summary>
    [Obsolete("Do not create instances of the Debugger class.  Call the static methods directly on this type instead", true)]
    public Debugger()
    {
    }

    /// <summary>发出信号表示连接调试器的断点。</summary>
    /// <exception cref="T:System.Security.SecurityException">
    /// <see cref="T:System.Security.Permissions.UIPermission" /> 未设置为在调试器中设置断点。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Break()
    {
      if (!Debugger.IsAttached)
      {
        try
        {
          new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
        }
        catch (SecurityException ex)
        {
          return;
        }
      }
      Debugger.BreakInternal();
    }

    [SecuritySafeCritical]
    private static void BreakCanThrow()
    {
      if (!Debugger.IsAttached)
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      Debugger.BreakInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void BreakInternal();

    /// <summary>启动调试器并将其连接到进程。</summary>
    /// <returns>如果启动成功或者调试器已连接，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.SecurityException">
    /// <see cref="T:System.Security.Permissions.UIPermission" /> 未设置为启动调试器。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Launch()
    {
      if (Debugger.IsAttached)
        return true;
      try
      {
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      }
      catch (SecurityException ex)
      {
        return false;
      }
      return Debugger.LaunchInternal();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void NotifyOfCrossThreadDependencySlow()
    {
      Debugger.CustomNotification((ICustomDebuggerNotification) new Debugger.CrossThreadDependencyNotification());
    }

    /// <summary>通知调试器执行即将进入一个涉及跨线程依赖项的路径。</summary>
    [ComVisible(false)]
    public static void NotifyOfCrossThreadDependency()
    {
      if (!Debugger.IsAttached)
        return;
      Debugger.NotifyOfCrossThreadDependencySlow();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool LaunchInternal();

    /// <summary>发送连接调试器的消息。</summary>
    /// <param name="level">消息重要性的说明。</param>
    /// <param name="category">消息的类别。</param>
    /// <param name="message">要显示的消息。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Log(int level, string category, string message);

    /// <summary>检查连接的调试器是否已启用日志记录。</summary>
    /// <returns>如果已连接调试器并已启用日志记录，则值为 true；否则为 false。连接的调试器是 DbgManagedDebugger 注册表项中已注册的托管调试器。有关该注册表项的更多信息，请参见 启用 JIT 附加调试。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsLogging();

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void CustomNotification(ICustomDebuggerNotification data);

    private class CrossThreadDependencyNotification : ICustomDebuggerNotification
    {
    }
  }
}
