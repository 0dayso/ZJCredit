// Decompiled with JetBrains decompiler
// Type: System.AppContextSwitches
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  internal static class AppContextSwitches
  {
    private static int _noAsyncCurrentCulture;
    private static int _throwExceptionIfDisposedCancellationTokenSource;
    private static int _preserveEventListnerObjectIdentity;

    public static bool NoAsyncCurrentCulture
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchNoAsyncCurrentCulture, ref AppContextSwitches._noAsyncCurrentCulture);
      }
    }

    public static bool ThrowExceptionIfDisposedCancellationTokenSource
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchThrowExceptionIfDisposedCancellationTokenSource, ref AppContextSwitches._throwExceptionIfDisposedCancellationTokenSource);
      }
    }

    public static bool PreserveEventListnerObjectIdentity
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchPreserveEventListnerObjectIdentity, ref AppContextSwitches._preserveEventListnerObjectIdentity);
      }
    }

    private static bool DisableCaching { get; set; }

    static AppContextSwitches()
    {
      bool isEnabled;
      if (!AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out isEnabled))
        return;
      AppContextSwitches.DisableCaching = isEnabled;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
    {
      if (switchValue < 0)
        return false;
      if (switchValue > 0)
        return true;
      return AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue);
    }

    private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
    {
      bool isEnabled;
      AppContext.TryGetSwitch(switchName, out isEnabled);
      if (AppContextSwitches.DisableCaching)
        return isEnabled;
      switchValue = isEnabled ? 1 : -1;
      return isEnabled;
    }
  }
}
