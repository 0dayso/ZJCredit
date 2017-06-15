// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.RemotePropertyHolderAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  internal class RemotePropertyHolderAttribute : IContextAttribute
  {
    private IList _cp;

    internal RemotePropertyHolderAttribute(IList cp)
    {
      this._cp = cp;
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      return false;
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
      for (int index = 0; index < this._cp.Count; ++index)
        ctorMsg.ContextProperties.Add(this._cp[index]);
    }
  }
}
