// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DefaultFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Diagnostics
{
  internal class DefaultFilter : AssertFilter
  {
    internal DefaultFilter()
    {
    }

    [SecuritySafeCritical]
    public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
    {
      return (AssertFilters) Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
    }
  }
}
