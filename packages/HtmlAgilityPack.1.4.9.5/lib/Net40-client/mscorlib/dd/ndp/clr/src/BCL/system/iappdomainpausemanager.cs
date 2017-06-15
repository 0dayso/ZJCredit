// Decompiled with JetBrains decompiler
// Type: System.AppDomainPauseManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System
{
  [SecurityCritical]
  internal class AppDomainPauseManager
  {
    private static readonly AppDomainPauseManager instance = new AppDomainPauseManager();
    private static volatile bool isPaused;

    internal static AppDomainPauseManager Instance
    {
      [SecurityCritical] get
      {
        return AppDomainPauseManager.instance;
      }
    }

    internal static bool IsPaused
    {
      [SecurityCritical] get
      {
        return AppDomainPauseManager.isPaused;
      }
    }

    internal static ManualResetEvent ResumeEvent { [SecurityCritical] get; [SecurityCritical] set; }

    [SecurityCritical]
    static AppDomainPauseManager()
    {
    }

    [SecurityCritical]
    public AppDomainPauseManager()
    {
      AppDomainPauseManager.isPaused = false;
    }

    [SecurityCritical]
    public void Pausing()
    {
    }

    [SecurityCritical]
    public void Paused()
    {
      if (AppDomainPauseManager.ResumeEvent == null)
        AppDomainPauseManager.ResumeEvent = new ManualResetEvent(false);
      else
        AppDomainPauseManager.ResumeEvent.Reset();
      Timer.Pause();
      AppDomainPauseManager.isPaused = true;
    }

    [SecurityCritical]
    public void Resuming()
    {
      AppDomainPauseManager.isPaused = false;
      AppDomainPauseManager.ResumeEvent.Set();
    }

    [SecurityCritical]
    public void Resumed()
    {
      Timer.Resume();
    }
  }
}
