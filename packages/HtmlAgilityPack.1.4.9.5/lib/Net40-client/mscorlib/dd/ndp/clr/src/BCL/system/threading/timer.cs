// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerQueue
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  internal class TimerQueue
  {
    private static TimerQueue s_queue = new TimerQueue();
    [SecurityCritical]
    private TimerQueue.AppDomainTimerSafeHandle m_appDomainTimer;
    private bool m_isAppDomainTimerScheduled;
    private int m_currentAppDomainTimerStartTicks;
    private uint m_currentAppDomainTimerDuration;
    private TimerQueueTimer m_timers;
    private volatile int m_pauseTicks;
    private static WaitCallback s_fireQueuedTimerCompletion;

    public static TimerQueue Instance
    {
      get
      {
        return TimerQueue.s_queue;
      }
    }

    private static int TickCount
    {
      [SecuritySafeCritical] get
      {
        if (!Environment.IsWindows8OrAbove)
          return Environment.TickCount;
        ulong UnbiasedTime;
        if (!Win32Native.QueryUnbiasedInterruptTime(out UnbiasedTime))
          throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
        return (int) (uint) (UnbiasedTime / 10000UL);
      }
    }

    private TimerQueue()
    {
    }

    [SecuritySafeCritical]
    private bool EnsureAppDomainTimerFiresBy(uint requestedDuration)
    {
      uint dueTime = Math.Min(requestedDuration, 268435455U);
      if (this.m_isAppDomainTimerScheduled)
      {
        uint num1 = (uint) (TimerQueue.TickCount - this.m_currentAppDomainTimerStartTicks);
        if (num1 >= this.m_currentAppDomainTimerDuration)
          return true;
        uint num2 = this.m_currentAppDomainTimerDuration - num1;
        if (dueTime >= num2)
          return true;
      }
      if (this.m_pauseTicks != 0)
        return true;
      if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
      {
        this.m_appDomainTimer = TimerQueue.CreateAppDomainTimer(dueTime);
        if (this.m_appDomainTimer.IsInvalid)
          return false;
        this.m_isAppDomainTimerScheduled = true;
        this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
        this.m_currentAppDomainTimerDuration = dueTime;
        return true;
      }
      if (!TimerQueue.ChangeAppDomainTimer(this.m_appDomainTimer, dueTime))
        return false;
      this.m_isAppDomainTimerScheduled = true;
      this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
      this.m_currentAppDomainTimerDuration = dueTime;
      return true;
    }

    [SecuritySafeCritical]
    internal static void AppDomainTimerCallback()
    {
      TimerQueue.Instance.FireNextTimers();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern TimerQueue.AppDomainTimerSafeHandle CreateAppDomainTimer(uint dueTime);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool ChangeAppDomainTimer(TimerQueue.AppDomainTimerSafeHandle handle, uint dueTime);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool DeleteAppDomainTimer(IntPtr handle);

    [SecurityCritical]
    internal void Pause()
    {
      lock (this)
      {
        if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
          return;
        this.m_appDomainTimer.Dispose();
        this.m_appDomainTimer = (TimerQueue.AppDomainTimerSafeHandle) null;
        this.m_isAppDomainTimerScheduled = false;
        this.m_pauseTicks = TimerQueue.TickCount;
      }
    }

    [SecurityCritical]
    internal void Resume()
    {
      lock (this)
      {
        try
        {
        }
        finally
        {
          int local_2 = this.m_pauseTicks;
          this.m_pauseTicks = 0;
          int local_3 = TimerQueue.TickCount;
          bool local_4 = false;
          uint local_5 = uint.MaxValue;
          for (TimerQueueTimer local_6 = this.m_timers; local_6 != null; local_6 = local_6.m_next)
          {
            uint local_7 = local_6.m_startTicks > local_2 ? (uint) (local_3 - local_6.m_startTicks) : (uint) (local_2 - local_6.m_startTicks);
            TimerQueueTimer temp_21 = local_6;
            int temp_27 = temp_21.m_dueTime > local_7 ? (int) local_6.m_dueTime - (int) local_7 : 0;
            temp_21.m_dueTime = (uint) temp_27;
            local_6.m_startTicks = local_3;
            if (local_6.m_dueTime < local_5)
            {
              local_4 = true;
              local_5 = local_6.m_dueTime;
            }
          }
          if (local_4)
            this.EnsureAppDomainTimerFiresBy(local_5);
        }
      }
    }

    private void FireNextTimers()
    {
      TimerQueueTimer timerQueueTimer = (TimerQueueTimer) null;
      lock (this)
      {
        try
        {
        }
        finally
        {
          this.m_isAppDomainTimerScheduled = false;
          bool local_3 = false;
          uint local_4 = uint.MaxValue;
          int local_5 = TimerQueue.TickCount;
          TimerQueueTimer local_6 = this.m_timers;
          while (local_6 != null)
          {
            uint local_7 = (uint) (local_5 - local_6.m_startTicks);
            if (local_7 >= local_6.m_dueTime)
            {
              TimerQueueTimer temp_33 = local_6.m_next;
              if ((int) local_6.m_period != -1)
              {
                local_6.m_startTicks = local_5;
                TimerQueueTimer temp_44 = local_6;
                int temp_45 = (int) temp_44.m_period;
                temp_44.m_dueTime = (uint) temp_45;
                if (local_6.m_dueTime < local_4)
                {
                  local_3 = true;
                  local_4 = local_6.m_dueTime;
                }
              }
              else
                this.DeleteTimer(local_6);
              if (timerQueueTimer == null)
                timerQueueTimer = local_6;
              else
                TimerQueue.QueueTimerCompletion(local_6);
              local_6 = temp_33;
            }
            else
            {
              uint local_8 = local_6.m_dueTime - local_7;
              if (local_8 < local_4)
              {
                local_3 = true;
                local_4 = local_8;
              }
              local_6 = local_6.m_next;
            }
          }
          if (local_3)
            this.EnsureAppDomainTimerFiresBy(local_4);
        }
      }
      if (timerQueueTimer == null)
        return;
      timerQueueTimer.Fire();
    }

    [SecuritySafeCritical]
    private static void QueueTimerCompletion(TimerQueueTimer timer)
    {
      WaitCallback callBack = TimerQueue.s_fireQueuedTimerCompletion;
      if (callBack == null)
        TimerQueue.s_fireQueuedTimerCompletion = callBack = new WaitCallback(TimerQueue.FireQueuedTimerCompletion);
      ThreadPool.UnsafeQueueUserWorkItem(callBack, (object) timer);
    }

    private static void FireQueuedTimerCompletion(object state)
    {
      ((TimerQueueTimer) state).Fire();
    }

    public bool UpdateTimer(TimerQueueTimer timer, uint dueTime, uint period)
    {
      if ((int) timer.m_dueTime == -1)
      {
        timer.m_next = this.m_timers;
        timer.m_prev = (TimerQueueTimer) null;
        if (timer.m_next != null)
          timer.m_next.m_prev = timer;
        this.m_timers = timer;
      }
      timer.m_dueTime = dueTime;
      timer.m_period = (int) period == 0 ? uint.MaxValue : period;
      timer.m_startTicks = TimerQueue.TickCount;
      return this.EnsureAppDomainTimerFiresBy(dueTime);
    }

    public void DeleteTimer(TimerQueueTimer timer)
    {
      if ((int) timer.m_dueTime == -1)
        return;
      if (timer.m_next != null)
        timer.m_next.m_prev = timer.m_prev;
      if (timer.m_prev != null)
        timer.m_prev.m_next = timer.m_next;
      if (this.m_timers == timer)
        this.m_timers = timer.m_next;
      timer.m_dueTime = uint.MaxValue;
      timer.m_period = uint.MaxValue;
      timer.m_startTicks = 0;
      timer.m_prev = (TimerQueueTimer) null;
      timer.m_next = (TimerQueueTimer) null;
    }

    [SecurityCritical]
    private class AppDomainTimerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
      public AppDomainTimerSafeHandle()
        : base(true)
      {
      }

      [SecurityCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      protected override bool ReleaseHandle()
      {
        return TimerQueue.DeleteAppDomainTimer(this.handle);
      }
    }
  }
}
