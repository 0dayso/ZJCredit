﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ActivationListener
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  internal class ActivationListener : MarshalByRefObject, IActivator
  {
    public virtual IActivator NextActivator
    {
      [SecurityCritical] get
      {
        return (IActivator) null;
      }
      [SecurityCritical] set
      {
        throw new InvalidOperationException();
      }
    }

    public virtual ActivatorLevel Level
    {
      [SecurityCritical] get
      {
        return ActivatorLevel.AppDomain;
      }
    }

    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      return (object) null;
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
    {
      if (ctorMsg == null || RemotingServices.IsTransparentProxy((object) ctorMsg))
        throw new ArgumentNullException("ctorMsg");
      ctorMsg.Properties[(object) "Permission"] = (object) "allowed";
      if (!RemotingConfigHandler.IsActivationAllowed(ctorMsg.ActivationTypeName))
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_PermissionDenied"), (object) ctorMsg.ActivationTypeName));
      if (ctorMsg.ActivationType == (Type) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) ctorMsg.ActivationTypeName));
      return ActivationServices.GetActivator().Activate(ctorMsg);
    }
  }
}
