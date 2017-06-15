// Decompiled with JetBrains decompiler
// Type: System.AppContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System
{
  /// <summary>为成员提供用于设置和检索有关应用程序的上下文数据。</summary>
  public static class AppContext
  {
    private static Dictionary<string, AppContext.SwitchValueState> s_switchMap = new Dictionary<string, AppContext.SwitchValueState>();
    private static readonly object s_syncLock = new object();

    /// <summary>获取程序集冲突解决程序用来探测程序集的基目录的路径名。</summary>
    /// <returns>该程序集冲突解决程序用来探测程序集的基目录的路径名。</returns>
    public static string BaseDirectory
    {
      get
      {
        return (string) AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") ?? AppDomain.CurrentDomain.BaseDirectory;
      }
    }

    static AppContext()
    {
      AppContextDefaultValues.PopulateDefaultValues();
    }

    /// <summary>Trues 来获取一个开关的值。</summary>
    /// <returns>true如果<paramref name="switchName" />设置和<paramref name="isEnabled" />参数中包含的开关 ； 值否则为false。</returns>
    /// <param name="switchName">开关的名称。</param>
    /// <param name="isEnabled">此方法返回时，包含值的<paramref name="switchName" />如果<paramref name="switchName" />找，或false如果<paramref name="switchName" />找不到。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="switchName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="switchName" /> 为 <see cref="F:System.String.Empty" />。</exception>
    public static bool TryGetSwitch(string switchName, out bool isEnabled)
    {
      if (switchName == null)
        throw new ArgumentNullException("switchName");
      if (switchName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "switchName");
      isEnabled = false;
      lock (AppContext.s_switchMap)
      {
        AppContext.SwitchValueState local_0;
        if (AppContext.s_switchMap.TryGetValue(switchName, out local_0))
        {
          if (local_0 == AppContext.SwitchValueState.UnknownValue)
          {
            isEnabled = false;
            return false;
          }
          isEnabled = (local_0 & AppContext.SwitchValueState.HasTrueValue) == AppContext.SwitchValueState.HasTrueValue;
          if ((local_0 & AppContext.SwitchValueState.HasLookedForOverride) == AppContext.SwitchValueState.HasLookedForOverride)
            return true;
          bool local_3;
          if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out local_3))
            isEnabled = local_3;
          AppContext.s_switchMap[switchName] = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
          return true;
        }
        bool local_5;
        if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out local_5))
        {
          isEnabled = local_5;
          AppContext.s_switchMap[switchName] = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
          return true;
        }
        AppContext.s_switchMap[switchName] = AppContext.SwitchValueState.UnknownValue;
      }
      return false;
    }

    /// <summary>设置一个开关的值。</summary>
    /// <param name="switchName">开关的名称。</param>
    /// <param name="isEnabled">开关的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="switchName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="switchName" /> 为 <see cref="F:System.String.Empty" />。</exception>
    public static void SetSwitch(string switchName, bool isEnabled)
    {
      if (switchName == null)
        throw new ArgumentNullException("switchName");
      if (switchName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "switchName");
      lock (AppContext.s_syncLock)
        AppContext.s_switchMap[switchName] = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
    }

    internal static void DefineSwitchDefault(string switchName, bool isEnabled)
    {
      AppContext.s_switchMap[switchName] = isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue;
    }

    [Flags]
    private enum SwitchValueState
    {
      HasFalseValue = 1,
      HasTrueValue = 2,
      HasLookedForOverride = 4,
      UnknownValue = 8,
    }
  }
}
