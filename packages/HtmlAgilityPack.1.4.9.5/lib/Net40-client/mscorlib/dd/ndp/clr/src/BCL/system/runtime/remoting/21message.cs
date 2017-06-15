// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>实现 <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> 接口以创建一条消息，该消息作为远程对象上的方法调用的响应。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
  {
    private IMethodReturnMessage _msg;
    private IDictionary _properties;
    private ArgMapper _argMapper;
    private object[] _args;
    private object _returnValue;
    private Exception _exception;

    /// <summary>获取在其上进行方法调用的远程对象的统一资源标识符 (URI)。</summary>
    /// <returns>远程对象的 URI。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string Uri
    {
      [SecurityCritical] get
      {
        return this._msg.Uri;
      }
      set
      {
        this._msg.Properties[(object) Message.UriKey] = (object) value;
      }
    }

    /// <summary>获取被调用方法的名称。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含调用的方法的名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual string MethodName
    {
      [SecurityCritical] get
      {
        return this._msg.MethodName;
      }
    }

    /// <summary>获取在其上进行方法调用的远程对象的完整类型名称。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含在其上进行方法调用的远程对象的完整类型名称。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual string TypeName
    {
      [SecurityCritical] get
      {
        return this._msg.TypeName;
      }
    }

    /// <summary>获取包含方法签名的对象。</summary>
    /// <returns>包含方法签名的 <see cref="T:System.Object" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object MethodSignature
    {
      [SecurityCritical] get
      {
        return this._msg.MethodSignature;
      }
    }

    /// <summary>获取当前方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</summary>
    /// <returns>当前方法调用的 <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this._msg.LogicalCallContext;
      }
    }

    /// <summary>获取被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</summary>
    /// <returns>被调用方法的 <see cref="T:System.Reflection.MethodBase" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this._msg.MethodBase;
      }
    }

    /// <summary>获取传递给该方法的参数的数目。</summary>
    /// <returns>表示传递给方法的参数数目的 <see cref="T:System.Int32" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual int ArgCount
    {
      [SecurityCritical] get
      {
        if (this._args != null)
          return this._args.Length;
        return 0;
      }
    }

    /// <summary>获取传递给该方法的参数数组。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，表示传递给方法的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object[] Args
    {
      [SecurityCritical] get
      {
        return this._args;
      }
      set
      {
        this._args = value;
      }
    }

    /// <summary>获取一个标志，该标志指示该方法是否接受数目可变的参数。</summary>
    /// <returns>如果该方法可以接受数目可变的参数，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return this._msg.HasVarArgs;
      }
    }

    /// <summary>获取方法调用中标记为 ref 或 out 参数的参数的数目。</summary>
    /// <returns>一个 <see cref="T:System.Int32" />，表示方法调用中标记为 ref 或 out 参数的参数数目。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.ArgCount;
      }
    }

    /// <summary>获取方法调用中标记为 ref 或 out 参数的一组参数。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，表示方法调用中标记为 ref 或 out 参数的参数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.Args;
      }
    }

    /// <summary>获取方法调用期间引发的异常；或者如果该方法未引发异常，则为 null。</summary>
    /// <returns>方法调用期间引发的 <see cref="T:System.Exception" />；或者如果该方法未引发异常，则为 null。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual Exception Exception
    {
      [SecurityCritical] get
      {
        return this._exception;
      }
      set
      {
        this._exception = value;
      }
    }

    /// <summary>获取方法调用的返回值。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，表示方法调用的返回值。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object ReturnValue
    {
      [SecurityCritical] get
      {
        return this._returnValue;
      }
      set
      {
        this._returnValue = value;
      }
    }

    /// <summary>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    /// <returns>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          this._properties = (IDictionary) new MethodReturnMessageWrapper.MRMWrapperDictionary((IMethodReturnMessage) this, this._msg.Properties);
        return this._properties;
      }
    }

    /// <summary>包装 <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> 以创建一个 <see cref="T:System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper" />。</summary>
    /// <param name="msg">作为远程对象上的输出方法调用的消息。</param>
    public MethodReturnMessageWrapper(IMethodReturnMessage msg)
      : base((IMessage) msg)
    {
      this._msg = msg;
      this._args = this._msg.Args;
      this._returnValue = this._msg.ReturnValue;
      this._exception = this._msg.Exception;
    }

    /// <summary>获取指定索引处的方法参数的名称。</summary>
    /// <returns>方法参数的名称。</returns>
    /// <param name="index">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual string GetArgName(int index)
    {
      return this._msg.GetArgName(index);
    }

    /// <summary>获取指定索引处作为对象的方法参数。</summary>
    /// <returns>作为对象的方法参数。</returns>
    /// <param name="argNum">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object GetArg(int argNum)
    {
      return this._args[argNum];
    }

    /// <summary>返回标记为 ref 或 out 参数的指定参数。</summary>
    /// <returns>标记为 ref 或 out 参数的指定参数。</returns>
    /// <param name="argNum">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual object GetOutArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArg(argNum);
    }

    /// <summary>返回标记为 ref 或 out 参数的指定参数的名称。</summary>
    /// <returns>参数名称；或者如果未实现当前方法，则为 null。</returns>
    /// <param name="index">请求的参数的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual string GetOutArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArgName(index);
    }

    private class MRMWrapperDictionary : Hashtable
    {
      private IMethodReturnMessage _mrmsg;
      private IDictionary _idict;

      public override object this[object key]
      {
        [SecuritySafeCritical] get
        {
          switch (key as string)
          {
            case "__Uri":
              return (object) this._mrmsg.Uri;
            case "__MethodName":
              return (object) this._mrmsg.MethodName;
            case "__MethodSignature":
              return this._mrmsg.MethodSignature;
            case "__TypeName":
              return (object) this._mrmsg.TypeName;
            case "__Return":
              return this._mrmsg.ReturnValue;
            case "__OutArgs":
              return (object) this._mrmsg.OutArgs;
            default:
              return this._idict[key];
          }
        }
        [SecuritySafeCritical] set
        {
          switch (key as string)
          {
            case "__MethodName":
            case "__MethodSignature":
            case "__TypeName":
            case "__Return":
            case "__OutArgs":
              throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
            case null:
              break;
            default:
              this._idict[key] = value;
              break;
          }
        }
      }

      public MRMWrapperDictionary(IMethodReturnMessage msg, IDictionary idict)
      {
        this._mrmsg = msg;
        this._idict = idict;
      }
    }
  }
}
