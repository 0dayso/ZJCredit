// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentStringExpressionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Util;

namespace System.Security.Permissions
{
  [Serializable]
  internal class EnvironmentStringExpressionSet : StringExpressionSet
  {
    public EnvironmentStringExpressionSet()
      : base(true, (string) null, false)
    {
    }

    public EnvironmentStringExpressionSet(string str)
      : base(true, str, false)
    {
    }

    protected override StringExpressionSet CreateNewEmpty()
    {
      return (StringExpressionSet) new EnvironmentStringExpressionSet();
    }

    protected override bool StringSubsetString(string left, string right, bool ignoreCase)
    {
      if (!ignoreCase)
        return string.Compare(left, right, StringComparison.Ordinal) == 0;
      return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
    }

    protected override string ProcessWholeString(string str)
    {
      return str;
    }

    protected override string ProcessSingleString(string str)
    {
      return str;
    }

    [SecuritySafeCritical]
    public override string ToString()
    {
      return this.UnsafeToString();
    }
  }
}
