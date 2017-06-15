// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.LeaseLifeTimeServiceProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
  [Serializable]
  internal class LeaseLifeTimeServiceProperty : IContextProperty, IContributeObjectSink
  {
    public string Name
    {
      [SecurityCritical] get
      {
        return "LeaseLifeTimeServiceProperty";
      }
    }

    [SecurityCritical]
    public bool IsNewContextOK(Context newCtx)
    {
      return true;
    }

    [SecurityCritical]
    public void Freeze(Context newContext)
    {
    }

    [SecurityCritical]
    public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
    {
      bool fServer;
      ServerIdentity serverIdentity = (ServerIdentity) MarshalByRefObject.GetIdentity(obj, out fServer);
      if (serverIdentity.IsSingleCall())
        return nextSink;
      object obj1 = obj.InitializeLifetimeService();
      if (obj1 == null)
        return nextSink;
      if (!(obj1 is ILease))
        throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_ILeaseReturn", obj1));
      ILease lease1 = (ILease) obj1;
      if (lease1.InitialLeaseTime.CompareTo(TimeSpan.Zero) <= 0)
      {
        if (lease1 is Lease)
          ((Lease) lease1).Remove();
        return nextSink;
      }
      Lease lease2 = (Lease) null;
      lock (serverIdentity)
      {
        if (serverIdentity.Lease != null)
        {
          lease2 = serverIdentity.Lease;
          Lease temp_62 = lease2;
          TimeSpan temp_63 = temp_62.InitialLeaseTime;
          temp_62.Renew(temp_63);
        }
        else
        {
          if (!(lease1 is Lease))
          {
            lease2 = (Lease) LifetimeServices.GetLeaseInitial(obj);
            if (lease2.CurrentState == LeaseState.Initial)
            {
              lease2.InitialLeaseTime = lease1.InitialLeaseTime;
              lease2.RenewOnCallTime = lease1.RenewOnCallTime;
              lease2.SponsorshipTimeout = lease1.SponsorshipTimeout;
            }
          }
          else
            lease2 = (Lease) lease1;
          serverIdentity.Lease = lease2;
          if (serverIdentity.ObjectRef != null)
            lease2.ActivateLease();
        }
      }
      if (lease2.RenewOnCallTime > TimeSpan.Zero)
        return (IMessageSink) new LeaseSink(lease2, nextSink);
      return nextSink;
    }
  }
}
