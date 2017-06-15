// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.LeaseManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
  internal class LeaseManager
  {
    private Hashtable leaseToTimeTable = new Hashtable();
    private Hashtable sponsorTable = new Hashtable();
    private ArrayList tempObjects = new ArrayList(10);
    private TimeSpan pollTime;
    private AutoResetEvent waitHandle;
    private TimerCallback leaseTimeAnalyzerDelegate;
    private volatile Timer leaseTimer;

    [SecurityCritical]
    private LeaseManager(TimeSpan pollTime)
    {
      this.pollTime = pollTime;
      this.leaseTimeAnalyzerDelegate = new TimerCallback(this.LeaseTimeAnalyzer);
      this.waitHandle = new AutoResetEvent(false);
      this.leaseTimer = new Timer(this.leaseTimeAnalyzerDelegate, (object) null, -1, -1);
      this.leaseTimer.Change((int) pollTime.TotalMilliseconds, -1);
    }

    internal static bool IsInitialized()
    {
      return Thread.GetDomain().RemotingData.LeaseManager != null;
    }

    [SecurityCritical]
    internal static LeaseManager GetLeaseManager(TimeSpan pollTime)
    {
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      LeaseManager leaseManager = remotingData.LeaseManager;
      if (leaseManager == null)
      {
        lock (remotingData)
        {
          if (remotingData.LeaseManager == null)
            remotingData.LeaseManager = new LeaseManager(pollTime);
          leaseManager = remotingData.LeaseManager;
        }
      }
      return leaseManager;
    }

    internal static LeaseManager GetLeaseManager()
    {
      return Thread.GetDomain().RemotingData.LeaseManager;
    }

    internal void ChangePollTime(TimeSpan pollTime)
    {
      this.pollTime = pollTime;
    }

    internal void ActivateLease(Lease lease)
    {
      lock (this.leaseToTimeTable)
        this.leaseToTimeTable[(object) lease] = (object) lease.leaseTime;
    }

    internal void DeleteLease(Lease lease)
    {
      lock (this.leaseToTimeTable)
        this.leaseToTimeTable.Remove((object) lease);
    }

    [Conditional("_LOGGING")]
    internal void DumpLeases(Lease[] leases)
    {
      int num = 0;
      while (num < leases.Length)
        ++num;
    }

    internal ILease GetLease(MarshalByRefObject obj)
    {
      bool fServer = true;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      if (identity == null)
        return (ILease) null;
      return (ILease) identity.Lease;
    }

    internal void ChangedLeaseTime(Lease lease, DateTime newTime)
    {
      lock (this.leaseToTimeTable)
        this.leaseToTimeTable[(object) lease] = (object) newTime;
    }

    internal void RegisterSponsorCall(Lease lease, object sponsorId, TimeSpan sponsorshipTimeOut)
    {
      lock (this.sponsorTable)
      {
        DateTime local_2 = DateTime.UtcNow.Add(sponsorshipTimeOut);
        this.sponsorTable[sponsorId] = (object) new LeaseManager.SponsorInfo(lease, sponsorId, local_2);
      }
    }

    internal void DeleteSponsor(object sponsorId)
    {
      lock (this.sponsorTable)
        this.sponsorTable.Remove(sponsorId);
    }

    [SecurityCritical]
    private void LeaseTimeAnalyzer(object state)
    {
      DateTime utcNow = DateTime.UtcNow;
      lock (this.leaseToTimeTable)
      {
        IDictionaryEnumerator local_3 = this.leaseToTimeTable.GetEnumerator();
        while (local_3.MoveNext())
        {
          DateTime local_4 = (DateTime) local_3.Value;
          Lease local_5 = (Lease) local_3.Key;
          if (local_4.CompareTo(utcNow) < 0)
            this.tempObjects.Add((object) local_5);
        }
        for (int local_6 = 0; local_6 < this.tempObjects.Count; ++local_6)
          this.leaseToTimeTable.Remove((object) (Lease) this.tempObjects[local_6]);
      }
      for (int index = 0; index < this.tempObjects.Count; ++index)
      {
        Lease lease = (Lease) this.tempObjects[index];
        if (lease != null)
          lease.LeaseExpired(utcNow);
      }
      this.tempObjects.Clear();
      lock (this.sponsorTable)
      {
        IDictionaryEnumerator local_10 = this.sponsorTable.GetEnumerator();
        while (local_10.MoveNext())
        {
          object temp_72 = local_10.Key;
          LeaseManager.SponsorInfo local_11 = (LeaseManager.SponsorInfo) local_10.Value;
          if (local_11.sponsorWaitTime.CompareTo(utcNow) < 0)
            this.tempObjects.Add((object) local_11);
        }
        for (int local_12 = 0; local_12 < this.tempObjects.Count; ++local_12)
          this.sponsorTable.Remove(((LeaseManager.SponsorInfo) this.tempObjects[local_12]).sponsorId);
      }
      for (int index = 0; index < this.tempObjects.Count; ++index)
      {
        LeaseManager.SponsorInfo sponsorInfo = (LeaseManager.SponsorInfo) this.tempObjects[index];
        if (sponsorInfo != null && sponsorInfo.lease != null)
        {
          sponsorInfo.lease.SponsorTimeout(sponsorInfo.sponsorId);
          this.tempObjects[index] = (object) null;
        }
      }
      this.tempObjects.Clear();
      this.leaseTimer.Change((int) this.pollTime.TotalMilliseconds, -1);
    }

    internal class SponsorInfo
    {
      internal Lease lease;
      internal object sponsorId;
      internal DateTime sponsorWaitTime;

      internal SponsorInfo(Lease lease, object sponsorId, DateTime sponsorWaitTime)
      {
        this.lease = lease;
        this.sponsorId = sponsorId;
        this.sponsorWaitTime = sponsorWaitTime;
      }
    }
  }
}
