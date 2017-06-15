// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityAction
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定可以使用声明安全性执行的安全操作。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum SecurityAction
  {
    Demand = 2,
    Assert = 3,
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")] Deny = 4,
    PermitOnly = 5,
    LinkDemand = 6,
    InheritanceDemand = 7,
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")] RequestMinimum = 8,
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")] RequestOptional = 9,
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")] RequestRefuse = 10,
  }
}
