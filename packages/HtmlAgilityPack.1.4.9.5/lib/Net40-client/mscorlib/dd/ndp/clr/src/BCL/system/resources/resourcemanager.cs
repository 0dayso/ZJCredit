﻿// Decompiled with JetBrains decompiler
// Type: System.Resources.WindowsRuntimeResourceManagerBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Resources
{
  [FriendAccessAllowed]
  [SecurityCritical]
  internal class WindowsRuntimeResourceManagerBase
  {
    public virtual CultureInfo GlobalResourceContextBestFitCultureInfo
    {
      [SecurityCritical] get
      {
        return (CultureInfo) null;
      }
    }

    [SecurityCritical]
    public virtual bool Initialize(string libpath, string reswFilename, out PRIExceptionInfo exceptionInfo)
    {
      exceptionInfo = (PRIExceptionInfo) null;
      return false;
    }

    [SecurityCritical]
    public virtual string GetString(string stringName, string startingCulture, string neutralResourcesCulture)
    {
      return (string) null;
    }

    [SecurityCritical]
    public virtual bool SetGlobalResourceContextDefaultCulture(CultureInfo ci)
    {
      return false;
    }
  }
}
