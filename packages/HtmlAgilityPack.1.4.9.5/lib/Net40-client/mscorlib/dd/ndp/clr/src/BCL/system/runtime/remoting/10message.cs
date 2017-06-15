// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>保存为响应远程对象上的方法调用而返回的消息。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
  {
    internal object _ret;
    internal object _properties;
    internal string _URI;
    internal Exception _e;
    internal object[] _outArgs;
    internal int _outArgsCount;
    internal string _methodName;
    internal string _typeName;
    internal Type[] _methodSignature;
    internal bool _hasVarArgs;
    internal LogicalCallContext _callContext;
    internal ArgMapper _argMapper;
    internal MethodBase _methodBase;

    /// <summary>获取或设置在其上调用远程方法的远程对象的 URI。</summary>
    /// <returns>在其上调用远程方法的远程对象的 URI。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string Uri
    {
      [SecurityCritical] get
      {
        return this._URI;
      }
      set
      {
        this._URI = value;
      }
    }

    /// <summary>获取被调用方法的名称。</summary>
    /// <returns>从中生成当前 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 的方法的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string MethodName
    {
      [SecurityCritical] get
      {
        return this._methodName;
      }
    }

    /// <summary>获取在其上调用远程方法的类型的名称。</summary>
    /// <returns>在其上调用远程方法的远程对象的类型名。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string TypeName
    {
      [SecurityCritical] get
      {
        return this._typeName;
      }
    }

    /// <summary>获取包含方法签名的 <see cref="T:System.Type" /> 对象的数组。</summary>
    /// <returns>包含方法签名的 <see cref="T:System.Type" /> 对象的数组。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object MethodSignature
    {
      [SecurityCritical] get
      {
        if (this._methodSignature == null && this._methodBase != (MethodBase) null)
          this._methodSignature = Message.GenerateMethodSignature(this._methodBase);
        return (object) this._methodSignature;
      }
    }

    /// <summary>获取被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</summary>
    /// <returns>被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this._methodBase;
      }
    }

    /// <summary>获取一个值，该值指示被调用方法是否接受数目可变的参数。</summary>
    /// <returns>如果被调用方法接受数目可变的参数，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return this._hasVarArgs;
      }
    }

    /// <summary>获取被调用方法的参数数目。</summary>
    /// <returns>被调用方法的参数数目。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public int ArgCount
    {
      [SecurityCritical] get
      {
        if (this._outArgs == null)
          return this._outArgsCount;
        return this._outArgs.Length;
      }
    }

    /// <summary>获取传递给在远程对象上调用的方法的指定参数。</summary>
    /// <returns>传递给在远程对象上调用的方法的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] Args
    {
      [SecurityCritical] get
      {
        if (this._outArgs == null)
          return new object[this._outArgsCount];
        return this._outArgs;
      }
    }

    /// <summary>获取被调用方法上的 out 或 ref 参数的数目。</summary>
    /// <returns>被调用方法上的 out 或 ref 参数的数目。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.ArgCount;
      }
    }

    /// <summary>获取作为 out 或 ref 参数传递给被调用方法的指定对象。</summary>
    /// <returns>作为 out 或 ref 参数传递给被调用方法的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.Args;
      }
    }

    /// <summary>获取在远程方法调用期间引发的异常。</summary>
    /// <returns>在方法调用期间引发的异常；或者如果在调用期间未出现异常，则为 null。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public Exception Exception
    {
      [SecurityCritical] get
      {
        return this._e;
      }
    }

    /// <summary>获取被调用方法所返回的对象。</summary>
    /// <returns>被调用方法所返回的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object ReturnValue
    {
      [SecurityCritical] get
      {
        return this._ret;
      }
    }

    /// <summary>获取包含在当前 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 中的属性的 <see cref="T:System.Collections.IDictionary" />。</summary>
    /// <returns>包含在当前 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 中的属性的 <see cref="T:System.Collections.IDictionary" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          this._properties = (object) new MRMDictionary((IMethodReturnMessage) this, (IDictionary) null);
        return (IDictionary) this._properties;
      }
    }

    /// <summary>获取被调用方法的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</summary>
    /// <returns>被调用方法的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this.GetLogicalCallContext();
      }
    }

    /// <summary>使用方法调用后返回到调用方的所有信息来初始化 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 类的新实例。</summary>
    /// <param name="ret">从中生成当前 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 实例的被调用方法所返回的对象。</param>
    /// <param name="outArgs">作为 out 参数从被调用方法返回的对象。</param>
    /// <param name="outArgsCount">从被调用方法返回的 out 参数的数目。</param>
    /// <param name="callCtx">方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</param>
    /// <param name="mcm">对被调用方法进行的原始方法调用。</param>
    [SecurityCritical]
    public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
    {
      this._ret = ret;
      this._outArgs = outArgs;
      this._outArgsCount = outArgsCount;
      this._callContext = callCtx == null ? Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext : callCtx;
      if (mcm == null)
        return;
      this._URI = mcm.Uri;
      this._methodName = mcm.MethodName;
      this._methodSignature = (Type[]) null;
      this._typeName = mcm.TypeName;
      this._hasVarArgs = mcm.HasVarArgs;
      this._methodBase = mcm.MethodBase;
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 类的新实例。</summary>
    /// <param name="e">执行远程调用的方法期间引发的异常。</param>
    /// <param name="mcm">用于创建 <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> 类的实例的 <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" />。</param>
    [SecurityCritical]
    public ReturnMessage(Exception e, IMethodCallMessage mcm)
    {
      this._e = ReturnMessage.IsCustomErrorEnabled() ? (Exception) new RemotingException(Environment.GetResourceString("Remoting_InternalError")) : e;
      this._callContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      if (mcm == null)
        return;
      this._URI = mcm.Uri;
      this._methodName = mcm.MethodName;
      this._methodSignature = (Type[]) null;
      this._typeName = mcm.TypeName;
      this._hasVarArgs = mcm.HasVarArgs;
      this._methodBase = mcm.MethodBase;
    }

    /// <summary>返回在方法调用期间传递给远程方法的指定参数。</summary>
    /// <returns>在方法调用期间传递给远程方法的参数。</returns>
    /// <param name="argNum">所请求的参数的从零开始的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetArg(int argNum)
    {
      if (this._outArgs == null)
      {
        if (argNum < 0 || argNum >= this._outArgsCount)
          throw new ArgumentOutOfRangeException("argNum");
        return (object) null;
      }
      if (argNum < 0 || argNum >= this._outArgs.Length)
        throw new ArgumentOutOfRangeException("argNum");
      return this._outArgs[argNum];
    }

    /// <summary>返回指定的方法参数的名称。</summary>
    /// <returns>指定的方法参数的名称。</returns>
    /// <param name="index">所请求的参数名称的从零开始的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public string GetArgName(int index)
    {
      if (this._outArgs == null)
      {
        if (index < 0 || index >= this._outArgsCount)
          throw new ArgumentOutOfRangeException("index");
      }
      else if (index < 0 || index >= this._outArgs.Length)
        throw new ArgumentOutOfRangeException("index");
      if (this._methodBase != (MethodBase) null)
        return InternalRemotingServices.GetReflectionCachedData(this._methodBase).Parameters[index].Name;
      return "__param" + (object) index;
    }

    /// <summary>返回在远程方法调用期间作为 out 或 ref 参数传递的对象。</summary>
    /// <returns>在远程方法调用期间作为 out 或 ref 参数传递的对象。</returns>
    /// <param name="argNum">所请求的 out 或 ref 参数的从零开始的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public object GetOutArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArg(argNum);
    }

    /// <summary>返回传递给远程方法的指定的 out 或 ref 参数的名称。</summary>
    /// <returns>表示指定的 out 或 ref 参数名称的字符串；或者如果未实现当前方法，则为 null。</returns>
    /// <param name="index">所请求的参数的从零开始的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public string GetOutArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArgName(index);
    }

    [SecurityCritical]
    internal LogicalCallContext GetLogicalCallContext()
    {
      if (this._callContext == null)
        this._callContext = new LogicalCallContext();
      return this._callContext;
    }

    internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
    {
      LogicalCallContext logicalCallContext = this._callContext;
      this._callContext = ctx;
      return logicalCallContext;
    }

    internal bool HasProperties()
    {
      return this._properties != null;
    }

    [SecurityCritical]
    internal static bool IsCustomErrorEnabled()
    {
      object data = CallContext.GetData("__CustomErrorsEnabled");
      if (data != null)
        return (bool) data;
      return false;
    }
  }
}
