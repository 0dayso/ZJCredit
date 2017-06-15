// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ConstructionLevelActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  [Serializable]
  internal class ConstructionLevelActivator : IActivator
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
        return ActivatorLevel.Construction;
      }
    }

    internal ConstructionLevelActivator()
    {
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
    {
      IConstructionCallMessage constructionCallMessage = ctorMsg;
      IActivator nextActivator = constructionCallMessage.Activator.NextActivator;
      constructionCallMessage.Activator = nextActivator;
      return ActivationServices.DoServerContextActivation(ctorMsg);
    }
  }
}
