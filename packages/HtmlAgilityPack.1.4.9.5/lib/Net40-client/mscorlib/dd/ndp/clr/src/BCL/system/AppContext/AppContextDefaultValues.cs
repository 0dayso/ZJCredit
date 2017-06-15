// Decompiled with JetBrains decompiler
// Type: System.AppContextDefaultValues
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  internal static class AppContextDefaultValues
  {
    internal static readonly string SwitchNoAsyncCurrentCulture = "Switch.System.Globalization.NoAsyncCurrentCulture";
    internal static readonly string SwitchThrowExceptionIfDisposedCancellationTokenSource = "Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource";
    internal static readonly string SwitchPreserveEventListnerObjectIdentity = "Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity";
    private static RegistryKey s_switchesRegKey = AppContextDefaultValues.OpenRegKeyNoThrow();

    public static void PopulateDefaultValues()
    {
      string identifier;
      string profile;
      int version;
      AppContextDefaultValues.ParseTargetFrameworkName(out identifier, out profile, out version);
      AppContextDefaultValues.PopulateDefaultValuesPartial(identifier, profile, version);
    }

    private static void ParseTargetFrameworkName(out string identifier, out string profile, out int version)
    {
      if (AppContextDefaultValues.TryParseFrameworkName(AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName, out identifier, out version, out profile))
        return;
      identifier = ".NETFramework";
      version = 40000;
      profile = string.Empty;
    }

    private static bool TryParseFrameworkName(string frameworkName, out string identifier, out int version, out string profile)
    {
      identifier = profile = string.Empty;
      version = 0;
      if (frameworkName == null || frameworkName.Length == 0)
        return false;
      string[] strArray1 = frameworkName.Split(',');
      version = 0;
      if (strArray1.Length < 2 || strArray1.Length > 3)
        return false;
      identifier = strArray1[0].Trim();
      if (identifier.Length == 0)
        return false;
      bool flag = false;
      profile = (string) null;
      for (int index = 1; index < strArray1.Length; ++index)
      {
        string[] strArray2 = strArray1[index].Split('=');
        if (strArray2.Length != 2)
          return false;
        string str = strArray2[0].Trim();
        string version1 = strArray2[1].Trim();
        if (str.Equals("Version", StringComparison.OrdinalIgnoreCase))
        {
          flag = true;
          if (version1.Length > 0 && ((int) version1[0] == 118 || (int) version1[0] == 86))
            version1 = version1.Substring(1);
          Version version2 = new Version(version1);
          version = version2.Major * 10000;
          if (version2.Minor > 0)
            version += version2.Minor * 100;
          if (version2.Build > 0)
            version += version2.Build;
        }
        else
        {
          if (!str.Equals("Profile", StringComparison.OrdinalIgnoreCase))
            return false;
          if (!string.IsNullOrEmpty(version1))
            profile = version1;
        }
      }
      return flag;
    }

    [SecuritySafeCritical]
    private static void TryGetSwitchOverridePartial(string switchName, ref bool overrideFound, ref bool overrideValue)
    {
      string str = (string) null;
      overrideFound = false;
      if (AppContextDefaultValues.s_switchesRegKey != null)
        str = AppContextDefaultValues.s_switchesRegKey.GetValue(switchName, (object) null) as string;
      if (str == null)
        str = CompatibilitySwitch.GetValue(switchName);
      bool result;
      if (str == null || !bool.TryParse(str, out result))
        return;
      overrideValue = result;
      overrideFound = true;
    }

    private static void PopulateDefaultValuesPartial(string platformIdentifier, string profile, int version)
    {
      if (!(platformIdentifier == ".NETCore") && !(platformIdentifier == ".NETFramework"))
      {
        if ((platformIdentifier == "WindowsPhone" || platformIdentifier == "WindowsPhoneApp") && version <= 80100)
        {
          AppContext.DefineSwitchDefault(AppContextDefaultValues.SwitchNoAsyncCurrentCulture, true);
          AppContext.DefineSwitchDefault(AppContextDefaultValues.SwitchThrowExceptionIfDisposedCancellationTokenSource, true);
        }
      }
      else if (version <= 40502)
      {
        AppContext.DefineSwitchDefault(AppContextDefaultValues.SwitchNoAsyncCurrentCulture, true);
        AppContext.DefineSwitchDefault(AppContextDefaultValues.SwitchThrowExceptionIfDisposedCancellationTokenSource, true);
      }
      AppContextDefaultValues.PopulateOverrideValuesPartial();
    }

    [SecuritySafeCritical]
    private static void PopulateOverrideValuesPartial()
    {
      string overridesInternalCall = CompatibilitySwitch.GetAppContextOverridesInternalCall();
      if (string.IsNullOrEmpty(overridesInternalCall))
        return;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      int num1 = -1;
      int num2 = -1;
      for (int index = 0; index <= overridesInternalCall.Length; ++index)
      {
        if (index == overridesInternalCall.Length || (int) overridesInternalCall[index] == 59)
        {
          if (flag1 & flag2 & flag3)
          {
            int startIndex1 = num1 + 1;
            int length1 = num2 - num1 - 1;
            string switchName = overridesInternalCall.Substring(startIndex1, length1);
            int startIndex2 = num2 + 1;
            int length2 = index - num2 - 1;
            bool result;
            if (bool.TryParse(overridesInternalCall.Substring(startIndex2, length2), out result))
              AppContext.SetSwitch(switchName, result);
          }
          num1 = index;
          int num3;
          flag1 = (num3 = 0) != 0;
          flag3 = num3 != 0;
          flag2 = num3 != 0;
        }
        else if ((int) overridesInternalCall[index] == 61)
        {
          if (!flag1)
          {
            flag1 = true;
            num2 = index;
          }
        }
        else if (flag1)
          flag3 = true;
        else
          flag2 = true;
      }
    }

    public static bool TryGetSwitchOverride(string switchName, out bool overrideValue)
    {
      overrideValue = false;
      bool overrideFound = false;
      AppContextDefaultValues.TryGetSwitchOverridePartial(switchName, ref overrideFound, ref overrideValue);
      return overrideFound;
    }

    private static RegistryKey OpenRegKeyNoThrow()
    {
      try
      {
        return Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\AppContext");
      }
      catch
      {
        return (RegistryKey) null;
      }
    }
  }
}
