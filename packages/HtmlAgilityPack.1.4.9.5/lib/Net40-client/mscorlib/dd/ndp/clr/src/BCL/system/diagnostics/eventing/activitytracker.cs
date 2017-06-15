// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ActivityTracker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
  internal class ActivityTracker
  {
    private static ActivityTracker s_activityTrackerInstance = new ActivityTracker();
    private static long m_nextId = 0;
    private AsyncLocal<ActivityTracker.ActivityInfo> m_current;
    private const ushort MAX_ACTIVITY_DEPTH = 100;

    public static ActivityTracker Instance
    {
      get
      {
        return ActivityTracker.s_activityTrackerInstance;
      }
    }

    private Guid CurrentActivityId
    {
      get
      {
        return this.m_current.Value.ActivityId;
      }
    }

    public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options)
    {
      if (this.m_current == null)
        return;
      ActivityTracker.ActivityInfo activityInfo = this.m_current.Value;
      string str = this.NormalizeActivityName(providerName, activityName, task);
      TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
      if (tplEtwProvider.Debug)
      {
        tplEtwProvider.DebugFacilityMessage("OnStartEnter", str);
        tplEtwProvider.DebugFacilityMessage("OnStartEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
      }
      if (activityInfo != null)
      {
        if (activityInfo.m_level >= 100)
        {
          activityId = Guid.Empty;
          relatedActivityId = Guid.Empty;
          if (!tplEtwProvider.Debug)
            return;
          tplEtwProvider.DebugFacilityMessage("OnStartRET", "Fail");
          return;
        }
        if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None && this.FindActiveActivity(str, activityInfo) != null)
        {
          this.OnStop(providerName, activityName, task, ref activityId);
          activityInfo = this.m_current.Value;
        }
      }
      long uniqueId = activityInfo != null ? Interlocked.Increment(ref activityInfo.m_lastChildID) : Interlocked.Increment(ref ActivityTracker.m_nextId);
      relatedActivityId = activityInfo != null ? activityInfo.ActivityId : Guid.Empty;
      ActivityTracker.ActivityInfo list = new ActivityTracker.ActivityInfo(str, uniqueId, activityInfo, options);
      this.m_current.Value = list;
      activityId = list.ActivityId;
      if (!tplEtwProvider.Debug)
        return;
      tplEtwProvider.DebugFacilityMessage("OnStartRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(list));
      tplEtwProvider.DebugFacilityMessage1("OnStartRet", activityId.ToString(), relatedActivityId.ToString());
    }

    public void OnStop(string providerName, string activityName, int task, ref Guid activityId)
    {
      if (this.m_current == null)
        return;
      string str = this.NormalizeActivityName(providerName, activityName, task);
      TplEtwProvider tplEtwProvider = TplEtwProvider.Log;
      if (tplEtwProvider.Debug)
      {
        tplEtwProvider.DebugFacilityMessage("OnStopEnter", str);
        tplEtwProvider.DebugFacilityMessage("OnStopEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value));
      }
      ActivityTracker.ActivityInfo list;
      ActivityTracker.ActivityInfo activeActivity;
      do
      {
        ActivityTracker.ActivityInfo startLocation = this.m_current.Value;
        list = (ActivityTracker.ActivityInfo) null;
        activeActivity = this.FindActiveActivity(str, startLocation);
        if (activeActivity == null)
        {
          activityId = Guid.Empty;
          if (!tplEtwProvider.Debug)
            return;
          tplEtwProvider.DebugFacilityMessage("OnStopRET", "Fail");
          return;
        }
        activityId = activeActivity.ActivityId;
        ActivityTracker.ActivityInfo activityInfo = startLocation;
        while (activityInfo != activeActivity && activityInfo != null)
        {
          if (activityInfo.m_stopped != 0)
          {
            activityInfo = activityInfo.m_creator;
          }
          else
          {
            if (activityInfo.CanBeOrphan())
            {
              if (list == null)
                list = activityInfo;
            }
            else
              activityInfo.m_stopped = 1;
            activityInfo = activityInfo.m_creator;
          }
        }
      }
      while (Interlocked.CompareExchange(ref activeActivity.m_stopped, 1, 0) != 0);
      if (list == null)
        list = activeActivity.m_creator;
      this.m_current.Value = list;
      if (!tplEtwProvider.Debug)
        return;
      tplEtwProvider.DebugFacilityMessage("OnStopRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(list));
      tplEtwProvider.DebugFacilityMessage("OnStopRet", activityId.ToString());
    }

    [SecuritySafeCritical]
    public void Enable()
    {
      if (this.m_current != null)
        return;
      this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
    }

    private ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
    {
      for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
      {
        if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
          return activityInfo;
      }
      return (ActivityTracker.ActivityInfo) null;
    }

    private string NormalizeActivityName(string providerName, string activityName, int task)
    {
      if (activityName.EndsWith("Start"))
        activityName = activityName.Substring(0, activityName.Length - "Start".Length);
      else if (activityName.EndsWith("Stop"))
        activityName = activityName.Substring(0, activityName.Length - "Stop".Length);
      else if (task != 0)
        activityName = "task" + task.ToString();
      return providerName + activityName;
    }

    private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
    {
      if (args.PreviousValue == args.CurrentValue)
        return;
      if (args.CurrentValue != null)
        EventSource.SetCurrentThreadActivityId(args.CurrentValue.ActivityId);
      else
        EventSource.SetCurrentThreadActivityId(Guid.Empty);
    }

    private class ActivityInfo
    {
      internal readonly string m_name;
      private readonly long m_uniqueId;
      internal readonly Guid m_guid;
      internal readonly int m_activityPathGuidOffset;
      internal readonly int m_level;
      internal readonly EventActivityOptions m_eventOptions;
      internal long m_lastChildID;
      internal int m_stopped;
      internal readonly ActivityTracker.ActivityInfo m_creator;

      public Guid ActivityId
      {
        get
        {
          return this.m_guid;
        }
      }

      public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, EventActivityOptions options)
      {
        this.m_name = name;
        this.m_eventOptions = options;
        this.m_creator = creator;
        this.m_uniqueId = uniqueId;
        this.m_level = creator != null ? creator.m_level + 1 : 0;
        this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
      }

      public static string Path(ActivityTracker.ActivityInfo activityInfo)
      {
        if (activityInfo == null)
          return "";
        return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + (object) activityInfo.m_uniqueId;
      }

      public override string ToString()
      {
        string str = "";
        if (this.m_stopped != 0)
          str = ",DEAD";
        return this.m_name + "(" + ActivityTracker.ActivityInfo.Path(this) + str + ")";
      }

      public static string LiveActivities(ActivityTracker.ActivityInfo list)
      {
        if (list == null)
          return "";
        return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
      }

      public bool CanBeOrphan()
      {
        return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
      }

      [SecuritySafeCritical]
      private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
      {
        fixed (Guid* outPtr = &idRet)
        {
          int whereToAddId1 = 0;
          int whereToAddId2;
          if (this.m_creator != null)
          {
            whereToAddId2 = this.m_creator.m_activityPathGuidOffset;
            idRet = this.m_creator.m_guid;
          }
          else
            whereToAddId2 = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, whereToAddId1, (uint) Thread.GetDomainID(), false);
          activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, whereToAddId2, (uint) this.m_uniqueId, false);
          if (12 < activityPathGuidOffset)
            this.CreateOverflowGuid(outPtr);
        }
      }

      [SecurityCritical]
      private unsafe void CreateOverflowGuid(Guid* outPtr)
      {
        for (ActivityTracker.ActivityInfo activityInfo = this.m_creator; activityInfo != null; activityInfo = activityInfo.m_creator)
        {
          if (activityInfo.m_activityPathGuidOffset <= 10)
          {
            uint id = (uint) Interlocked.Increment(ref activityInfo.m_lastChildID);
            *outPtr = activityInfo.m_guid;
            if (ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, activityInfo.m_activityPathGuidOffset, id, true) <= 12)
              break;
          }
        }
      }

      [SecurityCritical]
      private static unsafe int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
      {
        byte* numPtr1 = (byte*) outPtr;
        byte* endPtr = numPtr1 + 12;
        byte* ptr = numPtr1 + whereToAddId;
        if (endPtr <= ptr)
          return 13;
        if (0U < id && id <= 10U && !overflow)
        {
          ActivityTracker.ActivityInfo.WriteNibble(ref ptr, endPtr, id);
        }
        else
        {
          uint num = 4;
          if (id <= (uint) byte.MaxValue)
            num = 1U;
          else if (id <= (uint) ushort.MaxValue)
            num = 2U;
          else if (id <= 16777215U)
            num = 3U;
          if (overflow)
          {
            if (endPtr <= ptr + 2)
              return 13;
            ActivityTracker.ActivityInfo.WriteNibble(ref ptr, endPtr, 11U);
          }
          ActivityTracker.ActivityInfo.WriteNibble(ref ptr, endPtr, (uint) (12 + ((int) num - 1)));
          if (ptr < endPtr && (int) *ptr != 0)
          {
            if (id < 4096U)
            {
              *ptr = (byte) ((uint) (12 << 4) + (id >> 8));
              id &= (uint) byte.MaxValue;
            }
            ++ptr;
          }
          for (; 0U < num; --num)
          {
            if (endPtr <= ptr)
            {
              ++ptr;
              break;
            }
            *ptr++ = (byte) id;
            id >>= 8;
          }
        }
        uint* numPtr2 = (uint*) outPtr;
        numPtr2[3] = (uint) ((int) *numPtr2 + (int) *(uint*) ((IntPtr) numPtr2 + 4) + (int) numPtr2[2] + 1503500717);
        return (int) (ptr - (byte*) outPtr);
      }

      [SecurityCritical]
      private static unsafe void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
      {
        if ((int) *ptr != 0)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          byte*& local = @ptr;
          // ISSUE: explicit reference operation
          byte* numPtr1 = ^local;
          IntPtr num1 = (IntPtr) (numPtr1 + 1);
          // ISSUE: explicit reference operation
          ^local = (byte*) num1;
          byte* numPtr2 = numPtr1;
          int num2 = (int) (byte) ((uint) *numPtr2 | (uint) (byte) value);
          *numPtr2 = (byte) num2;
        }
        else
          *ptr = (byte) (value << 4);
      }

      private enum NumberListCodes : byte
      {
        End = 0,
        LastImmediateValue = 10,
        PrefixCode = 11,
        MultiByte1 = 12,
      }
    }
  }
}
