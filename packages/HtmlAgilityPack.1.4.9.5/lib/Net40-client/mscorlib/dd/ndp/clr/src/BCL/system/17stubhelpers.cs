// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.CleanupWorkListElement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
  [SecurityCritical]
  internal sealed class CleanupWorkListElement
  {
    public SafeHandle m_handle;
    public bool m_owned;

    public CleanupWorkListElement(SafeHandle handle)
    {
      this.m_handle = handle;
    }
  }
}
