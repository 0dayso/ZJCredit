// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.CallContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>提供与执行代码路径一起传送的属性集。此类不能被继承。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  public sealed class CallContext
  {
    internal static IPrincipal Principal
    {
      [SecurityCritical] get
      {
        return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
      }
      [SecurityCritical] set
      {
        Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
      }
    }

    /// <summary>获取或设置与当前线程相关联的主机上下文。</summary>
    /// <returns>与当前线程相关联的主机上下文。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    public static object HostContext
    {
      [SecurityCritical] get
      {
        ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
        return executionContextReader.IllogicalCallContext.HostContext ?? executionContextReader.LogicalCallContext.HostContext;
      }
      [SecurityCritical] set
      {
        ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
        if (value is ILogicalThreadAffinative)
        {
          executionContext.IllogicalCallContext.HostContext = (object) null;
          executionContext.LogicalCallContext.HostContext = value;
        }
        else
        {
          executionContext.IllogicalCallContext.HostContext = value;
          executionContext.LogicalCallContext.HostContext = (object) null;
        }
      }
    }

    private CallContext()
    {
    }

    internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      LogicalCallContext logicalCallContext1 = executionContext.LogicalCallContext;
      LogicalCallContext logicalCallContext2 = callCtx;
      executionContext.LogicalCallContext = logicalCallContext2;
      return logicalCallContext1;
    }

    /// <summary>清空具有指定名称的数据槽。</summary>
    /// <param name="name">要清空的数据槽的名称。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void FreeNamedDataSlot(string name)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContext.LogicalCallContext.FreeNamedDataSlot(name);
      executionContext.IllogicalCallContext.FreeNamedDataSlot(name);
    }

    /// <summary>从逻辑调用上下文中检索具有指定名称的对象。</summary>
    /// <returns>逻辑调用上下文中与指定名称关联的对象。</returns>
    /// <param name="name">逻辑调用上下文中的项的名称。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    public static object LogicalGetData(string name)
    {
      return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
    }

    private static object IllogicalGetData(string name)
    {
      return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
    }

    /// <summary>从 <see cref="T:System.Runtime.Remoting.Messaging.CallContext" /> 中检索具有指定名称的对象。</summary>
    /// <returns>调用上下文中与指定名称关联的对象。</returns>
    /// <param name="name">调用上下文中的项的名称。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetData(string name)
    {
      return CallContext.LogicalGetData(name) ?? CallContext.IllogicalGetData(name);
    }

    /// <summary>存储给定对象并将其与指定名称关联。</summary>
    /// <param name="name">要与新项关联的调用上下文中的名称。</param>
    /// <param name="data">要存储在调用上下文中的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void SetData(string name, object data)
    {
      if (data is ILogicalThreadAffinative)
      {
        CallContext.LogicalSetData(name, data);
      }
      else
      {
        ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
        executionContext.LogicalCallContext.FreeNamedDataSlot(name);
        executionContext.IllogicalCallContext.SetData(name, data);
      }
    }

    /// <summary>将一个给定对象存储在逻辑调用上下文中并将该对象与指定名称相关联。</summary>
    /// <param name="name">要与逻辑调用上下文中的新项相关联的名称。</param>
    /// <param name="data">要存储在逻辑调用上下文中的对象，此对象必须序列化。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    [SecurityCritical]
    public static void LogicalSetData(string name, object data)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContext.IllogicalCallContext.FreeNamedDataSlot(name);
      executionContext.LogicalCallContext.SetData(name, data);
    }

    /// <summary>返回与方法调用一起发送的标题。</summary>
    /// <returns>与方法调用一起发送的标题。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Header[] GetHeaders()
    {
      return Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalGetHeaders();
    }

    /// <summary>设置与方法调用一起发送的标题。</summary>
    /// <param name="headers">一个由要随方法调用发送的标头组成的 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 数组。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void SetHeaders(Header[] headers)
    {
      Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.InternalSetHeaders(headers);
    }
  }
}
