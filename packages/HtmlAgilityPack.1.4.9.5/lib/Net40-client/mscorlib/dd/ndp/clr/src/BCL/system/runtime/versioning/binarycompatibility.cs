// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.BinaryCompatibility
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
  [FriendAccessAllowed]
  internal static class BinaryCompatibility
  {
    private static readonly BinaryCompatibility.BinaryCompatibilityMap s_map = new BinaryCompatibility.BinaryCompatibilityMap();
    private static TargetFrameworkId s_AppWasBuiltForFramework;
    private static int s_AppWasBuiltForVersion;
    private const char c_componentSeparator = ',';
    private const char c_keyValueSeparator = '=';
    private const char c_versionValuePrefix = 'v';
    private const string c_versionKey = "Version";
    private const string c_profileKey = "Profile";

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Phone_V7_1
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Phone_V7_1;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Phone_V8_0
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Phone_V8_0;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Desktop_V4_5
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Desktop_V4_5_1
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_1;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Desktop_V4_5_2
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_2;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Desktop_V4_5_3
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_3;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Desktop_V4_5_4
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_4;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Desktop_V5_0
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V5_0;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Silverlight_V4
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V4;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Silverlight_V5
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V5;
      }
    }

    [FriendAccessAllowed]
    internal static bool TargetsAtLeast_Silverlight_V6
    {
      [FriendAccessAllowed] get
      {
        return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V6;
      }
    }

    [FriendAccessAllowed]
    internal static TargetFrameworkId AppWasBuiltForFramework
    {
      [FriendAccessAllowed] get
      {
        if (BinaryCompatibility.s_AppWasBuiltForFramework == TargetFrameworkId.NotYetChecked)
          BinaryCompatibility.ReadTargetFrameworkId();
        return BinaryCompatibility.s_AppWasBuiltForFramework;
      }
    }

    [FriendAccessAllowed]
    internal static int AppWasBuiltForVersion
    {
      [FriendAccessAllowed] get
      {
        if (BinaryCompatibility.s_AppWasBuiltForFramework == TargetFrameworkId.NotYetChecked)
          BinaryCompatibility.ReadTargetFrameworkId();
        return BinaryCompatibility.s_AppWasBuiltForVersion;
      }
    }

    private static bool ParseTargetFrameworkMonikerIntoEnum(string targetFrameworkMoniker, out TargetFrameworkId targetFramework, out int targetFrameworkVersion)
    {
      targetFramework = TargetFrameworkId.NotYetChecked;
      targetFrameworkVersion = 0;
      string identifier = (string) null;
      string profile = (string) null;
      BinaryCompatibility.ParseFrameworkName(targetFrameworkMoniker, out identifier, out targetFrameworkVersion, out profile);
      if (!(identifier == ".NETFramework"))
      {
        if (!(identifier == ".NETPortable"))
        {
          if (!(identifier == ".NETCore"))
          {
            if (!(identifier == "WindowsPhone"))
            {
              if (!(identifier == "WindowsPhoneApp"))
              {
                if (identifier == "Silverlight")
                {
                  targetFramework = TargetFrameworkId.Silverlight;
                  if (!string.IsNullOrEmpty(profile))
                  {
                    if (profile == "WindowsPhone")
                    {
                      targetFramework = TargetFrameworkId.Phone;
                      targetFrameworkVersion = 70000;
                    }
                    else if (profile == "WindowsPhone71")
                    {
                      targetFramework = TargetFrameworkId.Phone;
                      targetFrameworkVersion = 70100;
                    }
                    else if (profile == "WindowsPhone8")
                    {
                      targetFramework = TargetFrameworkId.Phone;
                      targetFrameworkVersion = 80000;
                    }
                    else if (profile.StartsWith("WindowsPhone", StringComparison.Ordinal))
                    {
                      targetFramework = TargetFrameworkId.Unrecognized;
                      targetFrameworkVersion = 70100;
                    }
                    else
                      targetFramework = TargetFrameworkId.Unrecognized;
                  }
                }
                else
                  targetFramework = TargetFrameworkId.Unrecognized;
              }
              else
                targetFramework = TargetFrameworkId.Phone;
            }
            else
              targetFramework = targetFrameworkVersion < 80100 ? TargetFrameworkId.Unspecified : TargetFrameworkId.Phone;
          }
          else
            targetFramework = TargetFrameworkId.NetCore;
        }
        else
          targetFramework = TargetFrameworkId.Portable;
      }
      else
        targetFramework = TargetFrameworkId.NetFramework;
      return true;
    }

    private static void ParseFrameworkName(string frameworkName, out string identifier, out int version, out string profile)
    {
      if (frameworkName == null)
        throw new ArgumentNullException("frameworkName");
      if (frameworkName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "frameworkName");
      string[] strArray1 = frameworkName.Split(',');
      version = 0;
      if (strArray1.Length < 2 || strArray1.Length > 3)
        throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameTooShort"), "frameworkName");
      identifier = strArray1[0].Trim();
      if (identifier.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameInvalid"), "frameworkName");
      bool flag = false;
      profile = (string) null;
      for (int index1 = 1; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split('=');
        if (strArray2.Length != 2)
          throw new ArgumentException(Environment.GetResourceString("SR.Argument_FrameworkNameInvalid"), "frameworkName");
        int index2 = 0;
        string str = strArray2[index2].Trim();
        int index3 = 1;
        string version1 = strArray2[index3].Trim();
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
            throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameInvalid"), "frameworkName");
          if (!string.IsNullOrEmpty(version1))
            profile = version1;
        }
      }
      if (!flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameMissingVersion"), "frameworkName");
    }

    [SecuritySafeCritical]
    private static void ReadTargetFrameworkId()
    {
      string targetFrameworkMoniker = AppDomain.CurrentDomain.GetTargetFrameworkName();
      string valueInternal = CompatibilitySwitch.GetValueInternal("TargetFrameworkMoniker");
      if (!string.IsNullOrEmpty(valueInternal))
        targetFrameworkMoniker = valueInternal;
      int targetFrameworkVersion = 0;
      TargetFrameworkId targetFramework;
      if (targetFrameworkMoniker == null)
        targetFramework = TargetFrameworkId.Unspecified;
      else if (!BinaryCompatibility.ParseTargetFrameworkMonikerIntoEnum(targetFrameworkMoniker, out targetFramework, out targetFrameworkVersion))
        targetFramework = TargetFrameworkId.Unrecognized;
      BinaryCompatibility.s_AppWasBuiltForFramework = targetFramework;
      BinaryCompatibility.s_AppWasBuiltForVersion = targetFrameworkVersion;
    }

    private sealed class BinaryCompatibilityMap
    {
      internal bool TargetsAtLeast_Phone_V7_1;
      internal bool TargetsAtLeast_Phone_V8_0;
      internal bool TargetsAtLeast_Phone_V8_1;
      internal bool TargetsAtLeast_Desktop_V4_5;
      internal bool TargetsAtLeast_Desktop_V4_5_1;
      internal bool TargetsAtLeast_Desktop_V4_5_2;
      internal bool TargetsAtLeast_Desktop_V4_5_3;
      internal bool TargetsAtLeast_Desktop_V4_5_4;
      internal bool TargetsAtLeast_Desktop_V5_0;
      internal bool TargetsAtLeast_Silverlight_V4;
      internal bool TargetsAtLeast_Silverlight_V5;
      internal bool TargetsAtLeast_Silverlight_V6;

      internal BinaryCompatibilityMap()
      {
        this.AddQuirksForFramework(BinaryCompatibility.AppWasBuiltForFramework, BinaryCompatibility.AppWasBuiltForVersion);
      }

      private void AddQuirksForFramework(TargetFrameworkId builtAgainstFramework, int buildAgainstVersion)
      {
        switch (builtAgainstFramework)
        {
          case TargetFrameworkId.NetFramework:
          case TargetFrameworkId.NetCore:
            if (buildAgainstVersion >= 50000)
              this.TargetsAtLeast_Desktop_V5_0 = true;
            if (buildAgainstVersion >= 40504)
              this.TargetsAtLeast_Desktop_V4_5_4 = true;
            if (buildAgainstVersion >= 40503)
              this.TargetsAtLeast_Desktop_V4_5_3 = true;
            if (buildAgainstVersion >= 40502)
              this.TargetsAtLeast_Desktop_V4_5_2 = true;
            if (buildAgainstVersion >= 40501)
              this.TargetsAtLeast_Desktop_V4_5_1 = true;
            if (buildAgainstVersion < 40500)
              break;
            this.TargetsAtLeast_Desktop_V4_5 = true;
            this.AddQuirksForFramework(TargetFrameworkId.Phone, 70100);
            this.AddQuirksForFramework(TargetFrameworkId.Silverlight, 50000);
            break;
          case TargetFrameworkId.Silverlight:
            if (buildAgainstVersion >= 40000)
              this.TargetsAtLeast_Silverlight_V4 = true;
            if (buildAgainstVersion >= 50000)
              this.TargetsAtLeast_Silverlight_V5 = true;
            if (buildAgainstVersion < 60000)
              break;
            this.TargetsAtLeast_Silverlight_V6 = true;
            break;
          case TargetFrameworkId.Phone:
            if (buildAgainstVersion >= 80000)
              this.TargetsAtLeast_Phone_V8_0 = true;
            if (buildAgainstVersion >= 80100)
            {
              this.TargetsAtLeast_Desktop_V4_5 = true;
              this.TargetsAtLeast_Desktop_V4_5_1 = true;
            }
            if (buildAgainstVersion < 710)
              break;
            this.TargetsAtLeast_Phone_V7_1 = true;
            break;
        }
      }
    }
  }
}
