// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.Internal.Environment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Reflection;
using System.Resources;

namespace System.Diagnostics.Tracing.Internal
{
  internal static class Environment
  {
    public static readonly string NewLine = System.Environment.NewLine;
    private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof (Environment).Assembly());

    public static int TickCount
    {
      get
      {
        return System.Environment.TickCount;
      }
    }

    public static string GetResourceString(string key, params object[] args)
    {
      string @string = Environment.rm.GetString(key);
      if (@string != null)
        return string.Format(@string, args);
      string str = string.Empty;
      foreach (object obj in args)
      {
        if (str != string.Empty)
          str += ", ";
        str += obj.ToString();
      }
      return key + " (" + str + ")";
    }

    public static string GetRuntimeResourceString(string key, params object[] args)
    {
      return Environment.GetResourceString(key, args);
    }
  }
}
